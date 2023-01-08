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

public partial class UI_UnitProcessExportBEFTNFile : System.Web.UI.Page
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
       // string duplicate = "";
        string fundCode = fundNameDropDownList.SelectedValue.ToString();
        StringBuilder sbMaster = new StringBuilder();
        StringBuilder sbFilter = new StringBuilder();
        DataTable dtDividend = new DataTable();
        string FY = DividendFYDropDownList.SelectedValue.ToString();
        string closingDate=ClosingDateDropDownList.SelectedItem.Text.ToString();
       
        if (NormalRadioButton.Checked)
        {
            //FOR IFIC BANK
            //sbMaster.Append("  SELECT  TO_CHAR(DIVI_PARA.ISS_DT,'DD-MON-YYYY') AS EFFECTIVE_DATE,CASE  WHEN LENGTH(DIVIDEND.BK_AC_NO)>13 THEN SUBSTR(DIVIDEND.BK_AC_NO, - 13) ELSE DIVIDEND.BK_AC_NO  END AS RECEIVER_ACC_NO, U_MASTER.HNAME AS RECEIVER_NAME, ");
            //sbMaster.Append("  DIVIDEND.FI_DIVI_QTY AS AMOUNT, DIVI_PARA.BK_ROUTING_NO AS ORIGINATING_BR_ROUTER_NO, ");
            //sbMaster.Append("  BANK_BRANCH.ROUTING_NO AS RECEIVING_BR_ROUTING_NO, DIVI_PARA.BK_AC_NO_MICR AS ORIGINATING_ACC_NO,");
            //sbMaster.Append("  '" + fundCode + "_2016' AS ORIGINATOR_NAME,   DIVIDEND.REG_BR || '/' || DIVIDEND.REG_NO || '/' || DIVIDEND.WAR_NO AS REMARKS");

            //sbMaster.Append(" FROM   BANK_BRANCH INNER JOIN   U_MASTER INNER JOIN    DIVI_PARA INNER JOIN   DIVIDEND ON DIVI_PARA.DIVI_NO = DIVIDEND.DIVI_NO AND DIVI_PARA.FUND_CD = DIVIDEND.FUND_CD AND DIVI_PARA.F_YEAR = DIVIDEND.FY ON ");
            //sbMaster.Append(" U_MASTER.REG_BK = DIVIDEND.REG_BK AND U_MASTER.REG_BR = DIVIDEND.REG_BR AND U_MASTER.REG_NO = DIVIDEND.REG_NO ON   BANK_BRANCH.BANK_CODE = DIVIDEND.BK_NM_CD AND BANK_BRANCH.BRANCH_CODE = DIVIDEND.BK_BR_NM_CD");
            //sbMaster.Append(" WHERE (DIVI_PARA.FUND_CD = '" + fundCode + "') AND (DIVIDEND.IS_BEFTN = 'Y') AND (DIVI_PARA.CLOSE_DT = '" + closingDate + "') AND DIVI_PARA.F_YEAR='" + FY.ToString() + "' AND DIVIDEND.BK_NM_CD=21 ");//DIVIDEND.BK_NM_CD 21 IS THE DIVIDEND CLEARING BANK CODE
            //FOR SHAHAJALAL BANK
            sbMaster.Append("  SELECT BANK_BRANCH.ROUTING_NO AS REC_BRANCH_CODE, 22 AS TX_CODE,CASE  WHEN LENGTH(DIVIDEND.BK_AC_NO)>13 THEN SUBSTR(DIVIDEND.BK_AC_NO, - 13) ELSE DIVIDEND.BK_AC_NO  END AS RECV_ACC_NO, ");
            sbMaster.Append(" 0 AS DEBIT, DIVIDEND.FI_DIVI_QTY AS CREDIT,DIVIDEND.WAR_NO AS RECV_ID ,U_MASTER.HNAME AS RECEIVER_NAME, DIVIDEND.REG_BR || '/' || DIVIDEND.REG_NO AS REMARKS ");
            sbMaster.Append(" FROM   BANK_BRANCH INNER JOIN   U_MASTER INNER JOIN    DIVI_PARA INNER JOIN   DIVIDEND ON DIVI_PARA.DIVI_NO = DIVIDEND.DIVI_NO AND DIVI_PARA.FUND_CD = DIVIDEND.FUND_CD AND DIVI_PARA.F_YEAR = DIVIDEND.FY ON ");
            sbMaster.Append(" U_MASTER.REG_BK = DIVIDEND.REG_BK AND U_MASTER.REG_BR = DIVIDEND.REG_BR AND U_MASTER.REG_NO = DIVIDEND.REG_NO ON   BANK_BRANCH.BANK_CODE = DIVIDEND.BK_NM_CD AND BANK_BRANCH.BRANCH_CODE = DIVIDEND.BK_BR_NM_CD");
            sbMaster.Append(" WHERE (DIVI_PARA.FUND_CD = '" + fundCode + "') AND (DIVIDEND.IS_BEFTN = 'Y') AND (DIVI_PARA.CLOSE_DT = '" + closingDate + "') AND DIVI_PARA.F_YEAR='" + FY.ToString() + "' AND DIVIDEND.BK_NM_CD=36 ");//DIVIDEND.BK_NM_CD 36 IS THE DIVIDEND CLEARING BANK CODE

                if (branchNameDropDownList.SelectedValue.ToString() != "0")
                {
                    sbFilter.Append(" AND DIVIDEND.REG_BR='" + branchNameDropDownList.SelectedValue.ToString() + "'");
                }

                if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text == "")
                {
                    sbFilter.Append(" AND TO_NUMBER(DIVIDEND.WAR_NO)>=" + Convert.ToInt32(fromWar_NoTextBox.Text.Trim().ToString()));
                }
                else if (fromWar_NoTextBox.Text == "" && toWar_NoTextBox.Text != "")
                {
                    sbFilter.Append(" AND TO_NUMBER(DIVIDEND.WAR_NO)<=" + Convert.ToInt32(toWar_NoTextBox.Text.Trim().ToString()));
                }
                else if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text != "")
                {
                    sbFilter.Append(" AND TO_NUMBER(DIVIDEND.WAR_NO) BETWEEN " + Convert.ToInt32(fromWar_NoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToUInt32(toWar_NoTextBox.Text.Trim().ToString()));
                }

                if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
                {
                    sbFilter.Append(" AND TO_NUMBER(DIVIDEND.REG_NO)>=" + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()));
                }
                else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
                {
                    sbFilter.Append(" AND TO_NUMBER(DIVIDEND.REG_NO)<=" + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()));
                }
                else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
                {
                    sbFilter.Append(" AND TO_NUMBER(DIVIDEND.REG_NO) BETWEEN " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()));
                }

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
                sbFilter.Append(" AND TO_NUMBER(DIVIDEND_ID.WAR_NO)>=" + Convert.ToInt32(fromWar_NoTextBox.Text.Trim().ToString()));
            }
            else if (fromWar_NoTextBox.Text == "" && toWar_NoTextBox.Text != "")
            {
                sbFilter.Append(" AND TO_NUMBER(DIVIDEND_ID.WAR_NO)<=" + Convert.ToInt32(toWar_NoTextBox.Text.Trim().ToString()));
            }
            else if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text != "")
            {
                sbFilter.Append(" AND TO_NUMBER(DIVIDEND_ID.WAR_NO) BETWEEN " + Convert.ToInt32(fromWar_NoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toWar_NoTextBox.Text.Trim().ToString()));
            }
            sbFilter.Append("  ORDER BY TO_NUMBER(WAR_NO)");
            sbMaster.Append(sbFilter.ToString());
            dtDividend = commonGatewayObj.Select(sbMaster.ToString());

        }
        if (dtDividend.Rows.Count > 0)
        {
            
            string separetor = MultipleWarrantTextBox.Text.Trim().ToString();
            string commaSeparator = "\"";
            string fileName = fundNameDropDownList.SelectedValue.ToString() + "_" + fromWar_NoTextBox.Text.Trim().ToString() + " TO " + toWar_NoTextBox.Text.Trim().ToString();

            FileStream fileStream = new FileStream(@"D:" + "\\" + fileName + ".txt", FileMode.Create);
          
            StreamWriter swFile = new StreamWriter(fileStream);
            StringBuilder sbFileData = new StringBuilder();
            for(int loop=0;loop<dtDividend.Rows.Count;loop++)
            {
                //FOR IFIC BANK
                sbFileData.Append(dtDividend.Rows[loop]["EFFECTIVE_DATE"].ToString());
                sbFileData.Append(separetor + commaSeparator + dtDividend.Rows[loop]["RECEIVER_ACC_NO"].ToString() + commaSeparator);
                sbFileData.Append(separetor + commaSeparator + dtDividend.Rows[loop]["RECEIVER_NAME"].ToString() + commaSeparator);
                sbFileData.Append(separetor + dtDividend.Rows[loop]["AMOUNT"].ToString());
                sbFileData.Append(separetor + commaSeparator + dtDividend.Rows[loop]["ORIGINATING_BR_ROUTER_NO"].ToString() + commaSeparator);
                sbFileData.Append(separetor + commaSeparator + dtDividend.Rows[loop]["RECEIVING_BR_ROUTING_NO"].ToString() + commaSeparator);
                sbFileData.Append(separetor + commaSeparator + dtDividend.Rows[loop]["ORIGINATING_ACC_NO"].ToString() + commaSeparator);
                sbFileData.Append(separetor + commaSeparator + dtDividend.Rows[loop]["ORIGINATOR_NAME"].ToString() + commaSeparator);
                sbFileData.Append(separetor + commaSeparator + dtDividend.Rows[loop]["REMARKS"].ToString() + commaSeparator);
                
                //FOR SHAHJALAL BANK
                //sbFileData.Append(dtDividend.Rows[loop]["REC_BRANCH_CODE"].ToString());
                //sbFileData.Append(separetor + dtDividend.Rows[loop]["TX_CODE"].ToString());
                //sbFileData.Append(separetor + dtDividend.Rows[loop]["RECV_ACC_NO"].ToString());
                //sbFileData.Append(separetor + dtDividend.Rows[loop]["DEBIT"].ToString());
                //sbFileData.Append(separetor + dtDividend.Rows[loop]["CREDIT"].ToString());
                //sbFileData.Append(separetor + dtDividend.Rows[loop]["RECV_ID"].ToString());
                //sbFileData.Append(separetor + dtDividend.Rows[loop]["RECEIVER_NAME"].ToString());               
                //sbFileData.Append(separetor + dtDividend.Rows[loop]["REMARKS"].ToString());

                sbFileData.Append("\r\n");
            }
            swFile.Write(sbFileData.ToString());
            swFile.Close();
            fileStream.Close();
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Data Exported Succesfully');", true);
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
    protected void ExportPhoneButton_Click(object sender, EventArgs e)
    {
        DataTable dtPhoneNumber = commonGatewayObj.Select("SELECT TEL_NO FROM U_MASTER WHERE LENGTH(TEL_NO)=11 AND REG_BR='AMC/01' AND REG_BK='IAMPH'");
        if (dtPhoneNumber.Rows.Count > 0)
        {
            FileStream fileStream = new FileStream(@"D:" + "\\Phone_Address_IAMPH.txt", FileMode.Create);
            StreamWriter swFile = new StreamWriter(fileStream);
            StringBuilder sbFileData = new StringBuilder();
            int counter = 1;
            for (int loop = 0; loop < dtPhoneNumber.Rows.Count; loop++)
            {
               
                sbFileData.Append(dtPhoneNumber.Rows[loop]["TEL_NO"].ToString());
                if (counter == 20)
                {
                    sbFileData.Append("\r\n");
                    counter = 1;
                }
                else
                {
                    sbFileData.Append(";");
                }
                counter++;
            }
            swFile.Write(sbFileData.ToString());
            swFile.Close();
            fileStream.Close();
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Data Exported Succesfully');", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data Found');", true);
        }
    }
}
