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
        private bool ModeM { get; set; } = false;
        private bool ModeK { get; set; } = true;

        public void DoCommonStuff(ref TablesGenerator m, string parameter)
        {
            m.GenerateF();
            m.GenerateKorM(m.M, m.SizeM, ModeM);
            m.GenerateKorM(m.K, m.SizeK, ModeK);

            switch (parameter)
            {
                case "mEqual":
                    ModeM = true;
                    m.GenerateKorM(m.M, m.SizeM, ModeM);
                    break;
                case "mRand":
                    ModeM = false;
                    m.GenerateKorM(m.M, m.SizeM, ModeM);
                    break;
                case "kEqual":
                    ModeK = true;
                    m.GenerateKorM(m.K, m.SizeK, ModeK);
                    break;
                case "kRand":
                    ModeK = false;
                    m.GenerateKorM(m.K, m.SizeK, ModeK);
                    break;
            }

            m.GenerateTable4();
            m.GenerateTable5();
            m.GenerateTable6();
        }

        public ActionResult Index(string parameter)
        {
            var m = new TablesGenerator
            {
                SizeM = 6,
                SizeK = 6,
                SizeE = 6,
                ViewName = "Index"
            };

            DoCommonStuff(ref m, parameter);

            return View(m);
        }

        public ActionResult Rectangle(string parameter)
        {
            var m = new TablesGenerator
            {
                SizeM = 7,
                SizeK = 14,
                SizeE = 7,
                ViewName = "Rectangle"
            };
            DoCommonStuff(ref m, parameter);

            return View(m);
        }

        public ActionResult EgM(string parameter)
        {
            var m = new TablesGenerator
            {
                SizeM = 4,
                SizeK = 13,
                SizeE = 5,
                ViewName = "EgM"
            };
            DoCommonStuff(ref m, parameter);

            return View(m);
        }
    }
}