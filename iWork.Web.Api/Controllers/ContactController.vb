Imports System.Net
Imports System.Web.Http

Namespace Controllers

    Public Class ContactController
        Implements IContactController

        Public Function add(requestModel As contact) As addResult Implements IContactController.add
            Dim x = requestModel
        End Function

        Public Function update(requestModel As contact) As updateResult Implements IContactController.update
            Dim y = requestModel
        End Function

    End Class


End Namespace