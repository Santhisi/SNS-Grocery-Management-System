Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Public Class CategoriesForm
    Inherits Form

    Private selectedId As Integer = 0

    Private Sub CategoriesForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === ROUNDED CORNERS ===
        SetRoundedRegion(Me, 30)
        SetRoundedRegion(pnlMain, 24)
        SetRoundedRegion(txtCatName, 10)
        SetRoundedRegion(btnAdd, 10)
        SetRoundedRegion(btnUpdate, 10)
        SetRoundedRegion(btnDelete, 10)
        SetRoundedRegion(btnBack, 12)
        SetRoundedRegion(btnClear, 12)
        SetRoundedRegion(dgvCategories, 16)

        ' === DATAGRIDVIEW CUSTOM STYLING (These can't be set in Designer) ===
        With dgvCategories
            .ColumnHeadersHeight = 42
            .RowTemplate.Height = 34
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 180, 100)
            .DefaultCellStyle.SelectionForeColor = Color.White
            .EnableHeadersVisualStyles = False  ' Important! To allow custom header colors
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 110, 60)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            .GridColor = Color.FromArgb(240, 240, 240)
        End With

        LoadData()
        txtCatName.Clear()
        selectedId = 0
        dgvCategories.ClearSelection()
        txtCatName.Focus()
    End Sub

    Private Sub LoadData()
        Try
            Dim dt = ExecuteQuery("SELECT category_id, category_name FROM categories ORDER BY category_id ASC")
            dgvCategories.DataSource = dt
        Catch
        End Try
    End Sub

    Private Sub btnBack_Click() Handles btnBack.Click
        Dim dashboardform As New DashboardForm()
        dashboardform.Show()
        Me.Close()
    End Sub

    Private Sub btnClear_Click() Handles btnClear.Click
        txtCatName.Clear()
        selectedId = 0
        dgvCategories.ClearSelection()
        txtCatName.Focus()
    End Sub

    Private Sub btnAdd_Click() Handles btnAdd.Click
        If Not ValidateCategoryName() Then Return

        Dim catName As String = txtCatName.Text.Trim()

        Dim count = Convert.ToInt32(ExecuteQuery(
            "SELECT COUNT(*) FROM categories WHERE LOWER(category_name) = LOWER(@n)",
            New List(Of MySqlParameter) From {New MySqlParameter("@n", catName)}
        ).Rows(0)(0))

        If count > 0 Then
            MessageBox.Show("Category with this name already exists!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCatName.SelectAll()
            txtCatName.Focus()
            Return
        End If

        ExecuteNonQuery("INSERT INTO categories (category_name) VALUES (@n)",
            New List(Of MySqlParameter) From {New MySqlParameter("@n", catName)})

        LoadData()
        btnClear_Click()
        MessageBox.Show("Category added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnUpdate_Click() Handles btnUpdate.Click
        If selectedId = 0 Then
            MessageBox.Show("Please select a category to update!", "Select Category", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not ValidateCategoryName() Then Return

        Dim catName As String = txtCatName.Text.Trim()

        Dim count = Convert.ToInt32(ExecuteQuery(
            "SELECT COUNT(*) FROM categories WHERE LOWER(category_name) = LOWER(@n) AND category_id <> @id",
            New List(Of MySqlParameter) From {
                New MySqlParameter("@n", catName),
                New MySqlParameter("@id", selectedId)
            }
        ).Rows(0)(0))

        If count > 0 Then
            MessageBox.Show("Another category with this name already exists!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ExecuteNonQuery("UPDATE categories SET category_name = @n WHERE category_id = @id",
            New List(Of MySqlParameter) From {
                New MySqlParameter("@n", catName),
                New MySqlParameter("@id", selectedId)
            })

        LoadData()
        MessageBox.Show("Category updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnDelete_Click() Handles btnDelete.Click
        If selectedId = 0 Then
            MessageBox.Show("Please select a category to delete!", "Select Category", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim catName = dgvCategories.SelectedRows(0).Cells("category_name").Value.ToString()

        If MessageBox.Show(
            $"Delete category '{catName}'?" & vbCrLf & vbCrLf &
            "All items in this category will also be deleted permanently!" & vbCrLf &
            "This action cannot be undone.",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then

            ExecuteNonQuery("DELETE FROM items WHERE category_id = @id",
                New List(Of MySqlParameter) From {New MySqlParameter("@id", selectedId)})

            ExecuteNonQuery("DELETE FROM categories WHERE category_id = @id",
                New List(Of MySqlParameter) From {New MySqlParameter("@id", selectedId)})

            LoadData()
            btnClear_Click()
            MessageBox.Show("Category and all related items deleted!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub dgvCategories_SelectionChanged() Handles dgvCategories.SelectionChanged
        If dgvCategories.SelectedRows.Count > 0 Then
            selectedId = CInt(dgvCategories.SelectedRows(0).Cells("category_id").Value)
            txtCatName.Text = dgvCategories.SelectedRows(0).Cells("category_name").Value.ToString()
        Else
            selectedId = 0
            txtCatName.Clear()
        End If
    End Sub

    Private Function ValidateCategoryName() As Boolean
        Dim name As String = txtCatName.Text.Trim()
        If String.IsNullOrWhiteSpace(name) Then
            MessageBox.Show("Please enter a category name!", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCatName.Focus()
            Return False
        End If
        If name.Length < 2 Then
            MessageBox.Show("Category name must be at least 2 characters!", "Too Short", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCatName.Focus()
            Return False
        End If
        Return True
    End Function

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

    Private Sub dgvCategories_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCategories.CellContentClick

    End Sub
End Class