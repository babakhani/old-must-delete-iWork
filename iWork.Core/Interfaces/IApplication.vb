Imports iWork.Core.Service
Imports iWork.Core.Repository

Public Interface IApplication

    Function GetService(Of T)() As T

End Interface