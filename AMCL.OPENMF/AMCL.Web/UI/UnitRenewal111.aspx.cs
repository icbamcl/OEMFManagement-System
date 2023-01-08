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

public partial class UI_Renewal : System.Web.UI.Page
{
   
    OMFDAO opendMFDAO = new OMFDAO();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitHolderRegistration regObj=new UnitHolderRegistration();
    BaseClass bcContent = new BaseClass();
    UnitRenewal renwalObj = new UnitRenewal();
    UnitRenewalBL renewalBLObj = new UnitRenewalBL();
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
        fundCodeTextBox.Text = fundCode.ToString();
        branchCodeTextBox.Text = branchCode.ToString();
        regNoTextBox.Focus();
        //saleDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
       // holderDateofBirthTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        if (!IsPostBack)
        {
            regObj.FundCode = fundCode.ToString();
            regObj.BranchCode = branchCode.ToString();
           // renewalNumberTextBox.Text = opendMFDAO.GetMaxSaleNo(regObj).ToString();
           // renewalDateTextBox.Text = opendMFDAO.getLastSaleDate(regObj).ToString("dd-MMM-yyyy");
           // totalUnitsTextBox.Text = opendMFDAO.getLastSaleRate(regObj).ToString();

        }
    
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        
        
        UnitHolderRegistration regObj = new UnitHolderRegistration();       
        regObj.FundCode = fundCodeTextBox.Text;
        regObj.BranchCode = branchCodeTextBox.Text;        
        regObj.RegNumber = regNoTextBox.Text;

        renwalObj = new UnitRenewal();
        renwalObj.RenewalNo = renewalNumberTextBox.Text.Trim().ToString();
        renwalObj.RenewalDate = renewalDateTextBox.Text.Trim().ToString();
        renwalObj.RenewalType = renewalTypeDropDownList.SelectedValue.ToString().ToUpper();
        renwalObj.RenewalUnitQty = Convert.ToInt32(renewalUnitsTextBox.Text.Trim());
                             
