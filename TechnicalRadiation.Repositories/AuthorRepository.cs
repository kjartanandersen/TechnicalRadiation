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
    }
}