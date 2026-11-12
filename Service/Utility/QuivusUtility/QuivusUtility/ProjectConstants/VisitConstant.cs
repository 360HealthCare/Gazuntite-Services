using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quivus.Utility
{
    /// <summary> 
    /// Author Mansoor
    /// Date 17 FEb 2011
    /// This constant class is intended for only Visit specific contants
    /// </summary>
    public class VisitConstant
    {
        #region Stored Procs

        public const string ENCOUNTER_TYPE_LOAD_ALL_SP = "[type.VisitEncounterType_LoadAll]";
        public const string SOAP_TEMPALTE_TYPE_LOAD_BY_CLINIC_ID_SP = "SoapTemplate_LoadByClinicBranch";
        public const string VISIT_INSERT_SP = "Visit_Insert";
        public const string VISIT_MARK_SIGNED_SP = "Visit_MarkAsSigned";
        public const string VISIT_LOAD_BY_PATIENT_AND_CLINIC_SP = "Visit_LoadForViewBackgroundListByClinicBranch";
        public const string VISIT_UPDATE_SP = "Visit_Update";

        public const string IMMUNIZATION_INSERT_SP = "PatientImmunization_Insert";
        public const string IMMUNIZATION_UPDATE_SP = "PatientImmunization_Update";
        public const string IMMUNIZATION_LOAD_ALL_SP = "[type.ImmunizationType_LoadAll]";

        public const string LOTNUMBER_LOAD_BY_CLINIC_ID_SP = "[Immunization.LotNumber_LoadAutoCompleteByClinicBranch]";
        public const string DOSE_LOAD_BY_CLINIC_ID_SP = "[Immunization.Dose_LoadAutoCompleteByClinicBranch]";
        public const string REFUSAL_REASON_LOAD_BY_CLINIC_ID_SP = "[Immunization.RefusalReason_LoadAutoCompleteByClinicBranch]";
        public const string SITE_LOAD_BY_CLINIC_ID_SP = "[Immunization.Site_LoadAutoCompleteByClinicBranch]";
        public const string SERIES_LOAD_BY_CLINIC_ID_SP = "[Immunization.Series_LoadAutoCompleteByClinicBranch]";
        public const string MANUFACTURER_LOAD_BY_CLINIC_ID_SP = "[Immunization.Manufacturer_LoadAutoCompleteByClinicBranch]";
        public const string ROUT_LOAD_BY_CLINIC_ID_SP = "[Immunization.Route_LoadAutoCompleteByClinicBranch]";
        

        public const string IMMUNIZATION_LOAD_BY_VISIT_ID_SP = "PatientImmunization_LoadByVisit";
        public const string IMMUNIZATION_LOAD_BY_PATIENT_ID_SP = "PatientImmunization_LoadByPatient";

        public const string MODULE_NOTES_LOAD_BY_VISIT_ID_SP = "ModuleNote_LoadByVisit";
        public const string MODULE_NOTES_MANAGE_SP = "ModuleNotes_AddUpdateDelete";

        public const string IMMUNIZATION_MANAGE_SP = "PatientImmunization_TVPAddUpdateDelete";


        #endregion

        #region Messages

        public const string VISIT_DTO_INVALID_ERROR_MSG = "* Visit object is Null.";
        public const string USER_ID_INVALID_ERROR_MSG = "* User id is Invalid.";
        public const string PATIENT_ID_INVALID_ERROR_MSG = "* Patient id is Invalid.";
        public const string CLINIC_DTO_INVALID_ERROR_MSG = "* Clinic object is Null.";
        public const string VISIT_DATE_INVALID_ERROR_MSG = "* Visit date is Null.";
        public const string VISIT_RECORD_STATUS_INVALID_ERROR_MSG = "* Visit record status is Invalid.";
        public const string USER_TYPE_INVALID_ERROR_MSG = "* User type is Null.";
        public const string VISIT_STATUS_INVALID_ERROR_MSG = "* Visit status is Invalid.";
        public const string VISIT_OWNER_ID_INVALID_ERROR_MSG = "* Visit owner id is Invalid.";
        public const string VISIT_TEMPLATE_ID_INVALID_ERROR_MSG = "Visit template id is Invalid";
        public const string VISIT_TYPE_ID_INVALID_ERROR_MSG = "Visit type id is Invalid";

        public const string IMMUNIZATION_DTO_INVALID_ERROR_MSG = "* Immunization object is Null.";
         public const string IMMUNIZATION_TYPE_INVALID_ERROR_MSG = "* Immunization Type is Invalid.";


        public const string MODULE_NOTE_DTO_INVALID_ERROR_MSG = "* Module Note object is Null.";
        public const string MODULE_TYPE_ID_INVALID_ERROR_MSG = "Module type id is Invalid";
        public const string NOTE_INVALID_ERROR_MSG = "Note is Invalid";

        public const string VISIT_ID_INVALID_ERROR_MSG = "Visit id is Invalid";

        #endregion

    }
}
