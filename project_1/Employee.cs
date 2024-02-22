using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_1
{
    public class Employee
    {

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public string Position { get; set; }
        public decimal Salary { get; set; }

        static SqlConnection connection;
        static Employee() {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public static IEnumerable<Employee> GetEmployees()
        {
            if(connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            var SqlStr = "SELECT * FROM Employees";
            SqlCommand cmd = new SqlCommand(SqlStr, connection);
            var employees = cmd.ExecuteReader();

            if(employees.HasRows) {

                foreach(System.Data.Common.DbDataRecord employee in employees)
                {
                    var id = employee.GetInt32(0);
                    var firstName = employee.GetString(1);
                    var lastName = employee.GetString(2);
                    var dateOfBirth = employee.GetDateTime(3);
                    var postion = employee.GetString(4);
                    var salary = employee.GetDecimal(5);

                    yield return new Employee
                    {
                        Id = id,
                        Firstname = firstName,
                        Lastname = lastName,
                        DateOfBirth = dateOfBirth,
                        Position = postion,
                        Salary = salary
                    };
                }           
            }
            connection.Close();
        }

        public void Insert()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            var SqlStr = "INSERT INTO Employees (Firstname, Lastname, Date_of_birth, Position, Salary) VALUES (" +
                "@firstname, " +
                "@lastname, " +
                "@dateOfBirth, " +
                "@position, " +
                "@salary" +
                ")";

            SqlCommand cmd = new SqlCommand(SqlStr,connection);
            cmd.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("firstname", Firstname),
                new SqlParameter("lastname", Lastname),
                new SqlParameter("dateOfBirth", DateOfBirth),
                new SqlParameter("position", Position),
                new SqlParameter("salary", Salary),
            });

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public static void Delete(int id)
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            var SqlStr = "DELETE FROM Employees WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(SqlStr, connection);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void Update()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            var SqlStr = "UPDATE Employees " +
                "SET " +
                "Firstname=@firstname, " +
                "Lastname=@lastname, " +
                "Date_of_birth=@dateOfBirth, " +
                "Position=@position, " +
                "Salary=@salary " +
                "WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(SqlStr, connection);
            cmd.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("id", Id),
                new SqlParameter("firstname", Firstname),
                new SqlParameter("lastname", Lastname),
                new SqlParameter("dateOfBirth", DateOfBirth),
                new SqlParameter("position", Position),
                new SqlParameter("salary", Salary),
            });
            cmd.ExecuteNonQuery();
            connection.Close();

        }

        public static Employee[]? FindByFirstName(string firstName)
        {
            List<Employee> foundEmployees = new List<Employee>();

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            var SqlStr = "SELECT * FROM Employees WHERE Firstname=@firstname";
            SqlCommand cmd = new SqlCommand(SqlStr, connection);
            cmd.Parameters.AddWithValue("firstname", firstName);
            var employees = cmd.ExecuteReader();

            if (employees.HasRows)
            {
                while (employees.Read())
                {
                    Employee employee = new Employee
                    {
                        Id = Convert.ToInt32(employees["Id"]),
                        Firstname = employees["Firstname"].ToString(),
                        Lastname = employees["Lastname"].ToString(),
                        DateOfBirth = (DateTime)employees["Date_of_birth"],
                        Position = employees["Position"].ToString(),
                        Salary = Convert.ToDecimal(employees["Salary"])
                    };

                    foundEmployees.Add(employee);
                }

                
            }
            connection.Close();

            if(foundEmployees.Count > 0) return foundEmployees.ToArray();

            return null;

        }

        public override string? ToString()
        {
           return $"№ {Id} Имя: {Firstname}, Фамиилия: {Lastname}, Дата рождения: {DateOfBirth}, Должность: {Position}, Оклад Труда: {Salary:0.000}";
        }
    }
}
