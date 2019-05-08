Imports System.Net.Sockets
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Namespace SIMOTION
    '*******************************************************
    '********************SIMOTION系统***********************
    '*******************************************************
    Public Class SimotionSystem
        ''' <summary>
        ''' 实例化函数
        ''' </summary>
        ''' <param name="valIPAddress">系统的IP地址</param>
        ''' <param name="valLocalTcpPort">本地的TCP端口号</param>
        ''' <param name="valRemoteUdpPort">远端的UDP端口号</param>
        ''' <remarks></remarks>
        Public Sub New(valIPAddress As String, Optional valLocalTcpPort As Integer = 3000, Optional valRemoteUdpPort As Integer = 8000)
            _IPAddress = System.Net.IPAddress.Parse(valIPAddress)
            LocalTcpPort = valLocalTcpPort
            RemoteUdpPort = valRemoteUdpPort
            UdpEndPoint = New System.Net.IPEndPoint(_IPAddress, RemoteUdpPort)
            UdpComm = New UdpClient(RemoteUdpPort)
            UdpComm.Client.ReceiveBufferSize = Marshal.SizeOf(Axis_Master.g_Axis_Status)
            ReDim _UdpReadBuffer(Marshal.SizeOf(Axis_Master.g_Axis_Status) - 1)
        End Sub

#Region "TCP通信"

        Public TcpComm As TcpClient
        Private _TcpConnected As Boolean = False

        <DescriptionAttribute("本地TCP端口号")> _
        Public Property LocalTcpPort As Integer

        <DescriptionAttribute("TCP/IP连接状态")> _
        Public ReadOnly Property TcpConnected As Boolean
            Get
                Return _TcpConnected
            End Get
        End Property

        <DescriptionAttribute("TCP发送缓存")> _
        Public Property TcpSendBuffer As Byte() = New Byte(0) {0}

        Private _TcpSendBufferSize As Integer
        Public Property TcpSendBufferSize As Integer
            Get
                Return _TcpSendBufferSize
            End Get
            Set(value As Integer)
                ReDim TcpSendBuffer(value)
                _TcpSendBufferSize = value
            End Set
        End Property

        ''' <summary>
        ''' 与SIMOTION系统建立连接
        ''' </summary>
        ''' <param name="valTimeOut">连接的超时时间，默认1.5秒</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function TcpConnect(Optional valTimeOut As Integer = 1500) As Boolean
            If TcpComm Is Nothing Then
                TcpComm = New TcpClient()
            ElseIf TcpComm.Client Is Nothing Then
                TcpComm = New TcpClient()
            End If
            Try
                If (Not TcpComm.ConnectAsync(_IPAddress, LocalTcpPort).Wait(valTimeOut)) Then
                    MsgBox("Connecting Time Out.")
                    TcpSetOffline()
                    Return False
                Else
                    TcpSetOnline()
                    Return True
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
                TcpSetOffline()
                Return False
            End Try
        End Function

        ''' <summary>
        ''' 断开TCP连接
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub TcpClose()
            TcpSetOffline()
        End Sub

        '设置为上线状态，配置TcpClient
        Private Sub TcpSetOnline()
            _TcpConnected = True
        End Sub
        '设置为离线状态，释放资源
        Private Sub TcpSetOffline()
            _TcpConnected = False
            If TcpComm IsNot Nothing Then
                TcpComm.Close()
            End If
        End Sub

        ''' <summary>
        ''' 查询当前的连接状态
        ''' </summary>
        ''' <returns>在线返回true，掉线返回false</returns>
        ''' <remarks></remarks>
        Public Function GetTcpConnectState() As Boolean
            If TcpComm.Client Is Nothing Then
                TcpSetOffline()
                Return False
            Else
                If TcpComm.Client.Poll(1000, SelectMode.SelectRead) Then
                    TcpSetOffline()
                    Return False
                Else
                    TcpSetOnline()
                    Return True
                End If
            End If
        End Function

        ''' <summary>
        ''' 发送TcpSendBuffer中的数据
        ''' </summary>
        ''' <remarks></remarks>
        Public Overloads Sub TcpSend()
            TcpSend(TcpSendBuffer)
        End Sub

        ''' <summary>
        ''' 发送帧数据
        ''' </summary>
        ''' <param name="valSendBuffer">待发送的数据</param>
        ''' <remarks></remarks>
        Public Overloads Sub TcpSend(valSendBuffer As Byte())
            Dim _e As New SocketAsyncEventArgs()
            AddHandler _e.Completed, AddressOf TcpWriteAsyncComplete
            Try
                _e.SetBuffer(valSendBuffer, 0, valSendBuffer.Length)
                TcpComm.Client.SendAsync(_e)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End Sub

        Private Sub TcpWriteAsyncComplete(sender As Object, e As SocketAsyncEventArgs)
            Debug.Print("Tcp Send.")
        End Sub

#End Region

#Region "UDP通信"
        Public UdpComm As UdpClient
        Public UdpEndPoint As System.Net.IPEndPoint

        <DescriptionAttribute("远端Udp端口号")> _
        Public Property RemoteUdpPort As Integer

        Private _UdpReadBuffer() As Byte = New Byte() {0}
        <DescriptionAttribute("Udp接收缓存")> _
        Public ReadOnly Property UdpReadBuffer As Byte()
            Get
                Return _UdpReadBuffer
            End Get
        End Property

        ''' <summary>
        ''' 解析Udp的数据并且更新设备状态信息
        ''' </summary>
        ''' <param name="valReadBuffer">需要解析的数据</param>
        ''' <returns>解析后的结构体</returns>
        ''' <remarks></remarks>
        Public Function UdpFrameResolve(ByVal valReadBuffer As Byte()) As Object
            Dim _sAxisStatus As sAxisStatus
            '解析过程
            _sAxisStatus = ByteToStruct(valReadBuffer, _sAxisStatus.GetType)
            Return _sAxisStatus
        End Function

        ''' <summary>
        ''' 读取Udp数据到refReadBuffer中
        ''' </summary>
        ''' <param name="refReadBuffer">存入的缓存</param>
        ''' <returns>读取成功返回true否则false</returns>
        ''' <remarks></remarks>
        Public Function UdpRead(ByRef refReadBuffer)
            If UdpComm.Available And refReadBuffer IsNot Nothing Then
                refReadBuffer = UdpComm.Receive(UdpEndPoint)
                Return True
            Else
                Return False
            End If
        End Function
#End Region

#Region "系统参数"
        Private _IPAddress As System.Net.IPAddress
        <DescriptionAttribute("系统的IP地址")> _
        Public Property IPAddress As String
            Get
                Return _IPAddress.ToString
            End Get
            Set(value As String)
                Try
                    _IPAddress = System.Net.IPAddress.Parse(value)
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            End Set
        End Property

        Private _Q_ALM_Contactor As Boolean
        <DescriptionAttribute("ALM外部接触器状态，电机程序初始化的时候上电")> _
        Public ReadOnly Property Q_ALM_Contactor As Boolean
            Get
                Return Q_ALM_Contactor
            End Get
        End Property

        <DescriptionAttribute("ALM使能控制寄存器，false停止供电，true开始供电")> _
        Public Property g_Enable_ALM As Boolean

        Private _g_ALM_Actived As Boolean
        <DescriptionAttribute("ALM真实状态")> _
        Public ReadOnly Property g_ALM_Actived As Boolean
            Get
                Return _g_ALM_Actived
            End Get
        End Property
#End Region

#Region "电机"
        <DescriptionAttribute("输入端电机")> _
        <TypeConverter(GetType(ExpandableObjectConverter))> _
        Public Property Axis_Master As New SimotionMotor("Axis_Master", valIndex:=1)
        <DescriptionAttribute("负载电机")> _
        <TypeConverter(GetType(ExpandableObjectConverter))> _
        Public Property Axis_Load As New SimotionMotor("Axis_Load", valIndex:=2)

        ''' <summary>
        ''' 重置系统，清除故障
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ResetSystem()
            TcpSend(Frame.Frame_Reset)
        End Sub

        ''' <summary>
        ''' 使能ALM电源
        ''' </summary>
        ''' <remarks>在控制电机前必须启动电机电源</remarks>
        Public Sub EnableALM()
            TcpSend(Frame.Frame_Enable_ALM)
            g_Enable_ALM = True
        End Sub

        ''' <summary>
        ''' 停止ALM电源
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DisableALM()
            TcpSend(Frame.Frame_Disable_ALM)
            g_Enable_ALM = False
        End Sub

        ''' <summary>
        ''' 切换电机的运行状态
        ''' </summary>
        ''' <param name="_simotionmotor">被切换的电机</param>
        ''' <param name="_mode">切换到的模式</param>
        ''' <remarks></remarks>
        Public Sub SwitchAxisMode(_simotionmotor As SimotionMotor, _mode As AXISOPERATEMODE, Optional _velocity As Double = 0, Optional _torque As Double = 0)
            Select Case _mode
                Case AXISOPERATEMODE.IDLE
                    '停止转动
                    _simotionmotor.SetVelocityMoveValue(0)
                    '去使能
                    _simotionmotor.SetEnableAxisValue(0)
                    _simotionmotor.SetVelocity(0)
                    _simotionmotor.SetTorqueLimit(0)
                    _simotionmotor.SetEnableTorqueLimitValue(0)
                Case AXISOPERATEMODE.RUNNING
                    _simotionmotor.SetVelocity(_velocity)
                    _simotionmotor.SetTorqueLimit(_torque)
                    _simotionmotor.SetEnableTorqueLimitValue(1)
                    _simotionmotor.SetEnableAxisValue(1)
                    _simotionmotor.SetVelocityMoveValue(1)
            End Select
            SetAxisCommand(_simotionmotor.g_Axis_CMD)
        End Sub

        ''' <summary>
        ''' 下发指定的CMD
        ''' </summary>
        ''' <param name="_sCMD">被下发的CMD</param>
        ''' <remarks></remarks>
        Public Overloads Sub SetAxisCommand(ByVal _sCMD As sCMD)
            SetAxisCommand(_sCMD, _sCMD)
        End Sub

        ''' <summary>
        ''' 将_sourceCMD的值配置到_destCMD中，并下发到电机控制器
        ''' 电机控制器通过Index来判断消息是发给谁的
        ''' </summary>
        ''' <param name="_sourceCMD">设置内容</param>
        ''' <param name="_destCMD">被赋值的控制CMD</param>
        ''' <remarks></remarks>
        Public Overloads Sub SetAxisCommand(ByVal _sourceCMD As sCMD, ByRef _destCMD As sCMD)
            If _sourceCMD.AxisIndex <> _destCMD.AxisIndex Then
                'AxisIndex不能用这种方式修改
                _sourceCMD.AxisIndex = _destCMD.AxisIndex
            End If
            _destCMD = _sourceCMD
            '结构体转字节流
            ReDim _TcpSendBuffer(Marshal.SizeOf(_sourceCMD))
            _TcpSendBuffer = StructToByte(_sourceCMD)
            '通过Tcp下发
            TcpSend()
        End Sub

        ''' <summary>
        ''' 获取轴状态并保存到refAxisState中
        ''' </summary>
        ''' <param name="refAxisState">存入的状态结构体</param>
        ''' <remarks></remarks>
        Public Sub GetAxisState(ByRef refAxisState As sAxisStatus)
            '读数据
            UdpRead(_UdpReadBuffer)
            '解析数据
            If refAxisState.AxisIndex = _UdpReadBuffer(0) Then
                refAxisState = UdpFrameResolve(_UdpReadBuffer)
            End If
            _g_ALM_Actived = refAxisState.g_ALM_Actived
        End Sub

        ''' <summary>
        ''' 结构体转字节流
        ''' </summary>
        ''' <param name="valStruct">需要被转换的结构体</param>
        ''' <returns>转换结果</returns>
        ''' <remarks></remarks>
        Public Function StructToByte(valStruct As Object) As Byte()
            Dim _size As Integer = Marshal.SizeOf(valStruct)
            Dim _buffer As IntPtr = Marshal.AllocHGlobal(_size)
            Try
                Marshal.StructureToPtr(valStruct, _buffer, False)
                Dim _bytes(_size - 1) As Byte
                Marshal.Copy(_buffer, _bytes, 0, _size)
                Return _bytes
            Finally
                Marshal.FreeHGlobal(_buffer)
            End Try
        End Function

        ''' <summary>
        ''' 字节流转结构体
        ''' </summary>
        ''' <param name="_bytes">被转换的字节数组</param>
        ''' <param name="_structType">用.GetType()得到的类型</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ByteToStruct(_bytes As Byte(), _structType As Type) As Object
            Dim _size As Integer = Marshal.SizeOf(_structType)
            Dim _buffer As IntPtr = Marshal.AllocHGlobal(_size)
            Try
                Marshal.Copy(_bytes, 0, _buffer, _size)
                Return Marshal.PtrToStructure(_buffer, _structType)
            Finally
                Marshal.FreeHGlobal(_buffer)
            End Try
        End Function

#End Region
    End Class
    'End of SimotionSystem

    '**********************************************************
    '***********电机类，一个系统两台电机***********************
    '**********************************************************
    Public Class SimotionMotor
        ''' <summary>
        ''' New方法
        ''' </summary>
        ''' <param name="valAxisName">在Simotion中的名称</param>
        ''' <param name="valIndex">在Simotion中的编号</param>
        ''' <remarks></remarks>
        Public Sub New(valAxisName As String, valIndex As Integer)
            _AxisName = valAxisName
            _Index = valIndex
            _g_Axis_CMD.AxisIndex = valIndex
            _g_Axis_CMD.Head = Frame.Head_Control
            _AxisMode = AXISOPERATEMODE.ALMOFF
            _g_Axis_Status.AxisIndex = valIndex
        End Sub

#Region "配置CMD"
        Public Sub SetVelocity(value As Double)
            _g_Axis_CMD.VelocitySet = value
        End Sub

        Public Sub SetTorqueLimit(value As Double)
            _g_Axis_CMD.TorqueLimitValue = value
        End Sub

        Public Sub SetEnableAxisValue(value As Byte)
            _g_Axis_CMD.EnableAxis = value
        End Sub

        Public Sub SetEnableTorqueLimitValue(value As Byte)
            _g_Axis_CMD.EnableTorqueLimit = value
        End Sub

        Public Sub SetVelocityMoveValue(value As Byte)
            _g_Axis_CMD.VelocityMove = value
        End Sub
#End Region

        Private _AxisName As String
        <DescriptionAttribute("SCOUT软件中轴的名称")> _
        Public Property AxisName As String
            Set(value As String)
                _AxisName = value
            End Set
            Get
                Return _AxisName
            End Get
        End Property

        <DescriptionAttribute("电机的运行状态")> _
        Public Property AxisMode As AXISOPERATEMODE

        Private _Index As Integer

        <DescriptionAttribute("在Scout中的编号，不要随意修改")> _
        Public ReadOnly Property Index As Integer
            Get
                Return _Index
            End Get
        End Property

        <DescriptionAttribute("电机控制指令结构体")> _
        <TypeConverter(GetType(ExpandableObjectConverter))> _
        Public Property g_Axis_CMD As sCMD

        <DescriptionAttribute("电机状态结构体")> _
        <TypeConverter(GetType(ExpandableObjectConverter))> _
        Public Property g_Axis_Status As sAxisStatus

    End Class
    'End of SimotionMotor

    '轴状态的结构体
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto, Pack:=1)> _
    Public Structure sAxisStatus

        Private _AxisIndex As Byte
        Private _AxisFault As Byte 'boolean占四个字节，所以用byte存储布尔量
        Private _AxisEnabled As Byte
        Private _AxisActualRPM As Double
        Private _AxisActualTorque As Single
        Private _AxisActualCurrent As Single
        Private _AxisActualTemp As Single
        Public g_ALM_Actived As Byte '系统ALM状态

        <DescriptionAttribute("指明是哪个轴的信息")> _
        Property AxisIndex As Byte
            Set(value As Byte)
                _AxisIndex = value
            End Set
            Get
                Return _AxisIndex
            End Get
        End Property

        <DescriptionAttribute("轴故障状态指示，True表示有故障")> _
        ReadOnly Property AxisFault As Byte
            Get
                Return _AxisFault
            End Get
        End Property
        <DescriptionAttribute("轴是否使能")> _
        ReadOnly Property AxisEnabled As Byte
            Get
                Return _AxisEnabled
            End Get
        End Property
        <DescriptionAttribute("轴的转速")> _
        ReadOnly Property AxisActualRPM As Double
            Get
                Return _AxisActualRPM
            End Get
        End Property
        <DescriptionAttribute("轴的转矩")> _
        ReadOnly Property AxisActualTorque As Single
            Get
                Return _AxisActualTorque
            End Get
        End Property
        <DescriptionAttribute("轴的电流")> _
        ReadOnly Property AxisActualCurrent As Single
            Get
                Return _AxisActualCurrent
            End Get
        End Property
        <DescriptionAttribute("轴的温度")> _
        ReadOnly Property AxisActualTemp As Single
            Get
                Return _AxisActualTemp
            End Get
        End Property
    End Structure
    'End of sAxisStatus

    '轴控制的结构体
    '***************主动加载：速度/转矩****************
    '1. EnableAxis = True
    '2. TorqueLimitValue = 设定值
    '3. EnableTorqueLimit = True
    '4. VeloctiySet = 设定值，正表示正转，负表示反转
    '5. VelocityMove = True
    '***************停止主动加载************
    '1. VelocityMove = False
    '2. EnableAxis = False
    '***************作为负载****************
    '0. VelocityMove = False
    '1. TorqueLimitValue = 0
    '2. EnableTorqueLimit = True
    '3. EnableAxis = True
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto, Pack:=1)> _
    Public Structure sCMD
        <DescriptionAttribute("控制帧头")> _
        Property Head As Byte
        <DescriptionAttribute("轴的编号")> _
        Property AxisIndex As Byte
        <DescriptionAttribute("使能轴控制")> _
        Property EnableAxis As Byte 'boolean类型占四个字节，所以用Byte储存
        <DescriptionAttribute("点动正转控制，True表示正转")> _
        Property JogFW As Byte
        <DescriptionAttribute("点动反转控制，True表示反转")> _
        Property JogBW As Byte
        <DescriptionAttribute("点动模式转速设定")> _
        Property JogVelocitySet As Double
        <DescriptionAttribute("速度模式的速度")> _
        Property VelocitySet As Double
        <DescriptionAttribute("速度模式，正反转取决于VelocitySet的正负")> _
        Property VelocityMove As Byte
        <DescriptionAttribute("启动转矩限幅,在SCOUT中要先将p2175/2177设置为0")> _
        Property EnableTorqueLimit As Byte
        <DescriptionAttribute("设定的转矩值")> _
        Property TorqueLimitValue As Double
    End Structure
    'End of sCMD

    '电机运行状态的枚举
    Public Enum AXISOPERATEMODE
        ALMOFF = 0 'ALM没有供电
        IDLE    '空闲，ALM供电，EnableAxis is False，TorqueLimitValue is 0, EnableTorqueLimit is true
        RUNNING 'VelocityMove = True
        [ERROR]     '故障
    End Enum
    'End of AXISOPERATEMODE

    Class Frame
        Public Const Head_Control As Byte = &H68
        Public Shared Frame_Reset() As Byte = New Byte(0) {&H66}
        'Public Const Head_Speed_Set As Byte = &H81
        'Public Const Head_Torque_Set As Byte = &H82
        Public Shared Frame_Enable_ALM() As Byte = New Byte(1) {&H67, &H1}
        Public Shared Frame_Disable_ALM() As Byte = New Byte(1) {&H67, &H0}
    End Class

End Namespace
