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
using System.Text;
using System.Data.OracleClient;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitRegiSearch : System.Web.UI.Page
{
    System.Web.UI.Page this_page_ref = null;
    OMFDAO opendMFDAO = new OMFDAO();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        string fundCode = bcContent.FundCode.ToString();
        string branchCode = bcContent.BranchCode.ToString();
        spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
       


            
        
      
        regNoTextBox.Focus();

        if (!IsPostBack)
        {

            bankNameDropDownList.DataSource = opendMFDAO.dtFillBankName(" CATE_CODE=1 ");
            bankNameDropDownList.DataTextField = "BANK_NAME";
            bankNameDropDownList.DataValueField = "BANK_CODE";
            bankNameDropDownList.DataBind();

            fundCodeDDL.DataSource = reportObj.dtFundCodeList();
            fundCodeDDL.DataTextField = "NAME";
            fundCodeDDL.DataValueField = "ID";
            fundCodeDDL.SelectedValue = fundCode.ToString();
            fundCodeDDL.DataBind();

            branchCodeDDL.DataSource = reportObj.dtBranchCodeList();
            branchCodeDDL.DataTextField = "NAME";
            branchCodeDDL.DataValueField = "ID";
            branchCodeDDL.SelectedValue = branchCode.ToString();
            branchCodeDDL.DataBind();

        }

    }
    
    private void ClearText()
    {
        
      
        
    }
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }


    protected void SerachButton_Click(object sender, EventArgs e)
    {
        StringBuilder sbMaster = new StringBuilder();
        DataTable dtSearchInfo = new DataTable();

        if (string.Compare(fundCodeDDL.SelectedValue.ToString().ToUpper(),"IAMPH", true) == 0)
        {
            sbMaster.Append("SELECT REG_BK, REG_BR, REG_NO, HNAME, ADDRESS, JNT_NAME, CIP, ID_FLAG, BK_FLAG,TIN,NID,FMH_NAME,MO_NAME,BIRTH_CERT_NO,PASS_NO,MOBILE1,EMAIL,BK_NM_CD,BK_BR_NM_CD,BK_AC_NO  ");
            sbMaster.Append(" FROM  (SELECT   U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, U_MASTER.HNAME,U_MASTER.TIN,U_MASTER.NID, U_MASTER.FMH_NAME, U_MASTER.MO_NAME,U_MASTER.PASS_NO, U_MASTER.BIRTH_CERT_NO, U_MASTER.MOBILE1,");
            sbMaster.Append(" U_MASTER.ADDRS1 || U_MASTER.ADDRS2 || ',' || U_MASTER.CITY AS ADDRESS, NULL AS JNT_NAME,U_MASTER.CIP,");
            sbMaster.Append(" U_MASTER.ID_FLAG, U_MASTER.BK_FLAG,EMAIL,BK_NM_CD,BK_BR_NM_CD,BK_AC_NO  FROM U_MASTER) A WHERE 1=1 AND REG_BK='" + fundCodeDDL.SelectedValue.ToString().ToUpper() + "'");

        }
        else
        {
            sbMaster.Append("SELECT REG_BK, REG_BR, REG_NO, HNAME, ADDRESS, JNT_NAME, CIP, ID_FLAG, BK_FLAG,TIN,NID,FMH_NAME,MO_NAME,BIRTH_CERT_NO,PASS_NO,MOBILE1 ,EMAIL,BK_NM_CD,BK_BR_NM_CD,BK_AC_NO");
            sbMaster.Append(" FROM  (SELECT   U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, U_MASTER.HNAME,U_MASTER.TIN,U_MASTER.NID, U_MASTER.FMH_NAME, U_MASTER.MO_NAME,U_MASTER.PASS_NO, U_MASTER.BIRTH_CERT_NO, U_MASTER.MOBILE1,");
            sbMaster.Append(" U_MASTER.ADDRS1 || U_MASTER.ADDRS2 || ',' || U_MASTER.CITY AS ADDRESS, U_JHOLDER.JNT_NAME, U_MASTER.CIP,");
            sbMaster.Append(" U_MASTER.ID_FLAG, U_MASTER.BK_FLAG ,EMAIL,BK_NM_CD,BK_BR_NM_CD,BK_AC_NO FROM U_MASTER LEFT OUTER JOIN   U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND");
            sbMaster.Append(" U_MASTER.REG_NO = U_JHOLDER.REG_NO) A WHERE 1=1 AND REG_BK='" + fundCodeDDL.SelectedValue.ToString().ToUpper() + "'");

            if (jHolderTextBox.Text != "")
            {
                sbMaster.Append(" AND ( JNT_NAME LIKE '%" + jHolderTextBox.Text.Trim().ToUpper().ToString() + "%')");
            }
        }
        if (regNoTextBox.Text != "")
        {
            sbMaster.Append(" AND (REG_NO=" + Convert.ToInt32(regNoTextBox.Text .Trim())+ ")");
            sbMaster.Append(" AND (REG_BR='" + branchCodeDDL.SelectedValue.ToString()+ "')");
        }
        if (holderNameTextBox.Text != "")
        {
            sbMaster.Append(" AND ( HNAME LIKE '%" + holderNameTextBox.Text.Trim().ToUpper().ToString() + "%')");
        }
        if (holderAddressTextBox.Text != "")
        {
            sbMaster.Append(" AND ( ADDRESS LIKE '%" + holderAddressTextBox.Text.Trim().ToUpper().ToString() + "%')");
        }
        if (NIDTextBox.Text != "")
        {
            sbMaster.Append(" AND ( NID = '" + NIDTextBox.Text.Trim().ToUpper().ToString() + "')");
        }
        if (TINTextBox.Text != "")
        {
            sbMaster.Append(" AND (  TIN = '" + TINTextBox.Text.Trim().ToUpper().ToString() + "')");
        }
        if (emailTextBox.Text != "")
        {
            sbMaster.Append(" AND ( EMAIL = '" + emailTextBox.Text.Trim().ToUpper().ToString() + "')");
        }
        if (mobileNumberTextBox.Text != "")
        {
            sbMaster.Append(" AND ( MOBILE1 = '" + mobileNumberTextBox.Text.Trim().ToUpper().ToString() + "')");
        }
        if(bankNameDropDownList.SelectedValue.ToString() !="0" )
        {
            sbMaster.Append(" AND ( BK_NM_CD = " + bankNameDropDownList.SelectedValue.ToString() + ")");
        }
        if (branchNameDropDownList.SelectedValue.ToString() != "0" && branchNameDropDownList.SelectedValue.ToString() != "")
        {
            sbMaster.Append(" AND ( BK_BR_NM_CD = " + branchNameDropDownList.SelectedValue.ToString() + ")");
        }
        if (bankAccTextBox.Text != "")
        {
            sbMaster.Append(" AND ( BK_AC_NO = '" + bankAccTextBox.Text.Trim().ToUpper().ToString() + "')");
        }
        if (PassportNoTextBox.Text != "")
        {
            sbMaster.Append(" AND ( PASS_NO = '" + PassportNoTextBox.Text.Trim().ToUpper().ToString() + "')");
        }
        if (BirthCertNoTextBox.Text != "")
        {
            sbMaster.Append(" AND (  BIRTH_CERT_NO= '" + BirthCertNoTextBox.Text.Trim().ToUpper().ToString() + "')");
        }
        if (certNoDropDownList.SelectedValue != "0" && certNoTextBox.Text != "")
        {
            sbMaster.Append(" AND REG_NO IN (SELECT REG_NO FROM SALE_CERT WHERE CERT_TYPE='" + certNoDropDownList.SelectedValue.ToString() + "' AND CERT_NO=" + Convert.ToUInt32(certNoTextBox.Text.Trim().ToString()) + " )");
            sbMaster.Append(" AND REG_BR IN (SELECT REG_BR FROM SALE_CERT WHERE CERT_TYPE='" + certNoDropDownList.SelectedValue.ToString() + "' AND CERT_NO=" + Convert.ToUInt32(certNoTextBox.Text.Trim().ToString()) + " )");
            
        }
        sbMaster.Append(" ORDER BY REG_BR, REG_NO");

        if (string.Compare(fundCodeDDL.SelectedValue.ToString().ToUpper(), "IAMPH", true) == 0)
        {
            OracleConnection Conn = new OracleConnection(ConfigReader.PENSION.ToString());
            Conn.Open();
            dtSearchInfo = commonGatewayObj.Select(sbMaster.ToString(), Conn);
        }
        else
        {
            dtSearchInfo = commonGatewayObj.Select(sbMaster.ToString());
        }
        if (dtSearchInfo.Rows.Count > 0)
        {
            totalRecordCountLabel.Text = dtSearchInfo.Rows.Count.ToString();
            dvSearchRegi.Visible = true;
            
            dgSearchRegi.DataSource = dtSearchInfo;
            dgSearchRegi.DataBind();
        }
        else
        {
            dvSearchRegi.Visible = false;
            totalRecordCountLabel.Text = "0";
             ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
        }

    }

    protected void bankNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        branchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(bankNameDropDownList.SelectedValue.ToString()));
        branchNameDropDownList.DataTextField = "BRANCH_NAME";
        branchNameDropDownList.DataValueField = "BRANCH_CODE";
        branchNameDropDownList.DataBind();
    }
}
