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
    //ulielewa kujoin tables na UI??
    public class Sub_CategoryController : Controller
    {
        private readonly LadyLuxeDbContext _context;

        public Sub_CategoryController(LadyLuxeDbContext context)
        {
            _context = context;
        }

        // GET: Sub_Category
        public async Task<IActionResult> Index()
        {
              return _context.Sub_Categories != null ? 
                          View(await _context.Sub_Categories.ToListAsync()) :
                          Problem("Entity set 'LadyLuxeDbContext.Sub_Categories'  is null.");
        }

        // GET: Sub_Category/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Sub_Categories == null)
            {
                return NotFound();
            }

            var sub_Category = await _context.Sub_Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sub_Category == null)
            {
                return NotFound();
            }

            return View(sub_Category);
        }

        // GET: Sub_Category/Create
        public async Task<IActionResult> Create(string Id)
        {
            ViewBag.CatId = Id;

            ViewBag.Subcategories = _context.Sub_Categories.Where(k => k.GategoryId == Id && k.Deletedstatus == false).ToList();

             return View();
        }

        // POST: Sub_Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Sub_Category sub_Category)
        {
            if (ModelState.IsValid)
            {
                sub_Category.Id = Guid.NewGuid();
                _context.Add(sub_Category);
                await _context.SaveChangesAsync();
                TempData["success"] = "Sub-category Added successfully";
                return Redirect("~/Sub_Category/Create/" + sub_Category.GategoryId);
            }
            return View(sub_Category);
        }

        // GET: Sub_Category/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Sub_Categories == null)
            {
                return NotFound();
            }

            var sub_Category = await _context.Sub_Categories.FindAsync(id);
            if (sub_Category == null)
            {
                return NotFound();
            }
            return View(sub_Category);
        }

        // POST: Sub_Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,GategoryId,CategoryName,Icon,Deletedstatus")] Sub_Category sub_Category)
        {
            if (id != sub_Category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sub_Category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Sub_CategoryExists(sub_Category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sub_Category);
        }

        // GET: Sub_Category/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Sub_Categories == null)
            {
                return NotFound();
            }

            var sub_Category = await _context.Sub_Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sub_Category == null)
            {
                return NotFound();
            }

            return View(sub_Category);
        }

        // POST: Sub_Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Sub_Categories == null)
            {
                return Problem("Entity set 'LadyLuxeDbContext.Sub_Categories'  is null.");
            }
            var sub_Category = await _context.Sub_Categories.FindAsync(id);
            if (sub_Category != null)
            {
                _context.Sub_Categories.Remove(sub_Category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Sub_CategoryExists(Guid id)
        {
          return (_context.Sub_Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
