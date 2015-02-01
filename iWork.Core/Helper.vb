Public Class Helper

    Public Shared Sub CreateMap(Of T, K)()
        AutoMapper.Mapper.CreateMap(Of T, K)()
    End Sub

    Public Shared Function Map(Of T)(value As Object) As T
        Return AutoMapper.Mapper.Map(Of T)(value)
    End Function

    Public Shared Function GetService(Of T As IService)() As T
        Return Application.GetService(Of T)()
    End Function

    Public Shared Function GetUOW() As IUnitOfWork
        Return Application.GetService(Of IUnitOfWork)()
    End Function

    Public Shared Function GetEntityRepository(Of T As Class)() As IGenericRepository(Of T)
        Return Application.GetService(Of IGenericRepository(Of T))()
    End Function

    Public Shared Function GetRepository(Of T As IRepository)() As T
        Return Application.GetService(Of T)()
    End Function

End Class
