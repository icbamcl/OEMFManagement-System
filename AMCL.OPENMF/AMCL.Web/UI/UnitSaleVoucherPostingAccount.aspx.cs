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

public partial class UI_UnitSaleVoucherPostingAccount : System.Web.UI.Page
{
    //System.Web.UI.Page this_page_ref = null;
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();    
    UnitUser userObj = new UnitUser();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
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
            TranDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");

           
         
        }
    
    }

    protected void findButton_Click(object sender, EventArgs e)
    {
        VoucherNoTexBox.Text = "";
        DataTable dtMoneyReceiptInfo = unitSaleBLObj.dtMoneyReceiptInfo(" AND RECEIPT_DATE BETWEEN '" + fromSaleDateTextBox.Text.Trim() + "' AND '" + toSaleDateTextBox.Text.Trim() + "' AND RECEIPT_TYPE='SL' ");
        if (dtMoneyReceiptInfo.Rows.Count > 0)
        {
            dvGridSurrender.Visible = true;
            SaleListGridView.DataSource = dtUnitSaleForGrid(dtMoneyReceiptInfo);
            SaleListGridView.DataBind();
        }
        else
        {           
            dvGridSurrender.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "popup", "alert('No Data Found')", true);
        }

    }

    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (fundNameDropDownList.SelectedValue.ToString() != "0")
        {
            DataTable dtMoneyReceiptInfo = unitSaleBLObj.dtMoneyReceiptInfo(" AND REG_BK='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "' AND RECEIPT_DATE BETWEEN '" + fromSaleDateTextBox.Text.Trim() + "' AND '" + toSaleDateTextBox.Text.Trim() + "' AND RECEIPT_TYPE='SL' ");
            if (dtMoneyReceiptInfo.Rows.Count > 0)
            {
                
                dvGridSurrender.Visible = true;
                SaleListGridView.DataSource = dtUnitSaleForGrid(dtMoneyReceiptInfo);
                SaleListGridView.DataBind();
                VoucherNoTexBox.Text = unitSaleBLObj.getNexAccountVoucherNo(unitRepBLObj.GetaccountSchema(fundNameDropDownList.SelectedValue.ToString()),"2");

            }
            else
            {
                VoucherNoTexBox.Text = "";
              dvGridSurrender.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "popup", "alert('No Data Found')", true);
            }
        }
        else
        {
            VoucherNoTexBox.Text = "";
            DataTable dtMoneyReceiptInfo = unitSaleBLObj.dtMoneyReceiptInfo(" AND RECEIPT_DATE BETWEEN '" + fromSaleDateTextBox.Text.Trim() + "' AND '" + toSaleDateTextBox.Text.Trim() + "' AND RECEIPT_TYPE='SL' ");
            if (dtMoneyReceiptInfo.Rows.Count > 0)
            {
                dvGridSurrender.Visible = true;
                SaleListGridView.DataSource = dtUnitSaleForGrid(dtMoneyReceiptInfo);
                SaleListGridView.DataBind();

            }
            else
            {
                dvGridSurrender.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "popup", "alert('No Data Found')", true);
            }
        }

    }
    
    protected void SaleVoucherButton_Click(object sender, EventArgs e)
    {
        if (fundNameDropDownList.SelectedValue.ToString() != "0")
        {
            try
            {
                SaveSaleUniAccountVoucher(unitRepBLObj.GetaccountSchema(fundNameDropDownList.SelectedValue.ToString()));

                DataTable dtMoneyReceiptInfo = unitSaleBLObj.dtMoneyReceiptInfo(" AND RECEIPT_DATE BETWEEN '" + fromSaleDateTextBox.Text.Trim() + "' AND '" + toSaleDateTextBox.Text.Trim() + "' AND RECEIPT_TYPE='SL' ");
                dvGridSurrender.Visible = true;
                SaleListGridView.DataSource = dtUnitSaleForGrid(dtMoneyReceiptInfo);
                SaleListGridView.DataBind();
                fundNameDropDownList.SelectedValue = "0";
                VoucherNoTexBox.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Successfully');", true);



            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed Due to exception error');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Please Select a fund to voucher posting ');", true);
        }
    }

    public DataTable dtUnitSaleForGrid(DataTable dtUnitSal)
    {
        DataTable dtUnitSaleForGrid = dtUnitSal.Clone();
     
       
        if (dtUnitSal.Rows.Count > 0)
        {
            DataRow drDataTable;
            for (int loop = 0; loop < dtUnitSal.Rows.Count; loop++)
            {
                drDataTable = dtUnitSaleForGrid.NewRow();
                drDataTable["ID"] = dtUnitSal.Rows[loop]["ID"];
                drDataTable["FUND_NM"] = dtUnitSal.Rows[loop]["FUND_NM"];
                drDataTable["RECEIPT_NO"] = dtUnitSal.Rows[loop]["RECEIPT_NO"];
                drDataTable["SALE_DATE"] =dtUnitSal.Rows[loop]["SALE_DATE"];
                drDataTable["HNAME"] = dtUnitSal.Rows[loop]["HNAME"];
                drDataTable["UNIT_QTY"] = dtUnitSal.Rows[loop]["UNIT_QTY"];
                drDataTable["RATE"] = dtUnitSal.Rows[loop]["RATE"];                
                drDataTable["TOTAL_AMT"] = dtUnitSal.Rows[loop]["TOTAL_AMT"];
                drDataTable["CHQ_DD_NO"] = dtUnitSal.Rows[loop]["CHQ_DD_NO"];
                drDataTable["CHQ_DATE"] = dtUnitSal.Rows[loop]["CHQ_DATE"];                
                drDataTable["SL_REP_DIFF"] = dtUnitSal.Rows[loop]["SL_REP_DIFF"];
                drDataTable["PAY_TYPE"] = dtUnitSal.Rows[loop]["PAY_TYPE"];
                dtUnitSaleForGrid.Rows.Add(drDataTable);

            }
        }

        return dtUnitSaleForGrid;
    }
    public void SaveSaleUniAccountVoucher(string accountSchema)
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
            string shareCapitalCode = "101010000";
            string premiumReserveCode = "101020000";
            string premiumIncomeCode = "302010000";
            

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
            if (unitRepBLObj.IsDuplicateAccVoucherNo(accountSchema, VoucherNoTexBox.Text.Trim(), "2", yearStartDate, yearEndDate))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Failed Duplicate Payment Voucher Number');", true);
            }
            else
            {
                decimal unitSaleQtyCash = 0;
                decimal unitSaleTotalValueCash = 0;
                string moneyReceiptID = "";

                foreach (GridViewRow Drv in SaleListGridView.Rows)
                {

                    decimal unitFaceValue = Convert.ToDecimal(faceValue);
                    decimal unitSaleQty = Convert.ToDecimal(Drv.Cells[5].Text.ToString());
                    decimal unitSaleRate = Convert.ToDecimal(Drv.Cells[6].Text.ToString());
                    decimal unitSaleTotalValue = Convert.ToDecimal(Drv.Cells[7].Text.ToString());
                    decimal unitRateDiffernceValue = Convert.ToDecimal(Drv.Cells[10].Text.ToString());
                    decimal totalBankPayment = Convert.ToDecimal(unitSaleRate * unitSaleQty);
                    decimal totalShareCapital = Convert.ToDecimal(unitFaceValue * unitSaleQty);
                    decimal totalPremiumIncome = Convert.ToDecimal(unitRateDiffernceValue * unitSaleQty);
                    decimal totalPremiumReserve = (totalShareCapital + totalPremiumIncome) - totalBankPayment;


                    if (Drv.Cells[11].Text.ToString().ToUpper() == "CASH")
                    {
                        unitSaleQtyCash = unitSaleQtyCash + Convert.ToDecimal(Drv.Cells[5].Text.ToString());
                        unitSaleTotalValueCash = unitSaleTotalValueCash + Convert.ToDecimal(Drv.Cells[7].Text.ToString());
                        if (moneyReceiptID == "")
                        {
                            moneyReceiptID = moneyReceiptID + Drv.Cells[0].Text.ToString();
                        }
                        else
                        {
                            moneyReceiptID = moneyReceiptID + "," + Drv.Cells[0].Text.ToString();
                        }
                    }
                    else
                    {
                        htUpdate = new Hashtable();
                        htUpdate.Add("ACC_VOUCHER_ENTRY_BY", userObj.UserID.ToString());
                        htUpdate.Add("ACC_VOUCHER_ENTRY_DATE", TranDateTextBox.Text.Trim());
                        htUpdate.Add("ACC_VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                        commonGatewayObj.Update(htUpdate, "MONEY_RECEIPT ", "ID=" + Convert.ToInt64(Drv.Cells[0].Text.ToString()));

                        //Bank Payment
                        contrlNumber++;
                        htInsert = new Hashtable();
                        htInsert.Add("TRAN_ID", tranNumber + 1);
                        htInsert.Add("ACCCODE", bankPaymentCode);
                        htInsert.Add("BANKACNO", bankPaymentCode);
                        htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                        htInsert.Add("TRAN_DATE", TranDateTextBox.Text.Trim());
                        htInsert.Add("REMARKS", unitSaleQty.ToString() + " Unit sold @tk." + unitSaleRate.ToString() + " per unit ");
                        htInsert.Add("TRAN_TYPE", "D");
                        htInsert.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                        htInsert.Add("TOTAL_AMNT", totalBankPayment);
                        htInsert.Add("CTRLNO", contrlNumber);
                        htInsert.Add("OP_ID", userObj.UserID.ToString());
                        htInsert.Add("VOUCHER_TYPE", "2");
                        if (Drv.Cells[11].Text.ToString().ToUpper() == "BOTH")
                        {
                            htInsert.Add("CHEQUENO", Drv.Cells[8].Text.Trim().ToString() + " ,CASH");
                            htInsert.Add("CHEQUE_DATE", Drv.Cells[9].Text.Trim().ToString());

                        }
                        else if (Drv.Cells[11].Text.ToString().ToUpper() == "MULT")
                        {
                         

                        }
                        else 
                        {
                            htInsert.Add("CHEQUENO", Drv.Cells[8].Text.Trim().ToString());
                            htInsert.Add("CHEQUE_DATE", Drv.Cells[9].Text.Trim().ToString());

                        }
                        htInsert.Add("TERMINAL_NO", acc_terminal_no);
                        htInsert.Add("RECENT", "y");
                        htInsert.Add("LATESTDEL", "m");
                        htInsert.Add("ISOUT", "N");
                        htInsert.Add("ISREV", "N");
                        htInsert.Add("OLDDATA", "N");
                        commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");
                        //ShareCapital
                        contrlNumber++;
                        htInsert = new Hashtable();
                        htInsert.Add("TRAN_ID", tranNumber + 1);
                        htInsert.Add("ACCCODE", shareCapitalCode);
                        htInsert.Add("BANKACNO_CONTRA", shareCapitalCode);
                        htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                        htInsert.Add("TRAN_DATE", TranDateTextBox.Text.Trim());
                        htInsert.Add("REMARKS", unitSaleQty.ToString() + " Unit sold @tk." + unitSaleRate.ToString() + " per unit ");
                        htInsert.Add("TRAN_TYPE", "C");
                        htInsert.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                        htInsert.Add("TOTAL_AMNT", totalShareCapital);
                        htInsert.Add("CTRLNO", contrlNumber);
                        htInsert.Add("OP_ID", userObj.UserID.ToString());
                        htInsert.Add("VOUCHER_TYPE", "2");
                        htInsert.Add("TERMINAL_NO", acc_terminal_no);
                        htInsert.Add("RECENT", "y");
                        htInsert.Add("LATESTDEL", "m");
                        htInsert.Add("ISOUT", "N");
                        htInsert.Add("ISREV", "N");
                        htInsert.Add("OLDDATA", "N");
                        commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                        //Premimum Income
                        contrlNumber++;
                        htInsert = new Hashtable();
                        htInsert.Add("TRAN_ID", tranNumber + 1);
                        htInsert.Add("ACCCODE", premiumIncomeCode);
                        htInsert.Add("BANKACNO_CONTRA", premiumIncomeCode);
                        htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                        htInsert.Add("TRAN_DATE", TranDateTextBox.Text.Trim());
                        htInsert.Add("REMARKS", unitSaleQty.ToString() + " Unit sold @tk." + unitSaleRate.ToString() + " per unit ");
                        htInsert.Add("TRAN_TYPE", "C");
                        htInsert.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                        htInsert.Add("TOTAL_AMNT", totalPremiumIncome);
                        htInsert.Add("CTRLNO", contrlNumber);
                        htInsert.Add("OP_ID", userObj.UserID.ToString());
                        htInsert.Add("VOUCHER_TYPE", "2");
                        htInsert.Add("TERMINAL_NO", acc_terminal_no);
                        htInsert.Add("RECENT", "y");
                        htInsert.Add("LATESTDEL", "m");
                        htInsert.Add("ISOUT", "N");
                        htInsert.Add("ISREV", "N");
                        htInsert.Add("OLDDATA", "N");
                        commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                        //premium reserve

                        if (totalPremiumReserve > 0)
                        {
                            contrlNumber++;
                            htInsert = new Hashtable();
                            htInsert.Add("TRAN_ID", tranNumber + 1);
                            htInsert.Add("ACCCODE", premiumReserveCode);
                            htInsert.Add("BANKACNO", premiumReserveCode);
                            htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                            htInsert.Add("TRAN_DATE", TranDateTextBox.Text.Trim());
                            htInsert.Add("REMARKS", unitSaleQty.ToString() + " unit sold @tk." + unitSaleRate.ToString() + " per unit ");
                            htInsert.Add("TRAN_TYPE", "D");
                            htInsert.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                            htInsert.Add("TOTAL_AMNT", totalPremiumReserve);
                            htInsert.Add("CTRLNO", contrlNumber);
                            htInsert.Add("OP_ID", userObj.UserID.ToString());
                            htInsert.Add("VOUCHER_TYPE", "2");
                            htInsert.Add("TERMINAL_NO", acc_terminal_no);
                            htInsert.Add("RECENT", "y");
                            htInsert.Add("LATESTDEL", "m");
                            htInsert.Add("ISOUT", "N");
                            htInsert.Add("ISREV", "N");
                            htInsert.Add("OLDDATA", "N");
                            commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                        }
                        else if (totalPremiumReserve < 0)
                        {
                            contrlNumber++;
                            htInsert = new Hashtable();
                            htInsert.Add("TRAN_ID", tranNumber + 1);
                            htInsert.Add("ACCCODE", premiumReserveCode);
                            htInsert.Add("BANKACNO_CONTRA", premiumReserveCode);
                            htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                            htInsert.Add("TRAN_DATE", TranDateTextBox.Text.Trim());
                            htInsert.Add("REMARKS", unitSaleQty.ToString() + " unit sold @tk." + unitSaleRate.ToString() + " per unit ");
                            htInsert.Add("TRAN_TYPE", "C");
                            htInsert.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                            htInsert.Add("TOTAL_AMNT", totalPremiumReserve * (-1));
                            htInsert.Add("CTRLNO", contrlNumber);
                            htInsert.Add("OP_ID", userObj.UserID.ToString());
                            htInsert.Add("VOUCHER_TYPE", "2");
                            htInsert.Add("TERMINAL_NO", acc_terminal_no);
                            htInsert.Add("RECENT", "y");
                            htInsert.Add("LATESTDEL", "m");
                            htInsert.Add("ISOUT", "N");
                            htInsert.Add("ISREV", "N");
                            htInsert.Add("OLDDATA", "N");
                            commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");
                        }

                    }

                    countCheck++;

                    if (countCheck == SaleListGridView.Rows.Count && unitSaleQtyCash > 0 && unitSaleTotalValueCash > 0)
                    {
                        decimal totalBankPaymentCash = Convert.ToDecimal(unitSaleRate * unitSaleQtyCash);
                        decimal totalShareCapitalCash = Convert.ToDecimal(unitFaceValue * unitSaleQtyCash);
                        decimal totalPremiumIncomeCash = Convert.ToDecimal(unitRateDiffernceValue * unitSaleQtyCash);
                        decimal totalPremiumReserveCash = (totalShareCapitalCash + totalPremiumIncomeCash) - totalBankPaymentCash;
                        htUpdate = new Hashtable();
                        htUpdate.Add("ACC_VOUCHER_ENTRY_BY", userObj.UserID.ToString());
                        htUpdate.Add("ACC_VOUCHER_ENTRY_DATE", TranDateTextBox.Text.Trim());
                        htUpdate.Add("ACC_VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                        commonGatewayObj.Update(htUpdate, "MONEY_RECEIPT ", "ID IN ("+ moneyReceiptID + ")");

                        //Bank Payment
                        contrlNumber++;
                        htInsert = new Hashtable();
                        htInsert.Add("TRAN_ID", tranNumber + 1);
                        htInsert.Add("ACCCODE", bankPaymentCode);
                        htInsert.Add("BANKACNO", bankPaymentCode);
                        htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                        htInsert.Add("TRAN_DATE", TranDateTextBox.Text.Trim());
                        htInsert.Add("REMARKS", unitSaleQtyCash.ToString() + " Unit sold @tk." + unitSaleRate.ToString() + " per unit ");
                        htInsert.Add("TRAN_TYPE", "D");
                        htInsert.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                        htInsert.Add("TOTAL_AMNT", totalBankPaymentCash);
                        htInsert.Add("CTRLNO", contrlNumber);
                        htInsert.Add("OP_ID", userObj.UserID.ToString());
                        htInsert.Add("VOUCHER_TYPE", "2");
                        htInsert.Add("CHEQUENO", "CASH");
                        htInsert.Add("TERMINAL_NO", acc_terminal_no);
                        htInsert.Add("RECENT", "y");
                        htInsert.Add("LATESTDEL", "m");
                        htInsert.Add("ISOUT", "N");
                        htInsert.Add("ISREV", "N");
                        htInsert.Add("OLDDATA", "N");
                        commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");
                        //ShareCapital
                        contrlNumber++;
                        htInsert = new Hashtable();
                        htInsert.Add("TRAN_ID", tranNumber + 1);
                        htInsert.Add("ACCCODE", shareCapitalCode);
                        htInsert.Add("BANKACNO_CONTRA", shareCapitalCode);
                        htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                        htInsert.Add("TRAN_DATE", TranDateTextBox.Text.Trim());
                        htInsert.Add("REMARKS", unitSaleQtyCash.ToString() + " Unit sold @tk." + unitSaleRate.ToString() + " per unit ");
                        htInsert.Add("TRAN_TYPE", "C");
                        htInsert.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                        htInsert.Add("TOTAL_AMNT", totalShareCapitalCash);
                        htInsert.Add("CTRLNO", contrlNumber);
                        htInsert.Add("OP_ID", userObj.UserID.ToString());
                        htInsert.Add("VOUCHER_TYPE", "2");
                        htInsert.Add("TERMINAL_NO", acc_terminal_no);
                        htInsert.Add("RECENT", "y");
                        htInsert.Add("LATESTDEL", "m");
                        htInsert.Add("ISOUT", "N");
                        htInsert.Add("ISREV", "N");
                        htInsert.Add("OLDDATA", "N");
                        commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                        //Premimum Income
                        contrlNumber++;
                        htInsert = new Hashtable();
                        htInsert.Add("TRAN_ID", tranNumber + 1);
                        htInsert.Add("ACCCODE", premiumIncomeCode);
                        htInsert.Add("BANKACNO_CONTRA", premiumIncomeCode);
                        htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                        htInsert.Add("TRAN_DATE", TranDateTextBox.Text.Trim());
                        htInsert.Add("REMARKS", unitSaleQtyCash.ToString() + " Unit sold @tk." + unitSaleRate.ToString() + " per unit ");
                        htInsert.Add("TRAN_TYPE", "C");
                        htInsert.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                        htInsert.Add("TOTAL_AMNT", totalPremiumIncomeCash);
                        htInsert.Add("CTRLNO", contrlNumber);
                        htInsert.Add("OP_ID", userObj.UserID.ToString());
                        htInsert.Add("VOUCHER_TYPE", "2");
                        htInsert.Add("TERMINAL_NO", acc_terminal_no);
                        htInsert.Add("RECENT", "y");
                        htInsert.Add("LATESTDEL", "m");
                        htInsert.Add("ISOUT", "N");
                        htInsert.Add("ISREV", "N");
                        htInsert.Add("OLDDATA", "N");
                        commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                        //premium reserve

                        if (totalPremiumReserve > 0)
                        {
                            contrlNumber++;
                            htInsert = new Hashtable();
                            htInsert.Add("TRAN_ID", tranNumber + 1);
                            htInsert.Add("ACCCODE", premiumReserveCode);
                            htInsert.Add("BANKACNO", premiumReserveCode);
                            htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                            htInsert.Add("TRAN_DATE", TranDateTextBox.Text.Trim());
                            htInsert.Add("REMARKS", unitSaleQtyCash.ToString() + " unit sold @tk." + unitSaleRate.ToString() + " per unit ");
                            htInsert.Add("TRAN_TYPE", "D");
                            htInsert.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                            htInsert.Add("TOTAL_AMNT", totalPremiumReserveCash);
                            htInsert.Add("CTRLNO", contrlNumber);
                            htInsert.Add("OP_ID", userObj.UserID.ToString());
                            htInsert.Add("VOUCHER_TYPE", "2");
                            htInsert.Add("TERMINAL_NO", acc_terminal_no);
                            htInsert.Add("RECENT", "y");
                            htInsert.Add("LATESTDEL", "m");
                            htInsert.Add("ISOUT", "N");
                            htInsert.Add("ISREV", "N");
                            htInsert.Add("OLDDATA", "N");
                            commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                        }
                        else if (totalPremiumReserve < 0)
                        {
                            contrlNumber++;
                            htInsert = new Hashtable();
                            htInsert.Add("TRAN_ID", tranNumber + 1);
                            htInsert.Add("ACCCODE", premiumReserveCode);
                            htInsert.Add("BANKACNO_CONTRA", premiumReserveCode);
                            htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                            htInsert.Add("TRAN_DATE", TranDateTextBox.Text.Trim());
                            htInsert.Add("REMARKS", unitSaleQtyCash.ToString() + " unit sold @tk." + unitSaleRate.ToString() + " per unit ");
                            htInsert.Add("TRAN_TYPE", "C");
                            htInsert.Add("VOUCHER_NO", VoucherNoTexBox.Text.Trim().ToString());
                            htInsert.Add("TOTAL_AMNT", totalPremiumReserveCash * (-1));
                            htInsert.Add("CTRLNO", contrlNumber);
                            htInsert.Add("OP_ID", userObj.UserID.ToString());
                            htInsert.Add("VOUCHER_TYPE", "2");
                            htInsert.Add("TERMINAL_NO", acc_terminal_no);
                            htInsert.Add("RECENT", "y");
                            htInsert.Add("LATESTDEL", "m");
                            htInsert.Add("ISOUT", "N");
                            htInsert.Add("ISREV", "N");
                            htInsert.Add("OLDDATA", "N");
                            commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");
                        }

                    }
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
