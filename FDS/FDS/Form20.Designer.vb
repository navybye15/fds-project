<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form20
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CancelBtn = New System.Windows.Forms.Button()
        Me.printReceipt = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.floorTxt = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Typelbl = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.unitNumber = New System.Windows.Forms.Label()
        Me.tenantName = New System.Windows.Forms.Label()
        Me.billingMonth = New System.Windows.Forms.Label()
        Me.datePayment = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.amountPaid = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label6.Location = New System.Drawing.Point(29, 264)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(161, 25)
        Me.Label6.TabIndex = 124
        Me.Label6.Text = "Date Payment"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label1.Location = New System.Drawing.Point(28, 225)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(161, 25)
        Me.Label1.TabIndex = 120
        Me.Label1.Text = "Billing Month"
        '
        'CancelBtn
        '
        Me.CancelBtn.Location = New System.Drawing.Point(164, 554)
        Me.CancelBtn.Name = "CancelBtn"
        Me.CancelBtn.Size = New System.Drawing.Size(103, 27)
        Me.CancelBtn.TabIndex = 116
        Me.CancelBtn.Text = "Cancel"
        Me.CancelBtn.UseVisualStyleBackColor = True
        '
        'printReceipt
        '
        Me.printReceipt.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.printReceipt.ForeColor = System.Drawing.Color.PaleGreen
        Me.printReceipt.Location = New System.Drawing.Point(287, 550)
        Me.printReceipt.Name = "printReceipt"
        Me.printReceipt.Size = New System.Drawing.Size(125, 31)
        Me.printReceipt.TabIndex = 115
        Me.printReceipt.Text = "Print receipt"
        Me.printReceipt.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(161, 114)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(0, 0)
        Me.Button1.TabIndex = 107
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Font = New System.Drawing.Font("Garamond", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(131, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(195, 22)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "ISARMS"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label3.Location = New System.Drawing.Point(63, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(330, 18)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Official Payment Receipt"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'floorTxt
        '
        Me.floorTxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.floorTxt.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.floorTxt.Location = New System.Drawing.Point(28, 185)
        Me.floorTxt.Name = "floorTxt"
        Me.floorTxt.Size = New System.Drawing.Size(160, 20)
        Me.floorTxt.TabIndex = 110
        Me.floorTxt.Text = "Unit"
        '
        'Panel1
        '
        Me.Panel1.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(-2, -2)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(658, 81)
        Me.Panel1.TabIndex = 108
        '
        'Typelbl
        '
        Me.Typelbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Typelbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Typelbl.Location = New System.Drawing.Point(28, 147)
        Me.Typelbl.Name = "Typelbl"
        Me.Typelbl.Size = New System.Drawing.Size(106, 20)
        Me.Typelbl.TabIndex = 109
        Me.Typelbl.Text = "Tenant Name"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label8.Location = New System.Drawing.Point(29, 302)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(161, 25)
        Me.Label8.TabIndex = 124
        Me.Label8.Text = "Method"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label26.Location = New System.Drawing.Point(-195, 75)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(842, 1)
        Me.Label26.TabIndex = 127
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label9.Location = New System.Drawing.Point(-195, 360)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(842, 1)
        Me.Label9.TabIndex = 128
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label10.Location = New System.Drawing.Point(28, 391)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(161, 25)
        Me.Label10.TabIndex = 124
        Me.Label10.Text = "Amount Paid"
        '
        'unitNumber
        '
        Me.unitNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.unitNumber.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.unitNumber.Location = New System.Drawing.Point(250, 185)
        Me.unitNumber.Name = "unitNumber"
        Me.unitNumber.Size = New System.Drawing.Size(160, 20)
        Me.unitNumber.TabIndex = 110
        Me.unitNumber.Text = "Unit"
        Me.unitNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tenantName
        '
        Me.tenantName.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tenantName.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.tenantName.Location = New System.Drawing.Point(243, 147)
        Me.tenantName.Name = "tenantName"
        Me.tenantName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tenantName.Size = New System.Drawing.Size(167, 20)
        Me.tenantName.TabIndex = 109
        Me.tenantName.Text = "Tenant Name"
        Me.tenantName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'billingMonth
        '
        Me.billingMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.billingMonth.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.billingMonth.Location = New System.Drawing.Point(250, 225)
        Me.billingMonth.Name = "billingMonth"
        Me.billingMonth.Size = New System.Drawing.Size(161, 25)
        Me.billingMonth.TabIndex = 120
        Me.billingMonth.Text = "Billing Month"
        Me.billingMonth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'datePayment
        '
        Me.datePayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.datePayment.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.datePayment.Location = New System.Drawing.Point(251, 264)
        Me.datePayment.Name = "datePayment"
        Me.datePayment.Size = New System.Drawing.Size(161, 25)
        Me.datePayment.TabIndex = 124
        Me.datePayment.Text = "Date Payment"
        Me.datePayment.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label12.Location = New System.Drawing.Point(251, 302)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(161, 25)
        Me.Label12.TabIndex = 124
        Me.Label12.Text = "Cash"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'amountPaid
        '
        Me.amountPaid.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.amountPaid.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.amountPaid.Location = New System.Drawing.Point(250, 391)
        Me.amountPaid.Name = "amountPaid"
        Me.amountPaid.Size = New System.Drawing.Size(161, 25)
        Me.amountPaid.TabIndex = 124
        Me.amountPaid.Text = "Amount Paid"
        Me.amountPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label14.Location = New System.Drawing.Point(-195, 447)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(842, 1)
        Me.Label14.TabIndex = 129
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label15.Location = New System.Drawing.Point(161, 497)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(133, 25)
        Me.Label15.TabIndex = 124
        Me.Label15.Text = "Landlord Signature"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label16.Location = New System.Drawing.Point(152, 495)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(149, 2)
        Me.Label16.TabIndex = 130
        '
        'Form20
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(446, 600)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.amountPaid)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.datePayment)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.billingMonth)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CancelBtn)
        Me.Controls.Add(Me.printReceipt)
        Me.Controls.Add(Me.tenantName)
        Me.Controls.Add(Me.Typelbl)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.unitNumber)
        Me.Controls.Add(Me.floorTxt)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form20"
        Me.Text = "Form20"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label6 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents CancelBtn As Button
    Friend WithEvents printReceipt As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents floorTxt As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Typelbl As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents unitNumber As Label
    Friend WithEvents tenantName As Label
    Friend WithEvents billingMonth As Label
    Friend WithEvents datePayment As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents amountPaid As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
End Class
