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

public partial class UI_UnitSIPEFTPaidEntry : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    BaseClass bcContent = new BaseClass();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    UnitRepurchaseBL unitRepBLObj = new UnitRepurchaseBL();
    UnitSIPGetSet unitSIPObj = new UnitSIPGetSet();
    UnitSIPBL unitSIPBLObj = new UnitSIPBL();
    UnitReport reportObj = new UnitReport();
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
            sipDayDropDownList.DataSource = unitSIPBLObj.dtSIPDayListDDL(" AND SIP_DETAILS.EFT_PULL_REQUEST_DATE IS NOT NULL   AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.EFT_PAID_ENTRY_BY IS NULL   ");
            sipDayDropDownList.DataTextField = "DEBIT_DATE";
            sipDayDropDownList.DataValueField = "ID";
            sipDayDropDownList.DataBind();

        }
       
    
    }
    
    
    private void ClearText()
    {               
       
       
    }
    
    protected void addListButton_Click(object sender, EventArgs e)
    {

        addtoList();
    }




    protected void sipDayDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dtSIPListforSchedule = unitSIPBLObj.dtSIPDetailsList(" AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.EFT_PULL_REQUEST_DATE IS NOT NULL AND SIP_DETAILS.EFT_PULL_REQUEST_BY IS NOT NULL AND SIP_DETAILS.EFT_PAID_ENTRY_BY IS NULL AND SIP_DETAILS.EFT_PAID_ENTRY_DATE IS NULL AND SIP_DETAILS.DEBIT_DATE='" + sipDayDropDownList.SelectedValue.ToString() + "'");
        if (dtSIPListforSchedule.Rows.Count > 0)
        {
            DataTable dtFundInfo = commonGatewayObj.Select("SELECT * FROM FUND_INFO WHERE FUND_CD='" + regObj.FundCode + "'");
            fundAccNameLabel.Text = dtFundInfo.Rows[0]["SIP_FUND_ACC_NAME"].ToString();
            fundAccNoLabel.Text = dtFundInfo.Rows[0]["SIP_FUND_ACC_NO"].ToString();

            DataTable dtBankBracnhInfo = unitHolderRegBLObj.dtGetBankBracnhInfo(dtFundInfo.Rows[0]["SIP_FUND_ACC_ROUTING_NO"].Equals(DBNull.Value) ? "0" : dtFundInfo.Rows[0]["SIP_FUND_ACC_ROUTING_NO"].ToString());
            string bankInfo = "";
            if (dtBankBracnhInfo.Rows.Count > 0)
            {
                BankNameLabel.Text = reportObj.getBankNameByBankCode(Convert.ToInt32(dtBankBracnhInfo.Rows[0]["BANK_CODE"].ToString())).ToString();
                bankInfo = reportObj.getBankBranchNameByCode(Convert.ToInt32(dtBankBracnhInfo.Rows[0]["BANK_CODE"].ToString()), Convert.ToInt32(dtBankBracnhInfo.Rows[0]["BRANCH_CODE"].ToString())).ToString() + "[" + dtFundInfo.Rows[0]["SIP_FUND_ACC_ROUTING_NO"] + "]";
                BranchNameLabel.Text = bankInfo;
            }
            string fileLocation = ConfigReader.SipEftPaidFileLocation.ToString() + "\\" + regObj.FundCode.ToUpper() + "\\";
            string sipBankStatement = fileLocation + regObj.FundCode.ToUpper()+"_"+ sipDayDropDownList.SelectedValue.ToString() + ".csv";
           
            if (File.Exists(sipBankStatement))
            {
                FileStatusLabel.Text = "Bank Statement File Uploaded On That Date ";
                
            }
            else
            {
                FileStatusLabel.Text = " ";
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
            ClearText();
        }

    }

    protected void uploadFileButton_Click(object sender, EventArgs e)
    {
        string fileLocation = ConfigReader.SipEftPaidFileLocation.ToString() + "\\" + regObj.FundCode.ToUpper() + "\\";
        if ((bankStatementUpload.PostedFile != null) && (bankStatementUpload.PostedFile.ContentLength > 0))
        {
            string fn = regObj.FundCode.ToUpper() + "_" + sipDayDropDownList.SelectedValue.ToString() + ".csv";
            string SaveLocation = fileLocation + fn;

            try
            {
                if (!File.Exists(SaveLocation))
                {
                    bankStatementUpload.PostedFile.SaveAs(SaveLocation);
                    FileStatusLabel.Text = "Bank Statement File Uploaded Successfully ";
                }
                else
                {
                    FileStatusLabel.Text = "Bank Statement File Uploaded On That Date ";

                }

            }
            catch (Exception ex)
            {


            }
        }
        else
        {

        }
    }

    protected void saveSIPPaidButton_Click(object sender, EventArgs e)
    {
        int countCheck = 0;
        long issueTotalUnits = 0;
        decimal issueRate = 0;
        decimal saleRepDiff = 0;
        string fundScheme = "";
        string liabiliesAccCode = "";
        string SIPIds = "";

        Hashtable htUpdate = new Hashtable();
        try
        {
            
            commonGatewayObj.BeginTransaction();


            foreach (GridViewRow Drv in SaleListGridView.Rows)
            {
                int scheduleNo = 0;
                long SIPNo = 0;
                long unitQty = 0;
                decimal totalAmount = 0;
                decimal amount = 0;
                decimal unitRate = 0;               
                decimal currentFracAmt = 0;
                decimal PreFracAmt = 0;
                decimal accAmtAdd = 0;

                CheckBox leftCheckBox = (CheckBox)SaleListGridView.Rows[countCheck].FindControl("leftCheckBox");

                if (leftCheckBox.Checked)
                {
                    if(SIPIds=="")
                    {
                        SIPIds = Drv.Cells[1].Text.ToString();
                    }
                    else
                    {
                        SIPIds = SIPIds+","+ Drv.Cells[1].Text.ToString();
                    }
                                     
                    unitRate = unitSaleBLObj.GetDateWisePrice(regObj, "SL", sipDayDropDownList.SelectedValue.ToString());

                    DataTable dtSaleDiscount=commonGatewayObj.Select (" SELECT NVL(SIP_SALE_DISCOUNT_AMT,0) AS SIP_SALE_DISCOUNT_AMT FROM FUND_INFO WHERE FUND_CD='" + regObj.FundCode.ToString() + "'");
                    issueRate = unitRate- Convert.ToDecimal(dtSaleDiscount.Rows[0]["SIP_SALE_DISCOUNT_AMT"]);
                    amount = Convert.ToDecimal(Drv.Cells[8].Text.ToUpper().ToString());
                    scheduleNo = Convert.ToInt32(Drv.Cells[7].Text.ToString());
                    SIPNo = Convert.ToInt32(Drv.Cells[6].Text.ToString());                   
                    liabiliesAccCode = Drv.Cells[9].Text.ToString();
                    fundScheme = Drv.Cells[10].Text.ToString();
                    saleRepDiff = Convert.ToDecimal( Drv.Cells[11].Text.ToString());

                 //   < " + Convert.ToInt32(scheduleNo)+ "
                    DataTable dtFractionalAmtCheq = commonGatewayObj.Select(" SELECT * FROM SIP_DETAILS WHERE SIP_DETAILS.REG_BK='" + regObj.FundCode.ToString() + "' AND SIP_DETAILS.SIP_NO=" + SIPNo + " AND SIP_DETAILS.SCHEDULE_NO< " + Convert.ToInt32(scheduleNo) + " AND EFT_PAID_STATUS='P' ORDER BY SCHEDULE_NO DESC ");
                    if (scheduleNo>1 && dtFractionalAmtCheq.Rows.Count>0)
                    {
                        DataTable dtFractionalAmount = commonGatewayObj.Select(" SELECT * FROM SIP_DETAILS WHERE SIP_DETAILS.REG_BK='" + regObj.FundCode.ToString() + "' AND SIP_DETAILS.SIP_NO=" + SIPNo + " AND SIP_DETAILS.SCHEDULE_NO< " + Convert.ToInt32(scheduleNo) + " AND EFT_PAID_STATUS='P'  ORDER BY SCHEDULE_NO DESC ");
                        PreFracAmt = Convert.ToDecimal(dtFractionalAmount.Rows[0]["FRAC_AMT"].ToString());
                        totalAmount = amount + PreFracAmt;
                        currentFracAmt = totalAmount % issueRate;
                        unitQty = Convert.ToInt32((totalAmount - currentFracAmt) / issueRate);

                        if(unitQty* issueRate > amount)
                        {
                            accAmtAdd = (amount - totalAmount)+ currentFracAmt;
                        }
                        else
                        {
                            accAmtAdd = amount % issueRate;
                        }

                        issueTotalUnits = issueTotalUnits + unitQty;
                    }
                    else 
                    {
                        currentFracAmt = amount % issueRate;
                        accAmtAdd = currentFracAmt;
                        unitQty =Convert.ToInt32( (amount - currentFracAmt) / issueRate);

                        issueTotalUnits = issueTotalUnits + unitQty;
                    }
                    htUpdate = new Hashtable();
                    htUpdate.Add("EFT_PAID_STATUS", "P");
                    htUpdate.Add("EFT_PAID_DATE", sipStartDateTextBox.Text.ToString());
                    htUpdate.Add("EFT_PAID_ENTRY_BY", userObj.UserID.ToString());
                    htUpdate.Add("EFT_PAID_ENTRY_DATE", DateTime.Now);
                    htUpdate.Add("UNIT_RATE", issueRate);
                    htUpdate.Add("UNIT_QTY", unitQty);
                    htUpdate.Add("FRAC_AMT", currentFracAmt);
                    htUpdate.Add("ACC_FRAC_AMT_ADD", accAmtAdd);
                    commonGatewayObj.Update(htUpdate, "SIP_DETAILS ", "REG_BK='" + Drv.Cells[2].Text.ToUpper().ToString() + "' AND REG_BR='" + Drv.Cells[3].Text.ToUpper().ToString() + "' AND REG_NO=" + Convert.ToInt32(Drv.Cells[4].Text.ToString()) + " AND SIP_NO=" + SIPNo + " AND SCHEDULE_NO=" + scheduleNo + " AND SIP_DETAILS.DEBIT_DATE = '" + sipDayDropDownList.SelectedValue.ToString() + "'");                   
                   
                }
                countCheck++;
               
            }
            if(issueTotalUnits > 0)
            {
                SaveSaleUniAccountVoucher(fundScheme, liabiliesAccCode, issueTotalUnits, issueRate, saleRepDiff, SIPIds, sipStartDateTextBox.Text.ToString());
                commonGatewayObj.CommitTransaction();
                sipDayDropDownList.DataSource = unitSIPBLObj.dtSIPDayListDDL(" AND SIP_DETAILS.EFT_PULL_REQUEST_DATE IS NOT NULL  AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.EFT_PAID_ENTRY_BY IS NULL   ");
                sipDayDropDownList.DataTextField = "DEBIT_DATE";
                sipDayDropDownList.DataValueField = "ID";
                sipDayDropDownList.DataBind();
                SaleListGridView.DataSource = dtGetNullTableForGrid();
                SaleListGridView.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Successfully');", true);
            }
            else
            {
                sipDayDropDownList.DataSource = unitSIPBLObj.dtSIPDayListDDL(" AND SIP_DETAILS.EFT_PULL_REQUEST_DATE IS NOT NULL  AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.EFT_PAID_ENTRY_BY IS NULL   ");
                sipDayDropDownList.DataTextField = "DEBIT_DATE";
                sipDayDropDownList.DataValueField = "ID";
                sipDayDropDownList.DataBind();
                SaleListGridView.DataSource = dtGetNullTableForGrid();
                SaleListGridView.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
            }
            

        }
        catch (Exception ex)
        {
            int counnn=countCheck;
            commonGatewayObj.RollbackTransaction();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed');", true);
        }

    }

    protected void saveSIPUnPaidButton_Click(object sender, EventArgs e)
    {
        int countCheck = 0;
        Hashtable htUpdate = new Hashtable();
        try
        {
            commonGatewayObj.BeginTransaction();
            foreach (GridViewRow Drv in SaleListGridView.Rows)
            {
                int scheduleNo = 0;
                long SIPNo = 0;
              
                decimal amount = 0;
                decimal unitRate = 0;
                

                CheckBox leftCheckBox = (CheckBox)SaleListGridView.Rows[countCheck].FindControl("leftCheckBox");

                if (leftCheckBox.Checked)
                {


                    unitRate = unitSaleBLObj.GetDateWisePrice(regObj, "SL", sipDayDropDownList.SelectedValue.ToString());
                    amount = Convert.ToDecimal(Drv.Cells[8].Text.ToUpper().ToString());
                    scheduleNo = Convert.ToInt32(Drv.Cells[7].Text.ToString());
                    SIPNo = Convert.ToInt32(Drv.Cells[6].Text.ToString());

                    htUpdate = new Hashtable();
                    htUpdate.Add("EFT_PAID_STATUS", "U");
                    htUpdate.Add("EFT_RETURN_DATE", sipStartDateTextBox.Text.ToString());
                    htUpdate.Add("EFT_PAID_ENTRY_BY", userObj.UserID.ToString());
                    htUpdate.Add("EFT_PAID_ENTRY_DATE", DateTime.Now);

                    commonGatewayObj.Update(htUpdate, "SIP_DETAILS ", "REG_BK='" + Drv.Cells[2].Text.ToUpper().ToString() + "' AND REG_BR='" + Drv.Cells[3].Text.ToUpper().ToString() + "' AND REG_NO=" + Convert.ToInt32(Drv.Cells[4].Text.ToString()) + " AND SIP_NO=" + SIPNo + " AND SCHEDULE_NO=" + scheduleNo + " AND SIP_DETAILS.DEBIT_DATE = '" + sipDayDropDownList.SelectedValue.ToString() + "'");
                    commonGatewayObj.CommitTransaction();
                }

                countCheck++;

            }

           
           
            sipDayDropDownList.DataSource = unitSIPBLObj.dtSIPDayListDDL(" AND SIP_DETAILS.EFT_PULL_REQUEST_DATE IS NOT NULL AND   SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.EFT_PAID_ENTRY_BY IS NULL   ");
            sipDayDropDownList.DataTextField = "DEBIT_DATE";
            sipDayDropDownList.DataValueField = "ID";
            sipDayDropDownList.DataBind();
            SaleListGridView.DataSource = dtGetNullTableForGrid();
            SaleListGridView.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Successfully');", true);

        }
        catch (Exception ex)
        {

            commonGatewayObj.RollbackTransaction();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed');", true);
        }
    }
    public void SaveSaleUniAccountVoucher(string accountSchema, string liabiliesAccCode, long totalUnits, decimal unitRate, decimal saleRepDiff, string SIPIds, string tranDate)
    {

        try
        {
           
            long tranNumber = commonGatewayObj.GetMaxNo(accountSchema + ".GL_BASICINFO", "TRAN_ID");
            long contrlNumber = commonGatewayObj.GetMaxNo(accountSchema + ".GL_BASICINFO", "TO_NUMBER(CTRLNO)") - 1;
            string fundCode = regObj.FundCode;
            string acc_op_id = unitRepBLObj.getAcc_OP_ID(fundCode.ToString());
            long acc_terminal_no = unitRepBLObj.getAcc_terminal_no(fundCode.ToString());

            int faceValue = unitRepBLObj.getUnitFaceValue(fundCode.ToString());           
            string getUnitFundBankCode = unitRepBLObj.getUnitFundBankCode(fundCode.ToString());
            string shareCapitalCode = "101010000";
            string premiumReserveCode = "101020000";
            string premiumIncomeCode = "302010000";
            string SIP_ACC_FRAC_LIB_CODE = liabiliesAccCode;
                      
            Hashtable htInsert = new Hashtable();
            Hashtable htUpdate = new Hashtable();
            commonGatewayObj.BeginTransaction();
            DataTable dtFundYearEndInfo = commonGatewayObj.Select("SELECT * FROM FUND_INFO WHERE FUND_CD='" + fundCode.ToString() + "'");
            int year = Convert.ToDateTime(DateTime.Now.ToString()).Year;
            int month = Convert.ToDateTime(DateTime.Now.ToString()).Month;
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
            //   string VoucherNo = unitSaleBLObj.getNexAccountVoucherNo(unitRepBLObj.GetaccountSchema(fundCode.ToString()), "9", yearStartDate, yearEndDate);
            string VoucherNo = unitSaleBLObj.getNexAccountVoucherNo(unitRepBLObj.GetaccountSchema(fundCode.ToString()), "9");


            decimal unitFaceValue = Convert.ToDecimal(faceValue);
            decimal unitSaleQty = Convert.ToDecimal(totalUnits);
            decimal unitSaleRate = Convert.ToDecimal(unitRate);
            decimal unitSaleTotalValue = unitSaleQty * unitSaleRate;
            decimal unitRateDiffernceValue = saleRepDiff;
            decimal totalBankPayment = Convert.ToDecimal(unitSaleRate * unitSaleQty);
            decimal totalShareCapital = Convert.ToDecimal(unitFaceValue * unitSaleQty);
            decimal totalPremiumIncome = Convert.ToDecimal(unitRateDiffernceValue * unitSaleQty);
            decimal totalPremiumReserve = (totalShareCapital + totalPremiumIncome) - totalBankPayment;


            //SIP Liabilities Debit
            contrlNumber++;
            htInsert = new Hashtable();
            htInsert.Add("TRAN_ID", tranNumber + 1);
            htInsert.Add("ACCCODE", SIP_ACC_FRAC_LIB_CODE);
            htInsert.Add("BANKACNO", SIP_ACC_FRAC_LIB_CODE);
            htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
            htInsert.Add("TRAN_DATE", tranDate);
            htInsert.Add("REMARKS", " Through SIP " + unitSaleQty.ToString() + " Unit sold @tk." + unitSaleRate.ToString() + " per unit");
            htInsert.Add("TRAN_TYPE", "D");
            htInsert.Add("VOUCHER_NO", VoucherNo);
            htInsert.Add("TOTAL_AMNT", totalBankPayment);
            htInsert.Add("CTRLNO", contrlNumber);
            htInsert.Add("OP_ID", userObj.UserID.ToString());
            htInsert.Add("VOUCHER_TYPE", "9");           
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
            htInsert.Add("TRAN_DATE", tranDate);
            htInsert.Add("REMARKS", " Through SIP " + unitSaleQty.ToString() + " Unit sold @tk." + unitSaleRate.ToString() + " per unit ");
            htInsert.Add("TRAN_TYPE", "C");
            htInsert.Add("VOUCHER_NO", VoucherNo);
            htInsert.Add("TOTAL_AMNT", totalShareCapital);
            htInsert.Add("CTRLNO", contrlNumber);
            htInsert.Add("OP_ID", userObj.UserID.ToString());
            htInsert.Add("VOUCHER_TYPE", "9");
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
            htInsert.Add("TRAN_DATE", tranDate);
            htInsert.Add("REMARKS", " Through SIP " + unitSaleQty.ToString() + " Unit sold @tk." + unitSaleRate.ToString() + " per unit ");
            htInsert.Add("TRAN_TYPE", "C");
            htInsert.Add("VOUCHER_NO", VoucherNo);
            htInsert.Add("TOTAL_AMNT", totalPremiumIncome);
            htInsert.Add("CTRLNO", contrlNumber);
            htInsert.Add("OP_ID", userObj.UserID.ToString());
            htInsert.Add("VOUCHER_TYPE", "9");
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
                htInsert.Add("TRAN_DATE", tranDate);
                htInsert.Add("REMARKS", " Through SIP " + unitSaleQty.ToString() + " unit sold @tk." + unitSaleRate.ToString() + " per unit ");
                htInsert.Add("TRAN_TYPE", "D");
                htInsert.Add("VOUCHER_NO", VoucherNo);
                htInsert.Add("TOTAL_AMNT", totalPremiumReserve);
                htInsert.Add("CTRLNO", contrlNumber);
                htInsert.Add("OP_ID", userObj.UserID.ToString());
                htInsert.Add("VOUCHER_TYPE", "9");
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
                htInsert.Add("TRAN_DATE", tranDate);
                htInsert.Add("REMARKS", " Through SIP " + unitSaleQty.ToString() + " unit sold @tk." + unitSaleRate.ToString() + " per unit ");
                htInsert.Add("TRAN_TYPE", "C");
                htInsert.Add("VOUCHER_NO", VoucherNo);
                htInsert.Add("TOTAL_AMNT", totalPremiumReserve * (-1));
                htInsert.Add("CTRLNO", contrlNumber);
                htInsert.Add("OP_ID", userObj.UserID.ToString());
                htInsert.Add("VOUCHER_TYPE", "9");
                htInsert.Add("TERMINAL_NO", acc_terminal_no);
                htInsert.Add("RECENT", "y");
                htInsert.Add("LATESTDEL", "m");
                htInsert.Add("ISOUT", "N");
                htInsert.Add("ISREV", "N");
                htInsert.Add("OLDDATA", "N");
                commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");
            }

            //UPDATE TRANSACTION NUMBER AND CONTROL NUMBER
            contrlNumber++;
            tranNumber++;
            commonGatewayObj.ExecuteNonQuery(" UPDATE " + accountSchema + ".GL_BASICINFO SET TRAN_ID=" + tranNumber + " , CTRLNO='" + contrlNumber + "' WHERE 1=1");

            //UPDATE TRANSACTION NUMBER AND CONTROL NUMBER
            commonGatewayObj.ExecuteNonQuery(" UPDATE SIP_DETAILS SET ACC_VOUCHER_NO='" + VoucherNo + "' WHERE ID IN ("+SIPIds+")");

            commonGatewayObj.CommitTransaction();
          

        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            throw ex;
        }

    }
    public void addtoList()
    {
        DataTable dtSIPListforSchedule = unitSIPBLObj.dtSIPDetailsList(" AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.EFT_PULL_REQUEST_DATE IS NOT NULL  AND SIP_DETAILS.EFT_PAID_ENTRY_BY IS NULL AND SIP_DETAILS.EFT_PAID_ENTRY_DATE IS NULL AND SIP_DETAILS.DEBIT_DATE='" + sipDayDropDownList.SelectedValue.ToString() + "'");
        if (dtSIPListforSchedule.Rows.Count > 0)
        {
            DataTable dtSIPInforation = new DataTable();
            string fileLocation = ConfigReader.SipEftPaidFileLocation.ToString() + "\\" + regObj.FundCode.ToUpper() + "\\";
            string sipBankStatement = fileLocation + regObj.FundCode.ToUpper() + "_" + sipDayDropDownList.SelectedValue.ToString() + ".csv";
            string sipNumberTotal = "";
            if (File.Exists(sipBankStatement))
            {

                StreamReader srFileReader;
                string line;
                srFileReader = new StreamReader(sipBankStatement);
                string[] lineContent;
                while (srFileReader.Peek() != -1)
                {
                    line = srFileReader.ReadLine();
                    lineContent = line.Split(',');
                    if (lineContent.Length > 0)
                    {
                        string[] sipSpliet = lineContent[1].Split(' ');
                        string[] extractSip = sipSpliet[sipSpliet.Length - 1].Split('/');
                        long sipNumber = Convert.ToInt64(extractSip[2].ToString());
                        if (sipNumberTotal != "")
                        {
                            sipNumberTotal = sipNumberTotal + "," + sipNumber.ToString();
                        }
                        else
                        {
                            sipNumberTotal = sipNumber.ToString();
                        }
                    }
                }
            }
            if (sipNumberTotal != "")
                dtSIPInforation = unitSIPBLObj.dtSIPDetailsList(" AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.EFT_PULL_REQUEST_DATE IS NOT NULL AND SIP_DETAILS.EFT_PULL_REQUEST_BY IS NOT NULL AND SIP_DETAILS.EFT_PAID_ENTRY_BY IS NULL AND SIP_DETAILS.EFT_PAID_ENTRY_DATE IS NULL AND SIP_DETAILS.DEBIT_DATE = '" + sipDayDropDownList.SelectedValue.ToString() + "' AND SIP_DETAILS.SIP_NO IN (" + sipNumberTotal + ")");
            else
                dtSIPInforation = dtSIPListforSchedule;

            if (dtSIPInforation.Rows.Count > 0)
            {

                SaleListGridView.DataSource = dtSIPInforation;
                SaleListGridView.DataBind();
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
           
        }
    }
    private DataTable dtGetNullTableForGrid()
    {
        DataTable dtTotalInstallmentList = new DataTable();
        dtTotalInstallmentList.Columns.Add("ID1", typeof(string));
        dtTotalInstallmentList.Columns.Add("REG_BK", typeof(string));
        dtTotalInstallmentList.Columns.Add("REG_BR", typeof(string));
        dtTotalInstallmentList.Columns.Add("REG_NO", typeof(string));
        dtTotalInstallmentList.Columns.Add("HNAME", typeof(string));
        dtTotalInstallmentList.Columns.Add("SIP_NO", typeof(string));
        dtTotalInstallmentList.Columns.Add("SCHEDULE_NO", typeof(string));       
        dtTotalInstallmentList.Columns.Add("AMOUNT", typeof(string));
        dtTotalInstallmentList.Columns.Add("SIP_ACC_FRAC_LIB_CODE", typeof(string));
        dtTotalInstallmentList.Columns.Add("ACCOUNT_SCHEMA", typeof(string));
        dtTotalInstallmentList.Columns.Add("SL_REP_DIFF", typeof(string));


        return dtTotalInstallmentList;
    }
}
