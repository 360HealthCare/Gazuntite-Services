using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


using Quivus.Utility.Logger;
using Visit.Business.Common;
using Visit.Business.Encounter;
using Visit.Business.Encounter.Impl;

using Quivus.Model.QuickInput;
using Quivus.Model.TypeDTOs;
using Quivus.Model.Visit;
using Quivus.Model.PatientBackground;
using Quivus.Utility.CustomException;
using QuivusUtility.Utilities;
using Quivus.Model.QuickInput.Notes;


namespace VisitService.Service
{
    public class VisitService : QuivusBaseService, IVisitService
    {

        VisitStaticDataManager objVisitSDManager = null;
        IVisitManager objVisitManager = null;

        #region Default Implementation of GetServiceVersion
        public override string GetServiceVersion()
        {
            string strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return strVersion;
        }
        #endregion

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>
        public List<EncounterTypeDTO> LoadEncounterTypes()
        {
            List<EncounterTypeDTO> response = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                response = objVisitSDManager.LoadEncounterTypes();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the template types
        /// </summary>
        /// <returns></returns>        
        public List<SoapTemplateTypeDTO> LoadSoapTemplateByClinicBranchId(int clinicBranchId)
        {
            List<SoapTemplateTypeDTO> response = null;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                response = objVisitManager.LoadSoapTemplateByClinicBranchId(clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit
        /// </summary>
        /// <returns></returns>      
        public int CreateVisit(VisitDTO visitDTO, int managedByUserId)
        {
            int visitId = 0;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                visitId = objVisitManager.CreateVisit(visitDTO, managedByUserId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return visitId;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// update visit       
        /// </summary>
        /// <returns></returns>      
        public bool SaveVisit(VisitDTO visitDTO, int managedByUserId)
        {
            bool result = false;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                result = objVisitManager.SaveVisit(visitDTO, managedByUserId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>        
        public bool SignVisit(VisitDTO visitDTO, int managedByUserId)
        {
            bool result = false;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                result = objVisitManager.SignVisit(visitDTO, managedByUserId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return result;
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
        public PatientEncounterResult LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll)
        {
            PatientEncounterResult response = null;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                response = objVisitManager.LoadPatientVisitsByPatientIdAndClinicId(patientId, clinicBranchId, showAll);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
        public bool ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int managedByUserId)
        {
            bool result = false;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                result = objVisitManager.ManagePatientImmunization(immunizationList, managedByUserId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization types static data
        /// </summary>        
        /// <returns></returns>
        public List<ImmunizationTypeDTO> LoadImmunizationTypes()
        {
            List<ImmunizationTypeDTO> response = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                response = objVisitSDManager.LoadImmunizationTypes();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
        public List<string> LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> response = null;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                response = objVisitManager.LoadImmunizationLotNumberByClinicId(searchCriteria, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
        public List<string> LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> response = null;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                response = objVisitManager.LoadImmunizationDoseByClinicId(searchCriteria, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
        public List<string> LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> response = null;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                response = objVisitManager.LoadImmunizationRefusalReasonByClinicId(searchCriteria, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
        public List<string> LoadImmunizationSiteByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> response = null;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                response = objVisitManager.LoadImmunizationSiteByClinicId(searchCriteria, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
        public List<string> LoadImmunizationSeriesByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> response = null;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                response = objVisitManager.LoadImmunizationSeriesByClinicId(searchCriteria, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>     
        public List<ImmunizationDTO> LoadImmunizationsByVisitId(int visitId)
        {
            List<ImmunizationDTO> ImmunizationList = null;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                ImmunizationList = objVisitManager.LoadImmunizationsByVisitId(visitId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ImmunizationList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>     
        public List<ModuleNoteDTO> LoadModuleNotesByVisitId(int visitId)
        {
            List<ModuleNoteDTO> ModuleNotesList = null;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                ModuleNotesList = objVisitManager.LoadModuleNotesByVisitId(visitId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ModuleNotesList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// manage module notes, insert/ update/ delete
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>        
        public bool ManageModuleNotes(List<ModuleNoteDTO> moduleNotes)
        {
            bool result = false;

            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                result = objVisitManager.ManageModuleNotes(moduleNotes);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>      
        public List<string> LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> response = null;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                response = objVisitManager.LoadImmunizationManufacturerByClinicId(searchCriteria, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
        public List<string> LoadImmunizationRoutByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> response = null;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                response = objVisitManager.LoadImmunizationRoutByClinicId(searchCriteria, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
        public List<ImmunizationDTO> LoadPatientImmunizationsByPatientId(int patientId, int providerUserId, int userTypeId)
        {
            List<ImmunizationDTO> ImmunizationList = null;
            try
            {
                if (objVisitManager == null)
                {
                    objVisitManager = new VisitManagerImpl();
                }

                ImmunizationList = objVisitManager.LoadPatientImmunizationsByPatientId(patientId, providerUserId, userTypeId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ImmunizationList;
        }
    }
}
