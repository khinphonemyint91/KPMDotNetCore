// See https://aka.ms/new-console-template for more information
using KPMDotNetCore.ConsoleApp.AdoDotNetExamples;
using KPMDotNetCore.ConsoleApp.DapperExamples;
using KPMDotNetCore.ConsoleApp.EFCoreExamples;
using KPMDotNetCore.ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

//SqlConnection
//SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
//stringBuilder.DataSource= "DESKTOP-2PIMDTH";
//stringBuilder.InitialCatalog = "KPMDotNetCore";
//stringBuilder.UserID = "sa";
//stringBuilder.Password = "sa@123";
//SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
//connection.Open();
//Console.WriteLine("Connection open.");
//string query = "select * from Tbl_Blog";
//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
//DataTable dt=new DataTable();
//sqlDataAdapter.Fill(dt);

//connection.Close();
//Console.WriteLine("Connection close.");

////dataset => datatable
////datatable => datarow
////datarow =>datacolumn

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine("Blog Id => " + dr["BlogId"]);
//    Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
//    Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
//    Console.WriteLine("Blog Content => " + dr["BlogContent"]);
//    Console.WriteLine("-----------------------------------------------");
//}

//ADO.Net Read
//CRUD
//AdoDotNetExample    adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create("title", "author", "content");
//adoDotNetExample.Update(8, "test title", "test author", "test content");
//adoDotNetExample.Delete(8);
//adoDotNetExample.Edit(8);
//adoDotNetExample.Edit(7);

//DapperExample dapperexample=new DapperExample();
//dapperexample.Run();

//EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Run();

//try
//{

//}catch (Exception ex)
//{
//    throw;
//}
//finally 
//{ 

//}
// add dependency injection
var connectionString = ConnectionStrings.sqlConnectionStringBuilder.ConnectionString;
var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
var serviceProvider = new ServiceCollection()
    .AddScoped( n => new AdoDotNetExample(sqlConnectionStringBuilder))
    .AddScoped( n => new DapperExample (sqlConnectionStringBuilder))
    .AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(connectionString);

    })
    .AddScoped<EFCoreExample>()
    .BuildServiceProvider();

//AppDbContext db = serviceProvider.GetRequiredService<AppDbContext>();

var adoDotNetExample = serviceProvider.GetRequiredService<AdoDotNetExample>();
adoDotNetExample.Read();

//var dapperExample = serviceProvider.GetRequiredService<DapperExample>();
//dapperExample.Run();

Console.ReadLine();
