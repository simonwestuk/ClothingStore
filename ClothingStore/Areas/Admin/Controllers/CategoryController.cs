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





        //API
    }
}
