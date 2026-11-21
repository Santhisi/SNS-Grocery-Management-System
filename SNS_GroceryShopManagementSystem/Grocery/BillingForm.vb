Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Public Class btnRemoveItem
    Inherits Form

    Private cart As New DataTable()
    Private totalAmount As Decimal = 0

    Private Sub BillingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === ROUNDED CORNERS ===
        SetRoundedRegion(Me, 30)
        SetRoundedRegion(pnlMain, 24)
        SetRoundedRegion(txtSearch, 10)
        SetRoundedRegion(numQty, 10)
        SetRoundedRegion(dgvItems, 16)
        SetRoundedRegion(dgvCart, 16)
        SetRoundedRegion(btnBack, 12)
        SetRoundedRegion(btnClearCart, 12)
        SetRoundedRegion(btnPrint, 12)
        SetRoundedRegion(Button1, 12)

        ' === PADDING FOR SEARCH TEXTBOX (INNER SPACING) ===
        txtSearch.Padding = New Padding(12)          ' ← YOU ASKED FOR THIS

        ' === REMOVE BORDERS FROM ALL BUTTONS (CLEAN FLAT LOOK) ===
        btnBack.FlatAppearance.BorderSize = 0
        btnClearCart.FlatAppearance.BorderSize = 0
        btnPrint.FlatAppearance.BorderSize = 0       ' ← ALL 3 BUTTONS NOW BORDERLESS
        Button1.FlatAppearance.BorderSize = 0

        ' === SEARCH TEXTBOX PLACEHOLDER ===
        txtSearch.Text = "Search items..."
        txtSearch.ForeColor = Color.Gray

        ' === DATAGRIDVIEWS STYLING ===
        StyleDataGridView(dgvItems)
        StyleDataGridView(dgvCart)

        SetupCart()
        dgvCart.ClearSelection()
        LoadItems()
    End Sub

    Private Sub StyleDataGridView(dgv As DataGridView)
        With dgv
            .EnableHeadersVisualStyles = False
            .ColumnHeadersHeight = 40
            .RowTemplate.Height = 36
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 110, 60)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            .BackgroundColor = Color.White
            .GridColor = Color.FromArgb(240, 240, 240)
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 180, 100)
            .DefaultCellStyle.SelectionForeColor = Color.White
        End With
    End Sub

    Private Sub SetupCart()
        cart.Columns.Add("item_id", GetType(Integer))
        cart.Columns.Add("item_name", GetType(String))
        cart.Columns.Add("qty", GetType(Integer))
        cart.Columns.Add("price", GetType(Decimal))
        cart.Columns.Add("total", GetType(Decimal))
        dgvCart.DataSource = cart
    End Sub

    Private Sub LoadItems()
        dgvItems.DataSource = ExecuteQuery("SELECT item_id, item_name, price, quantity FROM items WHERE quantity > 0 ORDER BY item_id ASC")
    End Sub

    Private Sub SearchItems() Handles txtSearch.TextChanged
        Dim s As String = txtSearch.Text.Trim()
        If s = "" Or s = "Search items..." Then
            LoadItems()
            Return
        End If
        Dim sql = "SELECT item_id, item_name, price, quantity FROM items WHERE item_name LIKE @s AND quantity > 0"
        dgvItems.DataSource = ExecuteQuery(sql, New List(Of MySqlParameter) From {New MySqlParameter("@s", "%" & s & "%")})
    End Sub

    Private Sub txtSearch_GotFocus() Handles txtSearch.GotFocus
        If txtSearch.Text = "Search items..." Then
            txtSearch.Text = ""
            txtSearch.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtSearch_LostFocus() Handles txtSearch.LostFocus
        If String.IsNullOrWhiteSpace(txtSearch.Text) Then
            txtSearch.Text = "Search items..."
            txtSearch.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub AddToCart() Handles dgvItems.DoubleClick
        If dgvItems.SelectedRows.Count = 0 Then Return
        Dim r = dgvItems.SelectedRows(0)
        Dim id As Integer = CInt(r.Cells("item_id").Value)
        Dim name As String = r.Cells("item_name").Value.ToString()
        Dim price As Decimal = CDec(r.Cells("price").Value)
        Dim stock As Integer = CInt(r.Cells("quantity").Value)
        Dim qty As Integer = CInt(numQty.Value)

        If qty > stock Then
            MessageBox.Show("Not enough stock!", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim existing = cart.Select($"item_id = {id}")
        If existing.Length > 0 Then
            Dim newQty = existing(0)("qty") + qty
            If newQty > stock Then
                MessageBox.Show("Not enough stock!", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            existing(0)("qty") = newQty
            existing(0)("total") = newQty * price
        Else
            cart.Rows.Add(id, name, qty, price, qty * price)
        End If

        UpdateTotal()
    End Sub

    Private Sub UpdateTotal()
        totalAmount = If(cart.Compute("SUM(total)", Nothing) Is DBNull.Value, 0D, CDec(cart.Compute("SUM(total)", Nothing)))
        lblTotal.Text = "Total: Rs. " & totalAmount.ToString("N2")
    End Sub

    Private Sub btnClearCart_Click() Handles btnClearCart.Click
        If cart.Rows.Count = 0 Then Return
        If MessageBox.Show("Clear entire cart?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            cart.Clear()
            UpdateTotal()
        End If
    End Sub

    Private Sub btnPrint_Click() Handles btnPrint.Click
        If cart.Rows.Count = 0 Then
            MessageBox.Show("Cart is empty!", "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            OpenConnection()
            Using trans = Conn.BeginTransaction()
                Dim billCmd As New MySqlCommand("INSERT INTO bills (total_amount, bill_date) VALUES (@t, NOW()); SELECT LAST_INSERT_ID();", Conn, trans)
                billCmd.Parameters.AddWithValue("@t", totalAmount)
                Dim billId = Convert.ToInt32(billCmd.ExecuteScalar())

                For Each row As DataRow In cart.Rows
                    Dim cmd As New MySqlCommand("INSERT INTO bill_items (bill_id, item_id, quantity, price) VALUES (@b, @i, @q, @p)", Conn, trans)
                    cmd.Parameters.AddWithValue("@b", billId)
                    cmd.Parameters.AddWithValue("@i", row("item_id"))
                    cmd.Parameters.AddWithValue("@q", row("qty"))
                    cmd.Parameters.AddWithValue("@p", row("price"))
                    cmd.ExecuteNonQuery()

                    Dim stockCmd As New MySqlCommand("UPDATE items SET quantity = quantity - @q WHERE item_id = @i", Conn, trans)
                    stockCmd.Parameters.AddWithValue("@q", row("qty"))
                    stockCmd.Parameters.AddWithValue("@i", row("item_id"))
                    stockCmd.ExecuteNonQuery()
                Next

                trans.Commit()
            End Using

            MessageBox.Show("Bill saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Dim preview As New ReceiptPreviewForm(cart, totalAmount)
            preview.ShowDialog()

            cart.Clear()
            UpdateTotal()
            LoadItems()
            txtSearch.Text = "Search items..."
            txtSearch.ForeColor = Color.Gray
            numQty.Value = 1

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub btnBack_Click() Handles btnBack.Click
        Dim dashboardform As New DashboardForm()
        dashboardform.Show()
        Me.Close()
    End Sub

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dashboardform As New DashboardForm()
        dashboardform.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If dgvCart.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a row to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedRow As DataGridViewRow = dgvCart.SelectedRows(0)
        Dim dataRow As DataRowView = TryCast(selectedRow.DataBoundItem, DataRowView)

        If dataRow IsNot Nothing Then
            dataRow.Row.Delete()
            UpdateTotal()
        End If

        dgvCart.ClearSelection()
    End Sub

    Private Sub dgvCart_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvCart.DataBindingComplete
        dgvCart.ClearSelection()
    End Sub

    Private Sub pnlMain_Paint(sender As Object, e As PaintEventArgs) Handles pnlMain.Paint

    End Sub
End Class