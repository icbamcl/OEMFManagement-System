using System;
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
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;



public partial class _Default : System.Web.UI.Page 
{
    CommonGateway commonGatewayObj = new CommonGateway();
    EncryptDecrypt encrypt = new EncryptDecrypt();
    UnitReport reportObj = new UnitReport();
    OMFDAO opendMFDAO = new OMFDAO();
    protected void Page_Load(object sender, EventArgs e)
    {       
        loginErrorLabel.Visible = false;
        loginIDTextBox.Focus();

        if(!IsPostBack)
        {
            DataTable dtFundInfoDetails = reportObj.dtFundInfoDetails("   ");
            DataTable dtPriceDetails = dtPriceDetails = reportObj.dtPriceDetails(" AND FUND_CD='" + dtFundInfoDetails.Rows[0]["FUND_CD"].ToString().ToUpper() + "'  AND REFIX_DT=(SELECT MAX (REFIX_DT) FROM PRICE_REFIX WHERE FUND_CD='" + dtFundInfoDetails.Rows[0]["FUND_CD"].ToString().ToUpper() + "') ");
            for (int looper = 1; looper < dtFundInfoDetails.Rows.Count; looper++)
            {
                DataTable dtprice = reportObj.dtPriceDetails(" AND FUND_CD='" + dtFundInfoDetails.Rows[looper]["FUND_CD"].ToString().ToUpper() + "'  AND REFIX_DT=(SELECT MAX (REFIX_DT) FROM PRICE_REFIX WHERE FUND_CD='" + dtFundInfoDetails.Rows[looper]["FUND_CD"].ToString().ToUpper() + "') ");
                dtPriceDetails.Merge(dtprice);
            }          
            priceListListGridView.DataSource = dtPriceDetails;
            priceListListGridView.DataBind();
        }


        if (IsPostBack)
        {
           
                if (IsUesrCheck(loginIDTextBox.Text.Trim().ToString(), encrypt.Encrypt(loginPasswardTextBox.Text.Trim().ToString())))
                {
                
                    Response.Redirect("UI/Home.aspx");
                }
                else
                {
                    loginErrorLabel.Visible = true;
                    loginErrorLabel.Text = "Invalid LoginID or Passward";
                    loginIDTextBox.Text = "";
                    loginPasswardTextBox.Text = "";
                 
                }
           
        }
        else
        {
            if (Session["BCContent"] != null)
            {
                BaseClass bcContent = new BaseClass();
                bcContent = (BaseClass)Session["BCContent"];

                LoginHistoryDAO lDao = new LoginHistoryDAO();
                long SessionID = bcContent.SessionID;
                lDao.Logout(SessionID);
                Session["BCContent"] = null;               
            }
        }

    }
  
    public bool IsUesrCheck(string loginID,string loginPassword)
    {


        DataTable dtUserInfo = new DataTable();
        dtUserInfo = commonGatewayObj.Select("SELECT * FROM USER_INFO WHERE USER_ID='" + loginID + "' AND USER_PASS='" + loginPassword + "' AND UPPER(USER_STATUS)='V'");
        if (dtUserInfo.Rows.Count > 0)
        {
            string UserID = dtUserInfo.Rows[0]["USER_ID"].ToString();
            string UserName = dtUserInfo.Rows[0]["USER_NM"].ToString();
            string UserLevel = dtUserInfo.Rows[0]["USER_LEVEL"].ToString();           
            Session["UserID"] = UserID;
            Session["UserName"] = UserName;
            Session["UseRole"] = UserLevel;    
            return true;
        }
        else
        {
            return false;
        }
    }
}
