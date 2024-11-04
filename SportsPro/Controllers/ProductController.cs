using System;
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
            TempData["SuccessMessage"] = "Product added successfully.";
            return View("AddEdit", new Product { ReleaseDate = DateTime.Now });
        }

        // GET THE EDIT PRODUCT VIEW
        [HttpGet]
        [Route("product/edit/{id}")]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("List");
            }
            return View("AddEdit", product);
        }

        // POST - SAVE (AddEdit functionality)
        [HttpPost]
        [Route("product/save")]
        public IActionResult Save(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (product.ProductID == 0)
                    {
                        _context.Products.Add(product);
                        TempData["SuccessMessage"] = "Product added successfully.";
                    }
                    else
                    {
                        _context.Products.Update(product);
                        TempData["SuccessMessage"] = "Product updated successfully.";
                    }

                    _context.SaveChanges();
                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Error saving product: {ex.Message}";
                    return View("AddEdit", product);
                }
            }
            else
            {
                TempData["ErrorMessage"] = "There was a problem with your submission.";
                return View("AddEdit", product);
            }
        }

        // GET THE PRODUCT YOU WANT TO DELETE
        [HttpGet]
        [Route("product/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("List");
            }
            return View(product);
        }

        // POST - DELETE THE ITEM
        [HttpPost, ActionName("Delete")]
        [Route("product/delete/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("List");
            }

            try
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Product deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting product: {ex.Message}";
            }

            return RedirectToAction("List");
        }
    }
}
