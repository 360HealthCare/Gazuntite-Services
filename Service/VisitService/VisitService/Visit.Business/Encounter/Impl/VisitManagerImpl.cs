using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Utility;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using Quivus.Utility.Factory;

using Visit.DataAccess;
using Visit.Business.Validators;

using Quivus.Model.QuickInput;
using Quivus.Model.Visit;
using Quivus.Model.PatientBackground;
using Quivus.Model.QuickInput.Notes;

namespace Visit.Business.Encounter.Impl
{
    /// <summary>
    /// Author Mansoor
    /// Date 17 Feb 2011   
    /// </summary>
    public class VisitManagerImpl : IVisitManager
    {
        private VisitDAO visitDAO;

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011   
        /// Get soap templates bases on clinic branch id
        /// </summary>
        public List<SoapTemplateTypeDTO> LoadSoapTemplateByClinicBranchId(int clinicBranchId)
        {
            List<SoapTemplateTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadSoapTemplateByClinicBranchId PARAM :: clinicBranchId " + clinicBranchId);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadSoapTemplateByClinicBranchId(clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit        
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns>
        public int CreateVisit(VisitDTO visitDTO, int managedByUserId)
        {
            int visitId = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling CreateVisit visit :: " + visitDTO + " managedByUserId " + managedByUserId);

                if (VisitValidator.ValidateVisit(visitDTO, managedByUserId, ref sb))
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }

                    visitDTO.RecGuid = System.Guid.NewGuid(); // create guid

                    visitId = visitDAO.CreateVisit(visitDTO, managedByUserId);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return visitId;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// update visit        
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns>
        public bool SaveVisit(VisitDTO visitDTO, int managedByUserId)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling CreateVisit visit :: " + visitDTO + " managedByUserId " + managedByUserId);

                if (VisitValidator.ValidateVisit(visitDTO, managedByUserId, ref sb))
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }

                    result = visitDAO.SaveVisit(visitDTO, managedByUserId);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed       
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns>
        public bool SignVisit(VisitDTO visitDTO, int managedByUserId)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling SignVisit visit :: " + visitDTO + " managedByUserId " + managedByUserId);

                if (VisitValidator.ValidateVisitForSign(visitDTO, managedByUserId, ref sb))
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }
                    result = visitDAO.SignVisit(visitDTO, managedByUserId);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <param name="patinetId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="showAll"></param>
        /// <returns></returns> 
        public PatientEncounterResult LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll)
        {
            PatientEncounterResult result = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientVisitsByPatientIdAndClinicId PARAM :: patientId" + patientId + " clinicBranchId " + clinicBranchId + " showAll " + showAll);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }
                    result = visitDAO.LoadPatientVisitsByPatientIdAndClinicId(patientId, clinicBranchId, showAll);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return result;
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
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManagePatientImmunization immunizationList :: " + immunizationList + " managedByUserId " + managedByUserId);

                if (VisitValidator.ValidateImmunization(immunizationList, managedByUserId, ref sb))
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }

                    result = visitDAO.ManagePatientImmunization(immunizationList, managedByUserId);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
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
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationLotNumberByClinicId PARAM :: clinicBranchId " + clinicBranchId + " searchCriteria " + searchCriteria);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadImmunizationLotNumberByClinicId(searchCriteria, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        public List<string> LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationDoseByClinicId PARAM :: clinicBranchId " + clinicBranchId + " searchCriteria " + searchCriteria);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadImmunizationDoseByClinicId(searchCriteria, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
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
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationRefusalReasonByClinicId PARAM :: clinicBranchId " + clinicBranchId + " searchCriteria " + searchCriteria);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadImmunizationRefusalReasonByClinicId(searchCriteria, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
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
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationSiteByClinicId PARAM :: clinicBranchId " + clinicBranchId + " searchCriteria " + searchCriteria);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadImmunizationSiteByClinicId(searchCriteria, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
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
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationSeriesByClinicId PARAM :: clinicBranchId " + clinicBranchId + " searchCriteria " + searchCriteria);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadImmunizationSeriesByClinicId(searchCriteria, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
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
                QuivusLogger.Info("Calling LoadImmunizationsByVisitId PARAM :: visitId " + visitId);

                if (visitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISTI_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                ImmunizationList = visitDAO.LoadImmunizationsByVisitId(visitId);
            }
            catch (Exception exp)
            {
                throw exp;
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
                QuivusLogger.Info("Calling LoadModuleNotesByVisitId PARAM :: visitId " + visitId);

                if (visitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISTI_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                ModuleNotesList = visitDAO.LoadModuleNotesByVisitId(visitId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ModuleNotesList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        public bool ManageModuleNotes(List<ModuleNoteDTO> moduleNotes)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManageModuleNotes moduleNotes :: " + moduleNotes);

                if (VisitValidator.ValidateModuleNotes(moduleNotes, ref sb))
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }

                    result = visitDAO.ManageModuleNotes(moduleNotes);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns> 
        public List<string> LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationManufacturerByClinicId PARAM :: clinicBranchId " + clinicBranchId + " searchCriteria " + searchCriteria);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadImmunizationManufacturerByClinicId(searchCriteria, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
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
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationRoutByClinicId PARAM :: clinicBranchId " + clinicBranchId + " searchCriteria " + searchCriteria);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadImmunizationRoutByClinicId(searchCriteria, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
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
                QuivusLogger.Info("Calling LoadPatientImmunizationsByPatientId PARAM :: patientId " + patientId + " providerUserId " + providerUserId + " userTypeId " + userTypeId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (providerUserId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (userTypeId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_TYPE_INVALID_ERROR_MSG , (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                ImmunizationList = visitDAO.LoadPatientImmunizationsByPatientId(patientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ImmunizationList;
        }
    }
}
