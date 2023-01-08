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

public partial class UI_UnitMoneyReceiptEntryRenewal : System.Web.UI.Page
{
   // System.Web.UI.Page this_page_ref = null;
    OMFDAO opendMFDAO = new OMFDAO();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    UnitHolderRegBL regBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    string errorMassege = "";
    BaseClass bcContent = new BaseClass();
    EncryptDecrypt encrypt = new EncryptDecrypt();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj=new UnitHolderRegistration();
           
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
        //saleDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
       // holderDateofBirthTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        if (!IsPostBack)
        {
          
            CashAmountTextBox.Enabled = false;
            MultiplePayTypeTextBox.Enabled = false;
        }
    
    }
   
    private void ClearText()
    {
        DataTable dtDino = opendMFDAO.getTableDinomination();
       
        regNoTextBox.Text = "";                      
        ChqRadioButton.Checked = true;
        ChequeTypeDropDownList.SelectedValue = "CGQ";
        CashRadioButton.Checked = false;
        BothRadioButton.Checked = false;
        MultiRadioButton.Checked = false;       
        CashAmountTextBox.Text = "";
        MultiplePayTypeTextBox.Text = "";
        chequeDateTextBox.Text = "";
        
    }
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }
    protected void findButton_Click(object sender, EventArgs e)
    {
       
        

      
       
    
    }
   
   
   

   
   
    protected void ChqRadioButton_CheckedChanged(object sender, EventArgs e)
    {
       
        CashAmountTextBox.Enabled = false;
        MultiplePayTypeTextBox.Enabled = false;
        ChequeTypeDropDownList.SelectedValue = "CHQ";
        

    }
    protected void CashRadioButton_CheckedChanged(object sender, EventArgs e)
    {        
        MultiplePayTypeTextBox.Enabled = false;
        CashAmountTextBox.Enabled = true;
        CashAmountTextBox.Focus();
    }
    protected void BothRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        MultiplePayTypeTextBox.Enabled = false;      
        CashAmountTextBox.Enabled = true;
        
    }
    protected void MultiRadioButton_CheckedChanged(object sender, EventArgs e)
    {              
        CashAmountTextBox.Enabled = false;
        MultiplePayTypeTextBox.Enabled = true;
        MultiplePayTypeTextBox.Focus();
    }
   
}
