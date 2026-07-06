Imports MySql.Data.MySqlClient
Imports System.Data

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Session.CurrentTenantID = 0 Then
            MessageBox.Show("There is no currently logged in, please go back to login form")
            Me.Close()
            Return
        End If

        loadMyProfile()
        setEditMode(False)
    End Sub

    Private Sub loadMyProfile()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String =
                "SELECT t.full_name, t.contact_no, t.emergency_contact, t.gov_id, " &
                "un.unit_number, un.type, un.floor, un.monthly_rate, l.status, " &
                "l.lease_end, l.security_deposit " &
                "FROM tenants t " &
                "LEFT JOIN leases l ON t.tenant_id = l.tenant_id AND l.status = 'active' " &
                "LEFT JOIN units un ON l.unit_id = un.unit_id " &
                "WHERE t.tenant_id = @tenant_id"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@tenant_id", Session.CurrentTenantID)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then

                unitCodelbl.Text = reader("unit_number").ToString()
                unitFloorlbl.Text = "Floor " & reader("floor").ToString()


                namelbl.Text = reader("full_name").ToString()


                fullNamelbl.Text = reader("full_name").ToString()
                contactlbl.Text = reader("contact_no").ToString()
                emergencylbl.Text = reader("emergency_contact").ToString()
                govIdlbl.Text = reader("gov_id").ToString()


                unitNumlbl.Text = reader("unit_number").ToString()
                typelbl.Text = reader("type").ToString()
                floorlbl.Text = reader("floor").ToString()
                statuslbl.Text = reader("status").ToString()


                If Not IsDBNull(reader("monthly_rate")) Then
                    Dim rate As Decimal = Convert.ToDecimal(reader("monthly_rate"))
                    monthlylbl.Text = "₱" & rate.ToString("N2")
                Else
                    monthlylbl.Text = ""
                End If

                If Not IsDBNull(reader("lease_end")) Then
                    leaseExpirationlbl.Text = Convert.ToDateTime(reader("lease_end")).ToString("MMM dd, yyyy")
                Else
                    leaseExpirationlbl.Text = "N/A"
                End If

                If Not IsDBNull(reader("security_deposit")) Then
                    securityDepositlbl.Text = "₱" & Convert.ToDecimal(reader("security_deposit")).ToString("N2")
                Else
                    securityDepositlbl.Text = "₱0.00"
                End If

                FullNametxt.Text = reader("full_name").ToString()
                contactTxt.Text = reader("contact_no").ToString()
                emergencyTxt.Text = reader("emergency_contact").ToString()
                govIdTxt.Text = reader("gov_id").ToString()
            End If


            reader.Close()
            conn.Close()

            loadOutstandingBalance(Session.CurrentTenantID)
            loadLastPayment(Session.CurrentTenantID)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub editBtn_Click(sender As Object, e As EventArgs) Handles editBtn.Click
        setEditMode(True)
    End Sub

    Private Sub setEditMode(isEditing As Boolean)
        FullNametxt.Enabled = isEditing
        contactTxt.Enabled = isEditing
        emergencyTxt.Enabled = isEditing
        govIdTxt.Enabled = isEditing

        editBtn.Enabled = Not isEditing
        saveBtn.Enabled = isEditing
    End Sub

    Private Sub saveBtn_Click(sender As Object, e As EventArgs) Handles saveBtn.Click
        If String.IsNullOrWhiteSpace(FullNametxt.Text) OrElse String.IsNullOrWhiteSpace(contactTxt.Text) Then
            MessageBox.Show("Full Name and Contact No. are required.")
            Return
        End If

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim cmd As New MySqlCommand(
                "UPDATE tenants SET full_name = @full_name, contact_no = @contact_no, " &
                "emergency_contact = @emergency_contact, gov_id = @gov_id " &
                "WHERE tenant_id = @tenant_id", conn)
            cmd.Parameters.AddWithValue("@full_name", FullNametxt.Text)
            cmd.Parameters.AddWithValue("@contact_no", contactTxt.Text)
            cmd.Parameters.AddWithValue("@emergency_contact", emergencyTxt.Text)
            cmd.Parameters.AddWithValue("@gov_id", govIdTxt.Text)
            cmd.Parameters.AddWithValue("@tenant_id", Session.CurrentTenantID)
            cmd.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show("Your profile has been updated!")
            loadMyProfile()
            setEditMode(False)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub loadOutstandingBalance(myTenantId As Integer)
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String = "SELECT SUM(base_rent + addtional_charges) AS outstanding " &
                               "FROM bills WHERE tenant_id = @myTenantId AND status IN ('unpaid', 'partial')"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@myTenantId", myTenantId)

            Dim result = cmd.ExecuteScalar()

            If IsDBNull(result) OrElse result Is Nothing Then
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
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
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
                paymentHistorylbl.Text = "₱" & Convert.ToDecimal(reader("amount_paid")).ToString("N2") & " on " & Convert.ToDateTime(reader("payment_date")).ToString("MMM dd, yyyy")
            Else
                paymentHistorylbl.Text = "No payments yet"
            End If

            reader.Close()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
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

    Private Sub namelbl_Click(sender As Object, e As EventArgs) Handles namelbl.Click

    End Sub
End Class