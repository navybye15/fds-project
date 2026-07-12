Imports MySql.Data.MySqlClient

Public Class Form18

    Private ReadOnly connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"

    Dim selectedPaymentId As Integer = 0

    Private Sub Form18_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadPayments()
    End Sub

    Public Sub RefreshAndShow()
        loadPayments()
        Me.Show()
    End Sub

    Private Sub loadPayments()
        Dim conn As New MySqlConnection(connStr)
        Try
            conn.Open()

            Dim query As String = "SELECT p.payment_id AS 'No.', t.full_name AS 'Tenant', " &
                                   "un.unit_number AS 'Unit', " &
                                   "b.billing_month AS 'Month Year', " &
                                   "p.amount_paid AS 'Amount Paid', " &
                                   "p.payment_date AS 'Payment Date' " &
                                   "FROM payments p " &
                                   "JOIN bills b ON p.bill_id = b.bill_id " &
                                   "JOIN tenants t ON b.tenant_id = t.tenant_id " &
                                   "JOIN units un ON b.unit_id = un.unit_id " &
                                   "ORDER BY p.payment_date DESC"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            PaymentsGrid.DataSource = dt

            conn.Close()

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

    Private Sub PaymentsGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles PaymentsGrid.CellClick
        If PaymentsGrid.SelectedRows.Count > 0 Then
            Dim row = PaymentsGrid.SelectedRows(0)

            selectedPaymentId = Convert.ToInt32(row.Cells("No.").Value)

            TenantTxt.Text = row.Cells("Tenant").Value.ToString()
            UnitTxt.Text = row.Cells("Unit").Value.ToString()
            BillingMonthTxt.Text = row.Cells("Month Year").Value.ToString()
            AmountPaidTxt.Text = row.Cells("Amount Paid").Value.ToString()
            DatePaymentTxt.Text = Convert.ToDateTime(row.Cells("Payment Date").Value).ToString("yyyy-MM-dd")
        End If
    End Sub

    Private Sub printReceiptBtn_Click(sender As Object, e As EventArgs) Handles printReceiptBtn.Click
        If selectedPaymentId = 0 Then
            MessageBox.Show("Please select a payment first.")
            Return
        End If

        Dim receiptForm As New Form20()
        receiptForm.paymentId = selectedPaymentId
        receiptForm.ShowDialog()
    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs) Handles Label25.Click
        Form6.Show()
        Me.Hide()

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Form7.RefreshAndShow()
        Me.Hide()

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Form8.RefreshAndShow()
        Me.Hide()

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Form9.RefreshAndShow()
        Me.Hide()

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Form15.RefreshAndShow()
        Me.Hide()

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click

    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        Form19.RefreshAndShow()
        Me.Hide()
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Form12.RefreshAndShow()
        Me.Hide()

    End Sub

    Private Sub btnSignOut_Click(sender As Object, e As EventArgs) Handles btnSignOut.Click
        Session.SignOut(Me)
    End Sub
End Class