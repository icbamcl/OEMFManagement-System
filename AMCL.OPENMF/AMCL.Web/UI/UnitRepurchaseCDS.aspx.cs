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

public partial class UI_UnitRepurchaseCDS : System.Web.UI.Page
{
    System.Web.UI.Page this_page_ref = null;
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();    
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    //UnitTransferBL unitTransferBLObj = new UnitTransferBL();
    UnitRepurchaseBL unitRepBLObj = new UnitRepurchaseBL();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
    BaseClass bcContent = new BaseClass();
    UnitLienBl unitLienBLObj = new UnitLienBl();
    EncryptDecrypt encrypt = new EncryptDecrypt();
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
        FundCodeTextBox.Text = fundCode.ToString();
        BranchCodeTextBox.Text = branchCode.ToString();
        
        RegNoTextBox.Focus();
      
            
 

     

        if (!IsPostBack)
        {
            //UnitRepurchase unitRepObj = new UnitRepurchase();
            //regObj.BranchCode = BranchCodeTextBox.Text.Trim().ToUpper().ToString();
            //regObj.FundCode = FundCodeTextBox.Text.Trim().ToUpper().ToString();
            //RepNoTextBox.Text = unitRepBLObj.getMaxRepurchaseNo(regObj).ToString();
            //unitRepObj.RepurchaseNo = unitRepBLObj.getMaxRepurchaseNo(regObj) - 1;
            //RepDateTextBox.Text = unitRepBLObj.getLastRepDate(regObj, unitRepObj).ToString("dd-MMM-yyyy");
            //RepRateTextBox.Text = unitRepBLObj.getLastRepRate(regObj, unitRepObj).ToString();
        }
    
    }    
    private void ClearText()
    {
        RepNoTextBox.Text = "";
        RepRateTextBox.Text = "";
        RepDateTextBox.Text = "";
        TotalUnitRepurchaseTextBox.Text = "";
        RegNoTextBox.Text = "";
        HolderJNameTextBox.Text = "";
        HolderNameTextBox.Text = "";
        //NormalRadioButton.Checked = true;
        //DeathRadioButton.Checked = false;
        
    }       
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
      
        UnitRepurchase unitRepObj = new UnitRepurchase();
        regObj.FundCode = FundCodeTextBox.Text.Trim();
        regObj.BranchCode = BranchCodeTextBox.Text.Trim();
        regObj.RegNumber = RegNoTextBox.Text.Trim();

        unitRepObj.RepurchaseNo = Convert.ToInt32(RepNoTextBox.Text.Trim().ToString());
        unitRepObj.RepurchaseRate = decimal.Parse(RepRateTextBox.Text.Trim().ToString());
        unitRepObj.RepurchaseDate = RepDateTextBox.Text.Trim().ToString();
        if (EFTRadioButton.Checked)
        {
            unitRepObj.PayType = "EFT";
        }
        else
        {
            unitRepObj.PayType = "CHQ";
        }
 
        try
        {
            if (opendMFDAO.IsValidRegistration(regObj))
            {
                if (unitRepBLObj.IsRepurchaseLock(regObj))
                {
                  
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + msgObj.Error().ToString() + " " + " Repurchase Operation is locked " + "');", true);

                }

                else if (unitRepBLObj.IsDuplicateRepurchase(regObj, unitRepObj))
                {
                    
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " " + "Repurchase Number " + "');", true);

                }
                else if (Convert.ToInt64(TotalUnitRepurchaseTextBox.Text.ToString()) <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed: Surrender Value con not be equal or less than Zero');", true);
                }
                else if (!unitRepBLObj.IsValidBEFTN(regObj, unitRepObj))
                {
                   
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + msgObj.Error().ToString() + " " + "Either No Router Number  or Account Number>13 digits " + "');", true);
                }
                else if (unitRepBLObj.IsIDAccount(regObj, unitRepObj))
                {
                   
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + msgObj.Error().ToString() + " " + "ID Account is not allow to BEFTN " + "');", true);
                }
                else
                {

                    DataTable dtGrid = opendMFDAO.getTableDataGridCDS();
                    DataRow drGrid;
                    foreach (DataGridItem gridRow in leftDataGrid.Items)
                    {
                        CheckBox leftCheckBox = (CheckBox)gridRow.FindControl("leftCheckBox");
                        if (leftCheckBox.Checked)
                        {
                            TextBox SL_TR_NOTxt = (TextBox)gridRow.FindControl("SL_TR_NoTextBox");
                            TextBox SURRENDER_UNITSTxt = (TextBox)gridRow.FindControl("Sale_UnitsTextBox");
                            TextBox EXIST_UNITSTxt = (TextBox)gridRow.FindControl("Exist_UnitsTextBox");
                            drGrid = dtGrid.NewRow();
                            drGrid["SL_TR_NO"] = SL_TR_NOTxt.Text.Trim().ToString();
                            drGrid["SURRENDER_UNITS"] = SURRENDER_UNITSTxt.Text.Trim().ToString();
                            drGrid["EXIST_UNITS"] = EXIST_UNITSTxt.Text.Trim().ToString();
                            dtGrid.Rows.Add(drGrid);

                        }
                    }

                    unitRepBLObj.saveRepurchaseCDS(dtGrid, regObj, unitRepObj, userObj);//save Repurchase Data
                    ClearText();

                    leftDataGrid.DataSource = opendMFDAO.getTableDataGridCDS();// hide remaining Data
                    leftDataGrid.DataBind();
                    TotalUnitHoldingTextBox.Text = "";
                    EFTRadioButton.Checked = true;
                    CHQRadioButton.Checked = false;

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save SuccessFully');", true);
                }
            }
            else
            {
                ClearText();
                leftDataGrid.DataSource = opendMFDAO.getTableDataGridCDS();// hide remaining Data
                leftDataGrid.DataBind();
                TotalUnitHoldingTextBox.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Registration Number');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + ex.Message.Replace("'", "").ToString() + "');", true);
        }
        
    }
    protected void RegNoTextBox_TextChanged(object sender, EventArgs e)
    {
        UnitHolderRegistration unitRegObj = new UnitHolderRegistration();
        unitRegObj.FundCode = FundCodeTextBox.Text.Trim();
        unitRegObj.BranchCode = BranchCodeTextBox.Text.Trim();
        unitRegObj.RegNumber = RegNoTextBox.Text.Trim();
        UnitRepurchase unitRepObj = new UnitRepurchase();

        if (opendMFDAO.IsValidRegistration(unitRegObj))
        {
            DataTable dtRegInfo = opendMFDAO.getDtRegInfo(unitRegObj);
            DataTable dtTotalSaleUnitCerts = opendMFDAO.getDtTotalSaleUnitCertsCDS(unitRegObj);
            decimal TotalUnitsBalance = opendMFDAO.getTotalSaleUnitBalanceCDS(unitRegObj);
            if (dtRegInfo.Rows.Count > 0)
            {
                HolderNameTextBox.Text = dtRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["HNAME"].ToString();

                HolderJNameTextBox.Text = dtRegInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["JNT_NAME"].ToString();

                string[] BranchCodeSign = unitRegObj.BranchCode.Split('/');
                string imageSignLocation = Path.Combine(ConfigReader.SingLocation + "\\" + unitRegObj.FundCode, unitRegObj.FundCode + "_" + BranchCodeSign[0] + "_" + BranchCodeSign[1] + "_" + unitRegObj.RegNumber + ".jpg");//"../../Image/IAMCL/Sign/"+ fundCode + "_" + branchCode + "_" + regNo + ".jpg";
                string imagePhotoLocation = Path.Combine(ConfigReader.PhotoLocation + "\\" + unitRegObj.FundCode, unitRegObj.FundCode + "_" + "_" + BranchCodeSign[0] + BranchCodeSign[1] + "_" + unitRegObj.RegNumber + ".jpg");

                if (File.Exists(Path.Combine(ConfigReader.SingLocation + "\\" + unitRegObj.FundCode, unitRegObj.FundCode + "_" + BranchCodeSign[0] + "_" + BranchCodeSign[1] + "_" + unitRegObj.RegNumber + ".jpg")))
                {
                    SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc( imageSignLocation.ToString());
                }
                else
                {
                    SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
                }
                if (dtTotalSaleUnitCerts.Rows.Count > 0)
                {
                  
                    leftDataGrid.DataSource = dtTotalSaleUnitCerts;
                    leftDataGrid.DataBind();
                    TotalUnitHoldingTextBox.Text = TotalUnitsBalance.ToString();
                    
                }
                else
                {
                   
                    TotalUnitHoldingTextBox.Text = "";
                    TotalUnitRepurchaseTextBox.Text = "";
                 
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Units To Repurchase');", true);

                }
                TotalUnitRepurchaseTextBox.Text = "";
                RepNoTextBox.Text = unitRepBLObj.getNextRepurchaseNo(unitRegObj, userObj).ToString();
              //  unitRepObj.RepurchaseNo = unitRepBLObj.getNextRepurchaseNo(unitRegObj, userObj) - 1;
            //    RepDateTextBox.Text = unitRepBLObj.getLastRepDate(unitRegObj, unitRepObj).ToString("dd-MMM-yyyy");
            //    RepRateTextBox.Text = unitRepBLObj.getLastRepRate(unitRegObj, unitRepObj).ToString();
               

            }
            else
            {
                TotalUnitHoldingTextBox.Text = "";
                TotalUnitRepurchaseTextBox.Text = "";
            
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Units To Repurchase');", true);
            }
        }
        else
        {
            ClearText();
            TotalUnitHoldingTextBox.Text = "";
            TotalUnitRepurchaseTextBox.Text = "";
           
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Registration Number');", true);
        }
    }

    protected void addTotalSum_Click(object sender, EventArgs e)
    {
        long totalSurrenderUnits = 0;
        DataTable dtGrid = opendMFDAO.getTableDataGridCDS();
        
        foreach (DataGridItem gridRow in leftDataGrid.Items)
        {
            CheckBox leftCheckBox = (CheckBox)gridRow.FindControl("leftCheckBox");
            if (leftCheckBox.Checked)
            {
                TextBox surrenderTxt = (TextBox)gridRow.FindControl("Sale_UnitsTextBox");
                totalSurrenderUnits = totalSurrenderUnits + Convert.ToInt64(surrenderTxt.Text.ToString());
               
            }
        }
        TotalUnitRepurchaseTextBox.Text = totalSurrenderUnits.ToString();

    }
}
