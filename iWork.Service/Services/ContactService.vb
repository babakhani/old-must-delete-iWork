Imports iWork.Repository

Public Class ContactService


    Dim rep1

    Public Sub New()
        rep1 = RepositoryFactory.GetInstance(Of iWorkEntities)()
    End Sub


    Public Sub Add(value As Contact)

    

        Dim rep2 = RepositoryFactory.GetInstance(Of iWorkEntities)()
        Dim k = rep1 Is rep2

        Using uow = RepositoryFactory.GetInstance(Of IUnitOfWork)()

            Dim rep = RepositoryFactory.GetInstance(Of IGenericRepository(Of Contact))()
            rep.Add(value)
            uow.Commit()


        End Using

        Using uow = RepositoryFactory.GetInstance(Of IUnitOfWork)()

            Dim rep = RepositoryFactory.GetInstance(Of IGenericRepository(Of Contact))()
            rep.Add(value)
            uow.Commit()


        End Using

    End Sub

End Class

Public Class BaseService(Of T)

End Class
Public Interface IService

End Interface