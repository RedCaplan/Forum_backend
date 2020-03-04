using System;
using System.Collections.Generic;
using AutoMapper;
using Forum.Api.DTO;
using Forum.Api.Filters.Attributes;
using Forum.Core.Common;
using Forum.DAL.Models.Entities;
using Forum.Services.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers
{
    /// <summary>
    /// Api Controller for everything that has to do with categories.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor takes two parameters and is handled automatically by ASP.NET
        /// </summary>
        /// <param name="mapper">Constructor injection done by Services Providers</param>
        /// <param name="categoryService">Constructor injection done by Services Providers</param>
        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        #endregion

        #region Api methods

        /// <summary>
        /// Return all the categories available to user
        /// </summary>
        /// <returns>All the categories available to user that's logged in or anonymous</returns>
        [HttpGet]
        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            IEnumerable<Category> categories = _categoryService.GetCategories();

            IEnumerable<CategoryDTO> categoryDtos =
                _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);

            return categoryDtos;
        }

        /// <summary>
        /// Get the threads with a specified Category (pagination included).
        /// </summary>
        /// <param name="name">Name of category</param>
        /// <param name="paginationRequest">Pagination model</param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public ActionResult<IEnumerable<ThreadDTO>> GetAllThreadsByCategoryName(string name, [FromQuery]PaginationRequest paginationRequest)
        {
            try
            {
                if (paginationRequest != null)
                {
                    paginationRequest = new PaginationRequest() {PageIndex = 0, PageSize = 10};
                }

                IEnumerable<Thread> threads = _categoryService.GetAllThreadsByCategoryName(name, paginationRequest);
                IEnumerable<ThreadDTO> threadDtos = _mapper.Map<IEnumerable<Thread>, IEnumerable<ThreadDTO>>(threads);

                return Ok(threadDtos);
               
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", e.Message);

                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Create category if you have permission, otherwise returns an error
        /// </summary>
        /// <param name="model">The model of the category we want to create</param>
        /// <returns>Returns the path to the newly generated category</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AuthorizeGroup("IsModerator")]
        [HttpPost]
        public ActionResult<CategoryDTO> CreateCategory([FromBody]CategoryDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("No entity provided");
                }

                Category category = _mapper.Map<CategoryDTO, Category>(model);
                _categoryService.AddCategory(category);

                return CreatedAtAction(nameof(GetAllThreadsByCategoryName),
                    routeValues: new {name = category.LatinName},
                    value: _mapper.Map<Category,CategoryDTO>(category));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the entity");
            }
        }

        /// <summary>
        ///  Deletes the specified category
        /// </summary>
        /// <param name="id">The id of the category you want to delete</param>
        /// <returns>If the request was successful (user have moderator rights and category exists) returns an 
        /// ok and the DTO of the category. Otherwise return badrequest</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AuthorizeGroup("IsModerator")]
        [HttpDelete("{id}/delete")]
        public ActionResult<CategoryDTO> DeleteCategoryWithNameId(int id)
        {
            try
            {
                Category category = _categoryService.GetCategoryById(id);
                CategoryDTO categoryDto = _mapper.Map<Category, CategoryDTO>(category);

                _categoryService.DeleteCategory(id);

                return Ok(categoryDto);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", e.Message);

                return BadRequest(ModelState);
            }
        }

        #endregion
    }
}
