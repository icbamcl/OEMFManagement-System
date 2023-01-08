using System;
using System.IO;
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

public partial class UnitCommon_new : System.Web.UI.MasterPage
{
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    BaseClass bcContent = new BaseClass();
    string CDSStatus = "";
    string UserID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        string FundCode = "";
        string BranchCode = "";
        string UserName = "";                      

        if (BaseContent.IsSessionExpired())
        {            
            Response.Redirect("../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];
        UserID=bcContent.LoginID;
        FundCode = bcContent.FundCode;
        BranchCode = bcContent.BranchCode;
        UserName = bcContent.LoginUserName;
        CDSStatus = bcContent.CDS.ToString().ToUpper();



        if (!IsPostBack)
        {

            DataTable dt = this.BindMenuData(0);
            DynamicMenuControlPopulation(dt, 0, null);


            if (FundCode == null || BranchCode == null)
            {

                Response.Redirect("../Default.aspx");
            }
            else
            {
                string cssColourCode = opendMFDAO.getFundCSSColourCode(FundCode.ToUpper());
                tdFundName.InnerHtml = "<span style=\"color:" + cssColourCode + ";\">" + opendMFDAO.GetFundName(FundCode) + "</span>";
                tdUser.InnerHtml = "<span style=\"color:" + cssColourCode + "\"> User: <b>" + UserName + "</b></span>";
                tdBranch.InnerHtml = "<span style=\"color:" + cssColourCode + "\">Branch: <b>" + opendMFDAO.GetBranchName(BranchCode) + "</b></span>";


            }
        }
       
    }
    protected DataTable BindMenuData(int parentmenuId)
    {

        string queryStringParent = "";
        UserID = bcContent.LoginID;
        if (CDSStatus == "Y")
        {
            if (string.Compare(UserID, "admin", false) == 0)
            {
                queryStringParent = "SELECT M_ID AS MenuId, M_PARENT_ID AS  ParentId,M_CAPTION AS Title,M_NAME AS Description,M_URL AS URL FROM MENU WHERE   VALID='Y'AND PROJECT_ID=1 AND CDS='Y'  ORDER BY M_ID";
            }
            else
            {
                queryStringParent = "SELECT USER_MENU.M_ID AS MenuId, MENU.M_PARENT_ID AS ParentId, MENU.M_CAPTION AS Title, MENU.M_NAME AS Description, MENU.M_URL AS URL FROM MENU, USER_MENU WHERE MENU.M_ID = USER_MENU.M_ID AND (MENU.VALID = 'Y') AND (MENU.PROJECT_ID = 1) AND (MENU.CDS = 'Y') AND (USER_MENU.USER_ID = '" + UserID.ToString() + "') ORDER BY MENU.M_ID ";
            }
        }
        else
        {
            if (string.Compare(UserID, "admin", false) == 0)
            {
                queryStringParent = "SELECT M_ID AS MenuId, M_PARENT_ID AS  ParentId,M_CAPTION AS Title,M_NAME AS Description,M_URL AS URL FROM MENU WHERE VALID='Y'AND PROJECT_ID=1 AND M_ID=NVL(NON_CDS_M_ID,M_ID) ORDER BY M_ID";
               // queryStringParent = "SELECT DISTINCT( M_PARENT_ID) AS PARENT_ID FROM MENU WHERE VALID='Y' and M_PARENT_ID<>0 AND PROJECT_ID=1 AND M_ID=NVL(NON_CDS_M_ID,M_ID) ORDER BY PARENT_ID";
            }
            else
            {
                queryStringParent = "SELECT USER_MENU.M_ID AS MenuId, MENU.M_PARENT_ID AS ParentId, MENU.M_CAPTION AS Title, MENU.M_NAME AS Description, MENU.M_URL AS URL FROM MENU, USER_MENU WHERE MENU.M_ID = USER_MENU.M_ID AND (MENU.VALID = 'Y') AND (MENU.PROJECT_ID = 1) AND (USER_MENU.USER_ID = '" + UserID.ToString() + "') ORDER BY MENU.M_ID ";
               // queryStringParent = "SELECT DISTINCT( M_PARENT_ID) AS PARENT_ID FROM MENU WHERE M_ID IN (SELECT M_ID FROM USER_MENU WHERE USER_ID='" + UserID.ToString() + "' AND VALID IS NULL) AND  VALID='Y' and M_PARENT_ID<>0 ORDER BY PARENT_ID";
            }
        }
        DataTable dtMenu = commonGatewayObj.Select(queryStringParent.ToString());
        var dv = dtMenu.DefaultView;
        dv.RowFilter = "ParentId='" + parentmenuId + "'";
        var newdt = dv.ToTable();
        return newdt;
    }
    protected void DynamicMenuControlPopulation(DataTable dt, int parentMenuId, MenuItem parentMenuItem)
    {
        string currentPage = Path.GetFileName(Request.Url.AbsolutePath);
        foreach (DataRow row in dt.Rows)
        {
            MenuItem menuItem = new MenuItem
            {
                Value = row["MenuId"].ToString(),
                Text = row["Title"].ToString(),
                NavigateUrl = row["URL"].ToString(),
                Selected = row["URL"].ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
            };
            if (parentMenuId == 0)
            {
                Menu1.Items.Add(menuItem);
                DataTable dtChild = this.BindMenuData(int.Parse(menuItem.Value));
                DynamicMenuControlPopulation(dtChild, int.Parse(menuItem.Value), menuItem);
            }
            else
            {

                parentMenuItem.ChildItems.Add(menuItem);
                DataTable dtChild = this.BindMenuData(int.Parse(menuItem.Value));
                DynamicMenuControlPopulation(dtChild, int.Parse(menuItem.Value), menuItem);

            }
        }
    }

}
