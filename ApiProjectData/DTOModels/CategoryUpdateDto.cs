namespace ApiProjectData.DTOModels;

public class CategoryUpdateDto
{
    public string? Name { get; set; }
    public Guid? ParentId { get; set; }
    public string? Description { get; set; }
}
