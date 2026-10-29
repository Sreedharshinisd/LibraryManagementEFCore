using LibraryManagementEFCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var services = new ServiceCollection();
services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
using var serviceProvider = services.BuildServiceProvider();
Console.WriteLine("Library Management System");
Console.WriteLine("Database services configured successfully.");
