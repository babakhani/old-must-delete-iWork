Imports System.Data.Entity
Imports iWork.Core
Imports iWork.Core.Repository

Namespace Repositories.EF

    Public Class UnitOfWork
        Implements IUnitOfWork

        Protected DbContext As DbContext

        Public Sub New(_dbContext As DbContext)
            DbContext = _dbContext
        End Sub

        Public Sub Commit() Implements IUnitOfWork.Commit
            DbContext.SaveChanges()
        End Sub

        Private disposedValue As Boolean

        Protected Overridable Sub Dispose(disposing As Boolean)

            If Not Me.disposedValue Then
                If disposing Then
                    If DbContext IsNot Nothing Then
                        'todo: think
                        'DbContext.Dispose()
                        'DbContext = Nothing
                    End If
                End If

            End If
            Me.disposedValue = True

        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub


    End Class

End Namespace