        try
        {
            if (renewalBLObj.IsRenewalLock(regObj))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + msgObj.Error().ToString() + " " + "Renewal Operation is Locked " + "');", true);
            }
            else if (renewalBLObj.IsDuplicateRenewal(regObj,renwalObj))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " " + "Renewal Number " + "');", true);
            }
            else
            {
                int rowNumber = 1;
                int line = 0;
                DataTable dtDino = opendMFDAO.getTableDinomination();
                DataRow drDino;
                bool invalidCert = false;
                bool allocateCert = true;
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
                    if (duplicateCerNoReg == 0)
                    {

                        if (opendMFDAO.validationDino(txtDino.Text.ToString().ToUpper(), regObj.FundCode.ToString().ToUpper()) && opendMFDAO.validationWeight(Convert.ToInt32(txtWeight.Text.ToString()), regObj.FundCode.ToString().ToUpper()))
                        {
                            if (allocateCert)
                            {

                                drDino["dino"] = txtDino.Text.Trim().ToString().ToUpper();
                                drDino["cert_no"] = Convert.ToInt32(txtCert.Text.Trim().ToString());
                                drDino["cert_weight"] = Convert.ToInt32(txtWeight.Text.Trim().ToString());
                                dtDino.Rows.Add(drDino);
                                rowNumber++;
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

                if (duplicateCerNoReg > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Certificate " + dino + "-" + cerNo + " is Already Used to Reg No:" + duplicateCerNoReg + "');", true);
                }
                else
                {
                    if (invalidCert)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Failed!!Invalid Dinomination or Weight at Line: " + line + "');", true);

                    }
                    else if (!allocateCert)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Failed!! Line: " + line + " and Certificate No: " + dino + "-" + cerNo + " is not allocated in this branch');", true);
                    }
                    else if (!invalidCert && allocateCert)
                    {

                        DataTable dtGrid = opendMFDAO.getTableDataGrid();
                        DataRow drGrid;
                        foreach (DataGridItem gridRow in leftDataGrid.Items)
                        {
                            CheckBox leftCheckBox = (CheckBox)gridRow.FindControl("leftCheckBox");
                            if (leftCheckBox.Checked)
                            {
                                drGrid = dtGrid.NewRow();
                                drGrid["SL_NO"] = gridRow.Cells[1].Text.Trim().ToString();
                                drGrid["CERTIFICATE"] = gridRow.Cells[2].Text.Trim().ToString();
                                drGrid["QTY"] = gridRow.Cells[3].Text.Trim().ToString();
                                dtGrid.Rows.Add(drGrid);
                            }
                        }

                        renewalBLObj.SaveUnitRenewal(regObj, dtGrid, renwalObj, dtDino, userObj);
                       // unitSaleBLObj.SaveUnitSale(regObj, saleObj, dtDino, userObj);
                        ClearText();
                        leftDataGrid.Visible = false;
                        renewalNumberTextBox.Text = "";
                        regNoTextBox.Text = "";
                        //saleNumberTextBox.Text = unitSaleBLObj.getNextSaleNo(regObj, userObj).ToString();
                        //saleObj.SaleNo = unitSaleBLObj.getNextSaleNo(regObj, userObj) - 1;
                        //saleDateTextBox.Text = opendMFDAO.getLastSaleDate(regObj, saleObj).ToString("dd-MMM-yyyy");
                        //saleRateTextBox.Text = opendMFDAO.getLastSaleRate(regObj, saleObj).ToString();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + msgObj.Success().ToString() + "');", true);
                    }
                }

            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + ex.Message.Replace("'", "").ToString() + "');", true);
        }


    }
    private void ClearText()
    {
        DataTable dtDino = opendMFDAO.getTableDinomination();
        renewalDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        holderNameTextBox.Text="";
        jHolderTextBox.Text="";
        holderAddress1TextBox.Text="";
        holderAddress2TextBox.Text="";       
        holderTelphoneTextBox.Text="";        
        tdCIP.InnerHtml = "";
        renewalTypeDropDownList.SelectedValue = "SL";
        //saleNumberTextBox.Text = "";
        totalUnitsTextBox.Text = "";        
        renewalUnitsTextBox.Text = "";
        RemarksTextBox.Text = "";        
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
        kTextBox.Text = "";
    }
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }
    protected void findButton_Click(object sender, EventArgs e)
    {
       
        displayRegInfo();
                    
    }
    protected void addListButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeTextBox.Text;
        regObj.BranchCode = branchCodeTextBox.Text;
        dvContent.Visible = true;
        dinoGridView.Visible = true;
        int unitQty = 0;
        int certNo = 0;
        DataTable dtDinomination = opendMFDAO.getTableDinomination();
        DataRow drDinomination = dtDinomination.NewRow();
        int certQty=0;


        if (kTextBox.Text != "")
        {
            if (string.Compare(fundCodeTextBox.Text.Trim().ToString(), "BDF") == 0)
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

            certNo = opendMFDAO.GetMaxCertNo("J",regObj, userObj);
            certQty=Convert.ToInt32(jTextBox.Text.Trim());
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
        if (unitQty==0)
        {
            
            unitSaleObj.SaleUnitQty = Convert.ToInt32(renewalUnitsTextBox.Text);
            dtDinomination = opendMFDAO.dtDinomination(unitSaleObj.SaleUnitQty, regObj, userObj);
            dinoGridView.DataSource = dtDinomination;
            dinoGridView.DataBind();
            Session["dtDinomination"] = dtDinomination;
        }
        else if (unitQty>0)
        {
            if (Convert.ToInt32(renewalUnitsTextBox.Text) != unitQty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('Unit Quantity and Dinomination Quantity is not Equal');", true);
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
    public void displayRegInfo()
    {
        UnitHolderRegistration unitRegObj = new UnitHolderRegistration();
        unitRegObj.FundCode = fundCodeTextBox.Text.Trim();
        unitRegObj.BranchCode = branchCodeTextBox.Text.Trim();
        unitRegObj.RegNumber = regNoTextBox.Text.Trim();
       
        if (opendMFDAO.IsValidRegistration(unitRegObj))
        {
            DataTable dtRegInfo = opendMFDAO.getDtRegInfo(unitRegObj);
            DataTable dtTotalSaleUnitCerts = opendMFDAO.getDtTotalSaleUnitCerts(unitRegObj);
            decimal TotalUnitsBalance = opendMFDAO.getTotalSaleUnitBalance(unitRegObj);
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
                    tdCIP.InnerHtml = "YES";
                }
                else if (string.Compare(CIP, "N", true) == 0)
                {
                    tdCIP.InnerHtml = "NO";
                }

                displaySign();
                
                if (dtTotalSaleUnitCerts.Rows.Count > 0)
                {
                    renewalUnitsTextBox.Text = "";
                    leftDataGrid.Visible = true;
                    dvContentBottom.Visible = true;
                    dinoGridView.Visible = false; 
                    leftDataGrid.DataSource = dtTotalSaleUnitCerts;
                    leftDataGrid.DataBind();
                    totalUnitsTextBox.Text = TotalUnitsBalance.ToString();

                }
                else
                {
                    dinoGridView.DataSource = opendMFDAO.getTableDinomination();
                    dinoGridView.DataBind();
                    dinoGridView.Visible = false;                  
                    leftDataGrid.Visible = false;
                    totalUnitsTextBox.Text = "";
                    renewalUnitsTextBox.Text = "";                    
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Units To Renewal');", true);
                }

                renewalNumberTextBox.Text = renewalBLObj.getNextRenNo(unitRegObj, userObj).ToString();

                // saleObj.SaleNo = unitSaleBLObj.getNextSaleNo(unitRegObj, userObj) - 1;
                // renewalDateTextBox.Text = opendMFDAO.getLastSaleDate(unitRegObj, saleObj).ToString("dd-MMM-yyyy");
                // saleRateTextBox.Text = opendMFDAO.getLastSaleRate(unitRegObj, saleObj).ToString();
                // saleTypeDropDownList.SelectedValue = unitSaleBLObj.GetNextSaleType(unitRegObj, userObj).ToString();

            }
            else
            {
               
                SignImage.ImageUrl = Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString();
                tdCIP.InnerHtml = "";
                leftDataGrid.Visible = false;
                dvContentBottom.Visible = false;
                //  PhotoImage.ImageUrl = Path.Combine(ConfigReader.PhotoLocation, "Notavailable.JPG").ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", " window.fnResetAll();", true);

            }
        }
        else
        {
            totalUnitsTextBox.Text = "";
            renewalUnitsTextBox.Text = "";                  
            dvContentBottom.Visible = false;
            leftDataGrid.Visible = false;
            ClearText();
            SignImage.ImageUrl = Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString();
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
            SignImage.ImageUrl = imageSignLocation.ToString();
        }
        else
        {
            SignImage.ImageUrl = Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString();
        }
        //if (File.Exists(Path.Combine(ConfigReader.PhotoLocation, fundCode + "_" + branchCode + "_" + regNo + ".jpg")))
        //{
        //    PhotoImage.ImageUrl = imagePhotoLocation.ToString();


        //}
        //else
        //{
        //    PhotoImage.ImageUrl = Path.Combine(ConfigReader.PhotoLocation, "Notavailable.JPG").ToString();
        //}
    }

    protected void regNoTextBox_TextChanged(object sender, EventArgs e)
    {
        displayRegInfo();
    }
}
