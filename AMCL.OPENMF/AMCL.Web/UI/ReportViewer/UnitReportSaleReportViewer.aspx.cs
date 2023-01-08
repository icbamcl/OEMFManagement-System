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

public partial class ReportViewer_UnitReportSaleReportViewer : System.Web.UI.Page
{

    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
    AMCL.REPORT.CR_UnitSaleStatementCDS sale_Statement_CDS = new AMCL.REPORT.CR_UnitSaleStatementCDS();
    AMCL.REPORT.CR_UnitSaleStatement sale_Statement = new AMCL.REPORT.CR_UnitSaleStatement();
    string CDSStatus = "";


    private ReportDocument rdoc = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        string fundCode = "";
        string branchCode="";
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        CDSStatus = bcContent.CDS.ToString().ToUpper();
        branchCode = (string)Session["branchCode"];
        fundCode = (string)Session["fundCode"];
            
       
        DataTable dtReport = reportObj.getDtForReportStatement();
        dtReport.TableName = "ReportSaleStatement";
        DataRow drReport;
        
        DataTable dtReportStatement = (DataTable)Session["dtReportStatement"];

        if (dtReportStatement.Rows.Count > 0)
        {
            int saleNo = 0;


            for (int looper = 0; looper < dtReportStatement.Rows.Count; looper++)
            {
                drReport = dtReport.NewRow();
                drReport["SL_NO"] = Convert.ToInt32(dtReportStatement.Rows[looper]["SL_NO"].ToString());
                drReport["SL_DT"] = dtReportStatement.Rows[looper]["SL_DT"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["SL_DT"].ToString();
                drReport["SL_TYPE"] = dtReportStatement.Rows[looper]["SL_TYPE"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["SL_TYPE"].ToString();
                drReport["HNAME"] = dtReportStatement.Rows[looper]["HNAME"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["HNAME"].ToString();
                drReport["MOBILE1"] = dtReportStatement.Rows[looper]["MOBILE1"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["MOBILE1"].ToString();

                drReport["NID"] = dtReportStatement.Rows[looper]["NID"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["NID"].ToString();
                drReport["MONY_RECT_NO"] = dtReportStatement.Rows[looper]["MONY_RECT_NO"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["MONY_RECT_NO"].ToString();

                drReport["TIN"] = dtReportStatement.Rows[looper]["TIN"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["TIN"].ToString();
                if (!dtReportStatement.Rows[looper]["REG_TYPE"].Equals(DBNull.Value))
                {
                    drReport["REG_TYPE"] = reportObj.getRegTypeFullName(dtReportStatement.Rows[looper]["REG_TYPE"].ToString()).ToString();
                }    
                drReport["JNT_NAME"] = dtReportStatement.Rows[looper]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["JNT_NAME"].ToString();               
                drReport["ADDRS1"] = dtReportStatement.Rows[looper]["ADDRS1"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["ADDRS1"].ToString();
                drReport["ADDRS2"] = dtReportStatement.Rows[looper]["ADDRS2"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["ADDRS2"].ToString();
                drReport["CITY"] = dtReportStatement.Rows[looper]["CITY"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["CITY"].ToString();
                drReport["REG_NO"] = dtReportStatement.Rows[looper]["REG_NO"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["REG_NO"].ToString();
                
                DataTable dtNominee = reportObj.dtNominee(dtReportStatement.Rows[looper]["REG_BK"].ToString(), dtReportStatement.Rows[looper]["REG_BR"].ToString(), Convert.ToInt32(dtReportStatement.Rows[looper]["RG_NO"].ToString()));
                if (dtNominee.Rows.Count > 0)
                {
                    for (int loop = 0; loop < dtNominee.Rows.Count; loop++)
                    {
                        if (Convert.ToInt16(dtNominee.Rows[loop]["NOMI_NO"].ToString()) == 1)
                        {
                            drReport["NOMI1_NAME"] = dtNominee.Rows[loop]["NOMI_NAME"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_NAME"].ToString();                            
                        }
                        else if (Convert.ToInt16(dtNominee.Rows[loop]["NOMI_NO"].ToString()) == 2)
                        {
                            drReport["NOMI2_NAME"] = dtNominee.Rows[loop]["NOMI_NAME"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_NAME"].ToString();                            
                        }
                    }
                }

                if (CDSStatus.ToString() == "Y")
                {                    
                    drReport["DRF_REF_NO"] = dtReportStatement.Rows[looper]["DRF_REF_NO"];
                    drReport["DRF_REG_FOLIO_NO"] = dtReportStatement.Rows[looper]["DRF_REG_FOLIO_NO"];
                    drReport["DRF_CERT_NO"] = dtReportStatement.Rows[looper]["DRF_CERT_NO"];
                    drReport["DRF_DISTNCT_NO_FROM"] = dtReportStatement.Rows[looper]["DRF_DISTNCT_NO_FROM"];
                    drReport["DRF_DISTNCT_NO_TO"] = dtReportStatement.Rows[looper]["DRF_DISTNCT_NO_TO"];
                    drReport["DRF_CUST_REQ_DATE"] = dtReportStatement.Rows[looper]["DRF_CUST_REQ_DATE"];
                    drReport["DRF_ACCEPT_NO"] = dtReportStatement.Rows[looper]["DRF_ACCEPT_NO"];
                    drReport["DRF_DRN"] = dtReportStatement.Rows[looper]["DRF_DRN"];
                    drReport["DRF_ACCEPT_DATE"] = dtReportStatement.Rows[looper]["DRF_ACCEPT_DATE"];
                    drReport["DRF_TR_SEQ_NO"] = dtReportStatement.Rows[looper]["DRF_TR_SEQ_NO"];
                    drReport["DRF_TR_DATE"] = dtReportStatement.Rows[looper]["DRF_TR_DATE"];
                    drReport["BO"] = dtReportStatement.Rows[looper]["HOLDER_BO"];
                }
                else
                {
                    saleNo = Convert.ToInt32(dtReportStatement.Rows[looper]["SL_NO"].ToString());
                    drReport["CERT_DLVRY_DT"] = dtReportStatement.Rows[looper]["CERT_DLVRY_DT"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["CERT_DLVRY_DT"].ToString();
                    drReport["CERT_RCV_BY"] = dtReportStatement.Rows[looper]["CERT_RCV_BY"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["CERT_RCV_BY"].ToString();
                    drReport["CERT_NO"] = reportObj.getTotalCertNo("SELECT  NVL(CERT_TYPE,' ') AS CERT_TYPE, NVL(CERT_NO,0) AS CERT_NO FROM SALE_CERT WHERE SL_NO=" + saleNo + " AND REG_BK='" + fundCode.ToString() + "'AND REG_BR='" + branchCode.ToString() + "'  ORDER BY CERT_TYPE ", fundCode.ToString()).ToString();
                }


                if (!dtReportStatement.Rows[looper]["BK_FLAG"].Equals(DBNull.Value))
                {
                    if (string.Compare(dtReportStatement.Rows[looper]["BK_FLAG"].ToString(), "Y", true) == 0)
                    {
                        if (!dtReportStatement.Rows[looper]["BK_NM_CD"].Equals(DBNull.Value) && !dtReportStatement.Rows[looper]["BK_BR_NM_CD"].Equals(DBNull.Value) && !dtReportStatement.Rows[looper]["BK_AC_NO"].Equals(DBNull.Value))
                        {
                            drReport["BK_AC_NO"] = dtReportStatement.Rows[looper]["BK_AC_NO"].ToString();
                            drReport["BANK_NAME"] = reportObj.getBankNameByBankCode(Convert.ToInt16(dtReportStatement.Rows[looper]["BK_NM_CD"].ToString())).ToString();
                            drReport["BRANCH_NAME"] = reportObj.getBankBranchNameByCode(Convert.ToInt16(dtReportStatement.Rows[looper]["BK_NM_CD"].ToString()), Convert.ToInt16(dtReportStatement.Rows[looper]["BK_BR_NM_CD"].ToString())).ToString();
                            drReport["BRANCH_ADDRESS"] = reportObj.getBankBranchAddressByCode(Convert.ToInt16(dtReportStatement.Rows[looper]["BK_NM_CD"].ToString()), Convert.ToInt16(dtReportStatement.Rows[looper]["BK_BR_NM_CD"].ToString())).ToString();
                        }
                        else
                        {
                            
                        }
                    }

                }
                drReport["CIP"] = dtReportStatement.Rows[looper]["CIP"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["CIP"].ToString();
                drReport["ID_AC"] = dtReportStatement.Rows[looper]["ID_AC"].Equals(DBNull.Value) ? "" : dtReportStatement.Rows[looper]["ID_AC"].ToString();
                drReport["QTY"] =Convert.ToInt32( dtReportStatement.Rows[looper]["QTY"].Equals(DBNull.Value) ? "0": dtReportStatement.Rows[looper]["QTY"].ToString());
                drReport["RATE"] =decimal.Parse( dtReportStatement.Rows[looper]["RATE"].Equals(DBNull.Value) ? "0" : dtReportStatement.Rows[looper]["RATE"].ToString());
                drReport["AMOUNT"] = decimal.Parse(dtReportStatement.Rows[looper]["AMOUNT"].Equals(DBNull.Value) ? "0" : dtReportStatement.Rows[looper]["AMOUNT"].ToString());
                drReport["FRAC_AMT"] = decimal.Parse(dtReportStatement.Rows[looper]["FRAC_AMT"].Equals(DBNull.Value) ? "0" : dtReportStatement.Rows[looper]["FRAC_AMT"].ToString());
                drReport["SIP_NO"] = Convert.ToInt32(dtReportStatement.Rows[looper]["SIP_NO"].Equals(DBNull.Value) ? "0" : dtReportStatement.Rows[looper]["SIP_NO"].ToString());

                dtReport.Rows.Add(drReport);
            }


            //dtReport.WriteXmlSchema(@"F:\GITHUB_PROJECT\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtUnitReportForStatement.xsd");
            if (CDSStatus.ToString()=="Y")
            {
                sale_Statement_CDS.Refresh();
                sale_Statement_CDS.SetDataSource(dtReport);

                sale_Statement_CDS.SetParameterValue("fundName", opendMFDAO.GetFundName(fundCode.ToString()));
                sale_Statement_CDS.SetParameterValue("branchName", opendMFDAO.GetBranchName(branchCode.ToString()).ToString());
                sale_Statement_CDS.SetParameterValue("branchCode", branchCode.ToString());

                CrystalReportViewer1.ReportSource = sale_Statement_CDS;
            }
            else
            {
                sale_Statement.Refresh();
                sale_Statement.SetDataSource(dtReport);

                sale_Statement.SetParameterValue("fundName", opendMFDAO.GetFundName(fundCode.ToString()));
                sale_Statement.SetParameterValue("branchName", opendMFDAO.GetBranchName(branchCode.ToString()).ToString());
                sale_Statement.SetParameterValue("branchCode", branchCode.ToString());

                CrystalReportViewer1.ReportSource = sale_Statement;
            }
           
         
           

        }
        else
        {
            Response.Write("No data found");
        }




    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        sale_Statement_CDS.Close();
        sale_Statement_CDS.Dispose();

        sale_Statement.Close();
        sale_Statement.Dispose();
    }
}
