using AMCL.COMMON;
using AMCL.GATEWAY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for DiviReconDAO
/// </summary>
public class DiviReconDAO
{
    public DiviReconDAO()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    string bizSchema = ConfigReader.SCHEMA;
    DiviReconCommonGateway DRcommonGatewayObj = new DiviReconCommonGateway();


    public DataTable dtFundList(UnitUser userObj)
    {
        StringBuilder sbQuery = new StringBuilder();
        if (userObj.UserID.ToString().ToUpper() == "ADMIN")
        {
            sbQuery.Append("SELECT FUND_CD , FUND_NM FROM FUND_INFO ORDER BY FUND_CD");
        }
        else
        {
            sbQuery.Append("SELECT FUND_CD , FUND_NM FROM FUND_INFO WHERE FUND_CD IN (SELECT FUND_CD FROM USER_FUND WHERE USER_ID='" + userObj.UserID.ToString() + "') ORDER BY FUND_CD");
        }
        DataTable dtFundList = DRcommonGatewayObj.Select(sbQuery.ToString());
        DataTable dtFundListDropDown = new DataTable();
        dtFundListDropDown.Columns.Add("FUND_CD", typeof(string));
        dtFundListDropDown.Columns.Add("FUND_NM", typeof(string));

        DataRow drFundListDropDown = dtFundListDropDown.NewRow();

        drFundListDropDown["FUND_NM"] = " ";
        drFundListDropDown["FUND_CD"] = "0";
        dtFundListDropDown.Rows.Add(drFundListDropDown);
        for (int loop = 0; loop < dtFundList.Rows.Count; loop++)
        {
            drFundListDropDown = dtFundListDropDown.NewRow();
            drFundListDropDown["FUND_NM"] = dtFundList.Rows[loop]["FUND_NM"].ToString();
            drFundListDropDown["FUND_CD"] = dtFundList.Rows[loop]["FUND_CD"].ToString();
            dtFundListDropDown.Rows.Add(drFundListDropDown);
        }
        return dtFundListDropDown;

    }
    public DataTable dtFundList1()
    {
        DataTable dtFundList = DRcommonGatewayObj.Select("SELECT FUND_CD , FUND_NM FROM FUND_INFO  ORDER BY FUND_CD");
        DataTable dtFundListDropDown = new DataTable();
        dtFundListDropDown.Columns.Add("FUND_CD", typeof(string));
        dtFundListDropDown.Columns.Add("FUND_NM", typeof(string));

        DataRow drFundListDropDown = dtFundListDropDown.NewRow();

        drFundListDropDown["FUND_NM"] = " ";
        drFundListDropDown["FUND_CD"] = "0";
        dtFundListDropDown.Rows.Add(drFundListDropDown);
        for (int loop = 0; loop < dtFundList.Rows.Count; loop++)
        {
            drFundListDropDown = dtFundListDropDown.NewRow();
            drFundListDropDown["FUND_NM"] = dtFundList.Rows[loop]["FUND_NM"].ToString();
            drFundListDropDown["FUND_CD"] = dtFundList.Rows[loop]["FUND_CD"].ToString();
            dtFundListDropDown.Rows.Add(drFundListDropDown);
        }
        return dtFundListDropDown;

    }

