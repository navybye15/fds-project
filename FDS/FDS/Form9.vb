Imports MySql.Data.MySqlClient
Public Class Form9
    Dim selectedLeaseId As Integer = 0

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadLeases()
    End Sub

    Public Sub RefreshAndShow()
        loadLeases()
        Me.Show()
    End Sub

    Private Sub loadLeases()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim cmdTotal As New MySqlCommand("SELECT COUNT(*) FROM leases", conn)
            totalLeasesLbl.Text = cmdTotal.ExecuteScalar().ToString()

            Dim query As String = "SELECT l.lease_id, t.full_name AS 'Tenant', un.unit_number AS 'Unit', " &
                                   "DATE_FORMAT(l.lease_start, '%b %d, %Y') AS 'Start Date', " &
                                   "DATE_FORMAT(l.lease_end, '%b %d, %Y') AS 'End Date', " &
                                   "l.monthly_rent AS 'Monthly Rent', " &
                                   "l.security_deposit AS 'Deposit', " &
                                   "l.status AS 'Status' " &
                                   "FROM leases l " &
                                   "JOIN tenants t ON l.tenant_id = t.tenant_id " &
                                   "JOIN units un ON l.unit_id = un.unit_id " &
                                   "ORDER BY l.lease_id DESC"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            LeasesGrid.DataSource = dt
            LeasesGrid.Columns("lease_id").Visible = False

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub LeasesGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles LeasesGrid.CellClick
        If LeasesGrid.SelectedRows.Count > 0 Then
            selectedLeaseId = LeasesGrid.SelectedRows(0).Cells("lease_id").Value
        End If
    End Sub

    Private Sub createLeaseBtn_Click(sender As Object, e As EventArgs) Handles createLeaseBtn.Click
        Form13.ShowDialog()
        loadLeases()
    End Sub


    Private Sub renewBtn_Click(sender As Object, e As EventArgs) Handles renewBtn.Click
        If selectedLeaseId = 0 Then
            MessageBox.Show("Please select a lease to renew.")
            Return
        End If

        Dim newStart As Date = renewStartPicker.Value
        Dim newEnd As Date = renewEndPicker.Value

        If newEnd <= newStart Then
            MessageBox.Show("End date must be after the start date.")
            Return
        End If

        Dim confirm = MessageBox.Show("Renew this lease from " & newStart.ToString("yyyy-MM-dd") &
            " to " & newEnd.ToString("yyyy-MM-dd") & "? A new lease record will be created.",
            "Confirm Renew", MessageBoxButtons.YesNo)
        If confirm <> DialogResult.Yes Then Return

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()


            Dim tenantId As String = ""
            Dim unitId As String = ""
            Dim rent As String = ""
            Dim deposit As String = ""

            Dim cmdGet As New MySqlCommand(
                "SELECT tenant_id, unit_id, monthly_rent, security_deposit FROM leases WHERE lease_id = @lease_id", conn)
            cmdGet.Parameters.AddWithValue("@lease_id", selectedLeaseId)

            Dim reader = cmdGet.ExecuteReader()
            If reader.Read() Then
                tenantId = reader("tenant_id").ToString()
                unitId = reader("unit_id").ToString()
                rent = reader("monthly_rent").ToString()
                deposit = reader("security_deposit").ToString()
            End If
            reader.Close()

            If tenantId = "" Then
                MessageBox.Show("Lease not found.")
                conn.Close()
                Return
            End If

            Dim cmdExpire As New MySqlCommand("UPDATE leases SET status = 'expired' WHERE lease_id = @lease_id", conn)
            cmdExpire.Parameters.AddWithValue("@lease_id", selectedLeaseId)
            cmdExpire.ExecuteNonQuery()

            Dim cmdNew As New MySqlCommand(
                "INSERT INTO leases (tenant_id, unit_id, lease_start, lease_end, monthly_rent, security_deposit, status) " &
                "VALUES (@tenant_id, @unit_id, @lease_start, @lease_end, @rent, @deposit, 'active')", conn)
            cmdNew.Parameters.AddWithValue("@tenant_id", tenantId)
            cmdNew.Parameters.AddWithValue("@unit_id", unitId)
            cmdNew.Parameters.AddWithValue("@lease_start", newStart.ToString("yyyy-MM-dd"))
            cmdNew.Parameters.AddWithValue("@lease_end", newEnd.ToString("yyyy-MM-dd"))
            cmdNew.Parameters.AddWithValue("@rent", rent)
            cmdNew.Parameters.AddWithValue("@deposit", deposit)
            cmdNew.ExecuteNonQuery()

            Dim cmdUnit As New MySqlCommand("UPDATE units SET unit_status = 'occupied' WHERE unit_id = @unit_id", conn)
            cmdUnit.Parameters.AddWithValue("@unit_id", unitId)
            cmdUnit.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show("Lease renewed successfully!")
            selectedLeaseId = 0
            loadLeases()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub


    Private Sub endBtn_Click(sender As Object, e As EventArgs) Handles endBtn.Click
        If selectedLeaseId = 0 Then
            MessageBox.Show("Please select a lease to end.")
            Return
        End If

        Dim confirm = MessageBox.Show("Terminate this lease? The unit will be marked Available.", "Confirm End Lease", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If confirm <> DialogResult.Yes Then Return

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()


            Dim cmdGetUnit As New MySqlCommand("SELECT unit_id FROM leases WHERE lease_id = @lease_id", conn)
            cmdGetUnit.Parameters.AddWithValue("@lease_id", selectedLeaseId)
            Dim unitId = cmdGetUnit.ExecuteScalar().ToString()


            Dim cmdEnd As New MySqlCommand("UPDATE leases SET status = 'terminated' WHERE lease_id = @lease_id", conn)
            cmdEnd.Parameters.AddWithValue("@lease_id", selectedLeaseId)
            cmdEnd.ExecuteNonQuery()


            Dim cmdUnit As New MySqlCommand("UPDATE units SET unit_status = 'available' WHERE unit_id = @unit_id", conn)
            cmdUnit.Parameters.AddWithValue("@unit_id", unitId)
            cmdUnit.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show("Lease terminated. Unit is now available.")
            selectedLeaseId = 0
            loadLeases()

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



    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Form15.RefreshAndShow()
        Me.Hide()
    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        Form18.RefreshAndShow()
        Me.Hide()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Form12.RefreshAndShow()
        Me.Hide()
    End Sub

    Private Sub btnSignOut_Click(sender As Object, e As EventArgs) Handles btnSignOut.Click
        Session.SignOut(Me)
    End Sub
End Class