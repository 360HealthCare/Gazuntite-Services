using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model.TypeDTOs;

namespace Quivus.Model.QuickInput
{
    [DataContract]
    public class ImmunizationDTO : QuivusBaseDTO
    {
        [DataMember]
        public ImmunizationTypeDTO ImmunizationType { get; set; }

        [DataMember]
        public int PatientImmunizationId { get; set; }

        [DataMember]
        public string Dose { get; set; }

        [DataMember]
        public string LotNumber { get; set; }

        [DataMember]
        public string Manufacturer { get; set; }

        [DataMember]
        public string Rout { get; set; }

        [DataMember]
        public DateTime DateAdmin { get; set; }

        [DataMember]
        public DateTime NextDoseDue { get; set; }

        [DataMember]
        public DateTime ExpiryDate { get; set; }

        [DataMember]
        public bool IsRefusal { get; set; }

        [DataMember]
        public string RefusalReason { get; set; }

        [DataMember]
        public string Site { get; set; }

        [DataMember]
        public string Series { get; set; }       

        [DataMember]
        public DateTime VisitDate { get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public int VisitId { get; set; }

        [DataMember]
        public Guid DictationID { get; set; }      

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public DateTime UpdatedAt { get; set; }

        [DataMember]
        public int ManagedBy { get; set; }

        [DataMember]
        public bool IsDirty { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        //[DataMember]
        //public DateTime DictationTimeStamp { get; set; }
    }
}
