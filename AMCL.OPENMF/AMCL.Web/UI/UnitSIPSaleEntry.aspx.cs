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
using CrystalDecisions.Shared;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;
using System.IO;

public partial class UI_UnitSIPSaleEntry : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    BaseClass bcContent = new BaseClass();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    UnitSIPGetSet unitSIPObj = new UnitSIPGetSet();
    UnitSIPBL unitSIPBLObj = new UnitSIPBL();
    UnitReport reportObj = new UnitReport();
    UnitRepurchaseBL unitRepBLObj = new UnitRepurchaseBL();
    CommonGateway commonGatewayObj = new CommonGateway();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
    AMCL.REPORT.CR_SIP_EFT CR_SIP_EFT = new AMCL.REPORT.CR_SIP_EFT();
    AMCL.REPORT.CR_SMS CR_SMS = new AMCL.REPORT.CR_SMS();
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
        spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
             

        regObj.FundCode = fundCode;
        


        if (!IsPostBack)
        {

            sipDayDropDownList.DataSource = unitSIPBLObj.dtSIPDayListDDL(" AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.EFT_PAID_STATUS='P' AND SIP_DETAILS.ACC_VOUCHER_NO IS NOT NULL AND SIP_DETAILS.SIP_SALE_NO IS NULL ");
            sipDayDropDownList.DataTextField = "DEBIT_DATE";
            sipDayDropDownList.DataValueField = "ID";
            sipDayDropDownList.DataBind();

            branchNameDropDownList.DataSource = opendMFDAO.dtBranchList();
            branchNameDropDownList.DataTextField = "BR_NM";
            branchNameDropDownList.DataValueField = "BR_CD";
            branchNameDropDownList.DataBind();
           

        }
       
    
    }
    
    
    private void ClearText()
    {
        certNoTexBox.Text = "";
        saleNoStartTexBox.Text = "";


    }
    
  
    protected void branchNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        regObj.BranchCode = branchNameDropDownList.SelectedValue.ToString();
        saleNoStartTexBox.Text = unitSIPBLObj.getNextSIPSaleNo(regObj).ToString();
        if (opendMFDAO.getCDSStatus(regObj.FundCode) == "N")
        {
            certNoTexBox.Text = unitSIPBLObj.getNextSIPSaleCertNo(regObj).ToString();
        }
        else
        {
            certNoTexBox.Text = "0";
        }
        
       
    }

    protected void addListButton_Click(object sender, EventArgs e)
    {
        DataTable dtSIPListforDetails = unitSIPBLObj.dtSIPDetailsList(" AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "'AND SIP_DETAILS.REG_BR = '" + branchNameDropDownList.SelectedValue.ToString() + "' AND SIP_DETAILS.EFT_PAID_STATUS='P' AND SIP_DETAILS.ACC_VOUCHER_NO IS NOT NULL AND SIP_DETAILS.SIP_SALE_NO IS NULL AND SIP_DETAILS.DEBIT_DATE='" + sipDayDropDownList.SelectedValue.ToString() + "'");
        if(dtSIPListforDetails.Rows.Count>0)
        {
            SaleListGridView.DataSource = dtGetSIPSaleDataForGrid(dtSIPListforDetails, Convert.ToInt32(saleNoStartTexBox.Text), Convert.ToInt32(certNoTexBox.Text));
            SaleListGridView.DataBind();
        }
    }

    private DataTable dtGetSIPSaleDataForGrid(DataTable dtSIPDetails,int saleNoStart,int certNoStart)
    {
        DataTable dtSIPSaleDetails = new DataTable();
        dtSIPSaleDetails.Columns.Add("ID", typeof(string));
        dtSIPSaleDetails.Columns.Add("REG_BK", typeof(string));
        dtSIPSaleDetails.Columns.Add("REG_BR", typeof(string));
        dtSIPSaleDetails.Columns.Add("REG_NO", typeof(string));
        dtSIPSaleDetails.Columns.Add("HNAME", typeof(string));
        dtSIPSaleDetails.Columns.Add("SIP_NO", typeof(string));
        dtSIPSaleDetails.Columns.Add("SCHEDULE_NO", typeof(string));
        dtSIPSaleDetails.Columns.Add("UNIT_QTY", typeof(string));
        dtSIPSaleDetails.Columns.Add("UNIT_RATE", typeof(string));
        dtSIPSaleDetails.Columns.Add("AMOUNT", typeof(string));
        dtSIPSaleDetails.Columns.Add("SL_NO", typeof(string));
        dtSIPSaleDetails.Columns.Add("CERT_NO", typeof(string));
        dtSIPSaleDetails.Columns.Add("FRAC_AMT", typeof(string));
        dtSIPSaleDetails.Columns.Add("ACC_FRAC_AMT_ADD", typeof(string));

        DataRow drSIPDetails;
        for(int loop=0;loop< dtSIPDetails.Rows.Count;loop++)
        {
            drSIPDetails = dtSIPSaleDetails.NewRow();
            drSIPDetails["ID"] = dtSIPDetails.Rows[loop]["ID1"];
            drSIPDetails["REG_BK"] = dtSIPDetails.Rows[loop]["REG_BK"];
            drSIPDetails["REG_BR"] = dtSIPDetails.Rows[loop]["REG_BR"];
            drSIPDetails["REG_NO"] = dtSIPDetails.Rows[loop]["REG_NO"];
            drSIPDetails["HNAME"] = dtSIPDetails.Rows[loop]["HNAME"];
            drSIPDetails["SIP_NO"] = dtSIPDetails.Rows[loop]["SIP_NO"];
            drSIPDetails["SCHEDULE_NO"] = dtSIPDetails.Rows[loop]["SCHEDULE_NO"];
            drSIPDetails["UNIT_QTY"] = dtSIPDetails.Rows[loop]["UNIT_QTY"];
            drSIPDetails["UNIT_RATE"] = dtSIPDetails.Rows[loop]["UNIT_RATE"];
            drSIPDetails["AMOUNT"] =Convert.ToString( Convert.ToDecimal(dtSIPDetails.Rows[loop]["UNIT_RATE"] )* Convert.ToDecimal(dtSIPDetails.Rows[loop]["UNIT_QTY"]));
            drSIPDetails["SL_NO"] = saleNoStart.ToString();
            if (opendMFDAO.getCDSStatus(dtSIPDetails.Rows[loop]["REG_BK"].ToString())=="N")
            {
                drSIPDetails["CERT_NO"] ="Z-"+ certNoStart.ToString();
            }
            else
            {
                drSIPDetails["CERT_NO"] = "";
            }
            drSIPDetails["FRAC_AMT"] = dtSIPDetails.Rows[loop]["FRAC_AMT"];
            drSIPDetails["ACC_FRAC_AMT_ADD"] = dtSIPDetails.Rows[loop]["ACC_FRAC_AMT_ADD"];

            dtSIPSaleDetails.Rows.Add(drSIPDetails);
            saleNoStart++;
            certNoStart++;

        }


        return dtSIPSaleDetails;

    }

    protected void saveSaleButton_Click(object sender, EventArgs e)
    {
        try
        {
            int countCheck = 0;
            commonGatewayObj.BeginTransaction();
            Hashtable htSale = new Hashtable();
            Hashtable htSaleCert = new Hashtable();
            Hashtable htSIPDetails = new Hashtable();

            foreach (GridViewRow Drv in SaleListGridView.Rows)
            {               
              
                    htSale = new Hashtable();
                    htSale.Add("SL_NO",Drv.Cells[10].Text.ToString());
                    htSale.Add("SL_DT", sipDayDropDownList.SelectedValue.ToString());
                    htSale.Add("REG_BK", Drv.Cells[1].Text.ToString());
                    htSale.Add("REG_BR", Drv.Cells[2].Text.ToString());
                    htSale.Add("REG_NO", Drv.Cells[3].Text.ToString());
                    htSale.Add("SL_PRICE", Drv.Cells[8].Text.ToString());
                    htSale.Add("QTY", Drv.Cells[7].Text.ToString());
                    htSale.Add("SL_TYPE", "SIP");
                    htSale.Add("PAY_TYPE", "EFT");
                    htSale.Add("FRAC_AMT", Drv.Cells[12].Text.ToString());
                    htSale.Add("ACC_FRAC_AMT_ADD", Drv.Cells[13].Text.ToString());
                    htSale.Add("SIP_NO", Drv.Cells[5].Text.ToString());

                    htSale.Add("USER_NM", userObj.UserID.ToString());
                    htSale.Add("ENT_DT", DateTime.Now.ToString());
                    htSale.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());

                    commonGatewayObj.Insert(htSale, "SALE");
                if (opendMFDAO.getCDSStatus(Drv.Cells[1].Text.ToString()) == "N")
                {
                    htSaleCert = new Hashtable();

                    htSaleCert.Add("SL_NO", Drv.Cells[10].Text.ToString());
                    htSaleCert.Add("REG_BK", Drv.Cells[1].Text.ToString());
                    htSaleCert.Add("REG_BR", Drv.Cells[2].Text.ToString());
                    htSaleCert.Add("REG_NO", Drv.Cells[3].Text.ToString());

                    htSaleCert.Add("CERTIFICATE", Drv.Cells[11].Text.ToString());
                    string[] certNo = Drv.Cells[11].Text.ToString().Split('-');
                    htSaleCert.Add("CERT_TYPE", "Z");
                    htSaleCert.Add("CERT_NO", certNo[1].ToString());
                    htSaleCert.Add("QTY", Drv.Cells[7].Text.ToString());

                    htSaleCert.Add("USER_NM", userObj.UserID.ToString());
                    htSaleCert.Add("ENT_DT", DateTime.Now.ToString());
                    htSaleCert.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());

                    commonGatewayObj.Insert(htSaleCert, "SALE_CERT");
                }

                    htSIPDetails = new Hashtable();
                    htSIPDetails.Add("SIP_SALE_NO", Drv.Cells[10].Text.ToString());
                    commonGatewayObj.Update(htSIPDetails, "SIP_DETAILS ", "ID=" + Drv.Cells[0].Text.ToString());
                    commonGatewayObj.CommitTransaction();

                    countCheck++;
               

            }
            sipDayDropDownList.DataSource = unitSIPBLObj.dtSIPDayListDDL(" AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.EFT_PAID_STATUS='P' AND SIP_DETAILS.ACC_VOUCHER_NO IS NOT NULL AND SIP_DETAILS.SIP_SALE_NO IS NULL ");
            sipDayDropDownList.DataTextField = "DEBIT_DATE";
            sipDayDropDownList.DataValueField = "ID";
            sipDayDropDownList.DataBind();

            SaleListGridView.DataSource = dtGetNullTableForGrid();
            SaleListGridView.DataBind();

            ClearText();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Successfully');", true);

        }
        catch (Exception ex)
        {

            commonGatewayObj.RollbackTransaction();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed');", true);
        }

    }
    private DataTable dtGetNullTableForGrid()
    {
        DataTable dtSIPSaleDetails = new DataTable();
        dtSIPSaleDetails.Columns.Add("ID", typeof(string));
        dtSIPSaleDetails.Columns.Add("REG_BK", typeof(string));
        dtSIPSaleDetails.Columns.Add("REG_BR", typeof(string));
        dtSIPSaleDetails.Columns.Add("REG_NO", typeof(string));
        dtSIPSaleDetails.Columns.Add("HNAME", typeof(string));
        dtSIPSaleDetails.Columns.Add("SIP_NO", typeof(string));
        dtSIPSaleDetails.Columns.Add("SCHEDULE_NO", typeof(string));
        dtSIPSaleDetails.Columns.Add("UNIT_QTY", typeof(string));
        dtSIPSaleDetails.Columns.Add("UNIT_RATE", typeof(string));
        dtSIPSaleDetails.Columns.Add("AMOUNT", typeof(string));
        dtSIPSaleDetails.Columns.Add("SL_NO", typeof(string));
        dtSIPSaleDetails.Columns.Add("CERT_NO", typeof(string));
        dtSIPSaleDetails.Columns.Add("FRAC_AMT", typeof(string));
        dtSIPSaleDetails.Columns.Add("ACC_FRAC_AMT_ADD", typeof(string));
        return dtSIPSaleDetails;
    }
}
