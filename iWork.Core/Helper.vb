Public Class Helper

    Public Shared Sub CreateMap(Of T, K)()
        AutoMapper.Mapper.CreateMap(Of T, K)()
    End Sub

    Public Shared Function Map(Of T)(value As Object) As T
        Return AutoMapper.Mapper.Map(Of T)(value)
    End Function

    Public Shared Function GetService(Of T As IService)() As T
        Return IoC.GetService(Of T)()
    End Function

    Public Shared Function GetUOW() As IUnitOfWork
        Return IoC.GetUnitOfWork(Of IUnitOfWork)()
    End Function

    Public Shared Function GetRepository(Of T As Class)() As IGenericRepository(Of T)
        Return IoC.GetRepository(Of IGenericRepository(Of T))()
    End Function

End Class
