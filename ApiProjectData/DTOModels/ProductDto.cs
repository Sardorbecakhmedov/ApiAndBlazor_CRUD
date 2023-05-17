using ApiProjectData.Models;
using Microsoft.AspNetCore.Http;

namespace ApiProjectData.DTOModels;

public class ProductDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public uint Price { get; set; } = 0;
    public IFormFile? Photo { get; set; }
}