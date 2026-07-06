Imports MySql.Data.MySqlClient
Public Class Form9
    Dim selectedLeaseId As Integer = 0

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadLeases()
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

    ' === RENEW: simpleng paraan, hiwalay-hiwalay na commands, walang transaction ===
    Private Sub renewBtn_Click(sender As Object, e As EventArgs) Handles renewBtn.Click
        If selectedLeaseId = 0 Then
            MessageBox.Show("Please select a lease to renew.")
            Return
        End If

        Dim confirm = MessageBox.Show("Renew this lease for another year? A new lease record will be created.", "Confirm Renew", MessageBoxButtons.YesNo)
        If confirm <> DialogResult.Yes Then Return

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            ' Step 1: Kunin ang details ng napiling lease gamit ang basic reader
            Dim tenantId As String = ""
            Dim unitId As String = ""
            Dim oldEndText As String = ""
            Dim rent As String = ""
            Dim deposit As String = ""

            Dim cmdGet As New MySqlCommand(
                "SELECT tenant_id, unit_id, lease_end, monthly_rent, security_deposit FROM leases WHERE lease_id = @lease_id", conn)
            cmdGet.Parameters.AddWithValue("@lease_id", selectedLeaseId)

            Dim reader = cmdGet.ExecuteReader()
            If reader.Read() Then
                tenantId = reader("tenant_id").ToString()
                unitId = reader("unit_id").ToString()
                oldEndText = reader("lease_end").ToString()
                rent = reader("monthly_rent").ToString()
                deposit = reader("security_deposit").ToString()
            End If
            reader.Close()

            If tenantId = "" Then
                MessageBox.Show("Lease not found.")
                conn.Close()
                Return
            End If

            ' Step 2: Kwentahin ang bagong lease_start at lease_end (basic Date math na lang)
            Dim oldEnd As Date = Convert.ToDateTime(oldEndText)
            Dim newStart As Date = oldEnd.AddDays(1)
            Dim newEnd As Date = newStart.AddYears(1)

            ' Step 3: I-mark ang lumang lease as expired (hiwalay na command)
            Dim cmdExpire As New MySqlCommand("UPDATE leases SET status = 'expired' WHERE lease_id = @lease_id", conn)
            cmdExpire.Parameters.AddWithValue("@lease_id", selectedLeaseId)
            cmdExpire.ExecuteNonQuery()

            ' Step 4: Mag-insert ng bagong lease record (hiwalay na command)
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

            conn.Close()

            MessageBox.Show("Lease renewed successfully!")
            selectedLeaseId = 0
            loadLeases()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    ' === END LEASE: simpleng paraan, hiwalay-hiwalay na commands, walang transaction ===
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

            ' Step 1: Kunin muna ang unit_id ng lease na ito
            Dim cmdGetUnit As New MySqlCommand("SELECT unit_id FROM leases WHERE lease_id = @lease_id", conn)
            cmdGetUnit.Parameters.AddWithValue("@lease_id", selectedLeaseId)
            Dim unitId = cmdGetUnit.ExecuteScalar().ToString()

            ' Step 2: I-update ang lease status (hiwalay na command)
            Dim cmdEnd As New MySqlCommand("UPDATE leases SET status = 'terminated' WHERE lease_id = @lease_id", conn)
            cmdEnd.Parameters.AddWithValue("@lease_id", selectedLeaseId)
            cmdEnd.ExecuteNonQuery()

            ' Step 3: I-update ang unit status (hiwalay na command)
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
        Form19.Show()
        Me.Hide()
    End Sub
End Class