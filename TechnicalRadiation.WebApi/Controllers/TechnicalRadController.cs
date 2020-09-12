using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechnicalRadiation.Models.Dto;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services;
using TechnicalRadiation.Models.ThirdParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalRadiation.WebApi.Controllers
{
    [Route("api")]
    public class TechnicalRadController : Controller
    {
        private NewsItemService _newsItemService = new NewsItemService();
        private CategoryService _categoryService = new CategoryService();
        private AuthorService _authorService = new AuthorService();

        // Unauthorized routes

        //Get all news

        // https://localhost:5001/api/ [GET]
        [HttpGet]
        [Route("")]

        public IActionResult GetAllNews([FromQuery] int pageSize = 25, int pageNumber = 1)
        {
            return Ok(_newsItemService.GetAllNewsitems(pageNumber, pageSize));
        }

        // Get news by id

        // https://localhost:5001/api/1 [GET]
        [HttpGet]
        [Route("{id:int}")]

        public IActionResult GetNewsById(int id)
        {
            return Ok(_newsItemService.GetNewsItemById(id));
        }

        // Get all categories

        // https://localhost:5001/api/categories [GET]
        [HttpGet]
        [Route("categories")]

        public IActionResult GetAllCategories()
        {
            return Ok(_categoryService.GetAllCategories());
        }

        // Get category by id

        // https://localhost:5001/api/categories [GET]
        [HttpGet]
        [Route("categories/{id:int}")]

        public IActionResult GetCategoryById(int id)
        {
            return Ok(_categoryService.GetCategoryById(id));
        }

        // Get all authors

        // https://localhost:5001/api/authors [GET]
        [HttpGet]
        [Route("authors")]
        
        public IActionResult GetAllAuthors()
        {
            return Ok(_authorService.GetAllAuthors());
        }

        // Get author by id

        // https://localhost:5001/api/authors/1 [GET]
        [HttpGet]
        [Route("authors/{id:int}")]

        public IActionResult GetAuthorById(int id)
        {
            return Ok(_authorService.GetAuthorById(id));
        }

        // Get all news by author id

        // https://localhost:5001/api/authors/1/newsItems [GET]
        [HttpGet]
        [Route("authors/{id:int}/newsItems")]

        public IActionResult GetAllNewsByAuthor(int id)
        {
            return Ok(_newsItemService.GetAllNewsItemsByAuthorId(id));
        }


        // Authorized routes
        // Create, update and delete News

        // https://localhost:5001/api/ [POST]
        [HttpPost]
        [Route("")]

        public IActionResult CreateNewsItem([FromBody] NewsItemInputModel news)
        {
            return Ok();
        }

        // https://localhost:5001/api/1 [PUT]
        [HttpPost]
        [Route("{id:int}")]

        public IActionResult UpdateNewsItem(int id, [FromBody] NewsItemInputModel news)
        {
            return Ok();
        }

        // https://localhost:5001/api/1 [DELETE]
        [HttpDelete]
        [Route("{id:int}")]

        public IActionResult DeleteNewsItem(int id)
        {
            return Ok();
        }

        // Create, update and delete categories


        // https://localhost:5001/api/categories [POST]
        [HttpPost]
        [Route("categories")]

        public IActionResult CreateCategoryItem([FromBody] CategoryInputModel category)
        {
            return Ok();
        }

        // https://localhost:5001/api/categories/1 [PUT]
        [HttpPut]
        [Route("categories/{id:int}")]

        public IActionResult UpdateCategoryItem(int id, [FromBody] CategoryInputModel news)
        {
            return Ok();
        }

        // https://localhost:5001/api/categories/1 [DELETE]
        [HttpDelete]
        [Route("categories/{id:int}")]

        public IActionResult DeleteCategoryItem(int id)
        {
            return Ok();
        }

        // https://localhost:5001/api/categories/1/newsItems/1 [POST]
        [HttpPost]
        [Route("categories/{categoryId:int}/newsItems/{newsItemId:int}")]
        
        public IActionResult CreateCategoryNewsLink(int categoryId, int newsItemId)
        {
            return Ok();
        }

        // Create update and delete authors

        // https://localhost:5001/api/authors [POST]
        [HttpPost]
        [Route("authors")]

        public IActionResult CreateAuthorItem([FromBody] AuthorInputModel category)
        {
            return Ok();
        }

        // https://localhost:5001/api/authors/1 [PUT]
        [HttpPut]
        [Route("authors/{id:int}")]

        public IActionResult UpdateAuthorItem(int id, [FromBody] AuthorInputModel news)
        {
            return Ok();
        }

        // https://localhost:5001/api/authors/1 [DELETE]
        [HttpDelete]
        [Route("authors/{id:int}")]

        public IActionResult DeleteAuthorItem(int id)
        {
            return Ok();
        }
        
        // https://localhost:5001/api/categories/1/newsItems/1 [POST]
        [HttpPost]
        [Route("authors/{categoryId:int}/newsItems/{newsItemId:int}")]
        
        public IActionResult CreateAuthorNewsLink(int categoryId, int newsItemId)
        {
            return Ok();
        }

    }
}