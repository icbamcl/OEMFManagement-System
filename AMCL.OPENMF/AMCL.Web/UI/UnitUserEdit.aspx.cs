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

            menuCheckBoxList.DataSource = opendMFDAO.getDtMenuList();
            menuCheckBoxList.DataTextField = "M_NAME";
            menuCheckBoxList.DataValueField = "M_ID";
            menuCheckBoxList.DataBind();
        }
    
    }
    protected void saveButton_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtUserMenu = new DataTable();
            dtUserMenu.Columns.Add("M_ID", typeof(string));
            DataRow drUserMenu = dtUserMenu.NewRow();

            UnitUser userObj = new UnitUser();
            UnitHolderRegistration regObj = new UnitHolderRegistration();
            userObj.UserID = UserIDTextBox.Text.Trim().ToString();
            userObj.UserPassword = encrypt.Encrypt(passwordTextBox.Text.Trim().ToString());
            userObj.UserName = userNameTextBox.Text.Trim().ToString().ToUpper();
            regObj.FundCode = fundNameDropDownList.SelectedValue.ToString();
            regObj.BranchCode = branchNameDropDownList.SelectedValue.ToString();

            if (menuCheckBoxList.Items.Count > 0)
            {
                for (int loop = 0; loop < menuCheckBoxList.Items.Count; loop++)
                {
                    if(menuCheckBoxList.Items[loop].Selected)
                    {
                        drUserMenu = dtUserMenu.NewRow();
                        drUserMenu["M_ID"] = menuCheckBoxList.Items[loop].Value.ToString();
                        dtUserMenu.Rows.Add(drUserMenu);
                    }
                }
            }
            if (dtUserMenu.Rows.Count > 0)
            {
                if(userBLObj.CheckDuplicate(regObj,userObj))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('User Create Failed: Duplicate User ID');", true);
                }
                else
                {
                  //  userBLObj.saveUser(regObj, userObj, dtUserMenu);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "window.fnReset();alert ('User Created Successfully');", true);
                   // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('User Created Successfully');", true);

                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('User Create Failed: No Menu is Checked');", true);
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
