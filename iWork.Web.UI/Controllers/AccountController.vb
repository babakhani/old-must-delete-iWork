'Imports iWork.Service
'Imports iWork.Core.Controllers
'Imports iWork.Core
'Imports iWork.Entities

'Namespace Controllers

'    Public Class AccountController
'        Implements IAccountController

'        Public Function Authenticate(requestModel As ivAccount) As ResponseModel Implements IAccountController.Authenticate

'            Dim isValid As Boolean = Application.GetService(Of IAccountService).Validate(requestModel.Username, requestModel.Password)

'            If isValid Then
'                Return ResponseModel.SendOK("Valid User!", Nothing)
'            Else
'                Return ResponseModel.SendError("Invalid User!", Nothing)
'            End If

'        End Function

'    End Class

'End Namespace

