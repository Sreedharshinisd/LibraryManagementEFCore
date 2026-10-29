using LibraryManagementEFCore.Data;
using LibraryManagementEFCore.Interfaces;
using LibraryManagementEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementEFCore.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task AddBookAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await _context.Books.AsNoTracking().ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }
}
