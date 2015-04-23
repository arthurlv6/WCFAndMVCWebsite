using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contract
{
    [DataContract]
    public class CustomizedException
    {
        public CustomizedException(string message)
        {
            Message = message;
            When = DateTime.Now.ToString();
            User = "Admin";
        }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string When { get; set; }
        [DataMember]
        public string User { get; set; }
    }
}
