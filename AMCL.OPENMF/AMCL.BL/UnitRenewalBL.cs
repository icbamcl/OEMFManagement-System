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

/// <summary>
/// Summary description for UnitReg
/// </summary>
namespace AMCL.BL
{
    public class UnitRenewalBL
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        OMFDAO OmfDAOObj = new OMFDAO();
        UnitRenewal renwalObj = new UnitRenewal();
        public UnitRenewalBL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public void SaveUnitRenewal(UnitHolderRegistration unitRegObj, DataTable dtTransferSaleCert, UnitRenewal renwalObj, DataTable dtDinomination, UnitUser unitUserObj)
        {
            
            Hashtable htUnitSaleCert = new Hashtable();            
            Hashtable htRenwal = new Hashtable();
            Hashtable htRenewalIN = new Hashtable();
            Hashtable htRenewalOUT = new Hashtable();
            UnitTransferBL unitTransferBLObj = new UnitTransferBL();                               
            int SL_TR_RN = 0;
            int CertNo = 0;
            string certType = "";
            string[] saleNoArray;
            string[] certArray;
            string valid = "N";
            string remarks = "RENEWAL NO:"+renwalObj.RenewalNo.ToString();
            string Old_SL_TR_RN = "";
            string FORM_CODE = "";

            if (renwalObj.RenewalType.ToString().ToUpper() == "S"||renwalObj.RenewalType.ToString().ToUpper() == "C")
            {
                FORM_CODE = "UNIT-UMA";
            }
            else if (renwalObj.RenewalType.ToString().ToUpper() == "S")
            {
                FORM_CODE = "UNIT-CHA";
            }
            try
            {
                commonGatewayObj.BeginTransaction();
                for (int loop = 0; loop < dtTransferSaleCert.Rows.Count; loop++)
                {


                    if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('S') >= 0)
                    {
                        htUnitSaleCert = new Hashtable();
                        htUnitSaleCert.Add("VALID", valid);
                        htUnitSaleCert.Add("REMARKS", remarks);


                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('S');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();
                        commonGatewayObj.Update(htUnitSaleCert, "SALE_CERT", "REG_NO=" + Convert.ToInt32(unitRegObj.RegNumber) + " AND REG_BK='" + unitRegObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + unitRegObj.BranchCode.ToString().ToUpper() + "' AND  SL_NO=" + SL_TR_RN + " AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "S" + SL_TR_RN.ToString();
                    }
                    else if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('T') >= 0)
                    {
                        htUnitSaleCert = new Hashtable();
                        htUnitSaleCert.Add("VALID", valid);
                        htUnitSaleCert.Add("REMARKS", remarks);


                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('T');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();

                        commonGatewayObj.Update(htUnitSaleCert, "TRANS_CERT", "F_CD='" + unitRegObj.FundCode.ToString().ToUpper() + "' AND TR_NO=" + SL_TR_RN + "  AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");
                    
                        Old_SL_TR_RN = "T" + SL_TR_RN.ToString();

                    }
                    else if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('D') >= 0)
                    {
                        htUnitSaleCert = new Hashtable();
                        htUnitSaleCert.Add("VALID", valid);
                        htUnitSaleCert.Add("REMARKS", remarks);


                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('D');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();

                        commonGatewayObj.Update(htUnitSaleCert, "TRANS_CERT", "F_CD='" + unitRegObj.FundCode.ToString().ToUpper() + "' AND TR_NO=" + SL_TR_RN + "  AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "D" + SL_TR_RN.ToString();

                    }
                    else if (dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().ToUpper().IndexOf('R') >= 0)
                    {
                        htUnitSaleCert = new Hashtable();
                        htUnitSaleCert.Add("VALID", valid);
                        htUnitSaleCert.Add("REMARKS", remarks);


                        saleNoArray = dtTransferSaleCert.Rows[loop]["SL_NO"].ToString().Split('R');
                        SL_TR_RN = Convert.ToInt32(saleNoArray[1].ToString());
                        certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');
                        CertNo = Convert.ToInt32(certArray[1].ToString());
                        certType = certArray[0].ToString().ToUpper();

                        commonGatewayObj.Update(htUnitSaleCert, "RENEWAL_OUT", "REN_NO='" + SL_TR_RN + "' AND REG_BK='" + unitRegObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + unitRegObj.BranchCode.ToString().ToUpper() + "' AND CERT_NO=" + CertNo + " AND CERT_TYPE='" + certType + "'");

                        Old_SL_TR_RN = "R" + SL_TR_RN.ToString();
                    }

                    htRenewalIN = new Hashtable();
                    htRenewalIN.Add("REG_BK", unitRegObj.FundCode.ToString().ToUpper());
                    htRenewalIN.Add("REG_BR", unitRegObj.BranchCode.ToString().ToUpper());
                    htRenewalIN.Add("REG_NO", Convert.ToInt32(unitRegObj.RegNumber.ToString()));
                    htRenewalIN.Add("REN_NO", renwalObj.RenewalNo.ToString());

                    certArray = dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().Split('-');

                    htRenewalIN.Add("CERT_TYPE", certArray[0].ToString().ToUpper());
                    htRenewalIN.Add("CERT_NO", Convert.ToInt32(certArray[1].ToString()));
                    htRenewalIN.Add("CERTIFICATE", dtTransferSaleCert.Rows[loop]["CERTIFICATE"].ToString().ToUpper());
                    htRenewalIN.Add("QTY", Convert.ToInt32(dtTransferSaleCert.Rows[loop]["QTY"].ToString()));
                    htRenewalIN.Add("SL_TR_NO", Old_SL_TR_RN.ToString().ToUpper());

                    commonGatewayObj.Insert(htRenewalIN, "RENEWAL_IN");

                    
                }

                DataTable dtTransfer = unitTransferBLObj.dtTrnasfer(dtTransferSaleCert);
                for (int i = 0; i < dtTransfer.Rows.Count; i++) //Insert Into Renewal Table
                {
                   

                    htRenwal = new Hashtable();

                    htRenwal.Add("REN_NO", renwalObj.RenewalNo.ToString());
                    htRenwal.Add("REN_DT",Convert.ToDateTime(renwalObj.RenewalDate).ToString("dd-MMM-yyyy"));
                    htRenwal.Add("REG_BK", unitRegObj.FundCode.ToString().ToUpper());
                    htRenwal.Add("REG_BR", unitRegObj.BranchCode.ToString());
                    htRenwal.Add("REG_NO", Convert.ToInt32(unitRegObj.RegNumber.ToString()));
                    htRenwal.Add("FORM_CODE", FORM_CODE);
                    htRenwal.Add("SL_TR_NO", dtTransfer.Rows[i]["SL_NO"].ToString());
                    htRenwal.Add("REN_TYPE", renwalObj.RenewalType.ToString().ToUpper());
                    htRenwal.Add("QTY", Convert.ToInt32(dtTransfer.Rows[i]["QTY"].ToString()));
                    htRenwal.Add("USER_NM", unitUserObj.UserID.ToString());
                    htRenwal.Add("ENT_DT", DateTime.Now.ToString());
                    htRenwal.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                    commonGatewayObj.Insert(htRenwal, "RENEWAL");
                }

                for (int looper = 0; looper < dtDinomination.Rows.Count; looper++)//Insert Into RenewalOut Table
                {
                    htRenewalOUT = new Hashtable();
                    htRenewalOUT.Add("REG_BK", unitRegObj.FundCode.ToString().ToUpper());
                    htRenewalOUT.Add("REG_BR", unitRegObj.BranchCode.ToString());
                    htRenewalOUT.Add("REG_NO",Convert.ToInt32( unitRegObj.RegNumber.ToString()));
                    htRenewalOUT.Add("REN_NO",renwalObj.RenewalNo.ToString());
                    htRenewalOUT.Add("CERT_TYPE", dtDinomination.Rows[looper]["dino"].ToString().ToUpper());
                    htRenewalOUT.Add("CERT_NO", Convert.ToInt32(dtDinomination.Rows[looper]["cert_no"].ToString()));
                    htRenewalOUT.Add("CERTIFICATE", SaleCertNo(Convert.ToInt32(dtDinomination.Rows[looper]["cert_no"].ToString()), dtDinomination.Rows[looper]["dino"].ToString().ToUpper()));
                    htRenewalOUT.Add("QTY", Convert.ToInt32(dtDinomination.Rows[looper]["cert_weight"].ToString()));
                    
                    commonGatewayObj.Insert(htRenewalOUT, "RENEWAL_OUT");
                                      
                }

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
        public bool IsDuplicateRenewal(UnitHolderRegistration regObj, UnitRenewal renwalObj)
        {
            DataTable dtRenewal = commonGatewayObj.Select("SELECT * FROM RENEWAL WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REN_NO='" + renwalObj.RenewalNo.ToString()+ "'");
            if (dtRenewal.Rows.Count > 0)
                return true;
            else
                return false;

        }
        public int getNextRenNo(UnitHolderRegistration regObj, UnitUser unitUserObj)
        {
            int nextSaleNo = 0;
            DataTable dtNextSaleNo = new DataTable();
            dtNextSaleNo = commonGatewayObj.Select("SELECT REN_NO AS MAX_SL_NO FROM RENEWAL WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND USER_NM='" + unitUserObj.UserID.ToString() + "' AND ENT_DT IN (SELECT MAX(ENT_DT) FROM  RENEWAL WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND USER_NM='" + unitUserObj.UserID.ToString() + "')");
            if (dtNextSaleNo.Rows.Count > 0)
            {               
              
                    nextSaleNo = Convert.ToInt16(dtNextSaleNo.Rows[0]["MAX_SL_NO"].ToString());                            
            }
            else
            {
                dtNextSaleNo = new DataTable();
                dtNextSaleNo = commonGatewayObj.Select("SELECT REN_NO AS MAX_SL_NO FROM RENEWAL WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "'  AND ENT_DT IN (SELECT MAX(ENT_DT) FROM  RENEWAL WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "')");
                if (dtNextSaleNo.Rows.Count > 0)
                {
                    nextSaleNo = dtNextSaleNo.Rows[0]["MAX_SL_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt16(dtNextSaleNo.Rows[0]["MAX_SL_NO"].ToString());
                }
            }
            return nextSaleNo + 1;

        }
        public bool IsRenewalLock(UnitHolderRegistration regObj)
        {
            bool lockStatus = false;
            DataTable dtLockStatus = commonGatewayObj.Select("SELECT NVL(ALL_LOCK,'N') AS ALL_LOCK, NVL(REN_LOCK,'N') AS REN_LOCK FROM U_MASTER WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO=" + regObj.RegNumber);
            if (dtLockStatus.Rows.Count > 0)
            {
                if (dtLockStatus.Rows[0]["ALL_LOCK"].ToString() == "Y" || dtLockStatus.Rows[0]["REN_LOCK"].ToString() == "Y")
                {
                    lockStatus = true;
                }
            }
            return lockStatus;
        }

    }
}