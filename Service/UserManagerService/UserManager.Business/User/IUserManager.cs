using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Quivus.Model.User.Patient;
using Quivus.Model.Common;
using Quivus.Model.User;

namespace UserManager.Business.User
{
    public interface IUserManager
    {
        PatientProfileCompleteSettingDTO LoadPatientProfileSettings(int clinicBranchId, int providerId, int PatientId);

        int ManagePatientProfileSettings(PatientProfileSettingDTO ProfileSettingObj);

        List<ProfileStaticDataDTO> LoadProfileStaticData();

        String GetUserEmailByUserId(int userId);

        PatientProfileCompleteSettingDTO LoadPatientProfileSettingsBySection(int clinicBranchId, int UserId, int PatientId, int SectionId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get the RecGuid of respective table, its a internal call used to get guid before saving Address Book
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param> 
        Guid GetPartyGuidByProfileIdAndTypeId(int roleProfileID, int addressPartyTypeID);

        List<UserBasicInfoDTO> GetUserBasicInfoById(List<int> ProviderIdList);

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// tell if a user is enrolled as provider on a specific branch
        /// </summary> 
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        bool IsProviderAtClinic(int userId, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 28 Feb 2011
        /// get the RecGuid of respective table, used to load the patient profile customized setting (AB)
        /// </summary>
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns> 
        Guid GetPartyGuidByPatientIdAndPartyTypeId(int patientIdD, int partyTypeID);
    }
}
