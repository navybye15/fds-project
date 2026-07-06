Imports MySql.Data.MySqlClient
Public Class Form8
    Dim selectedTenantId As Integer = 0
    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadTenants()
    End Sub


    Private Sub loadTenants()
        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim cmdTotal As New MySqlCommand("SELECT COUNT(*) FROM tenants", conn)
            totalTenantsLbl.Text = cmdTotal.ExecuteScalar().ToString()


            Dim query As String = "SELECT t.tenant_id, t.full_name AS 'Name', " &
                                   "un.unit_number AS 'Unit', " &
                                   "t.contact_no AS 'Contact', " &
                                   "DATE_FORMAT(l.lease_start, '%Y-%m-%d') AS 'Lease Start', " &
                                   "DATE_FORMAT(l.lease_end, '%Y-%m-%d') AS 'Lease End', " &
                                   "l.status AS 'Status' " &
                                   "FROM tenants t " &
                                   "LEFT JOIN leases l ON t.tenant_id = l.tenant_id AND l.status = 'active' " &
                                   "LEFT JOIN units un ON l.unit_id = un.unit_id"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            TenantsGrid.DataSource = dt
            TenantsGrid.Columns("tenant_id").Visible = False

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub TenantsGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TenantsGrid.CellClick
        If TenantsGrid.SelectedRows.Count > 0 Then
            Dim row = TenantsGrid.SelectedRows(0)
            selectedTenantId = row.Cells("tenant_id").Value


            FullNametxt.Text = row.Cells("Name").Value.ToString()
            contactTxt.Text = row.Cells("Contact").Value.ToString()

            If row.Cells("Lease Start").Value.ToString() <> "" Then
                LeaseStarttxt.Text = row.Cells("Lease Start").Value.ToString()
            End If

            If row.Cells("Lease End").Value.ToString() <> "" Then
                LeaseEndtxt.Text = row.Cells("Lease End").Value.ToString()
            End If


            Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
            Dim conn As New MySqlConnection(connStr)

            Try
                conn.Open()

                Dim query As String = "SELECT t.emergency_contact, t.gov_id, u.username, u.password " &
                                   "FROM tenants t " &
                                   "JOIN users u ON t.user_id = u.user_id " &
                                   "WHERE t.tenant_id = @tenant_id"

                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@tenant_id", selectedTenantId)

                Dim reader = cmd.ExecuteReader()

                If reader.Read() Then
                    emergencyTxt.Text = reader("emergency_contact").ToString()
                    govIdTxt.Text = reader("gov_id").ToString()
                    Usernametxt.Text = reader("username").ToString()
                    Passwordtxt.Text = reader("password").ToString()
                End If

                reader.Close()
                conn.Close()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub saveBtn_Click(sender As Object, e As EventArgs) Handles saveBtn.Click
        If selectedTenantId = 0 Then
            MessageBox.Show("Please select a tenant to update.")
            Return
        End If

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim cmdTenant As New MySqlCommand(
                "UPDATE tenants SET full_name = @full_name, contact_no = @contact_no, " &
                "emergency_contact = @emergency_contact, gov_id = @gov_id " &
                "WHERE tenant_id = @tenant_id", conn)
            cmdTenant.Parameters.AddWithValue("@full_name", FullNametxt.Text)
            cmdTenant.Parameters.AddWithValue("@contact_no", contactTxt.Text)
            cmdTenant.Parameters.AddWithValue("@emergency_contact", emergencyTxt.Text)
            cmdTenant.Parameters.AddWithValue("@gov_id", govIdTxt.Text)
            cmdTenant.Parameters.AddWithValue("@tenant_id", selectedTenantId)
            cmdTenant.ExecuteNonQuery()


            Dim cmdLease As New MySqlCommand(
                "UPDATE leases SET lease_start = @lease_start, lease_end = @lease_end " &
                "WHERE tenant_id = @tenant_id AND status = 'active'", conn)
            cmdLease.Parameters.AddWithValue("@lease_start", LeaseStarttxt.Text)
            cmdLease.Parameters.AddWithValue("@lease_end", LeaseEndtxt.Text)
            cmdLease.Parameters.AddWithValue("@tenant_id", selectedTenantId)
            cmdLease.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show("Tenant updated successfully!")
            clearFields()
            loadTenants()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub deleteBtn_Click(sender As Object, e As EventArgs) Handles deleteBtn.Click
        If selectedTenantId = 0 Then
            MessageBox.Show("Please select a tenant to delete.")
            Return
        End If

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()


            Dim cmdBillCount As New MySqlCommand("SELECT COUNT(*) FROM bills WHERE tenant_id = @tenant_id", conn)
            cmdBillCount.Parameters.AddWithValue("@tenant_id", selectedTenantId)
            Dim billCount As Integer = Convert.ToInt32(cmdBillCount.ExecuteScalar())

            Dim cmdLeaseCount As New MySqlCommand("SELECT COUNT(*) FROM leases WHERE tenant_id = @tenant_id", conn)
            cmdLeaseCount.Parameters.AddWithValue("@tenant_id", selectedTenantId)
            Dim leaseCount As Integer = Convert.ToInt32(cmdLeaseCount.ExecuteScalar())


            Dim billIds As New List(Of String)
            Dim cmdGetBillIds As New MySqlCommand("SELECT bill_id FROM bills WHERE tenant_id = @tenant_id", conn)
            cmdGetBillIds.Parameters.AddWithValue("@tenant_id", selectedTenantId)
            Using reader = cmdGetBillIds.ExecuteReader()
                While reader.Read()
                    billIds.Add(reader("bill_id").ToString())
                End While
            End Using

            Dim paymentCount As Integer = 0
            If billIds.Count > 0 Then
                Dim idList As String = String.Join(",", billIds)
                Dim cmdPaymentCount As New MySqlCommand("SELECT COUNT(*) FROM payments WHERE bill_id IN (" & idList & ")", conn)
                paymentCount = Convert.ToInt32(cmdPaymentCount.ExecuteScalar())
            End If


            Dim confirmMsg As String = "Are you sure you want to delete this tenant?"
            If billCount > 0 OrElse paymentCount > 0 OrElse leaseCount > 0 Then
                confirmMsg = "This tenant has existing records:" & vbCrLf &
                             "- " & leaseCount & " lease(s)" & vbCrLf &
                             "- " & billCount & " bill(s)" & vbCrLf &
                             "- " & paymentCount & " payment(s)" & vbCrLf & vbCrLf &
                             "Deleting this tenant will PERMANENTLY remove all of this history too." & vbCrLf &
                             "This cannot be undone. Continue?"
            End If

            Dim confirm = MessageBox.Show(confirmMsg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If confirm <> DialogResult.Yes Then Return


            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try

                Dim activeUnitId As Object = Nothing
                Dim cmdGetActiveUnit As New MySqlCommand(
                    "SELECT unit_id FROM leases WHERE tenant_id = @tenant_id AND status = 'active' LIMIT 1", conn, transaction)
                cmdGetActiveUnit.Parameters.AddWithValue("@tenant_id", selectedTenantId)
                activeUnitId = cmdGetActiveUnit.ExecuteScalar()


                If activeUnitId IsNot Nothing Then
                    Dim cmdFreeUnit As New MySqlCommand(
                        "UPDATE units SET unit_status = 'available' WHERE unit_id = @unit_id", conn, transaction)
                    cmdFreeUnit.Parameters.AddWithValue("@unit_id", activeUnitId)
                    cmdFreeUnit.ExecuteNonQuery()
                End If


                If billIds.Count > 0 Then
                    Dim idList As String = String.Join(",", billIds)
                    Dim cmdPayments As New MySqlCommand("DELETE FROM payments WHERE bill_id IN (" & idList & ")", conn, transaction)
                    cmdPayments.ExecuteNonQuery()
                End If


                Dim cmdBills As New MySqlCommand("DELETE FROM bills WHERE tenant_id = @tenant_id", conn, transaction)
                cmdBills.Parameters.AddWithValue("@tenant_id", selectedTenantId)
                cmdBills.ExecuteNonQuery()


                Dim cmdLease As New MySqlCommand("DELETE FROM leases WHERE tenant_id = @tenant_id", conn, transaction)
                cmdLease.Parameters.AddWithValue("@tenant_id", selectedTenantId)
                cmdLease.ExecuteNonQuery()


                Dim cmdTenant As New MySqlCommand("DELETE FROM tenants WHERE tenant_id = @tenant_id", conn, transaction)
                cmdTenant.Parameters.AddWithValue("@tenant_id", selectedTenantId)
                cmdTenant.ExecuteNonQuery()

                transaction.Commit()

                MessageBox.Show("Tenant deleted successfully!")
                clearFields()
                selectedTenantId = 0
                loadTenants()

            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show("Delete failed, no changes were made: " & ex.Message)
            End Try

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub addTenantBtn_Click(sender As Object, e As EventArgs) Handles addTenantBtn.Click
        Form10.ShowDialog()
        loadTenants()
    End Sub

    Private Sub clearFields()
        FullNametxt.Text = ""
        contactTxt.Text = ""
        Usernametxt.Text = ""
        Passwordtxt.Text = ""
        emergencyTxt.Text = ""
        govIdTxt.Text = ""
        LeaseStarttxt.Text = ""
        LeaseEndtxt.Text = ""
        selectedTenantId = 0
    End Sub

    Private Sub Loclbl_Click(sender As Object, e As EventArgs) Handles Loclbl.Click

    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click

    End Sub

    Private Sub UnitNumberlbl_Click(sender As Object, e As EventArgs) Handles UnitNumberlbl.Click

    End Sub

    Private Sub FullNametxt_TextChanged(sender As Object, e As EventArgs) Handles FullNametxt.TextChanged

    End Sub

    Private Sub Typelbl_Click(sender As Object, e As EventArgs) Handles Typelbl.Click

    End Sub

    Private Sub contactTxt_TextChanged(sender As Object, e As EventArgs) Handles contactTxt.TextChanged

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub LeaseStarttxt_TextChanged(sender As Object, e As EventArgs) Handles LeaseStarttxt.TextChanged

    End Sub

    Private Sub govIdTxt_TextChanged(sender As Object, e As EventArgs) Handles govIdTxt.TextChanged

    End Sub

    Private Sub Monthlylbl_Click(sender As Object, e As EventArgs) Handles Monthlylbl.Click

    End Sub

    Private Sub Usernametxt_TextChanged(sender As Object, e As EventArgs) Handles Usernametxt.TextChanged

    End Sub

    Private Sub Passwordtxt_TextChanged(sender As Object, e As EventArgs) Handles Passwordtxt.TextChanged

    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click

    End Sub

    Private Sub LeaseEndtxt_TextChanged(sender As Object, e As EventArgs) Handles LeaseEndtxt.TextChanged

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub emergencyTxt_TextChanged(sender As Object, e As EventArgs) Handles emergencyTxt.TextChanged

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Form7.Show()
        Me.Hide()

    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs) Handles Label25.Click
        Form6.Show()
        Me.Hide()

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Form12.Show()
        Me.Hide()

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Form9.Show()
        Me.Hide()

    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        Form19.Show()
        Me.Hide()
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Form15.Show()
        Me.Hide()
    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        Form18.Show()
        Me.Hide()
    End Sub
End Class