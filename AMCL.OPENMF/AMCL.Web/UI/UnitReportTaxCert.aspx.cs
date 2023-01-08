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


public partial class UI_UnitReportTaxCert : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();
    UnitLienBl unitLienBLObj = new UnitLienBl();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    string fundCode = "";
    string branchCode = "";
    string CDSStatus = "";

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
        fundCodeTextBox.Text = fundCode.ToString();
        branchCodeTextBox.Text = branchCode.ToString();
        CDSStatus = bcContent.CDS.ToString().ToUpper();
        
        if (!IsPostBack)
        {
            incomeTaxFYDropDownList.DataSource = reportObj.getDtFY(fundCode);
            incomeTaxFYDropDownList.DataTextField = "F_YEAR";
            incomeTaxFYDropDownList.DataValueField = "DIVI_NO";
            incomeTaxFYDropDownList.DataBind();

            fyPartDropDownList.DataSource = reportObj.getDtFYPart(fundCode);
            fyPartDropDownList.DataTextField = "FY_PART";
            fyPartDropDownList.DataValueField = "FY_PART";
            fyPartDropDownList.DataBind();          
        }
    
    }
   
    protected void regNoTextBox_TextChanged(object sender, EventArgs e)
    {
        surrenderDateDropDownList.DataSource = reportObj.getDtSurrenderDateRegWise(fundCodeTextBox.Text.Trim().ToString(), Convert.ToInt32(regNoTextBox.Text.Trim().ToString()), branchCodeTextBox.Text.Trim().ToString().ToUpper());
        surrenderDateDropDownList.DataTextField = "REP_DT";
        surrenderDateDropDownList.DataValueField = "REP_NO";
        surrenderDateDropDownList.DataBind();
        if (fundCode != "IAMPH")
        {
            fyPartDropDownList.SelectedValue = "2nd Half";
        }
        DataTable dtFYCheck=reportObj.getDtFY(fundCode);
        if (dtFYCheck.Rows.Count > 0)
        {
            string fy = incomeTaxFYDropDownList.SelectedItem.Text.ToString();
            string fyFrom = " ";
            string fyTo = " ";

            string[] divideFY = fy.Split('-');
            if (divideFY.Length > 1)
            {
                fyFrom = "01-JUL-" + divideFY[0].ToString();
                fyTo = "30-JUN-" + divideFY[1].ToString();
            }
            else
            {
                fyFrom = "01-JUL-" + incomeTaxFYDropDownList.SelectedItem.Text.ToString();
                fyTo = "30-JUN-" + Convert.ToString(Convert.ToUInt16(incomeTaxFYDropDownList.SelectedItem.Text.ToString()) + 1);
            }

            TTDateTextBox.Text = DateTime.Today.AddDays(-1).ToString("dd-MMM-yyyy");
            investFYFromTextBox.Text = fyFrom;
            investFYToTextBox.Text = fyTo;
            surrenderFYToTextBox.Text = fyTo;
        }
        else
        {
            investFYFromTextBox.Text = "";
            investFYToTextBox.Text = "";
            surrenderFYToTextBox.Text = "";
        }
    }
    protected void CloseButton_Click(object sender, EventArgs e)
    {         
        Response.Redirect("UnitHome.aspx");
           
    }

    protected void PrintButton_Click(object sender, EventArgs e)
    {
        bool flag = false;
        string certType = "";
        string investmentType = "ALL";
        StringBuilder sbQueryString = new StringBuilder();
        if (incomeTaxCertRadioButton.Checked)
        {
            DataTable dtIncomeTax = new DataTable();
            certType = "IncomeTaxCert";
            sbQueryString.Append("SELECT  U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, FUND_INFO.FUND_NM, U_MASTER.HNAME, U_MASTER.ADDRS1, TAX_DIDUCT_RT AS TAX_RATE,");
            sbQueryString.Append("  U_MASTER.ADDRS2, U_MASTER.CITY, U_JHOLDER.JNT_NAME, DIVI_PARA.F_YEAR, TO_CHAR(DIVI_PARA.CLOSE_DT,'DD-MON-YYYY')AS CLOSE_DT, DIVI_PARA.DIVI_NO,DECODE(DIVIDEND.CIP,'Y',TO_CHAR(DIVI_PARA.ISS_DT,'DD-MON-YYYY'),'')AS ISS_DT,");
            sbQueryString.Append("  DIVI_PARA.FY_PART, DIVI_PARA.RATE, TO_CHAR(DIVI_PARA.AGM_DT,'DD-MON-YYYY') AS AGM_DT, DIVI_PARA.CIP_RATE, DIVIDEND.TOT_DIVI, DIVIDEND.DIDUCT,DIVIDEND.TOT_DIVI-DIVIDEND.DIDUCT AS NET_DIVIDEND,DECODE(UPPER(DIVIDEND.CIP), 'Y', DIVIDEND.FI_DIVI_QTY, 0) AS FRACTION_DIVI, ");
            sbQueryString.Append("  DIVIDEND.FI_DIVI_QTY,DIVIDEND.CIP_QTY, DIVIDEND.BALANCE FROM U_MASTER INNER JOIN  DIVIDEND INNER JOIN ");
            sbQueryString.Append("  DIVI_PARA ON DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.FY = DIVI_PARA.F_YEAR AND DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO ON ");
            sbQueryString.Append("  U_MASTER.REG_BR = DIVIDEND.REG_BR AND U_MASTER.REG_BK = DIVIDEND.REG_BK AND U_MASTER.REG_NO = DIVIDEND.REG_NO INNER JOIN");
            sbQueryString.Append("  FUND_INFO ON U_MASTER.REG_BK = FUND_INFO.FUND_CD LEFT OUTER JOIN   U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND");
            sbQueryString.Append("  U_MASTER.REG_NO = U_JHOLDER.REG_NO WHERE (U_MASTER.REG_NO = " + Convert.ToInt32(regNoTextBox.Text.Trim().ToString())+") AND (U_MASTER.REG_BR = '"+branchCodeTextBox.Text.Trim().ToString().ToUpper()+"') AND ");
            sbQueryString.Append(" (U_MASTER.REG_BK = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (DIVI_PARA.F_YEAR = '" + incomeTaxFYDropDownList.SelectedItem.Text.Trim().ToString()+ "') AND (DIVI_PARA.FY_PART = '" + fyPartDropDownList.SelectedValue.ToString() + "')");

            dtIncomeTax = commonGatewayObj.Select(sbQueryString.ToString());
            if (dtIncomeTax.Rows.Count > 0)
            {
                Session["dtIncomeTax"] = dtIncomeTax;
                Session["FY"] = incomeTaxFYDropDownList.SelectedItem.Text.ToString();
                Session["REG_NO"] = Convert.ToInt32(regNoTextBox.Text.Trim().ToString());
            }
            else
            {
                flag = true;
            }


        }
        else if (InvestCertRadioButton.Checked)
        {
            certType = "InvestCert";
            sbQueryString = new StringBuilder();
            sbQueryString.Append(" SELECT FUND_INFO.FUND_NM, U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, U_MASTER.HNAME, U_MASTER.ADDRS1, ");
            sbQueryString.Append("  U_MASTER.ADDRS2, U_MASTER.CITY, U_JHOLDER.JNT_NAME FROM U_MASTER INNER JOIN  FUND_INFO ON U_MASTER.REG_BK = FUND_INFO.FUND_CD LEFT OUTER JOIN ");
            sbQueryString.Append("  U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND   U_MASTER.REG_NO = U_JHOLDER.REG_NO ");
            sbQueryString.Append("  WHERE (U_MASTER.REG_BR = '" + branchCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (U_MASTER.REG_BK = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (U_MASTER.REG_NO = " + Convert.ToInt32(regNoTextBox.Text.Trim().ToString()) + ")");
            DataTable dtInvestCertHolderInfo = commonGatewayObj.Select(sbQueryString.ToString());

            //sbQueryString = new StringBuilder();
            //sbQueryString.Append(" SELECT SL_NO, TO_CHAR(SL_DT, 'DD-MON-YYYY') AS SL_DT,SL_TYPE, QTY, SL_PRICE, QTY * SL_PRICE AS AMOUNT, REG_BK || '/' || REG_BR || '/' || REG_NO AS REG_NO");
            //sbQueryString.Append(" FROM SALE WHERE (REG_BR = '" + branchCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (REG_BK = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (REG_NO = " + Convert.ToInt32(regNoTextBox.Text.Trim().ToString()) + ") ");
            //sbQueryString.Append(" AND (SL_DT BETWEEN '" +Convert.ToDateTime( investFYFromTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") +"' AND '"+Convert.ToDateTime( investFYToTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") +"')");

            sbQueryString = new StringBuilder();
            sbQueryString.Append(" SELECT F.TRAN_DATE,'REP' AS TRAN_TYPE, REP_NO AS TRAN_NO, SUM(QTY) TRAN_QTY,REP_PRICE AS TRAN_RATE,SUM(F.REP_PRICE*F.QTY) AS TRAN_AMOUNT,SUM(F.SL_PRICE*F.QTY) AS TRAN_COST_AMOUNT, SUM(F.REP_PRICE*F.QTY)-SUM(F.SL_PRICE*F.QTY) AS TRAN_CAPITAL_GAIN");
            sbQueryString.Append(" FROM(SELECT R.*,S.* FROM( SELECT REG_BK,REG_BR,REG_NO,REP_NO,LTRIM(SL_TR_NO,'S') AS SL_TR_NO, TO_CHAR(REP_DT,'DD-MON-YYYY') AS TRAN_DATE, 'REP' AS TRAN_TYPE,REP_PRICE, QTY FROM REPURCHASE   WHERE");
            sbQueryString.Append(" REG_BK='IUF' AND REG_BR='AMC/01' AND REG_NO=2286 AND REP_DT BETWEEN '01-Jul-2021' and '30-Jun-2022') R ");
            sbQueryString.Append(" INNER JOIN (SELECT REG_BK,REG_BR,REG_NO,SL_NO,SL_PRICE FROM SALE  WHERE REG_BK='IUF' AND REG_BR='AMC/01' AND REG_NO=2286) S");
            sbQueryString.Append(" ON R.REG_BK=S.REG_BK AND R.REG_BR=S.REG_BR AND R.REG_NO=S.REG_NO AND R.SL_TR_NO=S.SL_NO) F GROUP BY F.TRAN_DATE,REP_PRICE,REP_NO ");
            sbQueryString.Append("  UNION ALL");
            sbQueryString.Append(" SELECT TO_CHAR(SL_DT,'DD-MON-YYYY') AS TRAN_DATE, SL_TYPE AS TRAN_TYPE, SL_NO AS TRAN_NO, QTY AS TRAN_QTY ,SL_PRICE AS TRAN_RATE, QTY*SL_PRICE AS TRAN_AMOUNT, 0 AS TRAN_COST_AMOUNT, 0 AS TRAN_CAPITAL_GAIN ");
            sbQueryString.Append(" FROM SALE WHERE REG_BK='IUF' AND REG_BR='AMC/01' AND REG_NO=2286 AND SL_DT BETWEEN '01-Jul-2021' and '30-Jun-2022' ");
            sbQueryString.Append("  ");

            //if (CIPRadioButton.Checked)
            //{
            //    investmentType = "CIP";
            //    sbQueryString.Append(" AND SL_TYPE='CIP' ");
            //}
            //else if (NonCIPRadioButton.Checked)
            //{
            //    investmentType = "NONCIP";
            //    sbQueryString.Append(" AND SL_TYPE <> 'CIP' ");
            //}
            sbQueryString.Append(" ORDER BY TO_DATE(TRAN_DATE) ");
            DataTable dtInvestSaleInfo = commonGatewayObj.Select(sbQueryString.ToString());

            //sbQueryString = new StringBuilder();
            //sbQueryString.Append(" SELECT SUM(QTY) as TOTAL_UNIT, sum(QTY * SL_PRICE) AS TOTAL_AMOUNT");
            //sbQueryString.Append(" FROM SALE WHERE (REG_BR = '" + branchCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (REG_BK = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (REG_NO = " + Convert.ToInt32(regNoTextBox.Text.Trim().ToString()) + ") ");
            //sbQueryString.Append(" AND (SL_DT BETWEEN '" + Convert.ToDateTime(investFYFromTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(investFYToTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            //if (CIPRadioButton.Checked)
            //{
            //    sbQueryString.Append(" AND SL_TYPE='CIP' ");
            //}
            //else if (NonCIPRadioButton.Checked)
            //{
            //    sbQueryString.Append(" AND SL_TYPE='SL' ");
            //}
            //DataTable dtInvestTotal = commonGatewayObj.Select(sbQueryString.ToString());
            
            if (dtInvestCertHolderInfo.Rows.Count > 0 && dtInvestSaleInfo.Rows.Count > 0)
            {
                Session["dtInvestCertHolderInfo"] = dtInvestCertHolderInfo;
                Session["dtInvestSaleInfo"] = dtInvestSaleInfo;
              //  Session["dtInvestTotal"] = dtInvestTotal;
                
              
                string FY = investFYFromTextBox.Text.Trim().ToString() + " to " + investFYToTextBox.Text.Trim().ToString();
                Session["FY"] = FY.ToString();
                Session["CloseDate"] = surrenderFYToTextBox.Text.ToString();
                Session["investmentType"] = investmentType.ToString();

            }
            else
            {
                flag = true;
            }

        }
        else if (surrenderRadioButton.Checked)
        {
            certType = "SurrendertCert";
            sbQueryString = new StringBuilder();
            sbQueryString.Append(" SELECT  FUND_INFO.FUND_NM , U_MASTER.HNAME, U_MASTER.ADDRS1,  U_MASTER.REG_BK || '/' || U_MASTER.REG_BR || '/' || U_MASTER.REG_NO AS REG_NO,");
            sbQueryString.Append(" U_MASTER.ADDRS2, U_MASTER.CITY FROM U_MASTER INNER JOIN  FUND_INFO ON U_MASTER.REG_BK = FUND_INFO.FUND_CD");
            sbQueryString.Append(" WHERE (U_MASTER.REG_BR = '" + branchCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (U_MASTER.REG_BK = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (U_MASTER.REG_NO = " + Convert.ToInt32(regNoTextBox.Text.Trim().ToString()) + ")");
            DataTable dtInvestCertHolderInfo = commonGatewayObj.Select(sbQueryString.ToString());

            UnitHolderRegistration unitRegObj = new UnitHolderRegistration();
            unitRegObj.FundCode = fundCodeTextBox.Text.Trim();
            unitRegObj.BranchCode = branchCodeTextBox.Text.Trim();
            unitRegObj.RegNumber = regNoTextBox.Text.Trim();
            DataTable dtLedger = reportObj.GetLedgerData(unitRegObj);
            DataTable dtLadgerForReport = reportObj.getTableForSurrenderTaxCert();
            DataTable dtSurrender = reportObj.getTableForSurrender();
            if (dtLedger.Rows.Count > 0)
            {
                int inBalance = 0;
                int outBalance = 0;
                int totalBalance = 0;
                int surrnederBalance = 0;
                string surrenderDate="";
                decimal surrenderRate=0;
                decimal saleAmount = 0;
              //  decimal costAmount = 0;
                decimal repAmount = 0;
                string asOnDate = surrenderFYToTextBox.Text.Trim().ToString();
            

                DataRow drReport;
                for (int loop = 0; loop < dtLedger.Rows.Count; loop++)
                {
                    drReport = dtLadgerForReport.NewRow();
                    if (Convert.ToDateTime(surrenderDateDropDownList.SelectedItem.Text.Trim().ToString()) == Convert.ToDateTime(Convert.ToDateTime(dtLedger.Rows[loop]["TRANS_DATE"].ToString()).ToString("dd-MMM-yyyy")) && (string.Compare(dtLedger.Rows[loop]["TRANS_TYPE"].ToString(), "REP", true) == 0))
                    {
                        surrnederBalance = surrnederBalance + Convert.ToInt32(dtLedger.Rows[loop]["QTY"].ToString());
                        surrenderRate = Convert.ToDecimal(dtLedger.Rows[loop]["RATE"].ToString());
                        surrenderDate = Convert.ToDateTime(dtLedger.Rows[loop]["TRANS_DATE"].ToString()).ToString("dd-MMM-yyyy").ToString();
                    }
                    if (Convert.ToDateTime(surrenderFYToTextBox.Text.Trim().ToString()) >= Convert.ToDateTime(Convert.ToDateTime(dtLedger.Rows[loop]["TRANS_DATE"].ToString()).ToString("dd-MMM-yyyy")))
                    {                   
                        if ((string.Compare(dtLedger.Rows[loop]["TRANS_TYPE"].ToString(), "SL", true) == 0) || (string.Compare(dtLedger.Rows[loop]["TRANS_TYPE"].ToString(), "CIP", true) == 0))
                        {
                            drReport["TRANS_DATE"] = Convert.ToDateTime(dtLedger.Rows[loop]["TRANS_DATE"].ToString()).ToString("dd-MMM-yyyy").ToString();
                            drReport["REG_NO"] = dtInvestCertHolderInfo.Rows[0]["REG_NO"].ToString();
                            drReport["SALE_UNIT"] = Convert.ToInt32(dtLedger.Rows[loop]["QTY"].ToString());
                            drReport["RATE"] = Convert.ToDecimal(dtLedger.Rows[loop]["RATE"].ToString());
                            drReport["SALE_AMOUNT"] = Convert.ToDecimal(dtLedger.Rows[loop]["RATE"].ToString())*Convert.ToDecimal(dtLedger.Rows[loop]["QTY"].ToString());

                            saleAmount = saleAmount + Convert.ToDecimal(dtLedger.Rows[loop]["RATE"].ToString()) * Convert.ToDecimal(dtLedger.Rows[loop]["QTY"].ToString());

                            inBalance=inBalance+ Convert.ToInt32(dtLedger.Rows[loop]["QTY"].ToString());
                        }
                        else if (string.Compare(dtLedger.Rows[loop]["TRANS_TYPE"].ToString(), "TRI", true) == 0)
                        {
                            drReport["TRANS_DATE"] = Convert.ToDateTime(dtLedger.Rows[loop]["TRANS_DATE"].ToString()).ToString("dd-MMM-yyyy").ToString();
                            drReport["REG_NO"] = dtInvestCertHolderInfo.Rows[0]["REG_NO"].ToString();
                            drReport["TRI_UNIT"] = Convert.ToInt32(dtLedger.Rows[loop]["QTY"].ToString());
                            
                            inBalance=inBalance + Convert.ToInt32(dtLedger.Rows[loop]["QTY"].ToString());
                        }
                        else if (string.Compare(dtLedger.Rows[loop]["TRANS_TYPE"].ToString(), "TRO", true) == 0)
                        {
                                drReport["TRANS_DATE"] = Convert.ToDateTime(dtLedger.Rows[loop]["TRANS_DATE"].ToString()).ToString("dd-MMM-yyyy").ToString();
                                drReport["REG_NO"] = dtInvestCertHolderInfo.Rows[0]["REG_NO"].ToString();
                                drReport["TRO_UNIT"] = Convert.ToInt32(dtLedger.Rows[loop]["QTY"].ToString());                           
                                outBalance= outBalance + Convert.ToInt32(dtLedger.Rows[loop]["QTY"].ToString()); 
                        }
                        else if (string.Compare(dtLedger.Rows[loop]["TRANS_TYPE"].ToString(), "REP", true) == 0)
                        {                           
                           
                            drReport["TRANS_DATE"] = Convert.ToDateTime(dtLedger.Rows[loop]["TRANS_DATE"].ToString()).ToString("dd-MMM-yyyy").ToString();
                            drReport["REG_NO"] = dtInvestCertHolderInfo.Rows[0]["REG_NO"].ToString();
                            drReport["REP_UNIT"] = Convert.ToInt32(dtLedger.Rows[loop]["QTY"].ToString());
                            drReport["RATE"] = Convert.ToDecimal(dtLedger.Rows[loop]["RATE"].ToString());
                            drReport["REP_AMOUNT"] = Convert.ToDecimal(dtLedger.Rows[loop]["RATE"].ToString()) * Convert.ToDecimal(dtLedger.Rows[loop]["QTY"].ToString());
                            repAmount = repAmount + Convert.ToDecimal(dtLedger.Rows[loop]["RATE"].ToString()) * Convert.ToDecimal(dtLedger.Rows[loop]["QTY"].ToString());
                            outBalance= outBalance + Convert.ToInt32(dtLedger.Rows[loop]["QTY"].ToString());
                            decimal costAmount = reportObj.getCostAmount(unitRegObj, dtLedger.Rows[loop]["TRANS_NO"].ToString());
                            decimal repAmountCapital= Convert.ToDecimal(dtLedger.Rows[loop]["RATE"].ToString()) * Convert.ToDecimal(dtLedger.Rows[loop]["QTY"].ToString());
                            decimal capitalGain = repAmountCapital - costAmount;
                            drReport["CAPITAL_GAIN"] = capitalGain;
                        }
                        dtLadgerForReport.Rows.Add(drReport);
                    }
                    
                    
                
                }
                if(surrnederBalance>0)
                {
                    drReport=dtSurrender.NewRow();
                    drReport["REP_DATE"]=surrenderDate;
                    drReport["REP_UNIT"]=surrnederBalance;
                    drReport["RATE"]=surrenderRate;
                    drReport["AMOUNT"]=surrenderRate*Convert.ToDecimal(surrnederBalance);
                    dtSurrender.Rows.Add(drReport);

                }
                if (dtSurrender.Rows.Count > 0)
                {
                    totalBalance = inBalance - outBalance;
                    Session["totalBalance"] = totalBalance;
                    Session["inBalance"] = inBalance;
                    Session["outBalance"] = outBalance;
                    Session["dtLadgerForReport"] = dtLadgerForReport;
                    Session["dtSurrender"] = dtSurrender;
                    Session["dtInvestCertHolderInfo"] = dtInvestCertHolderInfo;
                    Session["asOnDate"] = asOnDate;
                    Session["repAmount"] = repAmount;
                    Session["saleAmount"] = saleAmount;
                    Session["ledgerAmount"] = saleAmount - repAmount;
                }
                else
                {
                    flag = true;
                }

            }
            else
            {
                flag = true;
            }
           
            
        
        }
        else if (solventRadioButton.Checked)
        {
            certType = "SolventCert";
            decimal totalUnitHolding = 0;

            sbQueryString = new StringBuilder();
            sbQueryString.Append(" SELECT  FUND_INFO.FUND_NM, U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, U_MASTER.HNAME, U_MASTER.ADDRS1, U_MASTER.REG_BK || '/' || U_MASTER.REG_BR || '/' || U_MASTER.REG_NO AS REG_NUM,");
            sbQueryString.Append(" U_MASTER.ADDRS2, U_MASTER.CITY ,  DECODE(U_JHOLDER.JNT_NAME, NULL, NULL, 'AND' || ' ' || U_JHOLDER.JNT_NAME) AS JNT_NAME FROM U_MASTER INNER JOIN  FUND_INFO ON U_MASTER.REG_BK = FUND_INFO.FUND_CD  LEFT OUTER JOIN  U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND   U_MASTER.REG_NO = U_JHOLDER.REG_NO");
            sbQueryString.Append(" WHERE (U_MASTER.REG_BR = '" + branchCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (U_MASTER.REG_BK = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (U_MASTER.REG_NO = " + Convert.ToInt32(regNoTextBox.Text.Trim().ToString()) + ")");
            DataTable dtInvestCertHolderInfo = commonGatewayObj.Select(sbQueryString.ToString());
            UnitHolderRegistration unitRegObj = new UnitHolderRegistration();
            unitRegObj.FundCode = fundCodeTextBox.Text.Trim();
            unitRegObj.BranchCode = branchCodeTextBox.Text.Trim();
            unitRegObj.RegNumber = regNoTextBox.Text.Trim();
            
           
             totalUnitHolding = opendMFDAO.getTotalSaleUnitBalanceWithDate(unitRegObj, balanceDateTextBox.Text.Trim());
          

            if (dtInvestCertHolderInfo.Rows.Count > 0)
            {
                Session["dtInvestCertHolderInfo"] = dtInvestCertHolderInfo;              
                Session["totalUnitHolding"] = totalUnitHolding;
                Session["USDRate"] = USDRateTextBox.Text.Trim().ToString();
                Session["TTDate"] = TTDateTextBox.Text.Trim().ToString();
                Session["RepRate"] = RepRateTextBox.Text.ToString();
                Session["balanceDate"] = balanceDateTextBox.Text.Trim();
            }
            else
            {
                flag = true;
            }

        }
       
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
        }

        if (!flag)
        {
            Session["certType"] = certType;
            Session["fundCode"] = fundCodeTextBox.Text.Trim().ToString();
            Session["branchCode"] = branchCodeTextBox.Text.Trim().ToString();          
            ClientScript.RegisterStartupScript(this.GetType(), "UnitReportTaxCert", "window.open('ReportViewer/UnitReportTaxCertReportViewer.aspx')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
        }
    }

    protected void balanceDateTextBox_TextChanged(object sender, EventArgs e)
    {
        UnitHolderRegistration unitRegObj = new UnitHolderRegistration();
        unitRegObj.FundCode = fundCodeTextBox.Text.Trim();
        unitRegObj.BranchCode = branchCodeTextBox.Text.Trim();
        unitRegObj.RegNumber = regNoTextBox.Text.Trim();
        if (balanceDateTextBox.Text!="")
        {
            RepRateTextBox.Text = unitSaleBLObj.GetDateWisePrice(unitRegObj, "REP", balanceDateTextBox.Text.Trim().ToString()).ToString();
        }
        else
        {
            RepRateTextBox.Text = "";
        }
        
    }
}
