using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.Buisness.ManagerBL;
using QuivusManager.Buisness.ManagerBL.Impl;
using QuivusManager.IntegratedServies.UserManagerService;
using Quivus.Utility.Logger;
using QuivusManager.Buisness.Utility;
using QuivusManager.IntegratedServies.UserManagerService;



namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "QuivusManagerService" in code, svc and config file together.
    public partial class QuivusManagerService :QuivusBaseService, IQuivusManagerService
    {

        IManagerBL ManagerBL = null;

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
                QuivusLogger.Error("QuivusManagerService Calling LogLoginHistory");

                if (ManagerBL == null)
                {
                    ManagerBL = new ManagerBLImpl();
                }
                ManagerBL.LogLoginHistory(userId, userType, clinicBranch, isSucessFull);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
            }
        }

        /// <summary>
        /// Author Mansoor
        /// Created Date 17 Jan 2011
        /// This method is used to implement send email functionality
        /// </summary>
        /// <param name="requestedUserId"></param>
        /// <param name="subject"></param>
        /// <param name="receiptEmail"></param>
        /// <param name="message"></param>
        /// // Commented By Ismail
        //public QuivusManager.ServiceResponse.ServiceCustomResponse.EmailResponse SendEmail(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message)
        //{
        //    QuivusManager.ServiceResponse.ServiceCustomResponse.EmailResponse response = null;
        //    String Result = String.Empty;
        //    try
        //    {
        //        if (ManagerBL == null)
        //        {
        //            ManagerBL = new ManagerBLImpl();
        //        }
        //       response = ManagerBL.SendEmail(requestedUserId, clinicBranchID, subject, receiptEmail, message);
        //    }
        //    catch (FaultException<QuivusFault> exp)
        //    {
        //        QuivusLogger.Error(exp);

        //        if (response == null)
        //            response = new QuivusManager.ServiceResponse.ServiceCustomResponse.EmailResponse();
        //        ResponseErrorHandler.HandleException(response, exp);
        //    }
        //    return response;
        //}

        /// <summary>
        /// Author Ali Noor
        /// Created Date 1st Feb 2011
        /// This method is used to implement send email functionality including attachement
        /// </summary>
        /// <param name=""></param>
        //public QuivusManager.ServiceResponse.ServiceCustomResponse.EmailResponse SendEmail(EmailDTO data)
        //{
        //    QuivusManager.ServiceResponse.ServiceCustomResponse.EmailResponse response = null;
        // //   IEmailManager emailMangerImpl = null;
        //    try
        //    {
              
        //    }
        //    catch (Exception exp)
        //    {
               
        //    }
        //    return response;
        //}

        public override string GetServiceVersion()
        {
            return "Development";
        }

       
    }
}
