using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.TypeDTOs;
using Quivus.Model;

namespace Quivus.Model.User
{
    [DataContract]
    public class DoctorDTO : QuivusBaseDTO
    {
        public DoctorDTO()
        {
            SpecialityType = new SpecialityTypeDTO();
        }
        [DataMember]
        public int DoctorId { get; set; }

        [DataMember]
        public string DoctorName { get; set; }

        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public SpecialityTypeDTO SpecialityType { get; set; }
        [DataMember]
        public string NPI { get; set; }
        [DataMember]
        public string UPIN { get; set; }
        [DataMember]
        public string DAE { get; set; }
        [DataMember]
        public string TPI { get; set; }
        [DataMember]
        public string DentistLicenseNumber { get; set; }
        [DataMember]
        public string PriorAuthorization { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public Guid RecGuid { get; set; }

        [DataMember]
        public string WorkingDays { get; set; }
        [DataMember]
        public string WorkStartTime { get; set; }
        [DataMember]
        public string WorkEndTime { get; set; }
        [DataMember]
        public bool IsProfileComplete { get; set; }
    }
}
