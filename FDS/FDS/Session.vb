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

        currentForm.Close()

    End Sub
End Module
