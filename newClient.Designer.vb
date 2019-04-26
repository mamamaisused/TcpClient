<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newClient
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
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.btnDisconnect = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTorque = New System.Windows.Forms.TextBox()
        Me.txtVelocity = New System.Windows.Forms.TextBox()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnDisableALM = New System.Windows.Forms.Button()
        Me.btnEnableALM = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.tmUdpReader = New System.Windows.Forms.Timer(Me.components)
        Me.cboxMotor = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid1.Location = New System.Drawing.Point(0, 0)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(1021, 511)
        Me.PropertyGrid1.TabIndex = 0
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(12, 12)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(76, 41)
        Me.btnConnect.TabIndex = 1
        Me.btnConnect.Text = "连接"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'btnDisconnect
        '
        Me.btnDisconnect.Location = New System.Drawing.Point(94, 12)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(76, 41)
        Me.btnDisconnect.TabIndex = 2
        Me.btnDisconnect.Text = "断开"
        Me.btnDisconnect.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cboxMotor)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtTorque)
        Me.Panel1.Controls.Add(Me.txtVelocity)
        Me.Panel1.Controls.Add(Me.btnStop)
        Me.Panel1.Controls.Add(Me.btnStart)
        Me.Panel1.Controls.Add(Me.btnDisableALM)
        Me.Panel1.Controls.Add(Me.btnEnableALM)
        Me.Panel1.Controls.Add(Me.btnConnect)
        Me.Panel1.Controls.Add(Me.btnDisconnect)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1021, 65)
        Me.Panel1.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(710, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 18)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Nm"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(584, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 18)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "RPM"
        '
        'txtTorque
        '
        Me.txtTorque.Location = New System.Drawing.Point(642, 17)
        Me.txtTorque.Name = "txtTorque"
        Me.txtTorque.Size = New System.Drawing.Size(62, 28)
        Me.txtTorque.TabIndex = 10
        Me.txtTorque.Text = "2"
        Me.txtTorque.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtVelocity
        '
        Me.txtVelocity.Location = New System.Drawing.Point(516, 17)
        Me.txtVelocity.Name = "txtVelocity"
        Me.txtVelocity.Size = New System.Drawing.Size(62, 28)
        Me.txtVelocity.TabIndex = 10
        Me.txtVelocity.Text = "20"
        Me.txtVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(422, 11)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(76, 41)
        Me.btnStop.TabIndex = 9
        Me.btnStop.Text = "停止"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(340, 11)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(76, 41)
        Me.btnStart.TabIndex = 8
        Me.btnStart.Text = "启动"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnDisableALM
        '
        Me.btnDisableALM.Location = New System.Drawing.Point(258, 11)
        Me.btnDisableALM.Name = "btnDisableALM"
        Me.btnDisableALM.Size = New System.Drawing.Size(76, 41)
        Me.btnDisableALM.TabIndex = 7
        Me.btnDisableALM.Text = "断电"
        Me.btnDisableALM.UseVisualStyleBackColor = True
        '
        'btnEnableALM
        '
        Me.btnEnableALM.Location = New System.Drawing.Point(176, 11)
        Me.btnEnableALM.Name = "btnEnableALM"
        Me.btnEnableALM.Size = New System.Drawing.Size(76, 41)
        Me.btnEnableALM.TabIndex = 6
        Me.btnEnableALM.Text = "上电"
        Me.btnEnableALM.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.PropertyGrid1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 65)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1021, 511)
        Me.Panel2.TabIndex = 7
        '
        'tmUdpReader
        '
        Me.tmUdpReader.Enabled = True
        Me.tmUdpReader.Interval = 200
        '
        'cboxMotor
        '
        Me.cboxMotor.FormattingEnabled = True
        Me.cboxMotor.Items.AddRange(New Object() {"Master", "Load"})
        Me.cboxMotor.Location = New System.Drawing.Point(766, 19)
        Me.cboxMotor.Name = "cboxMotor"
        Me.cboxMotor.Size = New System.Drawing.Size(106, 26)
        Me.cboxMotor.TabIndex = 12
        Me.cboxMotor.Text = "Master"
        '
        'newClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1021, 576)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "newClient"
        Me.Text = "newClient"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Private WithEvents btnConnect As System.Windows.Forms.Button
    Private WithEvents btnDisconnect As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents tmUdpReader As System.Windows.Forms.Timer
    Private WithEvents btnDisableALM As System.Windows.Forms.Button
    Private WithEvents btnEnableALM As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTorque As System.Windows.Forms.TextBox
    Friend WithEvents txtVelocity As System.Windows.Forms.TextBox
    Private WithEvents btnStop As System.Windows.Forms.Button
    Private WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents cboxMotor As System.Windows.Forms.ComboBox
End Class
