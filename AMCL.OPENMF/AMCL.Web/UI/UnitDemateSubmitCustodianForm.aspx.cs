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

public partial class UI_UnitDemateSubmitCustodianForm : System.Web.UI.Page
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

            DRRefNoDropDownList.DataSource = unitSaleBLObj.dtDRRefNoListForCustodian("AND SALE.REG_BK='" + fundCode + "'");
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


            bool isDupliacteDRFolioNo = unitSaleBLObj.CheckDuplicateSaleRelatedInfo(" AND SALE.REG_BK='" + fundCode + "' AND SALE.DRF_REG_FOLIO_NO='" + RegFolioNoTextBox.Text + "'");
            bool isDupliacteDRCertNo = unitSaleBLObj.CheckDuplicateSaleRelatedInfo(" AND SALE.REG_BK='" + fundCode + "' AND SALE.DRF_CERT_NO ='" + CertNoTextBox.Text + "'");
            bool isDupliacteDRDistinctiveNo = unitSaleBLObj.CheckDuplicateSaleRelatedInfo("AND ((SALE.DRF_DISTNCT_NO_FROM BETWEEN " + DistinctNoFromTextBox.Text.Trim().ToString() + " AND " + DistinctNoToTextBox.Text.Trim().ToString() + ") AND (SALE.REG_BK = '" + fundCode + "') OR (SALE.DRF_DISTNCT_NO_TO BETWEEN " + DistinctNoFromTextBox.Text.Trim().ToString() + " AND " + DistinctNoToTextBox.Text.Trim().ToString() + ") AND (SALE.REG_BK = '" + fundCode + "'))");
            if (isDupliacteDRFolioNo)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed Due to Duplicate Folio number');", true);
            }
            else if (isDupliacteDRCertNo)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed Due to Duplicate Certificate number');", true);
            }
            else if (isDupliacteDRDistinctiveNo)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed Due to Duplicate Distinctive number');", true);
            }
            else if (((Convert.ToInt64(DistinctNoToTextBox.Text.Trim().ToString()) - Convert.ToInt64(DistinctNoFromTextBox.Text.Trim().ToString()) + 1)) != Convert.ToInt64(TotalUnitsTextBox.Text.Trim().ToString()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed Due to  Distinctive Range and Total Units is not Eqqaul');", true);
            }
            else
            { 
                
                    commonGatewayObj.BeginTransaction();

                    htUpdate = new Hashtable();
                    htUpdate.Add("DRF_CUST_REQ_BY", userObj.UserID.ToString());
                    htUpdate.Add("DRF_CUST_REQ_DATE", DateTime.Now);
                    htUpdate.Add("DRF_REG_FOLIO_NO", RegFolioNoTextBox.Text.Trim().ToString());
                    htUpdate.Add("DRF_CERT_NO", CertNoTextBox.Text.Trim().ToString());
                    htUpdate.Add("DRF_DISTNCT_NO_FROM", DistinctNoFromTextBox.Text.Trim().ToString());
                    htUpdate.Add("DRF_DISTNCT_NO_TO", DistinctNoToTextBox.Text.Trim().ToString());

                    commonGatewayObj.Update(htUpdate, "SALE", "REG_BK='" + fundCode.ToUpper().ToString() + "' AND REG_BR='" + branchCode.ToUpper().ToString() + "' AND SALE.DRF_REF_NO=" + DRRefNoDropDownList.SelectedValue.ToString());

                    commonGatewayObj.CommitTransaction();
                    DRRefNoDropDownList.DataSource = unitSaleBLObj.dtDRRefNoListForCustodian("AND SALE.REG_BK='" + fundCode + "'");
                    DRRefNoDropDownList.DataTextField = "NAME";
                    DRRefNoDropDownList.DataValueField = "ID";
                    DRRefNoDropDownList.DataBind();

                    RegFolioNoTextBox.Text = "";
                    CertNoTextBox.Text = "";
                    DistinctNoFromTextBox.Text = "";
                    DistinctNoToTextBox.Text = "";
                    TotalUnitsTextBox.Text = "";
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
        long maxDistinctiveNo = commonGatewayObj.GetMaxNo("SALE", "DRF_DISTNCT_NO_TO", "SALE.REG_BK = '" + fundCode + "'");
       

        if (dtSaleInfoForDRF.Rows.Count > 0)
        {
            SaleListGridView.DataSource = dtSaleInfoForDRF;
            SaleListGridView.DataBind();
            DistinctNoFromTextBox.Text =Convert.ToString( maxDistinctiveNo + 1);
            DistinctNoToTextBox.Text= Convert.ToString(maxDistinctiveNo + totalUnitsQty);
            RegFolioNoTextBox.Text= Convert.ToString(commonGatewayObj.GetMaxNo("SALE", "TO_NUMBER(DRF_REG_FOLIO_NO)", "SALE.REG_BK = '" + fundCode + "'")+1);
            CertNoTextBox.Text = Convert.ToString(commonGatewayObj.GetMaxNo("SALE", "TO_NUMBER(DRF_CERT_NO)", "SALE.REG_BK = '" + fundCode + "'") + 1);
            TotalUnitsTextBox.Text = totalUnitsQty.ToString();


        }
        else
        {
            SaleListGridView.DataSource = dtSaleInfoForDRF;
            SaleListGridView.DataBind();
            DistinctNoFromTextBox.Text = "";
            DistinctNoToTextBox.Text = "";
            RegFolioNoTextBox.Text = "";
            CertNoTextBox.Text = "";
            TotalUnitsTextBox.Text = "";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
           
        }
    }
}
