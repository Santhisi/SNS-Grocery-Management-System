Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Public Class LoginForm
    Inherits Form

    ' ALL CONTROLS MUST BE WithEvents TO USE "Handles"
    Private WithEvents txtUsername As TextBox
    Private WithEvents txtPassword As TextBox
    Private WithEvents btnLogin As Button
    Private WithEvents btnExit As Button
    Private WithEvents pnlContainer As Panel

    Private picLogo As PictureBox
    Private lblTitle As Label

    Public Sub New()
        MyBase.New()
        InitializeBeautifulForm()
    End Sub

    Private Sub InitializeBeautifulForm()
        ' === FORM SETTINGS ===
        Me.Text = "SnS Grocery - Login"
        Me.Size = New Size(500, 660)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.None
        Me.BackColor = Color.FromArgb(240, 248, 255)

        ' Rounded form
        SetRoundedRegion(Me, 30)

        ' === MAIN CONTAINER PANEL ===
        pnlContainer = New Panel With {
            .Size = New Size(420, 580),
            .Location = New Point(40, 40),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.None
        }
        SetRoundedRegion(pnlContainer, 25)
        Me.Controls.Add(pnlContainer)

        ' === PERFECT LOGO WITH REAL IMAGE FROM RESOURCES (Byte() → Image) ===
        picLogo = New PictureBox With {
    .Size = New Size(140, 140),
    .Location = New Point(140, 45),
    .SizeMode = PictureBoxSizeMode.Zoom,
    .BackColor = Color.Transparent
}
        ' CORRECT WAY: Convert Byte() to Image
        picLogo.Image = My.Resources.Resource1.sns_logo   ' or whatever your resource name is

        ' Make it perfectly round
        Dim path As New GraphicsPath()
        path.AddEllipse(0, 0, picLogo.Width, picLogo.Height)
        picLogo.Region = New Region(path)

        pnlContainer.Controls.Add(picLogo)

        ' === TITLE ===
        lblTitle = New Label With {
            .Text = "Welcome to SnS Grocery",
            .Font = New Font("Segoe UI", 18, FontStyle.Bold),
            .ForeColor = Color.FromArgb(30, 110, 60),
            .Location = New Point(0, 200),
            .Size = New Size(420, 50),
            .TextAlign = ContentAlignment.MiddleCenter
        }
        pnlContainer.Controls.Add(lblTitle)

        Dim subTitle As New Label With {
            .Text = "Sign in to continue",
            .Font = New Font("Segoe UI", 10),
            .ForeColor = Color.Gray,
            .Location = New Point(0, 240),
            .Size = New Size(420, 30),
            .TextAlign = ContentAlignment.MiddleCenter
        }
        pnlContainer.Controls.Add(subTitle)

        ' === USERNAME ===
        txtUsername = New TextBox With {
            .Size = New Size(300, 48),
            .Location = New Point(60, 300),
            .Font = New Font("Segoe UI", 11),
            .BorderStyle = BorderStyle.FixedSingle,
            .BackColor = Color.FromArgb(250, 252, 255),
            .Padding = New Padding(15, 12, 15, 12)
        }
        SetRoundedRegion(txtUsername, 14)
        pnlContainer.Controls.Add(txtUsername)

        ' === PASSWORD ===
        txtPassword = New TextBox With {
            .Size = New Size(300, 48),
            .Location = New Point(60, 370),
            .Font = New Font("Segoe UI", 11),
            .BorderStyle = BorderStyle.FixedSingle,
            .PasswordChar = "●"c,
            .BackColor = Color.FromArgb(250, 252, 255),
            .Padding = New Padding(15, 12, 15, 12)
        }
        SetRoundedRegion(txtPassword, 14)
        pnlContainer.Controls.Add(txtPassword)

        ' === LOGIN BUTTON ===
        btnLogin = New Button With {
            .Text = "LOGIN",
            .Size = New Size(300, 58),
            .Location = New Point(60, 450),
            .FlatStyle = FlatStyle.Flat,
            .BackColor = Color.FromArgb(46, 139, 87),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 14, FontStyle.Bold),
            .Cursor = Cursors.Hand
        }
        btnLogin.FlatAppearance.BorderSize = 0
        SetRoundedRegion(btnLogin, 16)
        pnlContainer.Controls.Add(btnLogin)

        ' === EXIT BUTTON ===
        btnExit = New Button With {
            .Text = "Exit",
            .Size = New Size(100, 42),
            .Location = New Point(160, 530),
            .FlatStyle = FlatStyle.Flat,
            .BackColor = Color.FromArgb(220, 70, 70),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold)
        }
        btnExit.FlatAppearance.BorderSize = 0
        SetRoundedRegion(btnExit, 12)
        pnlContainer.Controls.Add(btnExit)

        ' Hint
        Dim hint As New Label With {
            .Text = "Demo: admin / admin123",
            .Font = New Font("Segoe UI", 9),
            .ForeColor = Color.Gray,
            .Location = New Point(0, 590),
            .Size = New Size(420, 25),
            .TextAlign = ContentAlignment.MiddleCenter
        }
        pnlContainer.Controls.Add(hint)
    End Sub

    ' === ROUNDED CORNERS ===
    Private Sub SetRoundedRegion(ctrl As Control, radius As Integer)
        Dim path As New GraphicsPath()
        Dim rect As New Rectangle(0, 0, ctrl.Width, ctrl.Height)
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
        path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90)
        path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90)
        path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90)
        path.CloseFigure()
        ctrl.Region = New Region(path)
    End Sub

    ' === SHADOW EFFECT (NOW WORKS!) ===
    Private Sub pnlContainer_Paint(sender As Object, e As PaintEventArgs) Handles pnlContainer.Paint
        Using brush As New SolidBrush(Color.FromArgb(70, 0, 0, 0))
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            e.Graphics.FillEllipse(brush, -20, -20, 60, 60)
            e.Graphics.FillEllipse(brush, pnlContainer.Width - 40, -20, 60, 60)
            e.Graphics.FillEllipse(brush, -20, pnlContainer.Height - 40, 60, 60)
            e.Graphics.FillEllipse(brush, pnlContainer.Width - 40, pnlContainer.Height - 40, 60, 60)
        End Using
    End Sub

    ' === LOGIN BUTTON CLICK ===
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If String.IsNullOrWhiteSpace(txtUsername.Text) Or String.IsNullOrWhiteSpace(txtPassword.Text) Then
            MessageBox.Show("Please enter username and password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim dt As DataTable = ExecuteQuery("SELECT * FROM users WHERE username=@u AND password=@p",
                New List(Of MySqlParameter) From {
                    New MySqlParameter("@u", txtUsername.Text.Trim),
                    New MySqlParameter("@p", txtPassword.Text.Trim)
                })

            If dt.Rows.Count > 0 Then
                MessageBox.Show("Login Successful!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim dash As New DashboardForm()
                dash.Show()
                Me.Hide()
            Else
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    ' === EXIT BUTTON ===
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class