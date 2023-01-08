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

public partial class UI_UnitSaleEntry : System.Web.UI.Page
{
   // System.Web.UI.Page this_page_ref = null;
    OMFDAO opendMFDAO = new OMFDAO();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    UnitHolderRegBL regBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
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
            if (unitSaleBLObj.IsSaleLock(regObj))//Cheking Lock in status
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Error().ToString() + " " + " Sale Operation is Locked " + "');", true);
            }
            else if (unitSaleBLObj.IsDuplicateSale(regObj, saleObj))// Checking Duplicate Sale No
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " " + "Sale Number " + "');", true);
            }
            else if (unitHolderRegBLObj.IsEditPermit(regObj.FundCode.ToString()))
            {
                string errorMessage = " Please Contact with System Administrator ";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed :" + errorMessage + "');", true);

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
                int rowNumber = 1;
                int line = 0;
                DataTable dtDino = opendMFDAO.getTableDinomination();
                DataRow drDino;
                bool invalidCert = false;
                bool allocateCert = true;
                bool bannedCert = false;
                int duplicateCerNoReg = 0;
                string dino = "";
                string cerNo = "";
                foreach (DataGridItem gridItem in dinoGridView.Items)
                {
                    drDino = dtDino.NewRow();
                    TextBox txtDino = null;
                    TextBox txtCert = null;
                    TextBox txtWeight = null;
                    txtDino = (TextBox)(gridItem.FindControl("dinoTextBox"));
                    txtCert = (TextBox)(gridItem.FindControl("certNoTextBox"));
                    txtWeight = (TextBox)(gridItem.FindControl("weightTextBox"));
                    duplicateCerNoReg = opendMFDAO.duplicateCerNoReg(regObj, txtDino.Text.ToString(), txtCert.Text.ToString());
                    allocateCert = unitSaleBLObj.IsCertificateAllocate(regObj, txtDino.Text.ToString(), txtCert.Text.ToString());
                    bannedCert = unitSaleBLObj.IsCertificateBanned(regObj, txtDino.Text.ToString(), txtCert.Text.ToString());
                    if (duplicateCerNoReg == 0)
                    {
                        if (saleTypeDropDownList.SelectedValue.ToString().ToUpper() == "CIP")
                        {
                            if (opendMFDAO.validationDino(txtDino.Text.ToString().ToUpper(), regObj.FundCode.ToString().ToUpper()))
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
                        
                        else if(saleTypeDropDownList.SelectedValue.ToString().ToUpper() == "SL")
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
                    }
                    else
                    {
                        dino = txtDino.Text.Trim().ToString();
                        cerNo = txtCert.Text.Trim().ToString();
                        break;
                    }

                }

                if (duplicateCerNoReg > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Certificate " + dino + "-" + cerNo + " is Already Sold to Reg No:" + duplicateCerNoReg + "');", true);
                }
                else
                {
                    if (invalidCert)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed!!Invalid Dinomination or Weight at Line: " + line + "');", true);

                    }
                    else if (!allocateCert)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed!! Line: " + line + " and Certificate No: " + dino + "-" + cerNo + " is not allocated in this branch');", true);
                    }
                    else if (bannedCert)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed!! Line: " + line + " and Certificate No: " + dino + "-" + cerNo + " is banned for sale');", true);
                    }
                    else if (!invalidCert && allocateCert && !bannedCert )
                    {
                        unitSaleBLObj.SaveUnitSale(regObj, saleObj, dtDino, userObj);
                        ClearText();
                        //saleNumberTextBox.Text = unitSaleBLObj.getNextSaleNo(regObj, userObj).ToString();
                        //saleObj.SaleNo = unitSaleBLObj.getNextSaleNo(regObj, userObj) - 1;
                        //saleDateTextBox.Text = opendMFDAO.getLastSaleDate(regObj, saleObj).ToString("dd-MMM-yyyy");
                        //saleRateTextBox.Text = opendMFDAO.getLastSaleRate(regObj, saleObj).ToString();
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
        aTextBox.Text = "";
        bTextBox.Text = "";
        cTextBox.Text = "";
        dTextBox.Text = "";
        eTextBox.Text = "";
        fTextBox.Text = "";
        gTextBox.Text = "";
        hTextBox.Text = "";
        iTextBox.Text = "";
        jTextBox.Text = "";
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

        UnitSale saleObj = new UnitSale();
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeTextBox.Text;
        regObj.BranchCode = branchCodeTextBox.Text;
        saleNumberTextBox.Text = unitSaleBLObj.getNextSaleNo(regObj, userObj).ToString();
       // MoneyReceiptNoTextBox.Text = unitSaleBLObj.getNextMoneReceiptNo(regObj, userObj).ToString();
        saleObj.SaleNo = opendMFDAO.GetMaxSaleNo(regObj)-1;
        saleDateTextBox.Text = opendMFDAO.getLastSaleDate(regObj, saleObj).ToString("dd-MMM-yyyy");
        saleRateTextBox.Text = opendMFDAO.getLastSaleRate(regObj, saleObj).ToString();
    
    }
    protected void addListButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeTextBox.Text;
        regObj.BranchCode = branchCodeTextBox.Text;
        regObj.RegIsCIP = saleTypeDropDownList.SelectedValue.ToString().ToUpper();
        dinoGridView.Visible = true;
        int unitQty = 0;
        int certNo = 0;
        DataTable dtDinomination = opendMFDAO.getTableDinomination();
        DataRow drDinomination = dtDinomination.NewRow();
        int certQty=0;
        int saleLimitLower = unitSaleBLObj.SaleLimitLower(regObj);
        long saleLimmitUpper = unitSaleBLObj.SaleLimitUpper(regObj); 

        if(saleLimitLower>Convert.ToInt32(unitQtyTextBox.Text.Trim().ToString())&& saleTypeDropDownList.SelectedValue.ToString()=="SL")
        {
            dtDinomination = new DataTable(); ;
            dinoGridView.DataSource = dtDinomination;
            dinoGridView.DataBind();
            unitQtyTextBox.Focus();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Sale Unit Quantity can not less than " + saleLimitLower.ToString() + "');", true);

        }
        else if (saleLimmitUpper < Convert.ToInt32(unitQtyTextBox.Text.Trim().ToString()) && saleTypeDropDownList.SelectedValue.ToString() == "SL")
        {
            dtDinomination = new DataTable(); ;
            dinoGridView.DataSource = dtDinomination;
            dinoGridView.DataBind();
            unitQtyTextBox.Focus();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Sale Unit Quantity can not greater than " + saleLimmitUpper.ToString() + " Pleas Contact  AMCL Head Office to increase sale unit limit');", true);

        }
        else
        {

            if (string.Compare(regObj.FundCode.ToString(), "CFUF") == 0 || string.Compare(regObj.FundCode.ToString(), "IUF") == 0)
            {
                if (lTextBox.Text != "")
                {


                    certNo = opendMFDAO.GetMaxCertNo("L", regObj, userObj);
                    certQty = Convert.ToInt32(lTextBox.Text.Trim());
                    for (int i = 0; i < certQty; i++)
                    {
                        drDinomination = dtDinomination.NewRow();
                        drDinomination["dino"] = "L";
                        drDinomination["cert_no"] = certNo;
                        drDinomination["cert_weight"] = "20000";
                        dtDinomination.Rows.Add(drDinomination);
                        certNo++;
                    }
                    unitQty = unitQty + (certQty * 20000);


                }
            }
            if (string.Compare(regObj.FundCode.ToString(), "BDF") == 0 || string.Compare(regObj.FundCode.ToString(), "CFUF") == 0 || string.Compare(regObj.FundCode.ToString(), "IUF") == 0)
            {


                if (kTextBox.Text != "")
                {


                    certNo = opendMFDAO.GetMaxCertNo("K", regObj, userObj);
                    certQty = Convert.ToInt32(kTextBox.Text.Trim());
                    for (int i = 0; i < certQty; i++)
                    {
                        drDinomination = dtDinomination.NewRow();
                        drDinomination["dino"] = "K";
                        drDinomination["cert_no"] = certNo;
                        drDinomination["cert_weight"] = "10000";
                        dtDinomination.Rows.Add(drDinomination);
                        certNo++;
                    }
                    unitQty = unitQty + (certQty * 10000);


                }
            }
            if (jTextBox.Text != "")
            {

                certNo = opendMFDAO.GetMaxCertNo("J", regObj, userObj);
                certQty = Convert.ToInt32(jTextBox.Text.Trim());
                for (int i = 0; i < certQty; i++)
                {
                    drDinomination = dtDinomination.NewRow();
                    drDinomination["dino"] = "J";
                    drDinomination["cert_no"] = certNo;
                    drDinomination["cert_weight"] = "5000";
                    dtDinomination.Rows.Add(drDinomination);
                    certNo++;
                }
                unitQty = unitQty + (certQty * 5000);
            }
            if (iTextBox.Text != "")
            {

                certNo = opendMFDAO.GetMaxCertNo("I", regObj, userObj);
                certQty = Convert.ToInt32(iTextBox.Text.Trim());
                for (int i = 0; i < certQty; i++)
                {
                    drDinomination = dtDinomination.NewRow();
                    drDinomination["dino"] = "I";
                    drDinomination["cert_no"] = certNo;
                    drDinomination["cert_weight"] = "1000";
                    dtDinomination.Rows.Add(drDinomination);
                    certNo++;
                }
                unitQty = unitQty + (certQty * 1000);
            }
            if (hTextBox.Text != "")
            {

                certNo = opendMFDAO.GetMaxCertNo("H", regObj, userObj);
                certQty = Convert.ToInt32(hTextBox.Text.Trim());
                for (int i = 0; i < certQty; i++)
                {
                    drDinomination = dtDinomination.NewRow();
                    drDinomination["dino"] = "H";
                    drDinomination["cert_no"] = certNo;
                    drDinomination["cert_weight"] = "500";
                    dtDinomination.Rows.Add(drDinomination);
                    certNo++;

                }
                unitQty = unitQty + (certQty * 500);
            }
            if (gTextBox.Text != "")
            {

                certNo = opendMFDAO.GetMaxCertNo("G", regObj, userObj);
                certQty = Convert.ToInt32(gTextBox.Text.Trim());
                for (int i = 0; i < certQty; i++)
                {
                    drDinomination = dtDinomination.NewRow();
                    drDinomination["dino"] = "G";
                    drDinomination["cert_no"] = certNo;
                    drDinomination["cert_weight"] = "250";
                    dtDinomination.Rows.Add(drDinomination);
                    certNo++;

                }
                unitQty = unitQty + (certQty * 250);
            }
            if (fTextBox.Text != "")
            {

                certNo = opendMFDAO.GetMaxCertNo("F", regObj, userObj);
                certQty = Convert.ToInt32(fTextBox.Text.Trim());
                for (int i = 0; i < certQty; i++)
                {
                    drDinomination = dtDinomination.NewRow();
                    drDinomination["dino"] = "F";
                    drDinomination["cert_no"] = certNo;
                    drDinomination["cert_weight"] = "100";
                    dtDinomination.Rows.Add(drDinomination);
                    certNo++;

                }
                unitQty = unitQty + (certQty * 100);
            }
            if (eTextBox.Text != "")
            {

                certNo = opendMFDAO.GetMaxCertNo("E", regObj, userObj);
                certQty = Convert.ToInt32(eTextBox.Text.Trim());
                for (int i = 0; i < certQty; i++)
                {
                    drDinomination = dtDinomination.NewRow();
                    drDinomination["dino"] = "E";
                    drDinomination["cert_no"] = certNo;
                    drDinomination["cert_weight"] = "50";
                    dtDinomination.Rows.Add(drDinomination);
                    certNo++;

                }
                unitQty = unitQty + (certQty * 50);
            }
            if (dTextBox.Text != "")
            {

                certNo = opendMFDAO.GetMaxCertNo("D", regObj, userObj);
                certQty = Convert.ToInt32(dTextBox.Text.Trim());
                for (int i = 0; i < certQty; i++)
                {
                    drDinomination = dtDinomination.NewRow();
                    drDinomination["dino"] = "D";
                    drDinomination["cert_no"] = certNo;
                    drDinomination["cert_weight"] = "20";
                    dtDinomination.Rows.Add(drDinomination);
                    certNo++;
                }
                unitQty = unitQty + (certQty * 20);
            }
            if (cTextBox.Text != "")
            {

                certNo = opendMFDAO.GetMaxCertNo("C", regObj, userObj);
                certQty = Convert.ToInt32(cTextBox.Text.Trim());
                for (int i = 0; i < certQty; i++)
                {
                    drDinomination = dtDinomination.NewRow();
                    drDinomination["dino"] = "C";
                    drDinomination["cert_no"] = certNo;
                    drDinomination["cert_weight"] = "10";
                    dtDinomination.Rows.Add(drDinomination);
                    certNo++;
                }
                unitQty = unitQty + (certQty * 10);
            }
            if (bTextBox.Text != "")
            {

                certNo = opendMFDAO.GetMaxCertNo("B", regObj, userObj);
                certQty = Convert.ToInt32(bTextBox.Text.Trim());
                for (int i = 0; i < certQty; i++)
                {
                    drDinomination = dtDinomination.NewRow();
                    drDinomination["dino"] = "B";
                    drDinomination["cert_no"] = certNo;
                    drDinomination["cert_weight"] = "5";
                    dtDinomination.Rows.Add(drDinomination);
                    certNo++;
                }
                unitQty = unitQty + (certQty * 5);
            }
            if (aTextBox.Text != "")
            {
                certNo = opendMFDAO.GetMaxCertNo("A", regObj, userObj);
                certQty = Convert.ToInt32(aTextBox.Text.Trim());
                for (int i = 0; i < certQty; i++)
                {
                    drDinomination = dtDinomination.NewRow();
                    drDinomination["dino"] = "A";
                    drDinomination["cert_no"] = certNo;
                    drDinomination["cert_weight"] = "1";
                    dtDinomination.Rows.Add(drDinomination);
                    certNo++;
                }
                unitQty = unitQty + (certQty * 1);

            }
            UnitSale unitSaleObj = new UnitSale();
            if (unitQty == 0)
            {
                unitSaleObj.SaleUnitQty = Convert.ToInt32(unitQtyTextBox.Text);
                dtDinomination = opendMFDAO.dtDinomination(unitSaleObj.SaleUnitQty, regObj,userObj);
                dinoGridView.DataSource = dtDinomination;
                dinoGridView.DataBind();
                Session["dtDinomination"] = dtDinomination;
            }
            else if (unitQty > 0)
            {
                if (Convert.ToInt32(unitQtyTextBox.Text) != unitQty)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Unit Quantity and Denomination Quantity is not Equal');", true);
                    dtDinomination = new DataTable(); ;
                    dinoGridView.DataSource = dtDinomination;
                    dinoGridView.DataBind();
                    //Session["dtDinomination"] = dtDinomination;
                }
                else
                {
                    dinoGridView.DataSource = dtDinomination;
                    dinoGridView.DataBind();
                    Session["dtDinomination"] = dtDinomination;
                }
            }


        }
        
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
                dinoGridView.DataSource = opendMFDAO.getTableDinomination();
                dinoGridView.DataBind();
                dinoGridView.Visible = false;

                saleNumberTextBox.Text = unitSaleBLObj.getNextSaleNo(unitRegObj, userObj).ToString();
                // MoneyReceiptNoTextBox.Text = unitSaleBLObj.getNextMoneReceiptNo(unitRegObj, userObj).ToString();
                saleObj.SaleNo = unitSaleBLObj.getNextSaleNo(unitRegObj, userObj) - 1;
                saleDateTextBox.Text = opendMFDAO.getLastSaleDate(unitRegObj, saleObj).ToString("dd-MMM-yyyy");
                saleRateTextBox.Text = opendMFDAO.getLastSaleRate(unitRegObj, saleObj).ToString();
                saleTypeDropDownList.SelectedValue = unitSaleBLObj.GetNextSaleType(unitRegObj, userObj).ToString();
            }
            else
            {
                SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
                //  PhotoImage.ImageUrl = Path.Combine(ConfigReader.PhotoLocation, "Notavailable.JPG").ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", " window.fnResetAll();", true);
                tdCIP.InnerHtml = "";
                dinoGridView.Visible = false;
            }
        }
        else
        {
            SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
            tdCIP.InnerHtml = "";
            dinoGridView.Visible = false;
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
   
}
