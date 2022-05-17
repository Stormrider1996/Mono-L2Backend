using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleCRUD.Service.Sorting__Filtering__Paging_classes
{
    public class Filtering : IFiltering
    {
        public IQueryable<VehicleMake> MakeName { get; set; }
        public IQueryable<VehicleModel> ModelName { get; set; }
    }
}
