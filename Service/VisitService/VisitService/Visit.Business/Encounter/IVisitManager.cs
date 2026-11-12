using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Model.QuickInput;
using Quivus.Model.Visit;
using Quivus.Model.PatientBackground;
using Quivus.Model.QuickInput.Notes;

namespace Visit.Business.Encounter
{
    /// <summary>
    /// Author Mansoor
    /// Date 17 Feb 2011   
    /// </summary>
    public interface IVisitManager
    {
        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011   
        /// Get soap templates bases on clinic branch id
        /// </summary>
        List<SoapTemplateTypeDTO> LoadSoapTemplateByClinicBranchId(int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit
        /// </summary>
        /// <returns></returns>      
        int CreateVisit(VisitDTO visitDTO, int managedByUserId);


        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// update visit
        /// </summary>
        /// <returns></returns>      
        bool SaveVisit(VisitDTO visitDTO, int managedByUserId);

        /// <summary>        
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed               
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>    
        bool SignVisit(VisitDTO visitDTO, int managedByUserId);


        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <param name="patinetId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="showAll"></param>
        /// <returns></returns>      
        PatientEncounterResult LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>     
        bool ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int managedByUserId);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        List<string> LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId);


        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization dose
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        List<string> LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Reason
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>     
        List<string> LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId);


        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Site
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns> 
        List<string> LoadImmunizationSiteByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Series
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns> 
        List<string> LoadImmunizationSeriesByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        List<ImmunizationDTO> LoadImmunizationsByVisitId(int visitId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        List<ModuleNoteDTO> LoadModuleNotesByVisitId(int visitId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        bool ManageModuleNotes(List<ModuleNoteDTO> moduleNotes);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        List<string> LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization rout
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        List<string> LoadImmunizationRoutByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// get immunization list of patient        
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>     
        List<ImmunizationDTO> LoadPatientImmunizationsByPatientId(int patientId, int providerUserId, int userTypeId);        
    }
}
