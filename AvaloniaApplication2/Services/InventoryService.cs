using AvaloniaApplication2.Models;
using AvaloniaApplication2.Services;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace AvaloniaApplication2.ViewModels;

public class InventoryService
{
    private readonly DataBaseHelper _db = new();
    
    // get all products
    public List<Product> GetAllProducts()
    {
        var products = new List<Product>();
        using var connection = _db.GetConnection();
        connection.Open();
        
        var cmd = new MySqlCommand("SELECT * FROM products",  connection);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            products.Add(new Product
            {
                Id = reader.GetInt32("id"),
                Name = reader.GetString("name"),
                Category = reader.GetString("category"),
                Quantity = reader.GetInt32("quantity"),
                Price = reader.GetDecimal("price")
            });
        }
        return products;
    }

    public void AddProduct(Product product)
    {
        using var connection = _db.GetConnection();
        connection.Open();

        var cmd = new MySqlCommand(
            "INSERT Into products (name, category, quantity,price) VALUES (@name, @category, @quantity,  @price)",
            connection);
        cmd.Parameters.AddWithValue("@name", product.Name);
        cmd.Parameters.AddWithValue("@category", product.Category);
        cmd.Parameters.AddWithValue("@quantity", product.Quantity);
        cmd.Parameters.AddWithValue("@price", product.Price);
        cmd.ExecuteNonQuery();
    }

    public void UpdateProduct(Product product)
    {
        using var connection = _db.GetConnection();
        connection.Open();
        
        var cmd = new MySqlCommand(
            "UPDATE products SET name=@name, category=@category, quantity=@quantity, price=@price  WHERE id=@id",
            connection);
        cmd.Parameters.AddWithValue("@name", product.Name);
        cmd.Parameters.AddWithValue("@category", product.Category);
        cmd.Parameters.AddWithValue("@quantity", product.Quantity);
        cmd.Parameters.AddWithValue("@price", product.Price);
        cmd.Parameters.AddWithValue("@id", product.Id);
        cmd.ExecuteNonQuery();
    }

    public void DeleteProduct(int id)
    {
        using var connection = _db.GetConnection();
        connection.Open();
        
        var cmd = new MySqlCommand("DELETE FROM products WHERE id=@id", connection);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }
    
}


