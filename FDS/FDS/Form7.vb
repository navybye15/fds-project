Imports MySql.Data.MySqlClient
Public Class Form7


    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        statusCmb.Items.Clear()
        statusCmb.Items.AddRange({"occupied", "maintenance", "available"})
        statusCmb.DropDownStyle = ComboBoxStyle.DropDownList

        loadUnits()
    End Sub

    Private Sub loadUnits()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim cmdTotal As New MySqlCommand("SELECT COUNT(*) FROM units", conn)
            totalUnitsLbl.Text = cmdTotal.ExecuteScalar().ToString()


            Dim query As String = "SELECT u.unit_id, u.unit_number AS 'Unit #', u.type AS 'Type', u.floor AS 'Floor', " &
                                   "u.monthly_rate AS 'Monthly Rate', u.unit_status AS 'Status', t.full_name AS 'Tenant' FROM units u LEFT JOIN leases l ON u.unit_id = l.unit_id JOIN tenants t ON l.tenant_id = t.tenant_id"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            UnitsGrid.DataSource = dt


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

        Form14.ShowDialog()
        loadUnits()
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
            Dim unitId As Integer = Convert.ToInt32(UnitsGrid.SelectedRows(0).Cells("unit_id").Value)


            If Not statusCmb.Text.Equals("occupied", StringComparison.OrdinalIgnoreCase) Then
                Dim checkQuery As String = "SELECT COUNT(*) FROM leases WHERE unit_id = @unit_id AND status = 'active'"
                Dim checkCmd As New MySqlCommand(checkQuery, conn)
                checkCmd.Parameters.AddWithValue("@unit_id", unitId)

                Dim activeLeaseCount As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                If activeLeaseCount > 0 Then
                    MessageBox.Show("Cannot change status of Unit " & selectedUnitNumber & " because it still has an active tenant. Please end or reassign the lease first.",
                                     "Cannot Update", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If


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


            If billCount > 0 OrElse leaseCount > 0 OrElse expenseCount > 0 Then
                Dim msg As String = "Cannot delete Unit " & selectedUnitNumber & " because it still has related records:" & vbCrLf
                If billCount > 0 Then msg &= "- " & billCount & " bill(s)" & vbCrLf
                If leaseCount > 0 Then msg &= "- " & leaseCount & " lease(s)" & vbCrLf
                If expenseCount > 0 Then msg &= "- " & expenseCount & " expense(s)" & vbCrLf
                msg &= vbCrLf & "Please remove or reassign those records first before deleting this unit."

                MessageBox.Show(msg, "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If


            Dim cmd As New MySqlCommand("DELETE FROM units WHERE unit_id = @unit_id", conn)
            cmd.Parameters.AddWithValue("@unit_id", unitId)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Unit deleted successfully!")
            clearFields()
            loadUnits()

        Catch ex As MySqlException When ex.Number = 1451

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

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Form9.Show()
        Me.Hide()

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Form15.Show()
        Me.Hide()
    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        Form18.Show()
        Me.Hide()
    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        Form19.Show()
        Me.Hide()
    End Sub

    Private Sub btnSignOut_Click(sender As Object, e As EventArgs) Handles btnSignOut.Click
        Session.SignOut(Me)
    End Sub
End Class