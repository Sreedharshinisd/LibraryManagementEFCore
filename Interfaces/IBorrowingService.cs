
using LibraryManagementEFCore.Models;

namespace LibraryManagementEFCore.Interfaces;

public interface IBorrowingService
{
    Task BorrowBookAsync(int bookId, int memberId);

    Task ReturnBookAsync(int borrowRecordId);

    Task<List<BorrowRecord>> GetBorrowingHistoryAsync();
}
