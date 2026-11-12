using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using QuivusManager.ServiceResponse.ServiceCustomResponse;


namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IQuivusManagerService" in both code and config file together.
    [ServiceContract]
    public partial interface IQuivusManagerService
    {
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
        //M [OperationContract]        
        //M QuivusManager.ServiceResponse.ServiceCustomResponse.EmailResponse SendEmail(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message);

        //[OperationContract(Name="SendEmailWithAttachment")]
        //QuivusManager.ServiceResponse.ServiceCustomResponse.EmailResponse SendEmail(EmailDTO data);

    }
}
