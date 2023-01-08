using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text;
using System.IO;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class ReportViewer_UnitReportDividendStatementReportViewer : System.Web.UI.Page
{

    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    dividendDAO diviDAOObj = new dividendDAO();
    NumberToEnglish NumToEngObj = new NumberToEnglish();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
    

    protected void Page_Load(object sender, EventArgs e)
    {
      

       if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        string statementType = (string)Session["statementType"];
        string investorType = (string)Session["investorType"];
        string investmentType = (string)Session["investmentType"];
                      
       
        DataTable dtDividend = (DataTable)Session["dtStatement"];

        if (dtDividend.Rows.Count > 0)
        {
            DataTable dtReport = diviDAOObj.dtGetDataTableforDividend();

            DataRow drReport;
            for (int loop = 0; loop < dtDividend.Rows.Count; loop++)
            {
                drReport = dtReport.NewRow();
                drReport["FUND_CD"] = dtDividend.Rows[loop]["FUND_CD"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["FUND_CD"].ToString();
                drReport["FUND_NM"] = dtDividend.Rows[loop]["FUND_NM"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["FUND_NM"].ToString();
                drReport["REG_BR"] = dtDividend.Rows[loop]["REG_BR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["REG_BR"].ToString();
                drReport["REG_BR_NAME"] = dtDividend.Rows[loop]["REG_BR"].Equals(DBNull.Value) ? "" : opendMFDAO.GetBranchName(dtDividend.Rows[loop]["REG_BR"].ToString());
                drReport["DIVI_NO"] = Convert.ToInt32(dtDividend.Rows[loop]["DIVI_NO"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["DIVI_NO"].ToString());
                drReport["F_YEAR"] = dtDividend.Rows[loop]["F_YEAR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["F_YEAR"].ToString();
                drReport["CLOSE_DT"] = dtDividend.Rows[loop]["CLOSE_DT"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["CLOSE_DT"].ToString();
                drReport["TIN"] = dtDividend.Rows[loop]["TIN"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["TIN"].ToString();
              
                    drReport["WAR_TYPE"] = dtDividend.Rows[loop]["WAR_TYPE"];
                    drReport["BO_FOLIO"] = dtDividend.Rows[loop]["BO_FOLIO"];
                

                drReport["DIVI_RATE"] = Convert.ToDecimal(dtDividend.Rows[loop]["DIVI_RATE"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["DIVI_RATE"].ToString());
                drReport["BK_AC_NO"] = dtDividend.Rows[loop]["BK_AC_NO"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["BK_AC_NO"].ToString();
                drReport["BK_AC_NO_MICR"] = dtDividend.Rows[loop]["BK_AC_NO_MICR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["BK_AC_NO_MICR"].ToString();
                drReport["BK_NAME"] = dtDividend.Rows[loop]["BK_NAME"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["BK_NAME"].ToString();
                drReport["BK_ADDRS1"] = dtDividend.Rows[loop]["BK_ADDRS1"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["BK_ADDRS1"].ToString();

                drReport["BK_ADDRS2"] = dtDividend.Rows[loop]["BK_ADDRS2"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["BK_ADDRS2"].ToString();
                drReport["BK_ROUTING_NO"] = dtDividend.Rows[loop]["BK_ROUTING_NO"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["BK_ROUTING_NO"].ToString();
                drReport["BK_ROUTING_NO_MICR"] = dtDividend.Rows[loop]["BK_ROUTING_NO_MICR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["BK_ROUTING_NO_MICR"].ToString();
                drReport["BK_TRANSACTION_CODE"] = Convert.ToInt16(dtDividend.Rows[loop]["BK_TRANSACTION_CODE"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["BK_TRANSACTION_CODE"].ToString());
                drReport["ISS_DT"] = dtDividend.Rows[loop]["ISS_DT"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["ISS_DT"].ToString();
                drReport["REG_NO"] = dtDividend.Rows[loop]["REG_NO"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["REG_NO"].ToString();
                drReport["HNAME"] = dtDividend.Rows[loop]["HNAME"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["HNAME"].ToString();
                drReport["JNT_NAME"] = dtDividend.Rows[loop]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["JNT_NAME"].ToString();

                drReport["ADDRS1"] = dtDividend.Rows[loop]["ADDRS1"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["ADDRS1"].ToString();
                drReport["ADDRS2"] = dtDividend.Rows[loop]["ADDRS2"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["ADDRS2"].ToString();
                drReport["CITY"] = dtDividend.Rows[loop]["CITY"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["CITY"].ToString();
                drReport["WAR_NO"] = dtDividend.Rows[loop]["WAR_NO"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["WAR_NO"].ToString();
                drReport["WAR_NO_MICR"] = dtDividend.Rows[loop]["WAR_NO_MICR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["WAR_NO_MICR"].ToString();

                drReport["NO_OF_UNITS"] = Convert.ToUInt32(dtDividend.Rows[loop]["NO_OF_UNITS"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["NO_OF_UNITS"].ToString());
                drReport["TOT_DIVI"] = Convert.ToDecimal(dtDividend.Rows[loop]["TOT_DIVI"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["TOT_DIVI"].ToString());
                drReport["TAX_DIDUCT"] = Convert.ToDecimal(dtDividend.Rows[loop]["TAX_DIDUCT"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["TAX_DIDUCT"].ToString());
                drReport["FI_DIVI_QTY"] = Convert.ToDecimal(dtDividend.Rows[loop]["FI_DIVI_QTY"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["FI_DIVI_QTY"].ToString());
                drReport["FI_DIVI_QTY_INWORD"] = NumToEngObj.changeNumericToWords(Convert.ToDecimal(dtDividend.Rows[loop]["FI_DIVI_QTY"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["FI_DIVI_QTY"].ToString())) + "Only";

                drReport["CIP_QTY"] = Convert.ToInt32(dtDividend.Rows[loop]["CIP_QTY"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["CIP_QTY"].ToString());
                drReport["CIP_RATE"] = Convert.ToDecimal(dtDividend.Rows[loop]["CIP_RATE"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["CIP_RATE"].ToString());
                drReport["CIP"] = dtDividend.Rows[loop]["CIP"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["CIP"].ToString();
                drReport["AGM_DT"] = dtDividend.Rows[loop]["AGM_DT"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["AGM_DT"].ToString();
                drReport["TAX_RT_INDIVIDUAL"] = Convert.ToDecimal(dtDividend.Rows[loop]["TAX_RT_INDIVIDUAL"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["TAX_RT_INDIVIDUAL"].ToString());
                drReport["TAX_RT_INSTITUTION"] = Convert.ToDecimal(dtDividend.Rows[loop]["TAX_RT_INSTITUTION"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["TAX_RT_INSTITUTION"].ToString());

                drReport["REG_TYPE"] = dtDividend.Rows[loop]["REG_TYPE"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["REG_TYPE"].ToString();
                drReport["FY_PART"] = dtDividend.Rows[loop]["FY_PART"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["FY_PART"].ToString();
                drReport["NET_DIVI"] = Convert.ToDecimal(dtDividend.Rows[loop]["NET_DIVI"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["NET_DIVI"].ToString());
                drReport["FRAC_DIVI"] = Convert.ToDecimal(dtDividend.Rows[loop]["FRAC_DIVI"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["FRAC_DIVI"].ToString());
                drReport["REG_NUM"] = Convert.ToInt32(dtDividend.Rows[loop]["REG_NUM"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["REG_NUM"].ToString());
                drReport["REG_BR"] = dtDividend.Rows[loop]["REG_BR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["REG_BR"].ToString();

                if (dtDividend.Rows[loop]["ID_FLAG"].Equals("Y") && !dtDividend.Rows[loop]["ID_AC"].Equals(DBNull.Value) && !dtDividend.Rows[loop]["ID_BK_NM_CD"].Equals(DBNull.Value) && !dtDividend.Rows[loop]["ID_BK_BR_NM_CD"].Equals(DBNull.Value))
                {
                    drReport["ID_AC"] = dtDividend.Rows[loop]["ID_AC"].ToString();
                    drReport["ID_BK_NM"] = reportObj.getBankNameByBankCode(Convert.ToInt16(dtDividend.Rows[loop]["ID_BK_NM_CD"].ToString())).ToString();
                    drReport["ID_BK_BR_NM"] = reportObj.getBankBranchNameByCode(Convert.ToInt16(dtDividend.Rows[loop]["ID_BK_NM_CD"].ToString()), Convert.ToInt16(dtDividend.Rows[loop]["ID_BK_BR_NM_CD"].ToString())).ToString();
                }
                
                drReport["HOLDER_BK_ACC_NO"] = dtDividend.Rows[loop]["HOLDER_BK_ACC_NO"].ToString();
                drReport["HOLDER_BK_NM"] = reportObj.getBankNameByBankCode(Convert.ToInt16(dtDividend.Rows[loop]["HOLDER_BK_NM_CD"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["HOLDER_BK_NM_CD"].ToString())).ToString();
                drReport["HOLDER_BK_BR_NM"] = reportObj.getBankBranchNameByCode(Convert.ToInt16(dtDividend.Rows[loop]["HOLDER_BK_NM_CD"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["HOLDER_BK_NM_CD"].ToString()), Convert.ToInt16(dtDividend.Rows[loop]["HOLDER_BK_NM_CD"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["HOLDER_BK_BR_NM_CD"].ToString())).ToString();
                drReport["HOLDER_BK_BR_ADDRES"] = reportObj.getBankBranchAddressByCode(Convert.ToInt16(dtDividend.Rows[loop]["HOLDER_BK_NM_CD"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["HOLDER_BK_NM_CD"].ToString()), Convert.ToInt16(dtDividend.Rows[loop]["HOLDER_BK_NM_CD"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["HOLDER_BK_BR_NM_CD"].ToString())).ToString();
                if (string.Compare(statementType, "OFFICE_SIGN") == 0 && string.Compare(investmentType, "CIP") == 0)
                {
                    if( Convert.ToInt32(dtDividend.Rows[loop]["CIP_QTY"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["CIP_QTY"].ToString())>0)
                    {
                        int saleNo = Convert.ToInt32(dtDividend.Rows[loop]["CIP_SL_NO"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["CIP_SL_NO"].ToString());
                        drReport["SL_NO"] = saleNo;
                        string queryString = "SELECT  NVL(CERT_TYPE,' ') AS CERT_TYPE, NVL(CERT_NO,0) AS CERT_NO FROM  CIP_SALE_CERT WHERE SL_NO=" + saleNo + " AND REG_BK='" + Convert.ToString(dtDividend.Rows[loop]["FUND_CD"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["FUND_CD"].ToString()) + "' AND REG_BR='" + Convert.ToString(dtDividend.Rows[loop]["REG_BR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["REG_BR"].ToString()) + "' AND REG_NO=" + Convert.ToInt32(dtDividend.Rows[loop]["REG_NUM"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["REG_NUM"].ToString()) + " ORDER BY CERT_TYPE, CERT_NO";
                        drReport["CIP_CERT"] = reportObj.getTotalCertNo(queryString, dtDividend.Rows[loop]["FUND_CD"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["FUND_CD"].ToString());
                    }
                    
                }


                dtReport.Rows.Add(drReport);
            }

            dtReport.TableName = "ReportDividend";
          //dtReport.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\ReportDividend.xsd");
            if (string.Compare(investorType, "NON_ID") == 0 || string.Compare(investorType, "ALLID") == 0)
            {
                if (string.Compare(statementType, "OFFICE_SIGN")== 0 && string.Compare(investmentType, "NON_CIP")== 0)
                {
                    AMCL.REPORT.CR_UnitStatementCashSign CR_Dividend = new AMCL.REPORT.CR_UnitStatementCashSign();
                    CR_Dividend.SetDataSource(dtReport);
                    //CR_Dividend.SetParameterValue("duplicate", duplicate);
                    CrystalReportViewer1.ReportSource = CR_Dividend;
                }
                else if (string.Compare(statementType, "OFFICE_SIGN") == 0 && string.Compare(investmentType, "CIP") == 0)
                {
                    AMCL.REPORT.CR_UnitStatementCIPSign CR_Dividend = new AMCL.REPORT.CR_UnitStatementCIPSign();
                    CR_Dividend.SetDataSource(dtReport);
                    CR_Dividend.SetParameterValue("branchName", opendMFDAO.GetBranchName(dtReport.Rows[0]["REG_BR"].ToString()).ToString());
                    CrystalReportViewer1.ReportSource = CR_Dividend;
                }
                else if (string.Compare(statementType, "BANK") == 0)
                {
                    AMCL.REPORT.CR_UnitStatementBank CR_Dividend = new AMCL.REPORT.CR_UnitStatementBank();
                    CR_Dividend.SetDataSource(dtReport);
                    //CR_Dividend.SetParameterValue("branchName", opendMFDAO.GetBranchName(dtReport.Rows[0]["REG_BR"].ToString()).ToString());
                    CrystalReportViewer1.ReportSource = CR_Dividend;
                }
                else if (string.Compare(statementType, "OFFICE") == 0)
                {
                    AMCL.REPORT.CR_UnitStatementOffice CR_Dividend = new AMCL.REPORT.CR_UnitStatementOffice();
                    CR_Dividend.SetDataSource(dtReport);
                    CR_Dividend.SetParameterValue("branchName", opendMFDAO.GetBranchName(dtReport.Rows[0]["REG_BR"].ToString()).ToString());
                    CrystalReportViewer1.ReportSource = CR_Dividend;
                }
                else
                {
                    Response.Write("Ambiguous Selection or Under Construction");
                }
            }
            else if (string.Compare(statementType, "OFFICE") == 0 && string.Compare(investorType, "ID") == 0)
            {
                AMCL.REPORT.CR_UnitStatementOfficeID CR_Dividend = new AMCL.REPORT.CR_UnitStatementOfficeID();
                CR_Dividend.SetDataSource(dtReport);
                //CR_Dividend.SetParameterValue("branchName", opendMFDAO.GetBranchName(dtReport.Rows[0]["REG_BR"].ToString()).ToString());
                CrystalReportViewer1.ReportSource = CR_Dividend;
            }
            else if (string.Compare(statementType, "BANK") == 0 && string.Compare(investorType, "ID") == 0)
            {
                AMCL.REPORT.CR_UnitStatementBank CR_Dividend = new AMCL.REPORT.CR_UnitStatementBank();
                CR_Dividend.SetDataSource(dtReport);
                //CR_Dividend.SetParameterValue("branchName", opendMFDAO.GetBranchName(dtReport.Rows[0]["REG_BR"].ToString()).ToString());
                CrystalReportViewer1.ReportSource = CR_Dividend;
            }
            else if (string.Compare(statementType, "OFFICE_SIGN") == 0 && string.Compare(investmentType, "CIP") == 0 && string.Compare(investorType, "ID") == 0)
            {
                AMCL.REPORT.CR_UnitStatementCIPSign CR_Dividend = new AMCL.REPORT.CR_UnitStatementCIPSign();
                CR_Dividend.SetDataSource(dtReport);
                CR_Dividend.SetParameterValue("branchName", opendMFDAO.GetBranchName(dtReport.Rows[0]["REG_BR"].ToString()).ToString());
                CrystalReportViewer1.ReportSource = CR_Dividend;
            }
            else 
            {
                Response.Write("Under Construction");
            }

        
        }
        else
        {
            Response.Write("No data found");
        }





    }
}
