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

public partial class UI_UnitRepurchaseEdit : System.Web.UI.Page
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
        
    }       
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
       // UnitTransfer transferObj = new UnitTransfer();
        UnitRepurchase unitRepObj = new UnitRepurchase();
        regObj.FundCode = FundCodeTextBox.Text.Trim();
        regObj.BranchCode = BranchCodeTextBox.Text.Trim();
        regObj.RegNumber = RegNoTextBox.Text.Trim();

        unitRepObj.RepurchaseNo = Convert.ToInt32(RepNoTextBox.Text.Trim().ToString());
        unitRepObj.RepurchaseRate = Convert.ToInt32(RepRateTextBox.Text.Trim().ToString());
        unitRepObj.RepurchaseDate = RepDateTextBox.Text.Trim().ToString();
 
        try
        {
            if (unitRepBLObj.IsDuplicateRepurchase(regObj, unitRepObj))
            {
                dvContentBottom.Visible = true;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " " + "Repurchase Number " + "');", true);

            }
            else
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

                unitRepBLObj.saveRepurchase(dtGrid, regObj, unitRepObj, userObj);//save Repurchase Data
                ClearText();

                leftDataGrid.DataSource = opendMFDAO.getTableDataGrid();// hide remaining Data
                leftDataGrid.DataBind();
                TotalUnitHoldingTextBox.Text = "";     

                //RepNoTextBox.Text = unitRepBLObj.getNextRepurchaseNo(regObj, userObj).ToString();
                //unitRepObj.RepurchaseNo = unitRepBLObj.getNextRepurchaseNo(regObj, userObj) - 1;
                //RepDateTextBox.Text = unitRepBLObj.getLastRepDate(regObj, unitRepObj).ToString("dd-MMM-yyyy");
                //RepRateTextBox.Text = unitRepBLObj.getLastRepRate(regObj, unitRepObj).ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save SuccessFully');", true);
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
            DataTable dtTotalSaleUnitCerts = opendMFDAO.getDtTotalSaleUnitCerts(unitRegObj);
            decimal TotalUnitsBalance = opendMFDAO.getTotalSaleUnitBalance(unitRegObj);
            if (dtRegInfo.Rows.Count > 0)
            {
                HolderNameTextBox.Text = dtRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["HNAME"].ToString();

                HolderJNameTextBox.Text = dtRegInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["JNT_NAME"].ToString();

                string[] BranchCodeSign = unitRegObj.BranchCode.Split('/');
                string imageSignLocation = Path.Combine(ConfigReader.SingLocation + "\\" + unitRegObj.FundCode, unitRegObj.FundCode + "_" + BranchCodeSign[0] + "_" + BranchCodeSign[1] + "_" + unitRegObj.RegNumber + ".jpg");//"../../Image/IAMCL/Sign/"+ fundCode + "_" + branchCode + "_" + regNo + ".jpg";
                string imagePhotoLocation = Path.Combine(ConfigReader.PhotoLocation + "\\" + unitRegObj.FundCode, unitRegObj.FundCode + "_" + "_" + BranchCodeSign[0] + BranchCodeSign[1] + "_" + unitRegObj.RegNumber + ".jpg");

                if (File.Exists(Path.Combine(ConfigReader.SingLocation + "\\" + unitRegObj.FundCode, unitRegObj.FundCode + "_" + BranchCodeSign[0] + "_" + BranchCodeSign[1] + "_" + unitRegObj.RegNumber + ".jpg")))
                {
                    SignImage.ImageUrl = imageSignLocation.ToString();
                }
                else
                {
                    SignImage.ImageUrl = Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString();
                }
                if (dtTotalSaleUnitCerts.Rows.Count > 0)
                {
                    dvContentBottom.Visible = true;
                    leftDataGrid.DataSource = dtTotalSaleUnitCerts;
                    leftDataGrid.DataBind();
                    TotalUnitHoldingTextBox.Text = TotalUnitsBalance.ToString();
                    TotalLienUnitHoldingTextBox.Text = unitLienBLObj.totalLienAmount(unitRegObj).ToString();
                }
                else
                {
                   // dvContentBottom.Visible = false;
                    TotalUnitHoldingTextBox.Text = "";
                    TotalUnitRepurchaseTextBox.Text = "";
                    TotalLienUnitHoldingTextBox.Text = unitLienBLObj.totalLienAmount(unitRegObj).ToString();
                    dvContentBottom.Visible = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Units To Repurchase');", true);

                }

                RepNoTextBox.Text = unitRepBLObj.getNextRepurchaseNo(unitRegObj, userObj).ToString();
                unitRepObj.RepurchaseNo = unitRepBLObj.getNextRepurchaseNo(unitRegObj, userObj) - 1;
                RepDateTextBox.Text = unitRepBLObj.getLastRepDate(unitRegObj, unitRepObj).ToString("dd-MMM-yyyy");
                RepRateTextBox.Text = unitRepBLObj.getLastRepRate(unitRegObj, unitRepObj).ToString();
                //transferNoTextBox.Text = unitTransferBLObj.getNextTransferNo(unitRegObj, userObj).ToString();

            }
            else
            {
                //HolderNameTextBox.Text = "";
                //HolderJNameTextBox.Text = "";
                TotalUnitHoldingTextBox.Text = "";
                TotalUnitRepurchaseTextBox.Text = "";
                TotalLienUnitHoldingTextBox.Text = "";
                dvContentBottom.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Units To Repurchase');", true);
            }
        }
        else
        {
            TotalUnitHoldingTextBox.Text = "";
            TotalUnitRepurchaseTextBox.Text = "";
            TotalLienUnitHoldingTextBox.Text = "";
            dvContentBottom.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Registration Number');", true);
        }
    }

    protected void findButton_Click(object sender, EventArgs e)
    {

    }
}
