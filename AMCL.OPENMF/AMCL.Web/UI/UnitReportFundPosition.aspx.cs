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

public partial class UI_UnitReportFundPosition : System.Web.UI.Page
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
   
    
   
    
    
    protected void findButton_Click(object sender, EventArgs e)
    {
        DataTable dtUnitFundPosition = dtFundPosition();
        DataRow drUnitFundPosition;
        drUnitFundPosition = dtUnitFundPosition.NewRow();
        drUnitFundPosition["SI"] = DBNull.Value;
        drUnitFundPosition["TRANS_TYPE"] = "Net (1-5)) ";
        drUnitFundPosition["TOTAL_UNIT"] = Convert.ToInt64(dtUnitFundPosition.Rows[0]["TOTAL_UNIT"].ToString()) - Convert.ToInt64(dtUnitFundPosition.Rows[4]["TOTAL_UNIT"].ToString()) ;
        drUnitFundPosition["TOTAL_AMT"] =decimal.Parse( dtUnitFundPosition.Rows[0]["TOTAL_AMT"].ToString()) - decimal.Parse(dtUnitFundPosition.Rows[4]["TOTAL_AMT"].ToString());
        drUnitFundPosition["TOTAL_HOLD"] = DBNull.Value;
        

        dtUnitFundPosition.Rows.Add(drUnitFundPosition);
        dgFundPosition.DataSource = dtUnitFundPosition;
        dgFundPosition.DataBind();
    }

    protected void PrintButton_Click(object sender, EventArgs e)
    {
        DataTable dtUnitFundPosition = dtFundPosition();
        DataRow drUnitFundPosition;
        drUnitFundPosition = dtUnitFundPosition.NewRow();
        drUnitFundPosition["SI"] = DBNull.Value;
        drUnitFundPosition["TRANS_TYPE"] = "Net (1-5)) ";
        drUnitFundPosition["TOTAL_UNIT"] = Convert.ToInt64(dtUnitFundPosition.Rows[0]["TOTAL_UNIT"].ToString()) - Convert.ToInt64(dtUnitFundPosition.Rows[4]["TOTAL_UNIT"].ToString()) ;
        drUnitFundPosition["TOTAL_AMT"] = decimal.Parse(dtUnitFundPosition.Rows[0]["TOTAL_AMT"].ToString()) - decimal.Parse(dtUnitFundPosition.Rows[4]["TOTAL_AMT"].ToString());
        drUnitFundPosition["TOTAL_HOLD"] = DBNull.Value;
        drUnitFundPosition["TOTAL_TRANS"] = DBNull.Value;
        dtUnitFundPosition.Rows.Add(drUnitFundPosition);

        if (dtUnitFundPosition.Rows.Count > 0)
        {
            Session["reportType"] = "POSITION";
            Session["dtUnitFundPosition"] = dtUnitFundPosition;
            Session["FUND_NAME"] = fundNameDropDownList.SelectedItem.Text.ToString();
            Session["BRANCH_NAME"] = branchNameDropDownList.SelectedItem.Text.ToString();
            Session["DATE_FROM"] = fromDateTextBox.Text.ToString();
            Session["DATE_TO"] = toDateTextBox.Text.ToString();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UnitReportTaxCert", "window.open('ReportViewer/UnitReportFundPositionReportViewer.aspx')", true);
        }
        else
        {
            Session["reportType"] = null;
            Session["dtUnitFundPosition"] = null;
            Session["FUND_NAME"] = null;
            Session["BRANCH_NAME"] = null;
            Session["DATE_FROM"] = null;
            Session["DATE_TO"] = null;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
        }



    }
    public DataTable dtFundPosition()
    {
        StringBuilder sbQuery = new StringBuilder();
        DataTable dtUnitFundPosition = new DataTable();

        sbQuery.Append(" SELECT  1 AS SI, 'Sale (Including CIP,SIP)' AS TRANS_TYPE, NVL(SUM(QTY),0) AS TOTAL_UNIT, NVL(SUM(QTY * SL_PRICE),0) AS TOTAL_AMT, COUNT(DISTINCT REG_NO) AS TOTAL_HOLD,COUNT(*) AS TOTAL_TRANS ");
        sbQuery.Append(" FROM  SALE WHERE  (REG_BK = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "') ");
        if (branchNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbQuery.Append(" AND (REG_BR = '" + branchNameDropDownList.SelectedValue.ToString().ToUpper() + "')");

        }
        if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
        {
            sbQuery.Append(" AND (SL_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "')");
        }
        sbQuery.Append("UNION ALL");
        sbQuery.Append(" SELECT  2 AS SI, 'Sale (Excluding CIP,SIP)' AS TRANS_TYPE, NVL(SUM(QTY),0) AS TOTAL_UNIT, NVL(SUM(QTY * SL_PRICE),0) AS TOTAL_AMT, COUNT(DISTINCT REG_NO) AS TOTAL_HOLD ,COUNT(*) AS TOTAL_TRANS ");
        sbQuery.Append(" FROM  SALE WHERE  (REG_BK = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "') AND (SL_TYPE = 'SL')");
        if (branchNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbQuery.Append(" AND (REG_BR = '" + branchNameDropDownList.SelectedValue.ToString().ToUpper() + "')");

        }
        if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
        {
            sbQuery.Append(" AND (SL_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "')");
        }
        sbQuery.Append("UNION ALL");
        sbQuery.Append(" SELECT  3 AS SI, 'SIP' AS TRANS_TYPE, NVL(SUM(QTY),0) AS TOTAL_UNIT, NVL(SUM(QTY * SL_PRICE),0) AS TOTAL_AMT, COUNT(DISTINCT REG_NO) AS TOTAL_HOLD ,COUNT(*) AS TOTAL_TRANS ");
        sbQuery.Append(" FROM  SALE WHERE  (REG_BK = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "') AND  (SL_TYPE = 'SIP')");
        if (branchNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbQuery.Append(" AND (REG_BR = '" + branchNameDropDownList.SelectedValue.ToString().ToUpper() + "')");

        }
        if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
        {
            sbQuery.Append(" AND (SL_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "')");
        }
        sbQuery.Append("UNION ALL");
        sbQuery.Append(" SELECT  4 AS SI, 'CIP' AS TRANS_TYPE, NVL(SUM(QTY),0) AS TOTAL_UNIT, NVL(SUM(QTY * SL_PRICE),0) AS TOTAL_AMT, COUNT(DISTINCT REG_NO) AS TOTAL_HOLD ,COUNT(*) AS TOTAL_TRANS ");
        sbQuery.Append(" FROM  SALE WHERE  (REG_BK = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "') AND  (SL_TYPE = 'CIP')");
        if (branchNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbQuery.Append(" AND (REG_BR = '" + branchNameDropDownList.SelectedValue.ToString().ToUpper() + "')");

        }
        if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
        {
            sbQuery.Append(" AND (SL_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "')");
        }
        sbQuery.Append("UNION ALL");
        sbQuery.Append(" SELECT  5 AS SI, 'Repurchase' AS TRANS_TYPE, NVL(SUM(QTY),0) AS TOTAL_UNIT, NVL(SUM(QTY * REP_PRICE),0) AS TOTAL_AMT, COUNT(DISTINCT REG_NO) AS TOTAL_HOLD ,COUNT(*) AS TOTAL_TRANS ");
        sbQuery.Append(" FROM  REPURCHASE WHERE  (REG_BK = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')");
        if (branchNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbQuery.Append(" AND (REG_BR = '" + branchNameDropDownList.SelectedValue.ToString().ToUpper() + "')");

        }
        if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
        {
            sbQuery.Append(" AND (REP_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "')");
        }
        if (branchNameDropDownList.SelectedValue == "0")
        {

            sbQuery.Append("UNION ALL");
            sbQuery.Append(" SELECT  6 AS SI, 'Transfer' AS TRANS_TYPE, NVL(SUM(QTY),0) AS TOTAL_UNIT, 0 AS TOTAL_AMT, COUNT(DISTINCT REG_NO_I) AS TOTAL_HOLD ,COUNT(*) AS TOTAL_TRANS ");
            sbQuery.Append(" FROM  TRANSFER WHERE  (F_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')");
            // sbQuery.Append(" AND (REG_BR_O = REG_BR_I) AND (REG_BR_O = '" + branchNameDropDownList.SelectedValue.ToString() + "') AND (REG_BR_I = '" + branchNameDropDownList.SelectedValue.ToString() + "') ");

            if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
            {
                sbQuery.Append(" AND (TR_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "')");
            }
            sbQuery.Append("UNION ALL");
            sbQuery.Append(" SELECT  7 AS SI, 'Transfer IN ' AS TRANS_TYPE, NVL(SUM(QTY),0) AS TOTAL_UNIT, 0 AS TOTAL_AMT, COUNT(DISTINCT REG_NO_I) AS TOTAL_HOLD ,COUNT(*) AS TOTAL_TRANS ");
            sbQuery.Append(" FROM  TRANSFER WHERE  (F_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')");
            sbQuery.Append(" AND (REG_BR_O <> REG_BR_I) ");
            if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
            {
                sbQuery.Append(" AND (TR_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "')");
            }

            sbQuery.Append("UNION ALL");
            sbQuery.Append(" SELECT  8 AS SI, 'Transfer OUT ' AS TRANS_TYPE, NVL(SUM(QTY),0) AS TOTAL_UNIT, 0 AS TOTAL_AMT, COUNT(DISTINCT REG_NO_I) AS TOTAL_HOLD ,COUNT(*) AS TOTAL_TRANS ");
            sbQuery.Append(" FROM  TRANSFER WHERE  (F_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')");

            sbQuery.Append(" AND (REG_BR_O <> REG_BR_I) ");

            if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
            {
                sbQuery.Append(" AND (TR_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "')");
            }


        }
        else if (branchNameDropDownList.SelectedValue != "0")
        {

            sbQuery.Append("UNION ALL");
            sbQuery.Append(" SELECT  6 AS SI, 'Intra Branch Transfer' AS TRANS_TYPE, NVL(SUM(QTY),0) AS TOTAL_UNIT, 0 AS TOTAL_AMT, COUNT(DISTINCT REG_NO_I) AS TOTAL_HOLD ,COUNT(*) AS TOTAL_TRANS ");
            sbQuery.Append(" FROM  TRANSFER WHERE  (F_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')");
            sbQuery.Append(" AND (REG_BR_O = REG_BR_I) AND (REG_BR_O = '" + branchNameDropDownList.SelectedValue.ToString() + "') AND (REG_BR_I = '" + branchNameDropDownList.SelectedValue.ToString() + "') ");

            if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
            {
                sbQuery.Append(" AND (TR_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "')");
            }
            sbQuery.Append("UNION ALL");
            sbQuery.Append(" SELECT  7 AS SI, 'Inter Branch Transfer IN ' AS TRANS_TYPE, NVL(SUM(QTY),0) AS TOTAL_UNIT, 0 AS TOTAL_AMT, COUNT(DISTINCT REG_NO_I) AS TOTAL_HOLD ,COUNT(*) AS TOTAL_TRANS ");
            sbQuery.Append(" FROM  TRANSFER WHERE  (F_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')");
            sbQuery.Append(" AND (REG_BR_O <> REG_BR_I)AND (REG_BR_O <> '" + branchNameDropDownList.SelectedValue.ToString() + "') AND (REG_BR_I = '" + branchNameDropDownList.SelectedValue.ToString() + "') ");

            if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
            {
                sbQuery.Append(" AND (TR_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "')");
            }

            sbQuery.Append("UNION ALL");
            sbQuery.Append(" SELECT  8 AS SI, 'Inter Branch Transfer OUT ' AS TRANS_TYPE, NVL(SUM(QTY),0) AS TOTAL_UNIT, 0 AS TOTAL_AMT, COUNT(DISTINCT REG_NO_I) AS TOTAL_HOLD ,COUNT(*) AS TOTAL_TRANS");
            sbQuery.Append(" FROM  TRANSFER WHERE  (F_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')");

            sbQuery.Append(" AND (REG_BR_O <>REG_BR_I) AND (REG_BR_O = '" + branchNameDropDownList.SelectedValue.ToString() + "') AND (REG_BR_I <> '" + branchNameDropDownList.SelectedValue.ToString() + "') ");

            if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
            {
                sbQuery.Append(" AND (TR_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "')");
            }


        }
        if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
        {
            sbQuery.Append(" UNION ALL");
            sbQuery.Append(" SELECT  9 AS SI, 'CIP including 0 ' AS TRANS_TYPE, NVL(SUM(BALANCE),0) AS TOTAL_UNIT, 0 AS TOTAL_AMT, COUNT( DISTINCT REG_NO) AS TOTAL_HOLD, COUNT(*) AS TOTAL_TRANS ");
            sbQuery.Append(" FROM    (SELECT  U.REG_BK, U.REG_BR, U.REG_NO, U.CIP, NVL(S.QTY, 0) AS SALE, NVL(R.QTY, 0) AS REPURCHASE, NVL(TOUT.QTY, 0) AS TR_OUT, ");
            sbQuery.Append(" NVL(TIN.QTY, 0) AS TR_IN, NVL(S.QTY, 0) - NVL(R.QTY, 0) - NVL(TOUT.QTY, 0) + NVL(TIN.QTY, 0) AS BALANCE   FROM   U_MASTER U LEFT OUTER JOIN ");
            sbQuery.Append(" (SELECT REG_BK, REG_BR, REG_NO, SUM(NVL(QTY, 0)) AS QTY   FROM    SALE WHERE (SL_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "') GROUP BY REG_BK, REG_BR, REG_NO) S ON U.REG_BK = S.REG_BK AND U.REG_BR = S.REG_BR AND ");
            sbQuery.Append(" U.REG_NO = S.REG_NO LEFT OUTER JOIN   (SELECT REG_BK, REG_BR, REG_NO, SUM(NVL(QTY, 0)) AS QTY   FROM REPURCHASE WHERE (REP_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "') ");
            sbQuery.Append(" GROUP BY REG_BK, REG_BR, REG_NO) R ON U.REG_BK = R.REG_BK AND U.REG_BR = R.REG_BR AND   U.REG_NO = R.REG_NO LEFT OUTER JOIN ");
            sbQuery.Append(" (SELECT     REG_BK_O, REG_BR_O, REG_NO_O, SUM(NVL(QTY, 0)) AS QTY FROM TRANSFER WHERE (TR_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "') GROUP BY REG_BK_O, REG_BR_O, REG_NO_O) TOUT ON U.REG_BK = TOUT.REG_BK_O AND U.REG_BR = TOUT.REG_BR_O AND");
            sbQuery.Append("  U.REG_NO = TOUT.REG_NO_O LEFT OUTER JOIN   (SELECT     REG_BK_I, REG_BR_I, REG_NO_I, SUM(NVL(QTY, 0)) AS QTY ");
            sbQuery.Append("  FROM TRANSFER TRANSFER_1 WHERE (TR_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "') GROUP BY REG_BK_I, REG_BR_I, REG_NO_I) TIN ON U.REG_BK = TIN.REG_BK_I AND U.REG_BR = TIN.REG_BR_I AND ");
            sbQuery.Append("   U.REG_NO = TIN.REG_NO_I) A WHERE CIP='Y'  AND (REG_BK = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')");
        }
        else
        {
            sbQuery.Append(" UNION ALL");
            sbQuery.Append(" SELECT  9 AS SI, 'CIP including 0 ' AS TRANS_TYPE, NVL(SUM(BALANCE),0) AS TOTAL_UNIT, 0 AS TOTAL_AMT, COUNT( DISTINCT REG_NO) AS TOTAL_HOLD , COUNT(*) AS TOTAL_TRANS ");
            sbQuery.Append(" FROM    (SELECT  U.REG_BK, U.REG_BR, U.REG_NO, U.CIP, NVL(S.QTY, 0) AS SALE, NVL(R.QTY, 0) AS REPURCHASE, NVL(TOUT.QTY, 0) AS TR_OUT, ");
            sbQuery.Append(" NVL(TIN.QTY, 0) AS TR_IN, NVL(S.QTY, 0) - NVL(R.QTY, 0) - NVL(TOUT.QTY, 0) + NVL(TIN.QTY, 0) AS BALANCE   FROM   U_MASTER U LEFT OUTER JOIN ");
            sbQuery.Append(" (SELECT REG_BK, REG_BR, REG_NO, SUM(NVL(QTY, 0)) AS QTY   FROM    SALE   GROUP BY REG_BK, REG_BR, REG_NO) S ON U.REG_BK = S.REG_BK AND U.REG_BR = S.REG_BR AND ");
            sbQuery.Append(" U.REG_NO = S.REG_NO LEFT OUTER JOIN   (SELECT REG_BK, REG_BR, REG_NO, SUM(NVL(QTY, 0)) AS QTY   FROM REPURCHASE ");
            sbQuery.Append(" GROUP BY REG_BK, REG_BR, REG_NO) R ON U.REG_BK = R.REG_BK AND U.REG_BR = R.REG_BR AND   U.REG_NO = R.REG_NO LEFT OUTER JOIN ");
            sbQuery.Append(" (SELECT     REG_BK_O, REG_BR_O, REG_NO_O, SUM(NVL(QTY, 0)) AS QTY FROM TRANSFER GROUP BY REG_BK_O, REG_BR_O, REG_NO_O) TOUT ON U.REG_BK = TOUT.REG_BK_O AND U.REG_BR = TOUT.REG_BR_O AND");
            sbQuery.Append("  U.REG_NO = TOUT.REG_NO_O LEFT OUTER JOIN   (SELECT     REG_BK_I, REG_BR_I, REG_NO_I, SUM(NVL(QTY, 0)) AS QTY ");
            sbQuery.Append("  FROM TRANSFER TRANSFER_1  GROUP BY REG_BK_I, REG_BR_I, REG_NO_I) TIN ON U.REG_BK = TIN.REG_BK_I AND U.REG_BR = TIN.REG_BR_I AND ");
            sbQuery.Append("   U.REG_NO = TIN.REG_NO_I) A WHERE CIP='Y'  AND (REG_BK = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')");
        }


        if (branchNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbQuery.Append(" AND (REG_BR = '" + branchNameDropDownList.SelectedValue.ToString().ToUpper() + "')");

        }

        if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
        {
            sbQuery.Append(" UNION ALL");
            sbQuery.Append(" SELECT  10 AS SI, 'NON-CIP including 0 ' AS TRANS_TYPE, NVL(SUM(BALANCE),0) AS TOTAL_UNIT, 0 AS TOTAL_AMT, COUNT(DISTINCT REG_NO) AS TOTAL_HOLD , COUNT(*) AS TOTAL_TRANS ");
            sbQuery.Append(" FROM    (SELECT  U.REG_BK, U.REG_BR, U.REG_NO, U.CIP, NVL(S.QTY, 0) AS SALE, NVL(R.QTY, 0) AS REPURCHASE, NVL(TOUT.QTY, 0) AS TR_OUT, ");
            sbQuery.Append(" NVL(TIN.QTY, 0) AS TR_IN, NVL(S.QTY, 0) - NVL(R.QTY, 0) - NVL(TOUT.QTY, 0) + NVL(TIN.QTY, 0) AS BALANCE   FROM   U_MASTER U LEFT OUTER JOIN ");
            sbQuery.Append(" (SELECT REG_BK, REG_BR, REG_NO, SUM(NVL(QTY, 0)) AS QTY   FROM    SALE WHERE (SL_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "') GROUP BY REG_BK, REG_BR, REG_NO) S ON U.REG_BK = S.REG_BK AND U.REG_BR = S.REG_BR AND ");
            sbQuery.Append(" U.REG_NO = S.REG_NO LEFT OUTER JOIN   (SELECT REG_BK, REG_BR, REG_NO, SUM(NVL(QTY, 0)) AS QTY   FROM REPURCHASE WHERE (REP_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "') ");
            sbQuery.Append(" GROUP BY REG_BK, REG_BR, REG_NO) R ON U.REG_BK = R.REG_BK AND U.REG_BR = R.REG_BR AND   U.REG_NO = R.REG_NO LEFT OUTER JOIN ");
            sbQuery.Append(" (SELECT     REG_BK_O, REG_BR_O, REG_NO_O, SUM(NVL(QTY, 0)) AS QTY FROM TRANSFER WHERE (TR_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "') GROUP BY REG_BK_O, REG_BR_O, REG_NO_O) TOUT ON U.REG_BK = TOUT.REG_BK_O AND U.REG_BR = TOUT.REG_BR_O AND");
            sbQuery.Append("  U.REG_NO = TOUT.REG_NO_O LEFT OUTER JOIN   (SELECT     REG_BK_I, REG_BR_I, REG_NO_I, SUM(NVL(QTY, 0)) AS QTY ");
            sbQuery.Append("  FROM TRANSFER TRANSFER_1 WHERE (TR_DT BETWEEN '" + fromDateTextBox.Text.Trim().ToString() + "' AND '" + toDateTextBox.Text.Trim().ToString() + "') GROUP BY REG_BK_I, REG_BR_I, REG_NO_I) TIN ON U.REG_BK = TIN.REG_BK_I AND U.REG_BR = TIN.REG_BR_I AND ");
            sbQuery.Append("   U.REG_NO = TIN.REG_NO_I) A WHERE CIP='N'  AND (REG_BK = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')");
        }
        else
        {
            sbQuery.Append(" UNION ALL");
            sbQuery.Append(" SELECT  10 AS SI, 'NON-CIP including 0 ' AS TRANS_TYPE, NVL(SUM(BALANCE),0) AS TOTAL_UNIT, 0 AS TOTAL_AMT, COUNT(DISTINCT REG_NO) AS TOTAL_HOLD,   COUNT(*) AS TOTAL_TRANS ");
            sbQuery.Append(" FROM    (SELECT  U.REG_BK, U.REG_BR, U.REG_NO, U.CIP, NVL(S.QTY, 0) AS SALE, NVL(R.QTY, 0) AS REPURCHASE, NVL(TOUT.QTY, 0) AS TR_OUT, ");
            sbQuery.Append(" NVL(TIN.QTY, 0) AS TR_IN, NVL(S.QTY, 0) - NVL(R.QTY, 0) - NVL(TOUT.QTY, 0) + NVL(TIN.QTY, 0) AS BALANCE   FROM   U_MASTER U LEFT OUTER JOIN ");
            sbQuery.Append(" (SELECT REG_BK, REG_BR, REG_NO, SUM(NVL(QTY, 0)) AS QTY   FROM    SALE   GROUP BY REG_BK, REG_BR, REG_NO) S ON U.REG_BK = S.REG_BK AND U.REG_BR = S.REG_BR AND ");
            sbQuery.Append(" U.REG_NO = S.REG_NO LEFT OUTER JOIN   (SELECT REG_BK, REG_BR, REG_NO, SUM(NVL(QTY, 0)) AS QTY   FROM REPURCHASE ");
            sbQuery.Append(" GROUP BY REG_BK, REG_BR, REG_NO) R ON U.REG_BK = R.REG_BK AND U.REG_BR = R.REG_BR AND   U.REG_NO = R.REG_NO LEFT OUTER JOIN ");
            sbQuery.Append(" (SELECT     REG_BK_O, REG_BR_O, REG_NO_O, SUM(NVL(QTY, 0)) AS QTY FROM TRANSFER GROUP BY REG_BK_O, REG_BR_O, REG_NO_O) TOUT ON U.REG_BK = TOUT.REG_BK_O AND U.REG_BR = TOUT.REG_BR_O AND");
            sbQuery.Append("  U.REG_NO = TOUT.REG_NO_O LEFT OUTER JOIN   (SELECT     REG_BK_I, REG_BR_I, REG_NO_I, SUM(NVL(QTY, 0)) AS QTY ");
            sbQuery.Append("  FROM TRANSFER TRANSFER_1  GROUP BY REG_BK_I, REG_BR_I, REG_NO_I) TIN ON U.REG_BK = TIN.REG_BK_I AND U.REG_BR = TIN.REG_BR_I AND ");
            sbQuery.Append("   U.REG_NO = TIN.REG_NO_I) A WHERE CIP='N'  AND (REG_BK = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')");
        }


        if (branchNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbQuery.Append(" AND (REG_BR = '" + branchNameDropDownList.SelectedValue.ToString().ToUpper() + "')");

        }


        dtUnitFundPosition = commonGatewayObj.Select(sbQuery.ToString());
        return dtUnitFundPosition;
    }
}
