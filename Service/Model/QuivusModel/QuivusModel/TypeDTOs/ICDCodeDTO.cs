using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class ICDCodeDTO : QuivusBaseDTO
    {
        [DataMember]
        public int IcdCodeIdSeq { get; set; }

        [DataMember]
        public string IcdCodeName { get; set; }

        [DataMember]
        public string IcdCodeDescription { get; set; }

        [DataMember]
        public string IsChronic { get; set; }

        [DataMember]
        public string ProblemType { get; set; }

        [DataMember]
        public string SnomedCode { get; set; }

        [DataMember]
        public string SnomedDescription { get; set; }

        [DataMember]
        public int DiagnosisId { get; set; }
    }
}
