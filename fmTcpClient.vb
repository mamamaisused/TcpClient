Imports System.Net.Sockets
Imports System.Threading

Public Class fmTcpClient

    Dim SendBuffer(0) As Byte       '发送缓存，后面会ReDim修改大小
    Dim ReadBuffer(255) As Byte     '接收缓存，后面会ReDim修改大小
    Dim tcpClient As TcpClient
    Dim rdStream As NetworkStream   '数据读入流
    Dim ReadThread As Thread        '读数据线程
    Dim WriteThread As Thread       '发数据线程
    Dim TcpConnected As Boolean     '连接状态
    Dim IsSending As Boolean        '发送标志位

    '刷新接收数据的委托
    Public Delegate Sub deleUpdateUIByte(_byte As Byte)
    '定义刷新界面的委托，用于跨线程修改界面显示
    Public Delegate Sub deleUpdateUIVoid()
    '用于切换主线程上Timer的委托
    Public Delegate Sub deleUpdateUIBoolean(state As Boolean)
    'Public Delegate Sub deleGetSendData(ByRef _bytes() As Byte)

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
    Public Sub ReadBuffered()
        Dim index As Integer = 0
        Dim pUpdataUIByte As New deleUpdateUIByte(AddressOf RefreshReceive)
        Dim pUpdataUIBoolean As New deleUpdateUIBoolean(AddressOf ToggleTimer)
        Try
            While rdStream.DataAvailable
                '有数据可以接收时停止计时器，否则可能与接收状态冲突
                '用委托方法跨线程启动/停止计时器
                Me.Invoke(pUpdataUIBoolean, False)
                '动态调整数组大小
                ReDim Preserve ReadBuffer(index)
                rdStream.Read(ReadBuffer, index, 1)
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
            ReadBuffered()
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
        IsSending = False
        TcpConnected = False
        btnSend.Enabled = False
        btnDisconnect.Enabled = False
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
        tslbStatus.BackColor = Color.Yellow
        tslbStatus.Text = "Offline"
        Timer1.Enabled = False
        '释放socket资源，否则没法进行下一次连接
        tcpClient.Close()
    End Sub

    Private Sub SetOnline()
        TcpConnected = True
        btnDisconnect.Enabled = True
        btnSend.Enabled = True
        btnConnect.Enabled = False
        tslbStatus.BackColor = Color.Green
        tslbStatus.Text = "Online"
        'Timer1用于判断连接是否存活
        Timer1.Enabled = True
        rdStream = tcpClient.GetStream()
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
End Class
