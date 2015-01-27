Imports iWork.Core
Imports iWork.Core.Controllers

Namespace Controllers	

'-----------------------------------------------------------
'------------------ Interfaces ----------------------------
'-----------------------------------------------------------
	Public Interface IContactController
		Inherits IController
				Function Add(requestModel as ivContact) as ResponseModel
				Function Update(requestModel as ivContact) as ResponseModel
				Function Remove(requestModel as RequestIdModel) as ResponseModel
				Function Search(requestModel as ivContactSearch) as ResponseModel
				Function GetById(requestModel as RequestIdModel) as ResponseModel
			
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
							Public Property ContactId as integer
			
	End Class

	Public Class ivContactSearch
		Inherits RequestModel
							Public Property term as string
			
	End Class

End Namespace

