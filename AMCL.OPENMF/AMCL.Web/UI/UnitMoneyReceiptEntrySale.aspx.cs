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
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitMoneyReceiptEntrySale : System.Web.UI.Page
{
   // System.Web.UI.Page this_page_ref = null;
    OMFDAO opendMFDAO = new OMFDAO();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    UnitHolderRegBL regBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    string errorMassege = "";
    BaseClass bcContent = new BaseClass();
    EncryptDecrypt encrypt = new EncryptDecrypt();
    UnitReport reportObj = new UnitReport();
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

            agentNameDDL.DataSource = unitSaleBLObj.dtSellingAgentInfoforDDL();
            agentNameDDL.DataTextField = "NAME";
            agentNameDDL.DataValueField = "ID";
            agentNameDDL.SelectedValue ="0";
            agentNameDDL.DataBind();

            regObj.FundCode = fundCode.ToString();
            regObj.BranchCode = branchCode.ToString();

            ReceiptDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            ReceiptNoTextBox.Text = unitSaleBLObj.GetMaxReceiptNo(regObj, "SL").ToString();
            RateTextBox.Text= unitSaleBLObj.GetLastPrice(regObj, "SL").ToString();
            agentNameDDL.Enabled = true;
            sellingAgentCodeTextBox.Enabled = true;

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

        ReceiptDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        ReceiptNoTextBox.Text = unitSaleBLObj.GetMaxReceiptNo(regObj, "SL").ToString();

        regNoTextBox.Text = "";
        holderBOTextBox.Text = "";
        NameTextBox.Text= "";
        addressTextBox.Text = "";
        QtyTextBox.Text = "";       
        ChqRadioButton.Checked = true;
        ChequeTypeDropDownList.SelectedValue = "CHQ";
        CHQDDPONOTextBox.Text = "";
        chequeDateTextBox.Text = "";
        RoutingNoTextBox.Text = "";
        BankInfoTextBox.Text = "";
        
        CashRadioButton.Checked = false;
        BothRadioButton.Checked = false;
        MultiRadioButton.Checked = false;
        RoutingNoTextBox.Text = "";
        BankInfoTextBox.Text = "";     
        CashAmountTextBox.Text = "";
        MultiplePayTypeTextBox.Text = "";

        agentNameDDL.Enabled = true;
        agentNameDDL.SelectedValue = "0";
        CHQDDPONOTextBox.Text = "";
        sellingAgentCodeTextBox.Enabled = true;
        sellingAgentCodeTextBox.Text = "";


    }
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }
    protected void findButton_Click(object sender, EventArgs e)
    {


        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
        regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
        regObj.RegNumber = regNoTextBox.Text.Trim();
        regObj.BO = holderBOTextBox.Text.ToString().Trim();
       
        DataTable dtValidSearch = opendMFDAO.dtValidSearch(regObj);
        if (dtValidSearch.Rows.Count > 0)
        {
            regObj = new UnitHolderRegistration();
            regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
            regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
            regObj.RegNumber = dtValidSearch.Rows[0]["REG_NO"].ToString();
            DataTable dtRegInfo = opendMFDAO.getDtRegInfo(regObj);
           
            regNoTextBox.Text = dtRegInfo.Rows[0]["REG_NO"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["REG_NO"].ToString();
            holderBOTextBox.Text = dtRegInfo.Rows[0]["BO"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["BO"].ToString();

            NameTextBox.Text = dtRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["HNAME"].ToString();
            string string1 = dtRegInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["ADDRS1"].ToString();
            string string2= dtRegInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["ADDRS2"].ToString();
            string string3 = dtRegInfo.Rows[0]["CITY"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["CITY"].ToString();
            addressTextBox.Text = string1.ToString()+ " "+string2+" " + string3;

            if (!dtRegInfo.Rows[0]["SELLING_AGENT_ID"].Equals(DBNull.Value))
            {
                agentNameDDL.DataSource = unitSaleBLObj.dtSellingAgentInfoforDDL();
                agentNameDDL.DataTextField = "NAME";
                agentNameDDL.DataValueField = "ID";
                agentNameDDL.SelectedValue = dtRegInfo.Rows[0]["SELLING_AGENT_ID"].ToString();
                agentNameDDL.DataBind();
                agentNameDDL.Enabled = false;
                sellingAgentCodeTextBox.Text= dtRegInfo.Rows[0]["SELLING_AGENT_ID"].ToString();
                sellingAgentCodeTextBox.Enabled = false;

            }
            else
            {
               
             
                sellingAgentCodeTextBox.Enabled = true;
                sellingAgentCodeTextBox.Text = "";

                agentNameDDL.Enabled = true;
                agentNameDDL.DataSource = unitSaleBLObj.dtSellingAgentInfoforDDL();
                agentNameDDL.DataTextField = "NAME";
                agentNameDDL.DataValueField = "ID";
                agentNameDDL.SelectedValue = "0";
                agentNameDDL.DataBind();

            }
            

        }
        else
        {
            regNoTextBox.Text = "";
            holderBOTextBox.Text = "";
            NameTextBox.Text = "";
            addressTextBox.Text = "";
          ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Registration Number OR BO ');", true);
        }


    }

    protected void fundCodeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
        regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
        ReceiptNoTextBox.Text = unitSaleBLObj.GetMaxReceiptNo(regObj, "SL").ToString();
        RateTextBox.Text = unitSaleBLObj.GetLastPrice(regObj, "SL").ToString();
    }
    protected void findRoutingButton_Click(object sender, EventArgs e)
    {
        string bankInfo = "";
        DataTable dtBankBracnhInfo = regBLObj.dtGetBankBracnhInfo(RoutingNoTextBox.Text.Trim().ToString());
        if (dtBankBracnhInfo.Rows.Count > 0)
        {
            bankInfo= bankInfo  +reportObj.getBankNameByBankCode(Convert.ToInt32(dtBankBracnhInfo.Rows[0]["BANK_CODE"].ToString())).ToString();
            bankInfo = bankInfo+" , " + reportObj.getBankBranchNameByCode(Convert.ToInt32(dtBankBracnhInfo.Rows[0]["BANK_CODE"].ToString()), Convert.ToInt32(dtBankBracnhInfo.Rows[0]["BRANCH_CODE"].ToString())).ToString()+" BRANCH ";
            BankInfoTextBox.Text = bankInfo.ToString();

        }
        else
        {
            BankInfoTextBox.Text = "";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Bank Information Found');", true);
        }
    }

    protected void PrintSaveButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        
        long money_Receipt_ID = 0;
        Hashtable htInsertMoneyReceipt = new Hashtable();
        try
        {
            regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
            regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
            int saleLimitLower = unitSaleBLObj.SaleLimitLower(regObj);
            long saleLimmitUpper = unitSaleBLObj.SaleLimitUpper(regObj);

            if (unitSaleBLObj.CheckDuplicateMoneyReceiptNo(regObj, "SL", Convert.ToInt64(ReceiptNoTextBox.Text.Trim())))
            {                
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " " + "Receipt Number " + "');", true);
            }
            else if (saleLimitLower > Convert.ToInt32(QtyTextBox.Text.Trim().ToString()))
            {
                QtyTextBox.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Sale Unit Quantity can not less than " + saleLimitLower.ToString() + "');", true);

            }
            else if (saleLimmitUpper < Convert.ToInt32(QtyTextBox.Text.Trim().ToString()))
            {
                QtyTextBox.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Sale Unit Quantity can not greater than " + saleLimmitUpper.ToString() + "');", true);

            }
            else
            {
                money_Receipt_ID = commonGatewayObj.GetMaxNo("MONEY_RECEIPT", "ID") + 1;
                commonGatewayObj.BeginTransaction();
                htInsertMoneyReceipt.Add("ID", money_Receipt_ID);
                htInsertMoneyReceipt.Add("RECEIPT_NO", ReceiptNoTextBox.Text.Trim());
                htInsertMoneyReceipt.Add("RECEIPT_TYPE", "SL");
                htInsertMoneyReceipt.Add("RECEIPT_DATE", ReceiptDateTextBox.Text.Trim());
                htInsertMoneyReceipt.Add("REG_BK", fundCodeDDL.SelectedValue.ToString());
                htInsertMoneyReceipt.Add("REG_BR", branchCodeDDL.SelectedValue.ToString());
                if (regNoTextBox.Text.Trim() != "")
                {
                    htInsertMoneyReceipt.Add("REG_NO", regNoTextBox.Text.Trim());
                }
                if (holderBOTextBox.Text.Trim() != "")
                {
                    htInsertMoneyReceipt.Add("BO", holderBOTextBox.Text.Trim());
                }
                if (NameTextBox.Text.Trim() != "")
                {
                    htInsertMoneyReceipt.Add("HNAME", NameTextBox.Text.Trim());
                }
                htInsertMoneyReceipt.Add("ADDRESS", addressTextBox.Text.Trim());
                htInsertMoneyReceipt.Add("UNIT_QTY", QtyTextBox.Text.Trim());
                htInsertMoneyReceipt.Add("RATE", RateTextBox.Text.Trim());



                if (ChqRadioButton.Checked)
                {
                    htInsertMoneyReceipt.Add("PAY_TYPE", "CHQ");
                    htInsertMoneyReceipt.Add("CHQ_TYPE", ChequeTypeDropDownList.SelectedValue.ToString());
                }
                else if (CashRadioButton.Checked)
                {
                    htInsertMoneyReceipt.Add("PAY_TYPE", "CASH");
                }
                else if (BothRadioButton.Checked)
                {
                    htInsertMoneyReceipt.Add("PAY_TYPE", "BOTH");
                    htInsertMoneyReceipt.Add("CHQ_TYPE", ChequeTypeDropDownList.SelectedValue.ToString());
                }
                else if (MultiRadioButton.Checked)
                {
                    htInsertMoneyReceipt.Add("PAY_TYPE", "MULT");
                }
                if (CHQDDPONOTextBox.Text.Trim() != "")
                {
                    htInsertMoneyReceipt.Add("CHQ_DD_NO", CHQDDPONOTextBox.Text.Trim());
                }
                if (chequeDateTextBox.Text.Trim() != "")
                {
                    htInsertMoneyReceipt.Add("CHQ_DD_DATE", chequeDateTextBox.Text.Trim());
                }
                if (RoutingNoTextBox.Text.Trim() != "")
                {
                    htInsertMoneyReceipt.Add("ROUTING_NO", RoutingNoTextBox.Text.Trim());
                }
                if (BankInfoTextBox.Text.Trim() != "")
                {
                    htInsertMoneyReceipt.Add("BANK_INFO", BankInfoTextBox.Text.Trim());
                }
                if (CashAmountTextBox.Text.Trim() != "")
                {
                    htInsertMoneyReceipt.Add("CASH_AMT", CashAmountTextBox.Text.Trim());
                }
                if (MultiplePayTypeTextBox.Text.Trim() != "")
                {
                    htInsertMoneyReceipt.Add("MULTI_PAY_REMARKS", MultiplePayTypeTextBox.Text.Trim());
                }

                if(sellingAgentCodeTextBox.Text != "")
                {
                    if (sellingAgentCodeTextBox.Text != "0")
                    {
                        if (sellingAgentCodeTextBox.Text == agentNameDDL.SelectedValue.ToString())
                        {
                            htInsertMoneyReceipt.Add("SELLING_AGENT_ID", agentNameDDL.SelectedValue.ToString());
                            htInsertMoneyReceipt.Add("SELLING_AGENT_NAME", agentNameDDL.SelectedItem.Text.ToString());
                        }
                    }
                }


                htInsertMoneyReceipt.Add("RECEIPT_ENTRY_BY", userObj.UserID.ToString());
                htInsertMoneyReceipt.Add("RECEIPT_ENTRY_DATE", DateTime.Today.ToString("dd-MMM-yyyy"));

                commonGatewayObj.Insert(htInsertMoneyReceipt, "MONEY_RECEIPT");
                commonGatewayObj.CommitTransaction();
                ClearText();
                Session["MONEY_RECEIPT_ID"] = money_Receipt_ID.ToString();
                Session["MONEY_RECEIPT_TYPE"] = "SL";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "window.open('ReportViewer/UnitReportMoneyReceiptReportViewer.aspx');", true);
            }

        }

        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            Session["MONEY_RECEIPT_ID"] = null;
            Session["MONEY_RECEIPT_TYPE"] = null;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save and print Failed');", true);
        }
    }

    protected void agentCodefindButton_Click(object sender, EventArgs e)
    {
        agentNameDDL.DataSource = unitSaleBLObj.dtSellingAgentInfoforDDL();
        agentNameDDL.DataTextField = "NAME";
        agentNameDDL.DataValueField = "ID";
        agentNameDDL.SelectedValue = sellingAgentCodeTextBox.Text.ToString();
        agentNameDDL.DataBind();
    }



    protected void agentNameDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        sellingAgentCodeTextBox.Text = agentNameDDL.SelectedValue.ToString();
    }
}
