'Imports Microsoft.AspNet.Identity.EntityFramework
'Imports System.Data.Entity

'Namespace Repositories.EF

'    Public Class AccountContext
'        Inherits DbContext

'        Public Property Users As IDbSet(Of User)
'        Public Property Roles As IDbSet(Of Role)

'        Public Sub New(nameOrConnectionString As String)
'            MyBase.New(nameOrConnectionString)
'            Database.SetInitializer(New AccountDBInitializer)
'        End Sub

'    End Class

'    Public Class AccountDBInitializer
'        Inherits CreateDatabaseIfNotExists(Of AccountContext)

'        Protected Overrides Sub Seed(context As AccountContext)
'            context.Users.Add(New User With {.UserName = "tajan", .Password = "123", .Email = "info@tajan.ir"})
'            context.Roles.Add(New Role With {.Name = "admins"})
'            context.SaveChanges()
'        End Sub

'    End Class

'End Namespace
