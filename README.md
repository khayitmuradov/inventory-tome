# inventory-tome


## Features

- Add/find books
- Register library members
- Borrow and return books
- Calculate overdue fines
- (Optional) Notification and reports

## Tech Stack

- .NET Core (C#)
- ADO.NET for MySQL database access
- Moq for unit testing/mocking

## Learning Goals

- Practice **unit testing** and **mocking** with real-world patterns  
- Learn how to structure a testable .NET application  
- Separate business logic from UI and data access  

### Example Test Scenarios

- Borrowing a book (with mocked repository)
- Returning a book and calculating fines
- Registering members
- Edge cases (book not available, member already exists, etc.)

---

> This project is intentionally kept small, to learn and practice unit testing using Moq

