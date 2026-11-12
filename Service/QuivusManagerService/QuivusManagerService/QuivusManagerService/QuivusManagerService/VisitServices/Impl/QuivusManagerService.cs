using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

using Quivus.Utility.Logger;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse;

using QuivusManager.Buisness.VisitBL;
using QuivusManager.Buisness.VisitBL.Impl;
using QuivusManager.Buisness.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;
using Quivus.Utility.Fault;


namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial class QuivusManagerService
    {
        IVisitImpl visitImpl = null;

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>        
        public EncounterTypeResponse LoadEncounterTypes()
        {
            EncounterTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadEncounterTypes Called");
           //     QuivusLogger.Info("LoadUserDefaultQuickLinksbyLocation PARAM UserId::" + UserId + "ClinicBranchId::" + ClinicBranchId);
              

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.LoadEncounterTypes();
            }
            catch (FaultException<QuivusFault> exp)
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
        /// List all the template types
        /// </summary>
        /// <returns></returns>      
        public SoapTemplateResponse LoadSoapTemplateByClinicBranchId(int clinicBranchId)
        {
            SoapTemplateResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadSoapTemplateByClinicBranchId Called");
                QuivusLogger.Info("LoadSoapTemplateByClinicBranchId PARAM ClinicBranchId::" + clinicBranchId);
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.LoadSoapTemplateByClinicBranchId(clinicBranchId);
            }
            catch (FaultException<QuivusFault> exp)
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService CreateVisit Called");
                QuivusLogger.Info("CreateVisit PARAM visitDTO::" + visitDTO + "managedByUserId::" + managedByUserId);
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.CreateVisit(visitDTO, managedByUserId);
            }
            catch (FaultException<QuivusFault> exp)
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
        public ServiceResponse SaveVisit(VisitDTO visitDTO, int managedByUserId, int userTypeId)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SaveVisit Called");
                QuivusLogger.Info("SaveVisit PARAM visitDTO::" + visitDTO + "managedByUserId::" + managedByUserId + "userTypeId::" + userTypeId);
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.SaveVisit(visitDTO, managedByUserId);
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

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>      
        public ServiceResponse SignVisit(VisitDTO visitDTO, int managedByUserId)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SignVisit Called");
                QuivusLogger.Info("SignVisit PARAM visitDTO::" + visitDTO + "managedByUserId::" + managedByUserId);
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.SignVisit(visitDTO, managedByUserId);
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


        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// load patient encounters for patient background
        /// </summary>
        /// <returns></returns>        
        public PatientEncountersResponse LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll)
        {
            PatientEncountersResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientVisitsByPatientIdAndClinicId Called");
                QuivusLogger.Info("LoadPatientVisitsByPatientIdAndClinicId PARAM patientId::" + patientId + "clinicBranchId::" + clinicBranchId + "showAll::" + showAll);
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.LoadPatientVisitsByPatientIdAndClinicId(patientId, clinicBranchId, showAll);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientEncountersResponse();

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
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationTypes Called");
             //   QuivusLogger.Info("LoadPatientVisitsByPatientIdAndClinicId PARAM patientId::" + patientId + "clinicBranchId::" + clinicBranchId + "showAll::" + showAll);
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.LoadImmunizationTypes();
            }
            catch (FaultException<QuivusFault> exp)
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
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>     
        public ImmunizationSaveResponse ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int managedByUserId, int userType)
        {
            ImmunizationSaveResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientImmunization Called");
                QuivusLogger.Info("ManagePatientImmunization PARAM immunizationList::" + immunizationList + "managedByUserId::" + managedByUserId + "userType::" + userType);
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.ManagePatientImmunization(immunizationList, managedByUserId);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        public ImmunizationAutoCompleteResponse LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationLotNumberByClinicId Called");
                QuivusLogger.Info("LoadImmunizationLotNumberByClinicId PARAM searchCriteria::" + searchCriteria + "clinicBranchId::" + clinicBranchId );
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.LoadImmunizationLotNumberByClinicId(searchCriteria, clinicBranchId);
            }
            catch (FaultException<QuivusFault> exp)
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationDoseByClinicId Called");
                QuivusLogger.Info("LoadImmunizationDoseByClinicId PARAM searchCriteria::" + searchCriteria + "clinicBranchId::" + clinicBranchId);
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.LoadImmunizationLotNumberByClinicId(searchCriteria, clinicBranchId);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// get immunization Reason
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        public ImmunizationAutoCompleteResponse LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationRefusalReasonByClinicId Called");
                QuivusLogger.Info("LoadImmunizationRefusalReasonByClinicId PARAM searchCriteria::" + searchCriteria + "clinicBranchId::" + clinicBranchId);
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.LoadImmunizationRefusalReasonByClinicId(searchCriteria, clinicBranchId);
            }
            catch (FaultException<QuivusFault> exp)
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationSiteByClinicId Called");
                QuivusLogger.Info("LoadImmunizationSiteByClinicId PARAM searchCriteria::" + searchCriteria + "clinicBranchId::" + clinicBranchId);
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.LoadImmunizationSiteByClinicId(searchCriteria, clinicBranchId);
            }
            catch (FaultException<QuivusFault> exp)
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationSeriesByClinicId Called");
                QuivusLogger.Info("LoadImmunizationSeriesByClinicId PARAM searchCriteria::" + searchCriteria + "clinicBranchId::" + clinicBranchId);
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.LoadImmunizationSiteByClinicId(searchCriteria, clinicBranchId);
            }
            catch (FaultException<QuivusFault> exp)
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationsByVisitId Called");
                QuivusLogger.Info("LoadImmunizationsByVisitId PARAM visitId::" + visitId + "providerUserId::" + providerUserId + "userTypeId::" + userTypeId);
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.LoadImmunizationsByVisitId(visitId, providerUserId, userTypeId);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// get immunization manufacturer
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        public ImmunizationAutoCompleteResponse LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationManufacturerByClinicId Called");
                QuivusLogger.Info("LoadImmunizationManufacturerByClinicId PARAM searchCriteria::" + searchCriteria + "clinicBranchId::" + clinicBranchId );
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.LoadImmunizationManufacturerByClinicId(searchCriteria, clinicBranchId);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// Date 22 Feb 2011
        /// get immunization rout
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        public ImmunizationAutoCompleteResponse LoadImmunizationRoutByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationRoutByClinicId Called");
                QuivusLogger.Info("LoadImmunizationRoutByClinicId PARAM searchCriteria::" + searchCriteria + "clinicBranchId::" + clinicBranchId);
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.LoadImmunizationRoutByClinicId(searchCriteria, clinicBranchId);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>        
        public VisitNotesResponse LoadModuleNotesByVisitId(int visitId)
        {
            VisitNotesResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadModuleNotesByVisitId Called");
                QuivusLogger.Info("LoadModuleNotesByVisitId PARAM visitId::" + visitId );
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.LoadModuleNotesByVisitId(visitId);
            }
            catch (FaultException<QuivusFault> exp)
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
        public ServiceResponse ManageModuleNotes(List<ModuleNoteDTO> moduleNotes)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManageModuleNotes Called");
                QuivusLogger.Info("ManageModuleNotes PARAM moduleNotes::" + moduleNotes);
             

                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.ManageModuleNotes(moduleNotes);
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
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientImmunizationsByPatientId Called");
                QuivusLogger.Info("LoadPatientImmunizationsByPatientId PARAM patientId::" + patientId + "providerUserId" + providerUserId + "userTypeId::" + userTypeId);
             
                if (visitImpl == null)
                {
                    visitImpl = new VisitBLImpl();
                }
                response = visitImpl.LoadPatientImmunizationsByPatientId(patientId, providerUserId, userTypeId);
            }
            catch (FaultException<QuivusFault> exp)
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