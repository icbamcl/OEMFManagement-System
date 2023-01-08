using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections;
using System.Data;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

/// <summary>
/// Summary description for UnitSIPBL
/// </summary>
public class UnitSIPBL
{

    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO OmfDAOObj = new OMFDAO();
    UnitSIPGetSet unitSIPObj = new UnitSIPGetSet();
    public UnitSIPBL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int getNextSIPMasterInfo(UnitHolderRegistration regObj)
    {
        int nextSIPNo = 0;
        DataTable dtSIPMasterInfo = commonGatewayObj.Select("SELECT NVL(MAX(SIP_NO),0)+1 AS SIP_NO FROM SIP_MASTER  WHERE  VALID IS NULL AND SIP_STATUS IS NULL AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'");
        if (dtSIPMasterInfo.Rows.Count > 0)
        {
            nextSIPNo = Convert.ToInt32(dtSIPMasterInfo.Rows[0]["SIP_NO"].ToString());
        }
        return nextSIPNo;



    }
    public void SaveRegSIPInfo(UnitHolderRegistration regObj, UnitSIPGetSet unitSIPObj, UnitUser userObj)
    {
        Hashtable htRegSIPInfo = new Hashtable();
        try
        {
            commonGatewayObj.BeginTransaction();
            htRegSIPInfo.Add("ID", commonGatewayObj.GetMaxNo("SIP_MASTER", "ID") + 1);
            htRegSIPInfo.Add("REG_BK", regObj.FundCode);
            htRegSIPInfo.Add("REG_BR", regObj.BranchCode);
            htRegSIPInfo.Add("REG_NO", regObj.RegNumber);

            htRegSIPInfo.Add("SIP_NO", unitSIPObj.SipNumber);
            //  htRegSIPInfo.Add("SIP_DATE", "");
            htRegSIPInfo.Add("AMOUNT", unitSIPObj.InstallAmount);
            htRegSIPInfo.Add("SIP_DATE", unitSIPObj.EftDate);
            htRegSIPInfo.Add("SIP_START_DATE", unitSIPObj.EftDebitStart);
            htRegSIPInfo.Add("SIP_MATURE_DATE", unitSIPObj.EftDebitEnd);
            htRegSIPInfo.Add("SIP_DURATION_IN_MONTH", unitSIPObj.DurationInMonth);
            htRegSIPInfo.Add("IS_AUTO_RENEWAL", unitSIPObj.AutoRenewal);
            htRegSIPInfo.Add("PAYMENT_FREQUENCY", unitSIPObj.PayFrequency);
            htRegSIPInfo.Add("MONTH_START_DAY", unitSIPObj.MonhlySIPDate);
            htRegSIPInfo.Add("SIP_ACC_NO", unitSIPObj.BankAccNumber);
            htRegSIPInfo.Add("SIP_ACC_ROUTING_NO", unitSIPObj.RoutingNo);
            htRegSIPInfo.Add("SIP_ACC_TYPE", unitSIPObj.EftAccType.ToUpper());

            if (unitSIPObj.ScheduleCreateDate != null)
            {
                htRegSIPInfo.Add("SCHEDULE_CREATE_DATE", unitSIPObj.ScheduleCreateDate);
                htRegSIPInfo.Add("SCHEDULE_CREATE_BY", unitSIPObj.ScheduleCreateBy);
            }

            htRegSIPInfo.Add("ENTRY_BY", userObj.UserID.ToString().ToUpper());
            htRegSIPInfo.Add("ENTRY_DATE", DateTime.Now.ToString("dd-MMM-yyyy"));

            commonGatewayObj.ExecuteNonQuery("UPDATE U_MASTER SET SIP='Y' WHERE REG_BK='" + regObj.FundCode + "' AND REG_BR='" + regObj.BranchCode + "' AND REG_NO=" + regObj.RegNumber);
            commonGatewayObj.Insert(htRegSIPInfo, "SIP_MASTER");//save SIP info

            commonGatewayObj.CommitTransaction();

        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            throw ex;
        }
    }
    public int UpdateSIPInfo(UnitHolderRegistration regObj, UnitUser userObj, string editType, long ID)
    {
        Hashtable htUpdateRegSIPInfo = new Hashtable();
        int updateInfo = 0;
        try
        {
            commonGatewayObj.BeginTransaction();

            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" UPDATE  SIP_MASTER SET VALID='N',EDIT_TYPE='" + editType.ToUpper().ToString() + "' , ENTRY_BY='" + userObj.UserID.ToString() + "', ENTRY_DATE='" + DateTime.Today.ToString("dd-MMM-yyyy") + "'");
            sbQuery.Append(" WHERE  REG_BK='" + regObj.FundCode + "' AND REG_BR='" + regObj.BranchCode + "' AND ID =" + ID);
            updateInfo = commonGatewayObj.ExecuteNonQuery(sbQuery.ToString());


            if(editType.ToString().ToString()=="D")
            {
                DataTable dtSIPDetails = commonGatewayObj.Select("SELECT SIP_NO FROM SIP_MASTER WHERE ID =" + ID);
                long sipNo =Convert.ToInt32(dtSIPDetails.Rows[0]["SIP_NO"]);
                StringBuilder sbQuery1 = new StringBuilder();
                sbQuery1.Append(" DELETE FROM SIP_DETAILS ");
                sbQuery1.Append(" WHERE  REG_BK='" + regObj.FundCode + "' AND REG_BR='" + regObj.BranchCode + "' AND SIP_NO =" + sipNo);
                commonGatewayObj.ExecuteNonQuery(sbQuery1.ToString());
            }
            commonGatewayObj.CommitTransaction();

        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            throw ex;
        }
        return updateInfo;
    }
    public DataTable dtSIPMasterInfoByID(long ID)
    {
        DataTable dtSIPInfo = commonGatewayObj.Select(" SELECT * FROM SIP_MASTER WHERE ID=" + ID);
        return dtSIPInfo;
    }
    public bool isDuplicateSIP(UnitHolderRegistration regObj, UnitSIPGetSet unitSIPObj)
    {
        bool isDuplicateSIP = false;
        DataTable dtSIPInfo = commonGatewayObj.Select("SELECT * FROM SIP_MASTER WHERE VALID IS NULL AND SIP_STATUS IS NULL AND SIP_NO=" + unitSIPObj.SipNumber + " AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "'");
        if (dtSIPInfo.Rows.Count > 0)
            isDuplicateSIP = true;
        return isDuplicateSIP;
    }
    public DataTable dtSIPListDDL(string filter)
    {
        DataTable dtSIPList = commonGatewayObj.Select("SELECT ID, SIP_NO FROM SIP_MASTER WHERE VALID IS NULL AND SIP_STATUS IS NULL " + filter.ToString() + " ORDER BY SIP_NO ");
        DataTable dtSIPListDDL = new DataTable();
        if (dtSIPList.Rows.Count > 0)
        {

            dtSIPListDDL.Columns.Add("ID", typeof(string));
            dtSIPListDDL.Columns.Add("SIP_NO", typeof(string));
            DataRow drDDL;
            drDDL = dtSIPListDDL.NewRow();
            drDDL["ID"] = "0";
            drDDL["SIP_NO"] = " Select SIP ";
            dtSIPListDDL.Rows.Add(drDDL);

            for (int loop = 0; loop < dtSIPList.Rows.Count; loop++)
            {
                drDDL = dtSIPListDDL.NewRow();
                drDDL["ID"] = dtSIPList.Rows[loop]["ID"].ToString();
                drDDL["SIP_NO"] = dtSIPList.Rows[loop]["SIP_NO"].ToString();
                dtSIPListDDL.Rows.Add(drDDL);
            }
        }
        return dtSIPListDDL;
    }
    public DataTable dtSIPListDDLForStatement(string filter)
    {
        DataTable dtSIPList = commonGatewayObj.Select("SELECT ID, SIP_NO FROM SIP_MASTER WHERE VALID IS NULL AND SIP_STATUS IS NULL " + filter.ToString() + " ORDER BY SIP_NO DESC ");
        DataTable dtSIPListDDL = new DataTable();
        if (dtSIPList.Rows.Count > 0)
        {

            dtSIPListDDL.Columns.Add("ID", typeof(string));
            dtSIPListDDL.Columns.Add("SIP_NO", typeof(string));
            DataRow drDDL;
            drDDL = dtSIPListDDL.NewRow();
            drDDL["ID"] = "0";
            drDDL["SIP_NO"] = " Select SIP ";
            dtSIPListDDL.Rows.Add(drDDL);

            for (int loop = 0; loop < dtSIPList.Rows.Count; loop++)
            {
                drDDL = dtSIPListDDL.NewRow();
                drDDL["ID"] = dtSIPList.Rows[loop]["ID"].ToString();
                drDDL["SIP_NO"] = dtSIPList.Rows[loop]["SIP_NO"].ToString();
                dtSIPListDDL.Rows.Add(drDDL);
            }
        }
        return dtSIPListDDL;
    }
    public DataTable dtSIPMasterList(string filter)
    {
        DataTable dtSIPMasterList = new DataTable();
        StringBuilder querySQL = new StringBuilder();
        querySQL.Append(" SELECT FUND_INFO.*, U_MASTER.*, SIP_MASTER.*, BANK_BRANCH.*, BANK_NAME.*");
        querySQL.Append(" FROM BANK_NAME, BANK_BRANCH, FUND_INFO, U_MASTER, SIP_MASTER WHERE BANK_NAME.BANK_CODE = BANK_BRANCH.BANK_CODE AND BANK_BRANCH.ROUTING_NO = SIP_MASTER.SIP_ACC_ROUTING_NO");
        querySQL.Append(" AND FUND_INFO.FUND_CD = U_MASTER.REG_BK AND  U_MASTER.REG_BK = SIP_MASTER.REG_BK AND U_MASTER.REG_BR = SIP_MASTER.REG_BR AND U_MASTER.REG_NO = SIP_MASTER.REG_NO ");
        querySQL.Append(" AND SIP_MASTER.VALID IS NULL AND SIP_MASTER.SIP_STATUS IS NULL" + filter.ToString());
        querySQL.Append(" ORDER BY SIP_MASTER.SIP_NO ");
        dtSIPMasterList = commonGatewayObj.Select(querySQL.ToString());
        return dtSIPMasterList;
    }
    public DataTable dtSIPMasterListRemaining(string filter)
    {
        DataTable dtSIPMasterList = new DataTable();
        StringBuilder querySQL = new StringBuilder();
        querySQL.Append(" SELECT FUND_INFO.*, U_MASTER.*, SIP_MASTER.*, BANK_BRANCH.*, BANK_NAME.*");
        querySQL.Append(" FROM BANK_NAME, BANK_BRANCH, FUND_INFO, U_MASTER, SIP_MASTER WHERE BANK_NAME.BANK_CODE = BANK_BRANCH.BANK_CODE AND BANK_BRANCH.ROUTING_NO = SIP_MASTER.SIP_ACC_ROUTING_NO");
        querySQL.Append(" AND FUND_INFO.FUND_CD = U_MASTER.REG_BK AND  U_MASTER.REG_BK = SIP_MASTER.REG_BK AND U_MASTER.REG_BR = SIP_MASTER.REG_BR AND U_MASTER.REG_NO = SIP_MASTER.REG_NO ");
        querySQL.Append(" AND SIP_MASTER.VALID IS NULL " + filter.ToString());
        querySQL.Append(" ORDER BY SIP_MASTER.SIP_NO ");
        dtSIPMasterList = commonGatewayObj.Select(querySQL.ToString());
        return dtSIPMasterList;
    }
    public DataTable dtSIPDetailsList(string filter)
    {
        DataTable dtSIPMasterList = new DataTable();
        StringBuilder querySQL = new StringBuilder();
        querySQL.Append(" SELECT FUND_INFO.*, U_MASTER.*, SIP_MASTER.*, BANK_BRANCH.*, BANK_NAME.*,SIP_DETAILS.*");
        querySQL.Append(" FROM BANK_NAME, BANK_BRANCH, FUND_INFO, U_MASTER, SIP_MASTER,SIP_DETAILS WHERE BANK_NAME.BANK_CODE = BANK_BRANCH.BANK_CODE AND BANK_BRANCH.ROUTING_NO = SIP_MASTER.SIP_ACC_ROUTING_NO");
        querySQL.Append(" AND FUND_INFO.FUND_CD = U_MASTER.REG_BK AND  U_MASTER.REG_BK = SIP_MASTER.REG_BK AND U_MASTER.REG_BR = SIP_MASTER.REG_BR AND U_MASTER.REG_NO = SIP_MASTER.REG_NO ");
        querySQL.Append(" AND SIP_MASTER.REG_BK=SIP_DETAILS.REG_BK AND SIP_MASTER.REG_BR=SIP_DETAILS.REG_BR AND SIP_MASTER.REG_NO=SIP_DETAILS.REG_NO AND SIP_MASTER.SIP_NO=SIP_DETAILS.SIP_NO ");
        querySQL.Append(" AND SIP_MASTER.VALID IS NULL AND SIP_MASTER.SIP_STATUS IS NULL" + filter.ToString());
        querySQL.Append(" ORDER BY SIP_MASTER.SIP_NO ");
        dtSIPMasterList = commonGatewayObj.Select(querySQL.ToString());
        return dtSIPMasterList;
    }
    public DataTable dtSIPDetailsListTotal(string filter)
    {
        DataTable dtSIPMasterList = new DataTable();
        StringBuilder querySQL = new StringBuilder();
        querySQL.Append(" SELECT COUNT(*) AS TOTAL_HOLDER, SUM(SIP_DETAILS.DEBIT_AMOUNT) AS TOTAL_AMOUNT ");
        querySQL.Append(" FROM BANK_NAME, BANK_BRANCH, FUND_INFO, U_MASTER, SIP_MASTER,SIP_DETAILS WHERE BANK_NAME.BANK_CODE = BANK_BRANCH.BANK_CODE AND BANK_BRANCH.ROUTING_NO = SIP_MASTER.SIP_ACC_ROUTING_NO");
        querySQL.Append(" AND FUND_INFO.FUND_CD = U_MASTER.REG_BK AND  U_MASTER.REG_BK = SIP_MASTER.REG_BK AND U_MASTER.REG_BR = SIP_MASTER.REG_BR AND U_MASTER.REG_NO = SIP_MASTER.REG_NO ");
        querySQL.Append(" AND SIP_MASTER.REG_BK=SIP_DETAILS.REG_BK AND SIP_MASTER.REG_BR=SIP_DETAILS.REG_BR AND SIP_MASTER.REG_NO=SIP_DETAILS.REG_NO AND SIP_MASTER.SIP_NO=SIP_DETAILS.SIP_NO ");
        querySQL.Append(" AND SIP_MASTER.VALID IS NULL AND SIP_MASTER.SIP_STATUS IS NULL " + filter.ToString());        
        dtSIPMasterList = commonGatewayObj.Select(querySQL.ToString());
        return dtSIPMasterList;
    }
    public int getTotalInstallment(DataTable dtSIPDetails)
    {
        int totalInstallment = 0;
        int SIPDuratonInMonth = Convert.ToInt32(dtSIPDetails.Rows[0]["SIP_DURATION_IN_MONTH"]);
        int SIPPayFreq = Convert.ToInt32(dtSIPDetails.Rows[0]["PAYMENT_FREQUENCY"]);
        if (SIPPayFreq == 1)
            totalInstallment = SIPDuratonInMonth;
        else
            totalInstallment = SIPDuratonInMonth / SIPPayFreq;
        return totalInstallment;
    }
    public DataTable dtGetTotalInstallmentList(DataTable dtSIPDetails, string CDSStatus)
    {



        int SIPDuratonInMonth = Convert.ToInt32(dtSIPDetails.Rows[0]["SIP_DURATION_IN_MONTH"]);
        int SIPPayFreq = Convert.ToInt32(dtSIPDetails.Rows[0]["PAYMENT_FREQUENCY"]);
        string debitDate = string.Format("{0:dd-MMM-yyyy}", dtSIPDetails.Rows[0]["SIP_START_DATE"]);

        DataTable dtTotalInstallmentList = new DataTable();
        dtTotalInstallmentList.Columns.Add("REG_BK", typeof(string));
        dtTotalInstallmentList.Columns.Add("REG_BR", typeof(string));
        dtTotalInstallmentList.Columns.Add("REG_NO", typeof(string));

        dtTotalInstallmentList.Columns.Add("HNAME", typeof(string));
        dtTotalInstallmentList.Columns.Add("SIP_NO", typeof(string));
        dtTotalInstallmentList.Columns.Add("SCHEDULE_NO", typeof(string));

        dtTotalInstallmentList.Columns.Add("DEBIT_DATE", typeof(string));
        dtTotalInstallmentList.Columns.Add("AMOUNT", typeof(string));

        if (SIPPayFreq == 1)
        {
            DataRow drDDL;
            int sceduleNo = 0;
            int holyDaycount = 0;
            for (int loop = 0; loop < SIPDuratonInMonth; loop++)
            {
                int months = 0;
                string debitDate1 = "";
                if (loop == 0)
                {
                    debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate));
                    DayOfWeek day =Convert.ToDateTime(debitDate).DayOfWeek;
                    if(day== DayOfWeek.Friday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(2));
                        holyDaycount = 2;
                    }
                    else if(day == DayOfWeek.Saturday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(1));
                        holyDaycount = 1;
                    }

                }
                else
                {
                    debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate).AddMonths(SIPPayFreq));
                    DayOfWeek day = Convert.ToDateTime(debitDate1).DayOfWeek;
                    if (day == DayOfWeek.Friday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(2));
                        holyDaycount = 2;
                    }
                    else if (day == DayOfWeek.Saturday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(1));
                        holyDaycount = 1;
                    }
                }
                months = Convert.ToDateTime(debitDate1).Month;

                drDDL = dtTotalInstallmentList.NewRow();
                drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                drDDL["SCHEDULE_NO"] = sceduleNo + 1;
                drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                drDDL["DEBIT_DATE"] = debitDate1;
                dtTotalInstallmentList.Rows.Add(drDDL);
                sceduleNo++;

                debitDate = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(-holyDaycount));
                holyDaycount = 0;

                //if (CDSStatus == "N")
                //{
                //    if (months != 7)
                //    {
                //        drDDL = dtTotalInstallmentList.NewRow();
                //        drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                //        drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                //        drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                //        drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                //        drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                //        drDDL["SCHEDULE_NO"] = sceduleNo + 1;
                //        drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                //        drDDL["DEBIT_DATE"] = debitDate1;
                //        dtTotalInstallmentList.Rows.Add(drDDL);
                //        sceduleNo++;
                //    }

                //}
                //else
                //{
                //    if (months != 1)
                //    {
                //        drDDL = dtTotalInstallmentList.NewRow();
                //        drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                //        drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                //        drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                //        drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                //        drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                //        drDDL["SCHEDULE_NO"] = sceduleNo + 1;
                //        drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                //        drDDL["DEBIT_DATE"] = debitDate1;
                //        dtTotalInstallmentList.Rows.Add(drDDL);
                //        sceduleNo++;
                //    }
                //}


            }
        }
        else
        {
            DataRow drDDL;
            int sceduleNo = 0;
            int holyDaycount = 0;
            for (int loop = 0; loop < SIPDuratonInMonth/ SIPPayFreq; loop++)
            {
                int months = 0;
                string debitDate1 = "";
                if (loop == 0)
                {
                    debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate));
                    DayOfWeek day = Convert.ToDateTime(debitDate).DayOfWeek;
                    if (day == DayOfWeek.Friday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(2));
                        holyDaycount = 2;
                    }
                    else if (day == DayOfWeek.Saturday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(1));
                        holyDaycount = 1;
                    }

                }
                else
                {
                    debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate).AddMonths(SIPPayFreq));
                    DayOfWeek day = Convert.ToDateTime(debitDate1).DayOfWeek;
                    if (day == DayOfWeek.Friday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(2));
                        holyDaycount = 2;
                    }
                    else if (day == DayOfWeek.Saturday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(1));
                        holyDaycount = 1;
                    }
                }
                months = Convert.ToDateTime(debitDate1).Month;
               
                drDDL = dtTotalInstallmentList.NewRow();
                drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                drDDL["SCHEDULE_NO"] = sceduleNo + 1;
                drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                drDDL["DEBIT_DATE"] = debitDate1;
                dtTotalInstallmentList.Rows.Add(drDDL);
                sceduleNo++;
                debitDate = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(-holyDaycount));
                holyDaycount = 0;


                //if (CDSStatus == "N")
                //{
                //    if (months == 7)
                //    {
                //        drDDL = dtTotalInstallmentList.NewRow();
                //        drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                //        drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                //        drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                //        drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                //        drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                //        drDDL["SCHEDULE_NO"] = sceduleNo + 1;
                //        drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                //        drDDL["DEBIT_DATE"] = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddMonths(1));
                //        dtTotalInstallmentList.Rows.Add(drDDL);
                //        sceduleNo++;
                //    }
                //    else
                //    {
                //        drDDL = dtTotalInstallmentList.NewRow();
                //        drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                //        drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                //        drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                //        drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                //        drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                //        drDDL["SCHEDULE_NO"] = sceduleNo + 1;
                //        drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                //        drDDL["DEBIT_DATE"] = debitDate1;
                //        dtTotalInstallmentList.Rows.Add(drDDL);
                //        sceduleNo++;
                //    }
                //}
                //else
                //{
                //    if (months == 1)
                //    {
                //        drDDL = dtTotalInstallmentList.NewRow();
                //        drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                //        drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                //        drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                //        drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                //        drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                //        drDDL["SCHEDULE_NO"] = sceduleNo + 1;
                //        drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                //        drDDL["DEBIT_DATE"] = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddMonths(1));
                //        dtTotalInstallmentList.Rows.Add(drDDL);
                //        sceduleNo++;
                //    }
                //    else
                //    {
                //        drDDL = dtTotalInstallmentList.NewRow();
                //        drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                //        drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                //        drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                //        drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                //        drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                //        drDDL["SCHEDULE_NO"] = sceduleNo + 1;
                //        drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                //        drDDL["DEBIT_DATE"] = debitDate1;
                //        dtTotalInstallmentList.Rows.Add(drDDL);
                //        sceduleNo++;
                //    }
                //}
            }

        }


        return dtTotalInstallmentList;
    }
    public DataTable dtGetTotalInstallmentListRemaining(DataTable dtSIPDetails, string debit_date_start, string debit_date_end, int remaingMonths, int nextScheduleNo)
    {


        int SIPDuratonInMonth = remaingMonths;
        int SIPPayFreq = Convert.ToInt32(dtSIPDetails.Rows[0]["PAYMENT_FREQUENCY"]);
        string debitDate = string.Format("{0:dd-MMM-yyyy}", debit_date_start);

        DataTable dtTotalInstallmentList = new DataTable();
        dtTotalInstallmentList.Columns.Add("REG_BK", typeof(string));
        dtTotalInstallmentList.Columns.Add("REG_BR", typeof(string));
        dtTotalInstallmentList.Columns.Add("REG_NO", typeof(string));

        dtTotalInstallmentList.Columns.Add("HNAME", typeof(string));
        dtTotalInstallmentList.Columns.Add("SIP_NO", typeof(string));
        dtTotalInstallmentList.Columns.Add("SCHEDULE_NO", typeof(string));

        dtTotalInstallmentList.Columns.Add("DEBIT_DATE", typeof(string));
        dtTotalInstallmentList.Columns.Add("AMOUNT", typeof(string));

        if (SIPPayFreq == 1)
        {
            DataRow drDDL;
            int sceduleNo = nextScheduleNo;
            int holyDaycount = 0;
            for (int loop = 0; loop < SIPDuratonInMonth; loop++)
            {
                int months = 0;
                string debitDate1 = "";
                if (loop == 0)
                {
                    debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate));
                    DayOfWeek day = Convert.ToDateTime(debitDate).DayOfWeek;
                    if (day == DayOfWeek.Friday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(2));
                        holyDaycount = 2;
                    }
                    else if (day == DayOfWeek.Saturday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(1));
                        holyDaycount = 1;
                    }

                }
                else
                {
                    debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate).AddMonths(SIPPayFreq));
                    DayOfWeek day = Convert.ToDateTime(debitDate1).DayOfWeek;
                    if (day == DayOfWeek.Friday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(2));
                        holyDaycount = 2;
                    }
                    else if (day == DayOfWeek.Saturday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(1));
                        holyDaycount = 1;
                    }
                }

                drDDL = dtTotalInstallmentList.NewRow();
                drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                drDDL["SCHEDULE_NO"] = sceduleNo;
                drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                drDDL["DEBIT_DATE"] = debitDate1;
                dtTotalInstallmentList.Rows.Add(drDDL);
                sceduleNo++;
                months = Convert.ToDateTime(debitDate1).Month;


                //if (CDSStatus == "N")
                //{
                //    if (months != 7)
                //    {
                //        drDDL = dtTotalInstallmentList.NewRow();
                //        drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                //        drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                //        drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                //        drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                //        drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                //        drDDL["SCHEDULE_NO"] = sceduleNo + 1;
                //        drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                //        drDDL["DEBIT_DATE"] = debitDate1;
                //        dtTotalInstallmentList.Rows.Add(drDDL);
                //        sceduleNo++;
                //    }

                //}
                //else
                //{
                //    if (months != 1)
                //    {
                //        drDDL = dtTotalInstallmentList.NewRow();
                //        drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                //        drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                //        drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                //        drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                //        drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                //        drDDL["SCHEDULE_NO"] = sceduleNo + 1;
                //        drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                //        drDDL["DEBIT_DATE"] = debitDate1;
                //        dtTotalInstallmentList.Rows.Add(drDDL);
                //        sceduleNo++;
                //    }
                //}

              

                debitDate = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(-holyDaycount));
                holyDaycount = 0;
                if (debitDate1 == debit_date_end)
                    break;
            }
        }
        else
        {
            DataRow drDDL;
            int sceduleNo = nextScheduleNo;
            int holyDaycount = 0;
            for (int loop = 0; loop < SIPDuratonInMonth / SIPPayFreq; loop++)
            {
                int months = 0;
                string debitDate1 = "";
                if (loop == 0)
                {
                    debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate));
                    DayOfWeek day = Convert.ToDateTime(debitDate).DayOfWeek;
                    if (day == DayOfWeek.Friday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(2));
                        holyDaycount = 2;
                    }
                    else if (day == DayOfWeek.Saturday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(1));
                        holyDaycount = 1;
                    }

                }
                else
                {
                    debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate).AddMonths(SIPPayFreq));
                    DayOfWeek day = Convert.ToDateTime(debitDate1).DayOfWeek;
                    if (day == DayOfWeek.Friday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(2));
                        holyDaycount = 2;
                    }
                    else if (day == DayOfWeek.Saturday)
                    {
                        debitDate1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(1));
                        holyDaycount = 1;
                    }
                }

                drDDL = dtTotalInstallmentList.NewRow();
                drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                drDDL["SCHEDULE_NO"] = sceduleNo ;
                drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                drDDL["DEBIT_DATE"] = debitDate1;
                dtTotalInstallmentList.Rows.Add(drDDL);
                sceduleNo++;

                months = Convert.ToDateTime(debitDate1).Month;
                //if (CDSStatus == "N")
                //{
                //    if (months == 7)
                //    {
                //        drDDL = dtTotalInstallmentList.NewRow();
                //        drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                //        drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                //        drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                //        drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                //        drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                //        drDDL["SCHEDULE_NO"] = sceduleNo + 1;
                //        drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                //        drDDL["DEBIT_DATE"] = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddMonths(1));
                //        dtTotalInstallmentList.Rows.Add(drDDL);
                //        sceduleNo++;
                //    }
                //    else
                //    {
                //        drDDL = dtTotalInstallmentList.NewRow();
                //        drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                //        drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                //        drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                //        drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                //        drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                //        drDDL["SCHEDULE_NO"] = sceduleNo + 1;
                //        drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                //        drDDL["DEBIT_DATE"] = debitDate1;
                //        dtTotalInstallmentList.Rows.Add(drDDL);
                //        sceduleNo++;
                //    }
                //}
                //else
                //{
                //    if (months == 1)
                //    {
                //        drDDL = dtTotalInstallmentList.NewRow();
                //        drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                //        drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                //        drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                //        drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                //        drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                //        drDDL["SCHEDULE_NO"] = sceduleNo + 1;
                //        drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                //        drDDL["DEBIT_DATE"] = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddMonths(1));
                //        dtTotalInstallmentList.Rows.Add(drDDL);
                //        sceduleNo++;
                //    }
                //    else
                //    {
                //        drDDL = dtTotalInstallmentList.NewRow();
                //        drDDL["REG_BK"] = dtSIPDetails.Rows[0]["REG_BK"].ToString();
                //        drDDL["REG_BR"] = dtSIPDetails.Rows[0]["REG_BR"].ToString();
                //        drDDL["REG_NO"] = dtSIPDetails.Rows[0]["REG_NO"].ToString();
                //        drDDL["HNAME"] = dtSIPDetails.Rows[0]["HNAME"].ToString();
                //        drDDL["SIP_NO"] = dtSIPDetails.Rows[0]["SIP_NO"].ToString();
                //        drDDL["SCHEDULE_NO"] = sceduleNo + 1;
                //        drDDL["AMOUNT"] = dtSIPDetails.Rows[0]["AMOUNT"].ToString();
                //        drDDL["DEBIT_DATE"] = debitDate1;
                //        dtTotalInstallmentList.Rows.Add(drDDL);
                //        sceduleNo++;
                //    }
                //}
              
                debitDate = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(debitDate1).AddDays(-holyDaycount));
                holyDaycount = 0;
                if (debitDate1 == debit_date_end)
                    break;
            }

        }


        return dtTotalInstallmentList;
    }
    public void SaveSIPSchedule(DataTable dtSIPDetails, UnitUser userObj)
    {
        Hashtable htRegSIPSchedule = new Hashtable();
        try
        {
            commonGatewayObj.BeginTransaction();

            for (int loop = 0; loop < dtSIPDetails.Rows.Count; loop++)
            {
                htRegSIPSchedule = new Hashtable();
                htRegSIPSchedule.Add("ID", commonGatewayObj.GetMaxNo("SIP_DETAILS", "ID") + 1);
                htRegSIPSchedule.Add("REG_BK", dtSIPDetails.Rows[loop]["REG_BK"]);
                htRegSIPSchedule.Add("REG_BR", dtSIPDetails.Rows[loop]["REG_BR"]);
                htRegSIPSchedule.Add("REG_NO", dtSIPDetails.Rows[loop]["REG_NO"]);
                htRegSIPSchedule.Add("SIP_NO", dtSIPDetails.Rows[loop]["SIP_NO"]);
                htRegSIPSchedule.Add("SCHEDULE_NO", dtSIPDetails.Rows[loop]["SCHEDULE_NO"]);
                htRegSIPSchedule.Add("DEBIT_DATE", dtSIPDetails.Rows[loop]["DEBIT_DATE"]);
                htRegSIPSchedule.Add("DEBIT_AMOUNT", dtSIPDetails.Rows[loop]["AMOUNT"]);
                commonGatewayObj.Insert(htRegSIPSchedule, "SIP_DETAILS");//save SIP Schedule info
            }

            commonGatewayObj.ExecuteNonQuery("UPDATE SIP_MASTER SET SCHEDULE_CREATE_BY='" + userObj.UserID.ToString() + "', SCHEDULE_CREATE_DATE='" + DateTime.Today.ToString("dd-MMM-yyyy") + "' WHERE REG_BK='" + dtSIPDetails.Rows[0]["REG_BK"] + "' AND REG_BR='" + dtSIPDetails.Rows[0]["REG_BR"] + "' AND REG_NO=" + dtSIPDetails.Rows[0]["REG_NO"] + " AND VALID IS NULL AND SIP_STATUS IS NULL AND SIP_NO=" + dtSIPDetails.Rows[0]["SIP_NO"]);
            commonGatewayObj.CommitTransaction();

        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            throw ex;
        }
    }
    public void SaveSIPScheduleRemaining(DataTable dtSIPDetails, UnitUser userObj)
    {
        Hashtable htRegSIPSchedule = new Hashtable();
        try
        {
            commonGatewayObj.BeginTransaction();

            for (int loop = 0; loop < dtSIPDetails.Rows.Count; loop++)
            {
                htRegSIPSchedule = new Hashtable();
                htRegSIPSchedule.Add("ID", commonGatewayObj.GetMaxNo("SIP_DETAILS", "ID") + 1);
                htRegSIPSchedule.Add("REG_BK", dtSIPDetails.Rows[loop]["REG_BK"]);
                htRegSIPSchedule.Add("REG_BR", dtSIPDetails.Rows[loop]["REG_BR"]);
                htRegSIPSchedule.Add("REG_NO", dtSIPDetails.Rows[loop]["REG_NO"]);
                htRegSIPSchedule.Add("SIP_NO", dtSIPDetails.Rows[loop]["SIP_NO"]);
                htRegSIPSchedule.Add("SCHEDULE_NO", dtSIPDetails.Rows[loop]["SCHEDULE_NO"]);
                htRegSIPSchedule.Add("DEBIT_DATE", dtSIPDetails.Rows[loop]["DEBIT_DATE"]);
                htRegSIPSchedule.Add("DEBIT_AMOUNT", dtSIPDetails.Rows[loop]["AMOUNT"]);
                commonGatewayObj.Insert(htRegSIPSchedule, "SIP_DETAILS");//save SIP Schedule info
            }

          //  commonGatewayObj.ExecuteNonQuery("UPDATE SIP_MASTER SET SCHEDULE_CREATE_BY='" + userObj.UserID.ToString() + "', SCHEDULE_CREATE_DATE='" + DateTime.Today.ToString("dd-MMM-yyyy") + "' WHERE REG_BK='" + dtSIPDetails.Rows[0]["REG_BK"] + "' AND REG_BR='" + dtSIPDetails.Rows[0]["REG_BR"] + "' AND REG_NO=" + dtSIPDetails.Rows[0]["REG_NO"] + " AND VALID IS NULL AND SIP_STATUS IS NULL AND SIP_NO=" + dtSIPDetails.Rows[0]["SIP_NO"]);
            commonGatewayObj.CommitTransaction();

        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            throw ex;
        }
    }
    public DataTable dtSIPDayListDDLforEFTPull( string fund_code)
    {
        string querySQL = "SELECT DISTINCT DEBIT_DATE FROM SIP_DETAILS WHERE EFT_PULL_REQUEST_DATE IS NULL AND EFT_PULL_REQUEST_BY  IS NULL AND SIP_DETAILS.REG_BK='"+ fund_code + "'";
        querySQL = querySQL + "  AND TO_DATE(TO_CHAR(SYSDATE, 'MON-YYYY'), 'MON-YYYY')>=TO_DATE(TO_CHAR(DEBIT_DATE, 'MON-YYYY'), 'MON-YYYY')  ORDER BY  1";

        DataTable dtSIPList = commonGatewayObj.Select(querySQL.ToString());
        DataTable dtSIPListDDL = new DataTable();
        if (dtSIPList.Rows.Count > 0)
        {

            dtSIPListDDL.Columns.Add("ID", typeof(string));
            dtSIPListDDL.Columns.Add("DEBIT_DATE", typeof(string));
            DataRow drDDL;
            drDDL = dtSIPListDDL.NewRow();
            drDDL["ID"] = "0";
            drDDL["DEBIT_DATE"] = " Select SIP ";
            dtSIPListDDL.Rows.Add(drDDL);

            for (int loop = 0; loop < dtSIPList.Rows.Count; loop++)
            {
                drDDL = dtSIPListDDL.NewRow();
                drDDL["ID"] = string.Format("{0:dd-MMM-yyyy}", dtSIPList.Rows[loop]["DEBIT_DATE"]).ToString();
                drDDL["DEBIT_DATE"] = string.Format("{0:dd-MMM-yyyy}", dtSIPList.Rows[loop]["DEBIT_DATE"]).ToString();
                dtSIPListDDL.Rows.Add(drDDL);
            }
        }
        return dtSIPListDDL;
    }
    public DataTable dtSIPDayListDDL(string filter)
    {
        string querySQL = "SELECT DISTINCT DEBIT_DATE FROM SIP_DETAILS WHERE 1=1";
        querySQL = querySQL + filter + " ORDER BY  1";

        DataTable dtSIPList = commonGatewayObj.Select(querySQL.ToString());
        DataTable dtSIPListDDL = new DataTable();
        if (dtSIPList.Rows.Count > 0)
        {

            dtSIPListDDL.Columns.Add("ID", typeof(string));
            dtSIPListDDL.Columns.Add("DEBIT_DATE", typeof(string));
            DataRow drDDL;
            drDDL = dtSIPListDDL.NewRow();
            drDDL["ID"] = "0";
            drDDL["DEBIT_DATE"] = " Select SIP ";
            dtSIPListDDL.Rows.Add(drDDL);

            for (int loop = 0; loop < dtSIPList.Rows.Count; loop++)
            {
                drDDL = dtSIPListDDL.NewRow();
                drDDL["ID"] = string.Format("{0:dd-MMM-yyyy}", dtSIPList.Rows[loop]["DEBIT_DATE"]).ToString();
                drDDL["DEBIT_DATE"] = string.Format("{0:dd-MMM-yyyy}", dtSIPList.Rows[loop]["DEBIT_DATE"]).ToString();
                dtSIPListDDL.Rows.Add(drDDL);
            }
        }
        return dtSIPListDDL;
    }
    public void SaveSIPEFTPullRequest(DataTable dtSIPDetails, UnitUser userObj)
    {
        Hashtable htUpdateSIPSchedule = new Hashtable();
        try
        {
            commonGatewayObj.BeginTransaction();

            for (int loop = 0; loop < dtSIPDetails.Rows.Count; loop++)
            {
                htUpdateSIPSchedule = new Hashtable();               
                htUpdateSIPSchedule.Add("DEBIT_ACC_NO", dtSIPDetails.Rows[loop]["SIP_ACC_NO"]);
                htUpdateSIPSchedule.Add("DEBIT_ACC_ROUTING_NO", dtSIPDetails.Rows[loop]["SIP_ACC_ROUTING_NO"]);
                htUpdateSIPSchedule.Add("FUND_ACC_NO", dtSIPDetails.Rows[loop]["SIP_FUND_ACC_NO"]);
                htUpdateSIPSchedule.Add("FUND_ACC_ROUTING_NO", dtSIPDetails.Rows[loop]["SIP_FUND_ACC_ROUTING_NO"]);
                htUpdateSIPSchedule.Add("EFT_PULL_REQUEST_DATE", DateTime.Today.ToString("dd-MMM-yyyy"));
                htUpdateSIPSchedule.Add("EFT_PULL_REQUEST_BY", userObj.UserID.ToString());

                commonGatewayObj.Update(htUpdateSIPSchedule, "SIP_DETAILS", "REG_BK = '" + dtSIPDetails.Rows[loop]["REG_BK"] + "' AND REG_BR = '" + dtSIPDetails.Rows[loop]["REG_BR"] + "' AND REG_NO = " + dtSIPDetails.Rows[loop]["REG_NO"] + " AND SIP_NO = " + dtSIPDetails.Rows[loop]["SIP_NO"] + " AND SCHEDULE_NO=" + dtSIPDetails.Rows[loop]["SCHEDULE_NO"]);
            }

         
            commonGatewayObj.CommitTransaction();

        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            throw ex;
        }
    }
    public DataTable dtSIPPullSeqListDDL()
    {
        string querySQL = "SELECT DISTINCT EFT_PULL_SEQ_NO FROM SIP_DETAILS WHERE EFT_PULL_REQUEST_DATE IS NOT NULL AND EFT_PULL_REQUEST_BY  IS NOT  NULL";
        querySQL = querySQL + " AND FILE_DOWNLOAD_BY IS NULL AND FILE_DOWNLOAD_DT  IS NULL ORDER BY  1";

        DataTable dtSIPList = commonGatewayObj.Select(querySQL.ToString());
        DataTable dtSIPListDDL = new DataTable();
        if (dtSIPList.Rows.Count > 0)
        {

            dtSIPListDDL.Columns.Add("ID", typeof(string));
            dtSIPListDDL.Columns.Add("EFT_PULL_SEQ_NO", typeof(string));
            DataRow drDDL;
            drDDL = dtSIPListDDL.NewRow();
            drDDL["ID"] = "0";
            drDDL["EFT_PULL_SEQ_NO"] = " Select SIP ";
            dtSIPListDDL.Rows.Add(drDDL);

            for (int loop = 0; loop < dtSIPList.Rows.Count; loop++)
            {
                drDDL = dtSIPListDDL.NewRow();
                drDDL["ID"] = dtSIPList.Rows[loop]["EFT_PULL_SEQ_NO"];
                drDDL["EFT_PULL_SEQ_NO"] = dtSIPList.Rows[loop]["EFT_PULL_SEQ_NO"];
                dtSIPListDDL.Rows.Add(drDDL);
            }
        }
        return dtSIPListDDL;
    }
    public long getNextSIPSaleNo(UnitHolderRegistration regObj)
    {
        int nextSIPSaleNo = 0;
        DataTable dtSIPMasterInfo = commonGatewayObj.Select("SELECT NVL(MAX(SL_NO),0)+1 AS SL_NO FROM SALE  WHERE  REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "'");
        if (dtSIPMasterInfo.Rows.Count > 0)
        {
            nextSIPSaleNo = Convert.ToInt32(dtSIPMasterInfo.Rows[0]["SL_NO"].ToString());
        }
        return nextSIPSaleNo;

    }
    public long getNextSIPSaleCertNo(UnitHolderRegistration regObj)
    {
        int nextSIPSaleCertNo = 0;
        DataTable dtSIPMasterInfo = commonGatewayObj.Select("SELECT NVL(MAX(CERT_NO),0)+1 AS CERT_NO FROM SALE_CERT  WHERE  CERT_TYPE='Z' AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' ");
        if (dtSIPMasterInfo.Rows.Count > 0)
        {
            nextSIPSaleCertNo = Convert.ToInt32(dtSIPMasterInfo.Rows[0]["CERT_NO"].ToString());
        }
        return nextSIPSaleCertNo;
    }
    public bool isSIPHolder(UnitHolderRegistration regObj)
    {
        bool isSIP = false;
        DataTable dtSIPInfo = commonGatewayObj.Select("SELECT * FROM SIP_MASTER WHERE VALID IS NULL AND REG_BK='" + regObj.FundCode.ToString().ToUpper() + "' AND REG_BR='" + regObj.BranchCode.ToString().ToUpper() + "' AND REG_NO=" + regObj.RegNumber );
        if (dtSIPInfo.Rows.Count > 0)
            isSIP = true;
        return isSIP;
    }

}
    
   