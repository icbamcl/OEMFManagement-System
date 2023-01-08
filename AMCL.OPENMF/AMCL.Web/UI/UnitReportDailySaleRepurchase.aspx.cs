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

public partial class UI_UnitReportDailySaleRepurchase : System.Web.UI.Page
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

            branchNameDropDownList.SelectedValue = "AMC/01";



            fromDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            toDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");


        }
    
    }
   
    
   
    
    
    protected void findButton_Click(object sender, EventArgs e)
    {
        DataTable dtUnitFundPosition = dtFundPosition();
        DataTable dtUnitFundPositionTotal = dtFundPositionTotal();

        dtUnitFundPosition.Merge(dtUnitFundPositionTotal);
        if (dtUnitFundPosition.Rows.Count > 0)
        {
            dvLedger.Visible = true;
            SurrenderListGridView.DataSource = dtUnitFundPosition;
            SurrenderListGridView.DataBind();
        }
        else
        {
            dvLedger.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert('No Data Found');", true);
        }



    }

   
    public DataTable dtFundPosition()
    {
        StringBuilder sbQuery = new StringBuilder();
        DataTable dtUnitFundPosition = new DataTable();
        sbQuery.Append(" SELECT A.FUND_NM , A.SL_CASH_AMT,A.SL_CHQ_AMT, A.SL_CASH_AMT+A.SL_CHQ_AMT AS TOTAL_SL_AMT, A.REP_EFT_AMT,A.REP_CHQ_AMT, A.REP_EFT_AMT+A.REP_CHQ_AMT AS TOTAL_REP_AMT");
        sbQuery.Append(" ,( A.SL_CASH_AMT+A.SL_CHQ_AMT)-(A.REP_EFT_AMT+A.REP_CHQ_AMT) AS NET_AMOUNT,A.TOTAL_SL_UNIT,A.TOTAL_REP_UNIT,A.TOTAL_SL_UNIT-A.TOTAL_REP_UNIT AS NET_UNIT ");
        sbQuery.Append(" FROM ( SELECT MONEY_RECEIPT.REG_BK,SUM(DECODE(MONEY_RECEIPT.RECEIPT_TYPE, 'SL', MONEY_RECEIPT.UNIT_QTY,0)) AS TOTAL_SL_UNIT, SUM(DECODE(MONEY_RECEIPT.RECEIPT_TYPE, 'SL', DECODE(MONEY_RECEIPT.PAY_TYPE, 'CASH', MONEY_RECEIPT.UNIT_QTY * MONEY_RECEIPT.RATE, 'BOTH', ");
        sbQuery.Append(" MONEY_RECEIPT.CASH_AMT, 'MULT', MONEY_RECEIPT.CASH_AMT, 0), 0)) AS SL_CASH_AMT, SUM(DECODE(MONEY_RECEIPT.RECEIPT_TYPE, 'SL', DECODE(MONEY_RECEIPT.PAY_TYPE, 'CHQ', ");
        sbQuery.Append(" MONEY_RECEIPT.UNIT_QTY * MONEY_RECEIPT.RATE, 'BOTH', MONEY_RECEIPT.UNIT_QTY * MONEY_RECEIPT.RATE - NVL(MONEY_RECEIPT.CASH_AMT, 0), 'MULT', ");
        sbQuery.Append(" MONEY_RECEIPT.UNIT_QTY * MONEY_RECEIPT.RATE - NVL(MONEY_RECEIPT.CASH_AMT, 0), 0), 0)) AS SL_CHQ_AMT, SUM(DECODE(MONEY_RECEIPT.RECEIPT_TYPE, 'REP', DECODE(MONEY_RECEIPT.REP_PAY_TYPE, 'EFT', ");
        sbQuery.Append(" MONEY_RECEIPT.UNIT_QTY * MONEY_RECEIPT.RATE, 0), 0)) AS REP_EFT_AMT, SUM(DECODE(MONEY_RECEIPT.RECEIPT_TYPE, 'REP', DECODE(MONEY_RECEIPT.REP_PAY_TYPE, 'CHQ', ");
        sbQuery.Append(" MONEY_RECEIPT.UNIT_QTY * MONEY_RECEIPT.RATE, 0), 0)) AS REP_CHQ_AMT,SUM(DECODE(MONEY_RECEIPT.RECEIPT_TYPE, 'REP', MONEY_RECEIPT.UNIT_QTY,0)) AS TOTAL_REP_UNIT, FUND_INFO.FUND_CD, FUND_INFO.FUND_NM, FUND_INFO.FUND_SUB_OPEN_DT");
        sbQuery.Append(" FROM  MONEY_RECEIPT, FUND_INFO  WHERE MONEY_RECEIPT.REG_BK = FUND_INFO.FUND_CD AND MONEY_RECEIPT.VALID IS NULL ");
        if(fundNameDropDownList.SelectedValue.ToString()!="0")
        {
            sbQuery.Append(" AND  MONEY_RECEIPT.REG_BK='"+ fundNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
        }
        if (branchNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbQuery.Append(" AND  MONEY_RECEIPT.REG_BR='" + branchNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
        }
        if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
        {
            sbQuery.Append(" AND (MONEY_RECEIPT.RECEIPT_DATE BETWEEN  '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "')");
        }
       

        sbQuery.Append(" GROUP BY MONEY_RECEIPT.REG_BK, FUND_INFO.FUND_CD, FUND_INFO.FUND_NM, FUND_INFO.FUND_SUB_OPEN_DT ");
        sbQuery.Append(" ORDER BY FUND_INFO.FUND_SUB_OPEN_DT) A ");
        dtUnitFundPosition = commonGatewayObj.Select(sbQuery.ToString());

        return dtUnitFundPosition;

    }
    public DataTable dtFundPositionTotal()
    {
        StringBuilder sbQuery = new StringBuilder();
        DataTable dtUnitFundPositionTotal = new DataTable();
        sbQuery.Append(" SELECT 'Total'  AS FUND_NM, SUM( A.SL_CASH_AMT) AS SL_CASH_AMT ,SUM(A.SL_CHQ_AMT) AS SL_CHQ_AMT ,SUM( A.SL_CASH_AMT+A.SL_CHQ_AMT )AS TOTAL_SL_AMT, SUM( A.REP_EFT_AMT) REP_EFT_AMT,SUM(A.REP_CHQ_AMT) REP_CHQ_AMT, SUM( A.REP_EFT_AMT+A.REP_CHQ_AMT) AS TOTAL_REP_AMT ");
        sbQuery.Append(" ,SUM( A.SL_CASH_AMT+A.SL_CHQ_AMT)-SUM(A.REP_EFT_AMT+A.REP_CHQ_AMT) AS NET_AMOUNT ");
        sbQuery.Append(" FROM ( SELECT MONEY_RECEIPT.REG_BK, SUM(DECODE(MONEY_RECEIPT.RECEIPT_TYPE, 'SL', DECODE(MONEY_RECEIPT.PAY_TYPE, 'CASH', MONEY_RECEIPT.UNIT_QTY * MONEY_RECEIPT.RATE, 'BOTH', ");
        sbQuery.Append(" MONEY_RECEIPT.CASH_AMT, 'MULT', MONEY_RECEIPT.CASH_AMT, 0), 0)) AS SL_CASH_AMT, SUM(DECODE(MONEY_RECEIPT.RECEIPT_TYPE, 'SL', DECODE(MONEY_RECEIPT.PAY_TYPE, 'CHQ', ");
        sbQuery.Append(" MONEY_RECEIPT.UNIT_QTY * MONEY_RECEIPT.RATE, 'BOTH', MONEY_RECEIPT.UNIT_QTY * MONEY_RECEIPT.RATE - NVL(MONEY_RECEIPT.CASH_AMT, 0), 'MULT', ");
        sbQuery.Append(" MONEY_RECEIPT.UNIT_QTY * MONEY_RECEIPT.RATE - NVL(MONEY_RECEIPT.CASH_AMT, 0), 0), 0)) AS SL_CHQ_AMT, SUM(DECODE(MONEY_RECEIPT.RECEIPT_TYPE, 'REP', DECODE(MONEY_RECEIPT.REP_PAY_TYPE, 'EFT', ");
        sbQuery.Append(" MONEY_RECEIPT.UNIT_QTY * MONEY_RECEIPT.RATE, 0), 0)) AS REP_EFT_AMT, SUM(DECODE(MONEY_RECEIPT.RECEIPT_TYPE, 'REP', DECODE(MONEY_RECEIPT.REP_PAY_TYPE, 'CHQ', ");
        sbQuery.Append(" MONEY_RECEIPT.UNIT_QTY * MONEY_RECEIPT.RATE, 0), 0)) AS REP_CHQ_AMT, FUND_INFO.FUND_CD, FUND_INFO.FUND_NM, FUND_INFO.FUND_SUB_OPEN_DT");
        sbQuery.Append(" FROM  MONEY_RECEIPT, FUND_INFO  WHERE MONEY_RECEIPT.REG_BK = FUND_INFO.FUND_CD AND  MONEY_RECEIPT.VALID IS NULL ");
        if (fundNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbQuery.Append(" AND  MONEY_RECEIPT.REG_BK='" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
        }
        if (branchNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbQuery.Append(" AND  MONEY_RECEIPT.REG_BR='" + branchNameDropDownList.SelectedValue.ToString().ToUpper() + "'");
        }
        if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
        {
            sbQuery.Append(" AND (MONEY_RECEIPT.RECEIPT_DATE BETWEEN  '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "')");
        }


        sbQuery.Append(" GROUP BY MONEY_RECEIPT.REG_BK, FUND_INFO.FUND_CD, FUND_INFO.FUND_NM, FUND_INFO.FUND_SUB_OPEN_DT ");
        sbQuery.Append(" ORDER BY FUND_INFO.FUND_SUB_OPEN_DT) A ");
        dtUnitFundPositionTotal = commonGatewayObj.Select(sbQuery.ToString());
        return dtUnitFundPositionTotal;


    }
}
