using Microsoft.AspNetCore.Components.Forms;

namespace BlazorUI.DTOModels;

public class ProductDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; } = 0;
    public IBrowserFile? Photo { get; set; }
}