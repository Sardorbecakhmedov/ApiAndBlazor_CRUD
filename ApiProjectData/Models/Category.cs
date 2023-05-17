
namespace ApiProjectData.Models;

public class Category
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }

    public Guid? ParentId { get; set; }
    public Category? CategoryParent { get; set; }

    public IEnumerable<Category> Children { get; set; }
    public IEnumerable<Product> Products { get; set; }
}