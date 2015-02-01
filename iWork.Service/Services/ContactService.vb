Imports iWork.Core
Imports iWork.Entities
Imports iWork.Repository

Public Class ContactService
    Implements IContactService

    Public Sub Add(value As Contact) Implements IContactService.Add

        Using uow = Helper.GetUOW

            Dim rep = Helper.GetEntityRepository(Of Contact)()
            rep.Add(value)
            uow.Commit()

        End Using

    End Sub

    Public Sub Update(value As Contact) Implements IContactService.Update

        Using uow = Helper.GetUOW

            Dim rep = Helper.GetEntityRepository(Of Contact)()
            rep.Update(value)
            uow.Commit()

        End Using

    End Sub

    Public Sub Remove(value As Long) Implements IContactService.Remove

        Using uow = Helper.GetUOW

            Dim rep = Helper.GetEntityRepository(Of Contact)()
            rep.Remove(value)
            uow.Commit()

        End Using

    End Sub

    Public Function Search(term As String) As IEnumerable(Of Contact) Implements IContactService.Search
        Return Helper.GetRepository(Of IContactRepository).Search(term)
    End Function

    Public Function GetById(contactId As Integer) As Contact Implements IContactService.GetById

        Return Helper.GetEntityRepository(Of Contact).GetById(contactId)

    End Function


End Class
