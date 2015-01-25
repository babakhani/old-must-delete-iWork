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


        Public Function Add(requestModel As ivContact) As ivAddResult Implements IContactController.Add

            Dim contact = Mapper.Map(Of Contact)(requestModel)
            Dim srv = ServiceFactory.GetInstance(Of ContactService)()
            srv.Add(contact)

            Dim out As New ivAddResult
            out.Data = contact.ContactId
            out.HasError = False
            out.Message = contact.ContactId.ToString

            Return out

        End Function

        Public Function Update(requestModel As ivContact1) As ivAddResult Implements IContactController.Update

        End Function

    End Class


End Namespace

