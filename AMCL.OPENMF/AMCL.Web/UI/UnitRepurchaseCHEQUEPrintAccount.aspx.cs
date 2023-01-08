using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitRepurchaseCHEQUEPrintAccount : System.Web.UI.Page
{
    //System.Web.UI.Page this_page_ref = null;
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();    
    UnitUser userObj = new UnitUser();
    
    UnitRepurchaseBL unitRepBLObj = new UnitRepurchaseBL();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
    BaseClass bcContent = new BaseClass();
    AMCL.REPORT.CR_CHQPrint CR_ChequePrintIFIC = new AMCL.REPORT.CR_CHQPrint();
    NumberToEnglish numberToEnglishObj = new NumberToEnglish();
   
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

         

            DataTable dtChequeData = unitRepBLObj.dtGetChequePrintData("  ");
            if (dtChequeData.Rows.Count > 0)
            {
                SurrenderListGridView.DataSource = dtChequeData;
                SurrenderListGridView.DataBind();
            }
         
        }
    
    }



    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (fundNameDropDownList.SelectedValue.ToString() != "0")
        {

            DataTable dtChequeData = unitRepBLObj.dtGetChequePrintData(" AND FUND_INFO.FUND_CD='" + fundNameDropDownList.SelectedValue.ToString() + "'");
            SurrenderListGridView.DataSource = dtChequeData;
            SurrenderListGridView.DataBind();         
        }
        else
        {
            DataTable dtChequeData = unitRepBLObj.dtGetChequePrintData(" ");
            if (dtChequeData.Rows.Count > 0)
            {
                SurrenderListGridView.DataSource = dtChequeData;
                SurrenderListGridView.DataBind();
               
            }
        }

    }


    protected void ChecqueVoucherButton_Click(object sender, EventArgs e)
    {
       
        int countCheck = 0;
        int fileCount = 0;
        string ChqueDate = "";
        string NameWithAC = "";
        string Name = "";
        string AmountInWord = "";
        string Amount = "";

        foreach (GridViewRow Drv in SurrenderListGridView.Rows)
        {
            if (fileCount == 1)
                break;
            CheckBox leftCheckBox = (CheckBox)SurrenderListGridView.Rows[countCheck].FindControl("leftCheckBox");
            if (leftCheckBox.Checked)
            {
                fileCount++;
                decimal unitSaleTotalValue = Convert.ToDecimal(Drv.Cells[7].Text.ToString());
                Amount = unitSaleTotalValue.ToString("#,##0.00");
                AmountInWord = numberToEnglishObj.changeNumericToWords(Amount);
                Name = Drv.Cells[6].Text.ToString();
                ChqueDate = Drv.Cells[9].Text.ToString();
                DataTable dtHolderBankInfo = unitRepBLObj.dtGetHolderBankInfo(Drv.Cells[2].Text.ToUpper().ToString(), Drv.Cells[3].Text.ToUpper().ToString(), Convert.ToInt32(Drv.Cells[4].Text.ToString()));
                if (dtHolderBankInfo.Rows.Count > 0)
                {
                    NameWithAC = Drv.Cells[6].Text.ToString() +" "+ " A/C:" + dtHolderBankInfo.Rows[0]["BK_AC_NO"].ToString();
                }
                else
                {
                    NameWithAC = Drv.Cells[6].Text.ToString();
                }
                CR_ChequePrintIFIC.SetParameterValue("ChqueDate", ChqueDate);
                CR_ChequePrintIFIC.SetParameterValue("Amount", Amount);
                CR_ChequePrintIFIC.SetParameterValue("AmountInWord", AmountInWord);
                CR_ChequePrintIFIC.SetParameterValue("NameWithAC", NameWithAC);
                CR_ChequePrintIFIC.SetParameterValue("Name", Name);
             

                 Hashtable htUpdate = new Hashtable();
                commonGatewayObj.BeginTransaction();
                htUpdate.Add("CHQ_PRINT_DT", DateTime.Now.ToShortTimeString());
                htUpdate.Add("CHQ_PRINT_BY", userObj.UserID.ToString());
                commonGatewayObj.Update(htUpdate, "REPURCHASE ", "REG_BK='" + Drv.Cells[2].Text.ToUpper().ToString() + "' AND   REG_BR='" + Drv.Cells[3].Text.ToUpper().ToString() + "' AND   REG_NO='" + Drv.Cells[4].Text.ToUpper().ToString() + "' AND REP_NO='" + Drv.Cells[5].Text.ToUpper().ToString() + "'");
                commonGatewayObj.CommitTransaction();
              

                DataTable dtChequeData = unitRepBLObj.dtGetChequePrintData(" ");
                SurrenderListGridView.DataSource = dtChequeData;
                SurrenderListGridView.DataBind();

                CR_ChequePrintIFIC.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "RepurchaseChequePrint" + DateTime.Now + ".pdf");

            }
            countCheck++;
        }
          
                                
    }
}
