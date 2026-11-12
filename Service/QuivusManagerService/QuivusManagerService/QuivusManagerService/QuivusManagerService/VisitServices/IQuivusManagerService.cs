using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;

namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial interface IQuivusManagerService
    {
        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        EncounterTypeResponse LoadEncounterTypes();

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the template types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        SoapTemplateResponse LoadSoapTemplateByClinicBranchId(int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        CreateVisitResponse CreateVisit(VisitDTO visitDTO, int managedByUserId);


        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// update visit
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ServiceResponse SaveVisit(VisitDTO visitDTO, int managedByUserId, int userTypeId);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ServiceResponse SignVisit(VisitDTO visitDTO, int managedByUserId);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// load patient encounters for patient background
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        PatientEncountersResponse LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>     
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ImmunizationSaveResponse ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int managedByUserId, int userTypeId);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization types static data
        /// </summary>        
        /// <returns></returns>     
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ImmunizationTypesResponse LoadImmunizationTypes();

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ImmunizationAutoCompleteResponse LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization dose
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ImmunizationAutoCompleteResponse LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Reason
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>    
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ImmunizationAutoCompleteResponse LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Site
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>      
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ImmunizationAutoCompleteResponse LoadImmunizationSiteByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Series
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>             
        [OperationContract]
        //[FaultContract(typeof(QuivusFault)
        ImmunizationAutoCompleteResponse LoadImmunizationSeriesByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit        
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        [OperationContract]
        PatientImmunizationResponse LoadImmunizationsByVisitId(int visitId, int providerUserId, int userTypeId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>
        [OperationContract]
        VisitNotesResponse LoadModuleNotesByVisitId(int visitId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// manage module notes, insert/ update/ delete
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse ManageModuleNotes(List<ModuleNoteDTO> moduleNotes);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [OperationContract]
        ImmunizationAutoCompleteResponse LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization rout
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [OperationContract]
        ImmunizationAutoCompleteResponse LoadImmunizationRoutByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// get immunization list of patient        
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        [OperationContract]
        PatientImmunizationResponse LoadPatientImmunizationsByPatientId(int patientId, int providerUserId, int userTypeId);

    }
}