    public DataTable getDividendDataForSatcomPrint(string fundCode, int divi_no, string war_no)
    {
        StringBuilder sbMst = new StringBuilder();

        sbMst.Append(" SELECT        FUND_INFO.FUND_NM AS FUND_NAME_0, U_MASTER.HNAME AS NAME_1, U_JHOLDER.JNT_NAME AS JOINT_NAME_2, U_MASTER.ADDRS1 AS ADDRESS_3, ");
        sbMst.Append(" U_MASTER.ADDRS2 AS ADDRESS_4, U_MASTER.CITY AS CITY_5, DIVI_PARA.DIVI_NO AS DIVIDEND_NO_6, DIVI_PARA.F_YEAR AS FY_7,  ");
        sbMst.Append(" TO_CHAR(DIVI_PARA.ISS_DT,'DD-MON-YYYY') AS ISSUE_DATE_8, TO_CHAR( LPAD(DIVIDEND.WAR_NO,7,0)) AS WARRANT_NO_9, ");
        sbMst.Append(" DIVIDEND.REG_BK || '/' || DIVIDEND.REG_BR || '/' || DIVIDEND.REG_NO AS REGISTRATION_NO_10, DIVI_PARA.BK_AC_NO AS STD_AC_NO_11, ");
        sbMst.Append(" TO_CHAR( DIVI_PARA.AGM_DT,'DD-MON-YYYY') AS MEETING_DATE_12, DIVI_PARA.RATE AS DIVI_RATE_13, ");
        sbMst.Append(" TO_CHAR( DIVI_PARA.CLOSE_DT,'DD-MON-YYYY') AS YEAR_END_DATE_14, ");
        sbMst.Append(" DIVIDEND.BALANCE AS NO_UNITS_15, DIVIDEND.TOT_DIVI AS GROSS_DIVIDEND_16, DIVIDEND.DIDUCT AS TAX_DIDUCT_17, NVL(DECODE(U_MASTER.REG_TYPE, ");
        sbMst.Append(" 'N', DIVIDEND.TAX_DIDUCT_RT, 0), 0) AS TX_RT_IND_18, NVL(DECODE(U_MASTER.REG_TYPE, 'I', DIVIDEND.TAX_DIDUCT_RT, 'C', ");
        sbMst.Append(" DIVIDEND.TAX_DIDUCT_RT, 'F', DIVIDEND.TAX_DIDUCT_RT, 0), 0) AS TX_RT_INS_19, DIVIDEND.TOT_DIVI - DIVIDEND.DIDUCT AS NET_DIVIDEND_20, ");
        sbMst.Append(" DIVI_PARA.CIP_RATE AS CIP_RATE_21,NVL( DIVIDEND.CIP_QTY,0) AS CIP_UNIT_22, DECODE(DIVIDEND.CIP, 'Y', DIVIDEND.FI_DIVI_QTY, 0)  ");
        sbMst.Append(" AS FRACTION_DIVIDEND_23, ");
        sbMst.Append(" DIVIDEND.FI_DIVI_QTY AS FINAL_DIVIDEND_AMOUNT_24, ' ' AS AMOUNT_IN_WORD_25, BANK_NAME.BANK_NAME AS HOLDER_BANK_NAME_26,  ");
        sbMst.Append(" BANK_BRANCH.BRANCH_NAME AS HOLDER_BRANCH_NAME_27, DIVIDEND.BK_AC_NO AS HOLDER_BANK_ACC_NO_28, ");
        sbMst.Append(" DIVI_PARA.BK_NAME AS STD_BANK_NAME_29, DIVI_PARA.BK_ADDRS1 AS STD_BANK_ADDRESS_30, DIVI_PARA.BK_ADDRS2 AS STD_BANK_ADDRESS_31, ");
        sbMst.Append(" TO_CHAR( DIVI_PARA.BK_ROUTING_NO) AS STD_BANK_ROUTING_NO_32,DIVI_PARA.BK_AC_NO_MICR AS MICR_STD_BANK_ACC_33, ");
        sbMst.Append(" DIVI_PARA.BK_TRANSACTION_CODE AS MICR_TRANS_CODE_34 ");
        sbMst.Append(" FROM            BANK_NAME INNER JOIN ");
        sbMst.Append(" BANK_BRANCH ON BANK_NAME.BANK_CODE = BANK_BRANCH.BANK_CODE RIGHT OUTER JOIN ");
        sbMst.Append(" DIVI_PARA INNER JOIN ");
        sbMst.Append(" DIVIDEND ON DIVI_PARA.FUND_CD = DIVIDEND.FUND_CD AND DIVI_PARA.DIVI_NO = DIVIDEND.DIVI_NO AND DIVI_PARA.F_YEAR = DIVIDEND.FY AND  ");
        sbMst.Append(" DIVI_PARA.CLOSE_DT = DIVIDEND.CLOSE_DT INNER JOIN ");
        sbMst.Append(" U_MASTER ON DIVIDEND.REG_BK = U_MASTER.REG_BK AND DIVIDEND.REG_BR = U_MASTER.REG_BR AND ");
        sbMst.Append(" DIVIDEND.REG_NO = U_MASTER.REG_NO INNER JOIN ");
        sbMst.Append(" FUND_INFO ON U_MASTER.REG_BK = FUND_INFO.FUND_CD ON BANK_BRANCH.BANK_CODE = DIVIDEND.BK_NM_CD AND ");
        sbMst.Append(" BANK_BRANCH.BRANCH_CODE = DIVIDEND.BK_BR_NM_CD LEFT OUTER JOIN ");
        sbMst.Append(" U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND ");
        sbMst.Append(" U_MASTER.REG_NO = U_JHOLDER.REG_NO ");
        sbMst.Append(" WHERE        (DIVI_PARA.FUND_CD = '" + fundCode + "') AND (DIVI_PARA.DIVI_NO = " + divi_no + ") AND (DIVIDEND.WAR_NO IN " + divi_no + ") AND DIVIDEND.IS_BEFTN='Y' and IS_BEFTN_SUCCS='N' ");
        sbMst.Append(" ORDER BY DIVIDEND.WAR_NO; ");


        DataTable dtDividendPara = DRcommonGatewayObj.Select(sbMst.ToString());
        return dtDividendPara;
    }

