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

public partial class UI_UnitReg : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
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
        fundCodeTextBox.Text = fundCode.ToString();
        branchCodeTextBox.Text = branchCode.ToString();        
        regNoTextBox.Focus();
        
        ///holderDateofBirthTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        if (!IsPostBack)
        {
            DataTable dtOccupationList = opendMFDAO.dtOccopationList();
            holderOccupationDropDownList.DataSource = dtOccupationList;
            holderOccupationDropDownList.DataTextField= "DESCR";
            holderOccupationDropDownList.DataValueField = "CODE";
            holderOccupationDropDownList.DataBind();
            
           jHolderOccupationDropDownList.DataSource= dtOccupationList;
           jHolderOccupationDropDownList.DataTextField = "DESCR";
           jHolderOccupationDropDownList.DataValueField = "CODE";
           jHolderOccupationDropDownList.DataBind();


           nomi1OccupationDropDownList.DataSource = dtOccupationList;
           nomi1OccupationDropDownList.DataTextField = "DESCR";
           nomi1OccupationDropDownList.DataValueField = "CODE";
           nomi1OccupationDropDownList.DataBind();


           nomi2OccupationDropDownList.DataSource = dtOccupationList;
           nomi2OccupationDropDownList.DataTextField = "DESCR";
           nomi2OccupationDropDownList.DataValueField = "CODE";
           nomi2OccupationDropDownList.DataBind();

           bankNameDropDownList.DataSource = opendMFDAO.dtFillBankName(" CATE_CODE=1 " );
           bankNameDropDownList.DataTextField = "BANK_NAME";
           bankNameDropDownList.DataValueField = "BANK_CODE";
           bankNameDropDownList.DataBind();

           IDbankNameDropDownList.DataSource = opendMFDAO.dtFillBankName(" CATE_CODE=2 ");
           IDbankNameDropDownList.DataTextField = "BANK_NAME";
           IDbankNameDropDownList.DataValueField = "BANK_CODE";
           IDbankNameDropDownList.DataBind();

           regTypeDropDownList.DataSource = unitHolderRegBLObj.dtFillRegType();
           regTypeDropDownList.DataTextField = "NAME";
           regTypeDropDownList.DataValueField = "CODE";
           regTypeDropDownList.DataBind();

           regDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        }
    
    }
    protected void isBankDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (isBankDropDownList.SelectedValue.ToString() == "N")
        {
            bankAccTextBox.Text = "";
          
            bankAddressTextBox.Text = "";
            bankNameDropDownList.SelectedValue = "0";
            branchNameDropDownList.SelectedValue = "0";
            bankAccTextBox.Enabled = false;            
            bankAddressTextBox.Enabled = false;          
            bankNameDropDownList.Enabled = false;
            branchNameDropDownList.Enabled = false;
            bftnNoRadioButton.Checked = true;
            bftnYesRadioButton.Checked = false;
           
        }
        else if (isBankDropDownList.SelectedValue.ToString() == "Y")
        {
            bankAccTextBox.Enabled = true;
            bankNameDropDownList.Enabled = true;
            branchNameDropDownList.Enabled = true;

           
            bankAddressTextBox.Enabled = true;

            bftnNoRadioButton.Checked = false;
            bftnYesRadioButton.Checked = true;

        }
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
        regObj.IsIDAccount = isIDDropDownList.SelectedValue;
        if (isIDDropDownList.SelectedValue.ToString().ToUpper() == "Y")
        {            
            regObj.IdBankID = Convert.ToInt32(IDbankNameDropDownList.SelectedValue.ToString());
            regObj.IdBankBranchID = Convert.ToInt32(IDbranchNameDropDownList.SelectedValue.ToString());
        }
        regObj.IdAccNo = IDAccNoTextBox.Text.Trim().ToString();

        UnitHolderInfo unitHolderObj = new UnitHolderInfo();
        unitHolderObj.HolderName = holderNameTextBox.Text.Trim();
        unitHolderObj.HolderFMHName = holderFMTextBox.Text.Trim();
        unitHolderObj.HolderMotherName = holderMotherTextBox.Text.Trim();
        unitHolderObj.HolderAddress1 = holderAddress1TextBox.Text.Trim();
        unitHolderObj.HolderAddress2 = holderAddress2TextBox.Text.Trim();
        unitHolderObj.HolderOccupation = Convert.ToInt16(holderOccupationDropDownList.SelectedValue);
        unitHolderObj.HolderNationality = holderNationalityTextBox.Text.Trim();

        unitHolderObj.HolderTIN = holderTINTextBox.Text.Trim();

        unitHolderObj.HolderTelephone = holderTelphoneTextBox.Text.Trim();
        unitHolderObj.HolderEmail = holderEmailTextBox.Text.Trim();
        unitHolderObj.HolderMaritialStatus = holderMaritialStatusDropDownList.SelectedValue;
        unitHolderObj.HolderEduQua = holderEducationDropDownList.SelectedValue;
        unitHolderObj.HolderDateofBirth = holderDateofBirthTextBox.Text.Trim().ToString();
        unitHolderObj.HolderCity = holderCityTextBox.Text.Trim();
        unitHolderObj.HolderSex = holderSexDropDownList.SelectedValue;
        unitHolderObj.HolderReligion = holderReligionDropDownList.SelectedValue;
        unitHolderObj.HolderRemarks = holderRemarksTextBox.Text.Trim();

        UnitJointHolderInfo jHolderObj = new UnitJointHolderInfo();
        jHolderObj.JHolderName = jHolderNameTextBox.Text.Trim();
        jHolderObj.JHolderFMHName = jHolderFMTextBox.Text.Trim();
        jHolderObj.JHolderMotherName = jHolderMotherTextBox.Text.Trim().ToString().ToUpper();
        jHolderObj.JHolderAddress1 = jHolderAddress1TextBox.Text.Trim();
        jHolderObj.JHolderAddress2 = jHolderAddress2TextBox.Text.Trim();
        jHolderObj.JHolderNationality = jHolderNantionalityTextBox.Text.Trim();
        jHolderObj.JHolderOccupation = Convert.ToInt16(jHolderOccupationDropDownList.SelectedValue);

        UnitHolderNominee nomiObj = new UnitHolderNominee();
        nomiObj.NomiControlNo = NomiControlNoTextBox.Text.Trim().ToString();
        nomiObj.Nomi1Name = nomi1NameTextBox.Text.Trim();
        nomiObj.Nomi1FMName = nomi1FMTextBox.Text.Trim();
        nomiObj.Nomi1MotherName = nomi1MotherNameTextBox.Text.Trim().ToUpper().ToString();
        nomiObj.Nomi1Address1 = nomi1Address1TextBox.Text.Trim();
        nomiObj.Nomi1Address2 = nomi1Address2TextBox.Text.Trim();
        nomiObj.Nomi1Occupation =Convert.ToInt16( nomi1OccupationDropDownList.SelectedValue);
        nomiObj.Nomi1Nationality = nomi1NationalityTextBox.Text.Trim();
        nomiObj.Nomi1Relation = nomi1RelationDropDownList.SelectedValue;
        nomiObj.Nomi1Percentage = nomi1PtcTextBox.Text.Trim().ToString();

        nomiObj.Nomi2Name = nomi2NameTextBox.Text.Trim();
        nomiObj.Nomi2FMName = nomi2FMTextBox.Text.Trim();
        nomiObj.Nomi2MotherName = nomi2MotherNameTextBox.Text.Trim().ToString().ToUpper();
        nomiObj.Nomi2Address1 = nomi2Address1TextBox.Text.Trim();
        nomiObj.Nomi2Address2 = nomi2Address2TextBox.Text.Trim();
        nomiObj.Nomi2Occupation = Convert.ToInt16(nomi2OccupationDropDownList.SelectedValue);
        nomiObj.Nomi2Nationality = nomi2NationalityTextBox.Text.Trim();
        nomiObj.Nomi2Relation = nomi2RelationDropDownList.SelectedValue;
        nomiObj.Nomi2Percentage = nomi2PtcTextBox.Text.Trim();

        UnitHolderBankInfo bankInfoObj = new UnitHolderBankInfo();
        if (bftnNoRadioButton.Checked)
        {
            bankInfoObj.IsBEFTN = "N";
        }
        else if (bftnYesRadioButton.Checked)
        {
            bankInfoObj.IsBEFTN = "Y";
        }
        bankInfoObj.IsBankInfo = isBankDropDownList.SelectedValue;
        if (isBankDropDownList.SelectedValue.ToString().ToUpper() == "Y")
        {
            bankInfoObj.BankAccountNo = bankAccTextBox.Text.Trim();
            //bankInfoObj.BankName = bankNameDropDownList.SelectedItem.Text.Trim().ToString();
            //bankInfoObj.BranchName = branchNameDropDownList.SelectedItem.Text.Trim().ToString();
            //bankInfoObj.BankAddress = bankAddressTextBox.Text.Trim().ToString();
            bankInfoObj.BankCode = bankNameDropDownList.SelectedValue.ToString();
            bankInfoObj.BankBranchCode = branchNameDropDownList.SelectedValue.ToString();
           
        }

        try
        {

            //if (unitHolderRegBLObj.IsDuplicateNomineeControlEdit(regObj, nomiObj))
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Duplicate().ToString() + " " + "Nominee Control Number " + "');", true);
            //}
            //else
            if (unitHolderRegBLObj.CheckSanctionList(regObj, unitHolderObj) != "")
            {
                string errorMessage = unitHolderRegBLObj.CheckSanctionList(regObj, unitHolderObj);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed :" + errorMessage + "');", true);

            }
            else if (unitHolderRegBLObj.IsEditPermit(regObj.FundCode.ToString()))
            {
                string errorMessage = "Please Contact with Administrator";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed :" + errorMessage + "');", true);

            }
            //else if (regObj.FundCode == "IAMPH")
            //{
            //    string errorMessage = "Please Contact with Administrator";
            //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed :" + errorMessage + "');", true);

            //}
            else
            {
                unitHolderRegBLObj.SaveRegEditInfo(opendMFDAO.HolderRegInfo(regObj), opendMFDAO.dtNomineeRegInfo(regObj), userObj);// Backup Previous Information
                unitHolderRegBLObj.UpdateRegInfo(regObj, unitHolderObj, jHolderObj, nomiObj, bankInfoObj, userObj);//Update reg Info;
                ClearText();
                // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert(" msgObj.Success + ");", true);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + msgObj.Success().ToString() + "');", true);
                //ClientScript.RegisterStartupScript(this.GetType(), "Reset Fields", "fnReset();", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('"+msgObj.Error().ToString() + " " + ex.Message.Replace("'","").ToString()+"');", true);
        }


    }
    private void ClearText()
    {
        //regDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        //regNoTextBox.Text="";
        regTypeDropDownList.SelectedValue = "N";
        isCIPDropDownList.SelectedValue="N";
        isIDDropDownList.SelectedValue = "N";
        IDAccNoTextBox.Text = "";
        IDbankNameDropDownList.SelectedValue = "0";
        IDbranchNameDropDownList.SelectedValue = "0";
        IDbankNameDropDownList.Enabled = false;
        IDbranchNameDropDownList.Enabled = false;

        holderNameTextBox.Text="";
        holderFMTextBox.Text="";
        holderMotherTextBox.Text = "";
        holderAddress1TextBox.Text="";
        holderAddress2TextBox.Text="";
        holderOccupationDropDownList.SelectedValue="0";
        holderNationalityTextBox.Text="";
        holderTelphoneTextBox.Text="";
        holderEmailTextBox.Text="";
        holderMaritialStatusDropDownList.SelectedValue="0";
        holderEducationDropDownList.SelectedValue="0";
        holderDateofBirthTextBox.Text = "";
        holderCityTextBox.Text="";
        holderSexDropDownList.SelectedValue="0";
        holderReligionDropDownList.SelectedValue="0";
        holderRemarksTextBox.Text="";
        holderTINTextBox.Text = "";

        jHolderNameTextBox.Text="";
        jHolderFMTextBox.Text="";
        jHolderMotherTextBox.Text = "";
        jHolderAddress1TextBox.Text="";
        jHolderAddress2TextBox.Text="";
        jHolderNantionalityTextBox.Text="";
        jHolderOccupationDropDownList.SelectedValue="0";

        NomiControlNoTextBox.Text = "";
        nomi1NameTextBox.Text="";
        nomi1FMTextBox.Text = "";
        nomi1MotherNameTextBox.Text = "";
        nomi1Address1TextBox.Text="";
        nomi1Address2TextBox.Text="";
        nomi1OccupationDropDownList.SelectedValue="0";
        nomi1NationalityTextBox.Text="";
        nomi1RelationDropDownList.SelectedValue="0";
        nomi1PtcTextBox.Text = "";

        nomi2NameTextBox.Text="";
        nomi2FMTextBox.Text = "";
        nomi2MotherNameTextBox.Text = "";
        nomi2Address1TextBox.Text="";
        nomi2Address2TextBox.Text="";
        nomi2OccupationDropDownList.SelectedValue="0";
        nomi2NationalityTextBox.Text="";
        nomi2RelationDropDownList.SelectedValue="0";
        nomi2PtcTextBox.Text = "";

        isBankDropDownList.SelectedValue="N";
        bankAccTextBox.Text = ""; 
        
        bankAddressTextBox.Text="";
        bankNameDropDownList.SelectedValue = "0";
        branchNameDropDownList.SelectedValue = "0";
        bankAccTextBox.Enabled = false;       
        bankAddressTextBox.Enabled = false;
        bankNameDropDownList.Enabled = false;
        branchNameDropDownList.Enabled = false;
        bftnNoRadioButton.Checked = true;
        bftnYesRadioButton.Checked = false;
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
            DataTable dtNomineeRegInfo = opendMFDAO.dtNomineeRegInfo(regObj);
            if (dtHolderRegInfo.Rows.Count > 0)
            {
                regDateTextBox.Text = dtHolderRegInfo.Rows[0]["REG_DT"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtHolderRegInfo.Rows[0]["REG_DT"].ToString()).ToString("dd-MMM-yyyy");
                regTypeDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["REG_TYPE"].ToString();
                isCIPDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["CIP"].ToString();
                isIDDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["ID_FLAG"].ToString();
                if (dtHolderRegInfo.Rows[0]["ID_FLAG"].ToString() == "Y")
                {
                    IDAccNoTextBox.Text = dtHolderRegInfo.Rows[0]["ID_AC"].ToString();
                    if (!dtHolderRegInfo.Rows[0]["ID_BK_NM_CD"].Equals(DBNull.Value) && !dtHolderRegInfo.Rows[0]["ID_BK_BR_NM_CD"].Equals(DBNull.Value))
                    {
                        IDbankNameDropDownList.Enabled = true;
                        IDbranchNameDropDownList.Enabled = true;
                        IDbankNameDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["ID_BK_NM_CD"].ToString();
                        IDbranchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(dtHolderRegInfo.Rows[0]["ID_BK_NM_CD"].ToString()));
                        IDbranchNameDropDownList.DataTextField = "BRANCH_NAME";
                        IDbranchNameDropDownList.DataValueField = "BRANCH_CODE";
                        IDbranchNameDropDownList.DataBind();
                        IDbranchNameDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["ID_BK_BR_NM_CD"].ToString();
                    }
                    else
                    {
                        IDbankNameDropDownList.SelectedValue = "0";
                        IDbranchNameDropDownList.SelectedValue = "0";
                        IDbankNameDropDownList.Enabled = false;
                        IDbranchNameDropDownList.Enabled = false;
                    }
                  
                }
                else
                {
                    IDAccNoTextBox.Text="";
                    IDbankNameDropDownList.SelectedValue = "0";
                    IDbranchNameDropDownList.SelectedValue = "0";
                    IDbankNameDropDownList.Enabled = false;
                    IDbranchNameDropDownList.Enabled = false;
                }

                holderNameTextBox.Text = dtHolderRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["HNAME"].ToString();
                holderFMTextBox.Text = dtHolderRegInfo.Rows[0]["FMH_NAME"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["FMH_NAME"].ToString();
                holderMotherTextBox.Text = dtHolderRegInfo.Rows[0]["MO_NAME"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["MO_NAME"].ToString();
                holderAddress1TextBox.Text = dtHolderRegInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["ADDRS1"].ToString();
                holderAddress2TextBox.Text = dtHolderRegInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["ADDRS2"].ToString();
                holderOccupationDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["OCC_CODE"].Equals(DBNull.Value) ? "0" : dtHolderRegInfo.Rows[0]["OCC_CODE"].ToString();
                holderNationalityTextBox.Text = dtHolderRegInfo.Rows[0]["NATIONALITY"].Equals(DBNull.Value) ? "BANGLADESHI" : dtHolderRegInfo.Rows[0]["NATIONALITY"].ToString();
                holderTelphoneTextBox.Text = dtHolderRegInfo.Rows[0]["TEL_NO"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["TEL_NO"].ToString();

                holderTINTextBox.Text = dtHolderRegInfo.Rows[0]["TIN"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["TIN"].ToString();

                holderEmailTextBox.Text = dtHolderRegInfo.Rows[0]["EMAIL"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["EMAIL"].ToString();
                holderMaritialStatusDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["MAR_STAT"].Equals(DBNull.Value) ? "0" : dtHolderRegInfo.Rows[0]["MAR_STAT"].ToString();
                holderEducationDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["EDU_QUA"].Equals(DBNull.Value) ? "0" : dtHolderRegInfo.Rows[0]["EDU_QUA"].ToString();
                holderDateofBirthTextBox.Text = dtHolderRegInfo.Rows[0]["B_DATE"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtHolderRegInfo.Rows[0]["B_DATE"].ToString()).ToString("dd-MMM-yyyy");
                holderCityTextBox.Text = dtHolderRegInfo.Rows[0]["CITY"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["CITY"].ToString();
                holderSexDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["SEX"].Equals(DBNull.Value) ? "0" : dtHolderRegInfo.Rows[0]["SEX"].ToString();
                holderReligionDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["RELIGION"].Equals(DBNull.Value) ? "0" : dtHolderRegInfo.Rows[0]["RELIGION"].ToString();
                holderRemarksTextBox.Text = dtHolderRegInfo.Rows[0]["REMARKS"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["REMARKS"].ToString();

                if (dtHolderRegInfo.Rows[0]["IS_BEFTN"].Equals(DBNull.Value))
                {
                    bftnNoRadioButton.Checked = true;
                    bftnYesRadioButton.Checked = false;
                }
                else if(dtHolderRegInfo.Rows[0]["IS_BEFTN"].ToString().ToUpper()=="N")
                {
                    bftnNoRadioButton.Checked = true;
                    bftnYesRadioButton.Checked = false;
                }
                else if (dtHolderRegInfo.Rows[0]["IS_BEFTN"].ToString().ToUpper() == "Y")
                {
                    bftnNoRadioButton.Checked = false;
                    bftnYesRadioButton.Checked = true;
                }
             

                jHolderNameTextBox.Text = dtHolderRegInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["JNT_NAME"].ToString();
                jHolderFMTextBox.Text = dtHolderRegInfo.Rows[0]["JNT_FMH_NAME"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["JNT_FMH_NAME"].ToString();
                jHolderMotherTextBox.Text = dtHolderRegInfo.Rows[0]["JNT_MO_NAME"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["JNT_MO_NAME"].ToString();
                jHolderAddress1TextBox.Text = dtHolderRegInfo.Rows[0]["JNT_ADDRS1"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["JNT_ADDRS1"].ToString();
                jHolderAddress2TextBox.Text = dtHolderRegInfo.Rows[0]["JNT_ADDRS2"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["JNT_ADDRS2"].ToString();
                jHolderNantionalityTextBox.Text = dtHolderRegInfo.Rows[0]["JNT_NATIONALITY"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["JNT_NATIONALITY"].ToString();
                jHolderOccupationDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["JNT_OCC_CODE"].Equals(DBNull.Value) ? "0" : dtHolderRegInfo.Rows[0]["JNT_OCC_CODE"].ToString();

              

                if (dtHolderRegInfo.Rows[0]["BK_FLAG"].ToString() == "Y")
                {

                   
                    isBankDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["BK_FLAG"].ToString();
                    //string BankAccInfo = dtHolderRegInfo.Rows[0]["SPEC_IN1"].ToString() + dtHolderRegInfo.Rows[0]["SPEC_IN2"].ToString();
                    //string[] BankAccountInfo = BankAccInfo.Split(',');
                    //if (BankAccountInfo.Length > 0)
                    //{
                    //    bankAccTextBox.Text = BankAccountInfo[0].ToString();
                    //    if (BankAccountInfo.Length > 1)
                    //    {
                    //        bankNameTextBox.Text = BankAccountInfo[1].ToString();
                    //    }
                    //    if (BankAccountInfo.Length > 2)
                    //    {
                    //        branchNameTextBox.Text = BankAccountInfo[2].ToString();
                    //    }
                    //    if (BankAccountInfo.Length > 3)
                    //    {
                    //        for (int loop = 3; loop < BankAccountInfo.Length; loop++)
                    //        {
                    //            branchAddress = branchAddress + BankAccountInfo[loop].ToString();
                    //        }
                    //        bankAddressTextBox.Text = branchAddress;
                    //    }


                    //    bankAccTextBox.Enabled = true;
                    //    bankNameTextBox.Enabled = false;
                    //    branchNameTextBox.Enabled = false;
                    //    bankAddressTextBox.Enabled = true;
                    //    bankNameDropDownList.Enabled = true;
                    //    branchNameDropDownList.Enabled = true;
                    //    bankNameDropDownList.SelectedValue = "0";
                    //    branchNameDropDownList.SelectedValue = "0";
                    //}
                    //else
                    //{
                    //    bankAccTextBox.Enabled = true;
                    //    bankNameTextBox.Enabled = false;
                    //    branchNameTextBox.Enabled = false;
                    //    bankAddressTextBox.Enabled = true;
                    //}

                    if (!dtHolderRegInfo.Rows[0]["BK_NM_CD"].Equals(DBNull.Value) && !dtHolderRegInfo.Rows[0]["BK_BR_NM_CD"].Equals(DBNull.Value) && !dtHolderRegInfo.Rows[0]["BK_AC_NO"].Equals(DBNull.Value))
                    {
                        bankAccTextBox.Enabled = true;
                        bankNameDropDownList.Enabled = true;
                        branchNameDropDownList.Enabled = true;
                        bankAddressTextBox.Enabled = true;
                        bankNameDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["BK_NM_CD"].ToString();
                        branchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(dtHolderRegInfo.Rows[0]["BK_NM_CD"].ToString()));
                        branchNameDropDownList.DataTextField = "BRANCH_NAME";
                        branchNameDropDownList.DataValueField = "BRANCH_CODE";
                        branchNameDropDownList.DataBind();
                        branchNameDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["BK_BR_NM_CD"].ToString();
                        bankAccTextBox.Text = dtHolderRegInfo.Rows[0]["BK_AC_NO"].ToString();
                        DataTable dtBankBracnhInfo = unitHolderRegBLObj.dtGetBankBracnhInfo(Convert.ToInt32(dtHolderRegInfo.Rows[0]["BK_NM_CD"].ToString()), Convert.ToInt32(dtHolderRegInfo.Rows[0]["BK_BR_NM_CD"].ToString()));
                        if (dtBankBracnhInfo.Rows.Count > 0)
                        {
                            bankAddressTextBox.Text = "Routing No=[" + dtBankBracnhInfo.Rows[0]["ROUTING_NO"].ToString() + "] " + dtBankBracnhInfo.Rows[0]["ADDRESS"].ToString() + " ";
                        }
                    }

                }
                else
                {

                    isBankDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["BK_FLAG"].ToString();
                    bankAccTextBox.Text = "";                    
                    bankAddressTextBox.Text = "";
                    bankNameDropDownList.SelectedValue = "0";
                    branchNameDropDownList.SelectedValue = "0";
                    bankAccTextBox.Enabled = false;                                     
                    bankAddressTextBox.Enabled = false;
                    bankNameDropDownList.Enabled = false;
                    branchNameDropDownList.Enabled = false;
                }

                if (dtNomineeRegInfo.Rows.Count > 0)
                {
                    NomiControlNoTextBox.Text = dtNomineeRegInfo.Rows[0]["NOMI_CTL_NO"].Equals(DBNull.Value) ? "" : dtNomineeRegInfo.Rows[0]["NOMI_CTL_NO"].ToString();                   
                    nomi1NameTextBox.Text = dtNomineeRegInfo.Rows[0]["NOMI_NAME"].Equals(DBNull.Value) ? "" : dtNomineeRegInfo.Rows[0]["NOMI_NAME"].ToString();
                    nomi1FMTextBox.Text = dtNomineeRegInfo.Rows[0]["NOMI_FMH_NAME"].Equals(DBNull.Value) ? "" : dtNomineeRegInfo.Rows[0]["NOMI_FMH_NAME"].ToString();
                    nomi1MotherNameTextBox.Text = dtNomineeRegInfo.Rows[0]["NOMI_MO_NAME"].Equals(DBNull.Value) ? "" : dtNomineeRegInfo.Rows[0]["NOMI_MO_NAME"].ToString();
                    nomi1Address1TextBox.Text = dtNomineeRegInfo.Rows[0]["NOMI_ADDRS1"].Equals(DBNull.Value) ? "" : dtNomineeRegInfo.Rows[0]["NOMI_ADDRS1"].ToString();
                    nomi1Address2TextBox.Text = dtNomineeRegInfo.Rows[0]["NOMI_ADDRS2"].Equals(DBNull.Value) ? "" : dtNomineeRegInfo.Rows[0]["NOMI_ADDRS2"].ToString();
                    nomi1OccupationDropDownList.SelectedValue = dtNomineeRegInfo.Rows[0]["NOMI_OCC_CODE"].Equals(DBNull.Value) ? "0" : dtNomineeRegInfo.Rows[0]["NOMI_OCC_CODE"].ToString();
                    nomi1NationalityTextBox.Text = dtNomineeRegInfo.Rows[0]["NOMI_NATIONALITY"].Equals(DBNull.Value) ? "BANGLADESHI" : dtNomineeRegInfo.Rows[0]["NOMI_NATIONALITY"].ToString();
                    nomi1RelationDropDownList.SelectedValue = dtNomineeRegInfo.Rows[0]["NOMI_REL"].Equals(DBNull.Value) ? "0" : dtNomineeRegInfo.Rows[0]["NOMI_REL"].ToString();
                    nomi1PtcTextBox.Text = dtNomineeRegInfo.Rows[0]["PERCENTAGE"].Equals(DBNull.Value) ? "0" : dtNomineeRegInfo.Rows[0]["PERCENTAGE"].ToString();
                }
                if (dtNomineeRegInfo.Rows.Count > 1)
                {
                    nomi2NameTextBox.Text = dtNomineeRegInfo.Rows[1]["NOMI_NAME"].Equals(DBNull.Value) ? "" : dtNomineeRegInfo.Rows[1]["NOMI_NAME"].ToString();
                    nomi2FMTextBox.Text = dtNomineeRegInfo.Rows[1]["NOMI_FMH_NAME"].Equals(DBNull.Value) ? "" : dtNomineeRegInfo.Rows[1]["NOMI_FMH_NAME"].ToString();
                    nomi2MotherNameTextBox.Text = dtNomineeRegInfo.Rows[0]["NOMI_MO_NAME"].Equals(DBNull.Value) ? "" : dtNomineeRegInfo.Rows[0]["NOMI_MO_NAME"].ToString();
                    nomi2Address1TextBox.Text = dtNomineeRegInfo.Rows[1]["NOMI_ADDRS1"].Equals(DBNull.Value) ? "" : dtNomineeRegInfo.Rows[1]["NOMI_ADDRS1"].ToString();
                    nomi2Address2TextBox.Text = dtNomineeRegInfo.Rows[1]["NOMI_ADDRS2"].Equals(DBNull.Value) ? "" : dtNomineeRegInfo.Rows[1]["NOMI_ADDRS2"].ToString();
                    nomi2OccupationDropDownList.SelectedValue = dtNomineeRegInfo.Rows[1]["NOMI_OCC_CODE"].Equals(DBNull.Value) ? "0" : dtNomineeRegInfo.Rows[1]["NOMI_OCC_CODE"].ToString();
                    nomi2NationalityTextBox.Text = dtNomineeRegInfo.Rows[1]["NOMI_NATIONALITY"].Equals(DBNull.Value) ? "BANGLADESHI" : dtNomineeRegInfo.Rows[1]["NOMI_NATIONALITY"].ToString();
                    nomi2RelationDropDownList.SelectedValue = dtNomineeRegInfo.Rows[1]["NOMI_REL"].Equals(DBNull.Value) ? "0" : dtNomineeRegInfo.Rows[1]["NOMI_REL"].ToString();
                    nomi2PtcTextBox.Text = dtNomineeRegInfo.Rows[1]["PERCENTAGE"].Equals(DBNull.Value) ? "0" : dtNomineeRegInfo.Rows[1]["PERCENTAGE"].ToString();
                }
                if (dtNomineeRegInfo.Rows.Count <= 0)
                {
                    NomiControlNoTextBox.Text = "";
                    nomi1NameTextBox.Text = "";
                    nomi1FMTextBox.Text = "";
                    nomi1Address1TextBox.Text = "";
                    nomi1Address2TextBox.Text = "";
                    nomi1OccupationDropDownList.SelectedValue = "0";
                    nomi1NationalityTextBox.Text = "";
                    nomi1RelationDropDownList.SelectedValue = "0";
                    nomi1PtcTextBox.Text = "";

                    nomi2NameTextBox.Text = "";
                    nomi2FMTextBox.Text = "";
                    nomi2Address1TextBox.Text = "";
                    nomi2Address2TextBox.Text = "";
                    nomi2OccupationDropDownList.SelectedValue = "0";
                    nomi2NationalityTextBox.Text = "";
                    nomi2RelationDropDownList.SelectedValue = "0";
                    nomi2PtcTextBox.Text = "";
                }
                if (dtNomineeRegInfo.Rows.Count == 1)
                {
                    nomi2NameTextBox.Text = "";
                    nomi2FMTextBox.Text = "";
                    nomi2Address1TextBox.Text = "";
                    nomi2Address2TextBox.Text = "";
                    nomi2OccupationDropDownList.SelectedValue = "0";
                    nomi2NationalityTextBox.Text = "";
                    nomi2RelationDropDownList.SelectedValue = "0";
                    nomi2PtcTextBox.Text = "";
                }
            }
            else
            {
                regNoTextBox.Focus();
                ClearText();
                ScriptManager.RegisterStartupScript(this.Page,this.Page.GetType(), "Popup", "alert('No Data Found');", true);
                
            }
        }
        else
        {
            regNoTextBox.Focus();
            ClearText();
            ScriptManager.RegisterStartupScript(this.Page,this.Page.GetType(), "Popup", "alert('Invalid Registration Number!! Please Enter Valid Registration Number');", true);
            
        }
    }
    protected void bankNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        branchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(bankNameDropDownList.SelectedValue.ToString()));
        branchNameDropDownList.DataTextField = "BRANCH_NAME";
        branchNameDropDownList.DataValueField = "BRANCH_CODE";
        branchNameDropDownList.DataBind();
    }
    protected void regNoTextBox_TextChanged(object sender, EventArgs e)
    {
        FindRegInfo();
    }
    protected void IDbankNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDbranchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(IDbankNameDropDownList.SelectedValue.ToString()));
        IDbranchNameDropDownList.DataTextField = "BRANCH_NAME";
        IDbranchNameDropDownList.DataValueField = "BRANCH_CODE";
        IDbranchNameDropDownList.DataBind();
    }
    protected void isIDDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (isIDDropDownList.SelectedValue.ToString().ToUpper() == "Y")
        {
            IDAccNoTextBox.Enabled = true;
            IDAccNoTextBox.Focus();
            IDbankNameDropDownList.Enabled = true;
            IDbranchNameDropDownList.Enabled = true;

        }
        else if (isIDDropDownList.SelectedValue.ToString().ToUpper() == "N")
        {
            IDAccNoTextBox.Text = "";
            IDAccNoTextBox.Enabled = false;
            IDbankNameDropDownList.SelectedValue = "0";
            IDbranchNameDropDownList.SelectedValue = "0";
            IDbankNameDropDownList.Enabled = false;
            IDbranchNameDropDownList.Enabled = false;


        }
    }
    protected void branchNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtBankBracnhInfo = unitHolderRegBLObj.dtGetBankBracnhInfo(Convert.ToInt32(bankNameDropDownList.SelectedValue.ToString()), Convert.ToInt32(branchNameDropDownList.SelectedValue.ToString()));
        if (dtBankBracnhInfo.Rows.Count > 0)
        {
            bankAddressTextBox.Text = "Routing No=[" + dtBankBracnhInfo.Rows[0]["ROUTING_NO"].ToString() + "] " + dtBankBracnhInfo.Rows[0]["ADDRESS"].ToString() + " ";
        }
    }
    
}
