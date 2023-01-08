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

public partial class UI_UnitSaleEntryCDSWithMoneyRct : System.Web.UI.Page
{
   // System.Web.UI.Page this_page_ref = null;
    OMFDAO opendMFDAO = new OMFDAO();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    UnitHolderRegBL regBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    string errorMassege = "";
    BaseClass bcContent = new BaseClass();
    EncryptDecrypt encrypt = new EncryptDecrypt();
    string CDSStatus = "";
    CommonGateway commonGatewayObj = new CommonGateway();

    protected void Page_Load(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj=new UnitHolderRegistration();
           
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
        fundCodeTextBox.Text = fundCode.ToString();
        branchCodeTextBox.Text = branchCode.ToString();
        CDSStatus = bcContent.CDS.ToString().ToUpper();
                        
        
        regNoTextBox.Focus();
        //saleDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
       // holderDateofBirthTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        if (!IsPostBack)
        {
            bankNameDropDownList.DataSource = opendMFDAO.dtFillBankName(" CATE_CODE=1 ");
            bankNameDropDownList.DataTextField = "BANK_NAME";
            bankNameDropDownList.DataValueField = "BANK_CODE";
            bankNameDropDownList.DataBind();
            CashAmountTextBox.Enabled = false;
            MultiplePayTypeTextBox.Enabled = false;
        }
    
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        
        
        UnitHolderRegistration regObj = new UnitHolderRegistration();       
        regObj.FundCode = fundCodeTextBox.Text;
        regObj.BranchCode = branchCodeTextBox.Text;        
        regObj.RegNumber = regNoTextBox.Text;


       
        UnitSale saleObj = new UnitSale();
        saleObj.SaleNo = Convert.ToInt32(saleNumberTextBox.Text.Trim().ToString());
        if (moneyReceipDropDownList.SelectedValue.ToString() != "0")
        {
            saleObj.MoneyReceiptNo = Convert.ToInt32(moneyReceipDropDownList.SelectedItem.Text.ToString());
        }
        if (ChqRadioButton.Checked)
        {
            saleObj.PaymentType = ChequeTypeDropDownList.SelectedValue.ToString();
            if (CHQDDNoRemarksTextBox.Text.Trim() != "")
            {
                saleObj.ChequeNo = CHQDDNoRemarksTextBox.Text.Trim().ToString();
            }
            if (chequeDateTextBox.Text.Trim() != "")
            {
                saleObj.ChequeDate = chequeDateTextBox.Text.Trim().ToString();
            }
            if (bankNameDropDownList.SelectedValue.Trim() != "0")
            {
                saleObj.BankCode = Convert.ToInt16(bankNameDropDownList.SelectedValue.ToString());
            }
            if (branchNameDropDownList.SelectedValue.Trim() != "0" && branchNameDropDownList.SelectedValue.Trim() != "")
            {
                saleObj.BranchCode = Convert.ToInt16(branchNameDropDownList.SelectedValue.ToString());
            }
        }
        else if (CashRadioButton.Checked)
        {
            saleObj.PaymentType = "CASH";
            saleObj.CashAmount = Convert.ToDecimal(Convert.ToDecimal(saleRateTextBox.Text.Trim().ToString()) * Convert.ToInt32(unitQtyTextBox.Text.Trim().ToString()));
        }
        else if (BothRadioButton.Checked)
        {
            saleObj.PaymentType = "BOTH";
            saleObj.ChequeNo = CHQDDNoRemarksTextBox.Text.Trim().ToString();
            if (CHQDDNoRemarksTextBox.Text.Trim() != "")
            {
                saleObj.ChequeNo = CHQDDNoRemarksTextBox.Text.Trim().ToString();
            }
            if (chequeDateTextBox.Text.Trim() != "")
            {
                saleObj.ChequeDate = chequeDateTextBox.Text.Trim().ToString();
            }
            if (bankNameDropDownList.SelectedValue.Trim() != "0")
            {
                saleObj.BankCode = Convert.ToInt16(bankNameDropDownList.SelectedValue.ToString());
            }
            if (branchNameDropDownList.SelectedValue.Trim() != "0" || branchNameDropDownList.SelectedValue.Trim() != "")
            {
                saleObj.BranchCode = Convert.ToInt16(branchNameDropDownList.SelectedValue.ToString());
            }
            if (CashAmountTextBox.Text.Trim() != "")
            {
                saleObj.CashAmount = Convert.ToDecimal(CashAmountTextBox.Text.Trim().ToString());
            }
        }
        else if (MultiRadioButton.Checked)
        {
            saleObj.PaymentType = "MULTI";
            if (MultiplePayTypeTextBox.Text.Trim() != "")
            {
                saleObj.MultiPayType = MultiplePayTypeTextBox.Text.Trim().ToString();
            }
        }
        saleObj.SaleRate = Convert.ToDecimal(saleRateTextBox.Text.Trim().ToString());
        saleObj.SaleType = saleTypeDropDownList.SelectedValue.ToString().ToUpper();
        saleObj.SaleUnitQty = Convert.ToInt32(unitQtyTextBox.Text.Trim().ToString());
        saleObj.SaleDate = saleDateTextBox.Text.Trim().ToString();
        saleObj.SaleRemarks = saleRemarksTextBox.Text.Trim().ToString();
        int saleLimitLower=unitSaleBLObj.SaleLimitLower(regObj);
        long saleLimmitUpper=unitSaleBLObj.SaleLimitUpper(regObj); 

        
       
        try
        {
            if (unitSaleBLObj.IsSaleLock(regObj))//Cheking Lock in status
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Error().ToString() + " " + " Sale Operation is Locked " + "');", true);
            }
            else if (unitSaleBLObj.IsDuplicateSale(regObj, saleObj))// Checking Duplicate Sale No
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " " + "Sale Number " + "');", true);
            }
            else if (saleLimitLower > Convert.ToInt32(unitQtyTextBox.Text.Trim().ToString()) && saleTypeDropDownList.SelectedValue.ToString() == "SL")
            {
                unitQtyTextBox.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Sale Unit Quantity can not less than " + saleLimitLower.ToString() + "');", true);

            }
            else if (saleLimmitUpper < Convert.ToInt32(unitQtyTextBox.Text.Trim().ToString()))
            {
                unitQtyTextBox.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Sale Unit Quantity can not greater than " + saleLimmitUpper.ToString() + "');", true);

            }            
            else
            {
                commonGatewayObj.BeginTransaction();
                commonGatewayObj.ExecuteNonQuery("UPDATE U_MASTER SET SELLING_AGENT_ID=" + saleObj.SellingAgentCode + "  WHERE  REG_BK='" + regObj.FundCode + "' AND REG_BR='" + regObj.BranchCode + "' AND REG_NO='" + regObj.RegNumber + "' AND SELLING_AGENT_ID IS NULL");
                commonGatewayObj.ExecuteNonQuery("UPDATE MONEY_RECEIPT SET SL_REP_TR_RN_NO=" + saleObj.SaleNo + "  WHERE ID=" + Convert.ToUInt64(moneyReceipDropDownList.SelectedValue.ToString()));
                unitSaleBLObj.SaveUnitSaleCDS(regObj, saleObj,  userObj);       
                commonGatewayObj.CommitTransaction();
                ClearText();
                moneyReceipDropDownList.DataSource = unitSaleBLObj.dtMoneyRecieptInfoforDDL(regObj, "SL");
                moneyReceipDropDownList.DataTextField = "RECEIPT_NO";
                moneyReceipDropDownList.DataValueField = "ID";
                moneyReceipDropDownList.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Success().ToString() + "');", true);
       

            }

        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            errorMassege = msgObj.ExceptionErrorMessageString(ex.Message.ToString());
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + errorMassege.ToString() + "');", true);
        }


    }
    private void ClearText()
    {
        DataTable dtDino = opendMFDAO.getTableDinomination();
        saleDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        holderNameTextBox.Text="";
        regNoTextBox.Text = "";
        jHolderTextBox.Text="";
        holderAddress1TextBox.Text="";
        holderAddress2TextBox.Text="";       
        holderTelphoneTextBox.Text="";        
        tdCIP.InnerHtml = "";
        saleTypeDropDownList.SelectedValue = "SL";
        saleNumberTextBox.Text = "";
        saleRateTextBox.Text = "";        
        unitQtyTextBox.Text = "";
        saleRemarksTextBox.Text = "";

        moneyReceipDropDownList.SelectedValue = "0";
        
        ChqRadioButton.Checked = true;
        ChequeTypeDropDownList.SelectedValue = "CHQ";
        CashRadioButton.Checked = false;
        BothRadioButton.Checked = false;
        MultiRadioButton.Checked = false;
        CHQDDNoRemarksTextBox.Text = "";
        CashAmountTextBox.Text = "";
        MultiplePayTypeTextBox.Text = "";
        chequeDateTextBox.Text = "";
        bankNameDropDownList.SelectedValue = "0";
        branchNameDropDownList.SelectedValue = "0";
        sellingAgentCodeTextBox.Text = "";
    }
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }
    protected void findButton_Click(object sender, EventArgs e)
    {
       
        displayRegInfo();

        UnitSale saleObj = new UnitSale();
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeTextBox.Text;
        regObj.BranchCode = branchCodeTextBox.Text;
        saleNumberTextBox.Text = unitSaleBLObj.getNextSaleNo(regObj, userObj).ToString();       
        saleObj.SaleNo = opendMFDAO.GetMaxSaleNo(regObj)-1;
      
    }
    
    public void displayRegInfo()
    {
        UnitSale saleObj = new UnitSale();
        UnitHolderRegistration unitRegObj = new UnitHolderRegistration();
        unitRegObj.FundCode = fundCodeTextBox.Text.Trim();
        unitRegObj.BranchCode = branchCodeTextBox.Text.Trim();
        unitRegObj.RegNumber = regNoTextBox.Text.Trim();
        
        if (opendMFDAO.IsValidRegistration(unitRegObj))
        {
            moneyReceipDropDownList.DataSource = unitSaleBLObj.dtMoneyRecieptInfoforDDL(unitRegObj, "SL");
            moneyReceipDropDownList.DataTextField = "RECEIPT_NO";
            moneyReceipDropDownList.DataValueField = "ID";
            moneyReceipDropDownList.DataBind();
            DataTable dtRegInfo = opendMFDAO.getDtRegInfo(unitRegObj);
            if (dtRegInfo.Rows.Count > 0)
            {
                holderNameTextBox.Text = dtRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["HNAME"].ToString();
                jHolderTextBox.Text = dtRegInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["JNT_NAME"].ToString();
                holderAddress1TextBox.Text = dtRegInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["ADDRS1"].ToString();
                holderAddress2TextBox.Text = dtRegInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["ADDRS2"].ToString();
                holderTelphoneTextBox.Text = dtRegInfo.Rows[0]["TEL_NO"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["TEL_NO"].ToString();
                string CIP = dtRegInfo.Rows[0]["CIP"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["CIP"].ToString();
                if (string.Compare(CIP, "Y", true) == 0)
                {
                    tdCIP.InnerHtml = " YES ";
                }
                else if (string.Compare(CIP, "N", true) == 0)
                {
                    tdCIP.InnerHtml = " NO ";
                }

                displaySign();
              

                saleNumberTextBox.Text = unitSaleBLObj.getNextSaleNo(unitRegObj, userObj).ToString();
                
                saleObj.SaleNo = unitSaleBLObj.getNextSaleNo(unitRegObj, userObj) - 1;
             
            }
            else
            {
                SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());              
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", " window.fnResetAll();", true);
                tdCIP.InnerHtml = "";
               
            }
        }
        else
        {
            SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
            tdCIP.InnerHtml = "";
           
            ClearText();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Registration Number');", true);
            
        }
    }
    public void displaySign()
    {
        string regNo = "";
        string fundCode = "";
        string branchCode = "";
        string unitHolderName = "";
        string branchName = "";
        string fundName = "";

        regNo = regNoTextBox.Text.ToString();
        fundName = opendMFDAO.GetFundName(fundCodeTextBox.Text.ToString());
        fundCode = fundCodeTextBox.Text.ToString();
        branchName = opendMFDAO.GetBranchName(branchCodeTextBox.Text.ToString());
        branchCode = branchCodeTextBox.Text.ToString();


        unitHolderName = opendMFDAO.GetHolderName(fundCode, branchCode, regNo);


        string[] BranchCodeSign = branchCode.Split('/');
        string imageSignLocation = Path.Combine(ConfigReader.SingLocation + "\\" + fundCode, fundCode + "_" + BranchCodeSign[0] + "_" + BranchCodeSign[1] + "_" + regNo + ".jpg");//"../../Image/IAMCL/Sign/"+ fundCode + "_" + branchCode + "_" + regNo + ".jpg";
        string imagePhotoLocation = Path.Combine(ConfigReader.PhotoLocation + "\\" + fundCode, fundCode + "_" + BranchCodeSign[0] + BranchCodeSign[1] + "_" + regNo + ".jpg");

        if (File.Exists(Path.Combine(ConfigReader.SingLocation + "\\" + fundCode, fundCode + "_" + BranchCodeSign[0] + "_" + BranchCodeSign[1] + "_" + regNo + ".jpg")))
        {
            SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc( imageSignLocation.ToString());
        }
        else
        {
            SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
        }
       
    }

    protected void regNoTextBox_TextChanged(object sender, EventArgs e)
    {
        displayRegInfo();
    }
    protected void bankNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        branchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(bankNameDropDownList.SelectedValue.ToString()));
        branchNameDropDownList.DataTextField = "BRANCH_NAME";
        branchNameDropDownList.DataValueField = "BRANCH_CODE";
        branchNameDropDownList.DataBind();
    }
   
    protected void moneyReceipDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dtMoneyReceitInfoDetails = unitSaleBLObj.dtMoneyRecieptInfoDetails(Convert.ToInt64(moneyReceipDropDownList.SelectedValue.ToString()));
        if (dtMoneyReceitInfoDetails.Rows.Count > 0)
        {

            saleDateTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["RECEIPT_DATE"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtMoneyReceitInfoDetails.Rows[0]["RECEIPT_DATE"].ToString()).ToString("dd-MMM-yyyy");
            saleRateTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["RATE"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["RATE"].ToString();
            unitQtyTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["UNIT_QTY"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["UNIT_QTY"].ToString();
            CashAmountTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["CASH_AMT"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["CASH_AMT"].ToString();
            MultiplePayTypeTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["MULTI_PAY_REMARKS"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["MULTI_PAY_REMARKS"].ToString();
            sellingAgentCodeTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["SELLING_AGENT_ID"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["SELLING_AGENT_ID"].ToString();
            if (!dtMoneyReceitInfoDetails.Rows[0]["CHQ_TYPE"].Equals(DBNull.Value))
            {


                string chqType = dtMoneyReceitInfoDetails.Rows[0]["CHQ_TYPE"].ToString();
                if (chqType.ToString().ToUpper() == "CHQ" || chqType.ToString().ToUpper() == "DD" || chqType.ToString().ToUpper() == "PO")
                {
                    DataTable dtBankInfo = regBLObj.dtGetBankBracnhInfo(dtMoneyReceitInfoDetails.Rows[0]["ROUTING_NO"].ToString());
                    ChequeTypeDropDownList.SelectedValue = dtMoneyReceitInfoDetails.Rows[0]["CHQ_TYPE"].ToString();
                    bankNameDropDownList.SelectedValue = dtBankInfo.Rows[0]["BANK_CODE"].Equals(DBNull.Value) ? "0" : dtBankInfo.Rows[0]["BANK_CODE"].ToString();
                    branchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(dtBankInfo.Rows[0]["BANK_CODE"].Equals(DBNull.Value) ? "0" : dtBankInfo.Rows[0]["BANK_CODE"].ToString()));
                    branchNameDropDownList.DataTextField = "BRANCH_NAME";
                    branchNameDropDownList.DataValueField = "BRANCH_CODE";
                    branchNameDropDownList.DataBind();
                    branchNameDropDownList.SelectedValue = dtBankInfo.Rows[0]["BRANCH_CODE"].Equals(DBNull.Value) ? "0" : dtBankInfo.Rows[0]["BRANCH_CODE"].ToString();
                    CHQDDNoRemarksTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["CHQ_DD_NO"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["CHQ_DD_NO"].ToString();
                    chequeDateTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["CHQ_DD_DATE"].Equals(DBNull.Value) ? "" : dtMoneyReceitInfoDetails.Rows[0]["CHQ_DD_DATE"].ToString();

                }
            }
            if (!dtMoneyReceitInfoDetails.Rows[0]["PAY_TYPE"].Equals(DBNull.Value))
            {
                string chqType = dtMoneyReceitInfoDetails.Rows[0]["CHQ_TYPE"].ToString();
                if (chqType.ToString().ToUpper() == "CHQ")
                {
                    ChqRadioButton.Checked = true;
                }
                else if (chqType.ToString().ToUpper() == "CASH")
                {
                    CashRadioButton.Checked = true;
                }
                else if (chqType.ToString().ToUpper() == "BOTH")
                {
                    BothRadioButton.Checked = true;
                }
                else if (chqType.ToString().ToUpper() == "MULT")
                {
                    MultiRadioButton.Checked = true;
                }

            }


        }
        else
        {
            clearSale();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Registration Number');", true);
        }
    }
    public void clearSale()
    {
        saleTypeDropDownList.SelectedValue = "SL";
        saleNumberTextBox.Text = "";
        saleRateTextBox.Text = "";
        unitQtyTextBox.Text = "";
        saleRemarksTextBox.Text = "";       
        moneyReceipDropDownList.SelectedValue = "0";
        ChqRadioButton.Checked = true;
        ChequeTypeDropDownList.SelectedValue = "CGQ";
        CashRadioButton.Checked = false;
        BothRadioButton.Checked = false;
        MultiRadioButton.Checked = false;
        CHQDDNoRemarksTextBox.Text = "";
        CashAmountTextBox.Text = "";
        MultiplePayTypeTextBox.Text = "";
        chequeDateTextBox.Text = "";
        bankNameDropDownList.SelectedValue = "0";
        branchNameDropDownList.SelectedValue = "0";
    }
}
