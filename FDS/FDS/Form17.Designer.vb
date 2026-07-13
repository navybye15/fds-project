<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form17
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
        Me.CancelBtn = New System.Windows.Forms.Button()
        Me.Recordbtn = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MonthYearlbl = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Tenantlbl = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Paidtxt = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Chargelbl = New System.Windows.Forms.Label()
        Me.Rentlbl = New System.Windows.Forms.Label()
        Me.Totallbl = New System.Windows.Forms.Label()
        Me.partialPayment = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Statuscmb = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'CancelBtn
        '
        Me.CancelBtn.Location = New System.Drawing.Point(358, 540)
        Me.CancelBtn.Name = "CancelBtn"
        Me.CancelBtn.Size = New System.Drawing.Size(107, 27)
        Me.CancelBtn.TabIndex = 116
        Me.CancelBtn.Text = "Cancel"
        Me.CancelBtn.UseVisualStyleBackColor = True
        '
        'Recordbtn
        '
        Me.Recordbtn.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.Recordbtn.ForeColor = System.Drawing.Color.PaleGreen
        Me.Recordbtn.Location = New System.Drawing.Point(481, 536)
        Me.Recordbtn.Name = "Recordbtn"
        Me.Recordbtn.Size = New System.Drawing.Size(129, 31)
        Me.Recordbtn.TabIndex = 115
        Me.Recordbtn.Text = "Record Payment"
        Me.Recordbtn.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.MonthYearlbl)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Tenantlbl)
        Me.Panel1.Location = New System.Drawing.Point(-2, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(662, 81)
        Me.Panel1.TabIndex = 108
        '
        'MonthYearlbl
        '
        Me.MonthYearlbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MonthYearlbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.MonthYearlbl.Location = New System.Drawing.Point(195, 45)
        Me.MonthYearlbl.Name = "MonthYearlbl"
        Me.MonthYearlbl.Size = New System.Drawing.Size(241, 18)
        Me.MonthYearlbl.TabIndex = 13
        Me.MonthYearlbl.Text = "Month Year"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label3.Location = New System.Drawing.Point(171, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 18)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "-"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Font = New System.Drawing.Font("Garamond", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(23, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(195, 26)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Record Payment"
        '
        'Tenantlbl
        '
        Me.Tenantlbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tenantlbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Tenantlbl.Location = New System.Drawing.Point(25, 45)
        Me.Tenantlbl.Name = "Tenantlbl"
        Me.Tenantlbl.Size = New System.Drawing.Size(143, 18)
        Me.Tenantlbl.TabIndex = 0
        Me.Tenantlbl.Text = "Tenant"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label7.Location = New System.Drawing.Point(41, 421)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(161, 16)
        Me.Label7.TabIndex = 133
        Me.Label7.Text = "Status"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label6.Location = New System.Drawing.Point(41, 335)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(161, 25)
        Me.Label6.TabIndex = 131
        Me.Label6.Text = "Amount Paid"
        '
        'Paidtxt
        '
        Me.Paidtxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Paidtxt.Location = New System.Drawing.Point(44, 363)
        Me.Paidtxt.Multiline = True
        Me.Paidtxt.Name = "Paidtxt"
        Me.Paidtxt.Size = New System.Drawing.Size(566, 34)
        Me.Paidtxt.TabIndex = 130
        '
        'Panel2
        '
        Me.Panel2.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.Chargelbl)
        Me.Panel2.Controls.Add(Me.Rentlbl)
        Me.Panel2.Controls.Add(Me.Totallbl)
        Me.Panel2.Controls.Add(Me.partialPayment)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Location = New System.Drawing.Point(44, 109)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(566, 191)
        Me.Panel2.TabIndex = 109
        '
        'Chargelbl
        '
        Me.Chargelbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chargelbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Chargelbl.Location = New System.Drawing.Point(357, 92)
        Me.Chargelbl.Name = "Chargelbl"
        Me.Chargelbl.Size = New System.Drawing.Size(192, 25)
        Me.Chargelbl.TabIndex = 139
        Me.Chargelbl.Text = "Additional Chargers"
        Me.Chargelbl.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Rentlbl
        '
        Me.Rentlbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Rentlbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Rentlbl.Location = New System.Drawing.Point(362, 60)
        Me.Rentlbl.Name = "Rentlbl"
        Me.Rentlbl.Size = New System.Drawing.Size(187, 25)
        Me.Rentlbl.TabIndex = 138
        Me.Rentlbl.Text = "Base Rent"
        Me.Rentlbl.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Totallbl
        '
        Me.Totallbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Totallbl.ForeColor = System.Drawing.Color.MediumAquamarine
        Me.Totallbl.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Totallbl.Location = New System.Drawing.Point(367, 13)
        Me.Totallbl.Name = "Totallbl"
        Me.Totallbl.Size = New System.Drawing.Size(182, 25)
        Me.Totallbl.TabIndex = 137
        Me.Totallbl.Text = "Total Due"
        Me.Totallbl.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'partialPayment
        '
        Me.partialPayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.partialPayment.ForeColor = System.Drawing.Color.Firebrick
        Me.partialPayment.Location = New System.Drawing.Point(298, 123)
        Me.partialPayment.Name = "partialPayment"
        Me.partialPayment.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.partialPayment.Size = New System.Drawing.Size(247, 25)
        Me.partialPayment.TabIndex = 136
        Me.partialPayment.Text = "Partial Payment"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label8.Location = New System.Drawing.Point(13, 122)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(247, 25)
        Me.Label8.TabIndex = 136
        Me.Label8.Text = "Partial Payment"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label5.Location = New System.Drawing.Point(13, 92)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(247, 25)
        Me.Label5.TabIndex = 136
        Me.Label5.Text = "Additional Chargers"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label4.Location = New System.Drawing.Point(13, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(161, 25)
        Me.Label4.TabIndex = 135
        Me.Label4.Text = "Base Rent"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label1.Location = New System.Drawing.Point(11, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(161, 25)
        Me.Label1.TabIndex = 134
        Me.Label1.Text = "Total Due"
        '
        'Statuscmb
        '
        Me.Statuscmb.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Statuscmb.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Statuscmb.FormattingEnabled = True
        Me.Statuscmb.Items.AddRange(New Object() {"active", "terminated", "expired"})
        Me.Statuscmb.Location = New System.Drawing.Point(44, 440)
        Me.Statuscmb.Name = "Statuscmb"
        Me.Statuscmb.Size = New System.Drawing.Size(566, 33)
        Me.Statuscmb.TabIndex = 140
        '
        'Form17
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(659, 582)
        Me.Controls.Add(Me.Statuscmb)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Paidtxt)
        Me.Controls.Add(Me.CancelBtn)
        Me.Controls.Add(Me.Recordbtn)
        Me.Controls.Add(Me.Panel1)
        Me.MaximumSize = New System.Drawing.Size(677, 629)
        Me.MinimumSize = New System.Drawing.Size(677, 629)
        Me.Name = "Form17"
        Me.Text = "Form17"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CancelBtn As Button
    Friend WithEvents Recordbtn As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Tenantlbl As Label
    Friend WithEvents MonthYearlbl As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Paidtxt As TextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Chargelbl As Label
    Friend WithEvents Rentlbl As Label
    Friend WithEvents Totallbl As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Statuscmb As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents partialPayment As Label
End Class
