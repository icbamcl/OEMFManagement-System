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

public partial class ReportViewer_UnitReportTransferReportViewer : System.Web.UI.Page
{

    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
    ReportDocument rdoc = new ReportDocument();

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
           string tr_type = (string)Session["TR_TYPE"];

        DataTable dtReport = reportObj.getDtForReportStatement();
        dtReport.TableName = "ReportStatement";
        DataRow drReport;

        DataTable dtReportStatement = (DataTable)Session["dtReportStatement"];

        if (dtReportStatement.Rows.Count > 0)
        {
            int transferNo = 0;


            for (int looper = 0; looper < dtReportStatement.Rows.Count; looper++)
            {
                drReport = dtReport.NewRow();
                drReport["TR_NO"] = Convert.ToInt32(dtReportStatement.Rows[looper]["TR_NO"].ToString());
                drReport["TR_DT"] = dtReportStatement.Rows[looper]["TR_DT"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["TR_DT"].ToString();
                drReport["OLD_SL_TR_NO"] = dtReportStatement.Rows[looper]["OLD_SL_TR_NO"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["OLD_SL_TR_NO"].ToString();
                drReport["HNAME"] = dtReportStatement.Rows[looper]["HNAME"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["HNAME"].ToString();               
                drReport["ADDRS1"] = dtReportStatement.Rows[looper]["ADDRS1"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["ADDRS1"].ToString();
                drReport["ADDRS2"] = dtReportStatement.Rows[looper]["ADDRS2"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["ADDRS2"].ToString();
                drReport["CITY"] = dtReportStatement.Rows[looper]["CITY"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["CITY"].ToString();
                drReport["REG_NO"] = dtReportStatement.Rows[looper]["REG_NO"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["REG_NO"].ToString();

                transferNo = Convert.ToInt32(dtReportStatement.Rows[looper]["TR_NO"].ToString());
                if (!dtReportStatement.Rows[looper]["OLD_SL_TR_NO"].Equals(DBNull.Value))
                {
                    drReport["CERT_NO"] = reportObj.getTotalCertNo("SELECT NVL(CERT_TYPE,' ') AS CERT_TYPE, NVL(CERT_NO,0) AS CERT_NO FROM TRANS_CERT WHERE TR_NO=" + transferNo + " AND OLD_SL_TR_NO='" + dtReportStatement.Rows[looper]["OLD_SL_TR_NO"] + "' AND F_CD='" + fundCode.ToString() + "' AND BR_CODE='" + fundCode.ToString() + "_" + branchCode.ToString().ToUpper() + "'", fundCode.ToString()).ToString();
                }
                else
                {
                    drReport["CERT_NO"] = reportObj.getTotalCertNo("SELECT NVL(CERT_TYPE,' ') AS CERT_TYPE, NVL(CERT_NO,0) AS CERT_NO FROM TRANS_CERT WHERE TR_NO=" + transferNo + "  AND F_CD='" + fundCode.ToString() + "' AND BR_CODE='" + fundCode.ToString() + "_" + branchCode.ToString().ToUpper() + "'", fundCode.ToString()).ToString();
                }



               
                drReport["QTY"] =Convert.ToInt32( dtReportStatement.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0": dtReportStatement.Rows[looper]["QTY"].ToString());
               
               
                drReport["TFEREE_NAME"] = dtReportStatement.Rows[looper]["TFEREE_NAME"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["TFEREE_NAME"].ToString();
                drReport["TFEREE_ADDRS1"] = dtReportStatement.Rows[looper]["TFEREE_ADDRS1"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["TFEREE_ADDRS1"].ToString();
                drReport["TFEREE_ADDRS2"] = dtReportStatement.Rows[looper]["TFEREE_ADDRS2"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["TFEREE_ADDRS2"].ToString();
                drReport["TFEREE_CITY"] = dtReportStatement.Rows[looper]["TFEREE_CITY"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["TFEREE_CITY"].ToString();
                drReport["TFEREE_REG_NO"] = dtReportStatement.Rows[looper]["TFEREE_REG_NO"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["TFEREE_REG_NO"].ToString();
                drReport["CIP"] = dtReportStatement.Rows[looper]["CIP"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["CIP"].ToString();

              dtReport.Rows.Add(drReport);
            }

          // dtReport.WriteXmlSchema(@"D:\Project\Web\AMCL.OpenEndMF\UI\ReportViewer\Report\dtUnitReportForStatement.xsd");

            
            string Path = Server.MapPath("Report/rptTransStatement.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReport);
            CrystalReportViewer1.ReportSource = rdoc;
            rdoc.SetParameterValue("fundName", opendMFDAO.GetFundName(fundCode.ToString()));
            rdoc.SetParameterValue("branchName", opendMFDAO.GetBranchName(branchCode.ToString()).ToString());
            rdoc.SetParameterValue("branchCode", branchCode.ToString());
            if(tr_type=="T")
            {
                rdoc.SetParameterValue("tr_type", "Transfer");
            }
            else
            {
                rdoc.SetParameterValue("tr_type", "Death Transmission");
            }
           
           

        }
        else
        {
            Response.Write("No data found");
        }




    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CrystalReportViewer1.Dispose();
        CrystalReportViewer1 = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
        
        
    }
}
