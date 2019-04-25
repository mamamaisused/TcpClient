Imports System.Net.Sockets
Imports System.Threading

Public Class fmTcpClient
    Dim sm As New SIMOTION.SimotionSystem("192.168.1.2", , 300)
    Dim mm As New SIMOTION.SimotionMotor("main", 0)
    Dim SendBuffer(0) As Byte       '发送缓存，后面会ReDim修改大小
    Dim ReadBuffer(255) As Byte     '接收缓存，后面会ReDim修改大小
    Dim udpReadBuffer(0) As Byte
    Dim tcpClient As TcpClient
    Dim udpClient As UdpClient
    Dim tcp_rdStream As NetworkStream   '数据读入流
    Dim ReadThread As Thread        '读数据线程
    Dim WriteThread As Thread       '发数据线程
    Dim TcpConnected As Boolean     '连接状态
    Dim IsSending As Boolean        '发送标志位
    Dim DeviceState As mDeviceState
    Const udpBufferSize As Integer = 11
    Const udpPort As Integer = 8000
    '68H表示帧头，00H表示控制的是ALM，01H表示控制启停，01H表示启动
    Dim fmEnableALM = New Byte() {&H68, &H0, &H1, &H1}
    '68H表示帧头，01H表示控制的是输入电机/02表示负载电机，01H表示控制启停，01H表示启动/FFH表示反转/00H表示停止
    Dim fmEnableMotor = New Byte() {&H68, &H1, &H1, &H1}
    '68H表示帧头，01H表示控制的是输入电机/02表示负载电机，02H表示设定速度，最后两位表示速度值。
    Dim fmSetSpeed = New Byte() {&H68, &H1, &H2, &H0, &H30}

    Structure mDeviceState
        Dim ALMState As Integer
        Dim MasterDirect As String
        Dim MasterSpeed As Double
        Dim LoadDirect As String
        Dim LoadSpeed As Double
    End Structure

    '刷新接收数据的委托
    Public Delegate Sub deleUpdateUIByte(_byte As Byte)
    '定义刷新界面的委托，用于跨线程修改界面显示
    Public Delegate Sub deleUpdateUIVoid()
    '用于切换主线程上Timer的委托
    Public Delegate Sub deleUpdateUIBoolean(state As Boolean)
    '刷新显示S120系统状态
    Public Delegate Sub deleUpdateUIStruct(_struct As mDeviceState)

    Public Sub udpInit()
        udpClient = New UdpClient(udpPort)
        udpClient.Client.ReceiveBufferSize = udpBufferSize
        ReDim udpReadBuffer(udpBufferSize - 1)
        'System.Net.IPAddress.Parse(txtIP.Text)
    End Sub

    Public Sub udpReadFrame()
        If TcpConnected Then
            If udpClient.Available Then
                Dim IP As System.Net.IPAddress = System.Net.IPAddress.Parse(txtIP.Text)
                Dim EndPoint As New System.Net.IPEndPoint(IP, udpPort)
                udpReadBuffer = udpClient.Receive(EndPoint)
                udpFrameResolve(udpReadBuffer)
                Dim deleUpdateDeviceState As New deleUpdateUIStruct(AddressOf UpdateDeviceState)
                Me.Invoke(deleUpdateDeviceState, DeviceState)
            End If
        End If
    End Sub

    Public Sub udpFrameResolve(_data() As Byte)
        If _data(0) = &H68 And _data(1) = 0 Then
            DeviceState.ALMState = _data(2)
            DeviceState.MasterSpeed = (_data(4) * 65536 + _data(5) * 256 + _data(6)) / 100
            DeviceState.LoadSpeed = (_data(8) * 65536 + _data(9) * 256 + _data(10)) / 100
            If _data(3) = 0 Then
                DeviceState.MasterDirect = "Stop"
            ElseIf _data(3) = 1 Then
                DeviceState.MasterDirect = "Forward"
            Else
                DeviceState.MasterDirect = "Backward"
            End If
            If _data(7) = 0 Then
                DeviceState.LoadDirect = "Stop"
            ElseIf _data(7) = 1 Then
                DeviceState.LoadDirect = "Forward"
            Else
                DeviceState.LoadDirect = "Backward"
            End If
        End If
    End Sub

    Public Sub UpdateDeviceState(_struct As mDeviceState)
        If _struct.ALMState > 0 Then
            tslbALMState.BackColor = Color.LightGreen
            tslbALMState.Text = "ON"
        Else
            tslbALMState.BackColor = Color.Yellow
            tslbALMState.Text = "OFF"
        End If
        If tslbALMState.Text = "ON" Then
            pnMotorControl.Enabled = True
            btnEnableALM.Text = "Disable ALM"
        Else
            pnMotorControl.Enabled = False
            btnEnableALM.Text = "Enable ALM"
        End If
        txtLoadRPM.Text = DeviceState.LoadSpeed
        txtMasterRPM.Text = DeviceState.MasterSpeed
        txtMasterDirection.Text = DeviceState.MasterDirect
        txtLoadDirection.Text = DeviceState.LoadDirect
    End Sub

    ''' <summary>
    ''' 切换主进程的计时器状态
    ''' </summary>
    ''' <param name="state">state 为 True 启动计时器</param>
    ''' <remarks></remarks>
    Public Sub ToggleTimer(state As Boolean)
        If state Then
            Timer1.Start()
        Else
            Timer1.Stop()
        End If
    End Sub

    ''' <summary>
    ''' 接收到数据后刷新显示
    ''' </summary>
    ''' <param name="_byte">_byte为接收到的数据</param>
    ''' <remarks></remarks>
    Public Sub RefreshReceive(_byte As Byte)
        txtReceived.AppendText(_byte.ToString("X2") + " ")
    End Sub

    ''' <summary>
    ''' 从UI中的文本框中获取要发送的数据，并将String类型转换为Byte类型
    ''' </summary>
    ''' <param name="_bytes">转换后存入_bytes数组中</param>
    ''' <remarks></remarks>
    Public Sub GetSendData(ByRef _bytes() As Byte)
        '调整数组大小为发送区长度
        '按照字符长度来Redim生成的数组大小会比实际需要的数组大小多1，因为多了下标0
        ReDim _bytes(txtSend.Text.Length / 2 - 1)
        If txtSend.Text <> "" Then
            For i As Integer = 1 To txtSend.Text.Length Step 2
                'Mid 方法是从 1下标开始截取的
                Try
                    _bytes((i - 1) / 2) = Convert.ToByte(Mid(txtSend.Text, i, 2), 16)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Next
        End If
    End Sub

    ''' <summary>
    ''' 从以太网接收缓存中读取数据
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub tcpReadBuffered()
        Dim index As Integer = 0
        Dim pUpdataUIByte As New deleUpdateUIByte(AddressOf RefreshReceive)
        Dim pUpdataUIBoolean As New deleUpdateUIBoolean(AddressOf ToggleTimer)
        Try
            While tcp_rdStream.DataAvailable
                '有数据可以接收时停止计时器，否则可能与接收状态冲突
                '用委托方法跨线程启动/停止计时器
                Me.Invoke(pUpdataUIBoolean, False)
                '动态调整数组大小
                ReDim Preserve ReadBuffer(index)
                tcp_rdStream.Read(ReadBuffer, index, 1)
                Me.Invoke(pUpdataUIByte, ReadBuffer(index))
                index += 1
            End While
            If Timer1.Enabled = False Then
                Me.Invoke(pUpdataUIBoolean, True)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 读线程中启动的方法
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReadThreadFunc()
        While TcpConnected
            tcpReadBuffered()
            'udpReadFrame()
        End While
    End Sub

    ''' <summary>
    ''' 写线程中启动的方法
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub WriteThreadFunc()
        While TcpConnected
            '不断轮询标志位看是否有数据要发送
            If IsSending Then
                Dim _buf(0) As Byte
                Dim _e As New SocketAsyncEventArgs()
                '异步发送完成后的处理程序，无论是否发送成功都触发该事件
                AddHandler _e.Completed, AddressOf WriteAsyncComplete
                '从发送的TEXT控件中获取发送数据保存到_buf中
                GetSendData(_buf)
                '_e作为异步发送的形参，需要设置发送数据
                _e.SetBuffer(_buf, 0, _buf.Length)
                '异步发送，因为是单独开的线程，用同步发送也可以，同步发送会阻塞线程直到发送成功或超时
                tcpClient.Client.SendAsync(_e)
                IsSending = False
            End If
        End While
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '初始化变量和控件
        tslbStatus.BackColor = Color.Yellow
        tslbStatus.Text = "Offline"
        tslbALMState.BackColor = Color.Yellow
        tslbALMState.Text = "OFF"
        IsSending = False
        TcpConnected = False
        btnSend.Enabled = False
        btnDisconnect.Enabled = False
        pnMotorControl.Enabled = True
        pnMotorForm.Enabled = True
        udpInit()


        Debug.Print(sm.IPAddress)
        PropertyGrid1.SelectedObject = sm
        PropertyGrid2.SelectedObject = mm
        mm.AxisName = "ll"
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        '判断连接是否断开
        If tcpClient.Client Is Nothing Then
            SetOffline()
        Else
            If tcpClient.Client.Poll(1000, SelectMode.SelectRead) Then
                SetOffline()
            End If
        End If
    End Sub

    Private Sub WriteAsyncComplete(sender As Object, e As SocketAsyncEventArgs)
        Debug.Print("Send Finish.")
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        '如果调用了tcpClient.Close方法的话实例会被释放，因此每次重新new一下进行实例化
        tcpClient = New TcpClient()
        tslbStatus.Text = "Connecting ..."
        tslbStatus.BackColor = Color.Transparent
        'tcpClient.Connetc方法是阻塞的，所以用异步方法
        Try
            If (Not tcpClient.ConnectAsync(System.Net.IPAddress.Parse(txtIP.Text), txtPort.Text).Wait(1000)) Then
                MsgBox("Connecting Time Out.")
                SetOffline()
            Else
                SetOnline()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            tslbStatus.Text = "Error"
        End Try
    End Sub

    Private Sub SetOffline()
        TcpConnected = False
        btnSend.Enabled = False
        btnDisconnect.Enabled = False
        btnConnect.Enabled = True
        pnMotorForm.Enabled = False
        tslbStatus.BackColor = Color.Yellow
        tslbStatus.Text = "Offline"
        tslbALMState.BackColor = Color.Yellow
        tslbALMState.Text = "OFF"
        Timer1.Enabled = False
        '释放socket资源，否则没法进行下一次连接
        tcpClient.Close()
    End Sub

    Private Sub SetOnline()
        TcpConnected = True
        btnDisconnect.Enabled = True
        btnSend.Enabled = True
        btnConnect.Enabled = False
        tslbStatus.BackColor = Color.LightGreen
        tslbStatus.Text = "Online"
        pnMotorForm.Enabled = True
        'Timer1用于判断连接是否存活
        Timer1.Enabled = True
        tcp_rdStream = tcpClient.GetStream()
        ReadThread = New Thread(AddressOf ReadThreadFunc)
        WriteThread = New Thread(AddressOf WriteThreadFunc)
        ReadThread.Start()
        WriteThread.Start()
    End Sub

    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        If txtSend.Text <> "" Then
            If txtSend.Text.Length Mod 2 = 1 Then
                MsgBox("Data Length must be Even Number.")
            Else
                IsSending = True
            End If
        Else
            MsgBox("Send Data Empty.")
        End If

    End Sub

    Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click
        SetOffline()
    End Sub
    
    Private Sub timer_udpReader_Tick(sender As Object, e As EventArgs) Handles timer_udpReader.Tick
        udpReadFrame()
    End Sub

    Private Sub btnEnableALM_Click(sender As Object, e As EventArgs) Handles btnEnableALM.Click
        If TcpConnected Then
            If DeviceState.ALMState = 0 Then
                fmEnableALM(3) = &H1
            Else
                fmEnableALM(3) = 0
            End If
            SetSendText(fmEnableALM)
            IsSending = True
        End If
    End Sub

    ''' <summary>
    ''' 将需要发送的报文添加到发送对话框
    ''' </summary>
    ''' <param name="_data"></param>
    ''' <remarks></remarks>
    Public Sub SetSendText(_data As Byte())
        txtSend.Text = ""
        For Each item As Byte In _data
            txtSend.AppendText(item.ToString("X2"))
        Next
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        fmEnableMotor(3) = 0
        If DeviceState.MasterDirect <> "Stop" Then
            fmEnableMotor(1) = 1
        End If
        'Thread.Sleep(50) '防止数据粘连
        If DeviceState.LoadDirect <> "Stop" Then
            fmEnableMotor(1) = 2
        End If
        SetSendText(fmEnableMotor)
        IsSending = True
    End Sub

    Private Sub btnForward_Click(sender As Object, e As EventArgs) Handles btnForward.Click
        fmEnableMotor(3) = 1
        If cboxAxis.Text = "Axis_Load" Then
            fmEnableMotor(1) = 2
        Else
            fmEnableMotor(1) = 1
        End If
        SetSendText(fmEnableMotor)
        IsSending = True
    End Sub

    Private Sub btnBackward_Click(sender As Object, e As EventArgs) Handles btnBackward.Click
        fmEnableMotor(3) = &HFF
        If cboxAxis.Text = "Axis_Load" Then
            fmEnableMotor(1) = 2
        Else
            fmEnableMotor(1) = 1
        End If
        SetSendText(fmEnableMotor)
        IsSending = True
    End Sub

    Private Sub btnSetSpeed_Click(sender As Object, e As EventArgs) Handles btnSetSpeed.Click
        If cboxAxis.Text = "Axis_Load" Then
            fmSetSpeed(1) = 2
        Else
            fmSetSpeed(1) = 1
        End If
        fmSetSpeed(4) = Convert.ToInt16(txtSetSpeed.Text) Mod 256
        fmSetSpeed(3) = (Convert.ToInt16(txtSetSpeed.Text) - fmSetSpeed(4)) / 256
        SetSendText(fmSetSpeed)
        IsSending = True
    End Sub
End Class
