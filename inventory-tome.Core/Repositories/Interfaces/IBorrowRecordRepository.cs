using inventory_tome.Core.Models;

namespace inventory_tome.Core.Repositories.Interfaces
{
    public interface IBorrowRecordRepository
    {
        void Add(BorrowRecord record);
        void Update(BorrowRecord record);
        BorrowRecord? GetActiveByBookId(int bookId);
        BorrowRecord? GetById(int id);
    }
}
