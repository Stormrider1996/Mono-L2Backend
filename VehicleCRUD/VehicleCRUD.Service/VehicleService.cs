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

        public async Task<VehicleMake> GetVehicleMakeByNameAsync(string name)
        {
            return await Context.VehicleMakes.FirstOrDefaultAsync(x => x.Name == name);
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

        public async Task UpdateVehicleMakeAsync(VehicleMake make, string newMakeName, string newAbrv)
        {
            var fetchedName = await Context.VehicleMakes.FirstOrDefaultAsync(x => x.Name == make.Name);
            fetchedName.Name = newMakeName;

            var fetchedAbrv = await Context.VehicleMakes.FirstOrDefaultAsync(x => x.Abrv == make.Abrv);
            fetchedAbrv.Abrv = newAbrv;

        }

        public async Task DeleteMakeByNameAsync(string name)
        {
            var vehicleMake = await Context.VehicleMakes.FindAsync(name);
            Context.VehicleMakes.Remove(vehicleMake);
            await Context.SaveChangesAsync();
        }

        public async Task<VehicleModel> GetVehicleModelByNameAsync(string name)
        {
            return await Context.VehicleModels.FirstOrDefaultAsync(x => x.Name == name);
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

        public async Task UpdateVehicleModelAsync(VehicleModel model, string newModelName, string newAbrv)
        {
            var fetchedName = await Context.VehicleModels.FirstOrDefaultAsync(x => x.Name == model.Name);
            fetchedName.Name = newModelName;

            var fetchedAbrv = await Context.VehicleModels.FirstOrDefaultAsync(x => x.Abrv == model.Abrv);
            fetchedAbrv.Abrv = newAbrv;

        }

        public async Task DeleteModelByNameAsync(string name)
        {
            var vehicleModel = await Context.VehicleModels.FindAsync(name);
            Context.VehicleModels.Remove(vehicleModel);
            await Context.SaveChangesAsync();
        }



    }
}
