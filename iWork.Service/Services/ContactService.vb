Imports iWork.Repository

Public Class ContactService
    Implements IService

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

        Dim rep = Helper.GetRepository(Of Contact)()
        Dim out = From p In rep.GetAll Where p.Fullname.Contains(term) OrElse
                  p.Company.Contains(term) OrElse
                  p.Unit.Contains(term) OrElse
                  p.Tel1.Contains(term) OrElse
                  p.Tel2.Contains(term) OrElse
                  p.Tel3.Contains(term) OrElse
                  p.Tel4.Contains(term) OrElse
                  p.Description.Contains(term)


        Return out.ToList

    End Function

End Class
