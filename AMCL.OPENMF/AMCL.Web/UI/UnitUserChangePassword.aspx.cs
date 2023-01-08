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

public partial class UI_UnitUserCreate : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    EncryptDecrypt encrypt = new EncryptDecrypt();
    UnitUserBL userBLObj = new UnitUserBL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {           
            Response.Redirect("../Default.aspx");
            
        }
        else
        {
            string userID = (string)Session["UserID"].ToString();

            if (!IsPostBack)
            {
                UserIDTextBox.Text = (string)Session["UserID"].ToString();

            }
           
        }
       
    
    }
    protected void saveButton_Click(object sender, EventArgs e)
    {
        try
        {
            UnitUser userObj = new UnitUser();
            UnitHolderRegistration regObj = new UnitHolderRegistration();
            userObj.UserID = UserIDTextBox.Text.Trim().ToString();
            userObj.UserPassword = encrypt.Encrypt(oldPasswordTextBox.Text.Trim().ToString());
            userObj.UserChangePassword = encrypt.Encrypt(confirmPasswordTextBox.Text.Trim().ToString());
            if (string.Compare((string)Session["UserID"].ToString().ToUpper(), UserIDTextBox.Text.Trim().ToString().ToUpper()) == 0 || string.Compare("ADMIN",(string)Session["UserID"].ToString().ToUpper() ) == 0)
            {
                if (!userBLObj.CheckExistUser(userObj))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('Password Change Failed: Invalid User ID or Password');", true);
                }
                else
                {
                    userBLObj.updateUserPassword(userObj);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "window.fnReset();alert ('Password Changed Successfully');", true);
                    // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('User Created Successfully');", true);

                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('You have no permission to change password for this User');", true);
            }
         
        }
        catch(Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('User Create Failed:" + ex.Message.Replace("'", "").ToString() + "');", true);

        }
   

    }
    protected void regCloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
    }
}
