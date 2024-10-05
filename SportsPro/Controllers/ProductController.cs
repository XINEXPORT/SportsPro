using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Linq;

namespace SportsPro.Controllers
{
    public class ProductController : Controller
    {
        private readonly SportsProContext _context;

        // Constructor
        public ProductController(SportsProContext context)
        {
            _context = context;
        }

        // GET THE PRODUCT LIST
        [Route("products")]
        public IActionResult List()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // GET THE ADD PRODUCT VIEW

        public IActionResult Add()
        {
            return View(new Product { ReleaseDate = DateTime.Now });
        }

        // POST - ADD A PRODUCT
        public IActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {

                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("List");
            }

            return View(product);
        }

        // GET THE EDIT PRODUCT VIEW
        public IActionResult Edit(int id)
        {

            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST - ADD THE EDITED PRODUCT
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
 
            return View(product);
        }

        // GET THE PRODUCT YOU WANT TO DELETE
        public IActionResult Delete(int id)
        {

            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST DELETE THE ITEM
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
