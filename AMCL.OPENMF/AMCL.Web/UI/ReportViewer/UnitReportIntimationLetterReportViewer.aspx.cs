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

public partial class ReportViewer_UnitReportIntimationLetterReportViewer : System.Web.UI.Page
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
    AMCL.REPORT.CR_UnitIntimationLetter CR_IntimationLetter = new AMCL.REPORT.CR_UnitIntimationLetter();

    
    

    protected void Page_Load(object sender, EventArgs e)
    {

        string FYPart = "";       
        string fundCode = "";       
        string interim = "";
        string ended = "";
        
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        string fundName = (string)Session["fundName"];
        string BEFTN_Issue_Date = (string)Session["BEFTN_Issue_Date"];
        fundCode = (string)Session["fundCode"];
        DataTable dtDividendInformation = (DataTable)Session["dtDividendInformation"];
        DataTable dtDiviParaInformation = (DataTable)Session["dtDiviParaInformation"];

         if (string.Compare(fundCode, "IAMPH", true) == 0)
            {
                FYPart = dtDiviParaInformation.Rows[0]["FY_PART"].Equals(DBNull.Value) ? "" : dtDiviParaInformation.Rows[0]["FY_PART"].ToString();
                 FYPart = "(" + FYPart.ToString() + ")";
            }
           interim = dtDiviParaInformation.Rows[0]["FY_PART"].ToString();
            if (interim.ToString().ToUpper() == "INTERIM")
            {
                ended = "";
            }
            else if (interim.ToString().ToUpper() == "FINAL")
            {
                ended = "ended";
            }
            else
            {
                interim = "";
                ended = "ended";
            }

            dtDividendInformation.TableName = "dtIntimationLetter";

        //dtDividendInformation.WriteXmlSchema(@"D:\Project\Web\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtIntimationLetter.xsd");

        CR_IntimationLetter.Refresh();
        CR_IntimationLetter.SetDataSource(dtDividendInformation);
        CR_IntimationLetter.SetParameterValue("fundName", fundName);
        //CR_IntimationLetter.SetParameterValue("FY", dtDiviParaInformation.Rows[0]["F_YEAR"].ToString());
        CR_IntimationLetter.SetParameterValue("FYPart", FYPart.ToString());
        CR_IntimationLetter.SetParameterValue("ended", ended.ToString());
        CR_IntimationLetter.SetParameterValue("interim", interim.ToString());
        CR_IntimationLetter.SetParameterValue("Beftn_Issue_Date", BEFTN_Issue_Date.ToString());
        CR_IntimationLetter.SetParameterValue("fund_code", fundCode.ToString());
        CR_IntimationLetter.SetParameterValue("CLOSE_DT", Convert.ToDateTime(dtDiviParaInformation.Rows[0]["CLOSE_DT"].ToString()).ToString("dd-MMM-yyyy"));
        CR_IntimationLetter.SetParameterValue("DIVI_NO", Convert.ToInt32(dtDiviParaInformation.Rows[0]["DIVI_NO"].ToString()));
        CR_IntimationLetter.SetParameterValue("DIVI_RATE", Convert.ToDecimal(dtDiviParaInformation.Rows[0]["RATE"].ToString()));
        CR_IntimationLetter.SetParameterValue("CIP_RATE", Convert.ToDecimal(dtDiviParaInformation.Rows[0]["CIP_RATE"].ToString()));


        CrystalReportViewer1.ReportSource = CR_IntimationLetter;

    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_IntimationLetter.Close();
        CR_IntimationLetter.Dispose();
    }
}
