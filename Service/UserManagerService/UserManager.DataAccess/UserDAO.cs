using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using Quivus.Utility.Logger;
using UserManagerService.DataAccess.DB_Util;
using Quivus.Model.User;
using Quivus.Model.Common;
using Quivus.Model.TypeDTOs;
using Quivus.Utility;
using Quivus.Utility.CustomException;
using Quivus.Model.SearchEngine;
using System.Configuration;
using System.Reflection;
using Quivus.Model.QuickLink;
using Quivus.Model.Search;
using Quivus.Model.User.Patient;
using Quivus.Utility.Utilities;
using UserManagerService.DataAccess;

using Quivus.Model.Communication;
using Quivus.Model.RecentPatient;
using Quivus.Model.Common;
using Quivus.Model.Image;



namespace UserManagerService.DataAccess
{
    public class UserDAO : BaseDAO
    {
        /// <summary>
        /// Author Mansoor Ud-Din
        /// Created Date 12 jan 2011
        /// This Method call the DB proc to Verfiy the User Credential
        /// 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>

        public LoginDTO AutheticateUser(string userName, string userPassword, string hashPassword)
        {
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_DEFAULT_AUTHENTICATE_SP;
            LoginDTO loginDTO = null;

            bool IsValidUser = false;
            bool IsUserLocked = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@UserLoginID", DbType.String, userName);
                db.AddInParameter(command, "@UserPassword", DbType.String, userPassword);
                db.AddInParameter(command, "@UserHashedPassword", DbType.String, hashPassword);

                using (objIReader = db.ExecuteReader(command))
                {
                    // will always return this row to check if the user is authenticated
                    if (objIReader.Read())
                    {
                        IsValidUser = bool.Parse(objIReader["IsValidUser"].ToString());
                        IsUserLocked = bool.Parse(objIReader["IsUserLockedOut"].ToString());
                    }

                    if (IsValidUser && !IsUserLocked)
                    {
                        loginDTO = DataReaderToDTOConverter.LoginDTOConverter(objIReader);
                    }
                    else if (!IsValidUser)
                    {
                        throw new AuthenticationException(ProjectConstant.INVALID_LOGIN_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidCredentails);
                    }
                    else if (IsUserLocked)
                    {
                        throw new AuthenticationException(ProjectConstant.ACCOUNT_lOCKED_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.AccountLocked);
                    }
                }
            }

            catch
            {
                throw;
            }
            finally
            {
                //objIReader.Close();
            }

            return loginDTO;
        }

        /// <summary>
        /// Author Mansoor Ud-Din
        /// Created Date 14 jan 2011
        /// This Method call the DB proc to log the user login, for audit 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="UserType"></param>
        /// <param name="ClinicBranch"></param>
        /// <param name="isSucessFull"></param>

