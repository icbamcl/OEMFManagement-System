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
using System.IO;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;


public partial class UI_UnitRepurchaseChequeDelivery : System.Web.UI.Page
{
   // System.Web.UI.Page this_page_ref = null;
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();
    UnitLienBl unitLienBLObj = new UnitLienBl();
    EncryptDecrypt encrypt = new EncryptDecrypt();
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
        regNoTextBox.Focus();
      
        if (!IsPostBack)
        {
          

            fundCodeDDL.DataSource = reportObj.dtFundCodeList();
            fundCodeDDL.DataTextField = "NAME";
            fundCodeDDL.DataValueField = "ID";
            fundCodeDDL.SelectedValue = fundCode.ToString();
            fundCodeDDL.DataBind();

            branchCodeDDL.DataSource = reportObj.dtBranchCodeList();
            branchCodeDDL.DataTextField = "NAME";
            branchCodeDDL.DataValueField = "ID";
            branchCodeDDL.SelectedValue = branchCode.ToString();
            branchCodeDDL.DataBind();

           
            dateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        }
    
    }
    
    private void ClearText()
    {
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        string fundCode = bcContent.FundCode.ToString();
        string branchCode = bcContent.BranchCode.ToString();

        holderNameTextBox.Text="";
        jHolderTextBox.Text="";
        holderAddress1TextBox.Text="";
        holderAddress2TextBox.Text="";       
        holderTelphoneTextBox.Text="";        
        tdCIP.InnerHtml = "";
        Nominee1NameTextBox.Text = "";
        Nominee2NameTextBox.Text = "";
        BankInfoTextBox.Text = "";
        RemarksTextBox.Text = "";
        SignImage.ImageUrl = "";
        TotalLienUnitHoldingTextBox.Text = "";
        SaleLockLabel.Text = "NO";
        RepLockLabel.Text = "NO";
        TransferLockLabel.Text = "NO";
        LienLockLabel.Text = "NO";
        RenLockLabel.Text = "NO";
        dvLockin.Attributes.Add("style", "visibility:hidden");
        fundCodeDDL.SelectedValue = fundCode.ToString();
        branchCodeDDL.SelectedValue = branchCode.ToString();

    }
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }
    protected void findButton_Click(object sender, EventArgs e)
    {

        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
        regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
        regObj.RegNumber = regNoTextBox.Text.Trim();
        regObj.BO = holderBOTextBox.Text.ToString().Trim();
        regObj.Folio = folioTextBox.Text.Trim();

        DataTable dtValidSearch = opendMFDAO.dtValidSearch(regObj);
        if (dtValidSearch.Rows.Count > 0)
        {
            regObj = new UnitHolderRegistration();
            regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
            regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
            regObj.RegNumber = dtValidSearch.Rows[0]["REG_NO"].ToString();
            displayRegInfo(regObj);
        }
        else
        {
            SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
            ClearText();
            dvLedger.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Registration Number OR BO OR Foilo');", true);
        }


    }
   
    public void displayRegInfo(UnitHolderRegistration unitRegObj)
    {
        dvLedger.Visible = true;
      
        unitRegObj.FundCode = fundCodeDDL.SelectedValue.ToString();
        unitRegObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
        unitRegObj.RegNumber = regNoTextBox.Text.Trim();
        DataTable dtRegInfo = opendMFDAO.getDtRegInfo(unitRegObj);
        DataTable dtNominee = opendMFDAO.dtNomineeRegInfo(unitRegObj);


        if (dtRegInfo.Rows.Count > 0)
        {
            //Trasaction Lock Status 

            if (!(dtRegInfo.Rows[0]["ALL_LOCK"].Equals(DBNull.Value) || (dtRegInfo.Rows[0]["ALL_LOCK"].ToString() == "N")) || !(dtRegInfo.Rows[0]["SL_LOCK"].Equals(DBNull.Value) || (dtRegInfo.Rows[0]["SL_LOCK"].ToString() == "N")) || !(dtRegInfo.Rows[0]["REP_LOCK"].Equals(DBNull.Value) || (dtRegInfo.Rows[0]["REP_LOCK"].ToString() == "N")) || !(dtRegInfo.Rows[0]["TR_LOCK"].Equals(DBNull.Value) || (dtRegInfo.Rows[0]["TR_LOCK"].ToString() == "N")) || !(dtRegInfo.Rows[0]["LIEN_LOCK"].Equals(DBNull.Value) || (dtRegInfo.Rows[0]["LIEN_LOCK"].ToString() == "N")) || !(dtRegInfo.Rows[0]["REN_LOCK"].Equals(DBNull.Value) || (dtRegInfo.Rows[0]["REN_LOCK"].ToString() == "N")) || !(dtRegInfo.Rows[0]["LOCK_REMARKS"].Equals(DBNull.Value)))
            {
                dvLockin.Attributes.Add("style", "visibility:visible");
                if (dtRegInfo.Rows[0]["ALL_LOCK"].ToString() == "Y")
                {
                    SaleLockLabel.Text = "YES";
                    RepLockLabel.Text = "YES";
                    TransferLockLabel.Text = "YES";
                    LienLockLabel.Text = "YES";
                    RenLockLabel.Text = "YES";
                    LockRemarksTextBox.Text = dtRegInfo.Rows[0]["LOCK_REMARKS"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["LOCK_REMARKS"].ToString();
                }
                else
                {
                    if (dtRegInfo.Rows[0]["SL_LOCK"].ToString() == "Y")
                    {
                        SaleLockLabel.Text = "YES";
                    }
                    else
                    {
                        SaleLockLabel.Text = "NO";
                    }
                    if (dtRegInfo.Rows[0]["REP_LOCK"].ToString() == "Y")
                    {
                        RepLockLabel.Text = "YES";
                    }
                    else
                    {
                        RepLockLabel.Text = "NO";
                    }
                    if (dtRegInfo.Rows[0]["TR_LOCK"].ToString() == "Y")
                    {
                        TransferLockLabel.Text = "YES";
                    }
                    else
                    {
                        TransferLockLabel.Text = "NO";
                    }
                    if (dtRegInfo.Rows[0]["LIEN_LOCK"].ToString() == "Y")
                    {
                        LienLockLabel.Text = "YES";
                    }
                    else
                    {
                        LienLockLabel.Text = "NO";
                    }

                    if (dtRegInfo.Rows[0]["REN_LOCK"].ToString() == "Y")
                    {
                        RenLockLabel.Text = "YES";
                    }
                    else
                    {
                        RenLockLabel.Text = "NO";
                    }



                    LockRemarksTextBox.Text = dtRegInfo.Rows[0]["LOCK_REMARKS"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["LOCK_REMARKS"].ToString();
                }
            }
            else
            {
                SaleLockLabel.Text = "NO";
                RepLockLabel.Text = "NO";
                TransferLockLabel.Text = "NO";
                LienLockLabel.Text = "NO";
                RenLockLabel.Text = "NO";
                dvLockin.Attributes.Add("style", "visibility:hidden");
            }
            regNoTextBox.Text = dtRegInfo.Rows[0]["REG_NO"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["REG_NO"].ToString();
            holderBOTextBox.Text = dtRegInfo.Rows[0]["BO"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["BO"].ToString();
            folioTextBox.Text = dtRegInfo.Rows[0]["FOLIO_NO"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["FOLIO_NO"].ToString();
            holderNameTextBox.Text = dtRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["HNAME"].ToString();
            jHolderTextBox.Text = dtRegInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["JNT_NAME"].ToString();
            holderAddress1TextBox.Text = dtRegInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["ADDRS1"].ToString();
            holderAddress2TextBox.Text = dtRegInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["ADDRS2"].ToString();
            holderTelphoneTextBox.Text = dtRegInfo.Rows[0]["TEL_NO"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["TEL_NO"].ToString();
            tdTIN.InnerHtml = dtRegInfo.Rows[0]["TIN"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["TIN"].ToString();
            string CIP = dtRegInfo.Rows[0]["CIP"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["CIP"].ToString();
            string BEFTN = dtRegInfo.Rows[0]["IS_BEFTN"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["IS_BEFTN"].ToString();
            if (string.Compare(CIP, "Y", true) == 0)
            {
                tdCIP.InnerHtml = "YES";
            }
            else if (string.Compare(CIP, "N", true) == 0)
            {
                tdCIP.InnerHtml = "NO";
            }
            else
            {
                tdCIP.InnerHtml = " ";
            }
            if (string.Compare(BEFTN, "Y", true) == 0)
            {
                tdBEFTN.InnerHtml = "YES";
            }
            else
            {
                tdBEFTN.InnerHtml = "NO";
            }
            if (dtNominee.Rows.Count > 1)
            {
                Nominee1NameTextBox.Text = dtNominee.Rows[0]["NOMI_NAME"].ToString();
                Nominee2NameTextBox.Text = dtNominee.Rows[1]["NOMI_NAME"].ToString();
            }
            else if (dtNominee.Rows.Count > 0)
            {
                Nominee1NameTextBox.Text = dtNominee.Rows[0]["NOMI_NAME"].ToString();
                Nominee2NameTextBox.Text = "";
            }
            else
            {
                Nominee1NameTextBox.Text = "";
                Nominee2NameTextBox.Text = "";
            }
            RemarksTextBox.Text = dtRegInfo.Rows[0]["REMARKS"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["REMARKS"].ToString();
            if (dtRegInfo.Rows[0]["BK_FLAG"].ToString() == "Y")
            {

                string bankInfo = "";
                if (!dtRegInfo.Rows[0]["BK_NM_CD"].Equals(DBNull.Value) && !dtRegInfo.Rows[0]["BK_BR_NM_CD"].Equals(DBNull.Value) && !dtRegInfo.Rows[0]["BK_AC_NO"].Equals(DBNull.Value))
                {

                    bankInfo = "AC:" + dtRegInfo.Rows[0]["BK_AC_NO"].ToString();
                    bankInfo = bankInfo + " , " + reportObj.getBankNameByBankCode(Convert.ToInt32(dtRegInfo.Rows[0]["BK_NM_CD"].ToString())).ToString();
                    bankInfo = bankInfo + " , " + reportObj.getBankBranchNameByCode(Convert.ToInt32(dtRegInfo.Rows[0]["BK_NM_CD"].ToString()), Convert.ToInt32(dtRegInfo.Rows[0]["BK_BR_NM_CD"].ToString())).ToString();

                    DataTable dtBankBracnhInfo = unitHolderRegBLObj.dtGetBankBracnhInfo(Convert.ToInt32(dtRegInfo.Rows[0]["BK_NM_CD"].ToString()), Convert.ToInt32(dtRegInfo.Rows[0]["BK_BR_NM_CD"].ToString()));
                    if (dtBankBracnhInfo.Rows.Count > 0)
                    {
                        bankInfo = bankInfo + " Routing No=[" + dtBankBracnhInfo.Rows[0]["ROUTING_NO"].ToString() + "] " + dtBankBracnhInfo.Rows[0]["ADDRESS"].ToString() + " ";
                    }

                }
                BankInfoTextBox.Text = bankInfo.ToString();

            }
            else
            {
                BankInfoTextBox.Text = "";

            }

            DataTable dtDividendLedger = getDividendLedger(unitRegObj);
          //  int balance = 0;
            if (dtDividendLedger.Rows.Count > 0)
            {

                dgLedger.DataSource = dtDividendLedger;
                dgLedger.DataBind();
                totalRowCountLabel.Text = dtDividendLedger.Rows.Count.ToString();
                 Session["dtDividendLedger"] = dtDividendLedger;
                displaySign();
            }
            else
            {
                ClearText();
                SignImage.ImageUrl = Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString();
                dvLedger.Visible = false;
                 Session["dtDividendLedger"] =null;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Ledger Balance Data Found');", true);
            }
        }

        else
        {
            SignImage.ImageUrl = Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString();
            dvLedger.Visible = false;
             Session["dtDividendLedger"] =null;
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", " window.fnResetAll();", true);
            tdCIP.InnerHtml = "";
        }
    }

    public void displaySign()
    {
        string regNo = "";
        string fundCode = "";
        string branchCode = "";
        string unitHolderName = "";
        string branchName = "";
        string fundName = "";

        regNo = regNoTextBox.Text.ToString();
        fundName = opendMFDAO.GetFundName(fundCodeDDL.SelectedValue.ToString());
        fundCode = fundCodeDDL.SelectedValue.ToString();
        branchName = opendMFDAO.GetBranchName(branchCodeDDL.SelectedValue.ToString());
        branchCode = branchCodeDDL.SelectedValue.ToString();


        unitHolderName = opendMFDAO.GetHolderName(fundCode, branchCode, regNo);


        string[] BranchCodeSign = branchCode.Split('/');
        string imageSignLocation = Path.Combine(ConfigReader.SingLocation +"\\"+ fundCode, fundCode + "_" + BranchCodeSign[0] + "_" + BranchCodeSign[1] + "_" + regNo + ".jpg");//"../../Image/IAMCL/Sign/"+ fundCode + "_" + branchCode + "_" + regNo + ".jpg";
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

    protected void regNoTextBox_TextChanged(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
        regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
        regObj.RegNumber = regNoTextBox.Text.Trim();
        if (opendMFDAO.IsValidRegistration(regObj))
        {

            displayRegInfo(regObj);
        }
        else
        {
            ClearText();
            dvLedger.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Registration Number');", true);
        }

    }
    public DataTable getDividendLedger(UnitHolderRegistration unitRegObj)
    {
        StringBuilder sbQuery = new StringBuilder();
        sbQuery.Append("SELECT  DIVIDEND.ID, DIVIDEND.FUND_CD, DIVIDEND.DIVI_NO, DIVIDEND.FY, DIVIDEND.WAR_NO, DIVI_PARA.FY_PART, DIVIDEND.TOT_DIVI, DIVIDEND.DIDUCT, DIVIDEND.FI_DIVI_QTY, TO_CHAR(DIVIDEND.WAR_BK_PAY_DT,'DD-MON-YYYY') AS WAR_BK_PAY_DT, ");
        sbQuery.Append(" TO_CHAR(DIVI_PARA.ISS_DT ,'DD-MON-YYYY') AS ISSUE_DT,TO_CHAR(DIVIDEND.POST_RETURN_DATE ,'DD-MON-YYYY') AS POST_RETURN_DATE, TO_CHAR(DIVIDEND.POST_DATE ,'DD-MON-YYYY') AS POST_DATE,");
        sbQuery.Append(" TO_CHAR(DIVIDEND.BEFTN_RETURN_DT,'DD-MON-YYYY') AS BEFTN_RETURN_DT, DIVIDEND.CIP_QTY, DIVIDEND.BALANCE, TO_CHAR(DIVIDEND.WAR_DELEVARY_DT, 'DD-MON-YYYY') AS WAR_DELEVARY_DT, DECODE(DIVIDEND.IS_BEFTN, 'Y', ");
        sbQuery.Append(" 'BEFTN  PROCESS', 'BY HAND') AS IS_BEFTN, DIVIDEND.WARR_RECPT_BY FROM  DIVI_PARA INNER JOIN  DIVIDEND ON DIVI_PARA.FUND_CD = DIVIDEND.FUND_CD AND DIVI_PARA.DIVI_NO = DIVIDEND.DIVI_NO AND DIVI_PARA.F_YEAR = DIVIDEND.FY AND ");
        sbQuery.Append(" DIVI_PARA.CLOSE_DT = DIVIDEND.CLOSE_DT WHERE 1=1 ");
        sbQuery.Append(" AND REG_BR='" + unitRegObj.BranchCode.ToString() + "' AND REG_BK='" + unitRegObj.FundCode.ToString() + "' AND REG_NO=" + unitRegObj.RegNumber);
        sbQuery.Append(" ORDER BY DIVIDEND.DIVI_NO DESC ");
        DataTable getDividendLedger = commonGatewayObj.Select(sbQuery.ToString());
        DataTable dtDividendInfo = getDividendLedger.Clone();
        dtDividendInfo.Columns.Add("WAR_STATUS", typeof(string));
        DataRow drdtGrid;
        if (getDividendLedger.Rows.Count > 0)
        {
            for (int looper = 0; looper < getDividendLedger.Rows.Count; looper++)
            {
                drdtGrid = dtDividendInfo.NewRow();
                string WarDetails = "";

                drdtGrid["ID"] = getDividendLedger.Rows[looper]["ID"];
                drdtGrid["FY"] = getDividendLedger.Rows[looper]["FY"].ToString().ToUpper() + " : " + getDividendLedger.Rows[looper]["FY_PART"].ToString().ToUpper();
                drdtGrid["ISSUE_DT"] = getDividendLedger.Rows[looper]["ISSUE_DT"];
                drdtGrid["WAR_NO"] = getDividendLedger.Rows[looper]["WAR_NO"];
                drdtGrid["BALANCE"] = getDividendLedger.Rows[looper]["BALANCE"];
                drdtGrid["TOT_DIVI"] = getDividendLedger.Rows[looper]["TOT_DIVI"];
                drdtGrid["DIDUCT"] = getDividendLedger.Rows[looper]["DIDUCT"];
                drdtGrid["FI_DIVI_QTY"] = getDividendLedger.Rows[looper]["FI_DIVI_QTY"];
                drdtGrid["CIP_QTY"] = getDividendLedger.Rows[looper]["CIP_QTY"];
                drdtGrid["IS_BEFTN"] = getDividendLedger.Rows[looper]["IS_BEFTN"];

              


                if (!(getDividendLedger.Rows[looper]["WAR_BK_PAY_DT"].Equals(DBNull.Value)))
                {
                    if (WarDetails == "")
                    {
                        WarDetails = " PAID STATUS: PAID ";
                    }
                    else
                    {
                        WarDetails = WarDetails + "; PAID STATUS: PAID ";
                    }
                }


                if (!(getDividendLedger.Rows[looper]["WARR_RECPT_BY"].Equals(DBNull.Value)))
                {
                    if (WarDetails == "")
                    {
                        WarDetails = "  Delivery to: " + getDividendLedger.Rows[looper]["WARR_RECPT_BY"].ToString().ToUpper();
                    }
                    else
                    {
                        WarDetails = WarDetails + " ; Delivery to: " + getDividendLedger.Rows[looper]["WARR_RECPT_BY"].ToString().ToUpper();
                    }
                }


                if (!(getDividendLedger.Rows[looper]["WAR_DELEVARY_DT"].Equals(DBNull.Value)))
                {
                    if (WarDetails == "")
                    {
                        WarDetails = "Delivery  Date: " + Convert.ToDateTime(getDividendLedger.Rows[looper]["WAR_DELEVARY_DT"]).ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        WarDetails = WarDetails + " ;Delivery Date: " + Convert.ToDateTime(getDividendLedger.Rows[looper]["WAR_DELEVARY_DT"]).ToString("dd-MMM-yyyy");
                    }
                }
                if (!(getDividendLedger.Rows[looper]["POST_DATE"].Equals(DBNull.Value)))
                {
                    if (WarDetails == "")
                    {
                        WarDetails = " POST  Date: " + Convert.ToDateTime(getDividendLedger.Rows[looper]["POST_DATE"]).ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        WarDetails = WarDetails + " ; POST  Date: " + Convert.ToDateTime(getDividendLedger.Rows[looper]["POST_DATE"]).ToString("dd-MMM-yyyy");
                    }

                }
                if (!(getDividendLedger.Rows[looper]["POST_RETURN_DATE"].Equals(DBNull.Value)))
                {
                    if (WarDetails == "")
                    {
                        WarDetails = " POST Return Date: " + Convert.ToDateTime(getDividendLedger.Rows[looper]["POST_RETURN_DATE"]).ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        WarDetails = WarDetails + " ; POST Return Date: " + Convert.ToDateTime(getDividendLedger.Rows[looper]["POST_RETURN_DATE"]).ToString("dd-MMM-yyyy");
                    }

                }
                if (!(getDividendLedger.Rows[looper]["BEFTN_RETURN_DT"].Equals(DBNull.Value)))
                {
                    if (WarDetails == "")
                    {
                        WarDetails = " BEFTN Return Date: " + Convert.ToDateTime(getDividendLedger.Rows[looper]["BEFTN_RETURN_DT"]).ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        WarDetails = WarDetails + " ; BEFTN Return Date: " + Convert.ToDateTime(getDividendLedger.Rows[looper]["BEFTN_RETURN_DT"]).ToString("dd-MMM-yyyy");
                    }

                }


                if (!(getDividendLedger.Rows[looper]["IS_BEFTN"].Equals(DBNull.Value)))
                {
                    if (getDividendLedger.Rows[looper]["IS_BEFTN"].ToString().ToUpper() == "BEFTN  PROCESS")
                    {
                        DataTable dtDividendBankInfo = commonGatewayObj.Select("SELECT * FROM DIVIDEND WHERE ID=" + getDividendLedger.Rows[looper]["ID"]);
                        if (dtDividendBankInfo.Rows.Count > 0)
                        {
                            string bankInfo = "";
                            if (!dtDividendBankInfo.Rows[0]["BK_NM_CD"].Equals(DBNull.Value) && !dtDividendBankInfo.Rows[0]["BK_BR_NM_CD"].Equals(DBNull.Value) && !dtDividendBankInfo.Rows[0]["BK_AC_NO"].Equals(DBNull.Value))
                            {

                                bankInfo = "AC:" + dtDividendBankInfo.Rows[0]["BK_AC_NO"].ToString();
                                bankInfo = bankInfo + " , " + reportObj.getBankNameByBankCode(Convert.ToInt32(dtDividendBankInfo.Rows[0]["BK_NM_CD"].ToString())).ToString();
                                bankInfo = bankInfo + " , " + reportObj.getBankBranchNameByCode(Convert.ToInt32(dtDividendBankInfo.Rows[0]["BK_NM_CD"].ToString()), Convert.ToInt32(dtDividendBankInfo.Rows[0]["BK_BR_NM_CD"].ToString())).ToString();

                                DataTable dtBankBracnhInfo = unitHolderRegBLObj.dtGetBankBracnhInfo(Convert.ToInt32(dtDividendBankInfo.Rows[0]["BK_NM_CD"].ToString()), Convert.ToInt32(dtDividendBankInfo.Rows[0]["BK_BR_NM_CD"].ToString()));
                                if (dtBankBracnhInfo.Rows.Count > 0)
                                {
                                    bankInfo = bankInfo + " Routing No=[" + dtBankBracnhInfo.Rows[0]["ROUTING_NO"].ToString() + "] " + dtBankBracnhInfo.Rows[0]["ADDRESS"].ToString() + " ";
                                }

                            }
                            if (WarDetails == "")
                            {
                                WarDetails = bankInfo;
                            }
                            else
                            {
                                WarDetails = WarDetails +" ; "+ bankInfo;
                            }
                        }

                    }
                }
                drdtGrid["WAR_STATUS"] = WarDetails;

                dtDividendInfo.Rows.Add(drdtGrid);

            }
        }


        return dtDividendInfo; 

    }


    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            commonGatewayObj.BeginTransaction();
            Hashtable htDeliveryInfo = new Hashtable();
            DataTable dtDividendLedger = (DataTable)Session["dtDividendLedger"];
            int countRow = 0;
            if (dtDividendLedger.Rows.Count > 0)
            {
                foreach (DataGridItem gridRow in dgLedger.Items)
                {

                    CheckBox leftCheckBox = (CheckBox)gridRow.FindControl("leftCheckBox");
                    if (leftCheckBox.Checked)
                    {
                        htDeliveryInfo.Add("WAR_DELEVARY_DT", Convert.ToDateTime(dateTextBox.Text.Trim()).ToString("dd-MMM-yyyy"));
                        htDeliveryInfo.Add("WAR_BK_PAY_USER", userObj.UserID.ToString());
                        if (selfRadioButton.Checked)
                        {
                            htDeliveryInfo.Add("WARR_RECPT_BY", "SELF");
                        }
                        else
                        {
                            htDeliveryInfo.Add("WARR_RECPT_BY", authorizeTextBox.Text.Trim().ToString());
                        }

                        commonGatewayObj.Update(htDeliveryInfo, "DIVIDEND", "REG_BK='" + fundCodeDDL.SelectedValue.ToString() + "' AND ID=" + Convert.ToInt32(dtDividendLedger.Rows[countRow]["ID"]) + " AND REG_NO="+ Convert.ToInt32(regNoTextBox.Text.Trim())+" AND WAR_NO=" + Convert.ToInt32(dtDividendLedger.Rows[countRow]["WAR_NO"]));


                    }
                    htDeliveryInfo = new Hashtable();
                    countRow++;
                }

                commonGatewayObj.CommitTransaction();
                dvLedger.Visible = false;
                totalRowCountLabel.Text = "0";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found to Save');", true);
            }
        }
        catch (Exception ex)
        {
            // dvGridAttendenceInfo.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + ex.Message.Replace("'", "").ToString() + "');", true);
        }
    }

  
}
