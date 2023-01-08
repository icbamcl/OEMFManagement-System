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

public partial class UI_UnitTransferEntryCDS : System.Web.UI.Page
{
    //System.Web.UI.Page this_page_ref = null;
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();    
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitTransferBL unitTransferBLObj = new UnitTransferBL();
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
        tferorFundCodeTextBox.Text = fundCode.ToString();
        tferorBranchCodeTextBox.Text = branchCode.ToString();
        tfereeFundCodeTextBox.Text = fundCode.ToString();
        tfereeBranhCodeTextBox.Text = branchCode.ToString();
          
        if (!IsPostBack)
        {
            tferorRegNoTextBox.Focus();
            transferDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        }
    
    }    
    private void ClearText()
    {
        transferNoTextBox.Text = "";
        transferDateTextBox.Text = "";
        tferorHolderNameTextBox.Text = "";
        tferorjHolderNameTextBox.Text = "";
        tferorRegNoTextBox.Text = "";
        tfereeRegNoTextBox.Text = "";
        tfereeHolderNameTextBox.Text = "";
        tfereejHolderNameTextBox.Text = "";
        TotalUnitHoldingTextBox.Text = "";
        TotalUnitRepurchaseTextBox.Text = "";
        
    }       
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }
    protected void tferorRegNoTextBox_TextChanged(object sender, EventArgs e)
    {

        UnitHolderRegistration unitRegObj = new UnitHolderRegistration();
        unitRegObj.FundCode = tferorFundCodeTextBox.Text.Trim();
        unitRegObj.BranchCode = tferorBranchCodeTextBox.Text.Trim();
        unitRegObj.RegNumber = tferorRegNoTextBox.Text.Trim();


        DataTable dtRegInfo = opendMFDAO.getDtRegInfo(unitRegObj);
        DataTable dtTotalSaleUnitCerts = opendMFDAO.getDtTotalSaleUnitCertsCDS(unitRegObj);
        decimal TotalUnitsBalance = opendMFDAO.getTotalSaleUnitBalanceCDS(unitRegObj);
        if (dtRegInfo.Rows.Count > 0)
        {
            tferorHolderNameTextBox.Text = dtRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["HNAME"].ToString();
            tferorjHolderNameTextBox.Text = dtRegInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["JNT_NAME"].ToString();

            string[] branchCodeSign = unitRegObj.BranchCode.Split('/');
            string imageSignLocation = Path.Combine(ConfigReader.SingLocation + "\\" + unitRegObj.FundCode, unitRegObj.FundCode + "_" + branchCodeSign[0] + "_" + branchCodeSign[1] + "_" + unitRegObj.RegNumber + ".jpg");

            if (File.Exists(Path.Combine(ConfigReader.SingLocation + "\\" + unitRegObj.FundCode, unitRegObj.FundCode.ToString() + "_" + branchCodeSign[0] + "_" + branchCodeSign[1] + "_" + unitRegObj.RegNumber + ".jpg")))
            {
                SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(imageSignLocation.ToString());
            }
            else
            {
                SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
            }
            if (dtTotalSaleUnitCerts.Rows.Count > 0)
            {
                transferNoTextBox.Text = unitTransferBLObj.getNextTransferNo(unitRegObj, userObj,"T").ToString();
                leftDataGrid.DataSource = dtTotalSaleUnitCerts;
                leftDataGrid.DataBind();
                TotalUnitHoldingTextBox.Text = TotalUnitsBalance.ToString();
                TotalUnitRepurchaseTextBox.Text = "";
            }
            else
            {
                transferNoTextBox.Text = "";
                TotalUnitHoldingTextBox.Text = "";
                TotalUnitRepurchaseTextBox.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Units To Transfer');", true);
            }
            

        }
        else
        {
            TotalUnitHoldingTextBox.Text = "";           
            tferorHolderNameTextBox.Text = "";
            tferorjHolderNameTextBox.Text = "";
            TotalUnitRepurchaseTextBox.Text = "";
            transferNoTextBox.Text = "";
            
           // dvContentBottom.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Registration Number');", true);

        }
    }
    protected void tfereeRegNoTextBox_TextChanged(object sender, EventArgs e)
    {
        
        string unitHolderName = "";
        string branchName = "";
        string fundName = "";
        UnitHolderRegistration unitRegObj = new UnitHolderRegistration();
       
        unitRegObj.FundCode = tfereeFundCodeTextBox.Text.Trim();
        unitRegObj.BranchCode = tfereeBranhCodeTextBox.Text.Trim();
        unitRegObj.RegNumber = tfereeRegNoTextBox.Text.Trim();
        DataTable dtRegInfo = opendMFDAO.getDtRegInfo(unitRegObj);
        if (dtRegInfo.Rows.Count > 0)
        {
            tfereeHolderNameTextBox.Text = dtRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["HNAME"].ToString();
          
            tfereejHolderNameTextBox.Text = dtRegInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["JNT_NAME"].ToString();
            fundName = opendMFDAO.GetFundName(unitRegObj.FundCode.ToString());
            branchName = opendMFDAO.GetBranchName(unitRegObj.BranchCode.ToString());
            unitHolderName = opendMFDAO.GetHolderName(unitRegObj.FundCode, unitRegObj.BranchCode, unitRegObj.RegNumber);

            string[] branchCodeSign = unitRegObj.BranchCode.Split('/');
            string imageSignLocation = Path.Combine(ConfigReader.SingLocation + "\\" + unitRegObj.FundCode, unitRegObj.FundCode + "_" + branchCodeSign[0] + "_" + branchCodeSign[1] + "_" + unitRegObj.RegNumber + ".jpg");

            if (File.Exists(Path.Combine(ConfigReader.SingLocation + "\\" + unitRegObj.FundCode, unitRegObj.FundCode.ToString() + "_" + branchCodeSign[0] + "_" + branchCodeSign[1] + "_" + unitRegObj.RegNumber + ".jpg")))
            {
                PhotoImage.ImageUrl =encrypt.PhotoBase64ImgSrc( imageSignLocation.ToString());
            }
            else
            {
                PhotoImage.ImageUrl = encrypt.PhotoBase64ImgSrc(Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
            }

            unitRegObj = new UnitHolderRegistration();
            unitRegObj.FundCode = tferorFundCodeTextBox.Text.Trim();
            unitRegObj.BranchCode = tferorBranchCodeTextBox.Text.Trim();
            unitRegObj.RegNumber = tferorRegNoTextBox.Text.Trim();
            decimal tFerorTotalUnits = opendMFDAO.getTotalSaleUnitBalance(unitRegObj);
            if (tFerorTotalUnits > 0)
            {
               // dvContentBottom.Visible = true;
            }
            else
            {
               // dvContentBottom.Visible = false;
            }
        }
        else
        {
            
            PhotoImage.ImageUrl = "";
            tfereeHolderNameTextBox.Text = "";
            tfereejHolderNameTextBox.Text = "";
            //dvContentBottom.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Registration Number');", true);

        }

       
        
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {

        UnitHolderRegistration regObj = new UnitHolderRegistration();
        UnitTransfer transferObj=new UnitTransfer();
        regObj.FundCode = tfereeFundCodeTextBox.Text.Trim();
        regObj.BranchCode = tfereeBranhCodeTextBox.Text.Trim();
        regObj.RegNumber = tferorRegNoTextBox.Text.Trim();

        transferObj.TransferType = "T";
        transferObj.TransferNo = Convert.ToInt32(transferNoTextBox.Text.Trim());
        transferObj.TransferDate = transferDateTextBox.Text.Trim().ToString();
        transferObj.TransferorRegNo = tferorRegNoTextBox.Text.Trim().ToString();
        transferObj.TferorBranchCode = tferorBranchCodeTextBox.Text.Trim().ToString();
        transferObj.TransfereeRegNo = tfereeRegNoTextBox.Text.Trim().ToString();
        transferObj.TfereeBranchCode = tfereeBranhCodeTextBox.Text.Trim().ToString();
      
        try
        {

            if (unitTransferBLObj.IsTransferLock(regObj))
            {
               
                transferNoTextBox.Focus();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + msgObj.Error().ToString() + " " + "Transfer Opearation is Locked " + "');", true);
                
            }
            else if (unitTransferBLObj.IsDuplicateTransfer(regObj, transferObj,"T"))
            {
            
                transferNoTextBox.Focus();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " " + "Transfer Number " + "');", true);
                
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

                unitTransferBLObj.saveTransferCDS(dtGrid, regObj, transferObj, userObj);//save Transfer Data
                ClearText();
                leftDataGrid.DataSource = opendMFDAO.getTableDataGridCDS();// hide remaining Data
                leftDataGrid.DataBind();
                tferorRegNoTextBox.Focus();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save SuccessFully');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + ex.Message.Replace("'", "").ToString() + "');", true);
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
