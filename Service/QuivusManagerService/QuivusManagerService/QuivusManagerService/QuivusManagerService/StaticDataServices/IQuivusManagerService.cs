using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AddressBook;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Image;
using QuivusManager.ServiceResponse.ServiceCustomResponse.ICDCode;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;


namespace QuivusManagerService.QuivusManagerService.ManagerService
{

    public partial interface IQuivusManagerService
    {
        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 Jan 2011
        /// This Method Is Used To Load All Search Engine List
        /// </summary>
        [OperationContract]
         
        SearchEngineResponse LoadAllSearchEngines();

        /// <summary>
        /// Author Mansoor
        /// Date 19 Jan 2011
        /// Return all static data required for registeration
        /// </summary>
        /// <returns></returns>
        [OperationContract]
         
        RegistrationStaticDataResponse GetRegisterationStaticData();

        /// <summary>
        /// Author Mansoor
        /// Date 20 Jan 2011
        /// </summary>
        /// <returns></returns>
        [OperationContract]
         
        AddressBookStaticDataResponse GetAddressBookStaticData();

        /// <summary>
        /// Author Mansoor
        /// Date 19 JAn 2011
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        [OperationContract]
         
        CityResponse GetCityListByState(int stateId);

        /// <summary>
        /// Author Mansoor
        /// Date 20 Jan 2011
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        [OperationContract]
         
        ZipCodeResponse GetZipCodesByStateIdAndCityName(int stateId, string cityName);

        /// <summary>
        /// Author Mansoor
        /// Date 21 JAn 2011
        /// To fetch the list of 
        /// </summary>
        /// <param name="allergyName"></param>
        /// <returns></returns>
        [OperationContract]
         
        AllergyTypeResponse SearchAllergyTypeByName(string allergyName);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Jan 2011
        /// search the matching icd codes and gets the description, on the bases of icd code provided
        /// </summary>
        /// <param name="icdCode"></param>
        /// <returns></returns>
        [OperationContract]
         
        ICDCodeResponse SearchICDCodeByCode(string icdCode);

        /// <summary>
        /// Author Mansoor
        /// Date 19 Jan 2011
        /// Return static data required for registeration, based on type provided
        /// </summary>
        /// <returns></returns>
        [OperationContract]
         
        RegistrationStaticDataResponse GetRegisterationStaticDataByType(List<string> typeList);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// Method Used To Load Profile Static Information
        /// </summary>
        /// <returns></returns>
        [OperationContract]
         
        ProfileStaticDataResponse LoadProfileStaticData();

        /// <summary>
        /// Author Muhammad ismail
        /// Date Feb 11 2011
        /// Method Used To load Image Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
         
        ImageTypeResponse LoadImageTypes();


        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get the module types for notes etc
        /// </summary>
        [OperationContract]
        ModuleTypeResponse LoadAllModuleTypes();
    }
}
