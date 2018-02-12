using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Reflection;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.robotRoutes = GetRoutes("RobotsController");
            return View();
        }

        private string[] GetRoutes(string controller)
        {
            var asm = Assembly.GetExecutingAssembly();
            return asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic &&
                    !method.IsDefined(typeof(NonActionAttribute)) &&
                    method.DeclaringType.Name == controller
                )
                .Select(e => e.Name)
                .OrderBy(e => e)
                .ToArray();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
