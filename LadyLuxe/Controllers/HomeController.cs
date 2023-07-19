using LadyLuxe.Data;
using LadyLuxe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LadyLuxe.Controllers
{
    public class HomeController : Controller
    {

        //tuunde tu constructor yetu hata
        private readonly LadyLuxeDbContext _context;
        
        public HomeController(LadyLuxeDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            General general = new General();
            general.categories =await _context.Category.Where(k => k.DeletedStatus == false).ToListAsync();
            general.sub_Categories = await _context.Sub_Categories.Where(k => k.Deletedstatus == false).ToListAsync();
            //ivo sasa

            return View();

          //  return Json(general.categories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}