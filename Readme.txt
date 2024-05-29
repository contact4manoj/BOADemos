NAMING CONVENTIONS
														Hungarian Naming Convention
FIRSTNAME	Capital Casing		Constant	NAME		NAMESTRING		STRNAME	
FirstName	Pascal Casing		Public		Name		NameString		StrName
firstname	Small Casing		Local		name		namestring		strname
firstName	Camel Casing		Protected	name		nameString		strName
_firstName	Camel Casing		Private		_name		_nameString		_strName

--------------------------

MODEL
	a type representing shape of the data 

Types of Models:
	ViewModel			represents data sent by the Controller to the View
	InputModel			represents data sent by the View to the Controller on submission
	DataModel/Entity	represents DB structural model as a Class
	DomainModel			represents the data as per the businees rule (Customer->Info, Orders, Order Details, Shipping)
	PageModel			ASP.NET Razor Pages 
						represents the Model of the data sent to the Razor View from the Code-Beside File(.cshtml.cs)

--------------------------

from Controller to the View:
	ViewBag
	ViewData
	TempData
	ViewModel (best - typed data)

--------------------------

Entity Framework Code-First Approach:
1. Added Nuget Packages: 
		Microsoft.EntityFrameworkCore.SqlServer (latest version under the .NET version of the Project) 
		Microsoft.EntityFrameworkCore.Tools	    (latest version under the .NET version of the Project)
2. Define the Model(s)
3. Define the DbContext 
		Expose the collection of the Model (DbSet<T>)
4. Configure the connection string: appsetting.(envirionment).json
5. Program.cs
	Extract the connection string using the Configuration Service
	Register the EF Core DbContext object, 
		using the options pattern initialize the SQlService Service providing the connection string
6. Run EF Core Migrations (in Package Manager Console / Terminal )
	NOTE: Startup Project should be selected correctly, and PM Console Project should match the same.
	a. Add-Migration <name>
	b. Update-Database


EF Migration Commands
	Add-Migration
	Bundle-Migration
	Drop-Database
	Get-DbContext
	Get-Migration
	Optimize-DbContext
	Remove-Migration
	Scaffold-DbContext
	Script-Migration
	Update-Database

OWASP : Open Worldwide Application Security Project