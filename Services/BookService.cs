using LibraryManagementEFCore.Interfaces;
using LibraryManagementEFCore.Models;

namespace LibraryManagementEFCore.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task AddBookAsync(Book book)
    {
        if (string.IsNullOrWhiteSpace(book.Title))
            throw new ArgumentException("Book title is required.");
        if (string.IsNullOrWhiteSpace(book.Author))
            throw new ArgumentException("Author is required.");
        if (string.IsNullOrWhiteSpace(book.ISBN))
            throw new ArgumentException("ISBN is required.");
        if (book.Price < 0)
            throw new ArgumentException("Price cannot be negative.");
        if (book.AvailableCopies < 0)
            throw new ArgumentException("Available copies cannot be negative.");

        await _repository.AddBookAsync(book);
    }

    public Task<List<Book>> GetAllBooksAsync()
    {
        return _repository.GetAllBooksAsync();
    }

    public Task<Book?> GetBookByIdAsync(int id)
    {
        return _repository.GetBookByIdAsync(id);
    }
}
