Imports MySql.Data.MySqlClient

Public Class Form18

    Private ReadOnly connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"

    Private Sub Form18_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadPayments()
    End Sub

    Private Sub loadPayments()
        Dim conn As New MySqlConnection(connStr)
        Try
            conn.Open()

            Dim query As String = "SELECT p.payment_id AS 'No.', t.full_name AS 'Tenant', " &
                                   "b.billing_month AS 'Month Year', " &
                                   "p.amount_paid AS 'Amount Paid', " &
                                   "p.payment_date AS 'Payment Date' " &
                                   "FROM payments p " &
                                   "JOIN bills b ON p.bill_id = b.bill_id " &
                                   "JOIN tenants t ON b.tenant_id = t.tenant_id " &
                                   "ORDER BY p.payment_date DESC"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            PaymentsGrid.DataSource = dt

            conn.Close()

            ' Count & total collected should only reflect THIS month's payments,
            ' even though the grid above still shows the full history.
            ' (computed here in VB instead of using SQL functions)
            Dim monthlyCount As Integer = 0
            Dim monthlyTotal As Decimal = 0

            For Each r As DataRow In dt.Rows
                Dim payDate As DateTime = Convert.ToDateTime(r("Payment Date"))
                If payDate.Month = Date.Today.Month And payDate.Year = Date.Today.Year Then
                    monthlyCount += 1
                    monthlyTotal += Convert.ToDecimal(r("Amount Paid"))
                End If
            Next

            Countlbl.Text = monthlyCount.ToString() & " payments recorded"
            MonthYearLbl.Text = Date.Today.ToString("MMMM yyyy")
            Moneylbl.Text = monthlyTotal.ToString("C2")
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
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

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Form15.Show()
        Me.Hide()

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click


    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        Form19.Show()
        Me.Hide()
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Form12.Show()
        Me.Hide()

    End Sub
End Class