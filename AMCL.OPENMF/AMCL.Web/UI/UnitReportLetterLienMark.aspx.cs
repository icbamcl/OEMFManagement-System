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
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitReportLetterLienMark : System.Web.UI.Page
{
   
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();    
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    //UnitTransferBL unitTransferBLObj = new UnitTransferBL();
   // UnitRepurchaseBL unitRepBLObj = new UnitRepurchaseBL();
    UnitLienBl unitLienBLObj = new UnitLienBl();
    UnitReport reportObj = new UnitReport();
    string fundCode = "";
    string branchCode = "";
    
    UnitHolderRegistration regObj = new UnitHolderRegistration();
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
        fundCode = bcContent.FundCode.ToString();
        branchCode = bcContent.BranchCode.ToString();
       // spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
        FundCodeTextBox.Text = fundCode.ToString();
        BranchCodeTextBox.Text = branchCode.ToString();
        
        RegNoTextBox.Focus();
      
            
 

     

        if (!IsPostBack)
        {           
            LienbankNameDropDownList.DataSource = opendMFDAO.dtFillBankName();
            LienbankNameDropDownList.DataTextField = "BANK_NAME";
            LienbankNameDropDownList.DataValueField = "BANK_CODE";
            LienbankNameDropDownList.DataBind();

            SignatoryDropDownList.DataSource = reportObj.dtFillSignatory();
            SignatoryDropDownList.DataTextField = "NAME";
            SignatoryDropDownList.DataValueField = "ID";
            SignatoryDropDownList.DataBind();

           
        }
    
    }    
    private void ClearText()
    {
        LienMarkDropDownList.SelectedValue = "0";
        LienReqDateTextBox.Text = "";
        LienReqRefTextBox.Text = "";
        LienbankNameDropDownList.SelectedValue = "0";
        LienbranchNameDropDownList.SelectedValue = "0";
        //RegNoTextBox.Text = "";
        HolderJNameTextBox.Text = "";
        HolderNameTextBox.Text = "";
        //LienReqRefTextBox.Text = "";
        toTextBox.Text = "";
        DivisionTextBox.Text = "";
        Address1TextBox.Text = "";
        Address2TextBox.Text = "";
        Address3TextBox.Text = "";
        SignatoryDropDownList.SelectedValue = "0";
        DesignationDropDownList.SelectedValue = "0";
      
    }       
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }
   
   
    
    
    protected void findButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration unitRegObj = new UnitHolderRegistration();
        unitRegObj.FundCode = FundCodeTextBox.Text.Trim();
        unitRegObj.BranchCode = BranchCodeTextBox.Text.Trim();
        unitRegObj.RegNumber = RegNoTextBox.Text.Trim();
        UnitLien unitLienObj = new UnitLien();
        DataTable dtRegInfo = opendMFDAO.getDtRegInfo(unitRegObj);
        if (dtRegInfo.Rows.Count > 0)
        {
            HolderNameTextBox.Text = dtRegInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["HNAME"].ToString();
            HolderJNameTextBox.Text = dtRegInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtRegInfo.Rows[0]["JNT_NAME"].ToString();
            DataTable dtLienNumbers = unitLienBLObj.dtTotalLien(unitRegObj);
            if (dtLienNumbers.Rows.Count > 1)
            {
                LienMarkDropDownList.DataSource = dtLienNumbers;
                LienMarkDropDownList.DataTextField = "LIEN_NO";
                LienMarkDropDownList.DataValueField = "ID";
                LienMarkDropDownList.DataBind();

            }
            else
            {
                LienMarkDropDownList.DataSource = dtLienNumbers;
                LienMarkDropDownList.DataTextField = "LIEN_NO";
                LienMarkDropDownList.DataValueField = "ID";
                LienMarkDropDownList.DataBind();               
                ClearText();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Liened Units');", true);                            
            }
        }
    }
    protected void PrintButton_Click(object sender, EventArgs e)
    {
        int lienNo = Convert.ToInt32(LienMarkDropDownList.SelectedValue.ToString());
        int regNo = Convert.ToInt32(RegNoTextBox.Text.Trim().ToString());
        string lienReqDate = LienReqDateTextBox.Text.Trim().ToString();
        string lienReqRef = LienReqRefTextBox.Text.Trim().ToString();
        string lienInstitution = LienbankNameDropDownList.SelectedItem.Text.ToString();
        string lienInstitutionBranch = LienbranchNameDropDownList.SelectedItem.Text.ToString();
        string holderName = HolderNameTextBox.Text.Trim().ToString();
        string jHolderName = HolderJNameTextBox.Text.Trim().ToString();
        string toName = toTextBox.Text.Trim().ToString();
        string division = DivisionTextBox.Text.Trim().ToString();
        string address1 = Address1TextBox.Text.Trim().ToString();
        string address2 = Address2TextBox.Text.Trim().ToString();
        string address3 = Address3TextBox.Text.Trim().ToString();
        string signatory = SignatoryDropDownList.SelectedItem.Text.ToString();
        string designation = DesignationDropDownList.SelectedItem.Text.ToString();

        Session["lienNo"] = lienNo;
        Session["regNo"] = regNo;
        Session["lienReqDate"] = lienReqDate;
        Session["lienReqRef"] = lienReqRef;
        Session["lienInstitution"] = lienInstitution;
        Session["lienInstitutionBranch"] = lienInstitutionBranch;
        Session["holderName"] = holderName;
        Session["jHolderName"] = jHolderName;
        Session["toName"] = toName;
        Session["division"] = division;
        Session["address1"] = address1;
        Session["address2"] = address2;
        Session["address3"] = address3;
        Session["signatory"] = signatory;
        Session["designation"] = designation;
        Session["fundCode"] = fundCode;
        Session["branchCode"] = branchCode;
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UnitHolderLedgerReport", "window.open('ReportViewer/UnitReportLienLetterReportViewer.aspx')", true);
     
    }
    protected void LienMarkDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        UnitHolderRegistration unitRegObj = new UnitHolderRegistration();
        unitRegObj.FundCode = FundCodeTextBox.Text.Trim();
        unitRegObj.BranchCode = BranchCodeTextBox.Text.Trim();
        unitRegObj.RegNumber = RegNoTextBox.Text.Trim();
        UnitLien unitLienObj = new UnitLien();
        unitLienObj.LienNo = Convert.ToInt32(LienMarkDropDownList.SelectedValue.ToString());       
        DataTable dtLienDetails = unitLienBLObj.dtLienDetailsInfo(unitRegObj, unitLienObj);
        if (dtLienDetails.Rows.Count > 0)
        {
            LienbankNameDropDownList.SelectedValue = dtLienDetails.Rows[0]["LN_BK_CODE"].ToString();
            LienbranchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(dtLienDetails.Rows[0]["LN_BK_CODE"].ToString()));
            LienbranchNameDropDownList.DataTextField = "BRANCH_NAME";
            LienbranchNameDropDownList.DataValueField = "BRANCH_CODE";
            LienbranchNameDropDownList.DataBind();
            LienbranchNameDropDownList.SelectedValue = dtLienDetails.Rows[0]["LN_BK_BR_CODE"].ToString();
            LienReqDateTextBox.Text = dtLienDetails.Rows[0]["LN_REQ_DT"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtLienDetails.Rows[0]["LN_REQ_DT"].ToString()).ToString("dd-MMM-yyyy");
            LienReqRefTextBox.Text = dtLienDetails.Rows[0]["LN_REQ_REF"].Equals(DBNull.Value) ? "" : dtLienDetails.Rows[0]["LN_REQ_REF"].ToString();

        }
        else
        {
            
        }
    }
}
