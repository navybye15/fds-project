Imports MySql.Data.MySqlClient
Public Class Form10

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

            ' 3. Insert sa tenants (wala nang unit/lease dito, sa Leases module na ito)
            Dim cmdTenant As New MySqlCommand(
                "INSERT INTO tenants (user_id, full_name, contact_no, emergency_contact, gov_id) " &
                "VALUES (@user_id, @full_name, @contact_no, @emergency_contact, @gov_id)", conn)
            cmdTenant.Parameters.AddWithValue("@user_id", newUserId)
            cmdTenant.Parameters.AddWithValue("@full_name", FullNametxt.Text)
            cmdTenant.Parameters.AddWithValue("@contact_no", contactTxt.Text)
            cmdTenant.Parameters.AddWithValue("@emergency_contact", emergencyTxt.Text)
            cmdTenant.Parameters.AddWithValue("@gov_id", govIdTxt.Text)
            cmdTenant.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show("Tenant created successfully! You can now create a lease for this tenant in the Leases module.")
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub cancelBtn_Click(sender As Object, e As EventArgs) Handles cancelBtn.Click
        Me.Close()
    End Sub

End Class