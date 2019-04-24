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
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.btnDisconnect = New System.Windows.Forms.Button()
        Me.btnTcpSend = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PropertyGrid1.Location = New System.Drawing.Point(0, 72)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(888, 504)
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
        'btnTcpSend
        '
        Me.btnTcpSend.Location = New System.Drawing.Point(176, 12)
        Me.btnTcpSend.Name = "btnTcpSend"
        Me.btnTcpSend.Size = New System.Drawing.Size(76, 41)
        Me.btnTcpSend.TabIndex = 3
        Me.btnTcpSend.Text = "发送"
        Me.btnTcpSend.UseVisualStyleBackColor = True
        '
        'newClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(888, 576)
        Me.Controls.Add(Me.btnTcpSend)
        Me.Controls.Add(Me.btnDisconnect)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.PropertyGrid1)
        Me.Name = "newClient"
        Me.Text = "newClient"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Private WithEvents btnConnect As System.Windows.Forms.Button
    Private WithEvents btnDisconnect As System.Windows.Forms.Button
    Private WithEvents btnTcpSend As System.Windows.Forms.Button
End Class
