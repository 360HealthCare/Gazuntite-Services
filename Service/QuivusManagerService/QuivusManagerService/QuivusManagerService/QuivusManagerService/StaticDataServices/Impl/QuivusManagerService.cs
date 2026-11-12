using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AddressBook;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.ICDCode;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Image;
using QuivusManager.Buisness.StaticDataBL;
using QuivusManager.Buisness.StaticDataBL.Impl;
using System.ServiceModel;
using QuivusManager.Buisness.Utility;
using Quivus.Utility.Logger;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial class QuivusManagerService
    {
        IStaticDataBL StaticDataBL = null;
        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 Jan 2011
        /// This Method Is Used To Load All Search Engine List
        /// </summary>
        public SearchEngineResponse LoadAllSearchEngines()
        {
            SearchEngineResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadAllSearchEngines Called");

                if (StaticDataBL == null)
                {
                    StaticDataBL = new StaticDataBLImpl();
                }
                response = StaticDataBL.LoadAllSearchEngines();
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SearchEngineResponse();
                ResponseErrorHandler.HandleException(response, exp);
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetRegisterationStaticData Called");

                if (StaticDataBL == null)
                {
                    StaticDataBL = new StaticDataBLImpl();
                }
                response = StaticDataBL.GetRegisterationStaticData();
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new RegistrationStaticDataResponse();
                ResponseErrorHandler.HandleException(response, exp);
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetAddressBookStaticData Called");

                if (StaticDataBL == null)
                {
                    StaticDataBL = new StaticDataBLImpl();
                }
                response = StaticDataBL.GetAddressBookStaticData();
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new AddressBookStaticDataResponse();
                ResponseErrorHandler.HandleException(response, exp);
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetCityListByState Called");

                QuivusLogger.Info("GetCityListByState PARAM stateId::" + stateId);

                if (StaticDataBL == null)
                {
                    StaticDataBL = new StaticDataBLImpl();
                }
                response = StaticDataBL.GetCityListByState(stateId);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new CityResponse();
                ResponseErrorHandler.HandleException(response, exp);
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetZipCodesByStateIdAndCityName Called");
                QuivusLogger.Info("GetZipCodesByStateIdAndCityName PARAM stateId::" + stateId + "cityName::" + cityName);

                if (StaticDataBL == null)
                {
                    StaticDataBL = new StaticDataBLImpl();
                }
                response = StaticDataBL.GetZipCodesByStateIdAndCityName(stateId, cityName);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ZipCodeResponse();
                ResponseErrorHandler.HandleException(response, exp);
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService SearchAllergyTypeByName Called");
                QuivusLogger.Info("SearchAllergyTypeByName PARAM allergyName::" + allergyName);

                if (StaticDataBL == null)
                {
                    StaticDataBL = new StaticDataBLImpl();
                }
                response = StaticDataBL.SearchAllergyTypeByName(allergyName);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new AllergyTypeResponse();
                ResponseErrorHandler.HandleException(response, exp);
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService SearchICDCodeByCode Called");
                QuivusLogger.Info("SearchICDCodeByCode PARAM icdCode::" + icdCode);

                if (StaticDataBL == null)
                {
                    StaticDataBL = new StaticDataBLImpl();
                }
                response = StaticDataBL.SearchICDCodeByCode(icdCode);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ICDCodeResponse();
                ResponseErrorHandler.HandleException(response, exp);
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetRegisterationStaticDataByType Called");
                QuivusLogger.Info("GetRegisterationStaticDataByType PARAM typeList::" + typeList);

                if (typeList != null)
                    QuivusLogger.Info("List Count PARAM typeList::" + typeList.Count);
                    

                if (StaticDataBL == null)
                {
                    StaticDataBL = new StaticDataBLImpl();
                }
                response = StaticDataBL.GetRegisterationStaticDataByType(typeList);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new RegistrationStaticDataResponse();
                ResponseErrorHandler.HandleException(response, exp);
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadProfileStaticData Called");
               // QuivusLogger.Info("GetRegisterationStaticDataByType PARAM typeList::" + typeList);

                if (StaticDataBL == null)
                {
                    StaticDataBL = new StaticDataBLImpl();
                }
                response = StaticDataBL.LoadProfileStaticData();
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ProfileStaticDataResponse();
                ResponseErrorHandler.HandleException(response, exp);
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImageTypes Called");
                // QuivusLogger.Info("GetRegisterationStaticDataByType PARAM typeList::" + typeList);


                if (StaticDataBL == null)
                {
                    StaticDataBL = new StaticDataBLImpl();
                }
                response = StaticDataBL.LoadImageTypes();
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ImageTypeResponse();
                ResponseErrorHandler.HandleException(response, exp);
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadAllModuleTypes Called");
                // QuivusLogger.Info("GetRegisterationStaticDataByType PARAM typeList::" + typeList);


                if (StaticDataBL == null)
                {
                    StaticDataBL = new StaticDataBLImpl();
                }
                response = StaticDataBL.LoadAllModuleTypes();
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ModuleTypeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }
    }
}