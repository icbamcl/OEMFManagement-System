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

public partial class UI_UnitCertBookInfo : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();    
    UnitUserBL userBLObj = new UnitUserBL();
    UnitCertBookInfoBL certBookInfoBL = new UnitCertBookInfoBL();
    CommonGateway commonGatewayObj = new CommonGateway();
    Message msgObj = new Message();
    BaseClass bcContent = new BaseClass();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        string fundCode = "";
        string branchCode = "";

        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        fundCode = bcContent.FundCode.ToString();
        branchCode = bcContent.BranchCode.ToString();
        
        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = opendMFDAO.dtFundList();
            fundNameDropDownList.DataTextField = "FUND_NM";
            fundNameDropDownList.DataValueField = "FUND_CD";           
            fundNameDropDownList.DataBind();

            branchNameDropDownList.DataSource = opendMFDAO.dtBranchList();
            branchNameDropDownList.DataTextField = "BR_NM";
            branchNameDropDownList.DataValueField = "BR_CD";            
            branchNameDropDownList.DataBind();
            perCertBookAmtTextBox.Text = "100";
          //  assignBalance(fundCode);            

           
        }
    
    }
    protected void saveButton_Click(object sender, EventArgs e)
    {
        string userID = bcContent.LoginID.ToString();
        Hashtable htInsertBookCertInfo=new Hashtable();
        StringBuilder sbUpdate = new StringBuilder();
        
        try
        {
            commonGatewayObj.BeginTransaction();
            if (ACheckBox.Checked)
            {
                //for Insert Purposes
                int perBookCertAmt = (Convert.ToInt32(ACertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(ACertStartNoTextBox.Text.Trim().ToString()) +1)/ Convert.ToInt32(ANoBooksTextBox.Text.Trim().ToString());
                htInsertBookCertInfo.Add("ID", commonGatewayObj.GetMaxNo("CERT_BOOK_INFO", "ID") + 1);
                if (ReqLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("REQ_NO", ReqLetterNoTextBox.Text.Trim().ToString());
                }
                if (IssueLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("LETTER_NO", IssueLetterNoTextBox.Text.Trim().ToString());
                }
                htInsertBookCertInfo.Add("FUND_CD", fundNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("BR_CD", branchNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("CERT_TYPE", "A");
                htInsertBookCertInfo.Add("DELIV_DT", Convert.ToDateTime(DeliveryDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy"));
                htInsertBookCertInfo.Add("BOOK_NO_START", Convert.ToInt32(ABookStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_END", Convert.ToInt32(ABookEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_START", Convert.ToInt32(ACertStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_END", Convert.ToInt32(ACertEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_OPNING", Convert.ToInt32(ABookNoOpenTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_DISTRIBUTION", Convert.ToInt32(ANoBooksTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_BALANCE", Convert.ToInt32(ABookNoBalanceTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("PER_BOOK_CERT_AMT", perBookCertAmt);
                htInsertBookCertInfo.Add("USER_NM", userID.ToString());
                htInsertBookCertInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htInsertBookCertInfo.Add("ENT_DT", DateTime.Now.ToString("dd-MMM-yyyy"));
                //for Update Purpose
                int issueCertAmt = Convert.ToInt32(ACertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(ACertStartNoTextBox.Text.Trim().ToString()) + 1;             

                sbUpdate.Append("UPDATE CERT_BOOK_STOCK SET BOOK_AMT_ISSUE=NVL(BOOK_AMT_ISSUE,0)+" + Convert.ToInt32(ANoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(", BOOK_AMT_BALANCE=NVL(BOOK_AMT_BALANCE,0)-" + Convert.ToInt32(ANoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(" ,CERT_AMT_ISSUE=NVL(CERT_AMT_ISSUE,0)+" + issueCertAmt);
                sbUpdate.Append(" ,CERT_AMT_BALANCE=NVL(CERT_AMT_BALANCE,0)-" + issueCertAmt);
                sbUpdate.Append(" WHERE CERT_TYPE='A' AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
                commonGatewayObj.ExecuteNonQuery(sbUpdate.ToString());
                commonGatewayObj.Insert(htInsertBookCertInfo, "CERT_BOOK_INFO");

            }
            if (BCheckBox.Checked)
            {
                //for Insert Purposes
                htInsertBookCertInfo = new Hashtable();
                sbUpdate = new StringBuilder();
                int perBookCertAmt = (Convert.ToInt32(BCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(BCertStartNoTextBox.Text.Trim().ToString()) + 1) / Convert.ToInt32(BNoBooksTextBox.Text.Trim().ToString());
                htInsertBookCertInfo.Add("ID", commonGatewayObj.GetMaxNo("CERT_BOOK_INFO", "ID") + 1);
                if (ReqLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("REQ_NO", ReqLetterNoTextBox.Text.Trim().ToString());
                }
                if (IssueLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("LETTER_NO", IssueLetterNoTextBox.Text.Trim().ToString());
                }
                htInsertBookCertInfo.Add("FUND_CD", fundNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("BR_CD", branchNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("CERT_TYPE", "B");
                htInsertBookCertInfo.Add("DELIV_DT", Convert.ToDateTime(DeliveryDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy"));
                htInsertBookCertInfo.Add("BOOK_NO_START", Convert.ToInt32(BBookStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_END", Convert.ToInt32(BBookEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_START", Convert.ToInt32(BCertStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_END", Convert.ToInt32(BCertEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_OPNING", Convert.ToInt32(BBookNoOpenTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_DISTRIBUTION", Convert.ToInt32(BNoBooksTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_BALANCE", Convert.ToInt32(BBookNoBalanceTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("PER_BOOK_CERT_AMT", perBookCertAmt);
                htInsertBookCertInfo.Add("USER_NM", userID.ToString());
                htInsertBookCertInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htInsertBookCertInfo.Add("ENT_DT", DateTime.Now.ToString("dd-MMM-yyyy"));
                //for Update Purpose
                int issueCertAmt = Convert.ToInt32(BCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(BCertStartNoTextBox.Text.Trim().ToString()) + 1;

                sbUpdate.Append(" UPDATE CERT_BOOK_STOCK SET BOOK_AMT_ISSUE=NVL(BOOK_AMT_ISSUE,0)+" + Convert.ToInt32(BNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(", BOOK_AMT_BALANCE=NVL(BOOK_AMT_BALANCE,0)-" + Convert.ToInt32(BNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(" ,CERT_AMT_ISSUE=NVL(CERT_AMT_ISSUE,0)+" + issueCertAmt);
                sbUpdate.Append(" ,CERT_AMT_BALANCE=NVL(CERT_AMT_BALANCE,0)-" + issueCertAmt);
                sbUpdate.Append(" WHERE CERT_TYPE='B' AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
                commonGatewayObj.ExecuteNonQuery(sbUpdate.ToString());
                commonGatewayObj.Insert(htInsertBookCertInfo, "CERT_BOOK_INFO");

            }
            if (CCheckBox.Checked)
            {
                //for Insert Purposes
                htInsertBookCertInfo = new Hashtable();
                sbUpdate = new StringBuilder();
                int perBookCertAmt = (Convert.ToInt32(CCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(CCertStartNoTextBox.Text.Trim().ToString()) + 1) / Convert.ToInt32(CNoBooksTextBox.Text.Trim().ToString());
                htInsertBookCertInfo.Add("ID", commonGatewayObj.GetMaxNo("CERT_BOOK_INFO", "ID") + 1);
                if (ReqLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("REQ_NO", ReqLetterNoTextBox.Text.Trim().ToString());
                }
                if (IssueLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("LETTER_NO", IssueLetterNoTextBox.Text.Trim().ToString());
                }
                htInsertBookCertInfo.Add("FUND_CD", fundNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("BR_CD", branchNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("CERT_TYPE", "C");
                htInsertBookCertInfo.Add("DELIV_DT", Convert.ToDateTime(DeliveryDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy"));
                htInsertBookCertInfo.Add("BOOK_NO_START", Convert.ToInt32(CBookStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_END", Convert.ToInt32(CBookEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_START", Convert.ToInt32(CCertStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_END", Convert.ToInt32(CCertEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_OPNING", Convert.ToInt32(CBookNoOpenTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_DISTRIBUTION", Convert.ToInt32(CNoBooksTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_BALANCE", Convert.ToInt32(CBookNoBalanceTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("PER_BOOK_CERT_AMT", perBookCertAmt);
                htInsertBookCertInfo.Add("USER_NM", userID.ToString());
                htInsertBookCertInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htInsertBookCertInfo.Add("ENT_DT", DateTime.Now.ToString("dd-MMM-yyyy"));
                //for Update Purpose
                int issueCertAmt = Convert.ToInt32(CCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(CCertStartNoTextBox.Text.Trim().ToString()) + 1;

                sbUpdate.Append("UPDATE CERT_BOOK_STOCK SET BOOK_AMT_ISSUE=NVL(BOOK_AMT_ISSUE,0)+" + Convert.ToInt32(CNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(", BOOK_AMT_BALANCE=NVL(BOOK_AMT_BALANCE,0)-" + Convert.ToInt32(CNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(" ,CERT_AMT_ISSUE=NVL(CERT_AMT_ISSUE,0)+" + issueCertAmt);
                sbUpdate.Append(" ,CERT_AMT_BALANCE=NVL(CERT_AMT_BALANCE,0)-" + issueCertAmt);
                sbUpdate.Append(" WHERE CERT_TYPE='C' AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
                commonGatewayObj.ExecuteNonQuery(sbUpdate.ToString());
                commonGatewayObj.Insert(htInsertBookCertInfo, "CERT_BOOK_INFO");

            }
            if (DCheckBox.Checked)
            {
                //for Insert Purposes
                htInsertBookCertInfo = new Hashtable();
                sbUpdate = new StringBuilder();
                int perBookCertAmt = (Convert.ToInt32(DCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(DCertStartNoTextBox.Text.Trim().ToString()) + 1) / Convert.ToInt32(DNoBooksTextBox.Text.Trim().ToString());
                htInsertBookCertInfo.Add("ID", commonGatewayObj.GetMaxNo("CERT_BOOK_INFO", "ID") + 1);
                if (ReqLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("REQ_NO", ReqLetterNoTextBox.Text.Trim().ToString());
                }
                if (IssueLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("LETTER_NO", IssueLetterNoTextBox.Text.Trim().ToString());
                }
                htInsertBookCertInfo.Add("FUND_CD", fundNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("BR_CD", branchNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("CERT_TYPE", "D");
                htInsertBookCertInfo.Add("DELIV_DT", Convert.ToDateTime(DeliveryDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy"));
                htInsertBookCertInfo.Add("BOOK_NO_START", Convert.ToInt32(DBookStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_END", Convert.ToInt32(DBookEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_START", Convert.ToInt32(DCertStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_END", Convert.ToInt32(DCertEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_OPNING", Convert.ToInt32(DBookNoOpenTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_DISTRIBUTION", Convert.ToInt32(DNoBooksTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_BALANCE", Convert.ToInt32(DBookNoBalanceTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("PER_BOOK_CERT_AMT", perBookCertAmt);
                htInsertBookCertInfo.Add("USER_NM", userID.ToString());
                htInsertBookCertInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htInsertBookCertInfo.Add("ENT_DT", DateTime.Now.ToString("dd-MMM-yyyy"));
                //for Update Purpose
                int issueCertAmt = Convert.ToInt32(DCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(DCertStartNoTextBox.Text.Trim().ToString()) + 1;

                sbUpdate.Append("UPDATE CERT_BOOK_STOCK SET BOOK_AMT_ISSUE=NVL(BOOK_AMT_ISSUE,0)+" + Convert.ToInt32(DNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(", BOOK_AMT_BALANCE=NVL(BOOK_AMT_BALANCE,0)-" + Convert.ToInt32(DNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(" ,CERT_AMT_ISSUE=NVL(CERT_AMT_ISSUE,0)+" + issueCertAmt);
                sbUpdate.Append(" ,CERT_AMT_BALANCE=NVL(CERT_AMT_BALANCE,0)-" + issueCertAmt);
                sbUpdate.Append(" WHERE CERT_TYPE='D' AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
                commonGatewayObj.ExecuteNonQuery(sbUpdate.ToString());
                commonGatewayObj.Insert(htInsertBookCertInfo, "CERT_BOOK_INFO");

            }
            if (ECheckBox.Checked)
            {
                //for Insert Purposes
                htInsertBookCertInfo = new Hashtable();
                sbUpdate = new StringBuilder();
                int perBookCertAmt = (Convert.ToInt32(ECertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(ECertStartNoTextBox.Text.Trim().ToString()) + 1) / Convert.ToInt32(ENoBooksTextBox.Text.Trim().ToString());
                htInsertBookCertInfo.Add("ID", commonGatewayObj.GetMaxNo("CERT_BOOK_INFO", "ID") + 1);
                if (ReqLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("REQ_NO", ReqLetterNoTextBox.Text.Trim().ToString());
                }
                if (IssueLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("LETTER_NO", IssueLetterNoTextBox.Text.Trim().ToString());
                }
                htInsertBookCertInfo.Add("FUND_CD", fundNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("BR_CD", branchNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("CERT_TYPE", "E");
                htInsertBookCertInfo.Add("DELIV_DT", Convert.ToDateTime(DeliveryDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy"));
                htInsertBookCertInfo.Add("BOOK_NO_START", Convert.ToInt32(EBookStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_END", Convert.ToInt32(EBookEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_START", Convert.ToInt32(ECertStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_END", Convert.ToInt32(ECertEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_OPNING", Convert.ToInt32(EBookNoOpenTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_DISTRIBUTION", Convert.ToInt32(ENoBooksTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_BALANCE", Convert.ToInt32(EBookNoBalanceTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("PER_BOOK_CERT_AMT", perBookCertAmt);
                htInsertBookCertInfo.Add("USER_NM", userID.ToString());
                htInsertBookCertInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htInsertBookCertInfo.Add("ENT_DT", DateTime.Now.ToString("dd-MMM-yyyy"));
                //for Update Purpose
                int issueCertAmt = Convert.ToInt32(ECertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(ECertStartNoTextBox.Text.Trim().ToString()) + 1;

                sbUpdate.Append("UPDATE CERT_BOOK_STOCK SET BOOK_AMT_ISSUE=NVL(BOOK_AMT_ISSUE,0)+" + Convert.ToInt32(ENoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(", BOOK_AMT_BALANCE=NVL(BOOK_AMT_BALANCE,0)-" + Convert.ToInt32(ENoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(" ,CERT_AMT_ISSUE=NVL(CERT_AMT_ISSUE,0)+" + issueCertAmt);
                sbUpdate.Append(" ,CERT_AMT_BALANCE=NVL(CERT_AMT_BALANCE,0)-" + issueCertAmt);
                sbUpdate.Append(" WHERE CERT_TYPE='E' AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
                commonGatewayObj.ExecuteNonQuery(sbUpdate.ToString());
                commonGatewayObj.Insert(htInsertBookCertInfo, "CERT_BOOK_INFO");

            }
            if (FCheckBox.Checked)
            {
                //for Insert Purposes
                htInsertBookCertInfo = new Hashtable();
                sbUpdate = new StringBuilder();
                int perBookCertAmt = (Convert.ToInt32(FCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(FCertStartNoTextBox.Text.Trim().ToString()) + 1) / Convert.ToInt32(FNoBooksTextBox.Text.Trim().ToString());
                htInsertBookCertInfo.Add("ID", commonGatewayObj.GetMaxNo("CERT_BOOK_INFO", "ID") + 1);
                if (ReqLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("REQ_NO", ReqLetterNoTextBox.Text.Trim().ToString());
                }
                if (IssueLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("LETTER_NO", IssueLetterNoTextBox.Text.Trim().ToString());
                }
                htInsertBookCertInfo.Add("FUND_CD", fundNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("BR_CD", branchNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("CERT_TYPE", "F");
                htInsertBookCertInfo.Add("DELIV_DT", Convert.ToDateTime(DeliveryDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy"));
                htInsertBookCertInfo.Add("BOOK_NO_START", Convert.ToInt32(FBookStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_END", Convert.ToInt32(FBookEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_START", Convert.ToInt32(FCertStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_END", Convert.ToInt32(FCertEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_OPNING", Convert.ToInt32(FBookNoOpenTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_DISTRIBUTION", Convert.ToInt32(FNoBooksTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_BALANCE", Convert.ToInt32(FBookNoBalanceTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("PER_BOOK_CERT_AMT", perBookCertAmt);
                htInsertBookCertInfo.Add("USER_NM", userID.ToString());
                htInsertBookCertInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htInsertBookCertInfo.Add("ENT_DT", DateTime.Now.ToString("dd-MMM-yyyy"));
                //for Update Purpose
                int issueCertAmt = Convert.ToInt32(FCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(FCertStartNoTextBox.Text.Trim().ToString()) + 1;

                sbUpdate.Append("UPDATE CERT_BOOK_STOCK SET BOOK_AMT_ISSUE=NVL(BOOK_AMT_ISSUE,0)+" + Convert.ToInt32(FNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(", BOOK_AMT_BALANCE=NVL(BOOK_AMT_BALANCE,0)-" + Convert.ToInt32(FNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(" ,CERT_AMT_ISSUE=NVL(CERT_AMT_ISSUE,0)+" + issueCertAmt);
                sbUpdate.Append(" ,CERT_AMT_BALANCE=NVL(CERT_AMT_BALANCE,0)-" + issueCertAmt);
                sbUpdate.Append(" WHERE CERT_TYPE='F' AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
                commonGatewayObj.ExecuteNonQuery(sbUpdate.ToString());
                commonGatewayObj.Insert(htInsertBookCertInfo, "CERT_BOOK_INFO");

            }
            if (GCheckBox.Checked)
            {
                //for Insert Purposes
                htInsertBookCertInfo = new Hashtable();
                sbUpdate = new StringBuilder();
                int perBookCertAmt = (Convert.ToInt32(GCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(GCertStartNoTextBox.Text.Trim().ToString()) + 1) / Convert.ToInt32(GNoBooksTextBox.Text.Trim().ToString());
                htInsertBookCertInfo.Add("ID", commonGatewayObj.GetMaxNo("CERT_BOOK_INFO", "ID") + 1);
                if (ReqLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("REQ_NO", ReqLetterNoTextBox.Text.Trim().ToString());
                }
                if (IssueLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("LETTER_NO", IssueLetterNoTextBox.Text.Trim().ToString());
                }
                htInsertBookCertInfo.Add("FUND_CD", fundNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("BR_CD", branchNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("CERT_TYPE", "G");
                htInsertBookCertInfo.Add("DELIV_DT", Convert.ToDateTime(DeliveryDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy"));
                htInsertBookCertInfo.Add("BOOK_NO_START", Convert.ToInt32(GBookStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_END", Convert.ToInt32(GBookEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_START", Convert.ToInt32(GCertStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_END", Convert.ToInt32(GCertEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_OPNING", Convert.ToInt32(GBookNoOpenTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_DISTRIBUTION", Convert.ToInt32(GNoBooksTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_BALANCE", Convert.ToInt32(GBookNoBalanceTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("PER_BOOK_CERT_AMT", perBookCertAmt);
                htInsertBookCertInfo.Add("USER_NM", userID.ToString());
                htInsertBookCertInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htInsertBookCertInfo.Add("ENT_DT", DateTime.Now.ToString("dd-MMM-yyyy"));
                //for Update Purpose
                int issueCertAmt = Convert.ToInt32(GCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(GCertStartNoTextBox.Text.Trim().ToString()) + 1;

                sbUpdate.Append("UPDATE CERT_BOOK_STOCK SET BOOK_AMT_ISSUE=NVL(BOOK_AMT_ISSUE,0)+" + Convert.ToInt32(GNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(", BOOK_AMT_BALANCE=NVL(BOOK_AMT_BALANCE,0)-" + Convert.ToInt32(GNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(" ,CERT_AMT_ISSUE=NVL(CERT_AMT_ISSUE,0)+" + issueCertAmt);
                sbUpdate.Append(" ,CERT_AMT_BALANCE=NVL(CERT_AMT_BALANCE,0)-" + issueCertAmt);
                sbUpdate.Append(" WHERE CERT_TYPE='G' AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
                commonGatewayObj.ExecuteNonQuery(sbUpdate.ToString());
                commonGatewayObj.Insert(htInsertBookCertInfo, "CERT_BOOK_INFO");

            }
            if (HCheckBox.Checked)
            {
                //for Insert Purposes
                htInsertBookCertInfo = new Hashtable();
                sbUpdate = new StringBuilder();
                int perBookCertAmt = (Convert.ToInt32(HCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(HCertStartNoTextBox.Text.Trim().ToString()) + 1) / Convert.ToInt32(HNoBooksTextBox.Text.Trim().ToString());
                htInsertBookCertInfo.Add("ID", commonGatewayObj.GetMaxNo("CERT_BOOK_INFO", "ID") + 1);
                if (ReqLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("REQ_NO", ReqLetterNoTextBox.Text.Trim().ToString());
                }
                if (IssueLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("LETTER_NO", IssueLetterNoTextBox.Text.Trim().ToString());
                }
                htInsertBookCertInfo.Add("FUND_CD", fundNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("BR_CD", branchNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("CERT_TYPE", "H");
                htInsertBookCertInfo.Add("DELIV_DT", Convert.ToDateTime(DeliveryDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy"));
                htInsertBookCertInfo.Add("BOOK_NO_START", Convert.ToInt32(HBookStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_END", Convert.ToInt32(HBookEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_START", Convert.ToInt32(HCertStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_END", Convert.ToInt32(HCertEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_OPNING", Convert.ToInt32(HBookNoOpenTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_DISTRIBUTION", Convert.ToInt32(HNoBooksTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_BALANCE", Convert.ToInt32(HBookNoBalanceTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("PER_BOOK_CERT_AMT", perBookCertAmt);
                htInsertBookCertInfo.Add("USER_NM", userID.ToString());
                htInsertBookCertInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htInsertBookCertInfo.Add("ENT_DT", DateTime.Now.ToString("dd-MMM-yyyy"));
                //for Update Purpose
                int issueCertAmt = Convert.ToInt32(HCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(HCertStartNoTextBox.Text.Trim().ToString()) + 1;

                sbUpdate.Append("UPDATE CERT_BOOK_STOCK SET BOOK_AMT_ISSUE=NVL(BOOK_AMT_ISSUE,0)+" + Convert.ToInt32(HNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(", BOOK_AMT_BALANCE=NVL(BOOK_AMT_BALANCE,0)-" + Convert.ToInt32(HNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(" ,CERT_AMT_ISSUE=NVL(CERT_AMT_ISSUE,0)+" + issueCertAmt);
                sbUpdate.Append(" ,CERT_AMT_BALANCE=NVL(CERT_AMT_BALANCE,0)-" + issueCertAmt);
                sbUpdate.Append(" WHERE CERT_TYPE='H' AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
                commonGatewayObj.ExecuteNonQuery(sbUpdate.ToString());
                commonGatewayObj.Insert(htInsertBookCertInfo, "CERT_BOOK_INFO");

            }
            if (ICheckBox.Checked)
            {
                //for Insert Purposes
                htInsertBookCertInfo = new Hashtable();
                sbUpdate = new StringBuilder();
                int perBookCertAmt = (Convert.ToInt32(ICertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(ICertStartNoTextBox.Text.Trim().ToString()) + 1) / Convert.ToInt32(INoBooksTextBox.Text.Trim().ToString());
                htInsertBookCertInfo.Add("ID", commonGatewayObj.GetMaxNo("CERT_BOOK_INFO", "ID") + 1);
                if (ReqLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("REQ_NO", ReqLetterNoTextBox.Text.Trim().ToString());
                }
                if (IssueLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("LETTER_NO", IssueLetterNoTextBox.Text.Trim().ToString());
                }
                htInsertBookCertInfo.Add("FUND_CD", fundNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("BR_CD", branchNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("CERT_TYPE", "I");
                htInsertBookCertInfo.Add("DELIV_DT", Convert.ToDateTime(DeliveryDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy"));
                htInsertBookCertInfo.Add("BOOK_NO_START", Convert.ToInt32(IBookStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_END", Convert.ToInt32(IBookEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_START", Convert.ToInt32(ICertStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_END", Convert.ToInt32(ICertEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_OPNING", Convert.ToInt32(IBookNoOpenTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_DISTRIBUTION", Convert.ToInt32(INoBooksTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_BALANCE", Convert.ToInt32(IBookNoBalanceTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("PER_BOOK_CERT_AMT", perBookCertAmt);
                htInsertBookCertInfo.Add("USER_NM", userID.ToString());
                htInsertBookCertInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htInsertBookCertInfo.Add("ENT_DT", DateTime.Now.ToString("dd-MMM-yyyy"));
                //for Update Purpose
                int issueCertAmt = Convert.ToInt32(ICertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(ICertStartNoTextBox.Text.Trim().ToString()) + 1;

                sbUpdate.Append("UPDATE CERT_BOOK_STOCK SET BOOK_AMT_ISSUE=NVL(BOOK_AMT_ISSUE,0)+" + Convert.ToInt32(INoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(", BOOK_AMT_BALANCE=NVL(BOOK_AMT_BALANCE,0)-" + Convert.ToInt32(INoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(" ,CERT_AMT_ISSUE=NVL(CERT_AMT_ISSUE,0)+" + issueCertAmt);
                sbUpdate.Append(" ,CERT_AMT_BALANCE=NVL(CERT_AMT_BALANCE,0)-" + issueCertAmt);
                sbUpdate.Append(" WHERE CERT_TYPE='I' AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
                commonGatewayObj.ExecuteNonQuery(sbUpdate.ToString());
                commonGatewayObj.Insert(htInsertBookCertInfo, "CERT_BOOK_INFO");

            }
            if (JCheckBox.Checked)
            {
                //for Insert Purposes
                htInsertBookCertInfo = new Hashtable();
                sbUpdate = new StringBuilder();
                int perBookCertAmt = (Convert.ToInt32(JCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(JCertStartNoTextBox.Text.Trim().ToString()) + 1) / Convert.ToInt32(JNoBooksTextBox.Text.Trim().ToString());
                htInsertBookCertInfo.Add("ID", commonGatewayObj.GetMaxNo("CERT_BOOK_INFO", "ID") + 1);
                if (ReqLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("REQ_NO", ReqLetterNoTextBox.Text.Trim().ToString());
                }
                if (IssueLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("LETTER_NO", IssueLetterNoTextBox.Text.Trim().ToString());
                }
                htInsertBookCertInfo.Add("FUND_CD", fundNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("BR_CD", branchNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("CERT_TYPE", "J");
                htInsertBookCertInfo.Add("DELIV_DT", Convert.ToDateTime(DeliveryDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy"));
                htInsertBookCertInfo.Add("BOOK_NO_START", Convert.ToInt32(JBookStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_END", Convert.ToInt32(JBookEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_START", Convert.ToInt32(JCertStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_END", Convert.ToInt32(JCertEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_OPNING", Convert.ToInt32(JBookNoOpenTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_DISTRIBUTION", Convert.ToInt32(JNoBooksTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_BALANCE", Convert.ToInt32(JBookNoBalanceTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("PER_BOOK_CERT_AMT", perBookCertAmt);
                htInsertBookCertInfo.Add("USER_NM", userID.ToString());
                htInsertBookCertInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htInsertBookCertInfo.Add("ENT_DT", DateTime.Now.ToString("dd-MMM-yyyy"));
                //for Update Purpose
                int issueCertAmt = Convert.ToInt32(JCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(JCertStartNoTextBox.Text.Trim().ToString()) + 1;

                sbUpdate.Append("UPDATE CERT_BOOK_STOCK SET BOOK_AMT_ISSUE=NVL(BOOK_AMT_ISSUE,0)+" + Convert.ToInt32(JNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(", BOOK_AMT_BALANCE=NVL(BOOK_AMT_BALANCE,0)-" + Convert.ToInt32(JNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(" ,CERT_AMT_ISSUE=NVL(CERT_AMT_ISSUE,0)+" + issueCertAmt);
                sbUpdate.Append(" ,CERT_AMT_BALANCE=NVL(CERT_AMT_BALANCE,0)-" + issueCertAmt);
                sbUpdate.Append(" WHERE CERT_TYPE='J' AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
                commonGatewayObj.ExecuteNonQuery(sbUpdate.ToString());
                commonGatewayObj.Insert(htInsertBookCertInfo, "CERT_BOOK_INFO");

            }

            if (KCheckBox.Checked)
            {
                //for Insert Purposes
                htInsertBookCertInfo = new Hashtable();
                sbUpdate = new StringBuilder();
                int perBookCertAmt = (Convert.ToInt32(KCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(KCertStartNoTextBox.Text.Trim().ToString()) + 1) / Convert.ToInt32(KNoBooksTextBox.Text.Trim().ToString());
                htInsertBookCertInfo.Add("ID", commonGatewayObj.GetMaxNo("CERT_BOOK_INFO", "ID") + 1);
                if (ReqLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("REQ_NO", ReqLetterNoTextBox.Text.Trim().ToString());
                }
                if (IssueLetterNoTextBox.Text.Trim().ToString() != "")
                {
                    htInsertBookCertInfo.Add("LETTER_NO", IssueLetterNoTextBox.Text.Trim().ToString());
                }
                htInsertBookCertInfo.Add("FUND_CD", fundNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("BR_CD", branchNameDropDownList.SelectedValue.ToString().ToUpper());
                htInsertBookCertInfo.Add("CERT_TYPE", "K");
                htInsertBookCertInfo.Add("DELIV_DT", Convert.ToDateTime(DeliveryDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy"));
                htInsertBookCertInfo.Add("BOOK_NO_START", Convert.ToInt32(KBookStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_END", Convert.ToInt32(KBookEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_START", Convert.ToInt32(KCertStartNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("CERT_NO_END", Convert.ToInt32(KCertEndNoTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_OPNING", Convert.ToInt32(KBookNoOpenTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_DISTRIBUTION", Convert.ToInt32(KNoBooksTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("BOOK_NO_BALANCE", Convert.ToInt32(KBookNoBalanceTextBox.Text.Trim().ToString()));
                htInsertBookCertInfo.Add("PER_BOOK_CERT_AMT", perBookCertAmt);
                htInsertBookCertInfo.Add("USER_NM", userID.ToString());
                htInsertBookCertInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htInsertBookCertInfo.Add("ENT_DT", DateTime.Now.ToString("dd-MMM-yyyy"));
                //for Update Purpose
                int issueCertAmt = Convert.ToInt32(KCertEndNoTextBox.Text.Trim().ToString()) - Convert.ToInt32(KCertStartNoTextBox.Text.Trim().ToString()) + 1;

                sbUpdate.Append("UPDATE CERT_BOOK_STOCK SET BOOK_AMT_ISSUE=NVL(BOOK_AMT_ISSUE,0)+" + Convert.ToInt32(KNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(", BOOK_AMT_BALANCE=NVL(BOOK_AMT_BALANCE,0)-" + Convert.ToInt32(KNoBooksTextBox.Text.Trim().ToString()));
                sbUpdate.Append(" ,CERT_AMT_ISSUE=NVL(CERT_AMT_ISSUE,0)+" + issueCertAmt);
                sbUpdate.Append(" ,CERT_AMT_BALANCE=NVL(CERT_AMT_BALANCE,0)-" + issueCertAmt);
                sbUpdate.Append(" WHERE CERT_TYPE='K' AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
                commonGatewayObj.ExecuteNonQuery(sbUpdate.ToString());
                commonGatewayObj.Insert(htInsertBookCertInfo, "CERT_BOOK_INFO");

            }

            commonGatewayObj.CommitTransaction();
            assignBalance(fundNameDropDownList.SelectedValue.ToString().ToUpper());
            cleartext();
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('Save Successfuly');", true);
        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + ex.Message.Replace("'", "").ToString() + "');", true);
        }

    }
    protected void regCloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
    }
    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
             
        assignBalance(fundNameDropDownList.SelectedValue.ToString().ToUpper());
        cleartext();
        
    }
    public void assignBalance(string fundCode)
    {
        DataTable dtBookBalance=certBookInfoBL.dtAllBookBalance(fundCode);
       
        if (dtBookBalance.Rows.Count == 11)
        {
            ABookNoOpenTextBox.Text = dtBookBalance.Rows[0]["BOOK_AMT_BALANCE"].Equals(DBNull.Value) ? "0" : dtBookBalance.Rows[0]["BOOK_AMT_BALANCE"].ToString();
            BBookNoOpenTextBox.Text = dtBookBalance.Rows[1]["BOOK_AMT_BALANCE"].Equals(DBNull.Value) ? "0" : dtBookBalance.Rows[1]["BOOK_AMT_BALANCE"].ToString();
            CBookNoOpenTextBox.Text = dtBookBalance.Rows[2]["BOOK_AMT_BALANCE"].Equals(DBNull.Value) ? "0" : dtBookBalance.Rows[2]["BOOK_AMT_BALANCE"].ToString();
            DBookNoOpenTextBox.Text = dtBookBalance.Rows[3]["BOOK_AMT_BALANCE"].Equals(DBNull.Value) ? "0" : dtBookBalance.Rows[3]["BOOK_AMT_BALANCE"].ToString();
            EBookNoOpenTextBox.Text = dtBookBalance.Rows[4]["BOOK_AMT_BALANCE"].Equals(DBNull.Value) ? "0" : dtBookBalance.Rows[4]["BOOK_AMT_BALANCE"].ToString();
            FBookNoOpenTextBox.Text = dtBookBalance.Rows[5]["BOOK_AMT_BALANCE"].Equals(DBNull.Value) ? "0" : dtBookBalance.Rows[5]["BOOK_AMT_BALANCE"].ToString();
            GBookNoOpenTextBox.Text = dtBookBalance.Rows[6]["BOOK_AMT_BALANCE"].Equals(DBNull.Value) ? "0" : dtBookBalance.Rows[6]["BOOK_AMT_BALANCE"].ToString();
            HBookNoOpenTextBox.Text = dtBookBalance.Rows[7]["BOOK_AMT_BALANCE"].Equals(DBNull.Value) ? "0" : dtBookBalance.Rows[7]["BOOK_AMT_BALANCE"].ToString();
            IBookNoOpenTextBox.Text = dtBookBalance.Rows[8]["BOOK_AMT_BALANCE"].Equals(DBNull.Value) ? "0" : dtBookBalance.Rows[8]["BOOK_AMT_BALANCE"].ToString();
            JBookNoOpenTextBox.Text = dtBookBalance.Rows[9]["BOOK_AMT_BALANCE"].Equals(DBNull.Value) ? "0" : dtBookBalance.Rows[9]["BOOK_AMT_BALANCE"].ToString();
            KBookNoOpenTextBox.Text = dtBookBalance.Rows[10]["BOOK_AMT_BALANCE"].Equals(DBNull.Value) ? "0" : dtBookBalance.Rows[10]["BOOK_AMT_BALANCE"].ToString();
           
        }
        else
        {
            ABookNoOpenTextBox.Text = "0";
            BBookNoOpenTextBox.Text = "0";
            CBookNoOpenTextBox.Text = "0";
            DBookNoOpenTextBox.Text = "0";
            EBookNoOpenTextBox.Text = "0";
            FBookNoOpenTextBox.Text = "0";
            GBookNoOpenTextBox.Text = "0";
            HBookNoOpenTextBox.Text = "0";
            IBookNoOpenTextBox.Text = "0";
            JBookNoOpenTextBox.Text = "0";
            KBookNoOpenTextBox.Text = "0";
        }
        DataTable Adata=certBookInfoBL.dtGetNextBookStatrtNo(fundCode, "A");
        if (Adata.Rows.Count > 0)
        {
            ABookStartNoTextBox.Text = Adata.Rows[0]["BOOK_NO_END"].Equals(DBNull.Value) ? "1" :Convert.ToString(Convert.ToInt32(Adata.Rows[0]["BOOK_NO_END"].ToString())+1);
        }
        else
        {
            ABookStartNoTextBox.Text = "1";
        }
        DataTable Bdata = certBookInfoBL.dtGetNextBookStatrtNo(fundCode, "B");
        if (Bdata.Rows.Count > 0)
        {
            BBookStartNoTextBox.Text = Bdata.Rows[0]["BOOK_NO_END"].Equals(DBNull.Value) ? "1" : Convert.ToString(Convert.ToInt32(Bdata.Rows[0]["BOOK_NO_END"].ToString()) + 1);
        }
        else
        {
            BBookStartNoTextBox.Text = "1";
        }
        DataTable Cdata = certBookInfoBL.dtGetNextBookStatrtNo(fundCode, "C");
        if (Cdata.Rows.Count > 0)
        {
            CBookStartNoTextBox.Text = Cdata.Rows[0]["BOOK_NO_END"].Equals(DBNull.Value) ? "1" : Convert.ToString(Convert.ToInt32(Cdata.Rows[0]["BOOK_NO_END"].ToString()) + 1);
        }
        else
        {
            CBookStartNoTextBox.Text = "1";

        }
        DataTable Ddata = certBookInfoBL.dtGetNextBookStatrtNo(fundCode, "D");
        if (Ddata.Rows.Count > 0)
        {
            DBookStartNoTextBox.Text = Ddata.Rows[0]["BOOK_NO_END"].Equals(DBNull.Value) ? "1" : Convert.ToString(Convert.ToInt32(Ddata.Rows[0]["BOOK_NO_END"].ToString()) + 1);
        }
        else
        {
            DBookStartNoTextBox.Text = "1";
        }
        DataTable Edata = certBookInfoBL.dtGetNextBookStatrtNo(fundCode, "E");
        if (Edata.Rows.Count > 0)
        {
            EBookStartNoTextBox.Text = Edata.Rows[0]["BOOK_NO_END"].Equals(DBNull.Value) ? "1" : Convert.ToString(Convert.ToInt32(Edata.Rows[0]["BOOK_NO_END"].ToString()) + 1);
        }
        else
        {
            EBookStartNoTextBox.Text = "1";
        }
        DataTable Fdata = certBookInfoBL.dtGetNextBookStatrtNo(fundCode, "F");
        if (Fdata.Rows.Count > 0)
        {
            FBookStartNoTextBox.Text = Fdata.Rows[0]["BOOK_NO_END"].Equals(DBNull.Value) ? "1" : Convert.ToString(Convert.ToInt32(Fdata.Rows[0]["BOOK_NO_END"].ToString()) + 1);
        }
        else
        {
            FBookStartNoTextBox.Text = "1";
        }
        DataTable Gdata = certBookInfoBL.dtGetNextBookStatrtNo(fundCode, "G");
        if (Gdata.Rows.Count > 0)
        {
            GBookStartNoTextBox.Text = Gdata.Rows[0]["BOOK_NO_END"].Equals(DBNull.Value) ? "1" : Convert.ToString(Convert.ToInt32(Gdata.Rows[0]["BOOK_NO_END"].ToString()) + 1);
        }
        else
        {
            GBookStartNoTextBox.Text = "1";
        }
        DataTable Hdata = certBookInfoBL.dtGetNextBookStatrtNo(fundCode, "H");
        if (Hdata.Rows.Count > 0)
        {
            HBookStartNoTextBox.Text = Hdata.Rows[0]["BOOK_NO_END"].Equals(DBNull.Value) ? "1" : Convert.ToString(Convert.ToInt32(Hdata.Rows[0]["BOOK_NO_END"].ToString()) + 1);
        }
        else
        {
            HBookStartNoTextBox.Text = "1";
        }
        DataTable Idata = certBookInfoBL.dtGetNextBookStatrtNo(fundCode, "I");
        if (Idata.Rows.Count > 0)
        {
            IBookStartNoTextBox.Text = Idata.Rows[0]["BOOK_NO_END"].Equals(DBNull.Value) ? "1" : Convert.ToString(Convert.ToInt32(Idata.Rows[0]["BOOK_NO_END"].ToString()) + 1);
        }
        else
        {
            IBookStartNoTextBox.Text = "1";
        }
        DataTable Jdata = certBookInfoBL.dtGetNextBookStatrtNo(fundCode, "J");
        if (Jdata.Rows.Count > 0)
        {
            JBookStartNoTextBox.Text = Jdata.Rows[0]["BOOK_NO_END"].Equals(DBNull.Value) ? "1" : Convert.ToString(Convert.ToInt32(Jdata.Rows[0]["BOOK_NO_END"].ToString()) + 1);
        }
        else
        {
            JBookStartNoTextBox.Text = "1";
        }
        DataTable Kdata = certBookInfoBL.dtGetNextBookStatrtNo(fundCode, "K");
        if (Kdata.Rows.Count > 0)
        {
            KBookStartNoTextBox.Text = Kdata.Rows[0]["BOOK_NO_END"].Equals(DBNull.Value) ? "1" : Convert.ToString(Convert.ToInt32(Kdata.Rows[0]["BOOK_NO_END"].ToString()) + 1);
        }
        else
        {
            KBookStartNoTextBox.Text = "1";
        }
    }
    public void cleartext()
    {
        branchNameDropDownList.SelectedValue = "0";
        DeliveryDateTextBox.Text = "";
        ReqLetterNoTextBox.Text = "";
        IssueLetterNoTextBox.Text = "";

     
        ACheckBox.Checked = false;
        ABookEndNoTextBox.Text = "";
        ANoBooksTextBox.Text = "";
        ACertStartNoTextBox.Text = "";
        ACertEndNoTextBox.Text = "";
        ABookNoBalanceTextBox.Text = "";

        BCheckBox.Checked = false;
        BBookEndNoTextBox.Text = "";
        BNoBooksTextBox.Text = "";
        BCertStartNoTextBox.Text = "";
        BCertEndNoTextBox.Text = "";
        BBookNoBalanceTextBox.Text = "";

        CCheckBox.Checked = false;
        CBookEndNoTextBox.Text = "";
        CNoBooksTextBox.Text = "";
        CCertStartNoTextBox.Text = "";
        CCertEndNoTextBox.Text = "";
        CBookNoBalanceTextBox.Text = "";

        DCheckBox.Checked = false;
        DBookEndNoTextBox.Text = "";
        DNoBooksTextBox.Text = "";
        DCertStartNoTextBox.Text = "";
        DCertEndNoTextBox.Text = "";
        DBookNoBalanceTextBox.Text = "";

        ECheckBox.Checked = false;
        EBookEndNoTextBox.Text = "";
        ENoBooksTextBox.Text = "";
        ECertStartNoTextBox.Text = "";
        ECertEndNoTextBox.Text = "";
        EBookNoBalanceTextBox.Text = "";

        FCheckBox.Checked = false;
        FBookEndNoTextBox.Text = "";
        FNoBooksTextBox.Text = "";
        FCertStartNoTextBox.Text = "";
        FCertEndNoTextBox.Text = "";
        FBookNoBalanceTextBox.Text = "";

        GCheckBox.Checked = false;
        GBookEndNoTextBox.Text = "";
        GNoBooksTextBox.Text = "";
        GCertStartNoTextBox.Text = "";
        GCertEndNoTextBox.Text = "";
        GBookNoBalanceTextBox.Text = "";

        HCheckBox.Checked = false;
        HBookEndNoTextBox.Text = "";
        HNoBooksTextBox.Text = "";
        HCertStartNoTextBox.Text = "";
        HCertEndNoTextBox.Text = "";
        HBookNoBalanceTextBox.Text = "";

        ICheckBox.Checked = false;
        IBookEndNoTextBox.Text = "";
        INoBooksTextBox.Text = "";
        ICertStartNoTextBox.Text = "";
        ICertEndNoTextBox.Text = "";
        IBookNoBalanceTextBox.Text = "";

        JCheckBox.Checked = false;
        JBookEndNoTextBox.Text = "";
        JNoBooksTextBox.Text = "";
        JCertStartNoTextBox.Text = "";
        JCertEndNoTextBox.Text = "";
        JBookNoBalanceTextBox.Text = "";

        KCheckBox.Checked = false;
        KBookEndNoTextBox.Text = "";
        KNoBooksTextBox.Text = "";
        KCertStartNoTextBox.Text = "";
        KCertEndNoTextBox.Text = "";
        KBookNoBalanceTextBox.Text = "";
    }

}
