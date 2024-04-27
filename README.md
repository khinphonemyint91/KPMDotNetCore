Two types of EFCore
>Code First (write code to create table)
>Database First (using existing table)

To Test code first method existing database
run script using Console Package Manager
https://www.entityframeworktutorial.net/efcore/create-model-for-existing-database-in-ef-core.aspx
Scaffold-DbContext "Server=.;Database=KPMDotNetCore;User Id=sa;Password=sa@123;TrustServerCertificate=True;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context KPMDotNetCoreAppDbContext 


UI + Business Logic + Data Access + Db

Mobile , Web  => API => Database

C# => Db

KPMDotNetCore
> KPMDotNetCore.ConsoleApp

2024-04-09 Console App
2024-04-10 Ado.Net CRUD
2024-04-22 Dapper CRUD
2024-04-23 EFCore 

ASP.Net Core Web API
