using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.QuickInput
{
    [DataContract]
    public class AllergyDTO : QuivusBaseDTO
    {   
        //[DataMember]
        //public AllergyType AllergyType
        //{
        //    get { return _allergytype; }
        //    set { _allergytype = value; }
        //}

        //TODO: Allergy Type ? depends upon requirements (how much data will be required)

        [DataMember]
        public int PatientAllergyId { get; set; }

        [DataMember]
        public string Reaction { get; set; }

        [DataMember]
        public DateTime OnSet { get; set; }

        //[DataMember]
        //public DateTime CreatedDate { get; set; }

        [DataMember]
        public int VisitId { get; set; }

        [DataMember]
        public string Note { get; set; }

        //[DataMember]
        //public string IsOld { get; set; }

        [DataMember]
        public DateTime VisitDate { get; set; }        

        [DataMember]
        public Guid DictationID { get; set; }

        //[DataMember]
        //public DateTime DictationTimeStamp { get; set; }        

    }
}
