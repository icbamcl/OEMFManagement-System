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

public partial class UI_UnitMoneyReceiptEntryTransfer : System.Web.UI.Page
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
        string fundCode = bcContent.FundCode.ToString();
        string branchCode = bcContent.BranchCode.ToString();
        spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
      
                        
        
      
     
        if (!IsPostBack)
        {

            fundCodeDDL.DataSource = reportObj.dtFundCodeList();
            fundCodeDDL.DataTextField = "NAME";
            fundCodeDDL.DataValueField = "ID";
            fundCodeDDL.SelectedValue = fundCode.ToString();
            fundCodeDDL.DataBind();

            
            FundCodeDDL_I.DataSource = reportObj.dtFundCodeList();
            FundCodeDDL_I.DataTextField = "NAME";
            FundCodeDDL_I.DataValueField = "ID";
            FundCodeDDL_I.SelectedValue = fundCode.ToString();
            FundCodeDDL_I.DataBind();

            branchCodeDDL.DataSource = reportObj.dtBranchCodeList();
            branchCodeDDL.DataTextField = "NAME";
            branchCodeDDL.DataValueField = "ID";
            branchCodeDDL.SelectedValue = branchCode.ToString();
            branchCodeDDL.DataBind();

            BranchCodeDDL_I.DataSource = reportObj.dtBranchCodeList();
            BranchCodeDDL_I.DataTextField = "NAME";
            BranchCodeDDL_I.DataValueField = "ID";
            BranchCodeDDL_I.SelectedValue = branchCode.ToString();
            BranchCodeDDL_I.DataBind();

            regObj.FundCode = fundCode.ToString();
            regObj.BranchCode = branchCode.ToString();

            ReceiptDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            ReceiptNoTextBox.Text = unitSaleBLObj.GetMaxReceiptNo(regObj, "TR").ToString();
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

        FundCodeDDL_I.DataSource = reportObj.dtFundCodeList();
        FundCodeDDL_I.DataTextField = "NAME";
        FundCodeDDL_I.DataValueField = "ID";
        FundCodeDDL_I.SelectedValue = fundCode.ToString();
        FundCodeDDL_I.DataBind();

        BranchCodeDDL_I.DataSource = reportObj.dtBranchCodeList();
        BranchCodeDDL_I.DataTextField = "NAME";
        BranchCodeDDL_I.DataValueField = "ID";
        BranchCodeDDL_I.SelectedValue = branchCode.ToString();
        BranchCodeDDL_I.DataBind();

        regObj.FundCode = fundCode.ToString();
        regObj.BranchCode = branchCode.ToString();

        ReceiptDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        ReceiptNoTextBox.Text = unitSaleBLObj.GetMaxReceiptNo(regObj, "TR").ToString();

        regNoTextBox.Text = "";
        holderBOTextBox.Text = "";
        NameTextBox.Text = "";
        addressTextBox.Text = "";
        QtyTextBox.Text = "";        
        SaleTRRNNoTextBox.Text = "";

        RegNoTextBox_I.Text = "";
        HolderBOTextBox_I.Text = "";
        NameTextBox_I.Text = "";
        addressTextBox_I.Text = "";

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
            string string2 = dtRegInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["ADDRS2"].ToString();
            string string3 = dtRegInfo.Rows[0]["CITY"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["CITY"].ToString();
            addressTextBox.Text = string1.ToString() + " " + string2 + " " + string3;
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

    protected void findButton_I_Click(object sender, EventArgs e)
    {

        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = FundCodeDDL_I.SelectedValue.ToString();
        regObj.BranchCode = BranchCodeDDL_I.SelectedValue.ToString();
        regObj.RegNumber = RegNoTextBox_I.Text.Trim();
        regObj.BO = HolderBOTextBox_I.Text.ToString().Trim();

        DataTable dtValidSearch = opendMFDAO.dtValidSearch(regObj);
        if (dtValidSearch.Rows.Count > 0)
        {
            regObj = new UnitHolderRegistration();
            regObj.FundCode = FundCodeDDL_I.SelectedValue.ToString();
            regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
            regObj.RegNumber = dtValidSearch.Rows[0]["REG_NO"].ToString();
            DataTable dtRegInfo = opendMFDAO.getDtRegInfo(regObj);

            RegNoTextBox_I.Text = dtRegInfo.Rows[0]["REG_NO"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["REG_NO"].ToString();
            HolderBOTextBox_I.Text = dtRegInfo.Rows[0]["BO"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["BO"].ToString();
            NameTextBox_I.Text = dtRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["HNAME"].ToString();
            string string1 = dtRegInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["ADDRS1"].ToString();
            string string2 = dtRegInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["ADDRS2"].ToString();
            string string3 = dtRegInfo.Rows[0]["CITY"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["CITY"].ToString();
            addressTextBox_I.Text = string1.ToString() + " " + string2 + " " + string3;
        }
        else
        {
            RegNoTextBox_I.Text = "";
            HolderBOTextBox_I.Text = "";
            NameTextBox_I.Text = "";
            addressTextBox_I.Text = "";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Registration Number OR BO ');", true);
        }
    }

    protected void fundCodeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {

        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
        regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
        ReceiptNoTextBox.Text = unitSaleBLObj.GetMaxReceiptNo(regObj, "TR").ToString();
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

            if (unitSaleBLObj.CheckDuplicateMoneyReceiptNo(regObj, "TR", Convert.ToInt64(ReceiptNoTextBox.Text.Trim())))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " " + "Receipt Number " + "');", true);
            }
            else
            {
                money_Receipt_ID = commonGatewayObj.GetMaxNo("MONEY_RECEIPT", "ID") + 1;
                commonGatewayObj.BeginTransaction();
                htInsertMoneyReceipt.Add("ID", money_Receipt_ID);
                htInsertMoneyReceipt.Add("RECEIPT_NO", ReceiptNoTextBox.Text.Trim());
                htInsertMoneyReceipt.Add("RECEIPT_TYPE", "TR");
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


                htInsertMoneyReceipt.Add("REG_BK_I", fundCodeDDL.SelectedValue.ToString());
                htInsertMoneyReceipt.Add("REG_BR_I", branchCodeDDL.SelectedValue.ToString());
                htInsertMoneyReceipt.Add("REG_NO_I", regNoTextBox.Text.Trim());

                if (holderBOTextBox.Text.Trim() != "")
                {
                    htInsertMoneyReceipt.Add("BO_I", holderBOTextBox.Text.Trim());
                }
                htInsertMoneyReceipt.Add("HNAME_I", NameTextBox.Text.Trim());
                htInsertMoneyReceipt.Add("ADDRESS_I", addressTextBox.Text.Trim());
               

                htInsertMoneyReceipt.Add("UNIT_QTY", QtyTextBox.Text.Trim());
                htInsertMoneyReceipt.Add("TR_CERT_DELIVERY_DT", DeliveryDateTextBox.Text.Trim());
                htInsertMoneyReceipt.Add("SL_TR_RN_NO", SaleTRRNNoTextBox.Text.Trim().ToUpper());               
                htInsertMoneyReceipt.Add("RECEIPT_ENTRY_BY", userObj.UserID.ToString());
                htInsertMoneyReceipt.Add("RECEIPT_ENTRY_DATE", DateTime.Today.ToString("dd-MMM-yyyy"));

                commonGatewayObj.Insert(htInsertMoneyReceipt, "MONEY_RECEIPT");
                commonGatewayObj.CommitTransaction();
                ClearText();
                Session["MONEY_RECEIPT_ID"] = money_Receipt_ID.ToString();
                Session["MONEY_RECEIPT_TYPE"] = "REP";
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

   
}
