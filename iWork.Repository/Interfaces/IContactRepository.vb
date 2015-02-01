Imports iWork.Entities

Public Interface IContactRepository
    Inherits iWork.Core.IGenericRepository(Of Contact)

    Function Search(term As String) As IEnumerable(Of Contact)

End Interface