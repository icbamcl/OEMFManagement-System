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
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitNomineeEntry : System.Web.UI.Page
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

            nomiOccupationDropDownList.DataSource = dtOccupationList;
            nomiOccupationDropDownList.DataTextField = "DESCR";
            nomiOccupationDropDownList.DataValueField = "CODE";
            nomiOccupationDropDownList.DataBind();
        
        }
       
    
    }
    
    protected void regSaveButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        
        regObj.FundCode = fundCodeTextBox.Text.Trim();
        regObj.BranchCode = branchCodeTextBox.Text.Trim();      
        regObj.RegNumber = regNoTextBox.Text.Trim();


        UnitHolderNominee nomiObj = new UnitHolderNominee();
        nomiObj.NomiControlNo = controlNumberTextBox.Text.Trim().ToString().ToUpper(); 
        nomiObj.NomiType = TypeDropDownList.SelectedValue.ToString().ToUpper();
        nomiObj.NomiNumber = nomiNumberTextBox.Text.Trim().ToString().ToUpper();
        nomiObj.Nomi1Name = nomiNameTextBox.Text.Trim().ToString().ToUpper();
        nomiObj.Nomi1FMName = nomiFMTextBox.Text.Trim().ToString().ToUpper();
        nomiObj.Nomi1MotherName = nomiMotherTextBox.Text.Trim().ToString().ToUpper();
        nomiObj.Nomi1Occupation =Convert.ToInt32( nomiOccupationDropDownList.SelectedValue.ToString().ToUpper());
        nomiObj.Nomi1Address1 = nomiAddress1TextBox.Text.Trim().ToString().ToUpper();
        nomiObj.Nomi1Address2 = nomiAddress2TextBox.Text.Trim().ToString().ToUpper();
        nomiObj.NomiCity = nomiCityTextBox.Text.Trim().ToString().ToUpper();
        nomiObj.Nomi1Nationality = nomiNationalityTextBox.Text.Trim().ToString().ToUpper();
        nomiObj.NomiDateBirth = nomiDateofBirthTextBox.Text.Trim().ToString().ToUpper();
        nomiObj.NomiAge = nomiAgeTextBox.Text.Trim().ToString().ToUpper();
        nomiObj.Nomi1Relation = RelationDropDownList.SelectedValue.ToString().ToUpper();
        nomiObj.Nomi1Percentage = percentageTextBox.Text.Trim().ToString().ToUpper();
        nomiObj.NomiRemarks = nomiRemarksTextBox.Text.Trim().ToString().ToUpper();
        if (noMinorRadioButton.Checked)
        {
            nomiObj.NomiISMinor ="Y";
            nomiObj.GardianName = gardianNameTextBox.Text.Trim().ToString().ToUpper();
            nomiObj.GardianAddress = gardianAddressTextBox.Text.Trim().ToString().ToUpper();
            nomiObj.GardianDateOfBirth = gardianBirthDateTextBox.Text.Trim().ToString().ToUpper();
            nomiObj.GardianAge = gardianAgeTextBox.Text.Trim().ToString().ToUpper();
            nomiObj.GardianRelWithNominee = gardianRelationDropDownList.SelectedValue.ToString().ToUpper();

        }
        else
        {
            nomiObj.NomiISMinor = "N";
        }
        try
        {
            //if (unitHolderRegBLObj.IsDuplicateJointHolder(regObj))
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert(' Save Failed: One Join Holder Already Entried');", true);
            //}
            //else if (regObj.FundCode.ToString().ToUpper() == "IAMPH") //for ICB AMCL Pension Holder's Unit Fund Join Holder is not allowed
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert(' Save Failed: In case of Pnsion Holders Unit Fund Join Holder is not allowed');", true);
            //}

            //else
            //{


            unitHolderRegBLObj.SaveNomineeInfo(regObj, nomiObj, userObj);
                ClearText();
                regNoTextBox.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Success().ToString() + "');", true);



            //}

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + ex.Message.Replace("'", "").ToString() + "');", true);
        }

    }
    private void ClearText()
    {
       
        controlNumberTextBox.Text="";
        TypeDropDownList.SelectedValue="0";
        nomiNumberTextBox.Text="";
        nomiNameTextBox.Text="";
        nomiFMTextBox.Text="";
        nomiMotherTextBox.Text="";
        nomiOccupationDropDownList.SelectedValue = "0";
        nomiAddress1TextBox.Text = "";
        nomiAddress2TextBox.Text = "";
        nomiCityTextBox.Text = "";
        nomiNationalityTextBox.Text = "";
        nomiDateofBirthTextBox.Text = "";
        nomiAgeTextBox.Text = "";
        RelationDropDownList.SelectedValue = "0";
        percentageTextBox.Text = "";
        nomiRemarksTextBox.Text = "";
        noMinorRadioButton.Checked = true;
        yesMinorRadioButton.Checked = false;
        DivGardian.Style.Add("visibility", "hidden");
        
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
                DataTable dtNomineeInfo = unitHolderRegBLObj.dtGetNomineeInfo(regObj);

                NameLabel.Text = dtHolderRegInfo.Rows[0]["HNAME"].ToString();
                DateLabel.Text = dtHolderRegInfo.Rows[0]["REG_DT"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtHolderRegInfo.Rows[0]["REG_DT"].ToString()).ToString("dd-MMM-yyyy");
                TypeLabel.Text = dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("N") ? "INDIVIDUAL" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("C") ? "CHARITY" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("I") ? "INSTITUTION" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("F") ? "FOREIGNER" : dtHolderRegInfo.Rows[0]["REG_TYPE"].ToString();
                CIPLabel.Text =  dtHolderRegInfo.Rows[0]["CIP"].Equals("N") ? "NO" : "YES";
                IDLabel.Text = dtHolderRegInfo.Rows[0]["ID_FLAG"].Equals("N") ? "NO" : "YES";
                nomiNumberTextBox.Text = Convert.ToString(dtNomineeInfo.Rows.Count + 1);

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
    protected void nomiDateofBirthTextBox_TextChanged(object sender, EventArgs e)
    {

    }
    protected void gardianBirthDateTextBox_TextChanged(object sender, EventArgs e)
    {

    }
    protected void nomiFMTextBox_TextChanged(object sender, EventArgs e)
    {

    }
}
