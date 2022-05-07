using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleCRUD.Service
{
    public interface IVehicleModelService
    {
        Task DeleteModelByIdAsync(Guid id);
        Task<VehicleModel> GetVehicleModelByIdAsync(Guid id);
        Task<List<VehicleModel>> GetVehicleModelListAsync();
        Task InsertVehicleModelAsync(VehicleModel model);
        Task UpdateVehicleModelAsync(VehicleModel model);
        bool VehicleModelExists(Guid id);
    }
}