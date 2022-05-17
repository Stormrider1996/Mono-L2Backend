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
using PagedList;
using AutoMapper;
using VehicleCRUD.MVC.ViewModels;
using VehicleCRUD.MVC.Extension_methods;

namespace VehicleCRUD.MVC.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly IVehicleMakeService VehicleMakeService;
        private readonly IMapper Mapper;
        
        public VehicleMakesController(IVehicleMakeService vehicleMakeService, IMapper mapper)
        {
            VehicleMakeService = vehicleMakeService;  
            Mapper = mapper;
            
        }
        
        // GET: VehicleMakes
        public ViewResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var vehicleMakes = VehicleMakeService.VehicleMakeFind(sortOrder, searchString, currentFilter, page);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CurrentFilter = searchString;

            var model = vehicleMakes.ToMappedPagedListMake<VehicleMake, VehicleMakeViewModel>();

            return View(model);
        }

        // GET: VehicleMakes/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await VehicleMakeService.GetVehicleMakeByIdAsync(id);
            var model = Mapper.Map<VehicleMakeViewModel>(vehicleMake);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(model);
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
               await VehicleMakeService.InsertVehicleMakeAsync(vehicleMake);
               return RedirectToAction(nameof(Index));
            }
            
            var model = Mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(model);
        }

        // GET: VehicleMakes/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await VehicleMakeService.GetVehicleMakeByIdAsync(id);
            var model = Mapper.Map<VehicleMakeViewModel>(vehicleMake);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, [Bind(Include = "Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            
            if (id != vehicleMake.Id) 
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await VehicleMakeService.UpdateVehicleMakeAsync(vehicleMake);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleMakeService.VehicleMakeExists(vehicleMake.Id))
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
            var model = Mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(model);
        }

        // GET: VehicleMakes/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await VehicleMakeService.GetVehicleMakeByIdAsync(id);
            
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(model);
        }

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await VehicleMakeService.DeleteMakeByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

       
    }
    
}
