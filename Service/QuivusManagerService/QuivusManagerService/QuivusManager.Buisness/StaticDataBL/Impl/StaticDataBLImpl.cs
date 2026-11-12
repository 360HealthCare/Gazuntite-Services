using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickLinks;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.IntegratedServies.ReferenceFactory;
using Quivus.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AddressBook;
using QuivusManager.IntegratedServies.AddressBookService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.ICDCode;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Image;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;
using Quivus.Utility.Logger;

namespace QuivusManager.Buisness.StaticDataBL.Impl
{
    public class StaticDataBLImpl : IStaticDataBL
    {
        UserManagerServiceClient UserServiceClient = null;
        AddressBookServiceClient AddressBookClient = null;

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 Jan 2011
        /// This Method Is Used To Load All Search Engine List
        /// </summary>
        public SearchEngineResponse LoadAllSearchEngines()
        {
            SearchEngineResponse response = null;
            List<SearchEngineDTO> searchEngineList = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadAllSearchEngines Call TO User Service");

                searchEngineList = UserServiceClient.LoadAllSearchEngines();

                QuivusLogger.Info("Response Recieved searchEngineList::" + searchEngineList);

                if (response == null)
                {
                    response = new SearchEngineResponse();
                }
                if (searchEngineList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = searchEngineList;
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
        /// Date 19 Jan 2011
        /// Return all static data required for registeration
        /// </summary>
        /// <returns></returns>
        public RegistrationStaticDataResponse GetRegisterationStaticData()
        {
            RegistrationStaticDataResponse response = null;
            RegisterationStaticDataDTO DataDTO = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                QuivusLogger.Info("Dispatching GetRegisterationStaticData Call TO User Service");

                DataDTO = UserServiceClient.GetRegisterationStaticData();

                QuivusLogger.Info("Response Recieved DataDTO::" + DataDTO);

                if (response == null)
                {
                    response = new RegistrationStaticDataResponse();
                }
                if (DataDTO != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = DataDTO;
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
        /// Date 21 JAn 2011
        /// To fetch the list of 
        /// </summary>
        /// <param name="allergyName"></param>
        /// <returns></returns>
        public AllergyTypeResponse SearchAllergyTypeByName(string allergyName)
        {
            AllergyTypeResponse response = null;
            List<AllergySearchDTO> AllergyList = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching GetRegisterationStaticData Call TO User Service");

                AllergyList = UserServiceClient.SearchAllergyTypeByName(allergyName);

                QuivusLogger.Info("Response Recieved AllergyList::" + AllergyList);

                if (response == null)
                {
                    response = new AllergyTypeResponse();
                }
                if (AllergyList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = AllergyList;
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
        /// Date 19 JAn 2011
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        public CityResponse GetCityListByState(int stateId)
        {
            CityResponse response = null;
            List<CityDTO> CityList = null;

            try
            {
                if (AddressBookClient == null)
                {
                    AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                }

                QuivusLogger.Info("Dispatching GetRegisterationStaticData Call TO AddressBook Service");

                CityList = AddressBookClient.ListCities(stateId);

                QuivusLogger.Info("Response Recieved CityList::" + CityList);

                if (response == null)
                {
                    response = new CityResponse();
                }
                if (CityList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = CityList;
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
        /// Date 20 Jan 2011
        /// </summary>
        /// <returns></returns>
        public AddressBookStaticDataResponse GetAddressBookStaticData()
        {
            AddressBookStaticDataResponse response = null;
            AddressBookStaticDataDTO AddressDTO = null;

            try
            {
                if (AddressBookClient == null)
                {
                    AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                }

                QuivusLogger.Info("Dispatching GetAddressBookStaticData Call TO AddressBook Service");

                AddressDTO = AddressBookClient.GetAddressBookStaticData();

                QuivusLogger.Info("Response Recieved AddressDTO::" + AddressDTO);

                if (response == null)
                {
                    response = new AddressBookStaticDataResponse();
                }
                if (AddressDTO != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = AddressDTO;
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
        /// Date 20 Jan 2011
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public ZipCodeResponse GetZipCodesByStateIdAndCityName(int stateId, string cityName)
        {
            ZipCodeResponse response = null;
            List<ZipCodeDTO> ZipList = null;

            try
            {
                if (AddressBookClient == null)
                {
                    AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                }

                QuivusLogger.Info("Dispatching GetAddressBookStaticData Call TO AddressBook Service");

                ZipList = AddressBookClient.ListZipCodesByStateIdAndCityName(stateId, cityName);

                QuivusLogger.Info("Response Recieved ZipList::" + ZipList);

                if (response == null)
                {
                    response = new ZipCodeResponse();
                }
                if (ZipList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = ZipList;
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
        /// Date 21 Jan 2011
        /// search the matching icd codes and gets the description, on the bases of icd code provided
        /// </summary>
        /// <param name="icdCode"></param>
        /// <returns></returns>
        public ICDCodeResponse SearchICDCodeByCode(string icdCode)
        {
            ICDCodeResponse response = null;
            List<ICDCodeSearchDTO> IcdCodeList = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching SearchICDCodeByCode Call TO User Service");

                IcdCodeList = UserServiceClient.SearchICDCodeByCode(icdCode);

                QuivusLogger.Info("Response Recieved IcdCodeList::" + IcdCodeList);

                if (response == null)
                {
                    response = new ICDCodeResponse();
                }
                if (IcdCodeList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = IcdCodeList;
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
        /// Date 19 Jan 2011
        /// Return static data required for registeration, based on type provided
        /// </summary>
        /// <returns></returns>
        public RegistrationStaticDataResponse GetRegisterationStaticDataByType(List<string> typeList)
        {
            RegistrationStaticDataResponse response = null;
            RegisterationStaticDataDTO RegistrationDTO = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching GetRegisterationStaticDataByType Call TO User Service");

                RegistrationDTO = UserServiceClient.GetRegisterationStaticDataByType(typeList);

                QuivusLogger.Info("Response Recieved RegistrationDTO::" + RegistrationDTO);

                if (response == null)
                {
                    response = new RegistrationStaticDataResponse();
                }
                if (RegistrationDTO != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = RegistrationDTO;
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
        /// Created Date 01 Feb 2011
        /// Method Used To Load Profile Static Information
        /// </summary>
        /// <returns></returns>
        public ProfileStaticDataResponse LoadProfileStaticData()
        {
            ProfileStaticDataResponse response = null;
            List<ProfileStaticDataDTO> ProfileStaticList = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadProfileStaticData Call TO User Service");

                ProfileStaticList = UserServiceClient.LoadProfileStaticData();

                QuivusLogger.Info("Response Recieved ProfileStaticList::" + ProfileStaticList);

                if (response == null)
                {
                    response = new ProfileStaticDataResponse();
                }
                if (ProfileStaticList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = ProfileStaticList;
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
        /// Date Feb 11 2011
        /// Method Used To load Image Types
        /// </summary>
        /// <returns></returns>
        public ImageTypeResponse LoadImageTypes()
        {
            ImageTypeResponse response = null;
            List<ImageTypeDTO> ImageTypes = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadImageTypes Call TO User Service");
                ImageTypes = UserServiceClient.LoadImageTypes();

                QuivusLogger.Info("Response Recieved ImageTypes::" + ImageTypes);

                if (response == null)
                {
                    response = new ImageTypeResponse();
                }
                if (ImageTypes != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = ImageTypes;
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
        /// get the module types for notes etc
        /// </summary> 
        public ModuleTypeResponse LoadAllModuleTypes()
        {
            ModuleTypeResponse response = null;
            List<ModuleTypeDTO> ModuleTypes = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadModuleTypes Call TO User Service");

                ModuleTypes = UserServiceClient.LoadModuleTypes();

                QuivusLogger.Info("Response Recieved ModuleTypes::" + ModuleTypes);

                if (response == null)
                {
                    response = new ModuleTypeResponse();
                }
                if (ModuleTypes != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ModuleTypeData = ModuleTypes;
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
    }
}
