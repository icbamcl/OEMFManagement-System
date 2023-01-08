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
using System.IO;
using System.Text;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitMoneyReceiptEditRepurchase : System.Web.UI.Page
{
   // System.Web.UI.Page this_page_ref = null;
    OMFDAO opendMFDAO = new OMFDAO();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    UnitHolderRegBL regBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    string errorMassege = "";
    BaseClass bcContent = new BaseClass();
    UnitReport reportObj = new UnitReport();
    EncryptDecrypt encrypt = new EncryptDecrypt();
    CommonGateway commonGatewayObj = new CommonGateway();
    string fundCode = "";
    string branchCode = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj=new UnitHolderRegistration();
           
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        fundCode = bcContent.FundCode.ToString();
        branchCode = bcContent.BranchCode.ToString();
        spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
                                      
        regNoTextBox.Focus();       
        if (!IsPostBack)
        {
            fundCodeDDL.DataSource = reportObj.dtFundCodeList();
            fundCodeDDL.DataTextField = "NAME";
            fundCodeDDL.DataValueField = "ID";
            fundCodeDDL.SelectedValue = fundCode.ToString();
            fundCodeDDL.DataBind();

            branchCodeDDL.DataSource = reportObj.dtBranchCodeList();
            branchCodeDDL.DataTextField = "NAME";
            branchCodeDDL.DataValueField = "ID";
            branchCodeDDL.SelectedValue = branchCode.ToString();
            branchCodeDDL.DataBind();

            regObj.FundCode = fundCode.ToString();
            regObj.BranchCode = branchCode.ToString();
           

            moneyReceipDropDownList.DataSource = unitSaleBLObj.dtMoneyRecieptforDDL(" AND REG_BK = '" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR = '" + regObj.BranchCode.ToString().ToUpper() + "'AND RECEIPT_TYPE = 'REP' AND SL_REP_TR_RN_NO IS NULL  AND ACC_VOUCHER_NO IS NULL AND VALID IS  NULL ORDER BY RECEIPT_NO DESC ");
            moneyReceipDropDownList.DataTextField = "RECEIPT_NO";
            moneyReceipDropDownList.DataValueField = "ID";
            moneyReceipDropDownList.DataBind();

        }
    
    }
   
    private void ClearText()
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        fundCodeDDL.DataSource = reportObj.dtFundCodeList();
        fundCodeDDL.DataTextField = "NAME";
        fundCodeDDL.DataValueField = "ID";
        fundCodeDDL.SelectedValue = fundCode.ToString();
        fundCodeDDL.DataBind();

        branchCodeDDL.DataSource = reportObj.dtBranchCodeList();
        branchCodeDDL.DataTextField = "NAME";
        branchCodeDDL.DataValueField = "ID";
        branchCodeDDL.SelectedValue = branchCode.ToString();
        branchCodeDDL.DataBind();

        regObj.FundCode = fundCode.ToString();
        regObj.BranchCode = branchCode.ToString();

        ReceiptDateTextBox.Text ="";
        moneyReceipDropDownList.SelectedValue = "0";

        regNoTextBox.Text = "";
        holderBOTextBox.Text = "";
        NameTextBox.Text = "";
        addressTextBox.Text = "";
        QtyTextBox.Text = "";
        RateTextBox.Text = "";

        SaleTRRNNoTextBox.Text = "";
        PayDateTextBox.Text = "";
        EFTRadioButton.Checked = true;
        CHQRadioButton.Checked = false;


    }
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }
   
    protected void moneyReceipDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtMoneyReceitInfoDetails = unitSaleBLObj.dtMoneyRecieptInfoDetails(Convert.ToInt64(moneyReceipDropDownList.SelectedValue.ToString()));
        if (dtMoneyReceitInfoDetails.Rows.Count > 0)
        {
            regNoTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["REG_NO"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["REG_NO"].ToString();
            holderBOTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["BO"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["BO"].ToString();
            ReceiptDateTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["RECEIPT_DATE"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtMoneyReceitInfoDetails.Rows[0]["RECEIPT_DATE"].ToString()).ToString("dd-MMM-yyyy");
            NameTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["HNAME"].ToString();
            addressTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["ADDRESS"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["ADDRESS"].ToString();
            RateTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["RATE"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["RATE"].ToString();
            QtyTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["UNIT_QTY"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["UNIT_QTY"].ToString();
            SaleTRRNNoTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["SL_TR_RN_NO"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["SL_TR_RN_NO"].ToString();
            if (!dtMoneyReceitInfoDetails.Rows[0]["REP_PAY_TYPE"].Equals(DBNull.Value))
            {
                string chqType = dtMoneyReceitInfoDetails.Rows[0]["REP_PAY_TYPE"].ToString();
                if (chqType.ToString().ToUpper() == "CHQ")
                {
                    CHQRadioButton.Checked = true;
                    EFTRadioButton.Checked = false;
                }
               else
                {
                    EFTRadioButton.Checked = true;
                    CHQRadioButton.Checked = false;
                }

            }
            PayDateTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["REP_PAY_DATE"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtMoneyReceitInfoDetails.Rows[0]["REP_PAY_DATE"].ToString()).ToString("dd-MMM-yyyy");

        }
        else
        {
            ClearText();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Money Receipt Number');", true);
        }
    }
    protected void PrintSaveButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();

        long money_Receipt_ID = 0;
        Hashtable htInsertMoneyReceipt = new Hashtable();
        Hashtable htUpdateMoneyReceipt = new Hashtable();
        try
        {
            regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
            regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();

          
            commonGatewayObj.BeginTransaction();
            money_Receipt_ID = commonGatewayObj.GetMaxNo("MONEY_RECEIPT", "ID") + 1;

            htUpdateMoneyReceipt.Add("VALID", "N");
            htUpdateMoneyReceipt.Add("RECEIPT_EDIT_TYPE", "E");
            htUpdateMoneyReceipt.Add("RECEIPT_EDIT_BY", userObj.UserID.ToString());
            htUpdateMoneyReceipt.Add("RECEIPT_EDIT_DATE", DateTime.Today.ToString("dd-MMM-yyyy"));

            commonGatewayObj.Update(htUpdateMoneyReceipt, "MONEY_RECEIPT", "ID=" + moneyReceipDropDownList.SelectedValue.ToString());


            htInsertMoneyReceipt.Add("ID", money_Receipt_ID);
            htInsertMoneyReceipt.Add("RECEIPT_NO", moneyReceipDropDownList.SelectedItem.Text);
            htInsertMoneyReceipt.Add("RECEIPT_TYPE", "REP");
            htInsertMoneyReceipt.Add("RECEIPT_DATE", ReceiptDateTextBox.Text.Trim());
            htInsertMoneyReceipt.Add("REG_BK", fundCodeDDL.SelectedValue.ToString());
            htInsertMoneyReceipt.Add("REG_BR", branchCodeDDL.SelectedValue.ToString());               
            htInsertMoneyReceipt.Add("REG_NO", regNoTextBox.Text.Trim());
               
            if (holderBOTextBox.Text.Trim() != "")
            {
                htInsertMoneyReceipt.Add("BO", holderBOTextBox.Text.Trim());
            }                
            htInsertMoneyReceipt.Add("HNAME", NameTextBox.Text.Trim());                            
            htInsertMoneyReceipt.Add("ADDRESS", addressTextBox.Text.Trim());
            htInsertMoneyReceipt.Add("UNIT_QTY", QtyTextBox.Text.Trim());
            htInsertMoneyReceipt.Add("RATE", RateTextBox.Text.Trim());
            htInsertMoneyReceipt.Add("SL_TR_RN_NO", SaleTRRNNoTextBox.Text.Trim().ToUpper());
            if (EFTRadioButton.Checked)
            {
                htInsertMoneyReceipt.Add("REP_PAY_TYPE", "EFT");
            }
            else
            {
                htInsertMoneyReceipt.Add("REP_PAY_TYPE", "CHQ");
            }
            htInsertMoneyReceipt.Add("REP_PAY_DATE", PayDateTextBox.Text.Trim());
            htInsertMoneyReceipt.Add("RECEIPT_ENTRY_BY", userObj.UserID.ToString());
            htInsertMoneyReceipt.Add("RECEIPT_ENTRY_DATE", DateTime.Today.ToString("dd-MMM-yyyy"));

            commonGatewayObj.Insert(htInsertMoneyReceipt, "MONEY_RECEIPT");
            commonGatewayObj.CommitTransaction();
            ClearText();
            Session["MONEY_RECEIPT_ID"] = money_Receipt_ID.ToString();
            Session["MONEY_RECEIPT_TYPE"] = "REP";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "window.open('ReportViewer/UnitReportMoneyReceiptReportViewer.aspx');", true);
            

        }

        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            Session["MONEY_RECEIPT_ID"] = null;
            Session["MONEY_RECEIPT_TYPE"] = null;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save and print Failed');", true);
        }
    }
    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
        regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
        Hashtable htUpdateMoneyReceipt = new Hashtable();
        try
        {
            commonGatewayObj.BeginTransaction();
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("UPDATE MONEY_RECEIPT SET VALID='N' , RECEIPT_EDIT_TYPE='D' , RECEIPT_EDIT_BY='" + userObj.UserID.ToString() + "',");
            sbQuery.Append(" RECEIPT_EDIT_DATE='" + DateTime.Today.ToString("dd-MMM-yyyy") + "'  WHERE ID=" + moneyReceipDropDownList.SelectedValue.ToString());
            commonGatewayObj.ExecuteNonQuery(sbQuery.ToString());
            commonGatewayObj.CommitTransaction();
            ClearText();

            moneyReceipDropDownList.DataSource = unitSaleBLObj.dtMoneyRecieptforDDL(" AND REG_BK = '" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR = '" + regObj.BranchCode.ToString().ToUpper() + "'AND RECEIPT_TYPE = 'REP' AND SL_REP_TR_RN_NO IS NULL  AND VALID IS NULL AND ACC_VOUCHER_NO IS NULL ORDER BY RECEIPT_NO DESC ");
            moneyReceipDropDownList.DataTextField = "RECEIPT_NO";
            moneyReceipDropDownList.DataValueField = "ID";
            moneyReceipDropDownList.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Delete Success');", true);
        }

        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Delete Failed');", true);
        }
    }
    protected void fundCodeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
        regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
        moneyReceipDropDownList.DataSource = unitSaleBLObj.dtMoneyRecieptforDDL(" AND REG_BK = '" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR = '" + regObj.BranchCode.ToString().ToUpper() + "'AND RECEIPT_TYPE = 'REP' AND SL_REP_TR_RN_NO IS NULL AND VALID IS NULL AND ACC_VOUCHER_NO IS NULL ORDER BY RECEIPT_NO DESC ");
        moneyReceipDropDownList.DataTextField = "RECEIPT_NO";
        moneyReceipDropDownList.DataValueField = "ID";
        moneyReceipDropDownList.DataBind();
    }
}
