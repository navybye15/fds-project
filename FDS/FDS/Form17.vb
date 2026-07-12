Imports MySql.Data.MySqlClient

Public Class Form17

    Private ReadOnly connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"

    Dim billId As Integer
    Dim currentTotalDue As Decimal
    Dim alreadyPaid As Decimal
    Dim remainingBalance As Decimal


    Public Sub New(ByVal passedBillId As Integer)
        InitializeComponent()
        billId = passedBillId
    End Sub

    Private Sub Form17_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Statuscmb.Items.Clear()
        Statuscmb.Items.AddRange({"paid", "partial", "unpaid"})
        Statuscmb.SelectedIndex = 0

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
            End If
            reader.Close()


            Dim cmdPaid As New MySqlCommand("SELECT SUM(amount_paid) FROM payments WHERE bill_id = @bill_id", conn)
            cmdPaid.Parameters.AddWithValue("@bill_id", billId)
            Dim result = cmdPaid.ExecuteScalar()

            If IsDBNull(result) Then
                alreadyPaid = 0
            Else
                alreadyPaid = Convert.ToDecimal(result)
            End If

            conn.Close()


            remainingBalance = currentTotalDue - alreadyPaid

            partialPayment.Text = alreadyPaid.ToString("0.00")
            Totallbl.Text = remainingBalance.ToString("0.00")

            Paidtxt.Text = remainingBalance.ToString("0.00")

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

        If amountPaid > remainingBalance Then
            MessageBox.Show("Amount paid cannot be more than the remaining balance of " & remainingBalance.ToString("0.00") & ".")
            Return
        End If

        Dim conn As New MySqlConnection(connStr)
        Try
            conn.Open()
            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try

                Dim cmdBill As New MySqlCommand(
                    "UPDATE bills SET status = @status WHERE bill_id = @bill_id",
                    conn, transaction)
                cmdBill.Parameters.AddWithValue("@status", Statuscmb.Text)
                cmdBill.Parameters.AddWithValue("@bill_id", billId)
                cmdBill.ExecuteNonQuery()


                Dim cmdPayment As New MySqlCommand(
                    "INSERT INTO payments (bill_id, amount_paid, payment_date) VALUES (@bill_id, @amount, @pay_date)",
                    conn, transaction)
                cmdPayment.Parameters.AddWithValue("@bill_id", billId)
                cmdPayment.Parameters.AddWithValue("@amount", amountPaid)
                cmdPayment.Parameters.AddWithValue("@pay_date", DateTime.Today)
                cmdPayment.ExecuteNonQuery()

                Dim cmdGetId As New MySqlCommand("SELECT LAST_INSERT_ID()", conn, transaction)
                Dim newPaymentId As Integer = Convert.ToInt32(cmdGetId.ExecuteScalar())

                transaction.Commit()

                MessageBox.Show("Payment recorded successfully!")

                Dim receiptForm As New Form20()
                receiptForm.paymentId = newPaymentId
                receiptForm.ShowDialog()

                Me.Close()


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