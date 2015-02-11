Imports iWork.Entities
Imports iWork.Core
Imports iWork.Core.Web
Imports iWork.Core.Web.Controllers
Imports iWork.Service

Namespace Controllers

    Public Class ContactController
        Implements IContactController

        Public Sub New()
            Mapper.CreateMap(Of ivContact, Contact)()
        End Sub

        Public Function Add(requestModel As ivContact) As ResponseModel Implements IContactController.Add

            Dim contact = Mapper.Map(Of Contact)(requestModel)
            Application.GetService(Of IContactService).Add(contact)

            Return ResponseModel.SendOK("Contact has been added successfully.", contact.ContactId)

        End Function

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
            Application.GetService(Of IContactService).Update(contact)

            Return ResponseModel.SendOK("Contact has been updated successfully.", contact.ContactId)

        End Function

        Public Function GetById(requestModel As RequestIdModel) As ResponseModel Implements IContactController.GetById

            Dim data = Application.GetService(Of IContactService).GetById(requestModel.Id)
            Return ResponseModel.SendOK("", data)

        End Function

    End Class

End Namespace

