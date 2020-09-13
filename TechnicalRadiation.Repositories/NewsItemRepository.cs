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
    public class NewsItemRepository
    {
        
        //Convert NewsItem to NewsItemDto
        private NewsItemDto ToNewsItemDto(NewsItem item)
        {
            return new NewsItemDto
            {
                Id = item.Id,
                Title = item.Title,
                ImgSource = item.ImgSource,
                ShortDescription = item.ShortDescription,
            };
        }

        //Convert NewsItem to NewsItemDetailDto
        private NewsItemDetailDto ToNewsItemDetailDto(NewsItem item)
        {
            return new NewsItemDetailDto
            {
                Id = item.Id,
                Title = item.Title,
                ImgSource = item.ImgSource,
                ShortDescription = item.ShortDescription,
                LongDescription = item.LongDescription,
                PublishDate = item.PublishDate
            };
        }


        public IEnumerable<NewsItemDto> GetAllNewsItems()
        {
            return DataProvider.NewsItems.Select(r => ToNewsItemDto(r));
        }

        public IEnumerable<int> GetAllAuthorIdByNewsItemId(int newsItemId)
        {
            return DataProvider.NIA.Where(r => r.NewsItemId == newsItemId)
                                                    .Select(r => r.AuthorId);
        }

        public NewsItemDetailDto GetNewsItemById(int newsItenId)
        {
            var entity = DataProvider.NewsItems.FirstOrDefault(r => r.Id == newsItenId);
            if (entity == null) {return null;}
            return ToNewsItemDetailDto(entity);
        }

        public IEnumerable<NewsItemDto> GetAllNewsItemsByAuthorId(int authorId)
        {
            
            var entity =  
                    from newsItem in DataProvider.NewsItems
                    join NIAEnt in DataProvider.NIA 
                    on newsItem.Id equals NIAEnt.NewsItemId
                    where NIAEnt.AuthorId == authorId
                    select ToNewsItemDto(newsItem);

            if (entity == null) {return null;}
            return entity;

        }

        public NewsItemDetailDto CreateNewsItem(NewsItemInputModel newsItem)
        {
            var nextId = DataProvider.NewsItems.Max(r => r.Id)+1;
            var entity = new NewsItem
            {
                Id = nextId,
                Title = newsItem.Title,
                ImgSource = newsItem.ImgSource,
                ShortDescription = newsItem.ShortDescription,
                LongDescription = newsItem.LongDescription,
                PublishDate = newsItem.PublishDate,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            DataProvider.NewsItems.Add(entity);
            return new NewsItemDetailDto 
            {
                Id = entity.Id,
                Title = entity.Title,
                ImgSource = entity.ImgSource,
                ShortDescription = entity.ShortDescription,
                LongDescription = entity.LongDescription,
                PublishDate = entity.PublishDate
            }; 
        }

        public void UpdateNewsItemById(NewsItemInputModel newsItem, int id)
        {
            var entity = DataProvider.NewsItems.FirstOrDefault(r => r.Id == id);

            if (entity == null) {return;}

            entity.Title = newsItem.Title;
            entity.ImgSource = newsItem.ImgSource;
            entity.ShortDescription = newsItem.ShortDescription;
            entity.LongDescription = newsItem.LongDescription;
            entity.PublishDate = newsItem.PublishDate;
            entity.ModifiedDate = DateTime.Now;

        }

        public void DeleteNewsItemById(int id)
        {
            var entity = DataProvider.NewsItems.FirstOrDefault(r => r.Id == id);

            if (entity == null) {return;}
            DataProvider.NewsItems.Remove(entity);

        }

        
        
    }
}