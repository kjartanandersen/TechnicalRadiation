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

        
    }
}