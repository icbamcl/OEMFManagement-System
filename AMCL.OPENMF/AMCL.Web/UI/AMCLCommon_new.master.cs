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

public partial class UI_AMCLCommon_new : System.Web.UI.MasterPage
{
    CommonGateway commonGatewayObj = new CommonGateway();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
                      
            DataTable dt = this.BindMenuData(0);
            DynamicMenuControlPopulation(dt, 0, null);
        }
       
     
        
        

    }
   
    protected DataTable BindMenuData(int parentmenuId)
    {
        DataTable dtMenu = commonGatewayObj.Select("SELECT M_ID AS MenuId, M_PARENT_ID AS  ParentId,M_CAPTION AS Title,M_NAME AS Description,M_URL AS URL FROM MENU WHERE VALID='Y'AND PROJECT_ID=1  ORDER BY M_ID ");
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
