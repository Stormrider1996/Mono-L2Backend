using System.Linq;

namespace VehicleCRUD.Service.Sorting__Filtering__Paging_classes
{
    public interface IFiltering
    {
        IQueryable<VehicleMake> MakeName { get; set; }
        IQueryable<VehicleModel> ModelMake { get; set; }
    }
}