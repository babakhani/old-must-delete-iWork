Imports System.Net
Imports System.Web.Http
Imports iWork.Repository
Imports iWork.Service

Namespace Controllers

    Public Class ContactController
        Implements IContactController

        Public Sub New()
            Mapper.CreateMap(Of ivContact, Contact)()
        End Sub

        Public Function Add(requestModel As ivContact) As ivGeneralResult Implements IContactController.Add

            Dim contact = Mapper.Map(Of Contact)(requestModel)
            Helper.GetService(Of ContactService).Add(contact)

            Return ivGeneralResult.SendOK("Contact has been added successfully.", contact.ContactId)

        End Function

        Public Function Search(requestModel As ivContactSearch) As ivGeneralResult Implements IContactController.Search

            Dim data = Helper.GetService(Of ContactService).Search(requestModel.term)
            Dim out = ivGeneralResult.SendOK("", data)
            Return out

        End Function

        Public Function Remove(requestModel As ivContactRemove) As ivGeneralResult Implements IContactController.Remove

            Helper.GetService(Of ContactService).Remove(requestModel.ContactId)
            Return ivGeneralResult.SendOK("Contact has been removed successfully.", requestModel.ContactId)

        End Function

        Public Function Update(requestModel As ivContact) As ivGeneralResult Implements IContactController.Update

            Dim contact = Mapper.Map(Of Contact)(requestModel)
            Helper.GetService(Of ContactService).Update(contact)

            Return ivGeneralResult.SendOK("Contact has been updated successfully.", contact.ContactId)

        End Function

    End Class

End Namespace

