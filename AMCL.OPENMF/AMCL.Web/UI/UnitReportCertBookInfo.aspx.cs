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

public partial class UI_UnitReportCertBookInfo : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();    
    UnitUserBL userBLObj = new UnitUserBL();
    UnitCertBookInfoBL certBookInfoBL = new UnitCertBookInfoBL();
    CommonGateway commonGatewayObj = new CommonGateway();
    Message msgObj = new Message();
    BaseClass bcContent = new BaseClass();


    protected void Page_Load(object sender, EventArgs e)
    {
        string fundCode = "";
        string branchCode = "";

        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        fundCode = bcContent.FundCode.ToString();
        branchCode = bcContent.BranchCode.ToString();

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

            //  assignBalance(fundCode);            


        }
    }   
   
    protected void regCloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
    }




    protected void ShowDataButton_Click(object sender, EventArgs e)
    {
        StringBuilder sbQueryString = new StringBuilder();
        sbQueryString.Append("SELECT   ID, REQ_NO, LETTER_NO, FUND_CD, BR_CD, CERT_TYPE,TO_CHAR(DELIV_DT,'DD-MON-YYYY') AS DELIV_DT, BOOK_NO_START, BOOK_NO_END, CERT_NO_START, CERT_NO_END,  ");
        sbQueryString.Append(" BOOK_NO_OPNING, BOOK_NO_DISTRIBUTION, BOOK_NO_BALANCE, PER_BOOK_CERT_AMT, VALID, USER_NM,  ENT_DT, ENT_TM, REMARKS FROM   CERT_BOOK_INFO");
        sbQueryString.Append(" WHERE 1=1");
        if (fundNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbQueryString.Append(" AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString() + "'");
        }
        if (branchNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbQueryString.Append(" AND BR_CD='"+ branchNameDropDownList.SelectedValue.ToString()+"'");
        }
        if (certNoDropDownList.SelectedValue.ToString() != "0")
        {
            sbQueryString.Append(" AND CERT_TYPE='" + certNoDropDownList.SelectedValue.ToString() + "'");
        }
        sbQueryString.Append(" ORDER BY CERT_TYPE,CERT_NO_START ");
        DataTable dtCertBookInfo = commonGatewayObj.Select(sbQueryString.ToString());
        if (dtCertBookInfo.Rows.Count > 0)
        {
            dvSearchRegi.Visible = true;
            dgCertInfo.DataSource = dtCertBookInfo;
            dgCertInfo.DataBind();
        }
        else
        {
            dvSearchRegi.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);

        }
    }
}
