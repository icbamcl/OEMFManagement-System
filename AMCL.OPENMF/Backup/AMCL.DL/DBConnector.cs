using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.OracleClient;
using AMCL.COMMON;
using AMCL.GATEWAY;




/// <summary>
/// Summary description for DBConnector
/// </summary>
namespace AMCL.DL
{
    public class DBConnector
    {

        private string connectionString = null;
        private OracleConnection sqlConn = null;

        public DBConnector()
        {
            try
            {
                connectionString = ConfigReader.UNIT.ToString();
                sqlConn = new OracleConnection(connectionString);
            }

            catch (Exception exceptionObj)
            {
                throw exceptionObj;
            }
        }

        public OracleConnection GetConnection
        {
            get
            {
                return sqlConn;
            }
        }

    }
}
