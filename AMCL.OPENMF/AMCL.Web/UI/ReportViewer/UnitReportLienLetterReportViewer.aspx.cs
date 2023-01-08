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

public partial class ReportViewer_UnitReportLienLetterReportViewer : System.Web.UI.Page
{
    AMCL.REPORT.CR_LienLetter CR_Lien = new AMCL.REPORT.CR_LienLetter();

    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
    

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
        int lienNo = (int)Session["lienNo"];
        int regNo = (int)Session["regNo"];
        DateTime lienReqDate = Convert.ToDateTime((string)Session["lienReqDate"]);
        string lienReqRef = (string)Session["lienReqRef"];
        string lienInstitution = (string)Session["lienInstitution"];
        string lienInstitutionBranch = (string)Session["lienInstitutionBranch"];
        string holderName = (string)Session["holderName"];
        string jHolderName = (string)Session["jHolderName"];
        string toName = (string)Session["toName"];
        string division = (string)Session["division"];
        string address1 = (string)Session["address1"];
        string address2 = (string)Session["address2"];
        string address3 = (string)Session["address3"];
        string signatory = (string)Session["signatory"];
        string designation = (string)Session["designation"];



        DataTable dtReport = new DataTable();
        dtReport.Columns.Add("SL_TR_NO", typeof(string));
        dtReport.Columns.Add("CERT_NO", typeof(string));
        dtReport.Columns.Add("QTY", typeof(int));

        dtReport.TableName = "ReportLienLetter";
        DataRow drReport;

        DataTable dtReportStatement = commonGatewayObj.Select("SELECT SL_TR_NO,QTY FROM LIEN_MARK WHERE VALID IS NULL AND LIEN_NO=" + lienNo + " AND REG_BR='" + branchCode.ToString() + "' AND REG_BK='" + fundCode.ToString() + "' AND REG_NO="+regNo);

        if (dtReportStatement.Rows.Count > 0)
        {
            string saleNo = "";
            int totalUnits = 0;

            for (int looper = 0; looper < dtReportStatement.Rows.Count; looper++)
            {
                drReport = dtReport.NewRow();
                drReport["SL_TR_NO"] = dtReportStatement.Rows[looper]["SL_TR_NO"].Equals(DBNull.Value) ? "0" : dtReportStatement.Rows[looper]["SL_TR_NO"].ToString();
                saleNo = dtReportStatement.Rows[looper]["SL_TR_NO"].ToString();
                drReport["CERT_NO"] = reportObj.getTotalCertNo("SELECT * FROM LIEN_MARK_CERT_NO WHERE SL_TR_NO='" + saleNo + "' AND REG_BK='" + fundCode.ToString() + "'AND REG_BR='" + branchCode.ToString() + "' AND REG_NO=" + regNo + " AND LIEN_NO=" + lienNo, fundCode.ToString()).ToString();                             
                drReport["QTY"] =Convert.ToInt32( dtReportStatement.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0": dtReportStatement.Rows[looper]["QTY"].ToString());               
                dtReport.Rows.Add(drReport);
                totalUnits = totalUnits + Convert.ToInt32(dtReportStatement.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0" : dtReportStatement.Rows[looper]["QTY"].ToString());               
            }


          // dtReport.WriteXmlSchema(@"D:\Project\Web\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\ReportLienLetter.xsd");

           CR_Lien.Refresh();
           CR_Lien.SetDataSource(dtReport);
           CR_Lien.SetParameterValue("fudnName", opendMFDAO.GetFundName(fundCode.ToString()));
           CR_Lien.SetParameterValue("lienNo", lienNo);
           CR_Lien.SetParameterValue("regNo", regNo);
           CR_Lien.SetParameterValue("lienReqDate", lienReqDate);
           CR_Lien.SetParameterValue("lienReqRef", lienReqRef);
           CR_Lien.SetParameterValue("lienInstitution", lienInstitution);
           CR_Lien.SetParameterValue("lienInstitutionBranch", lienInstitutionBranch);
           CR_Lien.SetParameterValue("holderName", holderName);
           CR_Lien.SetParameterValue("jHolderName", jHolderName);
           CR_Lien.SetParameterValue("toName", toName);
           CR_Lien.SetParameterValue("division", division);
           CR_Lien.SetParameterValue("address1", address1);
           CR_Lien.SetParameterValue("address2", address2);
           CR_Lien.SetParameterValue("address3", address3);
           CR_Lien.SetParameterValue("signatory", signatory);
           CR_Lien.SetParameterValue("designation", designation);
           CR_Lien.SetParameterValue("fundCode", fundCode);
           CR_Lien.SetParameterValue("branchCode", branchCode);
           CR_Lien.SetParameterValue("totalUnits", totalUnits);
         //   rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            CR_Lien.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            //  CrystalReportViewer1.ReportSource = CR_Lien;

        }
        else
        {
            Response.Write("No data found");
        }




    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_Lien.Close();
        CR_Lien.Dispose();
    }
}
