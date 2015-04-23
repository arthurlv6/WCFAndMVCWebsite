using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Project.Contract;
using Project.Entities;

namespace Project.Service
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        SearchReturn GetVehiclesByCondition(SearchReturn condition, SortOrder sortOrder = SortOrder.Ascending);

        [OperationContract()]
        void Create(Vehicle vehicle);

        [OperationContract()]
        void Update(Vehicle vehicle);
        [OperationContract()]
        void Delete(int id);

        [OperationContract()]
        List<VehicleType> GetVehicleTypes();


    }
}
