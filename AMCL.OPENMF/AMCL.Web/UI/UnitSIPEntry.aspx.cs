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
using CrystalDecisions.Shared;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitSIPEntry : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    BaseClass bcContent = new BaseClass();
    UnitSIPGetSet unitSIPObj = new UnitSIPGetSet();
    UnitSIPBL unitSIPBLObj = new UnitSIPBL();
    AMCL.REPORT.CR_SIPAckReceipt CR_SIPAcknowledge = new AMCL.REPORT.CR_SIPAckReceipt();



    protected void Page_Load(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
       
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
        fundCodeTextBox.Text = fundCode.ToString();
        branchCodeTextBox.Text = branchCode.ToString();
            
        
       
      
        if (!IsPostBack)
        {
            bankNameDropDownList.DataSource = opendMFDAO.dtFillBankName(" CATE_CODE=1 ");
            bankNameDropDownList.DataTextField = "BANK_NAME";
            bankNameDropDownList.DataValueField = "BANK_CODE";
            bankNameDropDownList.DataBind();

        }
       
    
    }
    
    protected void regSaveButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
       
        regObj.FundCode = fundCodeTextBox.Text.Trim();
        regObj.BranchCode = branchCodeTextBox.Text.Trim();      
        regObj.RegNumber = regNoTextBox.Text.Trim();
        try
        {
            unitSIPObj.EftDate = sipDateTextBox.Text;
            unitSIPObj.SipNumber = Convert.ToInt32(sipNumberTextBox.Text.ToString());
            unitSIPObj.InstallAmount = Convert.ToInt32(sipAmountTextBox.Text.ToString());
            unitSIPObj.AutoRenewal = autoRenewalDropDownList.SelectedValue.ToString();
            unitSIPObj.PayFrequency = payFrequencyTypeDropDownList.SelectedValue.ToString();
            unitSIPObj.MonhlySIPDate = monthlySIPDateDropDownList.SelectedValue.ToString();
            unitSIPObj.EftDebitStart = Convert.ToDateTime(sipStartDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy");
            unitSIPObj.EftDebitEnd = Convert.ToDateTime(sipEndDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy");
            unitSIPObj.DurationInMonth = Convert.ToInt32(sipDurationTextBox.Text.ToString());
            unitSIPObj.EftAccType = accTypeDropDownList.SelectedItem.Text.ToUpper();
            unitSIPObj.RoutingNo = routingNoTextBox.Text.ToString();
            unitSIPObj.BankAccNumber = bankAccTextBox.Text.ToString();
            if (!unitSIPBLObj.isDuplicateSIP(regObj, unitSIPObj))
            {
                unitSIPBLObj.SaveRegSIPInfo(regObj, unitSIPObj, userObj);
                ClearText();
                regNoTextBox.Focus();
              //  PrintAcknowledge(regObj, unitSIPObj);
                DataTable dtSIPMaster = unitSIPBLObj.dtSIPMasterList(" AND SIP_MASTER.REG_BK='" + regObj.FundCode.ToString() + "' AND SIP_MASTER.SIP_NO=" + unitSIPObj.SipNumber);
                if (dtSIPMaster.Rows.Count > 0)
                {
                    Session["reportType"] = "ACKSingle";
                    Session["dtSIPMaster"] = dtSIPMaster;
                    Session["SIPNo"] =sipNumberTextBox.Text.ToString(); 
                    
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "window.open('ReportViewer/UnitReportSIPAcknowlodegeReportViewer.aspx');", true);
                }
                else
                {
                    Session["reportType"] = null;
                    Session["dtSIPMaster"] = null;
                    Session["SIPNo"] = null;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
                }


            }
            else
            {
                sipNumberTextBox.Focus();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Failed: Duplicate SIP Number');", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + ex.Message.Replace("'", "").ToString() + "');", true);
        }

    }
    private void ClearText()
    {               
        sipNumberTextBox.Text="";
        sipAmountTextBox.Text="";
        autoRenewalDropDownList.SelectedValue = "N";
        payFrequencyTypeDropDownList.SelectedValue = "1";
        monthlySIPDateDropDownList.SelectedValue = "10";
        sipStartDateTextBox.Text = "";
        sipDurationTextBox.Text = "";
        sipEndDateTextBox.Text = "";       
        bankAccTextBox.Text = "";
        routingNoTextBox.Text = "";
        sipDateTextBox.Text = "";
        accTypeDropDownList.SelectedValue = "1";
        
    }
    protected void regCloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
        
    }      
    
    
    protected void findButton_Click(object sender, EventArgs e)
    {
        FindRegInfo();
    }
    private void FindRegInfo()
    {
        UnitHolderRegistration regObj = new UnitHolderRegistration();
        regObj.FundCode = fundCodeTextBox.Text;
        regObj.BranchCode = branchCodeTextBox.Text;
        regObj.RegNumber = regNoTextBox.Text;
        if (opendMFDAO.IsValidRegistration(regObj))
        {
            DataTable dtHolderRegInfo = opendMFDAO.HolderRegInfo(regObj);

            if (dtHolderRegInfo.Rows.Count > 0)
            {               

                NameLabel.Text = dtHolderRegInfo.Rows[0]["HNAME"].ToString();
                DateLabel.Text = dtHolderRegInfo.Rows[0]["REG_DT"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtHolderRegInfo.Rows[0]["REG_DT"].ToString()).ToString("dd-MMM-yyyy");
                TypeLabel.Text = dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("N") ? "INDIVIDUAL" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("C") ? "CHARITY" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("I") ? "INSTITUTION" : dtHolderRegInfo.Rows[0]["REG_TYPE"].Equals("F") ? "FOREIGNER" : dtHolderRegInfo.Rows[0]["REG_TYPE"].ToString();

                CIPLabel.Text =  dtHolderRegInfo.Rows[0]["CIP"].Equals("N") ? "NO" : "YES";
                IDLabel.Text = dtHolderRegInfo.Rows[0]["ID_FLAG"].Equals("N") ? "NO" : "YES";
                sipNumberTextBox.Text = unitSIPBLObj.getNextSIPMasterInfo(regObj).ToString();
                bankAccTextBox.Text = "";


                if (dtHolderRegInfo.Rows[0]["BK_FLAG"].ToString() == "Y")
                {
                   
                    if (!dtHolderRegInfo.Rows[0]["BK_NM_CD"].Equals(DBNull.Value) && !dtHolderRegInfo.Rows[0]["BK_BR_NM_CD"].Equals(DBNull.Value) && !dtHolderRegInfo.Rows[0]["BK_AC_NO"].Equals(DBNull.Value))
                    {
                        
                        bankNameDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["BK_NM_CD"].ToString();
                        branchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(dtHolderRegInfo.Rows[0]["BK_NM_CD"].ToString()));
                        branchNameDropDownList.DataTextField = "BRANCH_NAME";
                        branchNameDropDownList.DataValueField = "BRANCH_CODE";
                        branchNameDropDownList.DataBind();
                        branchNameDropDownList.SelectedValue = dtHolderRegInfo.Rows[0]["BK_BR_NM_CD"].ToString();
                        bankAccTextBox.Text = dtHolderRegInfo.Rows[0]["BK_AC_NO"].ToString();

                        DataTable dtBankBracnhInfo = unitHolderRegBLObj.dtGetBankBracnhInfo(Convert.ToInt32(dtHolderRegInfo.Rows[0]["BK_NM_CD"].ToString()), Convert.ToInt32(dtHolderRegInfo.Rows[0]["BK_BR_NM_CD"].ToString()));
                        if (dtBankBracnhInfo.Rows.Count > 0)
                        {                           
                            routingNoTextBox.Text = dtBankBracnhInfo.Rows[0]["ROUTING_NO"].ToString();
                        }
                        else
                        {
                            routingNoTextBox.Text = "";
                        }
                        
                    }

                }
                else
                {                   
                    bankAccTextBox.Text = "";
                    routingNoTextBox.Text = "";
                    bankNameDropDownList.SelectedValue = "0";
                    branchNameDropDownList.SelectedValue = "0";
                    bankNameDropDownList.SelectedValue = "1";
                }

            }
            else
            {
                regNoTextBox.Focus();
                ClearText();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);

            }
        }
        else
        {
            regNoTextBox.Focus();
            ClearText();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Invalid Registration Number!! Please Enter Valid Registration Number');", true);

        }
    }
     
    protected void findRoutingNoButton_Click(object sender, EventArgs e)
    {
        DataTable dtBankBracnhInfo = unitHolderRegBLObj.dtGetBankBracnhInfo(routingNoTextBox.Text.Trim().ToString());
        if (dtBankBracnhInfo.Rows.Count > 0)
        {          
            bankNameDropDownList.DataSource = opendMFDAO.dtFillBankName(" CATE_CODE=1 ");
            bankNameDropDownList.DataTextField = "BANK_NAME";
            bankNameDropDownList.DataValueField = "BANK_CODE";
            bankNameDropDownList.DataBind();

            branchNameDropDownList.DataSource = opendMFDAO.dtFillBranchName(Convert.ToInt32(dtBankBracnhInfo.Rows[0]["BANK_CODE"].ToString()));
            branchNameDropDownList.DataTextField = "BRANCH_NAME";
            branchNameDropDownList.DataValueField = "BRANCH_CODE";
            branchNameDropDownList.DataBind();

            bankNameDropDownList.SelectedValue = dtBankBracnhInfo.Rows[0]["BANK_CODE"].ToString();
            branchNameDropDownList.SelectedValue = dtBankBracnhInfo.Rows[0]["BRANCH_CODE"].ToString();          
        }
        else
        {
            bankNameDropDownList.SelectedValue = "0";
            branchNameDropDownList.SelectedValue = "0";           
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Bank Information Found');", true);
        }
    }

    protected void sipDurationTextBox_TextChanged(object sender, EventArgs e)
    {
        sipEndDateTextBox.Text = string.Format("{0:dd-MMM-yyyy}",Convert.ToDateTime(sipStartDateTextBox.Text).AddMonths(Convert.ToInt32( sipDurationTextBox.Text)-1)).ToString();
    }

    private void PrintAcknowledge(UnitHolderRegistration regObj, UnitSIPGetSet unitSIPObj)
    {
        DataTable dtSIPMaster = unitSIPBLObj.dtSIPMasterList(" AND SIP_MASTER.REG_BK='" + regObj.FundCode.ToString() + "' AND SIP_MASTER.SIP_NO=" + unitSIPObj.SipNumber);
        if (dtSIPMaster.Rows.Count > 0)
        {
            dtSIPMaster.TableName = "dtSIPMaster";
           // dtSIPMaster.WriteXmlSchema(@"F:\GITHUB_PROJECT\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtSIPMaster.xsd");
            CR_SIPAcknowledge.Refresh();
            CR_SIPAcknowledge.SetDataSource(dtSIPMaster);
            CR_SIPAcknowledge.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "SIPAcknowledgeReceipt" + DateTime.Now + ".pdf");

            // rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "IntimitaionReport" + DateTime.Now + ".pdf")
            //  CrystalReportViewer1.ReportSource = CR_MoneyReceiptSale;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
        }
    }
}
