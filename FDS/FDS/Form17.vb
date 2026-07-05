' ================================================================
' Form17.vb - Record Payment (popup, called via .ShowDialog() from Form15's
'             "Record Bill" button, passed the selected bill_id)
' Follows the same connection pattern as Form8.vb (connStr inline, isarms_db)
'
' Control names:
'   tenantlbl, monthyearlbl          Labels, read-only display
'   totallbl, rentlbl, chargelbl     Labels, read-only display (Total Due / Base Rent / Additional Charges)
'   paidtxt                          TextBox, editable - Amount Paid
'   statuscmb                        ComboBox - paid / partial / unpaid
'   CancelBtn, RecordPayBtn          ASSUMED names - rename the Handles clause below if different
' ================================================================
Imports MySql.Data.MySqlClient

Public Class Form17

    Private ReadOnly connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"

    Dim billId As Integer
    Dim currentTotalDue As Decimal

    ' Constructor overload used by Form15: New Form17(selectedBillId)
    Public Sub New(ByVal passedBillId As Integer)
        InitializeComponent()
        billId = passedBillId
    End Sub

    Private Sub Form17_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Statuscmb.Items.Clear()
        Statuscmb.Items.AddRange({"paid", "partial", "unpaid"})
        Statuscmb.SelectedIndex = 0 ' default "paid"

        loadBillDetails()
    End Sub

    Private Sub loadBillDetails()
        Dim conn As New MySqlConnection(connStr)
        Try
            conn.Open()

            Dim query As String = "SELECT t.full_name, b.billing_month, b.base_rent, b.addtional_charges " &
                                   "FROM bills b " &
                                   "JOIN tenants t ON b.tenant_id = t.tenant_id " &
                                   "WHERE b.bill_id = @bill_id"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@bill_id", billId)

            Dim reader = cmd.ExecuteReader()
            If reader.Read() Then
                Dim baseRent As Decimal = Convert.ToDecimal(reader("base_rent"))
                Dim addCharges As Decimal = Convert.ToDecimal(reader("addtional_charges"))
                currentTotalDue = baseRent + addCharges

                Tenantlbl.Text = reader("full_name").ToString()
                MonthYearlbl.Text = reader("billing_month").ToString()
                Rentlbl.Text = baseRent.ToString("0.00")
                Chargelbl.Text = addCharges.ToString("0.00")
                Totallbl.Text = currentTotalDue.ToString("0.00")
            End If
            reader.Close()

            conn.Close()

            Paidtxt.Text = currentTotalDue.ToString("0.00")
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnRecordPayment_Click(sender As Object, e As EventArgs) Handles Recordbtn.Click
        Dim amountPaid As Decimal
        If Not Decimal.TryParse(Paidtxt.Text, amountPaid) OrElse amountPaid <= 0 Then
            MessageBox.Show("Please enter a valid amount.")
            Return
        End If

        Dim conn As New MySqlConnection(connStr)
        Try
            conn.Open()
            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                ' 1. update the bill's status
                Dim cmdBill As New MySqlCommand(
                    "UPDATE bills SET status = @status WHERE bill_id = @bill_id",
                    conn, transaction)
                cmdBill.Parameters.AddWithValue("@status", Statuscmb.Text)
                cmdBill.Parameters.AddWithValue("@bill_id", billId)
                cmdBill.ExecuteNonQuery()

                ' 2. record the actual payment
                Dim cmdPayment As New MySqlCommand(
                    "INSERT INTO payments (bill_id, amount_paid, payment_date) VALUES (@bill_id, @amount, @pay_date)",
                    conn, transaction)
                cmdPayment.Parameters.AddWithValue("@bill_id", billId)
                cmdPayment.Parameters.AddWithValue("@amount", amountPaid)
                cmdPayment.Parameters.AddWithValue("@pay_date", DateTime.Today)
                cmdPayment.ExecuteNonQuery()

                transaction.Commit()

                MessageBox.Show("Payment recorded successfully!")
                Me.Close()

                Dim frm18 As New Form18()
                frm18.Show()
            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show("Recording payment failed, no changes were made: " & ex.Message)
            End Try
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        Me.Close()
    End Sub

End Class