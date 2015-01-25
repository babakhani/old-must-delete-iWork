
Namespace Controllers	

'-----------------------------------------------------------
'------------------ Interfaces ----------------------------
'-----------------------------------------------------------
	Public Interface IContactController

				Function Add(requestModel as ivContact) as ivAddResult
				Function Update(requestModel as ivContact1) as ivAddResult
			
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

	Public Class ivContact1
		Inherits RequestModel
							Public Property aaaaaaa as string
			
	End Class

	Public Class ivAddResult
			Inherits ResponseModel
					
	End Class

End Namespace

