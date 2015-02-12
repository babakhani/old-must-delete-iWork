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
		Function Update(requestModel as ivContact) as ResponseModel

		<Authorize(allowRoles:="*", allowUsers:="", denyRoles:="?", denyUsers:="")>
		Function Remove(requestModel as RequestIdModel) as ResponseModel

		<Authorize(allowRoles:="*", allowUsers:="", denyRoles:="?", denyUsers:="")>
		Function Search(requestModel as ivContactSearch) as ResponseModel

		<Authorize(allowRoles:="*", allowUsers:="", denyRoles:="?", denyUsers:="")>
		Function ById(requestModel as RequestIdModel) as ResponseModel

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
							Public Property contactId as integer
				Public Property fullname as string
				Public Property gender as byte
				Public Property tel1 as string
				Public Property tel2 as string
				Public Property tel3 as string
				Public Property tel4 as string
				Public Property company as string
				Public Property unit as string
				Public Property description as string
			
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

