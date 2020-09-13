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
    public class AuthorRepository
    {
        private readonly string _authorName = DataProvider.GetAdminName();
        public IEnumerable<int> GetAllNewsitemIdByAuthorId(int authorId)
        {
            return DataProvider.NIA.Where(r => r.AuthorId == authorId)
                                                    .Select(r => r.NewsItemId);
        }

        private AuthorDto ToAuthorDto(Author item)
        {
            return new AuthorDto
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        private AuthorDetailDto ToAuthorDetailDto(Author item)
        {
            return new AuthorDetailDto
            {
                Id = item.Id,
                Name = item.Name,
                ProfileImgSource = item.ProfileImgSource,
                Bio = item.Bio
            };
        }
        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            return DataProvider.Authors.Select(r => ToAuthorDto(r));
        }

        public AuthorDetailDto GetAuthorById(int id)
        {
            var entity = ToAuthorDetailDto(DataProvider.Authors.FirstOrDefault(r => r.Id == id));
            if (entity == null) {return null;}
            return entity;
        }

        public int CreateAuthor(AuthorInputModel author)
        {
            var nextId = DataProvider.Authors.Max(r => r.Id)+1;

            var entity = new Author
            {
                Id = nextId,
                Name = author.Name,
                ProfileImgSource = author.ProfileImgSource,
                Bio = author.Bio,
                ModifiedBy = _authorName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            DataProvider.Authors.Add(entity);
            return nextId;

        }

        public void UpdateAuthorById(AuthorInputModel author, int id)
        {
            var entity = DataProvider.Authors.FirstOrDefault(r => r.Id == id);
            if (entity == null) {return;}

            entity.Name = author.Name;
            entity.ProfileImgSource = author.ProfileImgSource;
            entity.Bio = author.Bio;
            entity.ModifiedBy = _authorName;
            entity.ModifiedDate = DateTime.Now;
        }

        public void DeleteAuthorById(int id)
        {
            var entity = DataProvider.Authors.FirstOrDefault(r => r.Id == id);
            if (entity == null) {return;}

            DataProvider.Authors.Remove(entity);
        }

        public void CreateNewsItemAuthorLink(int authorId, int newsItemId)
        {
            var entity = new NewsItemAuthors
            {
                AuthorId = authorId,
                NewsItemId = newsItemId
            };

            DataProvider.NIA.Add(entity);
        }
    }
}