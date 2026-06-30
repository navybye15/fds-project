Imports MySql.Data.MySqlClient
Public Class Form7


    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadUnits()
    End Sub

    Private Sub loadUnits()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            ' === Total Units count ===
            Dim cmdTotal As New MySqlCommand("SELECT COUNT(*) FROM units", conn)
            totalUnitsLbl.Text = cmdTotal.ExecuteScalar().ToString()

            ' === Load sa DataGridView (idinagdag ang unit_id para sa internal use) ===
            Dim query As String = "SELECT unit_id, unit_number AS 'Unit #', type AS 'Type', floor AS 'Floor', " &
                                   "monthly_rate AS 'Monthly Rate', unit_status AS 'Status' FROM units"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            UnitsGrid.DataSource = dt

            ' Itago ang unit_id column sa view pero pwede pa rin i-access sa code
            If UnitsGrid.Columns.Contains("unit_id") Then
                UnitsGrid.Columns("unit_id").Visible = False
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub addUnitBtn_Click(sender As Object, e As EventArgs) Handles addUnitBtn.Click
        ' Basic validation muna
        If String.IsNullOrWhiteSpace(unitNumbertxt.Text) Then
            MessageBox.Show("Please enter a unit number.")
            Return
        End If

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

            MessageBox.Show("Unit added successfully!")
            clearFields()
            loadUnits()
        Catch ex As MySqlException When ex.Number = 1062
            ' Duplicate entry (unit_number ay UNIQUE)
            MessageBox.Show("That unit number already exists. Please use a different unit number.")
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
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

            MessageBox.Show("Unit updated successfully!")
            clearFields()
            loadUnits()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub deleteBtn_Click(sender As Object, e As EventArgs) Handles deleteBtn.Click
        If UnitsGrid.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a unit to delete.")
            Return
        End If

        Dim confirm = MessageBox.Show("Are you sure you want to delete this unit?", "Confirm Delete", MessageBoxButtons.YesNo)
        If confirm <> DialogResult.Yes Then Return

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim unitId As Integer = Convert.ToInt32(UnitsGrid.SelectedRows(0).Cells("unit_id").Value)
            Dim selectedUnitNumber = UnitsGrid.SelectedRows(0).Cells("Unit #").Value.ToString()

            ' === STEP 1: Check muna kung may related records (bills, leases, expenses) ===
            Dim checkQuery As String = "SELECT " &
                "(SELECT COUNT(*) FROM bills WHERE unit_id = @unit_id) AS bill_count, " &
                "(SELECT COUNT(*) FROM leases WHERE unit_id = @unit_id) AS lease_count, " &
                "(SELECT COUNT(*) FROM expenses WHERE unit_id = @unit_id) AS expense_count"

            Dim checkCmd As New MySqlCommand(checkQuery, conn)
            checkCmd.Parameters.AddWithValue("@unit_id", unitId)

            Dim billCount As Integer = 0
            Dim leaseCount As Integer = 0
            Dim expenseCount As Integer = 0

            Using reader As MySqlDataReader = checkCmd.ExecuteReader()
                If reader.Read() Then
                    billCount = Convert.ToInt32(reader("bill_count"))
                    leaseCount = Convert.ToInt32(reader("lease_count"))
                    expenseCount = Convert.ToInt32(reader("expense_count"))
                End If
            End Using

            ' === STEP 2: Kung may related records, huwag ituloy ang delete ===
            If billCount > 0 OrElse leaseCount > 0 OrElse expenseCount > 0 Then
                Dim msg As String = "Cannot delete Unit " & selectedUnitNumber & " because it still has related records:" & vbCrLf
                If billCount > 0 Then msg &= "- " & billCount & " bill(s)" & vbCrLf
                If leaseCount > 0 Then msg &= "- " & leaseCount & " lease(s)" & vbCrLf
                If expenseCount > 0 Then msg &= "- " & expenseCount & " expense(s)" & vbCrLf
                msg &= vbCrLf & "Please remove or reassign those records first before deleting this unit."

                MessageBox.Show(msg, "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' === STEP 3: Walang related records, ligtas na i-delete ===
            Dim cmd As New MySqlCommand("DELETE FROM units WHERE unit_id = @unit_id", conn)
            cmd.Parameters.AddWithValue("@unit_id", unitId)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Unit deleted successfully!")
            clearFields()
            loadUnits()

        Catch ex As MySqlException When ex.Number = 1451
            ' Fallback safety net kung sakaling may dumaan sa check sa itaas
            MessageBox.Show("Cannot delete this unit because it is still linked to other records (bills, leases, or expenses).",
                             "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
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

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Form12.Show()
        Me.Hide()

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs) Handles Label25.Click
        Form6.Show()
        Me.Hide()

    End Sub
End Class