using Microsoft.AspNetCore.Mvc;
using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.Model.ProductEntity;
using TorontoShop.Domain.ViewModel.Admin.Product;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TorontoShop.Web.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
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
        public async Task<IActionResult> CreateProductCategory(CreateProductCategoryViewModel productCategoryViewModel, IFormFile image)
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


        [HttpGet]
        public async Task<IActionResult> EditProductCategory(Guid productCategoryId)
        {
            var result = await _productService.GetEditProductCategory(productCategoryId);
            if (result == null) NotFound();
            
                return View(result);
            
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProductCategory(EditProductCategoryViewModel productCategoryViewModel,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.EditProductCategory(productCategoryViewModel, image);
                switch (result)
                {
                    case EditProductCategoryResult.IsExist:
                        TempData[ErrorMessage] = "url تکراری است";
                        break;
                    case EditProductCategoryResult.Success:
                        TempData[SuccessMessage] = "موفقیت آمیز";
                        return RedirectToAction("FilterProductCategories");
                }
            }
            return View(productCategoryViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> FilterProduct(FilterProductViewModel filterProductViewModel)
        {
            filterProductViewModel.State = ProductState.All;
            return View(await _productService.FilterProduct(filterProductViewModel));
        }



        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            TempData["Categories"] = await _productService.GetAllCategory();
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel createProductViewModel, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(createProductViewModel, image);

                switch (result)
                {
                    case CreateProductResult.NotHaveImage:
                        TempData[WarningMessage] = "لطفا برای محصول یک تصویر انتخاب کنید";
                        break;
                    case CreateProductResult.Success:
                        TempData[SuccessMessage] = "عملیات ثبت محصول با موفقیت انجام شد";
                        return RedirectToAction("FilterProduct");
                }
            }
            return View(createProductViewModel);
        }


        [HttpGet]

        public async Task<IActionResult> EditProduct(Guid productId)
        {
           var data= await _productService.GetEditProduct(productId);
            if (data == null)
            {
                return NotFound();
            }
            TempData["Categories"] = await _productService.GetAllCategory();
            return View(data);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(EditProductViewModel editProductViewModel, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.EditProduct(editProductViewModel, image);
                switch (result)
                {
                    case EditProductResult.NotFound:
                        TempData[ErrorMessage] = "محصولی یافت نشد";
                        break;
                    case EditProductResult.CategoryIsNull:
                        TempData[SuccessMessage] = "گروه کالایی وجود ندارد";
                        break;
                    case EditProductResult.Success:
                        return RedirectToAction("FilterProduct");
                }
            }
            TempData["Categories"] = await _productService.GetAllCategory();
            return View(editProductViewModel);
        }

    }
}
