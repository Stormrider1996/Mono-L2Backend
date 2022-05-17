using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleCRUD.Service.Sorting__Filtering__Paging_classes
{
    public class Paging : IPaging
    {
        protected int pageNumber = 1;

        public int PageNumber
        {
            get
            {
                return pageNumber;
            }
            set
            {
                pageNumber = value;
            }
        }

        public int pageSize = 3;

        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
            }
        }
    }
}
