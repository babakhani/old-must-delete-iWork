
Namespace Controllers	

	Public Interface IContactController
				Function add(requestModel as contact) as addResult
				Function update(requestModel as contact) as updateResult
			
	End Interface

	Public Class contact
		Inherits RequestModel
							Public Property id as integer
				Public Property fullname as string
				Public Property tel1 as string
				Public Property tel2 as string
				Public Property tel3 as string
				Public Property tel4 as string
				Public Property company as string
				Public Property unit as string
				Public Property description as string
			
	End Class

	Public Class addResult
			Inherits ResponseModel
					
	End Class

	Public Class updateResult
			Inherits ResponseModel
					
	End Class

End Namespace

