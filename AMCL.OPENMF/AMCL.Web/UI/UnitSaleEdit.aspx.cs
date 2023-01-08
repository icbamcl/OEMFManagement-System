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

public partial class UI_UnitSaleEdit : System.Web.UI.Page
{
   // System.Web.UI.Page this_page_ref = null;
    OMFDAO opendMFDAO = new OMFDAO();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    string errorMassege = "";
    BaseClass bcContent = new BaseClass();
    EncryptDecrypt encrypt = new EncryptDecrypt();
   
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


        saleNumberTextBox.Focus();
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
        if (MoneyReceiptNoTextBox.Text.Trim() != "")
        {
            saleObj.MoneyReceiptNo = Convert.ToInt32(MoneyReceiptNoTextBox.Text.Trim());
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
            //if (unitSaleBLObj.IsDuplicateSale(regObj, saleObj))// Checking Duplicate Sale No
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " "+"Sale Number "+"');", true);
            //}
            //else 
            if (saleLimitLower > Convert.ToInt32(unitQtyTextBox.Text.Trim().ToString()) && saleTypeDropDownList.SelectedValue.ToString() == "SL")
            {
                unitQtyTextBox.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Sale Unit Quantity can not less than "+saleLimitLower.ToString()+"');", true);

            }
            else if (saleLimmitUpper< Convert.ToInt32(unitQtyTextBox.Text.Trim().ToString()))
            {
                unitQtyTextBox.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Sale Unit Quantity can not greater than " + saleLimmitUpper.ToString() + "');", true);

            }

            else if (!opendMFDAO.IsValidRegistration(regObj))
            {
                regNoTextBox.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Invalid Registration Number!!');", true);

            }
            //else if (regObj.FundCode == "IAMPH")
            //{
            //    string errorMessage = "Please Contace with Administrator";
            //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed :" + errorMessage + "');", true);

