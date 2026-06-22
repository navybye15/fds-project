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

        Dim connStr As String = "Server=localhost;Port=3307;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)
        conn.Open()

        Dim query As String = "SELECT t.full_name, t.contact_no, t.emergency_contact, t.gov_id, u.unit_number, u.type, u.floor, u.monthly_rate, u.unit_status FROM tenants t JOIN leases l ON t.tenant_id = l.tenant_id JOIN units u ON u.unit_id = l.unit_id WHERE t.tenant_id = @myTenantId"
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
        End If

        reader.Close()
        conn.Close()
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class
