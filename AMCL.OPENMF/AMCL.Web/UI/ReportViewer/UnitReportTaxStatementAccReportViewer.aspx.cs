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

public partial class ReportViewer_UnitReportTaxStatementAccReportViewer : System.Web.UI.Page
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

    AMCL.REPORT.CR_UnitTaxStatementAcc CR_TaxStatement = new AMCL.REPORT.CR_UnitTaxStatementAcc();
    AMCL.REPORT.CR_UnitDividendTaxDeductList CR_TaxList = new AMCL.REPORT.CR_UnitDividendTaxDeductList();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        
      
        
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        string fundName = (string)Session["fundName"];
        string statementType = (string)Session["statementType"];
        DataTable dtDividendInformation = (DataTable)Session["dtDividendInformation"];
        DataTable dtDiviParaInformation = (DataTable)Session["dtDiviParaInformation"];

        dtDividendInformation.TableName = "dtTaxStatementAcc";

       // dtDividendInformation.WriteXmlSchema(@"D:\Project\Web\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtTaxStatementAcc.xsd");

       

        if (statementType.ToString().ToUpper() == "SAMMARY")
        {
            CR_TaxStatement.Refresh();
            CR_TaxStatement.SetDataSource(dtDividendInformation);
            CR_TaxStatement.SetParameterValue("fundName", fundName);
            CR_TaxStatement.SetParameterValue("FY", dtDiviParaInformation.Rows[0]["F_YEAR"].ToString());
            CR_TaxStatement.SetParameterValue("FY_PART", dtDiviParaInformation.Rows[0]["FY_PART"].ToString());
            CR_TaxStatement.SetParameterValue("CLOSE_DT", Convert.ToDateTime(dtDiviParaInformation.Rows[0]["CLOSE_DT"].ToString()).ToString("dd-MMM-yyyy"));
            CR_TaxStatement.SetParameterValue("DIVI_NO", Convert.ToInt32(dtDiviParaInformation.Rows[0]["DIVI_NO"].ToString()));
            CR_TaxStatement.SetParameterValue("DIVI_RATE", Convert.ToDecimal(dtDiviParaInformation.Rows[0]["RATE"].ToString()));
            CR_TaxStatement.SetParameterValue("CIP_RATE", Convert.ToDecimal(dtDiviParaInformation.Rows[0]["CIP_RATE"].ToString()));
            CrystalReportViewer1.ReportSource = CR_TaxStatement;
        }
        else
        {
            CR_TaxList.Refresh();
            CR_TaxList.SetDataSource(dtDividendInformation);
           
            CrystalReportViewer1.ReportSource = CR_TaxList;
        }

    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_TaxStatement.Close();
        CR_TaxStatement.Dispose();
        CR_TaxList.Close();
        CR_TaxList.Dispose();
    }
}
