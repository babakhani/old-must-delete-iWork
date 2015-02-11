Imports iWork.Entities
Imports iWork.Core.Repositories

Public Interface IContactRepository
    Inherits IGenericRepository(Of Contact)

    Function Search(term As String) As IEnumerable(Of Contact)

End Interface