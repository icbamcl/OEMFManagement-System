using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Collections;
using System.Data.OracleClient;
using AMCL.COMMON;
using AMCL.DL;
using AMCL.GATEWAY;

/// <summary>
/// Summary description for UnitReg
/// </summary>
namespace AMCL.BL
{
    public class UnitHolderRegBL
    {

        CommonGateway commonGatewayObj = new CommonGateway();
        OMFDAO OmfDAOObj = new OMFDAO();
        public UnitHolderRegBL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public void SaveRegInfo(UnitHolderRegistration regObj, UnitHolderInfo unitHolderObj,UnitHolderBankInfo bankInfoObj, UnitUser userObj) //save regInfo   UnitJointHolderInfo jHolderObj, UnitHolderNominee nomiObj, UnitHolderBankInfo bankInfoObj,
        {
            Hashtable htReg = new Hashtable();
            Hashtable htJointHolder = new Hashtable();
            Hashtable htNominee = new Hashtable();
            try
            {
                commonGatewayObj.BeginTransaction();
                //U_master
                htReg.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                htReg.Add("REG_BR", regObj.BranchCode.ToString());
                htReg.Add("REG_NO", regObj.RegNumber.ToString());
                htReg.Add("REG_TYPE", regObj.RegType.ToString().ToUpper());
                htReg.Add("REG_DT", regObj.RegDate.ToString());
                htReg.Add("CIP", regObj.RegIsCIP.ToString().ToUpper());

                if (regObj.IsIDAccount.ToString() == "N")
                {
                    htReg.Add("ID_FLAG", regObj.IsIDAccount.ToString().ToUpper());
                }
                else if (regObj.IsIDAccount.ToString() == "Y")
                {
                    htReg.Add("ID_FLAG", regObj.IsIDAccount.ToString().ToUpper());
                    htReg.Add("ID_AC", Convert.ToInt64(regObj.IdAccNo));
                    htReg.Add("ID_BK_NM_CD", Convert.ToInt16(regObj.IdBankID));
                    htReg.Add("ID_BK_BR_NM_CD", Convert.ToInt16(regObj.IdBankBranchID));
                }

                htReg.Add("HNAME", unitHolderObj.HolderName.ToString().ToUpper());
                htReg.Add("FMH_NAME", unitHolderObj.HolderFMHName.ToString().ToUpper());
                htReg.Add("MO_NAME", unitHolderObj.HolderMotherName.ToString().ToUpper());
                htReg.Add("SP_NAME", unitHolderObj.HolderSpouceName.ToString().ToUpper());
                if (unitHolderObj.HolderBONumber.ToString() != "")
                {
                    htReg.Add("BO", unitHolderObj.HolderBONumber.ToString().ToUpper());
                }
                htReg.Add("NID", unitHolderObj.HolderNID.ToString().ToUpper());
                htReg.Add("PASS_NO", unitHolderObj.HolderPassport.ToString().ToUpper());
                htReg.Add("BIRTH_CERT_NO", unitHolderObj.HolderBirthCertNo.ToString().ToUpper());
                htReg.Add("TIN", unitHolderObj.HolderTIN.ToString().ToUpper());
                if (unitHolderObj.HolderTIN.ToString() != "")
                {
                    htReg.Add("TIN_FLAG", unitHolderObj.HolderTINFlag.ToString().ToUpper());
                }

                htReg.Add("NATIONALITY", unitHolderObj.HolderNationality.ToString().ToUpper());
                htReg.Add("OCC_CODE", unitHolderObj.HolderOccupation);


                htReg.Add("ADDRS1", unitHolderObj.HolderAddress1.ToString().ToUpper());
                htReg.Add("ADDRS2", unitHolderObj.HolderAddress2.ToString().ToUpper());
                if (unitHolderObj.HolderCity.ToString() != "")
                {
                    htReg.Add("CITY", unitHolderObj.HolderCity.ToString().ToUpper());
                }

                htReg.Add("PADDRESS1", unitHolderObj.HolderPermanAddress1.ToString().ToUpper());
                htReg.Add("PADDRESS2", unitHolderObj.HolderPermanAddress2.ToString().ToUpper());
                if (unitHolderObj.HolderPermanCity.ToString() != "")
                {
                    htReg.Add("PCITY", unitHolderObj.HolderPermanCity.ToString().ToUpper());
                }

                htReg.Add("SOURCE_FUND", unitHolderObj.HolderSourceFund.ToString().ToUpper());

                if (unitHolderObj.HolderDateofBirth.ToString() != "")
                {
                    htReg.Add("B_DATE", Convert.ToDateTime(unitHolderObj.HolderDateofBirth).ToString("dd-MMM-yyyy"));
                }
                if (unitHolderObj.HolderSex.ToString() != "0")
                {
                    htReg.Add("SEX", unitHolderObj.HolderSex.ToString().ToUpper());
                }
                if (unitHolderObj.HolderMaritialStatus.ToString() != "0")
                {
                    htReg.Add("MAR_STAT", unitHolderObj.HolderMaritialStatus.ToString().ToUpper());
                }
                if (unitHolderObj.HolderReligion.ToString() != "0")
                {
                    htReg.Add("RELIGION", unitHolderObj.HolderReligion.ToString().ToUpper());
                }
                if (unitHolderObj.HolderEduQua.ToString() != "0")
                {
                    htReg.Add("EDU_QUA", unitHolderObj.HolderEduQua.ToString().ToUpper());
                }
                if (unitHolderObj.HolderTelephone.ToString() != "")
                {
                    htReg.Add("TEL_NO", unitHolderObj.HolderTelephone.ToString());
                }
                if (unitHolderObj.HolderEmail.ToString() != "")
                {
                    htReg.Add("EMAIL", unitHolderObj.HolderEmail.ToString());
                }
                if (unitHolderObj.HolderRemarks.ToString() != "")
                {
                    htReg.Add("REMARKS", unitHolderObj.HolderRemarks.ToString().ToUpper());
                }
                if (unitHolderObj.HolderKYC.ToString() != "")
                {
                    htReg.Add("KYC", unitHolderObj.HolderKYC.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("KYC", DBNull.Value);
                }
                //if (unitHolderObj.HolderNPBNo.ToString() != "")
                //{
                //    htReg.Add("NPB_NO", unitHolderObj.HolderNPBNo.ToString().ToUpper());
                //}
                //htReg.Add("NPB_TYPE", unitHolderObj.HolderNPBType.ToString().ToUpper());

                if (bankInfoObj.IsBankInfo.ToString() == "Y")
                {                                     
                    htReg.Add("BK_AC_NO", bankInfoObj.BankAccountNo.ToString());
                    htReg.Add("BK_NM_CD", Convert.ToInt16(bankInfoObj.BankCode.ToString()));
                    htReg.Add("BK_BR_NM_CD", Convert.ToInt16(bankInfoObj.BankBranchCode.ToString()));
                }
              
                htReg.Add("BK_FLAG", bankInfoObj.IsBankInfo.ToString().ToUpper());
                htReg.Add("IS_BEFTN", bankInfoObj.IsBEFTN.ToString().ToUpper());

                htReg.Add("USER_NM", userObj.UserID.ToString());
                htReg.Add("ENT_DT", DateTime.Today.ToString("dd-MMM-yyyy"));
                htReg.Add("ENT_TM", DateTime.Now.ToShortTimeString());
            
                commonGatewayObj.Insert(htReg, "U_MASTER");
           

                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }

        }
        public void SaveRegInfo(UnitHolderRegistration regObj, UnitHolderInfo unitHolderObj, UnitJointHolderInfo jHolderObj, UnitHolderNominee nomiObj, UnitHolderBankInfo bankInfoObj, UnitUser userObj) //save regInfo
        {
            Hashtable htReg = new Hashtable();
            Hashtable htJointHolder = new Hashtable();
            Hashtable htNominee = new Hashtable();
            try
            {
                commonGatewayObj.BeginTransaction();
                //U_master
                htReg.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                htReg.Add("REG_BR", regObj.BranchCode.ToString());
                htReg.Add("REG_NO", regObj.RegNumber.ToString());
                htReg.Add("REG_TYPE", regObj.RegType.ToString().ToUpper());
                htReg.Add("REG_DT", regObj.RegDate.ToString());
                htReg.Add("CIP", regObj.RegIsCIP.ToString().ToUpper());

                if (regObj.IsIDAccount.ToString() == "N")
                {
                    htReg.Add("ID_FLAG", regObj.IsIDAccount.ToString().ToUpper());
                }
                else if (regObj.IsIDAccount.ToString() == "Y")
                {
                    htReg.Add("ID_FLAG", regObj.IsIDAccount.ToString().ToUpper());
                    htReg.Add("ID_AC", Convert.ToInt64(regObj.IdAccNo));
                    htReg.Add("ID_BK_NM_CD", Convert.ToInt16(regObj.IdBankID));
                    htReg.Add("ID_BK_BR_NM_CD", Convert.ToInt16(regObj.IdBankBranchID));
                }

                htReg.Add("HNAME", unitHolderObj.HolderName.ToString().ToUpper());
                htReg.Add("FMH_NAME", unitHolderObj.HolderFMHName.ToString().ToUpper());
                htReg.Add("MO_NAME", unitHolderObj.HolderMotherName.ToString().ToUpper());
                htReg.Add("NATIONALITY", unitHolderObj.HolderNationality.ToString().ToUpper());
                htReg.Add("OCC_CODE", unitHolderObj.HolderOccupation);
                htReg.Add("ADDRS1", unitHolderObj.HolderAddress1.ToString().ToUpper());
                htReg.Add("ADDRS2", unitHolderObj.HolderAddress2.ToString().ToUpper());
                if (unitHolderObj.HolderCity.ToString() != "")
                {
                    htReg.Add("CITY", unitHolderObj.HolderCity.ToString().ToUpper());
                }
                if (unitHolderObj.HolderDateofBirth.ToString() != "")
                {
                    htReg.Add("B_DATE", Convert.ToDateTime(unitHolderObj.HolderDateofBirth).ToString("dd-MMM-yyyy"));
                }
                if (unitHolderObj.HolderSex.ToString() != "0")
                {
                    htReg.Add("SEX", unitHolderObj.HolderSex.ToString().ToUpper());
                }
                if (unitHolderObj.HolderMaritialStatus.ToString() != "0")
                {
                    htReg.Add("MAR_STAT", unitHolderObj.HolderMaritialStatus.ToString().ToUpper());
                }
                if (unitHolderObj.HolderReligion.ToString() != "0")
                {
                    htReg.Add("RELIGION", unitHolderObj.HolderReligion.ToString().ToUpper());
                }
                if (unitHolderObj.HolderEduQua.ToString() != "0")
                {
                    htReg.Add("EDU_QUA", unitHolderObj.HolderEduQua.ToString().ToUpper());
                }
                if (unitHolderObj.HolderTelephone.ToString() != "")
                {
                    htReg.Add("TEL_NO", unitHolderObj.HolderTelephone.ToString());
                }
                //if (unitHolderObj.HolderTIN.ToString() != "")
                //{
                //    htReg.Add("TIN", unitHolderObj.HolderTIN.ToString());
                //    htReg.Add("TIN_FLAG", unitHolderObj.HolderTIN.ToString());
                //}
                if (unitHolderObj.HolderEmail.ToString() != "")
                {
                    htReg.Add("EMAIL", unitHolderObj.HolderEmail.ToString());
                }
                if (unitHolderObj.HolderRemarks.ToString() != "")
                {
                    htReg.Add("REMARKS", unitHolderObj.HolderRemarks.ToString().ToUpper());
                }
                if (bankInfoObj.IsBankInfo.ToString() == "Y")
                {
                    htReg.Add("BK_FLAG", bankInfoObj.IsBankInfo.ToString().ToUpper());
                    htReg.Add("BK_AC_NO", bankInfoObj.BankAccountNo.ToString());
                    htReg.Add("BK_NM_CD", Convert.ToInt16(bankInfoObj.BankCode.ToString()));
                    htReg.Add("BK_BR_NM_CD", Convert.ToInt16(bankInfoObj.BankBranchCode.ToString()));
                }
                else
                {
                    htReg.Add("BK_FLAG", bankInfoObj.IsBankInfo.ToString().ToUpper());

                }

                htReg.Add("IS_BEFTN", bankInfoObj.IsBEFTN.ToString().ToUpper());



                htReg.Add("USER_NM", userObj.UserID.ToString());
                htReg.Add("ENT_DT", DateTime.Today.ToString("dd-MMM-yyyy"));
                htReg.Add("ENT_TM", DateTime.Now.ToShortTimeString());

                commonGatewayObj.Insert(htReg, "U_MASTER");

                if (nomiObj.Nomi1Name.ToString() != "")
                {
                    htNominee.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htNominee.Add("REG_BR", regObj.BranchCode.ToString());
                    htNominee.Add("REG_NO", regObj.RegNumber.ToString());

                    htNominee.Add("NOMI_NO", 1);
                    htNominee.Add("NOMI_NAME", nomiObj.Nomi1Name.ToString().ToUpper());
                    htNominee.Add("NOMI_CTL_NO", nomiObj.NomiControlNo.ToString());
                    if (nomiObj.Nomi1Nationality.ToString() != "")
                    {
                        htNominee.Add("NOMI_NATIONALITY", nomiObj.Nomi1Nationality.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi1MotherName.ToString() != "") ;
                    {
                        htNominee.Add("NOMI_MO_NAME", nomiObj.Nomi1MotherName.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi1FMName.ToString() != "")
                    {
                        htNominee.Add("NOMI_FMH_NAME", nomiObj.Nomi1FMName.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi1Occupation.ToString() != "0")
                    {
                        htNominee.Add("NOMI_OCC_CODE", nomiObj.Nomi1Occupation);
                    }
                    if (nomiObj.Nomi1Address1.ToString() != "")
                    {
                        htNominee.Add("NOMI_ADDRS1", nomiObj.Nomi1Address1.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi1Address2.ToString() != "")
                    {
                        htNominee.Add("NOMI_ADDRS2", nomiObj.Nomi1Address2.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi1Relation.ToString() != "0")
                    {
                        htNominee.Add("NOMI_REL", nomiObj.Nomi1Relation.ToString().ToUpper());
                    }
                    htNominee.Add("PERCENTAGE", Convert.ToDecimal(nomiObj.Nomi1Percentage.ToString()));

                    commonGatewayObj.Insert(htNominee, "U_NOMINEE");

                }


                if (nomiObj.Nomi2Name.ToString() != "")
                {
                    htNominee = new Hashtable();
                    htNominee.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htNominee.Add("REG_BR", regObj.BranchCode.ToString());
                    htNominee.Add("REG_NO", regObj.RegNumber.ToString());
                    htNominee.Add("NOMI_NO", 2);
                    htNominee.Add("NOMI_NAME", nomiObj.Nomi2Name.ToString().ToUpper());
                    htNominee.Add("NOMI_CTL_NO", nomiObj.NomiControlNo.ToString());
                    if (nomiObj.Nomi2Nationality.ToString() != "")
                    {
                        htNominee.Add("NOMI_NATIONALITY", nomiObj.Nomi2Nationality.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi2FMName.ToString() != "")
                    {
                        htNominee.Add("NOMI_FMH_NAME", nomiObj.Nomi2FMName.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi2MotherName.ToString() != "") ;
                    {
                        htNominee.Add("NOMI_MO_NAME", nomiObj.Nomi2MotherName.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi2Occupation.ToString() != "0")
                    {
                        htNominee.Add("NOMI_OCC_CODE", nomiObj.Nomi2Occupation);
                    }
                    if (nomiObj.Nomi2Address1.ToString() != "")
                    {
                        htNominee.Add("NOMI_ADDRS1", nomiObj.Nomi2Address1.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi2Address2.ToString() != "")
                    {
                        htNominee.Add("NOMI_ADDRS2", nomiObj.Nomi2Address2.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi2Relation.ToString() != "0")
                    {
                        htNominee.Add("NOMI_REL", nomiObj.Nomi2Relation.ToString().ToUpper());
                    }
                    htNominee.Add("PERCENTAGE", Convert.ToDecimal(nomiObj.Nomi2Percentage.ToString()));

                    commonGatewayObj.Insert(htNominee, "U_NOMINEE");

                }


                if (jHolderObj.JHolderName.ToString() != "")
                {

                    htJointHolder.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htJointHolder.Add("REG_BR", regObj.BranchCode.ToString());
                    htJointHolder.Add("REG_NO", regObj.RegNumber.ToString());
                    htJointHolder.Add("JNT_NAME", jHolderObj.JHolderName.ToString().ToUpper());
                    if (jHolderObj.JHolderFMHName.ToString() != "")
                    {
                        htJointHolder.Add("JNT_FMH_NAME", jHolderObj.JHolderFMHName.ToString().ToUpper());
                    }
                    if (jHolderObj.JHolderMotherName.ToString() != "")
                    {
                        htJointHolder.Add("JNT_MO_NAME", jHolderObj.JHolderMotherName.ToString().ToUpper());
                    }
                    if (jHolderObj.JHolderOccupation.ToString() != "0")
                    {
                        htJointHolder.Add("JNT_OCC_CODE", jHolderObj.JHolderOccupation);
                    }
                    if (jHolderObj.JHolderNationality.ToString() != "")
                    {
                        htJointHolder.Add("JNT_NATIONALITY", jHolderObj.JHolderNationality.ToString().ToUpper());
                    }
                    if (jHolderObj.JHolderAddress1.ToString() != "")
                    {
                        htJointHolder.Add("JNT_ADDRS1", jHolderObj.JHolderAddress1.ToString().ToUpper());
                    }
                    if (jHolderObj.JHolderAddress2.ToString() != "")
                    {
                        htJointHolder.Add("JNT_ADDRS2", jHolderObj.JHolderAddress2.ToString().ToUpper());
                    }
                    htJointHolder.Add("JNT_NO", 2);

                    if (!(string.Compare(regObj.FundCode.ToString().ToUpper(), "IAMPH", true) == 0))
                    {
                        commonGatewayObj.Insert(htJointHolder, "U_JHOLDER");//save joint holder info
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
        public void SaveNomineeInfo(UnitHolderRegistration regObj, UnitHolderNominee nomiObj, UnitUser userObj) //save Registration Holders Nominee Info
        {
            Hashtable htNomineeHolder = new Hashtable();

            try
            {
                commonGatewayObj.BeginTransaction();


                if (nomiObj.Nomi1Name.ToString() != "")
                {
                    htNomineeHolder.Add("USER_NM", userObj.UserID.ToString().ToUpper());
                    htNomineeHolder.Add("ENT_TM", DateTime.Now.ToString("dd-MMM-yyyy"));

                    htNomineeHolder.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htNomineeHolder.Add("REG_BR", regObj.BranchCode.ToString());
                    htNomineeHolder.Add("REG_NO", regObj.RegNumber.ToString());
                    htNomineeHolder.Add("NOMI_NAME", nomiObj.Nomi1Name.ToString().ToUpper());
                    htNomineeHolder.Add("NOMI_NO", Convert.ToInt32(nomiObj.NomiNumber.ToString()));
                    
                    htNomineeHolder.Add("NOMI_TYPE", nomiObj.NomiType.ToString());

                    if (nomiObj.NomiControlNo.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_CTL_NO", nomiObj.NomiControlNo.ToString().ToUpper());
                    }

                    if (nomiObj.Nomi1FMName.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_FMH_NAME", nomiObj.Nomi1FMName.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi1MotherName.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_MO_NAME", nomiObj.Nomi1MotherName.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi1Occupation.ToString() != "0")
                    {
                        htNomineeHolder.Add("NOMI_OCC_CODE", nomiObj.Nomi1Occupation);
                    }
                    if (nomiObj.Nomi1Address1.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_ADDRS1", nomiObj.Nomi1Address1.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi1Address2.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_ADDRS2", nomiObj.Nomi1Address2.ToString().ToUpper());
                    }
                    if (nomiObj.NomiCity.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_CITY", nomiObj.NomiCity.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi1Relation.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_REL", nomiObj.Nomi1Relation.ToString().ToUpper());
                    }

                    if (nomiObj.Nomi1Percentage.ToString() != "")
                    {
                        htNomineeHolder.Add("PERCENTAGE",Convert.ToDecimal( nomiObj.Nomi1Percentage.ToString()));
                    }

                    if (nomiObj.NomiRemarks.ToString() != "")
                    {
                        htNomineeHolder.Add("REAMARKS", nomiObj.NomiRemarks.ToString().ToUpper());
                    }
                    if (nomiObj.Nomi1Nationality.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_NATIONALITY", nomiObj.Nomi1Nationality.ToString().ToUpper());
                    }
                    if (nomiObj.NomiDateBirth.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_BIRTH_DT", Convert.ToDateTime(nomiObj.NomiDateBirth.ToString()));
                    }
                    if (nomiObj.NomiISMinor.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_IS_MINOR", nomiObj.NomiISMinor.ToString().ToUpper());
                    }
                    if (nomiObj.NomiISMinor.ToString().ToUpper() == "Y")
                    {
                        if (nomiObj.GardianName.ToString() != "")
                        {
                            htNomineeHolder.Add("NOMI_GARDIAN_NAME", nomiObj.GardianName.ToString().ToUpper());
                        }
                        if (nomiObj.GardianAddress.ToString() != "")
                        {
                            htNomineeHolder.Add("NOMI_GARDIAN_ADDRESS", nomiObj.GardianAddress.ToString().ToUpper());
                        }
                        if (nomiObj.GardianDateOfBirth.ToString() != "")
                        {
                            htNomineeHolder.Add("NOMI_GARDIAN_BIRTH_DT", Convert.ToDateTime(nomiObj.GardianDateOfBirth.ToString()));
                        }
                        if (nomiObj.GardianRelWithNominee.ToString() != "")
                        {
                            htNomineeHolder.Add("GARD_REL_WITH_NOMI", nomiObj.GardianRelWithNominee.ToString().ToUpper());
                        }
                    }
                    commonGatewayObj.Insert(htNomineeHolder, "U_NOMINEE");//save Nominee info                 


                }

                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }

        }
        public void EditNomineeInfo(UnitHolderRegistration regObj, UnitHolderNominee nomiObj, UnitUser userObj) //save Registration Holders Nominee Info
        {
            Hashtable htNomineeHolder = new Hashtable();

            try
            {
                commonGatewayObj.BeginTransaction();

                DataTable dtNomineeInfo = dtGetNomineeInfo(regObj, Convert.ToInt32(nomiObj.NomiNumber.ToString()));

                htNomineeHolder.Add("USER_NM", userObj.UserID.ToString().ToUpper());
                htNomineeHolder.Add("ENT_TM", DateTime.Now.ToString("dd-MMM-yyyy"));
                htNomineeHolder.Add("EDIT_TYPE", "E");
                htNomineeHolder.Add("REG_BK", dtNomineeInfo.Rows[0]["REG_BK"].ToString());
                htNomineeHolder.Add("REG_BR", dtNomineeInfo.Rows[0]["REG_BR"].ToString());
                htNomineeHolder.Add("REG_NO", dtNomineeInfo.Rows[0]["REG_NO"].ToString());

                htNomineeHolder.Add("NOMI_NO", Convert.ToInt32(nomiObj.NomiNumber.ToString()));


                htNomineeHolder.Add("NOMI_NAME", dtNomineeInfo.Rows[0]["NOMI_NAME"].ToString().ToUpper());
                htNomineeHolder.Add("NOMI_FMH_NAME", dtNomineeInfo.Rows[0]["NOMI_FMH_NAME"].ToString().ToUpper());
                htNomineeHolder.Add("NOMI_MO_NAME", dtNomineeInfo.Rows[0]["NOMI_MO_NAME"].ToString().ToUpper());
                if (dtNomineeInfo.Rows[0]["NOMI_TYPE"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_TYPE", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_TYPE", dtNomineeInfo.Rows[0]["NOMI_TYPE"].ToString().ToUpper());
                }
              
                if (dtNomineeInfo.Rows[0]["NOMI_OCC_CODE"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_OCC_CODE", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_OCC_CODE", Convert.ToInt16(dtNomineeInfo.Rows[0]["NOMI_OCC_CODE"].ToString()));
                }

                if (dtNomineeInfo.Rows[0]["NOMI_ADDRS1"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_ADDRS1", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_ADDRS1", dtNomineeInfo.Rows[0]["NOMI_ADDRS1"].ToString().ToUpper());
                }

                if (dtNomineeInfo.Rows[0]["NOMI_ADDRS2"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_ADDRS2", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_ADDRS2", dtNomineeInfo.Rows[0]["NOMI_ADDRS2"].ToString().ToUpper());
                }
                if (dtNomineeInfo.Rows[0]["NOMI_CITY"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_CITY", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_CITY", dtNomineeInfo.Rows[0]["NOMI_CITY"].ToString().ToUpper());
                }


                if (dtNomineeInfo.Rows[0]["NOMI_REL"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_REL", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_REL", dtNomineeInfo.Rows[0]["NOMI_REL"].ToString().ToUpper());
                }


                if (dtNomineeInfo.Rows[0]["PERCENTAGE"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("PERCENTAGE", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("PERCENTAGE", Convert.ToDecimal(dtNomineeInfo.Rows[0]["PERCENTAGE"].ToString().ToUpper()));
                }

                if (dtNomineeInfo.Rows[0]["REAMARKS"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("REAMARKS", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("REAMARKS", dtNomineeInfo.Rows[0]["REAMARKS"].ToString().ToUpper());
                }
                if (dtNomineeInfo.Rows[0]["NOMI_NATIONALITY"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_NATIONALITY", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_NATIONALITY", dtNomineeInfo.Rows[0]["NOMI_NATIONALITY"].ToString().ToUpper());
                }
                if (dtNomineeInfo.Rows[0]["NOMI_BIRTH_DT"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_BIRTH_DT", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_BIRTH_DT", Convert.ToDateTime(dtNomineeInfo.Rows[0]["NOMI_BIRTH_DT"]).ToString("dd-MMM-yyyy"));

                }

                if (dtNomineeInfo.Rows[0]["NOMI_IS_MINOR"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_IS_MINOR", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_IS_MINOR", dtNomineeInfo.Rows[0]["NOMI_IS_MINOR"].ToString().ToUpper());
                }

                if (dtNomineeInfo.Rows[0]["NOMI_GARDIAN_NAME"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_GARDIAN_NAME", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_GARDIAN_NAME", dtNomineeInfo.Rows[0]["NOMI_GARDIAN_NAME"].ToString().ToUpper());

                }
                if (dtNomineeInfo.Rows[0]["NOMI_GARDIAN_ADDRESS"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_GARDIAN_ADDRESS", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_GARDIAN_ADDRESS", dtNomineeInfo.Rows[0]["NOMI_GARDIAN_ADDRESS"].ToString().ToUpper());
                }

                if (dtNomineeInfo.Rows[0]["NOMI_GARDIAN_BIRTH_DT"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_GARDIAN_BIRTH_DT", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_GARDIAN_BIRTH_DT", Convert.ToDateTime(dtNomineeInfo.Rows[0]["NOMI_GARDIAN_BIRTH_DT"]).ToString("dd-MMM-yyyy"));

                }

                if (dtNomineeInfo.Rows[0]["GARD_REL_WITH_NOMI"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("GARD_REL_WITH_NOMI", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("GARD_REL_WITH_NOMI", dtNomineeInfo.Rows[0]["GARD_REL_WITH_NOMI"].ToString().ToUpper());
                }
                commonGatewayObj.Insert(htNomineeHolder, "U_NOMINEE_EDIT_INFO");//save Nominee previous  info                


                htNomineeHolder = new Hashtable();

                if (nomiObj.Nomi1Name.ToString() != "")
                {                   

                    htNomineeHolder.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htNomineeHolder.Add("REG_BR", regObj.BranchCode.ToString());
                    htNomineeHolder.Add("REG_NO", regObj.RegNumber.ToString());
                    htNomineeHolder.Add("NOMI_NAME", nomiObj.Nomi1Name.ToString().ToUpper());
                    htNomineeHolder.Add("NOMI_NO", Convert.ToInt32(nomiObj.NomiNumber.ToString()));

                    htNomineeHolder.Add("NOMI_TYPE", nomiObj.NomiType.ToString());

                    if (nomiObj.NomiControlNo.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_CTL_NO", nomiObj.NomiControlNo.ToString().ToUpper());
                    }
                    else
                    {
                        htNomineeHolder.Add("NOMI_CTL_NO", DBNull.Value);
                    }

                    if (nomiObj.Nomi1FMName.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_FMH_NAME", nomiObj.Nomi1FMName.ToString().ToUpper());
                    }
                    else
                    {
                        htNomineeHolder.Add("NOMI_FMH_NAME", DBNull.Value);
                    }
                    if (nomiObj.Nomi1MotherName.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_MO_NAME", nomiObj.Nomi1MotherName.ToString().ToUpper());
                    }
                    else
                    {
                        htNomineeHolder.Add("NOMI_MO_NAME", DBNull.Value);
                    }

                    if (nomiObj.Nomi1Occupation.ToString() != "0")
                    {
                        htNomineeHolder.Add("NOMI_OCC_CODE", nomiObj.Nomi1Occupation);
                    }
                    else
                    {
                        htNomineeHolder.Add("NOMI_OCC_CODE", 0);
                    }
                    if (nomiObj.Nomi1Address1.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_ADDRS1", nomiObj.Nomi1Address1.ToString().ToUpper());
                    }
                    else
                    {
                        htNomineeHolder.Add("NOMI_ADDRS1", DBNull.Value);
                    }
                    if (nomiObj.Nomi1Address2.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_ADDRS2", nomiObj.Nomi1Address2.ToString().ToUpper());
                    }
                    else
                    {
                        htNomineeHolder.Add("NOMI_ADDRS2", DBNull.Value);
                    }
                    if (nomiObj.NomiCity.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_CITY", nomiObj.NomiCity.ToString().ToUpper());
                    }
                    else
                    {
                        htNomineeHolder.Add("NOMI_CITY", DBNull.Value);
                    }
                    if (nomiObj.Nomi1Relation.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_REL", nomiObj.Nomi1Relation.ToString().ToUpper());
                    }
                    else
                    {
                        htNomineeHolder.Add("NOMI_REL", DBNull.Value);
                    }

                    if (nomiObj.Nomi1Percentage.ToString() != "")
                    {
                        htNomineeHolder.Add("PERCENTAGE", Convert.ToDecimal(nomiObj.Nomi1Percentage.ToString()));
                    }
                    else
                    {
                        htNomineeHolder.Add("PERCENTAGE", DBNull.Value);
                    }

                    if (nomiObj.NomiRemarks.ToString() != "")
                    {
                        htNomineeHolder.Add("REAMARKS", nomiObj.NomiRemarks.ToString().ToUpper());
                    }
                    else
                    {
                        htNomineeHolder.Add("REAMARKS", DBNull.Value);
                    }
                    if (nomiObj.Nomi1Nationality.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_NATIONALITY", nomiObj.Nomi1Nationality.ToString().ToUpper());
                    }
                    else
                    {
                        htNomineeHolder.Add("NOMI_NATIONALITY", DBNull.Value);
                    }
                    if (nomiObj.NomiDateBirth.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_BIRTH_DT", Convert.ToDateTime(nomiObj.NomiDateBirth.ToString()));
                    }
                    else
                    {
                        htNomineeHolder.Add("NOMI_BIRTH_DT", DBNull.Value);
                    }
                    if (nomiObj.NomiISMinor.ToString() != "")
                    {
                        htNomineeHolder.Add("NOMI_IS_MINOR", nomiObj.NomiISMinor.ToString().ToUpper());
                    }
                    else
                    {
                        htNomineeHolder.Add("NOMI_IS_MINOR", DBNull.Value);
                    }
                    if (nomiObj.NomiISMinor.ToString().ToUpper() == "Y")
                    {
                       
                        if (nomiObj.GardianName.ToString() != "")
                        {
                            htNomineeHolder.Add("NOMI_GARDIAN_NAME", nomiObj.GardianName.ToString().ToUpper());
                        }
                        else
                        {
                            htNomineeHolder.Add("NOMI_GARDIAN_NAME", DBNull.Value);
                        }
                        if (nomiObj.GardianAddress.ToString() != "")
                        {
                            htNomineeHolder.Add("NOMI_GARDIAN_ADDRESS", nomiObj.GardianAddress.ToString().ToUpper());
                        }
                        else
                        {
                            htNomineeHolder.Add("NOMI_GARDIAN_ADDRESS", DBNull.Value);
                        }
                        if (nomiObj.GardianDateOfBirth.ToString() != "")
                        {
                            htNomineeHolder.Add("NOMI_GARDIAN_BIRTH_DT", Convert.ToDateTime(nomiObj.GardianDateOfBirth.ToString()));
                        }
                        else
                        {
                            htNomineeHolder.Add("NOMI_GARDIAN_BIRTH_DT", DBNull.Value);
                        }
                        if (nomiObj.GardianRelWithNominee.ToString() != "")
                        {
                            htNomineeHolder.Add("GARD_REL_WITH_NOMI", nomiObj.GardianRelWithNominee.ToString().ToUpper());
                        }
                        else
                        {
                            htNomineeHolder.Add("GARD_REL_WITH_NOMI", DBNull.Value);
                        }
                    }
                    else
                    {                        
                        htNomineeHolder.Add("NOMI_GARDIAN_NAME", DBNull.Value);
                        htNomineeHolder.Add("NOMI_GARDIAN_ADDRESS", DBNull.Value);
                        htNomineeHolder.Add("NOMI_GARDIAN_BIRTH_DT", DBNull.Value);
                        htNomineeHolder.Add("GARD_REL_WITH_NOMI", DBNull.Value);                  
                    }
                    commonGatewayObj.Update(htNomineeHolder, "U_NOMINEE", " REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND REG_NO=" + regObj.RegNumber + "  AND NOMI_NO=" + Convert.ToInt32(nomiObj.NomiNumber.ToString()));                 
                    //commonGatewayObj.Insert(htNomineeHolder, "U_NOMINEE");//save Nominee info                 


                }

                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }

        }
        public void DeleteNomineeInfo(UnitHolderRegistration regObj, UnitHolderNominee nomiObj, UnitUser userObj) //save Registration Holders Nominee Info
        {
            Hashtable htNomineeHolder = new Hashtable();

            try
            {
                commonGatewayObj.BeginTransaction();

                DataTable dtNomineeInfo = dtGetNomineeInfo(regObj, Convert.ToInt32(nomiObj.NomiNumber.ToString()));

                htNomineeHolder.Add("USER_NM", userObj.UserID.ToString().ToUpper());
                htNomineeHolder.Add("ENT_TM", DateTime.Now.ToString("dd-MMM-yyyy"));
                htNomineeHolder.Add("EDIT_TYPE", "D");
                htNomineeHolder.Add("REG_BK", dtNomineeInfo.Rows[0]["REG_BK"].ToString());
                htNomineeHolder.Add("REG_BR", dtNomineeInfo.Rows[0]["REG_BR"].ToString());
                htNomineeHolder.Add("REG_NO", dtNomineeInfo.Rows[0]["REG_NO"].ToString());

                htNomineeHolder.Add("NOMI_NO", Convert.ToInt32(nomiObj.NomiNumber.ToString()));


                htNomineeHolder.Add("NOMI_NAME", dtNomineeInfo.Rows[0]["NOMI_NAME"].ToString().ToUpper());
                htNomineeHolder.Add("NOMI_FMH_NAME", dtNomineeInfo.Rows[0]["NOMI_FMH_NAME"].ToString().ToUpper());
                htNomineeHolder.Add("NOMI_MO_NAME", dtNomineeInfo.Rows[0]["NOMI_MO_NAME"].ToString().ToUpper());
                if (dtNomineeInfo.Rows[0]["NOMI_TYPE"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_TYPE", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_TYPE", dtNomineeInfo.Rows[0]["NOMI_TYPE"].ToString().ToUpper());
                }
               
                if (dtNomineeInfo.Rows[0]["NOMI_OCC_CODE"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_OCC_CODE", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_OCC_CODE", Convert.ToInt16(dtNomineeInfo.Rows[0]["NOMI_OCC_CODE"].ToString()));
                }

                if (dtNomineeInfo.Rows[0]["NOMI_ADDRS1"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_ADDRS1", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_ADDRS1", dtNomineeInfo.Rows[0]["NOMI_ADDRS1"].ToString().ToUpper());
                }

                if (dtNomineeInfo.Rows[0]["NOMI_ADDRS2"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_ADDRS2", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_ADDRS2", dtNomineeInfo.Rows[0]["NOMI_ADDRS2"].ToString().ToUpper());
                }
                if (dtNomineeInfo.Rows[0]["NOMI_CITY"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_CITY", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_CITY", dtNomineeInfo.Rows[0]["NOMI_CITY"].ToString().ToUpper());
                }


                if (dtNomineeInfo.Rows[0]["NOMI_REL"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_REL", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_REL", dtNomineeInfo.Rows[0]["NOMI_REL"].ToString().ToUpper());
                }


                if (dtNomineeInfo.Rows[0]["PERCENTAGE"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("PERCENTAGE", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("PERCENTAGE", Convert.ToDecimal(dtNomineeInfo.Rows[0]["PERCENTAGE"].ToString().ToUpper()));
                }

                if (dtNomineeInfo.Rows[0]["REAMARKS"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("REAMARKS", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("REAMARKS", dtNomineeInfo.Rows[0]["REAMARKS"].ToString().ToUpper());
                }
                if (dtNomineeInfo.Rows[0]["NOMI_NATIONALITY"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_NATIONALITY", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_NATIONALITY", dtNomineeInfo.Rows[0]["NOMI_NATIONALITY"].ToString().ToUpper());
                }
                if (dtNomineeInfo.Rows[0]["NOMI_BIRTH_DT"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_BIRTH_DT", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_BIRTH_DT", Convert.ToDateTime(dtNomineeInfo.Rows[0]["NOMI_BIRTH_DT"]).ToString("dd-MMM-yyyy"));

                }

                if (dtNomineeInfo.Rows[0]["NOMI_IS_MINOR"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_IS_MINOR", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_IS_MINOR", dtNomineeInfo.Rows[0]["NOMI_IS_MINOR"].ToString().ToUpper());
                }

                if (dtNomineeInfo.Rows[0]["NOMI_GARDIAN_NAME"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_GARDIAN_NAME", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_GARDIAN_NAME", dtNomineeInfo.Rows[0]["NOMI_GARDIAN_NAME"].ToString().ToUpper());

                }
                if (dtNomineeInfo.Rows[0]["NOMI_GARDIAN_ADDRESS"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_GARDIAN_ADDRESS", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_GARDIAN_ADDRESS", dtNomineeInfo.Rows[0]["NOMI_GARDIAN_ADDRESS"].ToString().ToUpper());
                }

                if (dtNomineeInfo.Rows[0]["NOMI_GARDIAN_BIRTH_DT"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("NOMI_GARDIAN_BIRTH_DT", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("NOMI_GARDIAN_BIRTH_DT", Convert.ToDateTime(dtNomineeInfo.Rows[0]["NOMI_GARDIAN_BIRTH_DT"]).ToString("dd-MMM-yyyy"));

                }

                if (dtNomineeInfo.Rows[0]["GARD_REL_WITH_NOMI"].Equals(DBNull.Value))
                {
                    htNomineeHolder.Add("GARD_REL_WITH_NOMI", DBNull.Value);
                }
                else
                {
                    htNomineeHolder.Add("GARD_REL_WITH_NOMI", dtNomineeInfo.Rows[0]["GARD_REL_WITH_NOMI"].ToString().ToUpper());
                }
                commonGatewayObj.Insert(htNomineeHolder, "U_NOMINEE_EDIT_INFO");//save Nominee previous  info                


                commonGatewayObj.DeleteByCommand("U_NOMINEE", " REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND REG_NO=" + regObj.RegNumber + "  AND NOMI_NO=" + Convert.ToInt32(nomiObj.NomiNumber.ToString()));
                                 
                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }

        }
        public void SaveJointHolderInfo(UnitHolderRegistration regObj, UnitHolderInfo unitHolderObj, UnitUser userObj) //save regJointHolder Info
        {

            Hashtable htJointHolder = new Hashtable();

            try
            {
                commonGatewayObj.BeginTransaction();



                    htJointHolder.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htJointHolder.Add("REG_BR", regObj.BranchCode.ToString());
                    htJointHolder.Add("REG_NO", regObj.RegNumber.ToString());

                    htJointHolder.Add("JNT_NO", 2);
                    htJointHolder.Add("JNT_NAME", unitHolderObj.HolderName.ToString().ToUpper());
                    htJointHolder.Add("JNT_FMH_NAME", unitHolderObj.HolderFMHName.ToString().ToUpper());
                    htJointHolder.Add("JNT_MO_NAME", unitHolderObj.HolderMotherName.ToString().ToUpper());
                    htJointHolder.Add("SP_NAME", unitHolderObj.HolderSpouceName.ToString().ToUpper());

                    htJointHolder.Add("NID", unitHolderObj.HolderNID.ToString().ToUpper());
                    htJointHolder.Add("PASS_NO", unitHolderObj.HolderPassport.ToString().ToUpper());
                    htJointHolder.Add("BIRTH_CERT_NO", unitHolderObj.HolderBirthCertNo.ToString().ToUpper());
                    htJointHolder.Add("TIN", unitHolderObj.HolderTIN.ToString().ToUpper());
                    //if (unitHolderObj.HolderTIN.ToString() != "")
                    //{
                    //    htJointHolder.Add("TIN_FLAG", unitHolderObj.HolderTINFlag.ToString().ToUpper());
                    //}

                    htJointHolder.Add("JNT_NATIONALITY", unitHolderObj.HolderNationality.ToString().ToUpper());
                    htJointHolder.Add("JNT_OCC_CODE", unitHolderObj.HolderOccupation);


                    htJointHolder.Add("JNT_ADDRS1", unitHolderObj.HolderAddress1.ToString().ToUpper());
                    htJointHolder.Add("JNT_ADDRS2", unitHolderObj.HolderAddress2.ToString().ToUpper());
                    if (unitHolderObj.HolderCity.ToString() != "")
                    {
                        htJointHolder.Add("JNT_CITY", unitHolderObj.HolderCity.ToString().ToUpper());
                    }

                    htJointHolder.Add("PADDRESS1", unitHolderObj.HolderAddress1.ToString().ToUpper());
                    htJointHolder.Add("PADDRESS2", unitHolderObj.HolderAddress2.ToString().ToUpper());
                    if (unitHolderObj.HolderCity.ToString() != "")
                    {
                        htJointHolder.Add("PCITY", unitHolderObj.HolderCity.ToString().ToUpper());
                    }

                    htJointHolder.Add("SOURCE_FUND", unitHolderObj.HolderSourceFund.ToString().ToUpper());

                    if (unitHolderObj.HolderDateofBirth.ToString() != "")
                    {
                        htJointHolder.Add("JNT_B_DATE", Convert.ToDateTime(unitHolderObj.HolderDateofBirth).ToString("dd-MMM-yyyy"));
                    }
                    if (unitHolderObj.HolderSex.ToString() != "0")
                    {
                        htJointHolder.Add("SEX", unitHolderObj.HolderSex.ToString().ToUpper());
                    }
                    if (unitHolderObj.HolderMaritialStatus.ToString() != "0")
                    {
                        htJointHolder.Add("MAR_STAT", unitHolderObj.HolderMaritialStatus.ToString().ToUpper());
                    }
                    if (unitHolderObj.HolderReligion.ToString() != "0")
                    {
                        htJointHolder.Add("RELIGION", unitHolderObj.HolderReligion.ToString().ToUpper());
                    }
                    if (unitHolderObj.HolderEduQua.ToString() != "0")
                    {
                        htJointHolder.Add("EDU_QUA", unitHolderObj.HolderEduQua.ToString().ToUpper());
                    }
                    if (unitHolderObj.HolderTelephone.ToString() != "")
                    {
                        htJointHolder.Add("JNT_TEL_NO", unitHolderObj.HolderTelephone.ToString());
                    }
                    if (unitHolderObj.HolderEmail.ToString() != "")
                    {
                        htJointHolder.Add("JNT_EMAIL", unitHolderObj.HolderEmail.ToString());
                    }
                    if (unitHolderObj.HolderRemarks.ToString() != "")
                    {
                        htJointHolder.Add("REMARKS", unitHolderObj.HolderRemarks.ToString().ToUpper());
                    }
                    if (unitHolderObj.HolderKYC.ToString() != "")
                    {
                        htJointHolder.Add("KYC", unitHolderObj.HolderKYC.ToString().ToUpper());
                    }
                    else
                    {
                        htJointHolder.Add("KYC", DBNull.Value);
                    }

                    htJointHolder.Add("USER_NM", userObj.UserID.ToString().ToUpper());
                    htJointHolder.Add("ENT_TM", DateTime.Now.ToString("dd-MMM-yyyy"));
                    commonGatewayObj.Insert(htJointHolder, "U_JHOLDER");//save joint holder info                 


                

                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }

        }
        public void SaveJointHolderInfo(UnitHolderRegistration regObj, UnitJointHolderInfo jHolderObj) //save regJointHolder Info
        {

            Hashtable htJointHolder = new Hashtable();

            try
            {
                commonGatewayObj.BeginTransaction();


                if (jHolderObj.JHolderName.ToString() != "")
                {

                    htJointHolder.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htJointHolder.Add("REG_BR", regObj.BranchCode.ToString());
                    htJointHolder.Add("REG_NO", regObj.RegNumber.ToString());
                    htJointHolder.Add("JNT_NAME", jHolderObj.JHolderName.ToString().ToUpper());
                    htJointHolder.Add("JNT_NO", 2);
                    if (jHolderObj.JHolderFMHName.ToString() != "")
                    {
                        htJointHolder.Add("JNT_FMH_NAME", jHolderObj.JHolderFMHName.ToString().ToUpper());
                    }
                    if (jHolderObj.JHolderMotherName.ToString() != "")
                    {
                        htJointHolder.Add("JNT_MO_NAME", jHolderObj.JHolderMotherName.ToString().ToUpper());
                    }
                    if (jHolderObj.JHolderOccupation.ToString() != "0")
                    {
                        htJointHolder.Add("JNT_OCC_CODE", jHolderObj.JHolderOccupation);
                    }
                    if (jHolderObj.JHolderNationality.ToString() != "")
                    {
                        htJointHolder.Add("JNT_NATIONALITY", jHolderObj.JHolderNationality.ToString().ToUpper());
                    }
                    if (jHolderObj.JHolderAddress1.ToString() != "")
                    {
                        htJointHolder.Add("JNT_ADDRS1", jHolderObj.JHolderAddress1.ToString().ToUpper());
                    }
                    if (jHolderObj.JHolderAddress2.ToString() != "")
                    {
                        htJointHolder.Add("JNT_ADDRS2", jHolderObj.JHolderAddress2.ToString().ToUpper());
                    }

                    if (jHolderObj.JHolderCity.ToString() != "")
                    {
                        htJointHolder.Add("JNT_CITY", jHolderObj.JHolderNationality.ToString().ToUpper());
                    }

                    if (jHolderObj.JHolderTelephone.ToString() != "")
                    {
                        htJointHolder.Add("JNT_TEL_NO", jHolderObj.JHolderTelephone.ToString().ToUpper());
                    }

                    if (jHolderObj.JHolderDateofBirth.ToString() != "")
                    {
                        htJointHolder.Add("JNT_B_DATE", jHolderObj.JHolderDateofBirth.ToString().ToUpper());
                    }
                    if (jHolderObj.JHolderEmail.ToString() != "")
                    {
                        htJointHolder.Add("JNT_EMAIL", jHolderObj.JHolderEmail.ToString().ToUpper());
                    }
                    if (jHolderObj.JHolderNPBNo.ToString() != "")
                    {
                        htJointHolder.Add("JNT_NBP_NO", jHolderObj.JHolderNPBNo.ToString().ToUpper());
                    }
                    if (jHolderObj.JHolderNPBType.ToString() != "")
                    {
                        htJointHolder.Add("JNT_NBP_TYPE", jHolderObj.JHolderNPBType.ToString().ToUpper());
                    }
                    commonGatewayObj.Insert(htJointHolder, "U_JHOLDER");//save joint holder info                 


                }

                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }

        }
        public void EditJointHolderInfo(UnitHolderRegistration regObj, UnitHolderInfo unitHolderObj, DataTable dtJointHolderInfo, UnitUser userObj) //save regJointHolder Info
        {

            Hashtable htJointHolder = new Hashtable();

            try
            {
                commonGatewayObj.BeginTransaction();

                htJointHolder.Add("USER_NM", userObj.UserID.ToString().ToUpper());
                htJointHolder.Add("ENT_TM", DateTime.Now.ToString("dd-MMM-yyyy"));


                htJointHolder.Add("EDIT_TYPE", "E");

                htJointHolder.Add("REG_BK", dtJointHolderInfo.Rows[0]["REG_BK"].ToString());
                htJointHolder.Add("REG_BR", dtJointHolderInfo.Rows[0]["REG_BR"].ToString());
                htJointHolder.Add("REG_NO", dtJointHolderInfo.Rows[0]["REG_NO"].ToString());

                htJointHolder.Add("JNT_NO", 2);


                htJointHolder.Add("JNT_NAME", dtJointHolderInfo.Rows[0]["JNT_NAME"].ToString().ToUpper());
                htJointHolder.Add("JNT_FMH_NAME", dtJointHolderInfo.Rows[0]["JNT_FMH_NAME"].ToString().ToUpper());
                htJointHolder.Add("JNT_MO_NAME", dtJointHolderInfo.Rows[0]["JNT_MO_NAME"].ToString().ToUpper());

                if (dtJointHolderInfo.Rows[0]["SP_NAME"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("SP_NAME", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("SP_NAME", dtJointHolderInfo.Rows[0]["SP_NAME"].ToString().ToUpper());
                }
            
               

                if (dtJointHolderInfo.Rows[0]["NID"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("NID", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("NID", dtJointHolderInfo.Rows[0]["NID"].ToString().ToUpper());
                }
                
                if (dtJointHolderInfo.Rows[0]["PASS_NO"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("PASS_NO", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("PASS_NO", dtJointHolderInfo.Rows[0]["PASS_NO"].ToString().ToUpper());
                }

               
                if (dtJointHolderInfo.Rows[0]["BIRTH_CERT_NO"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("BIRTH_CERT_NO", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("BIRTH_CERT_NO", dtJointHolderInfo.Rows[0]["BIRTH_CERT_NO"].ToString().ToUpper());

                }
                
                if (dtJointHolderInfo.Rows[0]["TIN"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("TIN", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("TIN", dtJointHolderInfo.Rows[0]["TIN"].ToString().ToUpper());

                }
              

               

                if (dtJointHolderInfo.Rows[0]["JNT_NATIONALITY"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_NATIONALITY", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_NATIONALITY", dtJointHolderInfo.Rows[0]["JNT_NATIONALITY"].ToString().ToUpper());

                }
              

                if (dtJointHolderInfo.Rows[0]["JNT_OCC_CODE"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_OCC_CODE", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_OCC_CODE", Convert.ToInt16(dtJointHolderInfo.Rows[0]["JNT_OCC_CODE"].ToString()));

                }

               
                if (dtJointHolderInfo.Rows[0]["JNT_ADDRS1"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_ADDRS1", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_ADDRS1", dtJointHolderInfo.Rows[0]["JNT_ADDRS1"].ToString().ToUpper());

                }
                
                if (dtJointHolderInfo.Rows[0]["JNT_ADDRS2"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_ADDRS2", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_ADDRS2", dtJointHolderInfo.Rows[0]["JNT_ADDRS2"].ToString().ToUpper());

                }
             
                if (dtJointHolderInfo.Rows[0]["JNT_CITY"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_CITY", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_CITY", dtJointHolderInfo.Rows[0]["JNT_CITY"].ToString().ToUpper());

                }


                if (dtJointHolderInfo.Rows[0]["PADDRESS1"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("PADDRESS1", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("PADDRESS1", dtJointHolderInfo.Rows[0]["PADDRESS1"].ToString().ToUpper());

                }
                
                if (dtJointHolderInfo.Rows[0]["PADDRESS2"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("PADDRESS2", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("PADDRESS2", dtJointHolderInfo.Rows[0]["PADDRESS2"].ToString().ToUpper());

                }
               
                if (dtJointHolderInfo.Rows[0]["PCITY"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("PCITY", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("PCITY", dtJointHolderInfo.Rows[0]["PCITY"].ToString().ToUpper());

                }

              
                if (dtJointHolderInfo.Rows[0]["SOURCE_FUND"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("SOURCE_FUND", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("SOURCE_FUND", dtJointHolderInfo.Rows[0]["SOURCE_FUND"].ToString().ToUpper());

                }

              
                if (dtJointHolderInfo.Rows[0]["JNT_B_DATE"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_B_DATE", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_B_DATE", Convert.ToDateTime(dtJointHolderInfo.Rows[0]["JNT_B_DATE"]).ToString("dd-MMM-yyyy"));

                }

              
                if (dtJointHolderInfo.Rows[0]["SEX"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("SEX", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("SEX", dtJointHolderInfo.Rows[0]["SEX"].ToString().ToUpper());

                }
              
                if (dtJointHolderInfo.Rows[0]["MAR_STAT"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("MAR_STAT", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("MAR_STAT", dtJointHolderInfo.Rows[0]["MAR_STAT"].ToString().ToUpper());


                }
               
                if (dtJointHolderInfo.Rows[0]["RELIGION"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("RELIGION", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("RELIGION", dtJointHolderInfo.Rows[0]["RELIGION"].ToString().ToUpper());

                }

              
                if (dtJointHolderInfo.Rows[0]["EDU_QUA"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("EDU_QUA", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("EDU_QUA", dtJointHolderInfo.Rows[0]["EDU_QUA"].ToString().ToUpper());

                }

              
                if (dtJointHolderInfo.Rows[0]["JNT_TEL_NO"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_TEL_NO", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_TEL_NO", dtJointHolderInfo.Rows[0]["JNT_TEL_NO"].ToString().ToUpper());


                }
             
                if (dtJointHolderInfo.Rows[0]["JNT_EMAIL"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_EMAIL", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_EMAIL", dtJointHolderInfo.Rows[0]["JNT_EMAIL"].ToString().ToUpper());

                }
               
                if (dtJointHolderInfo.Rows[0]["REMARKS"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("REMARKS", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("REMARKS", dtJointHolderInfo.Rows[0]["REMARKS"].ToString().ToUpper());

                }
                commonGatewayObj.Insert(htJointHolder, "U_JHOLDER_EDIT_INFO");//save joint holder info                 


                //htJointHolder.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                //htJointHolder.Add("REG_BR", regObj.BranchCode.ToString());
                //htJointHolder.Add("REG_NO", regObj.RegNumber.ToString());

                //htJointHolder.Add("JNT_NO", 2);
                htJointHolder = new Hashtable();
                htJointHolder.Add("JNT_NAME", unitHolderObj.HolderName.ToString().ToUpper());
                htJointHolder.Add("JNT_FMH_NAME", unitHolderObj.HolderFMHName.ToString().ToUpper());
                htJointHolder.Add("JNT_MO_NAME", unitHolderObj.HolderMotherName.ToString().ToUpper());
                if (unitHolderObj.HolderSpouceName.ToString() != "")
                {
                    htJointHolder.Add("SP_NAME", unitHolderObj.HolderSpouceName.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("SP_NAME", DBNull.Value);
                }

                
                if (unitHolderObj.HolderNID.ToString() != "")
                {
                    htJointHolder.Add("NID", unitHolderObj.HolderNID.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("NID", DBNull.Value);
                }
               
                if (unitHolderObj.HolderPassport.ToString() != "")
                {
                    htJointHolder.Add("PASS_NO", unitHolderObj.HolderPassport.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("PASS_NO", DBNull.Value);
                }
               // htJointHolder.Add("BIRTH_CERT_NO", unitHolderObj.HolderBirthCertNo.ToString().ToUpper());
                if (unitHolderObj.HolderBirthCertNo.ToString() != "")
                {
                    htJointHolder.Add("BIRTH_CERT_NO", unitHolderObj.HolderBirthCertNo.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("BIRTH_CERT_NO", DBNull.Value);
                }
               

                if (unitHolderObj.HolderTIN.ToString() != "")
                {
                    htJointHolder.Add("TIN", unitHolderObj.HolderTIN.ToString().ToUpper());                    
                }
                else
                {
                    htJointHolder.Add("TIN", DBNull.Value);                   
                }
               

              
                if (unitHolderObj.HolderNationality.ToString() != "")
                {
                    htJointHolder.Add("JNT_NATIONALITY", unitHolderObj.HolderNationality.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("JNT_NATIONALITY", DBNull.Value);
                }

                if (unitHolderObj.HolderOccupation.ToString() != "")
                {
                    htJointHolder.Add("JNT_OCC_CODE", unitHolderObj.HolderOccupation);
                }
                else
                {
                    htJointHolder.Add("JNT_OCC_CODE", DBNull.Value);
                }
               


                
                if (unitHolderObj.HolderAddress1.ToString() != "")
                {
                    htJointHolder.Add("JNT_ADDRS1", unitHolderObj.HolderAddress1.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("JNT_ADDRS1", DBNull.Value);
                }

                //htJointHolder.Add("JNT_ADDRS2", unitHolderObj.HolderAddress2.ToString().ToUpper());
                if (unitHolderObj.HolderAddress2.ToString() != "")
                {
                    htJointHolder.Add("JNT_ADDRS2", unitHolderObj.HolderAddress2.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("JNT_ADDRS2", DBNull.Value);
                }
               // htJointHolder.Add("JNT_CITY", unitHolderObj.HolderCity.ToString().ToUpper());

                if (unitHolderObj.HolderCity.ToString() != "")
                {
                    htJointHolder.Add("JNT_CITY", unitHolderObj.HolderCity.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("JNT_CITY", DBNull.Value);
                }

               // htJointHolder.Add("PADDRESS1", unitHolderObj.HolderAddress1.ToString().ToUpper());

                if (unitHolderObj.HolderPermanAddress1.ToString() != "")
                {
                    htJointHolder.Add("PADDRESS1", unitHolderObj.HolderPermanAddress1.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("PADDRESS1", DBNull.Value);
                }

              //  htJointHolder.Add("PADDRESS2", unitHolderObj.HolderAddress2.ToString().ToUpper());
                if (unitHolderObj.HolderPermanAddress2.ToString() != "")
                {
                    htJointHolder.Add("PADDRESS2", unitHolderObj.HolderPermanAddress2.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("PADDRESS2", DBNull.Value);
                }

                if (unitHolderObj.HolderPermanCity.ToString() != "")
                {
                    htJointHolder.Add("PCITY", unitHolderObj.HolderPermanCity.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("PCITY", DBNull.Value);
                }

                //htJointHolder.Add("SOURCE_FUND", unitHolderObj.HolderSourceFund.ToString().ToUpper());
                if (unitHolderObj.HolderSourceFund.ToString() != "")
                {
                    htJointHolder.Add("SOURCE_FUND", unitHolderObj.HolderSourceFund.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("SOURCE_FUND", DBNull.Value);
                }

                if (unitHolderObj.HolderDateofBirth.ToString() != "")
                {
                    htJointHolder.Add("JNT_B_DATE", Convert.ToDateTime(unitHolderObj.HolderDateofBirth).ToString("dd-MMM-yyyy"));
                }
                else
                {
                    htJointHolder.Add("JNT_B_DATE", DBNull.Value);
                }
                if (unitHolderObj.HolderSex.ToString() != "0")
                {
                    htJointHolder.Add("SEX", unitHolderObj.HolderSex.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("SEX", DBNull.Value);
                }
                if (unitHolderObj.HolderMaritialStatus.ToString() != "0")
                {
                    htJointHolder.Add("MAR_STAT", unitHolderObj.HolderMaritialStatus.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("MAR_STAT", DBNull.Value);
                }
                if (unitHolderObj.HolderReligion.ToString() != "0")
                {
                    htJointHolder.Add("RELIGION", unitHolderObj.HolderReligion.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("RELIGION", DBNull.Value);
                }
                if (unitHolderObj.HolderEduQua.ToString() != "0")
                {
                    htJointHolder.Add("EDU_QUA", unitHolderObj.HolderEduQua.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("EDU_QUA", DBNull.Value);
                }
                if (unitHolderObj.HolderTelephone.ToString() != "")
                {
                    htJointHolder.Add("JNT_TEL_NO", unitHolderObj.HolderTelephone.ToString());
                }
                else
                {
                    htJointHolder.Add("JNT_TEL_NO", DBNull.Value);
                }
                if (unitHolderObj.HolderEmail.ToString() != "")
                {
                    htJointHolder.Add("JNT_EMAIL", unitHolderObj.HolderEmail.ToString());
                }
                else
                {
                    htJointHolder.Add("JNT_EMAIL", DBNull.Value);
                }
                if (unitHolderObj.HolderRemarks.ToString() != "")
                {
                    htJointHolder.Add("REMARKS", unitHolderObj.HolderRemarks.ToString().ToUpper());
                }
                else
                {
                    htJointHolder.Add("REMARKS", DBNull.Value);
                }

                if (unitHolderObj.HolderKYC.ToString() != "")
                {
                    htJointHolder.Add("KYC", unitHolderObj.HolderKYC.ToString().ToUpper());
                    htJointHolder.Add("KYC_UPDATE_BY", userObj.UserID.ToString().ToUpper());
                    htJointHolder.Add("KYC_UPDATE_DT", DateTime.Now.ToString("dd-MMM-yyyy"));
                }
                else
                {
                    htJointHolder.Add("KYC", DBNull.Value);
                }
               

                commonGatewayObj.Update(htJointHolder, "U_JHOLDER"," REG_BK='"+regObj.FundCode.ToString().ToUpper()+"' AND REG_BR='"+regObj.BranchCode.ToString().ToUpper()+"' AND REG_NO="+regObj.RegNumber+"  AND JNT_NO=2");//update joint holder info                 



                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }

        }
        public void DeleteJointHolderInfo(UnitHolderRegistration regObj, DataTable dtJointHolderInfo, UnitUser userObj) //Delete  regJointHolder Info
        {

            Hashtable htJointHolder = new Hashtable();

            try
            {
                commonGatewayObj.BeginTransaction();

                htJointHolder.Add("USER_NM", userObj.UserID.ToString().ToUpper());
                htJointHolder.Add("ENT_TM", DateTime.Now.ToString("dd-MMM-yyyy"));


                htJointHolder.Add("EDIT_TYPE", "D");

                htJointHolder.Add("REG_BK", dtJointHolderInfo.Rows[0]["REG_BK"].ToString());
                htJointHolder.Add("REG_BR", dtJointHolderInfo.Rows[0]["REG_BR"].ToString());
                htJointHolder.Add("REG_NO", dtJointHolderInfo.Rows[0]["REG_NO"].ToString());

                htJointHolder.Add("JNT_NO", 2);


                htJointHolder.Add("JNT_NAME", dtJointHolderInfo.Rows[0]["JNT_NAME"].ToString().ToUpper());
                htJointHolder.Add("JNT_FMH_NAME", dtJointHolderInfo.Rows[0]["JNT_FMH_NAME"].ToString().ToUpper());
                htJointHolder.Add("JNT_MO_NAME", dtJointHolderInfo.Rows[0]["JNT_MO_NAME"].ToString().ToUpper());

                if (dtJointHolderInfo.Rows[0]["SP_NAME"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("SP_NAME", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("SP_NAME", dtJointHolderInfo.Rows[0]["SP_NAME"].ToString().ToUpper());
                }



                if (dtJointHolderInfo.Rows[0]["NID"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("NID", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("NID", dtJointHolderInfo.Rows[0]["NID"].ToString().ToUpper());
                }

                if (dtJointHolderInfo.Rows[0]["PASS_NO"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("PASS_NO", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("PASS_NO", dtJointHolderInfo.Rows[0]["PASS_NO"].ToString().ToUpper());
                }


                if (dtJointHolderInfo.Rows[0]["BIRTH_CERT_NO"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("BIRTH_CERT_NO", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("BIRTH_CERT_NO", dtJointHolderInfo.Rows[0]["BIRTH_CERT_NO"].ToString().ToUpper());

                }

                if (dtJointHolderInfo.Rows[0]["TIN"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("TIN", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("TIN", dtJointHolderInfo.Rows[0]["TIN"].ToString().ToUpper());

                }




                if (dtJointHolderInfo.Rows[0]["JNT_NATIONALITY"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_NATIONALITY", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_NATIONALITY", dtJointHolderInfo.Rows[0]["JNT_NATIONALITY"].ToString().ToUpper());

                }


                if (dtJointHolderInfo.Rows[0]["JNT_OCC_CODE"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_OCC_CODE", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_OCC_CODE", Convert.ToInt16(dtJointHolderInfo.Rows[0]["JNT_OCC_CODE"].ToString()));

                }


                if (dtJointHolderInfo.Rows[0]["JNT_ADDRS1"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_ADDRS1", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_ADDRS1", dtJointHolderInfo.Rows[0]["JNT_ADDRS1"].ToString().ToUpper());

                }

                if (dtJointHolderInfo.Rows[0]["JNT_ADDRS2"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_ADDRS2", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_ADDRS2", dtJointHolderInfo.Rows[0]["JNT_ADDRS2"].ToString().ToUpper());

                }

                if (dtJointHolderInfo.Rows[0]["JNT_CITY"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_CITY", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_CITY", dtJointHolderInfo.Rows[0]["JNT_CITY"].ToString().ToUpper());

                }


                if (dtJointHolderInfo.Rows[0]["PADDRESS1"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("PADDRESS1", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("PADDRESS1", dtJointHolderInfo.Rows[0]["PADDRESS1"].ToString().ToUpper());

                }

                if (dtJointHolderInfo.Rows[0]["PADDRESS2"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("PADDRESS2", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("PADDRESS2", dtJointHolderInfo.Rows[0]["PADDRESS2"].ToString().ToUpper());

                }

                if (dtJointHolderInfo.Rows[0]["PCITY"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("PCITY", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("PCITY", dtJointHolderInfo.Rows[0]["PCITY"].ToString().ToUpper());

                }


                if (dtJointHolderInfo.Rows[0]["SOURCE_FUND"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("SOURCE_FUND", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("SOURCE_FUND", dtJointHolderInfo.Rows[0]["SOURCE_FUND"].ToString().ToUpper());

                }


                if (dtJointHolderInfo.Rows[0]["JNT_B_DATE"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_B_DATE", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_B_DATE", Convert.ToDateTime(dtJointHolderInfo.Rows[0]["JNT_B_DATE"]).ToString("dd-MMM-yyyy"));

                }


                if (dtJointHolderInfo.Rows[0]["SEX"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("SEX", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("SEX", dtJointHolderInfo.Rows[0]["SEX"].ToString().ToUpper());

                }

                if (dtJointHolderInfo.Rows[0]["MAR_STAT"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("MAR_STAT", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("MAR_STAT", dtJointHolderInfo.Rows[0]["MAR_STAT"].ToString().ToUpper());


                }

                if (dtJointHolderInfo.Rows[0]["RELIGION"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("RELIGION", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("RELIGION", dtJointHolderInfo.Rows[0]["RELIGION"].ToString().ToUpper());

                }


                if (dtJointHolderInfo.Rows[0]["EDU_QUA"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("EDU_QUA", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("EDU_QUA", dtJointHolderInfo.Rows[0]["EDU_QUA"].ToString().ToUpper());

                }


                if (dtJointHolderInfo.Rows[0]["JNT_TEL_NO"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_TEL_NO", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_TEL_NO", dtJointHolderInfo.Rows[0]["JNT_TEL_NO"].ToString().ToUpper());


                }

                if (dtJointHolderInfo.Rows[0]["JNT_EMAIL"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("JNT_EMAIL", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("JNT_EMAIL", dtJointHolderInfo.Rows[0]["JNT_EMAIL"].ToString().ToUpper());

                }

                if (dtJointHolderInfo.Rows[0]["REMARKS"].Equals(DBNull.Value))
                {
                    htJointHolder.Add("REMARKS", DBNull.Value);
                }
                else
                {
                    htJointHolder.Add("REMARKS", dtJointHolderInfo.Rows[0]["REMARKS"].ToString().ToUpper());

                }
                commonGatewayObj.Insert(htJointHolder, "U_JHOLDER_EDIT_INFO");//save joint holder info  


                commonGatewayObj.DeleteByCommand( "U_JHOLDER", " REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND REG_NO=" + regObj.RegNumber + "  AND JNT_NO=2");//delete joint holder info                 



                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }

        }                
        public void SaveRegEditInfo(DataTable dtHolderReg_Info, DataTable dtNomineeRegInfo, UnitUser userObj)// Back up Edit Reg Info
        {
            string editTime = DateTime.Now.ToString("HH:mm:ss");

            Hashtable htReg = new Hashtable();
            try
            {
                commonGatewayObj.BeginTransaction();
                htReg.Add("USER_ID", userObj.UserID.ToString().ToUpper());
                htReg.Add("ED_DT", DateTime.Today.ToString("dd-MMM-yyyy"));
                htReg.Add("ED_TM", editTime.ToString());

                htReg.Add("ED_TYPE", "E");

                htReg.Add("REG_BK", dtHolderReg_Info.Rows[0]["REG_BK"].ToString());
                htReg.Add("REG_BR", dtHolderReg_Info.Rows[0]["REG_BR"].ToString());
                htReg.Add("REG_NO", dtHolderReg_Info.Rows[0]["REG_NO"].ToString());
                htReg.Add("REG_TYPE", dtHolderReg_Info.Rows[0]["REG_TYPE"].ToString());
                htReg.Add("REG_DT", Convert.ToDateTime(dtHolderReg_Info.Rows[0]["REG_DT"].ToString()).ToString("dd-MMM-yyyy"));
                htReg.Add("CIP", dtHolderReg_Info.Rows[0]["CIP"].ToString());
                htReg.Add("ID_FLAG", dtHolderReg_Info.Rows[0]["ID_FLAG"].ToString());
                if (dtHolderReg_Info.Rows[0]["ID_AC"].Equals(DBNull.Value))
                {
                    htReg.Add("ID_AC", DBNull.Value);
                }
                else
                {
                    htReg.Add("ID_AC", Convert.ToInt64(dtHolderReg_Info.Rows[0]["ID_AC"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["ID_BK_NM_CD"].Equals(DBNull.Value))
                {
                    htReg.Add("ID_BK_NM_CD", DBNull.Value);
                }
                else
                {
                    htReg.Add("ID_BK_NM_CD", Convert.ToInt32(dtHolderReg_Info.Rows[0]["ID_BK_NM_CD"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["ID_BK_BR_NM_CD"].Equals(DBNull.Value))
                {
                    htReg.Add("ID_BK_BR_NM_CD", DBNull.Value);
                }
                else
                {
                    htReg.Add("ID_BK_BR_NM_CD", Convert.ToInt32(dtHolderReg_Info.Rows[0]["ID_BK_BR_NM_CD"].ToString()));
                }

                if (dtHolderReg_Info.Rows[0]["TIN"].Equals(DBNull.Value))
                {
                    htReg.Add("TIN", DBNull.Value);
                }
                else
                {
                    htReg.Add("TIN", dtHolderReg_Info.Rows[0]["TIN"].ToString());
                }

                if (dtHolderReg_Info.Rows[0]["TIN_FLAG"].Equals(DBNull.Value))
                {
                    htReg.Add("TIN_FLAG", DBNull.Value);
                }
                else
                {
                    htReg.Add("TIN_FLAG",dtHolderReg_Info.Rows[0]["TIN_FLAG"].ToString());
                }


                htReg.Add("HNAME", dtHolderReg_Info.Rows[0]["HNAME"].ToString());
                htReg.Add("FMH_NAME", dtHolderReg_Info.Rows[0]["FMH_NAME"].ToString());
                htReg.Add("MO_NAME", dtHolderReg_Info.Rows[0]["MO_NAME"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["MO_NAME"].ToString());
                htReg.Add("NATIONALITY", dtHolderReg_Info.Rows[0]["NATIONALITY"].ToString());
                if(dtHolderReg_Info.Rows[0]["OCC_CODE"].Equals(DBNull.Value))
                {
                    htReg.Add("OCC_CODE", DBNull.Value);
                }
                else
                {
                    htReg.Add("OCC_CODE",Convert.ToInt16( dtHolderReg_Info.Rows[0]["OCC_CODE"]));
                }
                htReg.Add("ADDRS1", dtHolderReg_Info.Rows[0]["ADDRS1"].ToString());
                htReg.Add("ADDRS2", dtHolderReg_Info.Rows[0]["ADDRS2"].ToString());
                htReg.Add("CITY", dtHolderReg_Info.Rows[0]["CITY"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["CITY"].ToString());
                if (dtHolderReg_Info.Rows[0]["B_DATE"].Equals(DBNull.Value))
                {
                    htReg.Add("B_DATE", DBNull.Value);
                }
                else
                {
                    htReg.Add("B_DATE", Convert.ToDateTime(dtHolderReg_Info.Rows[0]["B_DATE"].ToString()).ToString("dd-MMM-yyyy"));
                }
                htReg.Add("ENT_TM", dtHolderReg_Info.Rows[0]["ENT_TM"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["ENT_TM"].ToString());
                htReg.Add("SEX", dtHolderReg_Info.Rows[0]["SEX"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["SEX"].ToString());
                htReg.Add("MAR_STAT", dtHolderReg_Info.Rows[0]["MAR_STAT"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["MAR_STAT"].ToString());
                htReg.Add("RELIGION", dtHolderReg_Info.Rows[0]["RELIGION"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["RELIGION"].ToString());
                htReg.Add("EDU_QUA", dtHolderReg_Info.Rows[0]["EDU_QUA"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["EDU_QUA"].ToString());
                htReg.Add("TEL_NO", dtHolderReg_Info.Rows[0]["TEL_NO"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["TEL_NO"].ToString());
                htReg.Add("EMAIL", dtHolderReg_Info.Rows[0]["EMAIL"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["EMAIL"].ToString());
                htReg.Add("REMARKS", dtHolderReg_Info.Rows[0]["REMARKS"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["REMARKS"].ToString());
                htReg.Add("SPEC_IN1", dtHolderReg_Info.Rows[0]["SPEC_IN1"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["SPEC_IN1"].ToString());
                htReg.Add("SPEC_IN2", dtHolderReg_Info.Rows[0]["SPEC_IN2"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["SPEC_IN2"].ToString());
                htReg.Add("BK_FLAG", dtHolderReg_Info.Rows[0]["BK_FLAG"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["BK_FLAG"].ToString());
                htReg.Add("BK_AC_NO", dtHolderReg_Info.Rows[0]["BK_AC_NO"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["BK_AC_NO"].ToString());


                if (dtHolderReg_Info.Rows[0]["BK_NM_CD"].Equals(DBNull.Value))
                {
                    htReg.Add("BK_NM_CD", DBNull.Value);
                }
                else
                {
                    htReg.Add("BK_NM_CD", Convert.ToInt16(dtHolderReg_Info.Rows[0]["BK_NM_CD"].ToString()));
                }

                if (dtHolderReg_Info.Rows[0]["BK_BR_NM_CD"].Equals(DBNull.Value))
                {
                    htReg.Add("BK_BR_NM_CD", DBNull.Value);
                }
                else
                {
                    htReg.Add("BK_BR_NM_CD", Convert.ToInt16(dtHolderReg_Info.Rows[0]["BK_BR_NM_CD"].ToString()));
                }

                if (dtHolderReg_Info.Rows[0]["OPN_BAL"].Equals(DBNull.Value))
                {
                    htReg.Add("OPN_BAL", DBNull.Value);
                }
                else
                {
                    htReg.Add("OPN_BAL", Convert.ToInt32(dtHolderReg_Info.Rows[0]["OPN_BAL"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["BALANCE"].Equals(DBNull.Value))
                {
                    htReg.Add("BALANCE", DBNull.Value);
                }
                else
                {
                    htReg.Add("BALANCE", Convert.ToInt32(dtHolderReg_Info.Rows[0]["BALANCE"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["DIVIDEND"].Equals(DBNull.Value))
                {
                    htReg.Add("DIVIDEND", DBNull.Value);
                }
                else
                {
                    htReg.Add("DIVIDEND", Convert.ToDecimal(dtHolderReg_Info.Rows[0]["DIVIDEND"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["CIP_DIV"].Equals(DBNull.Value))
                {
                    htReg.Add("CIP_DIV", DBNull.Value);
                }
                else
                {
                    htReg.Add("CIP_DIV", Convert.ToDecimal(dtHolderReg_Info.Rows[0]["CIP_DIV"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["TOT_DIV"].Equals(DBNull.Value))
                {
                    htReg.Add("TOT_DIV", DBNull.Value);
                }
                else
                {
                    htReg.Add("TOT_DIV", Convert.ToDecimal(dtHolderReg_Info.Rows[0]["TOT_DIV"].ToString()));
                }

                if (dtHolderReg_Info.Rows[0]["WAR_NO"].Equals(DBNull.Value))
                {
                    htReg.Add("WAR_NO", DBNull.Value);
                }
                else
                {
                    htReg.Add("WAR_NO", Convert.ToInt16(dtHolderReg_Info.Rows[0]["WAR_NO"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["U_ID"].Equals(DBNull.Value))
                {
                    htReg.Add("U_ID", DBNull.Value);
                }
                else
                {
                    htReg.Add("U_ID", Convert.ToInt16(dtHolderReg_Info.Rows[0]["U_ID"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["I_ED_DT"].Equals(DBNull.Value))
                {
                    htReg.Add("I_ED_DT", DBNull.Value);
                }
                else
                {
                    htReg.Add("I_ED_DT", Convert.ToDateTime(dtHolderReg_Info.Rows[0]["I_ED_DT"].ToString()).ToString("dd-MMM-yyyy"));
                }
                htReg.Add("INTER_BR", dtHolderReg_Info.Rows[0]["INTER_BR"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["INTER_BR"].ToString());
                if (dtHolderReg_Info.Rows[0]["CUR_BAL"].Equals(DBNull.Value))
                {
                    htReg.Add("CUR_BAL", DBNull.Value);
                }
                else
                {
                    htReg.Add("CUR_BAL", Convert.ToInt32(dtHolderReg_Info.Rows[0]["CUR_BAL"].ToString()));
                }
                htReg.Add("LIEN_MARK", dtHolderReg_Info.Rows[0]["LIEN_MARK"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["LIEN_MARK"].ToString());
                htReg.Add("LOST", dtHolderReg_Info.Rows[0]["LOST"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["LOST"].ToString());
                if (dtHolderReg_Info.Rows[0]["SL"].Equals(DBNull.Value))
                {
                    htReg.Add("SL", DBNull.Value);
                }
                else
                {
                    htReg.Add("SL", Convert.ToInt32(dtHolderReg_Info.Rows[0]["SL"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["REP"].Equals(DBNull.Value))
                {
                    htReg.Add("REP", DBNull.Value);
                }
                else
                {
                    htReg.Add("REP", Convert.ToInt32(dtHolderReg_Info.Rows[0]["REP"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["TR_IN"].Equals(DBNull.Value))
                {
                    htReg.Add("TR_IN", DBNull.Value);
                }
                else
                {
                    htReg.Add("TR_IN", Convert.ToInt32(dtHolderReg_Info.Rows[0]["TR_IN"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["TR_OUT"].Equals(DBNull.Value))
                {
                    htReg.Add("TR_OUT", DBNull.Value);
                }
                else
                {
                    htReg.Add("TR_OUT", Convert.ToInt32(dtHolderReg_Info.Rows[0]["TR_OUT"].ToString()));
                }

                htReg.Add("USER_NM", dtHolderReg_Info.Rows[0]["USER_NM"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["USER_NM"].ToString());
                // htReg.Add("LOST", dtHolderReg_Info.Rows[0]["LOST"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["LOST"].ToString());

                if (dtHolderReg_Info.Rows[0]["ENT_DT"].Equals(DBNull.Value))
                {
                    htReg.Add("ENT_DT", DBNull.Value);
                }
                else
                {
                    htReg.Add("ENT_DT", Convert.ToDateTime(dtHolderReg_Info.Rows[0]["ENT_DT"].ToString()).ToString("dd-MMM-yyyy"));
                }
                htReg.Add("JNT_NO", dtHolderReg_Info.Rows[0]["JNT_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt16(dtHolderReg_Info.Rows[0]["JNT_NO"].ToString()));
                htReg.Add("JNT_FMH_NAME", dtHolderReg_Info.Rows[0]["JNT_FMH_NAME"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["JNT_FMH_NAME"].ToString());
                htReg.Add("JNT_MO_NAME", dtHolderReg_Info.Rows[0]["JNT_MO_NAME"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["JNT_MO_NAME"].ToString());
                htReg.Add("JNT_OCC_CODE", dtHolderReg_Info.Rows[0]["OCC_CODE"].Equals(DBNull.Value) ? 0 : Convert.ToInt16(dtHolderReg_Info.Rows[0]["OCC_CODE"].ToString()));
                htReg.Add("JNT_ADDRS1", dtHolderReg_Info.Rows[0]["JNT_ADDRS1"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["JNT_ADDRS1"].ToString());
                htReg.Add("JNT_ADDRS2", dtHolderReg_Info.Rows[0]["JNT_ADDRS2"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["JNT_ADDRS2"].ToString());
                htReg.Add("JNT_NATIONALITY", dtHolderReg_Info.Rows[0]["JNT_NATIONALITY"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["JNT_NATIONALITY"].ToString());
                htReg.Add("JNT_CITY", dtHolderReg_Info.Rows[0]["JNT_CITY"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["JNT_CITY"].ToString());
                htReg.Add("JNT_TEL_NO", dtHolderReg_Info.Rows[0]["JNT_TEL_NO"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["JNT_TEL_NO"].ToString());
                htReg.Add("JNT_FMH_REL", dtHolderReg_Info.Rows[0]["JNT_FMH_REL"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["JNT_FMH_REL"].ToString());
                if (dtNomineeRegInfo.Rows.Count > 0)
                {
                    htReg.Add("NOMI_CTL_NO", dtNomineeRegInfo.Rows[0]["NOMI_CTL_NO"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[0]["NOMI_CTL_NO"].ToString());
                    htReg.Add("NOMI1", dtNomineeRegInfo.Rows[0]["NOMI_NAME"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[0]["NOMI_NAME"].ToString());
                    htReg.Add("NOMI1_REL", dtNomineeRegInfo.Rows[0]["NOMI_REL"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[0]["NOMI_REL"].ToString());
                    htReg.Add("NOMI1_FMH_NAME", dtNomineeRegInfo.Rows[0]["NOMI_FMH_NAME"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[0]["NOMI_FMH_NAME"].ToString());
                    htReg.Add("NOMI1_MO_NAME", dtNomineeRegInfo.Rows[0]["NOMI_MO_NAME"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[0]["NOMI_MO_NAME"].ToString());
                    htReg.Add("NOMI1_OCC_CODE", dtNomineeRegInfo.Rows[0]["NOMI_OCC_CODE"].Equals(DBNull.Value) ? 0 : Convert.ToInt16(dtNomineeRegInfo.Rows[0]["NOMI_OCC_CODE"].ToString()));
                    htReg.Add("NOMI1_ADDRS1", dtNomineeRegInfo.Rows[0]["NOMI_ADDRS1"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[0]["NOMI_ADDRS1"].ToString());
                    htReg.Add("NOMI1_ADDRS2", dtNomineeRegInfo.Rows[0]["NOMI_ADDRS2"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[0]["NOMI_ADDRS2"].ToString());
                    htReg.Add("NOMI1_CITY", dtNomineeRegInfo.Rows[0]["NOMI_CITY"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[0]["NOMI_CITY"].ToString());
                    htReg.Add("NOMI1_NATIONALITY", dtNomineeRegInfo.Rows[0]["NOMI_NATIONALITY"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[0]["NOMI_NATIONALITY"].ToString());
                    htReg.Add("NOMI1_PERCENTAGE", dtNomineeRegInfo.Rows[0]["PERCENTAGE"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(dtNomineeRegInfo.Rows[0]["PERCENTAGE"].ToString()));
                    if (dtNomineeRegInfo.Rows.Count > 1)
                    {
                        htReg.Add("NOMI2", dtNomineeRegInfo.Rows[1]["NOMI_NAME"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[1]["NOMI_NAME"].ToString());
                        htReg.Add("NOMI2_REL", dtNomineeRegInfo.Rows[1]["NOMI_REL"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[1]["NOMI_REL"].ToString());
                        htReg.Add("NOMI2_FMH_NAME", dtNomineeRegInfo.Rows[1]["NOMI_FMH_NAME"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[1]["NOMI_FMH_NAME"].ToString());
                        htReg.Add("NOMI2_MO_NAME", dtNomineeRegInfo.Rows[1]["NOMI_MO_NAME"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[1]["NOMI_MO_NAME"].ToString());
                        htReg.Add("NOMI2_OCC_CODE", dtNomineeRegInfo.Rows[1]["NOMI_OCC_CODE"].Equals(DBNull.Value) ? 0 : Convert.ToInt16(dtNomineeRegInfo.Rows[1]["NOMI_OCC_CODE"].ToString()));
                        htReg.Add("NOMI2_ADDRS1", dtNomineeRegInfo.Rows[1]["NOMI_ADDRS1"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[1]["NOMI_ADDRS1"].ToString());
                        htReg.Add("NOMI2_ADDRS2", dtNomineeRegInfo.Rows[1]["NOMI_ADDRS2"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[1]["NOMI_ADDRS2"].ToString());
                        htReg.Add("NOMI2_CITY", dtNomineeRegInfo.Rows[1]["NOMI_CITY"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[1]["NOMI_CITY"].ToString());
                        htReg.Add("NOMI2_NATIONALITY", dtNomineeRegInfo.Rows[1]["NOMI_NATIONALITY"].Equals(DBNull.Value) ? null : dtNomineeRegInfo.Rows[1]["NOMI_NATIONALITY"].ToString());
                        htReg.Add("NOMI2_PERCENTAGE", dtNomineeRegInfo.Rows[1]["PERCENTAGE"].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(dtNomineeRegInfo.Rows[1]["PERCENTAGE"].ToString()));
                    }

                }
                if (dtHolderReg_Info.Rows[0]["IS_BEFTN"].Equals(DBNull.Value))
                {
                    htReg.Add("IS_BEFTN", DBNull.Value);
                }
                else
                {
                    htReg.Add("IS_BEFTN", dtHolderReg_Info.Rows[0]["IS_BEFTN"].ToString());
                }
               
                commonGatewayObj.Insert(htReg, "ED_INFO_U_MASTER");//save edited registration info
              

                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }

        }
        public void SaveRegEditInfo(DataTable dtHolderReg_Info, UnitUser userObj)// Back up Edit Reg Info
        {
            string editTime = DateTime.Now.ToString("HH:mm:ss");

            Hashtable htReg = new Hashtable();
            try
            {
                commonGatewayObj.BeginTransaction();
                htReg.Add("USER_ID", userObj.UserID.ToString().ToUpper());
                htReg.Add("ED_DT", DateTime.Today.ToString("dd-MMM-yyyy"));
                htReg.Add("ED_TM", editTime.ToString());

                htReg.Add("ED_TYPE", "E");

                htReg.Add("REG_BK", dtHolderReg_Info.Rows[0]["REG_BK"].ToString());
                htReg.Add("REG_BR", dtHolderReg_Info.Rows[0]["REG_BR"].ToString());
                htReg.Add("REG_NO", dtHolderReg_Info.Rows[0]["REG_NO"].ToString());
                htReg.Add("REG_TYPE", dtHolderReg_Info.Rows[0]["REG_TYPE"].ToString());
                htReg.Add("REG_DT", Convert.ToDateTime(dtHolderReg_Info.Rows[0]["REG_DT"].ToString()).ToString("dd-MMM-yyyy"));
                htReg.Add("CIP", dtHolderReg_Info.Rows[0]["CIP"].ToString());
                htReg.Add("ID_FLAG", dtHolderReg_Info.Rows[0]["ID_FLAG"].ToString());

                if (!dtHolderReg_Info.Rows[0]["SOURCE_FUND"].Equals(DBNull.Value))
                {
                    htReg.Add("SOURCE_FUND", dtHolderReg_Info.Rows[0]["SOURCE_FUND"].ToString().ToUpper());
                }
                else
                {
                    htReg.Add("SOURCE_FUND", DBNull.Value);
                }

                if (!dtHolderReg_Info.Rows[0]["SP_NAME"].Equals(DBNull.Value))
                {
                    htReg.Add("SP_NAME", dtHolderReg_Info.Rows[0]["SP_NAME"].ToString().ToUpper());
                }
                else
                {
                    htReg.Add("SP_NAME", DBNull.Value);
                }
                htReg.Add("BO", dtHolderReg_Info.Rows[0]["BO"]);
                if (!dtHolderReg_Info.Rows[0]["NID"].Equals(DBNull.Value))
                {
                    htReg.Add("NID", dtHolderReg_Info.Rows[0]["NID"].ToString().ToUpper());
                }
                else
                {
                    htReg.Add("NID", DBNull.Value);
                }

                if (!dtHolderReg_Info.Rows[0]["PASS_NO"].Equals(DBNull.Value))
                {
                    htReg.Add("PASS_NO", dtHolderReg_Info.Rows[0]["PASS_NO"].ToString().ToUpper());
                }
                else
                {
                    htReg.Add("PASS_NO", DBNull.Value);
                }

                if (!dtHolderReg_Info.Rows[0]["BIRTH_CERT_NO"].Equals(DBNull.Value))
                {
                    htReg.Add("BIRTH_CERT_NO", dtHolderReg_Info.Rows[0]["BIRTH_CERT_NO"].ToString().ToUpper());
                }
                else
                {
                    htReg.Add("BIRTH_CERT_NO", DBNull.Value);
                }
                if (dtHolderReg_Info.Rows[0]["ID_AC"].Equals(DBNull.Value))
                {
                    htReg.Add("ID_AC", DBNull.Value);
                }
                else
                {
                    htReg.Add("ID_AC", Convert.ToInt64(dtHolderReg_Info.Rows[0]["ID_AC"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["ID_BK_NM_CD"].Equals(DBNull.Value))
                {
                    htReg.Add("ID_BK_NM_CD", DBNull.Value);
                }
                else
                {
                    htReg.Add("ID_BK_NM_CD", Convert.ToInt32(dtHolderReg_Info.Rows[0]["ID_BK_NM_CD"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["ID_BK_BR_NM_CD"].Equals(DBNull.Value))
                {
                    htReg.Add("ID_BK_BR_NM_CD", DBNull.Value);
                }
                else
                {
                    htReg.Add("ID_BK_BR_NM_CD", Convert.ToInt32(dtHolderReg_Info.Rows[0]["ID_BK_BR_NM_CD"].ToString()));
                }

                if (dtHolderReg_Info.Rows[0]["TIN"].Equals(DBNull.Value))
                {
                    htReg.Add("TIN", DBNull.Value);
                }
                else
                {
                    htReg.Add("TIN", dtHolderReg_Info.Rows[0]["TIN"].ToString());
                }

                if (dtHolderReg_Info.Rows[0]["TIN_FLAG"].Equals(DBNull.Value))
                {
                    htReg.Add("TIN_FLAG", DBNull.Value);
                }
                else
                {
                    htReg.Add("TIN_FLAG", dtHolderReg_Info.Rows[0]["TIN_FLAG"].ToString());
                }


                htReg.Add("HNAME", dtHolderReg_Info.Rows[0]["HNAME"].ToString());
                htReg.Add("FMH_NAME", dtHolderReg_Info.Rows[0]["FMH_NAME"].ToString());
                htReg.Add("MO_NAME", dtHolderReg_Info.Rows[0]["MO_NAME"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["MO_NAME"].ToString());
                htReg.Add("NATIONALITY", dtHolderReg_Info.Rows[0]["NATIONALITY"].ToString());
                if (dtHolderReg_Info.Rows[0]["OCC_CODE"].Equals(DBNull.Value))
                {
                    htReg.Add("OCC_CODE", DBNull.Value);
                }
                else
                {
                    htReg.Add("OCC_CODE", Convert.ToInt16(dtHolderReg_Info.Rows[0]["OCC_CODE"]));
                }
                htReg.Add("ADDRS1", dtHolderReg_Info.Rows[0]["ADDRS1"].ToString());
                htReg.Add("ADDRS2", dtHolderReg_Info.Rows[0]["ADDRS2"].ToString());
                htReg.Add("CITY", dtHolderReg_Info.Rows[0]["CITY"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["CITY"].ToString());
                htReg.Add("PADDRESS1", dtHolderReg_Info.Rows[0]["PADDRESS1"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["PADDRESS1"].ToString());
                htReg.Add("PADDRESS2", dtHolderReg_Info.Rows[0]["PADDRESS2"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["PADDRESS2"].ToString());
                htReg.Add("PCITY", dtHolderReg_Info.Rows[0]["PCITY"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["PCITY"].ToString());

                if (dtHolderReg_Info.Rows[0]["B_DATE"].Equals(DBNull.Value))
                {
                    htReg.Add("B_DATE", DBNull.Value);
                }
                else
                {
                    htReg.Add("B_DATE", Convert.ToDateTime(dtHolderReg_Info.Rows[0]["B_DATE"].ToString()).ToString("dd-MMM-yyyy"));
                }
                htReg.Add("ENT_TM", dtHolderReg_Info.Rows[0]["ENT_TM"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["ENT_TM"].ToString());
                htReg.Add("SEX", dtHolderReg_Info.Rows[0]["SEX"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["SEX"].ToString());
                htReg.Add("MAR_STAT", dtHolderReg_Info.Rows[0]["MAR_STAT"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["MAR_STAT"].ToString());
                htReg.Add("RELIGION", dtHolderReg_Info.Rows[0]["RELIGION"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["RELIGION"].ToString());
                htReg.Add("EDU_QUA", dtHolderReg_Info.Rows[0]["EDU_QUA"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["EDU_QUA"].ToString());
                htReg.Add("TEL_NO", dtHolderReg_Info.Rows[0]["TEL_NO"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["TEL_NO"].ToString());
                htReg.Add("EMAIL", dtHolderReg_Info.Rows[0]["EMAIL"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["EMAIL"].ToString());
                htReg.Add("REMARKS", dtHolderReg_Info.Rows[0]["REMARKS"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["REMARKS"].ToString());
                //   htReg.Add("SPEC_IN1", dtHolderReg_Info.Rows[0]["SPEC_IN1"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["SPEC_IN1"].ToString());
                //  htReg.Add("SPEC_IN2", dtHolderReg_Info.Rows[0]["SPEC_IN2"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["SPEC_IN2"].ToString());
                htReg.Add("BK_FLAG", dtHolderReg_Info.Rows[0]["BK_FLAG"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["BK_FLAG"].ToString());
                htReg.Add("BK_AC_NO", dtHolderReg_Info.Rows[0]["BK_AC_NO"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["BK_AC_NO"].ToString());


                if (dtHolderReg_Info.Rows[0]["BK_NM_CD"].Equals(DBNull.Value))
                {
                    htReg.Add("BK_NM_CD", DBNull.Value);
                }
                else
                {
                    htReg.Add("BK_NM_CD", Convert.ToInt16(dtHolderReg_Info.Rows[0]["BK_NM_CD"].ToString()));
                }

                if (dtHolderReg_Info.Rows[0]["BK_BR_NM_CD"].Equals(DBNull.Value))
                {
                    htReg.Add("BK_BR_NM_CD", DBNull.Value);
                }
                else
                {
                    htReg.Add("BK_BR_NM_CD", Convert.ToInt16(dtHolderReg_Info.Rows[0]["BK_BR_NM_CD"].ToString()));
                }

                if (dtHolderReg_Info.Rows[0]["OPN_BAL"].Equals(DBNull.Value))
                {
                    htReg.Add("OPN_BAL", DBNull.Value);
                }
                else
                {
                    htReg.Add("OPN_BAL", Convert.ToInt32(dtHolderReg_Info.Rows[0]["OPN_BAL"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["BALANCE"].Equals(DBNull.Value))
                {
                    htReg.Add("BALANCE", DBNull.Value);
                }
                else
                {
                    htReg.Add("BALANCE", Convert.ToInt32(dtHolderReg_Info.Rows[0]["BALANCE"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["DIVIDEND"].Equals(DBNull.Value))
                {
                    htReg.Add("DIVIDEND", DBNull.Value);
                }
                else
                {
                    htReg.Add("DIVIDEND", Convert.ToDecimal(dtHolderReg_Info.Rows[0]["DIVIDEND"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["CIP_DIV"].Equals(DBNull.Value))
                {
                    htReg.Add("CIP_DIV", DBNull.Value);
                }
                else
                {
                    htReg.Add("CIP_DIV", Convert.ToDecimal(dtHolderReg_Info.Rows[0]["CIP_DIV"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["TOT_DIV"].Equals(DBNull.Value))
                {
                    htReg.Add("TOT_DIV", DBNull.Value);
                }
                else
                {
                    htReg.Add("TOT_DIV", Convert.ToDecimal(dtHolderReg_Info.Rows[0]["TOT_DIV"].ToString()));
                }

                if (dtHolderReg_Info.Rows[0]["WAR_NO"].Equals(DBNull.Value))
                {
                    htReg.Add("WAR_NO", DBNull.Value);
                }
                else
                {
                    htReg.Add("WAR_NO", Convert.ToInt16(dtHolderReg_Info.Rows[0]["WAR_NO"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["U_ID"].Equals(DBNull.Value))
                {
                    htReg.Add("U_ID", DBNull.Value);
                }
                else
                {
                    htReg.Add("U_ID", Convert.ToInt16(dtHolderReg_Info.Rows[0]["U_ID"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["I_ED_DT"].Equals(DBNull.Value))
                {
                    htReg.Add("I_ED_DT", DBNull.Value);
                }
                else
                {
                    htReg.Add("I_ED_DT", Convert.ToDateTime(dtHolderReg_Info.Rows[0]["I_ED_DT"].ToString()).ToString("dd-MMM-yyyy"));
                }
                htReg.Add("INTER_BR", dtHolderReg_Info.Rows[0]["INTER_BR"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["INTER_BR"].ToString());
                if (dtHolderReg_Info.Rows[0]["CUR_BAL"].Equals(DBNull.Value))
                {
                    htReg.Add("CUR_BAL", DBNull.Value);
                }
                else
                {
                    htReg.Add("CUR_BAL", Convert.ToInt32(dtHolderReg_Info.Rows[0]["CUR_BAL"].ToString()));
                }
                htReg.Add("LIEN_MARK", dtHolderReg_Info.Rows[0]["LIEN_MARK"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["LIEN_MARK"].ToString());
                htReg.Add("LOST", dtHolderReg_Info.Rows[0]["LOST"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["LOST"].ToString());
                if (dtHolderReg_Info.Rows[0]["SL"].Equals(DBNull.Value))
                {
                    htReg.Add("SL", DBNull.Value);
                }
                else
                {
                    htReg.Add("SL", Convert.ToInt32(dtHolderReg_Info.Rows[0]["SL"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["REP"].Equals(DBNull.Value))
                {
                    htReg.Add("REP", DBNull.Value);
                }
                else
                {
                    htReg.Add("REP", Convert.ToInt32(dtHolderReg_Info.Rows[0]["REP"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["TR_IN"].Equals(DBNull.Value))
                {
                    htReg.Add("TR_IN", DBNull.Value);
                }
                else
                {
                    htReg.Add("TR_IN", Convert.ToInt32(dtHolderReg_Info.Rows[0]["TR_IN"].ToString()));
                }
                if (dtHolderReg_Info.Rows[0]["TR_OUT"].Equals(DBNull.Value))
                {
                    htReg.Add("TR_OUT", DBNull.Value);
                }
                else
                {
                    htReg.Add("TR_OUT", Convert.ToInt32(dtHolderReg_Info.Rows[0]["TR_OUT"].ToString()));
                }

                htReg.Add("USER_NM", dtHolderReg_Info.Rows[0]["USER_NM"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["USER_NM"].ToString());
                // htReg.Add("LOST", dtHolderReg_Info.Rows[0]["LOST"].Equals(DBNull.Value) ? null : dtHolderReg_Info.Rows[0]["LOST"].ToString());

                if (dtHolderReg_Info.Rows[0]["ENT_DT"].Equals(DBNull.Value))
                {
                    htReg.Add("ENT_DT", DBNull.Value);
                }
                else
                {
                    htReg.Add("ENT_DT", Convert.ToDateTime(dtHolderReg_Info.Rows[0]["ENT_DT"].ToString()).ToString("dd-MMM-yyyy"));
                }

                if (dtHolderReg_Info.Rows[0]["IS_BEFTN"].Equals(DBNull.Value))
                {
                    htReg.Add("IS_BEFTN", DBNull.Value);
                }
                else
                {
                    htReg.Add("IS_BEFTN", dtHolderReg_Info.Rows[0]["IS_BEFTN"].ToString());
                }

                commonGatewayObj.Insert(htReg, "ED_INFO_U_MASTER");//save edited registration info


                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }

        }
        public void UpdateRegInfo(UnitHolderRegistration regObj, UnitHolderInfo unitHolderObj, UnitJointHolderInfo jHolderObj, UnitHolderNominee nomiObj, UnitHolderBankInfo bankInfoObj, UnitUser userObj)//update Reg Info
        {
            Hashtable htReg = new Hashtable();
            Hashtable htJointHolder = new Hashtable();
            Hashtable htNominee = new Hashtable();
            try
            {
                commonGatewayObj.BeginTransaction();

                htReg.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                htReg.Add("REG_BR", regObj.BranchCode.ToString());
                htReg.Add("REG_NO", regObj.RegNumber.ToString());
                htReg.Add("REG_TYPE", regObj.RegType.ToString().ToUpper());
                htReg.Add("REG_DT", regObj.RegDate.ToString());
                htReg.Add("CIP", regObj.RegIsCIP.ToString().ToUpper());

                if (regObj.IsIDAccount.ToString() == "N")
                {
                    htReg.Add("ID_FLAG", regObj.IsIDAccount.ToString().ToUpper());
                    htReg.Add("ID_AC", DBNull.Value);
                    htReg.Add("ID_BK_NM_CD", DBNull.Value);
                    htReg.Add("ID_BK_BR_NM_CD", DBNull.Value);
                }
                else if (regObj.IsIDAccount.ToString() == "Y")
                {
                    htReg.Add("ID_FLAG", regObj.IsIDAccount.ToString().ToUpper());
                    htReg.Add("ID_AC", Convert.ToInt64(regObj.IdAccNo));
                    htReg.Add("ID_BK_NM_CD", Convert.ToInt16(regObj.IdBankID));
                    htReg.Add("ID_BK_BR_NM_CD", Convert.ToInt16(regObj.IdBankBranchID));
                    
                }

                htReg.Add("HNAME", unitHolderObj.HolderName.ToString().ToUpper());
                htReg.Add("FMH_NAME", unitHolderObj.HolderFMHName.ToString().ToUpper());
                htReg.Add("MO_NAME", unitHolderObj.HolderMotherName.ToString().ToUpper());
                htReg.Add("NATIONALITY", unitHolderObj.HolderNationality.ToString().ToUpper());
                htReg.Add("OCC_CODE", unitHolderObj.HolderOccupation);
                htReg.Add("ADDRS1", unitHolderObj.HolderAddress1.ToString().ToUpper());
                htReg.Add("ADDRS2", unitHolderObj.HolderAddress2.ToString().ToUpper());

                if (unitHolderObj.HolderCity.ToString() != "")
                {
                    htReg.Add("CITY", unitHolderObj.HolderCity.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("CITY", DBNull.Value);
                }

                if (unitHolderObj.HolderDateofBirth.ToString() != "")
                {
                    htReg.Add("B_DATE", Convert.ToDateTime(unitHolderObj.HolderDateofBirth).ToString("dd-MMM-yyyy"));
                }
                else
                {
                    htReg.Add("B_DATE", DBNull.Value);
                }
                if (unitHolderObj.HolderSex.ToString() != "0")
                {
                    htReg.Add("SEX", unitHolderObj.HolderSex.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("SEX", DBNull.Value);
                }
                if (unitHolderObj.HolderMaritialStatus.ToString() != "0")
                {
                    htReg.Add("MAR_STAT", unitHolderObj.HolderMaritialStatus.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("MAR_STAT", DBNull.Value);
                }
                if (unitHolderObj.HolderReligion.ToString() != "0")
                {
                    htReg.Add("RELIGION", unitHolderObj.HolderReligion.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("RELIGION", DBNull.Value);
                }
                if (unitHolderObj.HolderEduQua.ToString() != "0")
                {
                    htReg.Add("EDU_QUA", unitHolderObj.HolderEduQua.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("EDU_QUA", DBNull.Value);
                }
                if (unitHolderObj.HolderTelephone.ToString() != "")
                {
                    htReg.Add("TEL_NO", unitHolderObj.HolderTelephone.ToString());
                }
                else
                {
                    htReg.Add("TEL_NO", DBNull.Value);
                }
                if (unitHolderObj.HolderTIN.ToString() != "")
                {
                    htReg.Add("TIN", unitHolderObj.HolderTIN.ToString());
                    htReg.Add("TIN_FLAG", "Y");
                }
                else
                {
                    htReg.Add("TIN", DBNull.Value);
                    htReg.Add("TIN_FLAG", DBNull.Value);
                }
                if (unitHolderObj.HolderEmail.ToString() != "")
                {
                    htReg.Add("EMAIL", unitHolderObj.HolderEmail.ToString());
                }
                else
                {
                    htReg.Add("EMAIL", DBNull.Value);
                }
                if (unitHolderObj.HolderRemarks.ToString() != "")
                {
                    htReg.Add("REMARKS", unitHolderObj.HolderRemarks.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("REMARKS", DBNull.Value);
                }
                if (nomiObj.Nomi1Name.ToString() != "")
                {
                    htReg.Add("NOMI1", nomiObj.Nomi1Name.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("NOMI1", DBNull.Value);
                }
                if (nomiObj.Nomi1Relation.ToString() != "0")
                {
                    htReg.Add("NOMI1_REL", nomiObj.Nomi1Relation.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("NOMI1_REL", DBNull.Value);
                }

                if (nomiObj.Nomi2Name.ToString() != "")
                {
                    htReg.Add("NOMI2", nomiObj.Nomi2Name.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("NOMI2", DBNull.Value);
                }
                if (nomiObj.Nomi2Relation.ToString() != "0")
                {
                    htReg.Add("NOMI2_REL", nomiObj.Nomi2Relation.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("NOMI2_REL", DBNull.Value);
                }
                if (bankInfoObj.IsBankInfo.ToString() == "Y")
                {
                    htReg.Add("BK_FLAG", bankInfoObj.IsBankInfo.ToString().ToUpper());
                    //string bankACCName = bankInfoObj.BankAccountNo.ToString() + "," + bankInfoObj.BankName.ToString().ToUpper() + ",";
                    //string branchAddress = bankInfoObj.BranchName.ToString().ToUpper() + "," + bankInfoObj.BankAddress.ToString().ToUpper();
                    //htReg.Add("SPEC_IN1", bankACCName);
                    //htReg.Add("SPEC_IN2", branchAddress);
                    htReg.Add("BK_AC_NO", bankInfoObj.BankAccountNo.ToString());
                    htReg.Add("BK_NM_CD", Convert.ToInt16(bankInfoObj.BankCode.ToString()));
                    htReg.Add("BK_BR_NM_CD", Convert.ToInt32(bankInfoObj.BankBranchCode.ToString()));
                }
                else
                {
                    htReg.Add("BK_FLAG", bankInfoObj.IsBankInfo.ToString().ToUpper());
                    //htReg.Add("SPEC_IN1", DBNull.Value);
                    //htReg.Add("SPEC_IN2", DBNull.Value);
                    htReg.Add("BK_AC_NO", DBNull.Value);
                    htReg.Add("BK_NM_CD", DBNull.Value);
                    htReg.Add("BK_BR_NM_CD", DBNull.Value);
                }
                htReg.Add("IS_BEFTN", bankInfoObj.IsBEFTN.ToString().ToUpper());
                htReg.Add("USER_NM", userObj.UserID.ToString().ToUpper());
                htReg.Add("ENT_DT", DateTime.Now);
               
                commonGatewayObj.Update(htReg, "U_MASTER", "REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO='" + regObj.RegNumber.ToString() + "'");
             
                if (nomiObj.Nomi1Name.ToString() != "")
                {

                    htNominee.Add("NOMI_CTL_NO", nomiObj.NomiControlNo.ToString());
                    htNominee.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htNominee.Add("REG_BR", regObj.BranchCode.ToString());
                    htNominee.Add("REG_NO", regObj.RegNumber.ToString());
                    htNominee.Add("NOMI_NO", 1);
                    htNominee.Add("NOMI_NAME", nomiObj.Nomi1Name.ToString().ToUpper());

                    if (nomiObj.Nomi1Nationality.ToString() != "")
                    {
                        htNominee.Add("NOMI_NATIONALITY", nomiObj.Nomi1Nationality.ToString().ToUpper());
                    }
                    else
                    {
                        htNominee.Add("NOMI_NATIONALITY", DBNull.Value);
                    }
                    if (nomiObj.Nomi1FMName.ToString() != "")
                    {
                        htNominee.Add("NOMI_FMH_NAME", nomiObj.Nomi1FMName.ToString().ToUpper());
                    }
                    else
                    {
                        htNominee.Add("NOMI_FMH_NAME", DBNull.Value);
                    }
                    if (nomiObj.Nomi1MotherName.ToString() != "")
                    {
                        htNominee.Add("NOMI_MO_NAME", nomiObj.Nomi1MotherName.ToString().ToUpper());
                    }
                    else
                    {
                        htNominee.Add("NOMI_MO_NAME", DBNull.Value);
                    }
                    if (nomiObj.Nomi1Occupation.ToString() != "0")
                    {
                        htNominee.Add("NOMI_OCC_CODE", nomiObj.Nomi1Occupation);
                    }
                    else
                    {
                        htNominee.Add("NOMI_OCC_CODE", DBNull.Value);
                    }
                    if (nomiObj.Nomi1Address1.ToString() != "")
                    {
                        htNominee.Add("NOMI_ADDRS1", nomiObj.Nomi1Address1.ToString().ToUpper());
                    }
                    else
                    {
                        htNominee.Add("NOMI_ADDRS1", DBNull.Value);
                    }
                    if (nomiObj.Nomi1Address2.ToString() != "")
                    {
                        htNominee.Add("NOMI_ADDRS2", nomiObj.Nomi1Address2.ToString().ToUpper());
                    }
                    else
                    {
                        htNominee.Add("NOMI_ADDRS2", DBNull.Value);
                    }
                    if (nomiObj.Nomi1Relation.ToString() != "0")
                    {
                        htNominee.Add("NOMI_REL", nomiObj.Nomi1Relation.ToString().ToUpper());
                    }
                    else
                    {
                        htNominee.Add("NOMI_REL", DBNull.Value);
                    }
                    htNominee.Add("PERCENTAGE", Convert.ToDecimal(nomiObj.Nomi1Percentage.ToString()));                  
                    if (OmfDAOObj.IsExistNominee(regObj, 1))
                    {
                        commonGatewayObj.Update(htNominee, "U_NOMINEE", "REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO='" + regObj.RegNumber.ToString() + "' AND NOMI_NO=1");
                    }
                    else if (!OmfDAOObj.IsExistNominee(regObj, 1))
                    {
                        commonGatewayObj.Insert(htNominee, "U_NOMINEE");
                    }                
                }
                else if (nomiObj.Nomi1Name.ToString() == "" && nomiObj.Nomi1Address1.ToString() == "" && nomiObj.Nomi1Address2.ToString() == "" && nomiObj.Nomi1FMName.ToString() == "" && nomiObj.Nomi1Nationality == "" && nomiObj.Nomi1Occupation.ToString() == "0" && nomiObj.Nomi1Percentage.ToString() == "" && nomiObj.Nomi1Relation.ToString() == "0")
                {
                    if (OmfDAOObj.IsExistNominee(regObj, 1))
                    {                       
                        commonGatewayObj.DeleteByCommand("U_NOMINEE", "REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO='" + regObj.RegNumber.ToString() + "' AND NOMI_NO=1");
                      
                    }
                }


                if (nomiObj.Nomi2Name.ToString() != "")
                {
                    htNominee = new Hashtable();
                    htNominee.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                    htNominee.Add("REG_BR", regObj.BranchCode.ToString());
                    htNominee.Add("REG_NO", regObj.RegNumber.ToString());
                    htNominee.Add("NOMI_NO", 2);
                    htNominee.Add("NOMI_NAME", nomiObj.Nomi2Name.ToString().ToUpper());
                    if (nomiObj.Nomi2Nationality.ToString() != "")
                    {
                        htNominee.Add("NOMI_NATIONALITY", nomiObj.Nomi2Nationality.ToString().ToUpper());
                    }
                    else
                    {
                        htNominee.Add("NOMI_NATIONALITY", DBNull.Value);
                    }
                    if (nomiObj.Nomi2FMName.ToString() != "")
                    {
                        htNominee.Add("NOMI_FMH_NAME", nomiObj.Nomi2FMName.ToString().ToUpper());
                    }
                    else
                    {
                        htNominee.Add("NOMI_FMH_NAME", DBNull.Value);
                    }
                    if (nomiObj.Nomi2MotherName.ToString() != "")
                    {
                        htNominee.Add("NOMI_MO_NAME", nomiObj.Nomi2MotherName.ToString().ToUpper());
                    }
                    else
                    {
                        htNominee.Add("NOMI_MO_NAME", DBNull.Value);
                    }
                    if (nomiObj.Nomi2Occupation.ToString() != "0")
                    {
                        htNominee.Add("NOMI_OCC_CODE", nomiObj.Nomi2Occupation);
                    }
                    else
                    {
                        htNominee.Add("NOMI_OCC_CODE", DBNull.Value);
                    }
                    if (nomiObj.Nomi2Address1.ToString() != "")
                    {
                        htNominee.Add("NOMI_ADDRS1", nomiObj.Nomi2Address1.ToString().ToUpper());
                    }
                    else
                    {
                        htNominee.Add("NOMI_ADDRS1", DBNull.Value);
                    }
                    if (nomiObj.Nomi2Address2.ToString() != "")
                    {
                        htNominee.Add("NOMI_ADDRS2", nomiObj.Nomi2Address2.ToString().ToUpper());
                    }
                    else
                    {
                        htNominee.Add("NOMI_ADDRS2", DBNull.Value);
                    }
                    if (nomiObj.Nomi2Relation.ToString() != "0")
                    {
                        htNominee.Add("NOMI_REL", nomiObj.Nomi2Relation.ToString().ToUpper());
                    }
                    else
                    {
                        htNominee.Add("NOMI_REL", DBNull.Value);
                    }
                    htNominee.Add("PERCENTAGE", Convert.ToDecimal(nomiObj.Nomi2Percentage.ToString()));

                    if (OmfDAOObj.IsExistNominee(regObj, 2))
                    {
                        commonGatewayObj.Update(htNominee, "U_NOMINEE", "REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO='" + regObj.RegNumber.ToString() + "' AND NOMI_NO=2");
                    }
                    else if (!OmfDAOObj.IsExistNominee(regObj, 2))
                    {
                        commonGatewayObj.Insert(htNominee, "U_NOMINEE");
                    }                  
                }
                else if (nomiObj.Nomi2Name.ToString() == "" && nomiObj.Nomi2Address1.ToString() == "" && nomiObj.Nomi2Address2.ToString() == "" && nomiObj.Nomi2FMName.ToString() == "" && nomiObj.Nomi2Nationality == "" && nomiObj.Nomi2Occupation.ToString() == "0" && nomiObj.Nomi2Percentage.ToString() == "" && nomiObj.Nomi2Relation.ToString() == "0")
                {
                    if (OmfDAOObj.IsExistNominee(regObj, 2))
                    {                       
                        commonGatewayObj.DeleteByCommand("U_NOMINEE", "REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO='" + regObj.RegNumber.ToString() + "' AND NOMI_NO=2");
                        
                    }
                }

               if (!(string.Compare(regObj.FundCode.ToString().ToUpper(), "IAMPH", true) == 0))
               {
                    if (jHolderObj.JHolderName.ToString() != "")
                    {
                        htJointHolder.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                        htJointHolder.Add("REG_BR", regObj.BranchCode.ToString());
                        htJointHolder.Add("REG_NO", regObj.RegNumber.ToString());
                        htJointHolder.Add("JNT_NAME", jHolderObj.JHolderName.ToString().ToUpper());
                        if (jHolderObj.JHolderFMHName.ToString() != "")
                        {
                            htJointHolder.Add("JNT_FMH_NAME", jHolderObj.JHolderFMHName.ToString().ToUpper());
                        }
                        else
                        {
                            htJointHolder.Add("JNT_FMH_NAME", DBNull.Value);
                        }
                        if (jHolderObj.JHolderMotherName.ToString() != "")
                        {
                            htJointHolder.Add("JNT_MO_NAME", jHolderObj.JHolderMotherName.ToString().ToUpper());
                        }
                        else
                        {
                            htJointHolder.Add("JNT_MO_NAME", DBNull.Value);
                        }
                        if (jHolderObj.JHolderOccupation.ToString() != "0")
                        {
                            htJointHolder.Add("JNT_OCC_CODE", jHolderObj.JHolderOccupation);
                        }
                        else
                        {
                            htJointHolder.Add("JNT_OCC_CODE", DBNull.Value);
                        }
                        if (jHolderObj.JHolderNationality.ToString() != "")
                        {
                            htJointHolder.Add("JNT_NATIONALITY", jHolderObj.JHolderNationality.ToString().ToUpper());
                        }
                        else
                        {
                            htJointHolder.Add("JNT_NATIONALITY", DBNull.Value);
                        }
                        if (jHolderObj.JHolderAddress1.ToString() != "")
                        {
                            htJointHolder.Add("JNT_ADDRS1", jHolderObj.JHolderAddress1.ToString().ToUpper());
                        }
                        else
                        {
                            htJointHolder.Add("JNT_ADDRS1", DBNull.Value);
                        }
                        if (jHolderObj.JHolderAddress2.ToString() != "")
                        {
                            htJointHolder.Add("JNT_ADDRS2", jHolderObj.JHolderAddress2.ToString().ToUpper());
                        }
                        else
                        {
                            htJointHolder.Add("JNT_ADDRS2", DBNull.Value);
                        }
                        htJointHolder.Add("JNT_NO", 2);
                        if (OmfDAOObj.IsExistJointHolder(regObj))
                        {
                            commonGatewayObj.Update(htJointHolder, "U_JHOLDER", "REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO='" + regObj.RegNumber.ToString() + "'");
                        }
                        else
                        {
                            commonGatewayObj.Insert(htJointHolder, "U_JHOLDER");//save joint holder info
                        }
                    }
                    else if ((jHolderObj.JHolderMotherName.ToString() == "") && (jHolderObj.JHolderName.ToString() == "") && (jHolderObj.JHolderFMHName.ToString() == "") && (jHolderObj.JHolderOccupation.ToString() == "0") && (jHolderObj.JHolderNationality.ToString() == "") && (jHolderObj.JHolderAddress1.ToString() == "") && (jHolderObj.JHolderAddress1.ToString() == "") && (jHolderObj.JHolderAddress1.ToString() == ""))
                    {
                        if (OmfDAOObj.IsExistJointHolder(regObj))
                        {
                            commonGatewayObj.DeleteByCommand("U_JHOLDER", "REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO='" + regObj.RegNumber.ToString() + "'");
                        }
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
        public void UpdateRegInfo(UnitHolderRegistration regObj, UnitHolderInfo unitHolderObj, UnitHolderBankInfo bankInfoObj, UnitUser userObj)//update Reg Info
        {
            try
            {
                Hashtable htReg = new Hashtable();
                commonGatewayObj.BeginTransaction();
                //U_master
                ////htReg.Add("REG_BK", regObj.FundCode.ToString().ToUpper());
                ////htReg.Add("REG_BR", regObj.BranchCode.ToString());
                ////htReg.Add("REG_NO", regObj.RegNumber.ToString());
                htReg.Add("REG_TYPE", regObj.RegType.ToString().ToUpper());
                htReg.Add("REG_DT", regObj.RegDate.ToString());
                htReg.Add("CIP", regObj.RegIsCIP.ToString().ToUpper());

                if (regObj.IsIDAccount.ToString() == "N")
                {
                    htReg.Add("ID_FLAG", regObj.IsIDAccount.ToString().ToUpper());
                    htReg.Add("ID_AC", DBNull.Value);
                    htReg.Add("ID_BK_NM_CD", DBNull.Value);
                    htReg.Add("ID_BK_BR_NM_CD", DBNull.Value);

                }
                else if (regObj.IsIDAccount.ToString() == "Y")
                {
                    htReg.Add("ID_FLAG", regObj.IsIDAccount.ToString().ToUpper());
                    htReg.Add("ID_AC", Convert.ToInt64(regObj.IdAccNo));
                    htReg.Add("ID_BK_NM_CD", Convert.ToInt16(regObj.IdBankID));
                    htReg.Add("ID_BK_BR_NM_CD", Convert.ToInt16(regObj.IdBankBranchID));
                }

                htReg.Add("HNAME", unitHolderObj.HolderName.ToString().ToUpper());
                htReg.Add("FMH_NAME", unitHolderObj.HolderFMHName.ToString().ToUpper());
                htReg.Add("MO_NAME", unitHolderObj.HolderMotherName.ToString().ToUpper());
                if (unitHolderObj.HolderSpouceName.ToString() != "")
                {
                    htReg.Add("SP_NAME", unitHolderObj.HolderSpouceName.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("SP_NAME", DBNull.Value);
                }

                if (unitHolderObj.HolderNID.ToString() != "")
                {
                    htReg.Add("NID", unitHolderObj.HolderNID.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("NID", DBNull.Value);
                }
                              
                if (unitHolderObj.HolderPassport.ToString() != "")
                {
                    htReg.Add("PASS_NO", unitHolderObj.HolderPassport.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("PASS_NO", DBNull.Value);
                }
                
                if (unitHolderObj.HolderBirthCertNo.ToString() != "")
                {
                    htReg.Add("BIRTH_CERT_NO", unitHolderObj.HolderBirthCertNo.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("BIRTH_CERT_NO", DBNull.Value);
                }
                
                if (unitHolderObj.HolderTIN.ToString() != "")
                {
                    htReg.Add("TIN", unitHolderObj.HolderTIN.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("TIN", DBNull.Value);
                }
                if (unitHolderObj.HolderTIN.ToString() != "")
                {
                    htReg.Add("TIN_FLAG", unitHolderObj.HolderTINFlag.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("TIN_FLAG", DBNull.Value);
                }

           
                if (unitHolderObj.HolderNationality.ToString() != "")
                {
                    htReg.Add("NATIONALITY", unitHolderObj.HolderNationality.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("NATIONALITY", DBNull.Value);
                }
              
                if (unitHolderObj.HolderOccupation.ToString() != "")
                {
                    htReg.Add("OCC_CODE", unitHolderObj.HolderOccupation.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("OCC_CODE", DBNull.Value);
                }


             
                if (unitHolderObj.HolderAddress1.ToString() != "")
                {
                    htReg.Add("ADDRS1", unitHolderObj.HolderAddress1.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("ADDRS1", DBNull.Value);
                }
           
                if (unitHolderObj.HolderAddress2.ToString() != "")
                {
                    htReg.Add("ADDRS2", unitHolderObj.HolderAddress2.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("ADDRS2", DBNull.Value);
                }
                if (unitHolderObj.HolderCity.ToString() != "")
                {
                    htReg.Add("CITY", unitHolderObj.HolderCity.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("CITY", DBNull.Value);
                }

             
                if (unitHolderObj.HolderPermanAddress1.ToString() != "")
                {
                    htReg.Add("PADDRESS1", unitHolderObj.HolderPermanAddress1.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("PADDRESS1", DBNull.Value);
                }
           
                if (unitHolderObj.HolderPermanAddress2.ToString() != "")
                {
                    htReg.Add("PADDRESS2", unitHolderObj.HolderPermanAddress2.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("PADDRESS2", DBNull.Value);
                }
                if (unitHolderObj.HolderPermanCity.ToString() != "")
                {
                    htReg.Add("PCITY", unitHolderObj.HolderPermanCity.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("PCITY", DBNull.Value);
                }

             //   htReg.Add("SOURCE_FUND", unitHolderObj.HolderSourceFund.ToString().ToUpper());
                if (unitHolderObj.HolderSourceFund.ToString() != "")
                {
                    htReg.Add("SOURCE_FUND", unitHolderObj.HolderSourceFund.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("SOURCE_FUND", DBNull.Value);
                }

                if (unitHolderObj.HolderDateofBirth.ToString() != "")
                {
                    htReg.Add("B_DATE", Convert.ToDateTime(unitHolderObj.HolderDateofBirth).ToString("dd-MMM-yyyy"));
                }
                else
                {
                    htReg.Add("B_DATE", DBNull.Value);
                }
                if (unitHolderObj.HolderSex.ToString() != "0")
                {
                    htReg.Add("SEX", unitHolderObj.HolderSex.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("SEX", DBNull.Value);
                }

                if (unitHolderObj.HolderMaritialStatus.ToString() != "0")
                {
                    htReg.Add("MAR_STAT", unitHolderObj.HolderMaritialStatus.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("MAR_STAT", DBNull.Value);
                }

                if (unitHolderObj.HolderReligion.ToString() != "0")
                {
                    htReg.Add("RELIGION", unitHolderObj.HolderReligion.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("RELIGION", DBNull.Value);
                }

                if (unitHolderObj.HolderEduQua.ToString() != "0")
                {
                    htReg.Add("EDU_QUA", unitHolderObj.HolderEduQua.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("EDU_QUA", DBNull.Value);
                }
                if (unitHolderObj.HolderTelephone.ToString() != "")
                {
                    htReg.Add("TEL_NO", unitHolderObj.HolderTelephone.ToString());
                }
                else
                {
                    htReg.Add("TEL_NO", DBNull.Value);
                }
                if (unitHolderObj.HolderEmail.ToString() != "")
                {
                    htReg.Add("EMAIL", unitHolderObj.HolderEmail.ToString());
                }
                else
                {
                    htReg.Add("EMAIL", DBNull.Value);
                }
                if (unitHolderObj.HolderRemarks.ToString() != "")
                {
                    htReg.Add("REMARKS", unitHolderObj.HolderRemarks.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("REMARKS", DBNull.Value);
                }

                if (unitHolderObj.HolderKYC.ToString() != "")
                {
                    htReg.Add("KYC", unitHolderObj.HolderKYC.ToString().ToUpper());
                    htReg.Add("KYC_UPDATE_BY", userObj.UserID.ToString().ToUpper());
                    htReg.Add("KYC_UPDATE_DT", DateTime.Now.ToString("dd-MMM-yyyy"));
                }
                else
                {
                    htReg.Add("KYC", DBNull.Value);
                }
                if (unitHolderObj.HolderBONumber.ToString() != "")
                {
                    htReg.Add("BO", unitHolderObj.HolderBONumber.ToString().ToUpper());
                }
                else
                {
                    htReg.Add("BO", DBNull.Value);
                }
                //if (unitHolderObj.HolderNPBNo.ToString() != "")
                //{
                //    htReg.Add("NPB_NO", unitHolderObj.HolderNPBNo.ToString().ToUpper());
                //}
                //htReg.Add("NPB_TYPE", unitHolderObj.HolderNPBType.ToString().ToUpper());

                if (bankInfoObj.IsBankInfo.ToString() == "Y")
                {
                    htReg.Add("BK_FLAG", bankInfoObj.IsBankInfo.ToString().ToUpper());                    
                    htReg.Add("BK_AC_NO", bankInfoObj.BankAccountNo.ToString());
                    htReg.Add("BK_NM_CD", Convert.ToInt16(bankInfoObj.BankCode.ToString()));
                    htReg.Add("BK_BR_NM_CD", Convert.ToInt32(bankInfoObj.BankBranchCode.ToString()));
                }
                else
                {
                    htReg.Add("BK_FLAG", bankInfoObj.IsBankInfo.ToString().ToUpper());                   
                    htReg.Add("BK_AC_NO", DBNull.Value);
                    htReg.Add("BK_NM_CD", DBNull.Value);
                    htReg.Add("BK_BR_NM_CD", DBNull.Value);
                }

               

                
                htReg.Add("IS_BEFTN", bankInfoObj.IsBEFTN.ToString().ToUpper());

                htReg.Add("USER_NM", userObj.UserID.ToString());
                htReg.Add("ENT_DT", DateTime.Today.ToString("dd-MMM-yyyy"));
                htReg.Add("ENT_TM", DateTime.Now.ToShortTimeString());

                //commonGatewayObj.Insert(htReg, "U_MASTER");
                commonGatewayObj.Update(htReg, "U_MASTER", "REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO='" + regObj.RegNumber.ToString() + "'");

                commonGatewayObj.CommitTransaction();

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }
        }
        public bool IsDuplicateReg(UnitHolderRegistration regObj)
        {
            DataTable dtReg = commonGatewayObj.Select("SELECT * FROM U_MASTER WHERE REG_NO='" + regObj.RegNumber.ToString() + "'AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "'");
            if (dtReg.Rows.Count > 0)
                return true;
            else
                return false;

        }
        public bool IsDuplicateJointHolder(UnitHolderRegistration regObj)
        {
            DataTable dtReg = commonGatewayObj.Select("SELECT * FROM U_JHOLDER WHERE REG_NO='" + regObj.RegNumber.ToString() + "'AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "'");
            if (dtReg.Rows.Count > 0)
                return true;
            else
                return false;

        }
        public bool IsDuplicateNomineeControl(UnitHolderRegistration regObj, UnitHolderNominee nomiObj)
        {
            DataTable dtReg = commonGatewayObj.Select("SELECT * FROM U_NOMINEE WHERE NOMI_CTL_NO='" + nomiObj.NomiControlNo.ToString() + "'AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "'");
            if (dtReg.Rows.Count > 0)
                return true;
            else
                return false;

        }
        public bool IsDuplicateNomineeControlEdit(UnitHolderRegistration regObj, UnitHolderNominee nomiObj)
        {
            DataTable dtReg = commonGatewayObj.Select("SELECT * FROM U_NOMINEE WHERE NOMI_CTL_NO='" + nomiObj.NomiControlNo.ToString() + "'AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "' AND REG_NO<>"+regObj.RegNumber);
            if (dtReg.Rows.Count > 0)
                return true;
            else
                return false;

        }
        public int GetMaxRegNo(UnitHolderRegistration regObj)
        {
            DataTable dtMaxRegNo = new DataTable();
            dtMaxRegNo = commonGatewayObj.Select("SELECT MAX(REG_NO) AS MAX_REG_NO FROM U_MASTER WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'");
            int maxRegNo = dtMaxRegNo.Rows[0]["MAX_REG_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtMaxRegNo.Rows[0]["MAX_REG_NO"].ToString());
            return maxRegNo + 1;
        }
        public DateTime getLastRegDate(UnitHolderRegistration regObj)
        {
            DataTable dtLastRegDate = new DataTable();
            dtLastRegDate = commonGatewayObj.Select("SELECT MAX(REG_DT) AS MAX_REG_DATE FROM U_MASTER WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND REG_NO=" + Convert.ToInt32(regObj.RegNumber));
            DateTime lastRegDate = Convert.ToDateTime(dtLastRegDate.Rows[0]["MAX_REG_DATE"].Equals(DBNull.Value) ? DateTime.Today.ToString() : dtLastRegDate.Rows[0]["MAX_REG_DATE"].ToString());
            return lastRegDate;

        }
        public DataTable dtFillRegType()
        {
            DataTable dtFillRegType = commonGatewayObj.Select("SELECT ID, CODE , NAME FROM REG_TYPE WHERE VALID IS NULL  ORDER BY ID");
            DataTable dtFillRegTypeDropDown = new DataTable();
            dtFillRegTypeDropDown.Columns.Add("CODE", typeof(string));
            dtFillRegTypeDropDown.Columns.Add("NAME", typeof(string));

            DataRow drFillRegTypeDropDown = dtFillRegTypeDropDown.NewRow();
            //drBankNameDropDown["BANK_NAME"] = "--Select Bank--- ";
            //drBankNameDropDown["BANK_CODE"] = "0";
            //dtBankNameDropDown.Rows.Add(drBankNameDropDown);
            for (int loop = 0; loop < dtFillRegType.Rows.Count; loop++)
            {
                drFillRegTypeDropDown = dtFillRegTypeDropDown.NewRow();
                drFillRegTypeDropDown["NAME"] = dtFillRegType.Rows[loop]["NAME"].ToString();
                drFillRegTypeDropDown["CODE"] = dtFillRegType.Rows[loop]["CODE"].ToString();
                dtFillRegTypeDropDown.Rows.Add(drFillRegTypeDropDown);
            }

            return dtFillRegTypeDropDown;
        }
        public string getIDBankSelectedValue(string reg_br)
        {
            return "aa";
        }
        public string getIDBankBranchSelectedValue(string reg_br)
        {
            return "aa";
        }
        public string CheckSanctionList(UnitHolderRegistration regObj,UnitHolderInfo unitHolderObj)
        {
            string errorMessage = "";
            string[] holderString = unitHolderObj.HolderName.ToString().Split(' ');
          
            DataTable dtName=new DataTable();
            if (regObj.RegType.ToString().ToUpper() == "N") 
            {
                string holderName = "";
                for (int loop = 0; loop < holderString.Length; loop++)
                {
                    if (holderString[loop].ToString() != "")
                    {
                        if (holderName == "")
                        {
                            holderName = holderString[loop].ToString();
                        }
                        else
                        {
                            holderName = holderName + holderString[loop].ToString();
                        }
                    }
                }
                dtName = commonGatewayObj.Select("SELECT * FROM SLIST_INDIVIDUAL WHERE   UPPER(FIRST_NAME || SECOND_NAME || THIRD_NAME || FOURTH_NAME)='" + holderName.ToString().ToUpper() + "' OR UPPER(SORT_KEY) LIKE '%" + unitHolderObj.HolderName.ToString().ToUpper() + "%' ");
            }
            else
            {
                dtName = commonGatewayObj.Select("SELECT * FROM SLIST_ENTITY WHERE   UPPER(FIRST_NAME) LIKE '%" + unitHolderObj.HolderName.ToString().ToUpper() + "%' OR UPPER(SORT_KEY) LIKE '%" + unitHolderObj.HolderName.ToString().ToUpper() + "%' ");
            }
            if (dtName.Rows.Count > 0)
            {
                errorMessage = "This name is found in sanction list!!";
            }
            return errorMessage.ToString();
        }
        public DataTable dtGetBankBracnhInfo(int bankCode, int BranchCode)
        {
            DataTable dtBankBracnhInfo = new DataTable();
            try
            {
                dtBankBracnhInfo = commonGatewayObj.Select("SELECT  NVL(ROUTING_NO,'') AS ROUTING_NO, NVL(BRANCH_ADDRS1,'')||' '||NVL(BRANCH_ADDRS2,'')||' '|| NVL(BRANCH_DISTRICT,'')AS ADDRESS FROM BANK_BRANCH WHERE BANK_CODE=" + bankCode + " AND BRANCH_CODE=" + BranchCode);
                return dtBankBracnhInfo;

            }
              catch (Exception ex)
              {
                  commonGatewayObj.RollbackTransaction();
                  throw ex;
              }
        }
        public DataTable dtGetBankBracnhInfo(string routingNo)
        {
            DataTable dtBankBracnhInfo = new DataTable();
            try
            {
                dtBankBracnhInfo = commonGatewayObj.Select("SELECT BANK_CODE,BRANCH_CODE,   ROUTING_NO, NVL(BRANCH_ADDRS1,'')||' '||NVL(BRANCH_ADDRS2,'')||' '|| NVL(BRANCH_DISTRICT,'')AS ADDRESS FROM BANK_BRANCH WHERE ROUTING_NO='"+routingNo.ToString()+"'");
                return dtBankBracnhInfo;

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                throw ex;
            }
        }
        public DataTable dtGetNomineeInfo(UnitHolderRegistration regObj)
        {
            DataTable dtNominee = commonGatewayObj.Select("SELECT * FROM U_NOMINEE WHERE REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "'  AND REG_NO=" + Convert.ToInt32(regObj.RegNumber));
            return dtNominee;


        }
        public DataTable dtGetNomineeInfo(UnitHolderRegistration regObj,int nomiNumber)
        {
            DataTable dtNominee = commonGatewayObj.Select("SELECT * FROM U_NOMINEE WHERE  NOMI_NO=" + nomiNumber + " AND  REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'AND REG_BR='" + regObj.BranchCode.ToString() + "'  AND REG_NO=" + Convert.ToInt32(regObj.RegNumber));
            return dtNominee;
          

        }
        public bool IsEditPermit(string fund_cd)
        {

            DataTable dtReg = commonGatewayObj.Select("SELECT * FROM DIVI_PARA WHERE FUND_CD='" + fund_cd.ToString().ToUpper() + "' AND UPPER(EDIT_PERMIT)='N' OR  EDIT_PERMIT IS NULL ");
            if (dtReg.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public string kycStatus(UnitHolderInfo unitHolderObj)
        {
            string finalStatus = "";
            bool status1=true;
            bool status2 = true;
            bool status3 = true;

            if ((unitHolderObj.HolderNID.ToString() == "") && (unitHolderObj.HolderPassport.ToString() == "") && (unitHolderObj.HolderTIN.ToString() == "") && (unitHolderObj.HolderBirthCertNo.ToString() == ""))
            {
                status1 = false;
            }

            if (unitHolderObj.HolderAddress1.ToString() == "" && unitHolderObj.HolderAddress2.ToString() == "" && unitHolderObj.HolderCity.ToString() == "")
             {
                 status2 = false;
             }

            if (unitHolderObj.HolderPermanAddress1.ToString() == "" && unitHolderObj.HolderPermanAddress2.ToString() == "" && unitHolderObj.HolderPermanCity.ToString() == "")
             {
                 status3 = false;
             }
             if (status1 && status2 && status3)
             {
                 finalStatus = "Y";
             }
             return finalStatus;
        }
        
       
    }
}
