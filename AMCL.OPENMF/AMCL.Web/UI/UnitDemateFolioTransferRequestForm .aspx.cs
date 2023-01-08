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

public partial class UI_UnitDemateFolioTransferRequestForm : System.Web.UI.Page
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
           

            

        }
    
    }



    protected void addToListButton_Click(object sender, EventArgs e)
    {
        long nextDRReferenceNo = unitSaleBLObj.getNexDRReferenceNumber(" AND SALE.REG_BK='" + fundCode + "'");
        DataTable dtSaleInfoForDRF = unitSaleBLObj.dtGetSaleInfoForFolioTransfer(" AND SALE.REG_BK='" + fundCode + "' AND SALE.REG_BR='" + branchCode + "' AND SALE.SL_NO BETWEEN "+ fromSaleNoTextBox.Text.Trim()+" AND "+ toSaleNoTextBox.Text.Trim());

        if (dtSaleInfoForDRF.Rows.Count > 0)
        {
            SaleListGridView.DataSource = dtSaleInfoForDRF;
            SaleListGridView.DataBind();
            DRFRefNoTextBox.Text = nextDRReferenceNo.ToString();
        }
        else
        {
            SaleListGridView.DataSource = dtSaleInfoForDRF;
            SaleListGridView.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
            DRFRefNoTextBox.Text = "";
        }

    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        int countCheck = 0;
        int countUpdateRow = 0;
        bool isDupliacteDRRefNo = unitSaleBLObj.CheckDuplicateSaleRelatedInfo(" AND SALE.REG_BK='" + fundCode + "' AND DRF_REF_NO=" + Convert.ToInt64(DRFRefNoTextBox.Text.ToString()));
        if (!isDupliacteDRRefNo)
        {
            Hashtable htUpdate = new Hashtable();
            try
            {
                commonGatewayObj.BeginTransaction();
                foreach (GridViewRow Drv in SaleListGridView.Rows)
                {

                    CheckBox leftCheckBox = (CheckBox)SaleListGridView.Rows[countCheck].FindControl("leftCheckBox");

                    if (leftCheckBox.Checked)
                    {
                        htUpdate = new Hashtable();
                        htUpdate.Add("FOLIO_TR_REQ_BY", userObj.UserID.ToString());
                        htUpdate.Add("FOLIO_TR_REQ_DATE", DateTime.Now);
                        htUpdate.Add("HOLDER_BO", Drv.Cells[6].Text.ToUpper().ToString());
                        htUpdate.Add("DRF_REF_NO", Convert.ToInt64(DRFRefNoTextBox.Text.ToString()));

                        commonGatewayObj.Update(htUpdate, "SALE", "REG_BK='" + Drv.Cells[1].Text.ToUpper().ToString() + "' AND REG_BR='" + Drv.Cells[2].Text.ToUpper().ToString() + "' AND REG_NO=" + Convert.ToInt32(Drv.Cells[3].Text.ToString()) + " AND SL_NO=" + Convert.ToInt32(Drv.Cells[4].Text.ToString()));
                        countUpdateRow++;
                    }

                    countCheck++;

                }
                if (countUpdateRow == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed Due No Sale Selection ');", true);
                }
                else
                { 
                    commonGatewayObj.CommitTransaction();
                    DataTable dtSaleInfoForDRF = unitSaleBLObj.dtGetSaleInfoForDRF(" AND SALE.REG_BK='" + fundCode + "' AND SALE.REG_BR='" + branchCode + "' AND SALE.SL_NO BETWEEN 0 AND 0" );
                    SaleListGridView.DataSource = dtSaleInfoForDRF;
                    SaleListGridView.DataBind();
                    DRFRefNoTextBox.Text = "";
                    toSaleNoTextBox.Text = "";
                    fromSaleNoTextBox.Text = "";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Successfully');", true);
                }

            }
            catch (Exception ex)
            {

                commonGatewayObj.RollbackTransaction();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed:"+ ex.Message.ToString() + "');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed:Duplicate DR Reference Number ');", true);
        }
    }
   
}
