using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Data.Configuration;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class ProductController : Controller
    {
        private Repository<Product> ProductData { get; set; }

        // Constructor
        public ProductController(SportsProContext ctx)
        {
            ProductData = new Repository<Product>(ctx);
        }

        // GET THE PRODUCT LIST
        [Authorize(Roles = "Admin")]
        [Route("products")]
        public ViewResult List()
        {
            var options = new QueryOptions<Product> { OrderBy = p => p.Name };

            var products = ProductData.List(options);
            return View(products);
        }

        // GET THE ADD PRODUCT VIEW
        [Authorize]
        [HttpGet]
        [Route("product/add")]
        public ViewResult Add()
        {
            TempData["SuccessMessage"] = "Product added successfully.";
            return View("AddEdit", new Product { ReleaseDate = DateTime.Now });
        }

        // GET THE EDIT PRODUCT VIEW
        [Authorize]
        [HttpGet]
        [Route("product/edit/{id}")]
        public IActionResult Edit(int id)
        {
            var product = ProductData.Get(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("List");
            }
            return View("AddEdit", product);
        }

        // POST - SAVE (AddEdit functionality)
        [Authorize]
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
                        ProductData.Add(product);
                        TempData["SuccessMessage"] = "Product added successfully.";
                    }
                    else
                    {
                        ProductData.Update(product);
                        TempData["SuccessMessage"] = "Product updated successfully.";
                    }

                    ProductData.Save();
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
        [Authorize]
        [HttpGet]
        [Route("product/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var product = ProductData.Get(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("List");
            }
            return View(product);
        }

        // POST - DELETE THE ITEM
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [Route("product/delete/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = ProductData.Get(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("List");
            }

            try
            {
                ProductData.Delete(product);
                ProductData.Save();
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
