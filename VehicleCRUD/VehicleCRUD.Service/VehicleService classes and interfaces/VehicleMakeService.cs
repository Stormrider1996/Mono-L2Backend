using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PagedList;
using VehicleCRUD.Service.Sorting__Filtering__Paging_classes;

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

        public IPagedList<VehicleMake> VehicleMakeFind(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var vehicleMakes = from v in Context.VehicleMakes select v;
            Filtering name = new Filtering();
            name.MakeName = vehicleMakes.Where(v => v.Name.Contains(searchString));
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
                vehicleMakes = name.MakeName;
            }
            Sorting asc = new Sorting();
            Sorting desc = new Sorting();
            asc.SortOrderMake = vehicleMakes.OrderBy(v => v.Name);
            desc.SortOrderMake = vehicleMakes.OrderByDescending(v => v.Name);
            switch (sortOrder)
            {
                case "name_desc":
                    vehicleMakes = desc.SortOrderMake;
                    break;
                default:
                    vehicleMakes = asc.SortOrderMake;
                    break;
            }
            
            Paging paging = new Paging();
            var pageSize = paging.PageSize;
            var pageNumber = (page ?? paging.PageNumber);
            return vehicleMakes.ToPagedList(pageNumber, pageSize);
            
        }
    
       
    }
    
}
