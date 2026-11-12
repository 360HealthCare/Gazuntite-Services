using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.Clinic
{
    [DataContract]
    public class ClinicBranchDTO : QuivusBaseDTO
    {
        public ClinicBranchDTO()
        {
            //Clinic = new ClinicDTO();
        }

        [DataMember]
        public int ClinicBranchIdSeq { get; set; }

        [DataMember]
        public string ClinicBranchName { get; set; } //LocationName

        //DataMember]
        //public ClinicDTO Clinic { get; set; }         
    }
}