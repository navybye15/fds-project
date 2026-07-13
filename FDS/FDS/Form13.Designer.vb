<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form13
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
        Me.leaseStartDtp = New System.Windows.Forms.DateTimePicker()
        Me.leaseEndDtp = New System.Windows.Forms.DateTimePicker()
        Me.cancelBtn = New System.Windows.Forms.Button()
        Me.createLeaseBtn = New System.Windows.Forms.Button()
        Me.tenantCmb = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.UnitNumberlbl = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.texat = New System.Windows.Forms.Label()
        Me.Monthlylbl = New System.Windows.Forms.Label()
        Me.securityDepositTxt = New System.Windows.Forms.TextBox()
        Me.Loclbl = New System.Windows.Forms.Label()
        Me.monthlyRateTxt = New System.Windows.Forms.TextBox()
        Me.assignUnitCmb = New System.Windows.Forms.ComboBox()
        Me.statusCmb = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'leaseStartDtp
        '
        Me.leaseStartDtp.CustomFormat = ""
        Me.leaseStartDtp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.leaseStartDtp.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.leaseStartDtp.Location = New System.Drawing.Point(35, 350)
        Me.leaseStartDtp.Name = "leaseStartDtp"
        Me.leaseStartDtp.Size = New System.Drawing.Size(276, 30)
        Me.leaseStartDtp.TabIndex = 55
        '
        'leaseEndDtp
        '
        Me.leaseEndDtp.CustomFormat = ""
        Me.leaseEndDtp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.leaseEndDtp.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.leaseEndDtp.Location = New System.Drawing.Point(329, 350)
        Me.leaseEndDtp.Name = "leaseEndDtp"
        Me.leaseEndDtp.Size = New System.Drawing.Size(276, 30)
        Me.leaseEndDtp.TabIndex = 54
        '
        'cancelBtn
        '
        Me.cancelBtn.BackColor = System.Drawing.Color.White
        Me.cancelBtn.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.cancelBtn.Location = New System.Drawing.Point(329, 574)
        Me.cancelBtn.Name = "cancelBtn"
        Me.cancelBtn.Size = New System.Drawing.Size(104, 44)
        Me.cancelBtn.TabIndex = 53
        Me.cancelBtn.Text = "Cancel"
        Me.cancelBtn.UseVisualStyleBackColor = False
        '
        'createLeaseBtn
        '
        Me.createLeaseBtn.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.createLeaseBtn.ForeColor = System.Drawing.Color.PaleGreen
        Me.createLeaseBtn.Location = New System.Drawing.Point(451, 574)
        Me.createLeaseBtn.Name = "createLeaseBtn"
        Me.createLeaseBtn.Size = New System.Drawing.Size(154, 44)
        Me.createLeaseBtn.TabIndex = 52
        Me.createLeaseBtn.Text = "Create Lease"
        Me.createLeaseBtn.UseVisualStyleBackColor = False
        '
        'tenantCmb
        '
        Me.tenantCmb.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.tenantCmb.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tenantCmb.FormattingEnabled = True
        Me.tenantCmb.Location = New System.Drawing.Point(37, 138)
        Me.tenantCmb.Name = "tenantCmb"
        Me.tenantCmb.Size = New System.Drawing.Size(276, 33)
        Me.tenantCmb.TabIndex = 51
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label20.Location = New System.Drawing.Point(32, 424)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(185, 47)
        Me.Label20.TabIndex = 39
        Me.Label20.Text = "Status"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label7.Location = New System.Drawing.Point(32, 327)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(185, 47)
        Me.Label7.TabIndex = 42
        Me.Label7.Text = "Lease Start"
        '
        'UnitNumberlbl
        '
        Me.UnitNumberlbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UnitNumberlbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.UnitNumberlbl.Location = New System.Drawing.Point(32, 112)
        Me.UnitNumberlbl.Name = "UnitNumberlbl"
        Me.UnitNumberlbl.Size = New System.Drawing.Size(185, 47)
        Me.UnitNumberlbl.TabIndex = 36
        Me.UnitNumberlbl.Text = "Select Tenant"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label19.Location = New System.Drawing.Point(326, 327)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(185, 47)
        Me.Label19.TabIndex = 37
        Me.Label19.Text = "Lease End"
        '
        'texat
        '
        Me.texat.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.texat.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.texat.Location = New System.Drawing.Point(326, 112)
        Me.texat.Name = "texat"
        Me.texat.Size = New System.Drawing.Size(185, 47)
        Me.texat.TabIndex = 38
        Me.texat.Text = "Assign Unit"
        '
        'Monthlylbl
        '
        Me.Monthlylbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Monthlylbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Monthlylbl.Location = New System.Drawing.Point(326, 219)
        Me.Monthlylbl.Name = "Monthlylbl"
        Me.Monthlylbl.Size = New System.Drawing.Size(239, 47)
        Me.Monthlylbl.TabIndex = 44
        Me.Monthlylbl.Text = "Security Deposit"
        '
        'securityDepositTxt
        '
        Me.securityDepositTxt.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.securityDepositTxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.securityDepositTxt.Location = New System.Drawing.Point(329, 242)
        Me.securityDepositTxt.Multiline = True
        Me.securityDepositTxt.Name = "securityDepositTxt"
        Me.securityDepositTxt.Size = New System.Drawing.Size(276, 51)
        Me.securityDepositTxt.TabIndex = 47
        '
        'Loclbl
        '
        Me.Loclbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Loclbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Loclbl.Location = New System.Drawing.Point(32, 219)
        Me.Loclbl.Name = "Loclbl"
        Me.Loclbl.Size = New System.Drawing.Size(228, 47)
        Me.Loclbl.TabIndex = 41
        Me.Loclbl.Text = "Monthly Rate"
        '
        'monthlyRateTxt
        '
        Me.monthlyRateTxt.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.monthlyRateTxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.monthlyRateTxt.Location = New System.Drawing.Point(35, 242)
        Me.monthlyRateTxt.Multiline = True
        Me.monthlyRateTxt.Name = "monthlyRateTxt"
        Me.monthlyRateTxt.Size = New System.Drawing.Size(276, 51)
        Me.monthlyRateTxt.TabIndex = 49
        '
        'assignUnitCmb
        '
        Me.assignUnitCmb.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.assignUnitCmb.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.assignUnitCmb.FormattingEnabled = True
        Me.assignUnitCmb.Location = New System.Drawing.Point(329, 138)
        Me.assignUnitCmb.Name = "assignUnitCmb"
        Me.assignUnitCmb.Size = New System.Drawing.Size(276, 33)
        Me.assignUnitCmb.TabIndex = 51
        '
        'statusCmb
        '
        Me.statusCmb.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.statusCmb.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.statusCmb.FormattingEnabled = True
        Me.statusCmb.Items.AddRange(New Object() {"active", "terminated", "expired"})
        Me.statusCmb.Location = New System.Drawing.Point(35, 449)
        Me.statusCmb.Name = "statusCmb"
        Me.statusCmb.Size = New System.Drawing.Size(276, 33)
        Me.statusCmb.TabIndex = 51
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Font = New System.Drawing.Font("Garamond", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(23, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(195, 22)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Add Lease"
        '
        'Panel1
        '
        Me.Panel1.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(-9, -1)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(654, 81)
        Me.Panel1.TabIndex = 56
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label3.Location = New System.Drawing.Point(25, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(330, 18)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Create a new lease agreement"
        '
        'Form13
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(637, 645)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.leaseStartDtp)
        Me.Controls.Add(Me.leaseEndDtp)
        Me.Controls.Add(Me.cancelBtn)
        Me.Controls.Add(Me.createLeaseBtn)
        Me.Controls.Add(Me.assignUnitCmb)
        Me.Controls.Add(Me.statusCmb)
        Me.Controls.Add(Me.tenantCmb)
        Me.Controls.Add(Me.monthlyRateTxt)
        Me.Controls.Add(Me.Loclbl)
        Me.Controls.Add(Me.securityDepositTxt)
        Me.Controls.Add(Me.Monthlylbl)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.UnitNumberlbl)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.texat)
        Me.MaximumSize = New System.Drawing.Size(655, 692)
        Me.MinimumSize = New System.Drawing.Size(655, 692)
        Me.Name = "Form13"
        Me.Text = "Form13"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents leaseStartDtp As DateTimePicker
    Friend WithEvents leaseEndDtp As DateTimePicker
    Friend WithEvents cancelBtn As Button
    Friend WithEvents createLeaseBtn As Button
    Friend WithEvents tenantCmb As ComboBox
    Friend WithEvents Label20 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents UnitNumberlbl As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents texat As Label
    Friend WithEvents Monthlylbl As Label
    Friend WithEvents securityDepositTxt As TextBox
    Friend WithEvents Loclbl As Label
    Friend WithEvents monthlyRateTxt As TextBox
    Friend WithEvents assignUnitCmb As ComboBox
    Friend WithEvents statusCmb As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label3 As Label
End Class
