Imports MySql.Data.MySqlClient

Public Class Form2

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Form3.Show()
        Me.Hide()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Form4.Show()
        Me.Hide()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadLeaseDetails()
    End Sub

    Private Sub loadLeaseDetails()
        Dim myTenantId = Session.CurrentTenantID
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String = "SELECT t.full_name, " &
                                   "u.unit_number, u.type, u.floor, u.monthly_rate, u.unit_status, " &
                                   "l.lease_start, l.lease_end, l.monthly_rent, l.security_deposit, l.status " &
                                   "FROM tenants t " &
                                   "LEFT JOIN leases l ON t.tenant_id = l.tenant_id AND l.status = 'active' " &
                                   "LEFT JOIN units u ON u.unit_id = l.unit_id " &
                                   "WHERE t.tenant_id = @myTenantId"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@myTenantId", myTenantId)

            Dim reader = cmd.ExecuteReader()

            If reader.Read() Then

                TenantNamelbl.Text = reader("full_name").ToString()


                If Not IsDBNull(reader("unit_number")) Then
                    unitCodelbl.Text = reader("unit_number").ToString()
                    unitCodeMainlbl.Text = reader("unit_number").ToString()
                    unitValuelbl.Text = reader("unit_number").ToString()
                    Unitlbl.Text = "Tenant · Unit " & reader("unit_number").ToString()
                Else
                    unitCodelbl.Text = "N/A"
                    unitCodeMainlbl.Text = "N/A"
                    unitValuelbl.Text = "N/A"
                    Unitlbl.Text = "Tenant · No Unit"
                End If

                If Not IsDBNull(reader("floor")) Then
                    unitFloorlbl.Text = "Floor " & reader("floor").ToString()
                Else
                    unitFloorlbl.Text = "Floor N/A"
                End If


                If Not IsDBNull(reader("lease_start")) AndAlso Not IsDBNull(reader("lease_end")) Then
                    contractDatelbl.Text = reader("lease_start").ToString() & " to " & reader("lease_end").ToString()
                    leaseStartValuelbl.Text = reader("lease_start").ToString()
                    leaseEndValuelbl.Text = reader("lease_end").ToString()
                    leaseExpirationlbl.Text = Convert.ToDateTime(reader("lease_end")).ToString("MMM dd, yyyy")
                Else
                    contractDatelbl.Text = "No active lease"
                    leaseStartValuelbl.Text = "N/A"
                    leaseEndValuelbl.Text = "N/A"
                    leaseExpirationlbl.Text = "N/A"
                End If

                If Not IsDBNull(reader("monthly_rent")) Then
                    rentPricelbl.Text = "₱" & Convert.ToDecimal(reader("monthly_rent")).ToString("N2")
                    monthlyRentValuelbl.Text = "₱" & Convert.ToDecimal(reader("monthly_rent")).ToString("N2")
                Else
                    rentPricelbl.Text = "₱0.00"
                    monthlyRentValuelbl.Text = "₱0.00"
                End If


                If Not IsDBNull(reader("security_deposit")) Then
                    depositlbl.Text = "₱" & Convert.ToDecimal(reader("security_deposit")).ToString("N2")
                    securityDepositValuelbl.Text = "₱" & Convert.ToDecimal(reader("security_deposit")).ToString("N2")
                    securityDepositlbl.Text = "₱" & Convert.ToDecimal(reader("security_deposit")).ToString("N2")
                Else
                    depositlbl.Text = "₱0.00"
                    securityDepositValuelbl.Text = "₱0.00"
                    securityDepositlbl.Text = "₱0.00"
                End If



                If Not IsDBNull(reader("status")) Then
                    statuslbl.Text = reader("status").ToString()
                    contractStatusValuelbl.Text = reader("status").ToString()
                Else
                    statuslbl.Text = "No Active Lease"
                    contractStatusValuelbl.Text = "No Active Lease"
                End If

                tenantValuelbl.Text = reader("full_name").ToString()
            End If

            reader.Close()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        loadOutstandingBalance(myTenantId)
        loadLastPayment(myTenantId)
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

            If result Is Nothing OrElse IsDBNull(result) Then
                outstandinglbl.Text = "₱0.00"
            Else
                outstandinglbl.Text = "₱" & Convert.ToDecimal(result).ToString("N2")
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

    Private Sub btnSignOut_Click_1(sender As Object, e As EventArgs) Handles btnSignOut.Click
        Session.SignOut(Me)
    End Sub

    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click
        Form21.Show()
        Me.Hide()
    End Sub
End Class