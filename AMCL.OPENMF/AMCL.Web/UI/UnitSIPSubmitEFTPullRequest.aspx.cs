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
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitSIPSubmitEFTPullRequest : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    BaseClass bcContent = new BaseClass();
    UnitSIPGetSet unitSIPObj = new UnitSIPGetSet();
    UnitSIPBL unitSIPBLObj = new UnitSIPBL();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
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
       // fundCodeTextBox.Text = fundCode.ToString();
       

        regObj.FundCode = fundCode;
        


        if (!IsPostBack)
        {
            sipDayDropDownList.DataSource = unitSIPBLObj.dtSIPDayListDDLforEFTPull(regObj.FundCode );
            sipDayDropDownList.DataTextField = "DEBIT_DATE";
            sipDayDropDownList.DataValueField = "ID";
            sipDayDropDownList.DataBind();

        }
       
    
    }
    
    
    private void ClearText()
    {
        fundAccNameLabel.Text = "";
        fundAccNoLabel.Text = "";
        BankNameLabel.Text = "";
        BranchNameLabel.Text = "";
        totalHolderLabel.Text = "";
        TotalAmountLabel.Text = "";

    }

    protected void sipDayDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dtSIPListforSchedule = unitSIPBLObj.dtSIPDetailsList(" AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.EFT_PULL_REQUEST_DATE IS NULL AND SIP_DETAILS.EFT_PULL_REQUEST_BY IS NULL AND SIP_DETAILS.DEBIT_DATE='" + sipDayDropDownList.SelectedValue.ToString() + "'");
        DataTable dtSIPListforScheduleTotal = unitSIPBLObj.dtSIPDetailsListTotal(" AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.EFT_PULL_REQUEST_DATE IS NULL AND SIP_DETAILS.EFT_PULL_REQUEST_BY IS NULL AND SIP_DETAILS.DEBIT_DATE='" + sipDayDropDownList.SelectedValue.ToString() + "'");
        if (dtSIPListforSchedule.Rows.Count > 0)
        {
            DataTable dtFundInfo = commonGatewayObj.Select("SELECT * FROM FUND_INFO WHERE FUND_CD='" + regObj.FundCode + "'");
            fundAccNameLabel.Text = dtFundInfo.Rows[0]["SIP_FUND_ACC_NAME"].ToString();
            fundAccNoLabel.Text = dtFundInfo.Rows[0]["SIP_FUND_ACC_NO"].ToString();

            DataTable dtBankBracnhInfo = unitHolderRegBLObj.dtGetBankBracnhInfo(dtFundInfo.Rows[0]["SIP_FUND_ACC_ROUTING_NO"].Equals(DBNull.Value) ? "0" : dtFundInfo.Rows[0]["SIP_FUND_ACC_ROUTING_NO"].ToString());
            string bankInfo = "";
            if (dtBankBracnhInfo.Rows.Count > 0)
            {
                BankNameLabel.Text = reportObj.getBankNameByBankCode(Convert.ToInt32(dtBankBracnhInfo.Rows[0]["BANK_CODE"].ToString())).ToString();
                bankInfo = reportObj.getBankBranchNameByCode(Convert.ToInt32(dtBankBracnhInfo.Rows[0]["BANK_CODE"].ToString()), Convert.ToInt32(dtBankBracnhInfo.Rows[0]["BRANCH_CODE"].ToString())).ToString() + "[" + dtFundInfo.Rows[0]["SIP_FUND_ACC_ROUTING_NO"] + "]";
                BranchNameLabel.Text = bankInfo;
            }
            totalHolderLabel.Text = dtSIPListforScheduleTotal.Rows[0]["TOTAL_HOLDER"].ToString();
            TotalAmountLabel.Text = dtSIPListforScheduleTotal.Rows[0]["TOTAL_AMOUNT"].ToString();

            SaleListGridView.DataSource = dtSIPListforSchedule;
            SaleListGridView.DataBind();
        }
        else
        {
            ClearText();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
           
        }
                  
       
    }


    protected void submitEFTPullButton_Click(object sender, EventArgs e)
    {
        DataTable dtSIPListforSchedule = unitSIPBLObj.dtSIPDetailsList(" AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.EFT_PULL_REQUEST_DATE IS NULL AND SIP_DETAILS.EFT_PULL_REQUEST_BY IS NULL AND SIP_DETAILS.DEBIT_DATE='" + sipDayDropDownList.SelectedValue.ToString() + "'");
        if (dtSIPListforSchedule.Rows.Count > 0)
        {
            unitSIPBLObj.SaveSIPEFTPullRequest(dtSIPListforSchedule, userObj);

            sipDayDropDownList.DataSource = unitSIPBLObj.dtSIPDayListDDLforEFTPull (regObj.FundCode);
            sipDayDropDownList.DataTextField = "DEBIT_DATE";
            sipDayDropDownList.DataValueField = "ID";
            sipDayDropDownList.DataBind();

            SaleListGridView.DataSource = unitSIPBLObj.dtSIPDetailsList(" AND 1=2 AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.EFT_PULL_REQUEST_DATE IS NULL AND SIP_DETAILS.EFT_PULL_REQUEST_BY IS NULL AND SIP_DETAILS.DEBIT_DATE='" + sipDayDropDownList.SelectedValue.ToString() + "'");           
            SaleListGridView.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('EFT Pull Request Successfully Submited');", true);

        }
        else
        {
            ClearText();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
        }
           
    }

   
}
