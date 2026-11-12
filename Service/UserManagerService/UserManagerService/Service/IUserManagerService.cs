using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


using Quivus.Model.User;
using Quivus.Model.User.Patient;
using Quivus.Model.Common;
using Quivus.Model.AddressBook;

using Quivus.Model.Visit;

//using Quivus.Utility.ServiceCustomResponse;
//using Quivus.Model.ServiceCustomResponse.AddressBook;
//using Quivus.Model.ServiceCustomResponse.Allergy;
//using Quivus.Model.ServiceCustomResponse.ICDCode;
//using Quivus.Model.ServiceCustomResponse.User;
//using Quivus.Model.ServiceCustomResponse.User.Patient;
//using Quivus.Model.ServiceCustomResponse.StaticData;
//using Quivus.Model.ServiceCustomResponse.Search;
//using Quivus.Model.ServiceCustomResponse;
//using Quivus.Model.ServiceCustomResponse.QuickLinks;

using Quivus.Utility.Fault;


//using Quivus.Model.ServiceCustomResponse.Visit;
using Quivus.Model.QuickLink;
using Quivus.Model.SearchEngine;

using Quivus.Model.Search;

using Quivus.Model.RecentPatient;
using Quivus.Model.PatientProfileCustomization;
using Quivus.Model.TypeDTOs;
using Quivus.Model.User;





namespace UserManagerService.Service
{
    [ServiceContract]
    public interface IUserManagerService
    {
        //  [OperationContract(Name="TestMethod")] Commented By Ismail
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //string Test();      

        /// <summary>
        /// Author M.Ismail
        /// Created Date Jan 11 2010
        /// This Method is Used TO Authenicate the User 
        /// In Case Of Successfull verfication system return User DTO Object
        /// Update by Mansoor
        /// Date 13 Jan 2010
        /// Response type changed to custom to catter the unavaliblity of Exception handling on
        /// Clinet side
        /// Update by Mansoor
        /// Date 14 Jan 2010
        /// Incase of unsucessfull scenario system sends the error message and code
        /// Clinet side
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserPassword"></param>
        /// <returns></returns>
        /// 
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        LoginDTO AuthenticateUser(string UserId, string UserPassword);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load the user login history
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <param name="clinicBranch"></param>
        /// <param name="isSucessFull"></param>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        void LogLoginHistory(int userId, int userType, int clinicBranch, bool isSucessFull);

        /// <summary>
        /// Author Mansoor
        /// Created Date 17 Jan 2011
        /// This method is used to implement send email functionality
        /// </summary>
        /// <param name="requestedUserId"></param>
        /// <param name="subject"></param>
        /// <param name="receiptEmail"></param>
        /// <param name="message"></param>
        /// /* Coomented By Ismail Moved TO Streaming Service*/
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //string SendEmail(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Quick link List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<QuickLinkDTO> LoadUserDefaultQuickLinksbyLocation(int UserId, int ClinicBranchId);

        #region Search Engine Exponsed Methods

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Default Search Engine List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<SearchEngineDTO> LoadUserSearchEnginebyLocation(int UserId, int ClinicBranchId);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 Jan 2011
        /// This Method Is Used To Load All Search Engine List
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<SearchEngineDTO> LoadAllSearchEngines();

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 Jan 2011
        /// This Method Is Used To set specified Search engine As default
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="UserSearchEngineID"></param>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int SetDefaultSearchEngine(int ProviderUserID, int ClinicBranchID, int UserSearchEngineID);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Jan 17 2011
        /// Method Used to MAnage the Search engine for user against Location
        /// /// Changed 07 Feb 2011
        /// Changed by Mansoor
        /// Params removed [int UserSearchEngineID, bool IsDefault] //M
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="SearchEngineTypeIDs"></param>        
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int ManageUserSearchEngine(int ProviderUserID, int ClinicBranchID, List<Int32> SearchEngineTypeIDs, int CreatedBy);

