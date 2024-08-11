using System.Data;
using System.Data.SqlClient;

namespace Shop
{
    public class DataManager
    {
        public DataManager(string connectionSring)
        {
            this.connectionString = connectionSring;
        }
        string connectionString;

        public async Task LoadData(string XmlPath)
        {
            try
            {
                await ExecuteInsert(XmlPath,"InsertUsers");
                await ExecuteInsert(XmlPath,"InsertProducts");
                await ExecuteInsert(XmlPath,"InsertOrders");
                await ExecuteInsert(XmlPath,"InsertOrdersProducts");
                Console.WriteLine();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public async Task ExecuteInsert(string XmlPath,string procedureName)
        {
            string sqlExpression = procedureName;
            string tableName = procedureName.Substring(6);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@XmlPath",
                    Value = XmlPath
                };
                command.Parameters.Add(nameParam);
                int number = await command.ExecuteNonQueryAsync();
                Console.WriteLine("В таблицу "+ tableName +" добавлено объектов: {0}", number);

            }
        }
        public async Task GetSales()
        {
            string sqlExpression = "GetSales";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}");

                        while (await reader.ReadAsync())
                        {
                            string NameProduct = reader.GetString(0);
                            double Quantity = reader.GetDouble(1);
                            double Amount = reader.GetDouble(2);
                            Console.WriteLine($"{NameProduct} \t{Quantity} \t\t{Amount}");
                        }
                    }
                }
            }
        }
    }
}
