using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

using Quivus.Model.TypeDTOs;

namespace Quivus.Model.User.Patient
{
    /// <summary>
    /// Author Mansoor
    /// Date 18 Jan 2011
    /// </summary>
    [DataContract]
    public class PatientDTO : QuivusBaseDTO
    {
        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public string QuivusId { get; set; }

        [DataMember]
        public int IVR_PINCODE { get; set; }

        [DataMember]
        public bool IsReportingExempt { get; set; }

        [DataMember]
        public bool AllowEmailCommunication { get; set; }

        [DataMember]
        public string MotherMaidenName { get; set; }

        [DataMember]
        public string FatherName { get; set; }

        [DataMember]
        public string SpouseName { get; set; }

        [DataMember]
        public string MedicareNumber { get; set; }

        [DataMember]
        public bool IsProfileComplete { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public RaceTypeDTO RaceType { get; set; }

        [DataMember]
        public EthinicityTypeDTO EthinicityType { get; set; }

        [DataMember]
        public OccupationTypeDTO OccupationType { get; set; }

        [DataMember]
        public MaritalStatusTypeDTO MaritalStatusType { get; set; }

        [DataMember]
        public StatusTypeDTO StatusType { get; set; }

        //M front end will send NULL/TRUE/FALSE
        [DataMember]
        public string AlcoholStatus { get; set; }

        [DataMember]
        public string AlcoholStatusData { get; set; }

        //M front end will send NULL/TRUE/FALSE
        [DataMember]
        public string DrugStatus { get; set; }

        [DataMember]
        public string DrugStatusData { get; set; }

        [DataMember]
        public string EmployerName { get; set; }

        [DataMember]
        public Guid RecGuid { get; set; }

        [DataMember]
        public SmokingTypeDTO SmokingType { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }
    }
}
