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


public partial class UI_UnitReportIntimation : System.Web.UI.Page
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
        StringBuilder sbQuery = new StringBuilder();
        sbQuery.Append("SELECT   U_MASTER.HNAME, U_JHOLDER.JNT_NAME, U_MASTER.ADDRS1, U_MASTER.CITY, U_MASTER.ADDRS2, DIVI_PARA.F_YEAR, DIVI_PARA.FY_PART, DECODE(DIVIDEND.DIDUCT, 0, 0, ROUND((DIVIDEND.DIDUCT * 100) / (DIVIDEND.TOT_DIVI - DIVI_PARA.TAX_LIMIT), 2)) AS TAX_RATE, ");
        sbQuery.Append(" DIVI_PARA.RATE, DIVI_PARA.AGM_DT, DIVI_PARA.ISS_DT, DIVIDEND.FUND_CD, DIVIDEND.DIVI_NO, DIVIDEND.FY, DIVIDEND.CLOSE_DT, DECODE(DIVIDEND.CIP,'N',0,DIVIDEND.FI_DIVI_QTY) AS FRAC_DIVI,");
        sbQuery.Append(" LPAD(DIVIDEND.WAR_NO, 7, '0') AS WAR_NO , DIVIDEND.REG_BK, DIVIDEND.REG_BR, DIVIDEND.REG_NO, DIVIDEND.TOT_DIVI, DIVIDEND.DIDUCT, DIVIDEND.FI_DIVI_QTY,NVL(CIP_QTY,0) AS CIP_QTY,");
        sbQuery.Append(" DIVIDEND.CIP_SL_NO, DIVIDEND.USER_NM, DIVIDEND.ENT_DT, DIVIDEND.REMARKS, DIVIDEND.VALID, DIVIDEND.BALANCE,DIVIDEND.TOT_DIVI-DIVIDEND.DIDUCT AS NET_DIVI ,");
        sbQuery.Append(" DIVIDEND.ENT_TM, DIVIDEND.CIP, DIVIDEND.ID_FLAG, DIVIDEND.BK_FLAG, DIVIDEND.DIVI_RATE, DIVIDEND.CIP_RATE, DIVIDEND.ADDITIONAL_PAY, ");
        sbQuery.Append(" DIVIDEND.DIVI_STATUS, DIVIDEND.WAR_DELEVARY, DIVIDEND.WAR_DELEVARY_DT, DIVIDEND.WAR_BK_PAY, DIVIDEND.WAR_BK_PAY_DT, ");
        sbQuery.Append(" DIVIDEND.ID_BK_BR_NM_CD,CASE  WHEN LENGTH(DIVIDEND.BK_AC_NO)>13 THEN SUBSTR(DIVIDEND.BK_AC_NO, - 13) ELSE DIVIDEND.BK_AC_NO  END AS BK_AC_NO, DIVIDEND.BK_NM_CD, DIVIDEND.BK_BR_NM_CD, DIVIDEND.IS_BEFTN,  DIVIDEND.IS_BEFTN_SUCCS, DIVIDEND.BEFTN_CREDIT_DT, DIVIDEND.BEFTN_RETURN_DT,");
        sbQuery.Append(" BANK_NAME.BANK_NAME,BANK_BRANCH.BRANCH_NAME FROM DIVIDEND INNER JOIN DIVI_PARA ON DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.FY = DIVI_PARA.F_YEAR AND  DIVIDEND.CLOSE_DT = DIVI_PARA.CLOSE_DT AND DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO LEFT OUTER JOIN ");
        sbQuery.Append(" BANK_NAME INNER JOIN  BANK_BRANCH ON BANK_NAME.BANK_CODE = BANK_BRANCH.BANK_CODE ON DIVIDEND.BK_NM_CD = BANK_BRANCH.BANK_CODE AND ");
        sbQuery.Append(" DIVIDEND.BK_BR_NM_CD = BANK_BRANCH.BRANCH_CODE LEFT OUTER JOIN    U_MASTER ON DIVIDEND.REG_BK = U_MASTER.REG_BK AND DIVIDEND.REG_BR = U_MASTER.REG_BR AND ");
        sbQuery.Append(" DIVIDEND.REG_NO = U_MASTER.REG_NO LEFT OUTER JOIN  U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND ");
        sbQuery.Append(" U_MASTER.REG_NO = U_JHOLDER.REG_NO ");
        
       
        sbQuery.Append(" WHERE  1=1 ");
        sbQuery.Append(" AND (DIVI_PARA.FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')AND (DIVI_PARA.F_YEAR = '" + DividendFYDropDownList.SelectedItem.Text.ToString() + "')");
        sbQuery.Append(" AND (DIVI_PARA.CLOSE_DT = '" + ClosingDateDropDownList.SelectedItem.ToString().ToUpper() + "') AND (DIVI_PARA.FY_PART = '" + fyPartDropDownList.SelectedItem.Text.ToString() + "')");
        if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text == "")
        {
            sbQuery.Append(" AND TO_NUMBER(DIVIDEND.WAR_NO)>=" + Convert.ToUInt32(fromWar_NoTextBox.Text.Trim().ToString()));
        }
        else if (fromWar_NoTextBox.Text == "" && toWar_NoTextBox.Text != "")
        {
            sbQuery.Append(" AND TO_NUMBER(DIVIDEND.WAR_NO)<=" + Convert.ToUInt32(toWar_NoTextBox.Text.Trim().ToString()));
        }
        else if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text != "")
        {
            sbQuery.Append(" AND TO_NUMBER(DIVIDEND.WAR_NO) BETWEEN " + Convert.ToUInt32(fromWar_NoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToUInt32(toWar_NoTextBox.Text.Trim().ToString()));
        }
        sbQuery.Append(" ORDER BY WAR_NO ");
        DataTable dtDividendInformation = commonGatewayObj.Select(sbQuery.ToString());
        DataTable dtDiviParaInformation = commonGatewayObj.Select("SELECT * FROM DIVI_PARA WHERE (FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')AND (F_YEAR = '" + DividendFYDropDownList.SelectedItem.Text.ToString() + "')AND (CLOSE_DT = '" + ClosingDateDropDownList.SelectedItem.ToString().ToUpper() + "') AND (FY_PART = '" + fyPartDropDownList.SelectedItem.Text.ToString() + "') ");

        if ((dtDividendInformation.Rows.Count > 0) && (dtDiviParaInformation.Rows.Count > 0))
        {
            
            Session["dtDividendInformation"] = dtDividendInformation;
            Session["dtDiviParaInformation"] = dtDiviParaInformation;
            Session["fundName"] = fundNameDropDownList.SelectedItem.Text.ToString().ToUpper();
            Session["fundCode"] = fundNameDropDownList.SelectedValue.ToString().ToUpper();
            Session["BEFTN_Issue_Date"] = BEFTNIssueDateTextBox.Text.Trim().ToString();

            ClientScript.RegisterStartupScript(this.GetType(), "UnitReportTaxCert", "window.open('ReportViewer/UnitReportIntimationLetterReportViewer.aspx')", true);
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
