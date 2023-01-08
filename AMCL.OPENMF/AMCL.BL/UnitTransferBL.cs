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
    public class UnitTransferBL
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        OMFDAO opendMFDAO = new OMFDAO();
        public UnitTransferBL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public bool IsDuplicateTransfer(UnitHolderRegistration regObj, UnitTransfer transferObj, string tr_Type)
        {
            DataTable dtTransfer = new DataTable();           
            dtTransfer = commonGatewayObj.Select("SELECT * FROM TRANSFER WHERE TR_TYPE='" + tr_Type + "' AND  F_CD='" + regObj.FundCode.ToString().ToUpper() + "'AND BR_CODE='" + regObj.FundCode.ToString().ToUpper() + "_" + regObj.BranchCode.ToString() + "' AND TR_NO=" + Convert.ToInt32(transferObj.TransferNo.ToString()));           
            if (dtTransfer.Rows.Count > 0)
                return true;
            else
                return false;

        }
        public int getNextTransferNo(UnitHolderRegistration regObj, UnitUser unitUserObj, string tr_Type)
        {
            int nextTransferNo = 0;
            DataTable dtNextTransferNo = new DataTable();        
            dtNextTransferNo = commonGatewayObj.Select("SELECT MAX(TR_NO) AS MAX_TR_NO FROM TRANSFER WHERE TR_TYPE='"+tr_Type+"' AND  F_CD='" + regObj.FundCode.ToString().ToUpper() + "'AND BR_CODE='" + regObj.FundCode.ToString().ToUpper() + "_" + regObj.BranchCode.ToString() + "' AND USER_NM='" + unitUserObj.UserID.ToString() + "'");         
            if (!dtNextTransferNo.Rows[0]["MAX_TR_NO"].Equals(DBNull.Value))
            {
                nextTransferNo = Convert.ToInt16(dtNextTransferNo.Rows[0]["MAX_TR_NO"].ToString());
            }
            else
            {
                dtNextTransferNo = new DataTable();
               
                dtNextTransferNo = commonGatewayObj.Select("SELECT MAX(TR_NO) AS MAX_TR_NO FROM TRANSFER WHERE TR_TYPE='" + tr_Type + "' AND F_CD='" + regObj.FundCode.ToString().ToUpper() + "'AND BR_CODE='" + regObj.FundCode.ToString().ToUpper() + "_" + regObj.BranchCode.ToString() + "'");
              
                nextTransferNo = dtNextTransferNo.Rows[0]["MAX_TR_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt16(dtNextTransferNo.Rows[0]["MAX_TR_NO"].ToString());
            }
            return nextTransferNo + 1;

        }
        public void saveTransfer(DataTable dtTransferSaleCert, UnitHolderRegistration regObj, UnitTransfer transferObj, UnitUser unitUserObj)
        {
            try
            {
                commonGatewayObj.BeginTransaction();
                Hashtable htTransferRegInfo = new Hashtable();
                Hashtable htTransCertNoTemp = new Hashtable();
                int SL_TR_RN = 0;
                int CertNo = 0;
                string certType = "";
                string[] saleNoArray;
                string[] certArray;
                string statusFlag = transferObj.TransferType.ToString().ToUpper();               
                string Old_SL_TR_RN = "";

                for (int loop = 0; loop < dtTransferSaleCert.Rows.Count; loop++)
                {


                    if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('S') >= 0)
                    {
                        htTransferRegInfo = new Hashtable();
                        htTransferRegInfo.Add("CURR_REG_BK", regObj.FundCode.ToString().ToUpper());
                        htTransferRegInfo.Add("CURR_REG_BR", transferObj.TfereeBranchCode.ToString().ToUpper());
                        htTransferRegInfo.Add("CURR_REG_NO", transferObj.TransfereeRegNo.ToString());
                        htTransferRegInfo.Add("STATUS_FLAG", statusFlag.ToUpper().ToString());

                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('S');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();
                        commonGatewayObj.Update(htTransferRegInfo, "SALE_CERT", "REG_NO=" + Convert.ToInt32(regObj.RegNumber) + " AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND  SL_NO=" + SL_TR_RN + " AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "S" + SL_TR_RN.ToString();
                    }
                    else if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('T') >= 0)
                    {
                        htTransferRegInfo = new Hashtable();
                        htTransferRegInfo.Add("CURR_REG_BK", regObj.FundCode.ToString());
                        htTransferRegInfo.Add("CURR_REG_BR", transferObj.TfereeBranchCode.ToString().ToUpper());
                        htTransferRegInfo.Add("CURR_REG_NO", transferObj.TransfereeRegNo.ToString());
                        htTransferRegInfo.Add("STATUS_FLAG", statusFlag.ToUpper().ToString());

                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('T');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();


                        commonGatewayObj.Update(htTransferRegInfo, "TRANS_CERT", "F_CD='" + regObj.FundCode.ToString().ToUpper() + "' AND TR_NO=" + SL_TR_RN + " AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");
                        commonGatewayObj.Update(htTransferRegInfo, "SALE_CERT", "REG_NO=" + Convert.ToInt32(regObj.RegNumber) + " AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");
                        commonGatewayObj.Update(htTransferRegInfo, "RENEWAL_OUT", "REG_NO=" + Convert.ToInt32(regObj.RegNumber) + " AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "T" + SL_TR_RN.ToString();

                    }
                    else if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('D') >= 0)
                    {
                        htTransferRegInfo = new Hashtable();
                        htTransferRegInfo.Add("CURR_REG_BK", regObj.FundCode.ToString());
                        htTransferRegInfo.Add("CURR_REG_BR", transferObj.TfereeBranchCode.ToString().ToUpper());
                        htTransferRegInfo.Add("CURR_REG_NO", transferObj.TransfereeRegNo.ToString());
                        htTransferRegInfo.Add("STATUS_FLAG", statusFlag.ToUpper().ToString());

                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('D');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();


                        commonGatewayObj.Update(htTransferRegInfo, "TRANS_CERT", "F_CD='" + regObj.FundCode.ToString().ToUpper() + "' AND TR_NO=" + SL_TR_RN + " AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");
                        commonGatewayObj.Update(htTransferRegInfo, "SALE_CERT", "REG_NO=" + Convert.ToInt32(regObj.RegNumber) + " AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");
                        commonGatewayObj.Update(htTransferRegInfo, "RENEWAL_OUT", "REG_NO=" + Convert.ToInt32(regObj.RegNumber) + " AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "D" + SL_TR_RN.ToString();

                    }
                    else if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('R') >= 0)
                    {
                        htTransferRegInfo = new Hashtable();
                        htTransferRegInfo.Add("CURR_REG_BK", regObj.FundCode.ToString().ToUpper());
                        htTransferRegInfo.Add("CURR_REG_BR", transferObj.TfereeBranchCode.ToString().ToUpper());
                        htTransferRegInfo.Add("CURR_REG_NO", transferObj.TransfereeRegNo.ToString());
                        htTransferRegInfo.Add("STATUS_FLAG", statusFlag.ToUpper().ToString());

                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('R');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();

                        commonGatewayObj.Update(htTransferRegInfo, "RENEWAL_OUT", "REG_NO=" + Convert.ToInt32(regObj.RegNumber) + " AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "R" + SL_TR_RN.ToString();
                    }


                    htTransferRegInfo = new Hashtable();
                    htTransferRegInfo.Add("F_CD", regObj.FundCode.ToString().ToUpper());
                    htTransferRegInfo.Add("BR_CODE", regObj.FundCode.ToString().ToUpper() + "_" + transferObj.TfereeBranchCode.ToString().ToUpper());
                    htTransferRegInfo.Add("TR_NO", Convert.ToInt32(transferObj.TransferNo.ToString()));

                    certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');

                    htTransferRegInfo.Add("CERT_TYPE", certArray[0].ToString().ToUpper());
                    htTransferRegInfo.Add("CERT_NO", Convert.ToInt32(certArray[1].ToString()));
                    htTransferRegInfo.Add("CERTIFICATE", dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().ToUpper());
                    htTransferRegInfo.Add("QTY", Convert.ToInt32(dtTransferSaleCert.Rows[loop]["QTY"].ToString()));
                    htTransferRegInfo.Add("CURR_REG_BK", regObj.FundCode.ToString().ToUpper());
                    htTransferRegInfo.Add("CURR_REG_BR", transferObj.TfereeBranchCode.ToString().ToUpper());
                    htTransferRegInfo.Add("CURR_REG_NO", Convert.ToInt32(transferObj.TransfereeRegNo.ToString()));
                    htTransferRegInfo.Add("OLD_SL_TR_NO", Old_SL_TR_RN.ToString().ToUpper());
                    htTransferRegInfo.Add("TR_TYPE", transferObj.TransferType.ToString().ToUpper());

                    commonGatewayObj.Insert(htTransferRegInfo, "TRANS_CERT");
                }
                DataTable dtTransfer = dtTrnasfer(dtTransferSaleCert);
                for (int i = 0; i < dtTransfer.Rows.Count; i++)
                {

                  

                    htTransferRegInfo = new Hashtable();
                    htTransferRegInfo.Add("F_CD", regObj.FundCode.ToString().ToUpper());
                    htTransferRegInfo.Add("BR_CODE", regObj.FundCode.ToString().ToUpper() + "_" + transferObj.TfereeBranchCode.ToString().ToUpper());
                    htTransferRegInfo.Add("TR_NO", Convert.ToInt32(transferObj.TransferNo.ToString()));
                    htTransferRegInfo.Add("TR_DT", Convert.ToDateTime(transferObj.TransferDate.ToString()).ToString("dd-MMM-yyyy"));
                    htTransferRegInfo.Add("REG_BK_O", regObj.FundCode.ToString().ToUpper());
                    htTransferRegInfo.Add("REG_BR_O", transferObj.TferorBranchCode.ToString().ToUpper());
                    htTransferRegInfo.Add("REG_NO_O", Convert.ToInt32(transferObj.TransferorRegNo.ToString()));
                    htTransferRegInfo.Add("QTY", Convert.ToInt32(dtTransfer.Rows[i]["QTY"].ToString()));
                    htTransferRegInfo.Add("REG_BK_I", regObj.FundCode.ToString().ToUpper());
                    htTransferRegInfo.Add("REG_BR_I", transferObj.TfereeBranchCode.ToString().ToUpper());
                    htTransferRegInfo.Add("REG_NO_I", Convert.ToInt32(transferObj.TransfereeRegNo.ToString()));
                    htTransferRegInfo.Add("USER_NM", unitUserObj.UserID.ToString());
                    htTransferRegInfo.Add("ENT_DT", DateTime.Now.ToString());
                    htTransferRegInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                    htTransferRegInfo.Add("OLD_SL_TR_NO", dtTransfer.Rows[i]["SL_NO"].ToString());
                    htTransferRegInfo.Add("TR_TYPE", transferObj.TransferType.ToString().ToUpper());
                    commonGatewayObj.Insert(htTransferRegInfo, "TRANSFER");
                }
                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }
        }
        public DataTable dtTrnasfer(DataTable dtTransferSaleCert)
        {
            DataTable dtSearTable = new DataTable();
            dtSearTable = dtTransferSaleCert;
            dtSearTable.TableName = "Search";
            DataTable dtTarget = new DataTable();
            dtTarget = dtSearchTable(dtTransferSaleCert);
            dtTarget.TableName = "Target";
            int qty = 0;
            string certificate = "";
            DataTable dtTrnasfer = opendMFDAO.getTableTransfer();
            DataRow drTransfer;
            // string SL_NO = "";
            ArrayList rowIndex = new ArrayList();
            bool flag = false;
            //int looper = 0;

            for (int loop = 0; loop < dtTarget.Rows.Count; loop++)
            {
                for (int looper = 0; looper < dtSearTable.Rows.Count; looper++)
                {

                    if (dtTarget.Rows[loop]["SALE_NO"].ToString().ToUpper() == dtSearTable.Rows[looper]["SL_NO"].ToString().ToUpper())
                    {
                        if (certificate == "")
                        {
                            certificate = dtSearTable.Rows[looper]["CERTIFICATE"].ToString();
                        }
                        else
                        {
                            certificate = certificate + "," + dtSearTable.Rows[looper]["CERTIFICATE"].ToString();
                        }
                        qty = qty + Convert.ToInt32(dtSearTable.Rows[looper]["QTY"].ToString());
                        rowIndex.Add(looper);
                        flag = true;

                    }

                }

                if (flag)
                {
                    drTransfer = dtTrnasfer.NewRow();
                    drTransfer["QTY"] = qty;
                    drTransfer["SL_NO"] = dtTarget.Rows[loop]["SALE_NO"].ToString().ToUpper();
                    drTransfer["CERTIFICATE"] = certificate.ToString();
                    dtTrnasfer.Rows.Add(drTransfer);
                    dtSearTable = opendMFDAO.dtRemoveRow(dtSearTable, rowIndex);
                    dtSearTable.TableName = "Search";
                    rowIndex.Clear();
                    qty = 0;
                    certificate = "";
                    flag = false;
                }
                if (dtSearTable.Rows.Count == 0)
                {
                    break;
                }

            }

            return dtTrnasfer;
        }
       
        public DataTable dtSearchTable(DataTable dtTarget)
        {
            DataTable dtTargetTable = new DataTable();
            dtTargetTable.Columns.Add("SALE_NO", typeof(string));
            dtTargetTable.Columns.Add("CERT_NO", typeof(string));
            dtTargetTable.Columns.Add("QTY", typeof(string));

            DataRow drTargetTable;
            for (int loop = 0; loop < dtTarget.Rows.Count; loop++)
            {
                drTargetTable = dtTargetTable.NewRow();
                drTargetTable["SALE_NO"] = dtTarget.Rows[loop]["SL_NO"].ToString();
                drTargetTable["CERT_NO"] = dtTarget.Rows[loop]["CERTIFICATE"].ToString();
                drTargetTable["QTY"] = dtTarget.Rows[loop]["QTY"].ToString();
                dtTargetTable.Rows.Add(drTargetTable);
            }
            return dtTargetTable;
        }
        public int getMaxTRNo(UnitHolderRegistration regObj)
        {
            int MaxTrNo = 0;
            DataTable dtMaxTRNo = commonGatewayObj.Select("SELECT MAX(TR_NO) AS MAX_TR_NO FROM TRANSFER WHERE F_CD='" + regObj.FundCode.ToString() + "' AND F_CD='" + regObj.FundCode.ToString() + "_" + regObj.BranchCode.ToString() + " '");
            if (dtMaxTRNo.Rows.Count > 0)
            {
                MaxTrNo = dtMaxTRNo.Rows[0]["MAX_TR_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtMaxTRNo.Rows[0]["MAX_TR_NO"].ToString());
            }
            return MaxTrNo + 1;
           
        }
        public bool IsTransferLock(UnitHolderRegistration regObj)
        {
            bool lockStatus = false;
            DataTable dtLockStatus = commonGatewayObj.Select("SELECT NVL(ALL_LOCK,'N') AS ALL_LOCK, NVL(TR_LOCK,'N') AS TR_LOCK FROM U_MASTER WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO=" + regObj.RegNumber);
            if (dtLockStatus.Rows.Count > 0)
            {
                if (dtLockStatus.Rows[0]["ALL_LOCK"].ToString() == "Y" || dtLockStatus.Rows[0]["TR_LOCK"].ToString() == "Y")
                {
                    lockStatus = true;
                }
            }
            return lockStatus;
        }
        public void saveTransferCDS(DataTable dtSaleTrInfo, UnitHolderRegistration regObj,UnitTransfer transferObj, UnitUser unitUserObj)
        {
            try
            {

                commonGatewayObj.BeginTransaction();
                Hashtable htTransferRegInfo = new Hashtable();

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

                        htTransferRegInfo = new Hashtable();
                        htTransferRegInfo.Add("F_CD", regObj.FundCode.ToString().ToUpper());
                        htTransferRegInfo.Add("BR_CODE", regObj.FundCode.ToString().ToUpper() + "_" + transferObj.TfereeBranchCode.ToString().ToUpper());
                        htTransferRegInfo.Add("TR_NO", Convert.ToInt32(transferObj.TransferNo.ToString()));
                        htTransferRegInfo.Add("TR_DT", Convert.ToDateTime(transferObj.TransferDate.ToString()).ToString("dd-MMM-yyyy"));
                        htTransferRegInfo.Add("REG_BK_O", regObj.FundCode.ToString().ToUpper());
                        htTransferRegInfo.Add("REG_BR_O", transferObj.TferorBranchCode.ToString().ToUpper());
                        htTransferRegInfo.Add("REG_NO_O", Convert.ToInt32(transferObj.TransferorRegNo.ToString()));
                        htTransferRegInfo.Add("QTY", Convert.ToInt32(dtSaleTrInfo.Rows[loop]["SURRENDER_UNITS"].ToString()));
                        htTransferRegInfo.Add("REG_BK_I", regObj.FundCode.ToString().ToUpper());
                        htTransferRegInfo.Add("REG_BR_I", transferObj.TfereeBranchCode.ToString().ToUpper());
                        htTransferRegInfo.Add("REG_NO_I", Convert.ToInt32(transferObj.TransfereeRegNo.ToString()));
                        htTransferRegInfo.Add("USER_NM", unitUserObj.UserID.ToString());
                        htTransferRegInfo.Add("ENT_DT", DateTime.Now.ToString());
                        htTransferRegInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                        htTransferRegInfo.Add("OLD_SL_TR_NO", dtSaleTrInfo.Rows[loop]["SL_TR_NO"].ToString());
                        htTransferRegInfo.Add("TR_TYPE", transferObj.TransferType.ToString().ToUpper());
                        commonGatewayObj.Insert(htTransferRegInfo, "TRANSFER");
                    }
                    else if (dtSaleTrInfo.Rows[loop]["SL_TR_NO"].ToString().ToUpper().IndexOf('T') >= 0)
                    {

                        sbQuery = new StringBuilder();
                        saleNoArray = dtSaleTrInfo.Rows[loop]["SL_TR_NO"].ToString().Split('T');
                        sbQuery.Append("UPDATE TRANSFER SET QTY_OUT=NVL(QTY_OUT,0)+" + Convert.ToInt64(dtSaleTrInfo.Rows[loop]["SURRENDER_UNITS"].ToString()));
                        sbQuery.Append(" WHERE F_CD='" + regObj.FundCode.ToString().ToUpper() + "' AND BR_CODE='" + regObj.FundCode.ToString() + "_" + regObj.BranchCode.ToString().ToUpper() + "'");
                        sbQuery.Append(" AND REG_NO_I=" + Convert.ToInt32(regObj.RegNumber.ToString()) + " AND TR_NO=" + Convert.ToInt32(saleNoArray[1].ToString()));
                        sbQuery.Append(" AND TR_TYPE='" + transferObj.TransferType.ToString().ToUpper()+"'" );

                        commonGatewayObj.ExecuteNonQuery(sbQuery.ToString());

                       
                        htTransferRegInfo = new Hashtable();
                        htTransferRegInfo.Add("F_CD", regObj.FundCode.ToString().ToUpper());
                        htTransferRegInfo.Add("BR_CODE", regObj.FundCode.ToString().ToUpper() + "_" + transferObj.TfereeBranchCode.ToString().ToUpper());
                        htTransferRegInfo.Add("TR_NO", Convert.ToInt32(transferObj.TransferNo.ToString()));
                        htTransferRegInfo.Add("TR_DT", Convert.ToDateTime(transferObj.TransferDate.ToString()).ToString("dd-MMM-yyyy"));
                        htTransferRegInfo.Add("REG_BK_O", regObj.FundCode.ToString().ToUpper());
                        htTransferRegInfo.Add("REG_BR_O", transferObj.TferorBranchCode.ToString().ToUpper());
                        htTransferRegInfo.Add("REG_NO_O", Convert.ToInt32(transferObj.TransferorRegNo.ToString()));
                        htTransferRegInfo.Add("QTY", Convert.ToInt32(dtSaleTrInfo.Rows[loop]["SURRENDER_UNITS"].ToString()));
                        htTransferRegInfo.Add("REG_BK_I", regObj.FundCode.ToString().ToUpper());
                        htTransferRegInfo.Add("REG_BR_I", transferObj.TfereeBranchCode.ToString().ToUpper());
                        htTransferRegInfo.Add("REG_NO_I", Convert.ToInt32(transferObj.TransfereeRegNo.ToString()));
                        htTransferRegInfo.Add("USER_NM", unitUserObj.UserID.ToString());
                        htTransferRegInfo.Add("ENT_DT", DateTime.Now.ToString());
                        htTransferRegInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                        htTransferRegInfo.Add("OLD_SL_TR_NO", dtSaleTrInfo.Rows[loop]["SL_TR_NO"].ToString());
                        htTransferRegInfo.Add("TR_TYPE", transferObj.TransferType.ToString().ToUpper());
                        commonGatewayObj.Insert(htTransferRegInfo, "TRANSFER");
                    }
                    else if (dtSaleTrInfo.Rows[loop]["SL_TR_NO"].ToString().ToUpper().IndexOf('D') >= 0)
                    {

                        sbQuery = new StringBuilder();
                        saleNoArray = dtSaleTrInfo.Rows[loop]["SL_TR_NO"].ToString().Split('D');
                        sbQuery.Append("UPDATE TRANSFER SET QTY_OUT=NVL(QTY_OUT,0)+" + Convert.ToInt64(dtSaleTrInfo.Rows[loop]["SURRENDER_UNITS"].ToString()));
                        sbQuery.Append(" WHERE F_CD='" + regObj.FundCode.ToString().ToUpper() + "' AND BR_CODE='" + regObj.FundCode.ToString() + "_" + regObj.BranchCode.ToString().ToUpper() + "'");
                        sbQuery.Append(" AND REG_NO_I=" + Convert.ToInt32(regObj.RegNumber.ToString()) + " AND TR_NO=" + Convert.ToInt32(saleNoArray[1].ToString()));
                        sbQuery.Append(" AND TR_TYPE='" + transferObj.TransferType.ToString().ToUpper()+"'");

                        commonGatewayObj.ExecuteNonQuery(sbQuery.ToString());


                        htTransferRegInfo = new Hashtable();
                        htTransferRegInfo.Add("F_CD", regObj.FundCode.ToString().ToUpper());
                        htTransferRegInfo.Add("BR_CODE", regObj.FundCode.ToString().ToUpper() + "_" + transferObj.TfereeBranchCode.ToString().ToUpper());
                        htTransferRegInfo.Add("TR_NO", Convert.ToInt32(transferObj.TransferNo.ToString()));
                        htTransferRegInfo.Add("TR_DT", Convert.ToDateTime(transferObj.TransferDate.ToString()).ToString("dd-MMM-yyyy"));
                        htTransferRegInfo.Add("REG_BK_O", regObj.FundCode.ToString().ToUpper());
                        htTransferRegInfo.Add("REG_BR_O", transferObj.TferorBranchCode.ToString().ToUpper());
                        htTransferRegInfo.Add("REG_NO_O", Convert.ToInt32(transferObj.TransferorRegNo.ToString()));
                        htTransferRegInfo.Add("QTY", Convert.ToInt32(dtSaleTrInfo.Rows[loop]["SURRENDER_UNITS"].ToString()));
                        htTransferRegInfo.Add("REG_BK_I", regObj.FundCode.ToString().ToUpper());
                        htTransferRegInfo.Add("REG_BR_I", transferObj.TfereeBranchCode.ToString().ToUpper());
                        htTransferRegInfo.Add("REG_NO_I", Convert.ToInt32(transferObj.TransfereeRegNo.ToString()));
                        htTransferRegInfo.Add("USER_NM", unitUserObj.UserID.ToString());
                        htTransferRegInfo.Add("ENT_DT", DateTime.Now.ToString());
                        htTransferRegInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                        htTransferRegInfo.Add("OLD_SL_TR_NO", dtSaleTrInfo.Rows[loop]["SL_TR_NO"].ToString());
                        htTransferRegInfo.Add("TR_TYPE", transferObj.TransferType.ToString().ToUpper());
                        commonGatewayObj.Insert(htTransferRegInfo, "TRANSFER");
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
    }
}
