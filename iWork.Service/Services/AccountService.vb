Imports iWork.Entities
Imports iWork.Repository
Imports iWork.Core
Imports iWork.Core.Repositories

Public Class AccountService
    Implements IAccountService

    Public Function Validate(username As String, password As String) As Boolean Implements IAccountService.Validate

        Using uow = Application.GetService(Of IUnitOfWork)()

            Dim repository = Application.GetService(Of IGenericRepository(Of User))()
            Dim user = repository.GetAll.Where(Function(x) x.Username = username AndAlso x.Password = password).FirstOrDefault
            If user Is Nothing Then
                Return False
            Else
                user.LastLoginDate = Now
                Return True
            End If

        End Using


    End Function


End Class
