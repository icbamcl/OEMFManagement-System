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

public partial class UI_AMCLCommon : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        mnuMenu.Items.Clear();
        skmMenu.MenuItem item;
        skmMenu.MenuItem Subitem;
      //  skmMenu.MenuItem SSubitem;

        //Menu for Entry
        if (string.Compare((string)Session["UserID"].ToString(), "admin", true) == 0)
        {
            item = new skmMenu.MenuItem("User");
            Subitem = new skmMenu.MenuItem("Create User");
            Subitem.Url = "UnitUserCreate.aspx";
            item.SubItems.Add(Subitem);
            Subitem = new skmMenu.MenuItem("Edit");
            Subitem.Url = "UserEdit.aspx";
            item.SubItems.Add(Subitem);
            mnuMenu.Items.Add(item);

            //Subitem = new skmMenu.MenuItem("Trustee Fee");
            //SSubitem = new skmMenu.MenuItem("Fund Wise");
            //SSubitem.Url = "TrusteeFeeFundwise.aspx";
            //Subitem.SubItems.Add(SSubitem);
            //SSubitem = new skmMenu.MenuItem("Summery");
            //SSubitem.Url = "Pf1s1Entry.aspx";
            //Subitem.SubItems.Add(SSubitem);
            //item.SubItems.Add(Subitem);
            //Logout
        }
        item = new skmMenu.MenuItem("Change Password");
        item.Url = "UnitUserChangePassword.aspx";
        mnuMenu.Items.Add(item);
        item = new skmMenu.MenuItem("Home");
        item.Url = "Home.aspx";       
        mnuMenu.Items.Add(item);

        item = new skmMenu.MenuItem("Logout");
        item.Url = "../Default.aspx";
        mnuMenu.Items.Add(item);
        

    }
}
