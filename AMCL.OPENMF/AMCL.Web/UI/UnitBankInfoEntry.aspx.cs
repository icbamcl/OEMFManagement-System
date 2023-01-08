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
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitBankInfoEntry : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    BaseClass bcContent = new BaseClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
       
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
            
        
       
       // holderDateofBirthTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        if (!IsPostBack)
        {
            DataTable dtOccupationList = opendMFDAO.dtOccopationList();
            holderOccupationDropDownList.DataSource = dtOccupationList;
            holderOccupationDropDownList.DataTextField= "DESCR";
            holderOccupationDropDownList.DataValueField = "CODE";
            holderOccupationDropDownList.DataBind();
            

           //jHolderOccupationDropDownList.DataSource= dtOccupationList;
           //jHolderOccupationDropDownList.DataTextField = "DESCR";
           //jHolderOccupationDropDownList.DataValueField = "CODE";
           //jHolderOccupationDropDownList.DataBind();


           //nomi1OccupationDropDownList.DataSource = dtOccupationList;
           //nomi1OccupationDropDownList.DataTextField = "DESCR";
           //nomi1OccupationDropDownList.DataValueField = "CODE";
           //nomi1OccupationDropDownList.DataBind();


           //nomi2OccupationDropDownList.DataSource = dtOccupationList;
           //nomi2OccupationDropDownList.DataTextField = "DESCR";
           //nomi2OccupationDropDownList.DataValueField = "CODE";
           //nomi2OccupationDropDownList.DataBind();

           //bankNameDropDownList.DataSource = opendMFDAO.dtFillBankName(" CATE_CODE=1 ");
           //bankNameDropDownList.DataTextField = "BANK_NAME";
           //bankNameDropDownList.DataValueField = "BANK_CODE";
           //bankNameDropDownList.DataBind();

           IDbankNameDropDownList.DataSource = opendMFDAO.dtFillBankName(" CATE_CODE=2 ");
           IDbankNameDropDownList.DataTextField = "BANK_NAME";
           IDbankNameDropDownList.DataValueField = "BANK_CODE";
           IDbankNameDropDownList.DataBind();

           regTypeDropDownList.DataSource = unitHolderRegBLObj.dtFillRegType();
           regTypeDropDownList.DataTextField = "NAME";
           regTypeDropDownList.DataValueField = "CODE";
           regTypeDropDownList.DataBind();
           regTypeDropDownList.SelectedValue = "N";

           regObj.FundCode = fundCode.ToString();
           regObj.BranchCode = branchCode.ToString();
           
           regNoTextBox.Text = unitHolderRegBLObj.GetMaxRegNo(regObj).ToString();
           regObj.RegNumber = (unitHolderRegBLObj.GetMaxRegNo(regObj)-1).ToString();
           regDateTextBox.Text = unitHolderRegBLObj.getLastRegDate(regObj).ToString("dd-MMM-yyyy");
           regNoTextBox.Focus();
        }
        //isIDDropDownList.Attributes.Add("onchage", "HideTextBox();");
    
    }
    
    protected void regSaveButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeTextBox.Text.Trim();
        regObj.BranchCode = branchCodeTextBox.Text.Trim();
        regObj.RegDate = Convert.ToDateTime(regDateTextBox.Text.Trim().ToString());
        regObj.RegNumber = regNoTextBox.Text.Trim();
        regObj.RegType = regTypeDropDownList.SelectedValue;      
        regObj.RegIsCIP = isCIPDropDownList.SelectedValue;     
        //regObj.IsIDAccount = isIDDropDownList.SelectedValue;
        //if (isIDDropDownList.SelectedValue.ToString().ToUpper() == "Y")
        //{            
        //    regObj.IdBankID =Convert.ToInt16( IDbankNameDropDownList.SelectedValue.ToString());
        //    regObj.IdBankBranchID = Convert.ToInt16(IDbranchNameDropDownList.SelectedValue.ToString());
        //}
        //regObj.IdAccNo = IDAccNoTextBox.Text.Trim().ToString();

        UnitHolderInfo unitHolderObj = new UnitHolderInfo();
        unitHolderObj.HolderName = holderNameTextBox.Text.Trim();
        unitHolderObj.HolderFMHName = holderFMTextBox.Text.Trim();
        unitHolderObj.HolderMotherName = holderMotherTextBox.Text.Trim();
        unitHolderObj.HolderAddress1 = holderAddress1TextBox.Text.Trim();
        unitHolderObj.HolderAddress2 = holderAddress2TextBox.Text.Trim();
        unitHolderObj.HolderOccupation = Convert.ToInt16(holderOccupationDropDownList.SelectedValue);
        unitHolderObj.HolderNationality = holderNationalityTextBox.Text.Trim();
        unitHolderObj.HolderTelephone = holderTelphoneTextBox.Text.Trim();
        unitHolderObj.HolderEmail = holderEmailTextBox.Text.Trim();
        unitHolderObj.HolderMaritialStatus = holderMaritialStatusDropDownList.SelectedValue;
        unitHolderObj.HolderEduQua = holderEducationDropDownList.SelectedValue;
        unitHolderObj.HolderDateofBirth = holderDateofBirthTextBox.Text.Trim().ToString();
        unitHolderObj.HolderCity = holderCityTextBox.Text.Trim();
        unitHolderObj.HolderSex = holderSexDropDownList.SelectedValue;
        unitHolderObj.HolderReligion = holderReligionDropDownList.SelectedValue;
        unitHolderObj.HolderRemarks = holderRemarksTextBox.Text.Trim();

        //UnitJointHolderInfo jHolderObj = new UnitJointHolderInfo();
        //jHolderObj.JHolderName = jHolderNameTextBox.Text.Trim();
        //jHolderObj.JHolderFMHName = jHolderFMTextBox.Text.Trim();
        //jHolderObj.JHolderMotherName = jHolderMotherTextBox.Text.Trim().ToString().ToUpper();
        //jHolderObj.JHolderAddress1 = jHolderAddress1TextBox.Text.Trim();
        //jHolderObj.JHolderAddress2 = jHolderAddress2TextBox.Text.Trim();
        //jHolderObj.JHolderNationality = jHolderNantionalityTextBox.Text.Trim();
        //jHolderObj.JHolderOccupation = Convert.ToInt16(jHolderOccupationDropDownList.SelectedValue);

        //UnitHolderNominee nomiObj = new UnitHolderNominee();
        //nomiObj.NomiControlNo = NomiControlNoTextBox.Text.Trim().ToString();
        //nomiObj.Nomi1Name = nomi1NameTextBox.Text.Trim();
        //nomiObj.Nomi1FMName = nomi1FMTextBox.Text.Trim();
        //nomiObj.Nomi1MotherName = nomi1MotherNameTextBox.Text.Trim().ToUpper().ToString();
        //nomiObj.Nomi1Address1 = nomi1Address1TextBox.Text.Trim();
        //nomiObj.Nomi1Address2 = nomi1Address2TextBox.Text.Trim();
        //nomiObj.Nomi1Occupation =Convert.ToInt16( nomi1OccupationDropDownList.SelectedValue);
        //nomiObj.Nomi1Nationality = nomi1NationalityTextBox.Text.Trim();
        //nomiObj.Nomi1Relation = nomi1RelationDropDownList.SelectedValue;
        //nomiObj.Nomi1Percentage = nomi1PtcTextBox.Text.Trim().ToString();

        //nomiObj.Nomi2Name = nomi2NameTextBox.Text.Trim();
        //nomiObj.Nomi2FMName = nomi2FMTextBox.Text.Trim();
        //nomiObj.Nomi2MotherName = nomi2MotherNameTextBox.Text.Trim().ToString().ToUpper();
        //nomiObj.Nomi2Address1 = nomi2Address1TextBox.Text.Trim();
        //nomiObj.Nomi2Address2 = nomi2Address2TextBox.Text.Trim();
        //nomiObj.Nomi2Occupation = Convert.ToInt16(nomi2OccupationDropDownList.SelectedValue);
        //nomiObj.Nomi2Nationality = nomi2NationalityTextBox.Text.Trim();
        //nomiObj.Nomi2Relation = nomi2RelationDropDownList.SelectedValue;
        //nomiObj.Nomi2Percentage = nomi2PtcTextBox.Text.Trim().ToString();

        //UnitHolderBankInfo bankInfoObj = new UnitHolderBankInfo();
        //if (bftnNoRadioButton.Checked)
        //{
        //    bankInfoObj.IsBEFTN = "N";
        //}
        //else if (bfnYesRadioButton.Checked)
        //{
        //    bankInfoObj.IsBEFTN = "Y";
        //}
        //bankInfoObj.IsBankInfo = isBankDropDownList.SelectedValue;
        //if (isBankDropDownList.SelectedValue.ToString().ToUpper() == "Y")
        //{           
        //    bankInfoObj.BankAccountNo = bankAccTextBox.Text.Trim();
        //    //bankInfoObj.BankName = bankNameDropDownList.SelectedItem.Text.Trim().ToString();
        //    //bankInfoObj.BranchName = branchNameDropDownList.SelectedItem.Text.Trim().ToString();
        //    //bankInfoObj.BankAddress = bankAddressTextBox.Text.Trim();
        //    bankInfoObj.BankCode = bankNameDropDownList.SelectedValue.ToString();
        //    bankInfoObj.BankBranchCode = branchNameDropDownList.SelectedValue.ToString();
           
        //}

        try
        {
            if (unitHolderRegBLObj.IsDuplicateReg(regObj))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " "+"Registration Number "+"');", true);
            }
            else if (unitHolderRegBLObj.CheckSanctionList(regObj,unitHolderObj) != "")
            {
                string errorMessage = unitHolderRegBLObj.CheckSanctionList(regObj,unitHolderObj);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed :"+errorMessage+"');", true);
            
            }
           
            else
            {
          
                //if (unitHolderRegBLObj.IsDuplicateNomineeControl(regObj,nomiObj))
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " " + "Nominee Control Number " + "');", true);
                //}               
                //else
                //{
                //    unitHolderRegBLObj.SaveRegInfo(regObj, unitHolderObj, jHolderObj, nomiObj, bankInfoObj, userObj);
                //    ClearText();
                //    // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert(" msgObj.Success + ");", true);
                //    regNoTextBox.Text = unitHolderRegBLObj.GetMaxRegNo(regObj).ToString();
                //    regObj.RegNumber = (unitHolderRegBLObj.GetMaxRegNo(regObj) - 1).ToString();
                //    regDateTextBox.Text = unitHolderRegBLObj.getLastRegDate(regObj).ToString("dd-MMM-yyyy");
                //    regNoTextBox.Focus();
                //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Success().ToString() + "');", true);

                //    //ClientScript.RegisterStartupScript(this.GetType(), "Reset Fields", "fnReset();", true);
                //}
            }

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('"+msgObj.Error().ToString() + " " + ex.Message.Replace("'","").ToString() + "');", true);
        }


    }
    private void ClearText()
    {
        //regDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        regNoTextBox.Text="";
        regTypeDropDownList.SelectedValue = "N";
        isCIPDropDownList.SelectedValue="0";
        //isIDDropDownList.SelectedValue = "N";
        IDAccNoTextBox.Text = "";
        
        IDbankNameDropDownList.SelectedValue = "0";
        IDbranchNameDropDownList.SelectedValue = "0";
        IDbankNameDropDownList.Enabled = false;
        IDbranchNameDropDownList.Enabled = false;
        //IDAccNoTextBox.Enabled = false;

        holderNameTextBox.Text="";
        holderFMTextBox.Text="";
        holderMotherTextBox.Text = "";
        holderAddress1TextBox.Text="";
        holderAddress2TextBox.Text="";
        holderOccupationDropDownList.SelectedValue="0";
        holderNationalityTextBox.Text="BANGLADESHI";
        holderTelphoneTextBox.Text="";
        holderEmailTextBox.Text="";
        holderMaritialStatusDropDownList.SelectedValue="0";
        holderEducationDropDownList.SelectedValue="0";
        holderDateofBirthTextBox.Text = "";
        holderCityTextBox.Text="";
        holderSexDropDownList.SelectedValue="0";
        holderReligionDropDownList.SelectedValue="0";
        holderRemarksTextBox.Text="";

        //jHolderNameTextBox.Text="";
        //jHolderFMTextBox.Text="";
        //jHolderMotherTextBox.Text = "";
        //jHolderAddress1TextBox.Text="";
        //jHolderAddress2TextBox.Text="";
        //jHolderNantionalityTextBox.Text="";
        //jHolderOccupationDropDownList.SelectedValue="0";

        //NomiControlNoTextBox.Text = "";
        //nomi1NameTextBox.Text="";
        //nomi1FMTextBox.Text = "";
        //nomi1MotherNameTextBox.Text = "";
        //nomi1Address1TextBox.Text="";
        //nomi1Address2TextBox.Text="";
        //nomi1OccupationDropDownList.SelectedValue="0";
        //nomi1NationalityTextBox.Text="";
        //nomi1RelationDropDownList.SelectedValue="0";
        //nomi1PtcTextBox.Text = "";

        //nomi2NameTextBox.Text="";
        //nomi2FMTextBox.Text = "";
        //nomi2MotherNameTextBox.Text = "";
        //nomi2Address1TextBox.Text="";
        //nomi2Address2TextBox.Text="";
        //nomi2OccupationDropDownList.SelectedValue="0";
        //nomi2NationalityTextBox.Text="";
        //nomi2RelationDropDownList.SelectedValue="0";
        //nomi2PtcTextBox.Text = "";

        //isBankDropDownList.SelectedValue="N";
        //bankAccTextBox.Text = "";
        //bankNameDropDownList.SelectedValue = "0";
        //branchNameDropDownList.SelectedValue = "0";
        //bankAddressTextBox.Text="";
        //bankAccTextBox.Enabled = false;
        //bankNameDropDownList.Enabled = false;
        //branchNameDropDownList.Enabled = false;
        //bankAddressTextBox.Enabled = false;
        //bftnNoRadioButton.Checked = true;
        //bfnYesRadioButton.Checked = false;
        
    }
    protected void regCloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }      
    protected void regNoTextBox_TextChanged(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeTextBox.Text.Trim();
        regObj.BranchCode = branchCodeTextBox.Text.Trim();        
        regObj.RegNumber = regNoTextBox.Text.Trim();
        if (unitHolderRegBLObj.IsDuplicateReg(regObj))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Enter Valid Registration Number');", true);
            regNoTextBox.Focus();
        }
        else
        {
          
            regDateTextBox.Focus();
           
        }
    }
    //protected void isIDDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (isIDDropDownList.SelectedValue.ToString().ToUpper() == "Y")
    //    {
    //        IDAccNoTextBox.Enabled = true;
    //        IDAccNoTextBox.Focus();
    //        IDbankNameDropDownList.Enabled = true;
    //        IDbranchNameDropDownList.Enabled = true;
    //        IDbankNameDropDownList.SelectedValue = "0";
    //        IDbranchNameDropDownList.SelectedValue = "0";
            
    //    }
    //    else if (isIDDropDownList.SelectedValue.ToString().ToUpper() == "N")
    //    {
    //        IDAccNoTextBox.Text = "";
    //        IDAccNoTextBox.Enabled = false;
    //        IDbankNameDropDownList.SelectedValue = "0";
    //        IDbranchNameDropDownList.SelectedValue = "0";
    //        IDbankNameDropDownList.Enabled = false;
    //        IDbranchNameDropDownList.Enabled = false;
           

    //    }
    //}
    protected void IDbankNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDbranchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(IDbankNameDropDownList.SelectedValue.ToString()));
        IDbranchNameDropDownList.DataTextField = "BRANCH_NAME";
        IDbranchNameDropDownList.DataValueField = "BRANCH_CODE";
        IDbranchNameDropDownList.DataBind();
    }
   
   
}
