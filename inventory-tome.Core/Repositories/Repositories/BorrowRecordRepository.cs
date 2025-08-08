using inventory_tome.Core.Models;
using inventory_tome.Core.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_tome.Core.Repositories.Repositories
{
    public class BorrowRecordRepository : IBorrowRecordRepository
    {
        private readonly string _connectionString;

        public BorrowRecordRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(BorrowRecord record)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = new MySqlCommand(
                    @"INSERT INTO borrow_records (book_id, member_id, borrow_date, due_date)
                  VALUES (@book_id, @member_id, @borrow_date, @due_date)",
                    connection
                );

                cmd.Parameters.AddWithValue("@book_id", record.BookId);
                cmd.Parameters.AddWithValue("@member_id", record.MemberId);
                cmd.Parameters.AddWithValue("@borrow_date", record.BorrowDate);
                cmd.Parameters.AddWithValue("@due_date", record.DueDate);
                cmd.ExecuteNonQuery();
            }
        }

        public BorrowRecord? GetActiveByBookId(int bookId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = new MySqlCommand(
                    @"SELECT * FROM borrow_records
                  WHERE book_id = @book_id AND return_date IS NULL
                  LIMIT 1",
                    connection
                );

                cmd.Parameters.AddWithValue("@book_id", bookId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    return new BorrowRecord
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        BookId = Convert.ToInt32(reader["book_id"]),
                        MemberId = Convert.ToInt32(reader["member_id"]),
                        BorrowDate = Convert.ToDateTime(reader["borrow_date"]),
                        DueDate = Convert.ToDateTime(reader["due_date"]),
                        ReturnDate = reader["return_date"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(reader["return_date"])
                    };
                }
            }
        }

        public BorrowRecord? GetById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = new MySqlCommand(
                    "SELECT * FROM borrow_records WHERE id = @id",
                    connection
                );

                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    return new BorrowRecord
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        BookId = Convert.ToInt32(reader["book_id"]),
                        MemberId = Convert.ToInt32(reader["member_id"]),
                        BorrowDate = Convert.ToDateTime(reader["borrow_date"]),
                        DueDate = Convert.ToDateTime(reader["due_date"]),
                        ReturnDate = reader["return_date"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(reader["return_date"])
                    };
                }
            }
        }

        public void Update(BorrowRecord record)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = new MySqlCommand(
                    @"UPDATE borrow_records
                  SET return_date = @return_date
                  WHERE id = @id",
                    connection
                );

                cmd.Parameters.AddWithValue("@return_date", record.ReturnDate);
                cmd.Parameters.AddWithValue("@id", record.Id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
