using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.Clinic
{
    [DataContract]
    public class ClinicDTO : QuivusBaseDTO
    {
        [DataMember]
        public int ClinicIdSeq { get; set; }

        [DataMember]
        public string ClinicName { get; set; }

        [DataMember]
        public string ClinicStatus { get; set; }
    }
}