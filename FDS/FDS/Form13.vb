Imports MySql.Data.MySqlClient
Public Class Form13

    Private Sub Form13_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadTenants()
        loadAvailableUnits()
        loadStatusOptions()
    End Sub

    Private Sub loadTenants()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()
            Dim query As String = "SELECT tenant_id, full_name FROM tenants ORDER BY full_name"
            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            tenantCmb.DataSource = dt
            tenantCmb.DisplayMember = "full_name"
            tenantCmb.ValueMember = "tenant_id"

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading tenants: " & ex.Message)
        End Try
    End Sub

    Private Sub loadAvailableUnits()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()
            Dim query As String = "SELECT unit_id, CONCAT(unit_number, ' (', type, ') — ₱', monthly_rate, '/mo') AS display, monthly_rate " &
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
            MessageBox.Show("Error loading units: " & ex.Message)
        End Try
    End Sub

    Private Sub loadStatusOptions()
        statusCmb.Items.Clear()
        statusCmb.Items.AddRange({"active", "terminated", "expired"})
        statusCmb.SelectedIndex = 0
    End Sub

    ' Simpleng paraan: mag-query ulit sa database para makuha ang monthly_rate,
    ' imbes na mag-cast ng DataRowView mula sa ComboBox.
    Private Sub assignUnitCmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles assignUnitCmb.SelectedIndexChanged
        If assignUnitCmb.SelectedValue Is Nothing Then Return

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT monthly_rate FROM units WHERE unit_id = @unit_id", conn)
            cmd.Parameters.AddWithValue("@unit_id", assignUnitCmb.SelectedValue.ToString())
            Dim rate = cmd.ExecuteScalar()

            If rate IsNot Nothing Then
                monthlyRateTxt.Text = rate.ToString()
            End If

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub createLeaseBtn_Click(sender As Object, e As EventArgs) Handles createLeaseBtn.Click
        If tenantCmb.SelectedValue Is Nothing Or assignUnitCmb.SelectedValue Is Nothing Then
            MessageBox.Show("Please select a tenant and a unit.")
            Return
        End If

        If leaseEndDtp.Value <= leaseStartDtp.Value Then
            MessageBox.Show("Lease End date must be after Lease Start date.")
            Return
        End If

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim tenantId As String = tenantCmb.SelectedValue.ToString()
            Dim unitId As String = assignUnitCmb.SelectedValue.ToString()

            ' Step 1: Simpleng check kung may active lease na ang tenant (walang subquery)
            Dim cmdCheck As New MySqlCommand("SELECT COUNT(*) FROM leases WHERE tenant_id = @tenant_id AND status = 'active'", conn)
            cmdCheck.Parameters.AddWithValue("@tenant_id", tenantId)
            Dim activeCount As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

            If activeCount > 0 Then
                Dim proceed = MessageBox.Show("This tenant already has an active lease. Continue anyway?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If proceed <> DialogResult.Yes Then
                    conn.Close()
                    Return
                End If
            End If

            ' Step 2: I-insert ang bagong lease record
            Dim cmdLease As New MySqlCommand(
                "INSERT INTO leases (tenant_id, unit_id, lease_start, lease_end, monthly_rent, security_deposit, status) " &
                "VALUES (@tenant_id, @unit_id, @lease_start, @lease_end, @monthly_rent, @security_deposit, @status)", conn)
            cmdLease.Parameters.AddWithValue("@tenant_id", tenantId)
            cmdLease.Parameters.AddWithValue("@unit_id", unitId)
            cmdLease.Parameters.AddWithValue("@lease_start", leaseStartDtp.Value.ToString("yyyy-MM-dd"))
            cmdLease.Parameters.AddWithValue("@lease_end", leaseEndDtp.Value.ToString("yyyy-MM-dd"))
            cmdLease.Parameters.AddWithValue("@monthly_rent", monthlyRateTxt.Text)
            cmdLease.Parameters.AddWithValue("@security_deposit", If(securityDepositTxt.Text = "", "0", securityDepositTxt.Text))
            cmdLease.Parameters.AddWithValue("@status", statusCmb.Text)
            cmdLease.ExecuteNonQuery()

            ' Step 3: Kung active ang status, i-update ang unit status (hiwalay na command)
            If statusCmb.Text = "active" Then
                Dim cmdUnit As New MySqlCommand("UPDATE units SET unit_status = 'occupied' WHERE unit_id = @unit_id", conn)
                cmdUnit.Parameters.AddWithValue("@unit_id", unitId)
                cmdUnit.ExecuteNonQuery()
            End If

            conn.Close()

            MessageBox.Show("Lease created successfully!")
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub cancelBtn_Click(sender As Object, e As EventArgs) Handles cancelBtn.Click
        Me.Close()
    End Sub

End Class