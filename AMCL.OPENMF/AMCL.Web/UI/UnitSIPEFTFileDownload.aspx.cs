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
using System.Globalization;

public partial class UI_UnitSIPEFTFileDownload : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    BaseClass bcContent = new BaseClass();
    UnitSIPGetSet unitSIPObj = new UnitSIPGetSet();
    UnitSIPBL unitSIPBLObj = new UnitSIPBL();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
    AMCL.REPORT.CR_SIP_EFT CR_SIP_EFT = new AMCL.REPORT.CR_SIP_EFT();
    AMCL.REPORT.CR_SMS CR_SMS = new AMCL.REPORT.CR_SMS();
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
             

        regObj.FundCode = fundCode;
        


        if (!IsPostBack)
        {

            sipDayDropDownList.DataSource = unitSIPBLObj.dtSIPDayListDDL(" AND SIP_DETAILS.EFT_PULL_REQUEST_DATE IS NOT NULL AND SIP_DETAILS.EFT_PAID_ENTRY_DATE IS NULL AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' ");
            sipDayDropDownList.DataTextField = "DEBIT_DATE";
            sipDayDropDownList.DataValueField = "ID";
            sipDayDropDownList.DataBind();

        }
       
    
    }
    
    
    private void ClearText()
    {               
       
       
    }
    
    protected void addListButton_Click(object sender, EventArgs e)
    {
        string filter = "";
        DataTable dtFundInfo = commonGatewayObj.Select(" SELECT * FROM FUND_INFO WHERE FUND_CD='" + regObj.FundCode + "'");
        string routingNoCode = dtFundInfo.Rows[0]["SIP_FUND_ACC_ROUTING_NO"].ToString().Substring(0, 3);

        if (fileTypeDropDownList.SelectedValue.ToString()=="E")
        {
            filter =  " AND SIP_DETAILS.DEBIT_ACC_ROUTING_NO NOT LIKE '" + routingNoCode + "%' ";
        }
        else if(fileTypeDropDownList.SelectedValue.ToString() == "O")
        {
            filter = " AND SIP_DETAILS.DEBIT_ACC_ROUTING_NO  LIKE '" + routingNoCode + "%' ";
        }
        filter = filter + " AND SIP_DETAILS.EFT_PAID_STATUS IS NULL  AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.DEBIT_DATE='" + sipDayDropDownList.SelectedItem.Text.ToString() + "'";
        DataTable dtSIPListforEFTFile = unitSIPBLObj.dtSIPDetailsList(filter);
        if (dtSIPListforEFTFile.Rows.Count > 0)
        {
            
                SaleListGridView.DataSource = dtSIPListforEFTFile;
                SaleListGridView.DataBind();                                     

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
            ClearText();
        }
    }

    protected void downloadEFTFileButton_Click(object sender, EventArgs e)
    {
        DataTable dtEFT = new DataTable();
        dtEFT.Columns.Add("NARRATION", typeof(string));
        dtEFT.Columns.Add("DBL_ACC_NUMBER", typeof(string));
        dtEFT.Columns.Add("DEPOSITORS_BANK_ROUTING_NO", typeof(string));
        dtEFT.Columns.Add("DEPOSITORS_BANK_ACCNO", typeof(string));
        dtEFT.Columns.Add("ACCTYPE", typeof(string));
        dtEFT.Columns.Add("AMOUNT", typeof(string));
        dtEFT.Columns.Add("DEPOSITORS_NAME", typeof(string));
        dtEFT.Columns.Add("RECEIVERID", typeof(string));
        DataRow drEFT;

        string filter = "";
        DataTable dtFundInfo = commonGatewayObj.Select("SELECT * FROM FUND_INFO WHERE FUND_CD='" + regObj.FundCode + "'");
        string routingNoCode = dtFundInfo.Rows[0]["SIP_FUND_ACC_ROUTING_NO"].ToString().Substring(0, 3);
        if (fileTypeDropDownList.SelectedValue.ToString() == "E")
        {
            filter = " AND SIP_DETAILS.DEBIT_ACC_ROUTING_NO NOT LIKE '" + routingNoCode + "%' ";
        }
        else if (fileTypeDropDownList.SelectedValue.ToString() == "O")
        {
            filter = " AND SIP_DETAILS.DEBIT_ACC_ROUTING_NO  LIKE '" + routingNoCode + "%' ";
        }
        if(newSIPRadioButton.Checked)
        {
            filter = " AND SIP_DETAILS.SCHEDULE_NO =1 ";
        }

        filter = filter + " AND SIP_DETAILS.EFT_PAID_STATUS IS NULL AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.DEBIT_DATE='" + sipDayDropDownList.SelectedItem.Text.ToString() + "'";
        DataTable dtSIPListforEFTFile = unitSIPBLObj.dtSIPDetailsList(filter);
        if (dtSIPListforEFTFile.Rows.Count > 0)
        {

            for (int loop = 0; loop < dtSIPListforEFTFile.Rows.Count; loop++)
            {
                drEFT = dtEFT.NewRow();
                drEFT["NARRATION"] = dtSIPListforEFTFile.Rows[loop]["FUND_CD"].ToString()+ "(SIP) EFT PULL " + string.Format("{0:dd-MMM-yyyy}", dtSIPListforEFTFile.Rows[0]["DEBIT_DATE"]) ;
                drEFT["DBL_ACC_NUMBER"] = dtSIPListforEFTFile.Rows[loop]["FUND_ACC_NO"].ToString();
                drEFT["DEPOSITORS_BANK_ROUTING_NO"] = dtSIPListforEFTFile.Rows[loop]["DEBIT_ACC_ROUTING_NO"].ToString();
                drEFT["DEPOSITORS_BANK_ACCNO"] = dtSIPListforEFTFile.Rows[loop]["DEBIT_ACC_NO"].ToString();
                drEFT["ACCTYPE"] = dtSIPListforEFTFile.Rows[loop]["SIP_ACC_TYPE"].ToString();
                drEFT["AMOUNT"] = dtSIPListforEFTFile.Rows[loop]["DEBIT_AMOUNT"].ToString();
                drEFT["DEPOSITORS_NAME"] = dtSIPListforEFTFile.Rows[loop]["HNAME"].ToString();
                drEFT["RECEIVERID"] =  dtSIPListforEFTFile.Rows[loop]["REG_BR"].ToString() + "/"+ dtSIPListforEFTFile.Rows[loop]["SIP_NO"].ToString()+"/" + dtSIPListforEFTFile.Rows[loop]["SCHEDULE_NO"].ToString();
                dtEFT.Rows.Add(drEFT);
                
            }

            if (dtEFT.Rows.Count > 0)
            {
                dtEFT.TableName = "dtEFT";
               // dtEFT.WriteXmlSchema(@"F:\\GITHUB_PROJECT\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtEFT.xsd");
                CR_SIP_EFT.Refresh();
                CR_SIP_EFT.SetDataSource(dtEFT);
                CR_SIP_EFT.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, Response, true, "EFT_"+regObj.FundCode.ToString()+"_" + sipDayDropDownList.SelectedItem.Text.ToString() + "_" + DateTime.Now);
               
            }
            else
            {

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
            }
        }
        else
        {
            
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
            ClearText();
        }
    }

    

    protected void downloadSMSButton_Click(object sender, EventArgs e)
    {
        DataTable dtSMS = new DataTable();
        dtSMS.Columns.Add("MOBILE", typeof(string));
        dtSMS.Columns.Add("SMS_TEXT", typeof(string));
        DataRow drSMS;

        string filter = "";
        filter = filter + " AND SIP_DETAILS.EFT_PAID_STATUS IS NULL  AND SIP_DETAILS.REG_BK = '" + regObj.FundCode + "' AND SIP_DETAILS.DEBIT_DATE='" + sipDayDropDownList.SelectedItem.Text.ToString() + "' ";
        DataTable dtSIPListforEFTFile = unitSIPBLObj.dtSIPDetailsList(filter);
        if (dtSIPListforEFTFile.Rows.Count > 0)
        {
            
           for(int loop=0;loop<dtSIPListforEFTFile.Rows.Count;loop++)
            {
                drSMS = dtSMS.NewRow();
                drSMS["MOBILE"] = dtSIPListforEFTFile.Rows[loop]["MOBILE1"].ToString();
                drSMS["SMS_TEXT"] = "Dear Sir, Your SIP installment of "+ CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToDateTime(dtSIPListforEFTFile.Rows[loop]["DEBIT_DATE"].ToString()).Month) + ", " + Convert.ToDateTime(dtSIPListforEFTFile.Rows[loop]["DEBIT_DATE"].ToString()).Year.ToString() + " amounting BDT."+ dtSIPListforEFTFile.Rows[loop]["DEBIT_AMOUNT"].ToString() + " will be collected on "+string.Format("{0:dd-MMM-yyyy}", dtSIPListforEFTFile.Rows[loop]["DEBIT_DATE"]).ToString() + ". Please ensure the fund. Regards ICB AMCL ";
                dtSMS.Rows.Add(drSMS);
            }

            if (dtSMS.Rows.Count > 0)
            {
                dtSMS.TableName = "SMS";               
                CR_SMS.Refresh();
                CR_SMS.SetDataSource(dtSMS);                           
                CR_SMS.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, Response, true, "SMS_"+ sipDayDropDownList.SelectedItem.Text.ToString() + "_" + DateTime.Now);

            }
            else
            {
              
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
            ClearText();
        }

    }





    
}
