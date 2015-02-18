Imports Microsoft.Owin
Imports Owin
Imports System.Web.Hosting
Imports iView.ViewEngine
Imports iWork.Core

<Assembly: OwinStartup("iViewStartup", GetType(Startup))> 
Public Class Startup

    Public Sub Configuration(app As IAppBuilder)
        app.iViewConfig()
        app.CoreConfig()
    End Sub

End Class