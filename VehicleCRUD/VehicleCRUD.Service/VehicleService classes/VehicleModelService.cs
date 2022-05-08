using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleCRUD.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly VehiclesDbEntities Context;

        public VehicleModelService(VehiclesDbEntities context)
        {
            Context = context;
        }

        public async Task<VehicleModel> GetVehicleModelByIdAsync(Guid id)
        {
            return await Context.VehicleModels.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<VehicleModel>> GetVehicleModelListAsync()
        {
            return await Context.VehicleModels.ToListAsync();
        }

        public async Task InsertVehicleModelAsync(VehicleModel model)
        {
            var entity = new VehicleModel
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Abrv = model.Abrv,
                MakeId = model.MakeId,  
            };
            Context.VehicleModels.Add(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateVehicleModelAsync(VehicleModel model)
        {
            var entity = await Context.VehicleModels.FindAsync(model.Id);
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
        public bool VehicleModelExists(Guid id)
        {
            return Context.VehicleModels.Any(e => e.Id == id);
        }
        public IPagedList SortingFilteringPaging(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var vehicleModels = from v in Context.VehicleModels select v;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModels = vehicleModels.Where(v => v.VehicleMake.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    vehicleModels = vehicleModels.OrderByDescending(v => v.Name);
                    break;
                default:
                    vehicleModels = vehicleModels.OrderBy(v => v.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return vehicleModels.ToPagedList(pageNumber, pageSize);
        }

    }
}