using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using Quivus.Utility.Logger;
using System.ServiceModel;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickLinks;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.IntegratedServies.ReferenceFactory;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using Quivus.Utility.Fault;


namespace QuivusManager.Buisness.UserBL.Impl
{
    public class UserBLImpl : IUserBL
    {

        UserManagerServiceClient UserServiceClient = null;

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
        public LoginResponse AuthenticateUser(string UserId, string UserPassword)
        {
            LoginResponse response = null;
            LoginDTO loginDTO = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching AuthenticateUser Call TO User-Service");

                loginDTO = UserServiceClient.AuthenticateUser(UserId, UserPassword);

                QuivusLogger.Info("Response Recieved From loginDTO::" + loginDTO);

                if (response == null)
                {
                    response = new LoginResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (loginDTO != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = loginDTO;
                }
            }
            catch
            {
                throw;
            }

            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Quick link List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        public QuickLinkResponse LoadUserDefaultQuickLinksbyLocation(int UserId, int ClinicBranchId)
        {
            QuickLinkResponse response = null;
            List<QuickLinkDTO> quickLinkDTO = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadUserDefaultQuickLinksbyLocation Call TO User-Service");

                quickLinkDTO = UserServiceClient.LoadUserDefaultQuickLinksbyLocation(UserId, ClinicBranchId);

                QuivusLogger.Info("Response Recieved From quickLinkDTO::" + quickLinkDTO);

                if (response == null)
                {
                    response = new QuickLinkResponse();
                }
                if (quickLinkDTO != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = quickLinkDTO;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }

            return response;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Default Search Engine List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        public SearchEngineResponse LoadUserSearchEnginebyLocation(int UserId, int ClinicBranchId)
        {
            List<SearchEngineDTO> SearchengineList = null;
            SearchEngineResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadUserSearchEnginebyLocation Call TO User-Service");

                SearchengineList = UserServiceClient.LoadUserSearchEnginebyLocation(UserId, ClinicBranchId);

                QuivusLogger.Info("Response Recieved From SearchengineList::" + SearchengineList);

                if (response == null)
                {
                    response = new SearchEngineResponse();
                }
                if (SearchengineList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = SearchengineList;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 Jan 2011
        /// This Method Is Used To set specified Search engine As default
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="UserSearchEngineID"></param>
        public SearchEngineResponse SetDefaultSearchEngine(int ProviderUserID, int ClinicBranchID, int UserSearchEngineID)
        {
            int Result = 0;
            SearchEngineResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching SetDefaultSearchEngine Call TO User-Service");

                Result = UserServiceClient.SetDefaultSearchEngine(ProviderUserID, ClinicBranchID, UserSearchEngineID);

                QuivusLogger.Info("Response Recieved Result::" + Result);

                if (response == null)
                {
                    response = new SearchEngineResponse();
                }
                if (Result > 0)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Jan 17 2011
        /// Method Used to MAnage the Search engine for user against Location
        /// Changed 07 Feb 2011
        /// Changed by Mansoor
        /// Params removed [int UserSearchEngineID, bool IsDefault] //M
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="SearchEngineTypeIDs"></param>
        /// <param name="UserSearchEngineID"></param>
        /// <param name="IsDefault"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public SearchEngineResponse ManageUserSearchEngine(int ProviderUserID, int ClinicBranchID, List<int> SearchEngineTypeIDs, int CreatedBy)
        {
            int Result = 0;
            SearchEngineResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching ManageUserSearchEngine Call TO User-Service");

                Result = UserServiceClient.ManageUserSearchEngine(ProviderUserID, ClinicBranchID, SearchEngineTypeIDs, CreatedBy);

                QuivusLogger.Info("Response Recieved Result::" + Result);

                if (response == null)
                {
                    response = new SearchEngineResponse();
                }
                if (Result > 0)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
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
        /// Date 09 Feb 2011
        /// Method Used TO clear Search engines Added by User
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public SearchEngineResponse ClearUserAddedSearchEngines(int ProviderUserID, int ClinicBranchID, int ModifiedBy)
        {
            int Result = 0;
            SearchEngineResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                QuivusLogger.Info("Dispatching ClearUserAddedSearchEngines Call TO User-Service");

                Result = UserServiceClient.ClearUserAddedSearchEngines(ProviderUserID, ClinicBranchID, ModifiedBy);

                QuivusLogger.Info("Response Recieved Result::" + Result);

                if (response == null)
                {
                    response = new SearchEngineResponse();
                }
                if (Result > 0)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 jan 2011
        /// Method Used To Load Clinic Users
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>
        public ContactSearchResponse LoadUsersByClinicBranchID(int ClinicBranchID)
        {
            ContactSearchResponse response = null;
            List<ContactSearchDTO> ContactList = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                QuivusLogger.Info("Dispatching LoadUsersByClinicBranchID Call TO User-Service");
                ContactList = UserServiceClient.LoadUsersByClinicBranchID(ClinicBranchID);

                QuivusLogger.Info("Response Recieved ContactList::" + ContactList);

                if (response == null)
                {
                    response = new ContactSearchResponse();
                }
                if (ContactList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = ContactList;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 18 jan 2011
        /// Method Used To Register User/ Update User Information
        /// </summary>
        /// <param name="registrationDTO"></param>
        /// <returns></returns>
        public UserRegistrationResponse RegisterNewUser(RegistrationDTO registrationDTO)
        {
            UserDTO userDTO = null;
            UserRegistrationResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching RegisterNewUser Call TO User-Service");

                userDTO = UserServiceClient.RegisterNewUser(registrationDTO);

                QuivusLogger.Info("Response Recieved userDTO::" + userDTO);

                if (response == null)
                {
                    response = new UserRegistrationResponse();
                }
                if (userDTO != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = userDTO;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhaammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method will  be used as an abstarct interface to save user that might belong 
        /// to any category i.e either patient or clinical User
        /// </summary>
        /// <param name="baseUserDTO"></param>
        /// <returns></returns>
        public BaseUserRegistrationResponse RegisterBaseUser(BaseUserDTO baseUserDTO)
        {
            int Result = 0;
            BaseUserRegistrationResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching RegisterBaseUser Call TO User-Service");

                Result = UserServiceClient.RegisterBaseUser(baseUserDTO);

                QuivusLogger.Info("Response Recieved Result::" + Result);

                if (response == null)
                {
                    response = new BaseUserRegistrationResponse();
                }
                if (Result > 0)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.UserId = Result;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
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
        /// load the user email id by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>    
        public EmailResponse GetUserEmailByUserId(int userId)
        {
            string emailId = null;
            EmailResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching GetUserEmailByUserId Call TO User-Service");

                emailId = UserServiceClient.GetUserEmailByUserId(userId);

                QuivusLogger.Info("Response Recieved emailId::" + emailId);

                if (response == null)
                {
                    response = new EmailResponse();
                }
                if (!string.IsNullOrEmpty(emailId))
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.UserEmail = emailId;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
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
        /// Date 22 Feb 2011
        /// get the RecGuid of respective table, its a internal call used to get guid before saving Address Book
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns>       
        public Guid GetPartyGuidByProfileIdAndTypeId(int roleProfileID, int addressPartyTypeID)
        {
            Guid partyGuid = new Guid();
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching GetPartyGuidByProfileIdAndTypeId Call TO User-Service");

                partyGuid = UserServiceClient.GetPartyGuidByProfileIdAndTypeId(roleProfileID, addressPartyTypeID);

                QuivusLogger.Info("Response Recieved partyGuid::" + partyGuid);

            }
            catch
            {
                throw;
            }
            return partyGuid;
        }

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 22 Feb 2011
        /// This call will load user basic infor like name email using thier Ids :)
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns>
        public List<UserBasicInfoDTO> GetUserBasicInfoById(List<int> ProviderIdList)
        {
            List<UserBasicInfoDTO> response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching GetUserBasicInfoById Call TO User-Service");

                response = UserServiceClient.GetUserBasicInfoById(ProviderIdList);

                QuivusLogger.Info("Response Recieved response::" + response);

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
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching IsProviderAtClinic Call TO User-Service");

                isProvider = UserServiceClient.IsProviderAtClinic(userId, clinicBranchId);

                QuivusLogger.Info("Response Recieved isProvider::" + isProvider);

            }
            catch
            {
                throw;
            }
            return isProvider;
        }
    }
}
