using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text;
using System.IO;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class ReportViewer_UnitReportStatementAfterClosingReportViewer : System.Web.UI.Page
{

    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
    AMCL.REPORT.CR_UnitReportRegistrationForBranch CR_StatementAfterClosing = new AMCL.REPORT.CR_UnitReportRegistrationForBranch();

    protected void Page_Load(object sender, EventArgs e)
    {
        string fundCodeStatement = "";
        string branchCodeStatement = "";

       if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        branchCodeStatement = (string)Session["branchCodeStatement"];
        fundCodeStatement = (string)Session["fundCodeStatement"];
        



        DataTable dtUnitHolderInfo = reportObj.getDtHolderInfo();
        dtUnitHolderInfo.TableName = "UnitHolderStatementAfterClosing";
        DataRow drUnitHolderInfo;

        DataTable dtHolderInfo = (DataTable)Session["dtStatementAfterClosing"];
        
        if (dtHolderInfo.Rows.Count > 0)
        {
            
            for (int looper = 0; looper < dtHolderInfo.Rows.Count; looper++)
            {
                drUnitHolderInfo = dtUnitHolderInfo.NewRow();
                drUnitHolderInfo["REG_NO"] = dtHolderInfo.Rows[looper]["REGI_NUMBER"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["REGI_NUMBER"].ToString();
                             
         
                drUnitHolderInfo["HNAME"] = dtHolderInfo.Rows[looper]["HNAME"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["HNAME"].ToString();
                drUnitHolderInfo["REG_BR_NAME"] = dtHolderInfo.Rows[looper]["REG_BR"].Equals(DBNull.Value) ? "" : opendMFDAO.GetBranchName(dtHolderInfo.Rows[looper]["REG_BR"].ToString());

                drUnitHolderInfo["BEFTN"] = dtHolderInfo.Rows[looper]["BEFTN"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["BEFTN"].ToString();
               
                drUnitHolderInfo["ADDRS1"] = dtHolderInfo.Rows[looper]["ADDRS1"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["ADDRS1"].ToString();
                drUnitHolderInfo["ADDRS2"] = dtHolderInfo.Rows[looper]["ADDRS2"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["ADDRS2"].ToString();
                drUnitHolderInfo["CITY"] = dtHolderInfo.Rows[looper]["CITY"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["CITY"].ToString();
                drUnitHolderInfo["TIN"] = dtHolderInfo.Rows[looper]["TIN"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["TIN"].ToString();
                drUnitHolderInfo["PASS_NO"] = dtHolderInfo.Rows[looper]["PASS_NO"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["BEFTN"].ToString();
                drUnitHolderInfo["BIRTH_CERT_NO"] = dtHolderInfo.Rows[looper]["BIRTH_CERT_NO"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["BEFTN"].ToString();
                drUnitHolderInfo["BIRTH_CERT_NO"] = dtHolderInfo.Rows[looper]["BIRTH_CERT_NO"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["BEFTN"].ToString();                
                drUnitHolderInfo["NID"] = dtHolderInfo.Rows[looper]["NID"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["NID"].ToString();
                drUnitHolderInfo["BALANCE"] =Convert.ToInt32(dtHolderInfo.Rows[looper]["BALANCE"].Equals(DBNull.Value) ? "0" : dtHolderInfo.Rows[looper]["BALANCE"].ToString());
                
                drUnitHolderInfo["JNT_NAME"] = dtHolderInfo.Rows[looper]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["JNT_NAME"].ToString();
                

                drUnitHolderInfo["CIP"] = dtHolderInfo.Rows[looper]["CIP"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["CIP"].ToString();

                if (!dtHolderInfo.Rows[looper]["CIP"].Equals(DBNull.Value))
                {
                    if (string.Compare(dtHolderInfo.Rows[looper]["CIP"].ToString(), "Y", true) == 0)
                    {
                        drUnitHolderInfo["CIP"] = "YES";
                    }
                    else if (string.Compare(dtHolderInfo.Rows[looper]["CIP"].ToString(), "N", true) == 0)
                    {
                        drUnitHolderInfo["CIP"] = "NO";
                    }
                  
                }
               

                if (!dtHolderInfo.Rows[looper]["ID_FLAG"].Equals(DBNull.Value))
                {
                    if (string.Compare(dtHolderInfo.Rows[looper]["ID_FLAG"].ToString(), "Y", true )== 0)
                    {
                        drUnitHolderInfo["ID_FLAG"] = "YES";
                        drUnitHolderInfo["ID_AC"] = dtHolderInfo.Rows[looper]["ID_AC"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["ID_AC"].ToString();
                        drUnitHolderInfo["ID_INTSTITUTE_NAME"] = dtHolderInfo.Rows[looper]["BANK_NAME"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["BANK_NAME"].ToString();
                        drUnitHolderInfo["ID_INTSTITUTE_BRANCH_NAME"] = dtHolderInfo.Rows[looper]["BRANCH_NAME"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["BRANCH_NAME"].ToString();
                    }
                    else if (string.Compare(dtHolderInfo.Rows[looper]["ID_FLAG"].ToString(), "N", true)== 0)
                    {
                        drUnitHolderInfo["ID_FLAG"] = "NO";
                      
                    }
                   
                }
               


                if (!dtHolderInfo.Rows[looper]["BK_FLAG"].Equals(DBNull.Value))
                {
                    if (string.Compare(dtHolderInfo.Rows[looper]["BK_FLAG"].ToString(), "Y", true )== 0)
                    {
                        if (!dtHolderInfo.Rows[looper]["BK_NM_CD"].Equals(DBNull.Value) && !dtHolderInfo.Rows[looper]["BK_BR_NM_CD"].Equals(DBNull.Value) && !dtHolderInfo.Rows[looper]["BK_AC_NO"].Equals(DBNull.Value))
                        {
                            drUnitHolderInfo["BK_AC_NO"] = dtHolderInfo.Rows[looper]["BK_AC_NO"].ToString();
                            drUnitHolderInfo["BANK_NAME"] = reportObj.getBankNameByBankCode(Convert.ToInt16(dtHolderInfo.Rows[looper]["BK_NM_CD"].ToString())).ToString();
                            drUnitHolderInfo["BRANCH_NAME"] = reportObj.getBankBranchNameByCode(Convert.ToInt16(dtHolderInfo.Rows[looper]["BK_NM_CD"].ToString()), Convert.ToInt16(dtHolderInfo.Rows[looper]["BK_BR_NM_CD"].ToString())).ToString();
                            drUnitHolderInfo["BRANCH_ADDRESS"] = reportObj.getBankBranchAddressByCode(Convert.ToInt16(dtHolderInfo.Rows[looper]["BK_NM_CD"].ToString()), Convert.ToInt16(dtHolderInfo.Rows[looper]["BK_BR_NM_CD"].ToString())).ToString();
                        }
                        else
                        {
                            string branchAddress = "";
                            string BankAccInfo = dtHolderInfo.Rows[looper]["SPEC_IN1"].ToString() + dtHolderInfo.Rows[looper]["SPEC_IN2"].ToString();
                            string[] BankAccountInfo = BankAccInfo.Split(',');
                            if (BankAccountInfo.Length > 0)
                            {
                                drUnitHolderInfo["BK_AC_NO"] = BankAccountInfo[0].ToString();
                                if (BankAccountInfo.Length > 1)
                                {
                                    drUnitHolderInfo["BANK_NAME"] = BankAccountInfo[1].ToString();
                                }
                                if (BankAccountInfo.Length > 2)
                                {
                                    drUnitHolderInfo["BRANCH_NAME"] = BankAccountInfo[2].ToString();
                                }
                                if (BankAccountInfo.Length > 3)
                                {
                                    for (int loop = 3; loop < BankAccountInfo.Length; loop++)
                                    {
                                        branchAddress = branchAddress + BankAccountInfo[loop].ToString();
                                    }
                                    drUnitHolderInfo["BRANCH_ADDRESS"] = branchAddress;
                                }
                              
                            }
                        }
                    }
                  
                }
             

                DataTable dtNominee=reportObj.dtNominee(dtHolderInfo.Rows[looper]["REG_BK"].ToString(),dtHolderInfo.Rows[looper]["REG_BR"].ToString(),Convert.ToInt32(dtHolderInfo.Rows[looper]["REG_NO"].ToString()));
                if (dtNominee.Rows.Count>0)
                {
                    for (int loop = 0; loop < dtNominee.Rows.Count; loop++)
                    {
                        if (Convert.ToInt16(dtNominee.Rows[loop]["NOMI_NO"].ToString())==1)
                        {
                            
                            drUnitHolderInfo["NOMI1_NAME"] = dtNominee.Rows[loop]["NOMI_NAME"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_NAME"].ToString();
                            
                        }
                        else if (Convert.ToInt16(dtNominee.Rows[loop]["NOMI_NO"].ToString()) ==2)
                        {
                            drUnitHolderInfo["NOMI2_NAME"] = dtNominee.Rows[loop]["NOMI_NAME"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_NAME"].ToString();                            

                        }
                    }
                }
                dtUnitHolderInfo.Rows.Add(drUnitHolderInfo);
            }

            //dtUnitHolderInfo.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2008\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtUnitHolderStatementAfterClosing.xsd");


            CR_StatementAfterClosing.Refresh();
            CR_StatementAfterClosing.SetDataSource(dtUnitHolderInfo);
           
            CR_StatementAfterClosing.SetParameterValue("branchName", opendMFDAO.GetBranchName(branchCodeStatement.ToString()));
            CR_StatementAfterClosing.SetParameterValue("fundName", opendMFDAO.GetFundName(fundCodeStatement.ToString()));
            CrystalReportViewer1.ReportSource = CR_StatementAfterClosing;
            //CR_StatementAfterClosing.Close();
            //CR_StatementAfterClosing.Dispose();
  
           

        }
        else
        {
           
            Response.Write("No data found");
        }




    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_StatementAfterClosing.Close();
        CR_StatementAfterClosing.Dispose();
      

    }
    
}
