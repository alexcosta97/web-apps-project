using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class StopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}