            //}
            else
            {
                int loop = 0;
                DataTable dtSaleInfo = (DataTable)Session["dtSaleInfo"];
                int rowNumber = 1;
                int line = 0;
                int countCheck = 0;
                DataTable dtDino = unitSaleBLObj.getTableDinoForEdit();
                DataRow drDino;
                bool invalidCert = false;
                bool allocateCert = true;
                bool bannedCert = false;
                int duplicateCerNoReg=0;
                string dino = "";
                string cerNo = "";
                foreach (DataGridItem gridItem in dinoGridView.Items)
                {
                    drDino = dtDino.NewRow();
                    TextBox txtDino = null;
                    TextBox txtCert = null;
                    TextBox txtWeight = null;
                    TextBox txtStatus = null;
                    txtDino = (TextBox)(gridItem.FindControl("dinoTextBox"));
                    txtCert = (TextBox)(gridItem.FindControl("certNoTextBox"));
                    txtWeight = (TextBox)(gridItem.FindControl("weightTextBox"));
                    txtStatus = (TextBox)(gridItem.FindControl("statusTextBox"));
                    CheckBox leftCheckBox = (CheckBox)gridItem.FindControl("leftCheckBox");
                    if (leftCheckBox.Checked)
                    {
                        countCheck++;
                        duplicateCerNoReg = opendMFDAO.duplicateCerNoReg(regObj, txtDino.Text.ToString(), txtCert.Text.ToString());                        
                        allocateCert = unitSaleBLObj.IsCertificateAllocate(regObj, txtDino.Text.ToString(), txtCert.Text.ToString());
                        bannedCert = unitSaleBLObj.IsCertificateBanned(regObj, txtDino.Text.ToString(), txtCert.Text.ToString());
                        if (duplicateCerNoReg ==Convert.ToInt32( regNoTextBox.Text.Trim().ToString()) || duplicateCerNoReg ==0)
                        {

                            if (opendMFDAO.validationDino(txtDino.Text.ToString().ToUpper(), regObj.FundCode.ToString().ToUpper()) && opendMFDAO.validationWeight(Convert.ToInt32(txtWeight.Text.ToString()), regObj.FundCode.ToString().ToUpper()))
                            {
                                if (allocateCert)
                                {
                                    if (bannedCert)
                                    {
                                        bannedCert = true;
                                        line = rowNumber;
                                        dino = txtDino.Text.Trim().ToString();
                                        cerNo = txtCert.Text.Trim().ToString();
                                        break;
                                    }
                                    else
                                    {

                                        drDino["dino"] = txtDino.Text.Trim().ToString().ToUpper();
                                        drDino["cert_no"] = Convert.ToInt32(txtCert.Text.Trim().ToString());
                                        drDino["cert_weight"] = Convert.ToInt32(txtWeight.Text.Trim().ToString());
                                        drDino["status"] = txtStatus.Text.Trim().ToString().ToUpper();
                                        drDino["ROWID"] = dtSaleInfo.Rows[loop]["ROWID"].ToString();
                                        dtDino.Rows.Add(drDino);
                                        rowNumber++;
                                    }
                                }
                                else
                                {
                                    allocateCert = false;
                                    line = rowNumber;
                                    dino = txtDino.Text.Trim().ToString();
                                    cerNo = txtCert.Text.Trim().ToString();
                                    break;
                                }


                            }
                            else
                            {
                                invalidCert = true;
                                line = rowNumber;
                                break;
                            }
                        }
                        else
                        {
                            dino = txtDino.Text.Trim().ToString();
                            cerNo = txtCert.Text.Trim().ToString();
                            break;
                        }
                    }
                    loop++;
                }

                if (duplicateCerNoReg > 0 && !(duplicateCerNoReg ==Convert.ToInt32( regNoTextBox.Text.Trim().ToString())))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Certificate " + dino + "-" + cerNo + " is Already Sold to Reg No:" + duplicateCerNoReg + "');", true);
                }
                else
                {
                    if (invalidCert)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed!!Invalid Dinomination or Weight at Line: " + line + "');", true);

                    }
                    else if(!allocateCert)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed!! Line: " + line + " and Certificate No: " + dino + "-" + cerNo + " is not allocated in this branch');", true);
                    }
                    else if (bannedCert)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed!! Line: " + line + " and Certificate No: " + dino + "-" + cerNo + " is banned for sale');", true);
                    }
                    else if (!invalidCert && allocateCert && !bannedCert)
                    {
                        unitSaleBLObj.SaveSaleEditInfo(regObj, saleObj, dtDino, userObj);
                        ClearText();                        
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Success().ToString() + "');", true);
                    }
                }
               
            }

        }
        catch (Exception ex)
        {
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
        dinoGridView.DataSource = opendMFDAO.getTableDinomination();
        dinoGridView.DataBind();
        dinoGridView.Visible = false;        
        MoneyReceiptNoTextBox.Text = "";
        
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
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }
    protected void findButton_Click(object sender, EventArgs e)
    {
       
      displayRegInfo();

        
    
    }
   
    public void displayRegInfo()
    {
        UnitSale saleObj = new UnitSale();
        UnitHolderRegistration unitRegObj = new UnitHolderRegistration();
        unitRegObj.FundCode = fundCodeTextBox.Text.Trim();
        unitRegObj.BranchCode = branchCodeTextBox.Text.Trim();
        
        saleObj.SaleNo = Convert.ToInt32(saleNumberTextBox.Text.Trim().ToString());
         DataTable dtDino = unitSaleBLObj.getTableDinoForEdit();
        

        if (unitSaleBLObj.IsValidSaleNumber(unitRegObj, saleObj))
        {
            DataTable dtSaleInfo = unitSaleBLObj.dtGetSaleInfo(unitRegObj, saleObj);
            if (dtSaleInfo.Rows.Count > 0)
            {
                dinoGridView.Visible = true;
                regNoTextBox.Text = dtSaleInfo.Rows[0]["REG_NO"].ToString();
                holderNameTextBox.Text = dtSaleInfo.Rows[0]["HNAME"].ToString();
                jHolderTextBox.Text = dtSaleInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["JNT_NAME"].ToString();
                holderAddress1TextBox.Text = dtSaleInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["ADDRS1"].ToString();
                holderAddress2TextBox.Text = dtSaleInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["ADDRS2"].ToString();
                holderTelphoneTextBox.Text = dtSaleInfo.Rows[0]["TEL_NO"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["TEL_NO"].ToString();
                string CIP = dtSaleInfo.Rows[0]["CIP"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["CIP"].ToString();
                if (string.Compare(CIP, "Y", true) == 0)
                {
                    tdCIP.InnerHtml = " YES ";
                }
                else if (string.Compare(CIP, "N", true) == 0)
                {
                    tdCIP.InnerHtml = " NO ";
                }
                displaySign();
                MoneyReceiptNoTextBox.Text = dtSaleInfo.Rows[0]["MONY_RECT_NO"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["MONY_RECT_NO"].ToString();
                saleDateTextBox.Text = dtSaleInfo.Rows[0]["SL_DT"].Equals(DBNull.Value) ? "" :Convert.ToDateTime(dtSaleInfo.Rows[0]["SL_DT"].ToString()).ToString("dd-MMM-yyyy");
                saleRateTextBox.Text = dtSaleInfo.Rows[0]["SL_PRICE"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["SL_PRICE"].ToString();
                saleTypeDropDownList.SelectedValue = dtSaleInfo.Rows[0]["SL_TYPE"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["SL_TYPE"].ToString();
                unitQtyTextBox.Text = dtSaleInfo.Rows[0]["QTY"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["QTY"].ToString();

                if (dtSaleInfo.Rows[0]["PAY_TYPE"].Equals(DBNull.Value))
                {

                }
                else
                {
                    string payType = dtSaleInfo.Rows[0]["PAY_TYPE"].ToString();
                    if (payType.ToString().ToUpper() == "CHQ" || payType.ToString().ToUpper() == "DD" || payType.ToString().ToUpper() == "PO")
                    {
                        ChqRadioButton.Checked = true;
                        ChequeTypeDropDownList.SelectedValue = dtSaleInfo.Rows[0]["PAY_TYPE"].ToString();
                        bankNameDropDownList.Enabled = true;
                        branchNameDropDownList.Enabled = true;
                        CashAmountTextBox.Enabled = false;
                        MultiplePayTypeTextBox.Enabled = false;                        
                        CHQDDNoRemarksTextBox.Enabled = true;

                        bankNameDropDownList.SelectedValue =dtSaleInfo.Rows[0]["BANK_CODE"].Equals(DBNull.Value)? "0":dtSaleInfo.Rows[0]["BANK_CODE"].ToString();
                        branchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(dtSaleInfo.Rows[0]["BANK_CODE"].Equals(DBNull.Value) ? "0" : dtSaleInfo.Rows[0]["BANK_CODE"].ToString()));
                        branchNameDropDownList.DataTextField = "BRANCH_NAME";
                        branchNameDropDownList.DataValueField = "BRANCH_CODE";
                        branchNameDropDownList.DataBind();
                        branchNameDropDownList.SelectedValue = dtSaleInfo.Rows[0]["BRANCH_CODE"].Equals(DBNull.Value) ? "0" : dtSaleInfo.Rows[0]["BRANCH_CODE"].ToString();
                        CHQDDNoRemarksTextBox.Text = dtSaleInfo.Rows[0]["CHQ_DD_NO"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["CHQ_DD_NO"].ToString();
                        chequeDateTextBox.Text = dtSaleInfo.Rows[0]["CHEQUE_DT"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["CHEQUE_DT"].ToString();
                        

                    }

                }

                dinoGridView.DataSource = dtSaleInfo;
                dinoGridView.DataBind();
                Session["dtSaleInfo"] = dtSaleInfo;

            }
            else
            {

                SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
                tdCIP.InnerHtml = "";
                dinoGridView.Visible = false;
                ClearText();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Sale Information is not  Available');", true);
            }

        }
        else
        {
            SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
            tdCIP.InnerHtml = "";
            dinoGridView.Visible = false;
            ClearText();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Sale Number');", true);
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
   
    protected void bankNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        branchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(bankNameDropDownList.SelectedValue.ToString()));
        branchNameDropDownList.DataTextField = "BRANCH_NAME";
        branchNameDropDownList.DataValueField = "BRANCH_CODE";
        branchNameDropDownList.DataBind();
    }
    protected void ChqRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        bankNameDropDownList.Enabled = true;
        branchNameDropDownList.Enabled = true;
        CashAmountTextBox.Enabled = false;
        MultiplePayTypeTextBox.Enabled = false;
        ChequeTypeDropDownList.SelectedValue = "CHQ";
        CHQDDNoRemarksTextBox.Enabled = true;
        CHQDDNoRemarksTextBox.Focus();

    }
    protected void CashRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        bankNameDropDownList.SelectedValue = "0";
        bankNameDropDownList.Enabled = false;
        branchNameDropDownList.SelectedValue = "0";
        branchNameDropDownList.Enabled = false;
        CHQDDNoRemarksTextBox.Enabled = false;
        MultiplePayTypeTextBox.Enabled = false;
        CashAmountTextBox.Enabled = true;
        CashAmountTextBox.Focus();
    }
    protected void BothRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        MultiplePayTypeTextBox.Enabled = false;
        bankNameDropDownList.Enabled = true;
        branchNameDropDownList.Enabled = true;
        CHQDDNoRemarksTextBox.Enabled = true;
        CashAmountTextBox.Enabled = true;
        CHQDDNoRemarksTextBox.Focus();
    }
    protected void MultiRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        bankNameDropDownList.SelectedValue = "0";
        bankNameDropDownList.Enabled = false;
        branchNameDropDownList.SelectedValue = "0";
        branchNameDropDownList.Enabled = false;
        CHQDDNoRemarksTextBox.Enabled = false;        
        CashAmountTextBox.Enabled = false;
        MultiplePayTypeTextBox.Enabled = true;
        MultiplePayTypeTextBox.Focus();
    }
    protected void saleNumberTextBox_TextChanged(object sender, EventArgs e)
    {
        displayRegInfo();
    }
    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeTextBox.Text.ToString();
        regObj.BranchCode = branchCodeTextBox.Text.ToString();
        regObj.RegNumber =regNoTextBox.Text.Trim().ToString();
        UnitSale saleObj = new UnitSale();
        saleObj.SaleNo = Convert.ToInt32(saleNumberTextBox.Text.Trim().ToString());
        try
        {
            if (unitSaleBLObj.IsValidSaleNumber(regObj, saleObj))
            {
                DataTable dtSaleInfo = unitSaleBLObj.dtGetSaleInfo(regObj, saleObj);
                if (dtSaleInfo.Rows.Count > 0)
                {

                    saleObj.MoneyReceiptNo = Convert.ToInt32(dtSaleInfo.Rows[0]["MONY_RECT_NO"].Equals(DBNull.Value) ? "0" : dtSaleInfo.Rows[0]["MONY_RECT_NO"].ToString());
                    saleObj.SaleDate = dtSaleInfo.Rows[0]["SL_DT"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["SL_DT"].ToString();
                    saleObj.SaleRate =  decimal.Parse(dtSaleInfo.Rows[0]["SL_PRICE"].Equals(DBNull.Value) ? "0" : dtSaleInfo.Rows[0]["SL_PRICE"].ToString());
                    saleObj.SaleType = dtSaleInfo.Rows[0]["SL_TYPE"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["SL_TYPE"].ToString();
                    saleObj.SaleUnitQty = Convert.ToInt32(dtSaleInfo.Rows[0]["QTY"].Equals(DBNull.Value) ? "0" : dtSaleInfo.Rows[0]["QTY"].ToString());

                    if (dtSaleInfo.Rows[0]["PAY_TYPE"].Equals(DBNull.Value))
                    {

                    }

                   
                    else
                    {
                        string payType = dtSaleInfo.Rows[0]["PAY_TYPE"].ToString();
                        if (payType.ToString().ToUpper() == "CHQ" || payType.ToString().ToUpper() == "DD" || payType.ToString().ToUpper() == "PO")
                        {
                            saleObj.PaymentType = payType;
                            saleObj.ChequeNo = dtSaleInfo.Rows[0]["CHQ_DD_NO"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["CHQ_DD_NO"].ToString();
                            saleObj.ChequeDate = dtSaleInfo.Rows[0]["CHEQUE_DT"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["CHEQUE_DT"].ToString();
                            saleObj.BankCode = Convert.ToInt16(dtSaleInfo.Rows[0]["BANK_CODE"].Equals(DBNull.Value) ? "0" : dtSaleInfo.Rows[0]["BANK_CODE"].ToString());
                            saleObj.BranchCode = Convert.ToInt16(dtSaleInfo.Rows[0]["BRANCH_CODE"].Equals(DBNull.Value) ? "0" : dtSaleInfo.Rows[0]["BRANCH_CODE"].ToString());
                            saleObj.CashAmount = Convert.ToDecimal(dtSaleInfo.Rows[0]["CASH_AMT"].Equals(DBNull.Value) ? "0" : dtSaleInfo.Rows[0]["CASH_AMT"].ToString());
                            saleObj.SaleRemarks = dtSaleInfo.Rows[0]["MULTI_PAY_REMARKS"].Equals(DBNull.Value) ? "" : dtSaleInfo.Rows[0]["MULTI_PAY_REMARKS"].ToString();
                        }

                    }
                    DataTable dtDinomination = unitSaleBLObj.dtGetExistingSaleCertBySaleNo(regObj, saleObj);
                    if (dtDinomination.Rows.Count > 0)
                    {
                        unitSaleBLObj.DeleteSaleEditInfo(regObj, saleObj, dtDinomination, userObj);
                        SignImage.ImageUrl = Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString();
                        tdCIP.InnerHtml = "";
                        dinoGridView.Visible = false;
                        ClearText();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Sale Informaion Deleted');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Delete is not possible due to certificate are surrendered or Transferred');", true);
                    }
                }
                else
                {

                    SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc( Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
                    tdCIP.InnerHtml = "";
                    dinoGridView.Visible = false;
                    ClearText();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Sale Information is not  Available');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Sale Number');", true);
            }

        }
        catch(Exception ex)
        {
            errorMassege = msgObj.ExceptionErrorMessageString(ex.Message.ToString());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + errorMassege.ToString() + "');", true);
        }
    }

    
}
