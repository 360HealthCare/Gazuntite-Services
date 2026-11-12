using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;

namespace QuivusManager.Buisness.VisitBL
{
    public interface IVisitImpl
    {
        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>   
        EncounterTypeResponse LoadEncounterTypes();

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>     
        SoapTemplateResponse LoadSoapTemplateByClinicBranchId(int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit
        /// </summary>
        /// <returns></returns>     
        CreateVisitResponse CreateVisit(VisitDTO visitDTO, int managedByUserId);

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// update visit
        /// </summary>
        /// <returns></returns>     
        QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse SaveVisit(VisitDTO visitDTO, int managedByUserId);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>     
        QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse SignVisit(VisitDTO visitDTO, int managedByUserId);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// load patient encounters for patient background
        /// </summary>
        /// <param name="patinetId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="showAll"></param>
        /// <returns></returns>     
        PatientEncountersResponse LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>     
        ImmunizationSaveResponse ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int managedByUserId);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization types static data
        /// </summary>        
        /// <returns></returns>     
        ImmunizationTypesResponse LoadImmunizationTypes();

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        ImmunizationAutoCompleteResponse LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization dose
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        ImmunizationAutoCompleteResponse LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Reason
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>    
        ImmunizationAutoCompleteResponse LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Site
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>      
        ImmunizationAutoCompleteResponse LoadImmunizationSiteByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Series
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>     
        ImmunizationAutoCompleteResponse LoadImmunizationSeriesByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        ImmunizationAutoCompleteResponse LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization rout
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        ImmunizationAutoCompleteResponse LoadImmunizationRoutByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit        
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        PatientImmunizationResponse LoadImmunizationsByVisitId(int visitId, int providerUserId, int userTypeId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>    
        VisitNotesResponse LoadModuleNotesByVisitId(int visitId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// manage module notes, insert/ update/ delete
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>      
        QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse ManageModuleNotes(List<ModuleNoteDTO> moduleNotes);

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// get immunization list of patient        
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        PatientImmunizationResponse LoadPatientImmunizationsByPatientId(int patientId, int providerUserId, int userTypeId);
    }
}
