Imports MySql.Data.MySqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class Form19

    ' Remembers which report tab is currently open, so the Apply Filter button
    ' knows which report to reload
    Dim currentReport As String = "occupancy"

    Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"

    ' ============================================================
    '  FORM LOAD
    ' ============================================================
    Private Sub Form19_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Fromdate.Value = New Date(Now.Year, Now.Month, 1)
        Todate.Value = Now.Date

        perTenantCmb.Visible = False

        ShowOccupancyTab()
    End Sub

    ' ============================================================
    '  TAB CLICKS
    ' ============================================================
    Private Sub Label32_Click(sender As Object, e As EventArgs) Handles Label32.Click
        ShowOccupancyTab()
    End Sub

    Private Sub Label31_Click(sender As Object, e As EventArgs) Handles Label31.Click
        ShowMonthlyCollectionTab()
    End Sub

    Private Sub Label30_Click(sender As Object, e As EventArgs) Handles Label30.Click
        ShowOutstandingBalancesTab()
    End Sub

    Private Sub Label29_Click(sender As Object, e As EventArgs) Handles Label29.Click
        ShowPaymentHistoryTab()
    End Sub

    Private Sub ShowOccupancyTab()
        currentReport = "occupancy"
        Label4.Text = "Occupancy Report"
        label40.Text = "Current units status summary"

        labell1.Text = "Occupied:"
        Labell2.Text = "Available:"
        labell6.Text = "Maintenance:"

        labell1.Visible = True
        Labell2.Visible = True
        labell6.Visible = True
        Labell4.Visible = False
        perTenantCmb.Visible = False

        LoadOccupancyReport()
    End Sub

    Private Sub ShowMonthlyCollectionTab()
        currentReport = "monthly"
        Label4.Text = "Monthly Collection Report"
        label40.Text = "Summary of amount collected per month"

        labell1.Text = "Total Due:"
        Labell2.Text = "Total Collected:"

        labell1.Visible = True
        Labell2.Visible = True
        labell6.Visible = False
        Labell4.Visible = False
        perTenantCmb.Visible = False

        LoadMonthlyCollectionReport()
    End Sub

    Private Sub ShowOutstandingBalancesTab()
        currentReport = "outstanding"
        Label4.Text = "Outstanding Balances"
        label40.Text = "Unpaid and partially paid bills"

        labell1.Text = "Total Outstanding:"

        labell1.Visible = True
        Labell2.Visible = False
        labell6.Visible = False
        Labell4.Visible = False
        perTenantCmb.Visible = False

        LoadOutstandingBalancesReport()
    End Sub

    Private Sub ShowPaymentHistoryTab()
        currentReport = "paymenthistory"
        Label4.Text = "Payment History per Tenant"
        label40.Text = "Sum of payment history per tenant"

        labell1.Visible = True
        Labell2.Visible = False
        labell6.Visible = False
        Labell4.Visible = True
        perTenantCmb.Visible = True

        LoadTenantComboBox()
    End Sub

    ' ============================================================
    '  APPLY FILTER BUTTON
    ' ============================================================
    Private Sub applybtn_Click(sender As Object, e As EventArgs) Handles Applybtn.Click
        If currentReport = "occupancy" Then
            LoadOccupancyReport()
        ElseIf currentReport = "monthly" Then
            LoadMonthlyCollectionReport()
        ElseIf currentReport = "outstanding" Then
            LoadOutstandingBalancesReport()
        ElseIf currentReport = "paymenthistory" Then
            LoadTenantComboBox()
        End If
    End Sub

    ' ============================================================
    '  1. OCCUPANCY REPORT (current snapshot, not date filtered)
    ' ============================================================
    Private Sub LoadOccupancyReport()
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim query As String =
                "SELECT un.unit_number AS 'Unit', un.type AS 'Type', " &
                "IFNULL(t.full_name, '—') AS 'Tenant', " &
                "CONCAT('₱', FORMAT(un.monthly_rate, 2)) AS 'Rate', " &
                "un.unit_status AS 'Status' " &
                "FROM units un " &
                "LEFT JOIN leases l ON un.unit_id = l.unit_id AND l.status = 'active' " &
                "LEFT JOIN tenants t ON l.tenant_id = t.tenant_id " &
                "ORDER BY un.unit_number"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ReportsGrid.DataSource = dt

            ' Count occupied / available / maintenance units one at a time
            Dim cmdOccupied As New MySqlCommand("SELECT COUNT(*) FROM units WHERE unit_status = 'occupied'", conn)
            Dim occupiedCount As Integer = cmdOccupied.ExecuteScalar()

            Dim cmdAvailable As New MySqlCommand("SELECT COUNT(*) FROM units WHERE unit_status = 'available'", conn)
            Dim availableCount As Integer = cmdAvailable.ExecuteScalar()

            Dim cmdMaintenance As New MySqlCommand("SELECT COUNT(*) FROM units WHERE unit_status = 'maintenance'", conn)
            Dim maintenanceCount As Integer = cmdMaintenance.ExecuteScalar()

            labell1.Text = "Occupied: " & occupiedCount
            Labell2.Text = "Available: " & availableCount
            labell6.Text = "Maintenance: " & maintenanceCount

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    '  2. MONTHLY COLLECTION REPORT (filtered by due date range)
    ' ============================================================
    Private Sub LoadMonthlyCollectionReport()
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim fromD As String = Fromdate.Value.ToString("yyyy-MM-dd")
            Dim toD As String = Todate.Value.ToString("yyyy-MM-dd")

            Dim query As String =
                "SELECT t.full_name AS 'Tenant', un.unit_number AS 'Unit', b.billing_month AS 'Billing Month', " &
                "CONCAT('₱', FORMAT(b.base_rent + IFNULL(b.addtional_charges,0), 2)) AS 'Amount Due', " &
                "CONCAT('₱', FORMAT(IFNULL((SELECT SUM(p.amount_paid) FROM payments p WHERE p.bill_id = b.bill_id), 0), 2)) AS 'Amount Collected', " &
                "b.status AS 'Status' " &
                "FROM bills b " &
                "JOIN tenants t ON b.tenant_id = t.tenant_id " &
                "JOIN units un ON b.unit_id = un.unit_id " &
                "WHERE b.due_date BETWEEN @fromdate AND @todate " &
                "ORDER BY b.due_date"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@fromdate", fromD)
            cmd.Parameters.AddWithValue("@todate", toD)

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ReportsGrid.DataSource = dt

            ' Total Due
            Dim cmdDue As New MySqlCommand(
                "SELECT IFNULL(SUM(base_rent + IFNULL(addtional_charges,0)),0) FROM bills " &
                "WHERE due_date BETWEEN @fromdate AND @todate", conn)
            cmdDue.Parameters.AddWithValue("@fromdate", fromD)
            cmdDue.Parameters.AddWithValue("@todate", toD)
            Dim totalDue As Decimal = cmdDue.ExecuteScalar()

            ' Total Collected
            Dim cmdCollected As New MySqlCommand(
                "SELECT IFNULL(SUM(p.amount_paid),0) FROM payments p " &
                "JOIN bills b ON p.bill_id = b.bill_id " &
                "WHERE b.due_date BETWEEN @fromdate AND @todate", conn)
            cmdCollected.Parameters.AddWithValue("@fromdate", fromD)
            cmdCollected.Parameters.AddWithValue("@todate", toD)
            Dim totalCollected As Decimal = cmdCollected.ExecuteScalar()

            labell1.Text = "Total Due: ₱" & totalDue.ToString("#,##0.00")
            Labell2.Text = "Total Collected: ₱" & totalCollected.ToString("#,##0.00")

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    '  3. OUTSTANDING BALANCES (unpaid / partial bills in range)
    ' ============================================================
    Private Sub LoadOutstandingBalancesReport()
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim fromD As String = Fromdate.Value.ToString("yyyy-MM-dd")
            Dim toD As String = Todate.Value.ToString("yyyy-MM-dd")

            Dim query As String =
                "SELECT t.full_name AS 'Tenant', un.unit_number AS 'Unit', b.billing_month AS 'Month', " &
                "CONCAT('₱', FORMAT((b.base_rent + IFNULL(b.addtional_charges,0)) - " &
                "IFNULL((SELECT SUM(p.amount_paid) FROM payments p WHERE p.bill_id = b.bill_id), 0), 2)) AS 'Balance' " &
                "FROM bills b " &
                "JOIN tenants t ON b.tenant_id = t.tenant_id " &
                "JOIN units un ON b.unit_id = un.unit_id " &
                "WHERE b.status IN ('unpaid','partial') AND b.due_date BETWEEN @fromdate AND @todate " &
                "ORDER BY b.due_date"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@fromdate", fromD)
            cmd.Parameters.AddWithValue("@todate", toD)

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ReportsGrid.DataSource = dt

            Dim cmdTotal As New MySqlCommand(
                "SELECT IFNULL(SUM((b.base_rent + IFNULL(b.addtional_charges,0)) - " &
                "IFNULL((SELECT SUM(p.amount_paid) FROM payments p WHERE p.bill_id = b.bill_id), 0)), 0) " &
                "FROM bills b WHERE b.status IN ('unpaid','partial') AND b.due_date BETWEEN @fromdate AND @todate", conn)
            cmdTotal.Parameters.AddWithValue("@fromdate", fromD)
            cmdTotal.Parameters.AddWithValue("@todate", toD)
            Dim totalOutstanding As Decimal = cmdTotal.ExecuteScalar()

            labell1.Text = "Total Outstanding: ₱" & totalOutstanding.ToString("#,##0.00")

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    '  4. PAYMENT HISTORY PER TENANT
    ' ============================================================

    ' Fills the tenant dropdown with tenants who have bills in the selected date range
    Private Sub LoadTenantComboBox()
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim fromD As String = Fromdate.Value.ToString("yyyy-MM-dd")
            Dim toD As String = Todate.Value.ToString("yyyy-MM-dd")

            Dim query As String =
                "SELECT DISTINCT t.tenant_id, t.full_name " &
                "FROM tenants t " &
                "JOIN bills b ON t.tenant_id = b.tenant_id " &
                "WHERE b.due_date BETWEEN @fromdate AND @todate " &
                "ORDER BY t.full_name"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@fromdate", fromD)
            cmd.Parameters.AddWithValue("@todate", toD)

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            conn.Close()

            perTenantCmb.DataSource = dt
            perTenantCmb.DisplayMember = "full_name"
            perTenantCmb.ValueMember = "tenant_id"

            If dt.Rows.Count > 0 Then
                Dim firstTenantId As Integer = perTenantCmb.SelectedValue
                LoadPaymentHistoryForTenant(firstTenantId)
            Else
                ReportsGrid.DataSource = Nothing
                labell1.Text = "No tenants found in this date range"
                Labell4.Text = ""
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    ' Runs whenever the user picks a different tenant from the dropdown
    Private Sub perTenantCmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles perTenantCmb.SelectedIndexChanged
        If perTenantCmb.SelectedValue Is Nothing Then Return
        If Not IsNumeric(perTenantCmb.SelectedValue) Then Return

        Dim tenantId As Integer = perTenantCmb.SelectedValue
        LoadPaymentHistoryForTenant(tenantId)
    End Sub

    ' Shows only the payments made within the selected date range for one tenant
    Private Sub LoadPaymentHistoryForTenant(tenantId As Integer)
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()

            Dim fromD As String = Fromdate.Value.ToString("yyyy-MM-dd")
            Dim toD As String = Todate.Value.ToString("yyyy-MM-dd")

            Dim query As String =
                "SELECT CONCAT('PMT-', YEAR(p.payment_date), '-', LPAD(p.payment_id,3,'0')) AS 'Ref No', " &
                "t.full_name AS 'Tenant', un.unit_number AS 'Unit', b.billing_month AS 'Billing Month', " &
                "DATE_FORMAT(p.payment_date, '%b %e, %Y') AS 'Date Paid', " &
                "CONCAT('₱', FORMAT(p.amount_paid, 2)) AS 'Amount' " &
                "FROM payments p " &
                "JOIN bills b ON p.bill_id = b.bill_id " &
                "JOIN tenants t ON b.tenant_id = t.tenant_id " &
                "JOIN units un ON b.unit_id = un.unit_id " &
                "WHERE t.tenant_id = @tenantId AND p.payment_date BETWEEN @fromdate AND @todate " &
                "ORDER BY p.payment_date DESC"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@tenantId", tenantId)
            cmd.Parameters.AddWithValue("@fromdate", fromD)
            cmd.Parameters.AddWithValue("@todate", toD)

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ReportsGrid.DataSource = dt

            Dim paymentCount As Integer = dt.Rows.Count
            Dim tenantName As String = perTenantCmb.Text

            Dim cmdTotal As New MySqlCommand(
                "SELECT IFNULL(SUM(p.amount_paid),0) FROM payments p " &
                "JOIN bills b ON p.bill_id = b.bill_id " &
                "WHERE b.tenant_id = @tenantId AND p.payment_date BETWEEN @fromdate AND @todate", conn)
            cmdTotal.Parameters.AddWithValue("@tenantId", tenantId)
            cmdTotal.Parameters.AddWithValue("@fromdate", fromD)
            cmdTotal.Parameters.AddWithValue("@todate", toD)
            Dim periodTotal As Decimal = cmdTotal.ExecuteScalar()

            labell1.Text = paymentCount & " payments on file for " & tenantName
            Labell4.Text = "Total for Period: ₱" & periodTotal.ToString("#,##0.00")

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    '  PRINT / NAVIGATION (same as your original code)
    ' ============================================================
    Private Sub Printbtn_Click(sender As Object, e As EventArgs) Handles Printbtn.Click

        Dim saveDialog As New SaveFileDialog()
        saveDialog.Filter = "PDF Files|*.pdf"
        saveDialog.FileName = Label4.Text.Replace(" ", "_") & ".pdf"

        If saveDialog.ShowDialog() <> DialogResult.OK Then
            Return
        End If

        Try
            Dim doc As New iTextSharp.text.Document(PageSize.A4, 30, 30, 30, 30)
            PdfWriter.GetInstance(doc, New System.IO.FileStream(saveDialog.FileName, System.IO.FileMode.Create))
            doc.Open()

            ' === Mga fonts na gagamitin (para may lebel ng importansya bawat text) ===
            Dim propertyFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
            Dim titleFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, New BaseColor(0, 90, 60))
            Dim dateFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.ITALIC, BaseColor.GRAY)
            Dim headerFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE)
            Dim normalFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL)
            Dim summaryFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK)

            ' === HEADER SECTION ===
            Dim propertyName As New Paragraph("ISA-RMS", propertyFont)
            propertyName.Alignment = Element.ALIGN_CENTER
            doc.Add(propertyName)

            Dim subLabel As New Paragraph("Rental House Management System", dateFont)
            subLabel.Alignment = Element.ALIGN_CENTER
            subLabel.SpacingAfter = 10
            doc.Add(subLabel)

            ' Simpleng linya bilang divider
            Dim line As New Paragraph("_______________________________________________________________")
            line.Alignment = Element.ALIGN_CENTER
            doc.Add(line)

            Dim title As New Paragraph(Label4.Text, titleFont)
            title.Alignment = Element.ALIGN_CENTER
            title.SpacingBefore = 10
            doc.Add(title)

            Dim generatedOn As New Paragraph("Generated on: " & DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt"), dateFont)
            generatedOn.Alignment = Element.ALIGN_CENTER
            generatedOn.SpacingAfter = 15
            doc.Add(generatedOn)

            ' === TABLE ===
            Dim table As New PdfPTable(ReportsGrid.Columns.Count)
            table.WidthPercentage = 100
            table.SpacingBefore = 10

            ' Header row ng table, may background color
            For Each col As DataGridViewColumn In ReportsGrid.Columns
                Dim headerCell As New PdfPCell(New Phrase(col.HeaderText, headerFont))
                headerCell.BackgroundColor = New BaseColor(30, 60, 50) ' dark green, kasing tema ng sidebar niyo
                headerCell.Padding = 6
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER
                table.AddCell(headerCell)
            Next

            ' Data rows, may alternating na light gray para sa bawat ibang row (mas madaling basahin)
            Dim rowIndex As Integer = 0
            For Each row As DataGridViewRow In ReportsGrid.Rows
                If Not row.IsNewRow Then
                    For Each cell As DataGridViewCell In row.Cells
                        Dim value As String = ""
                        If cell.Value IsNot Nothing Then
                            value = cell.Value.ToString()
                        End If

                        Dim dataCell As New PdfPCell(New Phrase(value, normalFont))
                        dataCell.Padding = 5

                        If rowIndex Mod 2 = 0 Then
                            dataCell.BackgroundColor = New BaseColor(245, 245, 245) ' light gray
                        Else
                            dataCell.BackgroundColor = BaseColor.WHITE
                        End If

                        table.AddCell(dataCell)
                    Next
                    rowIndex += 1
                End If
            Next

            doc.Add(table)

            ' === SUMMARY SECTION (may box/border para tumambad) ===
            doc.Add(New Paragraph(" "))

            Dim summaryTable As New PdfPTable(1)
            summaryTable.WidthPercentage = 100

            If labell1.Visible AndAlso labell1.Text <> "" Then
                Dim c As New PdfPCell(New Phrase(labell1.Text, summaryFont))
                c.Border = Rectangle.NO_BORDER
                c.PaddingTop = 4
                summaryTable.AddCell(c)
            End If
            If Labell2.Visible AndAlso Labell2.Text <> "" Then
                Dim c As New PdfPCell(New Phrase(Labell2.Text, summaryFont))
                c.Border = Rectangle.NO_BORDER
                c.PaddingTop = 4
                summaryTable.AddCell(c)
            End If
            If labell6.Visible AndAlso labell6.Text <> "" Then
                Dim c As New PdfPCell(New Phrase(labell6.Text, summaryFont))
                c.Border = Rectangle.NO_BORDER
                c.PaddingTop = 4
                summaryTable.AddCell(c)
            End If
            If Labell4.Visible AndAlso Labell4.Text <> "" Then
                Dim c As New PdfPCell(New Phrase(Labell4.Text, summaryFont))
                c.Border = Rectangle.NO_BORDER
                c.PaddingTop = 4
                summaryTable.AddCell(c)
            End If

            doc.Add(summaryTable)

            doc.Close()

            MessageBox.Show("Report saved successfully!")
            Process.Start(saveDialog.FileName)

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs) Handles Label25.Click
        Form6.Show()
        Me.Hide()
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Form7.Show()
        Me.Hide()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Form8.Show()
        Me.Hide()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Form9.Show()
        Me.Hide()
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Form12.Show()
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