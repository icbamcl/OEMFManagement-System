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
using System.Text.RegularExpressions;
using System.IO;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitDividendReconCDBLFileData : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();
    dividendDAO diviDAOObj = new dividendDAO();

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

   
    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClosingDateDropDownList.DataSource = diviDAOObj.dtGetFYWiseClosinDate(fundNameDropDownList.SelectedValue.ToString().ToUpper());
        ClosingDateDropDownList.DataTextField = "CLOSE_DT";
        ClosingDateDropDownList.DataValueField = "CLOSE_DT";
        ClosingDateDropDownList.DataBind();

    }
   


    protected void showDataButton_Click(object sender, EventArgs e)
    {
        try
        {
            
            bool U_Master = false;
            bool CDBLData = false;
            string message = "";
            DataTable dtU_Master = commonGatewayObj.Select(" SELECT * FROM U_MASTER WHERE REG_BK='"+ fundNameDropDownList.SelectedValue.ToString() + "' AND BALANCE>0 AND BO NOT IN (SELECT BO FROM CDBL_DATA WHERE FUND_CODE='" + fundNameDropDownList.SelectedValue.ToString() + "' AND RECORD_DATE='"+ClosingDateDropDownList.SelectedItem.Text.ToString()+ "' )");
            if(dtU_Master.Rows.Count>0)
            {
                grdShowUMasterDetails.DataSource = dtU_Master;
                grdShowUMasterDetails.DataBind();
                U_Master = true;

            }
            else
            {
                message = "No Data Found In U_MASTER Table ";
            }
            DataTable dtCDBLData = commonGatewayObj.Select(" SELECT * FROM CDBL_DATA WHERE FUND_CODE='" + fundNameDropDownList.SelectedValue.ToString() + "' AND RECORD_DATE='" +ClosingDateDropDownList.SelectedItem.Text.ToString() + "' AND BO NOT IN (SELECT BO FROM U_MASTER WHERE REG_BK='" + fundNameDropDownList.SelectedValue.ToString() + "' AND BO IS NOT NULL ) ");
            if (dtCDBLData.Rows.Count > 0)
            {
                grdShowCDBLDetails.DataSource = dtCDBLData;
                grdShowCDBLDetails.DataBind();
                CDBLData = true;
            }
            else
            {
                message = message + " No Data Found In CDBL Table ";
            }
            if(U_Master && CDBLData)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('"+ message + "');", true);
            }
            else if(U_Master|| CDBLData)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('" + message + "');", true);
            }
        }
        catch (Exception Ex)
        {
            string strMessage = string.Format("Data Retrive Failed! {0}", Ex.Message);
            strMessage = strMessage.Replace("\r\n", "");         
            ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", string.Format("window.fnAlert(\"{0}\");", strMessage), true);
        }
    }

  
   
    
  
}
