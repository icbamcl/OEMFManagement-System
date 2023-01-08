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

public partial class UI_UnitRepurchaseBEFTNPostingAccount : System.Web.UI.Page
{
    //System.Web.UI.Page this_page_ref = null;
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();    
    UnitUser userObj = new UnitUser();
    
    UnitRepurchaseBL unitRepBLObj = new UnitRepurchaseBL();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
    BaseClass bcContent = new BaseClass();
  
   
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
        //spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
                                       
        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = opendMFDAO.dtFundList();
            fundNameDropDownList.DataTextField = "FUND_NM";
            fundNameDropDownList.DataValueField = "FUND_CD";
            fundNameDropDownList.DataBind();

            BEFTNDateDropDownList.DataSource = unitRepBLObj.dtBEFTNDateList("0");
            BEFTNDateDropDownList.DataTextField = "NAME";
            BEFTNDateDropDownList.DataValueField = "ID";
            BEFTNDateDropDownList.DataBind();
                      
           
        }
    
    }



    
    protected void AdviceButton_Click(object sender, EventArgs e)
    {

        DataTable dtBEFTNAdviceData = unitRepBLObj.dtGetBEFTNAdviceData("AND B.BEFTN_TRACKING_NO IS NULL AND B.BEFTN_DATE='" + BEFTNDateDropDownList.SelectedValue.ToString() + "' AND A.FUND_CD='" + fundNameDropDownList.SelectedValue.ToString()+"'");

        if (dtBEFTNAdviceData.Rows.Count > 0)
        {
            try
            {

                long beftnTrackingNumber = commonGatewayObj.GetMaxNo("REPURCHASE", "BEFTN_TRACKING_NO") + 1;
                commonGatewayObj.BeginTransaction();
                Hashtable htUpdate = new Hashtable();
                htUpdate.Add("BEFTN_TRACKING_NO", beftnTrackingNumber);
                commonGatewayObj.Update(htUpdate, "REPURCHASE ", "REG_BK='" + fundNameDropDownList.SelectedValue.ToString() + "' AND  BEFTN_TRACKING_NO IS NULL AND BEFTN_DATE='" + BEFTNDateDropDownList.SelectedValue.ToString() + "' ");
                commonGatewayObj.CommitTransaction();

               // Session["dtBEFTNAdviceData"] = dtBEFTNAdviceData;
                Session["BEFTN_DATE"] = BEFTNDateDropDownList.SelectedValue.ToString();
                Session["BEFTN_TRACKING_NO"] = beftnTrackingNumber.ToString();
                Session["FUND_CD"] = fundNameDropDownList.SelectedValue.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "UnitReportTaxCert", "window.open('ReportViewer/UnitReportRepurchaseBEFTNAdviceLetterReportViewer.aspx')", true);
            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert ('Print Fail:" + ex.Message.Replace("'", "").ToString() + "');", true);
            }
        }
        else
        {
            Session["BEFTN_DATE"] = null;
            Session["BEFTN_TRACKING_NO"] = null;
            Session["FUND_CD"] = null;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
        }
        
    }
    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BEFTNDateDropDownList.DataSource = unitRepBLObj.dtBEFTNDateList(fundNameDropDownList.SelectedValue.ToString());
        BEFTNDateDropDownList.DataTextField = "NAME";
        BEFTNDateDropDownList.DataValueField = "ID";
        BEFTNDateDropDownList.DataBind();

    }
}
