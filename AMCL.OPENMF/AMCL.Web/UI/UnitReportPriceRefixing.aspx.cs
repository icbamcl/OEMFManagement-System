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
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitReportPriceRefixing : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();
    dividendDAO diviDAOObj = new dividendDAO();
    Message msgObj = new Message();

    protected void Page_Load(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
       
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        
       // spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
//            fundCodeTextBox.Text = fundCode.ToString();
//            branchCodeTextBox.Text = branchCode.ToString();

        
        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = opendMFDAO.dtFundList();
            fundNameDropDownList.DataTextField = "FUND_NM";
            fundNameDropDownList.DataValueField = "FUND_CD";
            fundNameDropDownList.DataBind();

                         
        }
    
    }
   
    
    protected void CloseButton_Click(object sender, EventArgs e)
    {         
        Response.Redirect("UnitHome.aspx");
           
    }



    protected void SaveButton_Click(object sender, EventArgs e)
    {
        StringBuilder sbQuery = new StringBuilder();
        sbQuery.Append("SELECT * FROM PRICE_REFIX WHERE FUND_CD='"+ fundNameDropDownList.SelectedValue.ToString()+"'");
        if (fromPriceDateTextBox.Text != "" && toPriceDateTextBox.Text == "")
        {
            sbQuery.Append("AND REFIX_DT>='" + fromPriceDateTextBox.Text.Trim().ToString() + "'");
        }
        else if (fromPriceDateTextBox.Text == "" && toPriceDateTextBox.Text != "")
        {
            sbQuery.Append("AND REFIX_DT>='" + toPriceDateTextBox.Text.Trim().ToString() + "'");
        }
        else if (fromPriceDateTextBox.Text != "" && toPriceDateTextBox.Text != "")
        {
            sbQuery.Append("AND REFIX_DT BETWEEN '" + fromPriceDateTextBox.Text.Trim().ToString() + "' AND  '" + toPriceDateTextBox.Text.Trim().ToString() + "'");
        }
        sbQuery.Append(" ORDER BY REFIX_DT DESC");

        DataTable dtPriceInfo = commonGatewayObj.Select(sbQuery.ToString());
        if (dtPriceInfo.Rows.Count > 0)
        {
            Session["dtPriceInfo"] = dtPriceInfo;
            Session["fundCode"] = fundNameDropDownList.SelectedValue.ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "UnitHolderInfo", "window.open('ReportViewer/UnitReportPriceRefixationReportViewer.aspx')", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('No data found');", true);
        }
    }
}
