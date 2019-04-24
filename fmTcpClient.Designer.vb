<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fmTcpClient
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslbStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslbALMState = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtIP = New System.Windows.Forms.TextBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSend = New System.Windows.Forms.TextBox()
        Me.txtReceived = New System.Windows.Forms.TextBox()
        Me.btnDisconnect = New System.Windows.Forms.Button()
        Me.pnMotorForm = New System.Windows.Forms.Panel()
        Me.txtLoadDirection = New System.Windows.Forms.TextBox()
        Me.txtLoadRPM = New System.Windows.Forms.TextBox()
        Me.txtMasterDirection = New System.Windows.Forms.TextBox()
        Me.txtMasterRPM = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnMotorControl = New System.Windows.Forms.Panel()
        Me.btnBackward = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnForward = New System.Windows.Forms.Button()
        Me.btnSetSpeed = New System.Windows.Forms.Button()
        Me.txtSetSpeed = New System.Windows.Forms.TextBox()
        Me.cboxAxis = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnEnableALM = New System.Windows.Forms.Button()
        Me.timer_udpReader = New System.Windows.Forms.Timer(Me.components)
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.PropertyGrid2 = New System.Windows.Forms.PropertyGrid()
        Me.StatusStrip1.SuspendLayout()
        Me.pnMotorForm.SuspendLayout()
        Me.pnMotorControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(12, 290)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(110, 38)
        Me.btnConnect.TabIndex = 0
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.tslbStatus, Me.ToolStripStatusLabel2, Me.tslbALMState})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 655)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1051, 30)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "statusbar"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(64, 25)
        Me.ToolStripStatusLabel1.Text = "Status:"
        '
        'tslbStatus
        '
        Me.tslbStatus.Name = "tslbStatus"
        Me.tslbStatus.Size = New System.Drawing.Size(0, 25)
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(57, 25)
        Me.ToolStripStatusLabel2.Text = "ALM: "
        '
        'tslbALMState
        '
        Me.tslbALMState.Name = "tslbALMState"
        Me.tslbALMState.Size = New System.Drawing.Size(0, 25)
        '
        'txtIP
        '
        Me.txtIP.Location = New System.Drawing.Point(113, 14)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(165, 28)
        Me.txtIP.TabIndex = 2
        Me.txtIP.Text = "192.168.3.10"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(366, 14)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(63, 28)
        Me.txtPort.TabIndex = 2
        Me.txtPort.Text = "3000"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 18)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "IP Address"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(316, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 18)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Port"
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(293, 291)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(135, 37)
        Me.btnSend.TabIndex = 4
        Me.btnSend.Text = "Send"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(161, 18)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Send Data (HEX) :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 117)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(143, 18)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Received Data :"
        '
        'txtSend
        '
        Me.txtSend.Location = New System.Drawing.Point(10, 77)
        Me.txtSend.Name = "txtSend"
        Me.txtSend.Size = New System.Drawing.Size(418, 28)
        Me.txtSend.TabIndex = 6
        '
        'txtReceived
        '
        Me.txtReceived.Location = New System.Drawing.Point(10, 138)
        Me.txtReceived.Multiline = True
        Me.txtReceived.Name = "txtReceived"
        Me.txtReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReceived.Size = New System.Drawing.Size(418, 134)
        Me.txtReceived.TabIndex = 6
        '
        'btnDisconnect
        '
        Me.btnDisconnect.Location = New System.Drawing.Point(140, 291)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(138, 37)
        Me.btnDisconnect.TabIndex = 7
        Me.btnDisconnect.Text = "Disconnect"
        Me.btnDisconnect.UseVisualStyleBackColor = True
        '
        'pnMotorForm
        '
        Me.pnMotorForm.Controls.Add(Me.txtLoadDirection)
        Me.pnMotorForm.Controls.Add(Me.txtLoadRPM)
        Me.pnMotorForm.Controls.Add(Me.txtMasterDirection)
        Me.pnMotorForm.Controls.Add(Me.txtMasterRPM)
        Me.pnMotorForm.Controls.Add(Me.Label6)
        Me.pnMotorForm.Controls.Add(Me.Label8)
        Me.pnMotorForm.Controls.Add(Me.Label7)
        Me.pnMotorForm.Controls.Add(Me.Label5)
        Me.pnMotorForm.Controls.Add(Me.pnMotorControl)
        Me.pnMotorForm.Controls.Add(Me.btnEnableALM)
        Me.pnMotorForm.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnMotorForm.Location = New System.Drawing.Point(456, 0)
        Me.pnMotorForm.Name = "pnMotorForm"
        Me.pnMotorForm.Size = New System.Drawing.Size(595, 655)
        Me.pnMotorForm.TabIndex = 8
        '
        'txtLoadDirection
        '
        Me.txtLoadDirection.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtLoadDirection.Location = New System.Drawing.Point(21, 275)
        Me.txtLoadDirection.Name = "txtLoadDirection"
        Me.txtLoadDirection.ReadOnly = True
        Me.txtLoadDirection.Size = New System.Drawing.Size(124, 28)
        Me.txtLoadDirection.TabIndex = 4
        Me.txtLoadDirection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtLoadRPM
        '
        Me.txtLoadRPM.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtLoadRPM.Location = New System.Drawing.Point(21, 224)
        Me.txtLoadRPM.Name = "txtLoadRPM"
        Me.txtLoadRPM.ReadOnly = True
        Me.txtLoadRPM.Size = New System.Drawing.Size(124, 28)
        Me.txtLoadRPM.TabIndex = 4
        Me.txtLoadRPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMasterDirection
        '
        Me.txtMasterDirection.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtMasterDirection.Location = New System.Drawing.Point(21, 153)
        Me.txtMasterDirection.Name = "txtMasterDirection"
        Me.txtMasterDirection.ReadOnly = True
        Me.txtMasterDirection.Size = New System.Drawing.Size(124, 28)
        Me.txtMasterDirection.TabIndex = 4
        Me.txtMasterDirection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMasterRPM
        '
        Me.txtMasterRPM.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtMasterRPM.Location = New System.Drawing.Point(21, 101)
        Me.txtMasterRPM.Name = "txtMasterRPM"
        Me.txtMasterRPM.ReadOnly = True
        Me.txtMasterRPM.Size = New System.Drawing.Size(124, 28)
        Me.txtMasterRPM.TabIndex = 4
        Me.txtMasterRPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(29, 203)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 18)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Load RPM :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(18, 254)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(143, 18)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "Load Direction:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 132)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(161, 18)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Master Direction:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(29, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(116, 18)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Master RPM :"
        '
        'pnMotorControl
        '
        Me.pnMotorControl.Controls.Add(Me.PropertyGrid2)
        Me.pnMotorControl.Controls.Add(Me.btnBackward)
        Me.pnMotorControl.Controls.Add(Me.btnStop)
        Me.pnMotorControl.Controls.Add(Me.btnForward)
        Me.pnMotorControl.Controls.Add(Me.btnSetSpeed)
        Me.pnMotorControl.Controls.Add(Me.txtSetSpeed)
        Me.pnMotorControl.Controls.Add(Me.cboxAxis)
        Me.pnMotorControl.Controls.Add(Me.Label9)
        Me.pnMotorControl.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnMotorControl.Location = New System.Drawing.Point(170, 0)
        Me.pnMotorControl.Name = "pnMotorControl"
        Me.pnMotorControl.Size = New System.Drawing.Size(425, 655)
        Me.pnMotorControl.TabIndex = 2
        '
        'btnBackward
        '
        Me.btnBackward.Location = New System.Drawing.Point(144, 190)
        Me.btnBackward.Name = "btnBackward"
        Me.btnBackward.Size = New System.Drawing.Size(124, 31)
        Me.btnBackward.TabIndex = 3
        Me.btnBackward.Text = "Backward"
        Me.btnBackward.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Font = New System.Drawing.Font("Segoe UI Symbol", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStop.Location = New System.Drawing.Point(14, 241)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(254, 62)
        Me.btnStop.TabIndex = 3
        Me.btnStop.Text = "STOP"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnForward
        '
        Me.btnForward.Location = New System.Drawing.Point(14, 190)
        Me.btnForward.Name = "btnForward"
        Me.btnForward.Size = New System.Drawing.Size(124, 31)
        Me.btnForward.TabIndex = 3
        Me.btnForward.Text = "Forward"
        Me.btnForward.UseVisualStyleBackColor = True
        '
        'btnSetSpeed
        '
        Me.btnSetSpeed.Location = New System.Drawing.Point(184, 129)
        Me.btnSetSpeed.Name = "btnSetSpeed"
        Me.btnSetSpeed.Size = New System.Drawing.Size(84, 31)
        Me.btnSetSpeed.TabIndex = 3
        Me.btnSetSpeed.Text = "Set"
        Me.btnSetSpeed.UseVisualStyleBackColor = True
        '
        'txtSetSpeed
        '
        Me.txtSetSpeed.Location = New System.Drawing.Point(79, 132)
        Me.txtSetSpeed.Name = "txtSetSpeed"
        Me.txtSetSpeed.Size = New System.Drawing.Size(99, 28)
        Me.txtSetSpeed.TabIndex = 2
        Me.txtSetSpeed.Text = "200"
        Me.txtSetSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboxAxis
        '
        Me.cboxAxis.FormattingEnabled = True
        Me.cboxAxis.Items.AddRange(New Object() {"Axis_Master", "Axis_Load"})
        Me.cboxAxis.Location = New System.Drawing.Point(69, 52)
        Me.cboxAxis.Name = "cboxAxis"
        Me.cboxAxis.Size = New System.Drawing.Size(130, 26)
        Me.cboxAxis.TabIndex = 1
        Me.cboxAxis.Text = "Axis_Master"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(20, 138)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 18)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "RPM :"
        '
        'btnEnableALM
        '
        Me.btnEnableALM.Location = New System.Drawing.Point(6, 30)
        Me.btnEnableALM.Name = "btnEnableALM"
        Me.btnEnableALM.Size = New System.Drawing.Size(155, 34)
        Me.btnEnableALM.TabIndex = 0
        Me.btnEnableALM.Text = "Enable ALM"
        Me.btnEnableALM.UseVisualStyleBackColor = True
        '
        'timer_udpReader
        '
        Me.timer_udpReader.Enabled = True
        Me.timer_udpReader.Interval = 200
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PropertyGrid1.Location = New System.Drawing.Point(0, 348)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(456, 307)
        Me.PropertyGrid1.TabIndex = 4
        '
        'PropertyGrid2
        '
        Me.PropertyGrid2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PropertyGrid2.Location = New System.Drawing.Point(0, 348)
        Me.PropertyGrid2.Name = "PropertyGrid2"
        Me.PropertyGrid2.Size = New System.Drawing.Size(425, 307)
        Me.PropertyGrid2.TabIndex = 4
        '
        'fmTcpClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1051, 685)
        Me.Controls.Add(Me.PropertyGrid1)
        Me.Controls.Add(Me.pnMotorForm)
        Me.Controls.Add(Me.btnDisconnect)
        Me.Controls.Add(Me.txtReceived)
        Me.Controls.Add(Me.txtSend)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.txtIP)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnConnect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "fmTcpClient"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TcpClient"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.pnMotorForm.ResumeLayout(False)
        Me.pnMotorForm.PerformLayout()
        Me.pnMotorControl.ResumeLayout(False)
        Me.pnMotorControl.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tslbStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtIP As System.Windows.Forms.TextBox
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSend As System.Windows.Forms.TextBox
    Friend WithEvents txtReceived As System.Windows.Forms.TextBox
    Friend WithEvents btnDisconnect As System.Windows.Forms.Button
    Friend WithEvents pnMotorForm As System.Windows.Forms.Panel
    Friend WithEvents cboxAxis As System.Windows.Forms.ComboBox
    Friend WithEvents btnEnableALM As System.Windows.Forms.Button
    Friend WithEvents pnMotorControl As System.Windows.Forms.Panel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tslbALMState As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtLoadDirection As System.Windows.Forms.TextBox
    Friend WithEvents txtLoadRPM As System.Windows.Forms.TextBox
    Friend WithEvents txtMasterDirection As System.Windows.Forms.TextBox
    Friend WithEvents txtMasterRPM As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnBackward As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnForward As System.Windows.Forms.Button
    Friend WithEvents btnSetSpeed As System.Windows.Forms.Button
    Friend WithEvents txtSetSpeed As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents timer_udpReader As System.Windows.Forms.Timer
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents PropertyGrid2 As System.Windows.Forms.PropertyGrid

End Class
