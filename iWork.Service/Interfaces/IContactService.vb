Imports iWork.Core
Imports iWork.Entities

Public Interface IContactService
    Inherits IService

    Sub Add(value As Contact)
    Sub Update(value As Contact)
    Sub Remove(value As Long)
    Function Search(term As String) As IEnumerable(Of Contact)
    Function GetById(contactId As Integer) As Contact

End Interface
