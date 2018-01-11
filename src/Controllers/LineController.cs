using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class LineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}