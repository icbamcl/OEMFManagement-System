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

public partial class UI_AMCLCommon : System.Web.UI.MasterPage
{
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    BaseClass bcContent = new BaseClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserID="";
        string FundCode = "";
        string BranchCode = "";
        string UserName = "";
        string queryStringParent = "";
        string CDSStatus = "";
        

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
        
        mnuMenu.Items.Clear();
        skmMenu.MenuItem item;
        skmMenu.MenuItem Subitem;
        if (CDSStatus == "Y")
        {
            if (string.Compare(UserID, "admin", false) == 0)
            {
                queryStringParent = "SELECT DISTINCT( M_PARENT_ID) AS PARENT_ID FROM MENU WHERE VALID='Y' and M_PARENT_ID<>0 AND PROJECT_ID=1 AND CDS='Y' ORDER BY PARENT_ID";
            }
            else
            {
                queryStringParent = "SELECT DISTINCT( M_PARENT_ID) AS PARENT_ID FROM MENU WHERE NON_CDS_M_ID IN (SELECT M_ID FROM USER_MENU WHERE USER_ID='" + UserID.ToString() + "' AND VALID IS NULL ) AND  VALID='Y' and M_PARENT_ID <>0 AND CDS='Y'  ORDER BY PARENT_ID";
            }
        }
        else
        {
            if (string.Compare(UserID, "admin", false) == 0)
            {
                queryStringParent = "SELECT DISTINCT( M_PARENT_ID) AS PARENT_ID FROM MENU WHERE VALID='Y' and M_PARENT_ID<>0 AND PROJECT_ID=1 AND M_ID=NVL(NON_CDS_M_ID,M_ID) ORDER BY PARENT_ID";
            }
            else
            {
                queryStringParent = "SELECT DISTINCT( M_PARENT_ID) AS PARENT_ID FROM MENU WHERE M_ID IN (SELECT M_ID FROM USER_MENU WHERE USER_ID='" + UserID.ToString() + "' AND VALID IS NULL) AND  VALID='Y' and M_PARENT_ID<>0 ORDER BY PARENT_ID";
            }
        }
        DataTable dtSubMenu = new DataTable();
        DataTable dtParentMenu = new DataTable();
        string parentItem = "";
        if (queryStringParent!="")
        {            
            dtParentMenu = commonGatewayObj.Select(queryStringParent.ToString());

        }
        if (dtParentMenu.Rows.Count > 0)
        {
            for (int looper = 0; looper < dtParentMenu.Rows.Count; looper++)
            {
                parentItem = itemString(Convert.ToInt32(dtParentMenu.Rows[looper]["PARENT_ID"].ToString())).ToString();
                item = new skmMenu.MenuItem(parentItem.ToString());
                dtSubMenu = getdtSubMenu(Convert.ToInt32(dtParentMenu.Rows[looper]["PARENT_ID"].ToString()), UserID, BranchCode.ToString().ToUpper(), FundCode.ToString().ToUpper(),CDSStatus);
                if (dtSubMenu.Rows.Count == 1 && (dtSubMenu.Rows[0]["M_PARENT_ID"].ToString() == dtSubMenu.Rows[0]["M_ID"].ToString()))
                {                    
                    item.Url = dtSubMenu.Rows[0]["M_URL"].ToString();
                }
                else if (dtSubMenu.Rows.Count>0)
                {
                    for (int loop = 0; loop < dtSubMenu.Rows.Count; loop++)
                    {
                        Subitem = new skmMenu.MenuItem(dtSubMenu.Rows[loop]["M_CAPTION"].ToString());
                        Subitem.Url = dtSubMenu.Rows[loop]["M_URL"].ToString();
                        item.SubItems.Add(Subitem);
                    }
                }
                mnuMenu.Items.Add(item);
            }
        }

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
        mnuMenu1.Items.Clear();                    

        //Home
        item = new skmMenu.MenuItem("Home");
        item.Url = "Home.aspx";
        mnuMenu1.Items.Add(item);

        //Logout
        item = new skmMenu.MenuItem("Logout");
        item.Url = "../Default.aspx";
        mnuMenu1.Items.Add(item);
    }
    public string itemString(int parentID)
    {       
        DataTable dtItemString = commonGatewayObj.Select("SELECT M_CAPTION FROM MENU WHERE M_ID=" + parentID);
        string itemString = dtItemString.Rows[0]["M_CAPTION"].ToString();
        return itemString;
    }
    public DataTable getdtSubMenu(int parentID, string UserID, string branchCode, string fundCode,string CDSStatus)
    {
        string queryString = "";
        if (CDSStatus == "Y")
        {
            if (string.Compare(UserID, "admin", true) == 0)
            {
                queryString = "SELECT * FROM MENU WHERE M_PARENT_ID=" + parentID + " AND VALID='Y' and M_PARENT_ID<>0  AND PROJECT_ID=1 AND CDS='Y' ORDER BY M_ID";
            }
            else
            {
               
               queryString = "SELECT * FROM MENU WHERE M_PARENT_ID=" + parentID + " AND NON_CDS_M_ID IN (SELECT M_ID FROM USER_MENU WHERE USER_ID='" + UserID.ToString() + "' AND VALID IS NULL) AND VALID='Y' AND PROJECT_ID=1  and M_PARENT_ID <>0   AND CDS='Y' ORDER BY M_ID";
               
            }
        }
        else
        {
            if (string.Compare(UserID, "admin", true) == 0)
            {
                queryString = "SELECT * FROM MENU WHERE M_PARENT_ID=" + parentID + " AND VALID='Y' and M_PARENT_ID<>0  AND PROJECT_ID=1  ORDER BY M_ID";
            }
            else
            {
                if (string.Compare(fundCode, "IAMPH") == 0)
                {
                    queryString = "SELECT * FROM MENU WHERE M_PARENT_ID=" + parentID + " AND M_ID IN (SELECT M_ID FROM USER_MENU WHERE USER_ID='" + UserID.ToString() + "' AND VALID IS NULL AND M_ID NOT IN(4,12) )AND PROJECT_ID=1  AND VALID='Y' AND M_PARENT_ID<>0  ORDER BY M_ID";
                }
                else
                {
                    queryString = "SELECT * FROM MENU WHERE M_PARENT_ID=" + parentID + " AND M_ID IN (SELECT M_ID FROM USER_MENU WHERE USER_ID='" + UserID.ToString() + "' AND VALID IS NULL) AND VALID='Y' AND PROJECT_ID=1  and M_PARENT_ID<>0 ORDER BY M_ID";
                }
            }
        }
        DataTable dtSubMenu = commonGatewayObj.Select(queryString);
        return dtSubMenu;
    }
}
