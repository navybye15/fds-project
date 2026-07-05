' ================================================================
' Form18.vb - Payments (payment history / receipts list)
' Follows the same connection pattern as Form8.vb (connStr inline, isarms_db)
'
' Control names:
'   PaymentsGrid                (DataGridView)
'   Countlbl                    ("X payments recorded")
'   MonthYearlbl                ASSUMED name for the "Month Year" label in the footer
'   Moneylbl                    ("Total Collected: ...")
'   btnGenerateBill             (same shortcut button as on Form15)
' ================================================================
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

            Dim query As String = "SELECT p.payment_id AS 'Reference Number', t.full_name AS 'Tenant', " &
                                   "b.billing_month AS 'Month Year', " &
                                   "p.amount_paid AS 'Amount Paid', " &
                                   "DATE_FORMAT(p.payment_date, '%Y-%m-%d') AS 'Payment Date' " &
                                   "FROM payments p " &
                                   "JOIN bills b ON p.bill_id = b.bill_id " &
                                   "JOIN tenants t ON b.tenant_id = t.tenant_id " &
                                   "ORDER BY p.payment_date DESC"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            PaymentsGrid.DataSource = dt

            Countlbl.Text = dt.Rows.Count.ToString() & " payments recorded"
            MonthYearLbl.Text = Date.Today.ToString("MMMM yyyy")

            Dim total As Decimal = 0
            For Each r As DataRow In dt.Rows
                total += Convert.ToDecimal(r("Amount Paid"))
            Next
            Moneylbl.Text = total.ToString("C2")

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Form15.Show()
        Me.Hide()
    End Sub
End Class