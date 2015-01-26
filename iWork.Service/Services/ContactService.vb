Imports iWork.Repository

Public Class ContactService

    Public Sub Add(value As Contact)

        Using uow = Helper.GetUOW

            Dim rep = Helper.GetRepository(Of Contact)()
            rep.Add(value)
            uow.Commit()

        End Using

    End Sub

    Public Sub Update(value As Contact)

        Using uow = Helper.GetUOW

            Dim rep = Helper.GetRepository(Of Contact)()
            rep.Update(value)
            uow.Commit()

        End Using

    End Sub

    Public Sub Remove(value As Long)

        Using uow = Helper.GetUOW

            Dim rep = Helper.GetRepository(Of Contact)()
            rep.Remove(value)
            uow.Commit()

        End Using

    End Sub

    Public Function Search(term As String) As IEnumerable(Of Contact)

        Dim out

        Using uow = Helper.GetUOW

            Dim rep = Helper.GetRepository(Of Contact)()
            out = rep.GetAll.Where(Function(x) x.Fullname.Contains(term)).ToList()

        End Using

        Return out

    End Function

End Class