    public DataTable dtGetFundWiseFY(string fundCode)
    {
        DataTable dtFY = dtGetDataTableFY();
        DataRow drFY;
        drFY = dtFY.NewRow();
        drFY["F_YEAR"] = "--Select FY--";
        dtFY.Rows.Add(drFY);

        string SQL = "SELECT DISTINCT F_YEAR FROM " + bizSchema + ".DIVI_PARA WHERE FUND_CD='" + fundCode.ToString() + "' ORDER BY F_YEAR DESC";
        DataTable dtFYData = DRcommonGatewayObj.Select(SQL.ToString());
        if (dtFYData.Rows.Count > 0)
        {
            for (int loop = 0; loop < dtFYData.Rows.Count; loop++)
            {
                drFY = dtFY.NewRow();
                drFY["F_YEAR"] = dtFYData.Rows[loop]["F_YEAR"];
                dtFY.Rows.Add(drFY);

            }
        }
        return dtFY;
    }
    public DataTable dtGetFYWiseClosinDate(string FY, string fundCode)
    {
        string SQL = "SELECT DIVI_NO,TO_CHAR(CLOSE_DT,'DD-MON-YYYY') AS CLOSE_DT  FROM " + bizSchema + ".DIVI_PARA WHERE FUND_CD='" + fundCode.ToString() + "' AND F_YEAR='" + FY.ToString() + "' ORDER BY DIVI_NO DESC";
        DataTable dtClosingDate = DRcommonGatewayObj.Select(SQL.ToString());
        return dtClosingDate;
    }

    public object dtFY(string f_code)
    {
        DataTable dtList = DRcommonGatewayObj.Select("SELECT  DISTINCT F_YEAR, DIVI_NO FROM UNIT.DIVI_PARA WHERE FUND_CD='" + f_code + "' ORDER BY F_YEAR DESC");
        DataTable dtDropDown = new DataTable();
        dtDropDown.Columns.Add("F_YEAR", typeof(string));
        dtDropDown.Columns.Add("DIVI_NO", typeof(string));

        DataRow drdtDropDown = dtDropDown.NewRow();

        drdtDropDown["F_YEAR"] = "--Select F_YEAR--- ";
        drdtDropDown["DIVI_NO"] = "0";
        dtDropDown.Rows.Add(drdtDropDown);
        for (int loop = 0; loop < dtList.Rows.Count; loop++)
        {
            drdtDropDown = dtDropDown.NewRow();
            drdtDropDown["F_YEAR"] = dtList.Rows[loop]["F_YEAR"].ToString();
            drdtDropDown["DIVI_NO"] = dtList.Rows[loop]["DIVI_NO"].ToString();
            dtDropDown.Rows.Add(drdtDropDown);
        }
        return dtDropDown;
    }

    public DataTable getDividendProcessInfo(string fundCode, int divi_no, string fy, string filter)
    {
        DataTable dtDividendPara = DRcommonGatewayObj.Select("SELECT NVL(COUNT(*),0) AS NO_HOLDER, NVL(SUM(BALANCE),0) AS NO_UNITS, NVL(SUM(TOT_DIVI),0) AS GROSS, NVL(SUM(DIDUCT),0) AS TAX_AMOUNT, NVL(SUM(FI_DIVI_QTY),0) AS NET_AMOUNT FROM DIVIDEND WHERE FUND_CD='" + fundCode + "' AND FY = '" + fy + "' AND DIVI_NO=" + divi_no + " " + filter);
        return dtDividendPara;
    }

    public DataTable getDividendPara(string fundCode, int divi_no, string fy, string filter)
    {
        DataTable dtDividendPara = DRcommonGatewayObj.Select("SELECT * FROM DIVI_PARA WHERE FUND_CD='" + fundCode + "' AND F_YEAR = '" + fy + "' AND DIVI_NO=" + divi_no + " " + filter);
        return dtDividendPara;
    }

    public object dtFundList()
    {
        DataTable dtFundList = DRcommonGatewayObj.Select("SELECT FUND_CD , FUND_NM FROM UNIT.FUND_INFO WHERE FUND_CD IN(SELECT DISTINCT(FUND_CD) FROM UNIT.DIVI_PARA) ORDER BY FUND_CD");
        DataTable dtFundListDropDown = new DataTable();
        dtFundListDropDown.Columns.Add("FUND_CD", typeof(string));
        dtFundListDropDown.Columns.Add("FUND_NM", typeof(string));

        DataRow drFundListDropDown = dtFundListDropDown.NewRow();

        drFundListDropDown["FUND_NM"] = "--Select Fund--- ";
        drFundListDropDown["FUND_CD"] = "0";
        dtFundListDropDown.Rows.Add(drFundListDropDown);
        for (int loop = 0; loop < dtFundList.Rows.Count; loop++)
        {
            drFundListDropDown = dtFundListDropDown.NewRow();
            drFundListDropDown["FUND_NM"] = dtFundList.Rows[loop]["FUND_NM"].ToString();// + ", F_CODE [" + dtFundList.Rows[loop]["F_CD"].ToString()+"]";
            drFundListDropDown["FUND_CD"] = dtFundList.Rows[loop]["FUND_CD"].ToString();
            dtFundListDropDown.Rows.Add(drFundListDropDown);
        }
        return dtFundListDropDown;
    }

