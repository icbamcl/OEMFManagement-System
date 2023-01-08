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
    UnitRepurchaseBL unitRepBLObj = new UnitRepurchaseBL();

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
            if (!IsPostBack)
            {
                LienbankNameDropDownList.DataSource = opendMFDAO.dtFillBankName();
                LienbankNameDropDownList.DataTextField = "BANK_NAME";
                LienbankNameDropDownList.DataValueField = "BANK_CODE";
                LienbankNameDropDownList.DataBind();


            }
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

          
            UnitHolderRegistration regObj = new UnitHolderRegistration();
            // UnitTransfer transferObj = new UnitTransfer();
            UnitRepurchase unitRepObj = new UnitRepurchase();

            regObj.FundCode = fundCodeTextBox.Text.Trim();
            regObj.BranchCode = branchCodeTextBox.Text.Trim();
            unitRepObj.RepurchaseNo = Convert.ToInt32(fromRepNoTextBox.Text.Trim().ToString());

            StringBuilder sbMaster = new StringBuilder();
            StringBuilder sbFilter = new StringBuilder();
            DataTable dtReportStatement = new DataTable();
            StringBuilder sbReportString = new StringBuilder();


            string copy =fundCodeTextBox.Text.ToString() + " Copy";
            if (accoountRadioButton.Checked)
            {
                copy = "Accounts Copy";
            }
            
          
            sbMaster.Append("SELECT NVL(REPURCHASE.REP_NO,0) AS REP_NO , TO_CHAR(REPURCHASE.REP_DT, 'DD-MON-YYYY') AS REP_DT, U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO AS RG_NO, ");
            sbMaster.Append(" U_MASTER.HNAME, U_JHOLDER.JNT_NAME, U_MASTER.REG_BK || '/' || U_MASTER.REG_BR || '/' || U_MASTER.REG_NO AS REG_NO,U_MASTER.TIN,U_MASTER.BO,");
            sbMaster.Append(" U_MASTER.ADDRS1, U_MASTER.ADDRS2, U_MASTER.CITY, U_MASTER.BK_AC_NO,U_MASTER.BK_NM_CD,U_MASTER.ID_FLAG,U_MASTER.ID_BK_NM_CD, U_MASTER.ID_BK_BR_NM_CD,");
            sbMaster.Append(" U_MASTER.BK_BR_NM_CD, U_MASTER.BK_FLAG,REPURCHASE.QTY,  REPURCHASE.REP_PRICE AS RATE, REPURCHASE.QTY * REPURCHASE.REP_PRICE AS AMOUNT,  REPURCHASE.SL_TR_NO, U_MASTER.CIP, U_MASTER.ID_AC,");
            sbMaster.Append(" DECODE(REPURCHASE.PAY_TYPE,'EFT','BEFTN','CHEQUE') AS PAY_TYPE ");
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
                   string holderName = dtReportStatement.Rows[0]["HNAME"].ToString().ToUpper();
                    if (dtReportStatement.Rows[0]["BK_FLAG"].ToString().ToUpper() == "N" || dtReportStatement.Rows[0]["BK_NM_CD"].Equals(DBNull.Value) || dtReportStatement.Rows[0]["BK_BR_NM_CD"].Equals(DBNull.Value))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert ('Please Update Bank Information');", true);
                    }
                    else if ((dtReportStatement.Rows[0]["ID_FLAG"].ToString().ToUpper() == "Y") &&( dtReportStatement.Rows[0]["ID_BK_NM_CD"].Equals(DBNull.Value) || dtReportStatement.Rows[0]["ID_BK_BR_NM_CD"].Equals(DBNull.Value) || dtReportStatement.Rows[0]["ID_AC"].Equals(DBNull.Value)))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert ('Please Update ID  Information');", true);

                    }

                    else
                    {
                        if (dtReportStatement.Rows[0]["ID_FLAG"].ToString().ToUpper() == "Y")
                        {
                            holderName = reportObj.getBankNameByBankCode(Convert.ToInt32(dtReportStatement.Rows[0]["ID_BK_NM_CD"].ToString())).ToString() + " , " + reportObj.getBankBranchNameByCode(Convert.ToInt32(dtReportStatement.Rows[0]["ID_BK_NM_CD"].ToString()), Convert.ToInt32(dtReportStatement.Rows[0]["ID_BK_BR_NM_CD"].ToString())).ToString() + " ID NO:" + dtReportStatement.Rows[0]["ID_AC"].ToString().ToUpper();
                        }
                                               
                            string BankInfo = ", bank account no : " + dtReportStatement.Rows[0]["BK_AC_NO"].ToString().ToUpper() + " , " + reportObj.getBankNameByBankCode(int.Parse(dtReportStatement.Rows[0]["BK_NM_CD"].ToString())) + "," + " " + reportObj.getBankBranchNameByCode(int.Parse(dtReportStatement.Rows[0]["BK_NM_CD"].ToString()), int.Parse(dtReportStatement.Rows[0]["BK_BR_NM_CD"].ToString()));
                            DataTable dtBankBracnhInfo = unitHolderRegBLObj.dtGetBankBracnhInfo(Convert.ToInt32(dtReportStatement.Rows[0]["BK_NM_CD"].ToString()), Convert.ToInt32(dtReportStatement.Rows[0]["BK_BR_NM_CD"].ToString()));
                            if (dtBankBracnhInfo.Rows.Count > 0)
                            {
                            BankInfo = BankInfo + " [ " + dtBankBracnhInfo.Rows[0]["ROUTING_NO"].ToString() + "] ";
                            }

                    // string BankInfo = "";
                    decimal totalRepAmount = 0;
                        for (int looper = 0; looper < dtReportStatement.Rows.Count; looper++)
                        {
                            totalRepAmount = totalRepAmount + Convert.ToDecimal(dtReportStatement.Rows[looper]["AMOUNT"].ToString());
                        }
                        if (NoRadioButton.Checked)
                        {
                            string payType = unitRepBLObj.getPayType(regObj, unitRepObj);
                            if (payType.ToString() == "EFT")
                            {
                                sbReportString.Append(" Through BEFTN Tk. " + totalRepAmount.ToString("#,##0.00") + "  ( Taka " + numberToEnglishObj.changeNumericToWords(totalRepAmount) + " ) ");
                                sbReportString.Append(" be issued in favour of Mr./Ms. " + holderName.ToString().ToUpper() + "" + BankInfo);
                                sbReportString.Append(" being the surrended value of units.  ");
                            }
                            else
                            {
                                sbReportString.Append(" An account payee cheque for Tk. " + totalRepAmount.ToString("#,##0.00") + "  ( Taka " + numberToEnglishObj.changeNumericToWords(totalRepAmount) + " ) ");
                                sbReportString.Append(" be issued in favour of Mr./Ms. " + holderName.ToString().ToUpper() + "" + BankInfo);
                                sbReportString.Append(" being the surrended value of units.  ");
                            }
                            
                        }
                        else if (YesRadioButton.Checked)
                        {

                            decimal lienAmount = Convert.ToDecimal(LienAmountTextBox.Text.Trim().ToString());
                            decimal remainingAmount = totalRepAmount - lienAmount;

                            sbReportString.Append("According to the letter no: " + LienReqRefTextBox.Text.Trim().ToString() + " dated on " + LienReqDateTextBox.Text.Trim().ToString() + ", the above certificates are ");
                            sbReportString.Append(" surrendered by " + LienbankNameDropDownList.SelectedItem.Text.ToString() + ", " + LienbranchNameDropDownList.SelectedItem.Text.ToString() + " .  An account payee cheque for Tk. " + lienAmount.ToString("#,##0.00") + " ( Taka ");
                            sbReportString.Append(numberToEnglishObj.changeNumericToWords(lienAmount).ToString() + " )  be issued in favour of  " + LienbankNameDropDownList.SelectedItem.Text.ToString() + " , " + LienbranchNameDropDownList.SelectedItem.Text.ToString());
                            sbReportString.Append(" and another account payee cheque of remaining amout for Tk. " + remainingAmount.ToString("#,##0.00") + " ( Taka  " + numberToEnglishObj.changeNumericToWords(remainingAmount));
                            sbReportString.Append(" ) be issued in favour of Mr./Ms. " + holderName.ToString().ToUpper() + BankInfo + "   being the surrended value of units. ");
                        }
                        Session["dtReportStatement"] = dtReportStatement;
                        Session["fundCode"] = fundCodeTextBox.Text.Trim().ToString();
                        Session["branchCode"] = branchCodeTextBox.Text.Trim().ToString();
                        Session["sbReportString"] = sbReportString.ToString();
                        Session["copy"] = copy;

                        // ClientScript.RegisterStartupScript(this.GetType(), "UnitHolderInfo", "window.open('ReportViewer/UnitReportRepurchaseVoucherReportViewer.aspx')", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UnitHolderInfo", "window.open('ReportViewer/UnitReportRepurchaseVoucherReportViewer.aspx')", true);
                    }
         
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert ('No data found');", true);
                }
            
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert ('" + ex.Message.Replace("'", "").ToString() + "');", true);
        }

    }
    protected void LienbankNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LienbranchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(LienbankNameDropDownList.SelectedValue.ToString()));
        LienbranchNameDropDownList.DataTextField = "BRANCH_NAME";
        LienbranchNameDropDownList.DataValueField = "BRANCH_CODE";
        LienbranchNameDropDownList.DataBind();
    }
    protected void NoRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (NoRadioButton.Checked)
        {
            LienReqDateTextBox.Enabled = false;
            LienReqRefTextBox.Enabled = false;
            LienAmountTextBox.Enabled = false;
            LienReqDateTextBox.Text = "";
            LienReqRefTextBox.Text = "";
            LienAmountTextBox.Text = "";
            LienbankNameDropDownList.SelectedValue = "0";
            LienbranchNameDropDownList.SelectedValue = "0";
            LienbankNameDropDownList.Enabled = false;
            LienbranchNameDropDownList.Enabled = false;
            LienReqDateImageButton.Enabled = false;

        }
        else
        {

            LienAmountTextBox.Enabled = true;
            LienReqDateTextBox.Enabled = true;
            LienReqRefTextBox.Enabled = true;
            LienbankNameDropDownList.Enabled = true;
            LienbranchNameDropDownList.Enabled = true;
            LienReqDateImageButton.Enabled = true;
            LienAmountTextBox.Focus();
        }

    }
    protected void YesRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (NoRadioButton.Checked)
        {
            LienReqDateTextBox.Enabled = false;
            LienReqRefTextBox.Enabled = false;
            LienAmountTextBox.Enabled = false;
            LienReqDateTextBox.Text = "";
            LienReqRefTextBox.Text = "";
            LienAmountTextBox.Text = "";
            LienbankNameDropDownList.SelectedValue = "0";
            LienbranchNameDropDownList.SelectedValue = "0";
            LienbankNameDropDownList.Enabled = false;
            LienbranchNameDropDownList.Enabled = false;
            LienReqDateImageButton.Enabled = false;


        }
        else
        {
            LienAmountTextBox.Enabled = true;
            LienReqDateTextBox.Enabled = true;
            LienReqRefTextBox.Enabled = true;
            LienbankNameDropDownList.Enabled = true;
            LienbranchNameDropDownList.Enabled = true;
            LienReqDateImageButton.Enabled = true;
            LienAmountTextBox.Focus();
        }
    }
}
