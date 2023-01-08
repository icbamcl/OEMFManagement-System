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

public partial class UI_UnitDemateUpdateTransferInformationForm : System.Web.UI.Page
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

            DRRefNoDropDownList.DataSource = unitSaleBLObj.dtDRRefNoListForTransfer("AND SALE.REG_BK='" + fundCode + "'");
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
                int saleNo = Convert.ToInt32(Drv.Cells[3].Text.ToUpper().ToString());
                int regNo = Convert.ToInt32(Drv.Cells[2].Text.ToUpper().ToString());


                if (TRSeqNoTextBox.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed Due to NULL Transfer Sequence Number');", true);
                    break;
                }
                else
                {

                    commonGatewayObj.BeginTransaction();

                    htUpdate = new Hashtable();
                    htUpdate.Add("DRF_TR_ENTRY_BY", userObj.UserID.ToString());
                    htUpdate.Add("DRF_TR_ENTRY_DATE", DateTime.Now);
                    htUpdate.Add("DRF_TR_SEQ_NO", TRSeqNoTextBox.Text.Trim().ToString());
                    htUpdate.Add("DRF_TR_DATE", TransferDateTextBox.Text.Trim().ToString());

                    commonGatewayObj.Update(htUpdate, "SALE", "REG_BK='" + fundCode.ToUpper().ToString() + "' AND REG_BR='" + branchCode.ToUpper().ToString() + "' AND REG_NO=" + regNo + " AND SL_NO=" + saleNo + " AND SALE.DRF_REF_NO=" + DRRefNoDropDownList.SelectedValue.ToString());
                    commonGatewayObj.CommitTransaction();
                }



                countCheck++;
            }
           
            DRRefNoDropDownList.DataSource = unitSaleBLObj.dtDRRefNoListForTransfer("AND SALE.REG_BK='" + fundCode + "'");
            DRRefNoDropDownList.DataTextField = "NAME";
            DRRefNoDropDownList.DataValueField = "ID";
            DRRefNoDropDownList.DataBind();

            TRSeqFromNoTextBox.Text = "";
            TransferDateTextBox.Text = "";
            FolioNoLabel.Text = "";
            CertNoLabel.Text = "";
            DistinctNoToLabel.Text = "";
            DistNoFromLabel.Text = "";
            TotalUnitLabel.Text = "";
            DRFNoLabel.Text = "";
            DRNLabel.Text = "";
            DemateAcceptDateLabel.Text = "";

            DataTable dtSaleInfoForDRF = unitSaleBLObj.dtGetSaleInfoForDemate(" AND SALE.REG_BK='" + fundCode + "' AND SALE.REG_BR='" + branchCode + "' AND SALE.DRF_REF_NO=0");
            SaleListGridView.DataSource = dtSaleInfoForDRF;
            SaleListGridView.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Successfully');", true);

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
            DRFNoLabel.Text= dtSaleInfoForDRF.Rows[0]["DRF_ACCEPT_NO"].ToString();
            DRNLabel.Text= dtSaleInfoForDRF.Rows[0]["DRF_DRN"].ToString();
            DemateAcceptDateLabel.Text= Convert.ToDateTime( dtSaleInfoForDRF.Rows[0]["DRF_ACCEPT_DATE"]).ToString("dd-MMM-yyyy");

            FolioNoLabel.Text = dtSaleInfoForDRF.Rows[0]["DRF_REG_FOLIO_NO"].ToString();
            CertNoLabel.Text = dtSaleInfoForDRF.Rows[0]["DRF_CERT_NO"].ToString();
            DistinctNoToLabel.Text = dtSaleInfoForDRF.Rows[0]["DRF_DISTNCT_NO_TO"].ToString();
            DistNoFromLabel.Text = dtSaleInfoForDRF.Rows[0]["DRF_DISTNCT_NO_FROM"].ToString();
            TotalUnitLabel.Text = totalUnitsQty.ToString();

           
            SaleListGridView.DataSource = dtSaleInfoWithTrSeq;
            SaleListGridView.DataBind();
           
        }
        else
        {
            DRFNoLabel.Text = "";
            DRNLabel.Text = "";
            DemateAcceptDateLabel.Text = "";
            TRSeqFromNoTextBox.Text = "";          
            TransferDateTextBox.Text = "";
            FolioNoLabel.Text ="";
            CertNoLabel.Text =  "";
            DistinctNoToLabel.Text = "";
            DistNoFromLabel.Text = "";
            TotalUnitLabel.Text = "";
            SaleListGridView.DataSource = dtSaleInfoWithTrSeq;
            SaleListGridView.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
           
        }
    }

    protected void addToListButton_Click(object sender, EventArgs e)
    {

        bool isDupliacteTrSeqNo = unitSaleBLObj.CheckDuplicateSaleRelatedInfo(" AND SALE.REG_BK='" + fundCode + "' AND SALE.DRF_TR_SEQ_NO='" + TRSeqFromNoTextBox.Text + "'");
        DataTable dtSaleInfoForDRF = unitSaleBLObj.dtGetSaleInfoForDemate(" AND SALE.REG_BK='" + fundCode + "' AND SALE.REG_BR='" + branchCode + "' AND SALE.DRF_REF_NO=" + DRRefNoDropDownList.SelectedValue.ToString());
        DataTable dtSaleInfoWithTrSeq = dtSaleInfoForDRF.Clone();
      //  dtSaleInfoWithTrSeq.Columns.Add(" DRF_TR_SEQ_NO", typeof(long));
        DataRow drSaleInfoForTrSeq;
        long trSeqNo =Convert.ToInt64( TRSeqFromNoTextBox.Text.Trim());
        if (isDupliacteTrSeqNo)
        {
            SaleListGridView.DataSource = unitSaleBLObj.dtGetSaleInfoForDemate(" AND 1=2"); ;
            SaleListGridView.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Add to List Failed Due to Duplicate Transfer Sequence Number:" + TRSeqFromNoTextBox.Text + "');", true);
           
        }
        else if (dtSaleInfoForDRF.Rows.Count > 0)
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
                drSaleInfoForTrSeq["DRF_TR_SEQ_NO"] = trSeqNo;
                dtSaleInfoWithTrSeq.Rows.Add(drSaleInfoForTrSeq);
      

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
        else
        {
            SaleListGridView.DataSource = dtSaleInfoForDRF;
            SaleListGridView.DataBind();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);

        }



    }
}
