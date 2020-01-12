using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Forum.Core.Model;
using Forum.Core.Model.Enums;
using Forum.Data.Repository.Interfaces;
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
    public class ForumsController : ControllerBase
    {
        #region Fields

        private readonly UserManager<UserAccount> _userManager;
        private readonly IThreadRepository _threadRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IGroupRepository _groupRepo;

        #endregion

        #region Constructor

        public ForumsController(UserManager<UserAccount> um, IThreadRepository threadRepo, ICategoryRepository categoryRepo, IGroupRepository groupRepo)
        {
            _userManager = um;
            _threadRepo = threadRepo;
            _categoryRepo = categoryRepo;
            _groupRepo = groupRepo;
        }

        #endregion

        #region Api methods

        [HttpGet]
        public ActionResult<List<CategoryDTO>> GetAllCategories()
        {
            ICollection<Category> categories = _categoryRepo.GetAllCategories();

            return categories.Select(c=> new CategoryDTO(c)).ToList();
        }

        [HttpGet("{name}.{id}")]
        public ActionResult<List<Thread>> GetAllThreadsByCategory(string name, int id, int page = 0, int pageSize = 10)
        {
            try
            {
                return _threadRepo
                    .GetThreadsByCategory(_categoryRepo.GetCategoryByNameId(name, id))
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
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
                var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByEmailAsync(email);

                if (!user.IsModerator)
                {
                    throw new Exception("You aren't the moderator of this category");
                }
                if (model == null)
                {
                    return BadRequest("No entity provided");
                }
                
                Category category = new Category(model.Name, model.Description, model.ParentCategoryID, Status.WAITING_FOR_APPROVAL);
                _categoryRepo.AddCategory(category);
                _categoryRepo.SaveChanges();

                return CreatedAtAction(nameof(GetAllThreadsByCategory),
                    routeValues: new {name = category.LatinName, id = category.ID},
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
                Category category = _categoryRepo.GetCategoryByNameId(name, id);
                var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _userManager.FindByEmailAsync(email).Result;
                var qwe = user.UserGroups.ToList();

                if (!user.IsModerator)
                {
                    throw new Exception("You aren't the moderator of this category");
                }

                _categoryRepo.RemoveCategory(category);
                _categoryRepo.SaveChanges();

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
