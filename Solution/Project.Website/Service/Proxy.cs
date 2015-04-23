using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using Project.Website.ServiceReferenceVehicle;

namespace Project.Website.Service
{
    public class Proxy : ClientBase<IService>, IService
    {
        public Proxy() { }
        public Proxy(string endpointName) : base(endpointName) { }
        public Proxy(Binding binding, string address) : base(binding, new EndpointAddress(address)) { }

       
        public void Create(Entities.Vehicle vehicle)
        {
            Channel.Create(vehicle);
        }

        public Task CreateAsync(Entities.Vehicle vehicle)
        {
            return Channel.CreateAsync(vehicle);
        }

        public void Update(Entities.Vehicle vehicle)
        {
            Channel.Update(vehicle);
        }

        public Task UpdateAsync(Entities.Vehicle vehicle)
        {
            return Channel.UpdateAsync(vehicle);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public Task DeleteAsync(int id)
        {
            return Channel.DeleteAsync(id);
        }

        public SearchReturn GetVehiclesByCondition(SearchReturn condition, SortOrder sortOrder=SortOrder.Ascending)
        {
            return Channel.GetVehiclesByCondition(condition, sortOrder);
        }

        public Task<SearchReturn> GetVehiclesByConditionAsync(SearchReturn condition, SortOrder sortOrder=SortOrder.Ascending)
        {
            return Channel.GetVehiclesByConditionAsync(condition, sortOrder);
        }

        public ObservableCollection<Entities.VehicleType> GetVehicleTypes()
        {
            return Channel.GetVehicleTypes();
        }

        public Task<ObservableCollection<Entities.VehicleType>> GetVehicleTypesAsync()
        {
            return Channel.GetVehicleTypesAsync();
        }
     
    }
}
