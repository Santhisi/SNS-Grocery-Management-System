<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DashboardForm
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
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.cardItems = New System.Windows.Forms.Panel()
        Me.lblItems = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cardCats = New System.Windows.Forms.Panel()
        Me.lblCats = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cardBills = New System.Windows.Forms.Panel()
        Me.lblBills = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnItems = New System.Windows.Forms.Button()
        Me.btnCategories = New System.Windows.Forms.Button()
        Me.btnBilling = New System.Windows.Forms.Button()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.cardItems.SuspendLayout()
        Me.cardCats.SuspendLayout()
        Me.cardBills.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 25.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(50, 35)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(427, 59)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "SnS Grocery System"
        '
        'cardItems
        '
        Me.cardItems.BackColor = System.Drawing.Color.SeaGreen
        Me.cardItems.Controls.Add(Me.lblItems)
        Me.cardItems.Controls.Add(Me.Label1)
        Me.cardItems.Location = New System.Drawing.Point(60, 110)
        Me.cardItems.Name = "cardItems"
        Me.cardItems.Size = New System.Drawing.Size(260, 120)
        Me.cardItems.TabIndex = 1
        '
        'lblItems
        '
        Me.lblItems.AutoSize = True
        Me.lblItems.Font = New System.Drawing.Font("Microsoft Sans Serif", 31.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItems.ForeColor = System.Drawing.Color.White
        Me.lblItems.Location = New System.Drawing.Point(22, 52)
        Me.lblItems.Name = "lblItems"
        Me.lblItems.Size = New System.Drawing.Size(57, 61)
        Me.lblItems.TabIndex = 1
        Me.lblItems.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(22, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Total Items"
        '
        'cardCats
        '
        Me.cardCats.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.cardCats.Controls.Add(Me.lblCats)
        Me.cardCats.Controls.Add(Me.Label2)
        Me.cardCats.Location = New System.Drawing.Point(380, 110)
        Me.cardCats.Name = "cardCats"
        Me.cardCats.Size = New System.Drawing.Size(260, 120)
        Me.cardCats.TabIndex = 2
        '
        'lblCats
        '
        Me.lblCats.AutoSize = True
        Me.lblCats.Font = New System.Drawing.Font("Microsoft Sans Serif", 31.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCats.ForeColor = System.Drawing.Color.White
        Me.lblCats.Location = New System.Drawing.Point(22, 52)
        Me.lblCats.Name = "lblCats"
        Me.lblCats.Size = New System.Drawing.Size(57, 61)
        Me.lblCats.TabIndex = 2
        Me.lblCats.Text = "0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(22, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 22)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Categories"
        '
        'cardBills
        '
        Me.cardBills.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.cardBills.Controls.Add(Me.lblBills)
        Me.cardBills.Controls.Add(Me.Label4)
        Me.cardBills.Location = New System.Drawing.Point(700, 110)
        Me.cardBills.Name = "cardBills"
        Me.cardBills.Size = New System.Drawing.Size(240, 120)
        Me.cardBills.TabIndex = 3
        '
        'lblBills
        '
        Me.lblBills.AutoSize = True
        Me.lblBills.Font = New System.Drawing.Font("Microsoft Sans Serif", 31.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBills.ForeColor = System.Drawing.Color.White
        Me.lblBills.Location = New System.Drawing.Point(22, 52)
        Me.lblBills.Name = "lblBills"
        Me.lblBills.Size = New System.Drawing.Size(57, 61)
        Me.lblBills.TabIndex = 4
        Me.lblBills.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(22, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 22)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Today's Bills"
        '
        'btnItems
        '
        Me.btnItems.BackColor = System.Drawing.Color.SeaGreen
        Me.btnItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnItems.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnItems.ForeColor = System.Drawing.Color.White
        Me.btnItems.Location = New System.Drawing.Point(60, 346)
        Me.btnItems.Name = "btnItems"
        Me.btnItems.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.btnItems.Size = New System.Drawing.Size(880, 68)
        Me.btnItems.TabIndex = 4
        Me.btnItems.Text = "Manage Items"
        Me.btnItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnItems.UseVisualStyleBackColor = False
        '
        'btnCategories
        '
        Me.btnCategories.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCategories.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCategories.ForeColor = System.Drawing.Color.White
        Me.btnCategories.Location = New System.Drawing.Point(60, 259)
        Me.btnCategories.Name = "btnCategories"
        Me.btnCategories.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.btnCategories.Size = New System.Drawing.Size(880, 68)
        Me.btnCategories.TabIndex = 5
        Me.btnCategories.Text = "Manage Categories"
        Me.btnCategories.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCategories.UseVisualStyleBackColor = False
        '
        'btnBilling
        '
        Me.btnBilling.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.btnBilling.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBilling.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBilling.ForeColor = System.Drawing.Color.White
        Me.btnBilling.Location = New System.Drawing.Point(60, 432)
        Me.btnBilling.Name = "btnBilling"
        Me.btnBilling.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.btnBilling.Size = New System.Drawing.Size(880, 68)
        Me.btnBilling.TabIndex = 6
        Me.btnBilling.Text = "Create New Bill"
        Me.btnBilling.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBilling.UseVisualStyleBackColor = False
        '
        'btnLogout
        '
        Me.btnLogout.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Location = New System.Drawing.Point(60, 518)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.btnLogout.Size = New System.Drawing.Size(880, 68)
        Me.btnLogout.TabIndex = 7
        Me.btnLogout.Text = "Log out"
        Me.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogout.UseVisualStyleBackColor = False
        '
        'DashboardForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(982, 633)
        Me.Controls.Add(Me.btnLogout)
        Me.Controls.Add(Me.btnBilling)
        Me.Controls.Add(Me.btnCategories)
        Me.Controls.Add(Me.btnItems)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.cardItems)
        Me.Controls.Add(Me.cardCats)
        Me.Controls.Add(Me.cardBills)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DashboardForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dashboard - SnS Grocery"
        Me.cardItems.ResumeLayout(False)
        Me.cardItems.PerformLayout()
        Me.cardCats.ResumeLayout(False)
        Me.cardCats.PerformLayout()
        Me.cardBills.ResumeLayout(False)
        Me.cardBills.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents cardItems As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents lblItems As Label
    Friend WithEvents cardCats As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents lblCats As Label
    Friend WithEvents cardBills As Panel
    Friend WithEvents lblBills As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnItems As Button
    Friend WithEvents btnCategories As Button
    Friend WithEvents btnBilling As Button
    Friend WithEvents btnLogout As Button
End Class
