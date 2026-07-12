Imports MySql.Data.MySqlClient

Public Class Form15

    Dim selectedBillId As Integer = 0
    Private ReadOnly connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"

    Private Sub Form15_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TenantTxt.ReadOnly = True
        UnitTxt.ReadOnly = True
        StatusTxt.ReadOnly = True
        StatusTxt.Text = "unpaid"

        loadBills()
    End Sub

    Public Sub RefreshAndShow()
        loadBills()
        Me.Show()
    End Sub

    Private Sub loadBills()
        Dim conn As New MySqlConnection(connStr)
        Try
            conn.Open()

            Dim query As String = "SELECT b.bill_id, t.full_name AS 'Tenant', " &
                               "un.unit_number AS 'Unit', " &
                               "b.billing_month AS 'Billing Month', " &
                               "b.base_rent AS 'Base Rent', " &
                               "b.addtional_charges AS 'Add. Charges', " &
                               "(b.base_rent + b.addtional_charges) AS 'Total Due', " &
                               "b.due_date AS 'Due Date', " &
                               "b.status AS 'Status' " &
                               "FROM bills b " &
                               "JOIN tenants t ON b.tenant_id = t.tenant_id " &
                               "JOIN units un ON b.unit_id = un.unit_id " &
                               "ORDER BY b.due_date DESC"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            BillingGrid.DataSource = dt
            BillingGrid.Columns("bill_id").Visible = False

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub BillingGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BillingGrid.CellClick
        If BillingGrid.SelectedRows.Count > 0 Then
            Dim row = BillingGrid.SelectedRows(0)
            selectedBillId = row.Cells("bill_id").Value

            TenantTxt.Text = row.Cells("Tenant").Value.ToString()
            UnitTxt.Text = row.Cells("Unit").Value.ToString()
            BRTxt.Text = row.Cells("Base Rent").Value.ToString()
            AddChargeTxt.Text = row.Cells("Add. Charges").Value.ToString()
            DateTxt.Text = Convert.ToDateTime(row.Cells("Due Date").Value).ToString("yyyy-MM-dd")
            StatusTxt.Text = row.Cells("Status").Value.ToString()

            Dim totalDue As Decimal = Convert.ToDecimal(row.Cells("Total Due").Value)

            Dim conn As New MySqlConnection(connStr)
            conn.Open()

            Dim cmd As New MySqlCommand("SELECT SUM(amount_paid) FROM payments WHERE bill_id = @bill_id", conn)
            cmd.Parameters.AddWithValue("@bill_id", selectedBillId)
            Dim result = cmd.ExecuteScalar()

            conn.Close()

            Dim amountPaid As Decimal = 0
            If Not IsDBNull(result) Then
                amountPaid = Convert.ToDecimal(result)
            End If

            TotalTxt.Text = (totalDue - amountPaid).ToString("0.00")
        End If
    End Sub


    Private Sub btnGenerateBill_Click(sender As Object, e As EventArgs) Handles BillBtn.Click
        Form16.ShowDialog()
        loadBills()
    End Sub


    Private Sub btnRecordBill_Click(sender As Object, e As EventArgs) Handles RecordBtn.Click
        If selectedBillId = 0 Then
            MessageBox.Show("Please select a bill first.")
            Return
        End If

        Dim frm As New Form17(selectedBillId)
        frm.ShowDialog()
        loadBills()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles deleteBtn.Click
        If selectedBillId = 0 Then
            MessageBox.Show("Please select a bill to delete.")
            Return
        End If

        Dim confirm = MessageBox.Show("Are you sure you want to delete this bill? " &
            "This will also remove any recorded payment(s) tied to it. This cannot be undone.",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If confirm <> DialogResult.Yes Then Return

        Dim conn As New MySqlConnection(connStr)
        Try
            conn.Open()
            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                Dim cmdPayments As New MySqlCommand("DELETE FROM payments WHERE bill_id = @bill_id", conn, transaction)
                cmdPayments.Parameters.AddWithValue("@bill_id", selectedBillId)
                cmdPayments.ExecuteNonQuery()

                Dim cmdBill As New MySqlCommand("DELETE FROM bills WHERE bill_id = @bill_id", conn, transaction)
                cmdBill.Parameters.AddWithValue("@bill_id", selectedBillId)
                cmdBill.ExecuteNonQuery()

                transaction.Commit()

                MessageBox.Show("Bill deleted successfully!")
                selectedBillId = 0
                loadBills()
            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show("Delete failed, no changes were made: " & ex.Message)
            End Try
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub saveBtn_Click(sender As Object, e As EventArgs) Handles saveBtn.Click
        If selectedBillId = 0 Then
            MessageBox.Show("Please select a bill to update.")
            Return
        End If

        Dim baseRentVal As Decimal
        If Not Decimal.TryParse(BRTxt.Text, baseRentVal) Then
            MessageBox.Show("Invalid base rent.")
            Return
        End If

        Dim addChargeVal As Decimal
        If Not Decimal.TryParse(AddChargeTxt.Text, addChargeVal) Then
            addChargeVal = 0
        End If

        Dim conn As New MySqlConnection(connStr)
        Try
            conn.Open()

            Dim cmd As New MySqlCommand(
            "UPDATE bills SET base_rent = @base_rent, addtional_charges = @add_charges, " &
            "due_date = @due_date WHERE bill_id = @bill_id", conn)
            cmd.Parameters.AddWithValue("@base_rent", baseRentVal)
            cmd.Parameters.AddWithValue("@add_charges", addChargeVal)
            cmd.Parameters.AddWithValue("@due_date", DateTxt.Text)
            cmd.Parameters.AddWithValue("@bill_id", selectedBillId)
            cmd.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show("Bill updated successfully!")
            loadBills()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        Form19.RefreshAndShow()
        Me.Hide()
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

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        Form18.RefreshAndShow()
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