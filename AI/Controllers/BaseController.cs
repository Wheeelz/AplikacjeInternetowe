﻿using Microsoft.AspNet.Identity;
using AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AI.Controllers
{
    public class BaseController : Controller
    {
        private SIDb db = new SIDb();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["Sections"] = db.Sections.ToList();
            var user = db.Users.Find(User.Identity.GetUserId());
            if (user != null)
                ViewBag.nsfw = user.AllowNSFW;
            else
                ViewBag.nsfw = false;

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Avatar = db.Users.Find(User.Identity.GetUserId()).AvatarName;
                if (ViewBag.Avatar == null) ViewBag.Avatar = "default.jpg";
            }

            // theme
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                //var user = db.Users.Find(userId);

                if (user.ThemeDark)
                    ViewBag.Style = "dark";
                else
                    ViewBag.Style = "";
        }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult uptadeTheme()
        {

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                if (user.ThemeDark)
                    user.ThemeDark = false;
                else
                    user.ThemeDark = true;

                db.SaveChanges();
            }
            return null;
            
        }
    }
}