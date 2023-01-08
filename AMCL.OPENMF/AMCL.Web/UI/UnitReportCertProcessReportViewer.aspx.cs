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

public partial class ReportViewer_UnitReportCertProcessReportViewer : System.Web.UI.Page
{

    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
    AMCL.REPORT.CR_CertProcessr CR_Process = new AMCL.REPORT.CR_CertProcessr();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        StringBuilder MissingCertNo = new StringBuilder();
        MissingCertNo = (StringBuilder)Session["MissingCertNo"];
        string certType = (string)Session["certType"];
        

           // dtReportStatement.WriteXmlSchema(@"D:\Project\Web\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtPriceRefixation.xsd");
        

        CR_Process.SetParameterValue("Cert_Type", certType);
        CR_Process.SetParameterValue("Cert_No", MissingCertNo.ToString());

            CrystalReportViewer1.ReportSource = CR_Process;                                            
       




    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_Process.Close();
        CR_Process.Dispose();
    }
}
