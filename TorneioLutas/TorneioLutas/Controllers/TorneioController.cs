using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TorneioLutas.Controllers
{
    public class TorneioController : Controller
    {
        // GET: Torneio
        public ActionResult Index()
        {
            return View();
        }
    }
}