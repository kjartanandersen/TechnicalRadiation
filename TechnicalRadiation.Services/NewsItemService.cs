using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnicalRadiation.Models.Dto;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;
using TechnicalRadiation.Models.ThirdParty;

namespace TechnicalRadiation.Services
{
    public class NewsItemService
    {
        private readonly NewsItemRepository _newsItemRepository = new NewsItemRepository();
        private readonly CategoryRepository _categoryRepository = new CategoryRepository();

        public Envelope<NewsItemDto> GetAllNewsitems(int pageNumber, int pageSize)
        {
            var newsItems = _newsItemRepository.GetAllNewsItems().ToList();
        
            newsItems.ForEach(r => {
                var authorIds = _newsItemRepository.GetAllAuthorIdByNewsItemId(r.Id).ToList();
                var categoryIds = _categoryRepository.GetAllCategoriesIdByNewsItemId(r.Id).ToList();
                
                r.Links.AddReference("self", $"api/{r.Id}");
                r.Links.AddReference("edit", $"api/{r.Id}");
                r.Links.AddReference("delete", $"api/{r.Id}");
                authorIds.ForEach(s => {
                    r.Links.AddReference("authors", $"api/authors/{s}");
                });
                categoryIds.ForEach(s => {
                    r.Links.AddReference("categories", $"api/authors/{s}");
                });
                
            });
            
            
            var envelope = new Envelope<NewsItemDto>(pageNumber, pageSize, newsItems);
            return envelope;
            
        }

        public NewsItemDetailDto GetNewsItemById(int newsItemId)
        {
            var entity = _newsItemRepository.GetNewsItemById(newsItemId);
            var authorIds = _newsItemRepository.GetAllAuthorIdByNewsItemId(entity.Id).ToList();
            var categoryIds = _categoryRepository.GetAllCategoriesIdByNewsItemId(entity.Id).ToList();
            
            entity.Links.AddReference("self", new { href =  $"api/{entity.Id}"});
            entity.Links.AddReference("edit", new { href =  $"api/{entity.Id}"});
            entity.Links.AddReference("delete", new { href =  $"api/{entity.Id}"});
            authorIds.ForEach(s => {
                entity.Links.AddReference("authors", new { href =  $"api/authors/{s}"});
            });
            categoryIds.ForEach(s => {
                entity.Links.AddReference("categories", new { href =  $"api/authors/{s}"});
            });

            return entity;
        }

        public IEnumerable<NewsItemDto> GetAllNewsItemsByAuthorId(int authorId)
        {
            var entity = _newsItemRepository.GetAllNewsItemsByAuthorId(authorId).ToList();

            entity.ForEach(r => {
                var authorIds = _newsItemRepository.GetAllAuthorIdByNewsItemId(r.Id).ToList();
                var categoryIds = _categoryRepository.GetAllCategoriesIdByNewsItemId(r.Id).ToList();
                
                r.Links.AddReference("self", new {href =  $"api/{r.Id}"});
                r.Links.AddReference("edit", new { href =  $"api/{r.Id}"});
                r.Links.AddReference("delete", new { href =  $"api/{r.Id}"});
                authorIds.ForEach(s => {
                    r.Links.AddReference("authors", new { href =  $"api/authors/{s}"});
                });
                categoryIds.ForEach(s => {
                    r.Links.AddReference("categories", new { href =  $"api/authors/{s}"});
                });
                
            });

            return entity;
        }

        public NewsItemDetailDto CreateNewsItem(NewsItemInputModel newsItem)
        {
            return _newsItemRepository.CreateNewsItem(newsItem);
        }

        public void UpdateNewsItemById(NewsItemInputModel newsItem, int id)
        {
            _newsItemRepository.UpdateNewsItemById(newsItem, id);
        }

        public void DeleteNewsItemById(int id)
        {
            _newsItemRepository.DeleteNewsItemById(id);
        }


    }
}