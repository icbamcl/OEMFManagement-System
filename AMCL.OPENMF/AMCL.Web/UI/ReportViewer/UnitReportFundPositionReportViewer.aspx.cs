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

public partial class ReportViewer_UnitReportFundPositionReportViewer : System.Web.UI.Page
{

    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
    AMCL.REPORT.CR_UnitFundPosition unitFundPostion = new AMCL.REPORT.CR_UnitFundPosition();
    AMCL.REPORT.CR_UnitFundPositionHolder unitFundPostionHolder = new AMCL.REPORT.CR_UnitFundPositionHolder();
    AMCL.REPORT.CR_UnitFundPositionSummary unitFundPostionSummary = new AMCL.REPORT.CR_UnitFundPositionSummary();



    private ReportDocument rdoc = new ReportDocument();

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
            
            
        DataTable dtUnitFundPosition = (DataTable)Session["dtUnitFundPosition"];
        string FUND_NAME = (string)Session["FUND_NAME"];
        string BRANCH_NAME = (string)Session["BRANCH_NAME"];
        string DATE_FROM = (string)Session["DATE_FROM"];
        string DATE_TO = (string)Session["DATE_TO"];
        string ASONDATE = (string)Session["ASONDATE"];
        string reportType = (string)Session["reportType"];

        if (dtUnitFundPosition.Rows.Count>0)
        {
            if (reportType.ToUpper() == "POSITION")
            {
                dtUnitFundPosition.TableName = "dtUnitFundPosition";
                //dtUnitFundPosition.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtUnitFundPosition.xsd");

                unitFundPostion.Refresh();
                unitFundPostion.SetDataSource(dtUnitFundPosition);

                unitFundPostion.SetParameterValue("FUND_NAME", FUND_NAME);
                unitFundPostion.SetParameterValue("BRANCH_NAME", BRANCH_NAME);
                unitFundPostion.SetParameterValue("DATE_FROM", DATE_FROM);
                unitFundPostion.SetParameterValue("DATE_TO", DATE_TO);
                CrystalReportViewer1.ReportSource = unitFundPostion;
            }
            else if (reportType.ToUpper() == "HOLDER")
            {
                dtUnitFundPosition.TableName = "dtUnitFundPositionHolder";
                // dtUnitFundPosition.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtUnitFundPositionHolder.xsd");

                unitFundPostionHolder.Refresh();
                unitFundPostionHolder.SetDataSource(dtUnitFundPosition);

                unitFundPostionHolder.SetParameterValue("FUND_NAME", FUND_NAME);
                unitFundPostionHolder.SetParameterValue("BRANCH_NAME", BRANCH_NAME);
                unitFundPostionHolder.SetParameterValue("ASONDATE", ASONDATE);
                
                CrystalReportViewer1.ReportSource = unitFundPostionHolder;
            }
            else if (reportType.ToUpper() == "SUMMARY")
            {
                dtUnitFundPosition.TableName = "dtUnitFundPositionSummary";
                 //dtUnitFundPosition.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtUnitFundPositionSummary.xsd");

                unitFundPostionSummary.Refresh();
                unitFundPostionSummary.SetDataSource(dtUnitFundPosition);

                unitFundPostionSummary.SetParameterValue("FUND_NAME", FUND_NAME);
                unitFundPostionSummary.SetParameterValue("BRANCH_NAME", BRANCH_NAME);
                unitFundPostionSummary.SetParameterValue("ASONDATE", ASONDATE);
               
                CrystalReportViewer1.ReportSource = unitFundPostionSummary;
            }
            else
            {
                Response.Write("No data found");
            }


        }
        else
        {
            Response.Write("No data found");
        }




    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        unitFundPostion.Close();
        unitFundPostion.Dispose();
        unitFundPostionHolder.Close();
        unitFundPostionHolder.Dispose();
        unitFundPostionSummary.Close();
        unitFundPostionSummary.Dispose();


    }
}
