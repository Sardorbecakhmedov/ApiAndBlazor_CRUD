using ApiProjectData.Context;
using ApiProjectData.DTOModels;
using ApiProjectData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public CategoryController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<CategoryWithChildrenDto>> GetCategories()
    {
        var categories = await _appDbContext.Categories
                    .Where(ch => ch.ParentId == null).ToListAsync();

        return await GetAllCategoriesWithChildren(categories);
    }

    private async Task<List<CategoryWithChildrenDto>> GetAllCategoriesWithChildren(IEnumerable<Category> categories)
    {
        var newCategories = new List<CategoryWithChildrenDto>();

        foreach (var category in categories)
        {
            newCategories.Add(await MapToDto(category));
        }

        return newCategories;
    }

    private async Task<CategoryWithChildrenDto> MapToDto(Category category)
    {
        await _appDbContext.Entry(category).Collection(ch => ch.Children).LoadAsync();

        return new CategoryWithChildrenDto
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            Description = category.Description,
            Children = await GetAllCategoriesWithChildren(category.Children)
        };
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategoryById(Guid categoryId)
    {
        var category = await _appDbContext.Categories
            .FirstOrDefaultAsync(category => category.Id == categoryId);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDto model)
    {
        if (model.ParentId != null &&
            !await _appDbContext.Categories.AnyAsync(id => id.Id == model.ParentId))
        {
            return NotFound();
        }

        var category = new Category
        {
            ParentId = model.ParentId,
            CategoryName = model.Name,
            Description = model.Description,
        };

        await _appDbContext.Categories.AddAsync(category);
        await _appDbContext.SaveChangesAsync();

        return Ok(category);
    }

    [HttpPut("{categoryId}")]
    public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody] CategoryUpdateDto model)
    {
        var category = await _appDbContext.Categories
            .FirstOrDefaultAsync(category => category.Id == categoryId);

        if (category == null)
        {
            return NotFound();
        }

        category.CategoryName = model.Name ?? category.CategoryName;
        category.Description = model.Description ?? category.Description;
        category.ParentId = model.ParentId ?? category.ParentId;


        await _appDbContext.SaveChangesAsync();

        return Ok(category);
    }

    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteCategory(Guid categoryId)
    {
        var category = await _appDbContext.Categories
            .FirstOrDefaultAsync(category => category.Id == categoryId);

        if (category == null)
        {
            return NotFound();
        }

        _appDbContext.Categories.Remove(category);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

}