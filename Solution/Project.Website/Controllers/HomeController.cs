using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web.Caching;
using System.Web.Mvc;
using Project.Entities;
using Project.Website.ServiceAccess;
using Project.Contract;

namespace Project.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVehicleContainer _vc;
        public HomeController(IVehicleContainer iVehicleContainer)
        {
            _vc = iVehicleContainer;
        }
        public ActionResult Index()
        {
            try
            {
                var types = ReturnVehicleTypesCache();
                ViewBag.VehicleTypeId = new SelectList(types.Select(d => new { d.Id, d.Name }), "Id", "Name");
                var vehicles = _vc.GetVehicles(new SearchReturn() { CurrentPage = 1, PerPageSize = 10, Search = "" });
                if (types != null)
                {
                    foreach (var vehicle in vehicles.Data)
                    {
                        var typeText = types.FirstOrDefault(d => d.Id == vehicle.VehicleTypeId);
                        vehicle.Type = typeText == null ? "Unknow" : typeText.Name;
                    }
                }
                return View(vehicles);
            }
            catch (Exception)
            {
                throw new Exception("Fail to get data.");
            }
           
        }
        [HttpPost]
        public ActionResult Index(SearchReturn input)
        {
            try
            {
                var types = ReturnVehicleTypesCache();
                ViewBag.VehicleTypeId = new SelectList(types.Select(d => new { d.Id, d.Name }), "Id", "Name");
                input.Search = input.Search ?? "";
                var vehicles = _vc.GetVehicles(input);
                if (types != null)
                {
                    foreach (var vehicle in vehicles.Data)
                    {
                        var typeText = types.FirstOrDefault(d => d.Id == vehicle.VehicleTypeId);
                        vehicle.Type = typeText == null ? "Unknow" : typeText.Name;
                    }
                }
                return View(vehicles);
            }
            catch (Exception)
            {
                throw new Exception("fail to get data by conditions.");
            }
           
        }
        public JsonResult _DeleteConfirm(int id)
        {
            try
            {
                _vc.DeleteVehicle(id);
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _AddOrEdit(Vehicle vehicle)
        {
            try
            {
                _vc.AddOrEdit(vehicle);
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
        private IEnumerable<VehicleType> ReturnVehicleTypesCache()
        {
            var cacheKey = string.Format("TypesCache");
            var vechicleTypeCache = HttpContext.Cache[cacheKey] as List<VehicleType>;
            if (vechicleTypeCache == null)
            {
                vechicleTypeCache = _vc.GetVehicleTypes();
                HttpContext.Cache.Add(
                    cacheKey,
                    vechicleTypeCache,
                    null,
                    DateTime.Now.AddSeconds(6),
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.Normal, null
                    );
            }
            return vechicleTypeCache;
        }
    }
}