Imports MySql.Data.MySqlClient
Public Class Form12
    Dim selectedExpenseId As Integer = 0
    Dim loadingRow As Boolean = False
    Private Sub Form12_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CategoryCmb.Items.Count = 0 Then
            CategoryCmb.Items.AddRange(New String() {"Maintenance", "Utilities", "Other"})
        End If
        loadExpenses()
    End Sub

    Public Sub RefreshAndShow()
        loadExpenses()
        Me.Show()
    End Sub

    Private Sub loadExpenses()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim cmdMonth As New MySqlCommand(
                "SELECT IFNULL(SUM(amount), 0) FROM expenses " &
                "WHERE MONTH(expense_date) = MONTH(CURDATE()) AND YEAR(expense_date) = YEAR(CURDATE())", conn)
            totalmonthLbl.Text = "₱" & Convert.ToDecimal(cmdMonth.ExecuteScalar()).ToString("N2")

            Dim cmdMaint As New MySqlCommand("SELECT IFNULL(SUM(amount), 0) FROM expenses WHERE expense_type = 'Maintenance'", conn)
            maintenanceLbl.Text = "₱" & Convert.ToDecimal(cmdMaint.ExecuteScalar()).ToString("N2")

            Dim cmdUtil As New MySqlCommand("SELECT IFNULL(SUM(amount), 0) FROM expenses WHERE expense_type = 'Utilities'", conn)
            utilitiesLbl.Text = "₱" & Convert.ToDecimal(cmdUtil.ExecuteScalar()).ToString("N2")

            Dim cmdMisc As New MySqlCommand("SELECT IFNULL(SUM(amount), 0) FROM expenses WHERE expense_type NOT IN ('Maintenance', 'Utilities')", conn)
            misctotalLbl.Text = "₱" & Convert.ToDecimal(cmdMisc.ExecuteScalar()).ToString("N2")

            Dim query As String =
                "SELECT e.expense_id, " &
                "DATE_FORMAT(e.expense_date, '%Y-%m-%d') AS 'Date', " &
                "e.expense_type AS 'Category', " &
                "e.description AS 'Description', " &
                "e.amount AS 'Amount', " &
                "e.recorded_by AS 'Recorded By', " &
                "e.unit_id, " &
                "u.unit_number AS 'Unit Number' " &
                "FROM expenses e LEFT JOIN units u ON e.unit_id = u.unit_id " &
                "ORDER BY e.expense_date DESC"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ExpenseRecGrid.DataSource = dt
            ExpenseRecGrid.Columns("expense_id").Visible = False
            ExpenseRecGrid.Columns("unit_id").Visible = False

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
            DescriptionTxt.Text = row.Cells("Description").Value.ToString()
            AmtTxt.Text = row.Cells("Amount").Value.ToString()
            RecordedTxt.Text = row.Cells("Recorded By").Value.ToString()

            Dim unitIdVal As Integer? = Nothing
            If Not IsDBNull(row.Cells("unit_id").Value) Then
                unitIdVal = Convert.ToInt32(row.Cells("unit_id").Value)
            End If

            loadingRow = True
            CategoryCmb.Text = row.Cells("Category").Value.ToString()
            loadUnitsForCategory(CategoryCmb.Text, unitIdVal)
            loadingRow = False
        End If
    End Sub

    Private Sub CategoryCmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CategoryCmb.SelectedIndexChanged
        If loadingRow Then Return
        loadUnitsForCategory(CategoryCmb.Text)
    End Sub

    Private Sub loadUnitsForCategory(category As String, Optional preselectUnitId As Integer? = Nothing)
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String
            If category = "Maintenance" Then
                query = "SELECT unit_id, unit_number FROM units WHERE unit_status = 'maintenance' ORDER BY unit_number"
            Else
                query = "SELECT unit_id, unit_number FROM units WHERE unit_status <> 'maintenance' ORDER BY unit_number"
            End If

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            dt.Columns.Add("unit_id", GetType(Integer))
            dt.Columns.Add("unit_number", GetType(String))
            adapter.Fill(dt)

            Dim noUnitRow As DataRow = dt.NewRow()
            noUnitRow("unit_id") = DBNull.Value
            noUnitRow("unit_number") = "-- No specific unit --"
            dt.Rows.InsertAt(noUnitRow, 0)

            UnitCmb.DataSource = dt
            UnitCmb.DisplayMember = "unit_number"
            UnitCmb.ValueMember = "unit_id"

            If preselectUnitId.HasValue Then
                Dim foundIndex As Integer = -1
                For i As Integer = 0 To dt.Rows.Count - 1
                    If Not IsDBNull(dt.Rows(i)("unit_id")) AndAlso Convert.ToInt32(dt.Rows(i)("unit_id")) = preselectUnitId.Value Then
                        foundIndex = i
                        Exit For
                    End If
                Next
                UnitCmb.SelectedIndex = If(foundIndex >= 0, foundIndex, 0)
            Else
                UnitCmb.SelectedIndex = 0
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading units: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
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

        Dim unitIdVal As Object = DBNull.Value
        If UnitCmb.SelectedValue IsNot Nothing AndAlso Not IsDBNull(UnitCmb.SelectedValue) Then
            unitIdVal = UnitCmb.SelectedValue
        End If

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim cmd As New MySqlCommand(
                "UPDATE expenses SET expense_date = @expense_date, expense_type = @expense_type, " &
                "unit_id = @unit_id, description = @description, amount = @amount, recorded_by = @recorded_by " &
                "WHERE expense_id = @expense_id", conn)
            cmd.Parameters.AddWithValue("@expense_date", Datetxt.Text)
            cmd.Parameters.AddWithValue("@expense_type", CategoryCmb.Text)
            cmd.Parameters.AddWithValue("@unit_id", unitIdVal)
            cmd.Parameters.AddWithValue("@description", DescriptionTxt.Text)
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
        UnitCmb.DataSource = Nothing
        UnitCmb.Text = ""
        selectedExpenseId = 0
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
        Form18.RefreshAndShow()
        Me.Hide()
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Typelbl_Click(sender As Object, e As EventArgs) Handles Typelbl.Click

    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        Form19.RefreshAndShow()
        Me.Hide()
    End Sub
End Class