using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Contract;
using Project.Entities;
using Project.Website.ServiceAccess.Service;

namespace Project.Website.ServiceAccess
{
    public interface IVehicleContainer
    {
        List<VehicleType> GetVehicleTypes();
        SearchReturn GetVehicles(SearchReturn search);
        void DeleteVehicle(int id);
        void AddOrEdit(Vehicle vehicle);
    }

    public class VehicleContainer:BaseContainer, IVehicleContainer
    {
        public List<VehicleType> GetVehicleTypes()
        {
            return GetDataExceptionHandle((p) => p.GetVehicleTypes().ToList(), new Proxy("BasicHttpBinding_IService"));
        }

        public SearchReturn GetVehicles(SearchReturn search)
        {
            return GetDataByConditionExceptionHandle((p, c) => p.GetVehiclesByCondition(c), new Proxy(), search);
        }

        public void DeleteVehicle(int id)
        {
            DeleteExceptionHandle((p, vehicleId) => p.Delete(vehicleId), new Proxy(), id);
        }

        public void AddOrEdit(Vehicle vehicle)
        {
            AddOrEditExceptionHandle((p, v) =>
            {
                if (v.Id==0)
                {
                    p.Create(vehicle);
                }
                else
                {
                    p.Update(vehicle);
                }
            }, new Proxy(), vehicle);
        }
    }
}
