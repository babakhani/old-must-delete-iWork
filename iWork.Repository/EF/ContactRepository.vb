Imports iWork.Entities
Imports System.Data.Entity

Namespace EF

    Public Class ContactRepository
        Inherits GenericRepository(Of Contact)
        Implements IContactRepository

        Public Sub New(dbContext As DbContext)
            MyBase.New(dbContext)
        End Sub

        Public Function Search(term As String) As IEnumerable(Of Entities.Contact) Implements IContactRepository.Search

            Dim out = From p In Me.DbSet Where p.Fullname.Contains(term) OrElse
                      p.Company.Contains(term) OrElse
                      p.Unit.Contains(term) OrElse
                      p.Tel1.Contains(term) OrElse
                      p.Tel2.Contains(term) OrElse
                      p.Tel3.Contains(term) OrElse
                      p.Tel4.Contains(term) OrElse
                      p.Description.Contains(term)


            Return out.ToList

        End Function

    End Class

End Namespace

