using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UpravljanjeVozilima.Models;

namespace UpravljanjeVozilima
{
    public class MvcApplication : System.Web.HttpApplication
    {
        
            
        protected void Application_Start()
        {
            //LOAD DATA
            VehicleTypeConfig.RegisterVehiclesTypeList();
            VehicleBrandConfig.RegisterVehiclesBrandList();
            TravelStatusConfig.RegisterTravelStatusList();
            
            //REGISTER
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}