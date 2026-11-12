using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Quivus.Utility.Logger;
using Quivus.Utility;
using Quivus.Utility.CustomException;
 
using UserManagerService.DataAccess;
using UserManagerService.DataAccess.Patient;
using Quivus.Utility.Factory;
using UserManager.Business.Validators;

using Quivus.Model.RecentPatient;
using Quivus.Model.Search;
using Quivus.Model.PatientProfileCustomization;
using Quivus.Model.AddressBook;
using UserManager.Business.Utility;
using Quivus.Model.PatientProfileCustomization;

namespace UserManager.Business.User.Patient.Impl
{
    public class PatientManagerImpl : IPatientManager
    {
        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// </summary>       
        PatientDAO patientDAO = null;
        //IAddressBookService addressBookServiceManager = null;

        public bool SaveRecentPatient(int clinicBranchId, int providerUserId, int patientId)
        {
            bool result;

            try
            {
                QuivusLogger.Info("Calling SaveRecentPatient PARAM:: clinicBranchId" + clinicBranchId + " providerUserId" + providerUserId + " patientId" + patientId);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }
                if (providerUserId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PROFILE_ID_INVALID_ERROR_MSG);
                }
                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                result = patientDAO.SaveRecentPatient(clinicBranchId, providerUserId, patientId);

            }
            catch 
            {
                throw;
            }
            return result;
        }

        public List<RecentPatientDTO> LoadRecentPatientsByProviderIdAndBranchId(int providerUserId, int clinicBranchId)
        {
            List<RecentPatientDTO> result;

            try
            {
                QuivusLogger.Info("Calling SaveRecentPatient PARAM:: clinicBranchId" + clinicBranchId + " providerUserId" + providerUserId);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }
                if (providerUserId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PROFILE_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                result = patientDAO.LoadRecentPatientsByProviderIdAndBranchId(providerUserId, clinicBranchId);
            }
            catch 
            {
                throw;
            }
            return result;
        }

        public PatientSearchResultDTO LoadPatientbyClinicIdAndSearchString(int clinicBranchId, int providerId, int userTypeId, string searchString, int demandPageNo)
        {
            PatientSearchResultDTO searchResult = null;

            bool isDateSearch = false;
            DateTime DOB = new DateTime();

            try
            {
                QuivusLogger.Info("Calling LoadPatientbyClinicIdAndSearchString PARAM:: clinicId " + clinicBranchId + " providerId " + providerId + " userTypeId " + userTypeId + " searchString " + searchString + " demandPageNo " + demandPageNo);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }
                if (providerId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PROFILE_ID_INVALID_ERROR_MSG);
                }
                if (userTypeId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_TYPE_INVALID_ERROR_MSG);
                }
                if (demandPageNo < 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.DEMAND_PAGE_NO_INVALID_ERROR_MSG);
                }
                if (string.IsNullOrEmpty(searchString))
                {
                    throw new InvalidArgumentException(ProjectConstant.SEARCH_STRING_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }            

                isDateSearch = Validations.IsValidSearchDate(searchString, out DOB);

                int pageSize = ProjectConstant.PATIENT_SEARCH_PAGE_SIZE;

                searchResult = patientDAO.LaodPatientbyClinicIdAndSearchString(clinicBranchId, providerId, userTypeId, searchString, demandPageNo, pageSize, isDateSearch, DOB);
            }
            catch
            {
                throw;
            }
            return searchResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient info to show in the left window panel, also log in the most recent patients
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns> 
        public PatientProfileTaggedDTO LoadPatientProfileInfoForProvider(int patientId, int clinicBranchId, int providerUserId)
        {
            PatientProfileTaggedDTO response = null;

            PatientProfileBasicInfoDTO patientProfileBasicInfoObj = null;
            PatientInsuracneInfoDTO patientInsuracneInfoObj = null;
            PhoneDTO phoneDTOobj = null;

            try
            {
                QuivusLogger.Info("LoadPatientProfileInfoForProvider Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                patientProfileBasicInfoObj = patientDAO.LoadPatientProfileBasicInfo(patientId);
                patientInsuracneInfoObj = patientDAO.LoadPatientInsuranceInfo(patientId);

                //if (addressBookServiceManager == null)
                //{
                //    addressBookServiceManager = new AddressBookServiceImpl();
                //}

                //if (patientProfileBasicInfoObj != null)
                //{
                //    phoneDTOobj = addressBookServiceManager.GetPartyPrimaryPhoneInfoByGuid(patientProfileBasicInfoObj.RecGuid);
                //}

                patientDAO.SaveRecentPatient(clinicBranchId,providerUserId, patientId);

                if (patientProfileBasicInfoObj != null)
                {
                    if (response == null)
                    {
                        response = new PatientProfileTaggedDTO();
                    }
                    response.PatientId = patientId;
                    response.PatientProfileBasicInfoData = patientProfileBasicInfoObj;
                    response.PatientInsuracneInfoData = patientInsuracneInfoObj;
                  //  response.PrimaryPhoneNo = phoneDTOobj;
                }
            }
            catch 
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient info to show in the left window panel, also log in the most recent patients
        /// <param name="patientId"></param>
        /// <returns></returns>
        public PatientProfileAllergyDTO LoadPatientAllergiesForProvider(int patientId)
        {
            PatientProfileAllergyDTO response = null;
            List<PatientAllergyInfoDTO> PatientAllergyInfoList = null;

            try
            {
                QuivusLogger.Info("LoadPatientAllergiesForProvider Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                PatientAllergyInfoList = patientDAO.LoadPatientAllergiesByPatientId(patientId);

                if (PatientAllergyInfoList != null)
                {
                    if (response == null)
                    {
                        response = new PatientProfileAllergyDTO();
                    }
                    response.PatientId = patientId;
                    response.PatientAllergyInfoData = PatientAllergyInfoList;                    
                }
            }
            catch 
            {
                throw;
            }
            return response;
        }
        
        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile diagnosis for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>            
        public PatientProfileDiagnosisDTO LoadPatientDiagnosisForProvider(int patientId)
        {
            PatientProfileDiagnosisDTO response = null;
            List<PatientDiagnosisInfoDTO> PatientDiagnosisInfoList = null;

            try
            {
                QuivusLogger.Info("LoadPatientDiagnosisForProvider Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                PatientDiagnosisInfoList = patientDAO.LoadPatientDiagnosisByPatientId(patientId);

                if (PatientDiagnosisInfoList != null)
                {
                    if (response == null)
                    {
                        response = new PatientProfileDiagnosisDTO();
                    }
                    response.PatientId = patientId;
                    response.PatientDiagnosisInfoData = PatientDiagnosisInfoList;
                }
            }
            catch 
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile immunization for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>            
        public PatientProfileImmuniztionDTO LoadPatientImmunizationForProvider(int patientId)
        {
            PatientProfileImmuniztionDTO response = null;
            List<PatientImmunizationInfoDTO> PatientImmunizationInfoList = null;

            try
            {
                QuivusLogger.Info("LoadPatientDiagnosisForProvider Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                PatientImmunizationInfoList = patientDAO.LoadPatientImmunizationByPatientId(patientId);

                if (PatientImmunizationInfoList != null)
                {
                    if (response == null)
                    {
                        response = new PatientProfileImmuniztionDTO();
                    }
                    response.PatientId = patientId;
                    response.PatientImmunizationInfoData = PatientImmunizationInfoList;
                }
            }
            catch 
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile medication for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>           
        public PatientProfileChronicMedicationDTO LoadPatientMedicationForProvider(int patientId)
        {
            PatientProfileChronicMedicationDTO response = null;
            List<PatientChronicMedicationInfoDTO> PatientChronicMedicationInfoList = null;
            try
            {
                QuivusLogger.Info("LoadPatientMedicationForProvider Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                PatientChronicMedicationInfoList = patientDAO.LoadPatientMedicationByPatientId(patientId);

                if (PatientChronicMedicationInfoList != null)
                {
                    if (response == null)
                    {
                        response = new PatientProfileChronicMedicationDTO();
                    }
                    response.PatientId = patientId;
                    response.PatientChronicMedicationInfoData = PatientChronicMedicationInfoList;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad ismail
        /// Date 4 feb 2011
        /// Method Used to load insurance Info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public PatientProfileInsuranceDTO LoadPatientInsuranceForProvider(int patientId) 
        {
            PatientProfileInsuranceDTO response = null;
            List<PatientInsuracneInfoDTO> PatientInsuracneInfoList = null;
            try
            {
                QuivusLogger.Info("LoadPatientInsuranceForProvider Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                PatientInsuracneInfoList = patientDAO.LoadPatientInsuranceForProvider(patientId);

                if (PatientInsuracneInfoList != null)
                {
                    if (response == null)
                    {
                        response = new PatientProfileInsuranceDTO();
                       
                    }
                    response.PatientId = patientId;
                    response.PatientInsuranceInfoData = PatientInsuracneInfoList;
                }
            }
            catch 
            {
                throw;
               
            }
            return response;
        }
    }

}