    public DataTable dtGetDataTableFY()
    {
        DataTable dtClosingDate = new DataTable();
        dtClosingDate.Columns.Add("F_YEAR", typeof(string));
        return dtClosingDate;
    }
    public DataTable dtGetDataTableforDividend()
    {
        DataTable dtDividend = new DataTable();
        dtDividend.Columns.Add("FUND_CD", typeof(string));
        dtDividend.Columns.Add("FUND_NM", typeof(string));
        dtDividend.Columns.Add("REG_BR", typeof(string));
        dtDividend.Columns.Add("REG_BR_NAME", typeof(string));
        dtDividend.Columns.Add("DIVI_NO", typeof(int));
        dtDividend.Columns.Add("F_YEAR", typeof(string));
        dtDividend.Columns.Add("CLOSE_DT", typeof(string));
        dtDividend.Columns.Add("DIVI_RATE", typeof(decimal));
        dtDividend.Columns.Add("TIN", typeof(string));

        dtDividend.Columns.Add("BK_AC_NO", typeof(string));
        dtDividend.Columns.Add("BK_AC_NO_MICR", typeof(string));
        dtDividend.Columns.Add("BK_NAME", typeof(string));
        dtDividend.Columns.Add("BK_ADDRS1", typeof(string));
        dtDividend.Columns.Add("BK_ADDRS2", typeof(string));
        dtDividend.Columns.Add("BK_ROUTING_NO", typeof(string));
        dtDividend.Columns.Add("BK_ROUTING_NO_MICR", typeof(string));
        dtDividend.Columns.Add("BK_TRANSACTION_CODE", typeof(int));

        dtDividend.Columns.Add("ISS_DT", typeof(string));
        dtDividend.Columns.Add("REG_NO", typeof(string));
        dtDividend.Columns.Add("JNT_NAME", typeof(string));
        dtDividend.Columns.Add("HNAME", typeof(string));
        dtDividend.Columns.Add("ADDRS1", typeof(string));
        dtDividend.Columns.Add("ADDRS2", typeof(string));
        dtDividend.Columns.Add("CITY", typeof(string));

        dtDividend.Columns.Add("WAR_NO", typeof(string));
        dtDividend.Columns.Add("WAR_NO_MICR", typeof(string));
        dtDividend.Columns.Add("NO_OF_UNITS", typeof(int));
        dtDividend.Columns.Add("TOT_DIVI", typeof(decimal));
        dtDividend.Columns.Add("TAX_DIDUCT", typeof(decimal));
        dtDividend.Columns.Add("FI_DIVI_QTY", typeof(decimal));
        dtDividend.Columns.Add("FI_DIVI_QTY_INWORD", typeof(string));

        dtDividend.Columns.Add("CIP_QTY", typeof(int));
        dtDividend.Columns.Add("CIP_RATE", typeof(decimal));
        dtDividend.Columns.Add("CIP", typeof(string));
        dtDividend.Columns.Add("AGM_DT", typeof(string));
        dtDividend.Columns.Add("TAX_RT_INDIVIDUAL", typeof(decimal));
        dtDividend.Columns.Add("TAX_RT_INSTITUTION", typeof(decimal));
        dtDividend.Columns.Add("TAX_CAL_TEXT", typeof(string));


        dtDividend.Columns.Add("REG_TYPE", typeof(string));
        dtDividend.Columns.Add("FY_PART", typeof(string));
        dtDividend.Columns.Add("NET_DIVI", typeof(decimal));
        dtDividend.Columns.Add("FRAC_DIVI", typeof(decimal));
        dtDividend.Columns.Add("REG_NUM", typeof(int));
        dtDividend.Columns.Add("HOLDER_BK_ACC_NO", typeof(string));
        dtDividend.Columns.Add("HOLDER_BK_NM", typeof(string));
        dtDividend.Columns.Add("HOLDER_BK_BR_NM", typeof(string));
        dtDividend.Columns.Add("HOLDER_BK_BR_ADDRES", typeof(string));
        dtDividend.Columns.Add("SL_NO", typeof(int));
        dtDividend.Columns.Add("CIP_CERT", typeof(string));
        dtDividend.Columns.Add("ID_AC", typeof(string));
        dtDividend.Columns.Add("ID_BK_NM", typeof(string));
        dtDividend.Columns.Add("ID_BK_BR_NM", typeof(string));
        dtDividend.Columns.Add("WAR_TYPE", typeof(string));
        dtDividend.Columns.Add("BO_FOLIO", typeof(string));

        return dtDividend;


    }

    public DataTable GetWarrantInfo(string filter)
    {
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        DataTable dtCommonData = new DataTable();
        sbfilter.Append(" ");
        sbOrderBy.Append("");

        sbMst.Append("SELECT UNIT.FUND_INFO.*, DIVIDEND.*, DIVI_PARA.* FROM DIVIDEND, UNIT.FUND_INFO, DIVI_PARA WHERE DIVIDEND.FUND_CD = UNIT.FUND_INFO.FUND_CD ");
        sbMst.Append(" AND DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO AND DIVIDEND.FY = DIVI_PARA.F_YEAR AND  DIVIDEND.CLOSE_DT = DIVIDEND_PARA.CLOSE_DT ");
        sbMst.Append(filter.ToString());
        sbMst.Append(" ORDER BY DIVIDEND.WAR_NO  ");
        sbMst.Append(sbfilter.ToString());

        dtCommonData = DRcommonGatewayObj.Select(sbMst.ToString());

        return dtCommonData;
    }

