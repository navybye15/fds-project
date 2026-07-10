Module Session
    Public CurrentUserID As Integer
    Public CurrentTenantID As Integer
    Public CurrentTenantName As String
    Public CurrentRole As String

    Public Sub SignOut(currentForm As Form)
        ' i-reset lahat ng session values
        CurrentUserID = 0
        CurrentTenantID = 0
        CurrentTenantName = Nothing
        CurrentRole = Nothing

        ' i-redirect sa login form
        Dim loginForm As New Form5()
        loginForm.Show()

        ' isara lahat ng bukas na forms maliban sa login form
        For Each f As Form In Application.OpenForms.Cast(Of Form)().ToList()
            If f IsNot loginForm Then f.Close()
        Next
    End Sub
End Module
