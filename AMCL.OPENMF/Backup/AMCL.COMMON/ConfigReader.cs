using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Xml.Linq;
using AMCL.COMMON;
/// <summary>
/// Summary description for ConfigReader
namespace AMCL.COMMON
{
    public class ConfigReader
    {
        public ConfigReader()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string UNIT
        {
            get
            {

                if (System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING_OEMF] != null)
                {
                    return System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING_OEMF];
                }
                return "";
            }
        }
        public static string PENSION
        {
            get
            {

                if (System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING_OEMF_PEN] != null)
                {
                    return System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING_OEMF_PEN];
                }
                return "";
            }
        }
        public static string SingLocation
        {
            get
            {

                if (System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING_SignLocation] != null)
                {
                    return System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING_SignLocation];
                }
                return "";
            }
        }
        public static string PhotoLocation
        {
            get
            {

                if (System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING_PhotoLocation] != null)
                {
                    return System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING_PhotoLocation];
                }
                return "";
            }
        }
        public static string SCHEMA
        {
            get
            {

                if (System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING_BIZ_SCHEMA] != null)
                {
                    return System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING_BIZ_SCHEMA];
                }
                return "";
            }
        }
        public static string ExportFileLocation
        {
            get
            {

                if (System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING_ExportFileLocation] != null)
                {
                    return System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING_ExportFileLocation];
                }
                return "";
            }
        }

    }
}
