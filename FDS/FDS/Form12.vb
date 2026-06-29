Imports MySql.Data.MySqlClient
Public Class Form12
    Dim selectedExpenseId As Integer = 0

    Private Sub Form12_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadExpenses()
    End Sub

    Private Sub loadExpenses()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            ' === Total This Month (lahat ng expense_type) ===
            Dim cmdMonth As New MySqlCommand(
                "SELECT IFNULL(SUM(amount), 0) FROM expenses " &
                "WHERE MONTH(expense_date) = MONTH(CURDATE()) AND YEAR(expense_date) = YEAR(CURDATE())", conn)
            totalmonthLbl.Text = "₱" & Convert.ToDecimal(cmdMonth.ExecuteScalar()).ToString("N2")

            ' === Per-type totals (all-time; palitan ang WHERE kung gusto mo "this month" din) ===
            Dim cmdMaint As New MySqlCommand("SELECT IFNULL(SUM(amount), 0) FROM expenses WHERE expense_type = 'Maintenance'", conn)
            maintenanceLbl.Text = "₱" & Convert.ToDecimal(cmdMaint.ExecuteScalar()).ToString("N2")

            Dim cmdUtil As New MySqlCommand("SELECT IFNULL(SUM(amount), 0) FROM expenses WHERE expense_type = 'Utilities'", conn)
            utilitiesLbl.Text = "₱" & Convert.ToDecimal(cmdUtil.ExecuteScalar()).ToString("N2")

            ' "Misc/Oth" = lahat ng IBANG type maliban Maintenance/Utilities
            ' (sa data mo ngayon, ito ang Repair at Pest Control - automatic
            ' na masasama rito ang bagong expense_type sa future)
            Dim cmdMisc As New MySqlCommand("SELECT IFNULL(SUM(amount), 0) FROM expenses WHERE expense_type NOT IN ('Maintenance', 'Utilities')", conn)
            miscTotalLbl.Text = "₱" & Convert.ToDecimal(cmdMisc.ExecuteScalar()).ToString("N2")

            ' === Expense Records grid ===
            Dim query As String =
                "SELECT expense_id, " &
                "DATE_FORMAT(expense_date, '%Y-%m-%d') AS 'Date', " &
                "expense_type AS 'Category', " &
                "description AS 'Description', " &
                "amount AS 'Amount', " &
                "recorded_by AS 'Recorded By' " &
                "FROM expenses ORDER BY expense_date DESC"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ExpenseRecGrid.DataSource = dt
            ExpenseRecGrid.Columns("expense_id").Visible = False

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub ExpensesGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles ExpenseRecGrid.CellClick
        If ExpenseRecGrid.SelectedRows.Count > 0 Then
            Dim row = ExpenseRecGrid.SelectedRows(0)
            selectedExpenseId = Convert.ToInt32(row.Cells("expense_id").Value)

            Datetxt.Text = row.Cells("Date").Value.ToString()
            CategoryCmb.Text = row.Cells("Category").Value.ToString()
            DescriptionTxt.Text = row.Cells("Description").Value.ToString()
            AmtTxt.Text = row.Cells("Amount").Value.ToString()
            RecordedTxt.Text = row.Cells("Recorded By").Value.ToString()
        End If
    End Sub

    Private Sub saveBtn_Click(sender As Object, e As EventArgs) Handles saveBtn.Click
        If selectedExpenseId = 0 Then
            MessageBox.Show("Please select an expense record to update.")
            Return
        End If

        Dim amountVal As Decimal
        If Not Decimal.TryParse(AmtTxt.Text, amountVal) Then
            MessageBox.Show("Invalid amount.")
            Return
        End If

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim cmd As New MySqlCommand(
                "UPDATE expenses SET expense_date = @expense_date, expense_type = @expense_type, " &
                "description = @description, amount = @amount, recorded_by = @recorded_by " &
                "WHERE expense_id = @expense_id", conn)
            cmd.Parameters.AddWithValue("@expense_date", Datetxt.Text)
            cmd.Parameters.AddWithValue("@expense_type", CategoryCmb.Text)
            cmd.Parameters.AddWithValue("@description", descriptionTxt.Text)
            cmd.Parameters.AddWithValue("@amount", amountVal)
            cmd.Parameters.AddWithValue("@recorded_by", RecordedTxt.Text)
            cmd.Parameters.AddWithValue("@expense_id", selectedExpenseId)
            cmd.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show("Expense updated successfully!")
            clearFields()
            loadExpenses()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub deleteBtn_Click(sender As Object, e As EventArgs) Handles deleteBtn.Click
        If selectedExpenseId = 0 Then
            MessageBox.Show("Please select an expense record to delete.")
            Return
        End If

        Dim confirm = MessageBox.Show("Are you sure you want to delete this expense record? This cannot be undone.",
                                       "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If confirm <> DialogResult.Yes Then Return

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim cmd As New MySqlCommand("DELETE FROM expenses WHERE expense_id = @expense_id", conn)
            cmd.Parameters.AddWithValue("@expense_id", selectedExpenseId)
            cmd.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show("Expense deleted successfully!")
            clearFields()
            loadExpenses()
        Catch ex As Exception
            MessageBox.Show("Delete failed: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub addExpenseBtn_Click(sender As Object, e As EventArgs) Handles addExpenseBtn.Click
        ' Bagong instance tuwing bubuksan, tapos i-dispose pagkatapos -
        ' guaranteed fresh/blangko palagi (parehong fix gagawin natin
        ' sa Form8/Form10 sa ibaba).
        Dim addForm As New Form11()
        addForm.ShowDialog()
        addForm.Dispose()

        loadExpenses()
    End Sub

    Private Sub clearFields()
        Datetxt.Text = ""
        CategoryCmb.Text = ""
        DescriptionTxt.Text = ""
        AmtTxt.Text = ""
        RecordedTxt.Text = ""
        selectedExpenseId = 0
    End Sub

End Class