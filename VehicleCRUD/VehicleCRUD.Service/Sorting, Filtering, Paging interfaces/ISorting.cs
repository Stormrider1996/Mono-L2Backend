using System.Linq;

namespace VehicleCRUD.Service.Sorting__Filtering__Paging_classes
{
    public interface ISorting
    {
        IQueryable<VehicleMake> SortOrderMake { get; set; }
        IQueryable<VehicleModel> SortOrderModel { get; set; }
    }
}