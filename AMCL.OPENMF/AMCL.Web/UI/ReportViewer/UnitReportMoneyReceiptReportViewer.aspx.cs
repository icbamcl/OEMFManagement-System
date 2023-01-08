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

public partial class ReportViewer_UnitReportMoneyReceiptReportViewer : System.Web.UI.Page
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
    AMCL.REPORT.CR_MoneyReceiptSale CR_MoneyReceiptSale = new AMCL.REPORT.CR_MoneyReceiptSale();
    AMCL.REPORT.CR_MoneyReceiptRepurchase CR_MoneyReceiptRep = new AMCL.REPORT.CR_MoneyReceiptRepurchase();






    protected void Page_Load(object sender, EventArgs e)
    {

             
 
       
        
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
       

        string MONEY_RECEIPT_ID = (string)Session["MONEY_RECEIPT_ID"];
        string MONEY_RECEIPT_TYPE = (string)Session["MONEY_RECEIPT_TYPE"];
        if (MONEY_RECEIPT_TYPE == "SL")
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT  FUND_INFO.FUND_NM, FUND_INFO.FUND_CD, MONEY_RECEIPT.ID, MONEY_RECEIPT.RECEIPT_NO, MONEY_RECEIPT.RECEIPT_TYPE, MONEY_RECEIPT.RECEIPT_DATE,SELLING_AGENT_ID,SELLING_AGENT_NAME, ");
            sbQuery.Append(" MONEY_RECEIPT.HNAME, MONEY_RECEIPT.ADDRESS, MONEY_RECEIPT.UNIT_QTY, MONEY_RECEIPT.RATE, ");
            sbQuery.Append(" DECODE(MONEY_RECEIPT.CHQ_TYPE,'CHQ',' Cheque  NO.'|| MONEY_RECEIPT.CHQ_DD_NO|| ' Date: '||TO_CHAR(MONEY_RECEIPT.CHQ_DD_DATE ,'DD-MON-YYYY')|| ' Drawn on :'|| MONEY_RECEIPT.BANK_INFO, ");
            sbQuery.Append(" 'PO',' Pay Order No.'|| MONEY_RECEIPT.CHQ_DD_NO|| ' Date: '||TO_CHAR(MONEY_RECEIPT.CHQ_DD_DATE ,'DD-MON-YYYY')|| ' Drawn on :'|| MONEY_RECEIPT.BANK_INFO,");
            sbQuery.Append(" 'DD',' Demand Draft No.'|| MONEY_RECEIPT.CHQ_DD_NO|| ' Date: '||TO_CHAR(MONEY_RECEIPT.CHQ_DD_DATE ,'DD-MON-YYYY')|| ' Drawn on :'|| MONEY_RECEIPT.BANK_INFO,");
            sbQuery.Append(" 'BOTH','Cash Amount: '|| MONEY_RECEIPT.CASH_AMT || ' and  Cheque No.'|| MONEY_RECEIPT.CHQ_DD_NO|| ' Date: '||TO_CHAR(MONEY_RECEIPT.CHQ_DD_DATE ,'DD-MON-YYYY')|| ' Drawn on :'|| MONEY_RECEIPT.BANK_INFO,");
            sbQuery.Append(" 'MULT',MONEY_RECEIPT.MULTI_PAY_REMARKS, 'CASH')  AS PAY_TYPE,");
            sbQuery.Append(" MONEY_RECEIPT.ROUTING_NO, MONEY_RECEIPT.BANK_INFO, MONEY_RECEIPT.CASH_AMT, ");
            sbQuery.Append(" MONEY_RECEIPT.MULTI_PAY_REMARKS,  MONEY_RECEIPT.RECEIPT_ENTRY_BY, MONEY_RECEIPT.RECEIPT_ENTRY_DATE");
            sbQuery.Append(" FROM MONEY_RECEIPT,FUND_INFO WHERE MONEY_RECEIPT.REG_BK = FUND_INFO.FUND_CD AND MONEY_RECEIPT.ID = " + MONEY_RECEIPT_ID + " ");

            DataTable dtMneyReceiptDetails = commonGatewayObj.Select(sbQuery.ToString());

            if (dtMneyReceiptDetails.Rows.Count > 0)
            {
                DataTable DtUserInfo = unitSaleBLObj.dtUserInfoByUserID(userObj);              
                decimal totalAmount = Convert.ToDecimal(Convert.ToDecimal(dtMneyReceiptDetails.Rows[0]["UNIT_QTY"].ToString()) * Convert.ToDecimal(dtMneyReceiptDetails.Rows[0]["RATE"].ToString()));
                dtMneyReceiptDetails.TableName = "dtMneyReceiptDetails";
                //dtMneyReceiptDetails.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtMneyReceiptDetails.xsd");
                CR_MoneyReceiptSale.Refresh();
                CR_MoneyReceiptSale.SetDataSource(dtMneyReceiptDetails);
                CR_MoneyReceiptSale.SetParameterValue("user_name", DtUserInfo.Rows[0]["USER_NM"].ToString());
                CR_MoneyReceiptSale.SetParameterValue("fundName", dtMneyReceiptDetails.Rows[0]["FUND_NM"].ToString());
                CR_MoneyReceiptSale.SetParameterValue("total_units_word", numberToEnglisObj.changeNumericToWords(Convert.ToDecimal(dtMneyReceiptDetails.Rows[0]["UNIT_QTY"].ToString())).Trim());
                CR_MoneyReceiptSale.SetParameterValue("totalAmount", totalAmount);
                CR_MoneyReceiptSale.SetParameterValue("TotalAmountInWord", numberToEnglisObj.changeNumericToWords(totalAmount).Trim().ToString());

                CR_MoneyReceiptSale.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "SaleMoneyReceipt" + DateTime.Now + ".pdf");
               // rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "IntimitaionReport" + DateTime.Now + ".pdf")
               //  CrystalReportViewer1.ReportSource = CR_MoneyReceiptSale;
            }
            else
            {
                Response.Write("No Data Found");
            }
        }
        else if (MONEY_RECEIPT_TYPE == "REP")
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT  MONEY_RECEIPT.REG_BK||'/'||MONEY_RECEIPT.REG_BR||'/'||MONEY_RECEIPT.REG_NO AS REG_NO, FUND_INFO.FUND_NM, FUND_INFO.FUND_CD, MONEY_RECEIPT.ID, MONEY_RECEIPT.RECEIPT_NO, MONEY_RECEIPT.RECEIPT_TYPE, MONEY_RECEIPT.RECEIPT_DATE, ");
            sbQuery.Append(" MONEY_RECEIPT.HNAME, MONEY_RECEIPT.ADDRESS, MONEY_RECEIPT.UNIT_QTY, MONEY_RECEIPT.RATE,  MONEY_RECEIPT.BO,");
            sbQuery.Append(" MONEY_RECEIPT.SL_TR_RN_NO,DECODE(MONEY_RECEIPT.REP_PAY_TYPE,'EFT','BEFTN','CHEQUE') AS PAY_TYPE, ");
            sbQuery.Append(" TO_CHAR(MONEY_RECEIPT.REP_PAY_DATE,'DD-MON-YYYY') AS PAY_DATE, MONEY_RECEIPT.RECEIPT_ENTRY_BY, MONEY_RECEIPT.RECEIPT_ENTRY_DATE ");
            sbQuery.Append(" FROM MONEY_RECEIPT,FUND_INFO WHERE MONEY_RECEIPT.REG_BK = FUND_INFO.FUND_CD AND MONEY_RECEIPT.ID = " + MONEY_RECEIPT_ID + " ");

            DataTable dtMneyReceiptDetails = commonGatewayObj.Select(sbQuery.ToString());

            if (dtMneyReceiptDetails.Rows.Count > 0)
            {
               // decimal totalAmount = Convert.ToDecimal(Convert.ToDecimal(dtMneyReceiptDetails.Rows[0]["UNIT_QTY"].ToString()) * Convert.ToDecimal(dtMneyReceiptDetails.Rows[0]["RATE"].ToString()));
                dtMneyReceiptDetails.TableName = "dtMneyReceiptDetailsREP";
                //dtMneyReceiptDetails.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtMneyReceiptDetailsREP.xsd");
                CR_MoneyReceiptRep.Refresh();
                CR_MoneyReceiptRep.SetDataSource(dtMneyReceiptDetails);

                CR_MoneyReceiptRep.SetParameterValue("fundName", dtMneyReceiptDetails.Rows[0]["FUND_NM"].ToString());
                CR_MoneyReceiptRep.SetParameterValue("totalAmount", dtMneyReceiptDetails.Rows[0]["UNIT_QTY"].ToString());
                CR_MoneyReceiptRep.SetParameterValue("TotalAmountInWord", numberToEnglisObj.changeNumericToWords(dtMneyReceiptDetails.Rows[0]["UNIT_QTY"].ToString()).ToString());
                // CrystalReportViewer1.ReportSource = CR_MoneyReceiptRep;
                CR_MoneyReceiptRep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "RepMoneyReceipt" + DateTime.Now + ".pdf");
            }
            else
            {
                Response.Write("No Data Found");
            }
        }
        else
        {
            Response.Write("No Data Found");
        }




    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_MoneyReceiptSale.Close();
        CR_MoneyReceiptSale.Dispose();
    }
}
