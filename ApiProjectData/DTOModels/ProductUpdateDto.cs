using Microsoft.AspNetCore.Http;

namespace ApiProjectData.DTOModels;

public class ProductUpdateDto
{
    public Guid? CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public uint? Price { get; set; }
    public IFormFile? Photo { get; set; }

}