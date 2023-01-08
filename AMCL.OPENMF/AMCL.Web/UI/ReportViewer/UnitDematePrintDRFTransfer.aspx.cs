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

public partial class ReportViewer_UnitDematePrintDRFTransfer : System.Web.UI.Page
{

    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    NumberToEnglish numberToEnglishObj = new NumberToEnglish();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
   
    AMCL.REPORT.CR_DemateDRF DemateDRF = new AMCL.REPORT.CR_DemateDRF();
    AMCL.REPORT.CR_DemateSALE_TR DemateSALE_TR = new AMCL.REPORT.CR_DemateSALE_TR();
    string CDSStatus = "";


    private ReportDocument rdoc = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {      
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        CDSStatus = bcContent.CDS.ToString().ToUpper();                                                   
        DataTable dtReportStatement = (DataTable)Session["dtReportStatement"];
        string printType = (string)Session["printType"];

        if (dtReportStatement.Rows.Count > 0)
        {
            if (printType.ToString() == "DRF")
            {
                DataTable dtReport = dtReportStatement.Clone();
                dtReport.Columns.Add("AMTWORD", typeof(string));
                DataRow drReport;

             
                for (int loop = 0; loop < dtReportStatement.Rows.Count; loop++)
                {

                    drReport = dtReport.NewRow();
                    drReport["FUND_CD"] = dtReportStatement.Rows[loop]["FUND_CD"];
                    drReport["FUND_NM"] = dtReportStatement.Rows[loop]["FUND_NM"];
                    drReport["SALE_BO_NAME"] = dtReportStatement.Rows[loop]["SALE_BO_NAME"];
                    drReport["SALE_OF_UNIT_BO"] = dtReportStatement.Rows[loop]["SALE_OF_UNIT_BO"];
                    drReport["ISIN_NO"] = dtReportStatement.Rows[loop]["ISIN_NO"];
                    drReport["REG_BK"] = dtReportStatement.Rows[loop]["REG_BK"];
                    drReport["DRF_REF_NO"] = dtReportStatement.Rows[loop]["DRF_REF_NO"];
                    drReport["DRF_REG_FOLIO_NO"] = dtReportStatement.Rows[loop]["DRF_REG_FOLIO_NO"];
                    drReport["DRF_CERT_NO"] = dtReportStatement.Rows[loop]["DRF_CERT_NO"];
                    drReport["DRF_DISTNCT_NO_FROM"] = dtReportStatement.Rows[loop]["DRF_DISTNCT_NO_FROM"];
                    drReport["DRF_DISTNCT_NO_TO"] = dtReportStatement.Rows[loop]["DRF_DISTNCT_NO_TO"];
                    drReport["QTY"] = dtReportStatement.Rows[loop]["QTY"];
                    drReport["AMTWORD"] = numberToEnglishObj.changeNumericToWords(Convert.ToDecimal(dtReportStatement.Rows[loop]["QTY"]).ToString().ToUpper());
                    dtReport.Rows.Add(drReport);


                }
                dtReport.TableName = "dtUnitDemateDRF";
                   // dtReport.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtUnitDemateDRF.xsd");


                 DemateDRF.Refresh();
                 DemateDRF.SetDataSource(dtReport);               
                CrystalReportViewer1.ReportSource = DemateDRF;
                }
                else if(printType.ToString() == "SL_TR")
                {
                DataTable dtReport = dtReportStatement.Clone();
                dtReport.Columns.Add("AMTWORD", typeof(string));
                DataRow drReport;


                for (int loop = 0; loop < dtReportStatement.Rows.Count; loop++)
                {

                   

                    drReport = dtReport.NewRow();
                    drReport["FUND_CD"] = dtReportStatement.Rows[loop]["FUND_CD"];
                    drReport["SL_NO"] = dtReportStatement.Rows[loop]["SL_NO"];
                    drReport["FUND_NM"] = dtReportStatement.Rows[loop]["FUND_NM"];
                    drReport["ISIN_NO"] = dtReportStatement.Rows[loop]["ISIN_NO"];
                    drReport["CUST_DP_NAME"] = dtReportStatement.Rows[loop]["CUST_DP_NAME"];
                    drReport["CUST_DP_ID"] = dtReportStatement.Rows[loop]["CUST_DP_ID"];
                    drReport["SALE_BO_NAME"] = dtReportStatement.Rows[loop]["SALE_BO_NAME"];
                    drReport["SALE_OF_UNIT_BO"] = dtReportStatement.Rows[loop]["SALE_OF_UNIT_BO"];
                    drReport["HOLDER_DP_NAME"] = dtReportStatement.Rows[loop]["HOLDER_DP_NAME"];
                    drReport["HOLDER_DP_ID"] = dtReportStatement.Rows[loop]["HOLDER_DP_ID"];
                    drReport["HOLDER_BO"] = dtReportStatement.Rows[loop]["HOLDER_BO"];
                    drReport["HNAME"] = dtReportStatement.Rows[loop]["HNAME"];
                    drReport["DRF_REF_NO"] = dtReportStatement.Rows[loop]["DRF_REF_NO"];
                    drReport["QTY"] = dtReportStatement.Rows[loop]["QTY"];
                    drReport["AMTWORD"] = numberToEnglishObj.changeNumericToWords(Convert.ToDecimal(dtReportStatement.Rows[loop]["QTY"]).ToString().ToUpper());
                    dtReport.Rows.Add(drReport);

                }
                dtReport.TableName = "dtUnitDemateSALE_TR";
                //     dtReport.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtUnitDemateSALE_TR.xsd");


                DemateSALE_TR.Refresh();
                DemateSALE_TR.SetDataSource(dtReport);               
                CrystalReportViewer1.ReportSource = DemateSALE_TR;
            }

            
        }

        else
        {
            Response.Write("No data found");
        }




    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        DemateDRF.Close();
        DemateDRF.Dispose();
        DemateSALE_TR.Close();
        DemateSALE_TR.Dispose();


    }
}
