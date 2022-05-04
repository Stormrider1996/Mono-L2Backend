using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleCRUD.Service
{
    public interface IVehicleService
    {
        Task DeleteMakeByIdAsync(Guid id);
        Task DeleteModelByIdAsync(Guid id);
        Task<VehicleMake> GetVehicleMakeByIdAsync(Guid? id);
        Task<List<VehicleMake>> GetVehicleMakeListAsync();
        Task<VehicleModel> GetVehicleModelByIdAsync(Guid? id);
        Task<List<VehicleModel>> GetVehicleModelListAsync();
        Task InsertVehicleMakeAsync(VehicleMake make);
        Task InsertVehicleModelAsync(VehicleModel model);
        Task UpdateVehicleMakeAsync(VehicleMake make);
        Task UpdateVehicleModelAsync(VehicleModel model);
    }
}