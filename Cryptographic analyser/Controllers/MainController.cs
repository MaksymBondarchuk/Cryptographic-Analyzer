using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cryptographic_analyser.Models;

namespace Cryptographic_analyser.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            var m = new TablesGenerator();
            m.GenerateK();
            m.GenerateM();
            m.Generate();

            return View(m);
        }
    }
}