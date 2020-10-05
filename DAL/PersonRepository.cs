using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entity;
namespace DAL
{
    public class PersonRepository
    {
        private readonly SqlConnection _connection;
        private readonly List<Person> _Persons = new List<Person>();
        public PersonRepository(ConnectionManager connection)
        {
            _connection = connection._conexion;
        }
        public void Save(Person Person)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"Insert Into Person (Identification,Name,Age, Sex, Pulsation) 
                                        values (@Identification,@Name,@Age,@Sex,@Pulsation)";
                command.Parameters.AddWithValue("@Identification", Person.Identification);
                command.Parameters.AddWithValue("@Name", Person.Name);
                command.Parameters.AddWithValue("@Sex", Person.Sex);
                command.Parameters.AddWithValue("@Age", Person.Age);
                command.Parameters.AddWithValue("@Pulsation", Person.Pulsation);
                var filas = command.ExecuteNonQuery();
            }
        }
        public void Delete(Person Person)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "Delete from Person where Identification=@Identification";
                command.Parameters.AddWithValue("@Identification", Person.Identification);
                command.ExecuteNonQuery();
            }
        }
        public List<Person> GetList()
        {
            SqlDataReader dataReader;
            List<Person> Persons = new List<Person>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "Select * from Person ";
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Person Person = DataReaderMapToPerson(dataReader);
                        Persons.Add(Person);
                    }
                }
            }
            return Persons;
        }
        public Person SearchById(string identification)
        {
            SqlDataReader dataReader;
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select * from Person where Identification=@Identification";
                command.Parameters.AddWithValue("@Identification", identification);
                dataReader = command.ExecuteReader();
                dataReader.Read();
                return DataReaderMapToPerson(dataReader);
            }
        }

        public void Modify(Person Person)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "update Person set Name=@Name, Age=@Age, Sex=@Sex, Pulsation=@Pulsation where Identification=@Identification";
                command.Parameters.AddWithValue("@Identification", Person.Identification);
                command.Parameters.AddWithValue("@Name", Person.Name);
                command.Parameters.AddWithValue("@Sex", Person.Sex);
                command.Parameters.AddWithValue("@Age", Person.Age);
                command.Parameters.AddWithValue("@Pulsation", Person.Pulsation);
                command.ExecuteNonQuery();
            }
        }
        private Person DataReaderMapToPerson(SqlDataReader dataReader)
        {
            if (!dataReader.HasRows) return null;
            Person Person = new Person();
            Person.Identification = (string)dataReader["Identification"];
            Person.Name = (string)dataReader["Name"];
            Person.Sex = (string)dataReader["Sex"];
            Person.Age = (int)dataReader["Age"];
            Person.Pulsation = (decimal)dataReader["Pulsation"];
            return Person;
        }
        public int Totalizar()
        {
            
            return GetList().Count();
        }
        public int TotalizarTipo(string tipo)
        {
           
            return GetList().Where(p => p.Sex.Equals(tipo)).Count();
        }
    }
}