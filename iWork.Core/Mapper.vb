Public Class Mapper

    Public Shared Sub CreateMap(Of T, K)()
        AutoMapper.Mapper.CreateMap(Of T, K)()
    End Sub

    Public Shared Function Map(Of T)(value As Object) As T
        Return AutoMapper.Mapper.Map(Of T)(value)
    End Function

End Class