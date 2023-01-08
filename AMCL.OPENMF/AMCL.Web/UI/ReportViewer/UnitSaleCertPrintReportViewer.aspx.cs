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

public partial class ReportViewer_UnitSaleCertPrintReportViewer : System.Web.UI.Page
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
   

    AMCL.REPORT.CR_UnitTransCertPrint CR_TransCertPrint = new CR_UnitTransCertPrint();
    AMCL.REPORT.CR_BDFTransCertPrint CR_BTransCertPrint = new CR_BDFTransCertPrint();
    AMCL.REPORT.CR_BDFSaleCertPrint CR_BSaleCertPrint = new CR_BDFSaleCertPrint();
    AMCL.REPORT.CR_UnitSaleCertPrint CR_SaleCertPrint = new CR_UnitSaleCertPrint();
    AMCL.REPORT.CR_CFUFSaleCertPrint CR_CFUFSaleCertPrint = new CR_CFUFSaleCertPrint();
    AMCL.REPORT.CR_CFUFCIPCertPrint CR_CFUFCIPCertPrint = new CR_CFUFCIPCertPrint();
    AMCL.REPORT.CR_IUFCIPCertPrint CR_IUFCIPCertPrint = new CR_IUFCIPCertPrint();
    AMCL.REPORT.CR_UnitCIPSCRIPTCertPrint CR_UnitCIPScriptPrint = new CR_UnitCIPSCRIPTCertPrint();
   
    protected void Page_Load(object sender, EventArgs e)
    {

        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        string fundCode = bcContent.FundCode.ToString();
        string branchCode = bcContent.BranchCode.ToString();
        DataTable dtSaleCertPrintReport = (DataTable)Session["dtSaleCertPrintReport"];
        string certType = (string)Session["certType"];
       // string certificate = "";
        string literature = "";
        
        int lineNumber = (int)Session["lineNumber"];
        try
        {
            if (dtSaleCertPrintReport.Rows.Count > 0)
            {



                dtSaleCertPrintReport.TableName = "dtSaleCertPrintReport";
             //   dtSaleCertPrintReport.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.PORTAL\AMCL.REPORT\XMLSCHEMAS\dtSaleCertPrintReport.xsd");

              
                if (certType.ToString() == "SALE" || certType.ToString() == "RENE")
                {

                    if ((dtSaleCertPrintReport.Rows[0]["SL_TYPE"].ToString().ToUpper() == "CIP" || dtSaleCertPrintReport.Rows[0]["SL_TYPE"].ToString().ToUpper() == "SIP") && certType.ToString() == "SALE" && (fundCode.ToString().ToUpper() == "CFUF" || fundCode.ToString().ToUpper() == "IUF"))
                    {
                        //certificate = reportObj.getCIPCertificateForPrint(fundCode, branchCode,Convert.ToInt32(dtSaleCertPrintReport.Rows[0]["SL_NO"].ToString()));
                        literature = "This is to certify that the above mentioned person(s)/institution is/are the registerd holder(s) of ";
                        literature = literature + dtSaleCertPrintReport.Rows[0]["QTY"].ToString() + " ( " + NumToEngObj.changeNumericToWords(Convert.ToDecimal(dtSaleCertPrintReport.Rows[0]["QTY"].ToString())) + ")";
                        literature = literature + " Units of " + opendMFDAO.GetFundName(fundCode.ToString()) + " issued under the authority as provided in the Trust Deed of the " + opendMFDAO.GetFundName(fundCode.ToString()) + ".";
                    }
                    else if ((dtSaleCertPrintReport.Rows[0]["SL_TYPE"].ToString().ToUpper() == "CIP" || dtSaleCertPrintReport.Rows[0]["SL_TYPE"].ToString().ToUpper() == "SIP") && certType.ToString() == "SALE" && (fundCode.ToString().ToUpper() == "IAMCL" || fundCode.ToString().ToUpper() == "IAMPH"))
                    {
                        literature = " This is to certify that the above mentioned person(s)/institution is/are the registerd holder(s) of ";
                        literature = literature + dtSaleCertPrintReport.Rows[0]["QTY"].ToString() + " (" + NumToEngObj.changeNumericToWords(Convert.ToDecimal(dtSaleCertPrintReport.Rows[0]["QTY"].ToString())) + ") ";
                        literature = literature + " Units of " + opendMFDAO.GetFundName(fundCode.ToString()) + " 100.00 each issued under the authority as provided in the Trust Deed of the " + opendMFDAO.GetFundName(fundCode.ToString()) + ".";
                    }

                    if (fundCode.ToString().ToUpper() == "BDF")
                    {
                        CR_BSaleCertPrint.Refresh();
                        CR_BSaleCertPrint.SetDataSource(dtSaleCertPrintReport);
                        CrystalReportViewer1.ReportSource = CR_BSaleCertPrint;
                    }
                    else if (fundCode.ToString().ToUpper() == "CFUF" )
                    {
                        if ((dtSaleCertPrintReport.Rows[0]["SL_TYPE"].ToString().ToUpper() == "CIP" || dtSaleCertPrintReport.Rows[0]["SL_TYPE"].ToString().ToUpper() == "SIP") && certType.ToString() == "SALE")
                        {
                            CR_CFUFCIPCertPrint.Refresh();
                            CR_CFUFCIPCertPrint.SetDataSource(dtSaleCertPrintReport);
                            CrystalReportViewer1.ReportSource = CR_CFUFCIPCertPrint;                           
                            CR_CFUFCIPCertPrint.SetParameterValue("literature", literature);

                        }
                        else
                        {
                            CR_CFUFSaleCertPrint.Refresh();
                            CR_CFUFSaleCertPrint.SetDataSource(dtSaleCertPrintReport);
                            CrystalReportViewer1.ReportSource = CR_CFUFSaleCertPrint;
                        }
                    }
                    else if(fundCode.ToString().ToUpper() == "IUF")
                    {
                        if ((dtSaleCertPrintReport.Rows[0]["SL_TYPE"].ToString().ToUpper() == "CIP" || dtSaleCertPrintReport.Rows[0]["SL_TYPE"].ToString().ToUpper() == "SIP") && certType.ToString() == "SALE")
                        {
                            CR_IUFCIPCertPrint.Refresh();
                            CR_IUFCIPCertPrint.SetDataSource(dtSaleCertPrintReport);
                            CrystalReportViewer1.ReportSource = CR_IUFCIPCertPrint;
                            CR_IUFCIPCertPrint.SetParameterValue("literature", literature);

                        }
                        else
                        {
                            CR_CFUFSaleCertPrint.Refresh();
                            CR_CFUFSaleCertPrint.SetDataSource(dtSaleCertPrintReport);
                            CrystalReportViewer1.ReportSource = CR_CFUFSaleCertPrint;
                        }
                    }
                    else if (fundCode.ToString().ToUpper() == "IAMCL")
                    {
                        if ((dtSaleCertPrintReport.Rows[0]["SL_TYPE"].ToString().ToUpper() == "CIP"|| dtSaleCertPrintReport.Rows[0]["SL_TYPE"].ToString().ToUpper() == "SIP") && certType.ToString() == "SALE")
                        {
                            CR_UnitCIPScriptPrint.Refresh();
                            CR_UnitCIPScriptPrint.SetDataSource(dtSaleCertPrintReport);
                            CrystalReportViewer1.ReportSource = CR_UnitCIPScriptPrint;                            
                            CR_UnitCIPScriptPrint.SetParameterValue("literature", literature);

                        }
                        else
                        {
                            CR_SaleCertPrint.Refresh();
                            CR_SaleCertPrint.SetDataSource(dtSaleCertPrintReport);
                            CrystalReportViewer1.ReportSource = CR_SaleCertPrint;
                        }
                    }
                    else if (fundCode.ToString().ToUpper() == "IAMPH")
                    {
                        if ( dtSaleCertPrintReport.Rows[0]["SL_TYPE"].ToString().ToUpper() == "SIP" && certType.ToString() == "SALE")
                        {
                            CR_UnitCIPScriptPrint.Refresh();
                            CR_UnitCIPScriptPrint.SetDataSource(dtSaleCertPrintReport);
                            CrystalReportViewer1.ReportSource = CR_UnitCIPScriptPrint;
                            CR_UnitCIPScriptPrint.SetParameterValue("literature", literature);

                        }
                        else
                        {
                            CR_SaleCertPrint.Refresh();
                            CR_SaleCertPrint.SetDataSource(dtSaleCertPrintReport);
                            CrystalReportViewer1.ReportSource = CR_SaleCertPrint;
                        }
                    }
                    else
                    {
                        CR_SaleCertPrint.Refresh();
                        CR_SaleCertPrint.SetDataSource(dtSaleCertPrintReport);
                        CrystalReportViewer1.ReportSource = CR_SaleCertPrint;
                    }


                }
                else if (certType.ToString() == "TRAN")
                {
                    string date1 = "";
                    string date2 = "";
                    string date3 = "";
                    string date4 = "";
                    string date5 = "";
                    string date6 = "";
                    string date7 = "";

                    string transNumber1 = "";
                    string transNumber2 = "";
                    string transNumber3 = "";
                    string transNumber4 = "";
                    string transNumber5 = "";
                    string transNumber6 = "";
                    string transNumber7 = "";

                    string tfereeRegNo1 = "";
                    string tfereeRegNo2 = "";
                    string tfereeRegNo3 = "";
                    string tfereeRegNo4 = "";
                    string tfereeRegNo5 = "";
                    string tfereeRegNo6 = "";
                    string tfereeRegNo7 = "";

                    string tfereeName1 = "";
                    string tfereeName2 = "";
                    string tfereeName3 = "";
                    string tfereeName4 = "";
                    string tfereeName5 = "";
                    string tfereeName6 = "";
                    string tfereeName7 = "";

                    string tfreeJHolderName1 = "";
                    string tfreeJHolderName2 = "";
                    string tfreeJHolderName3 = "";
                    string tfreeJHolderName4 = "";
                    string tfreeJHolderName5 = "";
                    string tfreeJHolderName6 = "";
                    string tfreeJHolderName7 = "";

                    string tfreeAddress1 = "";
                    string tfreeAddress2 = "";
                    string tfreeAddress3 = "";
                    string tfreeAddress4 = "";
                    string tfreeAddress5 = "";
                    string tfreeAddress6 = "";
                    string tfreeAddress7 = "";

                    string tferorRegNo1 = "";
                    string tferorRegNo2 = "";
                    string tferorRegNo3 = "";
                    string tferorRegNo4 = "";
                    string tferorRegNo5 = "";
                    string tferorRegNo6 = "";
                    string tferorRegNo7 = "";

                    if (lineNumber == 1)
                    {
                        date1 = dtSaleCertPrintReport.Rows[0]["TR_DT"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TR_DT"].ToString();
                        transNumber1 = dtSaleCertPrintReport.Rows[0]["TR_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TR_NO"].ToString();
                        tfereeRegNo1 = dtSaleCertPrintReport.Rows[0]["TFREE_REG_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TFREE_REG_NO"].ToString();
                        tfereeName1 = dtSaleCertPrintReport.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["HNAME"].ToString();
                        tfreeJHolderName1 = dtSaleCertPrintReport.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["JNT_NAME"].ToString();
                        tfreeAddress1 = dtSaleCertPrintReport.Rows[0]["ADDRESS"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["ADDRESS"].ToString();
                        tferorRegNo1 = dtSaleCertPrintReport.Rows[0]["TFEROR_REG_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TFEROR_REG_NO"].ToString();
                    }
                    else if (lineNumber == 2)
                    {
                        date2 = dtSaleCertPrintReport.Rows[0]["TR_DT"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TR_DT"].ToString();
                        transNumber2 = dtSaleCertPrintReport.Rows[0]["TR_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TR_NO"].ToString();
                        tfereeRegNo2 = dtSaleCertPrintReport.Rows[0]["TFREE_REG_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TFREE_REG_NO"].ToString();
                        tfereeName2 = dtSaleCertPrintReport.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["HNAME"].ToString();
                        tfreeJHolderName2 = dtSaleCertPrintReport.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["JNT_NAME"].ToString();
                        tfreeAddress2 = dtSaleCertPrintReport.Rows[0]["ADDRESS"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["ADDRESS"].ToString();
                        tferorRegNo2 = dtSaleCertPrintReport.Rows[0]["TFEROR_REG_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TFEROR_REG_NO"].ToString();
                    }
                    else if (lineNumber == 3)
                    {
                        date3 = dtSaleCertPrintReport.Rows[0]["TR_DT"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TR_DT"].ToString();
                        transNumber3 = dtSaleCertPrintReport.Rows[0]["TR_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TR_NO"].ToString();
                        tfereeRegNo3 = dtSaleCertPrintReport.Rows[0]["TFREE_REG_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TFREE_REG_NO"].ToString();
                        tfereeName3 = dtSaleCertPrintReport.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["HNAME"].ToString();
                        tfreeJHolderName3 = dtSaleCertPrintReport.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["JNT_NAME"].ToString();
                        tfreeAddress3 = dtSaleCertPrintReport.Rows[0]["ADDRESS"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["ADDRESS"].ToString();
                        tferorRegNo3 = dtSaleCertPrintReport.Rows[0]["TFEROR_REG_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TFEROR_REG_NO"].ToString();
                    }
                    else if (lineNumber == 4)
                    {
                        date4 = dtSaleCertPrintReport.Rows[0]["TR_DT"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TR_DT"].ToString();
                        transNumber4 = dtSaleCertPrintReport.Rows[0]["TR_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TR_NO"].ToString();
                        tfereeRegNo4 = dtSaleCertPrintReport.Rows[0]["TFREE_REG_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TFREE_REG_NO"].ToString();
                        tfereeName4 = dtSaleCertPrintReport.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["HNAME"].ToString();
                        tfreeJHolderName4 = dtSaleCertPrintReport.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["JNT_NAME"].ToString();
                        tfreeAddress4 = dtSaleCertPrintReport.Rows[0]["ADDRESS"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["ADDRESS"].ToString();
                        tferorRegNo4 = dtSaleCertPrintReport.Rows[0]["TFEROR_REG_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TFEROR_REG_NO"].ToString();
                    }
                    else if (lineNumber == 5)
                    {
                        date5 = dtSaleCertPrintReport.Rows[0]["TR_DT"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TR_DT"].ToString();
                        transNumber5 = dtSaleCertPrintReport.Rows[0]["TR_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TR_NO"].ToString();
                        tfereeRegNo5 = dtSaleCertPrintReport.Rows[0]["TFREE_REG_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TFREE_REG_NO"].ToString();
                        tfereeName5 = dtSaleCertPrintReport.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["HNAME"].ToString();
                        tfreeJHolderName5 = dtSaleCertPrintReport.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["JNT_NAME"].ToString();
                        tfreeAddress5 = dtSaleCertPrintReport.Rows[0]["ADDRESS"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["ADDRESS"].ToString();
                        tferorRegNo5 = dtSaleCertPrintReport.Rows[0]["TFEROR_REG_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TFEROR_REG_NO"].ToString();
                    }
                    else if (lineNumber == 6)
                    {
                        date6 = dtSaleCertPrintReport.Rows[0]["TR_DT"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TR_DT"].ToString();
                        transNumber6 = dtSaleCertPrintReport.Rows[0]["TR_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TR_NO"].ToString();
                        tfereeRegNo6 = dtSaleCertPrintReport.Rows[0]["TFREE_REG_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TFREE_REG_NO"].ToString();
                        tfereeName6 = dtSaleCertPrintReport.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["HNAME"].ToString();
                        tfreeJHolderName6 = dtSaleCertPrintReport.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["JNT_NAME"].ToString();
                        tfreeAddress6 = dtSaleCertPrintReport.Rows[0]["ADDRESS"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["ADDRESS"].ToString();
                        tferorRegNo6 = dtSaleCertPrintReport.Rows[0]["TFEROR_REG_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TFEROR_REG_NO"].ToString();
                    }
                    else if (lineNumber == 7)
                    {
                        date7 = dtSaleCertPrintReport.Rows[0]["TR_DT"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TR_DT"].ToString();
                        transNumber7 = dtSaleCertPrintReport.Rows[0]["TR_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TR_NO"].ToString();
                        tfereeRegNo7 = dtSaleCertPrintReport.Rows[0]["TFREE_REG_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TFREE_REG_NO"].ToString();
                        tfereeName7 = dtSaleCertPrintReport.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["HNAME"].ToString();
                        tfreeJHolderName7 = dtSaleCertPrintReport.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["JNT_NAME"].ToString();
                        tfreeAddress7 = dtSaleCertPrintReport.Rows[0]["ADDRESS"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["ADDRESS"].ToString();
                        tferorRegNo7 = dtSaleCertPrintReport.Rows[0]["TFEROR_REG_NO"].Equals(DBNull.Value) ? "" : dtSaleCertPrintReport.Rows[0]["TFEROR_REG_NO"].ToString();
                    }

                    // dtSaleCertPrintReport.WriteXmlSchema(@"D:\Project\Web\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtTransCertPrintReport.xsd");
                    
                    //CR_Dividend.SetDataSource(dtReport);
                    if (fundCode.ToString().ToUpper() == "BDF")
                    {
                        CR_BTransCertPrint.Refresh();
                        CR_BTransCertPrint.SetParameterValue("date1", date1);
                        CR_BTransCertPrint.SetParameterValue("transNumber1", transNumber1);
                        CR_BTransCertPrint.SetParameterValue("tfereeRegNo1", tfereeRegNo1);
                        CR_BTransCertPrint.SetParameterValue("tfereeName1", tfereeName1);
                        CR_BTransCertPrint.SetParameterValue("tfreeJHolderName1", tfreeJHolderName1);
                        CR_BTransCertPrint.SetParameterValue("tfreeAddress1", tfreeAddress1);
                        CR_BTransCertPrint.SetParameterValue("tferorRegNo1", tferorRegNo1);

                        CR_BTransCertPrint.SetParameterValue("date2", date2);
                        CR_BTransCertPrint.SetParameterValue("transNumber2", transNumber2);
                        CR_BTransCertPrint.SetParameterValue("tfereeRegNo2", tfereeRegNo2);
                        CR_BTransCertPrint.SetParameterValue("tfereeName2", tfereeName2);
                        CR_BTransCertPrint.SetParameterValue("tfreeJHolderName2", tfreeJHolderName2);
                        CR_BTransCertPrint.SetParameterValue("tfreeAddress2", tfreeAddress2);
                        CR_BTransCertPrint.SetParameterValue("tferorRegNo2", tferorRegNo2);

                        CR_BTransCertPrint.SetParameterValue("date3", date3);
                        CR_BTransCertPrint.SetParameterValue("transNumber3", transNumber3);
                        CR_BTransCertPrint.SetParameterValue("tfereeRegNo3", tfereeRegNo3);
                        CR_BTransCertPrint.SetParameterValue("tfereeName3", tfereeName3);
                        CR_BTransCertPrint.SetParameterValue("tfreeJHolderName3", tfreeJHolderName3);
                        CR_BTransCertPrint.SetParameterValue("tfreeAddress3", tfreeAddress3);
                        CR_BTransCertPrint.SetParameterValue("tferorRegNo3", tferorRegNo3);

                        CR_BTransCertPrint.SetParameterValue("date4", date4);
                        CR_BTransCertPrint.SetParameterValue("transNumber4", transNumber4);
                        CR_BTransCertPrint.SetParameterValue("tfereeRegNo4", tfereeRegNo4);
                        CR_BTransCertPrint.SetParameterValue("tfereeName4", tfereeName4);
                        CR_BTransCertPrint.SetParameterValue("tfreeJHolderName4", tfreeJHolderName4);
                        CR_BTransCertPrint.SetParameterValue("tfreeAddress4", tfreeAddress4);
                        CR_BTransCertPrint.SetParameterValue("tferorRegNo4", tferorRegNo4);

                        CR_BTransCertPrint.SetParameterValue("date5", date5);
                        CR_BTransCertPrint.SetParameterValue("transNumber5", transNumber5);
                        CR_BTransCertPrint.SetParameterValue("tfereeRegNo5", tfereeRegNo5);
                        CR_BTransCertPrint.SetParameterValue("tfereeName5", tfereeName5);
                        CR_BTransCertPrint.SetParameterValue("tfreeJHolderName5", tfreeJHolderName5);
                        CR_BTransCertPrint.SetParameterValue("tfreeAddress5", tfreeAddress5);
                        CR_BTransCertPrint.SetParameterValue("tferorRegNo5", tferorRegNo5);

                        CR_BTransCertPrint.SetParameterValue("date6", date6);
                        CR_BTransCertPrint.SetParameterValue("transNumber6", transNumber6);
                        CR_BTransCertPrint.SetParameterValue("tfereeRegNo6", tfereeRegNo6);
                        CR_BTransCertPrint.SetParameterValue("tfereeName6", tfereeName6);
                        CR_BTransCertPrint.SetParameterValue("tfreeJHolderName6", tfreeJHolderName6);
                        CR_BTransCertPrint.SetParameterValue("tfreeAddress6", tfreeAddress6);
                        CR_BTransCertPrint.SetParameterValue("tferorRegNo6", tferorRegNo6);

                        CR_BTransCertPrint.SetParameterValue("date7", date7);
                        CR_BTransCertPrint.SetParameterValue("transNumber7", transNumber7);
                        CR_BTransCertPrint.SetParameterValue("tfereeRegNo7", tfereeRegNo7);
                        CR_BTransCertPrint.SetParameterValue("tfereeName7", tfereeName7);
                        CR_BTransCertPrint.SetParameterValue("tfreeJHolderName7", tfreeJHolderName7);
                        CR_BTransCertPrint.SetParameterValue("tfreeAddress7", tfreeAddress7);
                        CR_BTransCertPrint.SetParameterValue("tferorRegNo7", tferorRegNo7);

                        CrystalReportViewer1.ReportSource = CR_BTransCertPrint;
                    }
                    else
                    {
                        //CR_TransCertPrint.SetDatabaseLogon("UNIT", "UNIT","194.25.1.200","AMCL");
                        //CR_TransCertPrint.VerifyDatabase();


                        CR_TransCertPrint.Refresh();
                        CR_TransCertPrint.SetParameterValue("date1", date1);
                        CR_TransCertPrint.SetParameterValue("transNumber1", transNumber1);
                        CR_TransCertPrint.SetParameterValue("tfereeRegNo1", tfereeRegNo1);
                        CR_TransCertPrint.SetParameterValue("tfereeName1", tfereeName1);
                        CR_TransCertPrint.SetParameterValue("tfreeJHolderName1", tfreeJHolderName1);
                        CR_TransCertPrint.SetParameterValue("tfreeAddress1", tfreeAddress1);
                        CR_TransCertPrint.SetParameterValue("tferorRegNo1", tferorRegNo1);

                        CR_TransCertPrint.SetParameterValue("date2", date2);
                        CR_TransCertPrint.SetParameterValue("transNumber2", transNumber2);
                        CR_TransCertPrint.SetParameterValue("tfereeRegNo2", tfereeRegNo2);
                        CR_TransCertPrint.SetParameterValue("tfereeName2", tfereeName2);
                        CR_TransCertPrint.SetParameterValue("tfreeJHolderName2", tfreeJHolderName2);
                        CR_TransCertPrint.SetParameterValue("tfreeAddress2", tfreeAddress2);
                        CR_TransCertPrint.SetParameterValue("tferorRegNo2", tferorRegNo2);

                        CR_TransCertPrint.SetParameterValue("date3", date3);
                        CR_TransCertPrint.SetParameterValue("transNumber3", transNumber3);
                        CR_TransCertPrint.SetParameterValue("tfereeRegNo3", tfereeRegNo3);
                        CR_TransCertPrint.SetParameterValue("tfereeName3", tfereeName3);
                        CR_TransCertPrint.SetParameterValue("tfreeJHolderName3", tfreeJHolderName3);
                        CR_TransCertPrint.SetParameterValue("tfreeAddress3", tfreeAddress3);
                        CR_TransCertPrint.SetParameterValue("tferorRegNo3", tferorRegNo3);

                        CR_TransCertPrint.SetParameterValue("date4", date4);
                        CR_TransCertPrint.SetParameterValue("transNumber4", transNumber4);
                        CR_TransCertPrint.SetParameterValue("tfereeRegNo4", tfereeRegNo4);
                        CR_TransCertPrint.SetParameterValue("tfereeName4", tfereeName4);
                        CR_TransCertPrint.SetParameterValue("tfreeJHolderName4", tfreeJHolderName4);
                        CR_TransCertPrint.SetParameterValue("tfreeAddress4", tfreeAddress4);
                        CR_TransCertPrint.SetParameterValue("tferorRegNo4", tferorRegNo4);

                        CR_TransCertPrint.SetParameterValue("date5", date5);
                        CR_TransCertPrint.SetParameterValue("transNumber5", transNumber5);
                        CR_TransCertPrint.SetParameterValue("tfereeRegNo5", tfereeRegNo5);
                        CR_TransCertPrint.SetParameterValue("tfereeName5", tfereeName5);
                        CR_TransCertPrint.SetParameterValue("tfreeJHolderName5", tfreeJHolderName5);
                        CR_TransCertPrint.SetParameterValue("tfreeAddress5", tfreeAddress5);
                        CR_TransCertPrint.SetParameterValue("tferorRegNo5", tferorRegNo5);

                        CR_TransCertPrint.SetParameterValue("date6", date6);
                        CR_TransCertPrint.SetParameterValue("transNumber6", transNumber6);
                        CR_TransCertPrint.SetParameterValue("tfereeRegNo6", tfereeRegNo6);
                        CR_TransCertPrint.SetParameterValue("tfereeName6", tfereeName6);
                        CR_TransCertPrint.SetParameterValue("tfreeJHolderName6", tfreeJHolderName6);
                        CR_TransCertPrint.SetParameterValue("tfreeAddress6", tfreeAddress6);
                        CR_TransCertPrint.SetParameterValue("tferorRegNo6", tferorRegNo6);

                        CR_TransCertPrint.SetParameterValue("date7", date7);
                        CR_TransCertPrint.SetParameterValue("transNumber7", transNumber7);
                        CR_TransCertPrint.SetParameterValue("tfereeRegNo7", tfereeRegNo7);
                        CR_TransCertPrint.SetParameterValue("tfereeName7", tfereeName7);
                        CR_TransCertPrint.SetParameterValue("tfreeJHolderName7", tfreeJHolderName7);
                        CR_TransCertPrint.SetParameterValue("tfreeAddress7", tfreeAddress7);
                        CR_TransCertPrint.SetParameterValue("tferorRegNo7", tferorRegNo7);




                        CrystalReportViewer1.ReportSource = CR_TransCertPrint;

                    }
                   

                }

            }
            else
            {
                Response.Write("No data found");
            }

        }
        catch (Exception ex)
        {
            Response.Write("Error: "+ex.ToString());
        }
       
        }
    protected void Page_Unload(object sender, EventArgs e)
        {
            CR_TransCertPrint.Close();
            CR_TransCertPrint.Dispose();
            CR_SaleCertPrint.Close();
            CR_SaleCertPrint.Dispose();
            CR_BSaleCertPrint.Close();
            CR_BSaleCertPrint.Dispose();
            CR_BTransCertPrint.Close();
            CR_BTransCertPrint.Dispose();

            CR_CFUFSaleCertPrint.Close();
            CR_CFUFSaleCertPrint.Dispose();
            
        }
    }

