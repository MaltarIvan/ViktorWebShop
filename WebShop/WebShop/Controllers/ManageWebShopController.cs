using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models.ManageWebShop;
using System.IO;
using WebShop.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using WebShop.Core;

namespace WebShop.Controllers
{
    public class ManageWebShopController : Controller
    {
        private readonly IWebShopRepository _repository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ManageWebShopController(IWebShopRepository repository, IHostingEnvironment hostingEnvironment)
        {
            _repository = repository;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddNewProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(AddNewProductVM addNewProductVM)
        {
            var validImageTypes = new string[]
            {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
            };
            if (ModelState.IsValid)
            {
                if (!validImageTypes.Contains(addNewProductVM.Image.ContentType))
                {
                    ModelState.AddModelError("CustomError", "Please choose either a GIF, JPG or PNG image.");
                    addNewProductVM.Image = null;
                    return View(addNewProductVM);
                }
                else
                {
                    var file = addNewProductVM.Image;
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Content\\products");
                    string fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                    string path = Path.Combine(uploads, fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    Product product = new Product(addNewProductVM.Name, addNewProductVM.Description, addNewProductVM.Price, fileName, addNewProductVM.Quantity);
                    await _repository.AddProductAsync(product);
                    return RedirectToAction("Index");
                }
            }
            return View(addNewProductVM);
        }
    }
}