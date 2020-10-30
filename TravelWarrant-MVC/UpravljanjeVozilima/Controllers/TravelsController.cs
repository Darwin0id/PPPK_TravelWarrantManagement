using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using UpravljanjeVozilima.Dal;
using UpravljanjeVozilima.Dal.TravelManager;
using UpravljanjeVozilima.Models;

namespace UpravljanjeVozilima.Controllers
{
    public class TravelsController : Controller
    {
        private readonly ITravelRepo repo;
        public TravelsController() => repo = RepoFactory.GetTravelRepo();
        
        // GET
        public ActionResult Index()
        {
            IList<Travel> listOfTravels = repo.GetAllTravels();
            return View(listOfTravels);
        }
        
        public ActionResult Show(int id)
        {
            Travel travelWarrant = repo.GetTravelWarrant(id);
            return View(travelWarrant);
        }
        
        public ActionResult Delete(int id)
        {
            try
            {
                repo.DeleteTravelWarrant(id);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        
        public ActionResult Add()
        {
            return View();                                
        }
        
        [HttpPost]
        public ActionResult Add(Travel t)
        {
            try
            {
                repo.AddTravelWarrant(t);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}