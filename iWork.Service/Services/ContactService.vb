Imports iWork.Entities
Imports iWork.Repository
Imports iWork.Core
Imports iWork.Core.Repositories

Public Class ContactService
    Implements IContactService

    Public Sub Add(value As Contact) Implements IContactService.Add

        Using uow = Application.GetService(Of IUnitOfWork)()

            Dim rep = Application.GetService(Of IGenericRepository(Of Contact))()
            rep.Add(value)
            uow.Commit()

        End Using

    End Sub

    Public Sub Update(value As Contact) Implements IContactService.Update

        Using uow = Application.GetService(Of IUnitOfWork)()

            Dim rep = Application.GetService(Of IGenericRepository(Of Contact))()
            rep.Update(value)
            uow.Commit()

        End Using

    End Sub

    Public Sub Remove(value As Long) Implements IContactService.Remove

        Using uow = Application.GetService(Of IUnitOfWork)()

            Dim rep = Application.GetService(Of IGenericRepository(Of Contact))()
            rep.Remove(value)
            uow.Commit()

        End Using

    End Sub

    Public Function Search(term As String) As IEnumerable(Of Contact) Implements IContactService.Search

        Return Application.GetService(Of IContactRepository).Search(term)

    End Function

    Public Function GetById(contactId As Integer) As Contact Implements IContactService.GetById

        Return Application.GetService(Of IGenericRepository(Of Contact)).GetById(contactId)

    End Function


End Class
