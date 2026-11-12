using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.QuickInput.Notes
{
    [DataContract]
    public class ModuleNoteDTO : QuivusBaseDTO
    {
        [DataMember]
        public int ModuleNoteId { get; set; }

        [DataMember]
        public int ModuleTypeId { get; set; }

        [DataMember]
        public int VisitID { get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public int ManagedByUserId { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        //[DataMember]
        //public string NoteType { get; set; }

        //[DataMember]
        //public DateTime VisitDate { get; set; }
    }
}
