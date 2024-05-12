﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorontoShop.Domain.ViewModel.Admin.Product
{
    public class EditProductCategoryViewModel:CreateProductCategoryViewModel
    {
        public Guid CategoryId { get; set; }

        public string? ImageName { get; set; }
    }

    public enum EditProductCategoryResult
    {
        NotFound,
        Success,
        IsExist
    }
}
