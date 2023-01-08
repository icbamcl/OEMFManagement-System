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
using System.Text;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;
using System.IO;

public partial class UI_UnitSearchByBO : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    BaseClass bcContent = new BaseClass();
    CommonGateway commonGatewayObj = new CommonGateway();
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
        BOTextBox.Focus();        
        ///holderDateofBirthTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        if (!IsPostBack)
        {
            DataTable dtOccupationList = opendMFDAO.dtOccopationList();
            holderOccupationDropDownList.DataSource = dtOccupationList;
            holderOccupationDropDownList.DataTextField= "DESCR";
            holderOccupationDropDownList.DataValueField = "CODE";
            holderOccupationDropDownList.DataBind();
            
         

         



           //bankNameDropDownList.DataSource = opendMFDAO.dtFillBankName(" CATE_CODE=1 " );
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

           regDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
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
        //holderEducationDropDownList.SelectedValue="0";
        holderDateofBirthTextBox.Text = "";
        holderCityTextBox.Text="";
        holderSexDropDownList.SelectedValue="0";
        holderReligionDropDownList.SelectedValue="0";
        //holderRemarksTextBox.Text="";

        BOTextBox.Text="";
        AllotTextBox.Text = "";
        folioTextBox.Text = "";
        BalaceNoTextBox.Text = "";

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

        //bankAddressTextBox.Text="";
        //bankNameDropDownList.SelectedValue = "0";
        //branchNameDropDownList.SelectedValue = "0";
        //bankAccTextBox.Enabled = false;       
        //bankAddressTextBox.Enabled = false;
        //bankNameDropDownList.Enabled = false;
        //branchNameDropDownList.Enabled = false;
        //bftnNoRadioButton.Checked = true;
        //bftnYesRadioButton.Checked = false;
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

        StringBuilder sbQuery = new StringBuilder();
        sbQuery.Append("SELECT     REG_BK, REG_BR, VOTTER_NO AS REG_NO, '10-OCT-2013' AS REG_DT, DECODE(BO_TYPE, 'COMPANY', 'I', 'MUTUAL_FUND', 'C','O','I', 'N') ");
        sbQuery.Append("  AS REG_TYPE, NAME1 AS HNAME, BO_FATHER AS FMH_NAME, BO_MOTHER AS MO_NAME, ADDRESS1 || ADDRESS2 AS ADDRS1, BO,ALLOT_NO, FOLIO_NO, ");
        sbQuery.Append("   ADDRESS3 || ADDRESS4 AS ADDRS2, CITY || '-' || POST_CODE AS CITY, COUNTRY AS NATIONALITY, PHONE1 AS TEL_NO, EMAIL, ");
        sbQuery.Append("  BANK_ACC || ',' || BANK AS SPEC_IN1, BRANCH AS SPEC_IN2, ROUTING_NO, CONVERTED_UNITS AS BALANCE, NVL(IS_ID_ACC, 'N') AS ID_FLAG, ");
        sbQuery.Append("   'N' AS CIP, DECODE(BANK, NULL, 'N', 'Y') AS BK_FLAG, BANK_ACC AS BK_AC_NO, decode(ROUTING_NO, NULL, 'N', 'Y') AS IS_BEFTN ");
        sbQuery.Append(" FROM    CMF_CDBL WHERE     REG_BK='" + fundCodeTextBox.Text.ToString().ToUpper() +"' AND   REG_BR='"+branchCodeTextBox.Text.ToString().ToUpper()+"'");
        //if (regNoTextBox.Text != "")
        //{
        //    sbQuery.Append(" AND VOTTER_NO=" + int.Parse( regNoTextBox.Text.ToString()));
        //}
        if (BOTextBox.Text != "")
        {
            sbQuery.Append(" AND BO='" + BOTextBox.Text.ToString() + "'");
        }

        if (folioTextBox.Text != "")
        {
            sbQuery.Append(" AND FOLIO_NO=" + folioTextBox.Text.ToString());
        }
        if (AllotTextBox.Text != "")
        {
            sbQuery.Append(" AND ALLOT_NO=" + AllotTextBox.Text.ToString());
        }


        DataTable dtHolderRegInfo = commonGatewayObj.Select(sbQuery.ToString());
           // DataTable dtNomineeRegInfo = opendMFDAO.dtNomineeRegInfo(regObj);
        string regiNo = "";
            if (dtHolderRegInfo.Rows.Count > 0)
            {
                regDateTextBox.Text = dtHolderRegInfo.Rows[0]["REG_DT"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtHolderRegInfo.Rows[0]["REG_DT"].ToString()).ToString("dd-MMM-yyyy");
                regTypeDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["REG_TYPE"].ToString();
                isCIPDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["CIP"].ToString();
                isIDDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["ID_FLAG"].ToString();
                //if (dtHolderRegInfo.Rows[0]["ID_FLAG"].ToString() == "Y")
                //{
                //    IDAccNoTextBox.Text = dtHolderRegInfo.Rows[0]["ID_AC"].ToString();
                //    if (!dtHolderRegInfo.Rows[0]["ID_BK_NM_CD"].Equals(DBNull.Value) && !dtHolderRegInfo.Rows[0]["ID_BK_BR_NM_CD"].Equals(DBNull.Value))
                //    {
                //        IDbankNameDropDownList.Enabled = true;
                //        IDbranchNameDropDownList.Enabled = true;
                //        IDbankNameDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["ID_BK_NM_CD"].ToString();
                //        IDbranchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(dtHolderRegInfo.Rows[0]["ID_BK_NM_CD"].ToString()));
                //        IDbranchNameDropDownList.DataTextField = "BRANCH_NAME";
                //        IDbranchNameDropDownList.DataValueField = "BRANCH_CODE";
                //        IDbranchNameDropDownList.DataBind();
                //        IDbranchNameDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["ID_BK_BR_NM_CD"].ToString();
                //    }
                //    else
                //    {
                //        IDbankNameDropDownList.SelectedValue = "0";
                //        IDbranchNameDropDownList.SelectedValue = "0";
                //        IDbankNameDropDownList.Enabled = false;
                //        IDbranchNameDropDownList.Enabled = false;
                //    }
                  
                //}
                //else
                //{
                //    IDAccNoTextBox.Text="";
                //    IDbankNameDropDownList.SelectedValue = "0";
                //    IDbranchNameDropDownList.SelectedValue = "0";
                //    IDbankNameDropDownList.Enabled = false;
                //    IDbranchNameDropDownList.Enabled = false;
                //}
                regNoTextBox.Text = dtHolderRegInfo.Rows[0]["REG_NO"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["REG_NO"].ToString();
                regiNo = dtHolderRegInfo.Rows[0]["REG_NO"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["REG_NO"].ToString();
                BOTextBox.Text = dtHolderRegInfo.Rows[0]["BO"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["BO"].ToString();
                folioTextBox.Text = dtHolderRegInfo.Rows[0]["FOLIO_NO"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["FOLIO_NO"].ToString();
                AllotTextBox.Text = dtHolderRegInfo.Rows[0]["ALLOT_NO"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["ALLOT_NO"].ToString();
                BalaceNoTextBox.Text = dtHolderRegInfo.Rows[0]["BALANCE"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["BALANCE"].ToString();

                holderNameTextBox.Text = dtHolderRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["HNAME"].ToString();
                holderFMTextBox.Text = dtHolderRegInfo.Rows[0]["FMH_NAME"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["FMH_NAME"].ToString();
                holderMotherTextBox.Text = dtHolderRegInfo.Rows[0]["MO_NAME"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["MO_NAME"].ToString();
                holderAddress1TextBox.Text = dtHolderRegInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["ADDRS1"].ToString();
                holderAddress2TextBox.Text = dtHolderRegInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["ADDRS2"].ToString();
               // holderOccupationDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["OCC_CODE"].Equals(DBNull.Value) ? "0" : dtHolderRegInfo.Rows[0]["OCC_CODE"].ToString();
                holderNationalityTextBox.Text = dtHolderRegInfo.Rows[0]["NATIONALITY"].Equals(DBNull.Value) ? "BANGLADESHI" : dtHolderRegInfo.Rows[0]["NATIONALITY"].ToString();
                holderTelphoneTextBox.Text = dtHolderRegInfo.Rows[0]["TEL_NO"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["TEL_NO"].ToString();
                holderEmailTextBox.Text = dtHolderRegInfo.Rows[0]["EMAIL"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["EMAIL"].ToString();
               // holderMaritialStatusDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["MAR_STAT"].Equals(DBNull.Value) ? "0" : dtHolderRegInfo.Rows[0]["MAR_STAT"].ToString();
               // holderEducationDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["EDU_QUA"].Equals(DBNull.Value) ? "0" : dtHolderRegInfo.Rows[0]["EDU_QUA"].ToString();
             //   holderDateofBirthTextBox.Text = dtHolderRegInfo.Rows[0]["B_DATE"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtHolderRegInfo.Rows[0]["B_DATE"].ToString()).ToString("dd-MMM-yyyy");
                holderCityTextBox.Text = dtHolderRegInfo.Rows[0]["CITY"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["CITY"].ToString();
               // holderSexDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["SEX"].Equals(DBNull.Value) ? "0" : dtHolderRegInfo.Rows[0]["SEX"].ToString();
             //   holderReligionDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["RELIGION"].Equals(DBNull.Value) ? "0" : dtHolderRegInfo.Rows[0]["RELIGION"].ToString();
              //  holderRemarksTextBox.Text = dtHolderRegInfo.Rows[0]["REMARKS"].Equals(DBNull.Value) ? "" : dtHolderRegInfo.Rows[0]["REMARKS"].ToString();

                //if (dtHolderRegInfo.Rows[0]["IS_BEFTN"].Equals(DBNull.Value))
                //{
                //    bftnNoRadioButton.Checked = true;
                //    bftnYesRadioButton.Checked = false;
                //}
                //else if(dtHolderRegInfo.Rows[0]["IS_BEFTN"].ToString().ToUpper()=="N")
                //{
                //    bftnNoRadioButton.Checked = true;
                //    bftnYesRadioButton.Checked = false;
                //}
                //else if (dtHolderRegInfo.Rows[0]["IS_BEFTN"].ToString().ToUpper() == "Y")
                //{
                //    bftnNoRadioButton.Checked = false;
                //    bftnYesRadioButton.Checked = true;
                //}
             

             

              

                //if (dtHolderRegInfo.Rows[0]["BK_FLAG"].ToString() == "Y")
                //{

                //    //string branchAddress = "";
                //    isBankDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["BK_FLAG"].ToString();
                //    //string BankAccInfo = dtHolderRegInfo.Rows[0]["SPEC_IN1"].ToString() + dtHolderRegInfo.Rows[0]["SPEC_IN2"].ToString();
                //    //string[] BankAccountInfo = BankAccInfo.Split(',');
                //    //if (BankAccountInfo.Length > 0)
                //    //{
                //    //    bankAccTextBox.Text = BankAccountInfo[0].ToString();
                //    //    if (BankAccountInfo.Length > 1)
                //    //    {
                //    //        bankNameTextBox.Text = BankAccountInfo[1].ToString();
                //    //    }
                //    //    if (BankAccountInfo.Length > 2)
                //    //    {
                //    //        branchNameTextBox.Text = BankAccountInfo[2].ToString();
                //    //    }
                //    //    if (BankAccountInfo.Length > 3)
                //    //    {
                //    //        for (int loop = 3; loop < BankAccountInfo.Length; loop++)
                //    //        {
                //    //            branchAddress = branchAddress + BankAccountInfo[loop].ToString();
                //    //        }
                //    //        bankAddressTextBox.Text = branchAddress;
                //    //    }


                //    //    bankAccTextBox.Enabled = true;
                //    //    bankNameTextBox.Enabled = false;
                //    //    branchNameTextBox.Enabled = false;
                //    //    bankAddressTextBox.Enabled = true;
                //    //    bankNameDropDownList.Enabled = true;
                //    //    branchNameDropDownList.Enabled = true;
                //    //    bankNameDropDownList.SelectedValue = "0";
                //    //    branchNameDropDownList.SelectedValue = "0";
                //    //}
                //    //else
                //    //{
                //    //    bankAccTextBox.Enabled = true;
                //    //    bankNameTextBox.Enabled = false;
                //    //    branchNameTextBox.Enabled = false;
                //    //    bankAddressTextBox.Enabled = true;
                //    //}

                //    if (!dtHolderRegInfo.Rows[0]["BK_NM_CD"].Equals(DBNull.Value) && !dtHolderRegInfo.Rows[0]["BK_BR_NM_CD"].Equals(DBNull.Value) && !dtHolderRegInfo.Rows[0]["BK_AC_NO"].Equals(DBNull.Value))
                //    {
                //        bankAccTextBox.Enabled = true;
                //        bankNameDropDownList.Enabled = true;
                //        branchNameDropDownList.Enabled = true;
                //        bankAddressTextBox.Enabled = true;
                //        bankNameDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["BK_NM_CD"].ToString();
                //        branchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(dtHolderRegInfo.Rows[0]["BK_NM_CD"].ToString()));
                //        branchNameDropDownList.DataTextField = "BRANCH_NAME";
                //        branchNameDropDownList.DataValueField = "BRANCH_CODE";
                //        branchNameDropDownList.DataBind();
                //        branchNameDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["BK_BR_NM_CD"].ToString();
                //        bankAccTextBox.Text = dtHolderRegInfo.Rows[0]["BK_AC_NO"].ToString();
                //        DataTable dtBankBracnhInfo = unitHolderRegBLObj.dtGetBankBracnhInfo(Convert.ToInt32(dtHolderRegInfo.Rows[0]["BK_NM_CD"].ToString()), Convert.ToInt32(dtHolderRegInfo.Rows[0]["BK_BR_NM_CD"].ToString()));
                //        if (dtBankBracnhInfo.Rows.Count > 0)
                //        {
                //            bankAddressTextBox.Text = "Routing No=[" + dtBankBracnhInfo.Rows[0]["ROUTING_NO"].ToString() + "] " + dtBankBracnhInfo.Rows[0]["ADDRESS"].ToString() + " ";
                //        }
                //    }

                //}
                //else
                //{

                //    isBankDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["BK_FLAG"].ToString();
                //    bankAccTextBox.Text = "";                    
                //    bankAddressTextBox.Text = "";
                //    bankNameDropDownList.SelectedValue = "0";
                //    branchNameDropDownList.SelectedValue = "0";
                //    bankAccTextBox.Enabled = false;                                     
                //    bankAddressTextBox.Enabled = false;
                //    bankNameDropDownList.Enabled = false;
                //    branchNameDropDownList.Enabled = false;
                //}

                displaySign(regiNo);
            }
            else
            {
                BOTextBox.Focus();                
                ClearText();
                SignImage.ImageUrl = Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString();
                ScriptManager.RegisterStartupScript(this.Page,this.Page.GetType(), "Popup", "alert('No Data Found');", true);
                
            }
        
      
    }
   
    public void displaySign(string regiNo)
    {
        string regNo = "";
        string fundCode = "";
        string branchCode = "";
        string unitHolderName = "";
        string branchName = "";
        string fundName = "";

        regNo = regiNo.ToString();
        fundName = opendMFDAO.GetFundName(fundCodeTextBox.Text.ToString());
        fundCode = fundCodeTextBox.Text.ToString();
        branchName = opendMFDAO.GetBranchName(branchCodeTextBox.Text.ToString());
        branchCode = branchCodeTextBox.Text.ToString();


        unitHolderName = opendMFDAO.GetHolderName(fundCode, branchCode, regNo);


        string[] BranchCodeSign = branchCode.Split('/');
        string imageSignLocation = Path.Combine(ConfigReader.SingLocation + "\\" + fundCode, fundCode + "_" + BranchCodeSign[0] + "_" + BranchCodeSign[1] + "_" + regNo + ".jpg");//"../../Image/IAMCL/Sign/"+ fundCode + "_" + branchCode + "_" + regNo + ".jpg";
        string imagePhotoLocation = Path.Combine(ConfigReader.PhotoLocation + "\\" + fundCode, fundCode + "_" + "_" + BranchCodeSign[0] + "_" + BranchCodeSign[1] + "_" + regNo + ".jpg");

        if (File.Exists(Path.Combine(ConfigReader.SingLocation + "\\" + fundCode, fundCode + "_" + BranchCodeSign[0] + "_" + BranchCodeSign[1] + "_" + regNo + ".jpg")))
        {
            SignImage.ImageUrl = imageSignLocation.ToString();
        }
        else
        {
            SignImage.ImageUrl = Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString();
        }


    }
}
