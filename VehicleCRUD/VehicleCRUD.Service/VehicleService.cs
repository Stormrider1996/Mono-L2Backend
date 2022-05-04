using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleCRUD.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly VehiclesDbEntities Context;

        public VehicleService(VehiclesDbEntities context)
        {
            Context = context;
        }

        public async Task<VehicleMake> GetVehicleMakeByIdAsync(Guid? id)
        {
            return await Context.VehicleMakes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<VehicleMake>> GetVehicleMakeListAsync()
        {
            return await Context.VehicleMakes.ToListAsync();
        }

        public async Task InsertVehicleMakeAsync(VehicleMake make)
        {
            Context.VehicleMakes.Add(make);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateVehicleMakeAsync(VehicleMake make)
        {
            var entity = await Context.VehicleMakes.FindAsync(make.Id);
            if (entity != null)
            {
                entity.Name = make.Name;
                entity.Abrv = make.Abrv;
                Context.SaveChanges();
            }

        }

        public async Task DeleteMakeByIdAsync(Guid id)
        {
            var vehicleMake = await Context.VehicleMakes.FindAsync(id);
            var vehicleModel = await Context.VehicleModels.FirstOrDefaultAsync(x => x.MakeId == id);
            if (vehicleModel != null)
            {
                Context.VehicleModels.Remove(vehicleModel);
            }
            Context.VehicleMakes.Remove(vehicleMake);
            await Context.SaveChangesAsync();
        }

        public async Task<VehicleModel> GetVehicleModelByIdAsync(Guid? id)
        {
            return await Context.VehicleModels.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<VehicleModel>> GetVehicleModelListAsync()
        {
            return await Context.VehicleModels.ToListAsync();
        }

        public async Task InsertVehicleModelAsync(VehicleModel model)
        {
            Context.VehicleModels.Add(model);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateVehicleModelAsync(VehicleModel model)
        {
            var entity = await Context.VehicleMakes.FindAsync(model.Id);
            if (entity != null)
            {
                entity.Name = model.Name;
                entity.Abrv = model.Abrv;
                Context.SaveChanges();
            }

        }

        public async Task DeleteModelByIdAsync(Guid id)
        {
            var vehicleModel = await Context.VehicleModels.FindAsync(id);
            Context.VehicleModels.Remove(vehicleModel);
            await Context.SaveChangesAsync();
        }



    }
}
