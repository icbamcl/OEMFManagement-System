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

public partial class UI_UnitJointEntry : System.Web.UI.Page
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
        
        }
       
    
    }
    
    protected void regSaveButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeTextBox.Text.Trim();
        regObj.BranchCode = branchCodeTextBox.Text.Trim();
      
        regObj.RegNumber = regNoTextBox.Text.Trim();


            UnitHolderInfo unitHolderObj = new UnitHolderInfo();
            unitHolderObj.HolderName = NameTextBox.Text.Trim();
            unitHolderObj.HolderFMHName = FMTextBox.Text.Trim();
            unitHolderObj.HolderMotherName = MotherTextBox.Text.Trim();
            unitHolderObj.HolderSpouceName = spouceTextBox.Text.Trim();

            unitHolderObj.HolderOccupation = Convert.ToInt16(OccupationDropDownList.SelectedValue);
            unitHolderObj.HolderNationality = NationalityTextBox.Text.Trim();

            unitHolderObj.HolderNID = NIDTextBox.Text.Trim();
            unitHolderObj.HolderTIN = TINTextBox.Text.Trim();
            if (TINTextBox.Text.Trim() != "")
            {
                unitHolderObj.HolderTINFlag = "Y";
            }
            else
            {
                unitHolderObj.HolderTINFlag = "";
            }
            unitHolderObj.HolderPassport = passportTextBox.Text.Trim();
            unitHolderObj.HolderBirthCertNo = birthCertNoTextBox.Text.Trim();

            unitHolderObj.HolderAddress1 = presentAddress1TextBox.Text.Trim();
            unitHolderObj.HolderAddress2 = presentAddress2TextBox.Text.Trim();
            unitHolderObj.HolderCity = presentCityTextBox.Text.Trim();

            unitHolderObj.HolderPermanAddress1 = parmanAddress1TextBox.Text.Trim();
            unitHolderObj.HolderPermanAddress2 = parmanAddress2TextBox.Text.Trim();
            unitHolderObj.HolderPermanCity = parmentCityTextBox.Text.Trim();



            
            unitHolderObj.HolderMobile =TelphoneTextBox.Text.Trim();
            unitHolderObj.HolderEmail =EmailTextBox.Text.Trim();
            unitHolderObj.HolderMaritialStatus = MaritialStatusDropDownList.SelectedValue;
            unitHolderObj.HolderEduQua = EducationDropDownList.SelectedValue;
            unitHolderObj.HolderDateofBirth = DateofBirthTextBox.Text.Trim().ToString();
            
            unitHolderObj.HolderSex = SexDropDownList.SelectedValue;
            unitHolderObj.HolderReligion =ReligionDropDownList.SelectedValue;
            unitHolderObj.HolderSourceFund = SourceFundTextBox.Text.Trim();
            unitHolderObj.HolderRemarks = RemarksTextBox.Text.Trim();
            unitHolderObj.HolderKYC = unitHolderRegBLObj.kycStatus(unitHolderObj);
      

        try
        {
            if (unitHolderRegBLObj.IsDuplicateJointHolder(regObj))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert(' Save Failed: One Join Holder Already Entried');", true);
            }
            //else if (unitHolderRegBLObj.CheckSanctionList(regObj, unitHolderObj) != "")
            //{
            //    string errorMessage = unitHolderRegBLObj.CheckSanctionList(regObj, unitHolderObj);
            //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed :" + errorMessage + "');", true);

            //}
            else if (regObj.FundCode.ToString().ToUpper()=="IAMPH") //for ICB AMCL Pension Holder's Unit Fund Join Holder is not allowed
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert(' Save Failed: In case of Pension Fund Join Holder is not allowed');", true);
            }
                     
            else
            {


                  unitHolderRegBLObj.SaveJointHolderInfo(regObj, unitHolderObj, userObj);
                    ClearText();                                      
                    regNoTextBox.Focus();
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Success().ToString() + "');", true);

                   
                
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

        DateLabel.Text = "";
        TypeLabel.Text = "";
        CIPLabel.Text = "";
        IDLabel.Text = "";
        NIDTextBox.Text = "";
        passportTextBox.Text = "";
        TINTextBox.Text = "";
        birthCertNoTextBox.Text = "";


        
        NameTextBox.Text = "";
        FMTextBox.Text = "";
        MotherTextBox.Text = "";
        spouceTextBox.Text = "";

        presentAddress1TextBox.Text = "";
        presentAddress2TextBox.Text = "";
        presentCityTextBox.Text = "";

        parmanAddress1TextBox.Text = "";
        parmanAddress2TextBox.Text = "";
        parmentCityTextBox.Text = "";

        OccupationDropDownList.SelectedValue = "0";
        NationalityTextBox.Text = "BANGLADESHI";
        TelphoneTextBox.Text = "";
        EmailTextBox.Text = "";
        SourceFundTextBox.Text = "";
        webaddressTextBox.Text = "";
        MaritialStatusDropDownList.SelectedValue = "0";
        EducationDropDownList.SelectedValue = "0";
        DateofBirthTextBox.Text = "";

        SexDropDownList.SelectedValue = "0";
        ReligionDropDownList.SelectedValue = "0";
        RemarksTextBox.Text = "";

        parmenentCheckBox.Checked = false;

     
        
    }
    protected void regCloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }      
    
  
  


 
    protected void findButton_Click(object sender, EventArgs e)
    {
        FindRegInfo();
    }
    private void FindRegInfo()
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeTextBox.Text;
        regObj.BranchCode = branchCodeTextBox.Text;
        regObj.RegNumber = regNoTextBox.Text;
        if (opendMFDAO.IsValidRegistration(regObj))
        {
            DataTable dtHolderRegInfo = opendMFDAO.HolderRegInfo(regObj);

            if (dtHolderRegInfo.Rows.Count > 0)
            {


                NameLabel.Text = dtHolderRegInfo.Rows[0]["HNAME"].ToString();
                DateLabel.Text = dtHolderRegInfo.Rows[0]["REG_DT"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtHolderRegInfo.Rows[0]["REG_DT"].ToString()).ToString("dd-MMM-yyyy");
                TypeLabel.Text = dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("N") ? "INDIVIDUAL" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("C") ? "CHARITY" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("I") ? "INSTITUTION" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("F") ? "FOREIGNER" : dtHolderRegInfo.Rows[0]["REG_TYPE"].ToString();
                CIPLabel.Text = dtHolderRegInfo.Rows[0]["CIP"].Equals("N") ? "NO" : "YES";
                IDLabel.Text = dtHolderRegInfo.Rows[0]["ID_FLAG"].Equals("N") ? "NO" : "YES";

            }
            else
            {
                regNoTextBox.Focus();
                ClearText();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);

            }
        }
        else
        {
            regNoTextBox.Focus();
            ClearText();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Invalid Registration Number!! Please Enter Valid Registration Number');", true);

        }
    }
}
