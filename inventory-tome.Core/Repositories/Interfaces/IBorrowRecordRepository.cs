using inventory_tome.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_tome.Core.Repositories.Interfaces
{
    public interface IBorrowRecordRepository
    {
        BorrowRecord GetById(int id);
        IEnumerable<BorrowRecord> GetByMemberId(int memberId);
        IEnumerable<BorrowRecord> GetByBookId(int bookId);
        void Add(BorrowRecord record);
        void Update(BorrowRecord record);
    }
}
