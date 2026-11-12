using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Utility.Logger;
using Quivus.Utility.Factory;
using Visit.DataAccess;

using Quivus.Model.TypeDTOs;

namespace Visit.Business.Common
{
    /// <summary>
    /// Author Mansoor
    /// Date 17 Feb 2011
    /// List all the static data related to visit
    /// </summary>
    public class VisitStaticDataManager
    {
        private VisitDAO visitDAO;

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// get the Encounter types
        /// </summary>
        /// <returns></returns>
        public List<EncounterTypeDTO> LoadEncounterTypes()
        {
            List<EncounterTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListEncounterTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.GetAllEncounterTypes();
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
        /// get immunization types static data
        /// </summary>        
        /// <returns></returns>
        public List<ImmunizationTypeDTO> LoadImmunizationTypes()
        {
            List<ImmunizationTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadImmunizationTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.GetAllImmunizationTypes();
            }
            catch (Exception exp)
            {  
                throw exp;
            }
            return list;
        }
    }
}
