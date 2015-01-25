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

            Try

                Dim contact = Mapper.Map(Of Contact)(requestModel)
                Dim srv = ServiceFactory.GetInstance(Of ContactService)()
                srv.Add(contact)

                Return ivAddResult.SendOK("Contact has been added successfully.", contact.ContactId)

            Catch ex As Exception

                Return ivAddResult.SendError(ex.ToString, ex.StackTrace)

            End Try
            

        End Function

        Public Function Remove(requestModel As ivContactRemove) As ivRemoveResult Implements IContactController.Remove

            Dim srv = ServiceFactory.GetInstance(Of ContactService)()

        End Function

        Public Function Update(requestModel As ivContact) As ivUpdateResult Implements IContactController.Update

            Dim contact = Mapper.Map(Of Contact)(requestModel)
            Dim srv = ServiceFactory.GetInstance(Of ContactService)()
            srv.Add(contact)

            Dim out As New ivAddResult
            out.Data = contact.ContactId
            out.HasError = False
            out.Message = contact.ContactId.ToString

        End Function

    End Class


End Namespace

