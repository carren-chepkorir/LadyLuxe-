using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LadyLuxe.Data;
using LadyLuxe.Models.Domain;

namespace LadyLuxe.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly LadyLuxeDbContext _context;

        public CategoriesController(LadyLuxeDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
              return _context.Category != null ? 
                          View(await _context.Category.ToListAsync()) :
                          Problem("Entity set 'LadyLuxeDbContext.Category'  is null.");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string CategoryName,string Icon,bool DeletedStatus)
        {
            var newdata = new Category
            {
                CategoryName = CategoryName,
                Icon = Icon,
                DeletedStatus = false,
                Id = Guid.NewGuid()
            };

            
            
                
                _context.Add(newdata);
                await _context.SaveChangesAsync();
                TempData["Success"] = CategoryName + " Category added successfully ";
                return RedirectToAction(nameof(Index));
            
         
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id,string CategoryName, string Icon, bool DeletedStatus)
        {
            if (Id != null)
            {
                var record = await _context.Category.FirstOrDefaultAsync(k=>k.Id.ToString()==Id);
                if (record != null)
                {
                    // Update values..
                    record.CategoryName = CategoryName;
                    record.Icon = Icon;
                    record.DeletedStatus = DeletedStatus;
                    await _context.SaveChangesAsync();
                }
                TempData["Success"] = "Record Updated sucessfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "Something went wrong..";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Category == null)
            {
                return Problem("Entity set 'LadyLuxeDbContext.Category'  is null.");
            }
            var category = await _context.Category.FindAsync(id);
            if (category != null)
            {
                _context.Category.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            TempData["Error"] = "Category Removed successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(Guid id)
        {
          return (_context.Category?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
