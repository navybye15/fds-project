Imports MySql.Data.MySqlClient
Public Class Form6

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        Button5.Enabled = False
        Button6.Enabled = False
        Button7.Enabled = False
        Button8.Enabled = False
        loadDashboard()

    End Sub
    Private Sub loadDashboard()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            ' === Date ===
            dateLbl.Text = DateTime.Now.ToString("MMMM dd, yyyy")
            dateLbl2.Text = DateTime.Now.ToString("MMMM yyyy")


            ' === Active Tenants count sa dark panel ===
            Dim cmdActive As New MySqlCommand(
            "SELECT COUNT(*) FROM leases WHERE status = 'active' " &
            "AND MONTH(lease_start) = MONTH(CURDATE()) " &
            "AND YEAR(lease_start) = YEAR(CURDATE())", conn)
            activeTenantsLbl.Text = cmdActive.ExecuteScalar().ToString() & " Active Tenants"

            ' === Total Units card ===
            Dim cmdUnits As New MySqlCommand("SELECT COUNT(*) FROM units", conn)
            totalUnitsLbl.Text = cmdUnits.ExecuteScalar().ToString()

            ' === Active Tenants card ===
            Dim cmdTenants As New MySqlCommand("SELECT COUNT(*) FROM leases WHERE status = 'active'", conn)
            activeTenantsTotalLbl.Text = cmdTenants.ExecuteScalar().ToString()

            ' === Unpaid Bills card ===
            Dim cmdUnpaid As New MySqlCommand("SELECT COUNT(*) FROM bills WHERE status IN ('unpaid', 'partial')", conn)
            unpaidBillsLbl.Text = cmdUnpaid.ExecuteScalar().ToString()

            ' === Collected card (total amount paid) ===
            Dim cmdCollected As New MySqlCommand(
            "SELECT SUM(amount_paid) FROM payments " &
            "WHERE MONTH(payment_date) = MONTH(CURDATE()) " &
            "AND YEAR(payment_date) = YEAR(CURDATE())", conn)
            Dim collected = cmdCollected.ExecuteScalar()
            If collected.ToString() = "" Then
                collectedLbl.Text = "₱0.00"
            Else
                collectedLbl.Text = "₱" & collected.ToString()
            End If

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        loadRecentTenants()
    End Sub

    Private Sub loadRecentTenants()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String = "SELECT t.full_name AS 'Tenant Name', " &
                                   "u.unit_number AS 'Unit', " &
                                   "u.type AS 'Unit Type', " &
                                   "l.status AS 'Status' " &
                                   "FROM leases l " &
                                   "JOIN tenants t ON l.tenant_id = t.tenant_id " &
                                   "JOIN units u ON l.unit_id = u.unit_id " &
                                   "ORDER BY l.lease_start DESC"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            RecentTenantsGrid.DataSource = dt

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Form7.Show()
        Me.Hide()

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Form8.Show()
        Me.Hide()

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Form9.Show()
        Me.Hide()

    End Sub
End Class