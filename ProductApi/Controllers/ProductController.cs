using ApiProjectData.Context;
using ApiProjectData.DTOModels;
using ApiProjectData.Models;
using ApiProjectData.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public ProductController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _appDbContext.Products.ToListAsync();
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(Guid productId)
    {
        var product = await _appDbContext.Products.FindAsync(productId);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] ProductDto model)
    {
        var category = await _appDbContext.Categories.FindAsync(model.CategoryId);

        if (category == null)
        {
            return NotFound("Category not found");
        }

        var product = new Product
        {
            CategoryId = model.CategoryId,
            Name = model.Name,
            Description = model.Description,
            PhotoUrl = await FileHelper.SaveProductFile(model.Photo),
            Price = (uint)model.Price,
            Updated = DateTime.Now,
        };

        await _appDbContext.Products.AddAsync(product);
        await _appDbContext.SaveChangesAsync();

        return Ok("Product successfully created!");
    }


    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProduct(Guid productId, [FromForm] ProductUpdateDto model)
    {
        var product = await _appDbContext.Products.FindAsync(productId);

        if (product == null)
        {
            return NotFound(model);
        }

        product.CategoryId = model.CategoryId ?? product.CategoryId;
        product.Name = model.Name ?? product.Name;
        product.Description = model.Description ?? product.Description;
        product.Price = model.Price ?? product.Price;
        product.Updated = DateTime.Now;

        if (model.Photo != null)
        {
            FileHelper.DeleteFile(product.PhotoUrl);

            product.PhotoUrl = await FileHelper.SaveProductFile(model.Photo);
        }

        await _appDbContext.SaveChangesAsync();

        return Ok(product);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        var product = await _appDbContext.Products.FindAsync(productId);

        if (product == null)
        {
            return NotFound(productId);
        }

        _appDbContext.Products.Remove(product);

        await _appDbContext.SaveChangesAsync();

        return Ok();
    }
}