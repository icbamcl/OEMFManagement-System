using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Collections;
using AMCL.GATEWAY;
using AMCL.COMMON;

/// <summary>
/// Summary description for OMFDAO
namespace AMCL.DL
{
    public class OMFDAO
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        public OMFDAO()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
            DataTable dtFundList = commonGatewayObj.Select(sbQuery.ToString());
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
        public DataTable dtFundList()
        {
            DataTable dtFundList = commonGatewayObj.Select("SELECT FUND_CD , FUND_NM FROM FUND_INFO  ORDER BY FUND_CD");
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
        public DataTable dtBranchList()
        {
            DataTable dtBranchList = commonGatewayObj.Select("SELECT BR_CD,BR_NM FROM BRANCH_INFO ORDER BY BR_CD");
            DataTable dtBranchListDropDown = new DataTable();
            dtBranchListDropDown.Columns.Add("BR_CD", typeof(string));
            dtBranchListDropDown.Columns.Add("BR_NM", typeof(string));

            DataRow drBranchListDropDown = dtBranchListDropDown.NewRow();

            drBranchListDropDown["BR_NM"] = " ";
            drBranchListDropDown["BR_CD"] = "0";
            dtBranchListDropDown.Rows.Add(drBranchListDropDown);
            for (int loop = 0; loop < dtBranchList.Rows.Count; loop++)
            {
                drBranchListDropDown = dtBranchListDropDown.NewRow();
                drBranchListDropDown["BR_NM"] = dtBranchList.Rows[loop]["BR_NM"].ToString();
                drBranchListDropDown["BR_CD"] = dtBranchList.Rows[loop]["BR_CD"].ToString();
                dtBranchListDropDown.Rows.Add(drBranchListDropDown);
            }
            return dtBranchListDropDown;
        }
        public DataTable dtBranchList(UnitUser userObj)
        {
            StringBuilder sbQuery = new StringBuilder();
            if (userObj.UserID.ToString().ToUpper() == "ADMIN")
            {
                sbQuery.Append("SELECT BR_CD,BR_NM FROM BRANCH_INFO ORDER BY BR_CD");
            }
            else
            {
                sbQuery.Append(" SELECT BR_CD,BR_NM FROM BRANCH_INFO WHERE BR_CD IN (SELECT BR_CD FROM USER_BRANCH WHERE USER_ID='" + userObj.UserID.ToString() + "') ORDER BY BR_CD");
            }
            DataTable dtBranchList = commonGatewayObj.Select(sbQuery.ToString());
            DataTable dtBranchListDropDown = new DataTable();
            dtBranchListDropDown.Columns.Add("BR_CD", typeof(string));
            dtBranchListDropDown.Columns.Add("BR_NM", typeof(string));

            DataRow drBranchListDropDown = dtBranchListDropDown.NewRow();

            drBranchListDropDown["BR_NM"] = " ";
            drBranchListDropDown["BR_CD"] = "0";
            dtBranchListDropDown.Rows.Add(drBranchListDropDown);
            for (int loop = 0; loop < dtBranchList.Rows.Count; loop++)
            {
                drBranchListDropDown = dtBranchListDropDown.NewRow();
                drBranchListDropDown["BR_NM"] = dtBranchList.Rows[loop]["BR_NM"].ToString();
                drBranchListDropDown["BR_CD"] = dtBranchList.Rows[loop]["BR_CD"].ToString();
                dtBranchListDropDown.Rows.Add(drBranchListDropDown);
            }
            return dtBranchListDropDown;
        }
        public DataTable dtOccopationList()
        {
            DataTable dtOccopationList = commonGatewayObj.Select("SELECT * FROM OCC_CODE ORDER BY CODE");
            DataTable dtOccopationListDropDown = new DataTable();
            dtOccopationListDropDown.Columns.Add("CODE", typeof(string));
            dtOccopationListDropDown.Columns.Add("DESCR", typeof(string));

            DataRow drOccopationListDropDown = dtOccopationListDropDown.NewRow();

            drOccopationListDropDown["DESCR"] = " ";
            drOccopationListDropDown["CODE"] = "0";
            dtOccopationListDropDown.Rows.Add(drOccopationListDropDown);
            for (int loop = 0; loop < dtOccopationList.Rows.Count; loop++)
            {
                drOccopationListDropDown = dtOccopationListDropDown.NewRow();
                drOccopationListDropDown["DESCR"] = dtOccopationList.Rows[loop]["DESCR"].ToString();
                drOccopationListDropDown["CODE"] = dtOccopationList.Rows[loop]["CODE"].ToString();
                dtOccopationListDropDown.Rows.Add(drOccopationListDropDown);
            }
            return dtOccopationListDropDown;
        }
        public string GetFundName(string fundCode)
        {
            DataTable dtFundName = commonGatewayObj.Select("SELECT * FROM FUND_INFO WHERE FUND_CD='" + fundCode.ToString() + "'");

            if (dtFundName.Rows.Count > 0)
            {
                return dtFundName.Rows[0]["FUND_NM"].Equals(DBNull.Value) ? "" : dtFundName.Rows[0]["FUND_NM"].ToString();
            }
            else
            {
                return "";
            }
        }
        public string GetBranchName(string branchCode)
        {
            DataTable dtBranchName = commonGatewayObj.Select("SELECT * FROM BRANCH_INFO WHERE BR_CD='" + branchCode.ToString() + "'");
            if (dtBranchName.Rows.Count > 0)
            {
                return dtBranchName.Rows[0]["BR_NM"].Equals(DBNull.Value) ? "" : dtBranchName.Rows[0]["BR_NM"].ToString();
            }
            else
                return "";
        }
        public string GetHolderName(string fundCode, string branchCode, string regNo)
        {
            DataTable dtFundName = commonGatewayObj.Select("SELECT HNAME FROM U_MASTER WHERE REG_BK='" + fundCode.ToString() + "'AND REG_BR='" + branchCode + "'AND REG_NO='" + regNo + "'");
            if (dtFundName.Rows.Count > 0)
            {
                return dtFundName.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtFundName.Rows[0]["HNAME"].ToString();
            }
            else
                return "";
        }
        public DataTable HolderRegInfo(UnitHolderRegistration regObj)
        {
            StringBuilder sbHolderQuery = new StringBuilder();
            sbHolderQuery.Append("SELECT * FROM U_MASTER WHERE (U_MASTER.REG_BK = '" + regObj.FundCode.ToString() + "') AND (U_MASTER.REG_BR = '" + regObj.BranchCode.ToString() + "') AND (U_MASTER.REG_NO = '" + regObj.RegNumber.ToString() + "')");                     
            DataTable dtHolderRegInfo = commonGatewayObj.Select(sbHolderQuery.ToString());
            return dtHolderRegInfo;
        }
        public DataTable dtNomineeRegInfo(UnitHolderRegistration regObj)
        {
            StringBuilder sbNomineeQuery = new StringBuilder();
            sbNomineeQuery.Append("SELECT * FROM U_NOMINEE WHERE REG_BK = '" + regObj.FundCode.ToString() + "' AND REG_BR = '" + regObj.BranchCode.ToString() + "' AND REG_NO = '" + regObj.RegNumber.ToString() + "'");
            DataTable dtNomineeRegInfo = commonGatewayObj.Select(sbNomineeQuery.ToString());
            return dtNomineeRegInfo;
        }
        public DataTable dtJointHolderRegInfo(UnitHolderRegistration regObj)
        {
            StringBuilder sbJointQuery = new StringBuilder();
            sbJointQuery.Append("SELECT * FROM U_JHOLDER WHERE REG_BK = '" + regObj.FundCode.ToString() + "' AND REG_BR = '" + regObj.BranchCode.ToString() + "' AND REG_NO = '" + regObj.RegNumber.ToString() + "'");
            DataTable dtJointHolderRegInfo = commonGatewayObj.Select(sbJointQuery.ToString());
            return dtJointHolderRegInfo;
        }
        public bool IsExistJointHolder(UnitHolderRegistration regObj)
        {
            DataTable dtBranchName = commonGatewayObj.Select("SELECT * FROM U_JHOLDER WHERE REG_NO='" + regObj.RegNumber.ToString() + "'AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "'");
            if (dtBranchName.Rows.Count > 0)
                return true;
            else
                return false;

        }
        public bool IsExistNominee(UnitHolderRegistration regObj, int NomiNo)
        {
            DataTable dtExistNominee = new DataTable();

            dtExistNominee = commonGatewayObj.Select("SELECT * FROM U_NOMINEE WHERE REG_NO='" + regObj.RegNumber.ToString() + "'AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND NOMI_NO=" + NomiNo);

            if (dtExistNominee.Rows.Count > 0)
                return true;
            else
                return false;

        }       
        public int GetMaxCertNo(string certType, UnitHolderRegistration regObj, UnitUser unitUserObj)
        {
            int maxCertNo = 0;
            DataTable dtMaxCertNo = new DataTable();
            dtMaxCertNo = commonGatewayObj.Select("SELECT MAX(CERT_NO) AS MAX_CERT_NO FROM SALE_CERT WHERE CERT_TYPE='" + certType.ToString().ToUpper() + "' AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND USER_NM='" + unitUserObj.UserID.ToString() + "' AND ENT_DT IN(SELECT MAX(ENT_DT) FROM SALE_CERT SALE_1  WHERE CERT_TYPE='" + certType.ToString().ToUpper() + "' AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND USER_NM='" + unitUserObj.UserID.ToString() + "')");
            if (!dtMaxCertNo.Rows[0]["MAX_CERT_NO"].Equals(DBNull.Value))
            {
                maxCertNo = dtMaxCertNo.Rows[0]["MAX_CERT_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtMaxCertNo.Rows[0]["MAX_CERT_NO"].ToString());
            }
            else
            {
                dtMaxCertNo = new DataTable();
                dtMaxCertNo = commonGatewayObj.Select("SELECT MAX(CERT_NO) AS MAX_CERT_NO FROM SALE_CERT WHERE CERT_TYPE='" + certType.ToString().ToUpper() + "' AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "'");
                maxCertNo = dtMaxCertNo.Rows[0]["MAX_CERT_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtMaxCertNo.Rows[0]["MAX_CERT_NO"].ToString());
            }
            return maxCertNo + 1;
        }
        public DataTable getTableDinomination()
        {
            DataTable dtDinomination = new DataTable();
            dtDinomination.Columns.Add("dino", typeof(string));
            dtDinomination.Columns.Add("cert_no", typeof(string));
            dtDinomination.Columns.Add("cert_weight", typeof(string));
            return dtDinomination;

        }
        public DataTable dtDinomination(int unitQty, UnitHolderRegistration regObj, UnitUser userObj)
        {
            DataTable dtDinomination = getTableDinomination();
            DataRow drDinomination = dtDinomination.NewRow();
            int certNo = 0;
            int certQty = 0;
            int reminder = 0;
            if (regObj.RegIsCIP.ToString() == "CIP" &&( string.Compare(regObj.FundCode.ToString(), "CFUF") == 0|| string.Compare(regObj.FundCode.ToString(), "IAMCL") == 0))
            {                
                
                    certNo = GetMaxCertNo("Y", regObj, userObj);
                    drDinomination = dtDinomination.NewRow();
                    drDinomination["dino"] = "Y";
                    drDinomination["cert_no"] = certNo;
                    drDinomination["cert_weight"] = unitQty.ToString();
                    dtDinomination.Rows.Add(drDinomination);
               

            }
            else
            {

                if (string.Compare(regObj.FundCode.ToString(), "CFUF") == 0 || string.Compare(regObj.FundCode.ToString(), "IUF") == 0)
                {
                    if (unitQty >= 20000)//for L
                    {
                        certNo = GetMaxCertNo("L", regObj, userObj);
                        reminder = unitQty % 20000;
                        certQty = (unitQty - reminder) / 20000;
                        unitQty = unitQty - (certQty * 20000);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "L";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "20000";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                }
                if (string.Compare(regObj.FundCode.ToString(), "BDF") == 0 || string.Compare(regObj.FundCode.ToString(), "CFUF") == 0 || string.Compare(regObj.FundCode.ToString(), "IUF") == 0)
                {
                    if (unitQty >= 10000)//for K
                    {
                        certNo = GetMaxCertNo("K", regObj, userObj);
                        reminder = unitQty % 10000;
                        certQty = (unitQty - reminder) / 10000;
                        unitQty = unitQty - (certQty * 10000);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "K";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "10000";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                }


                do
                {
                    if (unitQty >= 5000)//for J
                    {
                        certNo = GetMaxCertNo("J", regObj, userObj);
                        reminder = unitQty % 5000;
                        certQty = (unitQty - reminder) / 5000;
                        unitQty = unitQty - (certQty * 5000);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "J";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "5000";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }



                    }
                    else if (unitQty >= 1000 && unitQty < 5000)
                    {
                        certNo = GetMaxCertNo("I", regObj, userObj);
                        reminder = unitQty % 1000;
                        certQty = (unitQty - reminder) / 1000;
                        unitQty = unitQty - (certQty * 1000);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "I";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "1000";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 500 && unitQty < 1000)
                    {
                        certNo = GetMaxCertNo("H", regObj, userObj);
                        reminder = unitQty % 500;
                        certQty = (unitQty - reminder) / 500;
                        unitQty = unitQty - (certQty * 500);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "H";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "500";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 250 && unitQty < 500)
                    {
                        certNo = GetMaxCertNo("G", regObj, userObj);
                        reminder = unitQty % 250;
                        certQty = (unitQty - reminder) / 250;
                        unitQty = unitQty - (certQty * 250);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "G";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "250";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 100 && unitQty < 250)
                    {
                        certNo = GetMaxCertNo("F", regObj, userObj);
                        reminder = unitQty % 100;
                        certQty = (unitQty - reminder) / 100;
                        unitQty = unitQty - (certQty * 100);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "F";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "100";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 50 && unitQty < 100)
                    {
                        certNo = GetMaxCertNo("E", regObj, userObj);
                        reminder = unitQty % 50;
                        certQty = (unitQty - reminder) / 50;
                        unitQty = unitQty - (certQty * 50);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "E";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "50";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 20 && unitQty < 50)
                    {
                        certNo = GetMaxCertNo("D", regObj, userObj);
                        reminder = unitQty % 20;
                        certQty = (unitQty - reminder) / 20;
                        unitQty = unitQty - (certQty * 20);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "D";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "20";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 10 && unitQty < 20)
                    {
                        certNo = GetMaxCertNo("C", regObj, userObj);
                        reminder = unitQty % 10;
                        certQty = (unitQty - reminder) / 10;
                        unitQty = unitQty - (certQty * 10);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "C";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "10";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 5 && unitQty < 10)
                    {
                        certNo = GetMaxCertNo("B", regObj, userObj);
                        reminder = unitQty % 5;
                        certQty = (unitQty - reminder) / 5;
                        unitQty = unitQty - (certQty * 5);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "B";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "5";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 1 && unitQty < 5)
                    {
                        certNo = GetMaxCertNo("A", regObj, userObj);

                        certQty = unitQty;
                        unitQty = unitQty - (certQty * 1);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "A";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "1";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }

                } while (unitQty != 0);
            }

            return dtDinomination;
        }
        public DataTable dtDinominationForRenewal(int unitQty, UnitHolderRegistration regObj, UnitUser userObj)
        {
            DataTable dtDinomination = getTableDinomination();
            DataRow drDinomination = dtDinomination.NewRow();
            int certNo = 0;
            int certQty = 0;
            int reminder = 0;


            if (string.Compare(regObj.FundCode.ToString(), "CFUF") == 0 || string.Compare(regObj.FundCode.ToString(), "IUF") == 0)
                {
                    if (unitQty >= 20000)//for L
                    {
                        certNo = GetMaxCertNo("L", regObj, userObj);
                        reminder = unitQty % 20000;
                        certQty = (unitQty - reminder) / 20000;
                        unitQty = unitQty - (certQty * 20000);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "L";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "20000";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                }
            if (string.Compare(regObj.FundCode.ToString(), "BDF") == 0 || string.Compare(regObj.FundCode.ToString(), "CFUF") == 0 || string.Compare(regObj.FundCode.ToString(), "IUF") == 0)
                {
                    if (unitQty >= 10000)//for K
                    {
                        certNo = GetMaxCertNo("K", regObj, userObj);
                        reminder = unitQty % 10000;
                        certQty = (unitQty - reminder) / 10000;
                        unitQty = unitQty - (certQty * 10000);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "K";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "10000";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                }


                do
                {
                    if (unitQty >= 5000)//for J
                    {
                        certNo = GetMaxCertNo("J", regObj, userObj);
                        reminder = unitQty % 5000;
                        certQty = (unitQty - reminder) / 5000;
                        unitQty = unitQty - (certQty * 5000);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "J";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "5000";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }



                    }
                    else if (unitQty >= 1000 && unitQty < 5000)
                    {
                        certNo = GetMaxCertNo("I", regObj, userObj);
                        reminder = unitQty % 1000;
                        certQty = (unitQty - reminder) / 1000;
                        unitQty = unitQty - (certQty * 1000);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "I";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "1000";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 500 && unitQty < 1000)
                    {
                        certNo = GetMaxCertNo("H", regObj, userObj);
                        reminder = unitQty % 500;
                        certQty = (unitQty - reminder) / 500;
                        unitQty = unitQty - (certQty * 500);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "H";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "500";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 250 && unitQty < 500)
                    {
                        certNo = GetMaxCertNo("G", regObj, userObj);
                        reminder = unitQty % 250;
                        certQty = (unitQty - reminder) / 250;
                        unitQty = unitQty - (certQty * 250);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "G";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "250";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 100 && unitQty < 250)
                    {
                        certNo = GetMaxCertNo("F", regObj, userObj);
                        reminder = unitQty % 100;
                        certQty = (unitQty - reminder) / 100;
                        unitQty = unitQty - (certQty * 100);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "F";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "100";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 50 && unitQty < 100)
                    {
                        certNo = GetMaxCertNo("E", regObj, userObj);
                        reminder = unitQty % 50;
                        certQty = (unitQty - reminder) / 50;
                        unitQty = unitQty - (certQty * 50);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "E";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "50";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 20 && unitQty < 50)
                    {
                        certNo = GetMaxCertNo("D", regObj, userObj);
                        reminder = unitQty % 20;
                        certQty = (unitQty - reminder) / 20;
                        unitQty = unitQty - (certQty * 20);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "D";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "20";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 10 && unitQty < 20)
                    {
                        certNo = GetMaxCertNo("C", regObj, userObj);
                        reminder = unitQty % 10;
                        certQty = (unitQty - reminder) / 10;
                        unitQty = unitQty - (certQty * 10);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "C";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "10";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 5 && unitQty < 10)
                    {
                        certNo = GetMaxCertNo("B", regObj, userObj);
                        reminder = unitQty % 5;
                        certQty = (unitQty - reminder) / 5;
                        unitQty = unitQty - (certQty * 5);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "B";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "5";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }
                    else if (unitQty >= 1 && unitQty < 5)
                    {
                        certNo = GetMaxCertNo("A", regObj, userObj);

                        certQty = unitQty;
                        unitQty = unitQty - (certQty * 1);
                        for (int i = 0; i < certQty; i++)
                        {
                            drDinomination = dtDinomination.NewRow();
                            drDinomination["dino"] = "A";
                            drDinomination["cert_no"] = certNo;
                            drDinomination["cert_weight"] = "1";
                            dtDinomination.Rows.Add(drDinomination);
                            certNo++;

                        }

                    }

                } while (unitQty != 0);
           

            return dtDinomination;
        }
        public DataTable getDtRegInfo(UnitHolderRegistration unitRegObj)
        {
            DataTable dtRegInfo = new DataTable();
            StringBuilder sbMaseter = new StringBuilder();
            sbMaseter.Append("SELECT U_MASTER.*,U_JHOLDER.* FROM U_JHOLDER RIGHT OUTER JOIN U_MASTER ON U_JHOLDER.REG_BK = U_MASTER.REG_BK AND U_JHOLDER.REG_BR = U_MASTER.REG_BR AND U_JHOLDER.REG_NO = U_MASTER.REG_NO WHERE U_MASTER.REG_BK='" + unitRegObj.FundCode.ToString() + "' AND U_MASTER.REG_BR='" + unitRegObj.BranchCode.ToString() + "'");
            bool flag = true;
            if (unitRegObj.RegNumber != "")
            {
                sbMaseter.Append(" AND U_MASTER.REG_NO=" + Convert.ToInt32(unitRegObj.RegNumber));
            }
            else if (unitRegObj.BO != "")
            {
                sbMaseter.Append(" AND U_MASTER.BO='" + unitRegObj.BO + "'");
            }
            else if (unitRegObj.Folio != "")
            {
                sbMaseter.Append(" AND U_MASTER.FOLIO_NO='" + unitRegObj.Folio + "'");
            }
            else
            {
                flag = false;
            }
            if (flag == true)
            {
                dtRegInfo = commonGatewayObj.Select(sbMaseter.ToString());

            }           

            return dtRegInfo;
        }
        public bool validationWeight(int weight, string fundCode)
        {
            if (fundCode.ToUpper() == "CFUF" || fundCode.ToUpper() == "IUF")
            {
                if (weight == 1 || weight == 5 || weight == 10 || weight == 20 || weight == 50 || weight == 100 || weight == 250 || weight == 500 || weight == 1000 || weight == 5000 || weight == 10000||weight==20000)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (fundCode.ToUpper() == "BDF" || fundCode.ToUpper() == "CFUF" || fundCode.ToUpper() == "IUF")
            {
                if (weight == 1 || weight == 5 || weight == 10 || weight == 20 || weight == 50 || weight == 100 || weight == 250 || weight == 500 || weight == 1000 || weight == 5000 || weight == 10000)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (weight == 1 || weight == 5 || weight == 10 || weight == 20 || weight == 50 || weight == 100 || weight == 250 || weight == 500 || weight == 1000 || weight == 5000 )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool validationDino(string dino,string fundCode)
        {
            if (fundCode.ToString().ToUpper() == "CFUF" || fundCode.ToString().ToUpper() == "IUF")//  Converted First Fund
            {
                if (dino.ToString().ToUpper() == "A" || dino.ToString().ToUpper() == "B" || dino.ToString().ToUpper() == "C" || dino.ToString().ToUpper() == "D" || dino.ToString().ToUpper() == "E" || dino.ToString().ToUpper() == "F" || dino.ToString().ToUpper() == "G" || dino.ToString().ToUpper() == "H" || dino.ToString().ToUpper() == "I" || dino.ToString().ToUpper() == "J" || dino.ToString().ToUpper() == "K" || dino.ToString().ToUpper() == "L" || dino.ToString().ToUpper() == "Y")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (fundCode.ToString().ToUpper() == "BDF" || fundCode.ToString().ToUpper() == "CFUF" || fundCode.ToString().ToUpper() == "IUF")// for BD Fund and Converted  First Fund
            {
                if (dino.ToString().ToUpper() == "A" || dino.ToString().ToUpper() == "B" || dino.ToString().ToUpper() == "C" || dino.ToString().ToUpper() == "D" || dino.ToString().ToUpper() == "E" || dino.ToString().ToUpper() == "F" || dino.ToString().ToUpper() == "G" || dino.ToString().ToUpper() == "H" || dino.ToString().ToUpper() == "I" || dino.ToString().ToUpper() == "J" || dino.ToString().ToUpper() == "K" || dino.ToString().ToUpper() == "Y")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else // for rest of the fund
            {
                if (dino.ToString().ToUpper() == "A" || dino.ToString().ToUpper() == "B" || dino.ToString().ToUpper() == "C" || dino.ToString().ToUpper() == "D" || dino.ToString().ToUpper() == "E" || dino.ToString().ToUpper() == "F" || dino.ToString().ToUpper() == "G" || dino.ToString().ToUpper() == "H" || dino.ToString().ToUpper() == "I" || dino.ToString().ToUpper() == "J" || dino.ToString().ToUpper() == "Y")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public int GetMaxSaleNo(UnitHolderRegistration regObj)
        {
            DataTable dtMaxSaleNo = new DataTable();
            dtMaxSaleNo = commonGatewayObj.Select("SELECT MAX(SL_NO) AS MAX_SL_NO FROM SALE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'");
            //DataTable dtMaxSaleNo = commonGatewayObj.Select("SELECT MAX(SL_NO) AS MAX_SL_NO FROM SALE WHERE REG_BK='"+regObj.FundCode.ToString().ToUpper()+"' AND REG_BR='"+regObj.BranchCode.ToString().ToUpper()+"'");
            int maxSaleNo = dtMaxSaleNo.Rows[0]["MAX_SL_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtMaxSaleNo.Rows[0]["MAX_SL_NO"].ToString());
            return maxSaleNo + 1;
        }
        public DateTime getLastSaleDate(UnitHolderRegistration regObj, UnitSale salObj)
        {
            DataTable dtLastSaleDate = new DataTable();
            dtLastSaleDate = commonGatewayObj.Select("SELECT MAX(SL_DT) AS MAX_SL_DATE FROM SALE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'AND SL_NO=" + salObj.SaleNo);
            DateTime lastSaleDate = Convert.ToDateTime(dtLastSaleDate.Rows[0]["MAX_SL_DATE"].Equals(DBNull.Value) ? DateTime.Today.ToString() : dtLastSaleDate.Rows[0]["MAX_SL_DATE"].ToString());
            return lastSaleDate;

        }
        public decimal getLastSaleRate(UnitHolderRegistration regObj, UnitSale salObj)
        {
            decimal lastSaleRate = 0;
            DataTable dtLastSaleRate = new DataTable();
            dtLastSaleRate = commonGatewayObj.Select("SELECT DISTINCT SL_PRICE AS MAX_SL_PRICE FROM SALE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND SL_DT IN (SELECT MAX(SL_DT) AS MAX_SL_DATE FROM SALE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND SL_NO=" + salObj.SaleNo + ")");
            if (dtLastSaleRate.Rows.Count > 0)
            {
                lastSaleRate = Convert.ToDecimal(dtLastSaleRate.Rows[0]["MAX_SL_PRICE"].ToString());
            }
            return lastSaleRate;

        }
        public int duplicateCerNoReg(UnitHolderRegistration regObj, string dino, string certNo)
        {
            DataTable dtDuplicateCertNoReg = commonGatewayObj.Select("SELECT REG_NO FROM SALE_CERT WHERE REG_BK='" + regObj.FundCode.ToString() + "' AND REG_BR='" + regObj.BranchCode.ToString() + "' AND CERT_TYPE='" + dino.ToString().ToUpper() + "' AND CERT_NO=" + Convert.ToInt32(certNo.ToString()));
            int duplicateCerNoReg = 0;
            if (dtDuplicateCertNoReg.Rows.Count > 0)
            {
                duplicateCerNoReg = dtDuplicateCertNoReg.Rows[0]["REG_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtDuplicateCertNoReg.Rows[0]["REG_NO"].ToString());
            }
            else
            {
                dtDuplicateCertNoReg = commonGatewayObj.Select("SELECT REG_NO FROM RENEWAL_OUT WHERE REG_BK='" + regObj.FundCode.ToString() + "' AND REG_BR='" + regObj.BranchCode.ToString() + "' AND CERT_TYPE='" + dino.ToString().ToUpper() + "' AND CERT_NO=" + Convert.ToInt32(certNo.ToString()));
                if (dtDuplicateCertNoReg.Rows.Count > 0)
                {
                    duplicateCerNoReg = dtDuplicateCertNoReg.Rows[0]["REG_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtDuplicateCertNoReg.Rows[0]["REG_NO"].ToString());
                }
            }

            return duplicateCerNoReg;
        }
        public bool GetUserLevelPermission(UnitHolderRegistration regObj, UnitUser userReg)
        {
            bool userLevelPermission = false;


            DataTable dtUserInfo = commonGatewayObj.Select("SELECT * FROM USER_INFO WHERE USER_ID='" + userReg.UserID.ToString() + "' AND USER_PASS='" + userReg.UserPassword.ToString() + "' AND FUND_CD='" + regObj.FundCode.ToString() + "' AND BR_CD='" + regObj.BranchCode.ToString() + "'");

            if (dtUserInfo.Rows.Count > 0)
            {
                string userLevel = dtUserInfo.Rows[0]["USER_LEVEL"].Equals(DBNull.Value) ? "" : dtUserInfo.Rows[0]["USER_LEVEL"].ToString();
                string branchCode = dtUserInfo.Rows[0]["BR_CD"].Equals(DBNull.Value) ? "" : dtUserInfo.Rows[0]["BR_CD"].ToString();
                if (string.Compare(branchCode, "01", true) == 0 || string.Compare(branchCode, "ALL", true) == 0)
                {
                    userLevelPermission = true;
                }
                else
                {
                    if (string.Compare(branchCode, regObj.BranchCode.ToString(), true) == 0)
                    {
                        userLevelPermission = true;
                    }
                    else
                    {
                        userLevelPermission = false;
                    }
                }
            }
            else
            {
                if (string.Compare(userReg.UserID.ToString(), "admin", true) == 0)
                {
                    userLevelPermission = true;
                }
                else
                {
                    userLevelPermission = false;
                }
            }

            return userLevelPermission;
        }
        public DataTable getDtTotalSaleUnitCerts(UnitHolderRegistration regObj)
        {
            DataTable dtTotalSaleUnitCerts = new DataTable();
            StringBuilder sbQueryString = new StringBuilder();
            sbQueryString.Append(" SELECT  A.SL_NO,A.CERTIFICATE,A.QTY FROM ( ");
            sbQueryString.Append(" SELECT 'S' || SL_NO AS SL_NO, CERTIFICATE, QTY  FROM  SALE_CERT WHERE  (STATUS_FLAG IS NULL) AND (REG_BK = '" + regObj.FundCode.ToString() + "')");
            sbQueryString.Append(" AND (REG_NO =" + regObj.RegNumber.ToString() + ") AND (REG_BR = '" + regObj.BranchCode.ToString() + "') AND (VALID IS NULL) UNION ALL");
            sbQueryString.Append(" SELECT 'T' || TR_NO AS SL_NO, CERTIFICATE, QTY FROM   TRANS_CERT WHERE (STATUS_FLAG IS NULL) AND (VALID IS NULL)  AND (BR_CODE = '" + regObj.FundCode.ToString() + "_" + regObj.BranchCode.ToString() + "') AND (TR_NO IN");
            sbQueryString.Append(" (SELECT TR_NO FROM  TRANSFER WHERE (REG_BK_I = '" + regObj.FundCode.ToString() + "') AND (BR_CODE = '" + regObj.FundCode.ToString() + "_" + regObj.BranchCode.ToString() + "')AND (REG_NO_I = " + regObj.RegNumber.ToString() + ") AND ");
            sbQueryString.Append(" (REG_BR_I = '" + regObj.BranchCode.ToString() + "')AND (VALID IS NULL))) UNION ALL");
            sbQueryString.Append(" SELECT 'R' || REN_NO AS SL_NO, CERTIFICATE, QTY FROM  RENEWAL_OUT  WHERE (STATUS_FLAG IS NULL) AND (REG_BK = '" + regObj.FundCode.ToString() + "')");
            sbQueryString.Append(" AND (REG_NO = " + regObj.RegNumber.ToString() + ") AND (REG_BR = '" + regObj.BranchCode.ToString() + "') AND (VALID IS NULL) ) A");
            sbQueryString.Append(" WHERE A.CERTIFICATE NOT IN ( SELECT CERTIFICATE FROM REP_CERT_NO WHERE (REG_BK = '" + regObj.FundCode.ToString() + "')");
            sbQueryString.Append(" AND (REG_NO = " + regObj.RegNumber.ToString() + ") AND (REG_BR = '" + regObj.BranchCode.ToString() + "') )");
            sbQueryString.Append(" ORDER BY SUBSTR(SL_NO,2) ");            
            dtTotalSaleUnitCerts = commonGatewayObj.Select(sbQueryString.ToString());          
            return dtTotalSaleUnitCerts;

        }
        public DataTable getDtTotalSaleUnitCertsCDS(UnitHolderRegistration regObj)
        {
            DataTable dtTotalSaleUnitBalance = new DataTable();
          
            StringBuilder sbQueryString = new StringBuilder();
            sbQueryString.Append("SELECT   'S' || SL_NO AS SL_TR_NO,QTY - NVL(QTY_OUT, 0) AS SURRENDER_UNITS, NVL(QTY_OUT, 0) AS OUT_UNITS, QTY - NVL(QTY_OUT, 0)  AS EXIST_UNITS ");
            sbQueryString.Append(" FROM   SALE  WHERE  QTY>NVL(QTY_OUT,0) AND(REG_BK = '" + regObj.FundCode.ToString() + "') AND (REG_BR = '" + regObj.BranchCode.ToString() + "') AND (REG_NO = " + regObj.RegNumber.ToString() + ")");
            sbQueryString.Append(" UNION ALL ");
            sbQueryString.Append(" SELECT 'T' || TR_NO AS SL_TR_NO, QTY - NVL(QTY_OUT, 0) AS SURRENDER_UNITS, NVL(QTY_OUT, 0) AS OUT_UNITS, QTY - NVL(QTY_OUT, 0) AS EXIST_UNITS  FROM   TRANSFER ");
            sbQueryString.Append(" WHERE QTY>NVL(QTY_OUT,0) AND (REG_BK_I = '" + regObj.FundCode.ToString() + "') AND (REG_BR_I = '" + regObj.BranchCode.ToString() + "') AND (REG_NO_I = " + regObj.RegNumber.ToString() + ") ");
           


            dtTotalSaleUnitBalance = commonGatewayObj.Select(sbQueryString.ToString());
            return dtTotalSaleUnitBalance;

        }
        public decimal getTotalSaleUnitBalance(UnitHolderRegistration regObj)
        {
            DataTable dtTotalSaleUnitBalance = new DataTable();
            decimal totalSaleUnitBalance = 0;
            StringBuilder sbQueryString = new StringBuilder();
            sbQueryString.Append(" SELECT SUM(T.QTY) AS TOTAL_BALANCE FROM (");
            sbQueryString.Append(" SELECT 'S' || SL_NO AS SL_NO, CERTIFICATE, QTY  FROM  SALE_CERT WHERE  (STATUS_FLAG IS NULL) AND (REG_BK = '" + regObj.FundCode.ToString() + "')");
            sbQueryString.Append(" AND (REG_NO =" + regObj.RegNumber.ToString() + ") AND (REG_BR = '" + regObj.BranchCode.ToString() + "') AND (VALID IS NULL) UNION ALL");
            sbQueryString.Append(" SELECT 'T' || TR_NO AS SL_NO, CERTIFICATE, QTY FROM   TRANS_CERT WHERE (STATUS_FLAG IS NULL) AND (VALID IS NULL)  AND (BR_CODE = '" + regObj.FundCode.ToString() + "_" + regObj.BranchCode.ToString() + "') AND (TR_NO IN");
            sbQueryString.Append(" (SELECT TR_NO FROM  TRANSFER WHERE (REG_BK_I = '" + regObj.FundCode.ToString() + "') AND (BR_CODE = '" + regObj.FundCode.ToString() + "_" + regObj.BranchCode.ToString() + "')AND (REG_NO_I = " + regObj.RegNumber.ToString() + ") AND ");
            sbQueryString.Append(" (REG_BR_I = '" + regObj.BranchCode.ToString() + "')AND (VALID IS NULL))) UNION ALL");
            sbQueryString.Append(" SELECT 'R' || REN_NO AS SL_NO, CERTIFICATE, QTY FROM  RENEWAL_OUT  WHERE (STATUS_FLAG IS NULL) AND (REG_BK = '" + regObj.FundCode.ToString() + "')");
            sbQueryString.Append("  AND (REG_NO = " + regObj.RegNumber.ToString() + ") AND (REG_BR = '" + regObj.BranchCode.ToString() + "') AND (VALID IS NULL)");
            sbQueryString.Append(" ) T");
            sbQueryString.Append(" WHERE T.CERTIFICATE NOT IN ( SELECT CERTIFICATE FROM REP_CERT_NO WHERE (REG_BK = '" + regObj.FundCode.ToString() + "')");
            sbQueryString.Append(" AND (REG_NO = " + regObj.RegNumber.ToString() + ") AND (REG_BR = '" + regObj.BranchCode.ToString() + "') )");         
            dtTotalSaleUnitBalance = commonGatewayObj.Select(sbQueryString.ToString());           
            if (dtTotalSaleUnitBalance.Rows.Count > 0)
            {
                totalSaleUnitBalance = Convert.ToDecimal(dtTotalSaleUnitBalance.Rows[0]["TOTAL_BALANCE"].Equals(DBNull.Value) ? "0" : dtTotalSaleUnitBalance.Rows[0]["TOTAL_BALANCE"].ToString());
            }
            return totalSaleUnitBalance;

        }
        public decimal getTotalSaleUnitBalanceCDS(UnitHolderRegistration regObj)
        {
            DataTable dtTotalSaleUnitBalance = new DataTable();
            decimal totalSaleUnitBalance = 0;
            StringBuilder sbQueryString = new StringBuilder();
            sbQueryString.Append("SELECT SUM(DECODE(SL_TYPE, 'SL', QTY, 0)) + SUM(DECODE(SL_TYPE, 'TI', QTY, 0)) - SUM(DECODE(SL_TYPE, 'TO', QTY, 0)) - SUM(DECODE(SL_TYPE, 'RP', QTY, 0)) AS TOTAL_BALANCE ");
            sbQueryString.Append(" FROM (SELECT   REG_BK, REG_BR, REG_NO, SUM(QTY) AS QTY, 'SL' AS SL_TYPE FROM SALE ");
            sbQueryString.Append(" WHERE  (REG_BK = '" + regObj.FundCode.ToString() + "') AND (REG_BR = '" + regObj.BranchCode.ToString() + "') AND (REG_NO = " + regObj.RegNumber.ToString() + ") ");
            sbQueryString.Append(" GROUP BY REG_BK, REG_BR, REG_NO UNION ALL ");
            sbQueryString.Append(" SELECT REG_BK, REG_BR, REG_NO, SUM(QTY) AS QTY, 'RP' AS SL_TYPE  FROM REPURCHASE ");
            sbQueryString.Append(" WHERE  (REG_BK = '" + regObj.FundCode.ToString() + "') AND (REG_BR = '" + regObj.BranchCode.ToString() + "') AND (REG_NO = " + regObj.RegNumber.ToString() + ") ");
            sbQueryString.Append(" GROUP BY REG_BK, REG_BR, REG_NO UNION ALL ");
            sbQueryString.Append(" SELECT  REG_BK_I, REG_BR_I, REG_NO_I, SUM(QTY) AS QTY, 'TI' AS SL_TYPE  FROM TRANSFER ");
            sbQueryString.Append(" WHERE (REG_BK_I = '" + regObj.FundCode.ToString() + "') AND (REG_BR_I = '" + regObj.BranchCode.ToString() + "') AND (REG_NO_I = " + regObj.RegNumber.ToString() + ") ");
            sbQueryString.Append(" GROUP BY REG_BK_I, REG_BR_I, REG_NO_I UNION ALL ");
            sbQueryString.Append(" SELECT  REG_BK_O, REG_BR_O, REG_NO_O, SUM(QTY) AS QTY, 'TO' AS SL_TYPE FROM  TRANSFER TRANSFER_1 ");
            sbQueryString.Append(" WHERE (REG_BK_O = '" + regObj.FundCode.ToString() + "') AND (REG_BR_O = '" + regObj.BranchCode.ToString() + "') AND (REG_NO_O = " + regObj.RegNumber.ToString() + ")");
            sbQueryString.Append(" GROUP BY REG_BK_O, REG_BR_O, REG_NO_O) A");
         

            dtTotalSaleUnitBalance = commonGatewayObj.Select(sbQueryString.ToString());
            if (dtTotalSaleUnitBalance.Rows.Count > 0)
            {
                totalSaleUnitBalance = Convert.ToDecimal(dtTotalSaleUnitBalance.Rows[0]["TOTAL_BALANCE"].Equals(DBNull.Value) ? "0" : dtTotalSaleUnitBalance.Rows[0]["TOTAL_BALANCE"].ToString());
            }
            return totalSaleUnitBalance;

        }
        public DataTable getTableDataGrid()
        {
            DataTable dtTableDataGrid = new DataTable();
            dtTableDataGrid.Columns.Add("SL_NO", typeof(string));
            dtTableDataGrid.Columns.Add("CERTIFICATE", typeof(string));
            dtTableDataGrid.Columns.Add("QTY", typeof(string));
            return dtTableDataGrid;

        }
        public DataTable getTableDataGridCDS()
        {
            DataTable dtTableDataGrid = new DataTable();
            dtTableDataGrid.Columns.Add("SL_TR_NO", typeof(string));
            dtTableDataGrid.Columns.Add("SURRENDER_UNITS", typeof(string));
            dtTableDataGrid.Columns.Add("EXIST_UNITS", typeof(string));
            return dtTableDataGrid;

        }
        public DataTable getTableTransfer()
        {
            DataTable dtTransfer = new DataTable();
            dtTransfer.Columns.Add("SL_NO", typeof(string));
            dtTransfer.Columns.Add("QTY", typeof(string));
            dtTransfer.Columns.Add("CERTIFICATE", typeof(string));
            return dtTransfer;
        }
        public DataTable dtGetBranchWiseSL_TR_NO(string fundCode, string branchCode)
        {
            StringBuilder sbSQLstring = new StringBuilder();
            sbSQLstring.Append(" SELECT  'S' || TO_CHAR (SL_NO) AS SL_TR_NO, REG_BK, REG_BR, REG_NO, ");
            sbSQLstring.Append(" CERT_TYPE, CERT_NO, CERTIFICATE, QTY, VALID, REMARKS,STATUS_FLAG,CURR_REG_BK, CURR_REG_BR, CURR_REG_NO, USER_NM,");
            sbSQLstring.Append(" ENT_DT, ENT_TM FROM  SALE_CERT WHERE  (CERTIFICATE IN (SELECT CERTIFICATE FROM   REP_CERT_NO WHERE      (SL_TR_NO = 'S' OR");
            sbSQLstring.Append(" SL_TR_NO = 'S-') AND (REG_BR = '" + branchCode + "'))) AND (REG_BR = '" + branchCode + "')AND (REG_BK = '" + fundCode + "')");
            DataTable dtGetBranchWiseSL_TR_NO = commonGatewayObj.Select(sbSQLstring.ToString());
            return dtGetBranchWiseSL_TR_NO;
        }
        public DataTable dtRemoveRow(DataTable dtTargetTable, ArrayList rowIndex)
        {
            DataTable dtReturnTable = new DataTable();

            for (int loop = rowIndex.Count - 1; loop >= 0; loop--)
            {
                dtTargetTable.Rows[Convert.ToInt16(rowIndex[loop].ToString())].Delete();

            }
            dtTargetTable.AcceptChanges();
            dtReturnTable = dtTargetTable;
            return dtReturnTable;
        }
        public DataTable getDtMenuList()
        {
            DataTable dtMenuList = commonGatewayObj.Select("SELECT M_ID,M_NAME FROM MENU WHERE VALID='Y' and M_PARENT_ID<>0 ORDER BY M_ID");
            return dtMenuList;
        }
        public DataTable getBranchForMenu()
        {
            DataTable dtBranchList = commonGatewayObj.Select("SELECT BR_CD,BR_NM FROM BRANCH_INFO ORDER BY BR_CD");
            return dtBranchList;
        }
        public DataTable getFundForMenu()
        {
            DataTable dtFundList = commonGatewayObj.Select("SELECT FUND_CD , FUND_NM FROM FUND_INFO  ORDER BY FUND_CD");
            return dtFundList;
        }
        public DataTable dtFillBankName()
        {
            DataTable dtBankName = commonGatewayObj.Select("SELECT BANK_CODE , BANK_NAME FROM BANK_NAME ORDER BY BANK_NAME");
            DataTable dtBankNameDropDown = new DataTable();
            dtBankNameDropDown.Columns.Add("BANK_CODE", typeof(string));
            dtBankNameDropDown.Columns.Add("BANK_NAME", typeof(string));

            DataRow drBankNameDropDown = dtBankNameDropDown.NewRow();
            drBankNameDropDown["BANK_NAME"] = "--Select Bank--- ";
            drBankNameDropDown["BANK_CODE"] = "0";
            dtBankNameDropDown.Rows.Add(drBankNameDropDown);
            for (int loop = 0; loop < dtBankName.Rows.Count; loop++)
            {
                drBankNameDropDown = dtBankNameDropDown.NewRow();
                drBankNameDropDown["BANK_NAME"] = dtBankName.Rows[loop]["BANK_NAME"].ToString();
                drBankNameDropDown["BANK_CODE"] = dtBankName.Rows[loop]["BANK_CODE"].ToString();
                dtBankNameDropDown.Rows.Add(drBankNameDropDown);
            }

            return dtBankNameDropDown;
        }
        public DataTable dtFillBankName(string filter)
        {
            DataTable dtBankName = commonGatewayObj.Select("SELECT BANK_CODE , BANK_NAME FROM BANK_NAME WHERE 1=1 and "+ filter .ToString()+"  ORDER BY BANK_NAME ");
            DataTable dtBankNameDropDown = new DataTable();
            dtBankNameDropDown.Columns.Add("BANK_CODE", typeof(string));
            dtBankNameDropDown.Columns.Add("BANK_NAME", typeof(string));

            DataRow drBankNameDropDown = dtBankNameDropDown.NewRow();
            drBankNameDropDown["BANK_NAME"] = "--Select Bank--- ";
            drBankNameDropDown["BANK_CODE"] = "0";
            dtBankNameDropDown.Rows.Add(drBankNameDropDown);
            for (int loop = 0; loop < dtBankName.Rows.Count; loop++)
            {
                drBankNameDropDown = dtBankNameDropDown.NewRow();
                drBankNameDropDown["BANK_NAME"] = dtBankName.Rows[loop]["BANK_NAME"].ToString();
                drBankNameDropDown["BANK_CODE"] = dtBankName.Rows[loop]["BANK_CODE"].ToString();
                dtBankNameDropDown.Rows.Add(drBankNameDropDown);
            }

            return dtBankNameDropDown;
        }
        public DataTable dtFillBranchName(int bankCode)
        {
            DataTable dtBankName = commonGatewayObj.Select("SELECT BRANCH_CODE , BRANCH_NAME FROM BANK_BRANCH where bank_code=" + bankCode + " ORDER BY BRANCH_NAME");
            DataTable dtBankNameDropDown = new DataTable();
            dtBankNameDropDown.Columns.Add("BRANCH_CODE", typeof(string));
            dtBankNameDropDown.Columns.Add("BRANCH_NAME", typeof(string));

            DataRow drBankNameDropDown = dtBankNameDropDown.NewRow();
            drBankNameDropDown["BRANCH_NAME"] = "--Select Branch--- ";
            drBankNameDropDown["BRANCH_CODE"] = "0";
            dtBankNameDropDown.Rows.Add(drBankNameDropDown);
            for (int loop = 0; loop < dtBankName.Rows.Count; loop++)
            {
                drBankNameDropDown = dtBankNameDropDown.NewRow();
                drBankNameDropDown["BRANCH_NAME"] = dtBankName.Rows[loop]["BRANCH_NAME"].ToString();
                drBankNameDropDown["BRANCH_CODE"] = dtBankName.Rows[loop]["BRANCH_CODE"].ToString();
                dtBankNameDropDown.Rows.Add(drBankNameDropDown);
            }

            return dtBankNameDropDown;
        }
        public bool IsValidRegistration(UnitHolderRegistration regObj)
        {
            DataTable dtValidReg = new DataTable();
            bool validReg = false;

            string queryString = "SELECT * FROM U_MASTER WHERE REG_BK='" + regObj.FundCode.ToString() + "' AND REG_BR='" + regObj.BranchCode + "' AND REG_NO=" + Convert.ToInt32(regObj.RegNumber.ToString());          
            dtValidReg = commonGatewayObj.Select(queryString.ToString());           
            if (dtValidReg.Rows.Count > 0)
            {
                if (dtValidReg.Rows[0]["REG_NO"].Equals(DBNull.Value))
                {
                    validReg = false;
                }
                else
                {
                    int Reg = Convert.ToInt32(dtValidReg.Rows[0]["REG_NO"].ToString());
                    if (regObj.RegNumber.ToString() == Reg.ToString())
                    {
                        validReg = true;
                    }
                    else
                    {
                        validReg = false;
                    }
                }
            }
            else
            {
                validReg = false;
            }

            return validReg;

        }
        public DataTable dtValidSearch(UnitHolderRegistration regObj)
        {
            DataTable dtRegInfo = new DataTable();
            StringBuilder sbMaseter = new StringBuilder();
            sbMaseter.Append("SELECT U_MASTER.* FROM  U_MASTER  WHERE U_MASTER.REG_BK='" + regObj.FundCode.ToString() + "' AND U_MASTER.REG_BR='" + regObj.BranchCode.ToString() + "'");
            bool flag = true;
            if (regObj.RegNumber != "")
            {
                sbMaseter.Append(" AND U_MASTER.REG_NO=" + Convert.ToInt32(regObj.RegNumber));
            }
            else if (regObj.BO != "")
            {
                sbMaseter.Append(" AND U_MASTER.BO='" + regObj.BO + "'");
            }
            else if (regObj.Folio != "")
            {
                sbMaseter.Append(" AND U_MASTER.FOLIO_NO='" + regObj.Folio + "'");
            }
            else
            {
                flag = false;
            }
            if (flag == true)
            {
                dtRegInfo = commonGatewayObj.Select(sbMaseter.ToString());

            }

            return dtRegInfo;

        }
        public decimal getMaxRepPrice(string fundCode)
        {
            DataTable dtMaxReprice = commonGatewayObj.Select("SELECT * FROM   PRICE_REFIX WHERE (REFIX_DT IN  (SELECT MAX(REFIX_DT) AS EXPR1 FROM  PRICE_REFIX PRICE_REFIX_1)) AND (FUND_CD = '" + fundCode.ToString() + "')");
            decimal maxRepPrice = Convert.ToDecimal(dtMaxReprice.Rows[0]["REFIX_REP_PR"].ToString());
            return maxRepPrice;

        }
        //process level function
        public string CheckDuplicateTransferEntry()
        {
            bool flag = false;
            string tr_no = "";
            string cert_type = "";
            DataTable dtTerget = commonGatewayObj.Select("SELECT * FROM TRANS_CERT");
            DataTable dtSearch = dtTerget;
            for (int i = 0; i < dtTerget.Rows.Count; i++)
            {
                for (int loop = i + 1; loop < dtSearch.Rows.Count; loop++)
                {
                    if (dtTerget.Rows[i]["CERT_TYPE"].ToString() == dtSearch.Rows[loop]["CERT_TYPE"].ToString() && dtTerget.Rows[i]["CERT_NO"].ToString() == dtSearch.Rows[loop]["CERT_NO"].ToString() && dtTerget.Rows[i]["F_CD"].ToString() == dtSearch.Rows[loop]["F_CD"].ToString() && dtTerget.Rows[i]["TR_NO"].ToString() == dtSearch.Rows[loop]["TR_NO"].ToString() && dtTerget.Rows[i]["BR_CODE"].ToString() == dtSearch.Rows[loop]["BR_CODE"].ToString())
                    {
                        flag = true;
                        cert_type = dtSearch.Rows[loop]["CERT_TYPE"].ToString();
                        tr_no = dtSearch.Rows[loop]["CERT_NO"].ToString();
                    }
                }
            }
            return "";
        }
        public DataTable dtgetLastPrice(string fundCode)
        {
            DataTable dtLastPrice = commonGatewayObj.Select("SELECT * FROM PRICE_REFIX WHERE  FUND_CD='" + fundCode.ToString().ToUpper() + "' AND ENT_DT IN (SELECT MAX(ENT_DT) FROM PRICE_REFIX WHERE  FUND_CD='" + fundCode.ToString().ToUpper() + "')");
            return dtLastPrice;
        }

        public int getRegNoByFolio(string folio)
        {
            int regNo = 0;
            DataTable dtRegNo = commonGatewayObj.Select(" SELECT * FROM CMF_CDBL WHERE BO='" + folio + "' and FUND_CODE=29 ");
            if (dtRegNo.Rows.Count > 0)
            {
                regNo = int.Parse(dtRegNo.Rows[0]["VOTTER_NO"].ToString());
            }
            return regNo;
        }
        public string getCDSStatus(string fund_Code)
        {
            string CDS = "N";
            DataTable dtCDS = commonGatewayObj.Select("SELECT * FROM FUND_INFO WHERE FUND_CD='"+fund_Code.ToString().ToUpper()+"'");
            if (dtCDS.Rows.Count > 0)
            {
                if(!dtCDS.Rows[0]["CDS"].Equals(DBNull.Value))
                {
                     CDS = dtCDS.Rows[0]["CDS"].ToString().ToUpper();
                }
            }
            return CDS;

        }
        public string getFundCSSColourCode(string fundCode)
        {

            DataTable dtCSSColourCode = commonGatewayObj.Select("SELECT NVL(CSS_COLOUR,'#CC9900') AS CSS_COLOUR FROM FUND_INFO WHERE FUND_CD='" + fundCode.ToUpper() + "' ");
            return dtCSSColourCode.Rows[0]["CSS_COLOUR"].ToString();
        }
    }
}