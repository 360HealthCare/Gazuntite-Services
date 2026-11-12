using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quivus.Utility
{
    /// <summary>
    /// Author Muhammad Ismail
    /// created Date 14 jan 2011
    ///  This Constant class Is intended for project general contants
    /// </summary>
    public class ProjectConstant
    {
        public const int THUMBNAIL_WIDTH = 18;
        public const int THUMBNAIL_HEIGHT = 19;

        public const string ALL_REGISTERATION_STATIC_DATA_ALL = "All";
        public const string STATUS_TYPE_REGISTERATION_STATIC_DATA = "StatusType";
        public const string USER_TYPE_REGISTERATION_STATIC_DATA = "UserType";
        public const string PREFIX_TYPE_REGISTERATION_STATIC_DATA = "PrefixType";
        public const string GENDER_TYPE_REGISTERATION_STATIC_DATA = "GenderType";
        public const string MARITAL_STATUS_TYPE_REGISTERATION_STATIC_DATA = "MaritalStatusType";
        public const string RACE_TYPE_REGISTERATION_STATIC_DATA = "RaceType";
        public const string ETHINICITY_TYPE_REGISTERATION_STATIC_DATA = "EthinicityType";
        public const string OCCUPATION_TYPE_REGISTERATION_STATIC_DATA = "OccupationType";
        public const string LANGUAGE_TYPE_REGISTERATION_STATIC_DATA = "LanguageType";
        public const string LANGUAGE_ABILITY_MODE_TYPE_REGISTERATION_STATIC_DATA = "LanguageAbilityModeType";
        public const string RELATIONSHIP_TYPE_REGISTERATION_STATIC_DATA = "RelationShipType";
        public const string SMOKING_TYPE_REGISTERATION_STATIC_DATA = "SmokingType";
        public const string INSURANCE_TYPE_REGISTERATION_STATIC_DATA = "InsuranceType";
        public const string INSURANCE_NATURE_TYPE_REGISTERATION_STATIC_DATA = "InsuranceNature";
        public const string INSURANCE_ORGANIZATION_TYPE_REGISTERATION_STATIC_DATA = "InsuranceOrganization";

        public const string PREFERENCE_DB = "PreferencesDB";
        public const string COMMUNICATION_DB = "CommunicationDB";
        public const string ADDRESS_BOOK_DB = "AddressBookDB";
        public const string INSURANCE_DB = "InsuranceBookDB";

        public const string PHONE_TVP = "PhoneTVP";
        public const string ADDRESS_TVP = "AddressTVP";
        public const string PATIENT_LANGUAGE_TVP = "PatientLanguageTVP";
        public const string PATIENT_INSURANCE_TVP = "PatientInsuranceTVP";
        public const string MODULE_NOTE_TVP = "ModuleNoteTVP";
        public const string IMMUNIZATION_TVP = "PatientImmunizationTVP";

        public const string PATIENT_ALLERGY_TVP = "PatientHistoryAllergyTVP";
        public const string PATIENT_MEDICAL_HISTORY_TVP = "PatientHistoryMedicalTVP";
        public const string PATIENT_FAMILY_HISTORY_TVP = "PatientHistoryFamilyTVP";

        public const bool RESPONSE_SUCCESS_STATUS = true;
        public const bool RESPONSE_FAILURE_STATUS = false;

        public const string STRING_NULL = "NULL";
        public const string STRING_TRUE = "True";
        public const string STRING_FALSE = "False";

        // front end can not send null value in date so this value is added to compare it before insert inot DB
        public static DateTime DATE_MINIMUM = new DateTime(1901, 01, 01, 12, 0, 0); //= DateTime.MinValue;

        #region Error Codes

        public enum ERROR_CODE
        {
            InvalidArguments,
            InvalidCredentails,
            AccountLocked,
            GenericDTOConversion,
            UserDTOConversion,
            ServiceUnavaliableError,
            AddressBookTableConversion,
            UserManagerTableConversion,
            EmergencyTableConversion,
            RecordNotInserted,
            AddressBookDTOConversion,
            VisitDTOConversion,
            ModuleNoteTableConversion
        }

        #endregion

        public enum ADDRESS_PARTY_TYPE
        {
            Patient = 1,
            EmergencyContact = 9,
            PatientGuarantor = 10
        }

        public enum ADDRESS_OWNER_TYPE
        {
            Self = 1,
            Mother = 2,
            Father = 3
        }

        public enum USER_PROFILE_CATEGORY_ID
        {
            Address = 11,
            State = 12,
            City = 13,
            Zip = 14,

            Phone = 6,
            MotherPhoneExt = 19,
            FatherPhoneExt = 21,
            EmergencyContactPhoneExt = 26,
            GurantorPhoneExt = 29

        }

        public const string HASH_ALGO_SHA1 = "SHA1";

        public const int PATIENT_SEARCH_PAGE_SIZE = 100;

        #region Error Messages

        public const string INVALID_LOGIN_ERROR_MSG = "Invalid user name or password.";
        public const string ACCOUNT_lOCKED_ERROR_MSG = "Account Locked.";
        public const string INVALID_ARGUMENT_EXCEPTION_MSG = "Invalid Arguments.";

        public const string GENERIC_EXCEPTION_MSG = "Service error. Please contact your administrator.";

        public const string GENERIC_DTO_CONVERSION_ERROR_MSG = "GenericDataReaderToDTOConverter";
        public const string USER_MANAGER_DTO_CONVERSION_ERROR_MSG = "DataReaderToDTOConverter";
        public const string ADDRESS_BOOK_DTO_CONVERSION_ERROR_MSG = "DataReaderToDTOConverter";
        public const string ADDRESS_BOOK_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "AddressBookTableConversionException";
        public const string MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "ModuleNoteTableConversionException";

        public const string VISIT_DTO_CONVERSION_ERROR_MSG = "DataReaderToDTOConverter";

        public const string EMERGENCY_CONTACT_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "Emergency Contact TableConversionException";

        public const string USER_MANAGER_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "UserManagerTableConversionException";
        public const string SERVICE_UNAVALIABLE_ERROR_MSG = "Service Unavailable";

        public const string STATE_ID_INVALID_ERROR_MSG = "Invalid State ID.";
        public const string CITY_NAME_INVALID_ERROR_MSG = "Invalid City Name.";
        public const string ZIP_CODE_INVALID_ERROR_MSG = "Invalid Zip Code.";
        public const string USER_ID_INVALID_ERROR_MSG = "Invalid User ID.";
        public const string PATIENT_ID_INVALID_ERROR_MSG = "Invalid Patient ID.";

        public const string PROFILE_ID_INVALID_ERROR_MSG = "Invalid Profile ID.";

        public const string PASSWORD_INVALID_ERROR_MSG = "Invalid User Password.";
        public const string USER_TYPE_INVALID_ERROR_MSG = "Invalid User Type ID.";
        public const string CLINIC_BRANCH_ID_INVALID_ERROR_MSG = "Invalid Clinic Branch ID.";
        public const string EMAIL_ID_INVALID_ERROR_MSG = "Invalid Email ID.";
        public const string SEARCH_ENGINE_ID_INVALID_ERROR_MSG = "Invalid Search Engine ID.";
        public const string ALLERGY_INVALID_ERROR_MSG = "Allergy is Empty.";
        public const string ALLERGY_NAME_INVALID_ERROR_MSG = "Invalid Allergy Name.";
        public const string ICDCODE_CODE_INVALID_ERROR_MSG = "Invalid ICD Code.";

        public const string DEMAND_PAGE_NO_INVALID_ERROR_MSG = "Invalid Demand Page No.";
        public const string SEARCH_STRING_INVALID_ERROR_MSG = "Invalid Search Criteria.";

        public static string EMERGENCY_CONTACT_TVP = "PatientEmergencyContactTVP";

        public const string RECORD_NOT_INSERTED_ERROR_MSG = "Record Not Inserted.";

        public const string PARTY_GUID_EMPTY_ERROR_MSG = "Invalid Guid.";

        public const string VISTI_ID_INVALID_ERROR_MSG = "Invalid Visit ID.";

        #endregion



        public static string INSERTION_FAILED_EXCEPTION = "Insertion Failed";
        public static string PROFILE_SETTING_ERROR_MSG = "Error Converting patient profile information";
        public static int SECTION_FIRST = 1;
        public static int SECTION_SECOND = 2;

        public static int SECTION_THIRD = 3;
        public static string ALLERGY_COLLECTION = "Allergies";
        public static string DIAGNOSIS_COLLECTION = "Diagnosis";
        public static string CHRONICMEDICATION_COLLECTION = "ChronicMedication";
        public static string IMMUNIZATION_COLLECTION = "Immunization";
        public static string INSURANCE_COLLECTION = "Insurance";
        public static string SECTION_ID_INVALID_ERROR_MSG = "Invalid Section Id";
        public static string SEARCH_ENGINE_TYPE_TVP = "Rec_Ids";
        public static string DEFUALT_PASSWORD = "quivus";
        public static string GURANTOR_LIST_EMPTY = "Guarantor list is empty";
        public static string EMPTY_LIST = "List is empty";
        public static string USER_ID_DTO_CONVERSION_ERROR_MSG = "Error Converting user id information ";





    }
}