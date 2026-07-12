Imports MySql.Data.MySqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class Form20

    Private ReadOnly connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;Convert Zero Datetime=True;Allow Zero Datetime=True;"

    Public paymentId As Integer

    Private Sub Form20_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadReciptDetails()
    End Sub

    Private Sub loadReciptDetails()
        Dim conn As New MySqlConnection(connStr)
        Try
            conn.Open()

            Dim query As String = "SELECT t.full_name, u.unit_number, b.billing_month, p.payment_date, p.amount_paid " &
                                   "FROM tenants t JOIN bills b ON t.tenant_id = b.tenant_id " &
                                   "JOIN units u ON b.unit_id = u.unit_id " &
                                   "JOIN payments p ON b.bill_id = p.bill_id " &
                                   "WHERE p.payment_id = @payment_id"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@payment_id", paymentId)

            Dim reader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim TotalPaid As Decimal = Convert.ToDecimal(reader("amount_paid"))

                tenantName.Text = reader("full_name").ToString()
                unitNumber.Text = reader("unit_number").ToString()
                billingMonth.Text = reader("billing_month").ToString()
                datePayment.Text = Convert.ToDateTime(reader("payment_date")).ToString("MMMM-dd-yyyy")
                amountPaid.Text = "₱ " + TotalPaid.ToString("0.00")
            End If
            reader.Close()

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub printReceipt_Click(sender As Object, e As EventArgs) Handles printReceipt.Click

        Dim saveDialog As New SaveFileDialog()
        saveDialog.Filter = "PDF Files|*.pdf"
        saveDialog.FileName = "Receipt_" & paymentId & ".pdf"

        If saveDialog.ShowDialog() <> DialogResult.OK Then
            Return
        End If

        Try
            Dim doc As New iTextSharp.text.Document(PageSize.A5, 30, 30, 30, 30)
            PdfWriter.GetInstance(doc, New System.IO.FileStream(saveDialog.FileName, System.IO.FileMode.Create))
            doc.Open()

            Dim propertyFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
            Dim titleFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, New BaseColor(0, 90, 60))
            Dim dateFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.ITALIC, BaseColor.GRAY)
            Dim labelFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.GRAY)
            Dim valueFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
            Dim totalFont As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD, New BaseColor(0, 90, 60))

            Dim propertyName As New Paragraph("ISA-RMS", propertyFont)
            propertyName.Alignment = Element.ALIGN_CENTER
            doc.Add(propertyName)

            Dim subLabel As New Paragraph("Rental House Management System", dateFont)
            subLabel.Alignment = Element.ALIGN_CENTER
            subLabel.SpacingAfter = 10
            doc.Add(subLabel)

            Dim line As New Paragraph("_______________________________________________")
            line.Alignment = Element.ALIGN_CENTER
            doc.Add(line)

            Dim title As New Paragraph("Payment Receipt", titleFont)
            title.Alignment = Element.ALIGN_CENTER
            title.SpacingBefore = 10
            doc.Add(title)

            Dim generatedOn As New Paragraph("Generated on: " & DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt"), dateFont)
            generatedOn.Alignment = Element.ALIGN_CENTER
            generatedOn.SpacingAfter = 20
            doc.Add(generatedOn)

            Dim table As New PdfPTable(2)
            table.WidthPercentage = 100
            table.SetWidths(New Single() {1, 1})

            addReceiptRow(table, "Receipt No.", paymentId.ToString(), labelFont, valueFont)
            addReceiptRow(table, "Tenant Name", tenantName.Text, labelFont, valueFont)
            addReceiptRow(table, "Unit Number", unitNumber.Text, labelFont, valueFont)
            addReceiptRow(table, "Billing Month", billingMonth.Text, labelFont, valueFont)
            addReceiptRow(table, "Payment Date", datePayment.Text, labelFont, valueFont)

            doc.Add(table)

            doc.Add(New Paragraph(" "))

            Dim totalLine As New Paragraph("_______________________________________________")
            totalLine.Alignment = Element.ALIGN_CENTER
            doc.Add(totalLine)

            Dim totalTable As New PdfPTable(2)
            totalTable.WidthPercentage = 100
            totalTable.SpacingBefore = 10
            totalTable.SetWidths(New Single() {1, 1})

            Dim totalLabelCell As New PdfPCell(New Phrase("Amount Paid", labelFont))
            totalLabelCell.Border = Rectangle.NO_BORDER
            totalLabelCell.PaddingTop = 6
            totalTable.AddCell(totalLabelCell)

            Dim totalValueCell As New PdfPCell(New Phrase(amountPaid.Text, totalFont))
            totalValueCell.Border = Rectangle.NO_BORDER
            totalValueCell.HorizontalAlignment = Element.ALIGN_RIGHT
            totalValueCell.PaddingTop = 6
            totalTable.AddCell(totalValueCell)

            doc.Add(totalTable)

            doc.Add(New Paragraph(" "))
            doc.Add(New Paragraph(" "))
            doc.Add(New Paragraph(" "))

            Dim signatureLine As New Paragraph("_______________________________", valueFont)
            signatureLine.Alignment = Element.ALIGN_RIGHT
            doc.Add(signatureLine)

            Dim signatureLabel As New Paragraph("Landlord's Signature", labelFont)
            signatureLabel.Alignment = Element.ALIGN_RIGHT
            doc.Add(signatureLabel)

            doc.Add(New Paragraph(" "))

            Dim footer As New Paragraph("Thank you for your payment!", dateFont)
            footer.Alignment = Element.ALIGN_CENTER
            doc.Add(footer)

            doc.Close()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub

    Private Sub addReceiptRow(table As PdfPTable, label As String, value As String,
                               labelFont As iTextSharp.text.Font, valueFont As iTextSharp.text.Font)
        Dim labelCell As New PdfPCell(New Phrase(label, labelFont))
        labelCell.Border = Rectangle.NO_BORDER
        labelCell.PaddingTop = 4
        labelCell.PaddingBottom = 4
        table.AddCell(labelCell)

        Dim valueCell As New PdfPCell(New Phrase(value, valueFont))
        valueCell.Border = Rectangle.NO_BORDER
        valueCell.HorizontalAlignment = Element.ALIGN_RIGHT
        valueCell.PaddingTop = 4
        valueCell.PaddingBottom = 4
        table.AddCell(valueCell)
    End Sub

    Private Sub CancelBtn_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        Me.Close()
    End Sub
End Class