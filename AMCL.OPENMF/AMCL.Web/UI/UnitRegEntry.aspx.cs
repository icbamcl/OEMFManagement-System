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

public partial class UI_UnitRegEntry : System.Web.UI.Page
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
            
        
       
      
        if (!IsPostBack)
        {
            DataTable dtOccupationList = opendMFDAO.dtOccopationList();
            OccupationDropDownList.DataSource = dtOccupationList;
            OccupationDropDownList.DataTextField= "DESCR";
            OccupationDropDownList.DataValueField = "CODE";
            OccupationDropDownList.DataBind();


            bankNameDropDownList.DataSource = opendMFDAO.dtFillBankName(" CATE_CODE=1 ");
            bankNameDropDownList.DataTextField = "BANK_NAME";
            bankNameDropDownList.DataValueField = "BANK_CODE";
            bankNameDropDownList.DataBind();

           IDbankNameDropDownList.DataSource = opendMFDAO.dtFillBankName(" CATE_CODE IN (1,2,3) ");
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
       
    
    }
    
    protected void regSaveButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeTextBox.Text.Trim();
        regObj.BranchCode = branchCodeTextBox.Text.Trim();
        regObj.RegDate = Convert.ToDateTime(regDateTextBox.Text.Trim().ToString());
        regObj.RegNumber = regNoTextBox.Text.Trim();
        if (IndividualButton.Checked)
        {
            regObj.RegType = regTypeDropDownList.SelectedValue;
        }
        else if (CompRadioButton.Checked)
        {
            regObj.RegType = regTypeDropDownList.SelectedValue;
        }
        regObj.RegIsCIP = isCIPDropDownList.SelectedValue;   

        if(IDNoRadioButton.Checked)
        {
            regObj.IsIDAccount = "N";
        }
        else if (IDYesRadioButton.Checked)
        {
            regObj.IsIDAccount = "Y";
            regObj.IdBankID = Convert.ToInt16(IDbankNameDropDownList.SelectedValue.ToString());
            regObj.IdBankBranchID = Convert.ToInt16(IDbranchNameDropDownList.SelectedValue.ToString());
        }
        regObj.IdAccNo = IDAccNoTextBox.Text.Trim().ToString();

        UnitHolderInfo unitHolderObj = new UnitHolderInfo();
        unitHolderObj.HolderName = NameTextBox.Text.Trim();
        unitHolderObj.HolderFMHName = FMTextBox.Text.Trim();
        unitHolderObj.HolderMotherName = MotherTextBox.Text.Trim();
        unitHolderObj.HolderSpouceName = spouceTextBox.Text.Trim();       
        unitHolderObj.HolderBONumber = holderBONumberTextBox.Text.Trim();        
        unitHolderObj.HolderOccupation = Convert.ToInt16(OccupationDropDownList.SelectedValue);
        unitHolderObj.HolderNationality = NationalityTextBox.Text.Trim();
        unitHolderObj.HolderBONumber = holderBONumberTextBox.Text.Trim();

        unitHolderObj.HolderNID = NIDTextBox.Text.Trim();
        unitHolderObj.HolderTIN = TINTextBox.Text.Trim();
        if (TINTextBox.Text.Trim() != "")
        {
            if (TINTextBox.Text.Trim().Length == 12)
            {
                unitHolderObj.HolderTINFlag = "Y";
            }
            else
            {
                unitHolderObj.HolderTINFlag = "N";
            }
        }
        else
        {
            unitHolderObj.HolderTINFlag = "N";
        }
        unitHolderObj.HolderPassport = passportTextBox.Text.Trim();
        unitHolderObj.HolderBirthCertNo = birthCertNoTextBox.Text.Trim();

        unitHolderObj.HolderAddress1 = presentAddress1TextBox.Text.Trim();
        unitHolderObj.HolderAddress2 = presentAddress2TextBox.Text.Trim();
        unitHolderObj.HolderCity = presentCityTextBox.Text.Trim();

        unitHolderObj.HolderPermanAddress1 = parmanAddress1TextBox.Text.Trim();
        unitHolderObj.HolderPermanAddress2 = parmanAddress2TextBox.Text.Trim();
        unitHolderObj.HolderPermanCity = parmentCityTextBox.Text.Trim();



        unitHolderObj.HolderMobile = MobileTextBox.Text.Trim();
        unitHolderObj.HolderTelephone =TelphoneTextBox.Text.Trim();
        unitHolderObj.HolderEmail =EmailTextBox.Text.Trim();
        unitHolderObj.HolderMaritialStatus = MaritialStatusDropDownList.SelectedValue;
        unitHolderObj.HolderEduQua = EducationDropDownList.SelectedValue;
        unitHolderObj.HolderDateofBirth = DateofBirthTextBox.Text.Trim().ToString();
        
        unitHolderObj.HolderSex = SexDropDownList.SelectedValue;
        unitHolderObj.HolderReligion =ReligionDropDownList.SelectedValue;
        unitHolderObj.HolderSourceFund = SourceFundTextBox.Text.Trim();
        unitHolderObj.HolderRemarks = RemarksTextBox.Text.Trim();
        unitHolderObj.HolderKYC = unitHolderRegBLObj.kycStatus(unitHolderObj);

        UnitHolderBankInfo bankInfoObj = new UnitHolderBankInfo();
        if (BankYesRadioButton.Checked)
        {
            bankInfoObj.IsBankInfo = "Y";
            if (bftnNoRadioButton.Checked)
            {
                bankInfoObj.IsBEFTN = "N";
            }
            else
            {
                bankInfoObj.IsBEFTN = "Y";
            }
            bankInfoObj.BankAccountNo = bankAccTextBox.Text.Trim().ToString();
            bankInfoObj.BankCode = bankNameDropDownList.SelectedValue.ToString();
            bankInfoObj.BankBranchCode=branchNameDropDownList.SelectedValue.ToString();
        }
        else if(BankNORadioButton.Checked)
        {
            bankInfoObj.IsBankInfo = "N";
            bankInfoObj.IsBEFTN = "N";
        }
        try
        {
            if (unitHolderRegBLObj.IsDuplicateReg(regObj))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " "+"Registration Number "+"');", true);
            }
            else if (unitHolderRegBLObj.CheckSanctionList(regObj,unitHolderObj) != "")
            {
                string errorMessage = unitHolderRegBLObj.CheckSanctionList(regObj,unitHolderObj);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "confirm('Save Failed :"+errorMessage+"');", true);
            
            }
            else if (unitHolderRegBLObj.IsEditPermit(regObj.FundCode.ToString()))
            {
                string errorMessage = " Please Contact with System Administrator ";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed :" + errorMessage + "');", true);

            }

            else
            {

             
                   unitHolderRegBLObj.SaveRegInfo(regObj, unitHolderObj,bankInfoObj, userObj);
                   ClearText();
                   
                    regNoTextBox.Text = unitHolderRegBLObj.GetMaxRegNo(regObj).ToString();
                    regObj.RegNumber = (unitHolderRegBLObj.GetMaxRegNo(regObj) - 1).ToString();
                    regDateTextBox.Text = unitHolderRegBLObj.getLastRegDate(regObj).ToString("dd-MMM-yyyy");
                    regNoTextBox.Focus();
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Success().ToString() + "');", true);

                    //ClientScript.RegisterStartupScript(this.GetType(), "Reset Fields", "fnReset();", true);
                
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
        //regNoTextBox.Text="";
        regTypeDropDownList.SelectedValue = "N";
        isCIPDropDownList.SelectedValue="0";       
        IDAccNoTextBox.Text = "";
        
        IDbankNameDropDownList.SelectedValue = "0";
        IDbranchNameDropDownList.SelectedValue = "0";
       
        IDNoRadioButton.Checked = true;
        IDYesRadioButton.Checked = false;
        BankNORadioButton.Checked = true;
        BankYesRadioButton.Checked = false;
        bftnNoRadioButton.Checked = true;
        bftnYesRadioButton.Checked = false;
        IndividualButton.Checked = true;
        CompRadioButton.Checked = false;
        //NIDRadioButton.Checked = true;
        NIDTextBox.Text = "";
        passportTextBox.Text = "";
        TINTextBox.Text = "";
        birthCertNoTextBox.Text = "";

       
        dvIDInof.Attributes.Add("style", "visibility:hidden");
        divBEFTN.Attributes.Add("style", "visibility:hidden");
        DivBank.Attributes.Add("style", "visibility:hidden");
        
        NameTextBox.Text="";
        FMTextBox.Text="";
        MotherTextBox.Text = "";
        spouceTextBox.Text = "";

        presentAddress1TextBox.Text = "";
        presentAddress2TextBox.Text = "";
        presentCityTextBox.Text = "";

        parmanAddress1TextBox.Text = "";
        parmanAddress2TextBox.Text = "";
        parmentCityTextBox.Text = "";

        OccupationDropDownList.SelectedValue="0";
        NationalityTextBox.Text="BANGLADESHI";
        TelphoneTextBox.Text="";
        MobileTextBox.Text = "";
        EmailTextBox.Text="";
        SourceFundTextBox.Text = "";
        webaddressTextBox.Text = "";
        MaritialStatusDropDownList.SelectedValue="0";
        EducationDropDownList.SelectedValue="0";
        DateofBirthTextBox.Text = "";
        
        SexDropDownList.SelectedValue="0";
        ReligionDropDownList.SelectedValue="0";
        RemarksTextBox.Text="";

        parmenentCheckBox.Checked = false;
       

     
        
    }
    protected void regCloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }      
    
  
    protected void IDbankNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvIDInof.Style.Add("visibility", "visible");
        IDbranchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(IDbankNameDropDownList.SelectedValue.ToString()));
        IDbranchNameDropDownList.DataTextField = "BRANCH_NAME";
        IDbranchNameDropDownList.DataValueField = "BRANCH_CODE";
        IDbranchNameDropDownList.DataBind();
    }


    protected void bankNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DivBank.Style.Add("visibility", "visible");
        divBEFTN.Style.Add("visibility", "visible");
        branchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(bankNameDropDownList.SelectedValue.ToString()));
        branchNameDropDownList.DataTextField = "BRANCH_NAME";
        branchNameDropDownList.DataValueField = "BRANCH_CODE";
        branchNameDropDownList.DataBind();
    }
    protected void findButton_Click(object sender, EventArgs e)
    {
        DivBank.Style.Add("visibility", "visible");
        divBEFTN.Style.Add("visibility", "visible");
        DataTable dtBankBracnhInfo = unitHolderRegBLObj.dtGetBankBracnhInfo(routingNoTextBox.Text.Trim().ToString());
        if (dtBankBracnhInfo.Rows.Count > 0)
        {
            //routingNoTextBox.Text = dtBankBracnhInfo.Rows[0]["ROUTING_NO"].ToString();

            bankNameDropDownList.DataSource = opendMFDAO.dtFillBankName(" CATE_CODE=1 ");
            bankNameDropDownList.DataTextField = "BANK_NAME";
            bankNameDropDownList.DataValueField = "BANK_CODE";
            bankNameDropDownList.DataBind();

            branchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(dtBankBracnhInfo.Rows[0]["BANK_CODE"].ToString()));
            branchNameDropDownList.DataTextField = "BRANCH_NAME";
            branchNameDropDownList.DataValueField = "BRANCH_CODE";
            branchNameDropDownList.DataBind();

            bankNameDropDownList.SelectedValue = dtBankBracnhInfo.Rows[0]["BANK_CODE"].ToString();
            branchNameDropDownList.SelectedValue = dtBankBracnhInfo.Rows[0]["BRANCH_CODE"].ToString();
            bankAddressTextBox.Text = dtBankBracnhInfo.Rows[0]["ADDRESS"].ToString() + " ";

        }
        else
        {
            bankNameDropDownList.SelectedValue = "0";
            branchNameDropDownList.SelectedValue = "0";           
            bankAddressTextBox.Text = "";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Bank Information Found');", true);
        }
    }
    protected void branchNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtBankBracnhInfo = unitHolderRegBLObj.dtGetBankBracnhInfo(Convert.ToInt32(bankNameDropDownList.SelectedValue.ToString()), Convert.ToInt32(branchNameDropDownList.SelectedValue.ToString()));
        if (dtBankBracnhInfo.Rows.Count > 0)
        {
            routingNoTextBox.Text = dtBankBracnhInfo.Rows[0]["ROUTING_NO"].ToString();
            bankAddressTextBox.Text = dtBankBracnhInfo.Rows[0]["ADDRESS"].ToString() + " ";

        }
        else
        {
            routingNoTextBox.Text = "";
            bankAddressTextBox.Text = "";
        }
    }
}
