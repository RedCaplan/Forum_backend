using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Forum.Core.Common;
using Forum.Core.Model;
using Forum.Core.Model.Enums;
using Forum.Services.BusinessServices.Interfaces;
using Forum.Web.Filters;
using Forum.Web.DTO;
using Forum.Web.Filters.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        #endregion

        #region Api methods

        [HttpGet]
        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            IEnumerable<Category> categories = _categoryService.GetCategories();

            IEnumerable<CategoryDTO> categoryDtos =
                _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);

            return categoryDtos;
        }

        [HttpGet("{name}")]
        public ActionResult<IEnumerable<ThreadDTO>> GetAllThreadsByCategoryName(string name, int page = 0, int pageSize = 10)
        {
            try
            {
                PaginationRequest request = new PaginationRequest {PageSize = pageSize, PageIndex = page};

                IEnumerable<Thread> threads = _categoryService.GetAllThreadsByCategoryName(name, request);
                IEnumerable<ThreadDTO> threadDtos = _mapper.Map<IEnumerable<Thread>, IEnumerable<ThreadDTO>>(threads);

                return Ok(threadDtos);
               
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", e.Message);

                return BadRequest(ModelState);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AuthorizeGroup("IsModerator")]
        [HttpPost]
        public ActionResult<CategoryDTO> CreateCategory(CategoryDTO model)
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AuthorizeGroup("IsModerator")]
        [HttpDelete("{name}.{id}/delete")]
        public ActionResult<CategoryDTO> DeleteCategoryWithNameId(string name, int id)
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
