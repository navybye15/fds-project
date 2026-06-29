Imports MySql.Data.MySqlClient
Public Class Form11

    Private Sub Form11_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        resetFields()
    End Sub

    Private Sub resetFields()
        If CategoryCmb.Items.Count = 0 Then
            ' Base sa existing data mo: Maintenance, Utilities, Repair, Pest Control
            CategoryCmb.Items.AddRange(New String() {"Maintenance", "Utilities", "Repair", "Pest Control", "Other"})
        End If
        CategoryCmb.SelectedIndex = -1
        CategoryCmb.Text = ""

        DateIncurredDtp.Value = DateTime.Today
        DescriptionTxt.Text = ""
        AmtTxt.Text = ""
        RecordedTxt.Text = ""
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

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim cmd As New MySqlCommand(
                "INSERT INTO expenses (expense_date, expense_type, description, amount, recorded_by) " &
                "VALUES (@expense_date, @expense_type, @description, @amount, @recorded_by)", conn)
            cmd.Parameters.AddWithValue("@expense_date", DateIncurredDtp.Value.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@expense_type", CategoryCmb.Text)
            cmd.Parameters.AddWithValue("@description", descriptionTxt.Text)
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

End Class