using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Quivus.Model.User;
using Quivus.Model.Common;
using Quivus.Model.Clinic;
using Quivus.Model.TypeDTOs;
using Quivus.Model.SearchEngine;
using Quivus.Model.QuickLink;
using System.IO;
using Quivus.Utility.Logger;
using Quivus.Model.Search;
using Quivus.Utility.CustomException;
using Quivus.Utility;

using Quivus.Model.User.Patient;
using Quivus.Model.PatientProfileCustomization;
using Quivus.Model.RecentPatient;
using Quivus.Model.Insurance;

using Quivus.Model.AddressBook;
using System.Data.SqlClient;

namespace UserManagerService.DataAccess.DB_Util
{
    public class DataReaderToDTOConverter
    {
        /// <summary>
        /// Author Mansoor Ud-Din
        /// Created Date 13 jan 2011
        /// This Method Verfiy the User Credential &
        /// 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>

        public static LoginDTO LoginDTOConverter(IDataReader objReader)
        {
            LoginDTO loginDTO = null;

            try
            {
                if (objReader.NextResult())
                {
                    //objReader.NextResult();

                    //Single Record will be returned.
                    if (objReader.Read())
                    {
                        loginDTO = new LoginDTO();
                        loginDTO.UserId = int.Parse(objReader["UserId"].ToString());
                        loginDTO.FirstName = objReader["FirstName"].ToString();
                        loginDTO.LastName = objReader["LastName"].ToString();

                        if (objReader["MiddleName"] != DBNull.Value)
                        {
                            loginDTO.MiddleName = objReader["MiddleName"].ToString();
                        }

                        if (objReader["Suffix"] != DBNull.Value)
                        {
                            loginDTO.Suffix = objReader["Suffix"].ToString();
                        }

                        if (objReader["PrefixName"] != DBNull.Value)
                        {
                            loginDTO.Prefix = objReader["PrefixName"].ToString();
                        }

                        loginDTO.Gender = objReader["GenderName"].ToString();
                        loginDTO.SSN = objReader["SSN"].ToString();
                        loginDTO.UserEmail = objReader["UserEmail"].ToString();
                    }

                    if (loginDTO != null)
                    {
                        objReader.NextResult();
                        List<UserClinicBranchDTO> UserClinicBranchesList = new List<UserClinicBranchDTO>();

                        UserClinicBranchDTO clinicBranchDTO = null;
                        //Multiple Records will be returned
                        while (objReader.Read())
                        {
                            clinicBranchDTO = new UserClinicBranchDTO();
                            clinicBranchDTO.ClinicBranch = new ClinicBranchDTO();

                            clinicBranchDTO.ClinicBranch.ClinicBranchIdSeq = int.Parse(objReader["ClinicBranchId"].ToString());

                            //clinicBranchDTO.UserClinicBranch.Clinic.ClinicIdSeq = int.Parse(objReader["CLINIC_ID"].ToString());
                            //clinicBranchDTO.UserClinicBranch.Clinic.ClinicName = objReader["CLINIC_NAME"].ToString();
                            clinicBranchDTO.ClinicBranch.ClinicBranchName = objReader["BranchName"].ToString();
                            //clinicBranchDTO.UserClinicBranch.Clinic.ClinicStatus = objReader["CLINIC_STATUS"].ToString();

                            UserClinicBranchesList.Add(clinicBranchDTO);
                        }

                        objReader.NextResult(); // Get All profiles

                        List<UserClinicRoleDTO> UserClinicRoles = new List<UserClinicRoleDTO>();
                        UserClinicRoleDTO objUserClinicRoleDTO = null;

                        //Multiple Records will be returned
                        while (objReader.Read())
                        {
                            objUserClinicRoleDTO = new UserClinicRoleDTO();
                            objUserClinicRoleDTO.UserTypeRole = new UserTypeDTO();

                            objUserClinicRoleDTO.UserTypeRole.UserTypeId = int.Parse(objReader["UserTypeId"].ToString());
                            objUserClinicRoleDTO.UserTypeRole.UserTypeName = objReader["UserTypeName"].ToString();
                            objUserClinicRoleDTO.UserTypeRole.ClinicBranchID = int.Parse(objReader["ClinicBranchId"].ToString());

                            objUserClinicRoleDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                            objUserClinicRoleDTO.UserTypeProfileID = int.Parse(objReader["UserTypeRoleID"].ToString());

                            UserClinicRoles.Add(objUserClinicRoleDTO);
                        }

                        UserClinicBranchDTO objClinicBranch;
                        for (int indexClinic = 0; indexClinic < UserClinicBranchesList.Count; indexClinic++)
                        {
                            objClinicBranch = UserClinicBranchesList[indexClinic];
                            objClinicBranch.ClinicBranchUserRolesList = new List<UserClinicRoleDTO>();

                            UserClinicRoleDTO objRole;
                            for (int indexRole = 0; indexRole < UserClinicRoles.Count; indexRole++)
                            {
                                objRole = UserClinicRoles[indexRole];

                                if (objRole.UserTypeRole.ClinicBranchID == objClinicBranch.ClinicBranch.ClinicBranchIdSeq)
                                {
                                    objClinicBranch.ClinicBranchUserRolesList.Add(objRole);
                                }
                            }
                        }

                        loginDTO.UserClinicBranchesList = UserClinicBranchesList;

                        //
                        objReader.NextResult();
                        List<UserTypeDTO> AllUserTypes = new List<UserTypeDTO>();

                        UserClinicRoleDTO objRole1;
                        for (int indexRole = 0; indexRole < UserClinicRoles.Count; indexRole++)
                        {
                            objRole1 = UserClinicRoles[indexRole];

                            UserTypeDTO objUserType;
                            bool isFound = false;

                            for (int indexType = 0; indexType < AllUserTypes.Count && !isFound; indexType++)
                            {
                                objUserType = AllUserTypes[indexType];

                                if (objRole1.UserTypeRole.UserTypeId == objUserType.UserTypeId)
                                {
                                    isFound = true;
                                }
                            }
                            if (!isFound)
                            {
                                objUserType = new UserTypeDTO();

                                objUserType.UserTypeId = objRole1.UserTypeRole.UserTypeId;
                                objUserType.UserTypeName = objRole1.UserTypeRole.UserTypeName;

                                AllUserTypes.Add(objUserType);
                            }
                        }

                        if (AllUserTypes.Count > 0)
                        {
                            loginDTO.AllUserTypes = AllUserTypes;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".LoginDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return loginDTO;
        }

        /// <summary>
        /// author Muhammad Ismail
        /// Create Date 14 Jan 2011
        /// this Method Populate Search Engine DTO by reading Object Reader By Location
        /// Mansoor
        /// Date 11 Feb 2011
        /// NULL value checks added
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<SearchEngineDTO> SearchEngineLocationConverter(IDataReader objReader)
        {
            List<SearchEngineDTO> SearchengineList = null;
            try
            {
                if (objReader != null)
                {
                    SearchengineList = new List<SearchEngineDTO>();
                    {
                        while (objReader.Read())
                        {
                            SearchEngineDTO SearchEngineObj = new SearchEngineDTO();

                            # region Parse String Properties
                            SearchEngineObj.label = objReader["SearchEngineName"].ToString();

                            if (!string.IsNullOrEmpty(objReader["Icon"].ToString()))
                            {
                                SearchEngineObj.icon = (byte[])objReader["Icon"];
                            }

                            if (objReader["Description"] != DBNull.Value) //check added, this value can be NULL 
                            {
                                SearchEngineObj.description = objReader["Description"].ToString();
                            }
                            SearchEngineObj.URL = objReader["URL"].ToString();
                            # endregion

                            # region Parse Interger Properties

                            if (!string.IsNullOrEmpty(objReader["UserSearchEngineID"].ToString()))
                            {
                                SearchEngineObj.UserSearchEngineID = int.Parse(objReader["UserSearchEngineID"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["SearchEngineTypeID"].ToString()))
                            {
                                SearchEngineObj.SearchEngineTypeID = int.Parse(objReader["SearchEngineTypeID"].ToString());
                            }
                            # endregion

                            # region Parse Bool Properties

                            if (!string.IsNullOrEmpty(objReader["IsPreferred"].ToString()))
                            {
                                SearchEngineObj.IsPreferred = bool.Parse(objReader["IsPreferred"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["IsSystemGenerated"].ToString()))
                            {
                                SearchEngineObj.IsSystemGenerated = bool.Parse(objReader["IsSystemGenerated"].ToString());
                            }
                            #endregion

                            # region Parse DateTime Properties
                            /* String createdAt = objReader["CreatedAt"].ToString();
                            if (!string.IsNullOrEmpty(createdAt))
                            {
                                DateTime create = DateTime.Parse(createdAt);
                                SearchEngineObj.CreatedAt = create;
                            }

                            String ModifyAt = objReader["ModifiedAt"].ToString();
                            if (!string.IsNullOrEmpty(ModifyAt))
                            {
                                DateTime Modify = DateTime.Parse(ModifyAt);
                                SearchEngineObj.ModifiedAt = Modify;
                            }*/
                            #endregion

                            SearchengineList.Add(SearchEngineObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".SearchEngineLocationConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return SearchengineList;
        }

        /// <summary>
        /// author Muhammad Ismail
        /// Create Date 17 Jan 2011
        /// this Method Populate Search Engine DTO by reading Object Reader For All Search Engines
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<SearchEngineDTO> SearchEngineAllConverter(IDataReader objReader)
        {
            List<SearchEngineDTO> SearchengineList = null;
            try
            {
                if (objReader != null)
                {
                    SearchengineList = new List<SearchEngineDTO>();
                    {
                        while (objReader.Read())
                        {
                            SearchEngineDTO SearchEngineObj = new SearchEngineDTO();

                            # region Parse String Properties
                            SearchEngineObj.label = objReader["SearchEngineName"].ToString();

                            if (!string.IsNullOrEmpty(objReader["Icon"].ToString()))
                            {
                                SearchEngineObj.icon = (byte[])objReader["Icon"];
                            }

                            if (objReader["Description"] != DBNull.Value) //check added, this value can be NULL 
                            {
                                SearchEngineObj.description = objReader["Description"].ToString();
                            }

                            SearchEngineObj.URL = objReader["URL"].ToString();
                            # endregion

                            # region Parse Interger Properties
                            if (!string.IsNullOrEmpty(objReader["SearchEngineTypeID"].ToString()))
                            {
                                SearchEngineObj.SearchEngineTypeID = int.Parse(objReader["SearchEngineTypeID"].ToString());
                            }
                            # endregion

                            # region Parse Bool Properties
                            if (!string.IsNullOrEmpty(objReader["IsActive"].ToString()))
                            {
                                SearchEngineObj.IsActive = bool.Parse(objReader["IsActive"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["IsDeleted"].ToString()))
                            {
                                SearchEngineObj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                            }


                            if (!string.IsNullOrEmpty(objReader["IsSystemGenerated"].ToString()))
                            {
                                SearchEngineObj.IsSystemGenerated = bool.Parse(objReader["IsSystemGenerated"].ToString());
                            }
                            #endregion

                            SearchengineList.Add(SearchEngineObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".SearchEngineAllConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return SearchengineList;
        }

        /// <summary>
        /// author Muhammad Ismail
        /// Create Date 14 Jan 2011
        /// this Method Populate Quick linkDTO by reading Object Reader
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<QuickLinkDTO> QuickLinkConverter(IDataReader objReader)
        {
            List<QuickLinkDTO> QuickLinkList = null;
            try
            {
                if (objReader != null)
                {
                    QuickLinkList = new List<QuickLinkDTO>();
                    {
                        while (objReader.Read())
                        {
                            QuickLinkDTO QuickLinkObj = new QuickLinkDTO();

                            # region Parse String Properties
                            QuickLinkObj.label = objReader["QuickLinkName"].ToString();
                            QuickLinkObj.description = objReader["Description"].ToString();

                            if (!string.IsNullOrEmpty(objReader["Icon"].ToString()))
                            {
                                QuickLinkObj.icon = (byte[])objReader["Icon"]; // Convert String To Byte Array
                            }

                            #endregion

                            # region Parse Int Properties
                            if (!string.IsNullOrEmpty(objReader["UserQuickLinks"].ToString()))
                            {
                                QuickLinkObj.UserQuickLinks = int.Parse(objReader["UserQuickLinks"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["QuickLinkTypeID"].ToString()))
                            {
                                QuickLinkObj.QuickLinkTypeID = int.Parse(objReader["QuickLinkTypeID"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["CreatedBy"].ToString()))
                            {
                                QuickLinkObj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["ModifiedBy"].ToString()))
                            {
                                QuickLinkObj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["ModifiedBy"].ToString()))
                            {
                                QuickLinkObj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                            }
                            #endregion

                            #region Parse Date Time Properties
                            String createdAt = objReader["CreatedAt"].ToString();

                            if (!string.IsNullOrEmpty(createdAt))
                            {
                                DateTime create = DateTime.Parse(createdAt);
                                QuickLinkObj.CreatedAt = create;
                            }
                            String ModifyAt = objReader["ModifiedAt"].ToString();
                            if (!string.IsNullOrEmpty(ModifyAt))
                            {
                                DateTime Modify = DateTime.Parse(ModifyAt);
                                QuickLinkObj.ModifiedAt = Modify;
                            }
                            #endregion

                            QuickLinkList.Add(QuickLinkObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".QuickLinkConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return QuickLinkList;
        }

        /// <summary>
        /// author Muhammad Ismail
        /// Create Date 14 Jan 2011
        /// this Method Convert String to Byte Array
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static byte[] StrToByteArray(string str)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(str);
        }

        internal static List<ContactSearchDTO> ContactSearchDTOConverter(IDataReader objReader)
        {
            List<ContactSearchDTO> ContactSearchList = null;
            try
            {
                if (objReader != null)
                {
                    ContactSearchList = new List<ContactSearchDTO>();
                    {
                        while (objReader.Read())
                        {
                            ContactSearchDTO QuickLinkObj = new ContactSearchDTO();

                            # region Parse String Properties
                            QuickLinkObj.FirstName = objReader["FirstName"].ToString();
                            QuickLinkObj.LastName = objReader["LastName"].ToString();
                            QuickLinkObj.MiddleName = objReader["MiddleName"].ToString();
                            QuickLinkObj.Suffix = objReader["Suffix"].ToString();
                            QuickLinkObj.UserEmail = objReader["UserEmail"].ToString();
                            #endregion

                            # region Parse Int Properties
                            if (!string.IsNullOrEmpty(objReader["UserId"].ToString()))
                            {
                                QuickLinkObj.UserId = int.Parse(objReader["UserId"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["PrefixTypeID"].ToString()))
                            {
                                QuickLinkObj.PrefixTypeID = int.Parse(objReader["PrefixTypeID"].ToString());
                            }

                            #endregion

                            ContactSearchList.Add(QuickLinkObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ContactSearchDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return ContactSearchList;
        }

        /// <summary>
        /// Auhtor Mansoor
        /// Date 24 Jan 2011
        /// Convert the reader to User Types DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<UserTypeDTO> UserTypeDTOConverter(IDataReader objReader)
        {
            List<UserTypeDTO> UserTypeList = null;
            try
            {
                if (objReader != null)
                {
                    UserTypeList = new List<UserTypeDTO>();
                    {
                        while (objReader.Read())
                        {
                            UserTypeDTO UserTypeObj = new UserTypeDTO();

                            UserTypeObj.UserTypeId = int.Parse(objReader["UserTypeId"].ToString());
                            UserTypeObj.UserTypeName = objReader["UserTypeName"].ToString();

                            UserTypeList.Add(UserTypeObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".UserTypeDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return UserTypeList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 28 Jan 2011
        /// Convert the reader to Pateient Search Result
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static PatientSearchResultDTO ConvertToPatientSearchResultDTO(IDataReader objReader)
        {
            PatientSearchResultDTO searchResult = null;
            List<PatientSearchDTO> patientSearchList = null;

            try
            {
                if (objReader != null && objReader.Read())
                {
                    searchResult = new PatientSearchResultDTO();

                    if (objReader["TotalRecords"] != DBNull.Value && !String.IsNullOrEmpty(objReader["TotalRecords"].ToString()))
                        searchResult.TotalNoOfRecords = int.Parse(objReader["TotalRecords"].ToString());

                    if (objReader["FirstRecord"] != DBNull.Value && !String.IsNullOrEmpty(objReader["FirstRecord"].ToString()))
                        searchResult.CurrentFirstRecordNo = int.Parse(objReader["FirstRecord"].ToString());

                    if (objReader["LastRecord"] != DBNull.Value && !String.IsNullOrEmpty(objReader["LastRecord"].ToString()))
                        searchResult.CurrentLastRecordNo = int.Parse(objReader["LastRecord"].ToString());

                    objReader.NextResult();
                    patientSearchList = new List<PatientSearchDTO>();

                    while (objReader.Read())
                    {
                        PatientSearchDTO PatientSearchObj = new PatientSearchDTO();

                        PatientSearchObj.UserId = int.Parse(objReader["UserId"].ToString());
                        PatientSearchObj.PatientId = int.Parse(objReader["PatientID"].ToString());
                        PatientSearchObj.FirstName = objReader["FirstName"].ToString();
                        PatientSearchObj.LastName = objReader["LastName"].ToString();

                        if (objReader["MiddleName"] != DBNull.Value)
                        {
                            PatientSearchObj.MiddleName = objReader["MiddleName"].ToString();
                        }

                        PatientSearchObj.QuivusID = objReader["QuivusID"].ToString();
                        PatientSearchObj.DOB = DateTime.Parse(objReader["DateOfBirth"].ToString());

                        PatientSearchObj.IsEmailIdAvailable = Boolean.Parse(objReader["AllowEmailCommunication"].ToString());

                        patientSearchList.Add(PatientSearchObj);
                    }
                    searchResult.PatientSearchList = patientSearchList;
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientSearchResultDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return searchResult;
        }


        /// <summary>
        /// Author Muhammad ismail
        /// Updated Date Feb 02 2011
        /// Method Used To convert Reader Set To Profile-SettingDTO :)
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<ProfileStaticDataDTO> ProfileStaticDataDTOConverter(IDataReader objReader)
        {
            List<ProfileStaticDataDTO> patientStaticDataList = null;
            try
            {
                if (objReader != null)
                {
                    patientStaticDataList = new List<ProfileStaticDataDTO>();

                    while (objReader.Read())
                    {
                        ProfileStaticDataDTO PatientSearchObj = new ProfileStaticDataDTO();

                        #region Parse Integer Properties

                        if (!string.IsNullOrEmpty(objReader["UserProfileCategoryID"].ToString()))
                        {
                            PatientSearchObj.UserProfileCategoryID = int.Parse(objReader["UserProfileCategoryID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(objReader["MainUserProfileCategoryID"].ToString()))
                        {
                            PatientSearchObj.MainUserProfileCategoryID = int.Parse(objReader["MainUserProfileCategoryID"].ToString());
                        }
                        #endregion

                        #region Parse String Properties
                        PatientSearchObj.MainCategoryName = objReader["MainCategoryName"].ToString();
                        PatientSearchObj.MainCategoryDisplayText = objReader["MainCategoryDisplayText"].ToString();

                        PatientSearchObj.CategoryName = objReader["CategoryName"].ToString();
                        PatientSearchObj.CategoryDisplayText = objReader["CategoryDisplayText"].ToString();
                        #endregion

                        patientStaticDataList.Add(PatientSearchObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + "ProfileStaticDataDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientStaticDataList;
        }

        /// <summary>
        /// Author Muhammad ismail
        /// Created Date 02 Feb 2011
        /// Method Used to Convert Profile Setting List
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<PatientProfileSettingDTO> ProfileSettingDTOConverter(IDataReader objReader)
        {
            List<PatientProfileSettingDTO> patientStaticDataList = null;
            try
            {
                if (objReader != null)
                {
                    patientStaticDataList = new List<PatientProfileSettingDTO>();

                    while (objReader.Read())
                    {
                        PatientProfileSettingDTO ProfileObj = new PatientProfileSettingDTO();

                        #region Parse Integer Properties


                        if (objReader["UserID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserID"].ToString()))
                        {
                            ProfileObj.UserID = int.Parse(objReader["UserID"].ToString());
                        }
                        if (objReader["SectionNo"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SectionNo"].ToString()))
                        {
                            ProfileObj.SectionNo = int.Parse(objReader["SectionNo"].ToString());
                        }
                        if (objReader["DisplayText"] != DBNull.Value && !string.IsNullOrEmpty(objReader["DisplayText"].ToString()))
                        {
                            ProfileObj.DisplayText = objReader["DisplayText"].ToString();
                        }
                        if (objReader["FieldData"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FieldData"].ToString()))
                        {
                            ProfileObj.FieldData = objReader["FieldData"].ToString();
                        }
                        if (objReader["UserProfileCategoryID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserProfileCategoryID"].ToString()))
                        {
                            ProfileObj.UserProfileCategoryID = int.Parse(objReader["UserProfileCategoryID"].ToString());
                        }
                        if (objReader["UserProfileCategoryName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserProfileCategoryName"].ToString()))
                        {
                            ProfileObj.UserProfileCategoryName = objReader["UserProfileCategoryName"].ToString();
                        }

                        if (objReader["TooltipText"] != DBNull.Value && !string.IsNullOrEmpty(objReader["TooltipText"].ToString()))
                        {
                            ProfileObj.TooltipText = objReader["TooltipText"].ToString();
                        }
                        
                        #endregion

                        //ProfileObj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());

                        #region Parse Date Time

                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    String createdAt = objReader["CreatedAt"].ToString();
                        //    if (!string.IsNullOrEmpty(createdAt))
                        //    {
                        //        DateTime create = DateTime.Parse(createdAt);
                        //        ProfileObj.CreatedAt = create;
                        //    }
                        //}

                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    String modifyAt = objReader["ModifiedAt"].ToString();
                        //    if (!string.IsNullOrEmpty(modifyAt))
                        //    {
                        //        DateTime Modified = DateTime.Parse(modifyAt);
                        //        ProfileObj.ModifiedAt = Modified;
                        //    }
                        //}
                        #endregion

                        patientStaticDataList.Add(ProfileObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.PROFILE_SETTING_ERROR_MSG + "ProfileStaticDataDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientStaticDataList;
        }

        /// <summary>
        /// Author Muhammad ismail
        /// Created Date 02 Feb 2011
        /// Method Used to Convert Profile Setting List
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<PatientProfileSettingDTO> GeneralProfileSettingDTOConverter(IDataReader objReader)
        {
            List<PatientProfileSettingDTO> patientStaticDataList = null;
            try
            {
                if (objReader != null)
                {
                    patientStaticDataList = new List<PatientProfileSettingDTO>();

                    while (objReader.Read())
                    {
                        PatientProfileSettingDTO ProfileObj = new PatientProfileSettingDTO();

                        #region Parse Integer Properties

                        if (objReader["UserID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserID"].ToString()))
                        {
                            ProfileObj.UserID = int.Parse(objReader["UserID"].ToString());
                        }
                        if (objReader["SectionNo"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SectionNo"].ToString()))
                        {
                            ProfileObj.SectionNo = int.Parse(objReader["SectionNo"].ToString());
                        }
                        if (objReader["UserProfileCategoryID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserProfileCategoryID"].ToString()))
                        {
                            ProfileObj.UserProfileCategoryID = int.Parse(objReader["UserProfileCategoryID"].ToString());
                        }
                        if (objReader["UserProfileCategoryName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserProfileCategoryName"].ToString()))
                        {
                            ProfileObj.UserProfileCategoryName = objReader["UserProfileCategoryName"].ToString();
                        }
                        //M 07 Feb 2011
                        if (objReader["UserUserProfileCategoryID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserUserProfileCategoryID"].ToString()))
                        {
                            ProfileObj.UserUserProfileCategoryID = int.Parse(objReader["UserUserProfileCategoryID"].ToString());
                        }

                        #endregion

                        patientStaticDataList.Add(ProfileObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.PROFILE_SETTING_ERROR_MSG + "ProfileStaticDataDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientStaticDataList;
        }
        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Convert the reader to Pateient Search Result
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<RecentPatientDTO> ConvertToRecentPatientDTO(IDataReader objReader)
        {
            List<RecentPatientDTO> patientSearchList = null;

            try
            {
                if (objReader != null)
                {
                    patientSearchList = new List<RecentPatientDTO>();
                    {
                        while (objReader.Read())
                        {
                            RecentPatientDTO RecentPatientDTOObj = new RecentPatientDTO();

                            RecentPatientDTOObj.Index = int.Parse(objReader["Index"].ToString());
                            RecentPatientDTOObj.PatientId = int.Parse(objReader["PatientID"].ToString());

                            RecentPatientDTOObj.PatientUserID = int.Parse(objReader["PatientUserID"].ToString());

                            RecentPatientDTOObj.PatientName = objReader["PatientName"].ToString();
                            RecentPatientDTOObj.QuivusId = objReader["QuivusID"].ToString();
                            RecentPatientDTOObj.PatientDOB = DateTime.Parse(objReader["DateOfBirth"].ToString());

                            if (objReader["LastVisitDate"] != DBNull.Value)
                            {
                                RecentPatientDTOObj.LastVisitDate = objReader["LastVisitDate"].ToString();
                            }

                            RecentPatientDTOObj.IsEmailIdAvailable = Boolean.Parse(objReader["AllowEmailCommunication"].ToString());

                            patientSearchList.Add(RecentPatientDTOObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToRecentPatientDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientSearchList;
        }

        internal static PatientProfileBasicInfoDTO ConvertToPatientProfileBasicInfoDTO(IDataReader objReader)
        {
            PatientProfileBasicInfoDTO patientBasicProfileInfo = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        patientBasicProfileInfo = new PatientProfileBasicInfoDTO();

                        patientBasicProfileInfo.FirstName = objReader["FirstName"].ToString();
                        patientBasicProfileInfo.LastName = objReader["LastName"].ToString();

                        if (objReader["MiddleName"] != DBNull.Value)
                        {
                            patientBasicProfileInfo.MiddleName = objReader["MiddleName"].ToString();
                        }

                        patientBasicProfileInfo.DateOfBirth = DateTime.Parse(objReader["DateOfBirth"].ToString());
                        patientBasicProfileInfo.QuivusID = objReader["QuivusID"].ToString();
                        patientBasicProfileInfo.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileBasicInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientBasicProfileInfo;
        }

        internal static PatientInsuracneInfoDTO ConvertToPatientInsuranceInfoDTO(IDataReader objReader)
        {
            PatientInsuracneInfoDTO patientInsuranceInfo = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        patientInsuranceInfo = new PatientInsuracneInfoDTO();

                        //M these fields cann't be null, so null check is ommited
                        patientInsuranceInfo.PatientInsuranceId = int.Parse(objReader["PatientInsuranceId"].ToString());
                        patientInsuranceInfo.InsuranceOrganizationName = objReader["InsuranceOrganizationName"].ToString();
                        patientInsuranceInfo.EndDate = DateTime.Parse(objReader["EndDate"].ToString());
                        patientInsuranceInfo.IsEligible = Boolean.Parse(objReader["IsEligible"].ToString());
                        patientInsuranceInfo.InsuranceBalance = int.Parse(objReader["InsuranceBalance"].ToString());
                        patientInsuranceInfo.PatientBalance = int.Parse(objReader["PatientBalance"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientInsuranceInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientInsuranceInfo;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Date 04 Feb 2011
        /// Method used to Convert Profile Setting Section
        /// </summary>
        /// <param name="objIReader"></param>
        /// <returns></returns>
        internal static PatientProfileSettingDTO ProfileSettingSectionDTOConverter(IDataReader objReader)
        {
            PatientProfileSettingDTO ProfileObj = null;
            try
            {
                if (objReader != null)
                {
                    ProfileObj = new PatientProfileSettingDTO();

                    while (objReader.Read())
                    {

                        #region Parse Integer Properties


                        if (objReader["UserID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserID"].ToString()))
                        {
                            ProfileObj.UserID = int.Parse(objReader["UserID"].ToString());
                        }
                        if (objReader["SectionNo"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SectionNo"].ToString()))
                        {
                            ProfileObj.SectionNo = int.Parse(objReader["SectionNo"].ToString());
                        }

                        if (objReader["DisplayText"] != DBNull.Value && !string.IsNullOrEmpty(objReader["DisplayText"].ToString()))
                        {
                            ProfileObj.DisplayText = objReader["DisplayText"].ToString();
                        }
                        if (objReader["FieldData"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FieldData"].ToString()))
                        {
                            ProfileObj.FieldData = objReader["FieldData"].ToString();
                        }
                        if (objReader["UserProfileCategoryID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserProfileCategoryID"].ToString()))
                        {
                            ProfileObj.UserProfileCategoryID = int.Parse(objReader["UserProfileCategoryID"].ToString());
                        }
                        if (objReader["TooltipText"] != DBNull.Value && !string.IsNullOrEmpty(objReader["TooltipText"].ToString()))
                        {
                            ProfileObj.TooltipText = objReader["TooltipText"].ToString();
                        }
                        #endregion

                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.PROFILE_SETTING_ERROR_MSG + "ProfileStaticDataDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return ProfileObj;
        }


        internal static List<PatientAllergyInfoDTO> ConvertToPatientProfileAllergyInfo(IDataReader objReader)
        {
            List<PatientAllergyInfoDTO> allergyList = null;
            PatientAllergyInfoDTO allergyObj = null;

            try
            {
                if (objReader != null)
                {
                    allergyList = new List<PatientAllergyInfoDTO>();

                    while (objReader.Read())
                    {
                        allergyObj = new PatientAllergyInfoDTO();

                        allergyObj.PatientAllergyId = int.Parse(objReader["PatientHistoryAllergyID"].ToString());

                        if (objReader["AllergyTypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["AllergyTypeName"].ToString()))
                        {
                            allergyObj.AllergyName = objReader["AllergyTypeName"].ToString();
                        }

                        if (objReader["IsActive"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsActive"].ToString()))
                        {
                            allergyObj.IsActive = bool.Parse(objReader["IsActive"].ToString());
                        }
                        allergyList.Add(allergyObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileAllergyInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return allergyList;
        }

        internal static List<PatientDiagnosisInfoDTO> ConvertToPatientProfileDiagnosisInfo(IDataReader objReader)
        {
            List<PatientDiagnosisInfoDTO> diagnosisList = null;
            PatientDiagnosisInfoDTO diagnosisObj = null;

            try
            {
                if (objReader != null)
                {
                    diagnosisList = new List<PatientDiagnosisInfoDTO>();

                    while (objReader.Read())
                    {
                        diagnosisObj = new PatientDiagnosisInfoDTO();

                        diagnosisObj.PatientHistoryMedicalID = int.Parse(objReader["PatientHistoryMedicalID"].ToString());

                        if (objReader["ICDCodeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ICDCodeName"].ToString()))
                        {
                            diagnosisObj.ICDCodeId = objReader["ICDCodeName"].ToString();
                        }
                        if (objReader["Description"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Description"].ToString()))
                        {
                            diagnosisObj.Description = objReader["Description"].ToString();
                        }
                        if (objReader["Nature"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Nature"].ToString()))
                        {
                            diagnosisObj.Nature = objReader["Nature"].ToString();
                        }
                        if (objReader["IsActive"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsActive"].ToString()))
                        {
                            diagnosisObj.IsActive = bool.Parse(objReader["IsActive"].ToString());
                        }
                        diagnosisList.Add(diagnosisObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileDiagnosisInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return diagnosisList;
        }

        /// <summary>
        /// Author Muhammad ismail
        /// Date 4 feb 2011
        /// Method Used to convert insurance Info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        internal static List<PatientInsuracneInfoDTO> ConvertToPatientInsuranceInfo(IDataReader objReader)
        {
            List<PatientInsuracneInfoDTO> insuranceList = null;
            PatientInsuracneInfoDTO insuranceObj = null;

            try
            {
                if (objReader != null)
                {
                    insuranceList = new List<PatientInsuracneInfoDTO>();

                    while (objReader.Read())
                    {
                        insuranceObj = new PatientInsuracneInfoDTO();

                        if (objReader["PatientInsuranceId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PatientInsuranceId"].ToString()))
                        {
                            insuranceObj.PatientInsuranceId = int.Parse(objReader["PatientInsuranceId"].ToString());
                        }
                        if (objReader["PatientId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PatientId"].ToString()))
                        {
                            insuranceObj.PatientId = int.Parse(objReader["PatientId"].ToString());
                        }
                        if (objReader["InsuranceTypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceTypeName"].ToString()))
                        {
                            insuranceObj.InsuranceTypeName = objReader["InsuranceTypeName"].ToString();
                        }
                        if (objReader["PolicyNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PolicyNumber"].ToString()))
                        {
                            insuranceObj.PolicyNumber = objReader["PolicyNumber"].ToString();
                        }
                        if (objReader["InsuranceOrganizationName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceOrganizationName"].ToString()))
                        {
                            insuranceObj.InsuranceOrganizationName = objReader["InsuranceOrganizationName"].ToString();
                        }
                        if (objReader["EndDate"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EndDate"].ToString()))
                        {
                            insuranceObj.EndDate = DateTime.Parse(objReader["EndDate"].ToString());
                        }
                        if (objReader["IsEligible"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsEligible"].ToString()))
                        {
                            insuranceObj.IsEligible = bool.Parse(objReader["IsEligible"].ToString());
                        }
                        insuranceList.Add(insuranceObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileAllergyInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return insuranceList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Base User DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static BaseUserDTO ConvertToBaseUserDTO(IDataReader objReader)
        {
            BaseUserDTO baseUser = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        baseUser = new BaseUserDTO();

                        baseUser.UserId = int.Parse(objReader["UserId"].ToString());

                        if (objReader["PrefixTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PrefixTypeID"].ToString()))
                        {
                            baseUser.Prefix = new PrefixTypeDTO();
                            baseUser.Prefix.PrefixId = int.Parse(objReader["PrefixTypeID"].ToString());

                            if (objReader["PrefixName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PrefixName"].ToString()))
                            {
                                baseUser.Prefix.PrefixName = objReader["PrefixName"].ToString();
                            }
                        }

                        baseUser.FirstName = objReader["FirstName"].ToString();
                        baseUser.LastName = objReader["LastName"].ToString();

                        if (objReader["MiddleName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MiddleName"].ToString()))
                        {
                            baseUser.MiddleName = objReader["MiddleName"].ToString();
                        }

                        if (objReader["Suffix"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Suffix"].ToString()))
                        {
                            baseUser.Suffix = objReader["Suffix"].ToString();
                        }

                        baseUser.UserEmail = objReader["UserEmail"].ToString();

                        if (objReader["GenderTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderTypeID"].ToString()))
                        {
                            baseUser.Gender = new GenderTypeDTO();
                            baseUser.Gender.GenderId = int.Parse(objReader["GenderTypeID"].ToString());

                            if (objReader["GenderName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderName"].ToString()))
                            {
                                baseUser.Gender.GenderName = objReader["GenderName"].ToString();
                            }
                        }                       

                        if (objReader["SSN"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SSN"].ToString()))
                        {
                            baseUser.SSN = objReader["SSN"].ToString();
                        }

                        if (objReader["AccountStatus"] != DBNull.Value && !string.IsNullOrEmpty(objReader["AccountStatus"].ToString()))
                        {
                            baseUser.AccountStatus = new StatusTypeDTO();
                            baseUser.AccountStatus.StatusID = int.Parse(objReader["AccountStatus"].ToString());
                        }

                        if (objReader["LicenseNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LicenseNumber"].ToString()))
                        {
                            baseUser.LicenseNumber = objReader["LicenseNumber"].ToString();
                        }

                        if (objReader["FederalTaxId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FederalTaxId"].ToString()))
                        {
                            baseUser.FederalTaxId = objReader["FederalTaxId"].ToString();
                        }

                        if (objReader["EIN"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EIN"].ToString()))
                        {
                            baseUser.FederalTaxId = objReader["EIN"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToBaseUserDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return baseUser;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static PatientDTO ConvertToPatientDTO(IDataReader objReader)
        {
            PatientDTO patientDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        patientDTO = new PatientDTO();

                        patientDTO.PatientId = int.Parse(objReader["PatientID"].ToString());

                        if (objReader["MaritalStatusID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MaritalStatusID"].ToString()))
                        {
                            patientDTO.MaritalStatusType = new MaritalStatusTypeDTO();
                            patientDTO.MaritalStatusType.MaritalStatusId = int.Parse(objReader["MaritalStatusID"].ToString());

                            if (objReader["MaritalStatusName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MaritalStatusName"].ToString()))
                            {
                                patientDTO.MaritalStatusType.MaritalStatusName = objReader["MaritalStatusName"].ToString();
                            }
                        }

                        if (objReader["DateOfBirth"] != DBNull.Value && !string.IsNullOrEmpty(objReader["DateOfBirth"].ToString()))
                        {
                            patientDTO.DateOfBirth = DateTime.Parse(objReader["DateOfBirth"].ToString());
                        }

                        if (objReader["QuivusID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["QuivusID"].ToString()))
                        {
                            patientDTO.QuivusId = objReader["QuivusID"].ToString();
                        }

                        if (objReader["AllowEmailCommunication"] != DBNull.Value && !string.IsNullOrEmpty(objReader["AllowEmailCommunication"].ToString()))
                        {
                            patientDTO.AllowEmailCommunication = bool.Parse(objReader["AllowEmailCommunication"].ToString());
                        }

                        if (objReader["SpouseName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SpouseName"].ToString()))
                        {
                            patientDTO.SpouseName = objReader["SpouseName"].ToString();
                        }

                        if (objReader["MotherMaidenName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MotherMaidenName"].ToString()))
                        {
                            patientDTO.MotherMaidenName = objReader["MotherMaidenName"].ToString();
                        }

                        if (objReader["FatherName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FatherName"].ToString()))
                        {
                            patientDTO.FatherName = objReader["FatherName"].ToString();
                        }

                        if (objReader["MedicareNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MedicareNumber"].ToString()))
                        {
                            patientDTO.MedicareNumber = objReader["MedicareNumber"].ToString();
                        }

                        if (objReader["IVR_PINCode"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IVR_PINCode"].ToString()))
                        {
                            patientDTO.IVR_PINCODE = int.Parse(objReader["IVR_PINCode"].ToString());
                        }

                        if (objReader["PatientStatusID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PatientStatusID"].ToString()))
                        {
                            patientDTO.StatusType = new StatusTypeDTO();
                            patientDTO.StatusType.StatusID = int.Parse(objReader["PatientStatusID"].ToString());
                        }

                        if (objReader["ExemptReporting"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ExemptReporting"].ToString()))
                        {
                            patientDTO.IsReportingExempt = bool.Parse(objReader["ExemptReporting"].ToString());
                        }

                        if (objReader["RaceID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RaceID"].ToString()))
                        {
                            patientDTO.RaceType = new RaceTypeDTO();
                            patientDTO.RaceType.RaceId = int.Parse(objReader["RaceID"].ToString());

                            if (objReader["RaceName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RaceName"].ToString()))
                            {
                                patientDTO.RaceType.RaceName = objReader["RaceName"].ToString();
                            }
                        }

                        if (objReader["EthinicityID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EthinicityID"].ToString()))
                        {
                            patientDTO.EthinicityType = new EthinicityTypeDTO();
                            patientDTO.EthinicityType.EthinicityId = int.Parse(objReader["EthinicityID"].ToString());

                            if (objReader["EthinicityName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EthinicityName"].ToString()))
                            {
                                patientDTO.EthinicityType.EthinicityName = objReader["EthinicityName"].ToString();
                            }
                        }

                        if (objReader["OccupationID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["OccupationID"].ToString()))
                        {
                            patientDTO.OccupationType = new OccupationTypeDTO();
                            patientDTO.OccupationType.OccupationId = int.Parse(objReader["OccupationID"].ToString());

                            if (objReader["OccupationName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["OccupationName"].ToString()))
                            {
                                patientDTO.OccupationType.OccupationName = objReader["OccupationName"].ToString();
                            }
                        }

                        if (objReader["IsAlcoholStatusYes"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsAlcoholStatusYes"].ToString()))
                        {
                            patientDTO.AlcoholStatus = objReader["IsAlcoholStatusYes"].ToString();
                        }
                        else
                        {
                            patientDTO.AlcoholStatus = null;
                        }

                        if (objReader["AlcoholStatusData"] != DBNull.Value && !string.IsNullOrEmpty(objReader["AlcoholStatusData"].ToString()))
                        {
                            patientDTO.AlcoholStatusData = objReader["AlcoholStatusData"].ToString();
                        }
                        else
                        {
                            patientDTO.AlcoholStatusData = null;
                        }

                        if (objReader["IsDrugStatusYes"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsDrugStatusYes"].ToString()))
                        {
                            patientDTO.DrugStatus = objReader["IsDrugStatusYes"].ToString();
                        }
                        else
                        {
                            patientDTO.DrugStatus = null;
                        }

                        if (objReader["DrugStatusData"] != DBNull.Value && !string.IsNullOrEmpty(objReader["DrugStatusData"].ToString()))
                        {
                            patientDTO.DrugStatusData = objReader["DrugStatusData"].ToString();
                        }
                        else
                        {
                            patientDTO.DrugStatusData = null;
                        }

                        if (objReader["EmployerName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EmployerName"].ToString()))
                        {
                            patientDTO.EmployerName = objReader["EmployerName"].ToString();
                        }

                        if (objReader["IsProfileComplete"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsProfileComplete"].ToString()))
                        {
                            patientDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                        }

                        patientDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());

                        if (objReader["SmokingStatus"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SmokingStatus"].ToString()))
                        {
                            if (objReader["SmokingTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SmokingTypeID"].ToString()))
                            {
                                patientDTO.SmokingType = new SmokingTypeDTO();
                                patientDTO.SmokingType.SmokingTypeId = int.Parse(objReader["SmokingTypeID"].ToString());
                            }

                            if (objReader["SmokingTypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SmokingTypeName"].ToString()))
                            {
                                patientDTO.SmokingType.SmokingTypeName = objReader["SmokingTypeName"].ToString();
                            }
                        }

                        //# region New Fields added By Ismail :)

                        //if (objReader["MaritalStatusName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MaritalStatusName"].ToString()))
                        //{
                        //    patientDTO.MaritalStatusType = new MaritalStatusTypeDTO();
                        //    patientDTO.MaritalStatusType.MaritalStatusName = objReader["MaritalStatusName"].ToString();
                        //}

                        //if (objReader["RaceName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RaceName"].ToString()))
                        //{
                        //    patientDTO.RaceType = new RaceTypeDTO();
                        //    patientDTO.RaceType.RaceName = objReader["RaceName"].ToString();
                        //}

                        //if (objReader["EthinicityName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EthinicityName"].ToString()))
                        //{
                        //    patientDTO.RaceType = new RaceTypeDTO();
                        //    patientDTO.EthinicityType.EthinicityName = objReader["EthinicityName"].ToString();
                        //}

                        //if (objReader["OccupationName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["OccupationName"].ToString()))
                        //{
                        //    patientDTO.OccupationType = new OccupationTypeDTO();
                        //    patientDTO.OccupationType.OccupationName = objReader["OccupationName"].ToString();
                        //}

                        //if (objReader["SmokingTypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SmokingTypeName"].ToString()))
                        //{
                        //    patientDTO.SmokingType = new SmokingTypeDTO();
                        //    patientDTO.SmokingType.SmokingTypeName = objReader["SmokingTypeName"].ToString();
                        //}

                        //# endregion New Fields added By Ismail
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientDTO;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static PatientGuarantorDTO ConvertToPatientGurantorDTO(IDataReader objReader)
        {
            PatientGuarantorDTO guarantorDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        guarantorDTO = new PatientGuarantorDTO();

                        guarantorDTO.GuarantorId = int.Parse(objReader["PatientGuarantorID"].ToString());
                        guarantorDTO.PatientId = int.Parse(objReader["PatientID"].ToString());

                        if (objReader["GuarantorFirstName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GuarantorFirstName"].ToString()))
                        {
                            guarantorDTO.GurantorFirstName = objReader["GuarantorFirstName"].ToString();
                        }

                        if (objReader["GuarantorLastName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GuarantorLastName"].ToString()))
                        {
                            guarantorDTO.GuarantorLastName = objReader["GuarantorLastName"].ToString();
                        }

                        if (objReader["GenderID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderID"].ToString()))
                        {
                            guarantorDTO.GenderType = new GenderTypeDTO();
                            guarantorDTO.GenderType.GenderId = int.Parse(objReader["GenderID"].ToString());

                            if (objReader["GenderName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderName"].ToString()))
                            {
                                guarantorDTO.GenderType.GenderName = objReader["GenderName"].ToString();
                            }
                        }

                        if (objReader["RelationShipID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RelationShipID"].ToString()))
                        {
                            guarantorDTO.RelationshipType = new RelationshipTypeDTO();
                            guarantorDTO.RelationshipType.RelationshipId = int.Parse(objReader["RelationShipID"].ToString());

                            if (objReader["RelationshipName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RelationshipName"].ToString()))
                            {
                                guarantorDTO.RelationshipType.RelationshipName = objReader["RelationshipName"].ToString();
                            }
                        }

                        if (objReader["DateOfBirth"] != DBNull.Value && !string.IsNullOrEmpty(objReader["DateOfBirth"].ToString()))
                        {
                            guarantorDTO.DOB = DateTime.Parse(objReader["DateOfBirth"].ToString());
                        }

                        if (objReader["StatusID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["StatusID"].ToString()))
                        {
                            guarantorDTO.StatusType = new StatusTypeDTO();
                            guarantorDTO.StatusType.StatusID = int.Parse(objReader["StatusID"].ToString());
                        }

                        if (objReader["MaritalStatusID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MaritalStatusID"].ToString()))
                        {
                            guarantorDTO.MaritalStatusType = new MaritalStatusTypeDTO();
                            guarantorDTO.MaritalStatusType.MaritalStatusId = int.Parse(objReader["MaritalStatusID"].ToString());

                            if (objReader["MaritalStatusName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MaritalStatusName"].ToString()))
                            {
                                guarantorDTO.MaritalStatusType.MaritalStatusName = objReader["MaritalStatusName"].ToString();
                            }
                        }

                        guarantorDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientGurantorDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return guarantorDTO;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient Lanaguage DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<PatientLanguageDTO> ConvertToPatientLanguageDTO(IDataReader objReader)
        {
            List<PatientLanguageDTO> PatientLanguageList = null;
            try
            {
                if (objReader != null)
                {
                    PatientLanguageList = new List<PatientLanguageDTO>();

                    while (objReader.Read())
                    {
                        PatientLanguageDTO objPatientLanguage = new PatientLanguageDTO();

                        objPatientLanguage.PatientLanguageId = int.Parse(objReader["PatientLanguageID"].ToString());
                        objPatientLanguage.PatientId = int.Parse(objReader["PatientID"].ToString());
                        objPatientLanguage.LanguageID = int.Parse(objReader["LanguageID"].ToString());

                        if (objReader["IsPreferred"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsPreferred"].ToString()))
                        {
                            objPatientLanguage.IsPreffered = bool.Parse(objReader["IsPreferred"].ToString());
                        }

                        if (objReader["LanguageName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LanguageName"].ToString()))
                        {
                            objPatientLanguage.LanguageName = objReader["LanguageName"].ToString();
                        }

                        if (objReader["LanguageAbilityModeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LanguageAbilityModeID"].ToString()))
                        {
                            objPatientLanguage.PatientLanguageAbilityMode = new LanguageAbilityModeDTO();
                            objPatientLanguage.PatientLanguageAbilityMode.LanguageAbilityModeId = int.Parse(objReader["LanguageAbilityModeID"].ToString());

                            if (objReader["LangaugeAbilityModeDescription"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LangaugeAbilityModeDescription"].ToString()))
                            {
                                objPatientLanguage.PatientLanguageAbilityMode.LangaugeAbilityModeDescription = objReader["LangaugeAbilityModeDescription"].ToString();
                            }
                        }

                        if (objReader["CountryID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["CountryID"].ToString()))
                        {
                            objPatientLanguage.Country = new CountryDTO();
                            objPatientLanguage.Country.CountryID = int.Parse(objReader["CountryID"].ToString());

                            //CountryName cann't be loaded
                        }

                        PatientLanguageList.Add(objPatientLanguage);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientLanguageDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return PatientLanguageList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient Emergency Contact DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<PatientEmergencyContactDTO> ConvertToPatientEmergencyContactDTO(IDataReader objReader)
        {
            List<PatientEmergencyContactDTO> PatientEmergencyContactList = null;
            try
            {
                if (objReader != null)
                {
                    PatientEmergencyContactList = new List<PatientEmergencyContactDTO>();

                    while (objReader.Read())
                    {
                        PatientEmergencyContactDTO objPatientEmergencyContact = new PatientEmergencyContactDTO();

                        objPatientEmergencyContact.PatientEmergencyContactId = int.Parse(objReader["PatientEmergencyContactID"].ToString());
                        objPatientEmergencyContact.PatientId = int.Parse(objReader["PatientID"].ToString());

                        if (objReader["FirstName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FirstName"].ToString()))
                        {
                            objPatientEmergencyContact.FirstName = objReader["FirstName"].ToString();
                        }

                        if (objReader["LastName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LastName"].ToString()))
                        {
                            objPatientEmergencyContact.LastName = objReader["LastName"].ToString();
                        }

                        if (objReader["RelationShipID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RelationShipID"].ToString()))
                        {
                            objPatientEmergencyContact.RelationShipType = new RelationshipTypeDTO();
                            objPatientEmergencyContact.RelationShipType.RelationshipId = int.Parse(objReader["RelationShipID"].ToString());

                            if (objReader["RelationshipName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RelationshipName"].ToString()))
                            {
                                objPatientEmergencyContact.RelationShipType.RelationshipName = objReader["RelationshipName"].ToString();
                            }
                        }

                        if (objReader["StatusID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["StatusID"].ToString()))
                        {
                            objPatientEmergencyContact.StatusType = new StatusTypeDTO();
                            objPatientEmergencyContact.StatusType.StatusID = int.Parse(objReader["StatusID"].ToString());
                        }

                        if (objReader["AllowCommunication"] != DBNull.Value && !string.IsNullOrEmpty(objReader["AllowCommunication"].ToString()))
                        {
                            objPatientEmergencyContact.AllowCommunication = bool.Parse(objReader["AllowCommunication"].ToString());
                        }

                        objPatientEmergencyContact.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());

                        PatientEmergencyContactList.Add(objPatientEmergencyContact);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientEmergencyContactDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return PatientEmergencyContactList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient Allergy DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<PatientAllergyDTO> ConvertToPatientAllergyDTO(IDataReader objReader)
        {
            List<PatientAllergyDTO> PatientAllergyList = null;
            try
            {
                if (objReader != null)
                {
                    PatientAllergyList = new List<PatientAllergyDTO>();

                    while (objReader.Read())
                    {
                        PatientAllergyDTO objPatientAllergy = new PatientAllergyDTO();

                        objPatientAllergy.PatientHistoryAllergyId = int.Parse(objReader["PatientHistoryAllergyID"].ToString());
                        objPatientAllergy.PatientId = int.Parse(objReader["PatientID"].ToString());

                        if (objReader["AllergyID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["AllergyID"].ToString()))
                        {
                            objPatientAllergy.AllergyType = new AllergyTypeDTO();
                            objPatientAllergy.AllergyType.AllergyTypeIdSeq = int.Parse(objReader["AllergyID"].ToString());

                            if (objReader["AllergyTypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["AllergyTypeName"].ToString()))
                            {
                                objPatientAllergy.AllergyType.AllergyName = objReader["AllergyTypeName"].ToString();
                            }
                        }

                        if (objReader["Reaction"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Reaction"].ToString()))
                        {
                            objPatientAllergy.Reaction = objReader["Reaction"].ToString();
                        }

                        if (objReader["IsActive"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsActive"].ToString()))
                        {
                            objPatientAllergy.IsActive = objReader["IsActive"].ToString();
                        }

                        PatientAllergyList.Add(objPatientAllergy);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientAllergyDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return PatientAllergyList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient Medical History DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<PatientMedicalHistoryDTO> ConvertToPatientMedicalHistoryDTO(IDataReader objReader)
        {
            List<PatientMedicalHistoryDTO> PatientMedicalHistoryList = null;
            try
            {
                if (objReader != null)
                {
                    PatientMedicalHistoryList = new List<PatientMedicalHistoryDTO>();

                    while (objReader.Read())
                    {
                        PatientMedicalHistoryDTO objPatientMedicalHistory = new PatientMedicalHistoryDTO();

                        objPatientMedicalHistory.PatientHistoryMedicalId = int.Parse(objReader["PatientHistoryMedicalID"].ToString());
                        objPatientMedicalHistory.PatientId = int.Parse(objReader["PatientID"].ToString());

                        if (objReader["ICDCodeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ICDCodeName"].ToString()))
                        {
                            objPatientMedicalHistory.ICDCodeType = new ICDCodeDTO();
                            objPatientMedicalHistory.ICDCodeType.IcdCodeName = objReader["ICDCodeName"].ToString();

                            objPatientMedicalHistory.ICDCodeType.ProblemType = objReader["ProblemType"].ToString();

                            if (objReader["Description"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ICDCodeName"].ToString()))
                            {
                                objPatientMedicalHistory.ICDCodeType.IcdCodeDescription = objReader["Description"].ToString();
                            }
                        }

                        if (objReader["IsActive"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsActive"].ToString()))
                        {
                            objPatientMedicalHistory.IsActive = objReader["IsActive"].ToString();
                        }

                        PatientMedicalHistoryList.Add(objPatientMedicalHistory);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientMedicalHistoryDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return PatientMedicalHistoryList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient Family History DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<PatientFamilyHistoryDTO> ConvertToPatientFamilyHistoryDTO(IDataReader objReader)
        {
            List<PatientFamilyHistoryDTO> PatientFamilyHistoryList = null;
            try
            {
                if (objReader != null)
                {
                    PatientFamilyHistoryList = new List<PatientFamilyHistoryDTO>();

                    while (objReader.Read())
                    {
                        PatientFamilyHistoryDTO objPatientFamilyHistory = new PatientFamilyHistoryDTO();

                        objPatientFamilyHistory.PatientHistoryFamilyId = int.Parse(objReader["PatientHistoryFamilyID"].ToString());
                        objPatientFamilyHistory.PatientId = int.Parse(objReader["PatientID"].ToString());

                        if (objReader["ICDCodeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ICDCodeName"].ToString()))
                        {
                            objPatientFamilyHistory.ICDCodeType = new ICDCodeDTO();
                            objPatientFamilyHistory.ICDCodeType.IcdCodeName = objReader["ICDCodeName"].ToString();

                            objPatientFamilyHistory.ICDCodeType.ProblemType = objReader["ProblemType"].ToString();

                            if (objReader["Description"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ICDCodeName"].ToString()))
                            {
                                objPatientFamilyHistory.ICDCodeType.IcdCodeDescription = objReader["Description"].ToString();
                            }
                        }

                        if (objReader["RelationshipTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RelationshipTypeID"].ToString()))
                        {
                            objPatientFamilyHistory.RelationshipType = new RelationshipTypeDTO();
                            objPatientFamilyHistory.RelationshipType.RelationshipId = int.Parse(objReader["RelationshipTypeID"].ToString());

                            if (objReader["RelationshipName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RelationshipName"].ToString()))
                            {
                                objPatientFamilyHistory.RelationshipType.RelationshipName = objReader["RelationshipName"].ToString();
                            }
                        }

                        if (objReader["IsActive"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsActive"].ToString()))
                        {
                            objPatientFamilyHistory.IsActive = objReader["IsActive"].ToString();
                        }

                        PatientFamilyHistoryList.Add(objPatientFamilyHistory);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientFamilyHistoryDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return PatientFamilyHistoryList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient Insurance DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<PatientInsuranceDTO> ConvertToPatientInsuranceDTO(IDataReader objReader)
        {
            List<PatientInsuranceDTO> PatientInsuranceList = null;
            try
            {
                if (objReader != null)
                {
                    PatientInsuranceList = new List<PatientInsuranceDTO>();

                    while (objReader.Read())
                    {
                        PatientInsuranceDTO objPatientInsurance = new PatientInsuranceDTO();

                        objPatientInsurance.PatientInsuranceId = int.Parse(objReader["PatientInsuranceId"].ToString());
                        objPatientInsurance.PatientId = int.Parse(objReader["PatientID"].ToString());

                        if (objReader["InsuranceType"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceType"].ToString()))
                        {
                            objPatientInsurance.InsuranceType = new InsuranceTypeDTO();
                            objPatientInsurance.InsuranceType.InsuranceTypeId = int.Parse(objReader["InsuranceType"].ToString());

                            if (objReader["InsuranceTypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceTypeName"].ToString()))
                            {
                                objPatientInsurance.InsuranceType.InsuranceTypeName = objReader["InsuranceTypeName"].ToString();
                            }
                        }

                        if (objReader["InsuranceNatureId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceNatureId"].ToString()))
                        {
                            objPatientInsurance.InsuranceNature = new InsuranceNatureDTO();
                            objPatientInsurance.InsuranceNature.InsuranceNatureId = int.Parse(objReader["InsuranceNatureId"].ToString());

                            if (objReader["InsuranceNatureName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceNatureName"].ToString()))
                            {
                                objPatientInsurance.InsuranceNature.Description = objReader["InsuranceNatureName"].ToString();
                            }
                        }

                        if (objReader["InsuranceOrganizationId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceOrganizationId"].ToString()))
                        {
                            objPatientInsurance.InsuranceOrganization = new InsuranceOrganizationDTO();
                            objPatientInsurance.InsuranceOrganization.InsuranceOrganizationID = int.Parse(objReader["InsuranceOrganizationId"].ToString());

                            if (objReader["InsuranceOrganizationName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceOrganizationName"].ToString()))
                            {
                                objPatientInsurance.InsuranceOrganization.InsuranceOrganizationName = objReader["InsuranceOrganizationName"].ToString();
                            }
                        }

                        if (objReader["PolicyNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PolicyNumber"].ToString()))
                        {
                            objPatientInsurance.PolicyNumber = objReader["PolicyNumber"].ToString();
                        }

                        if (objReader["GroupNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GroupNumber"].ToString()))
                        {
                            objPatientInsurance.GroupNumber = objReader["GroupNumber"].ToString();
                        }

                        if (objReader["InsurancePlan"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsurancePlan"].ToString()))
                        {
                            objPatientInsurance.InsurancePlan = objReader["InsurancePlan"].ToString();
                        }

                        if (objReader["PersonName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PersonName"].ToString()))
                        {
                            objPatientInsurance.InsuredPersonName = objReader["PersonName"].ToString();
                        }

                        if (objReader["PersonDateOfBirth"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PersonDateOfBirth"].ToString()))
                        {
                            objPatientInsurance.InsuredPersonDOB = DateTime.Parse(objReader["PersonDateOfBirth"].ToString());
                        }

                        if (objReader["GenderId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderId"].ToString()))
                        {
                            objPatientInsurance.InsuredPersonGender = new GenderTypeDTO();
                            objPatientInsurance.InsuredPersonGender.GenderId = int.Parse(objReader["GenderId"].ToString());

                            //GenderTypeName cann't be loaded
                        }

                        if (objReader["StartDate"] != DBNull.Value && !string.IsNullOrEmpty(objReader["StartDate"].ToString()))
                        {
                            objPatientInsurance.StartDate = DateTime.Parse(objReader["StartDate"].ToString());
                        }

                        if (objReader["EndDate"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EndDate"].ToString()))
                        {
                            objPatientInsurance.StartDate = DateTime.Parse(objReader["EndDate"].ToString());
                        }

                        if (objReader["Copay"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Copay"].ToString()))
                        {
                            objPatientInsurance.Copay = objReader["Copay"].ToString();
                        }

                        if (objReader["IsEligible"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsEligible"].ToString()))
                        {
                            objPatientInsurance.IsEligible = objReader["IsEligible"].ToString();
                        }

                        if (objReader["InsuranceStatus"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceStatus"].ToString()))
                        {
                            objPatientInsurance.InsuranceStatus = new StatusTypeDTO();
                            objPatientInsurance.InsuranceStatus.StatusID = int.Parse(objReader["InsuranceStatus"].ToString());
                        }

                        PatientInsuranceList.Add(objPatientInsurance);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientInsuranceDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return PatientInsuranceList;
        }

        internal static List<ImageTypeDTO> ImageTypeTOConverter(IDataReader objReader)
        {
            List<ImageTypeDTO> ImageTypeList = null;
            ImageTypeDTO imageTypeObj = null;

            try
            {
                if (objReader != null)
                {
                    ImageTypeList = new List<ImageTypeDTO>();

                    while (objReader.Read())
                    {
                        imageTypeObj = new ImageTypeDTO();

                        if (objReader["ImageTypeId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ImageTypeId"].ToString()))
                        {
                            imageTypeObj.ImageTypeId = int.Parse(objReader["ImageTypeId"].ToString());
                        }

                        if (objReader["TypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["TypeName"].ToString()))
                        {
                            imageTypeObj.TypeName = objReader["TypeName"].ToString();
                        }
                        if (objReader["TypeDescription"] != DBNull.Value && !string.IsNullOrEmpty(objReader["TypeDescription"].ToString()))
                        {
                            imageTypeObj.TypeDescription = objReader["TypeDescription"].ToString();
                        }

                        ImageTypeList.Add(imageTypeObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileAllergyInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return ImageTypeList;
        }

        internal static List<UserBasicInfoDTO> UserBasicInfoConverter(SqlDataReader objReader)
        {
            List<UserBasicInfoDTO> UserList = null;
            UserBasicInfoDTO UserObj = null;

            try
            {
                if (objReader != null)
                {
                    UserList = new List<UserBasicInfoDTO>();

                    while (objReader.Read())
                    {
                        UserObj = new UserBasicInfoDTO();

                        if (objReader["UserId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserId"].ToString()))
                        {
                            UserObj.UserId = int.Parse(objReader["UserId"].ToString());
                        }
                        if (objReader["PrefixTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PrefixTypeID"].ToString()))
                        {
                            UserObj.PrefixTypeID = int.Parse(objReader["PrefixTypeID"].ToString());
                        }
                        if (objReader["GenderTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderTypeID"].ToString()))
                        {
                            UserObj.GenderTypeID = int.Parse(objReader["GenderTypeID"].ToString());
                        }
                        if (objReader["GenderName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderName"].ToString()))
                        {
                            UserObj.GenderName = objReader["GenderName"].ToString();
                        }
                        if (objReader["FirstName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FirstName"].ToString()))
                        {
                            UserObj.FirstName = objReader["FirstName"].ToString();
                        }
                        if (objReader["LastName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LastName"].ToString()))
                        {
                            UserObj.LastName = objReader["LastName"].ToString();
                        }
                        if (objReader["MiddleName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MiddleName"].ToString()))
                        {
                            UserObj.MiddleName = objReader["MiddleName"].ToString();
                        }
                        if (objReader["Suffix"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Suffix"].ToString()))
                        {
                            UserObj.Suffix = objReader["Suffix"].ToString();
                        }

                        if (objReader["SSN"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SSN"].ToString()))
                        {
                            UserObj.SSN = objReader["SSN"].ToString();
                        }

                        UserList.Add(UserObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileAllergyInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return UserList;
        }
    }
}
