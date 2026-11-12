using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quivus.Utility.CustomException
{
    public class InvalidArgumentException : Exception
    {
        //private string p;
      
        public int Code { get; set; }
        public String ErrorDetail { get; set; }
        public InvalidArgumentException(string message, int code)
            : base(message)
        {
            Code = code;
        }

        public InvalidArgumentException(string message)
            : base(message)
        {

        }

        public InvalidArgumentException(string message,  String Detail)
            : base(message)
        {
           
            this.ErrorDetail = Detail;
        }

       
    }
}