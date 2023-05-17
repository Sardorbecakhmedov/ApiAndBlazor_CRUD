namespace BlazorUI.DTOModels;

public class CategoryUpdateDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid? ParentId { get; set; }
    public string? Description { get; set; }
}