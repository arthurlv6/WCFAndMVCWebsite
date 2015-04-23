using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web.Caching;
using System.Web.Mvc;
using Project.Entities;
using Project.Website.Service;
using Project.Website.ServiceReferenceVehicle;
using Project.Contract;

namespace Project.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var types = ReturnVehicleTypesCache();
            ViewBag.VehicleTypeId = new SelectList(types.Select(d => new { d.Id, d.Name }), "Id", "Name");

            var vehicles = ExceptionHandle((p, c) => p.GetVehiclesByCondition(c), new Proxy(), new SearchReturn() { CurrentPage = 1, PerPageSize = 10, Search = "" });
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

        
        [HttpPost]
        public ActionResult Index(SearchReturn input)
        {
            var types = ReturnVehicleTypesCache();
            ViewBag.VehicleTypeId = new SelectList(types.Select(d => new { d.Id, d.Name }), "Id", "Name");
            input.Search = input.Search ?? "";
            var vehicles= ExceptionHandle((p, c) => p.GetVehiclesByCondition(c), new Proxy(), input);
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
        
        public JsonResult _DeleteConfirm(int id)
        {
            return ExceptionHandle((p, productId) =>
            {
                p.Delete(productId);
                return Json("success", JsonRequestBehavior.AllowGet);
            },new Proxy(),id );
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _AddOrEdit(Vehicle vehicle)
        {
            return ExceptionHandle((p,v) =>
            {
                if (v.Id == 0)
                {
                    p.Create(vehicle);
                }
                else
                {
                    p.Update(vehicle);
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            },new Proxy(),vehicle );
        }
        private IEnumerable<VehicleType> ReturnVehicleTypesCache()
        {
            var cacheKey = string.Format("TypesCache");
            var vechicleTypeCache = HttpContext.Cache[cacheKey] as List<VehicleType>;
            if (vechicleTypeCache == null)
            {
                vechicleTypeCache = ExceptionHandle((p) => p.GetVehicleTypes().ToList(), new Proxy());
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
        #region exception
        private JsonResult ExceptionHandle(Func<Proxy,Vehicle, JsonResult> func, Proxy p, Vehicle v)
        {
            try
            {
                return func.Invoke(p,v);
            }
            catch (Exception)
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
            finally
            {
                p.Close();
            }
        }
        private JsonResult ExceptionHandle(Func<Proxy, int, JsonResult> func, Proxy p, int id)
        {
            try
            {
                return func.Invoke(p, id);
            }
            catch (Exception)
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
            finally
            {
                p.Close();
            }
        }
        private SearchReturn ExceptionHandle(Func<Proxy, SearchReturn, SearchReturn> func, Proxy p, SearchReturn c)
        {
            try
            {
                return func.Invoke(p, c);
            }
            catch (FaultException<CustomizedException> ex)
            {
                throw new Exception(string.Format("Message:{0}; Datetime:{1}; From User:{2}",ex.Detail.Message,ex.Detail.When,ex.Detail.User));
            }
            finally
            {
                p.Close();
            }
        }
        private List<VehicleType> ExceptionHandle(Func<Proxy, List<VehicleType>> func, Proxy p)
        {
            try
            {
                return func.Invoke(p);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                p.Close();
            }
        }
        #endregion
    }
}