using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_CHEQUEPrintAccountALL : System.Web.UI.Page
{
    //System.Web.UI.Page this_page_ref = null;
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();    
    UnitUser userObj = new UnitUser();
    
    UnitRepurchaseBL unitRepBLObj = new UnitRepurchaseBL();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
    BaseClass bcContent = new BaseClass();
    AMCL.REPORT.CR_CHQPrint CR_ChequePrintIFIC = new AMCL.REPORT.CR_CHQPrint();
    NumberToEnglish numberToEnglishObj = new NumberToEnglish();
   
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

        if (!IsPostBack)
        {

            chqDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        }

    }

    
    protected void ChecqueVoucherButton_Click(object sender, EventArgs e)
    {
             
        string ChqueDate = "";
        string NameWithAC = "";
        string Name = "";
        string AmountInWord = "";
        string Amount = "";

        Name = NameTextBox.Text.Trim();
        if (accNoTextBox.Text != "")
            NameWithAC = Name + " " + "AC: " + accNoTextBox.Text.Trim();
        else
            NameWithAC = Name;

        decimal amount = Convert.ToDecimal(AmountTextBox.Text.Trim());
        Amount = amount.ToString("#,##0.00");
        AmountInWord = numberToEnglishObj.changeNumericToWords(Amount);
        string AmountInWord1 = "";
        string AmountInWord2 = "";
      
        if (AmountInWord.Length>45 && AmountInWord.Split(' ').Length>6)
        {
            string[] AmountInWordspite = AmountInWord.Split(' ');
            for(int loop=0; loop< AmountInWordspite.Length;loop++)
            {
                if(loop<=5)
                {
                    if(loop==0)
                    {
                        AmountInWord1 = AmountInWordspite[loop] ;
                    }
                    else
                    {
                        AmountInWord1 = AmountInWord1+" " + AmountInWordspite[loop];
                    }
                }
                else
                {
                    if (loop == 6)
                    {
                        AmountInWord2 = AmountInWordspite[loop];
                    }
                    else
                    {
                        AmountInWord2 = AmountInWord2 + " " + AmountInWordspite[loop];
                    }
                }
            }
                  
        }
        else
        {
            AmountInWord1 = AmountInWord;
        }
        ChqueDate = chqDateTextBox.Text.Trim();

        CR_ChequePrintIFIC.SetParameterValue("ChqueDate", ChqueDate);
        CR_ChequePrintIFIC.SetParameterValue("Amount", Amount);
        CR_ChequePrintIFIC.SetParameterValue("AmountInWord1", AmountInWord1);
        CR_ChequePrintIFIC.SetParameterValue("AmountInWord2", AmountInWord2);
        CR_ChequePrintIFIC.SetParameterValue("NameWithAC", NameWithAC);
        CR_ChequePrintIFIC.SetParameterValue("Name", Name);                                        
        CR_ChequePrintIFIC.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "ISTCLChequePrint" + DateTime.Now + ".pdf");

          
                                
    }
}
