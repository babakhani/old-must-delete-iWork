Imports iWork.Repository

Public Class ContactService

    Public Sub Add(value As Contact)

        Using uow = RepositoryFactory.GetInstance(Of IUnitOfWork)()

            Dim rep = RepositoryFactory.GetInstance(Of IGenericRepository(Of Contact))()
            rep.Add(value)
            uow.Commit()

        End Using

    End Sub

    Public Sub Update(value As Contact)

        Using uow = RepositoryFactory.GetInstance(Of IUnitOfWork)()

            Dim rep = RepositoryFactory.GetInstance(Of IGenericRepository(Of Contact))()
            rep.Update(value)
            uow.Commit()

        End Using

    End Sub

    Public Sub Remove(value As Long)

        Using uow = RepositoryFactory.GetInstance(Of IUnitOfWork)()

            Dim rep = RepositoryFactory.GetInstance(Of IGenericRepository(Of Contact))()
            rep.Remove(value)
            uow.Commit()

        End Using

    End Sub

End Class
