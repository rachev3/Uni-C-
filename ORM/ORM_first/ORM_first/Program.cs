﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ORM_first.Data;
using System.Xml.Linq;



internal class Program
{
    private static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
        .AddSingleton<IDatabaseManager, DatabaseManager>() // Database operations
        .BuildServiceProvider();

        IDatabaseManager databaseManager = new DatabaseManager("Server=LENOVO-GAMING;Database=ORM_Demo;Trusted_Connection=True;TrustServerCertificate=True");

        Console.WriteLine(databaseManager.DatabaseExists("ORM_Demo"));
    }
}

//string connectionString = "Server=LENOVO-GAMING;Database=ORM_Demo;Trusted_Connection=True;TrustServerCertificate=True";

//var databaseName = "ORM_Demo";

//if (!DatabaseExists(databaseName))
//{
//    Console.WriteLine("Creating db......");
//    CreateDB();
//}

//using var conn = new SqlConnection(connectionString);
//conn.Open();
//Console.WriteLine($"Found and connected '{databaseName}'.");

//CreateTables(conn);
////InsertData(conn);
////Buy(conn, "Gosho", "Laptop");
////Buy(conn, "Pesho", "Desktop");
////Buy(conn, "Alice", "Tablet");
////Buy(conn, "Gosho", "Tablet");
////Buy(conn, "Pesho", "Laptop");
//GroupProductsByBuyerCount(conn);


//void CreateDB()
//{
//    var masterConnectionString = $"Server=LENOVO-GAMING;Database=master;Integrated Security=true;TrustServerCertificate=True;";
//    using (var masterConn = new SqlConnection(masterConnectionString))
//    {
//        masterConn.Open();
//        string createCmdText = $"CREATE DATABASE [{databaseName}]";
//        using var createCmd = new SqlCommand(createCmdText, masterConn);
//        createCmd.ExecuteNonQuery();
//    }

//    using var conn = new SqlConnection(connectionString);

//    Console.WriteLine($"Created '{databaseName}'.");
//}

//bool DatabaseExists(string dbName)
//{
//    string masterConnectionString = $"Server=LENOVO-GAMING;Database=master;Integrated Security=true;TrustServerCertificate=True;";
//    string check = @"SELECT COUNT(*) FROM sys.databases WHERE name = @DbName";

//    using var masterConn = new SqlConnection(masterConnectionString);
//    masterConn.Open();


//    var cmd = new SqlCommand(check, masterConn);

//    cmd.Parameters.AddWithValue("@DbName", dbName);
//    var a = cmd.ExecuteScalar();



//    return (int)cmd.ExecuteScalar() > 0;
//}

//void CreateTables(SqlConnection connection)
//{
//    string createCustomerTableSql = @"
//                CREATE TABLE Customers (
//                Id INT PRIMARY KEY IDENTITY(1, 1),
//                Name NVARCHAR(50)
//            )";

//    string createProductsTableSql = @"
//                CREATE TABLE Products (
//                Id INT PRIMARY KEY IDENTITY(1, 1),
//                Name NVARCHAR(50),
//                Quantity INT
//            )";

//    string createCustomerProductTableSql = @"
//            CREATE TABLE CustomerProducts (
//                CustomerId INT,
//                ProductId INT,
//                PRIMARY KEY (CustomerId, ProductId),
//                FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
//                FOREIGN KEY (ProductId) REFERENCES Products(Id)
//            )";

//    try
//    {
//        using (var cmd = new SqlCommand(createProductsTableSql, connection))
//        {
//            cmd.ExecuteNonQuery();
//        }

//        using (var cmd = new SqlCommand(createCustomerTableSql, connection))
//        {
//            cmd.ExecuteNonQuery();
//        }

//        using (var cmd = new SqlCommand(createCustomerProductTableSql, connection))
//        {
//            cmd.ExecuteNonQuery();
//        }
//        Console.WriteLine("'Customers' 'Products' 'CustomerProducts' created.");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
//}

//void InsertData(SqlConnection connection)
//{
//    string insertProductSql = @"
//        INSERT INTO Products (Name, Quantity) VALUES
//            ('Laptop', 5),
//            ('Desktop', 4),
//            ('Tablet', 1)";

//    string insertCustomerSql = @"
//        INSERT INTO Customers (Name) VALUES
//            ('Gosho'),
//            ('Pesho'),
//            ('Alice')";

//    try
//    {
//        using (var cmd = new SqlCommand(insertProductSql, connection))
//        {
//            cmd.ExecuteNonQuery();
//        }
//        using (var cmd = new SqlCommand(insertCustomerSql, connection))
//        {
//            cmd.ExecuteNonQuery();
//        }
//        Console.WriteLine("Data inserted.");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
//}

//void Buy(SqlConnection connection, string customerName, string item)
//{
//    string selectCustomerIdSql = "SELECT Id FROM Customers WHERE Name = @Name";
//    string selectProductIdSql = "SELECT Id FROM Products WHERE Name = @Name";
//    string insertCustomerProductSql = "INSERT INTO CustomerProducts (CustomerId, ProductId) VALUES (@customerId, @productId)";
//    string alterProductQuantitySql = "UPDATE Products SET Quantity = Quantity - 1 WHERE Id = @ProductId";

//    int customerId;
//    int productId;

//    try
//    {
//        using (var cmd = new SqlCommand(selectCustomerIdSql, connection))
//        {
//            cmd.Parameters.AddWithValue("@Name", customerName);
//            customerId = (int)cmd.ExecuteScalar();
//        }
//        using (var cmd = new SqlCommand(selectProductIdSql, connection))
//        {
//            cmd.Parameters.AddWithValue("@Name", item);
//            productId = (int)cmd.ExecuteScalar();
//        }
//        using (var cmd = new SqlCommand(insertCustomerProductSql, connection))
//        {
//            cmd.Parameters.AddWithValue("@customerId", customerId);
//            cmd.Parameters.AddWithValue("@productId", productId);
//            cmd.ExecuteNonQuery();
//        }
//        using (var cmd = new SqlCommand(alterProductQuantitySql, connection))
//        {
//            cmd.Parameters.AddWithValue("@ProductId", productId);
//            cmd.ExecuteNonQuery();
//        }

//        Console.WriteLine($"'{customerName}' bought '{item}'.");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
//}


//void GroupProductsByBuyerCount(SqlConnection connection)
//{
//    string sql = @"
//        SELECT p.Name AS ProductName, COUNT(cp.CustomerId) AS BuyerCount
//        FROM Products p
//        LEFT JOIN CustomerProducts cp ON p.Id = cp.ProductId
//        GROUP BY p.Name";

//    try
//    {
//        using (var cmd = new SqlCommand(sql, connection))
//        {
//            using (var reader = cmd.ExecuteReader())
//            {
//                Console.WriteLine("\nProduct Buyer Counts:");
//                while (reader.Read())
//                {
//                    string productName = reader.GetString(0);
//                    int buyerCount = reader.GetInt32(1);
//                    Console.WriteLine($"{productName}: {buyerCount}");
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
//}