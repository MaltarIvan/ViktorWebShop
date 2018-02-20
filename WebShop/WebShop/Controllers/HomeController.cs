using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WebShop.Core.Repositories;
using WebShop.Core;
using WebShop.Models.Home;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IWebShopRepository _repository;

        public HomeController(IWebShopRepository repository, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _repository = repository;
        }

        public IActionResult Index()
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "Content\\slide_show");
            DirectoryInfo directory = new DirectoryInfo(path);
            List<string> files = directory.GetFiles("*.*").Select(f => f.Name).ToList();
            return View(files);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Products()
        {
            return View();
        }

        public async Task<IActionResult> Gallery()
        {
            List<Picture> pictures = await _repository.GetAllPicturesAsync();
            List<PictureVM> picturesVM = new List<PictureVM>();
            foreach (var item in pictures)
            {
                picturesVM.Add(new PictureVM(item));
            }
            return View(picturesVM);
        }

        public IActionResult Health()
        {
            return View();
        }
    }
}
