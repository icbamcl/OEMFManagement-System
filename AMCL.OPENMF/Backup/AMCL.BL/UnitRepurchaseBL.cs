using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.OracleClient;
using System.Text;
using System.Collections;
using AMCL.COMMON;
using AMCL.DL;
using AMCL.GATEWAY;

/// <summary>
/// Summary description for UnitTransferBL
/// </summary>
namespace AMCL.BL
{
    public class UnitRepurchaseBL
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        OMFDAO opendMFDAO = new OMFDAO();
        //UnitSaleBL unitSaleBLObj = new UnitSaleBL();
       
        public UnitRepurchaseBL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public bool IsDuplicateRepurchase(UnitHolderRegistration regObj, UnitRepurchase repObj)
        {
            DataTable dtRepurchase = new DataTable();         
            dtRepurchase = commonGatewayObj.Select("SELECT * FROM REPURCHASE WHERE  REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REP_NO=" + Convert.ToInt32(repObj.RepurchaseNo.ToString()));            
            if (dtRepurchase.Rows.Count > 0)
                return true;
            else
                return false;

        }
        public int getNextRepurchaseNo(UnitHolderRegistration regObj, UnitUser unitUserObj)
        {
            int nextRepurchaseNo = 0;
            DataTable dtNextRepurchaseNo = new DataTable();

            dtNextRepurchaseNo = commonGatewayObj.Select("SELECT MAX(REP_NO) AS REP_NO FROM REPURCHASE WHERE  REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND USER_NM='" + unitUserObj.UserID.ToString() + "'");

            if (!dtNextRepurchaseNo.Rows[0]["REP_NO"].Equals(DBNull.Value))
            {
                nextRepurchaseNo = Convert.ToInt16(dtNextRepurchaseNo.Rows[0]["REP_NO"].ToString());
            }
            else
            {
                dtNextRepurchaseNo = new DataTable();
                dtNextRepurchaseNo = commonGatewayObj.Select("SELECT MAX(REP_NO) AS REP_NO FROM REPURCHASE WHERE  REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'");
                nextRepurchaseNo = dtNextRepurchaseNo.Rows[0]["REP_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt16(dtNextRepurchaseNo.Rows[0]["REP_NO"].ToString());
            }
            return nextRepurchaseNo + 1;

        }
        public int getMaxRepurchaseNo(UnitHolderRegistration regObj)
        {
            int maxRepNo = 0;
            DataTable dtMaxRepNo = commonGatewayObj.Select("SELECT MAX(REP_NO) AS MAX_REP_NO FROM REPURCHASE WHERE REG_BK ='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'");
            if (dtMaxRepNo.Rows.Count > 0)
            {
                maxRepNo = Convert.ToInt32(dtMaxRepNo.Rows[0]["MAX_REP_NO"].Equals(DBNull.Value) ? "0" : dtMaxRepNo.Rows[0]["MAX_REP_NO"].ToString());
            }
            return maxRepNo + 1;

        }
        public void saveRepurchase(DataTable dtTransferSaleCert, UnitHolderRegistration regObj, UnitRepurchase repObj, UnitUser unitUserObj)
        {
            try
            {
                UnitTransferBL unitTransferBLObj = new UnitTransferBL();
                commonGatewayObj.BeginTransaction();
                Hashtable htRepurchaseRegInfo = new Hashtable();
                Hashtable htCertNoTemp = new Hashtable();
                int SL_TR_RN = 0;
                int CertNo = 0;
                string certType = "";
                string[] saleNoArray;
                string[] certArray;
                string statusFlag = "R";
                string Old_SL_TR_RN = "";

                for (int loop = 0; loop < dtTransferSaleCert.Rows.Count; loop++)
                {


                    if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('S') >= 0)
                    {
                        htRepurchaseRegInfo = new Hashtable();
                        htRepurchaseRegInfo.Add("CURR_REG_BK", regObj.FundCode.ToString().ToUpper());
                        htRepurchaseRegInfo.Add("CURR_REG_BR", regObj.BranchCode.ToString().ToUpper());
                        htRepurchaseRegInfo.Add("CURR_REG_NO", regObj.RegNumber.ToString());
                        htRepurchaseRegInfo.Add("STATUS_FLAG", statusFlag.ToUpper().ToString());

                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('S');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();
                        commonGatewayObj.Update(htRepurchaseRegInfo, "SALE_CERT", "REG_NO=" + Convert.ToInt32(regObj.RegNumber) + " AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND  SL_NO=" + SL_TR_RN + " AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "S" + SL_TR_RN.ToString();
                    }
                    else if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('T') >= 0)
                    {
                        htRepurchaseRegInfo = new Hashtable();
                        htRepurchaseRegInfo.Add("CURR_REG_BK", regObj.FundCode.ToString().ToUpper());
                        htRepurchaseRegInfo.Add("CURR_REG_BR", regObj.BranchCode.ToString().ToUpper());
                        htRepurchaseRegInfo.Add("CURR_REG_NO", regObj.RegNumber.ToString());
                        htRepurchaseRegInfo.Add("STATUS_FLAG", statusFlag.ToUpper().ToString());


                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('T');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();


                        commonGatewayObj.Update(htRepurchaseRegInfo, "TRANS_CERT", "F_CD='" + regObj.FundCode.ToString().ToUpper() + "'  AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");
                        commonGatewayObj.Update(htRepurchaseRegInfo, "SALE_CERT", "REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");
                        commonGatewayObj.Update(htRepurchaseRegInfo, "RENEWAL_OUT", "REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "T" + SL_TR_RN.ToString();

                    }
                    else if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('R') >= 0)
                    {
                        htRepurchaseRegInfo = new Hashtable();
                        htRepurchaseRegInfo.Add("CURR_REG_BK", regObj.FundCode.ToString().ToUpper());
                        htRepurchaseRegInfo.Add("CURR_REG_BR", regObj.BranchCode.ToString().ToUpper());
                        htRepurchaseRegInfo.Add("CURR_REG_NO", regObj.RegNumber.ToString());
                        htRepurchaseRegInfo.Add("STATUS_FLAG", statusFlag.ToUpper().ToString());


                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('R');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();

                        commonGatewayObj.Update(htRepurchaseRegInfo, "RENEWAL_OUT", "REG_NO=" + Convert.ToInt32(regObj.RegNumber) + " AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "R" + SL_TR_RN.ToString();
                    }


                  

                    htRepurchaseRegInfo = new Hashtable();
                    htRepurchaseRegInfo.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htRepurchaseRegInfo.Add("REG_BR", regObj.BranchCode.ToString().ToUpper());
                    htRepurchaseRegInfo.Add("REP_NO", Convert.ToInt32(repObj.RepurchaseNo.ToString()));
                    htRepurchaseRegInfo.Add("REG_NO", Convert.ToInt32(regObj.RegNumber.ToString()));

                    certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');

                    htRepurchaseRegInfo.Add("CERT_TYPE", certArray[0].ToString().ToUpper());
                    htRepurchaseRegInfo.Add("CERT_NO", Convert.ToInt32(certArray[1].ToString()));
                    htRepurchaseRegInfo.Add("CERTIFICATE", dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().ToUpper());
                    htRepurchaseRegInfo.Add("QTY", Convert.ToInt32(dtTransferSaleCert.Rows[loop]["QTY"].ToString()));
                    htRepurchaseRegInfo.Add("SL_TR_NO", Old_SL_TR_RN.ToString().ToUpper());

                    commonGatewayObj.Insert(htRepurchaseRegInfo, "REP_CERT_NO");
                }

               



                DataTable dtTransfer = unitTransferBLObj.dtTrnasfer(dtTransferSaleCert);
                for (int i = 0; i < dtTransfer.Rows.Count; i++)
                {                   

                    htRepurchaseRegInfo = new Hashtable();
                    htRepurchaseRegInfo.Add("REP_NO", Convert.ToInt32(repObj.RepurchaseNo.ToString()));
                    htRepurchaseRegInfo.Add("REP_DT", Convert.ToDateTime(repObj.RepurchaseDate.ToString()).ToString("dd-MMM-yyyy"));
                    htRepurchaseRegInfo.Add("SL_TR_NO", dtTransfer.Rows[i]["SL_NO"].ToString());
                    htRepurchaseRegInfo.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htRepurchaseRegInfo.Add("REG_BR", regObj.BranchCode.ToString().ToUpper());
                    htRepurchaseRegInfo.Add("REG_NO", regObj.RegNumber.ToString());
                    htRepurchaseRegInfo.Add("REP_PRICE", decimal.Parse(repObj.RepurchaseRate.ToString()));
                    htRepurchaseRegInfo.Add("QTY", Convert.ToInt32(dtTransfer.Rows[i]["QTY"].ToString()));
                    htRepurchaseRegInfo.Add("PAY_TYPE", repObj.PayType.ToString().ToUpper());
                    htRepurchaseRegInfo.Add("USER_NM", unitUserObj.UserID.ToString());
                    htRepurchaseRegInfo.Add("ENT_DT", DateTime.Now.ToString());
                    htRepurchaseRegInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());

                    if (repObj.ChequeIssueTo== null)
                    {
                        htRepurchaseRegInfo.Add("CHQ_ISSUE_TO", DBNull.Value);
                    }
                    else
                    {
                        htRepurchaseRegInfo.Add("CHQ_ISSUE_TO", repObj.ChequeIssueTo.ToString());
                    }

                    commonGatewayObj.Insert(htRepurchaseRegInfo, "REPURCHASE");
                }
                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }
        }
        public void saveRepurchaseCDS(DataTable dtSaleTrInfo, UnitHolderRegistration regObj, UnitRepurchase repObj, UnitUser unitUserObj)
        {
            try
            {
               
                commonGatewayObj.BeginTransaction();
                Hashtable htRepurchaseRegInfo = new Hashtable();
                
                StringBuilder sbQuery = new StringBuilder();
                string[] saleNoArray;
                for (int loop = 0; loop < dtSaleTrInfo.Rows.Count; loop++)
                {
                    if (dtSaleTrInfo.Rows[loop]["SL_TR_NO"].ToString().ToUpper().IndexOf('S') >= 0)
                    {

                        sbQuery = new StringBuilder();
                      
                        saleNoArray = dtSaleTrInfo.Rows[loop]["SL_TR_NO"].ToString().Split('S');
                        sbQuery.Append("UPDATE SALE SET QTY_OUT=NVL(QTY_OUT,0)+" + Convert.ToInt64(dtSaleTrInfo.Rows[loop]["SURRENDER_UNITS"].ToString()));
                        sbQuery.Append(" WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'");
                        sbQuery.Append(" AND REG_NO=" + Convert.ToInt32(regObj.RegNumber.ToString()) + " AND SL_NO=" + Convert.ToInt32(saleNoArray[1].ToString()));

                        commonGatewayObj.ExecuteNonQuery(sbQuery.ToString());

                        htRepurchaseRegInfo = new Hashtable();
                        htRepurchaseRegInfo.Add("REP_NO",repObj.RepurchaseNo);
                        htRepurchaseRegInfo.Add("REP_DT", repObj.RepurchaseDate);
                        htRepurchaseRegInfo.Add("SL_TR_NO", dtSaleTrInfo.Rows[loop]["SL_TR_NO"]);
                        htRepurchaseRegInfo.Add("REG_BK",  regObj.FundCode.ToString().ToUpper());
                        htRepurchaseRegInfo.Add("REG_BR",regObj.BranchCode.ToString().ToUpper());
                        htRepurchaseRegInfo.Add("REG_NO", regObj.RegNumber.ToString());
                        htRepurchaseRegInfo.Add("REP_PRICE", repObj.RepurchaseRate);
                        htRepurchaseRegInfo.Add("QTY", dtSaleTrInfo.Rows[loop]["SURRENDER_UNITS"]);
                        htRepurchaseRegInfo.Add("PAY_TYPE", repObj.PayType.ToString().ToUpper());
                        htRepurchaseRegInfo.Add("USER_NM", unitUserObj.UserID.ToString());
                        htRepurchaseRegInfo.Add("ENT_DT", DateTime.Now.ToString());
                        htRepurchaseRegInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());

                        commonGatewayObj.Insert(htRepurchaseRegInfo, "REPURCHASE");
                    }                   
                    else if (dtSaleTrInfo.Rows[loop]["SL_TR_NO"].ToString().ToUpper().IndexOf('T') >= 0)
                    {

                        sbQuery = new StringBuilder();
                        saleNoArray = dtSaleTrInfo.Rows[loop]["SL_TR_NO"].ToString().Split('T');
                        sbQuery.Append("UPDATE TRANSFER SET QTY_OUT=NVL(QTY_OUT,0)+" + Convert.ToInt64(dtSaleTrInfo.Rows[loop]["SURRENDER_UNITS"].ToString()));
                        sbQuery.Append(" WHERE F_CD='" + regObj.FundCode.ToString().ToUpper() + "' AND BR_CODE='" + regObj.FundCode.ToString() + "_" + regObj.BranchCode.ToString().ToUpper() + "'");
                        sbQuery.Append(" AND REG_NO_I=" + Convert.ToInt32(regObj.RegNumber.ToString()) + " AND TR_NO=" + Convert.ToInt32(saleNoArray[1].ToString()));

                        commonGatewayObj.ExecuteNonQuery(sbQuery.ToString());

                        htRepurchaseRegInfo = new Hashtable();
                        htRepurchaseRegInfo.Add("REP_NO", repObj.RepurchaseNo);
                        htRepurchaseRegInfo.Add("REP_DT", repObj.RepurchaseDate);
                        htRepurchaseRegInfo.Add("SL_TR_NO", dtSaleTrInfo.Rows[loop]["SL_TR_NO"]);
                        htRepurchaseRegInfo.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                        htRepurchaseRegInfo.Add("REG_BR", regObj.BranchCode.ToString().ToUpper());
                        htRepurchaseRegInfo.Add("REG_NO", regObj.RegNumber.ToString());
                        htRepurchaseRegInfo.Add("REP_PRICE", repObj.RepurchaseRate);
                        htRepurchaseRegInfo.Add("QTY", dtSaleTrInfo.Rows[loop]["SURRENDER_UNITS"]);
                        htRepurchaseRegInfo.Add("PAY_TYPE", repObj.PayType.ToString().ToUpper());
                        htRepurchaseRegInfo.Add("USER_NM", unitUserObj.UserID.ToString());
                        htRepurchaseRegInfo.Add("ENT_DT", DateTime.Now.ToString());
                        htRepurchaseRegInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());

                        commonGatewayObj.Insert(htRepurchaseRegInfo, "REPURCHASE");
                    }
                }
                                                  
                   

                  
                  
                
                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }
        }
        public DateTime getLastRepDate(UnitHolderRegistration regObj, UnitRepurchase repObj)
        {
            DateTime lastSaleDate;
            DataTable dtLastSaleDate = new DataTable();
            dtLastSaleDate = commonGatewayObj.Select("SELECT MAX(REP_DT) AS MAX_REP_DATE FROM REPURCHASE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'AND REP_NO=" + repObj.RepurchaseNo);
            if (dtLastSaleDate.Rows.Count > 0)
            {
                lastSaleDate = Convert.ToDateTime(dtLastSaleDate.Rows[0]["MAX_REP_DATE"].Equals(DBNull.Value) ? DateTime.Today.ToString() : dtLastSaleDate.Rows[0]["MAX_REP_DATE"].ToString());
            }
            else
            {
                lastSaleDate = DateTime.Today;
            }
            return lastSaleDate;

        }
        public decimal getLastRepRate(UnitHolderRegistration regObj, UnitRepurchase repObj)
        {
            decimal lastRepRate = 0;
            DataTable dtLastSaleRate = new DataTable();
            dtLastSaleRate = commonGatewayObj.Select("SELECT DISTINCT REP_PRICE AS MAX_REP_PRICE FROM REPURCHASE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND REP_DT IN (SELECT MAX(REP_DT) AS MAX_REP_DATE FROM REPURCHASE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND REP_NO =" + repObj.RepurchaseNo + ")");
            if (dtLastSaleRate.Rows.Count > 0)
            {
                lastRepRate = Convert.ToDecimal(dtLastSaleRate.Rows[0]["MAX_REP_PRICE"].ToString());
            }
            return lastRepRate;

        }
        public bool IsRepurchaseLock(UnitHolderRegistration regObj)
        {
            bool lockStatus = false;
            DataTable dtLockStatus = commonGatewayObj.Select("SELECT NVL(ALL_LOCK,'N') AS ALL_LOCK, NVL(REP_LOCK,'N') AS REP_LOCK FROM U_MASTER WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO=" + regObj.RegNumber);
            if (dtLockStatus.Rows.Count > 0)
            {
                if (dtLockStatus.Rows[0]["ALL_LOCK"].ToString() == "Y" || dtLockStatus.Rows[0]["REP_LOCK"].ToString() == "Y")
                {
                    lockStatus = true;
                }
            }
            return lockStatus;
        }
        public DataTable dtGetBEFTNData(string filter)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT A.VOUCHER_NO, FUND_INFO.FUND_NM,FUND_INFO.BANK_CODE, U_MASTER.HNAME,A.AUDITED_BY, A.REP_NO, A.REG_BK, A.REG_BR, A.REG_NO, A.REP_DT, A.REP_PRICE, A.QTY, A.TOTAL");
            sbQuery.Append(" FROM (SELECT VOUCHER_NO, AUDITED_BY,REP_NO, REG_BK, REG_BR, REG_NO, TO_CHAR(REP_DT, 'DD-MON-YYYY') AS REP_DT, REP_PRICE, SUM(QTY) AS QTY, SUM(REP_PRICE * QTY) AS TOTAL ");
            sbQuery.Append(" FROM REPURCHASE WHERE (PAY_TYPE = 'EFT') AND (BEFTN_DATE IS NULL)");
            sbQuery.Append(" GROUP BY VOUCHER_NO,AUDITED_BY, REP_NO, REG_BK, REG_BR, REG_NO, REP_DT, REP_PRICE) A ");
            sbQuery.Append(" INNER JOIN   U_MASTER ON A.REG_BK = U_MASTER.REG_BK AND A.REG_BR = U_MASTER.REG_BR AND A.REG_NO = U_MASTER.REG_NO INNER JOIN ");
            sbQuery.Append(" FUND_INFO ON A.REG_BK = FUND_INFO.FUND_CD  ");
            sbQuery.Append(" WHERE 1=1" + filter.ToString());
            sbQuery.Append(" ORDER BY A.REG_BK, A.REG_BR, A.REP_NO ");

          
            DataTable dtBEFTNData = commonGatewayObj.Select(sbQuery.ToString());
            return dtBEFTNData;
        }
        public DataTable dtBEFTNBankList()
        {
            DataTable dtBankList = commonGatewayObj.Select(" SELECT DISTINCT BANK_NAME.BANK_NAME, BANK_NAME.BANK_CODE FROM   FUND_INFO INNER JOIN  BANK_NAME ON FUND_INFO.BANK_CODE = BANK_NAME.BANK_CODE ");

            DataTable dtFundListDropDown = new DataTable();
            dtFundListDropDown.Columns.Add("BANK_CODE", typeof(string));
            dtFundListDropDown.Columns.Add("BANK_NAME", typeof(string));

            DataRow drFundListDropDown = dtFundListDropDown.NewRow();

            drFundListDropDown["BANK_NAME"] = " ";
            drFundListDropDown["BANK_CODE"] = "0";
            dtFundListDropDown.Rows.Add(drFundListDropDown);
            for (int loop = 0; loop < dtBankList.Rows.Count; loop++)
            {
                drFundListDropDown = dtFundListDropDown.NewRow();
                drFundListDropDown["BANK_NAME"] = dtBankList.Rows[loop]["BANK_NAME"].ToString();
                drFundListDropDown["BANK_CODE"] = dtBankList.Rows[loop]["BANK_CODE"].ToString();
                dtFundListDropDown.Rows.Add(drFundListDropDown);
            }
            return dtFundListDropDown;
        }
        public DataTable dtGetSigantoryList()
        {
            DataTable dtSignatory = commonGatewayObj.Select("SELECT ID, NAME FROM INVEST.EMP_INFO WHERE (RANK < 'F') AND (VALID = 'Y') ORDER BY RANK, SENIORITY");
            return dtSignatory;
        }
        public DataTable dtGetBEFTNDdataWithVoucherNo(string filter)
        {
            DataTable dtBEFTNData = dtGetBEFTNData(filter);
            DataTable dtBEFTNDataTableWithVoucherNo = dtBEFTNTableWithVoucherNo();
            if (dtBEFTNData.Rows.Count > 0)
            {
                DataRow drDataTable;
                for (int loop = 0; loop < dtBEFTNData.Rows.Count; loop++)
                {
                    drDataTable = dtBEFTNDataTableWithVoucherNo.NewRow();                   
                    drDataTable["VOUCHER_NO"] = getNexAccountVoucherNo(GetaccountSchema(dtBEFTNData.Rows[loop]["REG_BK"].ToString()));
                    drDataTable["FUND_NM"] = dtBEFTNData.Rows[loop]["FUND_NM"].ToString();
                    drDataTable["REG_BK"] = dtBEFTNData.Rows[loop]["REG_BK"].ToString();
                    drDataTable["REG_BR"] = dtBEFTNData.Rows[loop]["REG_BR"].ToString();
                    drDataTable["REG_NO"] = dtBEFTNData.Rows[loop]["REG_NO"].ToString();
                    drDataTable["REP_NO"] = dtBEFTNData.Rows[loop]["REP_NO"].ToString();
                    drDataTable["REP_DT"] = dtBEFTNData.Rows[loop]["REP_DT"].ToString();
                    drDataTable["HNAME"] = dtBEFTNData.Rows[loop]["HNAME"].ToString();
                    drDataTable["QTY"] = dtBEFTNData.Rows[loop]["QTY"].ToString();
                    drDataTable["REP_PRICE"] = dtBEFTNData.Rows[loop]["REP_PRICE"].ToString();
                    drDataTable["TOTAL"] = dtBEFTNData.Rows[loop]["TOTAL"].ToString();

                    dtBEFTNDataTableWithVoucherNo.Rows.Add(drDataTable);
                }
            }
            return dtBEFTNDataTableWithVoucherNo;

        }
        public DataTable dtBEFTNTableWithVoucherNo()
        {
            DataTable dtBEFTNDataTableWithVoucherNo = new DataTable();
            dtBEFTNDataTableWithVoucherNo.Columns.Add("VOUCHER_NO", typeof(string));
            dtBEFTNDataTableWithVoucherNo.Columns.Add("FUND_NM", typeof(string));
            dtBEFTNDataTableWithVoucherNo.Columns.Add("REG_BK", typeof(string));
            dtBEFTNDataTableWithVoucherNo.Columns.Add("REG_BR", typeof(string));
            dtBEFTNDataTableWithVoucherNo.Columns.Add("REG_NO", typeof(string));
            dtBEFTNDataTableWithVoucherNo.Columns.Add("REP_NO", typeof(string));
            dtBEFTNDataTableWithVoucherNo.Columns.Add("REP_DT", typeof(string));
            dtBEFTNDataTableWithVoucherNo.Columns.Add("HNAME", typeof(string));
            dtBEFTNDataTableWithVoucherNo.Columns.Add("QTY", typeof(string));
            dtBEFTNDataTableWithVoucherNo.Columns.Add("REP_PRICE", typeof(string));
            dtBEFTNDataTableWithVoucherNo.Columns.Add("TOTAL", typeof(string));
            return dtBEFTNDataTableWithVoucherNo;
        }
        public string GetaccountSchema(string fund_Code)
        {
            DataTable dtAccountSchema = commonGatewayObj.Select("SELECT ACCOUNT_SCHEMA FROM FUND_INFO WHERE FUND_CD='" + fund_Code + "'");
            string accountSchema = dtAccountSchema.Rows[0]["ACCOUNT_SCHEMA"].ToString().ToUpper();
            return accountSchema;
        }
        public string getNexAccountVoucherNo(string accountSchema)
        {
            DataTable dtAccountVoucherNo = commonGatewayObj.Select("SELECT VOUCHER_NO FROM " + accountSchema + ".GL_TRAN WHERE (CTRLNO=(SELECT MAX(TO_NUMBER(CTRLNO))MAX_CTRLNO FROM " + accountSchema + ".GL_TRAN GL_TRAN_1 WHERE (VOUCHER_TYPE='1'))) ");

            string voucherNo = dtAccountVoucherNo.Rows[0]["VOUCHER_NO"].ToString().ToUpper();
            string[] wordVoucher = voucherNo.Split('/');
            int accVoucherNo = Convert.ToInt32(wordVoucher[0].ToString()) ;
            return accVoucherNo.ToString();
            
        }
        public int getUnitFaceValue(string fund_Code)
        {
            DataTable dtUnitFaceValue = commonGatewayObj.Select("SELECT FC_VAL FROM FUND_INFO WHERE FUND_CD='" + fund_Code + "'");
            int unitFaceValue = Convert.ToInt16(dtUnitFaceValue.Rows[0]["FC_VAL"].ToString());
            return unitFaceValue;
        }
        public string getUnitBankPaymentCode(string fund_Code)
        {
            DataTable dtUnitBankPaymentCode = commonGatewayObj.Select("SELECT ACC_BANK_CODE FROM FUND_INFO WHERE FUND_CD='" + fund_Code + "'");
            string unitBankPaymentCode = dtUnitBankPaymentCode.Rows[0]["ACC_BANK_CODE"].ToString();
            return unitBankPaymentCode;
        }
        public string getUnitFundBankCode(string fund_Code)
        {
            DataTable dtUnitUnitFundBankCode = commonGatewayObj.Select("SELECT BANK_CODE FROM FUND_INFO WHERE FUND_CD='" + fund_Code + "'");
            string UnitUnitFundBankCode = dtUnitUnitFundBankCode.Rows[0]["BANK_CODE"].ToString();
            return UnitUnitFundBankCode;
        }
        public DataTable dtGetHolderBankInfo(string reg_bk, string reg_br, int reg_no)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT U_MASTER.BK_AC_NO, U_MASTER.BK_NM_CD, U_MASTER.BK_BR_NM_CD, BANK_NAME.BANK_NAME, BANK_BRANCH.BRANCH_NAME, BANK_BRANCH.BRANCH_ADDRS1, BANK_BRANCH.BRANCH_ADDRS2,");
            sbQuery.Append(" BANK_BRANCH.BRANCH_DISTRICT, BANK_BRANCH.ROUTING_NO ");
            sbQuery.Append(" FROM  BANK_BRANCH INNER JOIN  BANK_NAME ON BANK_BRANCH.BANK_CODE = BANK_NAME.BANK_CODE INNER JOIN");
            sbQuery.Append(" U_MASTER ON BANK_BRANCH.BANK_CODE = U_MASTER.BK_NM_CD AND BANK_BRANCH.BRANCH_CODE = U_MASTER.BK_BR_NM_CD ");
            sbQuery.Append(" WHERE (BANK_BRANCH.ROUTING_NO IS NOT NULL) AND U_MASTER.REG_BK='" + reg_bk + "' AND U_MASTER.REG_BR='" + reg_br + "' AND U_MASTER.REG_NO=" + reg_no);

            DataTable dtHolderBankInfo = commonGatewayObj.Select(sbQuery.ToString());
            return dtHolderBankInfo;
          
        }
        public DataTable dtBEFTNDateList(string fund_Code)
        {
            DataTable dtList = commonGatewayObj.Select(" SELECT DISTINCT TO_CHAR(BEFTN_DATE, 'DD-MON-YYYY') AS NAME,  TO_CHAR(BEFTN_DATE, 'DD-MON-YYYY') AS ID FROM REPURCHASE WHERE (VOUCHER_NO IS NOT NULL) AND BEFTN_DATE IS NOT NULL AND BEFTN_TRACKING_NO IS NULL AND REG_BK='" + fund_Code + "' ORDER BY 1 DESC ");

            DataTable dtListDropDown = new DataTable();
            dtListDropDown.Columns.Add("NAME", typeof(string));
            dtListDropDown.Columns.Add("ID", typeof(string));

            DataRow drListDropDown = dtListDropDown.NewRow();

            drListDropDown["NAME"] = " ";
            drListDropDown["ID"] = "0";
            dtListDropDown.Rows.Add(drListDropDown);
            for (int loop = 0; loop < dtList.Rows.Count; loop++)
            {
                drListDropDown = dtListDropDown.NewRow();
                drListDropDown["NAME"] = dtList.Rows[loop]["NAME"].ToString();
                drListDropDown["ID"] = dtList.Rows[loop]["ID"].ToString();
                dtListDropDown.Rows.Add(drListDropDown);
            }
            return dtListDropDown;
        }
        public DataTable dtGetBEFTNAdviceData(string filter)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT A.BANK_AC_TYPE, A.BANK_AC_NO AS FUND_ACC_NO, A.FUND_NM || '(- A/C #' || A.BANK_AC_NO || '- ROUTING # ' || A.BANK_ROUTING_NO || ')' AS ACCOUNT_INFO, A.FUND_NM, A.BANK_ROUTING_NO,BANK_NAME.BANK_NAME, B.REG_BK,B.REG_BR,B.REG_NO,B.REP_NO,");
            sbQuery.Append(" C.REG_BK || '/' || C.REG_BR || '/' || C.REG_NO || '/' || B.REP_NO AS REMARKS, B.VOUCHER_NO, B.BEFTN_DATE, C.HNAME,CASE  WHEN LENGTH( B.HOLDER_AC_NO)>13 THEN SUBSTR( B.HOLDER_AC_NO, - 13) ELSE LPAD( B.HOLDER_AC_NO,13,'0') END AS HOLDER_AC_NO, B.HOLDER_ROUTING_NO, B.TOTAL_AMOUNT, A.BANK_CODE");
            sbQuery.Append(" FROM FUND_INFO A INNER JOIN ( SELECT  BEFTN_TRACKING_NO,REG_BK,REG_BR,REG_NO,REP_NO, VOUCHER_NO, TO_CHAR(BEFTN_DATE, 'DD-MON-YYYY') AS BEFTN_DATE, HOLDER_AC_NO,");
            sbQuery.Append(" HOLDER_ROUTING_NO, SUM(REP_PRICE * QTY) AS TOTAL_AMOUNT FROM REPURCHASE WHERE  (PAY_TYPE = 'EFT') AND (BEFTN_DATE IS NOT NULL) AND (VOUCHER_NO IS NOT NULL)");
            sbQuery.Append(" GROUP BY BEFTN_TRACKING_NO,REG_BK,REG_BR,REG_NO,REP_NO, VOUCHER_NO, BEFTN_DATE, HOLDER_AC_NO, HOLDER_ROUTING_NO  ORDER BY BEFTN_DATE DESC, REPURCHASE.REG_BK, REPURCHASE.REG_BR, REPURCHASE.REP_NO )  B ON A.FUND_CD = B.REG_BK INNER JOIN ");
            sbQuery.Append(" U_MASTER C ON B.REG_BK = C.REG_BK AND B.REG_BR = C.REG_BR AND B.REG_NO = C.REG_NO  INNER JOIN  BANK_BRANCH ON  B.HOLDER_ROUTING_NO=BANK_BRANCH.ROUTING_NO  INNER JOIN BANK_NAME   ON BANK_NAME.BANK_CODE = BANK_BRANCH.BANK_CODE WHERE 1=1 ");
            sbQuery.Append( filter.ToString());
            sbQuery.Append(" ORDER BY B.REP_NO ");
      
            DataTable dtGetBEFTNAdviceData = commonGatewayObj.Select(sbQuery.ToString());
            return dtGetBEFTNAdviceData;
        }
        public DataTable dtGetBEFTNSignatoryBankInfo(string filter)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT  BANK_NAME.BANK_NAME, BANK_BRANCH.BRANCH_NAME, BANK_BRANCH.BRANCH_ADDRS1 BANK_ADDRESS1, BANK_BRANCH.BRANCH_ADDRS2 BANK_ADDRESS2, BANK_BRANCH.BRANCH_POST_CODE BANK_ADDRESS3, INVEST.EMP_INFO.NAME");
            sbQuery.Append(" SIGNATORY_NAME1,  EMP_INFO_1.NAME AS SIGNATORY_NAME2, INVEST.EMP_DESIGNATION.NAME AS  SIGNATORY_DESIG1, EMP_DESIGNATION_1.NAME AS SIGNATORY_DESIG2, BANK_NAME.BANK_CODE");
            sbQuery.Append(" FROM   BANK_NAME INNER JOIN  BANK_BRANCH INNER JOIN  REPURCHASE INNER JOIN  FUND_INFO ON REPURCHASE.REG_BK = FUND_INFO.FUND_CD ON BANK_BRANCH.ROUTING_NO = FUND_INFO.BANK_ROUTING_NO");
            sbQuery.Append(" ON BANK_NAME.BANK_CODE = BANK_BRANCH.BANK_CODE INNER JOIN  INVEST.EMP_INFO ON REPURCHASE.SIGNATORY1_ID = INVEST.EMP_INFO.ID INNER JOIN  INVEST.EMP_INFO EMP_INFO_1 ON REPURCHASE.SIGANTORY2_ID = EMP_INFO_1.ID INNER JOIN");
            sbQuery.Append(" INVEST.EMP_DESIGNATION ON INVEST.EMP_INFO.DESIG_ID = INVEST.EMP_DESIGNATION.ID INNER JOIN INVEST.EMP_DESIGNATION EMP_DESIGNATION_1 ON EMP_INFO_1.DESIG_ID = EMP_DESIGNATION_1.ID ");
            sbQuery.Append(" WHERE  (REPURCHASE.PAY_TYPE = 'EFT') AND (REPURCHASE.BEFTN_DATE IS NOT NULL) AND (REPURCHASE.VOUCHER_NO IS NOT NULL)");
            sbQuery.Append(filter.ToString());

            DataTable dtBEFTNSignatoryBankInfo = commonGatewayObj.Select(sbQuery.ToString());
            return dtBEFTNSignatoryBankInfo;
        }
        public bool IsValidBEFTN(UnitHolderRegistration regObj,UnitRepurchase repObj)
        {
            bool BeftnStatus = true;
            if (repObj.PayType.ToString().ToUpper() == "EFT")
            {
                DataTable dtValidBEFTNInfo = commonGatewayObj.Select("SELECT U_MASTER.BK_AC_NO, BANK_BRANCH.ROUTING_NO FROM U_MASTER INNER JOIN BANK_BRANCH ON U_MASTER.BK_NM_CD = BANK_BRANCH.BANK_CODE AND U_MASTER.BK_BR_NM_CD = BANK_BRANCH.BRANCH_CODE  WHERE U_MASTER.REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND U_MASTER.REG_BR='" + regObj.BranchCode.ToString() + "' AND U_MASTER.REG_NO=" + regObj.RegNumber + " AND BANK_BRANCH.ROUTING_NO IS NOT NULL ");
                if (dtValidBEFTNInfo.Rows.Count > 0)
                {

                    if (dtValidBEFTNInfo.Rows[0]["BK_AC_NO"].ToString().Length > 13)
                    {
                        BeftnStatus = false;
                    }
                }
                else
                {
                    BeftnStatus = false;
                }
            }


            return BeftnStatus;
        }
        public bool IsIDAccount(UnitHolderRegistration regObj, UnitRepurchase repObj)
        {
            bool IDAccountStatus = false;
            if (repObj.PayType.ToString().ToUpper() == "EFT")
            {
                DataTable dtValidBEFTNInfo = commonGatewayObj.Select("SELECT * FROM U_MASTER WHERE ID_FLAG='Y' AND  U_MASTER.REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND U_MASTER.REG_BR='" + regObj.BranchCode.ToString() + "' AND U_MASTER.REG_NO=" + regObj.RegNumber);
                if (dtValidBEFTNInfo.Rows.Count > 0)
                {

                    IDAccountStatus = true;                   
                }
                
            }
            return IDAccountStatus;
        }
        public string getPayType(UnitHolderRegistration regObj,UnitRepurchase repObj)
        {
            DataTable dtpayTypeInfo = commonGatewayObj.Select(" SELECT PAY_TYPE FROM REPURCHASE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REP_NO=" + repObj.RepurchaseNo);
            string payType = dtpayTypeInfo.Rows[0]["PAY_TYPE"].ToString().ToUpper();
            return payType;
        }
        public string getAcc_OP_ID(string fund_Code)
        {
            DataTable dtUnitUnitAcc_OP_ID = commonGatewayObj.Select("SELECT ACC_OP_ID FROM FUND_INFO WHERE FUND_CD='" + fund_Code + "'");
            string Acc_OP_ID = dtUnitUnitAcc_OP_ID.Rows[0]["ACC_OP_ID"].ToString();
            return Acc_OP_ID;

        }
        public long getAcc_terminal_no(string fund_Code)
        {
            DataTable dtgetAcc_terminal_no = commonGatewayObj.Select("SELECT ACC_TERMINAL_NO FROM FUND_INFO WHERE FUND_CD='" + fund_Code + "'");
            long Acc_terminal_no =Convert.ToInt64( dtgetAcc_terminal_no.Rows[0]["ACC_TERMINAL_NO"].ToString());
            return Acc_terminal_no;

        }
        public decimal getBEFTNAdviceTotalAmount(string filter)
        {
            StringBuilder sbQuery = new StringBuilder();            

            sbQuery.Append(" SELECT SUM(REP_PRICE * QTY) AS TOTAL_AMOUNT FROM REPURCHASE WHERE  (PAY_TYPE = 'EFT')  AND (VOUCHER_NO IS NOT NULL)");
          
            sbQuery.Append(filter.ToString());
           
            DataTable dtGetBEFTNAdviceData = commonGatewayObj.Select(sbQuery.ToString());
            decimal getBEFTNAdviceTotalAmount=Convert.ToDecimal( dtGetBEFTNAdviceData.Rows[0]["TOTAL_AMOUNT"].Equals(DBNull.Value)? 0 :dtGetBEFTNAdviceData.Rows[0]["TOTAL_AMOUNT"]);
            return getBEFTNAdviceTotalAmount;
        }
    }
}
