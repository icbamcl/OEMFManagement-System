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

public partial class ReportViewer_UnitReportRepurchaseBEFTNAdviceLetterReportViewer : System.Web.UI.Page
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
    AMCL.REPORT.CR_BEFTNAdviceLetterIFIC CR_REP_BEFTN_ADVICE_IFIC_Letter = new AMCL.REPORT.CR_BEFTNAdviceLetterIFIC();

    
    

    protected void Page_Load(object sender, EventArgs e)
    {

             
        string fundCode = "";       
       
        
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
       

        fundCode = (string)Session["FUND_CD"];
        string beftnDate = (string)Session["BEFTN_DATE"];
        string beftnTrackingNo = (string)Session["BEFTN_TRACKING_NO"];

        DataTable dtRepurchaseBEFTNAdviceData = unitRepBLObj.dtGetBEFTNAdviceData(" AND B.BEFTN_TRACKING_NO=" + beftnTrackingNo + "  AND B.BEFTN_DATE='" + beftnDate + "' AND B.REG_BK='" + fundCode+"'" );
        DataTable dtBEFTNSignatoryBankInfo = unitRepBLObj.dtGetBEFTNSignatoryBankInfo("AND REPURCHASE.BEFTN_DATE='" + beftnDate + "' AND REPURCHASE.BEFTN_TRACKING_NO=" + beftnTrackingNo.ToString() + " AND FUND_INFO.FUND_CD ='" + fundCode.ToString()+"'");
        decimal totalAmount = unitRepBLObj.getBEFTNAdviceTotalAmount("AND BEFTN_TRACKING_NO=" + beftnTrackingNo + "  AND BEFTN_DATE='" + beftnDate + "' AND REG_BK='" + fundCode + "'");
        if (dtRepurchaseBEFTNAdviceData.Rows.Count > 0)
        {
            dtRepurchaseBEFTNAdviceData.TableName = "dtRepurchaseBEFTNAdviceData";

           //   dtRepurchaseBEFTNAdviceData.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2008\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtRepurchaseBEFTNAdviceData.xsd");
            if (dtBEFTNSignatoryBankInfo.Rows.Count > 0)
            {
                CR_REP_BEFTN_ADVICE_IFIC_Letter.Refresh();
                CR_REP_BEFTN_ADVICE_IFIC_Letter.SetDataSource(dtRepurchaseBEFTNAdviceData);

                CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("BANK_ACC_TYPE", dtRepurchaseBEFTNAdviceData.Rows[0]["BANK_AC_TYPE"].ToString());
                CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("BANK_AC_NO", dtRepurchaseBEFTNAdviceData.Rows[0]["FUND_ACC_NO"].ToString());
                CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("BANK_ROUTING_NO", dtRepurchaseBEFTNAdviceData.Rows[0]["BANK_ROUTING_NO"].ToString());
                CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("FUND_NM", dtRepurchaseBEFTNAdviceData.Rows[0]["FUND_NM"].ToString());
                CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("AmoutWord", numberToEnglisObj.changeNumericToWords(totalAmount).ToString()+".");

                CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("Bank_Name", dtBEFTNSignatoryBankInfo.Rows[0]["BANK_NAME"].ToString());
                CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("Bank_Branch_Name", dtBEFTNSignatoryBankInfo.Rows[0]["BRANCH_NAME"].ToString());
                CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("Bank_Branch_Addres1", dtBEFTNSignatoryBankInfo.Rows[0]["BANK_ADDRESS1"].ToString());
                CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("Bank_Branch_Addres2", dtBEFTNSignatoryBankInfo.Rows[0]["BANK_ADDRESS2"].ToString());
                CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("Bank_Branch_Addres3", dtBEFTNSignatoryBankInfo.Rows[0]["BANK_ADDRESS3"].ToString());
                //CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("Singnatory1_Name", dtBEFTNSignatoryBankInfo.Rows[0]["SIGNATORY_NAME1"].ToString());
                //CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("Signatory2_Name", dtBEFTNSignatoryBankInfo.Rows[0]["SIGNATORY_NAME2"].ToString());
                //CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("Signatory1_Designation", dtBEFTNSignatoryBankInfo.Rows[0]["SIGNATORY_DESIG1"].ToString());
                //CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("Signatory2_Designation", dtBEFTNSignatoryBankInfo.Rows[0]["SIGNATORY_DESIG2"].ToString());
                CR_REP_BEFTN_ADVICE_IFIC_Letter.SetParameterValue("Beftn_tracking_number", beftnTrackingNo);

                CrystalReportViewer1.ReportSource = CR_REP_BEFTN_ADVICE_IFIC_Letter;

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
        CR_REP_BEFTN_ADVICE_IFIC_Letter.Close();
        CR_REP_BEFTN_ADVICE_IFIC_Letter.Dispose();
    }
}
