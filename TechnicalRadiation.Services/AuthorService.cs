using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnicalRadiation.Models.Dto;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;
using TechnicalRadiation.Models.ThirdParty;

namespace TechnicalRadiation.Services
{
    public class AuthorService
    {
        private readonly AuthorRepository _authorRepository = new AuthorRepository();


        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            var entity = _authorRepository.GetAllAuthors().ToList();

            entity.ForEach(r => {
                var newsItemIds = _authorRepository.GetAllNewsitemIdByAuthorId(r.Id).ToList();
                r.Links.AddReference("self", new { href = $"api/authors/{r.Id}" });
                r.Links.AddReference("edit", new { href = $"api/authors/{r.Id}" });
                r.Links.AddReference("delete", new { href = $"api/authors/{r.Id}" });
                r.Links.AddReference("newsItems", new { href = $"api/authors/{r.Id}/newsItems" });
                newsItemIds.ForEach(s => {
                    r.Links.AddReference("newsItemsDetailed", new { href = $"api/{s}" });
                });
            });

            return entity;
        }

        public AuthorDetailDto GetAuthorById(int id)
        {
            var entity = _authorRepository.GetAuthorById(id);
            var newsItemIds = _authorRepository.GetAllNewsitemIdByAuthorId(id).ToList();

            entity.Links.AddReference("self", new {href = $"api/authors/{entity.Id}"});
            entity.Links.AddReference("edit", new {href = $"api/authors/{entity.Id}"});
            entity.Links.AddReference("delete", new {href = $"api/authors/{entity.Id}"});
            entity.Links.AddReference("newsItems", new {href = $"api/authors/{entity.Id}/newsItems"});
            newsItemIds.ForEach(s => {
                entity.Links.AddReference("newsItemsDetailed", new {href = $"api/{s}"});
            });
            return entity;
        }

    }
}