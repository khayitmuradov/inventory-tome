using inventory_tome.Core.Models;
using inventory_tome.Core.Repositories.Interfaces;
using MySql.Data.MySqlClient;

namespace inventory_tome.Core.Repositories.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Book book)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand(
                    "INSERT INTO books (title, author, status) VALUES (@title, @author, @status)",
                    connection
                );

                command.Parameters.AddWithValue("@title", book.Title);
                command.Parameters.AddWithValue("@author", book.Author);
                command.Parameters.AddWithValue("@status", book.Status);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Book> FindByTitle(string title)
        {
            var books = new List<Book>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM books WHERE title LIKE @title", connection);
                command.Parameters.AddWithValue("@title", "%" + title + "%");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Title = reader["title"].ToString(),
                            Author = reader["author"].ToString(),
                            Status = Convert.ToBoolean(reader["status"])
                        });
                    }
                }
            }
            return books;
        }

        public IEnumerable<Book> GetAll()
        {
            var books = new List<Book>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM books", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Title = reader["title"].ToString(),
                            Author = reader["author"].ToString(),
                            Status = Convert.ToBoolean(reader["status"])
                        });
                    }
                }
            }

            return books;
        }

        public Book? GetById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM books WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Book
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Title = reader["title"].ToString(),
                            Author = reader["author"].ToString(),
                            Status = Convert.ToBoolean(reader["status"]),
                        };
                    }
                    return null;
                }
            }
        }

        public void Update(Book book)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand(
                    "UPDATE books SET title = @title, author = @author, status = @isAvailable WHERE id = @id",
                    connection
                );
                command.Parameters.AddWithValue("@title", book.Title);
                command.Parameters.AddWithValue("@author", book.Author);
                command.Parameters.AddWithValue("@isAvailable", book.Status);
                command.Parameters.AddWithValue("@id", book.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}
