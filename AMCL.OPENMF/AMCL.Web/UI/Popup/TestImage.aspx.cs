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
using System.IO;
using AMCL.COMMON;
using AMCL.DL;

public partial class UI_TestImage : System.Web.UI.Page
{
    OMFDAO omfDAOObj = new OMFDAO();
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        string regNo="";
        string fundCode="";
        string branchCode="";
        string unitHolderName="";
        string branchName="";
        string fundName="";
        if(Request.QueryString["reg"]!=null)
        {
            regNo=Request.QueryString["reg"].ToString();
        }
         if(Request.QueryString["fund"]!=null)
         {
             fundName=omfDAOObj.GetFundName(Request.QueryString["fund"].ToString());
             fundCode = Request.QueryString["fund"].ToString();
         }
         if (Request.QueryString["branch"] != null)
         {
             branchName=omfDAOObj.GetBranchName(Request.QueryString["branch"].ToString());
             branchCode = Request.QueryString["branch"].ToString();

         }
         if (regNo != "" && fundCode != "" && branchCode != "")
         {
             unitHolderName = omfDAOObj.GetHolderName(fundCode, branchCode, regNo);
         }
         tdBranch.InnerHtml = "<span style=\"color:Green\"><b>" + branchName.ToString() + "</b></span>";
         tdFund.InnerHtml = "<span style=\"color:Green\"><b>"+fundName.ToString()+"</b></span>";
         tdHolderName.InnerHtml = "<span style=\"color:Green\"><b>" + unitHolderName.ToString() + "</b></span>";
         tdReg.InnerHtml = "<span style=\"color:Green\"><b>"+regNo.ToString()+ "</b></span>";
        //string imageLocation =
         string imageSignLocation = Path.Combine(ConfigReader.SingLocation, fundCode+"_" + branchCode + "_" + regNo + ".jpg");//"../../Image/IAMCL/Sign/"+ fundCode + "_" + branchCode + "_" + regNo + ".jpg";
         string imagePhotoLocation = Path.Combine(ConfigReader.PhotoLocation, fundCode + "_" + branchCode + "_" + regNo + ".jpg");

       
        //SignImage.ImageUrl = imageSignLocation.ToString();
       // PhotoImage.ImageUrl = imagePhotoLocation.ToString();
       
        
    }
}
