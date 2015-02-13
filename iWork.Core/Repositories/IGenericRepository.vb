Namespace Repositories

    Public Interface IGenericRepository(Of TEntity As Class, TKey)
        Inherits IRepository

        Function GetAll() As IQueryable(Of TEntity)
        Function GetById(id As TKey) As TEntity
        Sub Add(entity As TEntity)
        Sub Update(entity As TEntity)
        Sub Remove(id As TKey)
        Sub Remove(entity As TEntity)

    End Interface

    Public Interface IGenericRepository(Of TEntity As Class)
        Inherits IGenericRepository(Of TEntity, Long)

    End Interface

End Namespace