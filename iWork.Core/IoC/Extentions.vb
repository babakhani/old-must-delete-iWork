Imports System.Runtime.CompilerServices
Imports iWork.Core.Controllers
Imports iWork.Core.Services
Imports iWork.Core.Repositories

Public Module Extentions

    <Extension>
    Public Function GetService(Of T As IService)(controller As IController) As T

        Return Application.GetService(Of T)()

    End Function

    <Extension>
    Public Function GetUOW(Of T As IUnitOfWork)(service As IService) As T

        Return Application.GetService(Of T)()

    End Function

    <Extension>
    Public Function GetRepository(Of T As IRepository)(service As IService) As T

        Return Application.GetService(Of T)()

    End Function

    <Extension>
    Public Function GetGenericRepository(Of T As Class)(service As IService) As IGenericRepository(Of T)

        Return Application.GetService(Of IGenericRepository(Of T))()

    End Function

End Module
