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
namespace AMCL.BL
{
    public class UnitLienBl
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        OMFDAO opendMFDAO = new OMFDAO();

        public UnitLienBl()
        {
            
        }

        public bool IsDuplicateLienCancel(UnitHolderRegistration regObj, UnitLien unitLienObj)
        {
            DataTable dtLien = new DataTable();
            dtLien = commonGatewayObj.Select("SELECT * FROM LIEN_MARK_CANCEL WHERE  REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND LIEN_CANCL_NO='"+ unitLienObj.LienCancelNo.ToString()+"'");
            if (dtLien.Rows.Count > 0)
                return true;
            else
                return false;

        }
        public bool IsDuplicateLien(UnitHolderRegistration regObj, UnitLien unitLienObj)
        {
            DataTable dtLien = new DataTable();
            dtLien = commonGatewayObj.Select("SELECT * FROM LIEN_MARK WHERE  REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND LIEN_NO='" +unitLienObj.LienNo.ToString()+"'");
            if (dtLien.Rows.Count > 0)
                return true;
            else
                return false;

        }        
        public int getNextLienNo(UnitHolderRegistration regObj, UnitUser unitUserObj)
        {
            int nextLienNo = 0;
            DataTable dtNextLienNo = new DataTable();

            dtNextLienNo = commonGatewayObj.Select("SELECT MAX(TO_NUMBER(LIEN_NO)) AS LIEN_NO FROM LIEN_MARK WHERE  REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND USER_NM='" + unitUserObj.UserID.ToString() + "'");

            if (!dtNextLienNo.Rows[0]["LIEN_NO"].Equals(DBNull.Value))
            {
                nextLienNo = Convert.ToInt32(dtNextLienNo.Rows[0]["LIEN_NO"].ToString());
            }
            else
            {
                dtNextLienNo = new DataTable();
                dtNextLienNo = commonGatewayObj.Select("SELECT MAX(TO_NUMBER(LIEN_NO)) AS LIEN_NO FROM LIEN_MARK WHERE  REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'");
                nextLienNo = dtNextLienNo.Rows[0]["LIEN_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtNextLienNo.Rows[0]["LIEN_NO"].ToString());
            }
            return nextLienNo + 1;

        }
        public int getNextLienCancelNo(UnitHolderRegistration regObj, UnitUser unitUserObj)
        {
            int nextLienNo = 0;
            DataTable dtNextLienNo = new DataTable();

            dtNextLienNo = commonGatewayObj.Select("SELECT MAX(TO_NUMBER(LIEN_CANCL_NO)) AS LIEN_CANCL_NO FROM LIEN_MARK_CANCEL WHERE  REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND USER_NM='" + unitUserObj.UserID.ToString() + "'");

            if (!dtNextLienNo.Rows[0]["LIEN_CANCL_NO"].Equals(DBNull.Value))
            {
                nextLienNo = Convert.ToInt32(dtNextLienNo.Rows[0]["LIEN_CANCL_NO"].ToString());
            }
            else
            {
                dtNextLienNo = new DataTable();
                dtNextLienNo = commonGatewayObj.Select("SELECT MAX(TO_NUMBER(LIEN_CANCL_NO)) AS LIEN_CANCL_NO FROM LIEN_MARK_CANCEL WHERE  REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'");
                nextLienNo = dtNextLienNo.Rows[0]["LIEN_CANCL_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtNextLienNo.Rows[0]["LIEN_CANCL_NO"].ToString());
            }
            return nextLienNo + 1;

        }      
        public void saveLienMark(DataTable dtTransferSaleCert, UnitHolderRegistration regObj, UnitLien unitLienObj, UnitUser unitUserObj)
        {
            try
            {
                UnitTransferBL unitTransferBLObj = new UnitTransferBL();
                commonGatewayObj.BeginTransaction();
                Hashtable htLienRegInfo = new Hashtable();
                Hashtable htCertNoTemp = new Hashtable();
                int SL_TR_RN = 0;
                int CertNo = 0;
                string certType = "";
                string[] saleNoArray;
                string[] certArray;
                string statusFlag = "L";
                string Old_SL_TR_RN = "";

                for (int loop = 0; loop < dtTransferSaleCert.Rows.Count; loop++)
                {


                    if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('S') >= 0)
                    {
                        htLienRegInfo = new Hashtable();
                        htLienRegInfo.Add("CURR_REG_BK", regObj.FundCode.ToString().ToUpper());
                        htLienRegInfo.Add("CURR_REG_BR", regObj.BranchCode.ToString().ToUpper());
                        htLienRegInfo.Add("CURR_REG_NO", regObj.RegNumber.ToString());
                        htLienRegInfo.Add("STATUS_FLAG", statusFlag.ToUpper().ToString());

                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('S');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();
                        commonGatewayObj.Update(htLienRegInfo, "SALE_CERT", "REG_NO=" + Convert.ToInt32(regObj.RegNumber) + " AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND  SL_NO=" + SL_TR_RN + " AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "S" + SL_TR_RN.ToString();
                    }
                    else if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('T') >= 0)
                    {
                        htLienRegInfo = new Hashtable();
                        htLienRegInfo.Add("CURR_REG_BK", regObj.FundCode.ToString().ToUpper());
                        htLienRegInfo.Add("CURR_REG_BR", regObj.BranchCode.ToString().ToUpper());
                        htLienRegInfo.Add("CURR_REG_NO", regObj.RegNumber.ToString());
                        htLienRegInfo.Add("STATUS_FLAG", statusFlag.ToUpper().ToString());


                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('T');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();


                        commonGatewayObj.Update(htLienRegInfo, "TRANS_CERT", "F_CD='" + regObj.FundCode.ToString().ToUpper() + "' AND TR_NO=" + SL_TR_RN + "  AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");
                        //commonGatewayObj.Update(htLienRegInfo, "SALE_CERT", "CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");
                        //commonGatewayObj.Update(htLienRegInfo, "RENEWAL_OUT", "CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "T" + SL_TR_RN.ToString();

                    }
                    else if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('R') >= 0)
                    {
                        htLienRegInfo = new Hashtable();
                        htLienRegInfo.Add("CURR_REG_BK", regObj.FundCode.ToString().ToUpper());
                        htLienRegInfo.Add("CURR_REG_BR", regObj.BranchCode.ToString().ToUpper());
                        htLienRegInfo.Add("CURR_REG_NO", regObj.RegNumber.ToString());
                        htLienRegInfo.Add("STATUS_FLAG", statusFlag.ToUpper().ToString());


                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('R');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();

                        commonGatewayObj.Update(htLienRegInfo, "RENEWAL_OUT", "REG_NO=" + Convert.ToInt32(regObj.RegNumber) + " AND REN_NO='" + SL_TR_RN + "' AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "R" + SL_TR_RN.ToString();
                    }


                    htLienRegInfo = new Hashtable();
                    htLienRegInfo.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htLienRegInfo.Add("REG_BR", regObj.BranchCode.ToString().ToUpper());
                    htLienRegInfo.Add("REG_NO", regObj.RegNumber.ToString());
                    htLienRegInfo.Add("LIEN_NO", Convert.ToInt32(unitLienObj.LienNo.ToString()));

                    certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');

                    htLienRegInfo.Add("CERT_TYPE", certArray[0].ToString().ToUpper());
                    htLienRegInfo.Add("CERT_NO", Convert.ToInt32(certArray[1].ToString()));
                    htLienRegInfo.Add("CERTIFICATE", dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().ToUpper());
                    htLienRegInfo.Add("QTY", Convert.ToInt32(dtTransferSaleCert.Rows[loop]["QTY"].ToString()));
                    htLienRegInfo.Add("SL_TR_NO", Old_SL_TR_RN.ToString().ToUpper());

                    commonGatewayObj.Insert(htLienRegInfo, "LIEN_MARK_CERT_NO");
                }





                DataTable dtTransfer = unitTransferBLObj.dtTrnasfer(dtTransferSaleCert);
                for (int i = 0; i < dtTransfer.Rows.Count; i++)
                {
                   
                    htLienRegInfo = new Hashtable();
                    htLienRegInfo.Add("LIEN_NO", Convert.ToInt32(unitLienObj.LienNo.ToString()));
                    htLienRegInfo.Add("LIEN_DT", Convert.ToDateTime(unitLienObj.LienReqDate.ToString()).ToString("dd-MMM-yyyy"));
                    htLienRegInfo.Add("LN_REQ_REF",unitLienObj.LienRefference.ToString());
                    htLienRegInfo.Add("LN_BK_CODE", Convert.ToInt16(unitLienObj.LienInst.ToString()));
                    htLienRegInfo.Add("LN_BK_BR_CODE", Convert.ToInt16(unitLienObj.LienInstBranch.ToString()));
                    

                    htLienRegInfo.Add("SL_TR_NO", dtTransfer.Rows[i]["SL_NO"].ToString());
                    htLienRegInfo.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htLienRegInfo.Add("REG_BR", regObj.BranchCode.ToString().ToUpper());
                    htLienRegInfo.Add("REG_NO", regObj.RegNumber.ToString());                   
                    htLienRegInfo.Add("QTY", Convert.ToInt32(dtTransfer.Rows[i]["QTY"].ToString()));

                    htLienRegInfo.Add("USER_NM", unitUserObj.UserID.ToString());
                    htLienRegInfo.Add("ENT_DT", DateTime.Now.ToString());
                    htLienRegInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());

                    commonGatewayObj.Insert(htLienRegInfo, "LIEN_MARK");
                }
                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }
        }
        public void saveLienMarkCancel(DataTable dtTransferSaleCert, UnitHolderRegistration regObj, UnitLien unitLienObj, UnitUser unitUserObj)
        {
            try
            {
                UnitTransferBL unitTransferBLObj = new UnitTransferBL();
                commonGatewayObj.BeginTransaction();
                Hashtable htLienRegInfo = new Hashtable();
                Hashtable htCertNoTemp = new Hashtable();
                Hashtable htLienCancel = new Hashtable();
                int SL_TR_RN = 0;
                int CertNo = 0;
                string certType = "";
                string[] saleNoArray;
                string[] certArray;
                //string statusFlag = "L";
                string Old_SL_TR_RN = "";

                for (int loop = 0; loop < dtTransferSaleCert.Rows.Count; loop++)
                {


                    if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('S') >= 0)
                    {
                        htLienRegInfo = new Hashtable();
                       // htLienRegInfo.Add("CURR_REG_BK", regObj.FundCode.ToString().ToUpper());
                      //  htLienRegInfo.Add("CURR_REG_BR", regObj.BranchCode.ToString().ToUpper());
                     //   htLienRegInfo.Add("CURR_REG_NO", regObj.RegNumber.ToString());
                        htLienRegInfo.Add("STATUS_FLAG", DBNull.Value);

                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('S');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();
                        commonGatewayObj.Update(htLienRegInfo, "SALE_CERT", "REG_NO=" + Convert.ToInt32(regObj.RegNumber) + " AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND SL_NO=" + SL_TR_RN + " AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "S" + SL_TR_RN.ToString();
                    }
                    else if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('T') >= 0)
                    {
                        htLienRegInfo = new Hashtable();
                        //htLienRegInfo.Add("CURR_REG_BK", regObj.FundCode.ToString().ToUpper());
                        //htLienRegInfo.Add("CURR_REG_BR", regObj.BranchCode.ToString().ToUpper());
                        //htLienRegInfo.Add("CURR_REG_NO", regObj.RegNumber.ToString());
                        htLienRegInfo.Add("STATUS_FLAG", DBNull.Value);


                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('T');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();


                        commonGatewayObj.Update(htLienRegInfo, "TRANS_CERT", "F_CD='" + regObj.FundCode.ToString().ToUpper() + "' AND TR_NO=" + SL_TR_RN + "   AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");
                        //commonGatewayObj.Update(htLienRegInfo, "SALE_CERT", " REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND SL_NO=" + SL_TR_RN + " AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");
                        //commonGatewayObj.Update(htLienRegInfo, "RENEWAL_OUT", "REN_NO='" + SL_TR_RN + "' AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "T" + SL_TR_RN.ToString();

                    }
                    else if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('R') >= 0)
                    {
                        htLienRegInfo = new Hashtable();
                        //htLienRegInfo.Add("CURR_REG_BK", regObj.FundCode.ToString().ToUpper());
                        //htLienRegInfo.Add("CURR_REG_BR", regObj.BranchCode.ToString().ToUpper());
                        //htLienRegInfo.Add("CURR_REG_NO", regObj.RegNumber.ToString());
                        htLienRegInfo.Add("STATUS_FLAG", DBNull.Value);


                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('R');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();

                        commonGatewayObj.Update(htLienRegInfo, "RENEWAL_OUT", "REG_NO=" + Convert.ToInt32(regObj.RegNumber) + " AND REN_NO='" + SL_TR_RN + "' AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "R" + SL_TR_RN.ToString();
                    }


                    htLienRegInfo = new Hashtable();                   

                    htLienRegInfo.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htLienRegInfo.Add("REG_BR", regObj.BranchCode.ToString().ToUpper());
                    htLienRegInfo.Add("REG_NO", regObj.RegNumber.ToString());
                    htLienRegInfo.Add("LIEN_CANCL_NO", unitLienObj.LienCancelNo.ToString());
                    htLienRegInfo.Add("LIEN_NO", unitLienObj.LienNo.ToString());
                    certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                    htLienRegInfo.Add("CERT_TYPE", certArray[0].ToString().ToUpper());
                    htLienRegInfo.Add("CERT_NO", Convert.ToInt32(certArray[1].ToString()));
                    htLienRegInfo.Add("CERTIFICATE", dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().ToUpper());
                    htLienRegInfo.Add("QTY", Convert.ToInt32(dtTransferSaleCert.Rows[loop]["QTY"].ToString()));
                    htLienRegInfo.Add("SL_TR_NO", Old_SL_TR_RN.ToString().ToUpper());

                    commonGatewayObj.Insert(htLienRegInfo, "LIEN_MARK_CANCEL_CERT_NO");
                    htLienCancel = new Hashtable();
                    htLienCancel.Add("LN_MK_CANCEL", "Y");
                    commonGatewayObj.Update(htLienCancel, "LIEN_MARK_CERT_NO", "LIEN_NO='" + unitLienObj.LienNo.ToString() + "'AND REG_NO="+Convert.ToInt32(regObj.RegNumber.ToString())+" AND  REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND CERT_TYPE='" + certArray[0].ToString().ToUpper() + "' AND CERT_NO=" + Convert.ToInt32(certArray[1].ToString()));

                   

                }

                DataTable dtTransfer = unitTransferBLObj.dtTrnasfer(dtTransferSaleCert);
                for (int i = 0; i < dtTransfer.Rows.Count; i++)
                {

                    htLienRegInfo = new Hashtable();
                    htLienRegInfo.Add("LIEN_CANCL_NO", unitLienObj.LienCancelNo.ToString());
                    htLienRegInfo.Add("LIEN_NO", unitLienObj.LienNo.ToString());
                    htLienRegInfo.Add("LN_CAN_LTR_DT", Convert.ToDateTime(unitLienObj.LienCancelReqDate.ToString()).ToString("dd-MMM-yyyy"));
                    htLienRegInfo.Add("LIEN_CANCL_DT", Convert.ToDateTime(unitLienObj.LienCancelDate.ToString()).ToString("dd-MMM-yyyy"));

                    htLienRegInfo.Add("LN_CAN_LTR_REF", unitLienObj.LienRefference.ToString());
                    htLienRegInfo.Add("LN_BK_CODE", Convert.ToInt16(unitLienObj.LienInst.ToString()));
                    htLienRegInfo.Add("LN_BK_BR_CODE", Convert.ToInt16(unitLienObj.LienInstBranch.ToString()));


                    htLienRegInfo.Add("SL_TR_NO", dtTransfer.Rows[i]["SL_NO"].ToString());
                    htLienRegInfo.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htLienRegInfo.Add("REG_BR", regObj.BranchCode.ToString().ToUpper());
                    htLienRegInfo.Add("REG_NO", regObj.RegNumber.ToString());
                    htLienRegInfo.Add("QTY", Convert.ToInt32(dtTransfer.Rows[i]["QTY"].ToString()));

                    htLienRegInfo.Add("USER_NM", unitUserObj.UserID.ToString());
                    htLienRegInfo.Add("ENT_DT", DateTime.Now.ToString());
                    htLienRegInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());

                    commonGatewayObj.Insert(htLienRegInfo, "LIEN_MARK_CANCEL");
                }
                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }
        }
        public DateTime getLastLienReqDate(UnitHolderRegistration regObj,UnitLien unitLienObj)
        {
            DateTime lastDate;
            DataTable dtLastDate = new DataTable();
            dtLastDate = commonGatewayObj.Select("SELECT MAX(LN_REQ_DT) AS LN_REQ_DT FROM LIEN_MARK WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'AND LIEN_NO=" + unitLienObj.LienNo);
            if (dtLastDate.Rows.Count > 0)
            {
                lastDate = Convert.ToDateTime(dtLastDate.Rows[0]["LN_REQ_DT"].Equals(DBNull.Value) ? DateTime.Today.ToString() : dtLastDate.Rows[0]["LN_REQ_DT"].ToString());
            }
            else
            {
                lastDate = DateTime.Today;
            }
            return lastDate;

        }
        public DateTime getLastLienCancelReqDate(UnitHolderRegistration regObj, UnitLien unitLienObj)
        {
            DateTime lastDate;
            DataTable dtLastDate = new DataTable();
            dtLastDate = commonGatewayObj.Select("SELECT MAX(LIEN_CANCL_DT) AS LIEN_CANCL_DT FROM LIEN_MARK_CANCEL WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'AND LIEN_NO=" + unitLienObj.LienNo);
            if (dtLastDate.Rows.Count > 0)
            {
                lastDate = Convert.ToDateTime(dtLastDate.Rows[0]["LIEN_CANCL_DT"].Equals(DBNull.Value) ? DateTime.Today.ToString() : dtLastDate.Rows[0]["LIEN_CANCL_DT"].ToString());
            }
            else
            {
                lastDate = DateTime.Today;
            }
            return lastDate;

        }
        public long totalLienAmount(UnitHolderRegistration unitRegObj, UnitLien unitLienObj)
        {
            long totalLien = 0;
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("SELECT SUM(QTY) AS TOTAL_LIEN FROM LIEN_MARK_CERT_NO WHERE LN_MK_CANCEL IS NULL AND REG_BR='"+ unitRegObj.BranchCode.ToString()+"'");
            sbQuery.Append(" AND REG_BK='"+ unitRegObj.FundCode.ToString()+"' AND REG_NO="+ unitRegObj.RegNumber+" AND LIEN_NO='"+ unitLienObj.LienNo.ToString ()+"'");

            DataTable dtTotalLien = commonGatewayObj.Select(sbQuery.ToString());
            if (dtTotalLien.Rows.Count > 0)
            {
                totalLien = dtTotalLien.Rows[0]["TOTAL_LIEN"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtTotalLien.Rows[0]["TOTAL_LIEN"].ToString());
            }
            else
            {
                totalLien = 0;
            }
            return totalLien;
        }
        public long totalLienAmount(UnitHolderRegistration unitRegObj)
        {
            long totalLien = 0;
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("SELECT SUM(QTY) AS TOTAL_LIEN FROM LIEN_MARK_CERT_NO WHERE LN_MK_CANCEL IS NULL AND REG_BR='" + unitRegObj.BranchCode.ToString() + "'");
            sbQuery.Append(" AND REG_BK='" + unitRegObj.FundCode.ToString() + "' AND REG_NO=" + unitRegObj.RegNumber);

            DataTable dtTotalLien = commonGatewayObj.Select(sbQuery.ToString());
            if (dtTotalLien.Rows.Count > 0)
            {
                totalLien = dtTotalLien.Rows[0]["TOTAL_LIEN"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtTotalLien.Rows[0]["TOTAL_LIEN"].ToString());
            }
            else
            {
                totalLien = 0;
            }
            return totalLien;
        }
        public DataTable dtTotalLienCert(UnitHolderRegistration unitRegObj, UnitLien unitLienObj)
        {
            DataTable dtTotalSaleUnitCerts = new DataTable();
            StringBuilder sbQueryString = new StringBuilder();
            sbQueryString.Append(" SELECT SL_TR_NO AS SL_NO,CERTIFICATE,QTY FROM LIEN_MARK_CERT_NO WHERE LN_MK_CANCEL IS NULL AND REG_BR='" + unitRegObj.BranchCode.ToString() + "'");
            sbQueryString.Append(" AND REG_BK='"+ unitRegObj.FundCode.ToString() + "' AND REG_NO="+ Convert.ToInt32(unitRegObj.RegNumber) +" AND LIEN_NO='" + unitLienObj.LienNo.ToString ()+ "'");
            
            dtTotalSaleUnitCerts = commonGatewayObj.Select(sbQueryString.ToString());
            return dtTotalSaleUnitCerts;
        }
        public DataTable dtTotalLien(UnitHolderRegistration regObj)
        {
            DataTable dtTotalLien = new DataTable();
            dtTotalLien.Columns.Add("ID",typeof(int));
            dtTotalLien.Columns.Add("LIEN_NO", typeof(string));
            DataRow drTotalLien = dtTotalLien.NewRow();
            drTotalLien["ID"] = 0;
            drTotalLien["LIEN_NO"] = " ";
            dtTotalLien.Rows.Add(drTotalLien);
            DataTable dtLien = commonGatewayObj.Select("SELECT DISTINCT TO_NUMBER(LIEN_NO) AS LIEN_NO FROM LIEN_MARK_CERT_NO WHERE LN_MK_CANCEL IS NULL AND REG_BK='" + regObj.FundCode.ToString()+ "'AND REG_NO="+ regObj.RegNumber+" AND REG_BR='"+regObj.BranchCode.ToString()+"' ORDER BY TO_NUMBER(LIEN_NO)");
            if (dtLien.Rows.Count>0)
            {
                for (int loop = 0; loop < dtLien.Rows.Count; loop++)
                {
                    drTotalLien = dtTotalLien.NewRow();
                    drTotalLien["ID"] = dtLien.Rows[loop]["LIEN_NO"].ToString();
                    drTotalLien["LIEN_NO"] = dtLien.Rows[loop]["LIEN_NO"].ToString();
                    dtTotalLien.Rows.Add(drTotalLien);
                }

            }

            return dtTotalLien;
        }
        public DataTable dtLienDetailsInfo(UnitHolderRegistration unitRegObj, UnitLien unitLienObj)
        {
            DataTable dtLienDetailsInfo = new DataTable();
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("SELECT * FROM LIEN_MARK WHERE REG_BR='" + unitRegObj.BranchCode.ToString() + "' AND REG_BK = '" + unitRegObj.FundCode.ToString() + "'");
            sbQuery.Append(" AND REG_NO=" + Convert.ToInt32(unitRegObj.RegNumber) + " AND LIEN_NO='" + unitLienObj.LienNo.ToString() + "'");
            dtLienDetailsInfo = commonGatewayObj.Select(sbQuery.ToString());
            return dtLienDetailsInfo;
        }
        public DataTable dtLienDetailsCertificate(UnitHolderRegistration unitRegObj)
        {
            DataTable dtLienDetailCertificate=commonGatewayObj.Select("SELECT * FROM LIEN_MARK_CERT_NO WHERE LN_MK_CANCEL IS NULL AND REG_BR='" + unitRegObj.BranchCode.ToString() + "' AND REG_BK='" + unitRegObj.FundCode.ToString() + "' AND REG_NO=" + unitRegObj.RegNumber);
            return dtLienDetailCertificate;
        }
        public bool IsLienLock(UnitHolderRegistration regObj)
        {
            bool lockStatus = false;
            DataTable dtLockStatus = commonGatewayObj.Select("SELECT NVL(ALL_LOCK,'N') AS ALL_LOCK, NVL(LIEN_LOCK,'N') AS LIEN_LOCK FROM U_MASTER WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO=" + regObj.RegNumber);
            if (dtLockStatus.Rows.Count > 0)
            {
                if (dtLockStatus.Rows[0]["ALL_LOCK"].ToString() == "Y" || dtLockStatus.Rows[0]["LIEN_LOCK"].ToString() == "Y")
                {
                    lockStatus = true;
                }
            }
            return lockStatus;
        }
       
    }
}
