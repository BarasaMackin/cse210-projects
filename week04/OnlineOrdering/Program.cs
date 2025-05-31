using System;
using System.Collections.Generic;
using System.Text;

public class Product
{
    private readonly string _name;
    private readonly string _productId;
    private readonly decimal _price;
    private readonly int _quantity;

    public Product(string name, string productId, decimal price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    public string GetName() => _name;
    public string GetProductId() => _productId;
    public decimal GetPrice() => _price;
    public int GetQuantity() => _quantity;

    public decimal GetTotalCost()
    {
        return _price * _quantity;
    }
}
public class Address
{
    private readonly string _street;
    private readonly string _city;
    private readonly string _stateOrProvince;
    private readonly string _country;

    public Address(string street, string city, string stateOrProvince, string country)
    {
        _street = street;
        _city = city;
        _stateOrProvince = stateOrProvince;
        _country = country;
    }

    public bool IsInUSA()
    {
        return string.Equals(_country.Trim(), "USA", StringComparison.OrdinalIgnoreCase);
    }

    public string GetAddressString()
    {
        return $"{_street}{Environment.NewLine}{_city}, {_stateOrProvince}{Environment.NewLine}{_country}";
    }
}
public class Customer
{
    private readonly string _name;
    private readonly Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public string GetName() => _name;
    public Address GetAddress() => _address;

    public bool LivesInUSA()
    {
        return _address.IsInUSA();
    }
}
public class Order
{
    private const decimal USA_SHIPPING_COST = 5m;
    private const decimal INTERNATIONAL_SHIPPING_COST = 35m;

    private List<Product> _products = new List<Product>();
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public decimal GetTotalPrice()
    {
        decimal total = 0;
        foreach (var product in _products)
        {
            total += product.GetTotalCost();
        }
        total += _customer.LivesInUSA() ? USA_SHIPPING_COST : INTERNATIONAL_SHIPPING_COST;
        return total;
    }

    public string GetPackingLabel()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Packing Label:");
        foreach (var product in _products)
        {
            sb.AppendLine($"{product.GetName()} (ID: {product.GetProductId()})");
        }
        return sb.ToString();
    }

    public string GetShippingLabel()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Shipping Label:");
        sb.AppendLine(_customer.GetName());
        sb.AppendLine(_customer.GetAddress().GetAddressString());
        return sb.ToString();
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        // First customer and order
        Address address1 = new Address("27 Maisha", "Beacon", "MA", "USA");
        Customer customer1 = new Customer("Stacy Johnson", address1);
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Widget", "W123", 10.00m, 2));
        order1.AddProduct(new Product("Gadget", "G456", 15.50m, 1));

        // Second customer and order
        Address address2 = new Address("186 Acacia Ave", "Dallas", "TX", "Canada");
        Customer customer2 = new Customer("Peter James", address2);
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Mercedes", "C63", 7.25m, 3));
        order2.AddProduct(new Product("MacBook", "D012", 12.00m, 2));
        order2.AddProduct(new Product("Airbus", "A350", 5.00m, 5));

        // Display order 1
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice():0.00}\n");

        // Display order 2
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice():0.00}\n");
    }
}
