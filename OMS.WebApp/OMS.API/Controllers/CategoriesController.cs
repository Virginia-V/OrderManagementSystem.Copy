using OMS.Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;

namespace OMS.API.Controllers
{ 
    [Route("api/categories")]
    public class CategoriesController : AppBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService, 
            ILogger<CategoriesController> logger, 
            IStringLocalizer<Resource> stringLocalizer) : base(logger, stringLocalizer)
        {
            _categoryService = categoryService;
        }

        [HttpGet]

        public Task<IActionResult> GetCategoriesAsync()
        {
            return HandleAsync(() => _categoryService.GetCategoriesAsync());
        }
    }
}
