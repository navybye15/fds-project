
Imports System.Web.UI.WebControls
Imports MySql.Data.MySqlClient
Public Class Form5

    Public connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd="
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Enabled = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim conn As New MySqlConnection(connStr)
        conn.Open()

        Dim query As String = "SELECT u.user_id, u.role, t.tenant_id, t.full_name FROM users u LEFT JOIN tenants t ON u.user_id = t.user_id WHERE u.username = @u AND u.password = @p"

        Dim cmd As New MySqlCommand(query, conn)

        cmd.Parameters.AddWithValue("@u", usernametxt.Text)
        cmd.Parameters.AddWithValue("@p", passwordtxt.Text)

        Dim reader = cmd.ExecuteReader()

        If reader.Read() Then
            Session.CurrentUserID = reader("user_id")
            Session.CurrentRole = reader("role").ToString()

            If Session.CurrentRole = "tenant" Then
                Session.CurrentTenantID = reader("tenant_id")
                Session.CurrentTenantName = reader("full_name")


            End If

            If Session.CurrentRole = "tenant" Then
                Form1.Show()
                Me.Hide()

            ElseIf Session.CurrentRole = "admin" Then
                Form6.Show()
                Me.Hide()


            End If



            reader.Close()
            conn.Close()


        Else
            MessageBox.Show("Please enter a valid Username or Password")

        End If




    End Sub
End Class