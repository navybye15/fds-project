Imports MySql.Data.MySqlClient

Public Class Form14
    Private Sub addUnitBtn_Click(sender As Object, e As EventArgs) Handles addUnitBtn.Click
        If String.IsNullOrWhiteSpace(unitNumberTxt.Text) Then
            MessageBox.Show("Please fill-up the unit number.")
            Return
        End If

        Dim connStr As String = "Server=localhost;Port=3306;Database=isarms_db;Uid=root;Pwd=;"
        Dim conn As New MySqlConnection(connStr)

        Try
            conn.Open()


            Dim cmdUnit As New MySqlCommand(
                "INSERT INTO units (unit_number, type, floor, monthly_rate, unit_status) VALUES (@unitNumber, @unitType, @unitFloor,@unitMonthly,'available')", conn)
            cmdUnit.Parameters.AddWithValue("@unitNumber", unitNumberTxt.Text)
            cmdUnit.Parameters.AddWithValue("@unitType", typeTxt.Text)
            cmdUnit.Parameters.AddWithValue("@unitFloor", floorTxt.Text)
            cmdUnit.Parameters.AddWithValue("@unitMonthly", monthlyTxt.Text)
            cmdUnit.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show("Unit created succesfully!")
            clearFields()
            Me.Close()

        Catch ex As MySqlException When ex.Number = 1062
            MessageBox.Show("That unit number already exists. Please use a different unit number.")
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub clearFields()
        unitNumberTxt.Text = ""
        typeTxt.Text = ""
        floorTxt.Text = ""
        monthlyTxt.Text = ""
    End Sub

    Private Sub CancelBtn_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        Me.Close()

    End Sub

    Private Sub Form14_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class