Imports MySql.Data.MySqlClient
Public Class Form11

    Private Sub Form11_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        resetFields()
    End Sub

    Private Sub resetFields()
        If CategoryCmb.Items.Count = 0 Then
            CategoryCmb.Items.AddRange(New String() {"Maintenance", "Utilities", "Other"})
        End If
        CategoryCmb.SelectedIndex = -1
        CategoryCmb.Text = ""

        UnitCmb.DataSource = Nothing
        UnitCmb.Items.Clear()
        UnitCmb.Text = ""

        DateIncurredDtp.Value = DateTime.Today
        DescriptionTxt.Text = ""
        AmtTxt.Text = ""
        RecordedTxt.Text = ""
    End Sub

    Private Sub CategoryCmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CategoryCmb.SelectedIndexChanged
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

    Private Sub cancelBtn_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub saveExpenseBtn_Click(sender As Object, e As EventArgs) Handles saveExpenseBtn.Click
        If String.IsNullOrWhiteSpace(CategoryCmb.Text) Then
            MessageBox.Show("Please select a category.")
            Return
        End If

        Dim amountVal As Decimal
        If Not Decimal.TryParse(AmtTxt.Text, amountVal) OrElse amountVal <= 0 Then
            MessageBox.Show("Please enter a valid amount.")
            Return
        End If

        ' Kunin ang napiling unit_id; DBNull kung "-- No specific unit --" ang napili
        Dim unitIdVal As Object = DBNull.Value
        If UnitCmb.SelectedValue IsNot Nothing AndAlso Not IsDBNull(UnitCmb.SelectedValue) Then
            unitIdVal = UnitCmb.SelectedValue
        End If

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim cmd As New MySqlCommand(
                "INSERT INTO expenses (expense_date, expense_type, unit_id, description, amount, recorded_by) " &
                "VALUES (@expense_date, @expense_type, @unit_id, @description, @amount, @recorded_by)", conn)
            cmd.Parameters.AddWithValue("@expense_date", DateIncurredDtp.Value.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@expense_type", CategoryCmb.Text)
            cmd.Parameters.AddWithValue("@unit_id", unitIdVal)
            cmd.Parameters.AddWithValue("@description", DescriptionTxt.Text)
            cmd.Parameters.AddWithValue("@amount", amountVal)
            cmd.Parameters.AddWithValue("@recorded_by", RecordedTxt.Text)
            cmd.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show("Expense recorded successfully!")

            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub
End Class