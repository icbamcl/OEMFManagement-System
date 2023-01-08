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

public partial class UI_UnitJointEdit : System.Web.UI.Page
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

            DataTable dtJointHolder = opendMFDAO.dtJointHolderRegInfo(regObj);

        try
        {
            if (dtJointHolder.Rows.Count > 0) //IS Joint holder exist ??
            {
               

                if (regObj.FundCode.ToString().ToUpper() == "IAMPH") //for ICB AMCL Pension Holder's Unit Fund Join Holder is not allowed
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert(' Save Failed: In case of Pension Holders  Join Holder is not allowed');", true);
                }
                //else if (unitHolderRegBLObj.CheckSanctionList(regObj, unitHolderObj) != "")
                //{
                //    string errorMessage = unitHolderRegBLObj.CheckSanctionList(regObj, unitHolderObj);
                //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed :" + errorMessage + "');", true);

                //}
                else
                {


                    unitHolderRegBLObj.EditJointHolderInfo(regObj, unitHolderObj,dtJointHolder, userObj);
                    ClearText();
                    regNoTextBox.Focus();
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Success().ToString() + "');", true);



                }
            }
            else
            {
                ClearText();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Joint Holder Information Found to Edit !! Please Entry Joint Holder ');", true);
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
                DataTable dtJointHolder = opendMFDAO.dtJointHolderRegInfo(regObj);

                NameLabel.Text = dtHolderRegInfo.Rows[0]["HNAME"].ToString();
                DateLabel.Text = dtHolderRegInfo.Rows[0]["REG_DT"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtHolderRegInfo.Rows[0]["REG_DT"].ToString()).ToString("dd-MMM-yyyy");
                TypeLabel.Text = dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("N") ? "INDIVIDUAL" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("C") ? "CHARITY" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("I") ? "INSTITUTION" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("F") ? "FOREIGNER" : dtHolderRegInfo.Rows[0]["REG_TYPE"].ToString();
                CIPLabel.Text = dtHolderRegInfo.Rows[0]["CIP"].Equals("N") ? "NO" : "YES";
                IDLabel.Text = dtHolderRegInfo.Rows[0]["ID_FLAG"].Equals("N") ? "NO" : "YES";

                if (dtJointHolder.Rows.Count > 0)
                {
                    NameTextBox.Text = dtJointHolder.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["JNT_NAME"].ToString();
                    FMTextBox.Text = dtJointHolder.Rows[0]["JNT_FMH_NAME"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["JNT_FMH_NAME"].ToString();
                    MotherTextBox.Text = dtJointHolder.Rows[0]["JNT_MO_NAME"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["JNT_MO_NAME"].ToString();
                    spouceTextBox.Text = dtJointHolder.Rows[0]["SP_NAME"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["SP_NAME"].ToString();

                    NIDTextBox.Text = dtJointHolder.Rows[0]["NID"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["NID"].ToString(); ;
                    passportTextBox.Text = dtJointHolder.Rows[0]["PASS_NO"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["PASS_NO"].ToString(); ;
                    TINTextBox.Text = dtJointHolder.Rows[0]["TIN"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["TIN"].ToString(); ;
                    birthCertNoTextBox.Text = dtJointHolder.Rows[0]["BIRTH_CERT_NO"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["BIRTH_CERT_NO"].ToString(); ;



                    OccupationDropDownList.SelectedValue = dtJointHolder.Rows[0]["JNT_OCC_CODE"].Equals(DBNull.Value) ? "0" : dtJointHolder.Rows[0]["JNT_OCC_CODE"].ToString();
                    NationalityTextBox.Text = dtJointHolder.Rows[0]["JNT_NATIONALITY"].Equals(DBNull.Value) ? "BANGLADESHI" : dtJointHolder.Rows[0]["JNT_NATIONALITY"].ToString();
                    DateofBirthTextBox.Text = dtJointHolder.Rows[0]["JNT_B_DATE"].Equals(DBNull.Value) ? " " : dtJointHolder.Rows[0]["JNT_B_DATE"].ToString();

                    TelphoneTextBox.Text = dtJointHolder.Rows[0]["MOBILE1"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["MOBILE1"].ToString();

                    presentAddress1TextBox.Text = dtJointHolder.Rows[0]["JNT_ADDRS1"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["JNT_ADDRS1"].ToString();
                    presentAddress2TextBox.Text = dtJointHolder.Rows[0]["JNT_ADDRS2"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["JNT_ADDRS2"].ToString();
                    presentCityTextBox.Text = dtJointHolder.Rows[0]["JNT_CITY"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["JNT_CITY"].ToString();

                    parmanAddress1TextBox.Text = dtJointHolder.Rows[0]["PADDRESS1"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["PADDRESS1"].ToString();
                    parmanAddress2TextBox.Text = dtJointHolder.Rows[0]["PADDRESS2"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["PADDRESS2"].ToString();
                    parmentCityTextBox.Text = dtJointHolder.Rows[0]["PCITY"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["PCITY"].ToString();

                    SourceFundTextBox.Text = dtJointHolder.Rows[0]["SOURCE_FUND"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["SOURCE_FUND"].ToString();

                    EmailTextBox.Text = dtJointHolder.Rows[0]["JNT_EMAIL"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["JNT_EMAIL"].ToString();
                    MaritialStatusDropDownList.SelectedValue = dtJointHolder.Rows[0]["MAR_STAT"].Equals(DBNull.Value) ? "0" : dtJointHolder.Rows[0]["MAR_STAT"].ToString();
                    EducationDropDownList.SelectedValue = dtJointHolder.Rows[0]["EDU_QUA"].Equals(DBNull.Value) ? "0" : dtJointHolder.Rows[0]["EDU_QUA"].ToString();


                    SexDropDownList.SelectedValue = dtJointHolder.Rows[0]["SEX"].Equals(DBNull.Value) ? "0" : dtJointHolder.Rows[0]["SEX"].ToString();
                    ReligionDropDownList.SelectedValue = dtJointHolder.Rows[0]["RELIGION"].Equals(DBNull.Value) ? "0" : dtJointHolder.Rows[0]["RELIGION"].ToString();
                    RemarksTextBox.Text = dtJointHolder.Rows[0]["REMARKS"].Equals(DBNull.Value) ? "" : dtJointHolder.Rows[0]["REMARKS"].ToString();

                }
                else
                {
                   ClearText();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Joint Holder Information Found');", true);
                }

            }
            else
            {
                regNoTextBox.Focus();
                ClearText();
                DateLabel.Text = "";
                TypeLabel.Text = "";
                CIPLabel.Text = "";
                IDLabel.Text = "";
                NameLabel.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);

            }
        }
        else
        {
            regNoTextBox.Focus();
            ClearText();
            DateLabel.Text = "";
            TypeLabel.Text = "";
            CIPLabel.Text = "";
            IDLabel.Text = "";
            NameLabel.Text = "";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Invalid Registration Number!! Please Enter Valid Registration Number');", true);

        }
    }
    protected void regDeleteButton_Click(object sender, EventArgs e)
    {

        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeTextBox.Text.Trim();
        regObj.BranchCode = branchCodeTextBox.Text.Trim();
        regObj.RegNumber = regNoTextBox.Text.Trim();
        DataTable dtJointHolder = opendMFDAO.dtJointHolderRegInfo(regObj);
        try
        {
            if (dtJointHolder.Rows.Count > 0) //IS Joint holder exist ??
            {

                

                    unitHolderRegBLObj.DeleteJointHolderInfo(regObj,dtJointHolder,userObj);
                    ClearText();
                    regNoTextBox.Focus();
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Success().ToString() + "');", true);



                
            }
            else
            {
                ClearText();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Joint Holder Information Found to Edit !! Please Entry Joint Holder ');", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + ex.Message.Replace("'", "").ToString() + "');", true);
        }

    }
}
