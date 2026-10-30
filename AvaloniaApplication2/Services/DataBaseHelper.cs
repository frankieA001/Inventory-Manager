using System;
using MySql.Data.MySqlClient;

namespace AvaloniaApplication2.Services;

public class DataBaseHelper
{
    // The password is read from an environment variable, not stored in the code
    private static readonly string ConnectionString =
        $"Server=localhost;Database=inventory_db;Uid=root;Pwd={Environment.GetEnvironmentVariable("DB_PASSWORD")};";

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }
}

