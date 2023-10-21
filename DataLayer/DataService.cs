using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Data.SqlTypes;

namespace DataLayer;

public class DataService
{
    
    public IList<Category> GetCategories()
    {
        var db = new NorthwindContex();
        return db.Categories.ToList(); 
    }

    public Category? GetCategory(int categoryId)
    {
        var db = new NorthwindContex();
        return db.Categories.FirstOrDefault(x => x.Id == categoryId);
        //return db.Categories.Find(categoryId);
    }

    public bool DeleteCategory(Category category)
    {
        return DeleteCategory(category.Id);
    }
    public bool DeleteCategory(int categoryId)
    {
        var db = new NorthwindContex();
        var category = db.Categories.FirstOrDefault(x => x.Id == categoryId);
        if(category != null)
        {
            db.Categories.Remove(category);
            //db.Remove(category);
            return db.SaveChanges() > 0;
        }
        return false;
    }

    public Category CreateCategory(string name, string description) {
        var db = new NorthwindContex();
        var id = db.Categories.Max(x => x.Id) + 1;
        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };
        db.Add(category);
        db.SaveChanges();
        return category;
    }
    public bool UpdateCategory(int id, string name, string description)
    {
        var db= new NorthwindContex();
        var category = db.Categories.FirstOrDefault(x => x.Id == id);
        if (category != null)
        {
        
            category.Name = "UpdatedName";
            category.Description = "UpdatedDescription";
            db.SaveChanges();
            return true;
        }
        return false;
    }
    public DTOProductWithCategoryName? GetProduct(int productId)
    {
        var db = new NorthwindContex();

        var x = db.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == productId);
        if (x != null)
        {
            return CreateDto(x);
        }
        return null;
    }

    public DTOProductWithCategoryName CreateDto(Product product)
    {
        return new DTOProductWithCategoryName
        {
            Name = product.Name,
            CategoryName = product.Category.Name

        };      
    }

    public IList<DTOProductWithCategoryName> GetProductByCategory(int CategoryId)
    {
        var db = new NorthwindContex();
        return db.Products
            .Include(x => x.Category)
            .Where(x => x.Category.Id == CategoryId)
            .Select(x => new DTOProductWithCategoryName
            {
                CategoryName = x.Category.Name,
                Name = x.Name
            })
            .ToList();
    }

    public IList<DTOProductNameWithCategoryName> GetProductByName(string subString)
    {
        var db = new NorthwindContex();
        return db.Products
            .Where(x => x.Name.Contains(subString))
            .Select(x => new DTOProductNameWithCategoryName
            {
                CategoryName = x.Category.Name,
                ProductName = x.Name
            })
            .ToList();
    }

    public Order GetOrder(int orderId)
    {
        var db = new NorthwindContex();
        var res = db.Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Category)
            .FirstOrDefault(x => x.Id == orderId);

        return res;
    }

    public IList<Order> GetOrders()
    {
        var db = new NorthwindContex();
        return db.Orders.ToList();
    }

    public IList<DTOProductNameUnitPriceAndQuantity> GetOrderDetailsByOrderId(int orderId)
    {
        var db = new NorthwindContex();
        return db.OrderDetails
            .Include(x => x.Product)
            .Where(x => x.OrderId == orderId)
            .Select(x => new DTOProductNameUnitPriceAndQuantity
            {
                Product = x.Product,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity

            })
            .ToList();
    }

    public IList<DTOOrderDateUnitPriceAndQuantity> GetOrderDetailsByProductId(int productId)
    {
        var db = new NorthwindContex();
        return db.OrderDetails
            .Include(x => x.Order)
            .Where(x => x.ProductId == productId)
            .Select(x => new DTOOrderDateUnitPriceAndQuantity
            {
                OrderId = x.OrderId,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                Order = x.Order
            })
            .OrderBy(x => x.OrderId)
            .ToList();
    }

}



