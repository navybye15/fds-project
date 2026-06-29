Imports MySql.Data.MySqlClient
Imports System.Data

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Safety check - dapat naka-set na ang TenantId bago dumating dito
        If Session.CurrentTenantID = 0 Then
            MessageBox.Show("Walang naka-login na tenant session. Bumalik sa login.")
            Me.Close()
            Return
        End If

        loadMyProfile()
        setEditMode(False) ' disabled muna ang textboxes pagbukas ng form
    End Sub

    Private Sub loadMyProfile()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            ' Kunin lang ang record ng sariling tenant - WHERE t.tenant_id = @tenant_id
            ' gamit ang TenantId mula sa session, hindi mula sa user input,
            ' para hindi makakuha ng info ng ibang tenant.
            ' NOTE: 'type', 'floor', 'monthly_rate' ang TAMANG column names sa
            ' 'units' table (kumpirmado via SHOW COLUMNS FROM units;) - hindi
            ' 'unit_type' / 'floor_location'.
            Dim query As String =
                "SELECT t.full_name, t.contact_no, t.emergency_contact, t.gov_id, " &
                "un.unit_number, un.type, un.floor, un.monthly_rate, l.status " &
                "FROM tenants t " &
                "LEFT JOIN leases l ON t.tenant_id = l.tenant_id AND l.status = 'active' " &
                "LEFT JOIN units un ON l.unit_id = un.unit_id " &
                "WHERE t.tenant_id = @tenant_id"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@tenant_id", Session.CurrentTenantID)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                ' --- My Profile (read-only display) ---
                fullNamelbl.Text = reader("full_name").ToString()
                contactlbl.Text = reader("contact_no").ToString()
                emergencylbl.Text = reader("emergency_contact").ToString()
                govIdlbl.Text = reader("gov_id").ToString()

                ' --- My Unit (read-only display) ---
                unitNumlbl.Text = reader("unit_number").ToString()
                typelbl.Text = reader("type").ToString()
                floorlbl.Text = reader("floor").ToString()
                statuslbl.Text = reader("status").ToString()

                ' Monthly Rate - i-format bilang Peso para mas readable (hal. ₱5,000.00)
                If Not IsDBNull(reader("monthly_rate")) Then
                    Dim rate As Decimal = Convert.ToDecimal(reader("monthly_rate"))
                    monthlylbl.Text = "₱" & rate.ToString("N2")
                Else
                    monthlylbl.Text = ""
                End If

                ' --- I-fill din agad ang editable textboxes (disabled muna) ---
                FullNametxt.Text = reader("full_name").ToString()
                contactTxt.Text = reader("contact_no").ToString()
                emergencyTxt.Text = reader("emergency_contact").ToString()
                govIdTxt.Text = reader("gov_id").ToString()
            End If

            reader.Close()
            conn.Close()
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
        ' Simpleng validation muna
        If String.IsNullOrWhiteSpace(FullNametxt.Text) OrElse String.IsNullOrWhiteSpace(contactTxt.Text) Then
            MessageBox.Show("Full Name at Contact No. ay required.")
            Return
        End If

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            ' Update lang ang sariling info ng tenant - WALANG username/password/
            ' lease/unit dito. Yung WHERE clause gamit ang session TenantId,
            ' kaya hindi ito magiging pwedeng i-edit ang record ng ibang tenant.
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

            MessageBox.Show("Na-update na ang profile mo!")
            loadMyProfile()
            setEditMode(False)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

End Class