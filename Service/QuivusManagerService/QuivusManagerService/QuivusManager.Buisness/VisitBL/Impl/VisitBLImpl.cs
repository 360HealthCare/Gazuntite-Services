using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using Quivus.Utility;
using Quivus.Utility.Logger;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;

using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.IntegratedServies.ReferenceFactory;
using QuivusManager.Buisness.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;

using QuivusManager.Buisness.UserBL;
using QuivusManager.Buisness.UserBL.Impl;
using QuivusManager.IntegratedServies.UserManagerService;

using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;


namespace QuivusManager.Buisness.VisitBL.Impl
{
    /// <summary>
    /// Author Mansoor
    /// Date 21 Feb 2011    
    /// </summary>

    public class VisitBLImpl : IVisitImpl
    {
        VisitServiceClient clientVisit;
        IUserBL UserBL = null;
        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns> 
        public EncounterTypeResponse LoadEncounterTypes()
        {
            EncounterTypeResponse response = null;
            List<EncounterTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadEncounterTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadEncounterTypes Call TO VisitService");

                list = clientVisit.LoadEncounterTypes();

                QuivusLogger.Info("Response Recieved  list::" + list);

                if (response == null)
                {
                    response = new EncounterTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.EncounterTypeList = list;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new EncounterTypeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>  
        public SoapTemplateResponse LoadSoapTemplateByClinicBranchId(int clinicBranchId)
        {
            SoapTemplateResponse response = null;
            List<SoapTemplateTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadSoapTemplateByClinicBranchId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadSoapTemplateByClinicBranchId Call TO VisitService");

                list = clientVisit.LoadSoapTemplateByClinicBranchId(clinicBranchId);

                QuivusLogger.Info("Response Recieved  list::" + list);

                if (response == null)
                {
                    response = new SoapTemplateResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.SoapTemplateList = list;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SoapTemplateResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit
        /// </summary>
        /// <returns></returns> 
        public CreateVisitResponse CreateVisit(VisitDTO visitDTO, int managedByUserId)
        {
            CreateVisitResponse response = null;
            int visitId = 0;
            bool isProvider = false;
            try
            {
                QuivusLogger.Info("Calling CreateVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                if(UserBL == null)
                {
                    UserBL = new UserBLImpl();
                }

                QuivusLogger.Info("Calling IsProviderAtClinic Of User BL::");
                isProvider = UserBL.IsProviderAtClinic(managedByUserId, visitDTO.ClinicBranch.ClinicBranchIdSeq);
                QuivusLogger.Info("Response Recieved  isProvider::" + isProvider);

                if (isProvider)
                {
                    visitDTO.OwnerId = managedByUserId; // the creator is also a Doctor
                }
                else
                {
                    visitDTO.OwnerId = 0; // not set yet
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching CreateVisit Call TO VisitService");

                visitId = clientVisit.CreateVisit(visitDTO, managedByUserId);

                QuivusLogger.Info("Response Recieved  visitId::" + visitId);

                if (response == null)
                {
                    response = new CreateVisitResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.VisitId = visitId;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new CreateVisitResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// update visit
        /// </summary>
        /// <returns></returns>     
        public QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse SaveVisit(VisitDTO visitDTO, int managedByUserId)
        {
            QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse response = null;
            bool isProvider = false;
            bool result = false;
            try
            {
                QuivusLogger.Info("Calling SaveVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                if (UserBL == null)
                {
                    UserBL = new UserBLImpl();
                }

                QuivusLogger.Info("Calling IsProviderAtClinic Of User BL::");
                isProvider = UserBL.IsProviderAtClinic(managedByUserId, visitDTO.ClinicBranch.ClinicBranchIdSeq);
                QuivusLogger.Info("Response Recieved  isProvider::" + isProvider);

                

                if (visitDTO.OwnerId == 0 && isProvider)
                {
                    visitDTO.OwnerId = managedByUserId; // the updater is a Doctor and not set before 
                }               

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching SaveVisit Call TO VisitService");

                result = clientVisit.SaveVisit(visitDTO, managedByUserId);

                QuivusLogger.Info("Response Recieved  result::" + result);

                if (response == null)
                {
                    response = new QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse();                  
                }

                if (result)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>  
        public QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse SignVisit(VisitDTO visitDTO, int managedByUserId)
        {
            QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse response = null;
            bool result = false;
            try
            {
                QuivusLogger.Info("Calling SignVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching SignVisit Call TO VisitService");

                result = clientVisit.SignVisit(visitDTO, managedByUserId);

                QuivusLogger.Info("Response Recieved  result::" + result);

                if (response == null)
                {
                    response = new CreateVisitResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new CreateVisitResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// load patient encounters for patient background
        /// </summary>
        /// <param name="patinetId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="showAll"></param>
        /// <returns></returns> 
        public PatientEncountersResponse LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll)
        {
            PatientEncountersResponse response = null;
            PatientEncounterResult list = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientVisitsByPatientIdAndClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientVisitsByPatientIdAndClinicId Call TO VisitService");

                list = clientVisit.LoadPatientVisitsByPatientIdAndClinicId(patientId, clinicBranchId, showAll);

                QuivusLogger.Info("Response Recieved  list::" + list);

                if (response == null)
                {
                    response = new PatientEncountersResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.EncounterResult = list;

                UpdateVisitProvider(list); // This Method Will Update Visit Provider Names

            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientEncountersResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date Feb 22 2011
        /// Methos Used To Load Visit Provider Names Using UserID From User Sevice And Set Provider NAme in Encounter List
        /// </summary>
        /// <param name="list"></param>
        private void UpdateVisitProvider(PatientEncounterResult list)
        {
            QuivusLogger.Info("UpdateVisitProvider Called To UpdATE Visit Provider Name in List");

            if (list != null && list.PatientEncounterList != null && list.PatientEncounterList.Count > 0) /* Load Visit Provider Names Using UserID From User Sevice  */
            {
                List<int> UserIdList = null;

                UserIdList = EliminateCahceUserIds(list, UserIdList, QApplicationDataCache.USER_BASIC_INFO_CACHE_DATA); // This Method WIll Skip USer Id That Already Exist in Cache

                if (UserIdList != null && UserIdList.Count > 0)
                {
                    if (UserBL == null)
                    {
                        UserBL = new UserBLImpl();
                    }

                    QuivusLogger.Info("Forward GetUserBasicInfoById Call TO User bL ");

                    List<UserBasicInfoDTO> UserBasicList = UserBL.GetUserBasicInfoById(UserIdList); // Load User Basic Info Using Ids

                    QuivusLogger.Info("Response Recieved UserBasicList::" + UserBasicList);

                    if (UserBasicList != null && UserBasicList.Count > 0)
                    {
                        if (QApplicationDataCache.USER_BASIC_INFO_CACHE_DATA == null)
                        {
                            QApplicationDataCache.USER_BASIC_INFO_CACHE_DATA = UserBasicList;
                        }
                        else
                        {
                            var result = QApplicationDataCache.USER_BASIC_INFO_CACHE_DATA.Concat(UserBasicList);
                            // QApplicationDataCache.USER_BASIC_INFO_CACHE_DATA = result.ToList();
                        }
                    }
                }

                if (QApplicationDataCache.USER_BASIC_INFO_CACHE_DATA != null)
                {
                    foreach (PatientEncounterDTO Obj in list.PatientEncounterList)
                    {
                       // var PatientEncounterQuery =
                       //from d in list.PatientEncounterList
                       //where d.OwnerUserId.Equals(obj.UserId)
                       //select d;
                        var CacheQuery =
                          from d in QApplicationDataCache.USER_BASIC_INFO_CACHE_DATA
                          where d.UserId.Equals(Obj.OwnerUserId)
                          select d;

                        if (CacheQuery != null && CacheQuery.ToArray().Length > 0)
                        {
                            UserBasicInfoDTO resultObj = CacheQuery.First();
                            Obj.VisitProvider = resultObj.FirstName + " " + resultObj.LastName;
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Date Feb 22 2011
        /// This Method WIll Skip USer Id That Already Exist in Cache
        /// </summary>
        /// <param name="list"></param>
        /// <param name="UserIdList"></param>
        /// <param name="CacheUserList"></param>
        /// <returns></returns>
        private static List<int> EliminateCahceUserIds(PatientEncounterResult list, List<int> UserIdList, List<UserBasicInfoDTO> CacheUserList)
        {
            QuivusLogger.Info("EliminateCahceUserIds Called To Eliminate User In Cache");

            if (list != null && list.PatientEncounterList != null && list.PatientEncounterList.Count > 0)
            {
                foreach (PatientEncounterDTO Obj in list.PatientEncounterList)
                {
                    if (CacheUserList != null)
                    {
                        var CacheQuery =
                           from d in CacheUserList
                           where d.UserId.Equals(Obj.OwnerUserId)
                           select d;

                        if (CacheQuery != null && CacheQuery.ToArray().Length > 0)
                        {
                            // Record Already Exist in Cache SKIP
                        }
                        else
                        {
                            if (UserIdList == null)
                            {
                                UserIdList = new List<int>();
                            }
                            UserIdList.Add(Obj.OwnerUserId);
                        }
                    }
                    else
                    {
                        if (UserIdList == null)
                        {
                            UserIdList = new List<int>();
                        }
                        UserIdList.Add(Obj.OwnerUserId);
                    }
                }
            }
            return UserIdList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns> 
        public ImmunizationSaveResponse ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int managedByUserId)
        {
            ImmunizationSaveResponse response = null;
            bool result = true;
            try
            {
                QuivusLogger.Info("Calling ManagePatientImmunization");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientImmunization Call TO VisitService");

                result = clientVisit.ManagePatientImmunization(immunizationList, managedByUserId);

                QuivusLogger.Info("Response Recieved  result::" + result);

                if (response == null)
                {
                    response = new ImmunizationSaveResponse();                  
                }

                if (result)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationSaveResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization types static data
        /// </summary>        
        /// <returns></returns>
        public ImmunizationTypesResponse LoadImmunizationTypes()
        {
            ImmunizationTypesResponse response = null;
            List<ImmunizationTypeDTO> ImmunzationTypeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationTypes Call TO VisitService");

                ImmunzationTypeList = clientVisit.LoadImmunizationTypes();

                QuivusLogger.Info("Response Recieved  ImmunzationTypeList::" + ImmunzationTypeList);

                if (response == null)
                {
                    response = new ImmunizationTypesResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.ImmunzationTypeList = ImmunzationTypeList;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationTypesResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        public ImmunizationAutoCompleteResponse LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            List<string> LotList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationLotNumberByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationLotNumberByClinicId Call TO VisitService");

                LotList = clientVisit.LoadImmunizationLotNumberByClinicId(searchCriteria, clinicBranchId);

                QuivusLogger.Info("Response Recieved  LotList::" + LotList);

                if (response == null)
                {
                    response = new ImmunizationAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.AutoCompleteList = LotList;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization dose
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        public ImmunizationAutoCompleteResponse LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            List<string> LotList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationDoseByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationDoseByClinicId Call TO VisitService");

                LotList = clientVisit.LoadImmunizationDoseByClinicId(searchCriteria, clinicBranchId);

                QuivusLogger.Info("Response Recieved LoadImmunizationDoseByClinicId LotList::" + LotList);

                if (response == null)
                {
                    response = new ImmunizationAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.AutoCompleteList = LotList;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Reason
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns> 
        public ImmunizationAutoCompleteResponse LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            List<string> LotList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationRefusalReasonByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationRefusalReasonByClinicId Call TO VisitService");

                LotList = clientVisit.LoadImmunizationRefusalReasonByClinicId(searchCriteria, clinicBranchId);

                QuivusLogger.Info("Response Recieved LoadImmunizationRefusalReasonByClinicId LotList::" + LotList);

                if (response == null)
                {
                    response = new ImmunizationAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.AutoCompleteList = LotList;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Site
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        public ImmunizationAutoCompleteResponse LoadImmunizationSiteByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            List<string> LotList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationSiteByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationSiteByClinicId Call TO VisitService");

                LotList = clientVisit.LoadImmunizationSiteByClinicId(searchCriteria, clinicBranchId);

                QuivusLogger.Info("Response Recieved LoadImmunizationSiteByClinicId LotList::" + LotList);

                if (response == null)
                {
                    response = new ImmunizationAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.AutoCompleteList = LotList;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Series
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>  
        public ImmunizationAutoCompleteResponse LoadImmunizationSeriesByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            List<string> LotList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationSeriesByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationSeriesByClinicId Call TO VisitService");


                LotList = clientVisit.LoadImmunizationSeriesByClinicId(searchCriteria, clinicBranchId);

                QuivusLogger.Info("Response Recieved LoadImmunizationSeriesByClinicId LotList::" + LotList);

                if (response == null)
                {
                    response = new ImmunizationAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.AutoCompleteList = LotList;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        public ImmunizationAutoCompleteResponse LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            List<string> LotList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationManufacturerByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationManufacturerByClinicId Call TO VisitService");
                
                LotList = clientVisit.LoadImmunizationManufacturerByClinicId(searchCriteria, clinicBranchId);

                QuivusLogger.Info("Response Recieved LoadImmunizationManufacturerByClinicId LotList::" + LotList);

                if (response == null)
                {
                    response = new ImmunizationAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.AutoCompleteList = LotList;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization rout
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        public ImmunizationAutoCompleteResponse LoadImmunizationRoutByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            List<string> LotList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationRoutByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationRoutByClinicId Call TO VisitService");

                LotList = clientVisit.LoadImmunizationRoutByClinicId(searchCriteria, clinicBranchId);

                QuivusLogger.Info("Response Recieved LoadImmunizationRoutByClinicId LotList::" + LotList);

                if (response == null)
                {
                    response = new ImmunizationAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.AutoCompleteList = LotList;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit        
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>    
        public PatientImmunizationResponse LoadImmunizationsByVisitId(int visitId, int providerUserId, int userTypeId)
        {
            PatientImmunizationResponse response = null;
            List<ImmunizationDTO> PatientImmunizationList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationsByVisitId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationsByVisitId Call TO VisitService");

                PatientImmunizationList = clientVisit.LoadImmunizationsByVisitId(visitId);

                QuivusLogger.Info("Response Recieved  PatientImmunizationList::" + PatientImmunizationList);

                if (response == null)
                {
                    response = new PatientImmunizationResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.PatientImmunizationData = PatientImmunizationList;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientImmunizationResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>    
        public VisitNotesResponse LoadModuleNotesByVisitId(int visitId)
        {
            VisitNotesResponse response = null;
            List<ModuleNoteDTO> ModuleNoteList = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleNotesByVisitId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadModuleNotesByVisitId Call TO VisitService");

                ModuleNoteList = clientVisit.LoadModuleNotesByVisitId(visitId);

                QuivusLogger.Info("Response Recieved  ModuleNoteList::" + ModuleNoteList);

                if (response == null)
                {
                    response = new VisitNotesResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.ModuleNoteData = ModuleNoteList;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new VisitNotesResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// manage module notes, insert/ update/ delete
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>      
        public QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse ManageModuleNotes(List<ModuleNoteDTO> moduleNotes)
        {
            QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse response = null;
            bool result = false;
            try
            {
                QuivusLogger.Info("Calling ManageModuleNotes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManageModuleNotes Call TO VisitService");

                result = clientVisit.ManageModuleNotes(moduleNotes);

                QuivusLogger.Info("Response Recieved  result::" + result);

                if (response == null)
                {
                    response = new PatientImmunizationResponse();
                }

                if (result)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }                
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientImmunizationResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// get immunization list of patient        
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        public PatientImmunizationResponse LoadPatientImmunizationsByPatientId(int patientId, int providerUserId, int userTypeId)
        {
            PatientImmunizationResponse response = null;
            List<ImmunizationDTO> PatientImmunizationList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientImmunizationsByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientImmunizationsByPatientId Call TO VisitService");

                PatientImmunizationList = clientVisit.LoadPatientImmunizationsByPatientId(patientId, providerUserId, userTypeId);

                QuivusLogger.Info("Response Recieved  PatientImmunizationList::" + PatientImmunizationList);

                if (response == null)
                {
                    response = new PatientImmunizationResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.PatientImmunizationData = PatientImmunizationList;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientImmunizationResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }
    }
}
