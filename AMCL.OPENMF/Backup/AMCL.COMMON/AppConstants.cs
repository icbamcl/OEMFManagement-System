using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Xml.Linq;

/// <summary>
/// Summary description for AppConstants
/// </summary>
namespace AMCL.COMMON
{
    public class AppConstants
    {
        public AppConstants()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string CONN_STRING_OEMF = "UNIT";
        public static string CONN_STRING_OEMF_PEN = "PENSION";
        public static string CONN_STRING_SignLocation = "SingLocation";
        public static string CONN_STRING_PhotoLocation = "PhotoLocation";
        public static string CONN_STRING_BIZ_SCHEMA = "SCHEMA";
        public static string CONN_STRING_ExportFileLocation = "ExportFileLocation";
    }
}