        /// <summary>
        /// Author Muhammad ismail 
        /// Date 09 Feb 2011
        /// Method Used TO clear Search engines Added by User
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int ClearUserAddedSearchEngines(int ProviderUserID, int ClinicBranchID, int ModifiedBy);
        #endregion

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 jan 2011
        /// Method Used To Load Clinic Users
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ContactSearchDTO> LoadUsersByClinicBranchID(int ClinicBranchId);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 18 jan 2011
        /// Method Used To Register User
        /// </summary>
        /// <param name="registrationDTO"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        UserDTO RegisterNewUser(RegistrationDTO registrationDTO);

        /// <summary>
        /// Author Mansoor
        /// Date 19 Jan 2011
        /// Return all static data required for registeration
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        RegisterationStaticDataDTO GetRegisterationStaticData();

        ///// <summary>
        ///// Author Mansoor
        ///// Date 20 Jan 2011
        ///// </summary>
        ///// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //AddressBookStaticDataResponse GetAddressBookStaticData();

        ///// <summary>
        ///// Author Mansoor
        ///// Date 19 JAn 2011
        ///// </summary>
        ///// <param name="stateCode"></param>
        ///// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //CityResponse GetCityListByState(int stateId);

        /// <summary>
        /// Author Mansoor
        /// Date 20 Jan 2011
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //ZipCodeResponse GetZipCodesByStateIdAndCityName(int stateId, string cityName);

        /// <summary>
        /// Author Mansoor
        /// Date 21 JAn 2011
        /// To fetch the list of 
        /// </summary>
        /// <param name="allergyName"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<AllergySearchDTO> SearchAllergyTypeByName(string allergyName);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Jan 2011
        /// search the matching icd codes and gets the description, on the bases of icd code provided
        /// </summary>
        /// <param name="icdCode"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ICDCodeSearchDTO> SearchICDCodeByCode(string icdCode);


        #region Patient Registeration

        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //ServiceResponse RegisterNewUser(RegistrationDTO registrationDTO);       

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011
        /// Save the patient profile and language w.r.t the userId provided
        /// </summary>
        /// <param name="patientDTO"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int SavePatientBasicProfileAndLangauge(int userId, int managedByUserID, PatientDTO patientDTO, List<PatientLanguageDTO> PatientLanguageDTOList);

        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //ServiceResponse SavePatientEmergencyContact(List<PatientEmergencyContactDTO> patientECDTOList);

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011        
        /// save the patient Gurantor Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientGurantorDTO"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int SavePatientGurantor(int patientID, int managedByUserID, PatientGuarantorDTO patientGurantorDTO);

        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int SavePatientHealthInfo(int patientID, int managedByUserID, PatientHealthDTO patientHealthDTO);

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011        
        /// save the patient Insurance Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientInsuranceDTOList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int SavePatientInsuranceInfo(int patientID, int managedByUserID, List<PatientInsuranceDTO> patientInsuranceDTOList);

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011        
        /// save the user address book, specified in the Party Type
        /// </summary>
        /// <param name="addressBookDTO"></param>
        /// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //ServiceResponse SaveAddressBook(int roleProfileID, int managedByUserID, AddressBookDTO addressBookDTO);

        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //ServiceResponse SavePatientEmergencyContactAndGurantor(List<PatientEmergencyContactDTO> patientECDTOList, PatientGuarantorDTO patientGurantorDTO);

        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //ServiceResponse SavePatientHealthInfoAndInsurance(PatientHealthDTO patientHealthDTO, List<PatientInsuranceDTO> patientInsuranceDTOList);

        #endregion


        //Mansoor test the flex
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //List<string> TestSize(List<string> testList);

        /// <summary>
        /// Author Muhaammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method will  be used as an abstarct interface to save user that might belong 
        /// to any category i.e either patient or clinical User
        /// </summary>
        /// <param name="baseUserDTO"></param>
        /// <returns>UserId</returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int RegisterBaseUser(BaseUserDTO baseUserDTO);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method Used To Insert/Update/Delete The patient emergency contact Information :)
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="EmergencyContactList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int ManagePatientEmergencyContact(int PatientID, int CreatedBy, List<PatientEmergencyContactDTO> EmergencyContactList);

        /// <summary>
        /// Author Mansoor
        /// Date 19 Jan 2011
        /// Return static data required for registeration, based on type provided
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        RegisterationStaticDataDTO GetRegisterationStaticDataByType(List<string> typeList);

