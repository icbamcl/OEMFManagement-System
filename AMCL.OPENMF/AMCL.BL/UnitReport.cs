using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Text;
using System.Data.OracleClient;
using AMCL.COMMON;
using AMCL.DL;
using AMCL.GATEWAY;


/// <summary>
/// Summary description for UnitReport
/// </summary>
namespace AMCL.BL
{
    public class UnitReport
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        OMFDAO opendMFDAO = new OMFDAO();
        public UnitReport()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable getDtHolderInfo()
        {
            DataTable dtHolderInfo = new DataTable();
            dtHolderInfo.Columns.Add("REG_NO", typeof(string));
            dtHolderInfo.Columns.Add("REG_BR_NAME", typeof(string)); 
            dtHolderInfo.Columns.Add("REG_DT", typeof(string));
            dtHolderInfo.Columns.Add("REG_TYPE", typeof(string));
            
            dtHolderInfo.Columns.Add("HNAME", typeof(string));
            dtHolderInfo.Columns.Add("FMH_NAME", typeof(string));
            dtHolderInfo.Columns.Add("MO_NAME", typeof(string));
            dtHolderInfo.Columns.Add("ADDRS1", typeof(string));
            dtHolderInfo.Columns.Add("ADDRS2", typeof(string));
            dtHolderInfo.Columns.Add("CITY", typeof(string));
            dtHolderInfo.Columns.Add("NATIONALITY", typeof(string));
            dtHolderInfo.Columns.Add("OCC_CODE", typeof(string));
            dtHolderInfo.Columns.Add("B_DATE", typeof(string));
            dtHolderInfo.Columns.Add("SEX", typeof(string));
            dtHolderInfo.Columns.Add("MAR_STAT", typeof(string));
            dtHolderInfo.Columns.Add("RELIGION", typeof(string));
            dtHolderInfo.Columns.Add("EDU_QUA", typeof(string));
            dtHolderInfo.Columns.Add("TEL_NO", typeof(string));
            dtHolderInfo.Columns.Add("EMAIL", typeof(string));
            dtHolderInfo.Columns.Add("CIP", typeof(string));
            dtHolderInfo.Columns.Add("ID_FLAG", typeof(string));
            dtHolderInfo.Columns.Add("ID_AC", typeof(string));
            dtHolderInfo.Columns.Add("ID_INTSTITUTE_NAME", typeof(string));
            dtHolderInfo.Columns.Add("ID_INTSTITUTE_BRANCH_NAME", typeof(string));
            dtHolderInfo.Columns.Add("BK_FLAG", typeof(string));
            dtHolderInfo.Columns.Add("BK_AC_NO", typeof(string));
            dtHolderInfo.Columns.Add("BANK_NAME", typeof(string));
            dtHolderInfo.Columns.Add("BRANCH_NAME", typeof(string));
            dtHolderInfo.Columns.Add("BRANCH_ADDRESS", typeof(string));
            dtHolderInfo.Columns.Add("BALANCE", typeof(int));
            dtHolderInfo.Columns.Add("BEFTN", typeof(string));
            dtHolderInfo.Columns.Add("TIN", typeof(string));
            dtHolderInfo.Columns.Add("PASS_NO", typeof(string));
            dtHolderInfo.Columns.Add("BIRTH_CERT_NO", typeof(string));
            dtHolderInfo.Columns.Add("NID", typeof(string));

            dtHolderInfo.Columns.Add("JNT_NAME", typeof(string));
            dtHolderInfo.Columns.Add("JNT_FMH_NAME", typeof(string));
            dtHolderInfo.Columns.Add("JNT_MO_NAME", typeof(string));
            dtHolderInfo.Columns.Add("JNT_OCC_CODE", typeof(string));
            dtHolderInfo.Columns.Add("JNT_ADDRS1", typeof(string));
            dtHolderInfo.Columns.Add("JNT_ADDRS2", typeof(string));
            dtHolderInfo.Columns.Add("JNT_NATIONALITY", typeof(string));
            dtHolderInfo.Columns.Add("JNT_CITY", typeof(string));
            dtHolderInfo.Columns.Add("JNT_TEL_NO", typeof(string));
            dtHolderInfo.Columns.Add("JNT_FMH_REL", typeof(string));

            dtHolderInfo.Columns.Add("NOMI_CTL_NO", typeof(string));
            dtHolderInfo.Columns.Add("NOMI1_NAME", typeof(string));
            dtHolderInfo.Columns.Add("NOMI1_FMH_NAME", typeof(string));
            dtHolderInfo.Columns.Add("NOMI1_MO_NAME", typeof(string));
            dtHolderInfo.Columns.Add("NOMI1_OCC_CODE", typeof(string));
            dtHolderInfo.Columns.Add("NOMI1_ADDRS1", typeof(string));
            dtHolderInfo.Columns.Add("NOMI1_ADDRS2", typeof(string));
            dtHolderInfo.Columns.Add("NOMI1_CITY", typeof(string));
            dtHolderInfo.Columns.Add("NOMI1_NATIONALITY", typeof(string));
            dtHolderInfo.Columns.Add("NOMI1_NOMI_REL", typeof(string));
            dtHolderInfo.Columns.Add("NOMI1_PERCENTAGE", typeof(string));

            dtHolderInfo.Columns.Add("NOMI2_NAME", typeof(string));
            dtHolderInfo.Columns.Add("NOMI2_FMH_NAME", typeof(string));
            dtHolderInfo.Columns.Add("NOMI2_MO_NAME", typeof(string));
            dtHolderInfo.Columns.Add("NOMI2_OCC_CODE", typeof(string));
            dtHolderInfo.Columns.Add("NOMI2_ADDRS1", typeof(string));
            dtHolderInfo.Columns.Add("NOMI2_ADDRS2", typeof(string));
            dtHolderInfo.Columns.Add("NOMI2_CITY", typeof(string));
            dtHolderInfo.Columns.Add("NOMI2_NATIONALITY", typeof(string));
            dtHolderInfo.Columns.Add("NOMI2_NOMI_REL", typeof(string));
            dtHolderInfo.Columns.Add("NOMI2_PERCENTAGE", typeof(string));

            return dtHolderInfo;
        }
        public string getMaritialFullName(string shortName)
        {
            string fullName = "";
            if (string.Compare(shortName, "M", true) == 0)
            {
                fullName = "MARRIED";
            }
            else if (string.Compare(shortName, "U", true) == 0)
            {
                fullName = "UNMARRIED";
            }
            else if (string.Compare(shortName, "O", true) == 0)
            {
                fullName = "OTHERS";
            }
            return fullName;
        }
        public string getReligionFullName(string shortName)
        {
            string fullName = "";
            if (string.Compare(shortName, "M", true) == 0)
            {
                fullName = "MUSLIM";
            }
            else if (string.Compare(shortName, "H", true) == 0)
            {
                fullName = "HINDU";
            }
            else if (string.Compare(shortName, "C", true) == 0)
            {
                fullName = "CHRISTIAN";
            }
            else if (string.Compare(shortName, "B", true) == 0)
            {
                fullName = "BUDDAH";
            }
            else if (string.Compare(shortName, "O", true) == 0)
            {
                fullName = "OTHERS";
            }
            return fullName;
        }
        public string getRegTypeFullName(string shortName)
        {
            string fullName = "";
            if (string.Compare(shortName, "N", true) == 0)
            {
                fullName = "NORMAL";
            }
            else if (string.Compare(shortName, "C", true) == 0)
            {
                fullName = "CHARITY";
            }
            else if (string.Compare(shortName, "I", true) == 0)
            {
                fullName = "INSTITUTION";
            }
            return fullName;
        }
        public string getBankNameByBankCode(int bankCode)
        {
            string bankName = "";
            DataTable dtBankName = commonGatewayObj.Select("SELECT * FROM BANK_NAME WHERE BANK_CODE=" + bankCode);
            if (dtBankName.Rows.Count > 0)
            {
                bankName = dtBankName.Rows[0]["BANK_NAME"].Equals(DBNull.Value) ? "" : dtBankName.Rows[0]["BANK_NAME"].ToString();
            }
            return bankName;


        }
        public string getBankBranchNameByCode(int bankCode, int branchCode)
        {
            string branchName = "";
            DataTable dtBranchName = commonGatewayObj.Select("SELECT * FROM BANK_BRANCH WHERE BANK_CODE=" + bankCode + " AND BRANCH_CODE=" + branchCode);
            if (dtBranchName.Rows.Count > 0)
            {
                branchName = dtBranchName.Rows[0]["BRANCH_NAME"].Equals(DBNull.Value) ? "" : dtBranchName.Rows[0]["BRANCH_NAME"].ToString();
            }
            return branchName;


        }
        public string getBankBranchAddressByCode(int bankCode, int branchCode)
        {
            string branchAdddress = "";

            DataTable dtBranchAddress = commonGatewayObj.Select("SELECT * FROM BANK_BRANCH WHERE BANK_CODE=" + bankCode + " AND BRANCH_CODE=" + branchCode);
            if (dtBranchAddress.Rows.Count > 0)
            {
                string branch1 = dtBranchAddress.Rows[0]["BRANCH_ADDRS1"].Equals(DBNull.Value) ? "" : dtBranchAddress.Rows[0]["BRANCH_ADDRS1"].ToString();
                string branch2 = dtBranchAddress.Rows[0]["BRANCH_ADDRS2"].Equals(DBNull.Value) ? "" : dtBranchAddress.Rows[0]["BRANCH_ADDRS2"].ToString();
                string branch3 = dtBranchAddress.Rows[0]["BRANCH_DISTRICT"].Equals(DBNull.Value) ? "" : dtBranchAddress.Rows[0]["BRANCH_DISTRICT"].ToString();

                branchAdddress = branch1.ToString() + " " + branch2.ToString() + " " + branch3.ToString();

            }
            return branchAdddress;


        }
        public DataTable dtNominee(string fundCode, string branchCode, int regNo)
        {
            DataTable dtNominee = new DataTable();
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT  U_NOMINEE.REG_BK, U_NOMINEE.REG_BR, U_NOMINEE.REG_NO, U_NOMINEE.NOMI_NO, U_NOMINEE.NOMI_NAME,U_NOMINEE.NOMI_CTL_NO,");
            sbQuery.Append(" U_NOMINEE.NOMI_FMH_NAME, OCC_CODE.DESCR, U_NOMINEE.NOMI_ADDRS1, U_NOMINEE.NOMI_ADDRS2, U_NOMINEE.NOMI_CITY,");
            sbQuery.Append(" U_NOMINEE.NOMI_NATIONALITY, U_NOMINEE.NOMI_REL, U_NOMINEE.PERCENTAGE FROM  U_NOMINEE  LEFT OUTER JOIN OCC_CODE ON U_NOMINEE.NOMI_OCC_CODE = OCC_CODE.CODE ");
            sbQuery.Append(" WHERE U_NOMINEE.REG_BK='" + fundCode.ToString() + "' AND  U_NOMINEE.REG_BR='" + branchCode.ToString() + "' AND U_NOMINEE.REG_NO=" + regNo + "");
            sbQuery.Append(" ORDER BY NOMI_NO");
            dtNominee = commonGatewayObj.Select(sbQuery.ToString());
            return dtNominee;

        }
        public DataTable getDtForReportStatement()
        {
            DataTable dtReportStatement = new DataTable();
            dtReportStatement.Columns.Add("SL_NO", typeof(int));
            dtReportStatement.Columns.Add("SL_DT", typeof(string));
            dtReportStatement.Columns.Add("HNAME", typeof(string));
            dtReportStatement.Columns.Add("REG_TYPE", typeof(string));
            dtReportStatement.Columns.Add("NOMI1_NAME", typeof(string));
            dtReportStatement.Columns.Add("NOMI2_NAME", typeof(string));
            dtReportStatement.Columns.Add("JNT_NAME", typeof(string));
            dtReportStatement.Columns.Add("ADDRS1", typeof(string));
            dtReportStatement.Columns.Add("ADDRS2", typeof(string));
            dtReportStatement.Columns.Add("MOBILE1", typeof(string));      
            dtReportStatement.Columns.Add("CITY", typeof(string));
            dtReportStatement.Columns.Add("REG_NO", typeof(string));
            dtReportStatement.Columns.Add("BK_AC_NO", typeof(string));
            dtReportStatement.Columns.Add("BANK_NAME", typeof(string));
            dtReportStatement.Columns.Add("BRANCH_NAME", typeof(string));
            dtReportStatement.Columns.Add("BRANCH_ADDRESS", typeof(string));
            dtReportStatement.Columns.Add("CERT_NO", typeof(string));
            dtReportStatement.Columns.Add("QTY", typeof(int));
            dtReportStatement.Columns.Add("RATE", typeof(decimal));
            dtReportStatement.Columns.Add("AMOUNT", typeof(decimal));
            dtReportStatement.Columns.Add("CIP", typeof(string));
            dtReportStatement.Columns.Add("ID_AC", typeof(string));
            dtReportStatement.Columns.Add("SL_TYPE", typeof(string));
            dtReportStatement.Columns.Add("TIN", typeof(string));
            dtReportStatement.Columns.Add("BO", typeof(string));
            dtReportStatement.Columns.Add("NID", typeof(string));
            dtReportStatement.Columns.Add("MONY_RECT_NO", typeof(string));
            dtReportStatement.Columns.Add("BEFTN", typeof(string));


            dtReportStatement.Columns.Add("CERT_RCV_BY", typeof(string));
            dtReportStatement.Columns.Add("CERT_DLVRY_DT", typeof(string));

            dtReportStatement.Columns.Add("DRF_REF_NO", typeof(long));
            dtReportStatement.Columns.Add("DRF_REG_FOLIO_NO", typeof(string));
            dtReportStatement.Columns.Add("DRF_CERT_NO", typeof(string));

            dtReportStatement.Columns.Add("DRF_DISTNCT_NO_FROM", typeof(long));
            dtReportStatement.Columns.Add("DRF_DISTNCT_NO_TO", typeof(long));
            dtReportStatement.Columns.Add("DRF_CUST_REQ_DATE", typeof(string));

            dtReportStatement.Columns.Add("DRF_ACCEPT_NO", typeof(long));
            dtReportStatement.Columns.Add("DRF_DRN", typeof(long));
            dtReportStatement.Columns.Add("DRF_ACCEPT_DATE", typeof(string));

            dtReportStatement.Columns.Add("DRF_TR_SEQ_NO", typeof(long));
            dtReportStatement.Columns.Add("DRF_TR_DATE", typeof(string));
           

            dtReportStatement.Columns.Add("REP_NO", typeof(int));
            dtReportStatement.Columns.Add("REP_DT", typeof(string));
            dtReportStatement.Columns.Add("PAY_TYPE", typeof(string));
            dtReportStatement.Columns.Add("SL_TR_REP_NO", typeof(string));
            dtReportStatement.Columns.Add("OLD_SL_TR_NO", typeof(string));


            dtReportStatement.Columns.Add("TR_NO", typeof(int));
            dtReportStatement.Columns.Add("TR_DT", typeof(string));
            dtReportStatement.Columns.Add("TFEREE_REG_NO", typeof(string));
            dtReportStatement.Columns.Add("TFEREE_NAME", typeof(string));
            dtReportStatement.Columns.Add("TFEREE_JNT_NAME", typeof(string));
            dtReportStatement.Columns.Add("TFEREE_ADDRS1", typeof(string));
            dtReportStatement.Columns.Add("TFEREE_ADDRS2", typeof(string));
            dtReportStatement.Columns.Add("TFEREE_CITY", typeof(string));

            dtReportStatement.Columns.Add("FRAC_AMT", typeof(decimal));
            dtReportStatement.Columns.Add("SIP_NO", typeof(int));

            return dtReportStatement;






        }
        public string getTotalCertNo(string queryString, string fundCode)
        {
            string totalCert = "";          
            string certTrailer = "";
            int count = 0;
            DataTable dtCert = new DataTable();

            dtCert = commonGatewayObj.Select(queryString.ToString());

            for (int looper = 0; looper < dtCert.Rows.Count; looper++)
            {
                if (totalCert == "")
                {
                    totalCert = dtCert.Rows[looper]["CERT_TYPE"].ToString() + dtCert.Rows[looper]["CERT_NO"].ToString();
                   
                }
                else
                {
                    if (dtCert.Rows[looper]["CERT_TYPE"].ToString().ToUpper() == dtCert.Rows[looper - 1]["CERT_TYPE"].ToString().ToUpper())
                    {
                        if (Convert.ToInt32(dtCert.Rows[looper]["CERT_NO"].ToString()) - 1 == Convert.ToInt32(dtCert.Rows[looper - 1]["CERT_NO"].ToString()))
                        {
                            count++;
                            certTrailer = "-" + dtCert.Rows[looper]["CERT_NO"].ToString();
                        }
                        else
                        {
                            if (count > 0)
                            {
                                totalCert = totalCert+ certTrailer;
                                count = 0;
                                certTrailer = "";
                            }
                            totalCert = totalCert + "," + dtCert.Rows[looper]["CERT_TYPE"].ToString() + dtCert.Rows[looper]["CERT_NO"].ToString();
                        }
                      
                    }
                    else
                    {
                        if (count > 0)
                        {
                            totalCert = totalCert + certTrailer;
                            count = 0;
                            certTrailer = "";
                        }
                        totalCert = totalCert + "," + dtCert.Rows[looper]["CERT_TYPE"].ToString() + dtCert.Rows[looper]["CERT_NO"].ToString();
                    }
                   
                }
                if (looper == dtCert.Rows.Count - 1)
                {
                    if (count > 0)
                    {
                        totalCert = totalCert + certTrailer;
                        count = 0;
                        certTrailer = "";
                    }
                }
            }
            return totalCert;
        }
        public DataTable GetDtLedgerTable()
        {
            DataTable dtLedger = new DataTable();
            dtLedger.Columns.Add("SI", typeof(int));
            dtLedger.Columns.Add("TRANS_DATE", typeof(string));
            dtLedger.Columns.Add("TRANS_TYPE", typeof(string));
            dtLedger.Columns.Add("TRANS_NO", typeof(int));
            dtLedger.Columns.Add("RATE", typeof(decimal));
            dtLedger.Columns.Add("UNIT_CREDIT", typeof(int));
            dtLedger.Columns.Add("UNIT_DEBIT", typeof(int));
            dtLedger.Columns.Add("BALANCE", typeof(int));
            dtLedger.Columns.Add("FRAC_AMT", typeof(string));
            return dtLedger;

        }
        public DataTable GetLedgerData(UnitHolderRegistration regObj)
        {
            StringBuilder sbQueryString = new StringBuilder();
            DataTable dtLedgerData = new DataTable();
            sbQueryString.Append(" SELECT TRANS_DATE, TRANS_TYPE, TRANS_NO, RATE, QTY ,TO_CHAR(FRAC_AMT ) AS FRAC_AMT FROM ");
            sbQueryString.Append(" (SELECT SL_DT AS TRANS_DATE, NVL(SL_TYPE,'SL') AS TRANS_TYPE, SL_NO AS TRANS_NO, SL_PRICE AS RATE, QTY,TO_CHAR(FRAC_AMT ) AS FRAC_AMT FROM  SALE ");
            sbQueryString.Append(" WHERE (REG_BR = '" + regObj.BranchCode.ToString().ToUpper() + "') AND (REG_BK = '" + regObj.FundCode.ToString().ToUpper() + "') AND (REG_NO =" + Convert.ToInt32(regObj.RegNumber.ToString()) + ")  UNION ALL");

            sbQueryString.Append(" SELECT TR_DT AS TRANS_DATE, 'TRI' AS TRANS_TYPE, TR_NO AS TRANS_NO, NULL AS RATE, SUM(QTY) AS QTY, ' ' AS FRAC_AMT   FROM  TRANSFER WHERE TR_TYPE='T' ");
            sbQueryString.Append(" AND (REG_BR_I = '" + regObj.BranchCode.ToString().ToUpper() + "') AND (REG_BK_I = '" + regObj.FundCode.ToString().ToUpper() + "') AND (REG_NO_I = " + Convert.ToInt32(regObj.RegNumber.ToString()) + ")  GROUP BY TR_NO, TR_DT  UNION ALL");
            sbQueryString.Append(" SELECT TR_DT AS TRANS_DATE, 'TRO' AS TRANS_TYPE, TR_NO AS TRANS_NO, NULL AS RATE, SUM(QTY) AS QTY, ' ' AS FRAC_AMT    FROM  TRANSFER TRANSFER_1 WHERE TR_TYPE='T' ");
            sbQueryString.Append(" AND (REG_BR_O = '" + regObj.BranchCode.ToString().ToUpper() + "') AND (REG_BK_O = '" + regObj.FundCode.ToString().ToUpper() + "') AND (REG_NO_O = " + Convert.ToInt32(regObj.RegNumber.ToString()) + ")  GROUP BY TR_NO, TR_DT  UNION ALL");

            sbQueryString.Append(" SELECT TR_DT AS TRANS_DATE, 'DTI' AS TRANS_TYPE, TR_NO AS TRANS_NO, NULL AS RATE, SUM(QTY) AS QTY, ' ' AS FRAC_AMT  FROM  TRANSFER  TRANSFER_2 WHERE TR_TYPE='D' ");
            sbQueryString.Append(" AND (REG_BR_I = '" + regObj.BranchCode.ToString().ToUpper() + "') AND (REG_BK_I = '" + regObj.FundCode.ToString().ToUpper() + "') AND (REG_NO_I = " + Convert.ToInt32(regObj.RegNumber.ToString()) + ")  GROUP BY TR_NO, TR_DT  UNION ALL");
            sbQueryString.Append(" SELECT TR_DT AS TRANS_DATE, 'DTO' AS TRANS_TYPE, TR_NO AS TRANS_NO, NULL AS RATE, SUM(QTY) AS QTY, ' ' AS FRAC_AMT    FROM  TRANSFER TRANSFER_3 WHERE TR_TYPE='D' ");
            sbQueryString.Append(" AND  (REG_BR_O = '" + regObj.BranchCode.ToString().ToUpper() + "') AND (REG_BK_O = '" + regObj.FundCode.ToString().ToUpper() + "') AND (REG_NO_O = " + Convert.ToInt32(regObj.RegNumber.ToString()) + ") GROUP BY TR_NO, TR_DT  UNION ALL");

            sbQueryString.Append(" SELECT REP_DT AS TRANS_DATE, 'REP' AS TRANS_TYPE, REP_NO AS TRANS_NO, REP_PRICE AS RATE, SUM(QTY) AS QTY,' ' AS FRAC_AMT  FROM   REPURCHASE");
            sbQueryString.Append(" WHERE (REG_BR = '" + regObj.BranchCode.ToString().ToUpper() + "') AND (REG_BK = '" + regObj.FundCode.ToString().ToUpper() + "') AND (REG_NO = " + Convert.ToInt32(regObj.RegNumber.ToString()) + ") GROUP BY REP_NO, REP_DT, REP_PRICE) LEDGER");
            sbQueryString.Append(" ORDER BY TRANS_DATE");


            dtLedgerData = commonGatewayObj.Select(sbQueryString.ToString());
          
            return dtLedgerData;
        }
        public DataTable getDtFY(string fundCode)
        {
            DataTable dtFY = commonGatewayObj.Select("SELECT F_YEAR ,DIVI_NO FROM DIVI_PARA WHERE FUND_CD='" + fundCode.ToString().ToUpper() + "' ORDER BY DIVI_NO DESC ");
            return dtFY;
        }
        public DataTable getDtFYPart(string fundCode)
        {
            DataTable dtFYPart = commonGatewayObj.Select("SELECT DISTINCT FY_PART FROM DIVI_PARA WHERE FUND_CD='" + fundCode.ToString().ToUpper() + "' ORDER BY FY_PART DESC ");
            return dtFYPart;

        }
        public DataTable getDtSurrenderDateRegWise(string fundCode, int regNo, string branchCode)
        {
            DataTable dtSurrenderDate = commonGatewayObj.Select(" SELECT DISTINCT REP_NO ,TO_CHAR(REP_DT ,'DD-MON-YYYY')AS REP_DT FROM REPURCHASE WHERE REG_BK='" + fundCode.ToString() + "' AND REG_BR='" + branchCode.ToString() + "' AND REG_NO=" + regNo + "");
            return dtSurrenderDate;
        }
        public DataTable getTableForSurrenderTaxCert()
        {
            DataTable dtSurrenderTaxCert = new DataTable();

            dtSurrenderTaxCert.Columns.Add("TRANS_DATE", typeof(string));
            dtSurrenderTaxCert.Columns.Add("REG_NO", typeof(string));
            dtSurrenderTaxCert.Columns.Add("SALE_UNIT", typeof(int));
            dtSurrenderTaxCert.Columns.Add("REP_UNIT", typeof(int));
            dtSurrenderTaxCert.Columns.Add("TRI_UNIT", typeof(int));
            dtSurrenderTaxCert.Columns.Add("TRO_UNIT", typeof(int));
            dtSurrenderTaxCert.Columns.Add("RATE", typeof(decimal));
            dtSurrenderTaxCert.Columns.Add("SALE_AMOUNT", typeof(decimal));
            dtSurrenderTaxCert.Columns.Add("REP_AMOUNT", typeof(decimal));
            dtSurrenderTaxCert.Columns.Add("CAPITAL_GAIN", typeof(decimal));
            return dtSurrenderTaxCert;
        }
        public DataTable getTableForSurrender()
        {
            DataTable dtTableForSurrender = new DataTable();
            dtTableForSurrender.Columns.Add("REP_DATE", typeof(string));
            dtTableForSurrender.Columns.Add("REP_UNIT", typeof(int));
            dtTableForSurrender.Columns.Add("RATE", typeof(decimal));
            dtTableForSurrender.Columns.Add("AMOUNT", typeof(decimal));
            return dtTableForSurrender;
        }
        public decimal getCostAmount(UnitHolderRegistration regObj,string rep_no)
        {
            decimal costAmount = 0;
            decimal totalCostAmount = 0;
            DataTable dtSurrender = commonGatewayObj.Select(" SELECT * FROM REPURCHASE WHERE REG_BK='" + regObj.FundCode.ToString() + "' AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO=" + regObj.RegNumber + " AND REP_NO='"+ rep_no + "'");
            if (dtSurrender.Rows.Count > 0)
            {
                for (int loop = 0; loop < dtSurrender.Rows.Count; loop++)
                {
                    if (dtSurrender.Rows[loop]["SL_TR_NO"].ToString().ToUpper().IndexOf('S') >= 0)
                    {
                        string[] saleNoArray = dtSurrender.Rows[loop]["SL_TR_NO"].ToString().Split('S');
                        int SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        DataTable dtSaleRate = commonGatewayObj.Select("SELECT * FROM SALE WHERE REG_BK='" + regObj.FundCode.ToString() + "' AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO=" + regObj.RegNumber + " AND SL_NO=" + SL_TR_RN);
                        decimal saleRate = dtSaleRate.Rows[0]["SL_PRICE"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(dtSaleRate.Rows[0]["SL_PRICE"].ToString());
                        costAmount = saleRate *Convert.ToDecimal(dtSurrender.Rows[loop]["QTY"]);
                        totalCostAmount = totalCostAmount + costAmount;
                    }
                }
            }
             return totalCostAmount;
        }
        public DataTable getDtUnitPosition()
        {
            DataTable dtUnitPosition = new DataTable();
            dtUnitPosition.Columns.Add("SI", typeof(int));
            dtUnitPosition.Columns.Add("TRANS_TYPE", typeof(string));
            dtUnitPosition.Columns.Add("TOTAL_UNIT", typeof(int));
            dtUnitPosition.Columns.Add("TOTAL_UNIT", typeof(int));
            dtUnitPosition.Columns.Add("TOTAL_AMT", typeof(int));
            dtUnitPosition.Columns.Add("TOTAL_HOLD", typeof(int));
            return dtUnitPosition;
            
        }
        public DataTable dtFillBranchGroup()
        {
            DataTable dtBranchList = commonGatewayObj.Select("SELECT DISTINCT BR_SHORT_NM FROM BRANCH_INFO ");
            DataTable dtBranchGroupDropDown = new DataTable();
            dtBranchGroupDropDown.Columns.Add("ID", typeof(int));
            dtBranchGroupDropDown.Columns.Add("BR_SHORT_NM", typeof(string));

            DataRow drGroupDropDown = dtBranchGroupDropDown.NewRow();
            drGroupDropDown["BR_SHORT_NM"] = "--Select Group--- ";
            drGroupDropDown["ID"] = 0;
            dtBranchGroupDropDown.Rows.Add(drGroupDropDown);

            for (int loop = 0; loop < dtBranchList.Rows.Count; loop++)
            {
                drGroupDropDown = dtBranchGroupDropDown.NewRow();
                drGroupDropDown["BR_SHORT_NM"] = dtBranchList.Rows[loop]["BR_SHORT_NM"].ToString();
                drGroupDropDown["ID"] = loop + 1;
                dtBranchGroupDropDown.Rows.Add(drGroupDropDown);
            }

            return dtBranchGroupDropDown;
        }
        public DataTable dtFillSignatory()
        {
            DataTable dtSignatoryName = commonGatewayObj.Select(" SELECT ID,NAME FROM INVEST.EMP_INFO WHERE RANK<'E' AND VALID='Y' ORDER BY RANK,ID");
            DataTable dtSignatory = new DataTable();
            dtSignatory.Columns.Add("ID", typeof(string));
            dtSignatory.Columns.Add("NAME", typeof(string));

            DataRow drSignatory = dtSignatory.NewRow();
            drSignatory["NAME"] = "--Select Signatory--- ";
            drSignatory["ID"] = "0";
            dtSignatory.Rows.Add(drSignatory);

            dtSignatoryName.Merge(dtSignatory);
            return dtSignatoryName;
             
           
        }
        public DataTable getDtCertDeliveryInfo()
        {
            DataTable dtCertDeliveryInfo = new DataTable();
            
            dtCertDeliveryInfo.Columns.Add("REG_No", typeof(int));
            dtCertDeliveryInfo.Columns.Add("TRANS_TYPE",typeof(string));
            dtCertDeliveryInfo.Columns.Add("SL_TR_RN_NO", typeof(string));
            dtCertDeliveryInfo.Columns.Add("CERTIFIACTE", typeof(string));
            dtCertDeliveryInfo.Columns.Add("CABI_NO", typeof(string));
            dtCertDeliveryInfo.Columns.Add("LOCK_NO", typeof(string));
            dtCertDeliveryInfo.Columns.Add("DELVERY_DT", typeof(string));
            dtCertDeliveryInfo.Columns.Add("RECEIVED_BY", typeof(string));
            dtCertDeliveryInfo.Columns.Add("DELVERY_BY", typeof(string));
            return dtCertDeliveryInfo;
        }
        public string getCIPCertificateForPrint(string fundCode, string branchCode, int saleNo)
        {
            string certificate = "";
            DataTable dtCertificate = commonGatewayObj.Select("SELECT CERTIFICATE FROM SALE_CERT WHERE  REG_BK='" + fundCode + "' AND REG_BR='" + branchCode + "' AND SL_NO=" + saleNo);
            if(dtCertificate.Rows.Count>0)
            {
                certificate = dtCertificate.Rows[0]["CERTIFICATE"].ToString().ToUpper();
            }
            return certificate;
        }
        public DataTable dtFundCodeList()
        {
            DataTable dtList = commonGatewayObj.Select("SELECT FUND_CD AS NAME ,FUND_CD AS ID FROM FUND_INFO ORDER BY FUND_CD");
            return dtList;
        }
        public DataTable dtBranchCodeList()
        {
            DataTable dtList = commonGatewayObj.Select("SELECT BR_CD AS NAME ,BR_CD AS ID FROM BRANCH_INFO ORDER BY BR_CD");
            return dtList;
        }
        public DataTable dtFundInfoDetails( string filter)
        {
            DataTable dtFundInfoDetails = commonGatewayObj.Select("SELECT * FROM FUND_INFO WHERE 1=1  " + filter.ToString()+ "  ORDER BY FUND_SUB_OPEN_DT ");
            return dtFundInfoDetails;
        }
        public DataTable dtPriceDetails(string filter)
        {
            DataTable dtFundInfoDetails = commonGatewayObj.Select("SELECT PRICE_REFIX.*, TO_CHAR(PRICE_REFIX.REFIX_DT,'DD-MON-YYYY') AS REFIX_DATE,TO_CHAR(PRICE_REFIX.EFFECTIVE_DATE,'DD-MON-YYYY') AS EFFECTIVE_DT  FROM PRICE_REFIX WHERE 1=1  " + filter.ToString() + "  ORDER BY FUND_CD ");
            return dtFundInfoDetails;
        }
        public DataTable dtPriceDetailsWithNAV(string filter)
        {
            DataTable dtFundInfoDetails = commonGatewayObj.Select(" SELECT * FROM NAV.NAV_MASTER WHERE 1=1 "+filter.ToString());
            return dtFundInfoDetails;
        }
        public bool CheckDuplicate(string refixationDate)
        {
            DataTable dtDuplicate = commonGatewayObj.Select("SELECT * FROM PRICE_REFIX WHERE  REFIX_DT='" + refixationDate +"'");
            if (dtDuplicate.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable dtFillFixationDate(string filter)
        {
            DataTable dtFillFixationDate = commonGatewayObj.Select(" SELECT DISTINCT REFIX_DT AS ID ,TO_CHAR(REFIX_DT,'DD-MON-YYYY') AS NAME FROM PRICE_REFIX WHERE 1=1 " + filter.ToString() + " ORDER BY REFIX_DT DESC");
            DataTable dtDate = new DataTable();
            dtDate.Columns.Add("ID", typeof(string));
            dtDate.Columns.Add("NAME", typeof(string));

            DataRow drDate = dtDate.NewRow();
            drDate["NAME"] = "--Select Date--- ";
            drDate["ID"] = "0";
            dtDate.Rows.Add(drDate);


            for (int loop = 0; loop < dtFillFixationDate.Rows.Count; loop++)
            {
                drDate = dtDate.NewRow();
                drDate["NAME"] = dtFillFixationDate.Rows[loop]["NAME"].ToString();
                drDate["ID"] = dtFillFixationDate.Rows[loop]["ID"].ToString();
                dtDate.Rows.Add(drDate);
            }

           
            return dtDate;


        }
        public DataTable dtFillEffectiveDate(string filter)
        {
            DataTable dtFillEffectiveDate = commonGatewayObj.Select(" SELECT DISTINCT REFIX_DT AS ID ,TO_CHAR(REFIX_DT,'DD-MON-YYYY') AS NAME FROM PRICE_REFIX WHERE 1=1 " + filter.ToString() + " ORDER BY REFIX_DT DESC");
            DataTable dtDate = new DataTable();
            dtDate.Columns.Add("ID", typeof(string));
            dtDate.Columns.Add("NAME", typeof(string));

            DataRow drDate = dtDate.NewRow();
            drDate["NAME"] = "--Select Date--- ";
            drDate["ID"] = "0";
            dtDate.Rows.Add(drDate);


            for (int loop = 0; loop < dtFillEffectiveDate.Rows.Count; loop++)
            {
                drDate = dtDate.NewRow();
                drDate["NAME"] = dtFillEffectiveDate.Rows[loop]["NAME"].ToString();
                drDate["ID"] = dtFillEffectiveDate.Rows[loop]["ID"].ToString();
                dtDate.Rows.Add(drDate);
            }


            return dtDate;


        }

    }
}