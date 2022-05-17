using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleCRUD.MVC.ViewModels;

namespace VehicleCRUD.MVC.Extension_methods
{
    public static class Extensions
    {
        public static IPagedList<VehicleMakeViewModel> ToMappedPagedListMake<VehicleMake, VehicleMakeViewModel>(this IPagedList<VehicleMake> list)
        {

            var config = new MapperConfiguration(cfg =>

            cfg.CreateMap<VehicleMake, VehicleMakeViewModel>()

            );
            var mapper = new Mapper(config);
            IMapper Mapper = mapper;

            IEnumerable<VehicleMakeViewModel> sourceList = Mapper.Map<IEnumerable<VehicleMake>, IEnumerable<VehicleMakeViewModel>>(list);
            IPagedList<VehicleMakeViewModel> pagedResult = new StaticPagedList<VehicleMakeViewModel>(sourceList, list.GetMetaData());
            return pagedResult;

        }

        public static IPagedList<VehicleModelViewModel> ToMappedPagedListModel<VehicleModel, VehicleModelViewModel>(this IPagedList<VehicleModel> list)
        {

            var config = new MapperConfiguration(cfg =>

            cfg.CreateMap<VehicleModel, VehicleModelViewModel>()

            );
            var mapper = new Mapper(config);
            IMapper Mapper = mapper;

            IEnumerable<VehicleModelViewModel> sourceList = Mapper.Map<IEnumerable<VehicleModel>, IEnumerable<VehicleModelViewModel>>(list);
            IPagedList<VehicleModelViewModel> pagedResult = new StaticPagedList<VehicleModelViewModel>(sourceList, list.GetMetaData());
            return pagedResult;

        }
    }
}