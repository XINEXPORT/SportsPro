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
        public ViewResult List()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // GET THE ADD PRODUCT VIEW
        [HttpGet]
        [Route("product/add")]
        public ViewResult Add()
        {
            return View("AddEdit", new Product { ReleaseDate = DateTime.Now });
        }

        // GET THE EDIT PRODUCT VIEW
        [HttpGet]
        [Route("product/edit/{id}")]
        public ViewResult Edit(int id) 
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return View("NotFound"); 
            }
            return View("AddEdit", product);
        }

        // POST - SAVE (AddEdit functionality)
        [HttpPost]
        [Route("product/save")]

        public RedirectToActionResult Save(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    _context.Products.Add(product);
                    TempData["SuccessMessage"] = "Product added successfully.";
                }
                else
                {
                    _context.Products.Update(product);
                    TempData["SuccessMessage"] = "Product edited successfully.";
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

                TempData["Error"] = "There was a problem with your submission.";
                return RedirectToAction("AddEdit", product);

            }
        }

        // GET THE PRODUCT YOU WANT TO DELETE
        [HttpGet]
        [Route("product/delete/{id}")]
        public ViewResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return View("NotFound");
            }
            return View(product);
        }

        // POST - DELETE THE ITEM
        [HttpPost, ActionName("Delete")]
        [Route("product/delete/{id}")]
        public ViewResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return View("NotFound");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Product deleted successfully.";

            var products = _context.Products.ToList();
            return View("List", products);
        }

        // Viewbag storage for AddEdit
        private void StoreDataInViewBag(string actionType)
        {
            ViewBag.ActionType = actionType;
        }
    }
}
