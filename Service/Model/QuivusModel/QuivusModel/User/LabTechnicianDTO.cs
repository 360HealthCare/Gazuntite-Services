using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.User
{
    [DataContract]
    public class LabTechnicianDTO : QuivusBaseDTO
    {
        [DataMember]
        public int LabTechnicianId { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public Guid RecGuid { get; set; }
        [DataMember]
        public bool IsProfileComplete { get; set; }
    }
}
