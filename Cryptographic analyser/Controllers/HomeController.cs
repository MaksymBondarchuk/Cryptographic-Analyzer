using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cryptographic_analyser.Models;

namespace Cryptographic_analyser.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var m = new TablesGenerator();
            m.GenerateKorM(m.M, m.SizeM, false);
            m.GenerateKorM(m.K, m.SizeK, true);
            m.GenerateF();
            m.GenerateTable4();
            m.GenerateTable5();
            m.GenerateTable6();

            return View(m);
        }
    }
}