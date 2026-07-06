Public Class Form19
    Private Sub Printbtn_Click(sender As Object, e As EventArgs) Handles Printbtn.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label31_Click(sender As Object, e As EventArgs) Handles Label31.Click
        Label4.Text = "Monthly Collection Report"
        label40.Text = "Summary of amount collected per month"
        labell1.Text = "Total Due:"
        Labell2.Text = "Total Collected:"

        labell6.Visible = False
        Labell4.Visible = False
    End Sub

    Private Sub Form19_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        labell1.Text = "Occupied:"
        Labell2.Text = "Available:"
        labell6.Text = "Maintenance:"
    End Sub

    Private Sub Label30_Click(sender As Object, e As EventArgs) Handles Label30.Click
        Label4.Text = "Outstanding Balances"
        label40.Text = "unpaid and partially paid bills"
        labell1.Text = "Total Outstanding:"
        Labell2.Visible = False

        labell6.Visible = False
        Labell4.Visible = False
    End Sub

    Private Sub Label29_Click(sender As Object, e As EventArgs) Handles Label29.Click
        Label4.Text = "Payment History per Tenant"
        label40.Text = "sum of payment history per tenant"
        perTenantCmb.Visible = True
    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs) Handles Label25.Click
        Form6.Show()
        Me.Hide()

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Form7.Show()
        Me.Hide()

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Form8.Show()
        Me.Hide()

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Form9.Show()
        Me.Hide()

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Form12.Show()
        Me.Hide()

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Form15.Show()
        Me.Hide()

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        Form18.Show()
        Me.Hide()

    End Sub
End Class