using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    [DataContract]
    public class Vehicle
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required]
        public string Plate { get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int VehicleTypeId { get; set; }
        public string Type { get; set; }
    }
}
