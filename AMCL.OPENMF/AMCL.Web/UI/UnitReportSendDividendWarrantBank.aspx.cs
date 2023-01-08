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

public partial class UI_UnitReportSendDividendWarrantBank : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();
    dividendDAO diviDAOObj = new dividendDAO();

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
        
       // spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
//            fundCodeTextBox.Text = fundCode.ToString();
//            branchCodeTextBox.Text = branchCode.ToString();

        
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

            //bankNameDropDownList.DataSource = dtFillBankName(" CATE_CODE=1 ");
            //bankNameDropDownList.DataTextField = "BANK_NAME";
            //bankNameDropDownList.DataValueField = "BANK_CODE";
            //bankNameDropDownList.DataBind();

            SignatoryDropDownList.DataSource = reportObj.dtFillSignatory();
            SignatoryDropDownList.DataTextField = "NAME";
            SignatoryDropDownList.DataValueField = "ID";
            SignatoryDropDownList.DataBind();
                         
        }
    
    }
   
    
    protected void CloseButton_Click(object sender, EventArgs e)
    {         
        Response.Redirect("UnitHome.aspx");
           
    }

    protected void PrintButton_Click(object sender, EventArgs e)
    {
        
        string fundCode = fundNameDropDownList.SelectedValue.ToString();
        StringBuilder sbMaster = new StringBuilder();
        StringBuilder sbFilter = new StringBuilder();
        DataTable dtDividend = new DataTable();
        string FY = DividendFYDropDownList.SelectedValue.ToString();
        string closingDate=ClosingDateDropDownList.SelectedItem.Text.ToString();
      //ORDER BY DIVIDEND.WAR_NO 



        sbMaster.Append("SELECT U_MASTER.REG_BK || '/' || U_MASTER.REG_BR || '/' || U_MASTER.REG_NO AS REGI_NO, LPAD(DIVIDEND.WAR_NO, 7, '0') AS WAR_NO,U_MASTER.HNAME, ");
        sbMaster.Append(" DIVIDEND.REG_BK, DIVIDEND.REG_BR, DIVIDEND.REG_NO, DIVIDEND.BK_NM_CD, DIVIDEND.BK_BR_NM_CD, DIVIDEND.BK_AC_NO, DIVIDEND.FY, ");
        sbMaster.Append(" DIVIDEND.CLOSE_DT, DIVIDEND.TOT_DIVI, DIVIDEND.DIDUCT, DIVIDEND.FI_DIVI_QTY, DIVIDEND.CIP_QTY, DIVIDEND.BALANCE, DIVIDEND.BK_FLAG, ");
        sbMaster.Append(" DIVIDEND.CIP_RATE, DIVIDEND.DIVI_RATE, BANK_NAME.BANK_NAME, BANK_BRANCH.BRANCH_NAME, BANK_BRANCH.BRANCH_ADDRS1, ");
        sbMaster.Append(" BANK_BRANCH.BRANCH_ADDRS2, BANK_BRANCH.BRANCH_POST_CODE, BANK_BRANCH.BRANCH_DISTRICT FROM BANK_NAME INNER JOIN ");
        sbMaster.Append(" U_MASTER INNER JOIN DIVIDEND ON U_MASTER.REG_BK = DIVIDEND.REG_BK AND U_MASTER.REG_BR = DIVIDEND.REG_BR AND U_MASTER.REG_NO = DIVIDEND.REG_NO INNER JOIN ");
        sbMaster.Append(" BANK_BRANCH ON DIVIDEND.BK_NM_CD = BANK_BRANCH.BANK_CODE AND DIVIDEND.BK_BR_NM_CD = BANK_BRANCH.BRANCH_CODE ON ");
        sbMaster.Append(" BANK_NAME.BANK_CODE = BANK_BRANCH.BANK_CODE  WHERE 1=1 ");
        sbMaster.Append(" AND (DIVIDEND.FY ='" + DividendFYDropDownList.SelectedValue.ToString() + "' )");
        sbMaster.Append(" AND (DIVIDEND.CLOSE_DT ='" + ClosingDateDropDownList.SelectedItem.ToString() + "' )");
        sbMaster.Append(" AND (DIVIDEND.REG_BK ='" +fundNameDropDownList.SelectedValue.ToString() + "' )");
       


        if (branchNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbFilter.Append(" AND DIVIDEND.REG_BR='" + branchNameDropDownList.SelectedValue.ToString() + "'");
        }
        if (bankNameDropDownList.SelectedValue != "0")
        {
            sbFilter.Append(" AND DIVIDEND.BK_NM_CD="+Convert.ToInt32(bankNameDropDownList.SelectedValue.ToString())+" ");
        }
        if (bankbranchNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbFilter.Append(" AND DIVIDEND.BK_BR_NM_CD=" + Convert.ToInt32(bankbranchNameDropDownList.SelectedValue.ToString()) + " ");
        }

        if (War_NoTextBox.Text != "" )
        {
            sbFilter.Append(" AND TO_NUMBER(DIVIDEND.WAR_NO) IN (" + War_NoTextBox.Text.Trim() + ")");
        }


        if (RegNoTextBox.Text != "")
        {
            sbFilter.Append(" AND TO_NUMBER(DIVIDEND.WAR_NO) IN (" + RegNoTextBox.Text.Trim() + ")");
        }

        
        sbMaster.Append(sbFilter.ToString());
        dtDividend = commonGatewayObj.Select(sbMaster.ToString());
        if (dtDividend.Rows.Count > 0)
        {

            Session["dtDividend"] = dtDividend;
            Session["signatory"] = SignatoryDropDownList.SelectedItem.Text.ToString();
            Session["designation"] = DesignationDropDownList.SelectedItem.Text.ToString();
            Session["fundName"] = fundNameDropDownList.SelectedItem.Text.ToString();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UnitHolderLedgerReport", "window.open('ReportViewer/UnitReportSendingDividendBankReportViewer.aspx')", true);
        }
        else
        {

            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data Found');", true);
        }
        
        
       
    }
    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DividendFYDropDownList.DataSource = diviDAOObj.dtGetFundWiseFY(fundNameDropDownList.SelectedValue.ToString().ToUpper());
        DividendFYDropDownList.DataTextField = "F_YEAR";
        DividendFYDropDownList.DataValueField = "F_YEAR";
        DividendFYDropDownList.DataBind();

    }
    protected void DividendFYDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClosingDateDropDownList.DataSource = diviDAOObj.dtGetFYWiseClosinDate(DividendFYDropDownList.SelectedItem.Text.ToString(),fundNameDropDownList.SelectedValue.ToString().ToUpper());
        ClosingDateDropDownList.DataTextField = "CLOSE_DT";
        ClosingDateDropDownList.DataValueField = "DIVI_NO";
        ClosingDateDropDownList.DataBind();

        bankNameDropDownList.DataSource = dtFillBankName(" CATE_CODE=1 ");
        bankNameDropDownList.DataTextField = "BANK_NAME";
        bankNameDropDownList.DataValueField = "BANK_CODE";
        bankNameDropDownList.DataBind();

    }
    protected void bankNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        bankbranchNameDropDownList.DataSource = dtFillBranchName(Convert.ToInt32(bankNameDropDownList.SelectedValue.ToString()));
        bankbranchNameDropDownList.DataTextField = "BRANCH_NAME";
        bankbranchNameDropDownList.DataValueField = "BRANCH_CODE";
        bankbranchNameDropDownList.DataBind();

    }

    public DataTable dtFillBankName(string filter)
    {
        DataTable dtBankName = commonGatewayObj.Select("SELECT BANK_CODE , BANK_NAME FROM BANK_NAME WHERE 1=1 AND " + filter.ToString() + " AND BANK_CODE IN (SELECT DISTINCT BK_NM_CD FROM DIVIDEND WHERE REG_BK='" + fundNameDropDownList.SelectedValue.ToString() + "' AND FY='" + DividendFYDropDownList.SelectedValue.ToString() + "'   AND (BK_NM_CD IS NOT NULL) )  ORDER BY BANK_NAME ");
        DataTable dtBankNameDropDown = new DataTable();
        dtBankNameDropDown.Columns.Add("BANK_CODE", typeof(string));
        dtBankNameDropDown.Columns.Add("BANK_NAME", typeof(string));

        DataRow drBankNameDropDown = dtBankNameDropDown.NewRow();
        drBankNameDropDown["BANK_NAME"] = "--Select Bank--- ";
        drBankNameDropDown["BANK_CODE"] = "0";
        dtBankNameDropDown.Rows.Add(drBankNameDropDown);
        for (int loop = 0; loop < dtBankName.Rows.Count; loop++)
        {
            drBankNameDropDown = dtBankNameDropDown.NewRow();
            drBankNameDropDown["BANK_NAME"] = dtBankName.Rows[loop]["BANK_NAME"].ToString();
            drBankNameDropDown["BANK_CODE"] = dtBankName.Rows[loop]["BANK_CODE"].ToString();
            dtBankNameDropDown.Rows.Add(drBankNameDropDown);
        }

        return dtBankNameDropDown;
    }
    public DataTable dtFillBranchName(int bankCode)
    {
        DataTable dtBankName = commonGatewayObj.Select("SELECT BRANCH_CODE , BRANCH_NAME FROM BANK_BRANCH where bank_code=" + bankCode + " AND BRANCH_CODE IN (SELECT DISTINCT BK_BR_NM_CD FROM DIVIDEND WHERE  REG_BK='" + fundNameDropDownList.SelectedValue.ToString() + "' AND FY='" + DividendFYDropDownList.SelectedValue.ToString() + "'  AND (BK_BR_NM_CD IS NOT NULL) ) ORDER BY BRANCH_NAME");
        DataTable dtBankNameDropDown = new DataTable();
        dtBankNameDropDown.Columns.Add("BRANCH_CODE", typeof(string));
        dtBankNameDropDown.Columns.Add("BRANCH_NAME", typeof(string));

        DataRow drBankNameDropDown = dtBankNameDropDown.NewRow();
        drBankNameDropDown["BRANCH_NAME"] = "--Select Branch--- ";
        drBankNameDropDown["BRANCH_CODE"] = "0";
        dtBankNameDropDown.Rows.Add(drBankNameDropDown);
        for (int loop = 0; loop < dtBankName.Rows.Count; loop++)
        {
            drBankNameDropDown = dtBankNameDropDown.NewRow();
            drBankNameDropDown["BRANCH_NAME"] = dtBankName.Rows[loop]["BRANCH_NAME"].ToString();
            drBankNameDropDown["BRANCH_CODE"] = dtBankName.Rows[loop]["BRANCH_CODE"].ToString();
            dtBankNameDropDown.Rows.Add(drBankNameDropDown);
        }

        return dtBankNameDropDown;
    }
}
