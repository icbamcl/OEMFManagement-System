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


public partial class UI_UnitReportTaxStatementAcc : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();
    UnitLienBl unitLienBLObj = new UnitLienBl();
    dividendDAO diviDAOObj = new dividendDAO();
    string fundCode = "";
    string branchCode = "";

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
        fundCode = bcContent.FundCode.ToString();
        branchCode = bcContent.BranchCode.ToString();
        spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
      

        
        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = opendMFDAO.dtFundList();
            fundNameDropDownList.DataTextField = "FUND_NM";
            fundNameDropDownList.DataValueField = "FUND_CD";
            fundNameDropDownList.DataBind();

               
        }
    
    }
   
    
    protected void CloseButton_Click(object sender, EventArgs e)
    {         
        Response.Redirect("UnitHome.aspx");
           
    }

    protected void PrintButton_Click(object sender, EventArgs e)
    {
        string statementType = "SAMMARY";
        StringBuilder sbQuery = new StringBuilder();

        if (sammaryRadioButton.Checked)
        {
            sbQuery = new StringBuilder();
            sbQuery.Append("SELECT 'NON-CIP:' AS CIP_TYPE, 'HO' AS BRANCH, SUM(DIVIDEND.BALANCE) AS NO_UNITS, SUM(DIVIDEND.TOT_DIVI) AS TOTAL_DIVIDEND, ");
            sbQuery.Append(" NVL(SUM(DIVIDEND.CIP_QTY), 0) AS TOTAL_CIP, SUM(DIVIDEND.DIDUCT) AS TAX_DIDUCT, SUM(DIVIDEND.FI_DIVI_QTY) AS NET_DIVDEND ");
            sbQuery.Append(" FROM  DIVI_PARA INNER JOIN   DIVIDEND ON DIVI_PARA.FUND_CD = DIVIDEND.FUND_CD AND DIVI_PARA.CLOSE_DT = DIVIDEND.CLOSE_DT AND ");
            sbQuery.Append(" DIVI_PARA.F_YEAR = DIVIDEND.FY AND DIVI_PARA.DIVI_NO = DIVIDEND.DIVI_NO WHERE  (DIVIDEND.CIP = 'N') AND (DIVIDEND.REG_BR = 'AMC/01') AND (DIVIDEND.VALID IS NULL) ");
            sbQuery.Append(" AND (DIVI_PARA.FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')AND (DIVI_PARA.F_YEAR = '" + DividendFYDropDownList.SelectedItem.Text.ToString() + "')");
            sbQuery.Append(" AND (DIVI_PARA.CLOSE_DT = '" + ClosingDateDropDownList.SelectedItem.ToString().ToUpper() + "') AND (DIVI_PARA.FY_PART = '" + fyPartDropDownList.SelectedItem.Text.ToString() + "')");
            sbQuery.Append(" UNION ALL ");
            sbQuery.Append("SELECT 'NON-CIP:' AS CIP_TYPE, 'BRANCH' AS BRANCH, SUM(DIVIDEND.BALANCE) AS NO_UNITS, SUM(DIVIDEND.TOT_DIVI) AS TOTAL_DIVIDEND, ");
            sbQuery.Append(" NVL(SUM(DIVIDEND.CIP_QTY), 0) AS TOTAL_CIP, SUM(DIVIDEND.DIDUCT) AS TAX_DIDUCT, SUM(DIVIDEND.FI_DIVI_QTY) AS NET_DIVDEND ");
            sbQuery.Append(" FROM  DIVI_PARA INNER JOIN   DIVIDEND ON DIVI_PARA.FUND_CD = DIVIDEND.FUND_CD AND DIVI_PARA.CLOSE_DT = DIVIDEND.CLOSE_DT AND ");
            sbQuery.Append(" DIVI_PARA.F_YEAR = DIVIDEND.FY AND DIVI_PARA.DIVI_NO = DIVIDEND.DIVI_NO WHERE  (DIVIDEND.CIP = 'N') AND (DIVIDEND.REG_BR <> 'AMC/01') AND (DIVIDEND.VALID IS NULL)");
            sbQuery.Append(" AND (DIVI_PARA.FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')AND (DIVI_PARA.F_YEAR = '" + DividendFYDropDownList.SelectedItem.Text.ToString() + "')");
            sbQuery.Append(" AND (DIVI_PARA.CLOSE_DT = '" + ClosingDateDropDownList.SelectedItem.ToString().ToUpper() + "') AND (DIVI_PARA.FY_PART = '" + fyPartDropDownList.SelectedItem.Text.ToString() + "')");
            sbQuery.Append(" UNION ALL ");
            sbQuery.Append("SELECT 'CIP:' AS CIP_TYPE, 'HO' AS BRANCH, SUM(DIVIDEND.BALANCE) AS NO_UNITS, SUM(DIVIDEND.TOT_DIVI) AS TOTAL_DIVIDEND, ");
            sbQuery.Append(" NVL(SUM(DIVIDEND.CIP_QTY), 0) AS TOTAL_CIP, SUM(DIVIDEND.DIDUCT) AS TAX_DIDUCT, SUM(DIVIDEND.FI_DIVI_QTY) AS NET_DIVDEND ");
            sbQuery.Append(" FROM  DIVI_PARA INNER JOIN   DIVIDEND ON DIVI_PARA.FUND_CD = DIVIDEND.FUND_CD AND DIVI_PARA.CLOSE_DT = DIVIDEND.CLOSE_DT AND ");
            sbQuery.Append(" DIVI_PARA.F_YEAR = DIVIDEND.FY AND DIVI_PARA.DIVI_NO = DIVIDEND.DIVI_NO WHERE  (DIVIDEND.CIP = 'Y') AND (DIVIDEND.REG_BR = 'AMC/01') AND (DIVIDEND.VALID IS NULL)");
            sbQuery.Append(" AND (DIVI_PARA.FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')AND (DIVI_PARA.F_YEAR = '" + DividendFYDropDownList.SelectedItem.Text.ToString() + "')");
            sbQuery.Append(" AND (DIVI_PARA.CLOSE_DT = '" + ClosingDateDropDownList.SelectedItem.ToString().ToUpper() + "') AND (DIVI_PARA.FY_PART = '" + fyPartDropDownList.SelectedItem.Text.ToString() + "')");
            sbQuery.Append(" UNION ALL ");
            sbQuery.Append("SELECT 'CIP:' AS CIP_TYPE, 'BRANCH' AS BRANCH, SUM(DIVIDEND.BALANCE) AS NO_UNITS, SUM(DIVIDEND.TOT_DIVI) AS TOTAL_DIVIDEND, ");
            sbQuery.Append(" NVL(SUM(DIVIDEND.CIP_QTY), 0) AS TOTAL_CIP, SUM(DIVIDEND.DIDUCT) AS TAX_DIDUCT, SUM(DIVIDEND.FI_DIVI_QTY) AS NET_DIVDEND ");
            sbQuery.Append(" FROM  DIVI_PARA INNER JOIN   DIVIDEND ON DIVI_PARA.FUND_CD = DIVIDEND.FUND_CD AND DIVI_PARA.CLOSE_DT = DIVIDEND.CLOSE_DT AND ");
            sbQuery.Append(" DIVI_PARA.F_YEAR = DIVIDEND.FY AND DIVI_PARA.DIVI_NO = DIVIDEND.DIVI_NO WHERE  (DIVIDEND.CIP = 'Y') AND (DIVIDEND.REG_BR <> 'AMC/01') AND (DIVIDEND.VALID IS NULL)");
            sbQuery.Append(" AND (DIVI_PARA.FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')AND (DIVI_PARA.F_YEAR = '" + DividendFYDropDownList.SelectedItem.Text.ToString() + "')");
            sbQuery.Append(" AND (DIVI_PARA.CLOSE_DT = '" + ClosingDateDropDownList.SelectedItem.ToString().ToUpper() + "') AND (DIVI_PARA.FY_PART = '" + fyPartDropDownList.SelectedItem.Text.ToString() + "')");
        }
        else if (taxDeductListRadioButton.Checked)
        {
             statementType = "TAXLIST";
            sbQuery.Append("SELECT FUND_CD,FUND_NM,DIVI_NO,F_YEAR,CLOSE_DT,DIVI_RATE,BK_AC_NO ,BK_AC_NO_MICR||'C' AS BK_AC_NO_MICR,BK_NAME, BK_ADDRS1, BK_ADDRS2, BK_ROUTING_NO,BK_ROUTING_NO||'A' AS BK_ROUTING_NO_MICR,TIN,");
            sbQuery.Append(" BK_TRANSACTION_CODE, TO_CHAR(ISS_DT, 'DD-MON-YYYY') AS ISS_DT, HNAME, REG_NO, JNT_NAME, ADDRS1, ADDRS2, CITY, SPEC_IN1,SPEC_IN2,BK_FLAG,HOLDER_BK_ACC_NO,HOLDER_BK_NM_CD,HOLDER_BK_BR_NM_CD,");
            sbQuery.Append(" LPAD(WAR_NO, 7, '0') AS WAR_NO, 'C'||LPAD(WAR_NO, 7, '0')||'C' AS WAR_NO_MICR,NO_OF_UNITS, TOT_DIVI, TAX_DIDUCT, FI_DIVI_QTY, CIP_QTY, CIP_RATE, CIP, CIP_SL_NO,");
            sbQuery.Append(" TO_CHAR(AGM_DT, 'DD-MON-YYYY') AS AGM_DT,  TAX_RT_INDIVIDUAL, TAX_RT_INSTITUTION, REG_TYPE, FY_PART, NET_DIVI, FRAC_DIVI, REG_NUM,REG_BR,ID_FLAG,ID_AC,ID_BK_NM_CD,ID_BK_BR_NM_CD");

            sbQuery.Append(" FROM (SELECT DIVI_PARA.FUND_CD,FUND_INFO.FUND_NM, DIVI_PARA.DIVI_NO, DIVI_PARA.F_YEAR, TO_CHAR(DIVI_PARA.CLOSE_DT, 'DD-MON-YYYY') ");
            sbQuery.Append(" AS CLOSE_DT,NVL(U_MASTER.TIN,'') AS TIN,DIVI_PARA.RATE AS DIVI_RATE, DIVI_PARA.BK_AC_NO,DIVI_PARA.BK_AC_NO_MICR,DIVI_PARA.BK_NAME, DIVI_PARA.BK_ADDRS1, DIVI_PARA.BK_ADDRS2,");
            sbQuery.Append(" DIVI_PARA.BK_ROUTING_NO, DIVI_PARA.BK_TRANSACTION_CODE, DIVI_PARA.ISS_DT, U_MASTER.HNAME,U_MASTER.REG_BR, DIVIDEND.REG_BK || '/' || DIVIDEND.REG_BR || '/' || DIVIDEND.REG_NO AS ");
            sbQuery.Append(" REG_NO,DIVIDEND.REG_NO AS REG_NUM, U_JHOLDER.JNT_NAME,   U_MASTER.ADDRS1,");
            sbQuery.Append(" U_MASTER.ADDRS2, U_MASTER.CITY, DIVIDEND.WAR_NO, DIVIDEND.BALANCE AS NO_OF_UNITS, DIVIDEND.ID_FLAG,DIVIDEND.ID_AC,DIVIDEND.ID_BK_NM_CD,DIVIDEND.ID_BK_BR_NM_CD,");
            sbQuery.Append(" DIVIDEND.TOT_DIVI,DIVIDEND.DIDUCT AS TAX_DIDUCT, DIVIDEND.FI_DIVI_QTY, NVL(DIVIDEND.CIP_QTY, 0) AS CIP_QTY,DIVI_PARA.CIP_RATE, DIVIDEND.CIP,DIVIDEND.CIP_SL_NO,");
            sbQuery.Append(" DIVI_PARA.AGM_DT, DIVI_PARA.TAX_RT_INDIVIDUAL, DIVI_PARA.TAX_RT_INSTITUTION, U_MASTER.REG_TYPE, DIVI_PARA.FY_PART, DIVIDEND.TOT_DIVI - DIVIDEND.DIDUCT AS  NET_DIVI,");
            sbQuery.Append(" U_MASTER.SPEC_IN1,U_MASTER.SPEC_IN2,U_MASTER.BK_FLAG,U_MASTER.BK_AC_NO AS HOLDER_BK_ACC_NO,U_MASTER.BK_NM_CD AS HOLDER_BK_NM_CD,U_MASTER.BK_BR_NM_CD AS HOLDER_BK_BR_NM_CD,");

            sbQuery.Append(" DECODE(NVL(DIVIDEND.CIP_QTY, 0), 0, 0, DIVIDEND.FI_DIVI_QTY) AS FRAC_DIVI FROM DIVI_PARA INNER JOIN DIVIDEND ON DIVI_PARA.DIVI_NO = DIVIDEND.DIVI_NO ");
            sbQuery.Append("  AND DIVI_PARA.FUND_CD = DIVIDEND.FUND_CD  ");
            sbQuery.Append(" INNER JOIN FUND_INFO ON DIVI_PARA.FUND_CD = FUND_INFO.FUND_CD  INNER JOIN  U_MASTER ON DIVIDEND.REG_BK = U_MASTER.REG_BK AND DIVIDEND.REG_BR = U_MASTER.REG_BR AND");
            sbQuery.Append(" DIVIDEND.REG_NO = U_MASTER.REG_NO LEFT OUTER JOIN  U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR");
            sbQuery.Append(" AND U_MASTER.REG_NO = U_JHOLDER.REG_NO  WHERE  (DIVIDEND.VALID IS NULL) AND (DIVIDEND.DIDUCT>0)) ");
           
            
            sbQuery.Append(" WHERE (FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')AND (F_YEAR = '" + DividendFYDropDownList.SelectedItem.Text.ToString() + "')");
            sbQuery.Append(" AND (CLOSE_DT = '" + ClosingDateDropDownList.SelectedItem.ToString().ToUpper() + "') AND (FY_PART = '" + fyPartDropDownList.SelectedItem.Text.ToString() + "')");
            sbQuery.Append("  ORDER BY TO_NUMBER(WAR_NO)");
   

        }

        DataTable dtDividendInformation = commonGatewayObj.Select(sbQuery.ToString());
        DataTable dtDiviParaInformation = commonGatewayObj.Select("SELECT * FROM DIVI_PARA WHERE (FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')AND (F_YEAR = '" + DividendFYDropDownList.SelectedItem.Text.ToString() + "')AND (CLOSE_DT = '" + ClosingDateDropDownList.SelectedItem.ToString().ToUpper() + "') AND (FY_PART = '" + fyPartDropDownList.SelectedItem.Text.ToString() + "') ");

        if ((dtDividendInformation.Rows.Count > 0) && (dtDiviParaInformation.Rows.Count > 0))
        {
            
            Session["dtDividendInformation"] = dtDividendInformation;
            Session["dtDiviParaInformation"] = dtDiviParaInformation;
            Session["fundName"] = fundNameDropDownList.SelectedItem.Text.ToString().ToUpper();
            Session["statementType"] = statementType;
          
            ClientScript.RegisterStartupScript(this.GetType(), "UnitReportTaxCert", "window.open('ReportViewer/UnitReportTaxStatementAccReportViewer.aspx')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
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
        ClosingDateDropDownList.DataSource = diviDAOObj.dtGetFYWiseClosinDate(DividendFYDropDownList.SelectedItem.Text.ToString(), fundNameDropDownList.SelectedValue.ToString().ToUpper());
        ClosingDateDropDownList.DataTextField = "CLOSE_DT";
        ClosingDateDropDownList.DataValueField = "DIVI_NO";
        ClosingDateDropDownList.DataBind();

        fyPartDropDownList.DataSource = reportObj.getDtFYPart(fundNameDropDownList.SelectedValue.ToString().ToUpper());
        fyPartDropDownList.DataTextField = "FY_PART";
        fyPartDropDownList.DataValueField = "FY_PART";
        fyPartDropDownList.DataBind();     
    }
}
