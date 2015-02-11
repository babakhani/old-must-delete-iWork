Imports iWork.Core
Imports iWork.Service
Imports iWork.Core.Controllers
Imports iWork.Entities

Namespace Controllers	

'-----------------------------------------------------------
'------------------ Interfaces ----------------------------
'-----------------------------------------------------------
	Public Interface IContactController
		Inherits IController

		<Authorize(allowRoles:="*", allowUsers:="", denyRoles:="?", denyUsers:="")>
		Function Add(requestModel as ivContact) as ResponseModel

		<Authorize(allowRoles:="*", allowUsers:="", denyRoles:="?", denyUsers:="")>
		Function Update(requestModel as ivContact) as ResponseModel

		<Authorize(allowRoles:="*", allowUsers:="", denyRoles:="?", denyUsers:="")>
		Function Remove(requestModel as RequestIdModel) as ResponseModel

		<Authorize(allowRoles:="*", allowUsers:="", denyRoles:="?", denyUsers:="")>
		Function Search(requestModel as ivContactSearch) as ResponseModel

		<Authorize(allowRoles:="*", allowUsers:="", denyRoles:="?", denyUsers:="")>
		Function GetById(requestModel as RequestIdModel) as ResponseModel

		<Authorize(allowRoles:="*", allowUsers:="", denyRoles:="?", denyUsers:="")>
		Function GetList(requestModel as ivContactList) as ResponseModel

	
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

	Public Class ivContactSearch
		Inherits RequestModel
							Public Property term as string
			
	End Class

	Public Class ivContactList
		Inherits RequestModel
							Public Property columns as object
				Public Property order as object
				Public Property start as integer
				Public Property lenght as integer
				Public Property term as string
			
	End Class

End Namespace

