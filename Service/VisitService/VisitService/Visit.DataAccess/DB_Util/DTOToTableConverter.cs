using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Quivus.Model.QuickInput.Notes;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using Quivus.Model.QuickInput;

namespace Visit.DataAccess.DB_Util
{
    /// <summary>
    /// Author Mansoor
    /// Date 22 Feb 2011
    /// Convert the DTO to the required Table
    /// </summary>

    public class DTOToTableConverter
    {
        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// convert the module notes to TVP
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>
        public static DataTable ModuleNoteDTOListConverter(List<ModuleNoteDTO> moduleNotes)
        {
            DataTable dtModuleNote = null;

            try
            {
                dtModuleNote = new DataTable();
                dtModuleNote.Columns.Add("ModuleNoteID", typeof(System.Int32));
                dtModuleNote.Columns.Add("ModuleTypeID", typeof(System.Int32));
                dtModuleNote.Columns.Add("VisitID", typeof(System.Int32));
                dtModuleNote.Columns.Add("Note", typeof(System.String));
                dtModuleNote.Columns.Add("CreatedByUpdateByUserID", typeof(System.Int32));
                dtModuleNote.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtModuleNote.AcceptChanges();

                ModuleNoteDTO objModuleNoteDTO;

                for (int indexModuleNote = 0; indexModuleNote < moduleNotes.Count; indexModuleNote++)
                {
                    objModuleNoteDTO = moduleNotes[indexModuleNote];
                    DataRow dRow = dtModuleNote.NewRow();

                    dRow["ModuleNoteID"] = objModuleNoteDTO.ModuleNoteId;
                    dRow["ModuleTypeID"] = objModuleNoteDTO.ModuleTypeId;
                    dRow["VisitID"] = objModuleNoteDTO.VisitID;
                    dRow["Note"] = objModuleNoteDTO.Note;
                    dRow["CreatedByUpdateByUserID"] = objModuleNoteDTO.ManagedByUserId;
                    dRow["IsDeleted"] = objModuleNoteDTO.IsDeleted;

                    dtModuleNote.Rows.Add(dRow);
                }
                dtModuleNote.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".ModuleNoteDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtModuleNote;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// convert the Imunization notes to TVP
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <returns></returns>
        public static DataTable ImmunizationDTOListConverter(List<ImmunizationDTO> immunizationList)
        {
            DataTable dtImmunization = null;

            try
            {
                dtImmunization = new DataTable();      

                dtImmunization.Columns.Add("PatientImmunizationID", typeof(System.Int32));
                dtImmunization.Columns.Add("ImmunizationTypeID", typeof(System.Int32));
                dtImmunization.Columns.Add("ImmunizationNote", typeof(System.String));
                dtImmunization.Columns.Add("VisitID", typeof(System.Int32));
                dtImmunization.Columns.Add("Dose", typeof(System.String));
                dtImmunization.Columns.Add("LotNumber", typeof(System.String));

                dtImmunization.Columns.Add("DateAdmin", typeof(System.DateTime));
                dtImmunization.Columns.Add("NextDoseDue", typeof(System.DateTime));                

                dtImmunization.Columns.Add("IsRefusal", typeof(System.Boolean));
                dtImmunization.Columns.Add("RefusalReason", typeof(System.String));
                dtImmunization.Columns.Add("Site", typeof(System.String));
                dtImmunization.Columns.Add("Series", typeof(System.String));
                dtImmunization.Columns.Add("DictationID", typeof(System.Guid));
                dtImmunization.Columns.Add("Manufacturer", typeof(System.String));
                dtImmunization.Columns.Add("Route", typeof(System.String));
                dtImmunization.Columns.Add("ExpiryDate", typeof(System.DateTime));                

                dtImmunization.Columns.Add("CreatedByUpdateByUserID", typeof(System.Int32));
                dtImmunization.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtImmunization.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtImmunization.Columns.Add("IsUpdated", typeof(System.Boolean));
                dtImmunization.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtImmunization.AcceptChanges();

                ImmunizationDTO objImmunizationDTO;

                for (int indexImmunization = 0; indexImmunization < immunizationList.Count; indexImmunization++)
                {
                    objImmunizationDTO = immunizationList[indexImmunization];
                    DataRow dRow = dtImmunization.NewRow();

                    //If inserted, updated, or deleted then add in TVP

                    if (objImmunizationDTO.IsDeleted || objImmunizationDTO.IsDirty || objImmunizationDTO.PatientImmunizationId == 0)
                    {
                        dRow["PatientImmunizationID"] = objImmunizationDTO.PatientImmunizationId;
                        dRow["ImmunizationTypeID"] = objImmunizationDTO.ImmunizationType.ImmunizationTypeId;
                        dRow["VisitID"] = objImmunizationDTO.VisitId;

                        if (!string.IsNullOrEmpty(objImmunizationDTO.Note))
                        {
                            dRow["ImmunizationNote"] = objImmunizationDTO.Note;
                        }
                        else
                        {
                            dRow["ImmunizationNote"] = DBNull.Value;
                        }

                        if (!string.IsNullOrEmpty(objImmunizationDTO.Dose))
                        {
                            dRow["Dose"] = objImmunizationDTO.Dose;
                        }
                        else
                        {
                            dRow["Dose"] = DBNull.Value;
                        }

                        if (!string.IsNullOrEmpty(objImmunizationDTO.LotNumber))
                        {
                            dRow["LotNumber"] = objImmunizationDTO.LotNumber;
                        }
                        else
                        {
                            dRow["LotNumber"] = DBNull.Value;
                        }

                        dRow["IsRefusal"] = objImmunizationDTO.IsRefusal;

                        if (!string.IsNullOrEmpty(objImmunizationDTO.RefusalReason))
                        {
                            dRow["RefusalReason"] = objImmunizationDTO.RefusalReason;
                        }
                        else
                        {
                            dRow["RefusalReason"] = DBNull.Value;
                        }

                        if (!string.IsNullOrEmpty(objImmunizationDTO.Site))
                        {
                            dRow["Site"] = objImmunizationDTO.Site;
                        }
                        else
                        {
                            dRow["Site"] = DBNull.Value;
                        }

                        if (!string.IsNullOrEmpty(objImmunizationDTO.Series))
                        {
                            dRow["Series"] = objImmunizationDTO.Series;
                        }
                        else
                        {
                            dRow["Series"] = DBNull.Value;
                        }

                        if (!string.IsNullOrEmpty(objImmunizationDTO.Manufacturer))
                        {
                            dRow["Manufacturer"] = objImmunizationDTO.Manufacturer;
                        }
                        else
                        {
                            dRow["Manufacturer"] = DBNull.Value;
                        }

                        if (!string.IsNullOrEmpty(objImmunizationDTO.Rout))
                        {
                            dRow["Route"] = objImmunizationDTO.Rout;
                        }
                        else
                        {
                            dRow["Route"] = DBNull.Value;
                        }

                        if (objImmunizationDTO.DateAdmin != null && objImmunizationDTO.DateAdmin > ProjectConstant.DATE_MINIMUM)
                        {
                            dRow["DateAdmin"] = objImmunizationDTO.DateAdmin;
                        }
                        else
                        {
                            dRow["DateAdmin"] = DBNull.Value;
                        }

                        if (objImmunizationDTO.NextDoseDue != null && objImmunizationDTO.NextDoseDue > ProjectConstant.DATE_MINIMUM)
                        {
                            dRow["NextDoseDue"] = objImmunizationDTO.NextDoseDue;
                        }
                        else
                        {
                            dRow["NextDoseDue"] = DBNull.Value;
                        }

                        if (objImmunizationDTO.ExpiryDate != null && objImmunizationDTO.ExpiryDate > ProjectConstant.DATE_MINIMUM)
                        {
                            dRow["ExpiryDate"] = objImmunizationDTO.ExpiryDate;
                        }
                        else
                        {
                            dRow["ExpiryDate"] = DBNull.Value;
                        }

                        if (objImmunizationDTO.DictationID != System.Guid.Empty)
                        {
                            dRow["DictationID"] = objImmunizationDTO.DictationID;
                        }
                        else
                        {
                            dRow["DictationID"] = DBNull.Value;
                        }

                        dRow["CreatedByUpdateByUserID"] = objImmunizationDTO.ManagedBy;

                        if (objImmunizationDTO.IsDirty && objImmunizationDTO.UpdatedAt != null && objImmunizationDTO.UpdatedAt > ProjectConstant.DATE_MINIMUM)
                        {
                            dRow["ModifiedAt"] = objImmunizationDTO.UpdatedAt;
                            dRow["CreatedAt"] = DBNull.Value;
                        }
                        else //if (objImmunizationDTO.CreatedAt != null && objImmunizationDTO.CreatedAt > ProjectConstant.DATE_MINIMUM)
                        {
                            dRow["CreatedAt"] = objImmunizationDTO.CreatedAt;
                            dRow["ModifiedAt"] = DBNull.Value;
                        }                        

                        dRow["IsUpdated"] = objImmunizationDTO.IsDirty;
                        dRow["IsDeleted"] = objImmunizationDTO.IsDeleted;

                        dtImmunization.Rows.Add(dRow);
                    }
                }
                dtImmunization.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".ImmunizationDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtImmunization;
        }
    }
}
