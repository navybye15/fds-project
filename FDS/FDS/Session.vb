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

        Form5.usernametxt.Text = ""
        Form5.passwordtxt.Text = ""
        Form5.Show()

        Dim allForms = Application.OpenForms.Cast(Of Form)().ToList()
        For Each f In allForms
            If f IsNot Form5 Then f.Close()
        Next
    End Sub
End Module
