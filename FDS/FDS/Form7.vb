Imports MySql.Data.MySqlClient
Public Class Form7


    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub loadUnits()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            ' === Total Units count ===
            Dim cmdTotal As New MySqlCommand("SELECT COUNT(*) FROM units", conn)
            totalUnitsLbl.Text = cmdTotal.ExecuteScalar().ToString()

            ' === Load sa DataGridView ===
            Dim query As String = "SELECT unit_number AS 'Unit #', type AS 'Type', floor AS 'Floor', " &
                                   "monthly_rate AS 'Monthly Rate', unit_status AS 'Status' FROM units"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            UnitsGrid.DataSource = dt

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub addUnitBtn_Click(sender As Object, e As EventArgs) Handles addUnitBtn.Click
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String = "INSERT INTO units (unit_number, type, floor, monthly_rate, unit_status) " &
                                   "VALUES (@unit_number, @type, @floor, @monthly_rate, @unit_status)"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@unit_number", unitNumbertxt.Text)
            cmd.Parameters.AddWithValue("@type", typeCmb.Text)
            cmd.Parameters.AddWithValue("@floor", floorTxt.Text)
            cmd.Parameters.AddWithValue("@monthly_rate", monthlyRateTxt.Text)
            cmd.Parameters.AddWithValue("@unit_status", statusCmb.Text)

            cmd.ExecuteNonQuery()
            conn.Close()

            MessageBox.Show("Unit added successfully!")
            clearFields()
            loadUnits()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub saveBtn_Click(sender As Object, e As EventArgs) Handles saveBtn.Click
        If UnitsGrid.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a unit to update.")
            Return
        End If

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim selectedUnitNumber = UnitsGrid.SelectedRows(0).Cells("Unit #").Value.ToString()

            Dim query As String = "UPDATE units SET type = @type, floor = @floor, " &
                                   "monthly_rate = @monthly_rate, unit_status = @unit_status " &
                                   "WHERE unit_number = @unit_number"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@unit_number", selectedUnitNumber)
            cmd.Parameters.AddWithValue("@type", typeCmb.Text)
            cmd.Parameters.AddWithValue("@floor", floorTxt.Text)
            cmd.Parameters.AddWithValue("@monthly_rate", monthlyRateTxt.Text)
            cmd.Parameters.AddWithValue("@unit_status", statusCmb.Text)

            cmd.ExecuteNonQuery()
            conn.Close()

            MessageBox.Show("Unit updated successfully!")
            clearFields()
            loadUnits()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub deleteBtn_Click(sender As Object, e As EventArgs) Handles deleteBtn.Click
        If UnitsGrid.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a unit to delete.")
            Return
        End If

        Dim confirm = MessageBox.Show("Are you sure you want to delete this unit?", "Confirm Delete", MessageBoxButtons.YesNo)

        If confirm = DialogResult.Yes Then
            Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
            Dim conn As New MySqlConnection(connStr)

            Try
                conn.Open()

                Dim selectedUnitNumber = UnitsGrid.SelectedRows(0).Cells("Unit #").Value.ToString()

                Dim cmd As New MySqlCommand("DELETE FROM units WHERE unit_number = @unit_number", conn)
                cmd.Parameters.AddWithValue("@unit_number", selectedUnitNumber)

                cmd.ExecuteNonQuery()
                conn.Close()

                MessageBox.Show("Unit deleted successfully!")
                clearFields()
                loadUnits()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub UnitsGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles UnitsGrid.CellClick
        If UnitsGrid.SelectedRows.Count > 0 Then
            Dim row = UnitsGrid.SelectedRows(0)
            unitNumbertxt.Text = row.Cells("Unit #").Value.ToString()
            typeCmb.Text = row.Cells("Type").Value.ToString()
            floorTxt.Text = row.Cells("Floor").Value.ToString()
            monthlyRateTxt.Text = row.Cells("Monthly Rate").Value.ToString()
            statusCmb.Text = row.Cells("Status").Value.ToString()
        End If
    End Sub

    Private Sub clearFields()
        unitNumbertxt.Text = ""
        typeCmb.Text = ""
        floorTxt.Text = ""
        monthlyRateTxt.Text = ""
        statusCmb.Text = ""
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Form8.Show()
        Me.Hide()

    End Sub
End Class