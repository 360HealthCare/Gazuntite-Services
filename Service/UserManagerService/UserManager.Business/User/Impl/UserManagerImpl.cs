using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;


using Quivus.Model.User.Patient;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using UserManagerService.DataAccess;
using Quivus.Utility.Factory;

using Quivus.Utility.Utilities;
using UserManager.Business.Validators;

using Quivus.Model.Common;
using Quivus.Model.PatientProfileCustomization;
using Quivus.Model.AddressBook;

using UserManagerService.DataAccess.Patient;

using UserManager.Business.Utility;
using Quivus.Model.User.Patient;
using Quivus.Model.User;


namespace UserManager.Business.User.Impl
{
    public class UserManagerImpl : IUserManager
    {

        UserDAO userDAO = null;
        PatientDAO patientDAO = null;
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
        public PatientProfileCompleteSettingDTO LoadPatientProfileSettings(int clinicBranchId, int providerId, int PatientId)
        {
            PatientProfileCompleteSettingDTO response = null;
            PatientProfileSettingDTO profileSettingsSectionFirst = null;
            PatientProfileSettingDTO profileSettingsSectionSecond = null;
            try
            {
                QuivusLogger.Info("LoadPatientProfileSettings Called PARAM clinicBranchId::" + clinicBranchId + "providerId:: " + providerId);

                if (providerId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
                else if (clinicBranchId < 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }
                // Now make Call For Each Section i.e Section 1, 2 & 3

                profileSettingsSectionFirst = userDAO.LoadPatientProfileSectionSettings(clinicBranchId, providerId, PatientId, ProjectConstant.SECTION_FIRST);
                profileSettingsSectionSecond = userDAO.LoadPatientProfileSectionSettings(clinicBranchId, providerId, PatientId, ProjectConstant.SECTION_SECOND);

                if (profileSettingsSectionFirst != null || profileSettingsSectionSecond != null)
                {
                    if (response == null)
                    {
                        response = new PatientProfileCompleteSettingDTO();
                    }

                    response.SectionFirstData = profileSettingsSectionFirst;
                    response.SectionSecondData = profileSettingsSectionSecond;
                }
                #region Return Section third Info for Time Being By Ismail :)

                List<PatientProfileSettingDTO> ProfileSettingList = userDAO.LoadProfileSettingCollection(clinicBranchId, providerId);

                response = PopulateSectionThree(response, ProfileSettingList, PatientId);

                patientDAO.SaveRecentPatient(clinicBranchId, providerId, PatientId); // Save Into Most Recent Pateint List of provider

                /* For Testing Purpose
                 * 
                 * ProfileSettingsCollectionDTO profileSettingsSectionThird = new ProfileSettingsCollectionDTO();
                 * profileSettingsSectionThird.CollectionType = ProjectConstant.ALLERGY_COLLECTION;
                 * List<PatientAllergyInfoDTO> allergyList = new List<PatientAllergyInfoDTO>();
                 * profileSettingsSectionThird.AllergyList = allergyList;
                 * if (response == null)
                 * {
                 *  response = new PatientProfileSettingResponse();
                 *  response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                 * }
                 * response.SectionThirdData = profileSettingsSectionThird;
                */
                #endregion
            }
            catch
            {
                throw;
            }
            return response;
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
        public PatientProfileCompleteSettingDTO LoadPatientProfileSettingsBySection(int clinicBranchId, int providerId, int PatientId, int SectionId)
        {
            {
                PatientProfileCompleteSettingDTO response = null;
                PatientProfileSettingDTO profileSettingsSectionFirst = null;
                PatientProfileSettingDTO profileSettingsSectionSecond = null;
                try
                {
                    QuivusLogger.Info("LoadPatientProfileSettingsBySection Called PARAM clinicBranchId::" + clinicBranchId + "providerId:: " + providerId
                        + "PatientId ::" + PatientId + " SectionId:: " + SectionId);

                    if (providerId <= 0)
                    {
                        throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                    }
                    else if (clinicBranchId < 0)
                    {
                        throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                    }
                    else if (PatientId < 0)
                    {
                        throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                    }
                    else if (SectionId < 0)
                    {
                        throw new InvalidArgumentException(ProjectConstant.SECTION_ID_INVALID_ERROR_MSG);
                    }

                    if (userDAO == null)
                    {
                        userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                    }

                    // Now check & make Call For appropriate  Section i.e either Section 1, 2 & 3
                    if (SectionId.Equals(ProjectConstant.SECTION_FIRST))
                    {
                        profileSettingsSectionFirst = userDAO.LoadPatientProfileSectionSettings(clinicBranchId, providerId, PatientId, ProjectConstant.SECTION_FIRST);
                    }
                    if (SectionId.Equals(ProjectConstant.SECTION_SECOND))
                    {
                        profileSettingsSectionSecond = userDAO.LoadPatientProfileSectionSettings(clinicBranchId, providerId, PatientId, ProjectConstant.SECTION_SECOND);
                    }

                    if (profileSettingsSectionFirst != null || profileSettingsSectionSecond != null)
                    {
                        if (response == null)
                        {
                            response = new PatientProfileCompleteSettingDTO();
                        }

                        response.SectionFirstData = profileSettingsSectionFirst;
                        response.SectionSecondData = profileSettingsSectionSecond;


                    }
                    #region Return Section third Info  By Ismail :)
                    if (SectionId.Equals(ProjectConstant.SECTION_THIRD))
                    {
                        List<PatientProfileSettingDTO> ProfileSettingList = userDAO.LoadProfileSettingCollection(clinicBranchId, providerId);
                        response = PopulateSectionThree(response, ProfileSettingList, PatientId);
                    }
                    #endregion
                }

                catch
                {
                    throw;
                }

                return response;
            }
        }


        /// <summary>
        /// Author muhammad ismail
        /// Date Feb 04 2011
        /// Method Check UserProfileCategoryName & on UserProfileCategoryName basis load appropriate collection
        /// i.e Allergy or diagnosis or immunization or insurance
        /// </summary>
        /// <param name="response"></param>
        /// <param name="ProfileSettingList"></param>
        /// <returns></returns>
        private PatientProfileCompleteSettingDTO PopulateSectionThree(PatientProfileCompleteSettingDTO response, List<PatientProfileSettingDTO> ProfileSettingList, int patientId)
        {
            ProfileSettingsCollectionDTO profileSettingsSectionThird = new ProfileSettingsCollectionDTO();

            if (ProfileSettingList != null && ProfileSettingList.Count > 0)
            {
                var query =
                 from d in ProfileSettingList
                 where d.SectionNo.Equals(ProjectConstant.SECTION_THIRD)
                 select d;

                if (query != null && query.ToArray().Length > 0)
                {
                    PatientProfileSettingDTO SectionThird = query.First();
                    if (SectionThird != null)
                    {
                        if (response == null)
                        {
                            response = new PatientProfileCompleteSettingDTO();
                        }

                        String catgeoryName = SectionThird.UserProfileCategoryName;
                        profileSettingsSectionThird.CollectionType = catgeoryName;
                        profileSettingsSectionThird.UserUserProfileCategoryID = ProfileSettingList[0].UserUserProfileCategoryID; // Need to To Be Discuss With Atta We are getting mutliple records

                        if (patientDAO == null)
                        {
                            patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                        }

                        if (catgeoryName.Equals(ProjectConstant.ALLERGY_COLLECTION))
                        {
                            profileSettingsSectionThird.AllergyList = patientDAO.LoadPatientAllergiesByPatientId(patientId);
                        }
                        if (catgeoryName.Equals(ProjectConstant.DIAGNOSIS_COLLECTION))
                        {
                            profileSettingsSectionThird.DiagnosisList = patientDAO.LoadPatientDiagnosisByPatientId(patientId);
                        }
                        if (catgeoryName.Equals(ProjectConstant.IMMUNIZATION_COLLECTION))
                        {
                            profileSettingsSectionThird.ImmunizationList = patientDAO.LoadPatientImmunizationByPatientId(patientId);
                        }
                        if (catgeoryName.Equals(ProjectConstant.CHRONICMEDICATION_COLLECTION))
                        {
                            profileSettingsSectionThird.ChronicMedicationList = patientDAO.LoadPatientMedicationByPatientId(patientId);
                        }

                        if (catgeoryName.Equals(ProjectConstant.INSURANCE_COLLECTION))
                        {
                            profileSettingsSectionThird.InsuranceList = patientDAO.LoadPatientInsuranceForProvider(patientId);
                        }

                        //M 07 Feb 2011
                        //response.SectionThirdData = new ProfileSettingsCollectionDTO();
                        //response.SectionThirdData.UserUserProfileCategoryID = SectionThird.UserUserProfileCategoryID; // commented By Ismail not in use

                        response.SectionThirdData = profileSettingsSectionThird;
                    }
                }
            }
            else
            {
                if (response == null)
                {
                    response = new PatientProfileCompleteSettingDTO();
                }

                profileSettingsSectionThird.AllergyList = null;
                profileSettingsSectionThird.CollectionType = ProjectConstant.ALLERGY_COLLECTION;
                response.SectionThirdData = profileSettingsSectionThird;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to Update Patient Profile Settings 
        /// </summary>
        /// <param name="ProfileSettingObj"></param>

        /// <returns></returns>
        public int ManagePatientProfileSettings(PatientProfileSettingDTO ProfileSettingObj)
        {
            int UpdateResult = 0;

            try
            {
                QuivusLogger.Info("ManagePatientProfileSettings Called PARAM ProfileSettingObj::" + ProfileSettingObj);

                StringBuilder sb = new StringBuilder();
                if (QUserValidator.ValidatePatientSettingInfo(ProfileSettingObj, ref sb) == false)
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                UpdateResult = userDAO.ManagePatientProfileSettings(ProfileSettingObj);
            }
            catch
            {
                throw;
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
            List<ProfileStaticDataDTO> profileSettingsList = null;
            try
            {
                QuivusLogger.Info("LoadProfileStaticData Called PARAM clinicBranchId::");
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                profileSettingsList = userDAO.LoadProfileStaticData();
            }
            catch
            {
                throw;
            }

            return profileSettingsList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the user email id by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>        
        public String GetUserEmailByUserId(int userId)
        {
            String response = null;
            string userEmailID = "";

            try
            {
                QuivusLogger.Info("GetUserEmailByUserId Called PARAM userId::");

                if (userId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                userEmailID = userDAO.GetUserEmailByUserId(userId);
                response = userEmailID;
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get the RecGuid of respective table, its a internal call used to get guid before saving Address Book
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param> 
        public Guid GetPartyGuidByProfileIdAndTypeId(int roleProfileID, int addressPartyTypeID)
        {
            Guid guid = System.Guid.Empty;

            try
            {
                QuivusLogger.Info("calling GetPartyGuid PARAM :: roleProfileID " + roleProfileID + " addressPartyTypeID " + addressPartyTypeID);

                if (userDAO == null)
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));

                if (addressPartyTypeID == (int)ProjectConstant.ADDRESS_PARTY_TYPE.Patient)
                {
                    guid = userDAO.GetPatientGuidByPatientId(roleProfileID);
                }
                else if (addressPartyTypeID == (int)ProjectConstant.ADDRESS_PARTY_TYPE.PatientGuarantor)
                {
                    guid = userDAO.GetGurantorGuidByGurantorId(roleProfileID);
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return guid;
        }

        public List<UserBasicInfoDTO> GetUserBasicInfoById(List<int> ProviderIdList)
        {
            List<UserBasicInfoDTO> response = null;

            try
            {
                QuivusLogger.Info("GetUserBasicInfoById Called PARAM ProviderIdList::" + ProviderIdList);

                if (ProviderIdList == null)
                {
                    throw new InvalidArgumentException(ProjectConstant.EMPTY_LIST);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                response = userDAO.GetUserBasicInfoById(ProviderIdList);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// tell if a user is enrolled as provider on a specific branch
        /// </summary> 
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        public bool IsProviderAtClinic(int userId, int clinicBranchId)
        {
            bool isProvider = false;

            try
            {
                QuivusLogger.Info("calling IsProviderAtClinic PARAM :: userId " + userId + " clinicBranchId " + clinicBranchId);

                if (userDAO == null)
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));

                isProvider = userDAO.IsProviderAtClinic(userId, clinicBranchId);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return isProvider;
        }    

         /// <summary>
        /// Author Mansoor
        /// Date 28 Feb 2011
        /// get the RecGuid of respective table, used to load the patient profile customized setting (AB)
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="partyTypeID"></param>
        /// <returns></returns>        
        public Guid GetPartyGuidByPatientIdAndPartyTypeId(int patientId, int partyTypeID)
        {
            Guid guid = System.Guid.Empty;

            try
            {
                QuivusLogger.Info("calling GetPartyGuid PARAM :: patientId " + patientId + " partyTypeID " + partyTypeID);

                if (userDAO == null)
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));

                if (partyTypeID == (int)ProjectConstant.ADDRESS_PARTY_TYPE.EmergencyContact)
                {
                    guid = userDAO.GetEmergencyContactGuidByPatientId(patientId);
                }
                else if (partyTypeID == (int)ProjectConstant.ADDRESS_PARTY_TYPE.PatientGuarantor)
                {
                    guid = userDAO.GetGurantorGuidByPatientId(patientId);
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return guid;
        }
    }
}
