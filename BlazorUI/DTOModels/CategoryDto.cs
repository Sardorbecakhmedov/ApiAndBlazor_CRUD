﻿
namespace BlazorUI.DTOModels;

public class CategoryDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? ParentId { get; set; }
}