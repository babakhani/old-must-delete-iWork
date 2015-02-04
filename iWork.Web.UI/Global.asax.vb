Imports System.Web.SessionState
Imports System.Web.Http
Imports iWork.Core.Web
Imports iWork.Core

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
    End Sub

End Class