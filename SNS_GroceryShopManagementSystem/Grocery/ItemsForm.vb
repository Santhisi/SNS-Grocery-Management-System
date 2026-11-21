Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Public Class ItemsForm
    Inherits Form

    Private selectedItemId As Integer = 0

    Private Sub ItemsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === ROUNDED CORNERS ===
        Try
            SetRoundedRegion(Me, 30)
            SetRoundedRegion(pnlMain, 24)
            SetRoundedRegion(txtItemName, 10)
            SetRoundedRegion(cmbCategory, 10)
            SetRoundedRegion(numPrice, 10)
            SetRoundedRegion(numQuantity, 10)
            SetRoundedRegion(btnAdd, 10)
            SetRoundedRegion(btnUpdate, 10)
            SetRoundedRegion(btnDelete, 10)
            SetRoundedRegion(btnBack, 12)
            SetRoundedRegion(btnClear, 12)
            SetRoundedRegion(dgvItems, 16)
        Catch
            ' Ignore if sizes not yet initialized in designer run-time
        End Try

        ' === PADDING FOR TEXTBOX (INNER SPACING) ===
        txtItemName.Padding = New Padding(10)

        ' === REMOVE BORDER FROM ALL BUTTONS (CLEAN FLAT LOOK) ===
        btnBack.FlatAppearance.BorderSize = 0
        btnClear.FlatAppearance.BorderSize = 0
        btnAdd.FlatAppearance.BorderSize = 0
        btnUpdate.FlatAppearance.BorderSize = 0
        btnDelete.FlatAppearance.BorderSize = 0

        ' === DATAGRIDVIEW STYLING ===
        With dgvItems
            .EnableHeadersVisualStyles = False
            .ColumnHeadersHeight = 42
            .RowTemplate.Height = 32
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 110, 60)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            .GridColor = Color.FromArgb(240, 240, 240)
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 180, 100)
            .DefaultCellStyle.SelectionForeColor = Color.White
            .AllowUserToAddRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ClearSelection()
        End With

        ' In ItemsForm_Load or Designer properties
        numPrice.Minimum = 0.00D
        numPrice.Maximum = 9999999.99D   ' ← VERY IMPORTANT!
        numPrice.DecimalPlaces = 2
        numPrice.Increment = 5
        numPrice.ThousandsSeparator = True

        numQuantity.Minimum = 1
        numQuantity.Maximum = 999999      ' ← Safe high limit

        LoadCategories()
        LoadItems()
        ClearInputs()
    End Sub

    Private Sub LoadCategories()
        Try
            Dim dt = ExecuteQuery("SELECT category_id, category_name FROM categories ORDER BY category_name ASC")
            cmbCategory.DataSource = dt
            cmbCategory.DisplayMember = "category_name"
            cmbCategory.ValueMember = "category_id"
            If dt.Rows.Count = 0 Then
                MessageBox.Show("No categories found! Please add categories first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading categories: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadItems()
        Try
            Dim sql As String = "SELECT i.item_id, i.item_name, c.category_name, i.price, i.quantity " &
                                "FROM items i JOIN categories c ON i.category_id = c.category_id ORDER BY i.item_id ASC"
            Dim dt As DataTable = ExecuteQuery(sql)
            dgvItems.DataSource = dt
            dgvItems.ClearSelection()
            selectedItemId = 0
        Catch ex As Exception
            MessageBox.Show("Error loading items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearInputs()
        ' Clear text input
        txtItemName.Clear()

        ' --- Reset Price safely ---
        Try
            numPrice.Value = numPrice.Minimum
        Catch
            numPrice.Value = numPrice.Minimum
        End Try

        ' --- Reset Quantity safely ---
        Try
            numQuantity.Value = numQuantity.Minimum
        Catch
            numQuantity.Value = numQuantity.Minimum
        End Try

        ' --- Reset Category ---
        If cmbCategory.Items.Count > 0 Then
            cmbCategory.SelectedIndex = 0
        End If

        ' --- Reset selected item ---
        selectedItemId = 0

        ' --- Clear DataGridView selection ---
        dgvItems.ClearSelection()

        ' Focus on item name
        txtItemName.Focus()
    End Sub


    Private Sub btnClear_Click() Handles btnClear.Click
        ClearInputs()
    End Sub

    Private Sub btnAdd_Click() Handles btnAdd.Click
        If Not ValidateInputs() Then Return

        Dim itemName As String = txtItemName.Text.Trim()

        ' Check duplicate (safe)
        Dim dtCount As DataTable = ExecuteQuery(
            "SELECT COUNT(*) as cnt FROM items WHERE LOWER(item_name) = LOWER(@n)",
            New List(Of MySqlParameter) From {
                New MySqlParameter("@n", itemName)
            }
        )

        Dim count As Integer = 0
        If dtCount IsNot Nothing AndAlso dtCount.Rows.Count > 0 Then
            Integer.TryParse(dtCount.Rows(0)(0).ToString(), count)
        End If

        If count > 0 Then
            MessageBox.Show("Item with this name already exists!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtItemName.SelectAll()
            txtItemName.Focus()
            Return
        End If

        Dim inserted As Boolean = ExecuteNonQuery("INSERT INTO items (item_name, category_id, price, quantity) VALUES (@n,@c,@p,@q)",
            New List(Of MySqlParameter) From {
                New MySqlParameter("@n", itemName),
                New MySqlParameter("@c", If(cmbCategory.SelectedValue, DBNull.Value)),
                New MySqlParameter("@p", numPrice.Value),
                New MySqlParameter("@q", Convert.ToInt32(numQuantity.Value))
            })

        If inserted Then
            LoadItems()
            ClearInputs()
            MessageBox.Show("Item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Failed to add item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnUpdate_Click() Handles btnUpdate.Click
        If selectedItemId = 0 Then
            MessageBox.Show("Please double-click an item to load it for update.", "Select Item", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not ValidateInputs() Then Return

        Dim itemName As String = txtItemName.Text.Trim()

        ' Check duplicates except this id
        Dim dtCount As DataTable = ExecuteQuery(
            "SELECT COUNT(*) as cnt FROM items WHERE LOWER(item_name)=LOWER(@n) AND item_id<>@id",
            New List(Of MySqlParameter) From {
                New MySqlParameter("@n", itemName),
                New MySqlParameter("@id", selectedItemId)
            }
        )

        Dim count As Integer = 0
        If dtCount IsNot Nothing AndAlso dtCount.Rows.Count > 0 Then
            Integer.TryParse(dtCount.Rows(0)(0).ToString(), count)
        End If

        If count > 0 Then
            MessageBox.Show("Another item with this name already exists!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtItemName.Focus()
            Return
        End If

        Dim updated As Boolean = ExecuteNonQuery(
            "UPDATE items SET item_name=@n, category_id=@c, price=@p, quantity=@q WHERE item_id=@id",
            New List(Of MySqlParameter) From {
                New MySqlParameter("@n", itemName),
                New MySqlParameter("@c", If(cmbCategory.SelectedValue, DBNull.Value)),
                New MySqlParameter("@p", numPrice.Value),
                New MySqlParameter("@q", Convert.ToInt32(numQuantity.Value)),
                New MySqlParameter("@id", selectedItemId)
            })

        If updated Then
            LoadItems()
            ClearInputs()
            MessageBox.Show("Item updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Failed to update item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnDelete_Click() Handles btnDelete.Click
        If selectedItemId = 0 Then
            MessageBox.Show("Please double-click an item to select it for deletion.", "Select Item", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Delete this item permanently?" & vbCrLf & "This cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Dim deleted As Boolean = ExecuteNonQuery("DELETE FROM items WHERE item_id = @id",
                New List(Of MySqlParameter) From {
                    New MySqlParameter("@id", selectedItemId)
                })

            If deleted Then
                LoadItems()
                ClearInputs()
                MessageBox.Show("Item deleted!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to delete item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    ' =========================
    ' Double-click handler (fills fields)
    ' =========================
    Private Sub dgvItems_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvItems.CellDoubleClick
        ' Prevent header click
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return

        Try
            Dim row As DataGridViewRow = dgvItems.Rows(e.RowIndex)

            ' --- Load item_id ---
            If Not Integer.TryParse(row.Cells("item_id").Value?.ToString(), selectedItemId) Then
                selectedItemId = 0
                Return
            End If

            ' --- Load item name ---
            txtItemName.Text = row.Cells("item_name").Value?.ToString()

            ' --- Load category name (display only) ---
            cmbCategory.Text = row.Cells("category_name").Value?.ToString()

            ' --- SAFELY LOAD PRICE (THIS IS THE KEY!) ---
            Dim price As Decimal = 0D
            If Decimal.TryParse(row.Cells("price").Value?.ToString(), price) Then
                ' Clamp to allowed range
                price = Math.Max(numPrice.Minimum, Math.Min(numPrice.Maximum, price))
            Else
                price = 1D  ' fallback
            End If
            numPrice.Value = price   ' ← NOW SHOWS REAL PRICE!

            ' --- SAFELY LOAD QUANTITY ---
            Dim qty As Integer = 1
            If Integer.TryParse(row.Cells("quantity").Value?.ToString(), qty) Then
                qty = Math.Max(CInt(numQuantity.Minimum), Math.Min(CInt(numQuantity.Maximum), qty))
            End If
            numQuantity.Value = qty   ' ← NOW SHOWS REAL STOCK!

            ' --- Highlight the selected row ---
            dgvItems.ClearSelection()
            row.Selected = True
            dgvItems.CurrentCell = row.Cells(1)  ' Focus on item name column

            ' Optional: Focus on item name for quick edit
            txtItemName.Focus()
            txtItemName.SelectAll()

        Catch ex As Exception
            MessageBox.Show("Error loading item: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Function ValidateInputs() As Boolean
        If String.IsNullOrWhiteSpace(txtItemName.Text) Then
            MessageBox.Show("Please enter item name!", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtItemName.Focus()
            Return False
        End If

        If cmbCategory.SelectedValue Is Nothing OrElse cmbCategory.Items.Count = 0 Then
            MessageBox.Show("Please select a category!", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If numPrice.Value <= 0 Then
            MessageBox.Show("Price must be greater than 0!", "Invalid Price", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            numPrice.Focus()
            Return False
        End If

        If numQuantity.Value < 1 Then
            MessageBox.Show("Stock quantity must be at least 1!", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            numQuantity.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub btnBack_Click() Handles btnBack.Click
        Try
            Dim dashboardform As New DashboardForm()
            dashboardform.Show()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error opening dashboard: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' === Helper: Rounded region (keeps original) ===
    Private Sub SetRoundedRegion(ctrl As Control, radius As Integer)
        If ctrl Is Nothing Then Return
        If ctrl.Width <= 0 OrElse ctrl.Height <= 0 Then
            ' Delay region set until control is measured - subscribe to resize
            AddHandler ctrl.Resize, Sub()
                                        Try
                                            Dim path As New GraphicsPath()
                                            Dim rect As New Rectangle(0, 0, ctrl.Width, ctrl.Height)
                                            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
                                            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90)
                                            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90)
                                            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90)
                                            path.CloseFigure()
                                            ctrl.Region = New Region(path)
                                        Catch
                                        End Try
                                    End Sub
            Return
        End If

        Dim p As New GraphicsPath()
        Dim r As New Rectangle(0, 0, ctrl.Width, ctrl.Height)
        p.AddArc(r.X, r.Y, radius, radius, 180, 90)
        p.AddArc(r.Width - radius, r.Y, radius, radius, 270, 90)
        p.AddArc(r.Width - radius, r.Height - radius, radius, radius, 0, 90)
        p.AddArc(r.X, r.Height - radius, radius, radius, 90, 90)
        p.CloseFigure()
        ctrl.Region = New Region(p)
    End Sub

End Class
