using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace project_2
{
    public class Order
    {
        static string connectionString;
        static SqlConnection connection;
        static SqlDataAdapter adapter;
        static DataTable ordersTable = new DataTable();

        public int? Id { get; set; }
        public string? Name { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Cost { get; set; }
        public int? Quantity { get; set; }

        public  static void Connection()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public static DataTable GetAll()
        {
            Connection();
            string sql = "SELECT * FROM Orders";
            adapter = new SqlDataAdapter(sql, connection);
            adapter.Fill(ordersTable);
            connection.Close();

            return ordersTable;
        }

        public static void Update()
        {
            Connection();
            // SqlCommandBuilder позволяет сгенерить нужные SQL-выражения
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            adapter.Update(ordersTable);
            connection.Close();
        }

        public string? Find()
        {
            Connection();
            DataTable ordersTableForFind = new DataTable();
            string str = "";
            if(Id != null)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Orders WHERE Id=@id", connection);
                command.Parameters.AddWithValue("id", Id);
                adapter = new SqlDataAdapter(command);

                adapter.Fill(ordersTableForFind);
                foreach (DataRow row in ordersTableForFind.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                        str += $"\t{cell}";
                    str += "\n";
                }

                return str;
            }
            else if (Name != null)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Orders WHERE Name=@name", connection);
                command.Parameters.AddWithValue("name", Name);
                adapter = new SqlDataAdapter(command);

                adapter.Fill(ordersTableForFind);
                foreach (DataRow row in ordersTableForFind.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                        str += $"\t{cell}";
                    str += "\n";
                }

                return str;
            }
            else if (Weight != null)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Orders WHERE Weight=@weight", connection);
                command.Parameters.AddWithValue("weight", Weight);
                adapter = new SqlDataAdapter(command);

                adapter.Fill(ordersTableForFind);
                foreach (DataRow row in ordersTableForFind.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                        str += $"\t{cell}";
                    str += "\n";
                }

                return str;
            }            
            else if (Cost != null)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Orders WHERE Cost=@cost", connection);
                command.Parameters.AddWithValue("cost", Cost);
                adapter = new SqlDataAdapter(command);

                adapter.Fill(ordersTableForFind);
                foreach (DataRow row in ordersTableForFind.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                        str += $"\t{cell}";
                    str += "\n";
                }

                return str;
            } 
            else if (Quantity != null)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Orders WHERE Quantity=@quantity", connection);
                command.Parameters.AddWithValue("quantity", Quantity);
                adapter = new SqlDataAdapter(command);

                adapter.Fill(ordersTableForFind);
                foreach (DataRow row in ordersTableForFind.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                        str += $"\t{cell}";
                    str += "\n";
                }

                return str;
            }

            return null;
        }

        public override string ToString()
        {
            return $"{Id} наименование блюда: {Name}, масса: {Weight:0.00}, цена: {Cost:0.00}, количество: {Quantity}";
        }
    }
}
