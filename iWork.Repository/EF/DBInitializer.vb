Imports System.Data.Entity
Imports iWork.Entities

Namespace EF
    Public Class DBInitializer
        Inherits CreateDatabaseIfNotExists(Of iWorkContext)

        Protected Overrides Sub Seed(context As iWorkContext)
            InitContacts(context)
        End Sub

        Private Sub InitContacts(context As iWorkContext)
            context.Set(Of Contact).Add(New Contact With {.Fullname = "Microsoft"})
            context.Set(Of Contact).Add(New Contact With {.Fullname = "Google"})
            context.Set(Of Contact).Add(New Contact With {.Fullname = "Apple4"})
            context.Set(Of Contact).Add(New Contact With {.Fullname = "Apple2"})
            context.Set(Of Contact).Add(New Contact With {.Fullname = "Apple1"})
            context.SaveChanges()
        End Sub

    End Class

End Namespace
