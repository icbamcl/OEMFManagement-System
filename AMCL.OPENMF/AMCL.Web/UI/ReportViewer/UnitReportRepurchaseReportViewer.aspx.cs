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

public partial class ReportViewer_UnitReportRepurchaseReportViewer: System.Web.UI.Page
{

    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
    AMCL.REPORT.CR_UnitRepurchaseStatement rep_Statement = new AMCL.REPORT.CR_UnitRepurchaseStatement();
    AMCL.REPORT.CR_UnitRepurchaseStatementSummary rep_Statement_Summary = new AMCL.REPORT.CR_UnitRepurchaseStatementSummary();

    protected void Page_Load(object sender, EventArgs e)
    {
        string fundCode = "";
        string branchCode="";
        
       if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        branchCode = (string)Session["branchCode"];
        fundCode = (string)Session["fundCode"];
            
        
        DataTable dtReport = reportObj.getDtForReportStatement();
        dtReport.TableName = "ReportStatement";
        DataRow drReport;

        DataTable dtReportStatement = (DataTable)Session["dtReportStatement"];
        string report_Type=(string)Session["Report_Type"];

        if (dtReportStatement.Rows.Count > 0)
        {
            int repNo = 0;
            string SL_TR_NO="";


            for (int looper = 0; looper < dtReportStatement.Rows.Count; looper++)
            {
                drReport = dtReport.NewRow();
                drReport["REP_NO"] = Convert.ToInt32(dtReportStatement.Rows[looper]["REP_NO"].ToString());
                drReport["REP_DT"] = dtReportStatement.Rows[looper]["REP_DT"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["REP_DT"].ToString();
                if(report_Type=="DETAILS")
                drReport["SL_TR_REP_NO"] = dtReportStatement.Rows[looper]["SL_TR_NO"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["SL_TR_NO"].ToString();

                drReport["HNAME"] = dtReportStatement.Rows[looper]["HNAME"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["HNAME"].ToString();               
                drReport["JNT_NAME"] = dtReportStatement.Rows[looper]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["JNT_NAME"].ToString();             
                drReport["ADDRS1"] = dtReportStatement.Rows[looper]["ADDRS1"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["ADDRS1"].ToString();
                drReport["ADDRS2"] = dtReportStatement.Rows[looper]["ADDRS2"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["ADDRS2"].ToString();
                drReport["CITY"] = dtReportStatement.Rows[looper]["CITY"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["CITY"].ToString();
                drReport["REG_NO"] = dtReportStatement.Rows[looper]["REG_NO"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["REG_NO"].ToString();
               
                drReport["BO"] = dtReportStatement.Rows[looper]["BO"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["BO"].ToString();
                if (report_Type == "DETAILS")
                {
                    repNo = Convert.ToInt32(dtReportStatement.Rows[looper]["REP_NO"].ToString());
                    SL_TR_NO = dtReportStatement.Rows[looper]["SL_TR_NO"].ToString();
                    drReport["CERT_NO"] = reportObj.getTotalCertNo("SELECT NVL(CERT_TYPE,' ') AS CERT_TYPE, NVL(CERT_NO,0) AS CERT_NO FROM REP_CERT_NO WHERE REP_NO=" + repNo + " AND REG_BK='" + fundCode.ToString() + "'AND REG_BR='" + branchCode.ToString() + "' AND SL_TR_NO='" + SL_TR_NO.ToString() + "'", fundCode.ToString()).ToString();
                }
                drReport["CIP"] = dtReportStatement.Rows[looper]["CIP"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["CIP"].ToString();
               // drReport["ID_AC"] = dtReportStatement.Rows[looper]["ID_AC"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["ID_AC"].ToString();
                drReport["QTY"] =Convert.ToInt32( dtReportStatement.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0": dtReportStatement.Rows[looper]["QTY"].ToString());
                drReport["RATE"] =decimal.Parse( dtReportStatement.Rows[looper]["RATE"].Equals(DBNull.Value) ? "0" : dtReportStatement.Rows[looper]["RATE"].ToString());
                drReport["AMOUNT"] = decimal.Parse(dtReportStatement.Rows[looper]["AMOUNT"].Equals(DBNull.Value) ? "0" : dtReportStatement.Rows[looper]["AMOUNT"].ToString());


              dtReport.Rows.Add(drReport);
            }

            // dtReport.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtUnitReportForStatement.xsd");

            if (report_Type == "DETAILS")
            {
                rep_Statement.Refresh();
                rep_Statement.SetDataSource(dtReport);

                rep_Statement.SetParameterValue("fundName", opendMFDAO.GetFundName(fundCode.ToString()));
                rep_Statement.SetParameterValue("branchName", opendMFDAO.GetBranchName(branchCode.ToString()).ToString());
                rep_Statement.SetParameterValue("branchCode", branchCode.ToString());

                //  CrystalReportViewer1.ReportSource = rep_Statement;
                rep_Statement.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "RepurchaseStatementDetails" + DateTime.Now + ".pdf");
            }
            else
            {
                rep_Statement_Summary.Refresh();
                rep_Statement_Summary.SetDataSource(dtReport);

                rep_Statement_Summary.SetParameterValue("fundName", opendMFDAO.GetFundName(fundCode.ToString()));
                rep_Statement_Summary.SetParameterValue("branchName", opendMFDAO.GetBranchName(branchCode.ToString()).ToString());
                rep_Statement_Summary.SetParameterValue("branchCode", branchCode.ToString());
                rep_Statement_Summary.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "RepurchaseStatementSummary" + DateTime.Now + ".pdf");
                //  CrystalReportViewer1.ReportSource = rep_Statement_Summary;
            }

           



        }
        else
        {
            Response.Write("No data found");
        }




    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        
        rep_Statement.Close();
        rep_Statement.Dispose();
        rep_Statement_Summary.Close();
        rep_Statement_Summary.Dispose();
    }
}
