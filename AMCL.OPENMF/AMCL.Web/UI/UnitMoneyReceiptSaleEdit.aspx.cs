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

public partial class UI_UnitMoneyReceiptSaleEdit : System.Web.UI.Page
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

            regObj.FundCode = fundCode.ToString();
            regObj.BranchCode = branchCode.ToString();

            moneyReceipDropDownList.DataSource = unitSaleBLObj.dtMoneyRecieptforDDL(" AND REG_BK = '" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR = '" + regObj.BranchCode.ToString().ToUpper() + "'AND RECEIPT_TYPE = 'SL' AND SL_REP_TR_RN_NO IS NULL  AND ACC_VOUCHER_NO IS NULL  ORDER BY RECEIPT_NO DESC ");
            moneyReceipDropDownList.DataTextField = "RECEIPT_NO";
            moneyReceipDropDownList.DataValueField = "ID";
            moneyReceipDropDownList.DataBind();

            agentNameDDL.DataSource = unitSaleBLObj.dtSellingAgentInfoforDDL();
            agentNameDDL.DataTextField = "NAME";
            agentNameDDL.DataValueField = "ID";
            agentNameDDL.SelectedValue = "0";
            agentNameDDL.DataBind();
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
        moneyReceipDropDownList.SelectedValue="0";

        regNoTextBox.Text = "";
        holderBOTextBox.Text = "";
        NameTextBox.Text= "";
        addressTextBox.Text = "";
        QtyTextBox.Text = "";
        RateTextBox.Text = "";
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
    //protected void findButton_Click(object sender, EventArgs e)
    //{


    //    UnitHolderRegistration regObj = new UnitHolderRegistration();
    //    regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
    //    regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
    //    regObj.RegNumber = regNoTextBox.Text.Trim();
    //    regObj.BO = holderBOTextBox.Text.ToString().Trim();
       
    //    DataTable dtValidSearch = opendMFDAO.dtValidSearch(regObj);
    //    if (dtValidSearch.Rows.Count > 0)
    //    {
    //        regObj = new UnitHolderRegistration();
    //        regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
    //        regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
    //        regObj.RegNumber = dtValidSearch.Rows[0]["REG_NO"].ToString();
    //        DataTable dtRegInfo = opendMFDAO.getDtRegInfo(regObj);
           
    //        regNoTextBox.Text = dtRegInfo.Rows[0]["REG_NO"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["REG_NO"].ToString();
    //        holderBOTextBox.Text = dtRegInfo.Rows[0]["BO"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["BO"].ToString();

    //        NameTextBox.Text = dtRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["HNAME"].ToString();
    //        string string1 = dtRegInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["ADDRS1"].ToString();
    //        string string2= dtRegInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["ADDRS2"].ToString();
    //        string string3 = dtRegInfo.Rows[0]["CITY"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["CITY"].ToString();
    //        addressTextBox.Text = string1.ToString()+ " "+string2+" " + string3;
    //    }
    //    else
    //    {
    //        regNoTextBox.Text = "";
    //        holderBOTextBox.Text = "";
    //        NameTextBox.Text = "";
    //        addressTextBox.Text = "";
    //      ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Registration Number OR BO ');", true);
    //    }


    //}

    protected void fundCodeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
        regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
        moneyReceipDropDownList.DataSource = unitSaleBLObj.dtMoneyRecieptforDDL(" AND REG_BK = '" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR = '" + regObj.BranchCode.ToString().ToUpper() + "'AND RECEIPT_TYPE = 'SL' AND SL_REP_TR_RN_NO IS NULL AND VALID IS NULL AND ACC_VOUCHER_NO IS NULL ORDER BY RECEIPT_NO DESC ");
        moneyReceipDropDownList.DataTextField = "RECEIPT_NO";
        moneyReceipDropDownList.DataValueField = "ID";
        moneyReceipDropDownList.DataBind();

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
        Hashtable htUpdateMoneyReceipt = new Hashtable();
        try
        {
            regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
            regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();

            money_Receipt_ID = commonGatewayObj.GetMaxNo("MONEY_RECEIPT", "ID") + 1;
            commonGatewayObj.BeginTransaction();
           
            htUpdateMoneyReceipt.Add("VALID", "N");
            htUpdateMoneyReceipt.Add("RECEIPT_EDIT_TYPE","E");
            htUpdateMoneyReceipt.Add("RECEIPT_EDIT_BY", userObj.UserID.ToString());
            htUpdateMoneyReceipt.Add("RECEIPT_EDIT_DATE", DateTime.Today.ToString("dd-MMM-yyyy"));

            commonGatewayObj.Update(htUpdateMoneyReceipt, "MONEY_RECEIPT", "ID=" + moneyReceipDropDownList.SelectedValue.ToString());


            htInsertMoneyReceipt.Add("ID", money_Receipt_ID);
            htInsertMoneyReceipt.Add("RECEIPT_NO", moneyReceipDropDownList.SelectedItem.Text);
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
            if (sellingAgentCodeTextBox.Text != "")
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
            moneyReceipDropDownList.DataSource = unitSaleBLObj.dtMoneyRecieptforDDL(" AND REG_BK = '" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR = '" + regObj.BranchCode.ToString().ToUpper() + "'AND RECEIPT_TYPE = 'SL' AND SL_REP_TR_RN_NO IS NULL AND ACC_VOUCHER_NO IS NULL ORDER BY RECEIPT_NO DESC ");
            moneyReceipDropDownList.DataTextField = "RECEIPT_NO";
            moneyReceipDropDownList.DataValueField = "ID";
            moneyReceipDropDownList.DataBind();
            Session["MONEY_RECEIPT_ID"] = money_Receipt_ID.ToString();
            Session["MONEY_RECEIPT_TYPE"] = "SL";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "window.open('ReportViewer/UnitReportMoneyReceiptReportViewer.aspx');", true);
          
        }

        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            Session["MONEY_RECEIPT_ID"] = null;
            Session["MONEY_RECEIPT_TYPE"] = null;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Edit and print Failed');", true);
        }
    }

    protected void moneyReceipDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtMoneyReceitInfoDetails = unitSaleBLObj.dtMoneyRecieptInfoDetails(Convert.ToInt64(moneyReceipDropDownList.SelectedValue.ToString()));
        if (dtMoneyReceitInfoDetails.Rows.Count > 0)
        {
            regNoTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["REG_NO"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["REG_NO"].ToString();
            holderBOTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["BO"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["BO"].ToString();
            ReceiptDateTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["RECEIPT_DATE"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtMoneyReceitInfoDetails.Rows[0]["RECEIPT_DATE"].ToString()).ToString("dd-MMM-yyyy");
            NameTextBox.Text= dtMoneyReceitInfoDetails.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["HNAME"].ToString();
            addressTextBox.Text =dtMoneyReceitInfoDetails.Rows[0]["ADDRESS"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["ADDRESS"].ToString();


            RateTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["RATE"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["RATE"].ToString();
            QtyTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["UNIT_QTY"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["UNIT_QTY"].ToString();
            CashAmountTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["CASH_AMT"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["CASH_AMT"].ToString();
            MultiplePayTypeTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["MULTI_PAY_REMARKS"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["MULTI_PAY_REMARKS"].ToString();
            RoutingNoTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["ROUTING_NO"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["ROUTING_NO"].ToString();
            sellingAgentCodeTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["SELLING_AGENT_ID"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["SELLING_AGENT_ID"].ToString();

            if (!dtMoneyReceitInfoDetails.Rows[0]["SELLING_AGENT_ID"].Equals(DBNull.Value))
            {
                agentNameDDL.Enabled = true;
                sellingAgentCodeTextBox.Enabled = true;
                agentNameDDL.DataSource = unitSaleBLObj.dtSellingAgentInfoforDDL();
                agentNameDDL.DataTextField = "NAME";
                agentNameDDL.DataValueField = "ID";
                agentNameDDL.SelectedValue = dtMoneyReceitInfoDetails.Rows[0]["SELLING_AGENT_ID"].ToString();
                agentNameDDL.DataBind();
               
            }
           else
            {
                agentNameDDL.Enabled = true;
                sellingAgentCodeTextBox.Enabled = true;
                agentNameDDL.DataSource = unitSaleBLObj.dtSellingAgentInfoforDDL();
                agentNameDDL.DataTextField = "NAME";
                agentNameDDL.DataValueField = "ID";
                agentNameDDL.SelectedValue = "0";
                agentNameDDL.DataBind();
            }

            if (!dtMoneyReceitInfoDetails.Rows[0]["CHQ_TYPE"].Equals(DBNull.Value))
            {


                string chqType = dtMoneyReceitInfoDetails.Rows[0]["CHQ_TYPE"].ToString();
                if (chqType.ToString().ToUpper() == "CHQ" || chqType.ToString().ToUpper() == "DD" || chqType.ToString().ToUpper() == "PO")
                {
                   
                    DataTable dtBankInfo = regBLObj.dtGetBankBracnhInfo(dtMoneyReceitInfoDetails.Rows[0]["ROUTING_NO"].ToString());
                    ChequeTypeDropDownList.SelectedValue = dtMoneyReceitInfoDetails.Rows[0]["CHQ_TYPE"].ToString();
                    CHQDDPONOTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["CHQ_DD_NO"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["CHQ_DD_NO"].ToString();
                    chequeDateTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["CHQ_DD_DATE"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtMoneyReceitInfoDetails.Rows[0]["CHQ_DD_DATE"].ToString()).ToString("dd-MMM-yyyy");

                    string bankInfo = "";
                    if (dtBankInfo.Rows.Count > 0)
                    {
                        bankInfo = bankInfo + reportObj.getBankNameByBankCode(Convert.ToInt32(dtBankInfo.Rows[0]["BANK_CODE"].ToString())).ToString();
                        bankInfo = bankInfo + " , " + reportObj.getBankBranchNameByCode(Convert.ToInt32(dtBankInfo.Rows[0]["BANK_CODE"].ToString()), Convert.ToInt32(dtBankInfo.Rows[0]["BRANCH_CODE"].ToString())).ToString() + " BRANCH ";
                        BankInfoTextBox.Text = bankInfo.ToString();

                    }
                    else
                    {
                        BankInfoTextBox.Text = "";
                       
                    }
                   
                    //ChequeTypeDropDownList.SelectedValue = "CHQ";
                }
                else
                {
                    CHQDDPONOTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["CHQ_DD_NO"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["CHQ_DD_NO"].ToString();
                    chequeDateTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["CHQ_DD_DATE"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtMoneyReceitInfoDetails.Rows[0]["CHQ_DD_DATE"].ToString()).ToString("dd-MMM-yyyy");
                    ChequeTypeDropDownList.SelectedValue = "CHQ";
                }
            }
            else 
            {
                CHQDDPONOTextBox.Text = "";
                chequeDateTextBox.Text = "";
                BankInfoTextBox.Text = "";
            }
            if (!dtMoneyReceitInfoDetails.Rows[0]["PAY_TYPE"].Equals(DBNull.Value))
            {
                string chqType = dtMoneyReceitInfoDetails.Rows[0]["CHQ_TYPE"].ToString();
                if (chqType.ToString().ToUpper() == "CHQ")
                {
                    ChqRadioButton.Checked = true;
                }
                else if (chqType.ToString().ToUpper() == "CASH")
                {
                    CashRadioButton.Checked = true;
                }
                else if (chqType.ToString().ToUpper() == "BOTH")
                {
                    BothRadioButton.Checked = true;
                }
                else if (chqType.ToString().ToUpper() == "MULT")
                {
                    MultiRadioButton.Checked = true;
                }

            }


        }
        else
        {
            ClearText();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Money Receipt Number');", true);
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

            moneyReceipDropDownList.DataSource = unitSaleBLObj.dtMoneyRecieptforDDL(" AND REG_BK = '" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR = '" + regObj.BranchCode.ToString().ToUpper() + "'AND RECEIPT_TYPE = 'SL' AND SL_REP_TR_RN_NO IS NULL  NULL AND ACC_VOUCHER_NO IS NULL ORDER BY RECEIPT_NO DESC ");
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
}
