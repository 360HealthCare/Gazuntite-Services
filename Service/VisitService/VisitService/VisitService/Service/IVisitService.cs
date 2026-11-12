using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using Quivus.Model.TypeDTOs;
using Quivus.Model.QuickInput;
using Quivus.Model.Visit;
using Quivus.Model.PatientBackground;
using Quivus.Model.QuickInput.Notes;
using Quivus.Utility.Fault;

namespace VisitService.Service
{
    [ServiceContract]
    public interface IVisitService
    {
        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>        
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        List<EncounterTypeDTO> LoadEncounterTypes();

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        List<SoapTemplateTypeDTO> LoadSoapTemplateByClinicBranchId(int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        int CreateVisit(VisitDTO visitDTO, int managedByUserId);

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// update visit
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        bool SaveVisit(VisitDTO visitDTO, int managedByUserId);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>
        [OperationContract]
        //M  [FaultContract(typeof(QuivusFault))]
        bool SignVisit(VisitDTO visitDTO, int managedByUserId);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// load patient encounters for patient background
        /// </summary>
        /// <param name="patinetId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="showAll"></param>
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        PatientEncounterResult LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>
        [OperationContract]
        //M  [FaultContract(typeof(QuivusFault))]
        bool ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int managedByUserId);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization types static data
        /// </summary>        
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        List<ImmunizationTypeDTO> LoadImmunizationTypes();

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        List<string> LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization dose
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        List<string> LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Reason
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        List<string> LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Site
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        List<string> LoadImmunizationSiteByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Series
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        List<string> LoadImmunizationSeriesByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        List<ImmunizationDTO> LoadImmunizationsByVisitId(int visitId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        List<ModuleNoteDTO> LoadModuleNotesByVisitId(int visitId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// manage module notes, insert/ update/ delete
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        bool ManageModuleNotes(List<ModuleNoteDTO> moduleNotes);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        List<string> LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization rout
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
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
        [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        List<ImmunizationDTO> LoadPatientImmunizationsByPatientId(int patientId, int providerUserId, int userTypeId);
    }
}
