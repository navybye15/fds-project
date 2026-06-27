Imports MySql.Data.MySqlClient

Public Class Form1

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Form2.Show()
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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadProfileOverview()
    End Sub


    Private Sub loadProfileOverview()
        Dim myTenantId = Session.CurrentTenantID
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String = "SELECT t.full_name, t.contact_no, t.emergency_contact, t.gov_id, " &
                                   "u.unit_number, u.type, u.floor, u.monthly_rate, u.unit_status, " &
                                   "l.lease_end, l.security_deposit " &
                                   "FROM tenants t " &
                                   "JOIN leases l ON t.tenant_id = l.tenant_id AND l.status = 'active' " &
                                   "JOIN units u ON u.unit_id = l.unit_id " &
                                   "WHERE t.tenant_id = @myTenantId"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@myTenantId", myTenantId)

            Dim reader = cmd.ExecuteReader()

            If reader.Read() Then
                fullNamelbl.Text = reader("full_name")
                contactlbl.Text = reader("contact_no")
                emergencylbl.Text = reader("emergency_contact")
                govIdlbl.Text = reader("gov_id")
                unitNumlbl.Text = reader("unit_number")
                typelbl.Text = reader("type")
                floorlbl.Text = reader("floor")
                monthlylbl.Text = reader("monthly_rate")
                statuslbl.Text = reader("unit_status")
                namelbl.Text = reader("full_name")

                ' === YOUR UNIT panel (kaliwa) ===
                unitCodelbl.Text = reader("unit_number")
                unitFloorlbl.Text = "Floor " & reader("floor").ToString()

                ' === Lease Expiration card ===
                leaseExpirationlbl.Text = Convert.ToDateTime(reader("lease_end")).ToString("MMM dd, yyyy")

                ' === Security Deposit card ===
                securityDepositlbl.Text = "₱" & Convert.ToDecimal(reader("security_deposit")).ToString("N2")
            End If

            reader.Close()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading profile/unit info: " & ex.Message)
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

            If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                outstandinglbl.Text = "₱" & Convert.ToDecimal(result).ToString("N2")
            Else
                outstandinglbl.Text = "₱0.00"
            End If

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading outstanding balance: " & ex.Message)
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
                paymentHistorylbl.Text = "₱" & Convert.ToDecimal(reader("amount_paid")).ToString("N2") &
                                          " on " & Convert.ToDateTime(reader("payment_date")).ToString("MMM dd, yyyy")
            Else
                paymentHistorylbl.Text = "No payments yet"
            End If


            reader.Close()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading payment history: " & ex.Message)
        End Try
    End Sub
    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class
