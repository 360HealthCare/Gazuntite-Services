using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class SpecialityTypeDTO
    {
        [DataMember]
        public int SpecialityId { get; set; }

        [DataMember]
        public string SpecialityName { get; set; }
    }

}
