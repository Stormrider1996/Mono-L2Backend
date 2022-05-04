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
        private readonly VehiclesDbEntities Context;
        private readonly IVehicleService VehicleService;

        public VehicleModelsController(VehiclesDbEntities context, IVehicleService vehicleService)
        {
            Context = context;
            VehicleService = vehicleService;   
        }

        // GET: VehicleModels
        public async Task<ActionResult> Index()
        {
            var vehicleModels = Context.VehicleModels.Include(v => v.VehicleMake);
            return View(await VehicleService.GetVehicleModelListAsync());
        }

        // GET: VehicleModels/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await VehicleService.GetVehicleModelByIdAsync(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        public ActionResult Create()
        {
            ViewBag.MakeId = new SelectList(Context.VehicleMakes, "Id", "Name");
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
                await VehicleService.InsertVehicleModelAsync(vehicleModel);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MakeId = new SelectList(Context.VehicleMakes, "Id", "Name", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await Context.VehicleModels.FindAsync(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakeId = new SelectList(Context.VehicleMakes, "Id", "Name", vehicleModel.MakeId);
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
                    await VehicleService.UpdateVehicleModelAsync(vehicleModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!VehicleModelExists(vehicleModel.Id))
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

            ViewBag.MakeId = new SelectList(Context.VehicleMakes, "Id", "Name", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await VehicleService.GetVehicleModelByIdAsync(id);
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
            await VehicleService.DeleteModelByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleModelExists(Guid id)
        {
            return Context.VehicleModels.Any(e => e.Id == id);
        }
    }
}
