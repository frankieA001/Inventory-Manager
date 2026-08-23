using System;
using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaApplication2.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AvaloniaApplication2.Models;
using AvaloniaApplication2.Services;

namespace AvaloniaApplication2.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly InventoryService _service = new();

    [ObservableProperty]  private ObservableCollection<Product> _products = new();
    
    [ObservableProperty]
    private Product? _selectedProduct;
    
    [ObservableProperty]
    private string _name = string.Empty;
    
    [ObservableProperty]
    private string _category = string.Empty;
    
    [ObservableProperty]
    private int _quantity;
    
    [ObservableProperty]
    private decimal _price;
    
    [ObservableProperty]
    private string _searchText = string.Empty;

    public MainWindowViewModel()
    {
        try
        {
            LoadProducts();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database connection error: {ex.Message}");
        }
        
    }
    
    //Load all products from db 
    private void LoadProducts()
    {
        try
        {
            Products.Clear();
            foreach (var p in _service.GetAllProducts())
                Products.Add(p);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load: {ex.Message}");
        }
        
        
    }
    
    //Add
    [RelayCommand]
    private void AddProduct()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return;

        try
        {
            _service.AddProduct(new Product
            {
                Name = Name,
                Category = Category,
                Quantity = Quantity,
                Price = Price
            });



            ClearForm();
            LoadProducts();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Add Failed: {ex.Message}");
        }
    }


    //Product to Edit 
    partial void OnSelectedProductChanged(Product? value)
    {
        if (value is null) return;
        Name = value.Name;
        Category = value.Category;
        Quantity = value.Quantity;
        Price = value.Price;
    }
    //Update 
    [RelayCommand]
    private void UpdateProduct()
    {
        if (SelectedProduct is null) return;

        SelectedProduct.Name = Name;
        SelectedProduct.Category = Category;
        SelectedProduct.Quantity = Quantity;
        SelectedProduct.Price = Price;
        
        _service.UpdateProduct(SelectedProduct);
        ClearForm();
        LoadProducts();
    }
    
    // Delete 
    [RelayCommand]
    private void DeleteProduct()
    {
        if (SelectedProduct is null) return;
        _service.DeleteProduct(SelectedProduct.Id);
        ClearForm();
        LoadProducts();
    }
    
    //Search 
    [RelayCommand]
    private void SearchProducts()
    {
        var all = _service.GetAllProducts();
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            Products = new ObservableCollection<Product>(all);
        }
        else
        {
            var filtered = all.Where(p=> p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) || 
                                         p.Category.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            
            Products = new ObservableCollection<Product>(filtered);
            
        }
    }
    
    //ClearForm 
    private void ClearForm()
    {
        Name = string.Empty;
        Category = string.Empty;
        Quantity = 0;
        Price = 0;
        SelectedProduct = null;
    }
}