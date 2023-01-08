﻿using System;
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

public partial class UI_UnitTransferEdit : System.Web.UI.Page
{
    System.Web.UI.Page this_page_ref = null;
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();    
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitTransferBL unitTransferBLObj = new UnitTransferBL();
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
        tferorFundCodeTextBox.Text = fundCode.ToString();
        tferorBranchCodeTextBox.Text = branchCode.ToString();
        tfereeFundCodeTextBox.Text = fundCode.ToString();
        tfereeBranhCodeTextBox.Text = branchCode.ToString();
            //dvContentBottom.Visible = false;
            //if( transferNoTextBox.Text=="")
            //{
            //    transferNoTextBox.Focus();
            //}
            //if (transferDateTextBox.Text == "")
            //{
            //    transferDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            //}
            
      

        
        //tferorRegNoTextBox.Focus();
       // holderDateofBirthTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
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
        DataTable dtTotalSaleUnitCerts = opendMFDAO.getDtTotalSaleUnitCerts(unitRegObj);
        decimal TotalUnitsBalance = opendMFDAO.getTotalSaleUnitBalance(unitRegObj);
        if (dtRegInfo.Rows.Count > 0)
        {
            tferorHolderNameTextBox.Text = dtRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["HNAME"].ToString();
            tferorjHolderNameTextBox.Text = dtRegInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["JNT_NAME"].ToString();

            string imageSignLocation = Path.Combine(ConfigReader.SingLocation, unitRegObj.FundCode + "_" + unitRegObj.BranchCode + "_" + unitRegObj.RegNumber + ".jpg");

            if (File.Exists(Path.Combine(ConfigReader.SingLocation, unitRegObj.FundCode.ToString() + "_" + unitRegObj.BranchCode + "_" + unitRegObj.RegNumber + ".jpg")))
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
            }
            else
            {
                dvContentBottom.Visible = false;
            }
            transferNoTextBox.Text = unitTransferBLObj.getNextTransferNo(unitRegObj, userObj,"T" ).ToString();
            TotalUnitHoldingTextBox.Text = TotalUnitsBalance.ToString();

        }
        else
        {
            tferorHolderNameTextBox.Text = "";
            tferorjHolderNameTextBox.Text = "";
            dvContentBottom.Visible = false;
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('No Units To Transfer');", true);

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

            string imageSignLocation = Path.Combine(ConfigReader.SingLocation, unitRegObj.FundCode + "_" + unitRegObj.BranchCode + "_" + unitRegObj.RegNumber + ".jpg");

            if (File.Exists(Path.Combine(ConfigReader.SingLocation, unitRegObj.FundCode.ToString() + "_" + unitRegObj.BranchCode + "_" + unitRegObj.RegNumber + ".jpg")))
            {
                PhotoImage.ImageUrl = imageSignLocation.ToString();
            }
            else
            {
                PhotoImage.ImageUrl = Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString();
            }
        }
        else
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "Popup", " window.fnResetAll();", true);
            PhotoImage.ImageUrl = "";

        }

        unitRegObj = new UnitHolderRegistration();
        unitRegObj.FundCode = tferorFundCodeTextBox.Text.Trim();
        unitRegObj.BranchCode = tferorBranchCodeTextBox.Text.Trim();
        unitRegObj.RegNumber = tferorRegNoTextBox.Text.Trim();
        decimal tFerorTotalUnits = opendMFDAO.getTotalSaleUnitBalance(unitRegObj);
        if (tFerorTotalUnits > 0 )
        {
            dvContentBottom.Visible = true;
        }
        else
        {
            dvContentBottom.Visible = false;
        }
        
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {

        UnitHolderRegistration regObj = new UnitHolderRegistration();
        UnitTransfer transferObj=new UnitTransfer();
        regObj.FundCode = tfereeFundCodeTextBox.Text.Trim();
        regObj.BranchCode = tfereeBranhCodeTextBox.Text.Trim();
        regObj.RegNumber = tferorRegNoTextBox.Text.Trim();

        transferObj.TransferNo = Convert.ToInt32(transferNoTextBox.Text.Trim());
        transferObj.TransferDate = transferDateTextBox.Text.Trim().ToString();
        transferObj.TransferorRegNo = tferorRegNoTextBox.Text.Trim().ToString();
        transferObj.TferorBranchCode = tferorBranchCodeTextBox.Text.Trim().ToString();
        transferObj.TransfereeRegNo = tfereeRegNoTextBox.Text.Trim().ToString();
        transferObj.TfereeBranchCode = tfereeBranhCodeTextBox.Text.Trim().ToString();
      
        try
        {
            if (unitTransferBLObj.IsDuplicateTransfer(regObj,transferObj,"T"))
            {
                dvContentBottom.Visible = true;
                transferNoTextBox.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " " + "Transfer Number " + "');", true);
                
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
                
                unitTransferBLObj.saveTransfer(dtGrid, regObj, transferObj, userObj);//save Transfer Data
                ClearText();
                leftDataGrid.DataSource = opendMFDAO.getTableDataGrid();// hide remaining Data
                leftDataGrid.DataBind();
                tferorRegNoTextBox.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('Save SuccessFully');", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + ex.Message.Replace("'", "").ToString() + "');", true);
        }
        
    }

}
