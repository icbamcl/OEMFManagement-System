using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Collections;
using AMCL.COMMON;
using AMCL.DL;
using AMCL.GATEWAY;

/// <summary>
/// Summary description for UnitUserBL
/// </summary>
namespace AMCL.BL
{
    public class UnitUserBL
    {
        public UnitUserBL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        CommonGateway commonGatewayObj = new CommonGateway();
        OMFDAO opendMFDAO = new OMFDAO();
        public bool CheckDuplicate(UnitHolderRegistration regObj, UnitUser userObj)
        {
            DataTable dtDuplicate = commonGatewayObj.Select("SELECT * FROM USER_INFO WHERE  USER_ID='" + userObj.UserID.ToString() + "'");//FUND_CD='" + regObj.FundCode.ToUpper().ToString() + "' AND BR_CD='" + regObj.BranchCode.ToUpper().ToString() + "' AND
            if (dtDuplicate.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void saveUser(UnitHolderRegistration regObj, UnitUser userObj ,DataTable dtUserBranch, DataTable dtUserFund)
        {
            try
            {
                commonGatewayObj.BeginTransaction();
                Hashtable htUser = new Hashtable();
                Hashtable htUserBranch = new Hashtable();
                Hashtable htUserFund = new Hashtable();

                htUser.Add("FUND_CD", regObj.FundCode.ToUpper().ToString());
                htUser.Add("BR_CD", regObj.BranchCode.ToUpper().ToString());
                htUser.Add("USER_ID", userObj.UserID.ToString());
                htUser.Add("USER_PASS", userObj.UserPassword.ToString());
                htUser.Add("USER_NM", userObj.UserName.ToString().ToUpper());
                htUser.Add("USER_STATUS", "V");
                htUser.Add("ENT_DT", DateTime.Today.ToString("dd-MMM-yyyy"));
                htUser.Add("ENT_TM", DateTime.Now.ToShortTimeString());

                commonGatewayObj.Insert(htUser, "USER_INFO");
                if (dtUserBranch.Rows.Count > 0)
                {
                    for (int loop = 0; loop < dtUserBranch.Rows.Count; loop++)
                    {
                        htUserBranch.Add("ID", commonGatewayObj.GetMaxNo("USER_BRANCH", "ID") + 1);
                        htUserBranch.Add("BR_CD", dtUserBranch.Rows[loop]["BR_CD"].ToString());
                        htUserBranch.Add("USER_ID", userObj.UserID.ToString());
                        commonGatewayObj.Insert(htUserBranch, "USER_BRANCH");
                        htUserBranch = new Hashtable();

                    }
                }
                if (dtUserFund.Rows.Count > 0)
                {
                    for (int loop = 0; loop < dtUserFund.Rows.Count; loop++)
                    {
                        htUserFund.Add("ID", commonGatewayObj.GetMaxNo("USER_FUND", "ID") + 1);
                        htUserFund.Add("FUND_CD", dtUserFund.Rows[loop]["FUND_CD"].ToString());
                        htUserFund.Add("USER_ID", userObj.UserID.ToString());
                        commonGatewayObj.Insert(htUserFund, "USER_FUND");
                        htUserFund = new Hashtable();

                    }
                }
                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }
        }
        public void saveUserMenue(UnitHolderRegistration regObj, UnitUser userObj,DataTable dtUserBranch, DataTable dtUserFund, DataTable dtUserMenu)
        {
            try
            {
                commonGatewayObj.BeginTransaction();
                Hashtable htUser = new Hashtable();
                Hashtable htUserMenu = new Hashtable();
                Hashtable htUserBranch = new Hashtable();
                Hashtable htUserFund = new Hashtable();

                htUser.Add("FUND_CD", regObj.FundCode.ToUpper().ToString());
                htUser.Add("BR_CD", regObj.BranchCode.ToUpper().ToString());
                htUser.Add("USER_ID", userObj.UserID.ToString());
                htUser.Add("USER_PASS", userObj.UserPassword.ToString());
                htUser.Add("USER_NM", userObj.UserName.ToString().ToUpper());
                htUser.Add("USER_STATUS", "V");
                htUser.Add("ENT_DT", DateTime.Today.ToString("dd-MMM-yyyy"));
                htUser.Add("ENT_TM", DateTime.Now.ToShortTimeString());

                commonGatewayObj.Insert(htUser, "USER_INFO");
                if (dtUserMenu.Rows.Count > 0)
                {
                    for (int loop = 0; loop < dtUserMenu.Rows.Count; loop++)
                    {
                        htUserMenu.Add("ID", commonGatewayObj.GetMaxNo("USER_MENU", "ID") + 1);
                        htUserMenu.Add("M_ID", dtUserMenu.Rows[loop]["M_ID"].ToString());
                        htUserMenu.Add("USER_ID", userObj.UserID.ToString());
                        commonGatewayObj.Insert(htUserMenu, "USER_MENU");
                        htUserMenu = new Hashtable();

                    }
                }
                if (dtUserBranch.Rows.Count > 0)
                {
                    for (int loop = 0; loop < dtUserBranch.Rows.Count; loop++)
                    {
                        htUserBranch.Add("ID", commonGatewayObj.GetMaxNo("USER_BRANCH", "ID") + 1);
                        htUserBranch.Add("BR_CD", dtUserBranch.Rows[loop]["BR_CD"].ToString());
                        htUserBranch.Add("USER_ID", userObj.UserID.ToString());
                        commonGatewayObj.Insert(htUserBranch, "USER_BRANCH");
                        htUserBranch = new Hashtable();

                    }
                }
                if (dtUserFund.Rows.Count > 0)
                {
                    for (int loop = 0; loop < dtUserFund.Rows.Count; loop++)
                    {
                        htUserFund.Add("ID", commonGatewayObj.GetMaxNo("USER_FUND", "ID") + 1);
                        htUserFund.Add("FUND_CD", dtUserFund.Rows[loop]["FUND_CD"].ToString());
                        htUserFund.Add("USER_ID", userObj.UserID.ToString());
                        commonGatewayObj.Insert(htUserFund, "USER_FUND");
                        htUserFund = new Hashtable();

                    }
                }
                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }
        }
        public bool CheckExistUser(UnitUser userObj)
        {
            DataTable dtExistUser = commonGatewayObj.Select("SELECT * FROM USER_INFO WHERE  USER_ID='" + userObj.UserID.ToString() + "' AND USER_PASS='" + userObj.UserPassword.ToString() + "'");//FUND_CD='" + regObj.FundCode.ToUpper().ToString() + "' AND BR_CD='" + regObj.BranchCode.ToUpper().ToString() + "' AND
            if (dtExistUser.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void updateUserPassword(UnitUser userObj)
        {
            try
            {
                commonGatewayObj.BeginTransaction();
                Hashtable htUser = new Hashtable();


                htUser.Add("USER_PASS", userObj.UserChangePassword.ToString());
                commonGatewayObj.Update(htUser, "USER_INFO", "USER_ID='" + userObj.UserID.ToString() + "'");
                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }
        }
        public bool GetUserBranchPermission(UnitHolderRegistration regObj, UnitUser userReg)
        {
            bool permission = false;
            DataTable dtPermission = commonGatewayObj.Select("SELECT * FROM USER_BRANCH WHERE USER_ID='" + userReg.UserID.ToString() + "' AND BR_CD='" + regObj.BranchCode.ToString() + "' AND VALID IS NULL");
            if (dtPermission.Rows.Count > 0)
            {
                permission = true;
            }
            else
            {
                permission = false;
            }
            return permission;

        }
        public bool GetUserFundPermission(UnitHolderRegistration regObj, UnitUser userReg)
        {
            bool permission = false;
            DataTable dtPermission = commonGatewayObj.Select("SELECT * FROM USER_FUND WHERE USER_ID='" + userReg.UserID.ToString() + "' AND FUND_CD='" + regObj.FundCode.ToString() + "' AND VALID IS NULL");
            if (dtPermission.Rows.Count > 0)
            {
                permission = true;
            }
            else
            {
                permission = false;
            }
            return permission;

        }
     
    }
}