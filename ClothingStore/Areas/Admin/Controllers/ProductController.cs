using ClothingStore.Data;
using ClothingStore.Models;
using ClothingStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

     
        //GET
        public async Task<IActionResult> Upsert(int? id)
        {
            ProductModel product = new ProductModel();

            ProductViewModel productVM = new ProductViewModel()
            {
                Product = product,

                CategoryList = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

                TypeList = _db.Types.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
            {
                return View(productVM);
            }

            product = await _db.Products.FindAsync(id.GetValueOrDefault());

            if (product == null)
            {
                return NotFound();
            }

            productVM.Product = product;
            return View(productVM);

        }

        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                if (productVM.Product.Id == 0)
                {
                    await _db.Products.AddAsync(productVM.Product);
                }
                else
                {
                    _db.Products.Update(productVM.Product);
                }
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                productVM.CategoryList = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                if (productVM.Product.Id != 0)
                {
                    productVM.Product = await _db.Products.FindAsync(productVM.Product.Id);
                }
            }
            return View(productVM);
        }

        //API

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _db.Products.Include("Category").Include("Type");

            return Json(new { data = all });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var prod = await _db.Products.FindAsync(id);

            if (prod == null)
            {
                return Json(new { success = false, message = "Error - Not Found." });
            }

            _db.Products.Remove(prod);

            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Item Deleted." });
        }
    }
}
