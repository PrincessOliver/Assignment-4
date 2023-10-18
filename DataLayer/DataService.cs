using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

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

        var x = db.Products.Include(x => x.Category)
                          .FirstOrDefault(x => x.Id == productId);
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

}



