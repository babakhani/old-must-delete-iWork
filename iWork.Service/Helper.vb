Imports iWork.Repository


Public Class Helper

    Public Shared Function GetUOW() As IUnitOfWork
        Return RepositoryFactory.GetInstance(Of IUnitOfWork)()
    End Function

    Public Shared Function GetRepository(Of T As Class)() As IGenericRepository(Of T)
        Return RepositoryFactory.GetInstance(Of IGenericRepository(Of T))()
    End Function

End Class
