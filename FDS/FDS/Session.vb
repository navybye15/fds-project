Module Session
    Public CurrentUserID As Integer
    Public CurrentTenantID As Integer
    Public CurrentTenantName As String
    Public CurrentRole As String


    Public Sub SignOut(currentForm As Form)

        CurrentUserID = 0
        CurrentTenantID = 0
        CurrentTenantName = Nothing
        CurrentRole = Nothing


        Dim loginForm As New Form5()
        loginForm.Show()


        For Each f As Form In Application.OpenForms.Cast(Of Form)().ToList()
            If f IsNot loginForm Then f.Close()
        Next
    End Sub
End Module
