using Microsoft.AspNetCore.Components.Forms;

namespace BlazorUI.DTOModels;

public class ProductUpdateDto
{
    public Guid Id { get; set; }
    public Guid? CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Price { get; set; }
    public IBrowserFile? Photo { get; set; }

}