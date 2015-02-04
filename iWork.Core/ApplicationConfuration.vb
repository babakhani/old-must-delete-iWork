Imports iWork.Core.Service
Imports iWork.Core.Repository
Imports iWork.Core

Public Class ApplicationConfuration

    Private Shared scope As IScope
    Public Shared Sub Install(action As Func(Of IScope))
        installAction = action
    End Sub

    Private Shared installAction As Func(Of IScope)

    Public Shared Function GetApplication() As IApplication
        Return installAction.Invoke.GetScopeService(Of IApplication)()
    End Function

End Class


Public Interface IScope

    Function GetScopeService(Of T)() As T

End Interface