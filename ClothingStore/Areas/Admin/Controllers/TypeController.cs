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
    public class TypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TypeController(ApplicationDbContext db)
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
            TypeModel type = new TypeModel();

            if (id == null)
            {
                return View(type);
            }

            type = await _db.Types.FindAsync(id.GetValueOrDefault());

            if (type == null)
            {
                return NotFound();
            }

            return View(type);

        }


        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TypeModel type)
        {
            if (ModelState.IsValid)
            {
                if (type.Id == 0)
                {
                    await _db.Types.AddAsync(type);
                }
                else
                {
                    _db.Types.Update(type);
                }
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(type);
        }

        //API

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _db.Types;
            return Json(new { data = all });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var type = await _db.Types.FindAsync(id);

            if (type == null)
            {
                return Json(new { success = false, message = "Error - Not Found."});  
            }

            _db.Types.Remove(type);

            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Item Deleted." });
        }

    }
}
