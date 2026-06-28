Imports MySql.Data.MySqlClient
Public Class Form10
    Private Sub Form10_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadAvailableUnits()
    End Sub
    Private Sub loadAvailableUnits()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String = "SELECT unit_id, CONCAT(unit_number, ' (', type, ') — ₱', monthly_rate, '/mo') AS display " &
                                   "FROM units WHERE unit_status = 'available'"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            assignUnitCmb.DataSource = dt
            assignUnitCmb.DisplayMember = "display"
            assignUnitCmb.ValueMember = "unit_id"

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub createTenantBtn_Click(sender As Object, e As EventArgs) Handles createTenantBtn.Click
        If FullNametxt.Text = "" Or Usernametxt.Text = "" Or Passwordtxt.Text = "" Then
            MessageBox.Show("Full Name, Username, and Password are required.")
            Return
        End If

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            ' 1. Insert sa users
            Dim cmdUser As New MySqlCommand(
                "INSERT INTO users (username, password, role) VALUES (@username, @password, 'tenant')", conn)
            cmdUser.Parameters.AddWithValue("@username", Usernametxt.Text)
            cmdUser.Parameters.AddWithValue("@password", Passwordtxt.Text)
            cmdUser.ExecuteNonQuery()

            ' 2. Get new user_id
            Dim cmdUserId As New MySqlCommand("SELECT LAST_INSERT_ID()", conn)
            Dim newUserId = cmdUserId.ExecuteScalar().ToString()

            ' 3. Insert sa tenants
            Dim cmdTenant As New MySqlCommand(
                "INSERT INTO tenants (user_id, full_name, contact_no, emergency_contact, gov_id) " &
                "VALUES (@user_id, @full_name, @contact_no, @emergency_contact, @gov_id)", conn)
            cmdTenant.Parameters.AddWithValue("@user_id", newUserId)
            cmdTenant.Parameters.AddWithValue("@full_name", FullNametxt.Text)
            cmdTenant.Parameters.AddWithValue("@contact_no", contactTxt.Text)
            cmdTenant.Parameters.AddWithValue("@emergency_contact", emergencyTxt.Text)
            cmdTenant.Parameters.AddWithValue("@gov_id", govIdTxt.Text)
            cmdTenant.ExecuteNonQuery()

            ' 4. Get new tenant_id
            Dim cmdTenantId As New MySqlCommand("SELECT LAST_INSERT_ID()", conn)
            Dim newTenantId = cmdTenantId.ExecuteScalar().ToString()

            ' 5. Insert sa leases
            Dim selectedUnitId = assignUnitCmb.SelectedValue.ToString()

            Dim cmdLease As New MySqlCommand(
                "INSERT INTO leases (tenant_id, unit_id, lease_start, lease_end, monthly_rent, security_deposit, status) " &
                "VALUES (@tenant_id, @unit_id, @lease_start, @lease_end, " &
                "(SELECT monthly_rate FROM units WHERE unit_id = @unit_id), 0, 'active')", conn)
            cmdLease.Parameters.AddWithValue("@tenant_id", newTenantId)
            cmdLease.Parameters.AddWithValue("@unit_id", selectedUnitId)
            cmdLease.Parameters.AddWithValue("@lease_start", leaseStartDtp.Value.ToString("yyyy-MM-dd"))
            cmdLease.Parameters.AddWithValue("@lease_end", leaseEndDtp.Value.ToString("yyyy-MM-dd"))
            cmdLease.ExecuteNonQuery()

            ' 6. Update unit status to occupied
            Dim cmdUnit As New MySqlCommand(
                "UPDATE units SET unit_status = 'occupied' WHERE unit_id = @unit_id", conn)
            cmdUnit.Parameters.AddWithValue("@unit_id", selectedUnitId)
            cmdUnit.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show("Tenant created successfully!")
            Me.Close() ' isara ang Form10
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub cancelBtn_Click(sender As Object, e As EventArgs) Handles cancelBtn.Click
        Me.Close()
    End Sub

End Class