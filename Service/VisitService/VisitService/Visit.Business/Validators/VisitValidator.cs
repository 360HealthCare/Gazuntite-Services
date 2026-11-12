using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Utility;
using Quivus.Model.Clinic;
using Quivus.Model.TypeDTOs;
using Quivus.Model.Visit;
using Quivus.Model.QuickInput;
using Quivus.Model.QuickInput.Notes;

namespace Visit.Business.Validators
{
    /// <summary>
    /// Author Mansoor
    /// Date 17 Feb 2011
    /// methods to Valiadate the visit related info
    /// </summary>

    public class VisitValidator
    {
        public static bool ValidateVisit(VisitDTO visitDTO, int managedByUserId, ref StringBuilder sb)
        {
            bool flag = true;

            //M TODO: implement it           

            if (visitDTO != null)
            {                
                flag = ValidateUserId(managedByUserId, sb, flag);

                flag = ValidatePatientId(visitDTO.PatientId, sb, flag);

                flag = ValidateClinic(visitDTO.ClinicBranch, sb, flag);

                flag = ValidateDate(visitDTO.VisitDate, sb, flag);

                //not in use
                //flag = ValidateRecordStatus(visitDTO.RecordStatus, sb, flag);

                flag = ValidateUserId(visitDTO.UserId, sb, flag);

                flag = ValidateUserType(visitDTO.UserType, sb, flag);

                flag = ValidateVisitStatus(visitDTO.VisitStatus, sb, flag);

                //M flag = ValidateOwnerId(visitDTO.OwnerId, sb, flag);

                //M flag = ValidateTemplateId(visitDTO.TemplateId, sb, flag);
              
                //not in use
                //flag = ValidateEncounterTypeId(visitDTO.EncounterTypeId, sb, flag);
            }
            else
            {
                sb.Append("\n" + VisitConstant.VISIT_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        public static bool ValidateVisitForSign(VisitDTO visitDTO, int managedByUserId, ref StringBuilder sb)
        {
            bool flag = true;

            //M TODO: implement it           

            if (visitDTO != null)
            {
                flag = ValidateUserId(managedByUserId, sb, flag);

                //flag = ValidateRecordStatus(visitDTO.RecordStatus, sb, flag);

                flag = ValidateUserId(visitDTO.SignedByUserId, sb, flag);

                flag = ValidateUserType(visitDTO.SignedByUserType, sb, flag);

                flag = ValidateDate(visitDTO.SignedDate, sb, flag);
            }
            else
            {
                sb.Append("\n" + VisitConstant.VISIT_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        public static bool ValidateImmunization(List<ImmunizationDTO> immunizationList, int managedByUserId, ref StringBuilder sb)
        {
            bool flag = true;

            //M TODO: implement it           

            if (immunizationList != null)
            {
                foreach (ImmunizationDTO obj in immunizationList)
                {
                    if (obj != null)
                    {
                        flag = ValidateImmunizationType(obj.ImmunizationType, sb, flag);

                        flag = ValidateVisitId(obj.VisitId, sb, flag);                       

                        flag = ValidateUserId(obj.ManagedBy, sb, flag);
                    }
                    else
                    {
                        sb.Append("\n" + VisitConstant.MODULE_NOTE_DTO_INVALID_ERROR_MSG);
                        flag = false;
                    }
                    return flag;
                }  
            }
            else
            {
                sb.Append("\n" + VisitConstant.IMMUNIZATION_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        public static bool ValidateModuleNotes(List<ModuleNoteDTO> moduleNotes, ref StringBuilder sb)
        {
            bool flag = true;                

            if (moduleNotes != null)
            {
                foreach (ModuleNoteDTO obj in moduleNotes)
                {
                    if (obj != null)
                    {
                        flag = ValidateModuleType(obj.ModuleTypeId, sb, flag);

                        flag = ValidateVisitId(obj.VisitID, sb, flag);

                        flag = ValidateNote(obj.Note, sb, flag);

                        flag = ValidateUserId(obj.ManagedByUserId, sb, flag);
                    }
                    else
                    {
                        sb.Append("\n" + VisitConstant.MODULE_NOTE_DTO_INVALID_ERROR_MSG);
                        flag = false;
                    }                    
                }               
            }
            else
            {
                sb.Append("\n" + VisitConstant.MODULE_NOTE_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateUserId(int userId, StringBuilder sb, bool flag)
        {
            if (userId <= 0)
            {
                sb.Append("\n" + VisitConstant.USER_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidatePatientId(int patientId, StringBuilder sb, bool flag)
        {
            if (patientId <= 0)
            {
                sb.Append("\n" + VisitConstant.PATIENT_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateClinic(ClinicBranchDTO clinic, StringBuilder sb, bool flag)
        {
            if (clinic == null || clinic.ClinicBranchIdSeq <= 0)
            {
                sb.Append("\n" + VisitConstant.CLINIC_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateDate(DateTime date, StringBuilder sb, bool flag)
        {
            if (date == null)
            {
                sb.Append("\n" + VisitConstant.VISIT_DATE_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateRecordStatus(string recordStatus, StringBuilder sb, bool flag)
        {
            if (string.IsNullOrEmpty(recordStatus))
            {
                sb.Append("\n" + VisitConstant.VISIT_RECORD_STATUS_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateUserType(UserTypeDTO userType, StringBuilder sb, bool flag)
        {
            if (userType == null || userType.UserTypeId <= 0)
            {
                sb.Append("\n" + VisitConstant.USER_TYPE_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateVisitStatus(string visitStatus, StringBuilder sb, bool flag)
        {
            if (string.IsNullOrEmpty(visitStatus))
            {
                sb.Append("\n" + VisitConstant.VISIT_STATUS_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateOwnerId(int ownerId, StringBuilder sb, bool flag)
        {
            if (ownerId <= 0)
            {
                sb.Append("\n" + VisitConstant.VISIT_OWNER_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateTemplateId(int templateId, StringBuilder sb, bool flag)
        {
            if (templateId <= 0)
            {
                sb.Append("\n" + VisitConstant.VISIT_TEMPLATE_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateEncounterTypeId(int encounterTypeId, StringBuilder sb, bool flag)
        {
            if (encounterTypeId <= 0)
            {
                sb.Append("\n" + VisitConstant.VISIT_TYPE_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateModuleType(int moduleTypeId, StringBuilder sb, bool flag)
        {
            if (moduleTypeId <= 0)
            {
                sb.Append("\n" + VisitConstant.MODULE_TYPE_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateVisitId(int visitId, StringBuilder sb, bool flag)
        {
            if (visitId <= 0)
            {
                sb.Append("\n" + VisitConstant.VISIT_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateNote(string note, StringBuilder sb, bool flag)
        {
            if (string.IsNullOrEmpty(note.Trim()))
            {
                sb.Append("\n" + VisitConstant.NOTE_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

         private static bool ValidateImmunizationType(ImmunizationTypeDTO ImmunizationType, StringBuilder sb, bool flag)
        {
            if (ImmunizationType == null || ImmunizationType.ImmunizationTypeId <= 0)
            {
                sb.Append("\n" + VisitConstant.IMMUNIZATION_TYPE_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }
        
    }
}
