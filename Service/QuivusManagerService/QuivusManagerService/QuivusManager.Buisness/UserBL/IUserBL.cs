using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickLinks;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;

namespace QuivusManager.Buisness.UserBL
{
    public interface IUserBL
    {
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
        
        LoginResponse AuthenticateUser(string UserId, string UserPassword);

        QuickLinkResponse LoadUserDefaultQuickLinksbyLocation(int UserId, int ClinicBranchId);

        SearchEngineResponse LoadUserSearchEnginebyLocation(int UserId, int ClinicBranchId);

        SearchEngineResponse SetDefaultSearchEngine(int ProviderUserID, int ClinicBranchID, int UserSearchEngineID);

        SearchEngineResponse ManageUserSearchEngine(int ProviderUserID, int ClinicBranchID, List<int> SearchEngineTypeIDs, int CreatedBy);

        SearchEngineResponse ClearUserAddedSearchEngines(int ProviderUserID, int ClinicBranchID, int ModifiedBy);

        ContactSearchResponse LoadUsersByClinicBranchID(int ClinicBranchID);

        UserRegistrationResponse RegisterNewUser(RegistrationDTO registrationDTO);

        BaseUserRegistrationResponse RegisterBaseUser(BaseUserDTO baseUserDTO);

        EmailResponse GetUserEmailByUserId(int userId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get the RecGuid of respective table, its a internal call used to get guid before saving Address Book
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns>       
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
    }
}
