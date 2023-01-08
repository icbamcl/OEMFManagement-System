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

public partial class UI_UnitDemateUpdateFolioTransferInfoFormm : System.Web.UI.Page
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

            DRRefNoDropDownList.DataSource = unitSaleBLObj.dtDRRefNoListForFolioTransfer("AND SALE.REG_BK='" + fundCode + "'");
            DRRefNoDropDownList.DataTextField = "NAME";
            DRRefNoDropDownList.DataValueField = "ID";
            DRRefNoDropDownList.DataBind();

        }
    
    }





    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        int countCheck = 0;
        Hashtable htUpdate = new Hashtable();
        try
        {
            foreach (GridViewRow Drv in SaleListGridView.Rows)
            {

                TextBox TRSeqNoTextBox = (TextBox)SaleListGridView.Rows[countCheck].FindControl("TRSeqNoTextBox");

                bool isDupliacteTrSeqNo = unitSaleBLObj.CheckDuplicateSaleRelatedInfo(" AND SALE.REG_BK='" + fundCode + "' AND SALE.DRF_TR_SEQ_NO='" + TRSeqNoTextBox.Text + "'");
               

                //if (isDupliacteTrSeqNo)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed Due to Duplicate Transfer Sequence Number:"+ TRSeqNoTextBox.Text + "');", true);
                //    break;
                //}
               
                //else
                //{

                    commonGatewayObj.BeginTransaction();

                    htUpdate = new Hashtable();
                    htUpdate.Add("DRF_TR_ENTRY_BY", userObj.UserID.ToString());
                    htUpdate.Add("DRF_TR_ENTRY_DATE", DateTime.Now);
                    htUpdate.Add("DRF_TR_SEQ_NO", TRSeqNoTextBox.Text.Trim().ToString());                   
                    htUpdate.Add("DRF_TR_DATE", TransferDateTextBox.Text.Trim().ToString());

                    commonGatewayObj.Update(htUpdate, "SALE", "REG_BK='" + fundCode.ToUpper().ToString() + "' AND REG_BR='" + branchCode.ToUpper().ToString() + "' AND SALE.DRF_REF_NO=" + DRRefNoDropDownList.SelectedValue.ToString());
                   //}
                commonGatewayObj.CommitTransaction();
                DRRefNoDropDownList.DataSource = unitSaleBLObj.dtDRRefNoListForFolioTransfer("AND SALE.REG_BK='" + fundCode + "'");
                DRRefNoDropDownList.DataTextField = "NAME";
                DRRefNoDropDownList.DataValueField = "ID";
                DRRefNoDropDownList.DataBind();

                TRSeqFromNoTextBox.Text = "";               
                TransferDateTextBox.Text = "";
               

                DataTable dtSaleInfoForDRF = unitSaleBLObj.dtGetSaleInfoForDemate(" AND SALE.REG_BK='" + fundCode + "' AND SALE.REG_BR='" + branchCode + "' AND SALE.DRF_REF_NO=0");
                SaleListGridView.DataSource = dtSaleInfoForDRF;
                SaleListGridView.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Successfully');", true);
               
                countCheck++;
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
        DataTable dtSaleInfoWithTrSeq = dtSaleInfoForDRF.Clone();
        dtSaleInfoWithTrSeq.Columns.Add("TR_SEQ_NO", typeof(long));
        if (dtSaleInfoForDRF.Rows.Count > 0)
        {           
            
           
            SaleListGridView.DataSource = dtSaleInfoWithTrSeq;
            SaleListGridView.DataBind();
           
        }
        else
        {
        
            SaleListGridView.DataSource = dtSaleInfoWithTrSeq;
            SaleListGridView.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
           
        }
    }

    protected void addToListButton_Click(object sender, EventArgs e)
    {
        DataTable dtSaleInfoForDRF = unitSaleBLObj.dtGetSaleInfoForDemate(" AND SALE.REG_BK='" + fundCode + "' AND SALE.REG_BR='" + branchCode + "' AND SALE.DRF_REF_NO=" + DRRefNoDropDownList.SelectedValue.ToString());
        DataTable dtSaleInfoWithTrSeq = dtSaleInfoForDRF.Clone();
        dtSaleInfoWithTrSeq.Columns.Add("TR_SEQ_NO", typeof(long));
        DataRow drSaleInfoForTrSeq;
        long trSeqNo =Convert.ToInt64( TRSeqFromNoTextBox.Text.Trim());
        if (dtSaleInfoForDRF.Rows.Count > 0)
        {
            for (int loop = 0; loop < dtSaleInfoForDRF.Rows.Count; loop++)
            {
                drSaleInfoForTrSeq = dtSaleInfoWithTrSeq.NewRow();
                drSaleInfoForTrSeq["REG_BK"] = dtSaleInfoForDRF.Rows[loop]["REG_BK"];
                drSaleInfoForTrSeq["REG_BR"] = dtSaleInfoForDRF.Rows[loop]["REG_BR"];
                drSaleInfoForTrSeq["REG_NO"] = dtSaleInfoForDRF.Rows[loop]["REG_NO"];
                drSaleInfoForTrSeq["SL_NO"] = dtSaleInfoForDRF.Rows[loop]["SL_NO"];
                drSaleInfoForTrSeq["HNAME"] = dtSaleInfoForDRF.Rows[loop]["HNAME"];
                drSaleInfoForTrSeq["HOLDER_BO"] = dtSaleInfoForDRF.Rows[loop]["HOLDER_BO"];
                drSaleInfoForTrSeq["QTY"] = dtSaleInfoForDRF.Rows[loop]["QTY"];
                drSaleInfoForTrSeq["TR_SEQ_NO"] = trSeqNo;
                dtSaleInfoWithTrSeq.Rows.Add(drSaleInfoForTrSeq);
                trSeqNo++;

            }
            if (dtSaleInfoWithTrSeq.Rows.Count > 0)
            {
                SaleListGridView.DataSource = dtSaleInfoWithTrSeq;
                SaleListGridView.DataBind();
            }
            else
            {
                SaleListGridView.DataSource = dtSaleInfoForDRF;
                SaleListGridView.DataBind();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);

            }

        }
       


    }
}
