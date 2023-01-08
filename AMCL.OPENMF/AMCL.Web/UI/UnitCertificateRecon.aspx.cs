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
using System.IO;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitCertificateRecon : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();
    dividendDAO diviDAOObj = new dividendDAO();

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
        
       // spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
//            fundCodeTextBox.Text = fundCode.ToString();
//            branchCodeTextBox.Text = branchCode.ToString();

        
        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = opendMFDAO.dtFundList();
            fundNameDropDownList.DataTextField = "FUND_NM";
            fundNameDropDownList.DataValueField = "FUND_CD";
            fundNameDropDownList.DataBind();
            branchNameDropDownList.DataSource = opendMFDAO.dtBranchList();
            branchNameDropDownList.DataTextField = "BR_NM";
            branchNameDropDownList.DataValueField = "BR_CD";
            branchNameDropDownList.DataBind();
                         
                         
        }
    
    }






    protected void processButton_Click(object sender, EventArgs e)
    {
        StringBuilder sbQueryString = new StringBuilder();
        int certStartingNo = 0;
        int certEndingNo = 0;
        StringBuilder MissingCertNo = new StringBuilder();
       
       
        if (unUsedCertRadioButton.Checked)
        {
            sbQueryString.Append("SELECT CERT_NO FROM SALE_CERT WHERE 1=1 ");
            sbQueryString.Append(" AND REG_BK='" + fundNameDropDownList.SelectedValue.ToString() + "'");
            if (branchNameDropDownList.SelectedValue.ToString() != "0")
            {
                sbQueryString.Append(" AND REG_BR='" + branchNameDropDownList.SelectedValue.ToString()+"'");

            }
            if (certNoDropDownList.SelectedValue.ToString().ToUpper() != "0")
            {
                sbQueryString.Append(" AND CERT_TYPE='"+ certNoDropDownList.SelectedValue.ToString().ToUpper()+"'");
            }
            if (fromCertNoTextBox.Text != "" && toCertNoTextBox.Text != "")
            {
                sbQueryString.Append(" AND CERT_NO BETWEEN " +Convert.ToInt32( fromCertNoTextBox.Text.Trim()) + " AND " + Convert.ToInt32( toCertNoTextBox.Text ));
                certStartingNo = Convert.ToInt32(fromCertNoTextBox.Text.Trim());
                certEndingNo = Convert.ToInt32(toCertNoTextBox.Text.Trim());
            }
            else if (fromCertNoTextBox.Text == "" && toCertNoTextBox.Text != "")
            {
                sbQueryString.Append(" AND CERT_NO<= " + Convert.ToInt32(toCertNoTextBox.Text.Trim()));
                certEndingNo = Convert.ToInt32(toCertNoTextBox.Text.Trim());
                certStartingNo = 1;
            }
            else if (fromCertNoTextBox.Text != "" && toCertNoTextBox.Text == "")
            {
                sbQueryString.Append(" AND CERT_NO>= " + Convert.ToInt32(fromCertNoTextBox.Text.Trim()));
                certEndingNo = 1;
                certStartingNo = Convert.ToInt32(fromCertNoTextBox.Text.Trim());
            }
           
            sbQueryString.Append(" ORDER BY CERT_NO ");

            DataTable dtTotalCert = commonGatewayObj.Select(sbQueryString.ToString());
            if (dtTotalCert.Rows.Count > 0)
            {              

                if (fromCertNoTextBox.Text == "" && toCertNoTextBox.Text == "")
                {
                    certStartingNo = 1;
                    certEndingNo = dtTotalCert.Rows.Count;
                }


                for (int loop = 0; loop < dtTotalCert.Rows.Count; loop++)
                {

                   
                    if (certStartingNo != Convert.ToInt32(dtTotalCert.Rows[loop]["CERT_NO"].ToString()))
                    {
                        if (MissingCertNo.ToString() == "")
                        {
                            MissingCertNo.Append(certStartingNo);
                        }
                        else
                        {
                            MissingCertNo.Append(" , " + certStartingNo);
                        }                     
                        loop = loop - 1;
                    }
                    if (certStartingNo < certEndingNo)
                    {
                        certStartingNo++;
                    }
                }
                if (certEndingNo > Convert.ToInt32(dtTotalCert.Rows[dtTotalCert.Rows.Count-1]["CERT_NO"].ToString()))
                {
                    long MidStratCertNo = Convert.ToInt32(dtTotalCert.Rows[dtTotalCert.Rows.Count - 1]["CERT_NO"].ToString()) + 1;
                    //MissingCertNo.Append("," + dtTotalCert.Rows.Count+1 + "-" + certEndingNo);
                    if (MissingCertNo.ToString() == "")
                    {
                        MissingCertNo.Append(MidStratCertNo.ToString() + " - " + certEndingNo);
                    }
                    else
                    {
                        MissingCertNo.Append(" , " + MidStratCertNo.ToString() + "-" + certEndingNo);
                    }      
                }
                if (MissingCertNo.ToString()!="")
                {

                    Session["MissingCertNo"] = MissingCertNo;
                    Session["certType"] = certNoDropDownList.SelectedValue.ToString().ToUpper();
                    ClientScript.RegisterStartupScript(this.GetType(), "UnitHolderInfo", "window.open('ReportViewer/UnitReportCertProcessReportViewer.aspx')", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('No Unused Certificate Found');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('No Data Found ');", true);
            }
        }


    }
}
