using LadyLuxe.Data;
using LadyLuxe.Models;
using LadyLuxe.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LadyLuxe.Controllers
{
    public class HomeController : Controller
    {
        public static List<Cart> _MyCart = new List<Cart>();
        public static int cartCount = 0;

      
        private readonly LadyLuxeDbContext _context;
        
        public HomeController(LadyLuxeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddtoCart(string id) {
            //we check kama item iko available kwa cart we update the quantity
           // return Content(id);
            
            var Product = await _context.Products.FirstOrDefaultAsync(k=>k.Id.ToString()==id);
            // we also need to check kama iyo product bado iko kwa stock..

            if (Product.Quality > 1)
            { 
                var ifexist = _MyCart.FirstOrDefault(k => k.ProductId == id);//returns the element of sequence that satisfies condition
                if (ifexist != null)
                {
                    //Update QTY
                    ifexist.Quantity = ifexist.Quantity + 1;
                }
                else
                {
                    var newrecord = new Cart
                    {
                        ProductId = id,
                        Id = Guid.NewGuid().ToString(),
                        ImageURL = Product.Image,
                        price = Product.Price,
                        Quantity = 1,
                        subtotal = Product.Price,
                        ProductName=Product.ProductName,
                    };

                    _MyCart.Add(newrecord);
                    TempData["Success"] = "Item Added to cart";

                }
            }
            else
            {
                TempData["Err"] = "Item out of stock";

            }
            int total_qty = 0;
            foreach(var items in _MyCart)
            {
                total_qty += items.Quantity;
            }
            cartCount = total_qty;
           

            return RedirectToAction("HomePage");
        }
        public async Task<IActionResult> CartPage()
        {
           
            return View(_MyCart);//ivo tu
        }
        public async Task<IActionResult> HomePage()
        {
            General general = new General();
            general.products = _context.Products.ToList();

            return View(general);
        }
            public async Task<IActionResult> Index()
        {
            General general = new General();
            general.categories =await _context.Category.Where(k => k.DeletedStatus == false).ToListAsync();
            general.sub_Categories = await _context.Sub_Categories.Where(k => k.Deletedstatus == false).ToListAsync();
            

            return View();

            //return Json(general.categories);
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