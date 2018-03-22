using PlaylistManager.Models;
using PlaylistManager.Services;
using PlaylistManager.Web.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaylistManager.Web.Controllers
{
    public class CustomErrorsController : Controller
    {
        private CustomExceptionsService service;

        public CustomErrorsController()
        {
            service = new CustomExceptionsService(new DataAccess.UnitOfWork());
        }

        [LoginFilter]
        [AdminFilter]
        public ActionResult Index()
        {
            List<CustomException> exceptions = service.GetAll();
            return View(exceptions);
        }

        [LoginFilter]
        [AdminFilter]
        public ActionResult Details(int id)
        {
            CustomException exception = service.GetById(id);
            return View(exception);
        }
    }
}