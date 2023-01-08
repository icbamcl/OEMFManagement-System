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

public partial class UI_UnitRepurchaseCHEQUEAudit : System.Web.UI.Page
{
    System.Web.UI.Page this_page_ref = null;
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
       
                                       
        if (!IsPostBack)
        {


            fundNameDropDownList.DataSource = opendMFDAO.dtFundList();
            fundNameDropDownList.DataTextField = "FUND_NM";
            fundNameDropDownList.DataValueField = "FUND_CD";
            fundNameDropDownList.DataBind();

            DataTable dtChequeData = unitRepBLObj.dtGetChequePaymentData(" AND A.AUDITED_BY IS  NULL");
            SurrenderListGridView.DataSource = dtChequeData;
            SurrenderListGridView.DataBind();
            

        }
    
    }



    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (fundNameDropDownList.SelectedValue.ToString() != "0")
        {
            DataTable dtChequeData = unitRepBLObj.dtGetChequePaymentData("AND A.AUDITED_BY IS  NULL AND FUND_INFO.FUND_CD='" + fundNameDropDownList.SelectedValue.ToString() + "'");
            SurrenderListGridView.DataSource = dtChequeData;
            SurrenderListGridView.DataBind();
        }
        else
        {
            DataTable dtChequeData = unitRepBLObj.dtGetChequePaymentData("AND A.AUDITED_BY IS  NULL ");
            SurrenderListGridView.DataSource = dtChequeData;
            SurrenderListGridView.DataBind();
           
        }
        
    }


    protected void AuditButton_Click(object sender, EventArgs e)
    {
        int countCheck = 0;
        Hashtable htUpdate = new Hashtable();
        try
        {
            commonGatewayObj.BeginTransaction();
            foreach (GridViewRow Drv in SurrenderListGridView.Rows)
            {

                CheckBox leftCheckBox = (CheckBox)SurrenderListGridView.Rows[countCheck].FindControl("leftCheckBox");

                if (leftCheckBox.Checked)
                {
                    htUpdate = new Hashtable();                   
                    htUpdate.Add("AUDITED_BY", userObj.UserID.ToString());
                    htUpdate.Add("AUDITED_DATE", DateTime.Now);
                    commonGatewayObj.Update(htUpdate, "REPURCHASE ", "REG_BK='" + Drv.Cells[2].Text.ToUpper().ToString() + "' AND REG_BR='" + Drv.Cells[3].Text.ToUpper().ToString() + "' AND REG_NO=" + Convert.ToInt32(Drv.Cells[4].Text.ToString()) + " AND REP_NO=" + Convert.ToInt32(Drv.Cells[5].Text.ToString()));

                }

                countCheck++;

            }
            commonGatewayObj.CommitTransaction();
            if (fundNameDropDownList.SelectedValue.ToString() != "0")
            {
                DataTable dtChequeData = unitRepBLObj.dtGetChequePaymentData("AND A.AUDITED_BY IS  NULL AND FUND_INFO.FUND_CD='" + fundNameDropDownList.SelectedValue.ToString() + "'");
                SurrenderListGridView.DataSource = dtChequeData;
                SurrenderListGridView.DataBind();
            }
            else
            {
                DataTable dtChequeData = unitRepBLObj.dtGetChequePaymentData("AND A.AUDITED_BY IS  NULL ");
               
                    SurrenderListGridView.DataSource = dtChequeData;
                    SurrenderListGridView.DataBind();
               
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Successfully');", true);

        }
        catch (Exception ex)
        {
         
            commonGatewayObj.RollbackTransaction();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed');", true);
        }
      
    }
}
