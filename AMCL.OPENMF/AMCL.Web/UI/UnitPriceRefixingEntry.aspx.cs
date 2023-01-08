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

public partial class UI_UnitPriceRefixingEntry : System.Web.UI.Page
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
        Hashtable htPriceRefixation = new Hashtable();
        try
        {
            commonGatewayObj.BeginTransaction();
            htPriceRefixation.Add("FUND_CD", fundNameDropDownList.SelectedValue.ToString().ToUpper());
            htPriceRefixation.Add("FUND_NM", opendMFDAO.GetFundName(fundNameDropDownList.SelectedValue.ToString()));
            htPriceRefixation.Add("REFIX_DT", Convert.ToDateTime(PriceDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
            htPriceRefixation.Add("REFIX_SL_PR", Convert.ToDecimal(SalePriceTextBox.Text.Trim().ToString()));
            htPriceRefixation.Add("REFIX_REP_PR", Convert.ToDecimal(RepPriceTextBox.Text.Trim().ToString()));
            htPriceRefixation.Add("USER_NM", userObj.UserID.ToString());
            htPriceRefixation.Add("ENT_DT", DateTime.Today.ToString("dd-MMM-yyyy"));
            htPriceRefixation.Add("ENT_TM", DateTime.Now.ToShortTimeString());
            commonGatewayObj.Insert(htPriceRefixation,"PRICE_REFIX");
            commonGatewayObj.CommitTransaction();

            PriceDateTextBox.Text = "";
            SalePriceTextBox.Text = "";
            RepPriceTextBox.Text = "";
            fundNameDropDownList.Focus();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert('" + msgObj.Success().ToString() + "');", true);
        }
        catch(Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + ex.Message.Replace("'", "").ToString() + "');", true);
        }

    }
    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtLastPrice = opendMFDAO.dtgetLastPrice(fundNameDropDownList.SelectedValue.ToString());
        if (dtLastPrice.Rows.Count > 0)
        {
            PriceDateTextBox.Text =Convert.ToDateTime( dtLastPrice.Rows[0]["REFIX_DT"]).ToString("dd-MMM-yyyy");
            SalePriceTextBox.Text =dtLastPrice.Rows[0]["REFIX_SL_PR"].ToString();
            RepPriceTextBox.Text = dtLastPrice.Rows[0]["REFIX_REP_PR"].ToString();

        }
        else
        {
            PriceDateTextBox.Text = "";
            SalePriceTextBox.Text = "";
            RepPriceTextBox.Text = "";
            fundNameDropDownList.Focus();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
        }
        
    }
    //protected void SalePriceTextBox_TextChanged(object sender, EventArgs e)
    //{
    //    //RepPriceTextBox.Text = Convert.ToString(Convert.ToInt32(SalePriceTextBox.Text.Trim().ToString())-5);
    //}
}
