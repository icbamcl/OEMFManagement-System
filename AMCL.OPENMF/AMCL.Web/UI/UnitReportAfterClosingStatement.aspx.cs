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

public partial class UI_UnitReportAfterClosingStatement : System.Web.UI.Page
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

            IDbankNameDropDownList.DataSource = opendMFDAO.dtFillBankName(" CATE_CODE IN (2,3) ");
            IDbankNameDropDownList.DataTextField = "BANK_NAME";
            IDbankNameDropDownList.DataValueField = "BANK_CODE";
            IDbankNameDropDownList.DataBind();

            DataTable dtOccupationList = opendMFDAO.dtOccopationList();
            holderOccupationDropDownList.DataSource = dtOccupationList;
            holderOccupationDropDownList.DataTextField = "DESCR";
            holderOccupationDropDownList.DataValueField = "CODE";
            holderOccupationDropDownList.DataBind();
                         
        }
    
    }
   
    
    protected void CloseButton_Click(object sender, EventArgs e)
    {         
        Response.Redirect("UnitHome.aspx");
           
    }

    protected void PrintButton_Click(object sender, EventArgs e)
    {

        string fundCodeStatement = fundNameDropDownList.SelectedValue.ToString();
        string branchCodeStatement = "";
        if (branchNameDropDownList.SelectedValue.ToString() != "0")
        {
            branchCodeStatement = branchNameDropDownList.SelectedValue.ToString();
        }
        string FY = DividendFYDropDownList.SelectedValue.ToString();
        string closingDate = ClosingDateDropDownList.SelectedItem.Text.ToString();

        StringBuilder sbMaster = new StringBuilder();
        StringBuilder sbFilter = new StringBuilder();
        sbMaster.Append(" SELECT  U_MASTER.REG_BK ||'/'|| U_MASTER.REG_BR ||'/'|| U_MASTER.REG_NO AS REGI_NUMBER,   U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, U_MASTER.REG_TYPE, U_MASTER.REG_DT, U_MASTER.HNAME,DECODE(U_MASTER.IS_BEFTN,'Y','YES','NO') AS BEFTN,NVL(U_MASTER.TIN,'') AS TIN,DECODE(U_MASTER.REG_TYPE,'N','INDIVIDUAL','I','INSTITUTE','C','CHARITY','NA') AS REG_TYPE,");
        sbMaster.Append(" U_MASTER.ADDRS1, U_MASTER.ADDRS2, U_MASTER.CITY, U_MASTER.SEX, U_MASTER.OPN_BAL, U_MASTER.BALANCE, U_MASTER.CIP, U_MASTER.ID_FLAG, U_MASTER.ID_AC, U_MASTER.ID_BK_NM_CD, U_MASTER.ID_BK_BR_NM_CD, BANK_BRANCH.BRANCH_NAME,");
        sbMaster.Append("  BANK_NAME.BANK_NAME, U_MASTER.BK_FLAG, U_MASTER.SPEC_IN1, U_MASTER.SPEC_IN2, U_MASTER.CUR_BAL, U_MASTER.OCC_CODE,  U_MASTER.BK_AC_NO, U_MASTER.BK_NM_CD, U_MASTER.BK_BR_NM_CD, U_JHOLDER.JNT_NAME ");
        sbMaster.Append(" FROM  U_JHOLDER RIGHT OUTER JOIN  U_MASTER LEFT OUTER JOIN BANK_NAME INNER JOIN  BANK_BRANCH ON BANK_NAME.BANK_CODE = BANK_BRANCH.BANK_CODE ON U_MASTER.ID_BK_NM_CD = BANK_BRANCH.BANK_CODE AND ");
        sbMaster.Append("  U_MASTER.ID_BK_BR_NM_CD = BANK_BRANCH.BRANCH_CODE ON U_JHOLDER.REG_BK = U_MASTER.REG_BK AND  U_JHOLDER.REG_BR = U_MASTER.REG_BR AND U_JHOLDER.REG_NO = U_MASTER.REG_NO");
        sbMaster.Append(" WHERE U_MASTER.REG_BK='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "'");

        if (branchNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbFilter.Append(" AND U_MASTER.REG_BR='" + branchNameDropDownList.SelectedValue + "'");
        }
        if (fromBalanceTextBox.Text != "" && toBalanceTextBox.Text != "")
        {
            sbFilter.Append(" AND U_MASTER.BALANCE BETWEEN " + fromBalanceTextBox.Text + " AND " + toBalanceTextBox.Text + "");
        }
        else if (fromBalanceTextBox.Text != "" && toBalanceTextBox.Text == "")
        {
            sbFilter.Append(" AND U_MASTER.BALANCE >= " + fromBalanceTextBox.Text + "");
        }
        else if (fromBalanceTextBox.Text == "" && toBalanceTextBox.Text != "")
        {
            sbFilter.Append(" AND U_MASTER.BALANCE <= " + toBalanceTextBox.Text + "");
        }

        if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
        {
            sbFilter.Append(" AND U_MASTER.REG_NO BETWEEN " + fromRegNoTextBox.Text.Trim() + " AND " + toRegNoTextBox.Text.Trim() + "");
        }
        else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
        {
            sbFilter.Append(" AND U_MASTER.REG_NO >= " + fromRegNoTextBox.Text.Trim() + "");
        }
        else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
        {
            sbFilter.Append(" AND U_MASTER.REG_NO <= " + toRegNoTextBox.Text.Trim() + "");
        }
        if (holderNameTextBox.Text != "")
        {
            sbFilter.Append(" AND U_MASTER.HNAME ='" + holderNameTextBox.Text.Trim().ToUpper().ToString() + "'");
        }
        if (holderOccupationDropDownList.SelectedValue != "0")
        {
            sbFilter.Append(" AND U_MASTER.OCC_CODE=" +Convert.ToUInt16( holderOccupationDropDownList.SelectedValue.ToString() )+ "");
        }

        if (MaleRadioButton.Checked)
        {
            sbFilter.Append(" AND U_MASTER.SEX='M'");
        }
        else if (FemaleRadioButton.Checked)
        {
            sbFilter.Append(" AND U_MASTER.SEX='F'");
        }
        if (NonCIPRadioButton.Checked)
        {
            sbFilter.Append(" AND U_MASTER.CIP='N'");
        }
        else if (CIPRadioButton.Checked)
        {
            sbFilter.Append(" AND U_MASTER.CIP='Y'");            
        }
        if (BEFTNRadioButton.Checked)
        {
            sbMaster.Append(" AND (DIVIDEND.IS_BEFTN='Y') ");
        }
        else if (NONBEFTNRadioButton.Checked)
        {
            sbMaster.Append(" AND (DIVIDEND.IS_BEFTN='N') ");
        }
        if (NonIDRadioButton.Checked)
        {
            sbFilter.Append(" AND U_MASTER.ID_FLAG='N'");
        }
        else if (IDRadioButton.Checked)
        {
            sbFilter.Append(" AND U_MASTER.ID_FLAG='Y'");

            if (IDbankNameDropDownList.SelectedValue != "0")
            {
                sbFilter.Append(" AND U_MASTER.ID_BK_NM_CD=" + IDbankNameDropDownList.SelectedValue.ToString()+ "");
                if (IDbranchNameDropDownList.SelectedValue != "0")
                {
                    sbFilter.Append(" AND U_MASTER.ID_BK_BR_NM_CD=" + IDbranchNameDropDownList.SelectedValue.ToString() + "");
                }
            }
            
        }
        sbMaster.Append(sbFilter.ToString());
        sbMaster.Append(" ORDER BY U_MASTER.REG_NO ");

        DataTable dtStatementAfterClosing=commonGatewayObj.Select(sbMaster.ToString());
        if (dtStatementAfterClosing.Rows.Count > 0)
        {
            Session["dtStatementAfterClosing"] = dtStatementAfterClosing;
            Session["FY"] = FY;
            Session["fundCodeStatement"] = fundCodeStatement;
            Session["closingDate"] = closingDate;
            Session["branchCodeStatement"] = branchCodeStatement;
            ClientScript.RegisterStartupScript(this.GetType(), "UnitHolderInfoAfterClosing", "window.open('ReportViewer/UnitReportStatementAfterClosingReportViewer.aspx')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "popup", "alert('No Data Found')", true);
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
    }
    protected void IDbankNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDbranchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(IDbankNameDropDownList.SelectedValue.ToString()));
        IDbranchNameDropDownList.DataTextField = "BRANCH_NAME";
        IDbranchNameDropDownList.DataValueField = "BRANCH_CODE";
        IDbranchNameDropDownList.DataBind();
    }
    protected void IDRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (IDRadioButton.Checked)
        {
            IDbankNameDropDownList.Enabled = true;
            IDbranchNameDropDownList.Enabled = true;
        }
        else
        {
            IDbankNameDropDownList.SelectedValue = "0";
            IDbranchNameDropDownList.SelectedValue = "0";
            IDbankNameDropDownList.Enabled = false;
            IDbranchNameDropDownList.Enabled = false;
        }
    }
    protected void NonIDRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (NonIDRadioButton.Checked)
        {
            IDbankNameDropDownList.SelectedValue = "0";
            IDbranchNameDropDownList.SelectedValue = "0";
            IDbankNameDropDownList.Enabled = false;
            IDbranchNameDropDownList.Enabled = false;
            
        }
        else
        {
            IDbankNameDropDownList.Enabled = true;
            IDbranchNameDropDownList.Enabled = true;
        }
    }
    protected void AllIDRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (NonIDRadioButton.Checked)
        {
            IDbankNameDropDownList.SelectedValue = "0";
            IDbranchNameDropDownList.SelectedValue = "0";
            IDbankNameDropDownList.Enabled = false;
            IDbranchNameDropDownList.Enabled = false;

        }
        else
        {
            IDbankNameDropDownList.Enabled = true;
            IDbranchNameDropDownList.Enabled = true;
        }
    }
}
