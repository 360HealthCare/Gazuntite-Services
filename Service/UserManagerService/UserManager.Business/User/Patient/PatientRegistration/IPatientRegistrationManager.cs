using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

using Quivus.Model.User.Patient;

 

namespace UserManager.Business.User.Patient.PatientRegistration
{
    public interface IPatientRegistrationManager
    {
        int SavePatientBasicProfileAndLangauge(int userId, int managedByUserID, PatientDTO patientDTO, List<PatientLanguageDTO> PatientLanguageDTOList);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method Used To Insert/Update/Delete The patient emergency contact Information :)
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="EmergencyContactList"></param>
        /// <returns></returns>
        int ManagePatientEmergencyContact(List<PatientEmergencyContactDTO> EmergencyContactList, int PatientId, int CreatedBy);

        int ManagePatientGurantor(int patientID, int managedByUserID, PatientGuarantorDTO patientGurantorDTO);

        int ManagePatientInsuranceInfo(int patientID, int managedByUserID, List<PatientInsuranceDTO> patientInsuranceDTOList);

        int ManagePatientHealthInfo(int patientID, int managedByUserID, PatientHealthDTO patientHealthDTO);

        PatientProfileDTO LoadPatientCompleteProfilebyPatientId(int patientId, int clinicBranchId, int providerUserId);

    }
}
