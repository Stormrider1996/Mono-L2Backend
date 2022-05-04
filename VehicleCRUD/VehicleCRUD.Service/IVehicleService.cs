using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleCRUD.Service
{
    public interface IVehicleService
    {
        Task DeleteMakeByNameAsync(string name);
        Task DeleteModelByNameAsync(string name);
        Task<VehicleMake> GetVehicleMakeByNameAsync(string name);
        Task<List<VehicleMake>> GetVehicleMakeListAsync();
        Task<VehicleModel> GetVehicleModelByNameAsync(string name);
        Task<List<VehicleModel>> GetVehicleModelListAsync();
        Task InsertVehicleMakeAsync(VehicleMake make);
        Task InsertVehicleModelAsync(VehicleModel model);
        Task UpdateVehicleMakeAsync(VehicleMake make, string newMakeName, string newAbrv);
        Task UpdateVehicleModelAsync(VehicleModel model, string newModelName, string newAbrv);
    }
}