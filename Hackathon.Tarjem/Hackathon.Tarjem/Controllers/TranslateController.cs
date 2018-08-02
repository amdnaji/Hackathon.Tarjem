using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Tarjem.Controllers
{
    public class TranslateController : Controller
    {
        [HttpPost]
        public IActionResult Start([FromBody]string text)
        {
            //Start the trnaslation
            var body = new
            {
                to = "Hi",
                from = "Welcome"
            };
            return Json(body);
        }
    }
}