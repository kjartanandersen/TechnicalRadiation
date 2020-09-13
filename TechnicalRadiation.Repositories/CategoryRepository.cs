using AutoMapper;
using TechnicalRadiation.Models.Dto;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Data;
using System.Collections.Generic;
using System;
using System.Linq;

namespace TechnicalRadiation.Repositories
{
    public class CategoryRepository
    {
        private readonly string _adminName = DataProvider.GetAdminName();
        //Convert Category to CategoryDto
        private CategoryDto ToCategoryDto(Category item)
        {
            return new CategoryDto
            {
                Id = item.Id,
                Name = item.Name,
                Slug = item.Slug
            };
        }

        private int GetNumberOfNewsItemsByCategoryId(int categoryId)
        {
            return DataProvider.NIC.Where(r => r.CategoryId == categoryId).Select(s => s.CategoryId).Count();
        }


         private CategoryDetailDto ToCategoryDetailDto(Category item)
        {
            return new CategoryDetailDto
            {
                Id = item.Id,
                Name = item.Name,
                Slug = item.Slug,
                NumberOfNewsItems = GetNumberOfNewsItemsByCategoryId(item.Id)    
            };
        }

        public IEnumerable<int> GetAllCategoriesIdByNewsItemId(int newsItemId)
        {
            return DataProvider.NIC.Where(r => r.NewsItemId == newsItemId)
                                                    .Select(r => r.CategoryId);
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            return DataProvider.Categories.Select(r => ToCategoryDto(r));
        }

        public CategoryDetailDto GetCategoryById(int categoryId)
        {
            var entity = DataProvider.Categories.FirstOrDefault(r => r.Id == categoryId);
            if (entity == null) {return null;}
            return ToCategoryDetailDto(entity);
        }

        public int CreateCategory(CategoryInputModel category)
        {
            var nextId = DataProvider.Categories.Max(r => r.Id)+1;
            var slugString = category.Name.ToLower().Replace(' ', '-');
            var entity = new Category
            {
                Id = nextId,
                Name = category.Name,
                Slug = slugString,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ModifiedBy = _adminName
            };
            DataProvider.Categories.Add(entity);
            return nextId;
        }

        public void UpdateCategoryById(CategoryInputModel category, int id)
        {
            var entity = DataProvider.Categories.FirstOrDefault(r => r.Id == id);
            if (entity == null) {return;}
            var slugString = category.Name.ToLower().Replace(' ', '-');

            entity.Name = category.Name;
            entity.Slug = slugString;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = _adminName;
        }

        public void DeleteCategory(int id)
        {
            var entity = DataProvider.Categories.FirstOrDefault(r => r.Id == id);

            if (entity == null) {return;}
            DataProvider.Categories.Remove(entity);

        }

        public void CreateNewsItemCategoryLink(int categoryId, int newsItemId)
        {
            var entity = new NewsItemCategories
            {
                CategoryId = categoryId,
                NewsItemId = newsItemId
            };

            DataProvider.NIC.Add(entity);
        }
    }
}