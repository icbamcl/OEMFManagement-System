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

public partial class UI_UnitRepurchaseWithMoneyRctCDS : System.Web.UI.Page
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
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();

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
            regObj.FundCode = fundCode.ToString();
            regObj.BranchCode = branchCode.ToString();
            moneyReceipDropDownList.DataSource = unitSaleBLObj.dtMoneyRecieptforDDL(" AND REG_BK = '" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR = '" + regObj.BranchCode.ToString().ToUpper() + "'AND RECEIPT_TYPE = 'REP' AND SL_REP_TR_RN_NO IS NULL  ORDER BY RECEIPT_NO DESC ");
            moneyReceipDropDownList.DataTextField = "RECEIPT_NO";
            moneyReceipDropDownList.DataValueField = "ID";
            moneyReceipDropDownList.DataBind();
        }
    
    }    
    private void ClearText()
    {
        moneyReceipDropDownList.SelectedValue = "0";
        RepRateTextBox.Text = "";
        RepDateTextBox.Text = "";
        TotalUnitRepurchaseTextBox.Text = "";
        RegNoTextBox.Text = "";
        HolderJNameTextBox.Text = "";
        HolderNameTextBox.Text = "";
        payTypeLabel.Text = "";
        QtyTextBox.Text = "";

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

        unitRepObj.RepurchaseNo = Convert.ToInt32(moneyReceipDropDownList.SelectedItem.Text.ToString());
        unitRepObj.RepurchaseRate = decimal.Parse(RepRateTextBox.Text.Trim().ToString());
        unitRepObj.RepurchaseDate = RepDateTextBox.Text.Trim().ToString();
        unitRepObj.PayType = payTypeLabel.Text.ToUpper();

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
                    long totalSurrenderUnits = 0;
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
                            totalSurrenderUnits = totalSurrenderUnits + Convert.ToInt64(SURRENDER_UNITSTxt.Text.ToString());



                        }
                    }
                    if (dtGrid.Rows.Count > 0)
                    {
                        if (totalSurrenderUnits == Convert.ToInt64(TotalUnitRepurchaseTextBox.Text) && totalSurrenderUnits == Convert.ToInt64(QtyTextBox.Text))
                        {
                            commonGatewayObj.ExecuteNonQuery("UPDATE MONEY_RECEIPT SET SL_REP_TR_RN_NO=" + unitRepObj.RepurchaseNo + "  WHERE ID=" + Convert.ToUInt64(moneyReceipDropDownList.SelectedValue.ToString()));
                            unitRepBLObj.saveRepurchaseCDS(dtGrid, regObj, unitRepObj, userObj);//save Repurchase Data
                            ClearText();

                            moneyReceipDropDownList.DataSource = unitSaleBLObj.dtMoneyRecieptforDDL(" AND REG_BK = '" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR = '" + regObj.BranchCode.ToString().ToUpper() + "'AND RECEIPT_TYPE = 'REP' AND SL_REP_TR_RN_NO IS NULL  ORDER BY RECEIPT_NO DESC ");
                            moneyReceipDropDownList.DataTextField = "RECEIPT_NO";
                            moneyReceipDropDownList.DataValueField = "ID";
                            moneyReceipDropDownList.DataBind();

                            leftDataGrid.DataSource = opendMFDAO.getTableDataGridCDS();// hide remaining Data
                            leftDataGrid.DataBind();
                            TotalUnitHoldingTextBox.Text = "";

                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save SuccessFully');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed : Total Selected Units and Add Total Units is not equal');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed Due to No Sale Selected');", true);
                    }
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
    protected void moneyReceipDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {



        DataTable dtMoneyReceitInfoDetails = unitSaleBLObj.dtMoneyRecieptInfoDetails(Convert.ToInt64(moneyReceipDropDownList.SelectedValue.ToString()));
        if (dtMoneyReceitInfoDetails.Rows.Count > 0)
        {
          
            regObj.FundCode = FundCodeTextBox.Text.ToString();
            regObj.BranchCode = BranchCodeTextBox.Text.ToString();
            regObj.RegNumber = dtMoneyReceitInfoDetails.Rows[0]["REG_NO"].ToString();
            RegNoTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["REG_NO"].ToString();
            QtyTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["UNIT_QTY"].ToString();
            RepDateTextBox.Text = Convert.ToDateTime(dtMoneyReceitInfoDetails.Rows[0]["RECEIPT_DATE"].ToString()).ToString("dd-MMM-yyyy");
            RepRateTextBox.Text = dtMoneyReceitInfoDetails.Rows[0]["RATE"].ToString();
            if (!dtMoneyReceitInfoDetails.Rows[0]["REP_PAY_TYPE"].Equals(DBNull.Value))
            {
                payTypeLabel.Text = dtMoneyReceitInfoDetails.Rows[0]["REP_PAY_TYPE"].ToString().ToUpper();

            }
            DataTable dtRegInfo = opendMFDAO.getDtRegInfo(regObj);
            DataTable dtTotalSaleUnitCerts = opendMFDAO.getDtTotalSaleUnitCertsCDS(regObj);
            decimal TotalUnitsBalance = opendMFDAO.getTotalSaleUnitBalanceCDS(regObj);

            if (dtRegInfo.Rows.Count > 0)
            {
                HolderNameTextBox.Text = dtRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["HNAME"].ToString();
                HolderJNameTextBox.Text = dtRegInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["JNT_NAME"].ToString();
                string[] BranchCodeSign = regObj.BranchCode.Split('/');
                string imageSignLocation = Path.Combine(ConfigReader.SingLocation + "\\" + regObj.FundCode, regObj.FundCode + "_" + BranchCodeSign[0] + "_" + BranchCodeSign[1] + "_" + regObj.RegNumber + ".jpg");//"../../Image/IAMCL/Sign/"+ fundCode + "_" + branchCode + "_" + regNo + ".jpg";
                string imagePhotoLocation = Path.Combine(ConfigReader.PhotoLocation + "\\" + regObj.FundCode, regObj.FundCode + "_" + "_" + BranchCodeSign[0] + BranchCodeSign[1] + "_" + regObj.RegNumber + ".jpg");

                if (File.Exists(Path.Combine(ConfigReader.SingLocation + "\\" + regObj.FundCode, regObj.FundCode + "_" + BranchCodeSign[0] + "_" + BranchCodeSign[1] + "_" + regObj.RegNumber + ".jpg")))
                {
                    SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(imageSignLocation.ToString());
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

            }
            else
            {
                ClearText();
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
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
        }
    }
}
