using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using UpravljanjeVozilima.Dal;
using UpravljanjeVozilima.Dal.PersonManager;
using UpravljanjeVozilima.Models;

namespace UpravljanjeVozilima.Controllers
{
    
    public class DriversController : Controller
    {
        private readonly IPersonRepo repo;
        public DriversController() => repo = RepoFactory.GetPersonRepo();

        public ActionResult Index()
        {
            IList<Person> listOfDrivers = repo.GetAllPersons();
            return View(listOfDrivers);
        }

        public ActionResult Edit(int id)
        {
            Person driver = repo.GetPerson(id);
            return View(driver);
        }
        
        [HttpPost]
        public ActionResult Edit(Person p)
        {
            try
            {
                repo.EditPerson(p);
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
        public ActionResult Add(Person p)
        {
            try
            {
                repo.AddPerson(p);
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
                repo.DeletePerson(id);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}