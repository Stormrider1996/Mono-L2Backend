using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VehicleCRUD.MVC.ViewModels;
using VehicleCRUD.Service;

namespace VehicleCRUD.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVehicleModelService VehicleModelService;
        private readonly IVehicleMakeService VehicleMakeService;
        public HomeController(IVehicleModelService vehicleModelService, IVehicleMakeService vehicleMakeService)
        {
            VehicleModelService = vehicleModelService;
            VehicleMakeService = vehicleMakeService;
        }
        public async Task<ActionResult> Index()
        {
            var vehicleMakeList = await VehicleMakeService.GetVehicleMakeListAsync();
            var vehicleModelList = await VehicleModelService.GetVehicleModelListAsync();
            VehicleMakeAndModelCount count = new VehicleMakeAndModelCount()
            {
                VehicleMakeCount = vehicleMakeList.Count(),
                VehicleModelCount = vehicleModelList.Count(),
            };
            IEnumerable<VehicleMakeAndModelCount> result = new List<VehicleMakeAndModelCount> { count };
            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}