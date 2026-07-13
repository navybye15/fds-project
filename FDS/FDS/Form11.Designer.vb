<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form11
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CategoryCmb = New System.Windows.Forms.ComboBox()
        Me.Typelbl = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateIncurredDtp = New System.Windows.Forms.DateTimePicker()
        Me.DescriptionTxt = New System.Windows.Forms.TextBox()
        Me.Desclbl = New System.Windows.Forms.Label()
        Me.AmtTxt = New System.Windows.Forms.TextBox()
        Me.Monthlylbl = New System.Windows.Forms.Label()
        Me.RecordedTxt = New System.Windows.Forms.TextBox()
        Me.Loclbl = New System.Windows.Forms.Label()
        Me.saveExpenseBtn = New System.Windows.Forms.Button()
        Me.CancelBtn = New System.Windows.Forms.Button()
        Me.UnitCmb = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(156, 114)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(0, 0)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(-3, -2)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(654, 81)
        Me.Panel1.TabIndex = 27
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
        Me.Label2.Text = "Add Expense"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label3.Location = New System.Drawing.Point(25, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(330, 18)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Log a new building expense record"
        '
        'CategoryCmb
        '
        Me.CategoryCmb.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CategoryCmb.FormattingEnabled = True
        Me.CategoryCmb.Location = New System.Drawing.Point(27, 120)
        Me.CategoryCmb.Name = "CategoryCmb"
        Me.CategoryCmb.Size = New System.Drawing.Size(262, 30)
        Me.CategoryCmb.TabIndex = 58
        '
        'Typelbl
        '
        Me.Typelbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Typelbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Typelbl.Location = New System.Drawing.Point(24, 97)
        Me.Typelbl.Name = "Typelbl"
        Me.Typelbl.Size = New System.Drawing.Size(106, 20)
        Me.Typelbl.TabIndex = 57
        Me.Typelbl.Text = "Category"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label1.Location = New System.Drawing.Point(23, 387)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 20)
        Me.Label1.TabIndex = 59
        Me.Label1.Text = "Date Incurred"
        '
        'DateIncurredDtp
        '
        Me.DateIncurredDtp.CustomFormat = ""
        Me.DateIncurredDtp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateIncurredDtp.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateIncurredDtp.Location = New System.Drawing.Point(24, 404)
        Me.DateIncurredDtp.Name = "DateIncurredDtp"
        Me.DateIncurredDtp.Size = New System.Drawing.Size(276, 30)
        Me.DateIncurredDtp.TabIndex = 60
        '
        'DescriptionTxt
        '
        Me.DescriptionTxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DescriptionTxt.Location = New System.Drawing.Point(27, 214)
        Me.DescriptionTxt.Multiline = True
        Me.DescriptionTxt.Name = "DescriptionTxt"
        Me.DescriptionTxt.Size = New System.Drawing.Size(556, 37)
        Me.DescriptionTxt.TabIndex = 62
        '
        'Desclbl
        '
        Me.Desclbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Desclbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Desclbl.Location = New System.Drawing.Point(23, 191)
        Me.Desclbl.Name = "Desclbl"
        Me.Desclbl.Size = New System.Drawing.Size(116, 20)
        Me.Desclbl.TabIndex = 61
        Me.Desclbl.Text = "Description"
        '
        'AmtTxt
        '
        Me.AmtTxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AmtTxt.Location = New System.Drawing.Point(27, 323)
        Me.AmtTxt.Multiline = True
        Me.AmtTxt.Name = "AmtTxt"
        Me.AmtTxt.Size = New System.Drawing.Size(262, 34)
        Me.AmtTxt.TabIndex = 64
        '
        'Monthlylbl
        '
        Me.Monthlylbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Monthlylbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Monthlylbl.Location = New System.Drawing.Point(24, 300)
        Me.Monthlylbl.Name = "Monthlylbl"
        Me.Monthlylbl.Size = New System.Drawing.Size(160, 30)
        Me.Monthlylbl.TabIndex = 63
        Me.Monthlylbl.Text = "Amount (₱)"
        '
        'RecordedTxt
        '
        Me.RecordedTxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RecordedTxt.Location = New System.Drawing.Point(309, 323)
        Me.RecordedTxt.Multiline = True
        Me.RecordedTxt.Name = "RecordedTxt"
        Me.RecordedTxt.Size = New System.Drawing.Size(274, 34)
        Me.RecordedTxt.TabIndex = 66
        '
        'Loclbl
        '
        Me.Loclbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Loclbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Loclbl.Location = New System.Drawing.Point(306, 305)
        Me.Loclbl.Name = "Loclbl"
        Me.Loclbl.Size = New System.Drawing.Size(161, 25)
        Me.Loclbl.TabIndex = 65
        Me.Loclbl.Text = "Recorded By"
        '
        'saveExpenseBtn
        '
        Me.saveExpenseBtn.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.saveExpenseBtn.ForeColor = System.Drawing.Color.PaleGreen
        Me.saveExpenseBtn.Location = New System.Drawing.Point(483, 403)
        Me.saveExpenseBtn.Name = "saveExpenseBtn"
        Me.saveExpenseBtn.Size = New System.Drawing.Size(125, 31)
        Me.saveExpenseBtn.TabIndex = 67
        Me.saveExpenseBtn.Text = "Save Expense"
        Me.saveExpenseBtn.UseVisualStyleBackColor = False
        '
        'CancelBtn
        '
        Me.CancelBtn.Location = New System.Drawing.Point(374, 405)
        Me.CancelBtn.Name = "CancelBtn"
        Me.CancelBtn.Size = New System.Drawing.Size(103, 27)
        Me.CancelBtn.TabIndex = 68
        Me.CancelBtn.Text = "Cancel"
        Me.CancelBtn.UseVisualStyleBackColor = True
        '
        'UnitCmb
        '
        Me.UnitCmb.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UnitCmb.FormattingEnabled = True
        Me.UnitCmb.Location = New System.Drawing.Point(321, 120)
        Me.UnitCmb.Name = "UnitCmb"
        Me.UnitCmb.Size = New System.Drawing.Size(262, 30)
        Me.UnitCmb.TabIndex = 70
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label4.Location = New System.Drawing.Point(318, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(106, 20)
        Me.Label4.TabIndex = 69
        Me.Label4.Text = "Unit Number"
        '
        'Form11
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(637, 473)
        Me.Controls.Add(Me.UnitCmb)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.CancelBtn)
        Me.Controls.Add(Me.saveExpenseBtn)
        Me.Controls.Add(Me.RecordedTxt)
        Me.Controls.Add(Me.Loclbl)
        Me.Controls.Add(Me.AmtTxt)
        Me.Controls.Add(Me.Monthlylbl)
        Me.Controls.Add(Me.DescriptionTxt)
        Me.Controls.Add(Me.Desclbl)
        Me.Controls.Add(Me.DateIncurredDtp)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CategoryCmb)
        Me.Controls.Add(Me.Typelbl)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button1)
        Me.MaximumSize = New System.Drawing.Size(655, 520)
        Me.MinimumSize = New System.Drawing.Size(655, 520)
        Me.Name = "Form11"
        Me.Text = "Form11"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents CategoryCmb As ComboBox
    Friend WithEvents Typelbl As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents DateIncurredDtp As DateTimePicker
    Friend WithEvents DescriptionTxt As TextBox
    Friend WithEvents Desclbl As Label
    Friend WithEvents AmtTxt As TextBox
    Friend WithEvents Monthlylbl As Label
    Friend WithEvents RecordedTxt As TextBox
    Friend WithEvents Loclbl As Label
    Friend WithEvents saveExpenseBtn As Button
    Friend WithEvents CancelBtn As Button
    Friend WithEvents UnitCmb As ComboBox
    Friend WithEvents Label4 As Label
End Class
