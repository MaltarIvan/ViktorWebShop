using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
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

        public IActionResult Gallery()
        {
            return View();
        }
    }
}
