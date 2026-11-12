using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Quivus.Utility.Logger;

namespace UserManager.Business.Validators
{
    public static class Validations
    {
        public static bool ValidateSSN(string value)
        {
            string SSN = value;
            //SSN = SSN.Replace("-", string.Empty);

            // For xxx-xxx-xxxx format use \d{3}[-]\d{3}[-]\d{4}
            //  if (!Regex.IsMatch(SSN, @"^\d{9}$"))
            if (Regex.IsMatch(SSN, @"^\d{3}[-]\d{2}[-]\d{4}$"))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateEmailAddress(string emailAddress)
        {
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
                        + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                        + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                        + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                        + @"[a-zA-Z]{2,}))$";
            Regex reStrict = new Regex(patternStrict);

            bool isStrictMatch = reStrict.IsMatch(emailAddress);
            return isStrictMatch;

        }

        public static bool ValidatePhoneNumber(string value)
        {
            string PhoneNumber = value;
            PhoneNumber = PhoneNumber.Replace("(", string.Empty);
            PhoneNumber = PhoneNumber.Replace(")", string.Empty);
            PhoneNumber = PhoneNumber.Replace("-", string.Empty);

            if (Regex.IsMatch(PhoneNumber, @"^([0-9]{10,20})$"))
            {
                return true;
            }
            return false;
        }

        public static bool ValidatePhoneExtension(string value)
        {
            if (!Regex.IsMatch(value, @"^\d{4}$"))
            {
                return false;
            }
            return true;
        }
        //^[0-9a-zA-Z]+$
        public static bool IsAlphaNumeric(string strToCheck)
        {

            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9 ]");
            return objAlphaNumericPattern.IsMatch(strToCheck);
        }

        public static bool IsValidateDateTime(DateTime dt)
        {
            QuivusLogger.Info("Calling Method IsValidateDateTime dt:: " + dt);
            if (dt != null) // Added By Ismail to Be check by mansoor
            {
                DateTime result = DateTime.Now;
                DateTime limit = new DateTime(1901, 1, 1);
                if (DateTime.Compare(dt, limit) < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
                return true;
        }

        public static bool IsValidSearchDate(string value, out DateTime date)
        {
            bool flag = false;

            date = new DateTime();

            try
            {
                if (Regex.IsMatch(value, @"^[0-9]{8}$"))
                {
                    date = new DateTime(int.Parse(value.Substring(4, 4)), int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 2)));
                    flag = true;
                }
                else if (Regex.IsMatch(value, @"^[0-9]{2}-[0-9]{2}-[0-9]{2}$"))
                {
                    flag = true;
                    date = new DateTime(2000 + int.Parse(value.Substring(6, 2)), int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(3, 2)));
                }
                else if (Regex.IsMatch(value, @"^[0-9]{2}/[0-9]{2}/[0-9]{4}$"))
                {
                    flag = true;
                    date = new DateTime(int.Parse(value.Substring(6, 4)), int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(3, 2)));
                }                
            }
            catch
            {                
            }
            return flag;
        }

    }
}
