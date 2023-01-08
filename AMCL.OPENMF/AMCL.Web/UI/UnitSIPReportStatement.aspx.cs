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

public partial class UI_UnitSIPReportStatement : System.Web.UI.Page
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

            if (fromSIPNoTextBox.Text != "" && toSIPNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (SIP_MASTER.SIP_NO BETWEEN " + Convert.ToInt32(fromSIPNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toSIPNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromSIPNoTextBox.Text != "" && toSIPNoTextBox.Text == "")
            {
                sbFilter.Append(" AND (SIP_MASTER.SIP_NO >=" + Convert.ToInt32(fromSIPNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromSIPNoTextBox.Text == "" && toSIPNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (SIP_MASTER.SIP_NO <=" + Convert.ToInt32(toSIPNoTextBox.Text.Trim().ToString()) + ")");
            }

            DataTable dtSIPMaster = unitSIPBLObj.dtSIPMasterList(" AND SIP_MASTER.REG_BK='" + fundCodeDDL.SelectedValue.ToString() + "' "+sbFilter.ToString());
            string reportType = "ACK_STATE";
            if(stateSIPRadioButton.Checked)
            {
                reportType = "SIP_STATE";
            }
            if (dtSIPMaster.Rows.Count > 0)
            {
                Session["reportType"] = reportType;
                Session["dtSIPMaster"] = dtSIPMaster;
               

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


  
}
