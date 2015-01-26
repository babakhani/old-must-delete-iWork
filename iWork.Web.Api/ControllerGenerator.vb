
Namespace Controllers	

'-----------------------------------------------------------
'------------------ Interfaces ----------------------------
'-----------------------------------------------------------
	Public Interface IContactController
		Inherits IController
				Function Add(requestModel as ivContact) as ivGeneralResult
				Function Update(requestModel as ivContact) as ivGeneralResult
				Function Remove(requestModel as ivContactRemove) as ivGeneralResult
				Function Search(requestModel as ivContactSearch) as ivGeneralResult
			
	End Interface

'-----------------------------------------------------------
'------------------ Controllers ----------------------------
'-----------------------------------------------------------
	Partial Public Class ContactController
		Inherits BaseController

	End Class


'-----------------------------------------------------------
'------------------ Models ----------------------------
'-----------------------------------------------------------

	Public Class ivContact
		Inherits RequestModel
							Public Property ContactId as integer
				Public Property Fullname as string
				Public Property Tel1 as string
				Public Property Tel2 as string
				Public Property Tel3 as string
				Public Property Tel4 as string
				Public Property Company as string
				Public Property Unit as string
				Public Property Description as string
			
	End Class

	Public Class ivContactRemove
		Inherits RequestModel
							Public Property ContactId as integer
			
	End Class

	Public Class ivContactSearch
		Inherits RequestModel
							Public Property term as string
			
	End Class

	Public Class ivGeneralResult
			Inherits ResponseModel(of ivGeneralResult)
					
	End Class

End Namespace