        /// <summary>
        /// Author Mansoor
        /// Date 27 Jan 2011
        /// Get portion of patient search data, based on query string and demand page no
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchString"></param>
        /// <param name="demandPageNo"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientSearchResultDTO SearchPatientbyClinicIdAndSearchString(int clinicBranchId, int providerId, int userTypeId, string searchString, int demandPageNo);


        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Save the patient in the most patient list for that user on this clinic
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool SaveRecentPatient(int clinicBranchId, int providerUserId, int patientId);

        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Load the list of most recent loaded patients for the provider for location
        /// </summary>
        /// <param name="providerUserId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<RecentPatientDTO> LoadRecentPatientsByProviderIdAndBranchId(int providerUserId, int clinicBranchId);

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
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientProfileCompleteSettingDTO LoadPatientProfileSettings(int clinicBranchId, int UserId, int PatientId);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 08 Feb 2011
        /// This Method Will be used to load Patient Profile Setting Providing For specific Section
        /// Location-Id & Provider i.e doctor or nurse Id.
        /// Settings are customized by combination of Provider&User Id 
        /// i.e Same user on different location may have different setting Vise Vers :)
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        ///  /// <param name="SectionId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientProfileCompleteSettingDTO LoadPatientProfileSettingsBySection(int clinicBranchId, int UserId, int PatientId, int SectionId);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to Manage Patient Profile Settings 
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int ManagePatientProfileSettings(PatientProfileSettingDTO ProfileSettingsObj);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// Method Used To Load Profile Static Information
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ProfileStaticDataDTO> LoadProfileStaticData();

        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Save the patient allergy info provided
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="patientAllergy"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool SavePatientAllergy(int patientId, int createdBy, PatientAllergyDTO patientAllergy);

        /// <summary>
        /// Author Mansoor
        /// Date 2 Feb 2011
        /// load the patient profile and also save the required info in the provider's most recent patient list.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientProfileDTO LoadPatientCompleteProfileForClinic(int patientId, int clinicBranchId, int providerUserId);

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the user email id by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        String GetUserEmailByUserId(int userId);

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient info to show in the left window panel
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientProfileTaggedDTO LoadPatientProfileInfoForProvider(int patientId, int clinicBranchId, int providerUserId);

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient profile allergies for the provider, to show in patient exam container
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientProfileAllergyDTO LoadPatientAllergiesForProvider(int patientId);

        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile diagnosis for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>    
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientProfileDiagnosisDTO LoadPatientDiagnosisForProvider(int patientId);

        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile immunization for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>    
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientProfileImmuniztionDTO LoadPatientImmunizationForProvider(int patientId);

        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile medication for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>    
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientProfileChronicMedicationDTO LoadPatientMedicationForProvider(int patientId);

        /// <summary>
        /// Author Ismail
        /// Date 4 Feb 2011
        /// load the patient profile Insurance for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>      
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientProfileInsuranceDTO LoadPatientInsuranceForProvider(int patientId);

        /// <summary>
        /// Author Muhammad ismail
        /// Date Feb 11 2011
        /// Method Used To load Image Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ImageTypeDTO> LoadImageTypes();



        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get the RecGuid of respective table, its a internal call used to get guid before saving Address Book
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        Guid GetPartyGuidByProfileIdAndTypeId(int roleProfileID, int addressPartyTypeID);


        /// <summary>
        /// Author Muhammad ISmail
        /// Date 22 Feb 2011
        /// This call will load user basic infor like name email using thier Ids :)
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<UserBasicInfoDTO> GetUserBasicInfoById(List<int> ProviderIdList);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get the module types for notes etc
        /// </summary>              
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ModuleTypeDTO> LoadModuleTypes();

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// tell if a user is enrolled as provider on a specific branch
        /// </summary> 
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool IsProviderAtClinic(int userId, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 28 Feb 2011
        /// get the RecGuid of respective table, used to load the patient profile customized setting (AB)
       /// </summary>
        /// <param name="patientId"></param>
        /// <param name="partyTypeID"></param>
       /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        Guid GetPartyGuidByPatientIdAndPartyTypeId(int patientId, int partyTypeID);

    }
}
