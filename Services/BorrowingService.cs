
using LibraryManagementEFCore.Data;
using LibraryManagementEFCore.Interfaces;
using LibraryManagementEFCore.Models;

namespace LibraryManagementEFCore.Services;

public class BorrowingService : IBorrowingService
{
    private readonly LibraryDbContext _context;

    public BorrowingService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task BorrowBookAsync(int bookId, int memberId)
    {
        var book = await _context.Books.FindAsync(bookId);

        if (book == null)
            throw new InvalidOperationException("Book not found.");

        var member = await _context.Members.FindAsync(memberId);

        if (member == null)
            throw new InvalidOperationException("Member not found.");

        if (book.AvailableCopies <= 0)
            throw new InvalidOperationException("No copies are available.");

        book.AvailableCopies--;

        var record = new BorrowRecord
        {
            BookId = bookId,
            MemberId = memberId,
            BorrowDate = DateTime.Now,
            IsReturned = false
        };

        await _context.BorrowRecords.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task ReturnBookAsync(int borrowRecordId)
    {
        var record = await _context.BorrowRecords.FindAsync(borrowRecordId);

        if (record == null)
            throw new InvalidOperationException("Borrow record not found.");

        if (record.IsReturned)
            throw new InvalidOperationException(
                "This book has already been returned.");

        var book = await _context.Books.FindAsync(record.BookId);

        if (book == null)
            throw new InvalidOperationException("Book not found.");

        record.IsReturned = true;
        record.ReturnDate = DateTime.Now;
        book.AvailableCopies++;

        await _context.SaveChangesAsync();
    }
}
