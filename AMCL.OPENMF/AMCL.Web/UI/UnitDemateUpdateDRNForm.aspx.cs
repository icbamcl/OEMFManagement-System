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

public partial class UI_UnitDemateUpdateDRNForm : System.Web.UI.Page
{
    System.Web.UI.Page this_page_ref = null;
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();    
    UnitUser userObj = new UnitUser();

    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
    BaseClass bcContent = new BaseClass();
    string fundCode = "";
    string branchCode = "";

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        fundCode = bcContent.FundCode.ToString();
        branchCode = bcContent.BranchCode.ToString();
        spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
                                       
        if (!IsPostBack)
        {

            DRRefNoDropDownList.DataSource = unitSaleBLObj.dtDRRefNoListForDRN("AND SALE.REG_BK='" + fundCode + "'");
            DRRefNoDropDownList.DataTextField = "NAME";
            DRRefNoDropDownList.DataValueField = "ID";
            DRRefNoDropDownList.DataBind();


        }
    
    }





    protected void SubmitButton_Click(object sender, EventArgs e)
    {

        Hashtable htUpdate = new Hashtable();
        try
        {


            bool isDupliacteDRF = unitSaleBLObj.CheckDuplicateSaleRelatedInfo(" AND SALE.REG_BK='" + fundCode + "' AND SALE.DRF_ACCEPT_NO='" + DRFNoTextBox.Text + "'");
            bool isDupliacteDRN = unitSaleBLObj.CheckDuplicateSaleRelatedInfo(" AND SALE.REG_BK='" + fundCode + "' AND SALE.DRF_DRN ='" + DRNTextBox.Text + "'");
          
            if (isDupliacteDRF)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed Due to Duplicate DRF No');", true);
            }
            else if (isDupliacteDRN)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed Due to Duplicate DRN');", true);
            }
           
            else
            { 
                
                    commonGatewayObj.BeginTransaction();

                    htUpdate = new Hashtable();
                    htUpdate.Add("DRF_ACCEPT_ENTRY_BY", userObj.UserID.ToString());
                    htUpdate.Add("DRF_ACCEPT_ENTRY_DATE", DateTime.Now);
                    htUpdate.Add("DRF_ACCEPT_NO", DRFNoTextBox.Text.Trim().ToString());
                    htUpdate.Add("DRF_DRN", DRNTextBox.Text.Trim().ToString());
                    htUpdate.Add("DRF_ACCEPT_DATE", DamateDateTextBox.Text.Trim().ToString());
                   

                    commonGatewayObj.Update(htUpdate, "SALE", "REG_BK='" + fundCode.ToUpper().ToString() + "' AND REG_BR='" + branchCode.ToUpper().ToString() + "' AND SALE.DRF_REF_NO=" + DRRefNoDropDownList.SelectedValue.ToString());

                    commonGatewayObj.CommitTransaction();
                    DRRefNoDropDownList.DataSource = unitSaleBLObj.dtDRRefNoListForDRN("AND SALE.REG_BK='" + fundCode + "'");
                    DRRefNoDropDownList.DataTextField = "NAME";
                    DRRefNoDropDownList.DataValueField = "ID";
                    DRRefNoDropDownList.DataBind();

                    DRFNoTextBox.Text = "";
                    DRNTextBox.Text = "";
                    DamateDateTextBox.Text = "";
                    FolioNoLabel.Text = "";
                    CertNoLabel.Text = "";
                    DistinctNoToLabel.Text = "";
                    DistNoFromLabel.Text = "";  
                    TotalUnitLabel.Text = "";
                    DataTable dtSaleInfoForDRF = unitSaleBLObj.dtGetSaleInfoForDemate(" AND SALE.REG_BK='" + fundCode + "' AND SALE.REG_BR='" + branchCode + "' AND SALE.DRF_REF_NO=0");
                    SaleListGridView.DataSource = dtSaleInfoForDRF;
                    SaleListGridView.DataBind();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Successfully');", true);
               }

            }
            catch (Exception ex)
            {

                commonGatewayObj.RollbackTransaction();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed Due to Error:" + ex.Message.ToString() + "');", true);
        
            }
    
    }

   

    protected void DRRefNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtSaleInfoForDRF = unitSaleBLObj.dtGetSaleInfoForDemate(" AND SALE.REG_BK='" + fundCode + "' AND SALE.REG_BR='" + branchCode + "' AND SALE.DRF_REF_NO="+DRRefNoDropDownList.SelectedValue.ToString());
        long totalUnitsQty= unitSaleBLObj.getTotalUnitsForDRF(" AND SALE.REG_BK='" + fundCode + "' AND SALE.REG_BR='" + branchCode + "' AND SALE.DRF_REF_NO=" + DRRefNoDropDownList.SelectedValue.ToString());
             
        if (dtSaleInfoForDRF.Rows.Count > 0)
        {
            SaleListGridView.DataSource = dtSaleInfoForDRF;
            SaleListGridView.DataBind();
            FolioNoLabel.Text = dtSaleInfoForDRF.Rows[0]["DRF_REG_FOLIO_NO"].ToString();
            CertNoLabel.Text = dtSaleInfoForDRF.Rows[0]["DRF_CERT_NO"].ToString();
            DistinctNoToLabel.Text = dtSaleInfoForDRF.Rows[0]["DRF_DISTNCT_NO_TO"].ToString();
            DistNoFromLabel.Text = dtSaleInfoForDRF.Rows[0]["DRF_DISTNCT_NO_FROM"].ToString();
            TotalUnitLabel.Text = totalUnitsQty.ToString();            
        }
        else
        {
            SaleListGridView.DataSource = dtSaleInfoForDRF;
            SaleListGridView.DataBind();
            DRFNoTextBox.Text = "";
            DRNTextBox.Text = "";
            DamateDateTextBox.Text = "";
            FolioNoLabel.Text ="";
            CertNoLabel.Text =  "";
            DistinctNoToLabel.Text = "";
            DistNoFromLabel.Text = "";
            TotalUnitLabel.Text = "";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
           
        }
    }
}
