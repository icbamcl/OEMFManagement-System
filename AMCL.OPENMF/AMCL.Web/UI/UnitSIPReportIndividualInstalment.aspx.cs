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
using System.IO;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitSIPReportIndividualInstalment : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
    BaseClass bcContent = new BaseClass();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    UnitSIPBL unitSIPBLObj = new UnitSIPBL();
    string CDSStatus = "";

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
       
        CDSStatus = bcContent.CDS.ToString().ToUpper();


    
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

            sipNumberDropDownList.DataSource = unitSIPBLObj.dtSIPListDDLForStatement("AND REG_BK = '" + fundCode + "'");
            sipNumberDropDownList.DataTextField = "SIP_NO";
            sipNumberDropDownList.DataValueField = "ID";
            sipNumberDropDownList.DataBind();


        }

    }
   

    

    private void ClearText()
    {
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        string fundCode = bcContent.FundCode.ToString();
        string branchCode = bcContent.BranchCode.ToString();
        fundCodeDDL.SelectedValue = fundCode.ToString();
       
    }
    protected void regCloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");

    }


    protected void ShowReportButton_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sbFilter = new StringBuilder();
            sbFilter.Append(" ");
            if(unpaidRadioButton.Checked)
            {
                sbFilter.Append (" AND SIP_DETAILS.EFT_PAID_STATUS='P' ");
            }
            else if(paidRadioButton.Checked)
            {
                sbFilter.Append(" AND SIP_DETAILS.EFT_PAID_STATUS IS NULL ");
            }
            DataTable dtSIPDetails = unitSIPBLObj.dtSIPDetailsList(" AND SIP_DETAILS.REG_BK='" + fundCodeDDL.SelectedValue.ToString() + "' AND SIP_DETAILS.SIP_NO=" + Convert.ToInt64(sipNumberDropDownList.SelectedItem.Text.ToString())+" "+sbFilter.ToString());
            string reportType = "SIP_SCHEDULE";
            
            if (dtSIPDetails.Rows.Count > 0)
            {
                Session["reportType"] = reportType;
                Session["dtSIPMaster"] = dtSIPDetails;
               

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "window.open('ReportViewer/UnitReportSIPAcknowlodegeReportViewer.aspx');", true);
            }
            else
            {
                Session["reportType"] = null;
                Session["dtSIPMaster"] = null;
               
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
            }
        }
        catch (Exception ex)
        {
         
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + ex.Message.Replace("'", "").ToString() + "');", true);
        }

    }




    protected void findButton_Click(object sender, EventArgs e)
    {

        sipNumberDropDownList.DataSource = unitSIPBLObj.dtSIPListDDLForStatement("AND REG_BK = '" + regObj.FundCode + "' AND REG_BR = '" + regObj.BranchCode + "' AND REG_NO=" + regObj.RegNumber);
        sipNumberDropDownList.DataTextField = "SIP_NO";
        sipNumberDropDownList.DataValueField = "ID";
        sipNumberDropDownList.DataBind();
    }
}
