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

public partial class UI_UnitReportDividendStatement : System.Web.UI.Page
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

            IDbankNameDropDownList.DataSource = opendMFDAO.dtFillBankName("  CATE_CODE IN (2,3) ");
            IDbankNameDropDownList.DataTextField = "BANK_NAME";
            IDbankNameDropDownList.DataValueField = "BANK_CODE";
            IDbankNameDropDownList.DataBind();

            branchGroupDropDownList.DataSource = reportObj.dtFillBranchGroup();
            branchGroupDropDownList.DataTextField = "BR_SHORT_NM";
            branchGroupDropDownList.DataValueField = "ID";
            branchGroupDropDownList.DataBind();
                         
        }
    
    }
   
    
    protected void CloseButton_Click(object sender, EventArgs e)
    {         
        Response.Redirect("UnitHome.aspx");
           
    }

    protected void PrintButton_Click(object sender, EventArgs e)
     {
        string statementType = "";        
        string investorType = "";
        string investmentType = "ALL";
        string fundCode = fundNameDropDownList.SelectedValue.ToString();
        string FY = DividendFYDropDownList.SelectedValue.ToString();
        string closingDate = ClosingDateDropDownList.SelectedItem.Text.ToString();
        
        StringBuilder sbMaster = new StringBuilder();
        StringBuilder sbFilter = new StringBuilder();
        DataTable dtStatement = new DataTable();

        if (NonIDRadioButton.Checked)
        {
            investorType = "NON_ID";
        }
        else if (IDRadioButton.Checked)
        {
            investorType = "ID";
        }
        else 
        {
            investorType = "ALLID";
        }
        if (OfficeRadioButton.Checked)
        {
            statementType = "OFFICE";
        }
        else if (OfficeWithSignRadioButton.Checked)
        {
            statementType = "OFFICE_SIGN";
        }
        else if (BankRadioButton.Checked)
        {
            statementType = "BANK";
        }

        if (IDRadioButton.Checked && BankRadioButton.Checked)
        {
           sbMaster.Append(" SELECT  FUND_INFO.FUND_CD, FUND_INFO.FUND_NM, DIVI_PARA.F_YEAR, TO_CHAR(DIVI_PARA.CLOSE_DT, 'DD-MON-YYYY') AS CLOSE_DT, 'C' || LPAD(DIVIDEND_ID.WAR_NO, 7, '0') || 'C' AS WAR_NO_MICR,DIVIDEND_ID.CIP,NULL AS REG_TYPE,DECODE(NVL(DIVIDEND_ID.CIP_QTY, 0), 0, 0, DIVIDEND_ID.FI_DIVI_QTY) AS FRAC_DIVI,DIVIDEND_ID.TIN,");
           sbMaster.Append(" DIVIDEND_ID.TOT_DIVI - DIVIDEND_ID.DIDUCT AS NET_DIVI, DIVI_PARA.DIVI_NO, DIVI_PARA.FY_PART, DIVIDEND_ID.REG_BR,  DIVIDEND_ID.DIVI_RATE, DIVIDEND_ID.CIP_RATE,NVL(DIVIDEND_ID.CIP_QTY, 0) AS CIP_QTY, NVL(DIVIDEND_ID.FI_DIVI_QTY, 0) AS FI_DIVI_QTY,  NVL(DIVIDEND_ID.DIDUCT,  0) AS TAX_DIDUCT, DIVIDEND_ID.TOT_DIVI, ");
           sbMaster.Append(" DIVIDEND_ID.TOT_BALANCE AS NO_OF_UNITS, DIVI_PARA.BK_AC_NO, DIVI_PARA.BK_NAME, ' ' AS REG_NO, NULL AS JNT_NAME, DIVI_PARA.BK_ADDRS1, DIVI_PARA.BK_ADDRS2, TO_CHAR(DIVI_PARA.AGM_DT, 'DD-MON-YYYY') AS AGM_DT, TO_CHAR(DIVI_PARA.ISS_DT, 'DD-MON-YYYY') AS ISS_DT,");
           sbMaster.Append(" DIVI_PARA.TAX_RT_INDIVIDUAL, DIVI_PARA.TAX_RT_INSTITUTION, DIVI_PARA.BK_ROUTING_NO, DIVI_PARA.BK_TRANSACTION_CODE, DIVI_PARA.BK_AC_NO_MICR, DIVIDEND_ID.BK_AC_NO AS H_BK_AC_NO, DIVIDEND_ID.BK_NM_CD AS H_BK_NM_CD,DIVI_PARA.BK_ROUTING_NO || 'A' AS BK_ROUTING_NO_MICR,");
           sbMaster.Append(" DIVIDEND_ID.BK_BR_NM_CD AS H_BK_BR_NM_CD, BANK_NAME.BANK_NAME || ' , ' || BANK_BRANCH.BRANCH_NAME AS HNAME, BANK_BRANCH.BRANCH_ADDRS1 AS ADDRS1, BANK_BRANCH.BRANCH_ADDRS2 AS ADDRS2, BANK_BRANCH.BRANCH_DISTRICT AS CITY,'WARRANT' AS WAR_TYPE, '' AS BO_FOLIO, ");
           sbMaster.Append(" DIVIDEND_ID.BK_FLAG,DIVIDEND_ID.ID_FLAG,DIVIDEND_ID.CIP , DIVIDEND_ID.BK_AC_NO AS HOLDER_BK_ACC_NO, DIVIDEND_ID.BK_NM_CD AS HOLDER_BK_NM_CD,DIVIDEND_ID.BK_BR_NM_CD AS HOLDER_BK_BR_NM_CD,");
           sbMaster.Append(" LPAD(DIVIDEND_ID.WAR_NO, 7, '0') AS WAR_NO, NULL AS REG_NUM FROM  BANK_BRANCH INNER JOIN  BANK_NAME ON BANK_BRANCH.BANK_CODE = BANK_NAME.BANK_CODE INNER JOIN  DIVI_PARA INNER JOIN");
           sbMaster.Append(" FUND_INFO ON DIVI_PARA.FUND_CD = FUND_INFO.FUND_CD INNER JOIN DIVIDEND_ID ON DIVI_PARA.FUND_CD = DIVIDEND_ID.FUND_CD AND DIVI_PARA.F_YEAR = DIVIDEND_ID.FY AND   DIVI_PARA.DIVI_NO = DIVIDEND_ID.DIVI_NO AND DIVI_PARA.CLOSE_DT = DIVIDEND_ID.CLOSE_DT ON ");
           sbMaster.Append(" BANK_BRANCH.BANK_CODE = DIVIDEND_ID.ID_BK_NM_CD AND BANK_BRANCH.BRANCH_CODE = DIVIDEND_ID.ID_BK_BR_NM_CD");
           sbMaster.Append(" WHERE     (DIVIDEND_ID.FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')");
           sbMaster.Append(" AND (DIVI_PARA.CLOSE_DT='" + closingDate.ToString() + "') AND (DIVI_PARA.F_YEAR = '" + FY + "')");
           if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text == "")
           {
               sbFilter.Append(" AND TO_NUMBER(DIVIDEND_ID.WAR_NO)>=" + Convert.ToUInt32(fromWar_NoTextBox.Text.Trim().ToString()));
           }
           else if (fromWar_NoTextBox.Text == "" && toWar_NoTextBox.Text != "")
           {
               sbFilter.Append(" AND TO_NUMBER(DIVIDEND_ID.WAR_NO)<=" + Convert.ToUInt32(toWar_NoTextBox.Text.Trim().ToString()));
           }
           else if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text != "")
           {
               sbFilter.Append(" AND TO_NUMBER(DIVIDEND_ID.WAR_NO) BETWEEN " + Convert.ToUInt32(fromWar_NoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToUInt32(toWar_NoTextBox.Text.Trim().ToString()));
           }
           if (CIPRadioButton.Checked)
           {
               sbFilter.Append(" AND DIVIDEND_ID.CIP='Y' ");
           }
           else if(NonCIPRadioButton.Checked)
           {
               sbFilter.Append(" AND DIVIDEND_ID.CIP='N' ");
           }
           sbFilter.Append("  ORDER BY TO_NUMBER(WAR_NO)");
           sbMaster.Append(sbFilter.ToString());
           dtStatement = commonGatewayObj.Select(sbMaster.ToString());

        }
       
        else
        {
            sbMaster.Append("SELECT FUND_CD,FUND_NM,DIVI_NO,F_YEAR,CLOSE_DT,DIVI_RATE,BK_AC_NO_MICR AS BK_AC_NO ,BK_AC_NO_MICR||'C' AS BK_AC_NO_MICR,BK_NAME, BK_ADDRS1, BK_ADDRS2, BK_ROUTING_NO,BK_ROUTING_NO||'A' AS BK_ROUTING_NO_MICR,TIN,");
            sbMaster.Append(" BK_TRANSACTION_CODE, TO_CHAR(ISS_DT, 'DD-MON-YYYY') AS ISS_DT, HNAME, REG_NO, JNT_NAME, ADDRS1, ADDRS2, CITY, SPEC_IN1,SPEC_IN2,BK_FLAG,HOLDER_BK_ACC_NO,HOLDER_BK_NM_CD,HOLDER_BK_BR_NM_CD,BO_FOLIO,");
            sbMaster.Append(" LPAD(WAR_NO, 7, '0') AS WAR_NO, 'C'||LPAD(WAR_NO, 7, '0')||'C' AS WAR_NO_MICR,NO_OF_UNITS, TOT_DIVI, TAX_DIDUCT, FI_DIVI_QTY, CIP_QTY, CIP_RATE, CIP, CIP_SL_NO,DECODE(NVL(IS_BEFTN,'N'),'Y','BEFTN','WARRANT') AS WAR_TYPE,");
            sbMaster.Append(" TO_CHAR(AGM_DT, 'DD-MON-YYYY') AS AGM_DT,  TAX_RT_INDIVIDUAL, TAX_RT_INSTITUTION, REG_TYPE, FY_PART, NET_DIVI, FRAC_DIVI, REG_NUM,REG_BR,ID_FLAG,ID_AC,ID_BK_NM_CD,ID_BK_BR_NM_CD");

            sbMaster.Append(" FROM (SELECT DIVI_PARA.FUND_CD,FUND_INFO.FUND_NM, DIVI_PARA.DIVI_NO, DIVI_PARA.F_YEAR, TO_CHAR(DIVI_PARA.CLOSE_DT, 'DD-MON-YYYY')  ");
            sbMaster.Append(" AS CLOSE_DT,NVL(DIVIDEND.TIN,'') AS TIN,DIVI_PARA.RATE AS DIVI_RATE, DIVI_PARA.BK_AC_NO,DIVI_PARA.BK_AC_NO_MICR,DIVI_PARA.BK_NAME, DIVI_PARA.BK_ADDRS1, DIVI_PARA.BK_ADDRS2,");
            sbMaster.Append(" DIVI_PARA.BK_ROUTING_NO, DIVI_PARA.BK_TRANSACTION_CODE, DIVI_PARA.ISS_DT, U_MASTER.HNAME,U_MASTER.REG_BR, DIVIDEND.REG_BK || '/' || DIVIDEND.REG_BR || '/' || DIVIDEND.REG_NO AS ");
            sbMaster.Append(" REG_NO,DIVIDEND.REG_NO AS REG_NUM, U_JHOLDER.JNT_NAME,   U_MASTER.ADDRS1,DIVIDEND.IS_BEFTN,NVL(U_MASTER.BO,U_MASTER.FOLIO_NO) AS BO_FOLIO,");
            sbMaster.Append(" U_MASTER.ADDRS2, U_MASTER.CITY, DIVIDEND.WAR_NO, DIVIDEND.BALANCE AS NO_OF_UNITS, DIVIDEND.ID_FLAG,DIVIDEND.ID_AC,DIVIDEND.ID_BK_NM_CD,DIVIDEND.ID_BK_BR_NM_CD,");
            sbMaster.Append(" DIVIDEND.TOT_DIVI,DIVIDEND.DIDUCT AS TAX_DIDUCT, DIVIDEND.FI_DIVI_QTY, NVL(DIVIDEND.CIP_QTY, 0) AS CIP_QTY,DIVI_PARA.CIP_RATE, DIVIDEND.CIP,DIVIDEND.CIP_SL_NO,");
            sbMaster.Append(" DIVI_PARA.AGM_DT, DIVI_PARA.TAX_RT_INDIVIDUAL, DIVI_PARA.TAX_RT_INSTITUTION, U_MASTER.REG_TYPE, DIVI_PARA.FY_PART, DIVIDEND.TOT_DIVI - DIVIDEND.DIDUCT AS  NET_DIVI,");
            sbMaster.Append(" U_MASTER.SPEC_IN1,U_MASTER.SPEC_IN2,DIVIDEND.BK_FLAG,DIVIDEND.BK_AC_NO AS HOLDER_BK_ACC_NO,DIVIDEND.BK_NM_CD AS HOLDER_BK_NM_CD,DIVIDEND.BK_BR_NM_CD AS HOLDER_BK_BR_NM_CD,");

            sbMaster.Append(" DECODE(NVL(DIVIDEND.CIP_QTY, 0), 0, 0, DIVIDEND.FI_DIVI_QTY) AS FRAC_DIVI FROM DIVI_PARA INNER JOIN DIVIDEND ON DIVI_PARA.DIVI_NO = DIVIDEND.DIVI_NO INNER JOIN");
            sbMaster.Append(" FUND_INFO ON DIVI_PARA.FUND_CD = FUND_INFO.FUND_CD INNER JOIN  U_MASTER ON DIVIDEND.REG_BK = U_MASTER.REG_BK AND DIVIDEND.REG_BR = U_MASTER.REG_BR AND");
            sbMaster.Append(" DIVIDEND.REG_NO = U_MASTER.REG_NO LEFT OUTER JOIN  U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR");
            sbMaster.Append(" AND U_MASTER.REG_NO = U_JHOLDER.REG_NO  WHERE (DIVI_PARA.F_YEAR = '" + FY + "') AND (DIVIDEND.VALID IS NULL) ");
            sbMaster.Append(" AND (DIVIDEND.FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "') AND (DIVIDEND.CLOSE_DT='" + closingDate.ToString() + "') ");
            if (IDRadioButton.Checked && OfficeRadioButton.Checked)
            {
                sbMaster.Append(" AND (DIVIDEND.ID_FLAG='Y') ");
                if (IDbankNameDropDownList.SelectedValue != "0")
                {
                    sbMaster.Append(" AND (DIVIDEND.ID_BK_NM_CD=" + Convert.ToInt32(IDbankNameDropDownList.SelectedValue.ToString()) + ")");
                    if (IDbranchNameDropDownList.SelectedValue != "0")
                    {
                        sbMaster.Append(" AND (DIVIDEND.ID_BK_BR_NM_CD=" + Convert.ToInt32(IDbranchNameDropDownList.SelectedValue.ToString()) + ")");
                    }
                }

            }
            else if (IDRadioButton.Checked && OfficeWithSignRadioButton.Checked && CIPRadioButton.Checked)
            {
                sbMaster.Append(" AND (DIVIDEND.ID_FLAG='Y') ");
                sbMaster.Append(" AND (DIVIDEND.CIP='Y') ");
                if (IDbankNameDropDownList.SelectedValue != "0")
                {
                    sbMaster.Append(" AND (DIVIDEND.ID_BK_NM_CD=" + Convert.ToInt32(IDbankNameDropDownList.SelectedValue.ToString()) + ")");
                    if (IDbranchNameDropDownList.SelectedValue != "0")
                    {
                        sbMaster.Append(" AND (DIVIDEND.ID_BK_BR_NM_CD=" + Convert.ToInt32(IDbranchNameDropDownList.SelectedValue.ToString()) + ")");
                    }
                }

            }
            else if (NonIDRadioButton.Checked && OfficeWithSignRadioButton.Checked && CIPRadioButton.Checked)
            {
                sbMaster.Append(" AND (DIVIDEND.ID_FLAG='N') ");
                sbMaster.Append(" AND (DIVIDEND.CIP='Y') ");
              

            }
            else if (NonCIPRadioButton.Checked)
            {
                sbMaster.Append(" AND (DIVIDEND.CIP='N') ");
            }
            if (BEFTNRadioButton.Checked)
            {
                sbMaster.Append(" AND (DIVIDEND.IS_BEFTN='Y') ");
            }
            else if (NONBEFTNRadioButton.Checked)
            {
                sbMaster.Append(" AND (DIVIDEND.IS_BEFTN='N') ");
            }

            sbMaster.Append(" AND (DIVIDEND.FUND_CD ='" + fundCode + "'))  DERIVEDTBL_1 WHERE 1=1 ");
            if (branchNameDropDownList.SelectedValue.ToString() != "0")
            {
                sbFilter.Append(" AND  (DERIVEDTBL_1.REG_BR='" + branchNameDropDownList.SelectedValue.ToString().ToUpper() + "') ");
            }
            if (branchGroupDropDownList.SelectedValue.ToString() != "0")
            {
                sbFilter.Append(" AND (DERIVEDTBL_1.REG_BR LIKE '%" + branchGroupDropDownList.SelectedItem.Text.ToString() + "%') ");
            }

            sbFilter.Append("AND  DERIVEDTBL_1.CLOSE_DT='" + closingDate.ToString() + "' ");
            if (CIPRadioButton.Checked)
            {
                sbFilter.Append(" AND DERIVEDTBL_1.CIP='Y'");
                investmentType = "CIP";
            }
            else if (NonCIPRadioButton.Checked)
            {
                sbFilter.Append(" AND DERIVEDTBL_1.CIP='N'");
                investmentType = "NON_CIP";
            }


            if (!IDRadioButton.Checked)
            {
                if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text == "")
                {
                    sbFilter.Append(" AND TO_NUMBER(DERIVEDTBL_1.WAR_NO)>=" + Convert.ToInt32(fromWar_NoTextBox.Text.Trim().ToString()));
                }
                else if (fromWar_NoTextBox.Text == "" && toWar_NoTextBox.Text != "")
                {
                    sbFilter.Append(" AND TO_NUMBER(DERIVEDTBL_1.WAR_NO)<=" + Convert.ToInt32(toWar_NoTextBox.Text.Trim().ToString()));
                }
                else if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text != "")
                {
                    sbFilter.Append(" AND TO_NUMBER(DERIVEDTBL_1.WAR_NO) BETWEEN " + Convert.ToInt32(fromWar_NoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toWar_NoTextBox.Text.Trim().ToString()));
                }
            }

            if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
            {
                sbFilter.Append(" AND TO_NUMBER(DERIVEDTBL_1.REG_NUM)>=" + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()));
            }
            else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND TO_NUMBER(DERIVEDTBL_1.REG_NUM)<=" + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()));
            }
            else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND TO_NUMBER(DERIVEDTBL_1.REG_NUM) BETWEEN " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()));
            }
            sbFilter.Append(" AND (DERIVEDTBL_1.FUND_CD ='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')");
           
            sbFilter.Append(" ORDER BY TO_NUMBER(REG_NUM)");

            sbMaster.Append(sbFilter.ToString());

            dtStatement = commonGatewayObj.Select(sbMaster.ToString());
        }
        if (dtStatement.Rows.Count > 0)
        {
            Session["dtStatement"] = dtStatement;
            Session["statementType"] = statementType;
            Session["investorType"] = investorType;
            Session["investmentType"] = investmentType;
            ClientScript.RegisterStartupScript(this.GetType(), "UnitDividendStatement", "window.open('ReportViewer/UnitReportDividendStatementReportViewer.aspx')", true);
        }
        else
        {
            Session["dtStatement"] = null;
            Session["statementType"] = null;
            Session["investorType"] = null;
            Session["investmentType"] = null;
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
        else if (NonIDRadioButton.Checked || TypeAllRadioButton.Checked)
        {
            IDbankNameDropDownList.SelectedValue = "0";
            IDbranchNameDropDownList.SelectedValue = "0";
            IDbankNameDropDownList.Enabled = false;
            IDbranchNameDropDownList.Enabled = false;
        }
    }
    protected void NonIDRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (NonIDRadioButton.Checked || TypeAllRadioButton.Checked)
        {
            IDbankNameDropDownList.SelectedValue = "0";
            IDbranchNameDropDownList.SelectedValue = "0";
            IDbankNameDropDownList.Enabled = false;
            IDbranchNameDropDownList.Enabled = false;
            
        }
        else if (IDRadioButton.Checked)
        {
            IDbankNameDropDownList.Enabled = true;
            IDbranchNameDropDownList.Enabled = true;
        }
        
      
    }

    protected void TypeAllRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (NonIDRadioButton.Checked || TypeAllRadioButton.Checked)
        {
            IDbankNameDropDownList.SelectedValue = "0";
            IDbranchNameDropDownList.SelectedValue = "0";
            IDbankNameDropDownList.Enabled = false;
            IDbranchNameDropDownList.Enabled = false;

        }
        else if (IDRadioButton.Checked)
        {
            IDbankNameDropDownList.Enabled = true;
            IDbranchNameDropDownList.Enabled = true;
        }
        
    }
}
