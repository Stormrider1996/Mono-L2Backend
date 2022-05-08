using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace VehicleCRUD.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly VehiclesDbEntities Context;

        public VehicleMakeService(VehiclesDbEntities context)
        {
            Context = context;
        }

        public async Task<VehicleMake> GetVehicleMakeByIdAsync(Guid id)
        {
            return await Context.VehicleMakes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<VehicleMake>> GetVehicleMakeListAsync()
        {
            return await Context.VehicleMakes.ToListAsync();
        }

        public async Task InsertVehicleMakeAsync(VehicleMake make)
        {
            var entity = new VehicleMake
            {
                Id = Guid.NewGuid(),
                Name = make.Name,
                Abrv = make.Abrv,
            };
            Context.VehicleMakes.Add(entity);
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
            Context.VehicleModels.RemoveRange(Context.VehicleModels.Where(x => x.MakeId == id));
            Context.VehicleMakes.Remove(vehicleMake);
            await Context.SaveChangesAsync();
        }

        public bool VehicleMakeExists(Guid id)
        {
            return Context.VehicleMakes.Any(e => e.Id == id);
        }

        public IPagedList SortingFilteringPaging(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var vehicleMakes = from v in Context.VehicleMakes select v;
            
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
                vehicleMakes = vehicleMakes.Where(v => v.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(v => v.Name);
                    break;
                default:
                    vehicleMakes = vehicleMakes.OrderBy(v => v.Name);
                    break;
            }
            
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return vehicleMakes.ToPagedList(pageNumber, pageSize);
        }
    
    }
}
