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

public partial class ReportViewer_UnitReportTaxCertReportViewer : System.Web.UI.Page
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
    dividendDAO diviDAOObj = new dividendDAO();
   
    private ReportDocument rdoc = new ReportDocument();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        string FYPart = "";
        string CertType = "";
        string fundCode = "";
        string branchCode = "";
        string interim = "";
        string ended = "";
        
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        CertType = (string)Session["CertType"];
        fundCode = (string)Session["FundCode"];
        branchCode = (string)Session["branchCode"];
     

        


        if (string.Compare(CertType, "IncomeTaxCert", true) == 0)//for Income Tax Report
        {

            DataTable dtIncomeTax = (DataTable)Session["dtIncomeTax"];
            string FY = (string)Session["FY"];
            int REG_NO = (int)Session["REG_NO"];
            dtIncomeTax.TableName = "IncomeTax";
            if (string.Compare(fundCode, "IAMPH", true) == 0)
            {
                 FYPart= dtIncomeTax.Rows[0]["FY_PART"].Equals(DBNull.Value) ? "" : dtIncomeTax.Rows[0]["FY_PART"].ToString();
                 FYPart = "(" + FYPart.ToString() + ")";
            }
            interim = dtIncomeTax.Rows[0]["FY_PART"].ToString();
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
         //dtIncomeTax.WriteXmlSchema(@"D:\Project\Web\AMCL.OPENMF\AMCL.Web\UI\ReportViewer\Report\dtReportIncomeTax.xsd");


            DataTable dtTaxCal = diviDAOObj.dtDividendInfo(REG_NO,fundCode,branchCode,FY);
            ReportDocument rdoc = new ReportDocument();

          
                rdoc = new ReportDocument();
                string Path = Server.MapPath("Report/rptIncomeTax.rpt");
                rdoc.Load(Path);
                rdoc.Refresh();
                rdoc.SetDataSource(dtIncomeTax);
                CrystalReportViewer1.ReportSource = rdoc;

                rdoc.SetParameterValue("fundCode", fundCode);
                rdoc.SetParameterValue("branchCode", branchCode);
                rdoc.SetParameterValue("FYPart", FYPart.ToString());
                rdoc.SetParameterValue("Interim", interim.ToString());
                rdoc.SetParameterValue("ended", ended.ToString());
                rdoc = ReportFactory.GetReport(rdoc.GetType());
            //}
        }
        else if (string.Compare(CertType, "SolventCert", true) == 0)//for Solvent 
        {
            opendMFDAOObj = new OMFDAO();
            DataTable dtInvestCertHolderInfo = (DataTable)Session["dtInvestCertHolderInfo"];          
            decimal totalUnitHolding = (decimal)Session["totalUnitHolding"];
            decimal USDRate =Convert.ToDecimal( (string)Session["USDRate"]);
            decimal repRate = Convert.ToDecimal((string)Session["RepRate"]);
            string TTDate=(string)Session["TTDate"];
            string balanceDate = (string)Session["balanceDate"];

            decimal equivalentInvestValue = totalUnitHolding * repRate;
            equivalentInvestValue = decimal.Round(equivalentInvestValue / USDRate, 2);
            
            decimal equivalentMarketValue = totalUnitHolding * repRate;
            equivalentMarketValue = decimal.Round(equivalentMarketValue, 2);
           
          

            // dtInvestSaleInfo.WriteXmlSchema(@"D:\Project\Web\AMCL.OPENMF\AMCL.Web\UI\ReportViewer\Report\rptSolventCert.xsd");

            
            string Path = Server.MapPath("Report/rptSolventCert.rpt");
            rdoc.Load(Path);
            rdoc.Refresh();
            rdoc.SetDataSource(dtInvestCertHolderInfo);
            CrystalReportViewer1.ReportSource = rdoc;           
            rdoc.SetParameterValue("fundCode", fundCode);
            rdoc.SetParameterValue("branchCode", branchCode);
            rdoc.SetParameterValue("HNAME", dtInvestCertHolderInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["HNAME"].ToString());
            rdoc.SetParameterValue("JNT_NAME", dtInvestCertHolderInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["JNT_NAME"].ToString());
            rdoc.SetParameterValue("ADDRESS1", dtInvestCertHolderInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["ADDRS1"].ToString());
            rdoc.SetParameterValue("ADDRESS2", dtInvestCertHolderInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["ADDRS2"].ToString());
            rdoc.SetParameterValue("CITY", dtInvestCertHolderInfo.Rows[0]["CITY"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["CITY"].ToString());
            rdoc.SetParameterValue("FUND_NAME", dtInvestCertHolderInfo.Rows[0]["FUND_NM"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["FUND_NM"].ToString());
            rdoc.SetParameterValue("Reg_No", dtInvestCertHolderInfo.Rows[0]["REG_NUM"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["REG_NUM"].ToString());
           
            rdoc.SetParameterValue("TotalUnit",totalUnitHolding);
            
            rdoc.SetParameterValue("equivalentInvestValue", equivalentInvestValue);
            rdoc.SetParameterValue("equivalentInvestValueinWord", numberToEnglisUSDObj.changeNumericToWords(equivalentInvestValue));

            rdoc.SetParameterValue("equivalentMarketValue", equivalentMarketValue);
            rdoc.SetParameterValue("equivalentMarketValueinWord", numberToEnglisUSDObj.changeNumericToWords(equivalentMarketValue));

            rdoc.SetParameterValue("TTDate",Convert.ToDateTime(TTDate).ToString("dd-MMM-yyyy"));
            rdoc.SetParameterValue("balanceDate", balanceDate);

            rdoc = ReportFactory.GetReport(rdoc.GetType());

        }
        else if (string.Compare(CertType, "SurrendertCert", true) == 0)// for Surrender
        {
            DataTable dtInvestCertHolderInfo = (DataTable)Session["dtInvestCertHolderInfo"];
            DataTable dtLadgerForReport = (DataTable)Session["dtLadgerForReport"];
            DataTable dtSurrender = (DataTable)Session["dtSurrender"];
            dtLadgerForReport.TableName = "dtLadgerForReport";
            dtSurrender.TableName = "dtSurrender";
            dtInvestCertHolderInfo.TableName = "dtInvestCertHolderInfo";

           //  dtLadgerForReport.WriteXmlSchema(@"F:\GITHUB_PROJECT\DOTNET2015\AMCL.OPENMF\AMCL.Web\UI\ReportViewer\Report\rptSurrenderCert.xsd");

            ReportDocument rdoc = new ReportDocument();
            string Path = Server.MapPath("Report/rptSurrenderCert.rpt");
            rdoc.Load(Path);
            rdoc.Refresh();
            rdoc.SetDataSource(dtLadgerForReport);
            CrystalReportViewer1.ReportSource = rdoc;

            rdoc.SetParameterValue("branchCode", branchCode);
            rdoc.SetParameterValue("fundCode", fundCode);
            rdoc.SetParameterValue("HNAME", dtInvestCertHolderInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["HNAME"].ToString());
            rdoc.SetParameterValue("ADDRESS1", dtInvestCertHolderInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["ADDRS1"].ToString());
            rdoc.SetParameterValue("ADDRESS2", dtInvestCertHolderInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["ADDRS2"].ToString());
            rdoc.SetParameterValue("CITY", dtInvestCertHolderInfo.Rows[0]["CITY"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["CITY"].ToString());
            rdoc.SetParameterValue("FUND_NAME", dtInvestCertHolderInfo.Rows[0]["FUND_NM"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["FUND_NM"].ToString());
            rdoc.SetParameterValue("TotalBalance", (int)Session["totalBalance"]);
            rdoc.SetParameterValue("outBalance", (int)Session["outBalance"]);
            rdoc.SetParameterValue("inBalance", (int)Session["inBalance"]);
            rdoc.SetParameterValue("sur_Unit",Convert.ToInt32( dtSurrender.Rows[0]["REP_UNIT"].ToString()));
            rdoc.SetParameterValue("Sur_Date", dtSurrender.Rows[0]["REP_DATE"].ToString());
            rdoc.SetParameterValue("sur_Amount",Convert.ToDecimal( dtSurrender.Rows[0]["AMOUNT"].ToString()));
            rdoc.SetParameterValue("inWord",numberToEnglisObj.changeNumericToWords( Convert.ToDecimal(dtSurrender.Rows[0]["AMOUNT"].ToString())));
            rdoc.SetParameterValue("sur_Rate",Convert.ToDecimal( dtSurrender.Rows[0]["RATE"].ToString()));
            rdoc.SetParameterValue("repAmount", (decimal)Session["repAmount"]);
            rdoc.SetParameterValue("saleAmount", (decimal)Session["saleAmount"]);
            rdoc.SetParameterValue("ledgerAmount", (decimal)Session["ledgerAmount"]);
            rdoc.SetParameterValue("asOnDate", Convert.ToDateTime((string)Session["asOnDate"]));
            rdoc = ReportFactory.GetReport(rdoc.GetType());
           
        }
        else if (string.Compare(CertType, "InvestCert", true) == 0)// for Invest
        {
            DataTable dtInvestCertHolderInfo = (DataTable)Session["dtInvestCertHolderInfo"];
            DataTable dtInvestSaleInfo = (DataTable)Session["dtInvestSaleInfo"];
            DataTable dtInvestTotal = (DataTable)Session["dtInvestTotal"];
            string FY = (string)Session["FY"];
            string CloseDate = (string)Session["CloseDate"];
            string investmentType = (string)Session["investmentType"];

          //   dtInvestSaleInfo.WriteXmlSchema(@"D:\Project\Web\AMCL.OPENMF\AMCL.Web\UI\ReportViewer\Report\rptInvestCert.xsd");

            rdoc = new ReportDocument();
            string Path = "";
            if(investmentType=="CIP")
            {
                Path = Server.MapPath("Report/rptInvestCertCIP.rpt");
            }
            else
            {
             Path = Server.MapPath("Report/rptInvestCert.rpt");
            }
            rdoc.Load(Path);
            rdoc.Refresh();
            rdoc.SetDataSource(dtInvestSaleInfo);
            CrystalReportViewer1.ReportSource = rdoc;

            rdoc.SetParameterValue("branchCode", branchCode);
            rdoc.SetParameterValue("fundCode", fundCode);
            rdoc.SetParameterValue("FY", FY.ToString());
            rdoc.SetParameterValue("close_dt", CloseDate.ToString());
         
            rdoc.SetParameterValue("HNAME", dtInvestCertHolderInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["HNAME"].ToString());
            rdoc.SetParameterValue("JHOLDER", dtInvestCertHolderInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["JNT_NAME"].ToString());
            rdoc.SetParameterValue("ADDRESS1", dtInvestCertHolderInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["ADDRS1"].ToString());
            rdoc.SetParameterValue("ADDRESS2", dtInvestCertHolderInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["ADDRS2"].ToString());
            rdoc.SetParameterValue("CITY", dtInvestCertHolderInfo.Rows[0]["CITY"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["CITY"].ToString());
            rdoc.SetParameterValue("FUND_NAME", dtInvestCertHolderInfo.Rows[0]["FUND_NM"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[0]["FUND_NM"].ToString());
            rdoc.SetParameterValue("TotalAmt", Convert.ToDecimal(dtInvestTotal.Rows[0]["TOTAL_AMOUNT"].Equals(DBNull.Value) ? "0" : dtInvestTotal.Rows[0]["TOTAL_AMOUNT"].ToString()));

            
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else 
        {
            Response.Write("No Data Found");
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
