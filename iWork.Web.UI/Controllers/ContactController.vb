Imports iWork.Service
Imports iWork.Core.Controllers
Imports iWork.Core
Imports iWork.Entities

Namespace Controllers

    Public Class ContactController
        Implements IContactController


        Public Sub New()
            Mapper.CreateMap(Of ivContact, Contact)()
            Mapper.CreateMap(Of Contact, ivContact)()
        End Sub

        Public Function Search(requestModel As ivContactSearch) As ResponseModel Implements IContactController.Search

            Dim data = Application.GetService(Of IContactService).Search(requestModel.term)
            Return ResponseModel.SendOK(data.Count.ToString & " record(s) found!", data)

        End Function

        Public Function Remove(requestModel As RequestIdModel) As ResponseModel Implements IContactController.Remove

            Application.GetService(Of IContactService).Remove(requestModel.Id)
            Return ResponseModel.SendOK("Contact has been removed successfully.", requestModel.Id)

        End Function

        Public Function Update(requestModel As ivContact) As ResponseModel Implements IContactController.Update

            Dim contact = Mapper.Map(Of Contact)(requestModel)

            If contact.Id = 0 Then
                'add
                Application.GetService(Of IContactService).Add(contact)
                Return ResponseModel.SendOK("Contact has been added successfully.", contact.Id)

            Else
                'update
                Application.GetService(Of IContactService).Update(contact)
                Return ResponseModel.SendOK("Contact has been updated successfully.", contact.Id)
            End If

        End Function

        Public Function ById(requestModel As RequestIdModel) As ResponseModel Implements IContactController.ById

            Dim data = Application.GetService(Of IContactService).GetById(requestModel.Id)
            Return ResponseModel.SendOK("", Mapper.Map(Of ivContact)(data))

        End Function

        Public Function GetList(requestModel As ivContactList) As ResponseModel Implements IContactController.GetList
            Return Nothing
        End Function

    End Class

End Namespace

