using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;

namespace Quivus.Model.Communication
{
    [DataContract]
    public class EmailDTO : QuivusBaseDTO
    {

        [DataMember]
        public int UserId { get; set; }
        [DataMember]

        public int ClinicBranchID { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string RecipientEmail { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public byte[] Attachment { get; set; }

        [DataMember]
        public string AttachedFileName { get; set; }
    }
}
