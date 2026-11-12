using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.IntegratedServies.ReferenceFactory;
using System.ServiceModel;
using Quivus.Utility.Logger;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using Quivus.Utility;
using QuivusManager.Buisness.Utility;



namespace QuivusManager.Buisness.ManagerBL.Impl
{
    public class ManagerBLImpl : IManagerBL
    {
        UserManagerServiceClient UserServiceClient = null;

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load the user login history
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <param name="clinicBranch"></param>
        /// <param name="isSucessFull"></param>
        public void LogLoginHistory(int userId, int userType, int clinicBranch, bool isSucessFull)
        {
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));

                }

                QuivusLogger.Error("QuivusManagerService ManagerBLImpl :: Calling LogLoginHistory On User-Service");

                UserServiceClient.LogLoginHistory(userId, userType, clinicBranch, isSucessFull);
            }
            catch
            {
                throw;
            }
        }

       /* Commented By Ismail Moved TO Streaming Service
        * public EmailResponse SendEmail(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message)
        {
            EmailResponse response = null;
            String Result = String.Empty;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                if (response == null)
                {
                    response = new EmailResponse();
                }
                Result = UserServiceClient.SendEmail(requestedUserId, clinicBranchID, subject, receiptEmail, message);

                if (!string.IsNullOrEmpty(Result))
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
        */



    }
}
