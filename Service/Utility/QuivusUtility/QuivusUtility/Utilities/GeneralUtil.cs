using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quivus.Utility.Utilities
{
    public static class GeneralUtil
    {
        public static string GetUniqueNumber(int length)
        {
            Random r = new Random(GenerateId());
            string result = "" + r.Next(10000000, 99999999);
            if (result.Length > length)
            {
                result = result.Substring(0, length);
            }
            return result;

        }
        private static int GenerateId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt16(buffer, 0);
        }
    }
}
