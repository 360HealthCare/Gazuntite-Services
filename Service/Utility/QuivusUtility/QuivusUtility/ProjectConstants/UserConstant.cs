using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quivus.Utility
{
    /// <summary> 
    /// Author Muhammad Ismail
    /// created Date 14 jan 2011
    /// This Constant class Is intended for only user specific contants
    /// </summary>
    public class UserConstant
    {
        public const string USER_DEFAULT_AUTHENTICATE_SP = "User_AuthenticateUser";
        public const string USER_DEFAULT_LOGIN_LOG_SP = "LoginHistory_Insert";
        public const string USER_LOCATION_SEARCHENGINE_SP = "UserSearchEngine_LoadByUserIDClinicBranchID";
        public const string USER_DEFUALT_QUICK_LINK_SP = "UserQuickLink_LoadByUserIDClinicBranchID";
        public const string ALL_SEARCHENGINE_SP = "[type.SearchEngineType_LoadAll]";
        public const string SET_DEFAULT_SEARCHENGINE_SP = "UserSearchEngine_SetDefault";
        public const string CLINIC_USER_LIST_SP = "Users_LoadByClinicBranchID";
        public const string ADD_USER_SEARCHENGINE_SP = "UserSearchEngine_Insert";
        public const string USER_DEFAULT_EMAIL_LOG_SP = "UserEmail_Insert";
        public const string USER_DEFAULT_EMAIL_LOG_WITH_ATTACHMENT_SP = "UserEmail_InsertWithAttachment";
        public const string REGISTER_NEW_USER_SP = "User_Insert";
        public static string REGISTER_BASE_USER_SP = "User_Insert";
        public static string UPDATE_BASE_USER_SP = "User_Update";

        public const string USER_EMAIL_VERIFY_SP = "Users_CheckEmailExists";

        public const string ALLERGY_SEARCH_BY_NAME_SP = "[type.AllergyType_LoadByAllergyTypeNameAutoComplete]";
        public const string ICD_CODE_SEARCH_BY_CODE_SP = "[type.ICDCode_LoadByICDCodeNameAutoComplete]";

        //Types
        public const string TYPE_LOAD_ALL_STATUS_SP = "[type.status_LoadAll]";
        public const string TYPE_LOAD_ALL_USERS_SP = "[type.UserType_LoadAll]";
        public const string TYPE_LOAD_ALL_PREFIX_SP = "[type.PrefixType_LoadAll]";
        public const string TYPE_LOAD_ALL_GENDER_SP = "[type.GenderType_LoadAll]";
        public const string TYPE_LOAD_ALL_MARITAL_STATUS_SP = "[type.MaritalStatusType_LoadAll]";
        public const string TYPE_LOAD_ALL_RACE_SP = "[type.RaceType_LoadAll]";
        public const string TYPE_LOAD_ALL_ETHINICITY_SP = "[type.EthinicityType_LoadAll]";
        public const string TYPE_LOAD_ALL_OCCUPATION_SP = "[type.OccupationType_LoadAll]";
        public const string TYPE_LOAD_ALL_LANGUAGE_SP = "[type.LanguageType_LoadAll]";
        public const string TYPE_LOAD_ALL_LANGUAGE_ABILITY_SP = "[type.LanguageAbilityMode_LoadAll]";
        public const string TYPE_LOAD_ALL_RELATIONSHIP_SP = "[type.RelationShipType_LoadAll]";
        public const string TYPE_LOAD_ALL_SMOKING_SP = "[type.SmokingType_LoadAll]";
        public const string TYPE_LOAD_ALL_INSURANCE_NATURE_SP = "InsuranceNature_LoadAll";
        public const string TYPE_LOAD_ALL_INSURANCE_TYPE_SP = "InsuranceType_LoadAll";
        public const string TYPE_LOAD_ALL_INSURANCE_ORGANIZATION_SP = "InsuranceOrganization_LoadAll";

        //Address Book
        public const string TYPE_LOAD_ALL_ADDRESS_PARTY_SP = "[ab].[AddressPartyType_LoadAll]";
        public const string TYPE_LOAD_ALL_ADDRESS_TYPE_SP = "[ab].[AddressType_LoadAll]";
        public const string TYPE_LOAD_ALL_COUNTRY_SP = "[ab].[Country_LoadAll]";
        public const string TYPE_LOAD_ALL_NUMBER_TYPE_SP = "[ab].[NumberType_LoadAll]";
        public const string TYPE_LOAD_ALL_OWNER_TYPE_SP = "[ab].[OwnerType_LoadAll]";
        public const string TYPE_LOAD_ALL_PHONE_TYPE_SP = "[ab].[PhoneType_LoadAll]";
        public const string TYPE_LOAD_ALL_STATE_SP = "[ab].[State_LoadAll]";
        public const string TYPE_LOAD_ALL_ZIP_SP = "[ab].[ZipCode_LoadAll]";
        public const string TYPE_LOAD_CITY_BY_STATE_ID_SP = "City_LoadByStateID";
        public const string TYPE_LOAD_ZIP_BY_STATE_ID_AND_CITY_SP = "ZIPCode_LoadByCityNameStateID";


        public const string ADDRESS_BOOK_INSERT_SP = "AddressBook_InsertCompletePhoneBook";
        public static string MANAGE_PATIENT_EMERGENCY_CONTACT_SP = "PatientEmergencyContact_Insert";

        //Patient Registeration
        public const string PATIENT_INSERT_SP = "[user.Patient_Insert]";
        public const string PATIENT_LANGUAGE_INSERT_SP = "PatientLanguage_Insert";
        public const string PATIENT_GURANTOR_INSERT_SP = "PatientGuarantor_Insert";
        public const string PATIENT_INSURANCE_INSERT_SP = "PatientInsurance_Insert";

        public const string PATIENT_ALLERGY_INSERT_SP = "PatientHistoryAllergy_Insert";
        public const string PATIENT_MEDICAL_HISTORY_INSERT_SP = "PatientHistoryMedical_Insert";
        public const string PATIENT_FAMILY_HISTORY_INSERT_SP = "PatientHistoryFamily_Insert";

        public const string REC_GUID_PATIENT_GET_BY_PATIENT_ID_SP  = "[user.Patient_GetRecGuidByPatientID]";
        public const string REC_GUID_GURANTOR_GET_BY_GURANTOR_ID_SP = "[PatientGuarantor_GetRecGuidByPatientGuarantorID]";


        public const string REC_GUID_GURANTOR_GET_BY_PATIENT_ID_SP = "PatientGuarantor_RecGuidByPatientID";
        public const string REC_GUID_EMERGENCY_CONTACT_GET_BY_PATIENT_ID_SP = "PatientEmergencyContact_RecGuidByPatientID";

        public const string SEARCH_PATIENT_LOAD_BY_CLINIC_ID_AND_CRITERIA_SP = "[user.Patient_LoadBySearchCriteria]";
        public static string PROFILE_STATIC_DATA_SP="UserProfileCategory_LoadAll";
        public static string INSERT_PROFILE_SETTINGS_SP = "UserUserProfileCategory_Insert";
        public static string UPDATE_PROFILE_SETTINGS_SP = "UserUserProfileCategory_Update";
        public static string LOAD_PROFILE_SETTING_SP = "UserUserProfileCategory_Load";

        public const string USER_RECENT_PATIENT_INSERT_SP = "UserRecentPatient_Insert";
        public const string USER_RECENT_PATIENT_LOAD_SP = "UserRecentPatient_LoadByUserClinicBranch";

        public const string PATIENT_ALLERGY_INSERT_SINGLE_SP = "PatientHistoryAllergy_InsertSingleRecord";

        public const string USER_EMAIL_GET_BY_USER_ID_SP = "User_GetEmailByUserID";

        //Pateint Profile Customization
        public const string PATIENT_INSURANCE_LOAD_FOR_TAG_BY_PATIENT_ID_SP = "InsuranceInfo_LoadForBasicInfoTaggedWindow";
        public const string PATIENT_BASIC_INFO_LOAD_FOR_TAG_BY_PATIENT_ID_SP = "[user.Patient_LoadForBasicInfoTaggedWindow]";
        public const string PATIENT_ALLERGY_INFO_LOAD_FOR_TAG_BY_PATIENT_ID_SP = "PatientAllergy_LoadForHealthTaggedWindow";
        public const string PATIENT_DIAGNOSIS_INFO_LOAD_FOR_TAG_BY_PATIENT_ID_SP = "PatientHistoryMedical_LoadForHealthTaggedWindow";
        public const string PATIENT_INSURANCE_INFO_LOAD_FOR_TAG_BY_PATIENT_ID_SP = "InsuranceInfo_LoadForHealthTaggedWindow";


        public const string ADDRESS_INFO_LOAD_FOR_TAG_BY_GUID_SP = "[ab].[AddressBasicFields_LoadByPartyGuidOwnerType]";
        public const string PHONE_INFO_LOAD_FOR_TAG_BY_GUID_SP = "[ab].[Phone_LoadByPartyGuidRecordOwnerType]";


        public const string PATIENT_IMMUNIZATION_INFO_LOAD_FOR_TAG_BY_PATIENT_ID_SP = "PatientHistoryMedical_LoadForHealthTaggedWindow1"; // Not provided
        public const string PATIENT_MEDICATION_INFO_LOAD_FOR_TAG_BY_PATIENT_ID_SP = "PatientHistoryMedical_LoadForHealthTaggedWindow2"; // Not provided
        public static string LOAD_PROFILE_SETTINGS_COLLECTION_SP = "UserUserProfileCategory_LoadByUserClinicBranch";


        public const string USER_PRIMARY_PHONE_LOAD_BY_GUID_ADDRESS_BOOK_SP = "[ab].[PrimaryPhone_LoadByPartyGuid]";
        public static string PATIENT_INSURANCE_INFO_LOAD_FOR_TAG_SP = "InsuranceInfo_LoadForHealthTaggedWindow";

        public static string CLEAR_USER_SEARCHENGINE_SP = "UserSearchEngine_ClearAllUserAdded";


        //Patient Complete Profile Load
        public const string USER_INFO_LOAD_ALL_BY_PATIENT_ID_SP = "User_LoadByPatientID";
        public const string PATIENT_INFO_LOAD_ALL_BY_PATIENT_ID_SP = "[user.Patient_LoadByPatientID]";
        public const string PATIENT_LANGUAGE_INFO_LOAD_ALL_BY_PATIENT_ID_SP = "PatientLanguage_LoadByPatientID";
        public const string PATIENT_EMERGENCY_CONATCT_LOAD_ALL_BY_PATIENT_ID_SP = "PatientEmergencyContact_LoadByPatientID";
        public const string PATIENT_GURANTOR_INFO_LOAD_ALL_BY_PATIENT_ID_SP = "PatientGuarantor_LoadByPatientID";
        public const string PATIENT_ALLERGY_LOAD_ALL_BY_PATIENT_ID_SP = "PatientHistoryAllergy_LoadByPatientID";
        public const string PATIENT_MEDICAL_HISTORY_LOAD_ALL_BY_PATIENT_ID_SP = "PatientHistoryMedical_LoadByPatientID";
        public const string PATIENT_FAMILY_HISTORY_LOAD_ALL_BY_PATIENT_ID_SP = "PatientHistoryFamily_LoadByPatientID";
        public const string PATIENT_INSURANCE_INFO_LOAD_ALL_BY_PATIENT_ID_SP = "PatientInsurance_LoadByPatientID";

        public static string IMAGE_TYPE_LOAD_SP = "[type.ImageType_LoadAll]";
        public static string USER_IMAGE_INSERT_SP = "UserImage_Insert";

        public const string PATIENT_ADDERSS_BOOK_LOAD_ALL_BY_PARTY_GUID_SP = "[ab].[AddressBook_LoadByPartyGuid]";

        public static string USER_BASIC_INFO_LOAD_SP = "UserBasicInfo_LoadByIDs"; 

        public const string MODULE_TYPE_LOAD_ALL_SP = "[type.ModuleType_LoadAll]";
        public static string USER_IMAGE_LOAD_SP = "UserImage_LoadByUserIDUserTypeID";


        public const string USER_IS_PROVIDER_AT_CLINIC = "User_IsProviderAtClinicBranch";

    }
}