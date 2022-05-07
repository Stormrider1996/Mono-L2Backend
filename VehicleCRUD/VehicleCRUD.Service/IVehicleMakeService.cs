using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleCRUD.Service
{
    public interface IVehicleMakeService
    {
        Task DeleteMakeByIdAsync(Guid id);
        Task<VehicleMake> GetVehicleMakeByIdAsync(Guid? id);
        Task<List<VehicleMake>> GetVehicleMakeListAsync();
        Task InsertVehicleMakeAsync(VehicleMake make);
        Task UpdateVehicleMakeAsync(VehicleMake make);
        bool VehicleMakeExists(Guid id);
    }
}