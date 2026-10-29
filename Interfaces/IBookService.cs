using LibraryManagementEFCore.Models;

namespace LibraryManagementEFCore.Interfaces;

public interface IBookService
{
    Task AddBookAsync(Book book);
    Task<List<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
}
