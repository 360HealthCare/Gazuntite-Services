using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Quivus.Utility.Logger;
using Quivus.Model.Search;
using Quivus.Utility.CustomException;
using Quivus.Utility;

using Quivus.Model.PatientBackground;
using Quivus.Model.QuickInput;
using Quivus.Model.TypeDTOs;

namespace Visit.DataAccess.DB_Util
{
    /// <summary>
    /// Auhtor Mansoor
    /// Date 18 Feb 2011
    /// this class used to convert the Reader to Visit specific DTOs
    /// </summary>

    public class DataReaderToDTOConverter
    {
        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// this method convert the Reader to PatientEncounterDTO    
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<PatientEncounterDTO> PatientEncounterConverter(IDataReader objReader)
        {
            List<PatientEncounterDTO> patientEncounterList = null;
            try
            {
                if (objReader != null)
                {
                    patientEncounterList = new List<PatientEncounterDTO>();
                    {
                        while (objReader.Read())
                        {
                            PatientEncounterDTO objPatientEncounter = new PatientEncounterDTO();

                            objPatientEncounter.VisitId = int.Parse(objReader["VisitID"].ToString());

                            objPatientEncounter.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());

                            objPatientEncounter.VisitEncounterTypeName = objReader["VisitEncounterTypeName"].ToString();

                            objPatientEncounter.VisitStatus = objReader["VisitStatus"].ToString();

                            if (objReader["OwnerUserId"] != DBNull.Value)
                            {
                                objPatientEncounter.OwnerUserId = int.Parse(objReader["OwnerUserId"].ToString());
                            }

                            if (objReader["OwnerUserTypeId"] != DBNull.Value)
                            {
                                objPatientEncounter.OwnerUserTypeId = int.Parse(objReader["OwnerUserTypeId"].ToString());
                            }

                            objPatientEncounter.VisitProvider = "Provider Not Avaliable";

                            patientEncounterList.Add(objPatientEncounter);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.VISIT_DTO_CONVERSION_ERROR_MSG + ".PatientEncounterConverter", ProjectConstant.ERROR_CODE.VisitDTOConversion.ToString());
            }
            return patientEncounterList;
        }

        public static List<ImmunizationDTO> ImmunizationConverter(IDataReader objReader)
        {
            List<ImmunizationDTO> patientImmunizationList = null;
            try
            {
                if (objReader != null)
                {
                    patientImmunizationList = new List<ImmunizationDTO>();
                    {
                        while (objReader.Read())
                        {
                            ImmunizationDTO objImmunization = new ImmunizationDTO();

                            objImmunization.PatientImmunizationId = int.Parse(objReader["PatientImmunizationID"].ToString());

                            objImmunization.ImmunizationType = new ImmunizationTypeDTO();
                            objImmunization.ImmunizationType.ImmunizationTypeId = int.Parse(objReader["ImmunizationTypeID"].ToString());
                            objImmunization.VisitId = int.Parse(objReader["VisitID"].ToString());

                            if (objReader["ImmunizationNote"] != DBNull.Value)
                            {
                                objImmunization.Note = objReader["ImmunizationNote"].ToString();
                            }

                            if (objReader["Dose"] != DBNull.Value)
                            {
                                objImmunization.Dose = objReader["Dose"].ToString();
                            }

                            if (objReader["LotNumber"] != DBNull.Value)
                            {
                                objImmunization.LotNumber = objReader["LotNumber"].ToString();
                            }

                            if (objReader["DateAdmin"] != DBNull.Value)
                            {
                                objImmunization.DateAdmin = DateTime.Parse(objReader["DateAdmin"].ToString());
                            }

                            if (objReader["NextDoseDue"] != DBNull.Value)
                            {
                                objImmunization.NextDoseDue = DateTime.Parse(objReader["NextDoseDue"].ToString());
                            }

                            if (objReader["IsRefusal"] != DBNull.Value)
                            {
                                objImmunization.IsRefusal = bool.Parse(objReader["IsRefusal"].ToString());
                            }

                            if (objReader["RefusalReason"] != DBNull.Value)
                            {
                                objImmunization.RefusalReason = objReader["RefusalReason"].ToString();
                            }

                            if (objReader["Site"] != DBNull.Value)
                            {
                                objImmunization.Site = objReader["Site"].ToString();
                            }

                            if (objReader["Series"] != DBNull.Value)
                            {
                                objImmunization.Series = objReader["Series"].ToString();
                            }

                            if (objReader["Manufacturer"] != DBNull.Value)
                            {
                                objImmunization.Manufacturer = objReader["Manufacturer"].ToString();
                            }

                            if (objReader["Route"] != DBNull.Value)
                            {
                                objImmunization.Rout = objReader["Route"].ToString();
                            }

                            if (objReader["DictationID"] != DBNull.Value)
                            {
                                objImmunization.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                            }

                            patientImmunizationList.Add(objImmunization);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.VISIT_DTO_CONVERSION_ERROR_MSG + ".ImmunizationConverter", ProjectConstant.ERROR_CODE.VisitDTOConversion.ToString());
            }
            return patientImmunizationList;
        }
    }
}
