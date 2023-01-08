using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMCL.COMMON;
using System.Data;
using AMCL.DL;

/// <summary>
/// Summary description for dividendDAO
/// </summary>
public class JDiviReconDAO
{
    string bizSchema = ConfigReader.SCHEMA;
    CommonGateway commonGatewayObj = new CommonGateway();
    public JDiviReconDAO()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public object dtFundList()
    {
        DataTable dtFundList = commonGatewayObj.Select("SELECT FUND_CD , FUND_NM FROM UNIT.FUND_INFO WHERE FUND_CD IN(SELECT DISTINCT(FUND_CD) FROM UNIT.DIVI_PARA) ORDER BY FUND_CD");
        DataTable dtFundListDropDown = new DataTable();
        dtFundListDropDown.Columns.Add("FUND_CD", typeof(string));
        dtFundListDropDown.Columns.Add("FUND_NM", typeof(string));

        DataRow drFundListDropDown = dtFundListDropDown.NewRow();

        drFundListDropDown["FUND_NM"] = "--Select Fund--- ";
        drFundListDropDown["FUND_CD"] = "0";
        dtFundListDropDown.Rows.Add(drFundListDropDown);
        for (int loop = 0; loop < dtFundList.Rows.Count; loop++)
        {
            drFundListDropDown = dtFundListDropDown.NewRow();
            drFundListDropDown["FUND_NM"] = dtFundList.Rows[loop]["FUND_NM"].ToString();// + ", F_CODE [" + dtFundList.Rows[loop]["F_CD"].ToString()+"]";
            drFundListDropDown["FUND_CD"] = dtFundList.Rows[loop]["FUND_CD"].ToString();
            dtFundListDropDown.Rows.Add(drFundListDropDown);
        }
        return dtFundListDropDown;
    }
    public object dtFY(string f_code)
    {
        DataTable dtList = commonGatewayObj.Select("SELECT  F_YEAR, DIVI_NO FROM UNIT.DIVI_PARA WHERE FUND_CD='" + f_code + "' ORDER BY F_YEAR DESC");
        DataTable dtDropDown = new DataTable();
        dtDropDown.Columns.Add("F_YEAR", typeof(string));
        dtDropDown.Columns.Add("DIVI_NO", typeof(string));

        DataRow drdtDropDown = dtDropDown.NewRow();

        drdtDropDown["F_YEAR"] = "--Select F_YEAR--- ";
        drdtDropDown["DIVI_NO"] = "0";
        dtDropDown.Rows.Add(drdtDropDown);
        for (int loop = 0; loop < dtList.Rows.Count; loop++)
        {
            drdtDropDown = dtDropDown.NewRow();
            drdtDropDown["F_YEAR"] = dtList.Rows[loop]["F_YEAR"].ToString();
            drdtDropDown["DIVI_NO"] = dtList.Rows[loop]["DIVI_NO"].ToString();
            dtDropDown.Rows.Add(drdtDropDown);
        }
        return dtDropDown;
    }
    public DataTable dtGetFundWiseFY(string fundCode)
    {
        DataTable dtFY = dtGetDataTableFY();
        DataRow drFY;
        drFY = dtFY.NewRow();
        drFY["F_YEAR"] = "--Select FY--";
        dtFY.Rows.Add(drFY);

        string SQL = "SELECT DISTINCT F_YEAR, DIVI_NO FROM " + bizSchema + ".DIVI_PARA WHERE FUND_CD='" + fundCode.ToString() + "' ORDER BY F_YEAR DESC";
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
    public DataTable dtGetDataTableFY()
    {
        DataTable dtClosingDate = new DataTable();
        dtClosingDate.Columns.Add("F_YEAR", typeof(string));
        return dtClosingDate;
    }
    public DataTable dtGetDataTableCD()
    {
        DataTable dtClosingDate = new DataTable();
        dtClosingDate.Columns.Add("CLOSE_DT", typeof(string));
        dtClosingDate.Columns.Add("DIVI_NO", typeof(string));
        return dtClosingDate;
    }
    public DataTable dtGetFYWiseClosinDate(string FY, string fundCode)
    {
        DataTable dtCD = dtGetDataTableCD();
        DataRow drCD;
        drCD = dtCD.NewRow();
        drCD["CLOSE_DT"] = "--Select CD--";
        drCD["DIVI_NO"] = "0";
        dtCD.Rows.Add(drCD);

        string SQL = "SELECT DIVI_NO,TO_CHAR(CLOSE_DT,'DD-MON-YYYY') AS CLOSE_DT  FROM " + bizSchema + ".DIVI_PARA WHERE FUND_CD='" + fundCode.ToString() + "' AND F_YEAR='" + FY.ToString() + "' ORDER BY DIVI_NO DESC";
        DataTable dtClosingDate = commonGatewayObj.Select(SQL.ToString());
        if (dtClosingDate.Rows.Count > 0)
        {
            for (int loop = 0; loop < dtClosingDate.Rows.Count; loop++)
            {
                drCD = dtCD.NewRow();
                drCD["CLOSE_DT"] = dtClosingDate.Rows[loop]["CLOSE_DT"];
                drCD["DIVI_NO"] = dtClosingDate.Rows[loop]["DIVI_NO"].ToString();
                dtCD.Rows.Add(drCD);

            }
        }
        return dtCD;
    }
}