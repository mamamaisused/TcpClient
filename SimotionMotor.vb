Imports System.Net.Sockets
Imports System.ComponentModel
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
        Public Sub New(valIPAddress As String, Optional valLocalTcpPort As Integer = 8000, Optional valRemoteUdpPort As Integer = 8000)
            _IPAddress = System.Net.IPAddress.Parse(valIPAddress)
            LocalTcpPort = valLocalTcpPort
            RemoteUdpPort = valRemoteUdpPort
            UdpEndPoint = New System.Net.IPEndPoint(_IPAddress, RemoteUdpPort)
        End Sub

#Region "TCP通信"
        'TCP通信
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
        ''' <remarks></remarks>
        Public Sub UdpFrameResolve(ByVal valReadBuffer)

        End Sub

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
        Public Property Axis_Master As New SimotionMotor("Axis_Master")
        <DescriptionAttribute("负载电机")> _
        <TypeConverter(GetType(ExpandableObjectConverter))> _
        Public Property Axis_Load As New SimotionMotor("Axis_Load")
#End Region
    End Class
    'End of SimotionSystem

    '**********************************************************
    '***********电机类，一个系统两台电机***********************
    '**********************************************************
    Public Class SimotionMotor
        Public Sub New(valAxisName As String)
            _AxisName = valAxisName
        End Sub

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

        <DescriptionAttribute("电机控制指令结构体")> _
        <TypeConverter(GetType(ExpandableObjectConverter))> _
        Public Property g_Axis_CMD As sCMD = sCMD.Instance

        <DescriptionAttribute("电机状态结构体")> _
        <TypeConverter(GetType(ExpandableObjectConverter))> _
        Public Property g_Axis_Status As sAxisStatus

    End Class
    'End of SimotionMotor

    '轴状态的结构体
    Public Structure sAxisStatus
        Private _AxisFault As Boolean
        Private _AxisEnabled As Boolean
        Private _AxisActualRPM As Double
        Private _AxisActualTorque As Double
        Private _AxisActualCurrent As Double
        Private _AxisActualTemp As Double

        <DescriptionAttribute("轴故障状态指示，True表示有故障")> _
        ReadOnly Property AxisFault As Boolean
            Get
                Return _AxisFault
            End Get
        End Property
        <DescriptionAttribute("轴是否使能")> _
        ReadOnly Property AxisEnabled As Boolean
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
        ReadOnly Property AxisActualTorque As Double
            Get
                Return _AxisActualTorque
            End Get
        End Property
        <DescriptionAttribute("轴的电流")> _
        ReadOnly Property AxisActualCurrent As Double
            Get
                Return _AxisActualCurrent
            End Get
        End Property
        <DescriptionAttribute("轴的温度")> _
        ReadOnly Property AxisActualTemp As Double
            Get
                Return _AxisActualTemp
            End Get
        End Property
    End Structure
    'End of sAxisStatus

    '轴控制的结构体
    '***************主动加载****************
    '1. EnableAxis = True
    '2. TorqueLimitValue = 设定值
    '3. EnableTorqueLimit = True
    '4. VeloctiySet = 设定值，正表示正转，负表示反转
    '5. VelocityMove = True
    '***************停止主动加载************
    '1. VelocityMove = False
    '2. EnableTorqueLimit = False
    '3. EnableAxis = False
    '***************作为负载****************
    '0. 停止所有转动
    '1. TorqueLimitValue = 0
    '2. EnableAxis = True
    '3. EnableTorqueLimit = True
    Public Structure sCMD
        Public Shared Function Instance()
            Dim _sCMD As sCMD
            _sCMD.EnableAxis = False
            _sCMD.EnableTorqueLimit = False
            _sCMD.JogBW = False
            _sCMD.JogFW = False
            _sCMD.JogVelocitySet = 0.0
            _sCMD.TorqueLimitValue = 0.0
            _sCMD.VelocityMove = False
            Return _sCMD
        End Function
        <DescriptionAttribute("使能轴控制")> _
        Property EnableAxis As Boolean
        <DescriptionAttribute("'点动正转控制，True表示正转")> _
        Property JogFW As Boolean
        <DescriptionAttribute("点动反转控制，True表示反转")> _
        Property JogBW As Boolean
        <DescriptionAttribute("点动模式转速设定")> _
        Property JogVelocitySet As Double
        <DescriptionAttribute("速度模式的速度")> _
        Property VelocitySet As Double
        <DescriptionAttribute("速度模式，正反转取决于VelocitySet的正负")> _
        Property VelocityMove As Boolean
        <DescriptionAttribute("启动转矩限幅,在SCOUT中要先将p2175/2177设置为0")> _
        Property EnableTorqueLimit As Boolean
        <DescriptionAttribute("设定的转矩值")> _
        Property TorqueLimitValue As Double
    End Structure
    'End of sCMD

    '电机运行状态的枚举
    Public Enum AXISOPERATEMODE
        ALMOFF = 0 'ALM没有供电
        IDLE    '空闲，ALM供电，EnableAxis is False，TorqueLimitValue is 0, EnableTorqueLimit is true
        VELOCITYMODE    '速度控制模式, EnableAxis is True, VelocityMove is True
        TORQUEMODE  '转矩控制模式
        [ERROR]     '故障
    End Enum
    'End of AXXISOPERATEMODE

End Namespace
