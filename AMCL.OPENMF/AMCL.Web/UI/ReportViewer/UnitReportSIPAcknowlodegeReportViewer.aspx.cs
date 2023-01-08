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

public partial class ReportViewer_UnitReportSIPAcknowlodegeReportViewer : System.Web.UI.Page
{

    
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    NumberToEnglish numberToEnglisObj = new NumberToEnglish();
    NumberToEnglishUSD numberToEnglisUSDObj = new NumberToEnglishUSD();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAOObj = new OMFDAO();
    BaseClass bcContent = new BaseClass();
    UnitRepurchaseBL unitRepBLObj = new UnitRepurchaseBL();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    
    AMCL.REPORT.CR_SIPAckReceipt CR_SIPAcknowledge = new AMCL.REPORT.CR_SIPAckReceipt();
    AMCL.REPORT.CR_SIPStatement CR_SIPStatement = new AMCL.REPORT.CR_SIPStatement();
    AMCL.REPORT.CR_SIPIndividualSchedule CR_SIPIndividualSchedule = new AMCL.REPORT.CR_SIPIndividualSchedule();
    AMCL.REPORT.CR_SIPAutoDebitForm CR_SIPAutoDebitForm = new AMCL.REPORT.CR_SIPAutoDebitForm();
    AMCL.REPORT.CR_SIPStatementAfterSale CR_SIPStatementAfterSale = new AMCL.REPORT.CR_SIPStatementAfterSale();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        DataTable dtSIPMaster = (DataTable)Session["dtSIPMaster"];
        string reportType = (string)Session["reportType"];
        string sipNo = (string)Session["SIPNo"];



        if (dtSIPMaster.Rows.Count > 0)
        {
            if (reportType.ToUpper() == "ACKSINGLE")
            {
                
                CR_SIPAcknowledge.Refresh();
                CR_SIPAcknowledge.SetDataSource(dtSIPMaster);
                CR_SIPAcknowledge.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "SIPAcknowledgeReceipt_" + bcContent.FundCode.ToString() + "_" + sipNo + ".pdf");
            }
            else if (reportType.ToUpper() == "ACK_STATE")
            {
                
                CR_SIPAcknowledge.Refresh();
                CR_SIPAcknowledge.SetDataSource(dtSIPMaster);
                CR_SIPAcknowledge.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "SIPAcknowledgeStatement_" + bcContent.FundCode.ToString() + ".pdf");
            }
            else if(reportType.ToUpper() == "SIP_STATE")
            {

                CR_SIPStatement.Refresh();
                CR_SIPStatement.SetDataSource(dtSIPMaster);
                CR_SIPStatement.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "SIPStatement_" + bcContent.FundCode.ToString() + ".pdf");
            }
            else if (reportType.ToUpper() == "SIP_SCHEDULE")
            {
                dtSIPMaster.TableName = "dtSIPDetails";
                // dtSIPMaster.WriteXmlSchema(@"F:\GITHUB_PROJECT\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtSIPDetails.xsd");
                CR_SIPIndividualSchedule.Refresh();
                CR_SIPIndividualSchedule.SetDataSource(dtSIPMaster);
                CR_SIPIndividualSchedule.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "SIPInstallmentStatement_" + bcContent.FundCode.ToString() + ".pdf");
            }
            else if (reportType.ToUpper() == "FORM_STATE")
            {
                dtSIPMaster.TableName = "dtSIPDetails";
                // dtSIPMaster.WriteXmlSchema(@"F:\GITHUB_PROJECT\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtSIPDetails.xsd");
                CR_SIPAutoDebitForm.Refresh();
                CR_SIPAutoDebitForm.SetDataSource(dtSIPMaster);
                CR_SIPAutoDebitForm.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "SIPDebitForm_" + bcContent.FundCode.ToString() + ".pdf");
            }
            else if (reportType.ToUpper() == "SIP_SALE_STATE")
            {
                dtSIPMaster.TableName = "dtSIPDetails";
                // dtSIPMaster.WriteXmlSchema(@"F:\GITHUB_PROJECT\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtSIPDetails.xsd");
                CR_SIPStatementAfterSale.Refresh();
                CR_SIPStatementAfterSale.SetDataSource(dtSIPMaster);
                CR_SIPStatementAfterSale.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "SIPState_After_Sale_" + bcContent.FundCode.ToString() + ".pdf");
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_SIPAcknowledge.Close();
        CR_SIPAcknowledge.Dispose();
    }
}
