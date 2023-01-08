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
using System.Text;
using System.Data.OracleClient;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;


public partial class UI_UnitReportRepurchaseVoucher : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
    NumberToEnglish numberToEnglishObj = new NumberToEnglish();
    
    protected void Page_Load(object sender, EventArgs e)
    {
       if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        string fundCode = bcContent.FundCode.ToString();
        string branchCode = bcContent.BranchCode.ToString();
        spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
        fundCodeTextBox.Text = fundCode.ToString();
        branchCodeTextBox.Text = branchCode.ToString();
        
   
       // toRegDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
       // fromRegDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy"); ;
        ///holderDateofBirthTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");

        if (!IsPostBack)
        {
            LienbankNameDropDownList.DataSource = opendMFDAO.dtFillBankName();
            LienbankNameDropDownList.DataTextField = "BANK_NAME";
            LienbankNameDropDownList.DataValueField = "BANK_CODE";
            LienbankNameDropDownList.DataBind();
           

        }
        else
        {
            

        }
       

    }       

    private void ClearText()
    {

    }
    protected void regCloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");

    }

    protected void ShowReportButton_Click(object sender, EventArgs e)
    {
       
        try
        {
            //StringBuilder sbRepurchase = new StringBuilder();
            //sbRepurchase.Append("SELECT  SUM(QTY) AS QTY, SUM(QTY * REP_PRICE) AS AMOUNT FROM (SELECT REPURCHASE.REP_NO, REPURCHASE.REP_DT, REPURCHASE.QTY, U_MASTER.HNAME, REPURCHASE.SL_TR_NO, ");
            //sbRepurchase.Append(" ");


            StringBuilder sbMaster = new StringBuilder();
            StringBuilder sbFilter = new StringBuilder();
            DataTable dtReportStatement = new DataTable();
            StringBuilder sbReportString = new StringBuilder();
            
          
            sbMaster.Append("SELECT NVL(REPURCHASE.REP_NO,0) AS REP_NO , TO_CHAR(REPURCHASE.REP_DT, 'DD-MON-YYYY') AS REP_DT, U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO AS RG_NO, ");
            sbMaster.Append(" U_MASTER.HNAME, U_JHOLDER.JNT_NAME, U_MASTER.REG_BK || '/' || U_MASTER.REG_BR || '/' || U_MASTER.REG_NO AS REG_NO,");
            sbMaster.Append(" U_MASTER.ADDRS1, U_MASTER.ADDRS2, U_MASTER.CITY, U_MASTER.SPEC_IN1, U_MASTER.SPEC_IN2, U_MASTER.BK_AC_NO,");
            sbMaster.Append(" U_MASTER.BK_BR_NM_CD, U_MASTER.BK_FLAG,REPURCHASE.QTY,  REPURCHASE.REP_PRICE AS RATE, REPURCHASE.QTY * REPURCHASE.REP_PRICE AS AMOUNT,  REPURCHASE.SL_TR_NO, U_MASTER.CIP, U_MASTER.ID_AC");
            sbMaster.Append(" FROM  U_MASTER INNER JOIN  REPURCHASE ON U_MASTER.REG_BK = REPURCHASE.REG_BK AND U_MASTER.REG_BR = REPURCHASE.REG_BR AND  U_MASTER.REG_NO = REPURCHASE.REG_NO");
            sbMaster.Append(" LEFT OUTER JOIN  U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND U_MASTER.REG_NO = U_JHOLDER.REG_NO");
                      
            sbMaster.Append(" WHERE 1=1");
            sbMaster.Append(" AND (U_MASTER.REG_BK = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (U_MASTER.REG_BR = '" + branchCodeTextBox.Text.Trim().ToString().ToUpper() + "')");

            if (fromRepNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (REPURCHASE.REP_NO = " + Convert.ToInt32(fromRepNoTextBox.Text.Trim().ToString()) + ")");
            }
            if (fromRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (U_MASTER.REG_NO = " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + ")");
            }

            if (fromRepDateTextBox.Text != "")
            {
                sbFilter.Append(" AND ( REPURCHASE.REP_DT ='" + Convert.ToDateTime(fromRepDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }


            sbFilter.Append(" ORDER BY TO_NUMBER(SUBSTR(REPURCHASE.SL_TR_NO,2)) ");
            sbMaster.Append(sbFilter.ToString());          
            dtReportStatement = commonGatewayObj.Select(sbMaster.ToString());
           
            if (dtReportStatement.Rows.Count > 0)
            {
                decimal totalRepAmount = 0;
                for (int looper = 0; looper < dtReportStatement.Rows.Count; looper++)
                {
                     totalRepAmount =totalRepAmount+ Convert.ToDecimal(dtReportStatement.Rows[looper]["AMOUNT"].ToString());
                }
                if (NoRadioButton.Checked)
                {
                    sbReportString.Append("A cheque for Tk. " +string.Format("{0:0.00}",totalRepAmount) + "  (  " + numberToEnglishObj.changeCurrencyToWords(totalRepAmount.ToString())+" ) ");
                    sbReportString.Append(" be issued in favour of Mr./Ms. " + dtReportStatement.Rows[0]["HNAME"].ToString().ToUpper() + " being the surrended value of units. ");
                }
                else if (YesRadioButton.Checked)
                {
                    sbReportString.Append("According to the letter no: " + LienReqRefTextBox.Text.Trim().ToString() + " dated on " + LienReqDateTextBox.Text.Trim().ToString() + ", the above certificates are ");
                    sbReportString.Append(" surrendered by " + LienbankNameDropDownList.SelectedItem.Text.ToString() + ", " + LienbranchNameDropDownList.SelectedItem.Text.ToString() + " .  A Cheque for Tk. " + string.Format("{0:0.00}",LienAmountTextBox.Text.Trim().ToString()) + " ( ");
                    sbReportString.Append(numberToEnglishObj.changeCurrencyToWords(LienAmountTextBox.Text.ToString()).ToString() + " )  be issued in favour of  " + LienbankNameDropDownList.SelectedItem.Text.ToString() + " , " + LienbranchNameDropDownList.SelectedItem.Text.ToString());
                    sbReportString.Append(" and another cheque of remaining amout for Tk. " + string.Format("{0:0.00}", Convert.ToString(totalRepAmount - Convert.ToDecimal(LienAmountTextBox.Text.Trim().ToString()))) + " (  " + numberToEnglishObj.changeCurrencyToWords(Convert.ToString(totalRepAmount - Convert.ToDecimal(LienAmountTextBox.Text.Trim().ToString()))));
                    sbReportString.Append(" ) be issued in favour of Mr./Ms. " + dtReportStatement.Rows[0]["HNAME"].ToString().ToUpper() + "  being the surrended value of units. ");
                }
                Session["dtReportStatement"] = dtReportStatement;
                Session["fundCode"]=fundCodeTextBox.Text.Trim().ToString();
                Session["branchCode"] = branchCodeTextBox.Text.Trim().ToString();
                Session["sbReportString"] = sbReportString.ToString();

               // ClientScript.RegisterStartupScript(this.GetType(), "UnitHolderInfo", "window.open('ReportViewer/UnitReportRepurchaseVoucherReportViewer.aspx')", true);
                 ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UnitHolderInfo", "window.open('ReportViewer/UnitReportRepurchaseVoucherReportViewer.aspx')", true);

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('No data found');", true);
            }
        }
        catch (Exception ex)
        {
         
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + ex.Message.Replace("'", "").ToString() + "');", true);
        }

    }
    protected void LienbankNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LienbranchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(LienbankNameDropDownList.SelectedValue.ToString()));
        LienbranchNameDropDownList.DataTextField = "BRANCH_NAME";
        LienbranchNameDropDownList.DataValueField = "BRANCH_CODE";
        LienbranchNameDropDownList.DataBind();
      
        if (NormalRadioButton.Checked)
        {
            divDeath.Style.Add("visibility", "hidden");
            dvChequeIssue.Style.Add("visibility", "hidden");
        }
        if (YesRadioButton.Checked)
        {
            divLienMark.Style.Add("visibility", "visible");
        }
      
      
      
    }

    protected void ChequeIssueToDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

        dinoGridView.DataSource = commonGatewayObj.Select("SELECT NOMI_NAME AS chqName FROM U_NOMINEE WHERE REG_BK='" + fundCodeTextBox.Text.Trim().ToString() + "' AND REG_BR='" + branchCodeTextBox.Text.Trim().ToString() + "' AND NOMI_TYPE='" + ChequeIssueToDropDownList.SelectedValue.ToString() + "' AND REG_NO=" + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()));
        dinoGridView.DataBind();
       if (NoRadioButton.Checked)
        {
            divLienMark.Style.Add("visibility", "hidden");
        }
        if (DeathRadioButton.Checked)
        {

            divDeath.Style.Add("visibility", "visible");
            dvChequeIssue.Style.Add("visibility", "visible");
        }
        
       
    }
    protected void findButton_Click(object sender, EventArgs e)
    {
        DataTable dtRepinfo = commonGatewayObj.Select("SELECT * FROM REPURCHASE WHERE REG_BK='"+fundCodeTextBox.Text.Trim().ToString()+"' AND REG_BR='"+branchCodeTextBox.Text.Trim().ToString()+"' AND REP_NO=" + Convert.ToInt32(fromRepNoTextBox.Text.Trim().ToString()));
        if (dtRepinfo.Rows.Count > 0)
        {
            fromRegNoTextBox.Text = dtRepinfo.Rows[0]["REG_NO"].Equals(DBNull.Value) ? "" : dtRepinfo.Rows[0]["REG_NO"].ToString();
            fromRepDateTextBox.Text =Convert.ToDateTime(dtRepinfo.Rows[0]["REP_DT"].ToString()).ToString("dd-MMM-yyyy");
            dinoGridView.DataSource = commonGatewayObj.Select("SELECT NOMI_NAME AS chqName FROM U_NOMINEE WHERE REG_BK='" + fundCodeTextBox.Text.Trim().ToString() + "' AND REG_BR='" + branchCodeTextBox.Text.Trim().ToString() + "' AND NOMI_TYPE='" + ChequeIssueToDropDownList.SelectedValue.ToString() + "' AND REG_NO=" + Convert.ToInt32(dtRepinfo.Rows[0]["REG_NO"].ToString()));
            dinoGridView.DataBind();
            if (DeathRadioButton.Checked)
            {

                divDeath.Style.Add("visibility", "visible");
                dvChequeIssue.Style.Add("visibility", "visible");
            }
            else
            {
                divDeath.Style.Add("visibility", "hidden");
                dvChequeIssue.Style.Add("visibility", "hidden");
            }
            if (YesRadioButton.Checked)
            {
                divLienMark.Style.Add("visibility", "visible");
            }
            else
            {
                divLienMark.Style.Add("visibility", "hidden");
            }


        }
        
    }
}
