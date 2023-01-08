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
using System.Text;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class UI_UnitDividendReconPostReturnEntry : System.Web.UI.Page
{
    DiviReconCommonGateway DRcommonGatewayObj = new DiviReconCommonGateway();
    //OMFDAO opendMFDAO = new OMFDAO();
    //UnitUser userObj = new UnitUser();
    //UnitReport reportObj = new UnitReport();
    //BaseClass bcContent = new BaseClass();
    DiviReconDAO diviDAOObj = new DiviReconDAO();
   // DiviReconBankStatementBL aDiviReconBankStatementBL = new DiviReconBankStatementBL();
    //Message msgObj = new Message();
    //NumberToEnglish numberToEnglishObj = new NumberToEnglish();
    //private ReportDocument rdoc = new ReportDocument();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        if (!IsPostBack)
        {
            FundNameDropDownList.DataSource = diviDAOObj.dtFundList();
            FundNameDropDownList.DataTextField = "FUND_NM";
            FundNameDropDownList.DataValueField = "FUND_CD";
            FundNameDropDownList.DataBind();

        }

    }
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }





    void ClearFields()
    {
        warrantNosTextBox.Text = "";
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {


        string fundCode = FundNameDropDownList.SelectedValue.ToString();
        int divi_no = Convert.ToInt32(ClosingDateDropDownList.SelectedValue.ToString());
        string war_no = warrantNosTextBox.Text.Trim().ToString();


        DataTable dtPrintSatComData = diviDAOObj.getDividendDataForSatcomPrint(fundCode, divi_no, war_no);


        dtPrintSatComData.TableName = "dtPrintSatComData";
        dtPrintSatComData.WriteXmlSchema(@"G:\ShareForAll\dividend_recon_cmf\AMCL.Dividend\App_Code\JAGONNATH\Report\dtPrintSatComData.xsd");

        ReportDocument rdoc = new ReportDocument();
        string Path = "";
        Path = Server.MapPath("JAGONNATH/Report/crtPrintSatComData.rpt");
        rdoc.Load(Path);
        rdoc.SetDataSource(dtPrintSatComData);
        //rdoc.SetParameterValue("fundName", fundName);
        //rdoc.SetParameterValue("RecordDate", recordDate);
        //rdoc.SetParameterValue("AccountInfo", bankInfo);
        //rdoc.SetParameterValue("FY", fy);
        //rdoc.SetParameterValue("diviRate", diviRate);
        //rdoc.SetParameterValue("status", status);

        //For  download
        rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "PaidBEFTN" + DateTime.Now + ".pdf");

        //Hashtable htWarrantDeliveredInfo = new Hashtable();

       // string fundCode = FundNameDropDownList.SelectedValue.ToString();
        //string fy = DividendFYDropDownList.SelectedItem.Text.ToString();
        //int diviNo = Convert.ToInt16(ClosingDateDropDownList.SelectedValue.ToString());

        //try
        //{
        //    DRcommonGatewayObj.BeginTransaction();

        //    string LoginID = Session["UserID"].ToString();
        //    htWarrantDeliveredInfo.Add("POST_RETURN_DATE", DateTime.Now.ToString("dd-MMM-yyyy"));
        //   // htWarrantDeliveredInfo.Add("WAR_BK_PAY_USER", LoginID);
        //   // htWarrantDeliveredInfo.Add("WAR_BK_PAY_ENT_DT", DateTime.Now.ToString("dd-MMM-yyyy"));


        //    int noOfupdatedRows = DRcommonGatewayObj.Update(htWarrantDeliveredInfo, "DIVIDEND", "  WAR_NO IN (" + warrantNosTextBox.Text.Trim() + ") AND FUND_CD=" + fundCode + " AND FY = '" + fy + "' AND DIVI_NO=" + diviNo);
        //    DRcommonGatewayObj.CommitTransaction();

        //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + noOfupdatedRows + " Warrant In Hand Update Successfully');", true);
        //    ClearFields();
        //}
        //catch (Exception ex)
        //{
        //    DRcommonGatewayObj.RollbackTransaction();
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert('Process Exucution Failed!!" + ex.Message.Replace("'", "").ToString() + "');", true);
        //}
    }

    protected void FundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DividendFYDropDownList.DataSource = diviDAOObj.dtGetFundWiseFY(FundNameDropDownList.SelectedValue.ToString());
        DividendFYDropDownList.DataTextField = "F_YEAR";
        DividendFYDropDownList.DataValueField = "F_YEAR";
        DividendFYDropDownList.DataBind();
    }

    protected void resetButton_Click(object sender, EventArgs e)
    {
        warrantNosTextBox.Text = "";
        DividendFYDropDownList.SelectedValue = "0";
        FundNameDropDownList.SelectedValue = "0";
        ClosingDateDropDownList.SelectedValue = "0";
    }

    protected void DividendFYDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClosingDateDropDownList.DataSource = diviDAOObj.dtGetFYWiseClosinDate(DividendFYDropDownList.SelectedItem.Text.ToString(), FundNameDropDownList.SelectedValue.ToString().ToUpper());
        ClosingDateDropDownList.DataTextField = "CLOSE_DT";
        ClosingDateDropDownList.DataValueField = "DIVI_NO";
        ClosingDateDropDownList.DataBind();
    }
}