        public void LogLoginHistroy(int userId, int userType, int clinicBranch, bool isSucessFull)
        {
            try
            {
                QuivusLogger.Info("Calling SP:: " + UserConstant.USER_DEFAULT_LOGIN_LOG_SP);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(UserConstant.USER_DEFAULT_LOGIN_LOG_SP);

                db.AddInParameter(command, "@UserID", DbType.Int32, userId);
                db.AddInParameter(command, "@UserTypeID", DbType.Int32, userType);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranch);
                db.AddInParameter(command, "@IsLoginSuccessful", DbType.Boolean, isSucessFull);

                db.ExecuteNonQuery(command);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Default Search Engine List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        public List<SearchEngineDTO> LoadUserSearchEngineByLocation(int UserId, int ClinicBranchId)
        {
            List<SearchEngineDTO> SearchengineList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_LOCATION_SEARCHENGINE_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ProviderUserID", DbType.Int32, UserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    SearchengineList = DataReaderToDTOConverter.SearchEngineLocationConverter(objIReader);
                }
            }

            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return SearchengineList;
        }

        /// <summary>
        /// Author Mansoor
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Quick Links List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        public List<QuickLinkDTO> LoadUserDefaultQuickLinksByLocation(int UserId, int ClinicBranchId)
        {
            List<QuickLinkDTO> quickLinkList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_DEFUALT_QUICK_LINK_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ProviderUserID", DbType.Int32, UserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    quickLinkList = DataReaderToDTOConverter.QuickLinkConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return quickLinkList;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load All Search Engine List
        /// New Method As we might need different functionality In future
        /// </summary>
        public List<SearchEngineDTO> LoadAllSearchEngines()
        {
            List<SearchEngineDTO> SearchengineList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.ALL_SEARCHENGINE_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    SearchengineList = DataReaderToDTOConverter.SearchEngineAllConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return SearchengineList;
        }


        //Change by Mansoor
        //Date 11 Feb 2011
        //ExecuteReader is replaced with ExecuteNonQuery

        public int SetSearchEngineAsDefault(int ProviderUserID, int ClinicBranchID, int UserSearchEngineID)
        {
            int UpdateResult = 0;
            string sProc = UserConstant.SET_DEFAULT_SEARCHENGINE_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ProviderUserID", DbType.Int32, ProviderUserID);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchID);
                db.AddInParameter(command, "@UserSearchEngineID", DbType.Int32, UserSearchEngineID);


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Jan 2011
        /// Load user quick links from DB based on location
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>

        public List<ContactSearchDTO> LoadUserDefaultQuickLinksByLocation(int ClinicBranchId)
        {
            List<ContactSearchDTO> ContactSearchList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.CLINIC_USER_LIST_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    ContactSearchList = DataReaderToDTOConverter.ContactSearchDTOConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return ContactSearchList;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Jan 17 2011
        /// Method Used to MAnage the Search engine for user against Location
        /// Changed by Mansoor
        /// Date 11 Feb 2011
        /// connection was not closed
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="SearchEngineTypeIDs"></param>
        /// <param name="UserSearchEngineID"></param>
        /// <param name="IsDefault"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public int ManageUserSearchEngine(int ProviderUserID, int ClinicBranchID, List<int> SearchEngineTypeIDs, int CreatedBy)
        {
            bool isConnectionOpen = false;
            int UpdateResult = 0;
            string sProc = UserConstant.ADD_USER_SEARCHENGINE_SP;

            DataTable dtSearchEngineType = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                dtSearchEngineType = DTOToTableConverter.SearchengineTypeListConverter(SearchEngineTypeIDs);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ProviderUserID", ProviderUserID);
                sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchID);

                SqlParameter tvpMedical = sqlCommand.Parameters.AddWithValue("@SearchEngineTypeIDs", dtSearchEngineType);
                tvpMedical.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpMedical.TypeName = ProjectConstant.SEARCH_ENGINE_TYPE_TVP;

                sqlCommand.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }

            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                {
                    connection.Close();
                }
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Jan 2011
        /// </summary>
        /// <param name="requestedUserId"></param>
        /// <param name="clinicBranchID"></param>
        /// <param name="subject"></param>
        /// <param name="receiptEmail"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public int LogEmailSend(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message, DateTime mailSentAt)
        {
            int UpdateResult = 0;
            string sProc = UserConstant.USER_DEFAULT_EMAIL_LOG_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.COMMUNICATION_DB); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID ", DbType.Int32, requestedUserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchID);
                db.AddInParameter(command, "@RecipientEmail", DbType.String, receiptEmail);
                db.AddInParameter(command, "@MailBody", DbType.String, message);
                db.AddInParameter(command, "@MailSubject", DbType.String, subject);
                db.AddInParameter(command, "@MailSentAt", DbType.DateTime, mailSentAt);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, requestedUserId);
                db.AddOutParameter(command, "@UserEmailIDOUT", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }

            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 18 jan 2011
        /// Method Used To Register User
        /// Changed by Mansoor
        /// Date 11 Feb 2011
        /// Reader is replaced with ExecuteNonQuery
        /// </summary>
        /// <param name="registrationDTO"></param>
        /// <returns></returns>
        public int RegisterNewUser(RegistrationDTO registrationDTO)
        {
            int UpdateResult = 0;
            string sProc = UserConstant.REGISTER_NEW_USER_SP;
            int UserId = 0;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PrefixTypeID", DbType.Int32, registrationDTO.User.Prefix.PrefixId);
                db.AddInParameter(command, "@FirstName", DbType.String, registrationDTO.User.FirstName);
                db.AddInParameter(command, "@LastName", DbType.String, registrationDTO.User.LastName);
                db.AddInParameter(command, "@MiddleName", DbType.String, registrationDTO.User.MiddleName);
                db.AddInParameter(command, "@UserPassword", DbType.String, registrationDTO.User.UserPassword);

                db.AddInParameter(command, "@Suffix", DbType.String, registrationDTO.User.Suffix);
                db.AddInParameter(command, "@UserEmail", DbType.String, registrationDTO.User.UserEmail);
                db.AddInParameter(command, "@GenderTypeID", DbType.Int32, registrationDTO.User.Gender.GenderId);
                db.AddInParameter(command, "@SSN", DbType.String, registrationDTO.User.SSN);
                db.AddInParameter(command, "@LicenseNumber", DbType.String, registrationDTO.User.LicenseNumber);

                db.AddInParameter(command, "@FederalTaxId", DbType.String, registrationDTO.User.FederalTaxId);
                db.AddInParameter(command, "@EIN", DbType.String, registrationDTO.User.EIN);
                db.AddInParameter(command, "@QuivusUserID", DbType.Int32, 1);

                db.AddInParameter(command, "@AccountStatus", DbType.String, registrationDTO.User.AccountStatus.StatusID);
                db.AddInParameter(command, "@TextPassword", DbType.String, registrationDTO.User.UserPassword);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, registrationDTO.User.ActiveUserType.UserTypeId);
                db.AddInParameter(command, "@ActiveUserTypeID", DbType.Int32, registrationDTO.User.ActiveUserType.UserTypeId);
                db.AddInParameter(command, "@CliniBranchID", DbType.Int32, registrationDTO.ClinicBranhId);

                db.AddOutParameter(command, "@UserIDOUT", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }


        //void TestLoadTypeDTOsusingReflection()
        //{
        //    try
        //    {
        //        IDataReader objIReader = null;
        //        string sProc = UserConstant.TYPE_LOAD_ALL_PREFIX_SP;

        //        List<PrefixTypeDTO> PrefixTypeList = new List<PrefixTypeDTO>();

        //        // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
        //        Database db = DatabaseFactory.CreateDatabase(); // Load default DB
        //        DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

        //        using (objIReader = db.ExecuteReader(command))
        //        {
        //            if (objIReader != null)
        //            {
        //                GenericDataReaderToDTOConverter.ConvertToDTOList(PrefixTypeList, objIReader, typeof(PrefixTypeDTO));
        //                //SearchengineList = DataReaderToDTOConverter.SearchEngineAllConverter(objIReader);                        
        //            }
        //        }
        //    }
        //    catch (Exception exp)
        //    {

        //    }
        //}


        /// <summary>
        /// Author Muhaammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method will  be used as an abstarct interface to save/update user that might belong 
        /// to any category i.e either patient or clinical User
        /// </summary>
        /// <param name="baseUserDTO"></param>
        /// <returns></returns>

        public int RegisterBaseUser(BaseUserDTO baseUserDTO, bool IsUpdate)
        {
            int UserId = 0;

            string sProc = UserConstant.REGISTER_BASE_USER_SP;

            try
            {
                if (IsUpdate)
                {
                    QuivusLogger.Info("Update Case for RegisterBaseUser:: ");
                    sProc = UserConstant.UPDATE_BASE_USER_SP;
                }
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                if (baseUserDTO.Prefix != null)
                {
                    db.AddInParameter(command, "@PrefixTypeID", DbType.Int32, baseUserDTO.Prefix.PrefixId);
                }
                else
                {
                    db.AddInParameter(command, "@PrefixTypeID", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(command, "@FirstName", DbType.String, baseUserDTO.FirstName);
                db.AddInParameter(command, "@LastName", DbType.String, baseUserDTO.LastName);
                db.AddInParameter(command, "@MiddleName", DbType.String, baseUserDTO.MiddleName);

                db.AddInParameter(command, "@UserPassword", DbType.String, PasswordHashUtil.ComputeHash(baseUserDTO.UserPassword, ProjectConstant.HASH_ALGO_SHA1));

                db.AddInParameter(command, "@Suffix", DbType.String, baseUserDTO.Suffix);
                db.AddInParameter(command, "@GenderTypeID", DbType.Int32, baseUserDTO.Gender.GenderId);
                db.AddInParameter(command, "@SSN", DbType.String, baseUserDTO.SSN);
                db.AddInParameter(command, "@AccountStatus", DbType.String, baseUserDTO.AccountStatus.StatusID);
                db.AddInParameter(command, "@LicenseNumber", DbType.String, baseUserDTO.LicenseNumber);
                db.AddInParameter(command, "@FederalTaxId", DbType.String, baseUserDTO.FederalTaxId);
                db.AddInParameter(command, "@EIN", DbType.String, baseUserDTO.EIN);

                db.AddInParameter(command, "@QuivusUserID", DbType.Int32, baseUserDTO.QuivusUserID);
                db.AddInParameter(command, "@TextPassword", DbType.String, baseUserDTO.UserPassword);

                db.AddInParameter(command, "@ActiveUserTypeID", DbType.Int32, baseUserDTO.ActiveUserType.UserTypeId);
                db.AddInParameter(command, "@CliniBranchID", DbType.Int32, baseUserDTO.ClinicBranhId);


                if (!IsUpdate)// Ignore In case Of Update User Section
                {
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, baseUserDTO.CreatedBy);
                    db.AddInParameter(command, "@UserEmail", DbType.String, baseUserDTO.UserEmail);
                }
                if (IsUpdate) // Update Case 
                {
                    db.AddInParameter(command, "@UserID", DbType.String, baseUserDTO.UserId);
                    db.AddInParameter(command, "@ModifiedBy", DbType.String, baseUserDTO.ModifiedBy);
                }

                db.AddOutParameter(command, "@UserIDOUT", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UserId = int.Parse(db.GetParameterValue(command, "@UserIDOUT").ToString());
                }

            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UserId;
        }

        /// <summary>
        /// Author Ismail 
        /// Date Feb 11 2011
        /// Method Used To Save User Image
        /// </summary>
        /// <param name="imageDTO"></param>
        public int ManageUserImage(ImageDTO imageDTO)
        {
            string sProc = UserConstant.USER_IMAGE_INSERT_SP;

            int UserImageId;
            int ImageId;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@UserID", DbType.Int32, imageDTO.UserId);
                db.AddInParameter(command, "@UserTypeID", DbType.Int32, imageDTO.UserTypeID);
                db.AddInParameter(command, "@ImageName", DbType.String, imageDTO.ImageName);
                db.AddInParameter(command, "@ImageCaption", DbType.String, imageDTO.ImageCaption);
                db.AddInParameter(command, "@ImagePath", DbType.String, imageDTO.ImagePath);
                db.AddInParameter(command, "@ImageData", DbType.Binary, imageDTO.ImageData);
                db.AddInParameter(command, "@ImageType", DbType.Int32, imageDTO.ImageTypeId);

                //TODO: shd map the dTO field
                db.AddInParameter(command, "@IsDefault", DbType.Boolean, true);
                db.AddInParameter(command, "@CreatedBy", DbType.String, imageDTO.CreatedBy);

                db.AddOutParameter(command, "@UserImageIDOUT", DbType.Int32, 1);

                db.AddOutParameter(command, "@ImageIDOUT", DbType.Int32, 1);
                if (db.ExecuteNonQuery(command) == 0)
                {
                    //throw new RecordNotInserted
                }

                UserImageId = int.Parse(db.GetParameterValue(command, "@UserImageIDOUT").ToString());
                ImageId = int.Parse(db.GetParameterValue(command, "@ImageIDOUT").ToString());
            }
            catch
            {

                throw;
            }
            finally
            {
                //objIReader.Close();
            }

            return UserImageId;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 24 jan 2011
        /// This Method call the DB proc to Verfiy the User Email
        /// 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>

        public bool VerifyUserEmail(string UserEmail)
        {
            string sProc = UserConstant.USER_EMAIL_VERIFY_SP;

            bool IsValidUser = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@UserEmail", DbType.String, UserEmail);

                bool IsEmailExist = (bool)db.ExecuteScalar(command); // ExecuteScalar Return Only Single Record :)

                IsValidUser = !IsEmailExist;

            }
            catch (ConfigurationException cxp)
            {
                QuivusLogger.Error(cxp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (TargetInvocationException txp)
            {
                QuivusLogger.Error(txp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (SqlException sxp)
            {
                QuivusLogger.Error(sxp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw exp;
            }
            finally
            {
                //objIReader.Close();
            }

            return IsValidUser;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 26 Jan 2011
        /// Load Patient Guid by PatientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public Guid GetPatientGuidByPatientId(int patientId)
        {
            Guid guid = System.Guid.Empty;
            string sProc = UserConstant.REC_GUID_PATIENT_GET_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                guid = (Guid)db.ExecuteScalar(command);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return guid;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 26 Jan 2011
        /// Load Patient Guid by PatientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public Guid GetGurantorGuidByGurantorId(int gurantorId)
        {
            object o = null;
            Guid guid = System.Guid.Empty;
            string sProc = UserConstant.REC_GUID_GURANTOR_GET_BY_GURANTOR_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientGuarantorID", DbType.Int32, gurantorId);

                guid = (Guid)db.ExecuteScalar(command);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return guid;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to load Patient Profile Setting Providing 
        /// Location-Id & Provider i.e doctor or nurse Id.
        /// Settings are customized by combination of Provider&User Id 
        /// i.e Same user on different location may have different setting Vise Vers :)
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public List<PatientProfileSettingDTO> LoadPatientProfileSettings(int clinicBranchId, int UserId, int PatientId, int sectionNo)
        {
            List<PatientProfileSettingDTO> ProfileSettingList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.LOAD_PROFILE_SETTING_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@UserID", DbType.Int32, UserId);
                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);
                db.AddInParameter(command, "@SectionNo", DbType.Int32, sectionNo);

                using (objIReader = db.ExecuteReader(command))
                {
                    ProfileSettingList = DataReaderToDTOConverter.ProfileSettingDTOConverter(objIReader);
                }
            }
            catch (ConfigurationException cxp)
            {
                QuivusLogger.Error(cxp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (TargetInvocationException txp)
            {
                QuivusLogger.Error(txp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (SqlException sxp)
            {
                QuivusLogger.Error(sxp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return ProfileSettingList;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to load Patient Profile Setting Providing 
        /// Location-Id & Provider i.e doctor or nurse Id.
        /// Settings are customized by combination of Provider&User Id 
        /// i.e Same user on different location may have different setting Vise Vers :)
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public PatientProfileSettingDTO LoadPatientProfileSectionSettings(int clinicBranchId, int UserId, int PatientId, int sectionNo)
        {
            PatientProfileSettingDTO ProfileSettingObj = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.LOAD_PROFILE_SETTING_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@UserID", DbType.Int32, UserId);
                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);
                db.AddInParameter(command, "@SectionNo", DbType.Int32, sectionNo);

                using (objIReader = db.ExecuteReader(command))
                {
                    //  ProfileSettingList = DataReaderToDTOConverter.ProfileSettingDTOConverter(objIReader);
                    ProfileSettingObj = DataReaderToDTOConverter.ProfileSettingSectionDTOConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return ProfileSettingObj;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to Update Patient Profile Settings 
        /// </summary>
        /// <param name="PatientProfileSettingDTO"></param>
        /// <returns></returns>
        public int ManagePatientProfileSettings(PatientProfileSettingDTO ProfileSettingObj)
        {
            int UpdateResult = 0;
            string sProc_Insert = UserConstant.INSERT_PROFILE_SETTINGS_SP;
            string sProc_Update = UserConstant.UPDATE_PROFILE_SETTINGS_SP;
            bool IsUpdate = false;
            string sProc = sProc_Insert;
            try
            {
                if (ProfileSettingObj.UserUserProfileCategoryID > 0) /* If UserUserProfileCategoryID is provide Simply Update Record */
                {
                    sProc = sProc_Update;
                    IsUpdate = true;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID", DbType.Int32, ProfileSettingObj.UserID);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ProfileSettingObj.ClinicBranchID);
                db.AddInParameter(command, "@SectionNo", DbType.Int32, ProfileSettingObj.SectionNo);
                db.AddInParameter(command, "@UserProfileCategoryID", DbType.Int32, ProfileSettingObj.UserProfileCategoryID);

                if (IsUpdate)
                {
                    db.AddInParameter(command, "@ModifiedBy", DbType.Int32, ProfileSettingObj.ModifiedBy);
                    db.AddInParameter(command, "@UserUserProfileCategoryID", DbType.Int32, ProfileSettingObj.UserUserProfileCategoryID);
                }
                else
                {
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, ProfileSettingObj.CreatedBy);
                    db.AddOutParameter(command, "@UserUserProfileCategoryIDOUT", DbType.Int32, 1); // Specify Out Param In Insertion Case only
                }

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }
            catch (ConfigurationException cxp)
            {
                QuivusLogger.Error(cxp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (TargetInvocationException txp)
            {
                QuivusLogger.Error(txp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (SqlException sxp)
            {
                QuivusLogger.Error(sxp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// Method Used To Load Profile Static Information
        /// </summary>
        /// <returns></returns>
        public List<ProfileStaticDataDTO> LoadProfileStaticData()
        {
            List<ProfileStaticDataDTO> ProfileStaticList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.PROFILE_STATIC_DATA_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Namekd
                using (objIReader = db.ExecuteReader(command))
                {
                    ProfileStaticList = DataReaderToDTOConverter.ProfileStaticDataDTOConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return ProfileStaticList;
        }


        /// <summary>
        /// Author Ali Noor
        /// </summary>
        /// <param name="emailDTO"></param>
        /// <param name="mailSentAt"></param>
        /// <returns></returns>
        public int LogEmailWithAttachment(EmailDTO emailDTO, DateTime mailSentAt)
        {
            int UpdateResult = 0;
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_DEFAULT_EMAIL_LOG_WITH_ATTACHMENT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.COMMUNICATION_DB); // Load DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID ", DbType.Int32, emailDTO.UserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, emailDTO.ClinicBranchID);
                db.AddInParameter(command, "@RecipientEmail", DbType.String, emailDTO.RecipientEmail);
                db.AddInParameter(command, "@MailBody", DbType.String, emailDTO.Message);
                db.AddInParameter(command, "@MailSubject", DbType.String, emailDTO.Subject);
                db.AddInParameter(command, "@MailSentAt", DbType.DateTime, mailSentAt);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, emailDTO.UserId);
                db.AddInParameter(command, "@AttachedFileName ", DbType.String, emailDTO.AttachedFileName);
                db.AddInParameter(command, "@Attachment ", DbType.Binary, emailDTO.Attachment);
                db.AddOutParameter(command, "@UserEmailIDOUT", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }
            catch (ConfigurationException cxp)
            {
                QuivusLogger.Error(cxp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (TargetInvocationException txp)
            {
                QuivusLogger.Error(txp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (SqlException sxp)
            {
                QuivusLogger.Error(sxp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }

        /// <summary>
        /// Auhtor Mansoor
        /// Date 1 Feb 2011
        /// this method will insert the last patient acccessed in the provider recent patients table
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public bool SaveRecentPatient(int clinicBranchId, int providerUserId, int patientId)
        {
            bool UpdateResult = false;
            string sProc = UserConstant.USER_RECENT_PATIENT_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserId", DbType.Int32, providerUserId);
                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);

                if (db.ExecuteNonQuery(command) == 0)
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
            return UpdateResult;
        }

        /// <summary>
        /// Auhtor Mansoor
        /// Date 1 Feb 2011
        /// this method will insert the last patient acccessed in the provider recent patients table
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>


        public List<RecentPatientDTO> LoadRecentPatientsByProviderIdAndBranchId(int providerUserId, int clinicBranchId)
        {
            List<RecentPatientDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_RECENT_PATIENT_LOAD_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<RecentPatientDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID", DbType.String, providerUserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.String, clinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    DataReaderToDTOConverter.ConvertToRecentPatientDTO(objIReader);
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        ///// <summary>
        ///// Auhtor Mansoor
        ///// Date 2 Feb 2011
        ///// This Method will load the user base info         
        ///// </summary>
        ///// <param name="patientId"></param>
        ///// <returns></returns>
        //public BaseUserDTO LoadBaseUserByPatientId(int patientId)
        //{
        //    BaseUserDTO baseUserDTOobj = null;

        //    try
        //    {

        //    }
        //    catch (Exception exp)
        //    {
        //        QuivusLogger.Error(exp);
        //        throw; // Just Re-Throw Exception From DAO LEVEL
        //    }
        //    return baseUserDTOobj;
        //}

        /// <summary>
        /// Auhtor Mansoor
        /// Date 2 Feb 2011
        /// this method will get the email for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserEmailByUserId(int userId)
        {
            string userEmail = "";
            object respose = null;

            string sProc = UserConstant.USER_EMAIL_GET_BY_USER_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID", DbType.Int32, userId);

                respose = db.ExecuteScalar(command);

                if (respose != DBNull.Value && respose != null)
                {
                    userEmail = respose.ToString();
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return userEmail;
        }

        public List<PatientProfileSettingDTO> LoadProfileSettingCollection(int clinicBranchId, int UserID)
        {
            List<PatientProfileSettingDTO> ProfileSettingList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.LOAD_PROFILE_SETTINGS_COLLECTION_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID", DbType.String, UserID);
                db.AddInParameter(command, "@ClinicBranchID", DbType.String, clinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    ProfileSettingList = DataReaderToDTOConverter.GeneralProfileSettingDTOConverter(objIReader);
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return ProfileSettingList;
        }


        /// <summary>
        /// Author Muhammad ismail 
        /// Date 09 Feb 2011
        /// Method Used TO clear Search engines Added by User
        /// Changed By Mansoor        
        /// Date 11 Feb 2011
        /// Connection was not closed
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public int ClearUserAddedSearchEngines(int ProviderUserID, int ClinicBranchID, int ModifiedBy)
        {
            bool isConnectionOpen = false;
            int UpdateResult = 0;
            string sProc = UserConstant.CLEAR_USER_SEARCHENGINE_SP;

            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ProviderUserID", ProviderUserID);
                sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchID);

                sqlCommand.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }

            }
            catch
            {
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
        /// Author Muhammad ismail
        /// Date Feb 11 2011
        /// Method Used To load Image Types
        /// </summary>
        /// <returns></returns>
        public List<ImageTypeDTO> LoadImageTypes()
        {
            List<ImageTypeDTO> ImageTypeList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.IMAGE_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Namekd
                using (objIReader = db.ExecuteReader(command))
                {
                    ImageTypeList = DataReaderToDTOConverter.ImageTypeTOConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return ImageTypeList;
        }

        public List<UserBasicInfoDTO> GetUserBasicInfoById(List<int> ProviderIdList)
        {
            List<UserBasicInfoDTO> ResponseList = null;

            bool isConnectionOpen = false;

            string sProc = UserConstant.USER_BASIC_INFO_LOAD_SP;

            DataTable UserIdTable = null;
            SqlConnection connection = null;
            SqlDataReader objIReader = null;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                UserIdTable = DTOToTableConverter.UserIdListConverter(ProviderIdList);

                Database DB = DatabaseFactory.CreateDatabase(); // Load default DB
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter tvpMedical = sqlCommand.Parameters.AddWithValue("@Rec_Ids", UserIdTable);
                tvpMedical.SqlDbType = SqlDbType.Structured;
                tvpMedical.TypeName = ProjectConstant.SEARCH_ENGINE_TYPE_TVP;

                connection.Open();
                isConnectionOpen = true;

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    ResponseList = DataReaderToDTOConverter.UserBasicInfoConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return ResponseList;
        }

        public bool IsProviderAtClinic(int userId, int clinicBranchId)
        {
            bool isProvider = false;
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_IS_PROVIDER_AT_CLINIC;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Namekd

                db.AddInParameter(command, "@UserID", DbType.Int32, userId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null && objIReader.Read())
                    {
                        if (objIReader["IsProvider"] != DBNull.Value)
                            isProvider = bool.Parse(objIReader["IsProvider"].ToString());
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return isProvider;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 26 Jan 2011
        /// Load Patient Guid by PatientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public Guid GetEmergencyContactGuidByPatientId(int patientId)
        {
            object o = null;
            Guid guid = System.Guid.Empty;
            IDataReader objIReader = null;
            string sProc = UserConstant.REC_GUID_EMERGENCY_CONTACT_GET_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null && objIReader.Read())
                    {
                        if (objIReader["RecGuid"] != DBNull.Value)
                        {
                            guid = Guid.Parse(objIReader["RecGuid"].ToString());
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return guid;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 26 Jan 2011
        /// Load Patient Guid by PatientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public Guid GetGurantorGuidByPatientId(int patientId)
        {
            object o = null;
            Guid guid = System.Guid.Empty;
            IDataReader objIReader = null;
            string sProc = UserConstant.REC_GUID_GURANTOR_GET_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null && objIReader.Read())
                    {
                        if (objIReader["RecGuid"] != DBNull.Value)
                        {
                            guid = Guid.Parse(objIReader["RecGuid"].ToString());
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return guid;
        }
    }
}