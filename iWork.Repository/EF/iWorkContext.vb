Imports System.Data.Entity
Imports iWork.Entities

Namespace EF

    Public Class iWorkContext
        Inherits DbContext

        Public Property Contacts As IDbSet(Of Contact)

        Public Sub New(nameOrConnectionString As String)
            MyBase.New(nameOrConnectionString)
            Database.SetInitializer(New DBInitializer)
        End Sub

    End Class

End Namespace

