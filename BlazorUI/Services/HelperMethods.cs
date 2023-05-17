using BlazorUI.DTOModels;

namespace BlazorUI.Services;

public class HelperMethods
{
    // Для показание всех категориий
    public static IEnumerable<SelectCategoryDto> GetCategoryNameAndId(CategoryWithChildrenDto category)
    {
        List<SelectCategoryDto> selectCategories = new();

        var selectCategory = new SelectCategoryDto
        {
            CategoryName = category.CategoryName,
            CategoryId = category.Id
        };

        selectCategories.Add(selectCategory);

        if (category.Children is not null)
        {
            foreach (var item in category.Children)
            {
               var categ = GetCategoryNameAndId(item);
               selectCategories.AddRange(categ);
            }
        }

        return selectCategories;
    }
}