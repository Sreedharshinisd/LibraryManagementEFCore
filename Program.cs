using LibraryManagementEFCore.Data;
using LibraryManagementEFCore.Interfaces;
using LibraryManagementEFCore.Models;
using LibraryManagementEFCore.Repositories;
using LibraryManagementEFCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var services = new ServiceCollection();
services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
services.AddScoped<IBookRepository, BookRepository>();
services.AddScoped<IMemberRepository, MemberRepository>();
services.AddScoped<IBorrowingService, BorrowingService>();
services.AddScoped<IBookService, BookService>();
services.AddScoped<IMemberService, MemberService>();

using var serviceProvider = services.BuildServiceProvider();
using var scope = serviceProvider.CreateScope();
var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();
var memberService = scope.ServiceProvider.GetRequiredService<IMemberService>();
var borrowingService = scope.ServiceProvider.GetRequiredService<IBorrowingService>();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("===== LIBRARY MANAGEMENT SYSTEM =====");
    Console.WriteLine("1. Add Book");
    Console.WriteLine("2. View Books");
    Console.WriteLine("3. Search Book by ID");
    Console.WriteLine("4. Register Member");
    Console.WriteLine("5. View Members");
    Console.WriteLine("6. Search Member by ID");
    Console.WriteLine("7. Borrow Book");
    Console.WriteLine("8. Return Book");
    Console.WriteLine("9. View Borrowing History");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");
    var choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                Console.Write("Title: ");
                var title = Console.ReadLine() ?? "";
                Console.Write("Author: ");
                var author = Console.ReadLine() ?? "";
                Console.Write("ISBN: ");
                var isbn = Console.ReadLine() ?? "";
                Console.Write("Price: ");
                if (!decimal.TryParse(Console.ReadLine(), out var price)) { Console.WriteLine("Invalid price."); break; }
                Console.Write("Available copies: ");
                if (!int.TryParse(Console.ReadLine(), out var copies)) { Console.WriteLine("Invalid number of copies."); break; }
                await bookService.AddBookAsync(new Book { Title = title, Author = author, ISBN = isbn, Price = price, AvailableCopies = copies });
                Console.WriteLine("Book added successfully.");
                break;
            case "2":
                var books = await bookService.GetAllBooksAsync();
                foreach (var book in books) Console.WriteLine($"ID: {book.Id} | {book.Title} | {book.Author} | ISBN: {book.ISBN} | Copies: {book.AvailableCopies}");
                if (books.Count == 0) Console.WriteLine("No books found.");
                break;
            case "3":
                Console.Write("Book ID: ");
                if (!int.TryParse(Console.ReadLine(), out var bookId)) { Console.WriteLine("Invalid ID."); break; }
                var foundBook = await bookService.GetBookByIdAsync(bookId);
                Console.WriteLine(foundBook == null ? "Book not found." : $"ID: {foundBook.Id} | {foundBook.Title} | {foundBook.Author} | Copies: {foundBook.AvailableCopies}");
                break;
            case "4":
                Console.Write("Full name: ");
                var fullName = Console.ReadLine() ?? "";
                Console.Write("Email: ");
                var email = Console.ReadLine() ?? "";
                Console.Write("Phone number: ");
                var phone = Console.ReadLine() ?? "";
                await memberService.AddMemberAsync(new Member { FullName = fullName, Email = email, PhoneNumber = phone });
                Console.WriteLine("Member registered successfully.");
                break;
            case "5":
                var members = await memberService.GetAllMembersAsync();
                foreach (var member in members) Console.WriteLine($"ID: {member.Id} | {member.FullName} | {member.Email} | {member.PhoneNumber}");
                if (members.Count == 0) Console.WriteLine("No members found.");
                break;
            case "6":
                Console.Write("Member ID: ");
                if (!int.TryParse(Console.ReadLine(), out var memberId)) { Console.WriteLine("Invalid ID."); break; }
                var foundMember = await memberService.GetMemberByIdAsync(memberId);
                Console.WriteLine(foundMember == null ? "Member not found." : $"ID: {foundMember.Id} | {foundMember.FullName} | {foundMember.Email}");
                break;
            case "7":
                Console.Write("Book ID: ");
                if (!int.TryParse(Console.ReadLine(), out var borrowBookId)) { Console.WriteLine("Invalid book ID."); break; }
                Console.Write("Member ID: ");
                if (!int.TryParse(Console.ReadLine(), out var borrowMemberId)) { Console.WriteLine("Invalid member ID."); break; }
                await borrowingService.BorrowBookAsync(borrowBookId, borrowMemberId);
                Console.WriteLine("Book borrowed successfully.");
                break;
            case "8":
                Console.Write("Borrow record ID: ");
                if (!int.TryParse(Console.ReadLine(), out var recordId)) { Console.WriteLine("Invalid record ID."); break; }
                await borrowingService.ReturnBookAsync(recordId);
                Console.WriteLine("Book returned successfully.");
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Operation failed: {ex.Message}");
    }
}
