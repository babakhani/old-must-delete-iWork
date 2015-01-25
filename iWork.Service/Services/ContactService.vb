Imports iWork.Repository

Public Class ContactService

    Public Sub Add(value As Contact)

        Using uow = RepositoryFactory.GetInstance(Of IUnitOfWork)()

            Dim rep = RepositoryFactory.GetInstance(Of IGenericRepository(Of Contact))()
            rep.Add(value)
            uow.Commit()

        End Using

    End Sub

End Class
