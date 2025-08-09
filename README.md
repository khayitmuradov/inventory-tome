# inventory-tome
## Features

- Add/find books
- Register library members
- Borrow and return books
- Calculate overdue fines
- (Optional) Notification and reports

### Example Test Scenarios

- Borrowing a book (with mocked repository)
- Returning a book and calculating fines
- Registering members
- Edge cases (book not available, member already exists, etc.)

### Books
- 📗 AddBook_ShouldAddWithStatusTrue ✅
- 📗 FindBooks_ShouldReturnMatches_FromRepository ✅
- 📗 GetBookById_ShouldReturnBook_WhenExists ✅
- 📗 GetBookById_ShouldReturnNull_WhenNotFound ✅
- 📗 GetAllBooks_ShouldReturnAll_FromRepository ✅
- 📗 UpdateBook_ShouldForwardToRepository ✅

### Members
- 🧑‍🤝‍🧑 RegisterMember_ShouldAddWithNames ✅
- 🧑‍🤝‍🧑 GetMemberById_ShouldReturnMember_WhenExists  
- 🧑‍🤝‍🧑 GetMemberById_ShouldReturnNull_WhenNotFound
- 🧑‍🤝‍🧑 GetAllMembers_ShouldReturnAll_FromRepository

### BorrowBook
- 📕 BorrowBook_ShouldFail_WhenBookNotFound_SetsMessage
- 📕 BorrowBook_ShouldFail_WhenBookAlreadyBorrowed_SetsMessage
- 📕 BorrowBook_ShouldFail_WhenMemberNotFound_SetsMessage
- 📕 BorrowBook_ShouldSucceed_UpdatesBookAndAddsBorrowRecord
- 📕 BorrowBook_ShouldSetBorrowAndDueDates_14DaysFromNow
- 📕 BorrowBook_ShouldReturnTrue_AndEmptyErrorMessage

### ReturnBook
🔁 ReturnBook_ShouldFail_WhenBookNotFound_SetsMessageAndZeroFine
🔁 ReturnBook_ShouldFail_WhenNoActiveBorrow_SetsMessageAndZeroFine
🔁 ReturnBook_ShouldSucceed_UpdatesRecordAndMarksBookAvailable
🔁 ReturnBook_ShouldSetReturnDate_OnBorrowRecord
🔁 ReturnBook_ShouldSetFineZero_WhenOnOrBeforeDueDate
🔁 ReturnBook_ShouldCalculateFinePerLateDay

---

> This project is intentionally kept small, to learn and practice unit testing using Moq

