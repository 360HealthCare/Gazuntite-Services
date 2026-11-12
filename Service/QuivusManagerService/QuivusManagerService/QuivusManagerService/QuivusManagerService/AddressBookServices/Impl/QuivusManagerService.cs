using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using QuivusManager.Buisness.AddressBookBL;
using QuivusManager.Buisness.AddressBookBL.Impl;
using System.ServiceModel;
using Quivus.Utility.Logger;

using QuivusManager.IntegratedServies.AddressBookService;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using Quivus.Utility;
using QuivusManager.Buisness.Utility;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AdderssBook;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial class QuivusManagerService
    {
        IAddressBookBL AddressBookBL = null;
        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011        
        /// save the user address book, specified in the Party Type
        /// </summary>
        /// <param name="addressBookDTO"></param>
        /// <returns></returns>
        public ServiceResponse SaveAddressBook(int roleProfileID, int managedByUserID, QuivusManager.IntegratedServies.AddressBookService.AddressBookDTO addressBookDTO)
        {
            int UpdateResult = 0;
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService SaveAddressBook Called");
                QuivusLogger.Error("SaveAddressBook Param roleProfileID::" + roleProfileID + " managedByUserID::" + managedByUserID + " addressBookDTO" + addressBookDTO);

                if (AddressBookBL == null)
                {
                    AddressBookBL = new AddressBookBLImpl();
                    UpdateResult = AddressBookBL.SaveUserAddressBook(roleProfileID, managedByUserID, addressBookDTO);

                    if (UpdateResult > 0)
                    {
                        if (response == null)
                        {
                            response = new ServiceResponse();
                        }
                        response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    }
                    else
                    {
                        response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                    }
                }
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ServiceResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response; 
        }


        public AddressBookPhoneResponse GetPartyPrimaryPhoneNoByRegGuid(Guid guid)
        {
            AddressBookPhoneResponse response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService GetPartyPrimaryPhoneNoByRegGuid Called");
                QuivusLogger.Error("GetPartyPrimaryPhoneNoByRegGuid Param guid::" + guid);

                if (AddressBookBL == null)
                {
                    AddressBookBL = new AddressBookBLImpl();
                }
                response = AddressBookBL.GetPartyPrimaryPhoneNoByRegGuid(guid);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new AddressBookPhoneResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response; 
        }



        public AddressBookAddressResponse LoadAddressBookByPartyRegGuid(Guid guid)
        {
            AddressBookAddressResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadAddressBookByPartyRegGuid Called");
                QuivusLogger.Info("LoadAddressBookByPartyRegGuid Param guid::" + guid);

                if (AddressBookBL == null)
                {
                    AddressBookBL = new AddressBookBLImpl();
                }
                response = AddressBookBL.LoadAddressBookByPartyRegGuid(guid);
            }

         catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new AddressBookAddressResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response; 
        }

    }
}