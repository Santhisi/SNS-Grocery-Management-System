Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Text

Public Class ReceiptPreviewForm
    Inherits Form

    Private cart As DataTable
    Private total As Decimal
    Private receiptText As String = ""
    Private WithEvents printDoc As New PrintDocument()

    Public Sub New(c As DataTable, t As Decimal)
        cart = c.Copy()
        total = t
        GenerateReceiptText()
        BuildUI()
    End Sub

    Private Sub BuildUI()
        Me.Text = "Receipt - SnS Grocery"
        Me.Size = New Size(520, 780)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.White

        Dim txtReceipt As New TextBox With {
            .Multiline = True,
            .ReadOnly = True,
            .Font = New Font("Consolas", 10.5),
            .Text = receiptText,
            .Size = New Size(480, 600),
            .Location = New Point(20, 20),
            .BorderStyle = BorderStyle.FixedSingle,
            .BackColor = Color.FromArgb(252, 252, 252),
            .ScrollBars = ScrollBars.Vertical
        }
        SetRoundedRegion(txtReceipt, 12)

        Dim btnPrint As New Button With {
            .Text = "Print",
            .Size = New Size(220, 60),
            .Location = New Point(40, 650),
            .BackColor = Color.FromArgb(46, 139, 87),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 13, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat
        }
        btnPrint.FlatAppearance.BorderSize = 0
        SetRoundedRegion(btnPrint, 14)
        AddHandler btnPrint.Click, AddressOf StartPrint

        Dim btnClose As New Button With {
            .Text = "Close",
            .Size = New Size(220, 60),
            .Location = New Point(270, 650),
            .BackColor = Color.FromArgb(220, 70, 70),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 13, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat
        }
        btnClose.FlatAppearance.BorderSize = 0
        SetRoundedRegion(btnClose, 14)
        AddHandler btnClose.Click, Sub() Me.Close()

        Me.Controls.AddRange({txtReceipt, btnPrint, btnClose})
    End Sub

    Private Sub GenerateReceiptText()
        Dim sb As New StringBuilder()
        sb.AppendLine("     SnS GROCERY STORE     ")
        sb.AppendLine("   No.12, Main Road, Colombo   ")
        sb.AppendLine("     Tel: 011-2345678     ")
        sb.AppendLine("===============================")
        sb.AppendLine("Date: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
        sb.AppendLine("===============================")
        sb.AppendLine()
        sb.AppendLine(String.Format("{0,-20} {1,4} {2,10} {3,10}", "ITEM", "QTY", "PRICE", "TOTAL"))
        sb.AppendLine("------------------------------------------")
        For Each r As DataRow In cart.Rows
            Dim name = If(r("item_name").ToString().Length > 20, r("item_name").ToString().Substring(0, 17) & "...", r("item_name"))
            sb.AppendLine(String.Format("{0,-20} {1,4} {2,10:N2} {3,10:N2}", name, r("qty"), r("price"), r("total")))
        Next
        sb.AppendLine("------------------------------------------")
        sb.AppendLine()
        sb.AppendLine(String.Format("       GRAND TOTAL: Rs. {0,10:N2}", total))
        sb.AppendLine()
        sb.AppendLine("     Thank You! Come Again!     ")
        sb.AppendLine("     www.sns-grocery.com     ")
        receiptText = sb.ToString()
    End Sub

    Private Sub StartPrint()
        Dim pd As New PrintDialog()
        pd.Document = printDoc
        If pd.ShowDialog() = DialogResult.OK Then
            printDoc.Print()
        End If
    End Sub

    Private Sub printDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDoc.PrintPage
        Dim font As New Font("Consolas", 9)
        Dim y As Single = 20
        For Each line In receiptText.Split(vbLf)
            e.Graphics.DrawString(line.TrimEnd(vbCr), font, Brushes.Black, 10, y)
            y += 16
        Next
    End Sub

    Private Sub SetRoundedRegion(ctrl As Control, radius As Integer)
        Dim path As New GraphicsPath()
        Dim rct As New Rectangle(0, 0, ctrl.Width, ctrl.Height)
        path.AddArc(rct.X, rct.Y, radius, radius, 180, 90)
        path.AddArc(rct.Width - radius, rct.Y, radius, radius, 270, 90)
        path.AddArc(rct.Width - radius, rct.Height - radius, radius, radius, 0, 90)
        path.AddArc(rct.X, rct.Height - radius, radius, radius, 90, 90)
        path.CloseFigure()
        ctrl.Region = New Region(path)
    End Sub

    Private Sub ReceiptPreviewForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class