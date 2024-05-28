MODEL
	a type representing shape of the data 

Types of Models:
	ViewModel			represents data sent by the Controller to the View
	InputModel			represents data sent by the View to the Controller on submission
	DataModel/Entity	represents DB structural model as a Class
	DomainModel			represents the data as per the businees rule (Customer->Info, Orders, Order Details, Shipping)
	PageModel			ASP.NET Razor Pages 
						represents the Model of the data sent to the Razor View from the Code-Beside File(.cshtml.cs)

from Controller to the View:
	ViewBag
	ViewData
	TempData
	ViewModel (best - typed data)