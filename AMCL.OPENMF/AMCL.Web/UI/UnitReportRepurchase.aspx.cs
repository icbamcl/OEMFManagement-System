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
using System.Text;
using System.Data.OracleClient;
using System.IO;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;


public partial class UI_UnitReportRepurchase : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
    UnitRepurchaseBL RepurchaseBLObj = new UnitRepurchaseBL();
    UnitRepurchase RepurchaseObj = new UnitRepurchase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        string fundCode = bcContent.FundCode.ToString();
        string branchCode = bcContent.BranchCode.ToString();
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
            DataTable dtReportStatement = new DataTable();

            sbMaster.Append("SELECT NVL(REPURCHASE.REP_NO,0) AS REP_NO , TO_CHAR(REPURCHASE.REP_DT, 'DD-MON-YYYY') AS REP_DT, U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO AS RG_NO, U_MASTER.BO,");
            sbMaster.Append(" U_MASTER.HNAME, U_JHOLDER.JNT_NAME, U_MASTER.REG_BK || '/' || U_MASTER.REG_BR || '/' || U_MASTER.REG_NO AS REG_NO,");
            sbMaster.Append(" U_MASTER.ADDRS1, U_MASTER.ADDRS2, U_MASTER.CITY, U_MASTER.SPEC_IN1, U_MASTER.SPEC_IN2, U_MASTER.BK_AC_NO,");
            sbMaster.Append(" U_MASTER.BK_BR_NM_CD, U_MASTER.BK_FLAG,REPURCHASE.QTY,  REPURCHASE.REP_PRICE AS RATE, REPURCHASE.QTY * REPURCHASE.REP_PRICE AS AMOUNT,  REPURCHASE.SL_TR_NO, U_MASTER.CIP, U_MASTER.ID_AC");
            sbMaster.Append(" FROM  U_MASTER INNER JOIN  REPURCHASE ON U_MASTER.REG_BK = REPURCHASE.REG_BK AND U_MASTER.REG_BR = REPURCHASE.REG_BR AND  U_MASTER.REG_NO = REPURCHASE.REG_NO");
            sbMaster.Append(" LEFT OUTER JOIN  U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND U_MASTER.REG_NO = U_JHOLDER.REG_NO");                      
            sbMaster.Append(" WHERE 1=1");
           
            sbMaster.Append(" AND (U_MASTER.REG_BK = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (U_MASTER.REG_BR = '" + branchCodeTextBox.Text.Trim().ToString().ToUpper() + "')");

            if (fromRepNoTextBox.Text != "" && toRepNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (REPURCHASE.REP_NO BETWEEN " + Convert.ToInt32(fromRepNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRepNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRepNoTextBox.Text != "" && toRepNoTextBox.Text == "")
            {
                sbFilter.Append(" AND (REPURCHASE.REP_NO>=" + Convert.ToInt32(fromRepNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRepNoTextBox.Text == "" && toRepNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (REPURCHASE.REP_NO<=" + Convert.ToInt32(toRepNoTextBox.Text.Trim().ToString()) + ")");
            }

            if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (U_MASTER.REG_NO BETWEEN " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
            {
                sbFilter.Append(" AND (U_MASTER.REG_NO >=" + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (U_MASTER.REG_NO <=" + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ")");
            }


            if (fromRepDateTextBox.Text != "" && toRepDateTextBox.Text != "")
            {
                sbFilter.Append(" AND ( REPURCHASE.REP_DT BETWEEN '" + Convert.ToDateTime(fromRepDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(toRepDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            else if (fromRepDateTextBox.Text != "" && toRepDateTextBox.Text == "")
            {
                sbFilter.Append(" AND ( REPURCHASE.REP_DT => '" + Convert.ToDateTime(fromRepDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            else if (fromRepDateTextBox.Text == "" && toRepDateTextBox.Text != "")
            {
                sbFilter.Append(" AND (REPURCHASE.REP_DT <= '" + Convert.ToDateTime(toRepDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }


            sbFilter.Append(" ORDER BY REPURCHASE.REP_NO ");
            sbMaster.Append(sbFilter.ToString());

            if (string.Compare(fundCodeTextBox.Text.Trim().ToString().ToUpper(), "IAMPH", true) == 0)
            {
                OracleConnection Conn = new OracleConnection(ConfigReader.PENSION.ToString());
                Conn.Open();
                dtReportStatement = commonGatewayObj.Select(sbMaster.ToString(), Conn);
            }
            else
            {
                dtReportStatement = commonGatewayObj.Select(sbMaster.ToString());
            }
            if (dtReportStatement.Rows.Count > 0)
            {
                Session["Report_Type"] ="DETAILS";
                Session["dtReportStatement"] = dtReportStatement;
                Session["fundCode"]=fundCodeTextBox.Text.Trim().ToString();
                Session["branchCode"] = branchCodeTextBox.Text.Trim().ToString();

                ClientScript.RegisterStartupScript(this.GetType(), "UnitHolderInfo", "window.open('ReportViewer/UnitReportRepurchaseReportViewer.aspx')", true);

            }
            else
            {
                Session["Report_Type"] =null;
                Session["dtReportStatement"] = null;
                Session["fundCode"] = null;
                Session["branchCode"] = null;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('No data found');", true);
            }
        }
        catch (Exception ex)
        {
         
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + ex.Message.Replace("'", "").ToString() + "');", true);
        }

    }
    protected void ExportReportButton_Click(object sender, EventArgs e)
    {
        StringBuilder sbQuery = new StringBuilder();
        StringBuilder sbFileHeader = new StringBuilder();
        StringBuilder sbFileData = new StringBuilder();

        UnitHolderRegistration regObject = new UnitHolderRegistration();
        regObject.FundCode = fundCodeTextBox.Text.Trim().ToString();
        regObject.BranchCode = branchCodeTextBox.Text.Trim().ToString();
        string[] BranchCode = branchCodeTextBox.Text.Trim().ToString().Split('/');
        string fileName = fundCodeTextBox.Text.Trim().ToString() + "_" + BranchCode[0].ToString() + "_" + BranchCode[1].ToString() + "_REP_";


        sbQuery.Append("SELECT * FROM REPURCHASE WHERE (REG_BK='" + fundCodeTextBox.Text.Trim().ToString() + "') AND ( REG_BR='" + branchCodeTextBox.Text.Trim().ToString() + "' ) ");
            if (fromRepNoTextBox.Text != "" && toRepNoTextBox.Text != "")
            {
                sbQuery.Append("AND  (REP_NO BETWEEN " + Convert.ToInt32(fromRepNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRepNoTextBox.Text.Trim().ToString()) + ") ");
                fileName = fileName + fromRepNoTextBox.Text.Trim().ToString() + "_" + toRepNoTextBox.Text.Trim().ToString();
            }
            else if (fromRepNoTextBox.Text != "" && toRepNoTextBox.Text == "")
            {
                sbQuery.Append("AND  (REP_NO >= " + Convert.ToInt32(fromRepNoTextBox.Text.Trim().ToString()) + ") ");
                int MaxRep = RepurchaseBLObj.getMaxRepurchaseNo(regObject);
                MaxRep = MaxRep - 1;
                fileName = fileName + fromRepNoTextBox.Text.Trim().ToString() + "_" + MaxRep.ToString();
            }
            else if (fromRepNoTextBox.Text == "" && toRepNoTextBox.Text != "")
            {
                sbQuery.Append("AND  (REP_NO <= " + Convert.ToInt32(toRepNoTextBox.Text.Trim().ToString()) + ") ");
                fileName = fileName + "1_" + toRepNoTextBox.Text.Trim().ToString();
            }

            if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
            {
                sbQuery.Append(" AND ( REG_NO BETWEEN " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ") ");
                fileName = fileName + "REG_";
                fileName = fileName + fromRegNoTextBox.Text.Trim().ToString() + "_" + toRegNoTextBox.Text.Trim().ToString();
            }
            else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
            {
                sbQuery.Append(" AND ( REG_NO >= " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString())  + ") ");
                fileName = fileName + "REG_";
                int MaxRegNo = unitHolderRegBLObj.GetMaxRegNo(regObject);
                MaxRegNo = MaxRegNo - 1;
                fileName = fileName + fromRegNoTextBox.Text.Trim().ToString() + "_" + MaxRegNo.ToString();
            }
            else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
            {
                sbQuery.Append(" AND ( REG_NO <= " + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ") ");
                fileName = fileName + "REG_";
                fileName = fileName + "1_" + toRegNoTextBox.Text.Trim().ToString();
            }

            if (fromRepDateTextBox.Text != "" && toRepDateTextBox.Text != "")
            {
                sbQuery.Append(" AND ( REP_DT BETWEEN '" + Convert.ToDateTime(fromRepDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(toRepDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "') ");
            }
            else if (fromRepDateTextBox.Text != "" && toRepDateTextBox.Text == "")
            {
                sbQuery.Append(" AND ( REP_DT => '" + Convert.ToDateTime(fromRepDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "') ");
            }
            else if (fromRepDateTextBox.Text == "" && toRepDateTextBox.Text != "")
            {
                sbQuery.Append(" AND (REP_DT <= '" + Convert.ToDateTime(toRepDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "') ");
            }
            string fileDirectory = ConfigReader.ExportFileLocation.ToString();
            StreamWriter swFile = new StreamWriter(fileDirectory + "\\" + fileName + ".txt");
            try
            { 
            DataTable dtRepurchase = commonGatewayObj.Select(sbQuery.ToString());
            if (dtRepurchase.Rows.Count > 0)
            {
                sbFileHeader.Append("REP_NO~REP_DT~SL_TR_NO~REG_BK~REG_BR~REG_NO~REP_PRICE~QTY~USER_NM~ENT_DT~ENT_TM~REP_CERTIFICATE");
                sbFileHeader.Append("\r\n");
                for (int looper = 0; looper < dtRepurchase.Rows.Count; looper++)
                {

                    if (dtRepurchase.Rows[looper]["REP_NO"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtRepurchase.Rows[looper]["REP_NO"].ToString() + "~");
                    }
                    if (dtRepurchase.Rows[looper]["REP_DT"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtRepurchase.Rows[looper]["REP_DT"].ToString() + "~");
                    }
                    if (dtRepurchase.Rows[looper]["SL_TR_NO"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtRepurchase.Rows[looper]["SL_TR_NO"].ToString() + "~");
                    }
                    if (dtRepurchase.Rows[looper]["REG_BK"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtRepurchase.Rows[looper]["REG_BK"].ToString() + "~");
                    }
                    if (dtRepurchase.Rows[looper]["REG_BR"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtRepurchase.Rows[looper]["REG_BR"].ToString() + "~");
                    }
                    if (dtRepurchase.Rows[looper]["REG_NO"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtRepurchase.Rows[looper]["REG_NO"].ToString() + "~");
                    }
                    if (dtRepurchase.Rows[looper]["REP_PRICE"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtRepurchase.Rows[looper]["REP_PRICE"].ToString() + "~");
                    }
                    if (dtRepurchase.Rows[looper]["QTY"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtRepurchase.Rows[looper]["QTY"].ToString() + "~");
                    }
                    if (dtRepurchase.Rows[looper]["USER_NM"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtRepurchase.Rows[looper]["USER_NM"].ToString() + "~");
                    }
                    if (dtRepurchase.Rows[looper]["ENT_DT"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtRepurchase.Rows[looper]["ENT_DT"].ToString() + "~");
                    }
                    if (dtRepurchase.Rows[looper]["ENT_TM"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtRepurchase.Rows[looper]["ENT_TM"].ToString() + "~");
                    }
                    string repCertificate = reportObj.getTotalCertNo("SELECT * FROM REP_CERT_NO WHERE REP_NO="+ Convert.ToInt32(dtRepurchase.Rows[looper]["REP_NO"].ToString()) + " AND REG_BR='" + dtRepurchase.Rows[looper]["REG_BR"].ToString() + "' AND REG_BK='" + dtRepurchase.Rows[looper]["REG_BK"].ToString() + "' AND SL_TR_NO='" + dtRepurchase.Rows[looper]["SL_TR_NO"].ToString() + "' ", dtRepurchase.Rows[looper]["REG_BK"].ToString());
                    if (repCertificate != "")
                    {
                        sbFileData.Append(repCertificate.ToString());
                    }
                    else
                    {
                        sbFileData.Append(" ~");
                    }
                    sbFileData.Append("\r\n");
                }
                sbFileHeader.Append(sbFileData.ToString());
                swFile.Write(sbFileHeader.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Exported Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Export Failed: No Data Found');", true);
            }

          
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Export Failed Due to Eror:" + ex.ToString() + " ');", true);
        }
        finally
        {
            swFile.Close();
        }

    }

   
    protected void SummaryReportButton_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sbMaster = new StringBuilder();
            StringBuilder sbFilter = new StringBuilder();
            DataTable dtReportStatement = new DataTable();

            sbMaster.Append("SELECT NVL(REPURCHASE.REP_NO,0) AS REP_NO , TO_CHAR(REPURCHASE.REP_DT, 'DD-MON-YYYY') AS REP_DT, U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO AS RG_NO, U_MASTER.BO,");
            sbMaster.Append(" U_MASTER.HNAME, U_JHOLDER.JNT_NAME, U_MASTER.REG_BK || '/' || U_MASTER.REG_BR || '/' || U_MASTER.REG_NO AS REG_NO,");
            sbMaster.Append(" U_MASTER.ADDRS1, U_MASTER.ADDRS2, U_MASTER.CITY, U_MASTER.BK_BR_NM_CD, U_MASTER.BK_FLAG,REPURCHASE.QTY,");
            sbMaster.Append(" REPURCHASE.REP_PRICE AS RATE, REPURCHASE.QTY * REPURCHASE.REP_PRICE AS AMOUNT, U_MASTER.CIP, U_MASTER.ID_AC");
            sbMaster.Append(" FROM  U_MASTER INNER JOIN   (SELECT REG_BK,REG_BR,REG_NO,REP_NO,REP_PRICE,REP_DT,SUM(QTY) AS QTY FROM REPURCHASE GROUP BY  REG_BK,REG_BR,REG_NO,REP_NO,REP_PRICE,REP_DT ) ");
            sbMaster.Append(" REPURCHASE ON U_MASTER.REG_BK = REPURCHASE.REG_BK AND U_MASTER.REG_BR = REPURCHASE.REG_BR AND  U_MASTER.REG_NO = REPURCHASE.REG_NO");
            sbMaster.Append(" LEFT OUTER JOIN  U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND U_MASTER.REG_NO = U_JHOLDER.REG_NO");
            sbMaster.Append(" WHERE 1=1");

            sbMaster.Append(" AND (U_MASTER.REG_BK = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (U_MASTER.REG_BR = '" + branchCodeTextBox.Text.Trim().ToString().ToUpper() + "')");

            if (fromRepNoTextBox.Text != "" && toRepNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (REPURCHASE.REP_NO BETWEEN " + Convert.ToInt32(fromRepNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRepNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRepNoTextBox.Text != "" && toRepNoTextBox.Text == "")
            {
                sbFilter.Append(" AND (REPURCHASE.REP_NO>=" + Convert.ToInt32(fromRepNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRepNoTextBox.Text == "" && toRepNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (REPURCHASE.REP_NO<=" + Convert.ToInt32(toRepNoTextBox.Text.Trim().ToString()) + ")");
            }

            if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (U_MASTER.REG_NO BETWEEN " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
            {
                sbFilter.Append(" AND (U_MASTER.REG_NO >=" + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (U_MASTER.REG_NO <=" + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ")");
            }


            if (fromRepDateTextBox.Text != "" && toRepDateTextBox.Text != "")
            {
                sbFilter.Append(" AND ( REPURCHASE.REP_DT BETWEEN '" + Convert.ToDateTime(fromRepDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(toRepDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            else if (fromRepDateTextBox.Text != "" && toRepDateTextBox.Text == "")
            {
                sbFilter.Append(" AND ( REPURCHASE.REP_DT => '" + Convert.ToDateTime(fromRepDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            else if (fromRepDateTextBox.Text == "" && toRepDateTextBox.Text != "")
            {
                sbFilter.Append(" AND (REPURCHASE.REP_DT <= '" + Convert.ToDateTime(toRepDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }


            sbFilter.Append(" ORDER BY REPURCHASE.REP_NO ");
            sbMaster.Append(sbFilter.ToString());

            if (string.Compare(fundCodeTextBox.Text.Trim().ToString().ToUpper(), "IAMPH", true) == 0)
            {
                OracleConnection Conn = new OracleConnection(ConfigReader.PENSION.ToString());
                Conn.Open();
                dtReportStatement = commonGatewayObj.Select(sbMaster.ToString(), Conn);
            }
            else
            {
                dtReportStatement = commonGatewayObj.Select(sbMaster.ToString());
            }
            if (dtReportStatement.Rows.Count > 0)
            {
                Session["Report_Type"] = "SUMMARY";
                Session["dtReportStatement"] = dtReportStatement;
                Session["fundCode"] = fundCodeTextBox.Text.Trim().ToString();
                Session["branchCode"] = branchCodeTextBox.Text.Trim().ToString();

                ClientScript.RegisterStartupScript(this.GetType(), "UnitHolderInfo", "window.open('ReportViewer/UnitReportRepurchaseReportViewer.aspx')", true);

            }
            else
            {
                Session["Report_Type"] = null;
                Session["dtReportStatement"] = null;
                Session["fundCode"] = null;
                Session["branchCode"] = null;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('No data found');", true);
            }
        }
        catch (Exception ex)
        {

            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + ex.Message.Replace("'", "").ToString() + "');", true);
        }

    }
}
