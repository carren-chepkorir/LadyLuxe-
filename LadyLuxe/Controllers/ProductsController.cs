using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LadyLuxe.Data;
using LadyLuxe.Models.Domain;
using Microsoft.AspNetCore.Hosting;/// yako ni iweb host..lemi paste code flani kwanza ikiwork i will explain sawa?okay
namespace LadyLuxe.Controllers
{
    public class ProductsController : Controller
    {
        private readonly LadyLuxeDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ProductsController(LadyLuxeDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Products am lost...nko sure ata hapo kwa file huku understand..okay kenye nafanya hapa..nataka kudisplay data to view na viewbag
        //juu nikitumia model siwezi upload file...so am tring to change souce iwe viewbag..okay i got that,..but ntaexplain pos..worry no more
        public async Task<IActionResult> Index()
        {
            ViewBag.Products = await _context.Products.ToListAsync();
            ViewBag.data = _context.Category.ToList();
            ViewBag.datasubcategory = _context.Sub_Categories.ToList();
            return View();
                        
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            // string ProductName,string CategoryId,string Sub_CategoryId,double Price,string Description,double PreviousPrice,int Quality

            //Image upload..
            string wwwRootPath = hostingEnvironment.WebRootPath;
            string filename = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            string extension = Path.GetExtension(product.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;//timestamp is appended to ensure unique file names
            string path = Path.Combine(wwwRootPath + "/Images/", filename);//place tutaeka images 
                                                                           //we upload file now to foldder...
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await product.ImageFile.CopyToAsync(fileStream);
            }
           
            var imagelocation = "https://localhost:7093/Images/" + filename;

            product.Image = imagelocation;
            _context.Add(product);
            await _context.SaveChangesAsync();
            TempData["success"] = product.ProductName + "added successfully";
            return RedirectToAction("Index");
            
              

        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id,
                                              string ProductName,
                                              string CategoryId,
                                              string Sub_CategoryId,
                                              double Price,
                                              string Description,
                                              double PreviousPrice,
                                              int Quality,
                                              string Image)
        {
            if (Id != null) {
                var records = await _context.Products.FirstOrDefaultAsync(k => k.Id.ToString() ==Id);
                if (records != null)
                {
                    records.ProductName = ProductName;
                    records.PreviousPrice = PreviousPrice;
                    records.Quality = Quality;
                    records.Image = Image;
                    records.Description = Description;
                    records.CategoryId = CategoryId;
                    records.Sub_CategoryId = Sub_CategoryId;
                    records.Price = Price;

                    await _context.SaveChangesAsync();
                }
                    TempData["success"] = "Records updated successfully";
                    return (RedirectToAction(nameof(Index)));
                } 
            else
            {


                TempData["Error"] = "Something went wrong ....";
                return (RedirectToAction(nameof(Index)));



            }
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'LadyLuxeDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
