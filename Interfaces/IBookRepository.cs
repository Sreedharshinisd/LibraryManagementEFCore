using LibraryManagementEFCore.Models;

namespace LibraryManagementEFCore.Interfaces;

public interface IBookRepository
{
    Task AddBookAsync(Book book);
    Task<List<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
}
