using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class ModuleTypeDTO : QuivusBaseDTO
    {    
        [DataMember]
        public int ModuleTypeId { get; set; }

        [DataMember]
        public string ModuleName { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
