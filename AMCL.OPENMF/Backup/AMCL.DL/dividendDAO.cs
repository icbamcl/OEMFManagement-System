using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using AMCL.GATEWAY;
using AMCL.COMMON;
using System.Data;

namespace AMCL.DL
{
   public class dividendDAO
    {
       string bizSchema = ConfigReader.SCHEMA;
       CommonGateway commonGatewayObj = new CommonGateway();
       public DataTable dtGetFundWiseFY(string fundCode)
       {
           DataTable dtFY = dtGetDataTableFY();
           DataRow drFY;
           drFY = dtFY.NewRow();          
           drFY["F_YEAR"] = "--Select FY--";
           dtFY.Rows.Add(drFY);

           string SQL = "SELECT DISTINCT F_YEAR FROM " + bizSchema + ".DIVI_PARA WHERE FUND_CD='" + fundCode.ToString() + "' ORDER BY F_YEAR DESC";
           DataTable dtFYData = commonGatewayObj.Select(SQL.ToString());
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
       public DataTable dtGetFYWiseClosinDate(string FY,string fundCode)
       {           
           string SQL = "SELECT DIVI_NO,TO_CHAR(CLOSE_DT,'DD-MON-YYYY') AS CLOSE_DT  FROM " + bizSchema + ".DIVI_PARA WHERE FUND_CD='" + fundCode.ToString() + "' AND F_YEAR='" + FY.ToString() + "' ORDER BY DIVI_NO DESC";
           DataTable dtClosingDate = commonGatewayObj.Select(SQL.ToString());
           return dtClosingDate;
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
          
           





           return dtDividend;


       }
       public int getCIPSaleNo(string fundCode,string branchCode,int regNumber)
       {
           int saleNo = 0;
           DataTable dtSaleNo = commonGatewayObj.Select("SELECT * FROM CIP_SALE WHERE REG_BK='" + fundCode + "' AND REG_BR='" + branchCode + "' AND REG_NO=" + regNumber);
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
       public decimal getTaxDiductRate(int regNumber, string fundCode, string branchCode, string fy,decimal txtLimit)
       {
           decimal taxRate = 0;
           StringBuilder sbQuery = new StringBuilder();

           sbQuery.Append("SELECT SUM(TOT_DIVI) AS TOT_DIVI, SUM(DIDUCT) AS DIDUCT ");
           sbQuery.Append(" FROM  DIVIDEND WHERE (REG_BR = '" + branchCode.ToString() + "') AND (REG_NO = " + regNumber + ") AND (REG_BK = '" + fundCode.ToString() + "') AND (FY = '" + fy.ToString() + "') ");

           //sbQuery.Append(" SELECT DECODE(DIVIDEND.DIDUCT, 0, 0, ROUND((DIVIDEND.DIDUCT * 100) / (DIVIDEND.TOT_DIVI - DIVI_PARA.TAX_LIMIT), 2)) AS TAX_RATE ");
           //sbQuery.Append(" FROM DIVIDEND INNER JOIN   DIVI_PARA ON DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO AND DIVIDEND.FY = DIVI_PARA.F_YEAR AND DIVIDEND.CLOSE_DT = DIVI_PARA.CLOSE_DT ");
           //sbQuery.Append(" WHERE (DIVIDEND.REG_BR = '" + branchCode.ToString() + "') AND (DIVIDEND.REG_NO = " + regNumber + ") AND (DIVIDEND.DIVI_NO =  " + diviNo + ") AND (DIVIDEND.REG_BK = '" + fundCode.ToString() + "')");
           DataTable dtTaxDiduct = commonGatewayObj.Select(sbQuery.ToString());
           if (dtTaxDiduct.Rows.Count > 0)
           {
               if (Convert.ToDecimal(dtTaxDiduct.Rows[0]["DIDUCT"].ToString())>0)
               {
                   taxRate = (Convert.ToDecimal(dtTaxDiduct.Rows[0]["DIDUCT"].ToString()) * 100) / (Convert.ToDecimal(dtTaxDiduct.Rows[0]["TOT_DIVI"].ToString()) - txtLimit);
                   taxRate = decimal.Round(taxRate, 2);
               }
           }
           return taxRate;

       }
       public string getRegType(int regNumber, string fundCode, string branchCode)
       {
           DataTable dtRegType = commonGatewayObj.Select("SELECT * FROM U_MASTER WHERE REG_BR='" + branchCode.ToString() + "' AND REG_BK='" + fundCode.ToString() + "' AND REG_NO=" + regNumber);
           string regType = dtRegType.Rows[0]["REG_TYPE"].ToString();
           return regType;
       }
       public DataTable dtDividendInfo(int regNumber, string fundCode, string branchCode, string fy)
       {

           StringBuilder sbQuery = new StringBuilder();
           sbQuery.Append("SELECT DIVIDEND.*,DIVI_PARA.FY_PART ,DIVI_PARA.RATE,DIVI_PARA.TAX_LIMIT FROM DIVIDEND INNER JOIN   DIVI_PARA ON DIVIDEND.FY = DIVI_PARA.F_YEAR AND DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.CLOSE_DT = DIVI_PARA.CLOSE_DT AND ");
           sbQuery.Append(" DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO WHERE (DIVIDEND.REG_BR = '" + branchCode.ToString() + "') AND (DIVIDEND.REG_NO = " + regNumber + ") AND (DIVIDEND.REG_BK = '" + fundCode.ToString() + "') AND (DIVIDEND.FY = '" + fy.ToString() + "')  ORDER BY DIVIDEND.DIVI_NO");
           DataTable dtTaxCal = commonGatewayObj.Select(sbQuery.ToString());
          return dtTaxCal;
       }
    }
}
