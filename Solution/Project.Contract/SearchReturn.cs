using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Project.Entities;

namespace Project.Contract
{
    [DataContract]
    public class SearchReturn
    {
        public SearchReturn()
        {
            Data = new List<Vehicle>();
        }
        [DataMember]
        public int CurrentPage { get; set; }
        [DataMember]
        public int PerPageSize { get; set; }
        [DataMember]
        public int TotalPages { get; set; }
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public string Search { get; set; }
        [DataMember]
        public List<Vehicle> Data { get; set; }
    }
}
