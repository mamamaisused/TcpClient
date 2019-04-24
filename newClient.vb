Imports TCP.SIMOTION
Public Class newClient
    Dim Simotion As New SimotionSystem("192.168.1.101")
    Private Sub newClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PropertyGrid1.SelectedObject = Simotion
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        Simotion.TcpConnect()
    End Sub

    Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click
        Simotion.TcpClose()
    End Sub

    Private Sub newClient_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        PropertyGrid1.SelectedObject = Simotion
    End Sub

    Private Sub btnTcpSend_Click(sender As Object, e As EventArgs) Handles btnTcpSend.Click
        Simotion.TcpSend()
    End Sub
End Class