using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UpravljanjeVozilima.Dal;
using UpravljanjeVozilima.Dal.VehiclesManager;
using UpravljanjeVozilima.Models;

namespace UpravljanjeVozilima.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepo repo;

        public VehiclesController() => repo = RepoFactory.GetVehicleRepo();
        
        // GET
        public ActionResult Index()
        {
            IList<Vehicle> listOfDrivers = repo.GetAllVehicles();
            return View(listOfDrivers);
        }
        
        public ActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Add(Vehicle v)
        {
            try
            {
                repo.AddVehicle(v);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        
        public ActionResult Edit(int id)
        {
            Vehicle vehicle = repo.GetVehicle(id);
            return View(vehicle);
        }
        
        [HttpPost]
        public ActionResult Edit(Vehicle v)
        {
            try
            {
                repo.EditVehicle(v);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        
        public ActionResult Delete(int id)
        {
            try
            {
                repo.DeleteVehicle(id);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}