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
using AMCL.DL;
using AMCL.COMMON;
using AMCL.BL;
using AMCL.GATEWAY;
using System.Text;

public partial class UI_ViewLienDetails : System.Web.UI.Page
{
    OMFDAO omfDAOObj = new OMFDAO();
    UnitLienBl unitLienBLObj = new UnitLienBl();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
    UnitReport reportObj = new UnitReport();
    UnitLien unitLienObj = new UnitLien();
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
             regObj.RegNumber = regNo;
             regObj.FundCode = fundCode.ToString();
             regObj.BranchCode = branchCode.ToString();
         }
         tdBranch.InnerHtml = "<span style=\"color:Green\"><b>" + unitLienBLObj.totalLienAmount(regObj).ToString() + "</b></span>";
         tdFund.InnerHtml = "<span style=\"color:Green\"><b>"+fundName.ToString()+"</b></span>";
         tdHolderName.InnerHtml = "<span style=\"color:Green\"><b>" + unitHolderName.ToString() + "</b></span>";
         tdReg.InnerHtml = "<span style=\"color:Green\"><b>" + fundCode.ToString() + "/" + branchCode.ToString() + "/" + regNo.ToString() + "</b></span>";
         StringBuilder sbInnerString = new StringBuilder();
         DataTable dtLienDetails = unitLienBLObj.dtLienDetailsCertificate(regObj);
         DataTable dtLienBankDetails = new DataTable();
         if (dtLienDetails.Rows.Count > 0)
         {
             sbInnerString.Append("<table width=\"800\" align=\"center\">");                         
            
            sbInnerString.Append("<colgroup width=\"120\"></colgroup>");
            sbInnerString.Append("<colgroup width=\"120\"></colgroup>");
             sbInnerString.Append("<colgroup width=\"120\"></colgroup>");
             sbInnerString.Append("<colgroup width=\"120\"></colgroup>");
             sbInnerString.Append("<tr>");
             sbInnerString.Append("<td <span style=\"color:Green;border:1px solid; text-align:center;\"><b> LIEN NUMBER </b></span></td>");
           // sbInnerString.Append("<td <span style=\"color:Green;border:1px solid; text-align:center;\"><b> LIEN DATE </b></span></td>");
            sbInnerString.Append("<td <span style=\"color:Green;border:1px solid; text-align:center;\"><b> TRANSACTION NUMBER</b></span></td>");
             sbInnerString.Append("<td <span style=\"color:Green;border:1px solid; text-align:center;\"><b> CERTIFICATE </b></span></td>");
             sbInnerString.Append("<td <span style=\"color:Green;border:1px solid; text-align:center;\"><b>WEIGHT </b></span></td>");
             sbInnerString.Append("<td <span style=\"color:Green;border:1px solid; text-align:center;\"><b>LIEN INSTITUTION </b></span></td>");
             sbInnerString.Append("</tr>");

             for (int looper = 0; looper < dtLienDetails.Rows.Count; looper++)
             {
                 unitLienObj = new UnitLien();
                 unitLienObj.LienNo =Convert.ToInt32( dtLienDetails.Rows[looper]["LIEN_NO"].ToString());
                 dtLienBankDetails = unitLienBLObj.dtLienDetailsInfo(regObj, unitLienObj);
                 sbInnerString.Append("<tr>");
                 sbInnerString.Append("<td style=\"border:1px solid; text-align:center;\"><b>" + dtLienDetails.Rows[looper]["LIEN_NO"].ToString() + " </b></td>");
                //sbInnerString.Append("<td style=\"border:1px solid; text-align:center;\"><b>" + dtLienDetails.Rows[looper]["LN_REQ_DT"].ToString() + " </b></td>");
                sbInnerString.Append("<td style=\"border:1px solid; text-align:center;\"><b> " + dtLienDetails.Rows[looper]["SL_TR_NO"] .ToString()+ "</b></td>");
                 sbInnerString.Append("<td style=\"border:1px solid; text-align:center;\"><b>" + dtLienDetails.Rows[looper]["CERTIFICATE"].ToString() + " </b></td>");
                 sbInnerString.Append("<td style=\"border:1px solid; text-align:center;\"><b>" + dtLienDetails.Rows[looper]["QTY"].ToString() + " </b></td>");
                 sbInnerString.Append("<td style=\"border:1px solid; text-align:center;\"><b>" + reportObj.getBankNameByBankCode(Convert.ToInt16(dtLienBankDetails.Rows[0]["LN_BK_CODE"].ToString())) + "," + reportObj.getBankBranchNameByCode(Convert.ToInt16(dtLienBankDetails.Rows[0]["LN_BK_CODE"].ToString()), Convert.ToInt16(dtLienBankDetails.Rows[0]["LN_BK_BR_CODE"].ToString())) + " </b></td>");
                 sbInnerString.Append("</tr>");
             }

             sbInnerString.Append("</table>");             
             tdLeinDetails.InnerHtml = sbInnerString.ToString();
             
         }
        
        
       
        
    }
}
