Imports TCP.SIMOTION
Imports System.Runtime.InteropServices
Public Class newClient
    Dim Simotion As New SimotionSystem("192.168.3.10")

    Private Sub newClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PropertyGrid1.SelectedObject = Simotion
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        Simotion.TcpConnect()
        PropertyGrid1.SelectedObject = Simotion
    End Sub

    Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click
        Simotion.TcpClose()
        PropertyGrid1.SelectedObject = Simotion
    End Sub

    Private Sub btnTcpSend_Click(sender As Object, e As EventArgs)
        Simotion.SetAxisCommand(Simotion.Axis_Master.g_Axis_CMD)
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs)

        Dim cmd1 As sCMD

        cmd1.Head = Frame.Head_Control
        cmd1.TorqueLimitValue = 1.8
        cmd1.EnableTorqueLimit = True

        Simotion.SetAxisCommand(cmd1, Simotion.Axis_Master.g_Axis_CMD)
    End Sub

    Private Sub btnModify_Click(sender As Object, e As EventArgs)
        PropertyGrid1.SelectedObject = Simotion
    End Sub

    Private Sub tmUdpReader_Tick(sender As Object, e As EventArgs) Handles tmUdpReader.Tick
        Simotion.GetAxisState(Simotion.Axis_Master.g_Axis_Status)
        Simotion.GetAxisState(Simotion.Axis_Load.g_Axis_Status)
        PropertyGrid1.SelectedObject = Simotion
    End Sub

    Private Sub btnEnableALM_Click(sender As Object, e As EventArgs) Handles btnEnableALM.Click
        Simotion.EnableALM()
    End Sub

    Private Sub btnDisableALM_Click(sender As Object, e As EventArgs) Handles btnDisableALM.Click
        Simotion.DisableALM()
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If cboxMotor.Text = "Master" Then
            Simotion.SwitchAxisMode(Simotion.Axis_Master, AXISOPERATEMODE.RUNNING, txtVelocity.Text, txtTorque.Text)
        End If
        If cboxMotor.Text = "Load" Then
            Simotion.SwitchAxisMode(Simotion.Axis_Load, AXISOPERATEMODE.RUNNING, txtVelocity.Text, txtTorque.Text)
        End If
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        If cboxMotor.Text = "Master" Then
            Simotion.SwitchAxisMode(Simotion.Axis_Master, AXISOPERATEMODE.IDLE)
        End If
        If cboxMotor.Text = "Load" Then
            Simotion.SwitchAxisMode(Simotion.Axis_Load, AXISOPERATEMODE.IDLE)
        End If
    End Sub

   
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Simotion.ResetSystem()
    End Sub
End Class