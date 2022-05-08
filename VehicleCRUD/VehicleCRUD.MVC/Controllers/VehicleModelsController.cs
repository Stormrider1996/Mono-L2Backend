using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleCRUD.Service;
using System.Data.Entity.Infrastructure;

namespace VehicleCRUD.MVC.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly IVehicleModelService VehicleModelService;
        private readonly IVehicleMakeService VehicleMakeService;
        public VehicleModelsController(IVehicleModelService vehicleModelService, IVehicleMakeService vehicleMakeService)
        {
            VehicleModelService = vehicleModelService;
            VehicleMakeService = vehicleMakeService;    
        }

        // GET: VehicleModels
        public ViewResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var vehicleModels = VehicleModelService.SortingFilteringPaging(sortOrder, searchString, currentFilter, page);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CurrentFilter = searchString;

            return View(vehicleModels);
        }

        // GET: VehicleModels/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await VehicleModelService.GetVehicleModelByIdAsync(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        public async Task<ActionResult> Create()
        {
            List<VehicleMake> vehicleMakes = await VehicleMakeService.GetVehicleMakeListAsync();
            ViewBag.MakeId = new SelectList(vehicleMakes, "Id", "Name");
            return View();
        }

        // POST: VehicleModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Abrv,MakeId")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                await VehicleModelService.InsertVehicleModelAsync(vehicleModel);
                return RedirectToAction(nameof(Index));
            }

            List<VehicleMake> vehicleMakes = await VehicleMakeService.GetVehicleMakeListAsync();
            ViewBag.MakeId = new SelectList(vehicleMakes, "Id", "Name");
            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await VehicleModelService.GetVehicleModelByIdAsync(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            List<VehicleMake> vehicleMakes = await VehicleMakeService.GetVehicleMakeListAsync();
            ViewBag.MakeId = new SelectList(vehicleMakes, "Id", "Name");
            return View(vehicleModel);
        }

        // POST: VehicleModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, [Bind(Include = "Id,Name,Abrv,MakeId")] VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await VehicleModelService.UpdateVehicleModelAsync(vehicleModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!VehicleModelService.VehicleModelExists(vehicleModel.Id))
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        throw;
                    }
                    
                }
                return RedirectToAction(nameof(Index));
            }

            List<VehicleMake> vehicleMakes = await VehicleMakeService.GetVehicleMakeListAsync();
            ViewBag.MakeId = new SelectList(vehicleMakes, "Id", "Name");
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await VehicleModelService.GetVehicleModelByIdAsync(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await VehicleModelService.DeleteModelByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
