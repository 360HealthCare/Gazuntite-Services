using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AddressBook;
using QuivusManager.ServiceResponse.ServiceCustomResponse.ICDCode;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;

namespace QuivusManager.Buisness.StaticDataBL
{
    public interface IStaticDataBL
    {
        SearchEngineResponse LoadAllSearchEngines();


        RegistrationStaticDataResponse GetRegisterationStaticData();

        AllergyTypeResponse SearchAllergyTypeByName(string allergyName);

       CityResponse GetCityListByState(int stateId);

       AddressBookStaticDataResponse GetAddressBookStaticData();

       ZipCodeResponse GetZipCodesByStateIdAndCityName(int stateId, string cityName);

       ICDCodeResponse SearchICDCodeByCode(string icdCode);

       RegistrationStaticDataResponse GetRegisterationStaticDataByType(List<string> typeList);

       ProfileStaticDataResponse LoadProfileStaticData();

       ServiceResponse.ServiceCustomResponse.Image.ImageTypeResponse LoadImageTypes();

       /// <summary>
       /// Author Mansoor
       /// Date 22 Feb 2011
       /// get the module types for notes etc
       /// </summary> 
       ModuleTypeResponse LoadAllModuleTypes();
    }
}
