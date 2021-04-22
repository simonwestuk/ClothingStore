using ClothingStore.Data;
using ClothingStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
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
            CategoryModel category = new CategoryModel();

            if (id == null)
            {
                return View(category);
            }

            category = await _db.Categories.FindAsync(id.GetValueOrDefault());

            if (category == null)
            {
                return NotFound();
            }

            return View(category);

        }


        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    await _db.Categories.AddAsync(category);
                }
                else
                {
                    _db.Categories.Update(category);
                }
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //API

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _db.Categories;
            return Json(new { data = all });
        }
    }
}
