using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSocketServerAPI.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";
			return View();
		}
		public JsonResult ActionOne()
		{
			ViewBag.Title = "Home Page";
			return Json("ActionOne", JsonRequestBehavior.AllowGet);
		}
		public JsonResult ActionTwo()
		{
			ViewBag.Title = "Home Page";
			return Json("ActionTwo", JsonRequestBehavior.AllowGet);
		}
		public JsonResult ActionThree()
		{
			ViewBag.Title = "Home Page";
			return Json("ActionThree", JsonRequestBehavior.AllowGet);
		}
		public JsonResult ActionFour()
		{
			ViewBag.Title = "Home Page";
			return Json("ActionFour", JsonRequestBehavior.AllowGet);
		}
		public JsonResult ActionFive()
		{
			ViewBag.Title = "Home Page";
			return Json("ActionFive", JsonRequestBehavior.AllowGet);
		}
		public JsonResult ActionSix()
		{
			ViewBag.Title = "Home Page";
			return Json("ActionSix", JsonRequestBehavior.AllowGet);
		}
		public JsonResult ActionSeven()
		{
			ViewBag.Title = "Home Page";
			return Json("ActionSeven", JsonRequestBehavior.AllowGet);
		}
		public JsonResult ActionEight()
		{
			ViewBag.Title = "Home Page";
			return Json("ActionEight", JsonRequestBehavior.AllowGet);
		}
		public JsonResult ActionNine()
		{
			ViewBag.Title = "Home Page";
			return Json("ActionNine", JsonRequestBehavior.AllowGet);
		}
		public JsonResult ActionTen()
		{
			ViewBag.Title = "Home Page";
			return Json("ActionTen", JsonRequestBehavior.AllowGet);
		}

	}
}
