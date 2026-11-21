Imports MySql.Data.MySqlClient

Module DBConnection

    '---------------------------------------------
    ' 1. MAIN CONNECTION STRING
    '---------------------------------------------
    Public ConnString As String = "Server=localhost;Database=grocery_db;Uid=root;Pwd=;"
    Public Conn As New MySqlConnection(ConnString)

    '---------------------------------------------
    ' 2. OPEN CONNECTION
    '---------------------------------------------
    Public Sub OpenConnection()
        Try
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
        Catch ex As Exception
            MessageBox.Show("Database Connection Error! " & ex.Message, "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '---------------------------------------------
    ' 3. CLOSE CONNECTION
    '---------------------------------------------
    Public Sub CloseConnection()
        Try
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to close database connection! " & ex.Message)
        End Try
    End Sub

    '---------------------------------------------
    ' 4. EXECUTE INSERT / UPDATE / DELETE
    '---------------------------------------------
    Public Function ExecuteNonQuery(ByVal query As String, Optional ByVal params As List(Of MySqlParameter) = Nothing) As Boolean
        Try
            OpenConnection()
            Using cmd As New MySqlCommand(query, Conn)

                If params IsNot Nothing Then
                    cmd.Parameters.AddRange(params.ToArray())
                End If

                cmd.ExecuteNonQuery()
            End Using

            CloseConnection()
            Return True

        Catch ex As Exception
            MessageBox.Show("Database Operation Failed: " & ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    '---------------------------------------------
    ' 5. EXECUTE SELECT → RETURNS DATATABLE
    '---------------------------------------------
    Public Function ExecuteQuery(ByVal query As String, Optional ByVal params As List(Of MySqlParameter) = Nothing) As DataTable
        Dim dt As New DataTable

        Try
            OpenConnection()
            Using cmd As New MySqlCommand(query, Conn)

                If params IsNot Nothing Then
                    cmd.Parameters.AddRange(params.ToArray())
                End If

                Using da As New MySqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error Loading Data: " & ex.Message)
        Finally
            CloseConnection()
        End Try

        Return dt
    End Function

    '---------------------------------------------
    ' 6. SIMPLE VALIDATION HELPER
    '---------------------------------------------
    Public Function IsEmpty(txt As TextBox, fieldName As String) As Boolean
        If txt.Text.Trim = "" Then
            MessageBox.Show(fieldName & " cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txt.Focus()
            Return True
        End If
        Return False
    End Function

End Module
