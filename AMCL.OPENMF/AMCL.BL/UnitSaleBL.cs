  using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Collections;
using AMCL.COMMON;
using AMCL.DL;
using AMCL.GATEWAY;
using System.Text;

/// <summary>
/// Summary description for UnitReg
/// </summary>
namespace AMCL.BL
{
    public class UnitSaleBL
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        OMFDAO OmfDAOObj = new OMFDAO();
        string bizSchema = ConfigReader.SCHEMA;
        public UnitSaleBL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public void SaveUnitSale(UnitHolderRegistration unitRegObj, UnitSale unitSaleObj, DataTable dtDinomination, UnitUser unitUserObj)
        {
            Hashtable htUnitSale = new Hashtable();
            Hashtable htUnitSaleCert = new Hashtable();
            Hashtable htCertNoTemp = new Hashtable();
            string certificate = "";
            try
            {
                commonGatewayObj.BeginTransaction();

                htUnitSale.Add("REG_BK", unitRegObj.FundCode.ToString().ToUpper());
                htUnitSale.Add("REG_BR", unitRegObj.BranchCode.ToString());
                htUnitSale.Add("REG_NO", unitRegObj.RegNumber.ToString());
                htUnitSale.Add("SL_NO", Convert.ToInt32(unitSaleObj.SaleNo.ToString()));
                htUnitSale.Add("SL_DT", Convert.ToDateTime(unitSaleObj.SaleDate).ToString("dd-MMM-yyyy"));
                htUnitSale.Add("SL_PRICE", Convert.ToDecimal(unitSaleObj.SaleRate.ToString()));
                htUnitSale.Add("QTY", Convert.ToInt32(unitSaleObj.SaleUnitQty.ToString()));
                htUnitSale.Add("SL_TYPE", unitSaleObj.SaleType.ToString().ToUpper());
                htUnitSale.Add("USER_NM", unitUserObj.UserID.ToString());
                htUnitSale.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htUnitSale.Add("ENT_DT", DateTime.Now.ToString());

                if (!(unitSaleObj.MoneyReceiptNo.ToString() == "0"))
                {
                    htUnitSale.Add("MONY_RECT_NO", Convert.ToInt32( unitSaleObj.MoneyReceiptNo));
                }
              
                if (!((unitSaleObj.MoneyReceiptNo.ToString() == "0") && unitSaleObj.PaymentType.ToString() == "CHQ" && unitSaleObj.ChequeNo == null && unitSaleObj.ChequeDate == null))
                {
                    htUnitSale.Add("PAY_TYPE", unitSaleObj.PaymentType); 
                }

                if (unitSaleObj.ChequeNo != null)
                {
                    htUnitSale.Add("CHQ_DD_NO", unitSaleObj.ChequeNo);
                }
                if (unitSaleObj.ChequeDate ==null)
                {
                    htUnitSale.Add("CHEQUE_DT", DBNull.Value);
                }
                else
                {
                    htUnitSale.Add("CHEQUE_DT", unitSaleObj.ChequeDate); 
                }
                if (!( unitSaleObj.BankCode.ToString() == "0"))
                {
                    htUnitSale.Add("BANK_CODE", Convert.ToInt16(unitSaleObj.BankCode));
                }

                if (!(unitSaleObj.BranchCode.ToString() == "0"))
                {
                    htUnitSale.Add("BRANCH_CODE", Convert.ToInt16(unitSaleObj.BranchCode));
                }
               
                if (!(unitSaleObj.CashAmount.ToString() == "0"))
                {
                    htUnitSale.Add("CASH_AMT",Convert.ToDecimal( unitSaleObj.CashAmount));
                }
                if (unitSaleObj.MultiPayType != null)
                {
                    htUnitSale.Add("MULTI_PAY_REMARKS", unitSaleObj.MultiPayType);
                }                
                 htUnitSale.Add("SELLING_AGENT_ID", unitSaleObj.SellingAgentCode);
               

                commonGatewayObj.Insert(htUnitSale, "SALE");

                htCertNoTemp.Add("REG_BK", unitRegObj.FundCode.ToString().ToUpper());
                htCertNoTemp.Add("REG_BR", unitRegObj.BranchCode.ToString());
                htCertNoTemp.Add("REG_NO", unitRegObj.RegNumber.ToString());
                htCertNoTemp.Add("SL_NO", Convert.ToInt32(unitSaleObj.SaleNo.ToString()));


                for (int looper = 0; looper < dtDinomination.Rows.Count; looper++)
                {
                    htUnitSaleCert.Add("REG_BK", unitRegObj.FundCode.ToString().ToUpper());
                    htUnitSaleCert.Add("REG_BR", unitRegObj.BranchCode.ToString());
                    htUnitSaleCert.Add("REG_NO", unitRegObj.RegNumber.ToString());
                    htUnitSaleCert.Add("SL_NO", Convert.ToInt32(unitSaleObj.SaleNo.ToString()));
                    htUnitSaleCert.Add("CERT_TYPE", dtDinomination.Rows[looper]["dino"].ToString().ToUpper());
                    htUnitSaleCert.Add("CERT_NO", Convert.ToInt32(dtDinomination.Rows[looper]["cert_no"].ToString()));
                    htUnitSaleCert.Add("CERTIFICATE", SaleCertNo(Convert.ToInt32(dtDinomination.Rows[looper]["cert_no"].ToString()), dtDinomination.Rows[looper]["dino"].ToString().ToUpper()));
                    htUnitSaleCert.Add("QTY", Convert.ToInt32(dtDinomination.Rows[looper]["cert_weight"].ToString()));
                    htUnitSaleCert.Add("USER_NM", unitUserObj.UserID.ToString());
                    htUnitSaleCert.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                    htUnitSaleCert.Add("ENT_DT", DateTime.Now.ToString());
                    commonGatewayObj.Insert(htUnitSaleCert, "SALE_CERT");
                    htUnitSaleCert = new Hashtable();
                    if (certificate == "")
                    {
                        certificate = SaleCertNo(Convert.ToInt32(dtDinomination.Rows[looper]["cert_no"].ToString()), dtDinomination.Rows[looper]["dino"].ToString().ToUpper());
                    }
                    else
                    {
                        certificate = certificate + "," + SaleCertNo(Convert.ToInt32(dtDinomination.Rows[looper]["cert_no"].ToString()), dtDinomination.Rows[looper]["dino"].ToString().ToUpper());
                    }
                }
                htCertNoTemp.Add("CERTIFICATE", certificate);
                htCertNoTemp.Add("USER_NM", unitUserObj.UserID.ToString());
                htCertNoTemp.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htCertNoTemp.Add("ENT_DT", DateTime.Now.ToString());
             //   commonGatewayObj.Insert(htCertNoTemp, "CERT_NO_TEMP");

                commonGatewayObj.CommitTransaction();
            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }

        }
        public void SaveUnitSaleCDS(UnitHolderRegistration unitRegObj, UnitSale unitSaleObj, UnitUser unitUserObj)
        {
            Hashtable htUnitSale = new Hashtable();
             try
            {
                commonGatewayObj.BeginTransaction();

                htUnitSale.Add("REG_BK", unitRegObj.FundCode.ToString().ToUpper());
                htUnitSale.Add("REG_BR", unitRegObj.BranchCode.ToString());
                htUnitSale.Add("REG_NO", unitRegObj.RegNumber.ToString());
                htUnitSale.Add("SL_NO", Convert.ToInt32(unitSaleObj.SaleNo.ToString()));
                htUnitSale.Add("SL_DT", Convert.ToDateTime(unitSaleObj.SaleDate).ToString("dd-MMM-yyyy"));
                htUnitSale.Add("SL_PRICE", Convert.ToDecimal(unitSaleObj.SaleRate.ToString()));
                htUnitSale.Add("QTY", Convert.ToInt32(unitSaleObj.SaleUnitQty.ToString()));
                htUnitSale.Add("SL_TYPE", unitSaleObj.SaleType.ToString().ToUpper());
                htUnitSale.Add("USER_NM", unitUserObj.UserID.ToString());
                htUnitSale.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htUnitSale.Add("ENT_DT", DateTime.Now.ToString());

                if (!(unitSaleObj.MoneyReceiptNo.ToString() == "0"))
                {
                    htUnitSale.Add("MONY_RECT_NO", Convert.ToInt32( unitSaleObj.MoneyReceiptNo));
                }
              
                if (!((unitSaleObj.MoneyReceiptNo.ToString() == "0") && unitSaleObj.PaymentType.ToString() == "CHQ" && unitSaleObj.ChequeNo == null && unitSaleObj.ChequeDate == null))
                {
                    htUnitSale.Add("PAY_TYPE", unitSaleObj.PaymentType); 
                }

                if (unitSaleObj.ChequeNo != null)
                {
                    htUnitSale.Add("CHQ_DD_NO", unitSaleObj.ChequeNo);
                }
                if (unitSaleObj.ChequeDate ==null)
                {
                    htUnitSale.Add("CHEQUE_DT", DBNull.Value);
                }
                else
                {
                    htUnitSale.Add("CHEQUE_DT", unitSaleObj.ChequeDate); 
                }
                if (!( unitSaleObj.BankCode.ToString() == "0"))
                {
                    htUnitSale.Add("BANK_CODE", Convert.ToInt16(unitSaleObj.BankCode));
                }

                if (!(unitSaleObj.BranchCode.ToString() == "0"))
                {
                    htUnitSale.Add("BRANCH_CODE", Convert.ToInt16(unitSaleObj.BranchCode));
                }
               
                if (!(unitSaleObj.CashAmount.ToString() == "0"))
                {
                    htUnitSale.Add("CASH_AMT",Convert.ToDecimal( unitSaleObj.CashAmount));
                }
                if (unitSaleObj.MultiPayType != null)
                {
                    htUnitSale.Add("MULTI_PAY_REMARKS", unitSaleObj.MultiPayType);
                }
                htUnitSale.Add("SELLING_AGENT_ID", unitSaleObj.SellingAgentCode);

                commonGatewayObj.Insert(htUnitSale, "SALE");
                commonGatewayObj.CommitTransaction();
            }
             catch (Exception ex)
             {
                 commonGatewayObj.RollbackTransaction();
                 throw ex;
             }
        }
        public string SaleCertNo(int certNo, string dino)
        {
            int certLenght = certNo.ToString().Length;
            if (certLenght == 1)
            {
                return dino + "-" + "00000" + certNo;
            }
            if (certLenght == 2)
            {
                return dino + "-" + "0000" + certNo;
            }
            else if (certLenght == 3)
            {
                return dino + "-" + "000" + certNo;
            }
            else if (certLenght == 4)
            {
                return dino + "-" + "00" + certNo;
            }
            else if (certLenght == 5)
            {
                return dino + "-" + "0" + certNo;
            }
            else
            {
                return dino + "-" + certNo;
            }


        }
        public bool IsDuplicateSale(UnitHolderRegistration regObj, UnitSale saleObj)
        {
            DataTable dtSale = commonGatewayObj.Select("SELECT * FROM SALE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND SL_NO=" + Convert.ToInt32(saleObj.SaleNo));
            if (dtSale.Rows.Count > 0)
                return true;
            else
                return false;

        }
        public int getNextSaleNo(UnitHolderRegistration regObj, UnitUser unitUserObj)
        {
            int nextSaleNo = 0;
            DataTable dtNextSaleNo = new DataTable();
            dtNextSaleNo = commonGatewayObj.Select("SELECT MAX(SL_NO) AS MAX_SL_NO FROM SALE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND USER_NM='" + unitUserObj.UserID.ToString() + "' AND ENT_DT IN(SELECT MAX(ENT_DT) FROM SALE SALE_1 WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND USER_NM='" + unitUserObj.UserID.ToString() + "' )");
            if (!dtNextSaleNo.Rows[0]["MAX_SL_NO"].Equals(DBNull.Value))
            {
                nextSaleNo = Convert.ToInt32(dtNextSaleNo.Rows[0]["MAX_SL_NO"].ToString());
            }
            else
            {
                dtNextSaleNo = new DataTable();
                dtNextSaleNo = commonGatewayObj.Select("SELECT MAX(SL_NO) AS MAX_SL_NO FROM SALE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "'");
                nextSaleNo = dtNextSaleNo.Rows[0]["MAX_SL_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtNextSaleNo.Rows[0]["MAX_SL_NO"].ToString());
            }
            return nextSaleNo + 1;

        }
        //public int getNextMoneReceiptNo(UnitHolderRegistration regObj, UnitUser unitUserObj)
        //{
        //    int nextMoneReceiptNo = 0;
        //    DataTable dtNextMoneReceiptNo = new DataTable();
        //    dtNextMoneReceiptNo = commonGatewayObj.Select("SELECT MAX(MONY_RECT_NO) AS MAX_MONEY_NO FROM SALE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND USER_NM='" + unitUserObj.UserID.ToString() + "' AND ENT_DT IN(SELECT MAX(ENT_DT) FROM SALE SALE_1 WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND USER_NM='" + unitUserObj.UserID.ToString() + "' )");
        //    if (!dtNextMoneReceiptNo.Rows[0]["MAX_MONEY_NO"].Equals(DBNull.Value))
        //    {
        //        nextMoneReceiptNo = Convert.ToInt16(dtNextMoneReceiptNo.Rows[0]["MAX_MONEY_NO"].ToString());
        //    }
        //    else
        //    {
        //        dtNextMoneReceiptNo = new DataTable();
        //        dtNextMoneReceiptNo = commonGatewayObj.Select("SELECT MAX(MONY_RECT_NO) AS MAX_MONEY_NO FROM SALE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "'");
        //        nextMoneReceiptNo = dtNextMoneReceiptNo.Rows[0]["MAX_MONEY_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt16(dtNextMoneReceiptNo.Rows[0]["MAX_MONEY_NO"].ToString());
        //    }
        //    return nextMoneReceiptNo + 1;

        //}
        public bool IsCertificateAllocate(UnitHolderRegistration regObj, string dino, string certNo)
        {
            bool isCertificateAllocate = false;
            StringBuilder sbQueryString = new StringBuilder();
            sbQueryString.Append(" SELECT  A.FUND_CD, A.BR_CD, A.CERT_TYPE,A.CERT_NO_START, A.CERT_NO_END FROM " + bizSchema + ".CERT_BOOK_INFO A");
            sbQueryString.Append(" WHERE (A.CERT_TYPE = '" + dino + "') AND (A.CERT_NO_START <= " + Convert.ToInt32(certNo.ToString()) + ") AND (A.CERT_NO_END >= " + Convert.ToInt32(certNo.ToString()) + ") ");
            sbQueryString.Append(" AND A.FUND_CD='"+regObj.FundCode.ToString()+"' AND A.BR_CD='"+ regObj.BranchCode.ToString() +"'");
            DataTable dtCertificateAllocate = commonGatewayObj.Select(sbQueryString.ToString());
            if (dtCertificateAllocate.Rows.Count > 0)
            {
                isCertificateAllocate = true;
            }
            return isCertificateAllocate;

            
        }
        public bool IsCertificateBanned(UnitHolderRegistration regObj, string dino, string certNo)
        {
            bool IsCertificateBanned = false;
            StringBuilder sbQueryString = new StringBuilder();
            sbQueryString.Append(" SELECT  A.FUND_CD,  A.CERT_TYPE,A.CERT_NO_BAN_START, A.CERT_NO_BAN_END FROM " + bizSchema + ".CERT_NO_BAN A");
            sbQueryString.Append(" WHERE (A.CERT_TYPE = '" + dino + "') AND (A.CERT_NO_BAN_START <= " + Convert.ToInt32(certNo.ToString()) + ") AND (A.CERT_NO_BAN_END >= " + Convert.ToInt32(certNo.ToString()) + ") ");
            sbQueryString.Append(" AND A.FUND_CD='" + regObj.FundCode.ToString() + "'");
            DataTable dtCertificateBanned = commonGatewayObj.Select(sbQueryString.ToString());
            if (dtCertificateBanned.Rows.Count > 0)
            {
                IsCertificateBanned = true;
            }
            return IsCertificateBanned;


        }
        public int SaleLimitLower(UnitHolderRegistration regObj)
        {
            int saleLimitLower = 0;
            DataTable dtUnitLimitLower=commonGatewayObj.Select("SELECT A.MIN_MK_LOT  FROM "+ bizSchema+ ".FUND_INFO A WHERE A.FUND_CD='"+ regObj.FundCode.ToString()+"'");
            saleLimitLower = Convert.ToInt32(dtUnitLimitLower.Rows[0]["MIN_MK_LOT"].Equals(DBNull.Value) ? "0" : dtUnitLimitLower.Rows[0]["MIN_MK_LOT"].ToString());
            return saleLimitLower;
        }
        public long  SaleLimitUpper(UnitHolderRegistration regObj)
        {
            int SaleLimitUpper = 0;
            DataTable dtUnitLimitUpper = commonGatewayObj.Select("SELECT A.MAX_MK_LOT  FROM " + bizSchema + ".FUND_INFO A WHERE A.FUND_CD='" + regObj.FundCode.ToString() + "'");
            SaleLimitUpper = Convert.ToInt32(dtUnitLimitUpper.Rows[0]["MAX_MK_LOT"].Equals(DBNull.Value) ? "0" : dtUnitLimitUpper.Rows[0]["MAX_MK_LOT"].ToString());
            return SaleLimitUpper;
        }
        public string GetNextSaleType(UnitHolderRegistration regObj,UnitUser unitUserObj)
        {
            string saleType = "SL";
            DataTable dtNextSaleType = new DataTable();
            dtNextSaleType = commonGatewayObj.Select("SELECT SL_TYPE FROM SALE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND USER_NM='" + unitUserObj.UserID.ToString() + "' AND ENT_DT IN(SELECT MAX(ENT_DT) FROM SALE SALE_1 WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND USER_NM='" + unitUserObj.UserID.ToString() + "' )");
            if (dtNextSaleType.Rows.Count > 0)
            {
                if (!dtNextSaleType.Rows[0]["SL_TYPE"].Equals(DBNull.Value))
                {
                    saleType = dtNextSaleType.Rows[0]["SL_TYPE"].ToString();
                }
                
            }
            return saleType;
        }
        public bool IsValidSaleNumber(UnitHolderRegistration regObj, UnitSale saleObj)
        {
            DataTable dtValidSaleNumber = new DataTable();
            bool ValidSaleNumber = false;

            string queryString = "SELECT * FROM SALE WHERE REG_BK='" + regObj.FundCode.ToString() + "' AND REG_BR='" + regObj.BranchCode + "' AND SL_NO=" + Convert.ToInt32(saleObj.SaleNo.ToString());
            dtValidSaleNumber = commonGatewayObj.Select(queryString.ToString());
            if (dtValidSaleNumber.Rows.Count > 0)
            {
                if (dtValidSaleNumber.Rows[0]["SL_NO"].Equals(DBNull.Value))
                {
                    ValidSaleNumber = false;
                }
                else
                {
                    int SaleNo = Convert.ToInt32(dtValidSaleNumber.Rows[0]["SL_NO"].ToString());
                    if (saleObj.SaleNo.ToString() == SaleNo.ToString())
                    {
                        ValidSaleNumber = true;
                    }
                    else
                    {
                        ValidSaleNumber = false;
                    }
                }
            }
            else
            {
                ValidSaleNumber = false;
            }

            return ValidSaleNumber;

        }
        public DataTable dtGetSaleInfo(UnitHolderRegistration regObj, UnitSale saleObj)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT  SALE.SL_NO, SALE.SL_DT, SALE.REG_NO, SALE.SL_PRICE, SALE.QTY, SALE.SL_TYPE, SALE.MONY_RECT_NO, SALE.PAY_TYPE, SALE.CHQ_DD_NO,");
            sbQuery.Append(" SALE.CASH_AMT, SALE.BANK_CODE, SALE.BRANCH_CODE, SALE.MULTI_PAY_REMARKS, SALE.CHEQUE_DT, SALE_CERT.CERT_TYPE AS DINO,SALE_CERT.ROWID,");
            sbQuery.Append(" SALE_CERT.CERT_NO, SALE_CERT.CERTIFICATE, SALE_CERT.QTY AS CERT_WEIGHT, SALE.REG_BK, SALE.REG_BR, U_MASTER.HNAME, ");
            sbQuery.Append(" U_JHOLDER.JNT_NAME, U_MASTER.ADDRS1, U_MASTER.ADDRS2,U_MASTER.TEL_NO, U_MASTER.CITY, U_MASTER.CIP , SALE_CERT.STATUS_FLAG FROM  U_MASTER INNER JOIN SALE INNER JOIN ");
            sbQuery.Append(" SALE_CERT ON SALE.SL_NO = SALE_CERT.SL_NO AND SALE.REG_BK = SALE_CERT.REG_BK AND SALE.REG_BR = SALE_CERT.REG_BR ON ");
            sbQuery.Append(" U_MASTER.REG_BK = SALE.REG_BK AND U_MASTER.REG_BR = SALE.REG_BR AND U_MASTER.REG_NO = SALE.REG_NO LEFT OUTER JOIN");
            sbQuery.Append(" U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND U_MASTER.REG_NO = U_JHOLDER.REG_NO");
            sbQuery.Append(" WHERE (SALE.REG_BR = '" + regObj.BranchCode.ToString() + "') AND (SALE.SL_NO = " + saleObj.SaleNo + ") AND (SALE.REG_BK = '" + regObj.FundCode.ToString() + "')");
            DataTable dtGetSaleInfo = commonGatewayObj.Select(sbQuery.ToString());
            return dtGetSaleInfo;
        }
        public DataTable dtGetSaleInfoCDS(UnitHolderRegistration regObj, UnitSale saleObj)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT SALE.*,U_MASTER.*,U_JHOLDER.* FROM  U_MASTER INNER JOIN SALE ");
            sbQuery.Append(" ON U_MASTER.REG_BK = SALE.REG_BK AND U_MASTER.REG_BR = SALE.REG_BR AND U_MASTER.REG_NO = SALE.REG_NO LEFT OUTER JOIN");
            sbQuery.Append(" U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND U_MASTER.REG_NO = U_JHOLDER.REG_NO");
            sbQuery.Append(" WHERE (SALE.REG_BR = '" + regObj.BranchCode.ToString() + "') AND (SALE.SL_NO = " + saleObj.SaleNo + ") AND (SALE.REG_BK = '" + regObj.FundCode.ToString() + "')");
            DataTable dtGetSaleInfo = commonGatewayObj.Select(sbQuery.ToString());
            return dtGetSaleInfo;
        }
        public void SaveSaleEditInfo(UnitHolderRegistration unitRegObj, UnitSale unitSaleObj, DataTable dtDinomination, UnitUser unitUserObj)
        {
            Hashtable htUnitSale = new Hashtable();
            Hashtable htUnitSaleCert = new Hashtable();
            Hashtable htCertNoTemp = new Hashtable();
            string certificate = "";
            try
            {
                commonGatewayObj.BeginTransaction();

                //htUnitSale.Add("REG_BK", unitRegObj.FundCode.ToString().ToUpper());
                //htUnitSale.Add("REG_BR", unitRegObj.BranchCode.ToString());
                htUnitSale.Add("REG_NO", unitRegObj.RegNumber.ToString());

                //htUnitSaleCert.Add("REG_BK", unitRegObj.FundCode.ToString().ToUpper());
                //htUnitSaleCert.Add("REG_BR", unitRegObj.BranchCode.ToString());
                htUnitSaleCert.Add("REG_NO", unitRegObj.RegNumber.ToString());

                //htUnitSale.Add("SL_NO", Convert.ToInt32(unitSaleObj.SaleNo.ToString()));
                htUnitSale.Add("SL_DT", Convert.ToDateTime(unitSaleObj.SaleDate).ToString("dd-MMM-yyyy"));
                htUnitSale.Add("SL_PRICE", Convert.ToDecimal(unitSaleObj.SaleRate.ToString()));
                htUnitSale.Add("QTY", Convert.ToInt32(unitSaleObj.SaleUnitQty.ToString()));
                htUnitSale.Add("SL_TYPE", unitSaleObj.SaleType.ToString().ToUpper());

                htCertNoTemp.Add("REG_BK", unitRegObj.FundCode.ToString().ToUpper());
                htCertNoTemp.Add("REG_BR", unitRegObj.BranchCode.ToString());
                htCertNoTemp.Add("REG_NO", unitRegObj.RegNumber.ToString());
                htCertNoTemp.Add("SL_NO", Convert.ToInt32(unitSaleObj.SaleNo.ToString()));
                htCertNoTemp.Add("SL_DT", Convert.ToDateTime(unitSaleObj.SaleDate).ToString("dd-MMM-yyyy"));
                htCertNoTemp.Add("SL_PRICE", Convert.ToDecimal(unitSaleObj.SaleRate.ToString()));
                htCertNoTemp.Add("QTY", Convert.ToInt32(unitSaleObj.SaleUnitQty.ToString()));
                htCertNoTemp.Add("SL_TYPE", unitSaleObj.SaleType.ToString().ToUpper());
                //htUnitSale.Add("USER_NM", unitUserObj.UserID.ToString());
                //htUnitSale.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                //htUnitSale.Add("ENT_DT", DateTime.Now.ToString());

                if (!(unitSaleObj.MoneyReceiptNo.ToString() == "0"))
                {
                    htUnitSale.Add("MONY_RECT_NO", Convert.ToInt32(unitSaleObj.MoneyReceiptNo));
                    htCertNoTemp.Add("MONY_RECT_NO", Convert.ToInt32(unitSaleObj.MoneyReceiptNo));
                }
                else
                {
                    htUnitSale.Add("MONY_RECT_NO",DBNull.Value );
                    htCertNoTemp.Add("MONY_RECT_NO", DBNull.Value);
                }

                if (!((unitSaleObj.MoneyReceiptNo.ToString() == "0") && unitSaleObj.PaymentType.ToString() == "CHQ" && unitSaleObj.ChequeNo == null && unitSaleObj.ChequeDate == null))
                {
                    htUnitSale.Add("PAY_TYPE", unitSaleObj.PaymentType);
                    htCertNoTemp.Add("PAY_TYPE", unitSaleObj.PaymentType);
                }
                else
                {
                    htUnitSale.Add("PAY_TYPE", DBNull.Value);
                    htCertNoTemp.Add("PAY_TYPE", DBNull.Value);
                }

                if (unitSaleObj.ChequeNo != null)
                {
                    htUnitSale.Add("CHQ_DD_NO", unitSaleObj.ChequeNo);
                    htCertNoTemp.Add("CHQ_DD_NO", unitSaleObj.ChequeNo);
                }
                else
                {
                    htUnitSale.Add("CHQ_DD_NO", DBNull.Value);
                    htCertNoTemp.Add("CHQ_DD_NO", DBNull.Value);
                }
                if (unitSaleObj.ChequeDate == null)
                {
                    htUnitSale.Add("CHEQUE_DT", DBNull.Value);
                    htCertNoTemp.Add("CHEQUE_DT", DBNull.Value);
                }
                else
                {
                    htUnitSale.Add("CHEQUE_DT", unitSaleObj.ChequeDate);
                    htCertNoTemp.Add("CHEQUE_DT", unitSaleObj.ChequeDate);
                }
                if (!(unitSaleObj.BankCode.ToString() == "0"))
                {
                    htUnitSale.Add("BANK_CODE", Convert.ToInt16(unitSaleObj.BankCode));
                    htCertNoTemp.Add("BANK_CODE", Convert.ToInt16(unitSaleObj.BankCode));
                }
                else
                {
                    htUnitSale.Add("BANK_CODE",  DBNull.Value);
                    htCertNoTemp.Add("BANK_CODE", DBNull.Value);
                }
                if (!(unitSaleObj.BranchCode.ToString() == "0"))
                {
                    htUnitSale.Add("BRANCH_CODE", Convert.ToInt16(unitSaleObj.BranchCode));
                    htCertNoTemp.Add("BRANCH_CODE", Convert.ToInt16(unitSaleObj.BranchCode));
                }
                else
                {
                    htUnitSale.Add("BRANCH_CODE", DBNull.Value);
                    htCertNoTemp.Add("BRANCH_CODE", DBNull.Value);
                }

                if (!(unitSaleObj.CashAmount.ToString() == "0"))
                {
                    htUnitSale.Add("CASH_AMT", Convert.ToDecimal(unitSaleObj.CashAmount));
                    htCertNoTemp.Add("CASH_AMT", Convert.ToDecimal(unitSaleObj.CashAmount));
                }
                else
                {
                    htUnitSale.Add("CASH_AMT", DBNull.Value);
                    htCertNoTemp.Add("CASH_AMT", DBNull.Value);
                }
                if (unitSaleObj.MultiPayType != null)
                {
                    htUnitSale.Add("MULTI_PAY_REMARKS", unitSaleObj.MultiPayType);
                    htCertNoTemp.Add("MULTI_PAY_REMARKS", unitSaleObj.MultiPayType);
                }
                else
                {
                    htUnitSale.Add("MULTI_PAY_REMARKS", DBNull.Value);
                    htCertNoTemp.Add("MULTI_PAY_REMARKS", DBNull.Value);
                }



                //update sale table 
                commonGatewayObj.Update(htUnitSale, "SALE", "SL_NO=" + Convert.ToInt32(unitSaleObj.SaleNo.ToString()) + " AND REG_BK='" + unitRegObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + unitRegObj.BranchCode.ToString() +"' ");
                //update sale_cert table if reg_no is change
                commonGatewayObj.Update(htUnitSaleCert, "SALE_CERT", "SL_NO=" + Convert.ToInt32(unitSaleObj.SaleNo.ToString()) + " AND REG_BK='" + unitRegObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + unitRegObj.BranchCode.ToString() + "' ");
                htUnitSaleCert = new Hashtable();

                if (dtDinomination.Rows.Count > 0)//Update certificate change information
                {
                    for (int looper = 0; looper < dtDinomination.Rows.Count; looper++)
                    {
                       
                        // htUnitSaleCert.Add("SL_NO", Convert.ToInt32(unitSaleObj.SaleNo.ToString()));
                      //  htUnitSaleCert.Add("STATUS_FLAG", dtDinomination.Rows[looper]["status"].ToString().ToUpper());
                        htUnitSaleCert.Add("CERT_TYPE", dtDinomination.Rows[looper]["dino"].ToString().ToUpper());
                        htUnitSaleCert.Add("CERT_NO", Convert.ToInt32(dtDinomination.Rows[looper]["cert_no"].ToString()));
                        htUnitSaleCert.Add("CERTIFICATE", SaleCertNo(Convert.ToInt32(dtDinomination.Rows[looper]["cert_no"].ToString()), dtDinomination.Rows[looper]["dino"].ToString().ToUpper()));
                        htUnitSaleCert.Add("QTY", Convert.ToInt32(dtDinomination.Rows[looper]["cert_weight"].ToString()));
                        // htUnitSaleCert.Add("USER_NM", unitUserObj.UserID.ToString());
                        //htUnitSaleCert.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                        //htUnitSaleCert.Add("ENT_DT", DateTime.Now.ToString());
                        commonGatewayObj.Update(htUnitSaleCert, "SALE_CERT", "SL_NO=" + Convert.ToInt32(unitSaleObj.SaleNo.ToString()) + " AND ROWID='" + dtDinomination.Rows[looper]["ROWID"].ToString() + "'");
                        htUnitSaleCert = new Hashtable();
                        if (certificate == "")
                        {
                            certificate = SaleCertNo(Convert.ToInt32(dtDinomination.Rows[looper]["cert_no"].ToString()), dtDinomination.Rows[looper]["dino"].ToString().ToUpper());
                        }
                        else
                        {
                            certificate = certificate + "," + SaleCertNo(Convert.ToInt32(dtDinomination.Rows[looper]["cert_no"].ToString()), dtDinomination.Rows[looper]["dino"].ToString().ToUpper());
                        }
                    }
                }
              

                if (certificate != "")
                {
                    htCertNoTemp.Add("CERTIFICATE", certificate);
                }
                else
                {
                    htCertNoTemp.Add("CERTIFICATE", DBNull.Value);
                }
                htCertNoTemp.Add("USER_NM", unitUserObj.UserID.ToString());
                htCertNoTemp.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htCertNoTemp.Add("ENT_DT", DateTime.Now.ToString());
                htCertNoTemp.Add("EDIT_TYPE", "E");
                commonGatewayObj.Insert(htCertNoTemp, "SALE_ED_INFO");

                commonGatewayObj.CommitTransaction();
            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }

        }
        public void SaveSaleEditInfoCDS(UnitHolderRegistration unitRegObj, UnitSale unitSaleObj,  UnitUser unitUserObj)
        {
            Hashtable htUnitSale = new Hashtable();
            Hashtable htUnitSaleCert = new Hashtable();
            StringBuilder sbQuery = new StringBuilder();
           
            try
            {
                commonGatewayObj.BeginTransaction();

                
                                                     
                htUnitSale.Add("SL_DT", Convert.ToDateTime(unitSaleObj.SaleDate).ToString("dd-MMM-yyyy"));
                htUnitSale.Add("SL_PRICE", Convert.ToDecimal(unitSaleObj.SaleRate.ToString()));
                htUnitSale.Add("QTY", Convert.ToInt32(unitSaleObj.SaleUnitQty.ToString()));
                htUnitSale.Add("SL_TYPE", unitSaleObj.SaleType.ToString().ToUpper());

               

                if (!(unitSaleObj.MoneyReceiptNo.ToString() == "0"))
                {
                    htUnitSale.Add("MONY_RECT_NO", Convert.ToInt32(unitSaleObj.MoneyReceiptNo));
                   
                }
                else
                {
                    htUnitSale.Add("MONY_RECT_NO", DBNull.Value);
                   
                }

                if (!((unitSaleObj.MoneyReceiptNo.ToString() == "0") && unitSaleObj.PaymentType.ToString() == "CHQ" && unitSaleObj.ChequeNo == null && unitSaleObj.ChequeDate == null))
                {
                    htUnitSale.Add("PAY_TYPE", unitSaleObj.PaymentType);
                   
                }
                else
                {
                    htUnitSale.Add("PAY_TYPE", DBNull.Value);
                   
                }

                if (unitSaleObj.ChequeNo != null)
                {
                    htUnitSale.Add("CHQ_DD_NO", unitSaleObj.ChequeNo);
                   
                }
                else
                {
                    htUnitSale.Add("CHQ_DD_NO", DBNull.Value);
                  
                }
                if (unitSaleObj.ChequeDate == null)
                {
                    htUnitSale.Add("CHEQUE_DT", DBNull.Value);
                   
                }
                else
                {
                    htUnitSale.Add("CHEQUE_DT", unitSaleObj.ChequeDate);
                   
                }
                if (!(unitSaleObj.BankCode.ToString() == "0"))
                {
                    htUnitSale.Add("BANK_CODE", Convert.ToInt16(unitSaleObj.BankCode));
                  
                }
                else
                {
                    htUnitSale.Add("BANK_CODE", DBNull.Value);
                   
                }
                if (!(unitSaleObj.BranchCode.ToString() == "0"))
                {
                    htUnitSale.Add("BRANCH_CODE", Convert.ToInt16(unitSaleObj.BranchCode));
                    
                }
                else
                {
                    htUnitSale.Add("BRANCH_CODE", DBNull.Value);
                    
                }

                if (!(unitSaleObj.CashAmount.ToString() == "0"))
                {
                    htUnitSale.Add("CASH_AMT", Convert.ToDecimal(unitSaleObj.CashAmount));
                   
                }
                else
                {
                    htUnitSale.Add("CASH_AMT", DBNull.Value);
                   
                }
                if (unitSaleObj.MultiPayType != null)
                {
                    htUnitSale.Add("MULTI_PAY_REMARKS", unitSaleObj.MultiPayType);
                   
                }
                else
                {
                    htUnitSale.Add("MULTI_PAY_REMARKS", DBNull.Value);
                   
                }


               
                commonGatewayObj.Update(htUnitSale, "SALE", "SL_NO=" + Convert.ToInt32(unitSaleObj.SaleNo.ToString()) + " AND REG_BK='" + unitRegObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + unitRegObj.BranchCode.ToString() + "' ");

                sbQuery.Append(" INSERT INTO  SALE_ED_INFO ( SL_NO, SL_DT, REG_BK, REG_BR, REG_NO, SL_PRICE, QTY, SL_TYPE, VALID, MONY_RECT_NO, PAY_TYPE, CHQ_DD_NO, ");
                sbQuery.Append(" CASH_AMT, BANK_CODE, BRANCH_CODE, MULTI_PAY_REMARKS, CHEQUE_DT,USER_NM, EDIT_TYPE, ENT_DT ) ");
                sbQuery.Append(" SELECT SL_NO, SL_DT, REG_BK, REG_BR, REG_NO, SL_PRICE, QTY, SL_TYPE, VALID, MONY_RECT_NO, PAY_TYPE, CHQ_DD_NO, CASH_AMT, BANK_CODE, BRANCH_CODE, MULTI_PAY_REMARKS, CHEQUE_DT, ");
                sbQuery.Append(" '" + unitUserObj.UserID + "' USER_NM,'E' EDIT_TYPE ,'" + DateTime.Now.ToString("dd-MMM-yyyy") + "'  ENT_DT  FROM  SALE "); 
                sbQuery.Append(" WHERE SL_NO=" + Convert.ToInt32(unitSaleObj.SaleNo.ToString()) + " AND REG_BK='" + unitRegObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + unitRegObj.BranchCode.ToString() + "' AND REG_NO=" + Convert.ToInt32( unitRegObj.RegNumber));
                commonGatewayObj.ExecuteNonQuery(sbQuery.ToString());
                          
                commonGatewayObj.CommitTransaction();
            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }

        }
        public void DeleteSaleEditInfo(UnitHolderRegistration unitRegObj, UnitSale unitSaleObj, DataTable dtDinomination, UnitUser unitUserObj)
        {
            Hashtable htUnitSale = new Hashtable();
            Hashtable htUnitSaleCert = new Hashtable();
            Hashtable htCertNoTemp = new Hashtable();
            string certificate = "";
            try
            {
                commonGatewayObj.BeginTransaction();
                

                htCertNoTemp.Add("REG_BK", unitRegObj.FundCode.ToString().ToUpper());
                htCertNoTemp.Add("REG_BR", unitRegObj.BranchCode.ToString());
                htCertNoTemp.Add("REG_NO", unitRegObj.RegNumber.ToString());
                htCertNoTemp.Add("SL_NO", Convert.ToInt32(unitSaleObj.SaleNo.ToString()));
                htCertNoTemp.Add("SL_DT", Convert.ToDateTime(unitSaleObj.SaleDate).ToString("dd-MMM-yyyy"));
                htCertNoTemp.Add("SL_PRICE", Convert.ToDecimal(unitSaleObj.SaleRate.ToString()));
                htCertNoTemp.Add("QTY", Convert.ToInt32(unitSaleObj.SaleUnitQty.ToString()));
                htCertNoTemp.Add("SL_TYPE", unitSaleObj.SaleType.ToString().ToUpper());              
                if (!(unitSaleObj.MoneyReceiptNo.ToString() == "0"))
                {
                   
                    htCertNoTemp.Add("MONY_RECT_NO", Convert.ToInt32(unitSaleObj.MoneyReceiptNo));
                }
                else
                {                    
                    htCertNoTemp.Add("MONY_RECT_NO", DBNull.Value);
                }

                if (unitSaleObj.PaymentType != null)
                {                    
                    htCertNoTemp.Add("PAY_TYPE", unitSaleObj.PaymentType);
                }
                else
                {                   
                    htCertNoTemp.Add("PAY_TYPE", DBNull.Value);
                }

                if (unitSaleObj.ChequeNo != null)
                {                    
                    htCertNoTemp.Add("CHQ_DD_NO", unitSaleObj.ChequeNo);
                }
                else
                {                   
                    htCertNoTemp.Add("CHQ_DD_NO", DBNull.Value);
                }
                if (unitSaleObj.ChequeDate == null || unitSaleObj.ChequeDate=="")
                {                    
                    htCertNoTemp.Add("CHEQUE_DT", DBNull.Value);
                }
                else
                {                  
                    htCertNoTemp.Add("CHEQUE_DT",Convert.ToDateTime(unitSaleObj.ChequeDate).ToString("dd-MMM-yyyy"));
                }
                if (!(unitSaleObj.BankCode.ToString() == "0"))
                {                   
                    htCertNoTemp.Add("BANK_CODE", Convert.ToInt16(unitSaleObj.BankCode));
                }
                else
                {                   
                    htCertNoTemp.Add("BANK_CODE", DBNull.Value);
                }
                if (!(unitSaleObj.BranchCode.ToString() =="0"))
                {                    
                    htCertNoTemp.Add("BRANCH_CODE", Convert.ToInt16(unitSaleObj.BranchCode));
                }
                else
                {
                    htCertNoTemp.Add("BRANCH_CODE", DBNull.Value);
                }

                if (!(unitSaleObj.CashAmount.ToString() == null))
                {                   
                    htCertNoTemp.Add("CASH_AMT", Convert.ToDecimal(unitSaleObj.CashAmount));
                }
                else
                {                   
                    htCertNoTemp.Add("CASH_AMT", DBNull.Value);
                }
                if (unitSaleObj.MultiPayType != null)
                {                    
                    htCertNoTemp.Add("MULTI_PAY_REMARKS", unitSaleObj.MultiPayType);
                }
                else
                {                   
                    htCertNoTemp.Add("MULTI_PAY_REMARKS", DBNull.Value);
                }
            
                if (dtDinomination.Rows.Count > 0)
                {
                    for (int looper = 0; looper < dtDinomination.Rows.Count; looper++)
                    {
                      
                        if (certificate == "")
                        {
                            certificate = SaleCertNo(Convert.ToInt32(dtDinomination.Rows[looper]["CERT_NO"].ToString()), dtDinomination.Rows[looper]["CERT_TYPE"].ToString().ToUpper());
                        }
                        else
                        {
                            certificate = certificate + "," + SaleCertNo(Convert.ToInt32(dtDinomination.Rows[looper]["CERT_NO"].ToString()), dtDinomination.Rows[looper]["CERT_TYPE"].ToString().ToUpper());
                        }
                    }
                }
                if (certificate != "")
                {
                    htCertNoTemp.Add("CERTIFICATE", certificate);
                }
                else
                {
                    htCertNoTemp.Add("CERTIFICATE", DBNull.Value);
                }
                htCertNoTemp.Add("USER_NM", unitUserObj.UserID.ToString());
                htCertNoTemp.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                htCertNoTemp.Add("ENT_DT", DateTime.Now.ToString());
                htCertNoTemp.Add("EDIT_TYPE", "D");
                commonGatewayObj.Insert(htCertNoTemp, "SALE_ED_INFO");
                commonGatewayObj.DeleteByCommand("SALE", " SL_NO=" + unitSaleObj.SaleNo + " AND REG_BR='" + unitRegObj.BranchCode.ToString() + "' AND REG_BK='" + unitRegObj.FundCode.ToString()+ "' AND REG_NO="+Convert.ToInt32( unitRegObj.RegNumber));
                commonGatewayObj.DeleteByCommand("SALE_CERT", " SL_NO=" + unitSaleObj.SaleNo + " AND REG_BR='" + unitRegObj.BranchCode.ToString() + "' AND  REG_BK='" + unitRegObj.FundCode.ToString() + "' AND REG_NO=" + Convert.ToInt32(unitRegObj.RegNumber));
                commonGatewayObj.ExecuteNonQuery(" UPDATE MONEY_RECEIPT SET SL_REP_TR_RN_NO= NULL WHERE RECEIPT_TYPE='SL' AND RECEIPT_NO="+ Convert.ToInt32(unitSaleObj.MoneyReceiptNo) + " AND REG_BR='" + unitRegObj.BranchCode.ToString() + "' AND REG_BK='" + unitRegObj.FundCode.ToString() + "' AND REG_NO=" + Convert.ToInt32(unitRegObj.RegNumber));

                commonGatewayObj.CommitTransaction();
            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }

        }
        public void DeleteSaleEditInfoCDS(UnitHolderRegistration unitRegObj, UnitSale unitSaleObj, UnitUser unitUserObj)
        {

           
            StringBuilder sbQuery = new StringBuilder();

            try
            {
                commonGatewayObj.BeginTransaction();

                commonGatewayObj.DeleteByCommand("SALE", "SL_NO=" + Convert.ToInt32(unitSaleObj.SaleNo.ToString()) + " AND REG_BK='" + unitRegObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + unitRegObj.BranchCode.ToString() + "' AND REG_NO=" + Convert.ToInt32(unitRegObj.RegNumber));
                sbQuery.Append(" INSERT INTO  SALE_ED_INFO ( SL_NO, SL_DT, REG_BK, REG_BR, REG_NO, SL_PRICE, QTY, SL_TYPE, VALID, MONY_RECT_NO, PAY_TYPE, CHQ_DD_NO, ");
                sbQuery.Append(" CASH_AMT, BANK_CODE, BRANCH_CODE, MULTI_PAY_REMARKS, CHEQUE_DT,USER_NM, EDIT_TYPE, ENT_DT ) ");
                sbQuery.Append(" SELECT SL_NO, SL_DT, REG_BK, REG_BR, REG_NO, SL_PRICE, QTY, SL_TYPE, VALID, MONY_RECT_NO, PAY_TYPE, CHQ_DD_NO, CASH_AMT, BANK_CODE, BRANCH_CODE, MULTI_PAY_REMARKS, CHEQUE_DT, ");
                sbQuery.Append(" '" + unitUserObj.UserID + "' USER_NM,'D' EDIT_TYPE ,'" + DateTime.Now.ToString("dd-MMM-yyyy") + "'  ENT_DT  FROM  SALE ");
                sbQuery.Append(" WHERE SL_NO=" + Convert.ToInt32(unitSaleObj.SaleNo.ToString()) + " AND REG_BK='" + unitRegObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + unitRegObj.BranchCode.ToString() + "' AND REG_NO=" + Convert.ToInt32(unitRegObj.RegNumber));
                commonGatewayObj.ExecuteNonQuery(sbQuery.ToString());
                commonGatewayObj.ExecuteNonQuery(" UPDATE MONEY_RECEIPT SET SL_REP_TR_RN_NO= NULL WHERE RECEIPT_TYPE='SL' AND RECEIPT_NO=" + Convert.ToInt32(unitSaleObj.MoneyReceiptNo) + " AND REG_BR='" + unitRegObj.BranchCode.ToString() + "' AND REG_BK='" + unitRegObj.FundCode.ToString()+"'");
                commonGatewayObj.CommitTransaction();
            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }
        }
        public DataTable getTableDinoForEdit()
        {
            DataTable dtDinomination = new DataTable();
            dtDinomination.Columns.Add("dino", typeof(string));
            dtDinomination.Columns.Add("cert_no", typeof(string));
            dtDinomination.Columns.Add("cert_weight", typeof(string));
            dtDinomination.Columns.Add("status", typeof(string));
            dtDinomination.Columns.Add("ROWID", typeof(string));
            return dtDinomination;

        }
        public DataTable dtGetExistingSaleCertBySaleNo(UnitHolderRegistration unitRegObj, UnitSale unitSaleObj)
        {
            DataTable dtDinomination = new DataTable();
            DataTable dtSaleCertInfo = commonGatewayObj.Select("SELECT * FROM SALE_CERT WHERE SL_NO=" + unitSaleObj.SaleNo + " AND REG_BK='" + unitRegObj.FundCode.ToString() + "' AND REG_BR='" + unitRegObj.BranchCode.ToString() + "' AND REG_NO=" + unitRegObj.RegNumber);
            DataTable dtSaleCertWithStatusCheck= commonGatewayObj.Select("SELECT * FROM SALE_CERT WHERE SL_NO=" + unitSaleObj.SaleNo + " AND REG_BK='" + unitRegObj.FundCode.ToString() + "' AND REG_BR='" + unitRegObj.BranchCode.ToString() + "' AND REG_NO=" + unitRegObj.RegNumber+" AND VALID IS NULL AND STATUS_FLAG IS NULL ");
            if (dtSaleCertInfo.Rows.Count == dtSaleCertWithStatusCheck.Rows.Count)
            {
                dtDinomination = dtSaleCertInfo;
            }
            return dtDinomination;
        }
        public DataTable dtGetSaleCertBySaleNo(UnitHolderRegistration unitRegObj, UnitSale unitSaleObj)
        {
           // DataTable dtDinomination = new DataTable();
            DataTable dtDinomination = commonGatewayObj.Select("SELECT * FROM SALE_CERT WHERE SL_NO=" + unitSaleObj.SaleNo + " AND REG_BK='" + unitRegObj.FundCode.ToString() + "' AND REG_BR='" + unitRegObj.BranchCode.ToString() + "' AND REG_NO=" + unitRegObj.RegNumber);
           
            return dtDinomination;
        }
        public bool IsSaleLock(UnitHolderRegistration regObj)
        {
            bool lockStatus = false;
            DataTable dtLockStatus = commonGatewayObj.Select("SELECT NVL(ALL_LOCK,'N') AS ALL_LOCK, NVL(SL_LOCK,'N') AS SL_LOCK FROM U_MASTER WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO=" + regObj.RegNumber);
            if (dtLockStatus.Rows.Count > 0)
            {
                if (dtLockStatus.Rows[0]["ALL_LOCK"].ToString() == "Y" || dtLockStatus.Rows[0]["SL_LOCK"].ToString() == "Y")
                {
                    lockStatus = true;
                }
            }
            return lockStatus;
        }
       

