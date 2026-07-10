
Imports MySql.Data.MySqlClient
Public Class Form3
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Form1.Show()
        Me.Hide()

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Form2.Show()
        Me.Hide()


    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Form4.Show()
        Me.Hide()

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadSidebarInfo()
        loadBillingHistory()
    End Sub

    Private Sub loadSidebarInfo()
        Dim myTenantId = Session.CurrentTenantID
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String = "SELECT t.full_name, u.unit_number, u.floor " &
                                   "FROM tenants t " &
                                   "JOIN leases l ON t.tenant_id = l.tenant_id AND l.status = 'active' " &
                                   "JOIN units u ON u.unit_id = l.unit_id " &
                                   "WHERE t.tenant_id = @myTenantId"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@myTenantId", myTenantId)

            Dim reader = cmd.ExecuteReader()

            If reader.Read() Then
                unitCodelbl.Text = reader("unit_number").ToString()
                unitFloorlbl.Text = "Floor " & reader("floor").ToString()

                TenantNamelbl.Text = reader("full_name").ToString()
                Unitlbl.Text = "Tenant · Unit " & reader("unit_number").ToString()
            End If

            reader.Close()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        loadOutstandingBalance(myTenantId)
        loadLastPayment(myTenantId)
        loadLeaseExpiration(myTenantId)
        loadSecurityDeposit(myTenantId)
    End Sub

    Private Sub loadOutstandingBalance(myTenantId As Integer)
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String = "SELECT SUM(base_rent + addtional_charges) AS outstanding " &
                                   "FROM bills WHERE tenant_id = @myTenantId AND status IN ('unpaid', 'partial')"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@myTenantId", myTenantId)

            Dim result = cmd.ExecuteScalar()

            If result.ToString() = "" Then
                outstandinglbl.Text = "₱0.00"
            Else
                outstandinglbl.Text = "₱" & result.ToString()
            End If

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub loadLastPayment(myTenantId As Integer)
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String = "SELECT p.amount_paid, p.payment_date " &
                                   "FROM payments p " &
                                   "JOIN bills b ON p.bill_id = b.bill_id " &
                                   "WHERE b.tenant_id = @myTenantId " &
                                   "ORDER BY p.payment_date DESC LIMIT 1"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@myTenantId", myTenantId)

            Dim reader = cmd.ExecuteReader()

            If reader.Read() Then
                paymentHistorylbl.Text = "₱" & reader("amount_paid").ToString() & " on " & reader("payment_date").ToString()
            Else
                paymentHistorylbl.Text = "No payments yet"
            End If

            reader.Close()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub loadLeaseExpiration(myTenantId As Integer)
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String = "SELECT lease_end FROM leases WHERE tenant_id = @myTenantId AND status = 'active'"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@myTenantId", myTenantId)

            Dim result = cmd.ExecuteScalar()

            If result Is Nothing OrElse IsDBNull(result) Then
                leaseExpirationlbl.Text = "N/A"
            Else
                leaseExpirationlbl.Text = Convert.ToDateTime(result).ToString("MMM dd, yyyy")
            End If

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub loadSecurityDeposit(myTenantId As Integer)
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String = "SELECT security_deposit FROM leases WHERE tenant_id = @myTenantId AND status = 'active'"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@myTenantId", myTenantId)

            Dim result = cmd.ExecuteScalar()

            If result.ToString() = "" Then
                securityDepositlbl.Text = "₱0.00"
            Else
                securityDepositlbl.Text = "₱" & result.ToString()
            End If

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub loadBillingHistory()
        Dim myTenantId = Session.CurrentTenantID
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String = "SELECT billing_month, base_rent, addtional_charges, due_date, status " &
                                   "FROM bills WHERE tenant_id = @myTenantId " &
                                   "ORDER BY due_date DESC"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@myTenantId", myTenantId)

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)


            dt.Columns("billing_month").ColumnName = "Billing Month"
            dt.Columns("base_rent").ColumnName = "Base Rent"
            dt.Columns("addtional_charges").ColumnName = "Additional Charges"
            dt.Columns("due_date").ColumnName = "Due Date"
            dt.Columns("status").ColumnName = "Status"

            BillingHistoryGrid.DataSource = dt

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSignOut_Click(sender As Object, e As EventArgs)
        Session.SignOut(Me)
    End Sub
End Class