﻿namespace ApiProjectData.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public uint Price { get; set; } = 0;
    public string PhotoUrl { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }


    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}