        //Demate and CDS Related Methods////
        public DataTable dtGetSaleInfoForDRF(string filter)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT  U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, SALE.SL_NO, U_MASTER.HNAME, U_MASTER.BO, SALE.QTY, SALE.SL_PRICE, SALE.QTY * SALE.SL_PRICE AS TOTAL_AMT");
            sbQuery.Append(" FROM U_MASTER, SALE WHERE U_MASTER.REG_BK = SALE.REG_BK AND U_MASTER.REG_BR = SALE.REG_BR AND U_MASTER.REG_NO = SALE.REG_NO AND (SALE.DRF_REQ_DATE IS NULL)  AND (SALE.DRF_REF_NO IS NULL) AND  U_MASTER.BO IS NOT NULL ");
            sbQuery.Append(" AND (SALE.FOLIO_TR_REQ_DATE IS NULL) ");
            sbQuery.Append(filter.ToString());
            sbQuery.Append(" ORDER BY SALE.SL_NO");
            DataTable dtSaleInfoForDRF = commonGatewayObj.Select(sbQuery.ToString());
            return dtSaleInfoForDRF;

        }
        public DataTable dtGetSaleInfoForFolioTransfer(string filter)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT  U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, SALE.SL_NO, U_MASTER.HNAME, U_MASTER.BO, SALE.QTY, SALE.SL_PRICE, SALE.QTY * SALE.SL_PRICE AS TOTAL_AMT,FUND_INFO.OMNIBUS_FOLIO_BO ");
            sbQuery.Append(" FROM U_MASTER, SALE,FUND_INFO WHERE U_MASTER.REG_BK = SALE.REG_BK AND U_MASTER.REG_BR = SALE.REG_BR AND U_MASTER.REG_NO = SALE.REG_NO AND U_MASTER.REG_BK = FUND_INFO.FUND_CD  ");
            sbQuery.Append(" AND (SALE.DRF_TR_SEQ_NO IS  NULL) AND (SALE.DRF_REQ_DATE IS NULL)  AND (SALE.DRF_REF_NO IS NULL) AND  U_MASTER.BO IS NOT NULL  AND (SALE.FOLIO_TR_REQ_DATE IS NULL) ");
            sbQuery.Append(filter.ToString());
            sbQuery.Append(" ORDER BY SALE.SL_NO");
            DataTable dtSaleInfoForDRF = commonGatewayObj.Select(sbQuery.ToString());
            return dtSaleInfoForDRF;

        }
        public long getNexDRReferenceNumber(string filter)
        {
            long nextDRRefNumber = 0;
            DataTable dtDRRefNo = commonGatewayObj.Select("SELECT MAX(NVL(DRF_REF_NO,0))+1 AS NEXDREFNO  FROM SALE WHERE 1=1 " + filter.ToString());
            if (dtDRRefNo.Rows.Count > 0)
            {
                nextDRRefNumber = Convert.ToInt64(dtDRRefNo.Rows[0]["NEXDREFNO"].ToString());
            }
                return nextDRRefNumber;


        }
        public bool CheckDuplicateSaleRelatedInfo(string filter)
        {
            bool check = false;
            DataTable dtCheck = commonGatewayObj.Select("SELECT * FROM SALE WHERE (1=1) " + filter.ToString());
            if(dtCheck.Rows.Count>0)
            {
                check = true;
            }
            return check;
        }
        public DataTable dtDRRefNoListForCustodian(string filter)
        {
            DataTable dtList = commonGatewayObj.Select("SELECT DISTINCT DRF_REF_NO AS NAME,DRF_REF_NO AS ID FROM SALE WHERE DRF_REF_NO IS NOT NULL AND DRF_CUST_REQ_DATE IS NULL AND DRF_CUST_REQ_BY IS  NULL AND DRF_REG_FOLIO_NO IS NULL AND DRF_CERT_NO IS NULL AND DRF_DISTNCT_NO_FROM IS NULL AND DRF_DISTNCT_NO_TO IS NULL AND FOLIO_TR_REQ_DATE IS NULL " + filter.ToString()+ " ORDER BY ID ");

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
        public DataTable dtGetSaleInfoForDemate(string filter)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT SALE.*, U_MASTER.HNAME FROM U_MASTER, SALE WHERE U_MASTER.REG_BK = SALE.REG_BK AND U_MASTER.REG_BR = SALE.REG_BR AND U_MASTER.REG_NO = SALE.REG_NO");           
            sbQuery.Append(filter.ToString());
            sbQuery.Append(" ORDER BY SALE.SL_NO");
            DataTable dtSaleInfoForCusto = commonGatewayObj.Select(sbQuery.ToString());
            return dtSaleInfoForCusto;

        }
        public long getTotalUnitsForDRF(string filter)
        {
            long totalUnits = 0;
            DataTable dtTotalUnits = commonGatewayObj.Select("SELECT SUM(QTY) AS TOTAL_QTY  FROM SALE WHERE 1=1 " + filter.ToString());
            if (dtTotalUnits.Rows.Count > 0)
            {
                totalUnits = Convert.ToInt64(dtTotalUnits.Rows[0]["TOTAL_QTY"].Equals(DBNull.Value) ? 0 : dtTotalUnits.Rows[0]["TOTAL_QTY"]);
            }
            return totalUnits;


        }
        public DataTable dtDRRefNoListForDRN(string filter)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT DISTINCT DRF_REF_NO AS NAME,DRF_REF_NO AS ID FROM SALE WHERE DRF_REF_NO IS NOT NULL AND DRF_CUST_REQ_DATE IS NOT NULL AND DRF_CUST_REQ_BY IS NOT NULL AND DRF_REG_FOLIO_NO IS NOT NULL AND DRF_CERT_NO IS NOT NULL AND DRF_DISTNCT_NO_FROM IS NOT NULL AND DRF_DISTNCT_NO_TO IS NOT NULL ");
            sbQuery.Append(" AND DRF_ACCEPT_NO IS  NULL AND DRF_DRN IS  NULL AND DRF_ACCEPT_DATE IS  NULL AND FOLIO_TR_REQ_DATE IS NULL " + filter.ToString() + " ORDER BY ID ");
            DataTable dtList = commonGatewayObj.Select(sbQuery.ToString());

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
        public DataTable dtDRRefNoListForTransfer(string filter)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT DISTINCT DRF_REF_NO AS NAME,DRF_REF_NO AS ID FROM SALE WHERE DRF_REF_NO IS NOT NULL AND DRF_CUST_REQ_DATE IS NOT NULL AND DRF_CUST_REQ_BY IS NOT NULL AND DRF_REG_FOLIO_NO IS NOT NULL AND DRF_CERT_NO IS NOT NULL AND DRF_DISTNCT_NO_FROM IS NOT NULL AND DRF_DISTNCT_NO_TO IS NOT NULL ");
            sbQuery.Append(" AND DRF_ACCEPT_NO IS NOT NULL AND DRF_DRN IS NOT NULL AND DRF_ACCEPT_DATE IS NOT NULL AND DRF_TR_DATE IS NULL AND DRF_TR_SEQ_NO IS NULL  AND FOLIO_TR_REQ_DATE IS NULL " + filter.ToString() + " ORDER BY ID ");
            DataTable dtList = commonGatewayObj.Select(sbQuery.ToString());

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
        public DataTable dtDRRefNoListForFolioTransfer(string filter)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT DISTINCT DRF_REF_NO AS NAME,DRF_REF_NO AS ID FROM SALE WHERE DRF_REF_NO IS NOT NULL AND DRF_CUST_REQ_DATE IS  NULL AND DRF_CUST_REQ_BY IS  NULL AND DRF_REG_FOLIO_NO IS  NULL AND DRF_CERT_NO IS  NULL AND DRF_DISTNCT_NO_FROM IS  NULL AND DRF_DISTNCT_NO_TO IS  NULL ");
            sbQuery.Append(" AND DRF_ACCEPT_NO IS  NULL AND DRF_DRN IS  NULL AND DRF_ACCEPT_DATE IS  NULL AND DRF_TR_DATE IS NULL AND DRF_TR_SEQ_NO IS NULL  AND FOLIO_TR_REQ_DATE IS NOT NULL " + filter.ToString() + " ORDER BY ID ");
            DataTable dtList = commonGatewayObj.Select(sbQuery.ToString());

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
        public DataTable dtGetSaleInfoForFolioTransferPrint(string filter)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT  U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, SALE.SL_NO, U_MASTER.HNAME, U_MASTER.BO, SALE.QTY, SALE.SL_PRICE, SALE.QTY * SALE.SL_PRICE AS TOTAL_AMT,FUND_INFO.OMNIBUS_FOLIO_BO ");
            sbQuery.Append(" FROM U_MASTER, SALE,FUND_INFO WHERE U_MASTER.REG_BK = SALE.REG_BK AND U_MASTER.REG_BR = SALE.REG_BR AND U_MASTER.REG_NO = SALE.REG_NO AND U_MASTER.REG_BK = FUND_INFO.FUND_CD  ");
            sbQuery.Append(" AND (SALE.DRF_TR_SEQ_NO IS  NULL) AND (SALE.DRF_REQ_DATE IS  NULL)  AND (SALE.DRF_REF_NO IS NOT NULL) AND  U_MASTER.BO IS NOT NULL  AND (SALE.FOLIO_TR_REQ_DATE IS NOT NULL) ");
            sbQuery.Append(filter.ToString());
            sbQuery.Append(" ORDER BY SALE.SL_NO");
            DataTable dtSaleInfoForDRF = commonGatewayObj.Select(sbQuery.ToString());
            return dtSaleInfoForDRF;

        }


        // Money Receipt////////////
        public long GetMaxReceiptNo(UnitHolderRegistration regObj, string receipt_type)
        {
            DataTable dtMaxReceiptNo = new DataTable();
            dtMaxReceiptNo = commonGatewayObj.Select("SELECT MAX(NVL(RECEIPT_NO,0)) AS MAX_RECEIPT_NO FROM MONEY_RECEIPT WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'AND RECEIPT_TYPE='" + receipt_type.ToUpper() + "' AND VALID IS NULL ");
            long MaxReceiptNo = dtMaxReceiptNo.Rows[0]["MAX_RECEIPT_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt64(dtMaxReceiptNo.Rows[0]["MAX_RECEIPT_NO"].ToString());
            return MaxReceiptNo + 1;
        }
        public decimal GetLastPrice(UnitHolderRegistration regObj, string price_type)
        {
            decimal price = 0;
           DataTable dtLastPrice = commonGatewayObj.Select("SELECT * FROM PRICE_REFIX WHERE FUND_CD='" + regObj.FundCode.ToString() + "'  AND REFIX_DT=(SELECT MAX (REFIX_DT) FROM PRICE_REFIX WHERE FUND_CD='" + regObj.FundCode.ToString().ToUpper() + "') ");
           if(price_type.ToUpper()=="SL")
            {
                price = dtLastPrice.Rows[0]["REFIX_SL_PR"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(dtLastPrice.Rows[0]["REFIX_SL_PR"].ToString());
            }
           else if (price_type.ToUpper() == "REP")
            {
                price = dtLastPrice.Rows[0]["REFIX_REP_PR"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(dtLastPrice.Rows[0]["REFIX_REP_PR"].ToString());
            }
            return price;
        }
        public decimal GetDateWisePrice(UnitHolderRegistration regObj, string tranType, string tranDate)
        {
            decimal price = 0;
            DataTable dtLastPrice = commonGatewayObj.Select("SELECT * FROM PRICE_REFIX WHERE FUND_CD='" + regObj.FundCode.ToString() + "'  AND REFIX_DT<='"+ tranDate + "' ORDER BY REFIX_DT DESC");
            if (tranType.ToUpper() == "SL")
            {
                price = dtLastPrice.Rows[0]["REFIX_SL_PR"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(dtLastPrice.Rows[0]["REFIX_SL_PR"].ToString());
            }
            else if (tranType.ToUpper() == "REP")
            {
                price = dtLastPrice.Rows[0]["REFIX_REP_PR"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(dtLastPrice.Rows[0]["REFIX_REP_PR"].ToString());
            }
            return price;
        }
        public bool CheckDuplicateMoneyReceiptNo(UnitHolderRegistration regObj, string receipt_type, long receiptNo)
        {
            bool check = false;
            DataTable dtCheck = commonGatewayObj.Select("SELECT * FROM MONEY_RECEIPT WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'AND RECEIPT_NO="+ receiptNo + " AND RECEIPT_TYPE='" + receipt_type.ToUpper() + "' AND VALID IS NULL ");
            if (dtCheck.Rows.Count > 0)
            {
                check = true;
            }
            return check;
        }
       
        public DataTable dtMoneyReceiptInfo(string filter)
        {
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT TO_CHAR(MONEY_RECEIPT.CHQ_DD_DATE,'DD-MON-YYYY') AS CHQ_DATE, TO_CHAR(MONEY_RECEIPT.RECEIPT_DATE,'DD-MON-YYYY') AS SALE_DATE, MONEY_RECEIPT.UNIT_QTY * MONEY_RECEIPT.RATE AS TOTAL_AMT, MONEY_RECEIPT.*,  FUND_INFO.*  FROM   FUND_INFO, MONEY_RECEIPT ");
            sbQuery.Append(" WHERE  VALID IS NULL AND MONEY_RECEIPT.ACC_VOUCHER_NO IS NULL AND FUND_INFO.FUND_CD = MONEY_RECEIPT.REG_BK " + filter.ToString());
            sbQuery.Append(" ORDER BY MONEY_RECEIPT.REG_BK,MONEY_RECEIPT.RECEIPT_NO ");           
            DataTable dtMoneyReceiptInfo = commonGatewayObj.Select(sbQuery.ToString());
            return dtMoneyReceiptInfo;

        }
        public string getNexAccountVoucherNo(string accountSchema, string voucherTypeNo)
        {
            int accVoucherNo = 0;
            DataTable dtAccountVoucherNo = commonGatewayObj.Select("SELECT VOUCHER_NO FROM " + accountSchema + ".GL_TRAN WHERE (CTRLNO=(SELECT MAX(TO_NUMBER(CTRLNO))MAX_CTRLNO FROM " + accountSchema + ".GL_TRAN GL_TRAN_1 WHERE (VOUCHER_TYPE='" + voucherTypeNo + "'))) ");
            if (dtAccountVoucherNo.Rows.Count > 0)
            {
                string voucherNo = dtAccountVoucherNo.Rows[0]["VOUCHER_NO"].ToString().ToUpper();
                string[] wordVoucher = voucherNo.Split('/');
                accVoucherNo = Convert.ToInt32(wordVoucher[0].ToString()) + 1;
            }
            else
            {
                accVoucherNo = 1;
            }
            return accVoucherNo.ToString();

        }      
        public string getNexAccountVoucherNo(string accountSchema, string voucherTypeNo, string yearStartDate, string yearEndDate)
        {
            int accVoucherNo = 0;
            DataTable dtAccountVoucherNo = commonGatewayObj.Select("SELECT   MAX(VOUCHER_NO) VOUCHER_NO FROM " + accountSchema + ".GL_TRAN WHERE (VOUCHER_TYPE='" + voucherTypeNo + "') AND  TRAN_DATE BETWEEN '" + yearStartDate + "' AND '" + yearEndDate + "'");
            if (dtAccountVoucherNo.Rows.Count > 0)
            {
                if (!dtAccountVoucherNo.Rows[0]["VOUCHER_NO"].Equals(DBNull.Value))
                {
                    string voucherNo = dtAccountVoucherNo.Rows[0]["VOUCHER_NO"].ToString().ToUpper();
                    accVoucherNo = Convert.ToInt32(voucherNo) + 1;
                }
                else
                {
                    accVoucherNo = 1;
                }
            }
            else
            {
                accVoucherNo = 1;
            }
            return accVoucherNo.ToString();

        }
        public DataTable dtMoneyRecieptInfoforDDL(UnitHolderRegistration regObj, string receipt_type)
        {
            DataTable dtMoneyRecieptInfo = commonGatewayObj.Select("SELECT ID,RECEIPT_NO FROM MONEY_RECEIPT WHERE VALID IS NULL AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'AND RECEIPT_TYPE='" + receipt_type.ToUpper() + "' AND SL_REP_TR_RN_NO IS NULL AND ACC_VOUCHER_NO IS NOT NULL ORDER BY RECEIPT_NO");
            DataTable dtDropDown = new DataTable();
            dtDropDown.Columns.Add("ID", typeof(string));
            dtDropDown.Columns.Add("RECEIPT_NO", typeof(string));

            DataRow drdtDropDownn = dtDropDown.NewRow();

            drdtDropDownn["RECEIPT_NO"] = "-Select-";
            drdtDropDownn["ID"] = "0";
            dtDropDown.Rows.Add(drdtDropDownn);
            for (int loop = 0; loop < dtMoneyRecieptInfo.Rows.Count; loop++)
            {
                drdtDropDownn = dtDropDown.NewRow();
                drdtDropDownn["RECEIPT_NO"] = dtMoneyRecieptInfo.Rows[loop]["RECEIPT_NO"].ToString();
                drdtDropDownn["ID"] = dtMoneyRecieptInfo.Rows[loop]["ID"].ToString();
                dtDropDown.Rows.Add(drdtDropDownn);
            }
            return dtDropDown;
        }
        public DataTable dtMoneyRecieptforDDL(string filter)
        {
            DataTable dtMoneyRecieptInfo = commonGatewayObj.Select("SELECT ID,RECEIPT_NO FROM MONEY_RECEIPT WHERE VALID IS NULL  "+filter.ToString());
            DataTable dtDropDown = new DataTable();
            dtDropDown.Columns.Add("ID", typeof(string));
            dtDropDown.Columns.Add("RECEIPT_NO", typeof(string));

            DataRow drdtDropDownn = dtDropDown.NewRow();

            drdtDropDownn["RECEIPT_NO"] = "-Select-";
            drdtDropDownn["ID"] = "0";
            dtDropDown.Rows.Add(drdtDropDownn);
            for (int loop = 0; loop < dtMoneyRecieptInfo.Rows.Count; loop++)
            {
                drdtDropDownn = dtDropDown.NewRow();
                drdtDropDownn["RECEIPT_NO"] = dtMoneyRecieptInfo.Rows[loop]["RECEIPT_NO"].ToString();
                drdtDropDownn["ID"] = dtMoneyRecieptInfo.Rows[loop]["ID"].ToString();
                dtDropDown.Rows.Add(drdtDropDownn);
            }
            return dtDropDown;
        }
        public DataTable dtMoneyRecieptInfoDetails(long ID)
        {
            DataTable dtMoneyRecieptInfo = commonGatewayObj.Select("SELECT * FROM MONEY_RECEIPT WHERE VALID IS NULL AND ID=" + ID);
            return dtMoneyRecieptInfo;
        }
        public DataTable dtUserInfoByUserID(UnitUser userObj)
        {
            DataTable dtUserInfoByUserID = commonGatewayObj.Select(" SELECT * FROM USER_INFO WHERE USER_STATUS='V' AND USER_ID='" + userObj.UserID + "'");
            return dtUserInfoByUserID;
        }
        public DataTable dtSellingAgentInfoforDDL()
        {
            DataTable dtMoneyRecieptInfo = commonGatewayObj.Select("SELECT ID, NAME||'['||ID||']' AS NAME FROM SELLING_AGENT_INFO ORDER BY ID ");
            DataTable dtDropDown = new DataTable();
            dtDropDown.Columns.Add("ID", typeof(string));
            dtDropDown.Columns.Add("NAME", typeof(string));

            DataRow drdtDropDownn = dtDropDown.NewRow();

            drdtDropDownn["NAME"] = "-Select-";
            drdtDropDownn["ID"] = "0";
            dtDropDown.Rows.Add(drdtDropDownn);
            for (int loop = 0; loop < dtMoneyRecieptInfo.Rows.Count; loop++)
            {
                drdtDropDownn = dtDropDown.NewRow();
                drdtDropDownn["NAME"] = dtMoneyRecieptInfo.Rows[loop]["NAME"].ToString();
                drdtDropDownn["ID"] = dtMoneyRecieptInfo.Rows[loop]["ID"].ToString();
                dtDropDown.Rows.Add(drdtDropDownn);
            }
            return dtDropDown;
        }


    }
}