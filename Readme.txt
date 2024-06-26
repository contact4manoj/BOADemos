Feedback Form: 
https://forms.gle/TbXxkppXsE3jYUeT9



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
	DtoModel			Data Transformation Model -> represent shape of data sent by an API

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
		Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore	(same as EF.SqlServer nuget package)
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
--- Steps of OWIN Identity Management
7. Add the Nuget Packages:
		Microsoft.AspNetCore.Identity.EntityFrameworkCore		(same as EF.SqlServer nuget package)
		Microsoft.AspNetCore.Identity.UI						(same as EF.SqlServer nuget package)
8. Add the custom IdentityUser model (BankingUser)
9. Changed the ApplicationDbContext base class from "DbContext" to "IdentityDbContext<BankingUser>"
10. Configure Program.cs to register the IdentityServices using the custom User model.
11. Remove all existing migrations (because the DbContext class was reset), and regenerated, updated database


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
KATANA : Security Models 
OWIN   : Open Web Interface for .NET (implementation of Katana)

	Role						Manager, Admin, User
	User						Name, Age, DOB, PassportInfo
	Permissions					CanTravelAbroad, CanDrink
	Claims						HasPassport
	Token						(external logins - Facebook, Google)

	ISignInManager
	IUserManager
	IRoleManager
	this.User
	this.User.IsInRole("Admin")

----------------------------------------

Create ASP.NET Project 
- Add Model
- Add DbContext
- Run Migrations
- Add Controller with Views using EF
- Optimized Controllers & Views as need
- Create ViewModel if needed

--------------------

Standards for API
	Open API 
	OData  (Open API Data)
	OAuth  (Open API Authentication and Authorization)

To add API Documentation
01 Add "Swashbuckle.AspNetCore" Nuget Package
02 In Program.cs
	(a) Register Swagger Gen Services
	(b) Activate the Middleware

-------
Testing Types
	Unit Level Testing
	Regression Testing
	Load Testing
	VAPT (Vulnerability Assessment and Penetration Testing)
	Integration Testing

Unit Testing Frameworks
	MSUnit
	NUnit
	xUnit
Integration Testing Frameworks
	xUnit
Nuget - Mocking Framework (MocK)

-----------------------

www.google.co.in
	<script src="http://www.google.co.in/demo.js"></script>
	<img src="logo.gif" />


(A) PRE-FETCH		<= HEADER check existing in local cache
(B) FETCH		<= HEADER + BODY

