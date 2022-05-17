namespace VehicleCRUD.Service.Sorting__Filtering__Paging_classes
{
    public interface IPaging
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}