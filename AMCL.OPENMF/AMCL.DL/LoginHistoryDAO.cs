using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMCL.GATEWAY;
using AMCL.COMMON;
using System.Collections;

namespace AMCL.DL
{
    public class LoginHistoryDAO
    {
        string bizSchema = ConfigReader.SCHEMA;
        CommonGateway commonGatewayObj = new CommonGateway();
        public long Login(BaseClass bcContent)
        {
            try
            {
                commonGatewayObj.BeginTransaction();
                Hashtable htUpdate = new Hashtable();
                htUpdate.Add("ISONLINE", 0);
                commonGatewayObj.Update(htUpdate, bizSchema + ".LOGINHISTORY", "USER_ID='" + bcContent.LoginID.ToString() + "' AND ISONLINE=1");
              //  string SQLQuery = "UPDATE " + bizSchema + ".LOGINHISTORY SET  ISONLINE=0 WHERE USER_ID='" + bcContent.LoginID.ToString() + "' AND ISONLINE=1";
                //commonGatewayObj.ExecuteNonQuery(SQLQuery.ToString());
                

               
                string SQLQuery = "SELECT NVL(MAX(ID),0)+1 AS MAX_ID FROM " + bizSchema + ".LOGINHISTORY WHERE VALID IS NULL";
                long SessionID = Convert.ToInt64(commonGatewayObj.ExecuteScalar(SQLQuery.ToString()));
                Hashtable htInsert = new Hashtable();
                
                htInsert.Add("ID", SessionID);
                htInsert.Add("USER_ID", bcContent.LoginID.ToString());
                htInsert.Add("PROJECT_ID", bcContent.AppId);
                htInsert.Add("LOGINTIME", DateTime.Now);
                htInsert.Add("ISONLINE", 1);
                htInsert.Add("LOGIN_BR", bcContent.BranchCode.ToString());
                htInsert.Add("LOGIN_BK", bcContent.FundCode.ToString());


                //SQLQuery = " INSERT INTO " + bizSchema + ".LOGINHISTORY(ID,USER_ID,LOGINTIME,ISONLINE,LOGIN_BR,LOGIN_BK) VALUES(" + SessionID + ",'" + bcContent.LoginID.ToString() + "',TO_CHAR(TO_DATE('" + DateTime.Now.ToString() + "','dd-MON-yyyy HH:MI:SS'),'dd-MON-yyyy HH:MI:SS'),1,'" + bcContent.BranchCode.ToString() + "','" + bcContent.FundCode.ToString() + "')";
                commonGatewayObj.Insert(htInsert, bizSchema + ".LOGINHISTORY");
                commonGatewayObj.CommitTransaction();

                return SessionID;
            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }
        }

        public void Logout(long sessionID)
        {
            try
            {
                commonGatewayObj.BeginTransaction();
                DateTime LogoutTime = DateTime.Now;
                commonGatewayObj.BeginTransaction();
                Hashtable htUpdate = new Hashtable();
                htUpdate.Add("ISONLINE", 0);
                htUpdate.Add("LOGOUTTIME", LogoutTime);
                commonGatewayObj.Update(htUpdate, bizSchema + ".LOGINHISTORY", "ID=" + sessionID);
               // string SQLQuery = "UPDATE " + bizSchema + ".LOGINHISTORY SET  ISONLINE=0, LOGOUTTIME='" + LogoutTime + "' WHERE ID=" + sessionID;
                //commonGatewayObj.ExecuteNonQuery(SQLQuery.ToString());
                commonGatewayObj.CommitTransaction();
            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }
        }
    }
}
