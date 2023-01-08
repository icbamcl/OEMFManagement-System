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
using CrystalDecisions.Shared;
using System.IO;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;
using System.Text;

public partial class UI_UnitReportLedger : System.Web.UI.Page
{
   // System.Web.UI.Page this_page_ref = null;
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    Message msgObj = new Message();
    EncryptDecrypt encrypt = new EncryptDecrypt();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();
    UnitLienBl unitLienBLObj = new UnitLienBl();
    UnitSIPBL unitSIPBLObj = new UnitSIPBL();
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
        tdEmail.InnerHtml = "";
        Nominee1NameTextBox.Text = "";
        Nominee2NameTextBox.Text = "";
        BankInfoTextBox.Text = "";       
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
        TotalUnitHoldingTextBox.Text = "";
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
            regObj.RegNumber =dtValidSearch.Rows[0]["REG_NO"].ToString();
            displayRegInfo(regObj);
        }
        else
        {
            SignImage.ImageUrl =encrypt.PhotoBase64ImgSrc( Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
            ClearText();
            dvLedger.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Invalid Registration Number OR BO OR Foilo');", true);
        }


       
        
    
    }

    public void displayRegInfo(UnitHolderRegistration unitRegObj)
    {
        dvLedger.Visible = true;


        DataTable dtRegInfo = opendMFDAO.getDtRegInfo(unitRegObj);
        DataTable dtNominee = opendMFDAO.dtNomineeRegInfo(unitRegObj);
       
        if (dtRegInfo.Rows.Count > 0)
        {
            //Trasaction Lock Status 

            if (!(dtRegInfo.Rows[0]["ALL_LOCK"].Equals(DBNull.Value) || (dtRegInfo.Rows[0]["ALL_LOCK"].ToString() == "N")) || !(dtRegInfo.Rows[0]["SL_LOCK"].Equals(DBNull.Value) || (dtRegInfo.Rows[0]["SL_LOCK"].ToString() == "N")) || !(dtRegInfo.Rows[0]["REP_LOCK"].Equals(DBNull.Value) || (dtRegInfo.Rows[0]["REP_LOCK"].ToString() == "N")) || !(dtRegInfo.Rows[0]["TR_LOCK"].Equals(DBNull.Value) || (dtRegInfo.Rows[0]["TR_LOCK"].ToString() == "N")) || !(dtRegInfo.Rows[0]["LIEN_LOCK"].Equals(DBNull.Value) || (dtRegInfo.Rows[0]["LIEN_LOCK"].ToString() == "N")) || !(dtRegInfo.Rows[0]["REN_LOCK"].Equals(DBNull.Value)|| (dtRegInfo.Rows[0]["REN_LOCK"].ToString() == "N")) || !(dtRegInfo.Rows[0]["LOCK_REMARKS"].Equals(DBNull.Value)))
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
            holderTelphoneTextBox.Text = dtRegInfo.Rows[0]["MOBILE1"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["MOBILE1"].ToString();
            tdTIN.InnerHtml = dtRegInfo.Rows[0]["TIN"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["TIN"].ToString();
            tdEmail.InnerHtml = dtRegInfo.Rows[0]["EMAIL"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["EMAIL"].ToString();
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
           // RemarksTextBox.Text = dtRegInfo.Rows[0]["REMARKS"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["REMARKS"].ToString();
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

            DataTable dtLedger = dtgetLedger(unitRegObj);

            if(dtLedger.Rows.Count>0)
            {
                dvLedger.Visible = true;
                TotalLienUnitHoldingTextBox.Text = unitLienBLObj.totalLienAmount(unitRegObj).ToString();
                TotalUnitHoldingTextBox.Text = dtLedger.Rows[dtLedger.Rows.Count-1]["BALANCE"].ToString();
                dgLedger.DataSource = dtLedger;
                dgLedger.DataBind();                
                displaySign();
            }
            else
            {
                ClearText();
                SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
                dvLedger.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Ledger Balance Data Found');", true);
            }
         
        }

        else
        {
                SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
                dvLedger.Visible = false;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", " window.fnResetAll();", true);
                tdCIP.InnerHtml = "";
            }
        }
    public DataTable dtgetLedger(UnitHolderRegistration unitRegObj)
    {
        DataTable dtLedger = reportObj.GetLedgerData(unitRegObj);
        DataTable dtLedgerForReport = reportObj.GetDtLedgerTable();
        int balance = 0;
        if (dtLedger.Rows.Count > 0)
        {
          
            DataRow drLedgerForReport;
            for (int looper = 0; looper < dtLedger.Rows.Count; looper++)
            {
                drLedgerForReport = dtLedgerForReport.NewRow();
                drLedgerForReport["SI"] = looper + 1;
                if (!dtLedger.Rows[looper]["TRANS_DATE"].Equals(DBNull.Value))
                {
                    drLedgerForReport["TRANS_DATE"] = Convert.ToDateTime(dtLedger.Rows[looper]["TRANS_DATE"].ToString()).ToString("dd-MMM-yyyy");
                }
                
                drLedgerForReport["TRANS_NO"] = Convert.ToInt32(dtLedger.Rows[looper]["TRANS_NO"].Equals(DBNull.Value) ? "0" : dtLedger.Rows[looper]["TRANS_NO"].ToString());
                //drLedgerForReport["TRANS_NO"] = Convert.ToInt32(dtLedgerForReport.Rows[looper]["TRANS_DATE"].Equals(DBNull.Value) ? "0" : dtLedgerForReport.Rows[looper]["TRANS_DATE"].ToString());
                if (!dtLedger.Rows[looper]["TRANS_TYPE"].Equals(DBNull.Value))
                {

                    drLedgerForReport["TRANS_TYPE"] = dtLedger.Rows[looper]["TRANS_TYPE"].ToString();
                    if ((string.Compare(dtLedger.Rows[looper]["TRANS_TYPE"].ToString(), "SL", true) == 0) || (string.Compare(dtLedger.Rows[looper]["TRANS_TYPE"].ToString(), "CIP", true) == 0) || (string.Compare(dtLedger.Rows[looper]["TRANS_TYPE"].ToString(), "SIP", true) == 0))
                    {
                        drLedgerForReport["UNIT_CREDIT"] = Convert.ToInt32(dtLedger.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0" : dtLedger.Rows[looper]["QTY"].ToString());
                        drLedgerForReport["RATE"] = decimal.Parse((dtLedger.Rows[looper]["RATE"].ToString()));

                        int saleBalance = Convert.ToInt32(dtLedger.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0" : dtLedger.Rows[looper]["QTY"].ToString());
                        balance = balance + saleBalance;
                        drLedgerForReport["BALANCE"] = balance;
                    }
                    else if (string.Compare(dtLedger.Rows[looper]["TRANS_TYPE"].ToString(), "TRI", true) == 0)
                    {
                        drLedgerForReport["UNIT_CREDIT"] = Convert.ToInt32(dtLedger.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0" : dtLedger.Rows[looper]["QTY"].ToString());
                        //drLedgerForReport["RATE"] = Convert.ToInt32(dtLedgerForReport.Rows[looper]["RATE"].ToString());
                        int transferInBalance = Convert.ToInt32(dtLedger.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0" : dtLedger.Rows[looper]["QTY"].ToString());
                        balance = balance + transferInBalance;
                        drLedgerForReport["BALANCE"] = balance;
                    }
                    else if (string.Compare(dtLedger.Rows[looper]["TRANS_TYPE"].ToString(), "TRO", true) == 0)
                    {
                        drLedgerForReport["UNIT_DEBIT"] = Convert.ToInt32(dtLedger.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0" : dtLedger.Rows[looper]["QTY"].ToString());
                        //drLedgerForReport["RATE"] = Convert.ToInt32(dtLedgerForReport.Rows[looper]["RATE"].ToString());
                        int transferOutBalance = Convert.ToInt32(dtLedger.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0" : dtLedger.Rows[looper]["QTY"].ToString());
                        balance = balance - transferOutBalance;
                        drLedgerForReport["BALANCE"] = balance;
                    }
                    else if (string.Compare(dtLedger.Rows[looper]["TRANS_TYPE"].ToString(), "DTI", true) == 0)
                    {
                        drLedgerForReport["UNIT_CREDIT"] = Convert.ToInt32(dtLedger.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0" : dtLedger.Rows[looper]["QTY"].ToString());
                        //drLedgerForReport["RATE"] = Convert.ToInt32(dtLedgerForReport.Rows[looper]["RATE"].ToString());
                        int transferInBalance = Convert.ToInt32(dtLedger.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0" : dtLedger.Rows[looper]["QTY"].ToString());
                        balance = balance + transferInBalance;
                        drLedgerForReport["BALANCE"] = balance;
                    }
                    else if (string.Compare(dtLedger.Rows[looper]["TRANS_TYPE"].ToString(), "DTO", true) == 0)
                    {
                        drLedgerForReport["UNIT_DEBIT"] = Convert.ToInt32(dtLedger.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0" : dtLedger.Rows[looper]["QTY"].ToString());
                        //drLedgerForReport["RATE"] = Convert.ToInt32(dtLedgerForReport.Rows[looper]["RATE"].ToString());
                        int transferOutBalance = Convert.ToInt32(dtLedger.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0" : dtLedger.Rows[looper]["QTY"].ToString());
                        balance = balance - transferOutBalance;
                        drLedgerForReport["BALANCE"] = balance;
                    }
                    else if (string.Compare(dtLedger.Rows[looper]["TRANS_TYPE"].ToString(), "REP", true) == 0)
                    {
                        drLedgerForReport["UNIT_DEBIT"] = Convert.ToInt32(dtLedger.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0" : dtLedger.Rows[looper]["QTY"].ToString());
                        drLedgerForReport["RATE"] = decimal.Parse((dtLedger.Rows[looper]["RATE"].ToString()));
                        int repurchaseBalance = Convert.ToInt32(dtLedger.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0" : dtLedger.Rows[looper]["QTY"].ToString());
                        balance = balance - repurchaseBalance;
                        drLedgerForReport["BALANCE"] = balance;
                    }
                    if (!dtLedger.Rows[looper]["FRAC_AMT"].Equals(DBNull.Value))
                    {
                        drLedgerForReport["FRAC_AMT"] = dtLedger.Rows[looper]["FRAC_AMT"].ToString();
                    }
                }
                dtLedgerForReport.Rows.Add(drLedgerForReport);
            }
            
        }
        return dtLedgerForReport;
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
            SignImage.ImageUrl =  encrypt.PhotoBase64ImgSrc(imageSignLocation.ToString());
        }
        else
        {
            SignImage.ImageUrl = encrypt.PhotoBase64ImgSrc(Path.Combine(ConfigReader.SingLocation, "Notavailable.JPG").ToString());
        }

       
    }

  
    protected void PrintReportButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
        regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
        regObj.RegNumber = regNoTextBox.Text.Trim().ToString();
        string sipHolder = "NO";
        DataTable dtLedger = dtgetLedger(regObj);
        Session["dtLedgerForReport"] = dtLedger;
        Session["fundCode"] = fundCodeDDL.SelectedValue.ToString();
        Session["branchCode"] = branchCodeDDL.SelectedValue.ToString();
        Session["regiNo"] = regNoTextBox.Text.Trim().ToString();
        if(unitSIPBLObj.isSIPHolder(regObj))
        {
            sipHolder = "YES";
        }
        Session["sipHolder"] = sipHolder;

        ClientScript.RegisterStartupScript(this.GetType(), "UnitHolderLedgerReport", "window.open('ReportViewer/UnitReportLedgerReportViewer.aspx')", true);

    }


    protected void PrintRepButton_Click(object sender, EventArgs e)
    {
        try
        {

            AMCL.REPORT.CR_DemateREP_TR DemateREP_TR = new AMCL.REPORT.CR_DemateREP_TR();

            UnitHolderRegistration regObj = new UnitHolderRegistration();
            regObj.FundCode = fundCodeDDL.SelectedValue.ToString();
            regObj.BranchCode = branchCodeDDL.SelectedValue.ToString();
            regObj.RegNumber = regNoTextBox.Text.Trim().ToString();
            StringBuilder sbMaster = new StringBuilder();
            DataTable dtReportStatement = new DataTable();

            sbMaster.Append("SELECT A.FUND_CD, A.FUND_NM,A.ISIN_NO,UPPER(D.DP_NAME) AS CUST_DP_NAME, D.DP_ID CUST_DP_ID,A.REP_BO_NAME AS SALE_BO_NAME,A.REPURCHASE_OF_UNIT_BO AS SALE_OF_UNIT_BO,"+TotalUnitHoldingTextBox.Text+ " AS QTY,UPPER(E.DP_NAME) AS HOLDER_DP_NAME,B.HOLDER_DP_ID,B.HNAME,B.HOLDER_BO,'REP_REG_NO:'||B.REG_BK||'/'||B.REG_BR||'/'||REG_NO AS DRF_REF_NO ");
            sbMaster.Append(" FROM  (SELECT FUND_CD, UPPER(FUND_INFO.FUND_NM) AS FUND_NM , ISIN_NO,REP_BO_NAME,REPURCHASE_OF_UNIT_BO, CUST_DP_ID FROM FUND_INFO ) A ");
            sbMaster.Append(" INNER JOIN ( SELECT DP_ID,DP_NAME FROM AMCL_DIVIDEND.CDBL_DP_LIST) D ON A.CUST_DP_ID=D.DP_ID  INNER JOIN ( SELECT REG_BK,REG_BR,REG_NO,HNAME,BO AS HOLDER_BO,SUBSTR(BO, 4, 5) AS HOLDER_DP_ID FROM U_MASTER) B ");
            sbMaster.Append(" ON B.REG_BK=A.FUND_CD  INNER JOIN ( SELECT DP_ID,DP_NAME FROM AMCL_DIVIDEND.CDBL_DP_LIST) E  ON B.HOLDER_DP_ID=E.DP_ID ");
            sbMaster.Append(" WHERE A.FUND_CD='" + regObj.FundCode + "' AND B.REG_BR='" + regObj.BranchCode + "' AND B.REG_NO=" + regObj.RegNumber );

            dtReportStatement = commonGatewayObj.Select(sbMaster.ToString());
            dtReportStatement.TableName = "dtUnitDemateSALE_TR";
            DemateREP_TR.Refresh();
            DemateREP_TR.SetDataSource(dtReportStatement);
            DemateREP_TR.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "REP_TR_REQ" + regObj.FundCode + "_" + regObj.BranchCode + "_" + regObj.RegNumber + ".pdf");




        }
        catch (Exception ex)
        {

            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + ex.Message.Replace("'", "").ToString() + "');", true);
        }
    }
}
