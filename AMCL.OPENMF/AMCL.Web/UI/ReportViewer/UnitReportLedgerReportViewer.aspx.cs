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

public partial class ReportViewer_UnitReportLedgerReportViewer : System.Web.UI.Page
{

    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    NumberToEnglish numberToEnglisObj = new NumberToEnglish();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
    ReportDocument rdoc = new ReportDocument();
    AMCL.REPORT.CR_UnitLedgerStatement CR_Ledger = new AMCL.REPORT.CR_UnitLedgerStatement();

    protected void Page_Load(object sender, EventArgs e)
    {
        string fundCode = "";
        string branchCode="";
        string regiNo = "";
        string sipHolder = "";
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
          
        branchCode = (string)Session["branchCode"];
        fundCode = (string)Session["fundCode"];
        regiNo = (string)Session["regiNo"];
        sipHolder = (string)Session["sipHolder"];





        DataTable dtLedgerForReport = (DataTable)Session["dtLedgerForReport"];
        dtLedgerForReport.TableName = "dtLedgerForReport";

        if (dtLedgerForReport.Rows.Count > 0)
        {
            UnitHolderRegistration unitRegObj = new UnitHolderRegistration();
            unitRegObj.FundCode = fundCode.ToString();
            unitRegObj.BranchCode = branchCode.ToString();
            unitRegObj.RegNumber = regiNo.ToString();
            DataTable dtLedgerRegInfo = opendMFDAO.getDtRegInfo(unitRegObj);
            dtLedgerRegInfo.TableName = "dtLedgerRegInfo";
            
            string regiNumber=dtLedgerRegInfo.Rows[0]["REG_BK"].ToString()+"/"+dtLedgerRegInfo.Rows[0]["REG_BR"].ToString()+"/"+dtLedgerRegInfo.Rows[0]["REG_NO"].ToString();
            string holderName=dtLedgerRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value)? "":dtLedgerRegInfo.Rows[0]["HNAME"].ToString();
            string JointHolderName=dtLedgerRegInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value)? "":dtLedgerRegInfo.Rows[0]["JNT_NAME"].ToString();
            string address1=dtLedgerRegInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value)? "":dtLedgerRegInfo.Rows[0]["ADDRS1"].ToString();
            string address2=dtLedgerRegInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value)? "":dtLedgerRegInfo.Rows[0]["ADDRS2"].ToString();
            string city=dtLedgerRegInfo.Rows[0]["CITY"].Equals(DBNull.Value)? "":dtLedgerRegInfo.Rows[0]["CITY"].ToString();
            string cip=dtLedgerRegInfo.Rows[0]["CIP"].Equals(DBNull.Value)? "":dtLedgerRegInfo.Rows[0]["CIP"].Equals("Y") ? "YES":"NO";
            string regType = dtLedgerRegInfo.Rows[0]["REG_TYPE"].Equals(DBNull.Value) ? "" : reportObj.getRegTypeFullName(dtLedgerRegInfo.Rows[0]["REG_TYPE"].ToString());
            string BO_Folio = "";
            if (!dtLedgerRegInfo.Rows[0]["BO"].Equals(DBNull.Value))
            {
                BO_Folio = dtLedgerRegInfo.Rows[0]["BO"].ToString();
            }
            else if ( !dtLedgerRegInfo.Rows[0]["FOLIO_NO"].Equals(DBNull.Value))
            {
                BO_Folio = dtLedgerRegInfo.Rows[0]["FOLIO_NO"].ToString();
            }
            string BEFTN = "";
            if (!dtLedgerRegInfo.Rows[0]["IS_BEFTN"].Equals(DBNull.Value))
            {
                BEFTN = dtLedgerRegInfo.Rows[0]["IS_BEFTN"].Equals("Y") ? "YES" : "NO";
            }
            
            string ETIN = dtLedgerRegInfo.Rows[0]["TIN"].Equals(DBNull.Value) ? "" : dtLedgerRegInfo.Rows[0]["TIN"].ToString();





         //  dtLedgerForReport. WriteXmlSchema(@"F:\GITHUB_PROJECT\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtLedgerForReport.xsd");

            CR_Ledger.Refresh();
            CR_Ledger.SetDataSource(dtLedgerForReport);

            CR_Ledger.SetParameterValue("fundName", opendMFDAO.GetFundName(fundCode.ToString()));
            CR_Ledger.SetParameterValue("branchName", opendMFDAO.GetBranchName(branchCode.ToString()).ToString());
            CR_Ledger.SetParameterValue("branchCode", branchCode.ToString());
            CR_Ledger.SetParameterValue("Regi_No", regiNumber);
            CR_Ledger.SetParameterValue("Holder_Name", holderName.ToString());
            CR_Ledger.SetParameterValue("Jholder_name", JointHolderName.ToString());
            CR_Ledger.SetParameterValue("Address1", address1.ToString());
            CR_Ledger.SetParameterValue("Address2", address2.ToString());
            CR_Ledger.SetParameterValue("City", city.ToString());
            CR_Ledger.SetParameterValue("Cip", cip.ToString());
          //  CR_Ledger.SetParameterValue("Reg_Type", regType.ToString());
            CR_Ledger.SetParameterValue("BO_Folio", BO_Folio.ToString());
            CR_Ledger.SetParameterValue("BEFTN", BEFTN.ToString());
            CR_Ledger.SetParameterValue("ETIN", ETIN.ToString());

            if(sipHolder.ToString().ToUpper()=="YES")
            {
                CR_Ledger.SetParameterValue("Frac_Amt", "Fraction Amount (Tk.)");
            }
            else
            {
                CR_Ledger.SetParameterValue("Frac_Amt", " ");
            }

            CrystalReportViewer1.ReportSource = CR_Ledger;

                                

        }
        else
        {
            Response.Write("No data found");
        }




    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_Ledger.Close();
        CR_Ledger.Dispose();
    }
}
