Imports MySql.Data.MySqlClient
Public Class Form8
    Dim selectedTenantId As Integer = 0
    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadTenants()
    End Sub


    Private Sub loadTenants()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim cmdTotal As New MySqlCommand("SELECT COUNT(*) FROM tenants", conn)
            totalTenantsLbl.Text = cmdTotal.ExecuteScalar().ToString()

            Dim query As String = "SELECT t.tenant_id, t.full_name AS 'Name', " &
                                   "un.unit_number AS 'Unit', " &
                                   "t.contact_no AS 'Contact', " &
                                   "l.lease_start AS 'Lease Start', " &
                                   "l.lease_end AS 'Lease End', " &
                                   "l.status AS 'Status' " &
                                   "FROM tenants t " &
                                   "LEFT JOIN leases l ON t.tenant_id = l.tenant_id AND l.status = 'active' " &
                                   "LEFT JOIN units un ON l.unit_id = un.unit_id"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            TenantsGrid.DataSource = dt
            TenantsGrid.Columns("tenant_id").Visible = False

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub TenantsGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TenantsGrid.CellClick
        If TenantsGrid.SelectedRows.Count > 0 Then
            Dim row = TenantsGrid.SelectedRows(0)
            selectedTenantId = row.Cells("tenant_id").Value

            ' Basic info from grid
            FullNametxt.Text = row.Cells("Name").Value.ToString()
            contactTxt.Text = row.Cells("Contact").Value.ToString()

            If row.Cells("Lease Start").Value.ToString() <> "" Then
                LeaseStarttxt.Text = row.Cells("Lease Start").Value.ToString()
            End If

            If row.Cells("Lease End").Value.ToString() <> "" Then
                LeaseEndtxt.Text = row.Cells("Lease End").Value.ToString()
            End If

            ' Fetch additional info from database
            Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
            Dim conn As New MySqlConnection(connStr)

            Try
                conn.Open()

                Dim query As String = "SELECT t.emergency_contact, t.gov_id, u.username, u.password " &
                                   "FROM tenants t " &
                                   "JOIN users u ON t.user_id = u.user_id " &
                                   "WHERE t.tenant_id = @tenant_id"

                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@tenant_id", selectedTenantId)

                Dim reader = cmd.ExecuteReader()

                If reader.Read() Then
                    emergencyTxt.Text = reader("emergency_contact").ToString()
                    govIdTxt.Text = reader("gov_id").ToString()
                    Usernametxt.Text = reader("username").ToString()
                    Passwordtxt.Text = reader("password").ToString()
                End If

                reader.Close()
                conn.Close()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub saveBtn_Click(sender As Object, e As EventArgs) Handles saveBtn.Click
        If selectedTenantId = 0 Then
            MessageBox.Show("Please select a tenant to update.")
            Return
        End If

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim cmdTenant As New MySqlCommand(
                "UPDATE tenants SET full_name = @full_name, contact_no = @contact_no, " &
                "emergency_contact = @emergency_contact, gov_id = @gov_id " &
                "WHERE tenant_id = @tenant_id", conn)
            cmdTenant.Parameters.AddWithValue("@full_name", FullNametxt.Text)
            cmdTenant.Parameters.AddWithValue("@contact_no", contactTxt.Text)
            cmdTenant.Parameters.AddWithValue("@emergency_contact", emergencyTxt.Text)
            cmdTenant.Parameters.AddWithValue("@gov_id", govIdTxt.Text)
            cmdTenant.Parameters.AddWithValue("@tenant_id", selectedTenantId)
            cmdTenant.ExecuteNonQuery()

            ' Update lease dates
            Dim cmdLease As New MySqlCommand(
                "UPDATE leases SET lease_start = @lease_start, lease_end = @lease_end " &
                "WHERE tenant_id = @tenant_id AND status = 'active'", conn)
            cmdLease.Parameters.AddWithValue("@lease_start", LeaseStarttxt.Text)
            cmdLease.Parameters.AddWithValue("@lease_end", LeaseEndtxt.Text)
            cmdLease.Parameters.AddWithValue("@tenant_id", selectedTenantId)
            cmdLease.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show("Tenant updated successfully!")
            clearFields()
            loadTenants()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub deleteBtn_Click(sender As Object, e As EventArgs) Handles deleteBtn.Click
        If selectedTenantId = 0 Then
            MessageBox.Show("Please select a tenant to delete.")
            Return
        End If

        Dim confirm = MessageBox.Show("Are you sure you want to delete this tenant?", "Confirm Delete", MessageBoxButtons.YesNo)

        If confirm = DialogResult.Yes Then
            Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
            Dim conn As New MySqlConnection(connStr)

            Try
                conn.Open()

                ' Free up ang unit
                Dim cmdFreeUnit As New MySqlCommand(
                    "UPDATE units SET unit_status = 'available' " &
                    "WHERE unit_id = (SELECT unit_id FROM leases WHERE tenant_id = @tenant_id AND status = 'active')", conn)
                cmdFreeUnit.Parameters.AddWithValue("@tenant_id", selectedTenantId)
                cmdFreeUnit.ExecuteNonQuery()

                ' Terminate lease
                Dim cmdLease As New MySqlCommand(
                    "UPDATE leases SET status = 'terminated' WHERE tenant_id = @tenant_id", conn)
                cmdLease.Parameters.AddWithValue("@tenant_id", selectedTenantId)
                cmdLease.ExecuteNonQuery()

                ' Delete tenant
                Dim cmdTenant As New MySqlCommand(
                    "DELETE FROM tenants WHERE tenant_id = @tenant_id", conn)
                cmdTenant.Parameters.AddWithValue("@tenant_id", selectedTenantId)
                cmdTenant.ExecuteNonQuery()

                conn.Close()

                MessageBox.Show("Tenant deleted successfully!")
                clearFields()
                selectedTenantId = 0
                loadTenants()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub addTenantBtn_Click(sender As Object, e As EventArgs) Handles addTenantBtn.Click
        Form10.ShowDialog() ' popup, hindi mawawala Form8
        loadTenants() ' i-refresh ang grid pagkatapos mag-add
    End Sub

    Private Sub clearFields()
        FullNametxt.Text = ""
        contactTxt.Text = ""
        Usernametxt.Text = ""
        Passwordtxt.Text = ""
        emergencyTxt.Text = ""
        govIdTxt.Text = ""
        LeaseStarttxt.Text = ""
        LeaseEndtxt.Text = ""
        selectedTenantId = 0
    End Sub
End Class