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
using System.Text;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitRepurchaseCHEQUEPostingAccount : System.Web.UI.Page
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

            Signatory1DropDownList.DataSource = unitRepBLObj.dtGetSigantoryList();
            Signatory1DropDownList.DataTextField = "NAME";
            Signatory1DropDownList.DataValueField = "ID";
           // Signatory1DropDownList.SelectedValue = "IAMCL312";
            Signatory1DropDownList.DataBind();

            Signatory2DropDownList.DataSource = unitRepBLObj.dtGetSigantoryList();
            Signatory2DropDownList.DataTextField = "NAME";
            Signatory2DropDownList.DataValueField = "ID";
           // Signatory2DropDownList.SelectedValue = "IAMCL535";
            Signatory2DropDownList.DataBind();

            TranDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");

            DataTable dtChequeData = unitRepBLObj.dtGetChequePaymentDataWithVoucherNo(" AND A.AUDITED_BY IS NOT NULL AND A.VOUCHER_NO IS NULL  ");
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

            DataTable dtChequeData = unitRepBLObj.dtGetChequePaymentDataWithVoucherNo("AND A.VOUCHER_NO IS NULL AND A.AUDITED_BY IS NOT NULL AND FUND_INFO.FUND_CD='" + fundNameDropDownList.SelectedValue.ToString() + "'");
            SurrenderListGridView.DataSource = dtChequeData;
            SurrenderListGridView.DataBind();
            VoucherNoTexBox.Text = unitRepBLObj.getNexAccountVoucherNo(unitRepBLObj.GetaccountSchema(fundNameDropDownList.SelectedValue.ToString()));
        }
        else
        {
            DataTable dtChequeData = unitRepBLObj.dtGetChequePaymentDataWithVoucherNo("AND A.VOUCHER_NO IS NULL AND A.AUDITED_BY IS NOT NULL ");
            if (dtChequeData.Rows.Count > 0)
            {
                SurrenderListGridView.DataSource = dtChequeData;
                SurrenderListGridView.DataBind();
                VoucherNoTexBox.Text = "";
            }
        }

    }


    protected void ChequeVoucherButton_Click(object sender, EventArgs e)
    {
        if (fundNameDropDownList.SelectedValue.ToString() != "0")
        {
            try
            {
               SaveAccountVoucherWithChequePayment(unitRepBLObj.GetaccountSchema(fundNameDropDownList.SelectedValue.ToString()));

                DataTable dtChequeData = unitRepBLObj.dtGetChequePaymentDataWithVoucherNo(" AND A.VOUCHER_NO IS NULL AND A.AUDITED_BY IS NOT NULL AND FUND_INFO.FUND_CD='" + fundNameDropDownList.SelectedValue.ToString() + "'");
                SurrenderListGridView.DataSource = dtChequeData;
                SurrenderListGridView.DataBind();
                VoucherNoTexBox.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Successfully');", true);
               
               
                    
                
            }
            catch (Exception ex)
            {               

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed');", true);
            }
        }
    }
    public  void SaveAccountVoucherWithChequePayment(string accountSchema)
    {
        
        

        try
        {
            int countCheck = 0;
            long tranNumber = commonGatewayObj.GetMaxNo(accountSchema + ".GL_BASICINFO", "TRAN_ID");
            long contrlNumber = commonGatewayObj.GetMaxNo(accountSchema + ".GL_BASICINFO", "TO_NUMBER(CTRLNO)") - 1;

            string acc_op_id = unitRepBLObj.getAcc_OP_ID(fundNameDropDownList.SelectedValue.ToString());
            long acc_terminal_no = unitRepBLObj.getAcc_terminal_no(fundNameDropDownList.SelectedValue.ToString());

            int faceValue = unitRepBLObj.getUnitFaceValue(fundNameDropDownList.SelectedValue.ToString());
            string bankPaymentCode = unitRepBLObj.getUnitBankPaymentCode(fundNameDropDownList.SelectedValue.ToString());
            string getUnitFundBankCode = unitRepBLObj.getUnitFundBankCode(fundNameDropDownList.SelectedValue.ToString());
            Hashtable htInsert = new Hashtable();
            Hashtable htUpdate = new Hashtable();
            commonGatewayObj.BeginTransaction();
            DataTable dtFundYearEndInfo = commonGatewayObj.Select("SELECT * FROM FUND_INFO WHERE FUND_CD='" + fundNameDropDownList.SelectedValue.ToString() + "'");
            int year = Convert.ToDateTime(TranDateTextBox.Text.ToString()).Year;
            int month = Convert.ToDateTime(TranDateTextBox.Text.ToString()).Month;
            string yearStartDate = dtFundYearEndInfo.Rows[0]["YEAR_START_MONTH"].ToString();
            string yearEndDate = dtFundYearEndInfo.Rows[0]["YEAR_END_MONTH"].ToString();
            if (yearStartDate == "01-JUL")
            {
                if (month < 6)
                {
                    yearEndDate = yearEndDate + "-" + year.ToString();
                    year--;
                    yearStartDate = yearStartDate + "-" + year.ToString();

                }
                else
                {
                    yearStartDate = yearStartDate + "-" + year.ToString();
                    year++;
                    yearEndDate = yearEndDate + "-" + year.ToString();
                }
            }
            else
            {
                yearStartDate = yearStartDate + "-" + year.ToString();
                yearEndDate = yearEndDate + "-" + year.ToString();
            }
            if (unitRepBLObj.IsDuplicateAccVoucherNo(accountSchema, VoucherNoTexBox.Text.Trim(), "1", yearStartDate, yearEndDate))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Failed Duplicate Payment Voucher Number');", true);
            }
            else
            { 
            foreach (GridViewRow Drv in SurrenderListGridView.Rows)
            {

                CheckBox leftCheckBox = (CheckBox)SurrenderListGridView.Rows[countCheck].FindControl("leftCheckBox");
                //TextBox VoucherNoTexBox = (TextBox)SurrenderListGridView.Rows[countCheck].FindControl("VoucherNumberTextBox");
                TextBox ChequeNoTexBox = (TextBox)SurrenderListGridView.Rows[countCheck].FindControl("ChequeNumberTextBox");

                if (leftCheckBox.Checked)
                {


                    decimal unitFaceValue = Convert.ToDecimal(faceValue);
                    decimal unitSaleQty = Convert.ToDecimal(Drv.Cells[8].Text.ToString());
                    decimal unitSaleRate = Convert.ToDecimal(Drv.Cells[9].Text.ToString());
                    decimal unitSaleTotalValue = Convert.ToDecimal(Drv.Cells[10].Text.ToString());
                        string unitRegRepNo = " to Reg.No.='" + Drv.Cells[2].Text.ToUpper().ToString() + "'/'" + Drv.Cells[3].Text.ToUpper().ToString() + "'/" + Drv.Cells[4].Text.ToString() + " and Rep. No=" + Drv.Cells[5].Text.ToString();
                        DataTable dtHolderBankInfo = unitRepBLObj.dtGetHolderBankInfo(Drv.Cells[2].Text.ToUpper().ToString(), Drv.Cells[3].Text.ToUpper().ToString(), Convert.ToInt32(Drv.Cells[4].Text.ToString()));


                    htUpdate = new Hashtable();
                    if (dtHolderBankInfo.Rows.Count > 0)
                    {
                        htUpdate.Add("HOLDER_AC_NO", dtHolderBankInfo.Rows[0]["BK_AC_NO"].ToString());
                        htUpdate.Add("HOLDER_ROUTING_NO", dtHolderBankInfo.Rows[0]["ROUTING_NO"].ToString());
                    }
                    htUpdate.Add("CHEQUE_DATE", ChequeDateTextBox.Text.ToString());
                    htUpdate.Add("CHEQUE_POSTED_BY", userObj.UserID.ToString());
                    htUpdate.Add("CHEQUE_POSTED_DATE", DateTime.Now.ToString());
                    htUpdate.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                    htUpdate.Add("CHEQUE_NO", ChequeNoTexBox.Text.Trim().ToString());
                        regObj.RegNumber = Drv.Cells[4].Text.ToString();
                        regObj.BranchCode = Drv.Cells[3].Text.ToString();
                        regObj.FundCode = Drv.Cells[2].Text.ToString();
                        if (unitRepBLObj.IsValidMobileNumber(regObj))
                        {
                            htUpdate.Add("SMS_CREATE_DATE", DateTime.Now.ToString());
                        }


                    htUpdate.Add("SIGNATORY1_ID", Signatory1DropDownList.SelectedValue.ToUpper().ToString());
                    htUpdate.Add("SIGANTORY2_ID", Signatory2DropDownList.SelectedValue.ToUpper().ToString());
                    htUpdate.Add("FUND_INFO_BANK_CODE", getUnitFundBankCode);

                    commonGatewayObj.Update(htUpdate, "REPURCHASE ", "REG_BK='" + Drv.Cells[2].Text.ToUpper().ToString() + "' AND REG_BR='" + Drv.Cells[3].Text.ToUpper().ToString() + "' AND REG_NO=" + Convert.ToInt32(Drv.Cells[4].Text.ToString()) + " AND REP_NO=" + Convert.ToInt32(Drv.Cells[5].Text.ToString()));
                    contrlNumber++;
                    htInsert = new Hashtable();
                    htInsert.Add("TRAN_ID", tranNumber + 1);
                    htInsert.Add("ACCCODE", "101010000");
                    htInsert.Add("BANKACNO", "101010000");
                    htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                    htInsert.Add("TRAN_DATE", TranDateTextBox.Text.Trim());
                    htInsert.Add("REMARKS", unitSaleQty.ToString() + " unit surrendered @tk." + unitSaleRate.ToString() + " per unit " + unitRegRepNo);
                        htInsert.Add("TRAN_TYPE", "D");
                    htInsert.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                    htInsert.Add("TOTAL_AMNT", Convert.ToDecimal(unitFaceValue * unitSaleQty));
                    htInsert.Add("CTRLNO", contrlNumber);
                    htInsert.Add("OP_ID", userObj.UserID.ToString());
                    htInsert.Add("VOUCHER_TYPE", "1");
                    htInsert.Add("TERMINAL_NO", acc_terminal_no);
                    htInsert.Add("RECENT", "y");
                    htInsert.Add("LATESTDEL", "m");
                    htInsert.Add("ISOUT", "N");
                    htInsert.Add("ISREV", "N");
                    htInsert.Add("OLDDATA", "N");


                    commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                    if (unitSaleRate < unitFaceValue)
                    {

                        contrlNumber++;

                        htInsert = new Hashtable();
                        htInsert.Add("TRAN_ID", tranNumber + 1);
                        htInsert.Add("ACCCODE", "101020000");
                        htInsert.Add("BANKACNO_CONTRA", "101020000");
                        htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                        htInsert.Add("TRAN_DATE", TranDateTextBox.Text.Trim());
                        htInsert.Add("REMARKS", unitSaleQty.ToString() + " unit surrendered @tk." + unitSaleRate.ToString() + " per unit " + unitRegRepNo);
                            htInsert.Add("TRAN_TYPE", "C");
                        htInsert.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                        htInsert.Add("TOTAL_AMNT", Convert.ToDecimal((unitFaceValue * unitSaleQty) - unitSaleTotalValue));
                        htInsert.Add("CTRLNO", contrlNumber);
                        htInsert.Add("OP_ID", userObj.UserID.ToString());
                        htInsert.Add("VOUCHER_TYPE", "1");
                        htInsert.Add("TERMINAL_NO", acc_terminal_no);
                        htInsert.Add("RECENT", "y");
                        htInsert.Add("LATESTDEL", "m");
                        htInsert.Add("ISOUT", "N");
                        htInsert.Add("ISREV", "N");
                        htInsert.Add("OLDDATA", "N");
                        commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                    }
                    else if (unitSaleRate > unitFaceValue)
                    {
                        contrlNumber++;
                        htInsert = new Hashtable();
                        htInsert.Add("TRAN_ID", tranNumber + 1);
                        htInsert.Add("ACCCODE", "101020000");
                        htInsert.Add("BANKACNO", "101020000");
                        htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                        htInsert.Add("TRAN_DATE", TranDateTextBox.Text.Trim());
                        htInsert.Add("REMARKS", unitSaleQty.ToString() + " unit surrendered @tk." + unitSaleRate.ToString() + " per unit " + unitRegRepNo);
                            htInsert.Add("TRAN_TYPE", "D");
                        htInsert.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                        htInsert.Add("TOTAL_AMNT", Convert.ToDecimal(unitSaleTotalValue - ((unitFaceValue * unitSaleQty))));
                        htInsert.Add("CTRLNO", contrlNumber);
                        htInsert.Add("OP_ID", userObj.UserID.ToString());
                        htInsert.Add("VOUCHER_TYPE", "1");
                        htInsert.Add("TERMINAL_NO", acc_terminal_no);
                        htInsert.Add("RECENT", "y");
                        htInsert.Add("LATESTDEL", "m");
                        htInsert.Add("ISOUT", "N");
                        htInsert.Add("ISREV", "N");
                        htInsert.Add("OLDDATA", "N");
                        commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");
                    }
                    contrlNumber++;
                    htInsert = new Hashtable();
                    htInsert.Add("TRAN_ID", tranNumber + 1);
                    htInsert.Add("ACCCODE", bankPaymentCode);
                    htInsert.Add("BANKACNO_CONTRA", bankPaymentCode);
                    htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                    htInsert.Add("TRAN_DATE", TranDateTextBox.Text.Trim());
                    htInsert.Add("REMARKS", unitSaleQty.ToString() + " unit surrendered @tk." + unitSaleRate.ToString() + " per unit " + unitRegRepNo);
                    htInsert.Add("TRAN_TYPE", "C");
                    htInsert.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                    htInsert.Add("TOTAL_AMNT", Convert.ToDecimal(unitSaleTotalValue));
                    htInsert.Add("CTRLNO", contrlNumber);
                    htInsert.Add("OP_ID", userObj.UserID.ToString());
                    htInsert.Add("VOUCHER_TYPE", "1");
                    htInsert.Add("CHEQUENO", ChequeNoTexBox.Text.Trim().ToString());
                    htInsert.Add("CHEQUE_DATE", ChequeDateTextBox.Text.Trim());
                    htInsert.Add("TERMINAL_NO", acc_terminal_no);
                    htInsert.Add("RECENT", "y");
                    htInsert.Add("LATESTDEL", "m");
                    htInsert.Add("ISOUT", "N");
                    htInsert.Add("ISREV", "N");
                    htInsert.Add("OLDDATA", "N");
                    commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                }

                countCheck++;

            }

            contrlNumber++;
            tranNumber++;
            commonGatewayObj.ExecuteNonQuery(" UPDATE " + accountSchema + ".GL_BASICINFO SET TRAN_ID=" + tranNumber + " , CTRLNO='" + contrlNumber + "' WHERE 1=1");
            commonGatewayObj.CommitTransaction();
        }


        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            throw ex;
        }
       
    }
   
}
