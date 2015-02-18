'Imports iWork.Core.Repositories

'Namespace Services

'    Public Class AccountService
'        Implements IAccountService

'        Public Function Register(user As User) As UserRegistratioinStatuses Implements IAccountService.Register

'            Try

'                Using uow = Application.GetService(Of IUnitOfWork)()

'                    Dim repository = Application.GetService(Of IAccountRepository)()

'                    If repository.GetAll.Where(Function(x) x.UserName.ToLower = user.UserName.ToLower).SingleOrDefault IsNot Nothing Then
'                        Return UserRegistratioinStatuses.UserExists
'                    End If

'                    uow.Commit()
'                End Using

'                Return UserRegistratioinStatuses.OK

'            Catch ex As Exception

'                Return UserRegistratioinStatuses.UnknownException

'            End Try

'        End Function

'        Public Function Find(username As String, password As String) As User Implements IAccountService.Find

'            Using uow = Application.GetService(Of IUnitOfWork)()

'                Dim repository = Application.GetService(Of IAccountRepository)()
'                Return repository.GetAll.Where(Function(x) x.UserName.ToLower = username.ToLower).SingleOrDefault

'                uow.Commit()

'            End Using

'        End Function

'    End Class

'    Public Enum UserRegistratioinStatuses
'        OK = 0
'        UserExists = 1
'        UnknownException = 2
'    End Enum

'End Namespace

