using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VehicleCRUD.MVC.ViewModels
{
    public class VehicleMakeViewModel 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        
    }
}