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
            var m = new TablesGenerator
            {
                SizeM = 6,
                SizeK = 6,
                SizeE = 6
            };
            m.GenerateKorM(m.M, m.SizeM, false);
            m.GenerateKorM(m.K, m.SizeK, true);
            m.GenerateF();
            m.GenerateTable4();
            m.GenerateTable5();
            m.GenerateTable6();

            return View(m);
        }

        public ActionResult Rectangle()
        {
            var m = new TablesGenerator
            {
                SizeM = 7,
                SizeK = 14,
                SizeE = 7
            };
            m.GenerateKorM(m.M, m.SizeM, false);
            m.GenerateKorM(m.K, m.SizeK, true);
            m.GenerateF();
            m.GenerateTable4();
            m.GenerateTable5();
            m.GenerateTable6();

            return View(m);
        }

        public ActionResult EgM()
        {
            var m = new TablesGenerator
            {
                SizeM = 4,
                SizeK = 13,
                SizeE = 5
            };
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