using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.Models.ViewModels
{
    public class ProductViewModel
    {
        public ProductModel Product { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
