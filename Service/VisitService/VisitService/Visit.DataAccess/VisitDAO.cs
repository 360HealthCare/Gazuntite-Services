using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using Quivus.Utility.Logger;
using Quivus.Utility;
using Quivus.Utility.CustomException;

using Visit.DataAccess.DB_Util;

using Quivus.Model.TypeDTOs;
using Quivus.Model.QuickInput;
using Quivus.Model.Visit;
using Quivus.Model.PatientBackground;
using Quivus.Model.QuickInput.Notes;

namespace Visit.DataAccess
{
    /// <summary>
    /// Author Mansoor
    /// Date 17 Feb 2011
    /// </summary>
    public class VisitDAO : BaseDAO
    {
        /// <summary>
        /// Author  Mansoor
        /// Date 17 Feb 2011
        /// To Get List of all Encounter Types
        /// </summary>
        /// <returns></returns>
        public List<EncounterTypeDTO> GetAllEncounterTypes()
        {
            List<EncounterTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.ENCOUNTER_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<EncounterTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(EncounterTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        /// Author  Mansoor
        /// Date 17 Feb 2011
        ///  To Get List soap templates depends upon clinic
        /// </summary>
        /// <returns></returns>
        public List<SoapTemplateTypeDTO> LoadSoapTemplateByClinicBranchId(int clinicBranchId)
        {
            List<SoapTemplateTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.SOAP_TEMPALTE_TYPE_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<SoapTemplateTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name                

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(SoapTemplateTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit/ save        
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns>       
        public int CreateVisit(VisitDTO visitDTO, int managedByUserId)
        {
            int visitID = 0;
            string sProc = VisitConstant.VISIT_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, visitDTO.PatientId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, visitDTO.ClinicBranch.ClinicBranchIdSeq);
                db.AddInParameter(command, "@VisitDate", DbType.DateTime, visitDTO.VisitDate);
                db.AddInParameter(command, "@RecordStatus", DbType.String, visitDTO.RecordStatus);
                db.AddInParameter(command, "@UserID", DbType.Int32, visitDTO.UserId);
                db.AddInParameter(command, "@UserTypeId", DbType.Int32, visitDTO.UserType.UserTypeId);
                db.AddInParameter(command, "@VisitStatus", DbType.String, visitDTO.VisitStatus);
                db.AddInParameter(command, "@GUID", DbType.Guid, visitDTO.RecGuid);
                db.AddInParameter(command, "@DictationID", DbType.Guid, visitDTO.DictationID);

                if (visitDTO.OwnerId > 0)
                {
                    db.AddInParameter(command, "@OwnerId", DbType.Int32, visitDTO.OwnerId);
                }
                else
                {
                    db.AddInParameter(command, "@OwnerId", DbType.Int32, DBNull.Value);
                }
                
                db.AddInParameter(command, "@IsVirtualVisit", DbType.Boolean, visitDTO.IsVirtualVisit);

                if (visitDTO.SignedByUserId > 0)
                {
                    db.AddInParameter(command, "@SignedByUserID", DbType.Int32, visitDTO.SignedByUserId);
                    db.AddInParameter(command, "@SignedByUserTypeId", DbType.Int32, visitDTO.SignedByUserType.UserTypeId);
                    db.AddInParameter(command, "@SignedDate", DbType.DateTime, visitDTO.SignedDate);
                }
                else
                {
                    db.AddInParameter(command, "@SignedByUserID", DbType.Int32, DBNull.Value);
                    db.AddInParameter(command, "@SignedByUserTypeId", DbType.Int32, DBNull.Value);
                    db.AddInParameter(command, "@SignedDate", DbType.DateTime, DBNull.Value);
                }

                if (visitDTO.AppointmentId != 0)
                {
                    db.AddInParameter(command, "@AppointmentID", DbType.Int32, visitDTO.AppointmentId);
                }
                else
                {
                    db.AddInParameter(command, "@AppointmentID", DbType.Int32, DBNull.Value);
                }

                if (visitDTO.TemplateId != 0)
                {
                    db.AddInParameter(command, "@SoapTemplateID", DbType.Int32, visitDTO.TemplateId);
                }
                else
                {
                    db.AddInParameter(command, "@SoapTemplateID", DbType.Int32, DBNull.Value);
                }

                if (visitDTO.EncounterTypeId != 0)
                {
                    db.AddInParameter(command, "@EncounterTypeId", DbType.Int32, visitDTO.EncounterTypeId);
                }
                else
                {
                    db.AddInParameter(command, "@EncounterTypeId", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(command, "@CreatedBy", DbType.Int32, managedByUserId);

                db.AddOutParameter(command, "@VisitIDOUT", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    visitID = int.Parse(db.GetParameterValue(command, "@VisitIDOUT").ToString());
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return visitID;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed       
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns> 
        public bool SignVisit(VisitDTO visitDTO, int managedByUserId)
        {
            bool result = false;
            string sProc = VisitConstant.VISIT_MARK_SIGNED_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, visitDTO.VisitId);
                db.AddInParameter(command, "@SignedByUserID", DbType.Int32, visitDTO.SignedByUserId);
                db.AddInParameter(command, "@SignedByUserTypeId", DbType.Int32, visitDTO.SignedByUserType.UserTypeId);
                db.AddInParameter(command, "@SignedDate", DbType.DateTime, visitDTO.SignedDate);
                db.AddInParameter(command, "@UpdatedBy", DbType.Int32, managedByUserId);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    result = true;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed       
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns> 
        public PatientEncounterResult LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll)
        {
            PatientEncounterResult result = null;
            List<PatientEncounterDTO> encounterList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.VISIT_LOAD_BY_PATIENT_AND_CLINIC_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                //list = new List<EncounterTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@ShowAllRecords", DbType.Boolean, showAll);
                //db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        if (objIReader.Read())
                        {
                            result = new PatientEncounterResult();

                            if (objIReader["TotalRecordsFound"] != DBNull.Value)
                            {
                                result.TotalRecords = int.Parse(objIReader["TotalRecordsFound"].ToString());
                            }

                            if (objIReader.NextResult())
                            {
                                encounterList = DataReaderToDTOConverter.PatientEncounterConverter(objIReader);

                                result.PatientEncounterList = encounterList;
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>        
        public bool ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int managedByUserId)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.IMMUNIZATION_MANAGE_SP;
            DataTable dtImmunization = null;
            SqlConnection connection = null;
            bool isConnectionOpen = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (immunizationList != null && immunizationList.Count > 0)
                    dtImmunization = DTOToTableConverter.ImmunizationDTOListConverter(immunizationList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientImmunizationTVP", dtImmunization);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.IMMUNIZATION_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author  Mansoor
        /// Date 18 Feb 2011
        /// To Get List of all immunization Types
        /// </summary>
        /// <returns></returns>
        public List<ImmunizationTypeDTO> GetAllImmunizationTypes()
        {
            List<ImmunizationTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.IMMUNIZATION_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ImmunizationTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(ImmunizationTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        public List<string> LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> lotNumberList = null;
            string lotNumber = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.LOTNUMBER_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchCriteria);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        lotNumberList = new List<string>();

                        while (objIReader.Read())
                        {
                            lotNumber = objIReader["LotNumber"].ToString();

                            lotNumberList.Add(lotNumber);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return lotNumberList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        public List<string> LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> doseList = null;
            string dose = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.DOSE_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchCriteria);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        doseList = new List<string>();

                        while (objIReader.Read())
                        {
                            dose = objIReader["Dose"].ToString();

                            doseList.Add(dose);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return doseList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization Refusal Reason
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        public List<string> LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> refusalReasonList = null;
            string refusalReason = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.REFUSAL_REASON_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchCriteria);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        refusalReasonList = new List<string>();

                        while (objIReader.Read())
                        {
                            refusalReason = objIReader["RefusalReason"].ToString();

                            refusalReasonList.Add(refusalReason);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return refusalReasonList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization site
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        public List<string> LoadImmunizationSiteByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> refusalReasonList = null;
            string refusalReason = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.SITE_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchCriteria);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        refusalReasonList = new List<string>();

                        while (objIReader.Read())
                        {
                            refusalReason = objIReader["Site"].ToString();

                            refusalReasonList.Add(refusalReason);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return refusalReasonList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization series
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        public List<string> LoadImmunizationSeriesByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> refusalReasonList = null;
            string refusalReason = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.SERIES_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchCriteria);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        refusalReasonList = new List<string>();

                        while (objIReader.Read())
                        {
                            refusalReason = objIReader["Series"].ToString();

                            refusalReasonList.Add(refusalReason);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return refusalReasonList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        public List<string> LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> ManufacturerList = null;
            string manufacturer = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.MANUFACTURER_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchCriteria);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        ManufacturerList = new List<string>();

                        while (objIReader.Read())
                        {
                            manufacturer = objIReader["Manufacturer"].ToString();

                            ManufacturerList.Add(manufacturer);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return ManufacturerList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization rout
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        public List<string> LoadImmunizationRoutByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> RoutList = null;
            string rout = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.ROUT_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchCriteria);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        RoutList = new List<string>();

                        while (objIReader.Read())
                        {
                            rout = objIReader["Route"].ToString();

                            RoutList.Add(rout);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return RoutList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        public List<ImmunizationDTO> LoadImmunizationsByVisitId(int visitId)
        {
            List<ImmunizationDTO> immunizationList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.IMMUNIZATION_LOAD_BY_VISIT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                immunizationList = new List<ImmunizationDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, visitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        immunizationList = DataReaderToDTOConverter.ImmunizationConverter(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return immunizationList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        public List<ModuleNoteDTO> LoadModuleNotesByVisitId(int visitId)
        {
            List<ModuleNoteDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.MODULE_NOTES_LOAD_BY_VISIT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ModuleNoteDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, visitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(ModuleNoteDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// manage module notes, insert/ update/ delete
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns> 
        public bool ManageModuleNotes(List<ModuleNoteDTO> moduleNotes)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.MODULE_NOTES_MANAGE_SP;
            DataTable dtModuleNote = null;
            SqlConnection connection = null;
            bool isConnectionOpen = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (moduleNotes != null && moduleNotes.Count > 0)
                    dtModuleNote = DTOToTableConverter.ModuleNoteDTOListConverter(moduleNotes);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
               
                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpModuleNotes = sqlCommand.Parameters.AddWithValue("@ModuleNotesTVP", dtModuleNote);
                tvpModuleNotes.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpModuleNotes.TypeName = ProjectConstant.MODULE_NOTE_TVP;

                connection.Open();
                isConnectionOpen = true;                

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }
      
        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        public List<ImmunizationDTO> LoadPatientImmunizationsByPatientId(int patientId)
        {
            List<ImmunizationDTO> immunizationList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.IMMUNIZATION_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                immunizationList = new List<ImmunizationDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        immunizationList = DataReaderToDTOConverter.ImmunizationConverter(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return immunizationList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// save visit
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns>       
        public bool SaveVisit(VisitDTO visitDTO, int managedByUserId)
        {
            bool result = false;
            string sProc = VisitConstant.VISIT_UPDATE_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, visitDTO.VisitId);
                db.AddInParameter(command, "@PatientID", DbType.Int32, visitDTO.PatientId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, visitDTO.ClinicBranch.ClinicBranchIdSeq);
                db.AddInParameter(command, "@VisitDate", DbType.DateTime, visitDTO.VisitDate);
                db.AddInParameter(command, "@RecordStatus", DbType.String, visitDTO.RecordStatus);
                db.AddInParameter(command, "@UserID", DbType.Int32, visitDTO.UserId);
                db.AddInParameter(command, "@UserTypeId", DbType.Int32, visitDTO.UserType.UserTypeId);
                db.AddInParameter(command, "@VisitStatus", DbType.String, visitDTO.VisitStatus);
                db.AddInParameter(command, "@GUID", DbType.Guid, visitDTO.RecGuid);
                db.AddInParameter(command, "@DictationID", DbType.Guid, visitDTO.DictationID);
                
                if (visitDTO.OwnerId > 0)
                {
                    db.AddInParameter(command, "@OwnerId", DbType.Int32, visitDTO.OwnerId);
                }
                else
                {
                    db.AddInParameter(command, "@OwnerId", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(command, "@IsVirtualVisit", DbType.Boolean, visitDTO.IsVirtualVisit);

                if (visitDTO.SignedByUserId > 0)
                {
                    db.AddInParameter(command, "@SignedByUserID", DbType.Int32, visitDTO.SignedByUserId);
                    db.AddInParameter(command, "@SignedByUserTypeId", DbType.Int32, visitDTO.SignedByUserType.UserTypeId);
                    db.AddInParameter(command, "@SignedDate", DbType.DateTime, visitDTO.SignedDate);
                }
                else
                {
                    db.AddInParameter(command, "@SignedByUserID", DbType.Int32, DBNull.Value);
                    db.AddInParameter(command, "@SignedByUserTypeId", DbType.Int32, DBNull.Value);
                    db.AddInParameter(command, "@SignedDate", DbType.DateTime, DBNull.Value);
                }

                if (visitDTO.AppointmentId != 0)
                {
                    db.AddInParameter(command, "@AppointmentID", DbType.Int32, visitDTO.AppointmentId);
                }
                else
                {
                    db.AddInParameter(command, "@AppointmentID", DbType.Int32, DBNull.Value);
                }

                if (visitDTO.TemplateId != 0)
                {
                    db.AddInParameter(command, "@SoapTemplateID", DbType.Int32, visitDTO.TemplateId);
                }
                else
                {
                    db.AddInParameter(command, "@SoapTemplateID", DbType.Int32, DBNull.Value);
                }

                if (visitDTO.EncounterTypeId != 0)
                {
                    db.AddInParameter(command, "@EncounterTypeId", DbType.Int32, visitDTO.EncounterTypeId);
                }
                else
                {
                    db.AddInParameter(command, "@EncounterTypeId", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(command, "@ModifiedBy", DbType.Int32, managedByUserId);                

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    result = true;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }
    }
}
