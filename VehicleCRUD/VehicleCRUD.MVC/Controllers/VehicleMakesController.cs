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
    public class VehicleMakesController : Controller
    {
        private readonly VehiclesDbEntities Context;
        private readonly IVehicleService VehicleService;

        public VehicleMakesController(VehiclesDbEntities context, IVehicleService vehicleService)
        {
            Context = context;
            VehicleService = vehicleService;  
        }
        
        // GET: VehicleMakes
        public async Task<ActionResult> Index()
        {
            return View(await VehicleService.GetVehicleMakeListAsync());
        }

        // GET: VehicleMakes/Details/5
        public async Task<ActionResult> Details(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await VehicleService.GetVehicleMakeByNameAsync(name);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleMakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
               await VehicleService.InsertVehicleMakeAsync(vehicleMake);
               return RedirectToAction(nameof(Index));
            }

            return View(vehicleMake);
        }

        // GET: VehicleMakes/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await Context.VehicleMakes.FindAsync(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string name, string abrv, [Bind(Include = "Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (name != vehicleMake.Name) 
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await VehicleService.UpdateVehicleMakeAsync(vehicleMake, name, abrv);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleMakeExists(vehicleMake.Name))
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
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Delete/5
        public async Task<ActionResult> Delete(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await VehicleService.GetVehicleMakeByNameAsync(name);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string name)
        {
            await VehicleService.DeleteMakeByNameAsync(name);
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleMakeExists(string name)
        {
            return Context.VehicleMakes.Any(e => e.Name == name);
        }
    }
}
