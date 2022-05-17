using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VehicleCRUD.MVC.ViewModels
{
    public class VehicleModelViewModel 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public Guid MakeId { get; set; }
        public virtual VehicleMakeViewModel VehicleMake { get; set; }
    }
}