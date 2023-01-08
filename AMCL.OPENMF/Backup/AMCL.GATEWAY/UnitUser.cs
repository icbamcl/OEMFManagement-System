using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;

/// <summary>
/// Summary description for User
/// </summary>
namespace AMCL.GATEWAY
{
    public class UnitUser
    {
        public UnitUser()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private string userID;

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string userPassword;

        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }
        private string userChangePassword;
        public string UserChangePassword
        {
            get { return userChangePassword; }
            set { userChangePassword = value; }
        }

    }
}