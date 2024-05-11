using Microsoft.AspNetCore.Mvc;
using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.Model.ProductEntity;
using TorontoShop.Domain.ViewModel.Admin.Product;

namespace TorontoShop.Web.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> FilterProductCategories(ProductCategoryFilterViewModel filter)
        {
            return View(await _productService.ProductCategoryFilter(filter));
        }

        [HttpGet]
        public IActionResult CreateProductCategory()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProductCategory(CreateProductCategoryViewModel productCategoryViewModel,
            IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProductCategory(productCategoryViewModel,image);
                switch (result)
                {
                    case  CreateProductCategoryResult.IsExist:
                        TempData[ErrorMessage] = "url تکراری است";
                        break;
                    case CreateProductCategoryResult.Success:
                        TempData[SuccessMessage] = "با موفقیت ثبت شد";
                        return RedirectToAction("FilterProductCategories");
                }
            }
            return View(productCategoryViewModel);
        }

    }
}
