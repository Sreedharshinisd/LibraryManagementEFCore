using LibraryManagementEFCore.Data;
using LibraryManagementEFCore.Interfaces;
using LibraryManagementEFCore.Repositories;
using LibraryManagementEFCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var services = new ServiceCollection();
services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

services.AddScoped<IBookRepository, BookRepository>();
services.AddScoped<IMemberRepository, MemberRepository>();
services.AddScoped<IBorrowingService, BorrowingService>();

using var serviceProvider = services.BuildServiceProvider();
Console.WriteLine("Library Management System");
Console.WriteLine("Services registered successfully.");
services.AddScoped<IBookService, BookService>();
services.AddScoped<IMemberService, MemberService>();
