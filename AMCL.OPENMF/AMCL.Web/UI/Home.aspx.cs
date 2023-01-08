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
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_Home : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitUserBL userBLObj = new UnitUserBL();
    LoginHistoryDAO logDAO = new LoginHistoryDAO();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        if (!IsPostBack)
        {
            
            
            UnitUser userObj = new UnitUser();
            
            userObj.UserID = (string)Session["UserID"].ToString();
            fundNameDropDownList.DataSource = opendMFDAO.dtFundList(userObj);
            fundNameDropDownList.DataTextField = "FUND_NM";
            fundNameDropDownList.DataValueField = "FUND_CD";
            fundNameDropDownList.DataBind();

            branchNameDropDownList.DataSource = opendMFDAO.dtBranchList(userObj);
            branchNameDropDownList.DataTextField = "BR_NM";
            branchNameDropDownList.DataValueField = "BR_CD";
            branchNameDropDownList.DataBind();
        }
    
    }
    protected void OkButton_Click(object sender, EventArgs e)
    {
        BaseClass bcContent = new BaseClass();
        UnitUser userObj = new UnitUser();
        UnitHolderRegistration regObj = new UnitHolderRegistration();      
        
        userObj.UserID = (string)Session["UserID"].ToString();
        //userObj.UserPassword = (string)Session["UserPassword"].ToString();
        regObj.FundCode = fundNameDropDownList.SelectedValue.ToString();
        regObj.BranchCode = branchNameDropDownList.SelectedValue.ToString();
        bool userLevelPermission = false;
        if (userObj.UserID.ToUpper().ToString() == "ADMIN")
        {
            userLevelPermission = true;
        }
        else
        {
            if (userBLObj.GetUserBranchPermission(regObj, userObj) && userBLObj.GetUserFundPermission(regObj, userObj))
            {
                userLevelPermission = true;
            }
            else
            {
                userLevelPermission = false;
            }
        }
       
        if (userLevelPermission)
        {
            bcContent.LoginID = (string)Session["UserID"];
            bcContent.LoginUserName = (string)Session["UserName"];
            bcContent.LoginTime = DateTime.Now;
            bcContent.Roles = (string)Session["UseRole"];
            bcContent.BranchCode = branchNameDropDownList.SelectedValue.ToString();
            bcContent.FundCode = fundNameDropDownList.SelectedValue.ToString();
            bcContent.CDS = opendMFDAO.getCDSStatus(fundNameDropDownList.SelectedValue.ToString());
            bcContent.AppId = 1;
            bcContent.DbServerType = "C";
            long loginSessionID= logDAO.Login(bcContent);
            bcContent.SessionID = loginSessionID;
            Session["BCContent"] = bcContent;
            Response.Redirect("UnitHome.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('You Have No Permission to Enter This Branch or Fund');", true);
        }
       

    }
}
