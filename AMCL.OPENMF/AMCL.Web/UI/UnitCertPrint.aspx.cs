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
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitCertPrint : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();

        string fundCode = "";
        string branchCode = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
              
        UnitHolderRegistration regObj = new UnitHolderRegistration();
       
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        fundCode = bcContent.FundCode.ToString();
        branchCode = bcContent.BranchCode.ToString();
        fundCodeTextBox.Text = fundCode.ToString();
        branchCodeTextBox.Text = branchCode.ToString();
        
        if (!IsPostBack)
        {
            SaleNoTextBox.Focus();
        }
    
    }
   
    
    protected void CloseButton_Click(object sender, EventArgs e)
    {         
        Response.Redirect("UnitHome.aspx");           
    }

    protected void PrintButton_Click(object sender, EventArgs e)
    {
        string certType = "";
       
        int lineNumber=0;
        if (SaleNoRadioButton.Checked)
        {
            certType = "SALE";

        }
        else if (RenNoRadioButton.Checked)
        {
            certType = "RENE";
           
        }
        else if (TrNoRadioButton.Checked)
        {

            certType = "TRAN";
            lineNumber = Convert.ToInt16(LineNoTextBox.Text.ToString().Trim());
            
        }
        DataTable dtSaleCertPrintReport = (DataTable)Session["dtHolderInfo"];

        if (dtSaleCertPrintReport.Rows.Count > 0)
        {
            Hashtable htPrint =new Hashtable();
            
            htPrint.Add("CERT_PRINT_BY", userObj.UserID.ToString());
            htPrint.Add("CERT_PRINT_TIME", DateTime.Now);
            if (certType == "SALE")
            {
                commonGatewayObj.CommitTransaction();
                commonGatewayObj.Update(htPrint, "SALE", "REG_BK='" + fundCode + "'  AND REG_BR='" + branchCode + "' AND SL_NO=" + Convert.ToInt32(SaleNoTextBox.Text.Trim().ToString()));
                commonGatewayObj.CommitTransaction();
            }
            else if (certType == "RENE")
            {
                commonGatewayObj.CommitTransaction();
                commonGatewayObj.Update(htPrint, "RENEWAL", "REG_BK='" + fundCode + "'  AND REG_BR='" + branchCode + "' AND REN_NO='" + RenNoTextBox.Text.Trim().ToString() + "'");
                commonGatewayObj.CommitTransaction();
            }

            if (certType == "TRAN")
            {
                Session["lineNumber"] = lineNumber;
                Session["dtSaleCertPrintReport"] = dtSaleCertPrintReport;
                Session["certType"] = certType;
                ClientScript.RegisterStartupScript(this.GetType(), "TransferCert", "window.open('ReportViewer/UnitTransCertPrintReportViewer.aspx')", true);
            }
            else
            {
                if (dtSaleCertPrintReport.Rows[0]["ID_FLAG"].ToString().ToUpper().ToString() == "Y" && certType.ToUpper().ToString() == "SALE")
                {
                    if (dtSaleCertPrintReport.Rows[0]["ID_AC"].Equals(DBNull.Value) || dtSaleCertPrintReport.Rows[0]["ID_BK_NM_CD"].Equals(DBNull.Value) || dtSaleCertPrintReport.Rows[0]["ID_BK_BR_NM_CD"].Equals(DBNull.Value))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please Update ID Account Information');", true);
                    }
                    else
                    {
                        Session["dtSaleCertPrintReport"] = dtSaleCertPrintReport;
                        Session["certType"] = certType;
                        Session["lineNumber"] = lineNumber;
                        ClientScript.RegisterStartupScript(this.GetType(), "DividendWarrant", "window.open('ReportViewer/UnitSaleCertPrintReportViewer.aspx')", true);
                    }

                }
                else
                {

                    Session["dtSaleCertPrintReport"] = dtSaleCertPrintReport;
                    Session["certType"] = certType;
                    Session["lineNumber"] = lineNumber;
                    ClientScript.RegisterStartupScript(this.GetType(), "DividendWarrant", "window.open('ReportViewer/UnitSaleCertPrintReportViewer.aspx')", true);
                }

            }
        }
        else
        {
            Session["dtSaleCertPrintReport"] = null;
            Session["certType"] = null;
            Session["lineNumber"] = null;
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data Found');", true);
        }
    }

    protected void findButton_Click(object sender, EventArgs e)
    {
        StringBuilder sbQueryString = new StringBuilder();
      
       
        if (SaleNoRadioButton.Checked)
        {
            sbQueryString.Append("SELECT U_MASTER.REG_NO, U_MASTER.REG_BK || '/' || U_MASTER.REG_BR || '/' || U_MASTER.REG_NO AS REGI_NO, SALE.SL_NO, TO_CHAR(SALE.SL_DT, 'DD-MON-YYYY') AS SL_DT,U_MASTER.ID_FLAG,U_MASTER.ID_AC, U_MASTER.ID_BK_NM_CD,U_MASTER.ID_BK_BR_NM_CD,");
            sbQueryString.Append(" DECODE(U_MASTER.ID_FLAG, 'Y', BANK_NAME.BANK_NAME || ', ' || BANK_BRANCH.BRANCH_NAME ,U_MASTER.HNAME) HNAME, U_MASTER.FMH_NAME, U_MASTER.REG_BK, U_MASTER.REG_BR,DECODE(U_MASTER.ID_FLAG, 'Y',' ',U_JHOLDER.JNT_NAME)JNT_NAME,DECODE(U_MASTER.ID_FLAG, 'Y', U_MASTER.HNAME||','||','||U_JHOLDER.JNT_NAME||','||'ID AC NO: ' || U_MASTER.ID_AC ||','||' ', U_MASTER.ADDRS1) AS ADDRS1,");
            sbQueryString.Append(" DECODE(U_MASTER.ID_FLAG, 'Y',BANK_BRANCH.BRANCH_ADDRS1 || ' ' || BANK_BRANCH.BRANCH_ADDRS2,U_MASTER.ADDRS2) AS ADDRS2,");
            sbQueryString.Append(" DECODE(U_MASTER.ID_FLAG, 'Y', BANK_BRANCH.BRANCH_DISTRICT, U_MASTER.CITY) AS CITY, SALE.QTY,SALE.SL_TYPE ");
            sbQueryString.Append(" FROM  U_JHOLDER RIGHT OUTER JOIN  BANK_NAME INNER JOIN  BANK_BRANCH ON BANK_NAME.BANK_CODE = BANK_BRANCH.BANK_CODE RIGHT OUTER JOIN   SALE INNER JOIN ");
            sbQueryString.Append(" U_MASTER ON SALE.REG_BK = U_MASTER.REG_BK AND SALE.REG_BR = U_MASTER.REG_BR AND SALE.REG_NO = U_MASTER.REG_NO ON BANK_BRANCH.BANK_CODE = U_MASTER.ID_BK_NM_CD AND BANK_BRANCH.BRANCH_CODE = U_MASTER.ID_BK_BR_NM_CD ON");
            sbQueryString.Append(" U_JHOLDER.REG_BK = U_MASTER.REG_BK AND U_JHOLDER.REG_BR = U_MASTER.REG_BR AND U_JHOLDER.REG_NO = U_MASTER.REG_NO");

            sbQueryString.Append(" WHERE  (SALE.SL_NO = " + Convert.ToInt32(SaleNoTextBox.Text.Trim().ToString()) + ") AND (SALE.REG_BK = '" + fundCode.ToString() + "') AND (SALE.REG_BR = '" + branchCode.ToString() + "')");
            RenNoTextBox.Text = "";
            TrNoTextBox.Text = "";
            LineNoTextBox.Text = "";
        }
        else if (RenNoRadioButton.Checked)
        {
            sbQueryString.Append(" SELECT U_MASTER.REG_NO, U_MASTER.REG_BK || '/' || U_MASTER.REG_BR || '/' || U_MASTER.REG_NO AS REGI_NO,'RN: '||RENEWAL.REN_NO AS SL_NO,TO_CHAR(RENEWAL.REN_DT, 'DD-MON-YYYY') AS SL_DT,U_MASTER.ID_FLAG,");
            sbQueryString.Append(" U_MASTER.HNAME,U_MASTER.FMH_NAME, U_MASTER.REG_BK, U_MASTER.REG_BR, U_JHOLDER.JNT_NAME, U_MASTER.ADDRS1, U_MASTER.ADDRS2,U_MASTER.CITY,RENEWAL.QTY,'REN' AS SL_TYPE ");
            sbQueryString.Append(" FROM U_MASTER LEFT OUTER JOIN U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND  U_MASTER.REG_NO = U_JHOLDER.REG_NO  LEFT OUTER JOIN");
            sbQueryString.Append(" RENEWAL ON U_MASTER.REG_BK = RENEWAL.REG_BK AND U_MASTER.REG_BR = RENEWAL.REG_BR AND U_MASTER.REG_NO = RENEWAL.REG_NO");
            sbQueryString.Append(" WHERE (RENEWAL.REN_NO = '"+RenNoTextBox.Text.Trim().ToString()+"') AND (RENEWAL.REG_BK = '" + fundCode.ToString() + "') AND (RENEWAL.REG_BR = '" + branchCode.ToString() + "')");
            SaleNoTextBox.Text = "";
            TrNoTextBox.Text = "";
            LineNoTextBox.Text = "";
        }
        else if (TrNoRadioButton.Checked)
        {
            string transferType = "T";
            if(deathRadioButton.Checked)
            {
                transferType = "D";
            }
            
            sbQueryString.Append(" SELECT TRANSFER.REG_BK_O || '/' || TRANSFER.REG_BR_O || '/' || TRANSFER.REG_NO_O AS TFEROR_REG_NO, U_MASTER.HNAME, U_MASTER.ID_FLAG,");
            sbQueryString.Append(" U_JHOLDER.JNT_NAME, U_MASTER.ADDRS1 || ' ' || U_MASTER.ADDRS2 || ' ' || U_MASTER.CITY AS ADDRESS,U_MASTER.REG_BK || '/' || U_MASTER.REG_BR || '/' || U_MASTER.REG_NO AS TFREE_REG_NO, TRANSFER.TR_NO, TO_CHAR(TRANSFER.TR_DT, 'DD-MON-YYYY') AS TR_DT,");
            sbQueryString.Append(" U_MASTER.ADDRS1,U_MASTER.ADDRS2,U_MASTER.CITY, U_MASTER.REG_NO,TRANSFER.QTY  FROM TRANSFER INNER JOIN U_MASTER ON TRANSFER.REG_BK_I = U_MASTER.REG_BK AND TRANSFER.REG_BR_I = U_MASTER.REG_BR AND");
            sbQueryString.Append(" TRANSFER.REG_NO_I = U_MASTER.REG_NO LEFT OUTER JOIN U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND U_MASTER.REG_NO = U_JHOLDER.REG_NO");
            sbQueryString.Append(" WHERE (TRANSFER.BR_CODE = '" + fundCode.ToString() + "_" + branchCode.ToString() + "') AND  (TRANSFER.TR_NO = "+Convert.ToInt32(TrNoTextBox.Text.Trim().ToString())+") AND TR_TYPE='"+ transferType.ToString().ToUpper()+"'");
           
            SaleNoTextBox.Text = "";
            RenNoTextBox.Text = "";
        }
        DataTable dtCertPrintReport = commonGatewayObj.Select(sbQueryString.ToString());
        if (dtCertPrintReport.Rows.Count > 0)
        {
            displayHolderInformation(dtCertPrintReport);
        }
        else
        {
            ClearText();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
        }
    }
    public void displayHolderInformation(DataTable dtHolderInfo)
    {
        regNoTextBox.Text =dtHolderInfo.Rows[0]["REG_NO"].Equals(DBNull.Value) ? "": dtHolderInfo.Rows[0]["REG_NO"].ToString();
        NoUnitsLabel.Text = dtHolderInfo.Rows[0]["QTY"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[0]["QTY"].ToString();
        HolderNameTextBox.Text = dtHolderInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[0]["HNAME"].ToString();
        JHolderNameTextBox.Text = dtHolderInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[0]["JNT_NAME"].ToString();
        Address1TextBox.Text = dtHolderInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[0]["ADDRS1"].ToString();
        Address2TextBox.Text = dtHolderInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[0]["ADDRS2"].ToString();
        CityTextBox.Text = dtHolderInfo.Rows[0]["CITY"].Equals(DBNull.Value) ? "" : dtHolderInfo.Rows[0]["CITY"].ToString();
        Session["dtHolderInfo"] = dtHolderInfo;
        
    }
    public void ClearText()
    {
        regNoTextBox.Text ="";
        NoUnitsLabel.Text = "";
        HolderNameTextBox.Text = "";
        JHolderNameTextBox.Text = "";
        Address1TextBox.Text = "";
        Address2TextBox.Text = "";
        CityTextBox.Text = "";
        Session["dtHolderInfo"] = null;
    }
}
