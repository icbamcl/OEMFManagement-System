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

public partial class UI_UnitLienMark : System.Web.UI.Page
{
   
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();    
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    //UnitTransferBL unitTransferBLObj = new UnitTransferBL();
   // UnitRepurchaseBL unitRepBLObj = new UnitRepurchaseBL();
    UnitLienBl unitLienBLObj = new UnitLienBl();
    
    
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
        spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
        FundCodeTextBox.Text = fundCode.ToString();
        BranchCodeTextBox.Text = branchCode.ToString();
        
        RegNoTextBox.Focus();
      
            
 

     

        if (!IsPostBack)
        {
            LienbankNameDropDownList.DataSource = opendMFDAO.dtFillBankName();
            LienbankNameDropDownList.DataTextField = "BANK_NAME";
            LienbankNameDropDownList.DataValueField = "BANK_CODE";
            LienbankNameDropDownList.DataBind();

           
        }
    
    }    
    private void ClearText()
    {
        //RepNoTextBox.Text = "";
        //RepRateTextBox.Text = "";
        //RepDateTextBox.Text = "";
        //TotalUnitLienTextBox.Text = "";
        LienMarkNoTextBox.Text = "";
        LienReqDateTextBox.Text = "";
        LienReqRefTextBox.Text = "";
        LienbankNameDropDownList.SelectedValue = "0";
        LienbranchNameDropDownList.SelectedValue = "0";
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
        regObj.FundCode = FundCodeTextBox.Text.Trim();
        regObj.BranchCode = BranchCodeTextBox.Text.Trim();
        regObj.RegNumber = RegNoTextBox.Text.Trim();
        UnitLien unitLienObj = new UnitLien();
        unitLienObj.LienNo = Convert.ToInt32(LienMarkNoTextBox.Text.Trim().ToString());
        unitLienObj.LienInst = Convert.ToInt16(LienbankNameDropDownList.SelectedValue.ToString());
        unitLienObj.LienInstBranch = Convert.ToInt32(LienbranchNameDropDownList.SelectedValue.ToString());
        unitLienObj.LienRefference = LienReqRefTextBox.Text.Trim().ToString();
        unitLienObj.LienReqDate = LienReqDateTextBox.Text.Trim().ToString();
       
       
 
        try
        {
            if (unitLienBLObj.IsLienLock(regObj))
            {
                dvContentBottom.Visible = true;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + msgObj.Error().ToString() + " " + "Lien Operation is locked " + "');", true);

            }
            else if (unitLienBLObj.IsDuplicateLien(regObj, unitLienObj))
            {
                dvContentBottom.Visible = true;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " " + "Lien Number " + "');", true);

            }
            else
            {
                long totalSurrenderUnits = 0;
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
                        totalSurrenderUnits = totalSurrenderUnits + Convert.ToInt64(gridRow.Cells[3].Text.Trim().ToString());
                        dtGrid.Rows.Add(drGrid);
                    }
                }

                if (totalSurrenderUnits == Convert.ToInt64(TotalUnitLienTextBox.Text))
                {
                    unitLienBLObj.saveLienMark(dtGrid, regObj, unitLienObj, userObj);//save Lien Data
                    ClearText();

                    leftDataGrid.DataSource = opendMFDAO.getTableDataGrid();// hide remaining Data
                    leftDataGrid.DataBind();
                    TotalUnitHoldingTextBox.Text = "";

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save SuccessFully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed : Total Selected Units and Add Total Units is not equal');", true);
                }
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
        UnitLien unitLienObj=new UnitLien();

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
                }
                else
                {
                   // dvContentBottom.Visible = false;
                    TotalUnitHoldingTextBox.Text = "";
                    TotalUnitLienTextBox.Text = "";
                    dvContentBottom.Visible = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Units To Lien ');", true);
                }

                LienMarkNoTextBox.Text = unitLienBLObj.getNextLienNo(unitRegObj, userObj).ToString();
                unitLienObj.LienNo = unitLienBLObj.getNextLienNo(unitRegObj, userObj) - 1;
                LienReqDateTextBox.Text = unitLienBLObj.getLastLienReqDate(unitRegObj, unitLienObj).ToString("dd-MMM-yyyy");                

            }
            else
            {
                //HolderNameTextBox.Text = "";
                //HolderJNameTextBox.Text = "";
                TotalUnitHoldingTextBox.Text = "";
                TotalUnitLienTextBox.Text = "";
                dvContentBottom.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Units To Lien');", true);
            }
        }
        else
        {
            TotalUnitHoldingTextBox.Text = "";
            TotalUnitLienTextBox.Text = "";
            dvContentBottom.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Registration Number');", true);
        }
    }

    protected void LienbankNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LienbranchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(LienbankNameDropDownList.SelectedValue.ToString()));
        LienbranchNameDropDownList.DataTextField = "BRANCH_NAME";
        LienbranchNameDropDownList.DataValueField = "BRANCH_CODE";
        LienbranchNameDropDownList.DataBind();

    }
    protected void AddTotalButton_Click(object sender, EventArgs e)
    {
        long totalSurrenderUnits = 0;
        DataTable dtGrid = opendMFDAO.getTableDataGrid();

        foreach (DataGridItem gridRow in leftDataGrid.Items)
        {
            CheckBox leftCheckBox = (CheckBox)gridRow.FindControl("leftCheckBox");
            if (leftCheckBox.Checked)
            {

                totalSurrenderUnits = totalSurrenderUnits + Convert.ToInt64(gridRow.Cells[3].Text.Trim().ToString());

            }
        }
        TotalUnitLienTextBox.Text = totalSurrenderUnits.ToString();
    }
}
