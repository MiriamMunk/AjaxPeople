using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DbManager
{
    public class PeopleRepository
    {
        private readonly string _ConnString;
        public PeopleRepository(string conn)
        {
            _ConnString = conn;
        }
        public void AddPerson(Person person)
        {
            using var connection = new SqlConnection(_ConnString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"INSERT into person 
                                VALUES(@firstName, @lastName, @age)";
            cmd.Parameters.AddWithValue("@firstName", person.FirstName);
            cmd.Parameters.AddWithValue("@lastName", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public List<Person> GetPeople()
        {
            using var connection = new SqlConnection(_ConnString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM person";
            List<Person> people = new();
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                people.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }
            return people;
        }
        public void EditPerson(Person person)
        {
            using var connection = new SqlConnection(_ConnString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"UPDATE person SET FirstName = @firstName, LastName = @lastName,
                                Age = @age WHERE id = @id";
            cmd.Parameters.AddWithValue("@firstName", person.FirstName);
            cmd.Parameters.AddWithValue("@lastName", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            cmd.Parameters.AddWithValue("@id", person.Id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeletePerson(int id)
        {
            using var connection = new SqlConnection(_ConnString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE from person WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
