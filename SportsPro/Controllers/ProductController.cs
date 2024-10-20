using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

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
        [HttpGet]
        public IActionResult Add()
        {
            return View("AddEdit", new Product { ReleaseDate = DateTime.Now });
        }

        // GET THE EDIT PRODUCT VIEW
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View("AddEdit", product);
        }

        // POST - SAVE (AddEdit functionality)
        [HttpPost]
        public IActionResult Save(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                   
                    _context.Products.Add(product);
                }
                else
                {
                    _context.Products.Update(product);
                }

                _context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                if (product.ProductID == 0)
                {
                    StoreDataInViewBag("Add");
                }
                else
                {
                    StoreDataInViewBag("Edit");
                }

                return View("AddEdit", product);
            }
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

        // POST - DELETE THE ITEM
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

        // Viewbag storage for AddEdit
        private void StoreDataInViewBag(string actionType)
        {
            ViewBag.ActionType = actionType; 

        }
    }
}
