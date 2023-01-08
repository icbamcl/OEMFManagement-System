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
using System.IO;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;


public partial class UI_UnitDividendSearchOneToEight : System.Web.UI.Page
{
   // System.Web.UI.Page this_page_ref = null;
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();
    UnitLienBl unitLienBLObj = new UnitLienBl();
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
            
       
      
        if (!IsPostBack)
        {


            fundCodeDDL.DataSource = reportObj.dtFundCodeList();
            fundCodeDDL.DataTextField = "NAME";
            fundCodeDDL.DataValueField = "ID";
            fundCodeDDL.SelectedValue = fundCode.ToString();
            fundCodeDDL.DataBind();
                                 
        }
    
    }
    
    
   
    protected void findButton_Click(object sender, EventArgs e)
    {

        DataTable dtDividendInfo = new DataTable();
        string fund_Code = fundCodeDDL.SelectedValue.ToString();
        int fund_No = Convert.ToInt16(fund_Code.Substring(3, 1).ToString());
        if(BOTextBox.Text.Trim() != "")
        {
            dtDividendInfo = commonGatewayObj.Select("SELECT * FROM AMCL_DIVIDEND.CDS_DIV_WAR WHERE FOLIO='"+ BOTextBox.Text.Trim() + "' AND FUND_NO=" + fund_No);
        }
        else if(folioTextBox.Text.Trim()!="")
        {
            dtDividendInfo = commonGatewayObj.Select("SELECT * FROM AMCL_DIVIDEND.DIV_WAR WHERE FOLIO="+ folioTextBox.Text.Trim() + " AND FUND_NO=" + fund_No);
        }

        if (dtDividendInfo.Rows.Count > 0)
        {
            dvLedger.Visible = true;
            holderNameTextBox.Text = dtDividendInfo.Rows[0]["NAME1"].Equals(DBNull.Value) ? "" : dtDividendInfo.Rows[0]["NAME1"].ToString();
            holderAddress1TextBox.Text = dtDividendInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value) ? "" : dtDividendInfo.Rows[0]["ADDRS1"].ToString();
            holderAddress2TextBox.Text = dtDividendInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtDividendInfo.Rows[0]["ADDRS2"].ToString();
            holderAddress3TextBox.Text = dtDividendInfo.Rows[0]["ADDRS3"].Equals(DBNull.Value) ? "" : dtDividendInfo.Rows[0]["ADDRS3"].ToString();

            dgLedger.DataSource = dtDividendInfo;
            dgLedger.DataBind();

        }
        else
        {
            holderNameTextBox.Text = "";
            holderAddress1TextBox.Text = "";
            holderAddress2TextBox.Text = "";
            holderAddress3TextBox.Text = "";
            dvLedger.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
        }


    }
   
   



   
   


  
}
