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

public partial class ReportViewer_UnitReportRegHolderReportViewer : System.Web.UI.Page
{

    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
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
        branchCode = (string)Session["branchCode"];
        fundCode = (string)Session["fundCode"];
            
        
        DataTable dtUnitHolderInfo = reportObj.getDtHolderInfo();
        dtUnitHolderInfo.TableName = "UnitHolderInfo";
        DataRow drUnitHolderInfo;

        DataTable dtHolderInfo = (DataTable)Session["dtHolderInfo"];
        
        if (dtHolderInfo.Rows.Count > 0)
        {
            
            for (int looper = 0; looper < dtHolderInfo.Rows.Count; looper++)
            {
                drUnitHolderInfo = dtUnitHolderInfo.NewRow();
                drUnitHolderInfo["REG_NO"] = dtHolderInfo.Rows[looper]["REG_NO"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["REG_NO"].ToString();
                drUnitHolderInfo["REG_DT"] = dtHolderInfo.Rows[looper]["REG_DT"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["REG_DT"].ToString();

                if (!dtHolderInfo.Rows[looper]["REG_TYPE"].Equals(DBNull.Value))
                {
                    drUnitHolderInfo["REG_TYPE"] = reportObj.getRegTypeFullName(dtHolderInfo.Rows[looper]["REG_TYPE"].ToString()).ToString();
                }               
         
                drUnitHolderInfo["HNAME"] = dtHolderInfo.Rows[looper]["HNAME"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["HNAME"].ToString();
                drUnitHolderInfo["FMH_NAME"] = dtHolderInfo.Rows[looper]["FMH_NAME"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["FMH_NAME"].ToString();
                drUnitHolderInfo["MO_NAME"] = dtHolderInfo.Rows[looper]["MO_NAME"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["MO_NAME"].ToString();
                drUnitHolderInfo["ADDRS1"] = dtHolderInfo.Rows[looper]["ADDRS1"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["ADDRS1"].ToString();
                drUnitHolderInfo["ADDRS2"] = dtHolderInfo.Rows[looper]["ADDRS2"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["ADDRS2"].ToString();
                drUnitHolderInfo["CITY"] = dtHolderInfo.Rows[looper]["CITY"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["CITY"].ToString();
                drUnitHolderInfo["NATIONALITY"] = dtHolderInfo.Rows[looper]["NATIONALITY"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["NATIONALITY"].ToString();
                drUnitHolderInfo["OCC_CODE"] = dtHolderInfo.Rows[looper]["OCC_CODE"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["OCC_CODE"].ToString();
                drUnitHolderInfo["B_DATE"] = dtHolderInfo.Rows[looper]["B_DATE"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["B_DATE"].ToString();

                if (!dtHolderInfo.Rows[looper]["SEX"].Equals(DBNull.Value))
                {
                    if (string.Compare(dtHolderInfo.Rows[looper]["SEX"].ToString(), "M", true)==0)
                    {
                        drUnitHolderInfo["SEX"] = "MAlE";
                    }
                    else if (string.Compare(dtHolderInfo.Rows[looper]["SEX"].ToString(), "F", true)==0)
                    {
                        drUnitHolderInfo["SEX"] = "FEMALE";
                    }                  
                }
               

                if (!dtHolderInfo.Rows[looper]["MAR_STAT"].Equals(DBNull.Value))
                {
                    drUnitHolderInfo["MAR_STAT"] =reportObj.getMaritialFullName(dtHolderInfo.Rows[looper]["MAR_STAT"].ToString()).ToString();
                }               

                if (!dtHolderInfo.Rows[looper]["RELIGION"].Equals(DBNull.Value))
                {
                    drUnitHolderInfo["RELIGION"] = reportObj.getReligionFullName(dtHolderInfo.Rows[looper]["RELIGION"].ToString()).ToString();
                }
              

                drUnitHolderInfo["EDU_QUA"] = dtHolderInfo.Rows[looper]["EDU_QUA"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["EDU_QUA"].ToString();
                drUnitHolderInfo["TEL_NO"] = dtHolderInfo.Rows[looper]["TEL_NO"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["TEL_NO"].ToString();
                drUnitHolderInfo["EMAIL"] = dtHolderInfo.Rows[looper]["EMAIL"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["EMAIL"].ToString();

                drUnitHolderInfo["JNT_NAME"] = dtHolderInfo.Rows[looper]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["JNT_NAME"].ToString();
                drUnitHolderInfo["JNT_FMH_NAME"] = dtHolderInfo.Rows[looper]["JNT_FMH_NAME"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["JNT_FMH_NAME"].ToString();
                drUnitHolderInfo["JNT_MO_NAME"] = dtHolderInfo.Rows[looper]["JNT_MO_NAME"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["JNT_MO_NAME"].ToString();
                drUnitHolderInfo["JNT_OCC_CODE"] = dtHolderInfo.Rows[looper]["JNT_OCC_CODE"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["JNT_OCC_CODE"].ToString();
                drUnitHolderInfo["JNT_ADDRS1"] = dtHolderInfo.Rows[looper]["JNT_ADDRS1"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["JNT_ADDRS1"].ToString();
                drUnitHolderInfo["JNT_ADDRS2"] = dtHolderInfo.Rows[looper]["JNT_ADDRS2"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["JNT_ADDRS2"].ToString();
                drUnitHolderInfo["JNT_NATIONALITY"] = dtHolderInfo.Rows[looper]["JNT_NATIONALITY"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["JNT_NATIONALITY"].ToString();
                drUnitHolderInfo["JNT_CITY"] = dtHolderInfo.Rows[looper]["JNT_CITY"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["JNT_CITY"].ToString();
                drUnitHolderInfo["JNT_TEL_NO"] = dtHolderInfo.Rows[looper]["JNT_TEL_NO"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["JNT_TEL_NO"].ToString();
                drUnitHolderInfo["JNT_FMH_REL"] = dtHolderInfo.Rows[looper]["JNT_FMH_REL"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["JNT_FMH_REL"].ToString();

                drUnitHolderInfo["CIP"] = dtHolderInfo.Rows[looper]["CIP"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["CIP"].ToString();

                if (!dtHolderInfo.Rows[looper]["CIP"].Equals(DBNull.Value))
                {
                    if (string.Compare(dtHolderInfo.Rows[looper]["CIP"].ToString(), "Y", true) == 0)
                    {
                        drUnitHolderInfo["CIP"] = "YES";
                    }
                    else if (string.Compare(dtHolderInfo.Rows[looper]["CIP"].ToString(), "N", true) == 0)
                    {
                        drUnitHolderInfo["CIP"] = "NO";
                    }
                  
                }
               

                if (!dtHolderInfo.Rows[looper]["ID_FLAG"].Equals(DBNull.Value))
                {
                    if (string.Compare(dtHolderInfo.Rows[looper]["ID_FLAG"].ToString(), "Y", true )== 0)
                    {
                        drUnitHolderInfo["ID_FLAG"] = "YES";
                        drUnitHolderInfo["ID_AC"] = dtHolderInfo.Rows[looper]["ID_AC"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[looper]["ID_AC"].ToString();
                        drUnitHolderInfo["ID_INTSTITUTE_NAME"] = reportObj.getBankNameByBankCode(Convert.ToInt16(dtHolderInfo.Rows[0]["ID_BK_NM_CD"].Equals(DBNull.Value) ? "0" : dtHolderInfo.Rows[0]["ID_BK_NM_CD"].ToString())).ToString();
                        drUnitHolderInfo["ID_INTSTITUTE_BRANCH_NAME"] = reportObj.getBankBranchNameByCode(Convert.ToInt16(dtHolderInfo.Rows[0]["ID_BK_NM_CD"].Equals(DBNull.Value) ? "0" : dtHolderInfo.Rows[0]["ID_BK_NM_CD"].ToString()),Convert.ToInt16(dtHolderInfo.Rows[0]["ID_BK_BR_NM_CD"].Equals(DBNull.Value) ? "0" : dtHolderInfo.Rows[0]["ID_BK_BR_NM_CD"].ToString())).ToString();

                    }
                    else if (string.Compare(dtHolderInfo.Rows[looper]["ID_FLAG"].ToString(), "N", true)== 0)
                    {
                        drUnitHolderInfo["ID_FLAG"] = "NO";
                      
                    }
                   
                }
               


                if (!dtHolderInfo.Rows[looper]["BK_FLAG"].Equals(DBNull.Value))
                {
                    if (string.Compare(dtHolderInfo.Rows[looper]["BK_FLAG"].ToString(), "Y", true )== 0)
                    {
                        if (!dtHolderInfo.Rows[looper]["BK_NM_CD"].Equals(DBNull.Value) && !dtHolderInfo.Rows[looper]["BK_BR_NM_CD"].Equals(DBNull.Value) && !dtHolderInfo.Rows[looper]["BK_AC_NO"].Equals(DBNull.Value))
                        {
                            drUnitHolderInfo["BK_AC_NO"] = dtHolderInfo.Rows[looper]["BK_AC_NO"].ToString();
                            drUnitHolderInfo["BANK_NAME"] = reportObj.getBankNameByBankCode(Convert.ToInt16(dtHolderInfo.Rows[looper]["BK_NM_CD"].ToString())).ToString();
                            drUnitHolderInfo["BRANCH_NAME"] = reportObj.getBankBranchNameByCode(Convert.ToInt16(dtHolderInfo.Rows[looper]["BK_NM_CD"].ToString()), Convert.ToInt16(dtHolderInfo.Rows[looper]["BK_BR_NM_CD"].ToString())).ToString();
                            drUnitHolderInfo["BRANCH_ADDRESS"] = reportObj.getBankBranchAddressByCode(Convert.ToInt16(dtHolderInfo.Rows[looper]["BK_NM_CD"].ToString()), Convert.ToInt16(dtHolderInfo.Rows[looper]["BK_BR_NM_CD"].ToString())).ToString();
                        }
                        else
                        {
                            string branchAddress = "";
                            string BankAccInfo = dtHolderInfo.Rows[looper]["SPEC_IN1"].ToString() + dtHolderInfo.Rows[looper]["SPEC_IN2"].ToString();
                            string[] BankAccountInfo = BankAccInfo.Split(',');
                            if (BankAccountInfo.Length > 0)
                            {
                                drUnitHolderInfo["BK_AC_NO"] = BankAccountInfo[0].ToString();
                                if (BankAccountInfo.Length > 1)
                                {
                                    drUnitHolderInfo["BANK_NAME"] = BankAccountInfo[1].ToString();
                                }
                                if (BankAccountInfo.Length > 2)
                                {
                                    drUnitHolderInfo["BRANCH_NAME"] = BankAccountInfo[2].ToString();
                                }
                                if (BankAccountInfo.Length > 3)
                                {
                                    for (int loop = 3; loop < BankAccountInfo.Length; loop++)
                                    {
                                        branchAddress = branchAddress + BankAccountInfo[loop].ToString();
                                    }
                                    drUnitHolderInfo["BRANCH_ADDRESS"] = branchAddress;
                                }
                              
                            }
                        }
                    }
                  
                }
             

                DataTable dtNominee=reportObj.dtNominee(dtHolderInfo.Rows[looper]["REG_BK"].ToString(),dtHolderInfo.Rows[looper]["REG_BR"].ToString(),Convert.ToInt32(dtHolderInfo.Rows[looper]["R_NO"].ToString()));
                if (dtNominee.Rows.Count>0)
                {
                    for (int loop = 0; loop < dtNominee.Rows.Count; loop++)
                    {
                        if (Convert.ToInt16(dtNominee.Rows[loop]["NOMI_NO"].ToString())==1)
                        {
                            drUnitHolderInfo["NOMI_CTL_NO"] = dtNominee.Rows[loop]["NOMI_CTL_NO"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_CTL_NO"].ToString();
                            drUnitHolderInfo["NOMI1_NAME"] = dtNominee.Rows[loop]["NOMI_NAME"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_NAME"].ToString();
                            drUnitHolderInfo["NOMI1_FMH_NAME"] = dtNominee.Rows[loop]["NOMI_FMH_NAME"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_FMH_NAME"].ToString();
                            drUnitHolderInfo["NOMI1_OCC_CODE"] = dtNominee.Rows[loop]["DESCR"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["DESCR"].ToString();
                            drUnitHolderInfo["NOMI1_ADDRS1"] = dtNominee.Rows[loop]["NOMI_ADDRS1"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_ADDRS1"].ToString();
                            drUnitHolderInfo["NOMI1_ADDRS2"] = dtNominee.Rows[loop]["NOMI_ADDRS2"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_ADDRS2"].ToString();
                            drUnitHolderInfo["NOMI1_CITY"] = dtNominee.Rows[loop]["NOMI_CITY"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_CITY"].ToString();
                            drUnitHolderInfo["NOMI1_NATIONALITY"] = dtNominee.Rows[loop]["NOMI_NATIONALITY"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_NATIONALITY"].ToString();
                            drUnitHolderInfo["NOMI1_NOMI_REL"] = dtNominee.Rows[loop]["NOMI_REL"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_REL"].ToString();
                            drUnitHolderInfo["NOMI1_PERCENTAGE"] = dtNominee.Rows[loop]["PERCENTAGE"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["PERCENTAGE"].ToString();
                        }
                        else if (Convert.ToInt16(dtNominee.Rows[loop]["NOMI_NO"].ToString()) ==2)
                        {
                            drUnitHolderInfo["NOMI2_NAME"] = dtNominee.Rows[loop]["NOMI_NAME"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_NAME"].ToString();
                            drUnitHolderInfo["NOMI2_FMH_NAME"] = dtNominee.Rows[loop]["NOMI_FMH_NAME"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_FMH_NAME"].ToString();
                            drUnitHolderInfo["NOMI2_OCC_CODE"] = dtNominee.Rows[loop]["DESCR"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["DESCR"].ToString();
                            drUnitHolderInfo["NOMI2_ADDRS1"] = dtNominee.Rows[loop]["NOMI_ADDRS1"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_ADDRS1"].ToString();
                            drUnitHolderInfo["NOMI2_ADDRS2"] = dtNominee.Rows[loop]["NOMI_ADDRS2"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_ADDRS2"].ToString();
                            drUnitHolderInfo["NOMI2_CITY"] = dtNominee.Rows[loop]["NOMI_CITY"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_CITY"].ToString();
                            drUnitHolderInfo["NOMI2_NATIONALITY"] = dtNominee.Rows[loop]["NOMI_NATIONALITY"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_NATIONALITY"].ToString();
                            drUnitHolderInfo["NOMI2_NOMI_REL"] = dtNominee.Rows[loop]["NOMI_REL"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["NOMI_REL"].ToString();
                            drUnitHolderInfo["NOMI2_PERCENTAGE"] = dtNominee.Rows[loop]["PERCENTAGE"].Equals(DBNull.Value) ? "" : dtNominee.Rows[loop]["PERCENTAGE"].ToString();

                        }
                    }
                }
                dtUnitHolderInfo.Rows.Add(drUnitHolderInfo);
            }

           // dtUnitHolderInfo.WriteXmlSchema(@"D:\Project\Web\AMCL.OPENMF\AMCL.Web\UI\ReportViewer\Report\dtUnitHolderInfo.xsd");

            
            string Path = Server.MapPath("Report/rptRegHolderInfo.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtUnitHolderInfo);
            CrystalReportViewer1.ReportSource = rdoc;
            rdoc.SetParameterValue("fundName", opendMFDAO.GetFundName(fundCode.ToString()));
            rdoc = ReportFactory.GetReport(rdoc.GetType());
           

        }
        else
        {
            Response.Write("No data found");
        }




    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CrystalReportViewer1.Dispose();
        CrystalReportViewer1 = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
