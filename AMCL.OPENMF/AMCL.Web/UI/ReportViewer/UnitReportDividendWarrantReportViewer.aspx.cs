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
using AMCL.REPORT;

public partial class ReportViewer_UnitReportDividendWarrantReportViewer : System.Web.UI.Page
{

    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
    dividendDAO diviDAOObj = new dividendDAO();
    NumberToEnglish NumToEngObj = new NumberToEnglish();
    AMCL.REPORT.CR_PenDividendWarrantPrint CRPen_Dividend = new CR_PenDividendWarrantPrint();
    AMCL.REPORT.CR_BDFDividendWarrantPrint CRBDF_Dividend = new CR_BDFDividendWarrantPrint();
    AMCL.REPORT.CR_UnitDividendWarrantPrint CRUnit_Dividend = new CR_UnitDividendWarrantPrint();
   // AMCL.REPORT.CR_BDFInterimDividendWarrantPrint CRBDF_Inetrim_Dividend = new CR_BDFInterimDividendWarrantPrint();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        string fundCode = (string)Session["fundCode"];
        DataTable dtDividend = (DataTable)Session["dtDividend"];
        string duplicate = (string)Session["duplicate"];
        string dividendCategory = (string)Session["dividendCategory"];
        
        if (dtDividend.Rows.Count > 0)
        {
            DataTable dtReport = diviDAOObj.dtGetDataTableforDividend();
            decimal taxDiduct=0;
            DataRow drReport;
            for (int loop = 0; loop < dtDividend.Rows.Count; loop++)
            {
                drReport = dtReport.NewRow();
                drReport["FUND_CD"] = dtDividend.Rows[loop]["FUND_CD"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["FUND_CD"].ToString();
                drReport["FUND_NM"] = dtDividend.Rows[loop]["FUND_NM"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["FUND_NM"].ToString();
                drReport["DIVI_NO"] =Convert.ToInt32( dtDividend.Rows[loop]["DIVI_NO"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["DIVI_NO"].ToString());
                drReport["F_YEAR"] = dtDividend.Rows[loop]["F_YEAR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["F_YEAR"].ToString();
                drReport["CLOSE_DT"] = dtDividend.Rows[loop]["CLOSE_DT"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["CLOSE_DT"].ToString();

                
                drReport["DIVI_RATE"] = Convert.ToDecimal(dtDividend.Rows[loop]["DIVI_RATE"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["DIVI_RATE"].ToString());
                drReport["BK_AC_NO"] = dtDividend.Rows[loop]["BK_AC_NO"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["BK_AC_NO"].ToString();
                drReport["BK_AC_NO_MICR"] = dtDividend.Rows[loop]["BK_AC_NO_MICR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["BK_AC_NO_MICR"].ToString();
                drReport["BK_NAME"] = dtDividend.Rows[loop]["BK_NAME"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["BK_NAME"].ToString();
                drReport["BK_ADDRS1"] = dtDividend.Rows[loop]["BK_ADDRS1"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["BK_ADDRS1"].ToString();

                drReport["BK_ADDRS2"] = dtDividend.Rows[loop]["BK_ADDRS2"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["BK_ADDRS2"].ToString();
                drReport["BK_ROUTING_NO"] = dtDividend.Rows[loop]["BK_ROUTING_NO"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["BK_ROUTING_NO"].ToString();
                drReport["BK_ROUTING_NO_MICR"] = dtDividend.Rows[loop]["BK_ROUTING_NO_MICR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["BK_ROUTING_NO_MICR"].ToString();
                drReport["BK_TRANSACTION_CODE"] =Convert.ToInt16( dtDividend.Rows[loop]["BK_TRANSACTION_CODE"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["BK_TRANSACTION_CODE"].ToString());
                drReport["ISS_DT"] = dtDividend.Rows[loop]["ISS_DT"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["ISS_DT"].ToString();
                drReport["REG_NO"] = dtDividend.Rows[loop]["REG_NO"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["REG_NO"].ToString();
                drReport["HNAME"] = dtDividend.Rows[loop]["HNAME"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["HNAME"].ToString();
                drReport["JNT_NAME"] = dtDividend.Rows[loop]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["JNT_NAME"].ToString();

                drReport["ADDRS1"] = dtDividend.Rows[loop]["ADDRS1"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["ADDRS1"].ToString();
                drReport["ADDRS2"] = dtDividend.Rows[loop]["ADDRS2"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["ADDRS2"].ToString();
                drReport["CITY"] = dtDividend.Rows[loop]["CITY"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["CITY"].ToString();
                drReport["WAR_NO"] = dtDividend.Rows[loop]["WAR_NO"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["WAR_NO"].ToString();
                drReport["WAR_NO_MICR"] = dtDividend.Rows[loop]["WAR_NO_MICR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["WAR_NO_MICR"].ToString();

                if (!(dtDividend.Rows[loop]["H_BK_AC_NO"].Equals(DBNull.Value)) && !(dtDividend.Rows[loop]["H_BK_NM_CD"].Equals(DBNull.Value)) && !(dtDividend.Rows[loop]["H_BK_BR_NM_CD"].Equals(DBNull.Value)))
                {
                    drReport["HOLDER_BK_ACC_NO"]=dtDividend.Rows[loop]["H_BK_AC_NO"].ToString();
                    drReport["HOLDER_BK_NM"] =reportObj.getBankNameByBankCode(Convert.ToInt16( dtDividend.Rows[loop]["H_BK_NM_CD"].ToString()));
                    drReport["HOLDER_BK_BR_NM"] = reportObj.getBankBranchNameByCode(Convert.ToInt16(dtDividend.Rows[loop]["H_BK_NM_CD"].ToString()),Convert.ToInt32( dtDividend.Rows[loop]["H_BK_BR_NM_CD"].ToString()));
                }

                drReport["NO_OF_UNITS"] =  Convert.ToInt32(dtDividend.Rows[loop]["NO_OF_UNITS"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["NO_OF_UNITS"].ToString());
                drReport["TOT_DIVI"] =Convert.ToDecimal( dtDividend.Rows[loop]["TOT_DIVI"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["TOT_DIVI"].ToString());
                drReport["TAX_DIDUCT"] =Convert.ToDecimal( dtDividend.Rows[loop]["TAX_DIDUCT"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["TAX_DIDUCT"].ToString());
               
                drReport["FI_DIVI_QTY"] =Convert.ToDecimal( dtDividend.Rows[loop]["FI_DIVI_QTY"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["FI_DIVI_QTY"].ToString());
                drReport["FI_DIVI_QTY_INWORD"] = NumToEngObj.changeNumericToWords(Convert.ToDecimal( dtDividend.Rows[loop]["FI_DIVI_QTY"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["FI_DIVI_QTY"].ToString()))+"Only";

                drReport["CIP_QTY"] =Convert.ToInt32( dtDividend.Rows[loop]["CIP_QTY"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["CIP_QTY"].ToString());
                drReport["CIP_RATE"] =Convert.ToDecimal( dtDividend.Rows[loop]["CIP_RATE"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["CIP_RATE"].ToString());
                drReport["CIP"] = dtDividend.Rows[loop]["CIP"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["CIP"].ToString();
                drReport["AGM_DT"] = dtDividend.Rows[loop]["AGM_DT"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["AGM_DT"].ToString();

                //taxDiduct = Convert.ToDecimal(dtDividend.Rows[loop]["TAX_DIDUCT"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["TAX_DIDUCT"].ToString());
                //decimal taxDiductCal = 0;
                //if (taxDiduct == 0)
                //{
                //    drReport["TAX_RT_INDIVIDUAL"] = 0;
                //    drReport["TAX_RT_INSTITUTION"] = 0;
                //    taxDiductCal = 0;
                //}
                //else if (taxDiduct > 0)
                //{
                //    if (diviDAOObj.getRegType(Convert.ToInt32(dtDividend.Rows[loop]["REG_NUM"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["REG_NUM"].ToString()), dtDividend.Rows[loop]["FUND_CD"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["FUND_CD"].ToString(), dtDividend.Rows[loop]["REG_BR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["REG_BR"].ToString()).ToString().ToUpper() == "I")
                //    {
                //        drReport["TAX_RT_INDIVIDUAL"] = 0;
                //        drReport["TAX_RT_INSTITUTION"] = diviDAOObj.getTaxDiductRate(Convert.ToInt32(dtDividend.Rows[loop]["REG_NUM"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["REG_NUM"].ToString()), dtDividend.Rows[loop]["FUND_CD"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["FUND_CD"].ToString(), dtDividend.Rows[loop]["REG_BR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["REG_BR"].ToString(), dtDividend.Rows[loop]["F_YEAR"].ToString(), Convert.ToDecimal(dtDividend.Rows[loop]["TAX_LIMIT"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["TAX_LIMIT"].ToString()));
                //        taxDiductCal = diviDAOObj.getTaxDiductRate(Convert.ToInt32(dtDividend.Rows[loop]["REG_NUM"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["REG_NUM"].ToString()), dtDividend.Rows[loop]["FUND_CD"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["FUND_CD"].ToString(), dtDividend.Rows[loop]["REG_BR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["REG_BR"].ToString(), dtDividend.Rows[loop]["F_YEAR"].ToString(), Convert.ToDecimal(dtDividend.Rows[loop]["TAX_LIMIT"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["TAX_LIMIT"].ToString()));
                //    }
                //    else
                //    {
                //        drReport["TAX_RT_INDIVIDUAL"] = diviDAOObj.getTaxDiductRate(Convert.ToInt32(dtDividend.Rows[loop]["REG_NUM"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["REG_NUM"].ToString()), dtDividend.Rows[loop]["FUND_CD"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["FUND_CD"].ToString(), dtDividend.Rows[loop]["REG_BR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["REG_BR"].ToString(), dtDividend.Rows[loop]["F_YEAR"].ToString(), Convert.ToDecimal(dtDividend.Rows[loop]["TAX_LIMIT"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["TAX_LIMIT"].ToString()));
                //        drReport["TAX_RT_INSTITUTION"] = 0;
                //        taxDiductCal = diviDAOObj.getTaxDiductRate(Convert.ToInt32(dtDividend.Rows[loop]["REG_NUM"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["REG_NUM"].ToString()), dtDividend.Rows[loop]["FUND_CD"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["FUND_CD"].ToString(), dtDividend.Rows[loop]["REG_BR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["REG_BR"].ToString(), dtDividend.Rows[loop]["F_YEAR"].ToString(), Convert.ToDecimal(dtDividend.Rows[loop]["TAX_LIMIT"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["TAX_LIMIT"].ToString()));
                //    }
                //}

                drReport["REG_TYPE"] = dtDividend.Rows[loop]["REG_TYPE"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["REG_TYPE"].ToString();
                drReport["FY_PART"] = dtDividend.Rows[loop]["FY_PART"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["FY_PART"].ToString();
                drReport["NET_DIVI"] =Convert.ToDecimal( dtDividend.Rows[loop]["NET_DIVI"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["NET_DIVI"].ToString());
                drReport["FRAC_DIVI"] =Convert.ToDecimal( dtDividend.Rows[loop]["FRAC_DIVI"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["FRAC_DIVI"].ToString());
                drReport["REG_NUM"] = Convert.ToInt32(dtDividend.Rows[loop]["REG_NUM"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["REG_NUM"].ToString());
                drReport["REG_BR"] = dtDividend.Rows[loop]["REG_BR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["REG_BR"].ToString();
                string taxCalText = "";
                //DataTable dtTaxCal = diviDAOObj.dtDividendInfo(Convert.ToInt32(dtDividend.Rows[loop]["REG_NUM"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["REG_NUM"].ToString()), dtDividend.Rows[loop]["FUND_CD"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["FUND_CD"].ToString(), dtDividend.Rows[loop]["REG_BR"].Equals(DBNull.Value) ? "" : dtDividend.Rows[loop]["REG_BR"].ToString(), dtDividend.Rows[loop]["F_YEAR"].ToString());
                //if (dtTaxCal.Rows.Count > 1)
                //{
                //    if (taxDiduct == 0)
                //    {
                //        drReport["TAX_CAL_TEXT"] = "";
                //    }
                //    else
                //    {
                //        decimal totalDividend = Convert.ToDecimal(dtTaxCal.Rows[0]["TOT_DIVI"].ToString()) + Convert.ToDecimal(dtTaxCal.Rows[1]["TOT_DIVI"].ToString());
                //        decimal totalDiduct = Convert.ToDecimal(dtTaxCal.Rows[0]["DIDUCT"].ToString()) + Convert.ToDecimal(dtTaxCal.Rows[1]["DIDUCT"].ToString()); ;
                //        decimal total_remainning = totalDividend - Convert.ToDecimal(dtDividend.Rows[loop]["TAX_LIMIT"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["TAX_LIMIT"].ToString());
                //        decimal taxtLimit = Convert.ToDecimal(dtDividend.Rows[loop]["TAX_LIMIT"].Equals(DBNull.Value) ? "0" : dtDividend.Rows[loop]["TAX_LIMIT"].ToString());
                //        decimal diduct1 = Convert.ToDecimal(dtTaxCal.Rows[0]["DIDUCT"].ToString());
                //        decimal diduct2 = Convert.ToDecimal(dtTaxCal.Rows[1]["DIDUCT"].ToString());


                //        string TAX_CAL_TEXT = "*" + dtTaxCal.Rows[0]["TOT_DIVI"].ToString() + " (" + dtTaxCal.Rows[0]["FY_PART"].ToString() + ") +" + dtTaxCal.Rows[1]["TOT_DIVI"].ToString() + " (" + dtTaxCal.Rows[1]["FY_PART"].ToString() + ") ";
                //        TAX_CAL_TEXT = TAX_CAL_TEXT + "=" + totalDividend.ToString() + "-" + taxtLimit + "=";
                //        TAX_CAL_TEXT = TAX_CAL_TEXT + total_remainning.ToString() + "@" + taxDiductCal + "%=" + totalDiduct.ToString() + " [" + diduct1 + "(" + dtTaxCal.Rows[0]["FY_PART"].ToString() + ")+" + diduct2 + "(" + dtTaxCal.Rows[1]["FY_PART"].ToString() + ")" + "]";

                //        drReport["TAX_CAL_TEXT"] = TAX_CAL_TEXT;

                //    }
                //}
                //else
                //{
                //    drReport["TAX_CAL_TEXT"] = "";
                //}
                drReport["TAX_CAL_TEXT"] = "";
                dtReport.Rows.Add(drReport);
            }

            dtReport.TableName = "ReportDividend";
          //dtReport.WriteXmlSchema(@"D:\Project\Web\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\ReportDividend.xsd");
            if (fundCode.ToString().ToUpper() == "IAMPH")
            {
                CRPen_Dividend.Refresh();
                CRPen_Dividend.SetDataSource(dtReport);
                CRPen_Dividend.SetParameterValue("duplicate", duplicate);
                CrystalReportViewer1.ReportSource = CRPen_Dividend;          
            }
            else if (fundCode.ToString().ToUpper() == "BDF")
            {
                if (dividendCategory == "INTERIM")
                {
                    CRBDF_Dividend.Refresh();
                    CRBDF_Dividend.SetDataSource(dtReport);
                    CRBDF_Dividend.SetParameterValue("duplicate", duplicate);
                    CrystalReportViewer1.ReportSource = CRBDF_Dividend;
                }
                else
                {
                    CRBDF_Dividend.Refresh();
                    CRBDF_Dividend.SetDataSource(dtReport);
                    CRBDF_Dividend.SetParameterValue("duplicate", duplicate);
                    CrystalReportViewer1.ReportSource = CRBDF_Dividend;
                }
            }
            else 
            {
                CRUnit_Dividend.Refresh();              
                CRUnit_Dividend.SetDataSource(dtReport);
                CRUnit_Dividend.SetParameterValue("duplicate", duplicate);
                CrystalReportViewer1.ReportSource = CRUnit_Dividend;
            }       
        }
        else
        {
            Response.Write("No data found");
        }




    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRPen_Dividend.Close();
        CRPen_Dividend.Dispose();
        CRBDF_Dividend.Close();
        CRBDF_Dividend.Dispose();
        CRUnit_Dividend.Close();
        CRUnit_Dividend.Dispose();
        

    }
}
