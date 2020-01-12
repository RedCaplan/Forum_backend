using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Forum.Core.Common;
using Forum.Core.Model;
using Forum.Core.Model.Enums;
using Forum.Data.Repository.Interfaces;
using Forum.Services.BusinessServices.Interfaces;
using Forum.Web.DTO;
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

        private readonly UserManager<UserAccount> _userManager;
        private readonly ICategoryService _categoryService;

        #endregion

        #region Constructor

        public CategoryController(UserManager<UserAccount> um, ICategoryService categoryService)
        {
            _userManager = um;
            _categoryService = categoryService;
        }

        #endregion

        #region Api methods

        [HttpGet]
        public ActionResult<List<CategoryDTO>> GetAllCategories()
        {
            ICollection<Category> categories = _categoryService.GetCategories();

            return categories.Select(c=> new CategoryDTO(c)).ToList();
        }

        [HttpGet("{name}")]
        public ActionResult<List<Thread>> GetAllThreadsByCategoryName(string name, int page = 0, int pageSize = 10)
        {
            try
            {
                PaginationRequest request = new PaginationRequest {PageSize = pageSize, PageIndex = page};
                return _categoryService.GetAllThreadsByCategoryName(name, request).ToList();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", e.Message);

                return BadRequest(ModelState);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("No entity provided");
                }

                var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByEmailAsync(email);

                if (!user.IsModerator)
                {
                    throw new Exception("You aren't the moderator of this category");
                }

                Category category = new Category(model.Name, model.Description, model.ParentCategoryID, Status.WAITING_FOR_APPROVAL);
                _categoryService.AddCategory(category);

                return CreatedAtAction(nameof(GetAllThreadsByCategoryName),
                    routeValues: new {name = category.LatinName},
                    value: new Category(category.Name, category.Description));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the entity");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{name}.{id}/delete")]
        public ActionResult<CategoryDTO> DeleteCategoryWithNameId(string name, int id)
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _userManager.FindByEmailAsync(email).Result;
                if (!user.IsModerator)
                {
                    throw new Exception("You aren't the moderator of this category");
                }

                _categoryService.DeleteCategory(id);
                Category category = _categoryService.GetCategoryById(id);
                return Ok(new CategoryDTO(category));
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
