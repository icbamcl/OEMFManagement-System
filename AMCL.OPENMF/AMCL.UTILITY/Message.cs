using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Summary description for Message
namespace AMCL.UTILITY
{
    public class Message
    {
        public Message()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string Success()
        {
            return "Save Successfully";
        }
        public string Error()
        {
            return "Save Failed:";
        }
        public string Duplicate()
        {
            return "Save Failed Duplicate";
        }
        public string ExceptionErrorMessageString(string exception)
        {
            exception.Replace("'", " ");
            exception.Replace(Convert.ToString(10), " ");
            return exception;
        }
        public string Design()
        {
            return "Design and Developed by Sakhawat";
        }
    }
}
