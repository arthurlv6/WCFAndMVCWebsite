using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    [DataContract]
    public sealed class VehicleType
    {
        public VehicleType()
        {
            Vehicles=new HashSet<Vehicle>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } 
    }
}
