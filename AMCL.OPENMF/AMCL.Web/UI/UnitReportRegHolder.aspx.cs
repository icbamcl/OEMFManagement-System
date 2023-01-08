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
using CrystalDecisions.Shared;
using System.Xml.Linq;
using System.Text;
using System.Data.OracleClient;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;


public partial class UI_UnitReportRegHolder : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();

 
    AMCL.REPORT.CR_UnitRegInfo CR_REGHOLDER_STATEMENT = new AMCL.REPORT.CR_UnitRegInfo();
    string fundCode = "";
    string branchCode = "";

    BaseClass bcContent = new BaseClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        fundCode = bcContent.FundCode.ToString();
        branchCode = bcContent.BranchCode.ToString();
        spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
        fundCodeTextBox.Text = fundCode.ToString();
        branchCodeTextBox.Text = branchCode.ToString();
    
   
       // toRegDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
       // fromRegDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy"); ;
        ///holderDateofBirthTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        if (!IsPostBack)
        {
            
        }

    }
   

    

    private void ClearText()
    {

    }
    protected void regCloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");

    }





    protected void ShowReportButton_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sbMaster = new StringBuilder();
            StringBuilder sbFilter = new StringBuilder();
            DataTable dtHolderInfo = new DataTable();
            DataTable dtNomineeInfo = new DataTable();

            dtNomineeInfo=commonGatewayObj.Select("SELECT * FROM U_NOMINEE WHERE 1 = 1  AND (U_NOMINEE.REG_BK = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (U_NOMINEE.REG_BR = '" + branchCodeTextBox.Text.Trim().ToString().ToUpper() + "')");


            sbMaster.Append(" SELECT A.*, B.H_BANK_NAME,B.H_BRANCH_NAME, C.I_BANK_NAME,C.I_BRANCH_NAME, D.DESCR AS H_PROFE, E.* ");
            sbMaster.Append(" FROM U_MASTER A LEFT OUTER JOIN (SELECT BANK_NAME.BANK_CODE AS H_BANK_CODE, BANK_NAME.BANK_NAME AS H_BANK_NAME, ");

            sbMaster.Append(" BANK_BRANCH.BRANCH_NAME  AS H_BRANCH_NAME ,BANK_BRANCH.BRANCH_CODE AS H_BRANCH_CODE FROM BANK_NAME,BANK_BRANCH ");
            sbMaster.Append(" WHERE BANK_NAME.BANK_CODE=BANK_BRANCH.BANK_CODE ) B  ON A.BK_NM_CD=B.H_BANK_CODE AND A.BK_BR_NM_CD=B.H_BRANCH_CODE ");
            sbMaster.Append(" LEFT OUTER JOIN  (SELECT BANK_NAME.BANK_CODE AS I_BANK_CODE, BANK_NAME.BANK_NAME AS I_BANK_NAME, ");
            sbMaster.Append(" BANK_BRANCH.BRANCH_NAME  AS I_BRANCH_NAME ,BANK_BRANCH.BRANCH_CODE AS I_BRANCH_CODE FROM BANK_NAME,BANK_BRANCH ");

            sbMaster.Append(" WHERE BANK_NAME.BANK_CODE=BANK_BRANCH.BANK_CODE ) C  ON A.ID_BK_NM_CD=C.I_BANK_CODE AND A.ID_BK_BR_NM_CD=C.I_BRANCH_CODE");
            sbMaster.Append(" LEFT OUTER JOIN OCC_CODE D ON A.OCC_CODE=D.CODE  LEFT OUTER JOIN ( SELECT J.*,O.DESCR AS J_PROFE FROM U_JHOLDER J LEFT OUTER JOIN  OCC_CODE O ON J.JNT_OCC_CODE=O.CODE  ) E ");
            sbMaster.Append(" ON A.REG_BK=E.REG_BK AND A.REG_BR=E.REG_BR AND A.REG_NO=E.REG_NO  ");           
            sbMaster.Append(" WHERE 1=1  AND (A.REG_BK = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (A.REG_BR = '" + branchCodeTextBox.Text.Trim().ToString().ToUpper() + "')");
            if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (A.REG_NO BETWEEN " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
            {
                sbFilter.Append(" AND (A.REG_NO> =" + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (A.REG_NO <=" + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ")");
            }

            if (fromRegDateTextBox.Text != "" && toRegDateTextBox.Text != "")
            {
                sbFilter.Append(" AND ( A.REG_DT BETWEEN '" + Convert.ToDateTime(fromRegDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(toRegDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            else if (fromRegDateTextBox.Text != "" && toRegDateTextBox.Text == "")
            {
                sbFilter.Append(" AND ( A.REG_DT >= '" + Convert.ToDateTime(fromRegDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            else if (fromRegDateTextBox.Text == "" && toRegDateTextBox.Text != "")
            {
                sbFilter.Append(" AND ( A.REG_DT <= '" + Convert.ToDateTime(toRegDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }

            sbFilter.Append(" ORDER BY A.REG_NO  ");
            sbMaster.Append(sbFilter.ToString());

            dtHolderInfo = commonGatewayObj.Select(sbMaster.ToString());

            if (dtHolderInfo.Rows.Count > 0)
            {
                dtHolderInfo.TableName = "dtUnitHolderInfo";               
                dtNomineeInfo.TableName = "dtUnitNomineeInfo";

              //  dtHolderInfo.WriteXmlSchema(@"F:\GITHUB_PROJECT\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtUnitHolderInfo.xsd");
             //   dtNomineeInfo.WriteXmlSchema(@"F:\GITHUB_PROJECT\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtUnitNomineeInfo.xsd");

                CR_REGHOLDER_STATEMENT.Refresh();
                CR_REGHOLDER_STATEMENT.SetDataSource(dtHolderInfo);
                CR_REGHOLDER_STATEMENT.SetParameterValue("fundName", opendMFDAO.GetFundName(fundCode.ToString()));
                CR_REGHOLDER_STATEMENT.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Holder_info_" + bcContent.FundCode.ToString() + ".pdf");


            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('No data found');", true);
            }
        }
        catch (Exception ex)
        {

            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + ex.Message.Replace("'", "").ToString() + "');", true);
        }

    }
}
