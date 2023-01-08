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

public partial class UI_UnitReportFundPositionWithBalance : System.Web.UI.Page
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
            asOnDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");



        }
    
    }


    protected void printHolderButton_Click(object sender, EventArgs e)
    {
        StringBuilder sbQuery = new StringBuilder();
        StringBuilder sbSaleFilter = new StringBuilder();
        StringBuilder sbRepFilter = new StringBuilder();
        StringBuilder sbTrInFilter = new StringBuilder();
        StringBuilder sbTrOutFilter = new StringBuilder();
        string fund_Code = fundNameDropDownList.SelectedValue.ToString();
        string branch_Code = branchNameDropDownList.SelectedValue.ToString();
        string asOndate = asOnDateTextBox.Text.Trim();
        if (branchNameDropDownList.SelectedValue.ToString()!="0")
        {
            sbSaleFilter.Append(" AND REG_BR='" + branch_Code + "'");
            sbRepFilter.Append(" AND REG_BR='" + branch_Code + "'");
            sbTrInFilter.Append(" AND REG_BR_I='" + branch_Code + "'");
            sbTrOutFilter.Append(" AND REG_BR_O='" + branch_Code + "'");
        }
        if (asOnDateTextBox.Text!="")
        {
            sbSaleFilter.Append(" AND SL_DT<='" + asOndate + "'");
            sbRepFilter.Append(" AND REP_DT<='" + asOndate + "'");
            sbTrInFilter.Append(" AND TR_DT<='" + asOndate + "'");
            sbTrOutFilter.Append(" AND TR_DT<='" + asOndate + "'");
        }

        sbQuery.Append(" SELECT REG_BK,REG_BR,REG_NO, CIP, HNAME,ADDRS1,ADDRS2,CITY,TIN,TEL_NO,REG_TYPE,U_SL_BALANCE, U_REP_BALANCE , U_TR_IN_BALANCE, U_TR_OUT_BALANCE,F_BALANCE");
        sbQuery.Append(" FROM(   SELECT U.REG_BK,U.REG_BR,U.CIP, U.REG_NO,  U.HNAME,U.ADDRS1,U.ADDRS2,U.CITY,U.TIN, U.TEL_NO,U.REG_TYPE,  S.U_SL_BALANCE, ");
        sbQuery.Append(" NVL(R.U_REP_BALANCE,0) U_REP_BALANCE ,NVL(TI.U_TR_IN_BALANCE,0) U_TR_IN_BALANCE, ");       
        sbQuery.Append(" NVL(TU.U_TR_OUT_BALANCE,0) U_TR_OUT_BALANCE,  S.U_SL_BALANCE-NVL(R.U_REP_BALANCE,0)- NVL(TU.U_TR_OUT_BALANCE,0)+NVL(TI.U_TR_IN_BALANCE,0) AS F_BALANCE");
        sbQuery.Append(" FROM( SELECT REG_BK,REG_BR, REG_NO, DECODE(NVL(CIP,'N'),'Y','YES','NO') CIP,HNAME,ADDRS1,ADDRS2,CITY, TIN, TEL_NO,REG_TYPE FROM U_MASTER  WHERE REG_BK='" + fund_Code +"' ) U LEFT OUTER JOIN ");
        sbQuery.Append(" (SELECT REG_BK,REG_BR,REG_NO,SUM(QTY) AS U_SL_BALANCE FROM SALE  WHERE REG_BK='"+ fund_Code +"' ");
        sbQuery.Append(sbSaleFilter.ToString());
        sbQuery.Append(" GROUP BY  REG_BK,REG_BR,REG_NO) S ");
        sbQuery.Append(" ON U.REG_BK=S.REG_BK AND U.REG_BR=S.REG_BR AND U.REG_NO=S.REG_NO   LEFT OUTER JOIN (SELECT REG_BK,REG_BR,REG_NO,SUM(QTY) AS U_REP_BALANCE ");
        sbQuery.Append(" FROM REPURCHASE WHERE REG_BK='" + fund_Code + "' ");
        sbQuery.Append(sbRepFilter.ToString());
        sbQuery.Append(" GROUP BY  REG_BK,REG_BR,REG_NO ) R  ON  U.REG_BK=R.REG_BK AND U.REG_BR=R.REG_BR AND U.REG_NO=R.REG_NO  LEFT OUTER JOIN  ");
        sbQuery.Append(" (SELECT REG_BK_I REG_BK ,REG_BR_I REG_BR,REG_NO_I REG_NO,SUM(QTY) AS U_TR_IN_BALANCE FROM TRANSFER  WHERE  REG_BK_I='" + fund_Code + "' ");
        sbQuery.Append(sbTrInFilter.ToString());
        sbQuery.Append(" GROUP BY  REG_BK_I,REG_BR_I,REG_NO_I ) TI ");
        sbQuery.Append(" ON  U.REG_BK=TI.REG_BK AND U.REG_BR=TI.REG_BR AND U.REG_NO=TI.REG_NO  LEFT OUTER JOIN  ");
        sbQuery.Append(" (SELECT REG_BK_O REG_BK ,REG_BR_O REG_BR,REG_NO_O REG_NO,SUM(QTY) AS U_TR_OUT_BALANCE FROM TRANSFER    WHERE  REG_BK_O='" + fund_Code + "' ");
        sbQuery.Append(sbTrOutFilter.ToString());
        sbQuery.Append("  GROUP BY  REG_BK_O,REG_BR_O,REG_NO_O ) TU");
        sbQuery.Append("  ON  U.REG_BK=TU.REG_BK AND U.REG_BR=TU.REG_BR AND U.REG_NO=TU.REG_NO  ) A  WHERE A.F_BALANCE>0  ORDER BY REG_BK,REG_BR,REG_NO ");

        DataTable dtUnitFundPositionHolder = commonGatewayObj.Select(sbQuery.ToString());
        if (dtUnitFundPositionHolder.Rows.Count > 0)
        {
            Session["reportType"] = "HOLDER";
            Session["dtUnitFundPosition"] = dtUnitFundPositionHolder;
            Session["FUND_NAME"] = fundNameDropDownList.SelectedItem.Text.ToString();
            Session["BRANCH_NAME"] = branchNameDropDownList.SelectedItem.Text.ToString();
            Session["ASONDATE"] = asOnDateTextBox.Text.ToString();
            

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UnitReportTaxCert", "window.open('ReportViewer/UnitReportFundPositionReportViewer.aspx')", true);
        }
        else
        {
            Session["reportType"] = null;
            Session["dtUnitFundPosition"] = null;
            Session["FUND_NAME"] = null;
            Session["BRANCH_NAME"] = null;
            Session["ASONDATE"] = null;           
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
        }

    }

    protected void printFundSummaryButton_Click(object sender, EventArgs e)
    {

        StringBuilder sbQuery = new StringBuilder();
        StringBuilder sbSaleFilter = new StringBuilder();
        StringBuilder sbRepFilter = new StringBuilder();
        StringBuilder sbTrInFilter = new StringBuilder();
        StringBuilder sbTrOutFilter = new StringBuilder();
        string fund_Code = fundNameDropDownList.SelectedValue.ToString();
        string branch_Code = branchNameDropDownList.SelectedValue.ToString();
        string asOndate = asOnDateTextBox.Text.Trim();
        //if (branchNameDropDownList.SelectedValue.ToString() != "0")
        //{
        //    sbSaleFilter.Append(" AND REG_BR='" + branch_Code + "'");
        //    sbRepFilter.Append(" AND REG_BR='" + branch_Code + "'");
        //    sbTrInFilter.Append(" AND REG_BR_I='" + branch_Code + "'");
        //    sbTrOutFilter.Append(" AND REG_BR_O='" + branch_Code + "'");
        //}
        if (asOnDateTextBox.Text != "")
        {
            sbSaleFilter.Append(" AND SL_DT<='" + asOndate + "'");
            sbRepFilter.Append(" AND REP_DT<='" + asOndate + "'");
            sbTrInFilter.Append(" AND TR_DT<='" + asOndate + "'");
            sbTrOutFilter.Append(" AND TR_DT<='" + asOndate + "'");
        }
        sbQuery.Append("SELECT F.FUND_NM, COUNT(UNIT_BAL.REG_NO) INVESTOR,SUM(UNIT_BAL.F_BALANCE) AS BALANCE ,ROUND(SUM(F_AMOUNT)/10000000,2) AS AMOUNT FROM ( ");
        sbQuery.Append(" SELECT REG_BK,REG_BR,REG_NO,  HNAME,ADDRS1,ADDRS2,CITY,TIN,TEL_NO,REG_TYPE,U_SL_BALANCE, U_REP_BALANCE , U_TR_IN_BALANCE, U_TR_OUT_BALANCE,F_BALANCE,SL_AMOUNT-REP_AMOUNT AS F_AMOUNT ");
        sbQuery.Append(" FROM(   SELECT U.REG_BK,U.REG_BR, U.REG_NO,  U.HNAME,U.ADDRS1,U.ADDRS2,U.CITY,U.TIN, U.TEL_NO,U.REG_TYPE,  S.U_SL_BALANCE, ");
        sbQuery.Append(" NVL(R.U_REP_BALANCE,0) U_REP_BALANCE ,NVL(TI.U_TR_IN_BALANCE,0) U_TR_IN_BALANCE,  NVL(S.SL_AMOUNT,0) SL_AMOUNT,NVL(R.REP_AMOUNT,0) REP_AMOUNT, ");
        sbQuery.Append(" NVL(TU.U_TR_OUT_BALANCE,0) U_TR_OUT_BALANCE,  S.U_SL_BALANCE-NVL(R.U_REP_BALANCE,0) AS F_BALANCE");
        sbQuery.Append(" FROM( SELECT REG_BK,REG_BR, REG_NO, HNAME,ADDRS1,ADDRS2,CITY, TIN, TEL_NO,REG_TYPE FROM U_MASTER  WHERE 1=1 ) U LEFT OUTER JOIN ");
        sbQuery.Append(" (SELECT REG_BK,REG_BR,REG_NO,SUM(QTY) AS U_SL_BALANCE, ROUND(SUM(QTY*SL_PRICE),2) AS SL_AMOUNT FROM SALE  WHERE 1=1 ");
        sbQuery.Append(sbSaleFilter.ToString());
        sbQuery.Append(" GROUP BY  REG_BK,REG_BR,REG_NO) S ");
        sbQuery.Append(" ON U.REG_BK=S.REG_BK AND U.REG_BR=S.REG_BR AND U.REG_NO=S.REG_NO   LEFT OUTER JOIN (SELECT REG_BK,REG_BR,REG_NO,SUM(QTY) AS U_REP_BALANCE ");
        sbQuery.Append(" ,ROUND(SUM(QTY*REP_PRICE),2) AS REP_AMOUNT FROM REPURCHASE WHERE 1=1  ");
        sbQuery.Append(sbRepFilter.ToString());
        sbQuery.Append(" GROUP BY  REG_BK,REG_BR,REG_NO ) R  ON  U.REG_BK=R.REG_BK AND U.REG_BR=R.REG_BR AND U.REG_NO=R.REG_NO  LEFT OUTER JOIN  ");
        sbQuery.Append(" (SELECT REG_BK_I REG_BK ,REG_BR_I REG_BR,REG_NO_I REG_NO,SUM(QTY) AS U_TR_IN_BALANCE FROM TRANSFER  WHERE 1=1 ");
        sbQuery.Append(sbTrInFilter.ToString());
        sbQuery.Append(" GROUP BY  REG_BK_I,REG_BR_I,REG_NO_I ) TI ");
        sbQuery.Append(" ON  U.REG_BK=TI.REG_BK AND U.REG_BR=TI.REG_BR AND U.REG_NO=TI.REG_NO  LEFT OUTER JOIN  ");
        sbQuery.Append(" (SELECT REG_BK_O REG_BK ,REG_BR_O REG_BR,REG_NO_O REG_NO,SUM(QTY) AS U_TR_OUT_BALANCE FROM TRANSFER   WHERE 1=1 ");
        sbQuery.Append(sbTrOutFilter.ToString());
        sbQuery.Append("  GROUP BY  REG_BK_O,REG_BR_O,REG_NO_O ) TU");
        sbQuery.Append("  ON  U.REG_BK=TU.REG_BK AND U.REG_BR=TU.REG_BR AND U.REG_NO=TU.REG_NO  ) A  WHERE A.F_BALANCE>0 ) UNIT_BAL ");
        sbQuery.Append("  INNER JOIN FUND_INFO F ON UNIT_BAL.REG_BK=F.FUND_CD   GROUP BY  UNIT_BAL.REG_BK, F.FUND_NM ORDER BY UNIT_BAL.REG_BK ");

        DataTable dtUnitFundPositionFundSummary = commonGatewayObj.Select(sbQuery.ToString());
        if (dtUnitFundPositionFundSummary.Rows.Count > 0)
        {
            Session["reportType"] = "SUMMARY";
            Session["dtUnitFundPosition"] = dtUnitFundPositionFundSummary;
            Session["FUND_NAME"] = fundNameDropDownList.SelectedItem.Text.ToString();
            Session["BRANCH_NAME"] = branchNameDropDownList.SelectedItem.Text.ToString();
            Session["ASONDATE"] = asOnDateTextBox.Text.ToString();


            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UnitReportTaxCert", "window.open('ReportViewer/UnitReportFundPositionReportViewer.aspx')", true);
        }
        else
        {
            Session["reportType"] = null;
            Session["dtUnitFundPosition"] = null;
            Session["FUND_NAME"] = null;
            Session["BRANCH_NAME"] = null;
            Session["ASONDATE"] = null;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
        }
    }
}
