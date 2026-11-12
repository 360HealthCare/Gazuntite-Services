using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.QuickInput
{
    [DataContract]
    public class ChronicMedicationDTO : QuivusBaseDTO
    {
        [DataMember]
        public int PatientChronicMedicationId { get; set; }

        //[DataMember]
        //public ChronicMedicationNote ChronicMedicationNote
        //{ get; set; }

        [DataMember]
        public DateTime DiscontinueDate { get; set; }

        //[DataMember]
        //public MedicationType MedicationTypeChronic  { get; set; }

        [DataMember]
        public string ChronicMedicationTypeStatus { get; set; }

        [DataMember]
        public string Sig { get; set; }

        [DataMember]
        public string Quantity { get; set; }

        [DataMember]
        public string NumberOfRefills { get; set; }

        [DataMember]
        public DateTime OnSet { get; set; }

        [DataMember]
        public string RecentRefills { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int VisitId { get; set; }

        //[DataMember]
        //public string IsOld { get; set; }

        [DataMember]
        public DateTime VisitDate { get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public string MedicationStrength { get; set; }

        [DataMember]
        public Guid DictationID { get; set; }

        //[DataMember]
        //public DateTime DictationTimeStamp { get; set; }
    }
}
