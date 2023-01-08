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

public partial class UI_UnitReportDividendWarrant : System.Web.UI.Page
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
                         
        }
    
    }
   
    
    protected void CloseButton_Click(object sender, EventArgs e)
    {         
        Response.Redirect("UnitHome.aspx");
           
    }

    protected void PrintButton_Click(object sender, EventArgs e)
    {
        string duplicate = "";
        string fundCode = fundNameDropDownList.SelectedValue.ToString();
        StringBuilder sbMaster = new StringBuilder();
        StringBuilder sbFilter = new StringBuilder();
        DataTable dtDividend = new DataTable();
        string FY = DividendFYDropDownList.SelectedValue.ToString();
        string closingDate=ClosingDateDropDownList.SelectedItem.Text.ToString();
        string dividendCategory = "NORMAL";
        if (InerimRadioButton.Checked)
        {
            dividendCategory = "INTERIM";
        }

        if (NormalRadioButton.Checked|| InerimRadioButton.Checked)
        {
            sbMaster.Append("SELECT FUND_CD,FUND_NM,DIVI_NO,F_YEAR,CLOSE_DT,DIVI_RATE,BK_AC_NO ,BK_AC_NO_MICR||'C' AS BK_AC_NO_MICR,BK_NAME, BK_ADDRS1, BK_ADDRS2, BK_ROUTING_NO,BK_ROUTING_NO||'A' AS BK_ROUTING_NO_MICR,TAX_LIMIT,");
            sbMaster.Append(" BK_TRANSACTION_CODE, TO_CHAR(ISS_DT, 'DD-MON-YYYY') AS ISS_DT, HNAME, REG_NO, JNT_NAME, ADDRS1, ADDRS2, CITY,H_BK_AC_NO,H_BK_NM_CD,H_BK_BR_NM_CD,");
            sbMaster.Append(" LPAD(WAR_NO, 7, '0') AS WAR_NO, 'C'||LPAD(WAR_NO, 7, '0')||'C' AS WAR_NO_MICR,NO_OF_UNITS, TOT_DIVI, TAX_DIDUCT, FI_DIVI_QTY, CIP_QTY, CIP_RATE, CIP, ");
            sbMaster.Append(" TO_CHAR(AGM_DT, 'DD-MON-YYYY') AS AGM_DT,  TAX_RT_INDIVIDUAL, TAX_RT_INSTITUTION, REG_TYPE, FY_PART, NET_DIVI, FRAC_DIVI, REG_NUM,REG_BR");

            sbMaster.Append(" FROM    (SELECT DIVI_PARA.FUND_CD,FUND_INFO.FUND_NM, DIVI_PARA.DIVI_NO, DIVI_PARA.F_YEAR, TO_CHAR(DIVI_PARA.CLOSE_DT, 'DD-MON-YYYY') ");
            sbMaster.Append(" AS CLOSE_DT,DIVI_PARA.RATE AS DIVI_RATE, DIVI_PARA.BK_AC_NO,DIVI_PARA.BK_AC_NO_MICR,DIVI_PARA.BK_NAME, DIVI_PARA.BK_ADDRS1, DIVI_PARA.BK_ADDRS2, DIVI_PARA.TAX_LIMIT,");
            sbMaster.Append(" DIVI_PARA.BK_ROUTING_NO, DIVI_PARA.BK_TRANSACTION_CODE, DIVI_PARA.ISS_DT,DIVIDEND.BK_AC_NO AS H_BK_AC_NO,DIVIDEND.BK_NM_CD AS H_BK_NM_CD,DIVIDEND.BK_BR_NM_CD AS H_BK_BR_NM_CD, U_MASTER.HNAME,U_MASTER.REG_BR, DIVIDEND.REG_BK || '/' || DIVIDEND.REG_BR || '/' || DIVIDEND.REG_NO AS ");
            sbMaster.Append(" REG_NO,DIVIDEND.REG_NO AS REG_NUM, U_JHOLDER.JNT_NAME, U_MASTER.ADDRS1, U_MASTER.ADDRS2, U_MASTER.CITY, DIVIDEND.WAR_NO, DIVIDEND.BALANCE AS NO_OF_UNITS,");
            sbMaster.Append(" DIVIDEND.TOT_DIVI,DIVIDEND.DIDUCT AS TAX_DIDUCT, DIVIDEND.FI_DIVI_QTY, NVL(DIVIDEND.CIP_QTY, 0) AS CIP_QTY,DIVI_PARA.CIP_RATE, DIVIDEND.CIP,");
            sbMaster.Append(" DIVI_PARA.AGM_DT, DIVI_PARA.TAX_RT_INDIVIDUAL, DIVI_PARA.TAX_RT_INSTITUTION, U_MASTER.REG_TYPE, DIVI_PARA.FY_PART,ROUND(ROUND(DIVIDEND.TOT_DIVI,2) -ROUND( DIVIDEND.DIDUCT,2),2) AS NET_DIVI,");

            sbMaster.Append(" DECODE(NVL(DIVIDEND.CIP_QTY, 0), 0, 0, DIVIDEND.FI_DIVI_QTY) AS FRAC_DIVI FROM DIVI_PARA INNER JOIN DIVIDEND ON DIVI_PARA.DIVI_NO = DIVIDEND.DIVI_NO INNER JOIN");
            sbMaster.Append(" FUND_INFO ON DIVI_PARA.FUND_CD = FUND_INFO.FUND_CD INNER JOIN  U_MASTER ON DIVIDEND.REG_BK = U_MASTER.REG_BK AND DIVIDEND.REG_BR = U_MASTER.REG_BR AND");
            sbMaster.Append(" DIVIDEND.REG_NO = U_MASTER.REG_NO LEFT OUTER JOIN  U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR");
            sbMaster.Append(" AND U_MASTER.REG_NO = U_JHOLDER.REG_NO  WHERE (DIVI_PARA.F_YEAR = '" + FY + "') AND (DIVIDEND.VALID IS NULL) AND (DIVIDEND.WAR_NO IS NOT NULL)  ");
            sbMaster.Append(" AND (DIVIDEND.FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "') AND (DIVIDEND.CLOSE_DT='" + closingDate.ToString() + "') ");
            //sbMaster.Append(" AND (DIVIDEND.BK_FLAG = 'N')UNION ALL  SELECT    DIVI_PARA_1.FUND_CD,  FUND_INFO_1.FUND_NM, DIVI_PARA_1.DIVI_NO, DIVI_PARA_1.F_YEAR, TO_CHAR(DIVI_PARA_1.CLOSE_DT, 'DD-MON-YYYY') ");
            //sbMaster.Append(" AS CLOSE_DT, DIVI_PARA_1.RATE AS DIVI_RATE, DIVI_PARA_1.BK_AC_NO,DIVI_PARA_1.BK_AC_NO_MICR, DIVI_PARA_1.BK_NAME, DIVI_PARA_1.BK_ADDRS1, DIVI_PARA_1.BK_ADDRS2,");

            //sbMaster.Append(" DIVI_PARA_1.BK_ROUTING_NO, DIVI_PARA_1.BK_TRANSACTION_CODE, DIVI_PARA_1.ISS_DT,DIVIDEND_1.BK_AC_NO AS H_BK_AC_NO,DIVIDEND_1.BK_NM_CD AS H_BK_NM_CD,DIVIDEND_1.BK_BR_NM_CD AS H_BK_BR_NM_CD, U_MASTER_1.HNAME,U_MASTER_1.REG_BR, DIVIDEND_1.REG_BK || '/' || DIVIDEND_1.REG_BR || '/' || DIVIDEND_1.REG_NO AS REG_NO,");
            //sbMaster.Append(" DIVIDEND_1.REG_NO AS REG_NUM,U_JHOLDER_1.JNT_NAME, U_MASTER_1.SPEC_IN1 AS ADDRS1, U_MASTER_1.SPEC_IN2 AS ADDRS2, ' ' AS CITY, DIVIDEND_1.WAR_NO, DIVIDEND_1.BALANCE AS ");
            //sbMaster.Append(" NO_OF_UNITS, DIVIDEND_1.TOT_DIVI, DIVIDEND_1.DIDUCT AS TAX_DIDUCT, DIVIDEND_1.FI_DIVI_QTY, NVL(DIVIDEND_1.CIP_QTY, 0) AS CIP_QTY, DIVI_PARA_1.CIP_RATE,");
            //sbMaster.Append(" DIVIDEND_1.CIP, DIVI_PARA_1.AGM_DT, DIVI_PARA_1.TAX_RT_INDIVIDUAL, DIVI_PARA_1.TAX_RT_INSTITUTION, U_MASTER_1.REG_TYPE, DIVI_PARA_1.FY_PART, DIVIDEND_1.TOT_DIVI - DIVIDEND_1.DIDUCT AS");
            //sbMaster.Append(" NET_DIVI, DECODE(NVL(DIVIDEND_1.CIP_QTY, 0), 0, 0, DIVIDEND_1.FI_DIVI_QTY) AS FRAC_DIVI FROM    DIVI_PARA DIVI_PARA_1 INNER JOIN DIVIDEND DIVIDEND_1 ON DIVI_PARA_1.DIVI_NO");

            //sbMaster.Append(" = DIVIDEND_1.DIVI_NO INNER JOIN FUND_INFO FUND_INFO_1 ON DIVI_PARA_1.FUND_CD = FUND_INFO_1.FUND_CD INNER JOIN  U_MASTER U_MASTER_1 ON DIVIDEND_1.REG_BK ");
            //sbMaster.Append(" = U_MASTER_1.REG_BK AND DIVIDEND_1.REG_BR = U_MASTER_1.REG_BR AND DIVIDEND_1.REG_NO = U_MASTER_1.REG_NO LEFT OUTER JOIN U_JHOLDER U_JHOLDER_1 ON U_MASTER_1.REG_BK = U_JHOLDER_1.REG_BK AND ");
            //sbMaster.Append(" U_MASTER_1.REG_BR = U_JHOLDER_1.REG_BR AND U_MASTER_1.REG_NO = U_JHOLDER_1.REG_NO WHERE    (DIVI_PARA_1.F_YEAR = '" + FY + "') AND (DIVIDEND_1.VALID IS NULL) AND (DIVIDEND_1.WAR_NO IS NOT NULL) ");
            //sbMaster.Append(" AND (DIVIDEND_1.FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "') AND (DIVIDEND_1.CLOSE_DT='" + closingDate.ToString() + "') ");
            //sbMaster.Append(" AND (DIVIDEND_1.BK_FLAG = 'Y')
             sbMaster.Append(" ) DERIVEDTBL_1 WHERE DERIVEDTBL_1.CLOSE_DT='" + closingDate.ToString() + "' ");
             sbMaster.Append(" AND DERIVEDTBL_1.FUND_CD='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "' ");

            if (branchNameDropDownList.SelectedValue.ToString() != "0")
            {
                sbFilter.Append(" AND DERIVEDTBL_1.REG_BR='" + branchNameDropDownList.SelectedValue.ToString() + "'");
            }

            if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text == "")
            {
                sbFilter.Append(" AND TO_NUMBER(DERIVEDTBL_1.WAR_NO)>=" + Convert.ToUInt32(fromWar_NoTextBox.Text.Trim().ToString()));
            }
            else if (fromWar_NoTextBox.Text == "" && toWar_NoTextBox.Text != "")
            {
                sbFilter.Append(" AND TO_NUMBER(DERIVEDTBL_1.WAR_NO)<=" + Convert.ToUInt32(toWar_NoTextBox.Text.Trim().ToString()));
            }
            else if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text != "")
            {
                sbFilter.Append(" AND TO_NUMBER(DERIVEDTBL_1.WAR_NO) BETWEEN " + Convert.ToUInt32(fromWar_NoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToUInt32(toWar_NoTextBox.Text.Trim().ToString()));
            }

            if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
            {
                sbFilter.Append(" AND TO_NUMBER(DERIVEDTBL_1.REG_NUM)>=" + Convert.ToUInt32(fromRegNoTextBox.Text.Trim().ToString()));
            }
            else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND TO_NUMBER(DERIVEDTBL_1.REG_NUM)<=" + Convert.ToUInt32(toRegNoTextBox.Text.Trim().ToString()));
            }
            else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND TO_NUMBER(DERIVEDTBL_1.REG_NUM) BETWEEN " + Convert.ToUInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToUInt32(toRegNoTextBox.Text.Trim().ToString()));
            }

            sbFilter.Append("  ORDER BY TO_NUMBER(WAR_NO)");
            sbMaster.Append(sbFilter.ToString());
            dtDividend = commonGatewayObj.Select(sbMaster.ToString());
           
        }
        else if (IDAccountRadioButton.Checked)
        {
            sbMaster.Append(" SELECT  DIVIDEND_ID.FUND_CD, FUND_INFO.FUND_NM, DIVI_PARA.DIVI_NO, DIVI_PARA.RATE AS DIVI_RATE, DIVI_PARA.F_YEAR, TO_CHAR(DIVI_PARA.CLOSE_DT, ");
            sbMaster.Append(" 'DD-MON-YYYY') AS CLOSE_DT, DIVI_PARA.FY_PART, DIVI_PARA.BK_AC_NO, DIVI_PARA.BK_NAME, DIVI_PARA.BK_ADDRS1, DIVI_PARA.BK_ADDRS2, DIVIDEND_ID.BK_AC_NO AS H_BK_AC_NO,DIVIDEND_ID.BK_NM_CD AS H_BK_NM_CD,DIVIDEND_ID.BK_BR_NM_CD AS H_BK_BR_NM_CD,");
            sbMaster.Append(" DIVI_PARA.BK_AC_NO_MICR || 'C' AS BK_AC_NO_MICR, DIVI_PARA.BK_TRANSACTION_CODE, DIVI_PARA.BK_ROUTING_NO, DIVIDEND_ID.TOT_DIVI - DIVIDEND_ID.DIDUCT AS NET_DIVI,");
            sbMaster.Append(" DIVI_PARA.BK_ROUTING_NO || 'A' AS BK_ROUTING_NO_MICR, TO_CHAR(DIVI_PARA.ISS_DT, 'DD-MON-YYYY') AS ISS_DT, ' ' AS REG_NO, NULL");
            sbMaster.Append(" AS JNT_NAME, BANK_NAME.BANK_NAME || ' , ' || BANK_BRANCH.BRANCH_NAME AS HNAME, BANK_BRANCH.BRANCH_ADDRS1 AS ADDRS1,DIVIDEND_ID.REG_BR,");
            sbMaster.Append(" BANK_BRANCH.BRANCH_ADDRS2 AS ADDRS2, BANK_BRANCH.BRANCH_DISTRICT AS CITY, LPAD(DIVIDEND_ID.WAR_NO, 7, '0') AS WAR_NO, NULL AS REG_NUM,");
            sbMaster.Append(" 'C' || LPAD(DIVIDEND_ID.WAR_NO, 7, '0') || 'C' AS WAR_NO_MICR, DIVIDEND_ID.TOT_BALANCE AS NO_OF_UNITS, DIVIDEND_ID.TOT_DIVI AS TOT_DIVI , NVL(DIVIDEND_ID.DIDUCT,  0) AS TAX_DIDUCT,");
            sbMaster.Append(" NVL(DIVIDEND_ID.FI_DIVI_QTY, 0) AS FI_DIVI_QTY, NVL(DIVIDEND_ID.CIP_QTY, 0) AS CIP_QTY, DIVIDEND_ID.CIP,NULL AS REG_TYPE,");
            sbMaster.Append(" DIVIDEND_ID.CIP_RATE, DECODE(NVL(DIVIDEND_ID.CIP_QTY, 0), 0, 0, DIVIDEND_ID.FI_DIVI_QTY) AS FRAC_DIVI, TO_CHAR(DIVI_PARA.AGM_DT,  'DD-MON-YYYY') AS AGM_DT , DIVI_PARA.TAX_RT_INDIVIDUAL, DIVI_PARA.TAX_RT_INSTITUTION ");
            sbMaster.Append(" FROM   DIVI_PARA INNER JOIN DIVIDEND_ID ON DIVI_PARA.FUND_CD = DIVIDEND_ID.FUND_CD AND DIVI_PARA.F_YEAR = DIVIDEND_ID.FY AND  DIVI_PARA.DIVI_NO = DIVIDEND_ID.DIVI_NO INNER JOIN");
            sbMaster.Append(" FUND_INFO ON DIVIDEND_ID.FUND_CD = FUND_INFO.FUND_CD INNER JOIN  BANK_BRANCH ON DIVIDEND_ID.ID_BK_BR_NM_CD = BANK_BRANCH.BRANCH_CODE AND");
            sbMaster.Append(" DIVIDEND_ID.ID_BK_NM_CD = BANK_BRANCH.BANK_CODE INNER JOIN   BANK_NAME ON BANK_BRANCH.BANK_CODE = BANK_NAME.BANK_CODE");
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
            sbFilter.Append("  ORDER BY TO_NUMBER(WAR_NO)");
            sbMaster.Append(sbFilter.ToString());
            dtDividend = commonGatewayObj.Select(sbMaster.ToString());

        }
        if (dtDividend.Rows.Count > 0)
        {
            if (DuplicateCheckBox.Checked)
            {
                duplicate = "DUPLICATE";
            }
            Session["dtDividend"] = dtDividend;
            Session["duplicate"] = duplicate;
            Session["fundCode"] = fundCode;
            Session["dividendCategory"] = dividendCategory;
            ClientScript.RegisterStartupScript(this.GetType(), "DividendWarrant", "window.open('ReportViewer/UnitReportDividendWarrantReportViewer.aspx')", true);
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
    }
}