    public DataTable GetWarrantInfoTable()
    {
        DataTable dtWarrantInfoTable = new DataTable();

        dtWarrantInfoTable.Columns.Add("ID", typeof(long));
        dtWarrantInfoTable.Columns.Add("WAR_NO", typeof(string));
        dtWarrantInfoTable.Columns.Add("NAME", typeof(string));
        dtWarrantInfoTable.Columns.Add("ACC_INFO", typeof(string));

        dtWarrantInfoTable.Columns.Add("NO_OF_UNIT", typeof(string));
        dtWarrantInfoTable.Columns.Add("NET_DIVIDEND", typeof(string));
        dtWarrantInfoTable.Columns.Add("WAR_TYPE", typeof(string));
        dtWarrantInfoTable.Columns.Add("WAR_DETAILS", typeof(string));


        return dtWarrantInfoTable;
    }

    public string getWarrantStatus(DataTable dtSource, int looper)
    {



        string WarDetails = "";



        if (!(dtSource.Rows[looper]["WAR_BK_PAY"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = "PAID STATUS: PAID";
            }
            else
            {
                WarDetails = WarDetails + " ; PAID STATUS: PAID ";
            }
        }
        if (!(dtSource.Rows[looper]["WAR_BK_PAY_DT"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = "Bank Paid Date: " + Convert.ToDateTime(dtSource.Rows[looper]["WAR_BK_PAY_DT"]).ToString("dd-MMM-yyyy");
            }
            else
            {
                WarDetails = WarDetails + " ;Bank Paid Date: " + Convert.ToDateTime(dtSource.Rows[looper]["BANK_PAYMENT_DATE"]).ToString("dd-MMM-yyyy");
            }
        }

        if (!(dtSource.Rows[looper]["BEFTN_RESENT_DATE"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = "BEFTN Resent Date: " + Convert.ToDateTime(dtSource.Rows[looper]["BEFTN_RESENT_DATE"]).ToString("dd-MMM-yyyy");
            }
            else
            {
                WarDetails = WarDetails + " ; BEFTN Resent Date: " + Convert.ToDateTime(dtSource.Rows[looper]["BEFTN_RESENT_DATE"]).ToString("dd-MMM-yyyy");
            }
        }
        if (!(dtSource.Rows[looper]["BEFTN_CREDIT_DT"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = " BEFTN CREDIT Date: " + Convert.ToDateTime(dtSource.Rows[looper]["BEFTN_CREDIT_DT"]).ToString("dd-MMM-yyyy");
            }
            else
            {
                WarDetails = WarDetails + " ; BEFTN CREDIT Date: " + Convert.ToDateTime(dtSource.Rows[looper]["BEFTN_CREDIT_DT"]).ToString("dd-MMM-yyyy");
            }
        }


        if (!(dtSource.Rows[looper]["ONLINE_CREDIT_DATE"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = " ONLINE Transfer on/before Date: " + Convert.ToDateTime(dtSource.Rows[looper]["ONLINE_CREDIT_DATE"]).ToString("dd-MMM-yyyy");
            }
            else
            {
                WarDetails = WarDetails + " ;ONLINE Transfer on/before Date: " + Convert.ToDateTime(dtSource.Rows[looper]["ONLINE_CREDIT_DATE"]).ToString("dd-MMM-yyyy");
            }
        }
        if (((dtSource.Rows[looper]["IS_BEFTN"].ToString() == "Y")) || !(dtSource.Rows[looper]["ONLINE_CREDIT_DATE"].Equals(DBNull.Value)))
        {
            string routingNo = dtSource.Rows[0]["ROUTING_NO"].Equals(DBNull.Value) ? "" : dtSource.Rows[0]["ROUTING_NO"].ToString();

            if (WarDetails == "")
            {
                WarDetails = " ACC:" + dtSource.Rows[0]["BK_AC_NO"].ToString() + ", " + dtSource.Rows[0]["BANK"].ToString() + " ," + dtSource.Rows[0]["BRANCH"].ToString() + "[" + routingNo + "]";
            }
            else
            {
                WarDetails = WarDetails + " ; ACC:" + dtSource.Rows[0]["BK_AC_NO"].ToString() + ", " + dtSource.Rows[0]["BANK"].ToString() + " ," + dtSource.Rows[0]["BRANCH"].ToString() + "[" + routingNo + "]";
            }

        }

        if (!(dtSource.Rows[looper]["WARR_RECPT_BY"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = "  Delivery to: " + dtSource.Rows[looper]["WARR_RECPT_BY"].ToString().ToUpper();
            }
            else
            {
                WarDetails = WarDetails + " ; Delivery to: " + dtSource.Rows[looper]["WARR_RECPT_BY"].ToString().ToUpper();
            }
        }


        if (!(dtSource.Rows[looper]["WAR_DELEVERY_DT"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = "Delivery  Date: " + Convert.ToDateTime(dtSource.Rows[looper]["WAR_DELEVERY_DT"]).ToString("dd-MMM-yyyy");
            }
            else
            {
                WarDetails = WarDetails + " ;Delivery Date: " + Convert.ToDateTime(dtSource.Rows[looper]["WAR_DELEVERY_DT"]).ToString("dd-MMM-yyyy");
            }
        }

        if (!(dtSource.Rows[looper]["BEFTN_RETURN_DT"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = " BEFTN Return Date: " + Convert.ToDateTime(dtSource.Rows[looper]["BEFTN_RETURN_DT"]).ToString("dd-MMM-yyyy");
            }
            else
            {
                WarDetails = WarDetails + " ; BEFTN Return Date: " + Convert.ToDateTime(dtSource.Rows[looper]["BEFTN_RETURN_DT"]).ToString("dd-MMM-yyyy");
            }

        }
        if (!(dtSource.Rows[looper]["ONLINE_RETURN_DATE"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = " ONLINE Return Date: " + Convert.ToDateTime(dtSource.Rows[looper]["ONLINE_RETURN_DATE"]).ToString("dd-MMM-yyyy");
            }
            else
            {
                WarDetails = WarDetails + " ; ONLINE Return Date: " + Convert.ToDateTime(dtSource.Rows[looper]["ONLINE_RETURN_DATE"]).ToString("dd-MMM-yyyy");
            }
        }
        if (!(dtSource.Rows[looper]["BEFTN_RETURN_REASON"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = " BEFTN Return Reason: " + dtSource.Rows[looper]["BEFTN_RETURN_REASON"].ToString().ToUpper();
            }
            else
            {
                WarDetails = WarDetails + " ; BEFTN Return Reason: " + dtSource.Rows[looper]["BEFTN_RETURN_REASON"].ToString().ToUpper();
            }

        }
        if (!(dtSource.Rows[looper]["IS_FORIGN_ADDRESS"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = "  FORIGN ADDRESS ";
            }
            else
            {
                WarDetails = WarDetails + " ; FORIGN ADDRESS ";
            }
        }
        if (!(dtSource.Rows[looper]["POST_RETURN_DATE"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = " Post Return Date: " + Convert.ToDateTime(dtSource.Rows[looper]["POST_RETURN_DATE"]).ToString("dd-MMM-yyyy");
            }
            else
            {
                WarDetails = WarDetails + " ; Post Return Date: " + Convert.ToDateTime(dtSource.Rows[looper]["POST_RETURN_DATE"]).ToString("dd-MMM-yyyy");
            }
        }

        if (!(dtSource.Rows[looper]["POST_DETAILS"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = " POST DETAILS: " + dtSource.Rows[looper]["POST_DETAILS"].ToString().ToUpper();
            }
            else
            {
                WarDetails = WarDetails + "; POST DETAILS; " + dtSource.Rows[looper]["POST_DETAILS"].ToString().ToUpper();
            }
        }

        if (!(dtSource.Rows[looper]["POST_DATE"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = " Post  Date: " + Convert.ToDateTime(dtSource.Rows[looper]["POST_DATE"]).ToString("dd-MMM-yyyy");
            }
            else
            {
                WarDetails = WarDetails + " ; Post Date: " + Convert.ToDateTime(dtSource.Rows[looper]["POST_DATE"]).ToString("dd-MMM-yyyy");
            }
        }

        if (!(dtSource.Rows[looper]["DUPLICATE_ISSUE_DATE"].Equals(DBNull.Value)))
        {
            if (WarDetails == "")
            {
                WarDetails = " Duplicate Issue  Date: " + Convert.ToDateTime(dtSource.Rows[looper]["DUPLICATE_ISSUE_DATE"]).ToString("dd-MMM-yyyy");
            }
            else
            {
                WarDetails = WarDetails + " ; Duplicate Issue  Date:" + Convert.ToDateTime(dtSource.Rows[looper]["DUPLICATE_ISSUE_DATE"]).ToString("dd-MMM-yyyy");
            }
        }

        return WarDetails;



    }

    public DataTable dtGetFundWiseAccNo(string fundCode, string FY, string closingDate)
    {
        DateTime ClosingDate = Convert.ToDateTime(closingDate);
        string SQL = "SELECT BK_AC_NO, DIVI_NO  FROM " + bizSchema + ".DIVI_PARA WHERE FUND_CD='" + fundCode.ToString() + "' AND F_YEAR='" + FY.ToString() + " AND CLOSE_DT=" + ClosingDate.ToString("dd-MMM-yyyy");
        DataTable bkAcNo = DRcommonGatewayObj.Select(SQL.ToString());
        return bkAcNo;
    }

    public DataTable getDividendPayDetailsInfo(string fundCode, int divi_no, string fy, string filter)
    {
        DataTable dtDividendPara = DRcommonGatewayObj.Select("SELECT DIVIDEND.FI_DIVI_QTY AS NET_DIVIDEND, DIVIDEND.BALANCE, DIVIDEND.WAR_NO, U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, U_MASTER.HNAME AS NAME1, U_MASTER.ADDRS1 AS ADDRESS1, U_MASTER.ADDRS2 AS ADDRESS2, U_MASTER.BO, U_MASTER.FOLIO_NO, U_MASTER.ALLOT_NO, U_MASTER.MOBILE1 AS PHONE1, U_MASTER.CITY AS ADDRESS3, U_MASTER.TEL_NO AS PHONE2 FROM DIVIDEND, U_MASTER WHERE DIVIDEND.REG_BK = U_MASTER.REG_BK AND DIVIDEND.REG_BR = U_MASTER.REG_BR AND DIVIDEND.REG_NO = U_MASTER.REG_NO AND DIVIDEND.FUND_CD ='" + fundCode + "' AND DIVIDEND.FY = '" + fy + "' AND DIVIDEND.DIVI_NO=" + divi_no + " " + filter + " ORDER BY DIVIDEND.WAR_NO");
        return dtDividendPara;
    }

    public int getCIPSaleNo(string fundCode, string branchCode, int regNumber)
    {
        int saleNo = 0;
        DataTable dtSaleNo = DRcommonGatewayObj.Select("SELECT * FROM CIP_SALE WHERE REG_BK='" + fundCode + "' AND REG_BR='" + branchCode + "' AND REG_NO=" + regNumber);
        if (dtSaleNo.Rows.Count > 0)
        {
            saleNo = Convert.ToInt32(dtSaleNo.Rows[0]["SL_NO"].Equals(DBNull.Value) ? "0" : dtSaleNo.Rows[0]["SL_NO"].ToString());
        }
        return saleNo;

    }
    //public int getBankCode(string fundCode, string FY, string closingDate)
    //{
    //    DataTable dtBankCode = commonGatewayObj.Select(" NVL(BANK_CODE,0) AS BANK_CODE WHERE F_YEAR='"+FY.ToString()+"' AND FUND_CD='"+fundCode.ToString()+"' AND CLOSE_DT='" + closingDate.ToString() + "' AND IS_BEFTN='Y' ");
    //    int bankCode = Convert.ToInt32(dtBankCode.Rows[0]["BANK_CODE"].Equals(DBNull.Value) ? "0" : dtBankCode.Rows[0]["BANK_CODE"].ToString());
    //}
    public decimal getTaxDiductRate(int regNumber, string fundCode, string branchCode, string fy, decimal txtLimit)
    {
        decimal taxRate = 0;
        StringBuilder sbQuery = new StringBuilder();

        sbQuery.Append("SELECT SUM(TOT_DIVI) AS TOT_DIVI, SUM(DIDUCT) AS DIDUCT ");
        sbQuery.Append(" FROM  DIVIDEND WHERE (REG_BR = '" + branchCode.ToString() + "') AND (REG_NO = " + regNumber + ") AND (REG_BK = '" + fundCode.ToString() + "') AND (FY = '" + fy.ToString() + "') ");

        //sbQuery.Append(" SELECT DECODE(DIVIDEND.DIDUCT, 0, 0, ROUND((DIVIDEND.DIDUCT * 100) / (DIVIDEND.TOT_DIVI - DIVI_PARA.TAX_LIMIT), 2)) AS TAX_RATE ");
        //sbQuery.Append(" FROM DIVIDEND INNER JOIN   DIVI_PARA ON DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO AND DIVIDEND.FY = DIVI_PARA.F_YEAR AND DIVIDEND.CLOSE_DT = DIVI_PARA.CLOSE_DT ");
        //sbQuery.Append(" WHERE (DIVIDEND.REG_BR = '" + branchCode.ToString() + "') AND (DIVIDEND.REG_NO = " + regNumber + ") AND (DIVIDEND.DIVI_NO =  " + diviNo + ") AND (DIVIDEND.REG_BK = '" + fundCode.ToString() + "')");
        DataTable dtTaxDiduct = DRcommonGatewayObj.Select(sbQuery.ToString());
        if (dtTaxDiduct.Rows.Count > 0)
        {
            if (Convert.ToDecimal(dtTaxDiduct.Rows[0]["DIDUCT"].ToString()) > 0)
            {
                taxRate = (Convert.ToDecimal(dtTaxDiduct.Rows[0]["DIDUCT"].ToString()) * 100) / (Convert.ToDecimal(dtTaxDiduct.Rows[0]["TOT_DIVI"].ToString()) - txtLimit);
                taxRate = decimal.Round(taxRate, 2);
            }
        }
        return taxRate;

    }
    public string getRegType(int regNumber, string fundCode, string branchCode)
    {
        DataTable dtRegType = DRcommonGatewayObj.Select("SELECT * FROM U_MASTER WHERE REG_BR='" + branchCode.ToString() + "' AND REG_BK='" + fundCode.ToString() + "' AND REG_NO=" + regNumber);
        string regType = dtRegType.Rows[0]["REG_TYPE"].ToString();
        return regType;
    }
    public DataTable dtDividendInfo(int regNumber, string fundCode, string branchCode, string fy)
    {

        StringBuilder sbQuery = new StringBuilder();
        sbQuery.Append("SELECT DIVIDEND.*,DIVI_PARA.FY_PART ,DIVI_PARA.RATE,DIVI_PARA.TAX_LIMIT FROM DIVIDEND INNER JOIN   DIVI_PARA ON DIVIDEND.FY = DIVI_PARA.F_YEAR AND DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.CLOSE_DT = DIVI_PARA.CLOSE_DT AND ");
        sbQuery.Append(" DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO WHERE (DIVIDEND.REG_BR = '" + branchCode.ToString() + "') AND (DIVIDEND.REG_NO = " + regNumber + ") AND (DIVIDEND.REG_BK = '" + fundCode.ToString() + "') AND (DIVIDEND.FY = '" + fy.ToString() + "')  ORDER BY DIVIDEND.DIVI_NO");
        DataTable dtTaxCal = DRcommonGatewayObj.Select(sbQuery.ToString());
        return dtTaxCal;
    }
    public DataTable getdtBOUploadTable()
    {
        DataTable dtBOUploadTable = new DataTable();

        dtBOUploadTable.Columns.Add("SI", typeof(string));
        dtBOUploadTable.Columns.Add("FUND_NAME", typeof(string));
        dtBOUploadTable.Columns.Add("BO", typeof(string));
        dtBOUploadTable.Columns.Add("NAME1", typeof(string));
        dtBOUploadTable.Columns.Add("NAME2", typeof(string));
        dtBOUploadTable.Columns.Add("BO_FATHER", typeof(string));
        dtBOUploadTable.Columns.Add("BO_MOTHER", typeof(string));
        dtBOUploadTable.Columns.Add("BO_TYPE", typeof(string));
        dtBOUploadTable.Columns.Add("BO_CATAGORY", typeof(string));
        dtBOUploadTable.Columns.Add("COUNTRY", typeof(string));
        dtBOUploadTable.Columns.Add("BANK_ACC_NO", typeof(string));
        dtBOUploadTable.Columns.Add("BANK_NAME", typeof(string));
        dtBOUploadTable.Columns.Add("BRANCH_NAME", typeof(string));
        dtBOUploadTable.Columns.Add("RESIDENCY", typeof(string));
        dtBOUploadTable.Columns.Add("BALANCE", typeof(string));
        dtBOUploadTable.Columns.Add("DIVIDEND", typeof(string));
        dtBOUploadTable.Columns.Add("TAX", typeof(string));
        dtBOUploadTable.Columns.Add("FINAL_DIVIDEND", typeof(string));
        dtBOUploadTable.Columns.Add("ADDRESS1", typeof(string));
        dtBOUploadTable.Columns.Add("ADDRESS2", typeof(string));
        dtBOUploadTable.Columns.Add("ADDRESS3", typeof(string));
        dtBOUploadTable.Columns.Add("ADDRESS4", typeof(string));
        dtBOUploadTable.Columns.Add("CITY", typeof(string));
        dtBOUploadTable.Columns.Add("POST_CODE", typeof(string));
        dtBOUploadTable.Columns.Add("ADDRESS_COUNTRY", typeof(string));
        dtBOUploadTable.Columns.Add("PHONE1", typeof(string));
        dtBOUploadTable.Columns.Add("PHONE2", typeof(string));
        dtBOUploadTable.Columns.Add("EMAIL", typeof(string));
        dtBOUploadTable.Columns.Add("NO_OF_BANKS", typeof(string));
        dtBOUploadTable.Columns.Add("BANK_CODE", typeof(int));
        dtBOUploadTable.Columns.Add("ROUTING_NO", typeof(string));
        dtBOUploadTable.Columns.Add("ETIN", typeof(string));
        dtBOUploadTable.Columns.Add("IS_VALID_ETIN", typeof(string));
        dtBOUploadTable.Columns.Add("OCCUPATION", typeof(string));
        dtBOUploadTable.Columns.Add("GENDER", typeof(string));
        dtBOUploadTable.Columns.Add("DATE_OF_BIRTH", typeof(string));
        dtBOUploadTable.Columns.Add("BO_NATIONALITY", typeof(string));
        dtBOUploadTable.Columns.Add("REG_BK", typeof(string));
        dtBOUploadTable.Columns.Add("REG_BR", typeof(string));
        dtBOUploadTable.Columns.Add("REG_NO", typeof(int));
        dtBOUploadTable.Columns.Add("NO_OF_REGNO", typeof(int));





        return dtBOUploadTable;


    }
    public DataTable dtRegInfo(string fund_code, string bo)
    {
        DataTable dtRegiInfo = DRcommonGatewayObj.Select("SELECT * FROM U_MASTER WHERE REG_BK='" + fund_code + "' AND SUBSTR(BO,9,8)=SUBSTR(" + bo + ",9,8)");
        DRcommonGatewayObj.BeginTransaction();
        if (dtRegiInfo.Rows.Count > 0)
        {
            DRcommonGatewayObj.ExecuteNonQuery("UPDATE U_MASTER SET CDBL_RECORD_DATE_STATUS='Y' WHERE REG_BK='" + fund_code + "' AND SUBSTR(BO,9,8)=SUBSTR(" + bo + ",9,8)");
            DRcommonGatewayObj.CommitTransaction();
            return dtRegiInfo;

        }
        else
        {
            DRcommonGatewayObj.RollbackTransaction();
            return dtRegiInfo;
        }

    }
}