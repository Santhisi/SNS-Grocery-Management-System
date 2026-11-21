Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Public Class DashboardForm
    Inherits Form

    Private Sub DashboardForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Apply rounded corners to everything
        SetRoundedRegion(Me, 30)

        ' Cards
        SetRoundedRegion(cardItems, 20)
        SetRoundedRegion(cardCats, 20)
        SetRoundedRegion(cardBills, 20)

        ' Buttons
        SetRoundedRegion(btnCategories, 16)
        SetRoundedRegion(btnItems, 16)
        SetRoundedRegion(btnBilling, 16)
        SetRoundedRegion(btnLogout, 16)

        LoadStats()
    End Sub

    Private Sub LoadStats()
        Try
            lblItems.Text = ExecuteScalar("SELECT COUNT(*) FROM items").ToString()
            lblCats.Text = ExecuteScalar("SELECT COUNT(*) FROM categories").ToString()
            lblBills.Text = ExecuteScalar("SELECT COUNT(*) FROM bills WHERE DATE(bill_date) = CURDATE()").ToString()
        Catch
            lblItems.Text = "0"
            lblCats.Text = "0"
            lblBills.Text = "0"
        End Try
    End Sub

    Private Sub btnCategories_Click() Handles btnCategories.Click
        Dim f As New CategoriesForm()
        f.Show()
        Me.Hide()
    End Sub

    Private Sub btnItems_Click() Handles btnItems.Click
        Dim f As New ItemsForm()
        f.Show()
        Me.Hide()
    End Sub

    Private Sub btnBilling_Click() Handles btnBilling.Click
        Dim f As New btnRemoveItem()
        f.Show()
        Me.Hide()
    End Sub

    Private Sub btnLogout_Click() Handles btnLogout.Click
        If MessageBox.Show("Are you sure you want to logout?", "Logout",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.Close()
            Dim login As New LoginForm()
            login.Show()
        End If
    End Sub

    ' Rounded corners helper
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

    ' Optional: Helper for scalar queries
    Private Function ExecuteScalar(query As String) As Object
        Try
            OpenConnection()
            Dim cmd As New MySqlCommand(query, Conn)
            Return cmd.ExecuteScalar()
        Catch
            Return 0
        Finally
            CloseConnection()
        End Try
    End Function
End Class