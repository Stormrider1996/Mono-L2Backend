using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using L2Backend.WepApi.Models;

namespace L2Backend.WepApi.Controllers
{
    public class VehicleMakesController : ApiController
    {
        private VehiclesDbEntities db = new VehiclesDbEntities();

        // GET: api/VehicleMakes
        [Route("api/AllMakes")]
        public IQueryable<VehicleMake> GetVehicleMakes()
        {
            return db.VehicleMakes;
        }

        // GET: api/VehicleMakes/5
        [ResponseType(typeof(VehicleMake))]
        public async Task<IHttpActionResult> GetVehicleMake(Guid id)
        {
            VehicleMake vehicleMake = await db.VehicleMakes.FindAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return Ok(vehicleMake);
        }

        // PUT: api/VehicleMakes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVehicleMake(Guid id, VehicleMake vehicleMake)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicleMake.Id)
            {
                return BadRequest();
            }

            db.Entry(vehicleMake).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleMakeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/VehicleMakes
        [ResponseType(typeof(VehicleMake))]
        public async Task<IHttpActionResult> PostVehicleMake(VehicleMake vehicleMake)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VehicleMakes.Add(vehicleMake);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VehicleMakeExists(vehicleMake.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vehicleMake.Id }, vehicleMake);
        }

        // DELETE: api/VehicleMakes/5
        [ResponseType(typeof(VehicleMake))]
        public async Task<IHttpActionResult> DeleteVehicleMake(Guid id)
        {
            VehicleMake vehicleMake = await db.VehicleMakes.FindAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            db.VehicleMakes.Remove(vehicleMake);
            await db.SaveChangesAsync();

            return Ok(vehicleMake);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VehicleMakeExists(Guid id)
        {
            return db.VehicleMakes.Count(e => e.Id == id) > 0;
        }
    }
}