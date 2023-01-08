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

public partial class UI_UnitNomineeEdit : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    BaseClass bcContent = new BaseClass();
    CommonGateway commonGatewayObj = new CommonGateway();
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

        if (yesMinorRadioButton.Checked)
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



               unitHolderRegBLObj.EditNomineeInfo(regObj, nomiObj,userObj);
                ClearText();
                regNoTextBox.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Success().ToString() + "');", true);



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
        NomiNumberDropDownList.SelectedValue = "0";
        
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

                DataTable dtNomineeList = commonGatewayObj.Select("SELECT NOMI_NO, NOMI_NO as NOMINEE FROM U_NOMINEE WHERE REG_BK='" + regObj.FundCode + "' AND REG_BR='" + regObj.BranchCode + "' AND REG_NO=" + regObj.RegNumber + " ORDER BY NOMI_NO");
                NameLabel.Text = dtHolderRegInfo.Rows[0]["HNAME"].ToString();
                DateLabel.Text = dtHolderRegInfo.Rows[0]["REG_DT"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtHolderRegInfo.Rows[0]["REG_DT"].ToString()).ToString("dd-MMM-yyyy");
                TypeLabel.Text = dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("N") ? "INDIVIDUAL" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("C") ? "CHARITY" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("I") ? "INSTITUTION" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("F") ? "FOREIGNER" : dtHolderRegInfo.Rows[0]["REG_TYPE"].ToString();
                CIPLabel.Text =  dtHolderRegInfo.Rows[0]["CIP"].Equals("N") ? "NO" : "YES";
                IDLabel.Text = dtHolderRegInfo.Rows[0]["ID_FLAG"].Equals("N") ? "NO" : "YES";
                if (dtNomineeList.Rows.Count > 0)
                {
                    DataTable dtNomineeListDDL = new DataTable();
                    dtNomineeListDDL.Columns.Add("ID", typeof(string));
                    dtNomineeListDDL.Columns.Add("NOMI_NO", typeof(string));
                    DataRow drDDL;
                    drDDL = dtNomineeListDDL.NewRow();
                    drDDL["ID"] = "0";
                    drDDL["NOMI_NO"] = " ";
                    dtNomineeListDDL.Rows.Add(drDDL);

                    for (int loop = 0; loop < dtNomineeList.Rows.Count; loop++)
                    {
                        drDDL = dtNomineeListDDL.NewRow();
                        drDDL["ID"] = dtNomineeList.Rows[loop]["NOMI_NO"].ToString();
                        drDDL["NOMI_NO"] = dtNomineeList.Rows[loop]["NOMINEE"].ToString();
                        dtNomineeListDDL.Rows.Add(drDDL);
                    }
                    NomiNumberDropDownList.DataSource = dtNomineeListDDL;

                    NomiNumberDropDownList.DataTextField = "NOMI_NO";
                    NomiNumberDropDownList.DataValueField = "ID";
                    NomiNumberDropDownList.DataBind();
                    ClearText();
                }
                else
                {
                    regNoTextBox.Focus();
                    ClearText();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Nominee Found Found');", true);
                }
               

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
    
    
    protected void NomiNumberDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeTextBox.Text;
        regObj.BranchCode = branchCodeTextBox.Text;
        regObj.RegNumber = regNoTextBox.Text;
        DataTable dtNomineeInfo = unitHolderRegBLObj.dtGetNomineeInfo(regObj,Convert.ToInt32( NomiNumberDropDownList.SelectedValue.ToString()));
        if (dtNomineeInfo.Rows.Count > 0)
        {
            controlNumberTextBox.Text = dtNomineeInfo.Rows[0]["NOMI_CTL_NO"].Equals(DBNull.Value) ? "" : dtNomineeInfo.Rows[0]["NOMI_CTL_NO"].ToString();
            nomiNumberTextBox.Text = dtNomineeInfo.Rows[0]["NOMI_NO"].Equals(DBNull.Value) ? "" : dtNomineeInfo.Rows[0]["NOMI_NO"].ToString();
            nomiNameTextBox.Text = dtNomineeInfo.Rows[0]["NOMI_NAME"].Equals(DBNull.Value) ? "" : dtNomineeInfo.Rows[0]["NOMI_NAME"].ToString();
            TypeDropDownList.SelectedValue = dtNomineeInfo.Rows[0]["NOMI_TYPE"].Equals(DBNull.Value) ? "N" : dtNomineeInfo.Rows[0]["NOMI_TYPE"].ToString();
            nomiFMTextBox.Text = dtNomineeInfo.Rows[0]["NOMI_FMH_NAME"].Equals(DBNull.Value) ? "" : dtNomineeInfo.Rows[0]["NOMI_FMH_NAME"].ToString();
            nomiMotherTextBox.Text = dtNomineeInfo.Rows[0]["NOMI_MO_NAME"].Equals(DBNull.Value) ? "" : dtNomineeInfo.Rows[0]["NOMI_MO_NAME"].ToString();
            nomiOccupationDropDownList.SelectedValue = dtNomineeInfo.Rows[0]["NOMI_OCC_CODE"].Equals(DBNull.Value) ? "0" : dtNomineeInfo.Rows[0]["NOMI_OCC_CODE"].ToString();

            nomiAddress1TextBox.Text = dtNomineeInfo.Rows[0]["NOMI_ADDRS1"].Equals(DBNull.Value) ? "" : dtNomineeInfo.Rows[0]["NOMI_ADDRS1"].ToString();
            nomiAddress2TextBox.Text = dtNomineeInfo.Rows[0]["NOMI_ADDRS2"].Equals(DBNull.Value) ? "" : dtNomineeInfo.Rows[0]["NOMI_ADDRS2"].ToString();
            nomiCityTextBox.Text = dtNomineeInfo.Rows[0]["NOMI_CITY"].Equals(DBNull.Value) ? "" : dtNomineeInfo.Rows[0]["NOMI_CITY"].ToString();
            nomiNationalityTextBox.Text = dtNomineeInfo.Rows[0]["NOMI_NATIONALITY"].Equals(DBNull.Value) ? "" : dtNomineeInfo.Rows[0]["NOMI_NATIONALITY"].ToString();
            nomiDateofBirthTextBox.Text = dtNomineeInfo.Rows[0]["NOMI_BIRTH_DT"].Equals(DBNull.Value) ? "" :Convert.ToDateTime( dtNomineeInfo.Rows[0]["NOMI_BIRTH_DT"]).ToString("dd-MMM-yyyy");
            //nomiAgeTextBox.Text = dtNomineeInfo.Rows[0]["NOMI_NAME"].Equals(DBNull.Value) ? "" : dtNomineeInfo.Rows[0]["NOMI_NAME"].ToString();
            RelationDropDownList.SelectedValue = dtNomineeInfo.Rows[0]["NOMI_REL"].Equals(DBNull.Value) ? "0" : dtNomineeInfo.Rows[0]["NOMI_REL"].ToString();
            percentageTextBox.Text = dtNomineeInfo.Rows[0]["PERCENTAGE"].Equals(DBNull.Value) ? "" : dtNomineeInfo.Rows[0]["PERCENTAGE"].ToString();
            nomiRemarksTextBox.Text = dtNomineeInfo.Rows[0]["REAMARKS"].Equals(DBNull.Value) ? "" : dtNomineeInfo.Rows[0]["REAMARKS"].ToString();

            if (dtNomineeInfo.Rows[0]["NOMI_IS_MINOR"].ToString() == "Y")
            {
                yesMinorRadioButton.Checked = true;
                noMinorRadioButton.Checked = false;
                DivGardian.Attributes.Add("style", "visibility:visible");
                gardianAddressTextBox.Text = dtNomineeInfo.Rows[0]["NOMI_GARDIAN_NAME"].Equals(DBNull.Value) ? "" : dtNomineeInfo.Rows[0]["NOMI_GARDIAN_NAME"].ToString();
                gardianRelationDropDownList.SelectedValue = dtNomineeInfo.Rows[0]["GARD_REL_WITH_NOMI"].Equals(DBNull.Value) ? "0" : dtNomineeInfo.Rows[0]["GARD_REL_WITH_NOMI"].ToString();
                gardianBirthDateTextBox.Text = dtNomineeInfo.Rows[0]["NOMI_GARDIAN_BIRTH_DT"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtNomineeInfo.Rows[0]["NOMI_GARDIAN_BIRTH_DT"]).ToString("dd-MMM-yyyy");
                gardianAddressTextBox.Text = dtNomineeInfo.Rows[0]["NOMI_GARDIAN_ADDRESS"].Equals(DBNull.Value) ? "" : dtNomineeInfo.Rows[0]["NOMI_GARDIAN_ADDRESS"].ToString();
            }
            else
            {
                yesMinorRadioButton.Checked = false;
                noMinorRadioButton.Checked = true;
                DivGardian.Attributes.Add("style", "visibility:hidden");
                gardianAddressTextBox.Text = "";
                gardianRelationDropDownList.SelectedValue = "0";
                gardianBirthDateTextBox.Text = "";
                gardianAddressTextBox.Text = "";
            }
        }
        else
        {
            ClearText();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);

        }

    }
    protected void regDeleteButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();

        regObj.FundCode = fundCodeTextBox.Text.Trim();
        regObj.BranchCode = branchCodeTextBox.Text.Trim();
        regObj.RegNumber = regNoTextBox.Text.Trim();


        UnitHolderNominee nomiObj = new UnitHolderNominee();
        nomiObj.NomiControlNo = controlNumberTextBox.Text.Trim().ToString().ToUpper();
        nomiObj.NomiType = TypeDropDownList.SelectedValue.ToString().ToUpper();
        nomiObj.NomiNumber = NomiNumberDropDownList.SelectedValue.ToString();

        try
        {



            unitHolderRegBLObj.DeleteNomineeInfo(regObj, nomiObj, userObj);
            ClearText();
            regNoTextBox.Focus();
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Success().ToString() + "');", true);



        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + ex.Message.Replace("'", "").ToString() + "');", true);
        }
    }
}
