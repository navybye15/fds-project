' ================================================================
' Form16.vb - Add Bill (popup, called via .ShowDialog() from Form15's "Generate Bill")
' Follows the same connection pattern as Form8.vb (connStr inline, isarms_db)
' Table name corrected to "bills" (per Form8.vb)
' ================================================================
Imports MySql.Data.MySqlClient

Public Class Form16

    Private ReadOnly connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"

    ' Holds tenant_id, full_name, unit_id, unit_number, monthly_rent
    ' for tenants that currently have an active lease.
    Dim leaseTable As New DataTable()

    Private Sub Form16_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        unitTxt.ReadOnly = True
        totalTxt.ReadOnly = True
        statusTxt.Text = "unpaid"
        statusTxt.ReadOnly = True
        loadActiveTenants()
        loadBillingMonthChoices()
    End Sub

    Private Sub loadActiveTenants()
        Dim conn As New MySqlConnection(connStr)
        Try
            conn.Open()

            Dim query As String = "SELECT l.tenant_id, t.full_name, l.unit_id, un.unit_number, l.monthly_rent " &
                                   "FROM leases l " &
                                   "JOIN tenants t ON l.tenant_id = t.tenant_id " &
                                   "JOIN units un ON l.unit_id = un.unit_id " &
                                   "WHERE l.status = 'active'"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            leaseTable = New DataTable()
            adapter.Fill(leaseTable)

            conn.Close()

            tenantCmb.DataSource = leaseTable
            tenantCmb.DisplayMember = "full_name"
            tenantCmb.ValueMember = "tenant_id"
            tenantCmb.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub loadBillingMonthChoices()
        monthCmb.Items.Clear()
        Dim baseDate As Date = Date.Today
        For i As Integer = 0 To 5
            monthCmb.Items.Add(baseDate.AddMonths(i).ToString("MMMM yyyy"))
        Next
        ' allow free typing too, e.g. "July 2026", since billing_month is just varchar(20)
        monthCmb.DropDownStyle = ComboBoxStyle.DropDown
    End Sub

    Private Sub cboTenant_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tenantCmb.SelectedIndexChanged
        If tenantCmb.SelectedIndex = -1 Then Exit Sub
        Dim row As DataRow = leaseTable.Rows(tenantCmb.SelectedIndex)

        unitTxt.Text = row("unit_number").ToString()
        baseTxt.Text = Convert.ToDecimal(row("monthly_rent")).ToString("0.00")
        recomputeTotal()
    End Sub

    Private Sub recomputeTotal()
        Dim baseRent As Decimal = 0
        Dim addCharges As Decimal = 0
        Decimal.TryParse(baseTxt.Text, baseRent)
        Decimal.TryParse(addTxt.Text, addCharges)
        totalTxt.Text = (baseRent + addCharges).ToString("0.00")
    End Sub

    Private Sub AmountFields_TextChanged(sender As Object, e As EventArgs) _
        Handles baseTxt.TextChanged, addTxt.TextChanged
        recomputeTotal()
    End Sub

    Private Sub btnAddBill_Click(sender As Object, e As EventArgs) Handles Addbillbtn.Click
        If tenantCmb.SelectedIndex = -1 Then
            MessageBox.Show("Please select a tenant.")
            Return
        End If
        If String.IsNullOrWhiteSpace(monthCmb.Text) Then
            MessageBox.Show("Please enter the billing month.")
            Return
        End If

        Dim row As DataRow = leaseTable.Rows(tenantCmb.SelectedIndex)
        Dim tenantId As Integer = Convert.ToInt32(row("tenant_id"))
        Dim unitId As Integer = Convert.ToInt32(row("unit_id"))

        Dim conn As New MySqlConnection(connStr)
        Try
            conn.Open()

            Dim query As String = "INSERT INTO bills (tenant_id, unit_id, billing_month, base_rent, addtional_charges, due_date, status) " &
                                   "VALUES (@tenant_id, @unit_id, @month, @base_rent, @add_charges, @due_date, 'unpaid')"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@tenant_id", tenantId)
            cmd.Parameters.AddWithValue("@unit_id", unitId)
            cmd.Parameters.AddWithValue("@month", monthCmb.Text)
            cmd.Parameters.AddWithValue("@base_rent", Convert.ToDecimal(baseTxt.Text))
            cmd.Parameters.AddWithValue("@add_charges",
                If(String.IsNullOrWhiteSpace(addTxt.Text), 0D, Convert.ToDecimal(addTxt.Text)))
            cmd.Parameters.AddWithValue("@due_date", Datepick.Value.Date)
            cmd.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show("Bill added successfully!")
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        Me.Close()
    End Sub

End Class