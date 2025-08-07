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
    public class MemberRepository : IMemberRepository
    {
        private readonly string _connectionString;

        public MemberRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(Member member)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (var insert = new MySqlCommand(
                    "INSERT INTO member (first_name, last_name) VALUES (@first_name, @last_name);",
                    connection))
                {
                    insert.Parameters.AddWithValue("@first_name", member.FirstName);
                    insert.Parameters.AddWithValue("@last_name", member.LastName);
                    insert.ExecuteNonQuery();
                }

                using (var idCmd = new MySqlCommand("SELECT LAST_INSERT_ID();", connection))
                {
                    var newIdObj = idCmd.ExecuteScalar();
                    member.Id = Convert.ToInt32(newIdObj);
                }
            }
        }

        public IEnumerable<Member> GetAll()
        {
            var result = new List<Member>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand(
                    "SELECT id, first_name, last_name FROM member",
                    connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Member
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString()
                            });
                        }
                    }
                }
            }

            return result;
        }

        public Member? GetById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand(
                    "SELECT id, first_name, last_name FROM member WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read()) return null;

                        return new Member
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            FirstName = reader["first_name"].ToString(),
                            LastName = reader["last_name"].ToString()
                        };
                    }
                }
            }
        }

        public void Update(Member member)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand(
                    @"UPDATE member
                  SET first_name = @first_name,
                      last_name  = @last_name
                  WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@first_name", member.FirstName);
                    command.Parameters.AddWithValue("@last_name", member.LastName);
                    command.Parameters.AddWithValue("@id", member.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
