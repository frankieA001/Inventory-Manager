using MySql.Data.MySqlClient;

namespace AvaloniaApplication2.Services;

public class DataBaseHelper
{
    private const string ConnectionString = 
        "Server=localhost;Database=inventory_db;Uid=root;Pwd=Fr42790?;";

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }
    
    
}
