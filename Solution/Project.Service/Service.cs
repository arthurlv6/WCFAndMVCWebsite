using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using Project.Contract;
using Project.Data;
using Project.Entities;

namespace Project.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class Service : IService, IDisposable
    {
        //readonly ProjectDbContext _context = new ProjectDbContext();
        readonly VehicleEntities _context = new VehicleEntities();
        
        public SearchReturn GetVehiclesByCondition(SearchReturn condition, SortOrder sortOrder = SortOrder.Ascending)
        {

            if(condition.CurrentPage<1)
                throw new FaultException<CustomizedException>(new CustomizedException("The current page number is not allowed less than 1"), "A exception message.");
            if(condition.PerPageSize<1)
                throw new FaultException<CustomizedException>(new CustomizedException("The perpage size must greater than 1"), "A exception message.");
            var data = _context.sp_vehicleData().ToList();
            var result = new SearchReturn();
            int total = data.Count(d => d.Name.Contains(condition.Search));
            var totalPages = total / condition.PerPageSize + (total % condition.PerPageSize > 0 ? 1 : 0);
            if (condition.CurrentPage > totalPages)
            {
                condition.CurrentPage = 1;
            }
            List<sp_vehicleData_Result> orderData;
            if (sortOrder == SortOrder.Ascending)
            {
                orderData =
                    data
                        .Where(d => d.Name.Contains(condition.Search))
                        .OrderBy(d=>d.Name)
                        .Skip((condition.CurrentPage - 1) * condition.PerPageSize)
                        .Take(condition.PerPageSize)
                        .ToList();
            }
            else
            {
                orderData =
                    data
                        .Where(d => d.Name.Contains(condition.Search))
                        .OrderByDescending(d=>d.Name)
                        .Skip((condition.CurrentPage - 1) * condition.PerPageSize)
                        .Take(condition.PerPageSize)
                        .ToList();
            }
            result.Data = orderData.Select(vehicle => new Vehicle() { Name = vehicle.Name, Description = vehicle.Description, Id = vehicle.Id, Plate = vehicle.Plate,  VehicleTypeId = vehicle.VehicleTypeId,Type = vehicle.Type}).ToList();
            result.TotalPages = totalPages == 0 ? 1 : totalPages;
            result.PerPageSize = condition.PerPageSize;
            result.CurrentPage = condition.CurrentPage;
            return result;
        }

       
        public void Create(Vehicle vehicle)
        {
            _context.sp_add(vehicle.Plate, vehicle.Name, vehicle.Description, vehicle.Type,vehicle.VehicleTypeId);
                _context.SaveChanges();
        }

        public void Update(Vehicle vehicle)
        {
            _context.sp_update(id: vehicle.Id, plate: vehicle.Plate, name: vehicle.Name,
                description: vehicle.Description, type: vehicle.Type);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.sp_del(id);
            _context.SaveChanges();
        }
        public List<VehicleType> GetVehicleTypes()
        {
            var data= _context.sp_vehicleTypeData().ToList();
            var output = new List<VehicleType>();
            foreach (var item in data)
            {
                output.Add(new VehicleType(){Id = item.Id,Name = item.Name});
            }
            return output;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        //private void Initalize()
        //{
        //     for (int i = 0; i < 5; i++)
        //     {
        //         var vt = new VehicleType();
        //         vt.Name = "Type" + i;
        //         for (int j = 0; j < 3; j++)
        //         {
        //             vt.Vehicles.Add(new Vehicle(){Name = "Vehicle"+i.ToString()+j.ToString(),Description = "There are some words",Plate = i.ToString()+j.ToString()});
        //         }
        //         _context.VehicleTypes.Add(vt);
        //     }
        //     _context.SaveChanges();
        //}
        //public List<VehicleType> GetVehicleTypes()
        //{
        //    return _context.VehicleTypes.ToList();
        //}
    }
}
