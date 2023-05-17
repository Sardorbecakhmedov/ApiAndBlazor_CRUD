namespace BlazorUI.DTOModels;

public class CategoryWithChildrenDto
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public IEnumerable<CategoryWithChildrenDto> Children { get; set; }
}