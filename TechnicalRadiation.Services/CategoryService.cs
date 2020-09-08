using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnicalRadiation.Models.Dto;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;
using TechnicalRadiation.Models.ThirdParty;

namespace TechnicalRadiation.Services
{
    public class CategoryService
    {
        private readonly NewsItemRepository _newsItemRepository = new NewsItemRepository();
        private readonly CategoryRepository _categoryRepository = new CategoryRepository();

        public List<CategoryDto> GetAllCategories()
        {
            var categories = _categoryRepository.GetAllCategories().ToList();
        
            categories.ForEach(r => {
                
                r.Links.AddReference("self", $"api/{r.Id}");
                r.Links.AddReference("edit", $"api/{r.Id}");
                r.Links.AddReference("delete", $"api/{r.Id}");
            });
            
            
            return categories;
            
        }

    }
}