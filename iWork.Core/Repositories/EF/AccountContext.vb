Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Data.Entity

Namespace Repositories.EF

    Public Class AccountContext
        Inherits IdentityDbContext(Of User, Role, Integer, UserLogin, UserRole, UserClaim)

        Public Sub New(nameOrConnectionString As String)
            MyBase.New(nameOrConnectionString)
            MyBase.Configuration.LazyLoadingEnabled = False
            MyBase.Configuration.ProxyCreationEnabled = False
        End Sub

        Protected Overrides Sub OnModelCreating(modelBuilder As Entity.DbModelBuilder)

            MyBase.OnModelCreating(modelBuilder)

            modelBuilder.Entity(Of User)().ToTable("Users")
            modelBuilder.Entity(Of Role)().ToTable("Roles")
            modelBuilder.Entity(Of UserRole)().ToTable("UserRoles")
            modelBuilder.Entity(Of UserLogin)().ToTable("UserLogins")
            modelBuilder.Entity(Of UserClaim)().ToTable("UserClaims")

        End Sub

    End Class

End Namespace
