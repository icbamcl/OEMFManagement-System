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

public partial class ReportViewer_UnitReportSendingDividendBankReportViewer : System.Web.UI.Page
{
    AMCL.REPORT.CR_SendingDividendBankLetter CR_DividendBank = new AMCL.REPORT.CR_SendingDividendBankLetter();

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
        string fundName = (string)Session["fundName"];
        string signatory = (string)Session["signatory"];
        string designation = (string)Session["designation"];
        DataTable dtDividend = (DataTable)Session["dtDividend"];



        if(dtDividend.Rows.Count>0)
        {

           dtDividend.TableName = "ReportLetter";

      //    dtDividend.WriteXmlSchema(@"D:\Project\Web\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\CR_SendingDividendBankLetter.xsd");

           CR_DividendBank.Refresh();
           CR_DividendBank.SetDataSource(dtDividend);
           
           CR_DividendBank.SetParameterValue("signatory", signatory);
           CR_DividendBank.SetParameterValue("designation", designation);
           CR_DividendBank.SetParameterValue("fundName", fundName);
           

           CrystalReportViewer1.ReportSource = CR_DividendBank;
                                          
        }
        else
        {
            Response.Write("No data found");
        }




    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_DividendBank.Close();
        CR_DividendBank.Dispose();
    }
}
