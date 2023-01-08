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

public partial class ReportViewer_UnitReportPriceRefixationReportViewer : System.Web.UI.Page
{

    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
    AMCL.REPORT.CR_PriceRefixationAllFundLast CR_PRICE = new AMCL.REPORT.CR_PriceRefixationAllFundLast();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];
        
        DataTable dtReportStatement = (DataTable)Session["dtPriceInfo"];
        string fundCode = (string)Session["fundCode"];
        if (dtReportStatement.Rows.Count > 0)
        {

           // dtReportStatement.WriteXmlSchema(@"D:\Project\Web\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtPriceRefixation.xsd");
            CR_PRICE.Refresh();
            CR_PRICE.SetDataSource(dtReportStatement);
            CR_PRICE.SetParameterValue("fudnName", opendMFDAO.GetFundName(fundCode.ToString()));
            CrystalReportViewer1.ReportSource = CR_PRICE;                                            
        }
        else
        {
            Response.Write("No data found");
        }




    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_PRICE.Close();
        CR_PRICE.Dispose();
      

    }
}
