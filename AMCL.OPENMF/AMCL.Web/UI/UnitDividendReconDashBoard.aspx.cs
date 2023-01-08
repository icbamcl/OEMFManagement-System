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

public partial class UI_UnitDividendReconDashBoard : System.Web.UI.Page
{
    DiviReconCommonGateway DRcommonGatewayObj = new DiviReconCommonGateway();  
    OMFDAO opendMFDAO = new OMFDAO();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();
    JDiviReconDAO diviDAOObj = new JDiviReconDAO();
    DiviReconDAO DRDAOObj = new DiviReconDAO();
    //DiviReconBankStatementBL aDiviReconBankStatementBL = new DiviReconBankStatementBL();
    Message msgObj = new Message();
    NumberToEnglish numberToEnglishObj = new NumberToEnglish();
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

            FundNameDropDownList.DataSource = DRDAOObj.dtFundList();
            FundNameDropDownList.DataTextField = "FUND_NM";
            FundNameDropDownList.DataValueField = "FUND_CD";
            FundNameDropDownList.DataBind();

        }

    }

    protected void FundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        fyDropDownList.DataSource = diviDAOObj.dtGetFundWiseFY(FundNameDropDownList.SelectedValue.ToString());
        fyDropDownList.DataTextField = "F_YEAR";
        fyDropDownList.DataValueField = "F_YEAR";
        fyDropDownList.DataBind();
        Clear();
        ClosingDateDropDownList.Text = null;
    }
    protected void searchButton_Click(object sender, EventArgs e)
    {
        string fundCode = FundNameDropDownList.SelectedValue.ToString();
        int divi_no = Convert.ToInt32(fyDropDownList.SelectedValue.ToString());
        string fy = fyDropDownList.SelectedItem.Text.ToString();

        long no_units = 0;
        decimal net_amount = 0;

        // dividend process
        DataTable dtDiviPara = DRDAOObj.getDividendPara(fundCode, divi_no, fy, " ");
        DataTable dtDividendProceesInfo = DRDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, " ");
        string bankInfo = "AC#:" + dtDiviPara.Rows[0]["BK_AC_NO"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_NAME"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_ADDRS1"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_ADDRS2"].ToString();

        fundNameLabel.Text = FundNameDropDownList.SelectedItem.Text.ToString();
        bankInfoLabel.Text = bankInfo.ToString();
        holderLabel.Text = dtDividendProceesInfo.Rows[0]["NO_HOLDER"].ToString();
        unitsLabel.Text = dtDividendProceesInfo.Rows[0]["NO_UNITS"].ToString();
        rateLabel.Text = dtDiviPara.Rows[0]["RATE"].ToString();
        grossLabel.Text = dtDividendProceesInfo.Rows[0]["GROSS"].ToString();
        taxAmountLabel.Text = dtDividendProceesInfo.Rows[0]["TAX_AMOUNT"].ToString();
        netLabel.Text = dtDividendProceesInfo.Rows[0]["NET_AMOUNT"].ToString();

        //dividend Paid
        no_units = 0;
        net_amount = 0;

        DataTable dtPaidEFT = DRDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, "  AND (IS_BEFTN = 'YES') AND (BEFTN_RETURN_DT IS NULL)  ");
        paidLabel1.Text = dtPaidEFT.Rows[0]["NO_HOLDER"].ToString();
        paidAmountLabel1.Text = dtPaidEFT.Rows[0]["NET_AMOUNT"].ToString();

        no_units = no_units + Convert.ToInt64(dtPaidEFT.Rows[0]["NO_HOLDER"].ToString());
        net_amount = net_amount + Convert.ToDecimal(dtPaidEFT.Rows[0]["NET_AMOUNT"].ToString());

        DataTable dtPaidEFTAfterReturn = DRDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, "  AND (IS_BEFTN = 'YES') AND (BEFTN_RETURN_DT IS NOT NULL) AND (WAR_BK_PAY IS NOT NULL)");
        paidAfterReturnLabel.Text = dtPaidEFTAfterReturn.Rows[0]["NO_HOLDER"].ToString();
        paidAmountAfterReturnLabel.Text = dtPaidEFTAfterReturn.Rows[0]["NET_AMOUNT"].ToString();

        no_units = no_units + Convert.ToInt64(dtPaidEFTAfterReturn.Rows[0]["NO_HOLDER"].ToString());
        net_amount = net_amount + Convert.ToDecimal(dtPaidEFTAfterReturn.Rows[0]["NET_AMOUNT"].ToString());

        DataTable dtPaidWarrant = DRDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'NO') AND (WAR_BK_PAY IS NOT NULL) ");
        paidLabel2.Text = dtPaidWarrant.Rows[0]["NO_HOLDER"].ToString();
        paidAmountLabel2.Text = dtPaidWarrant.Rows[0]["NET_AMOUNT"].ToString();

        no_units = no_units + Convert.ToInt64(dtPaidWarrant.Rows[0]["NO_HOLDER"].ToString());
        net_amount = net_amount + Convert.ToDecimal(dtPaidWarrant.Rows[0]["NET_AMOUNT"].ToString());

        paidLabel3.Text = no_units.ToString();
        paidAmountLabel3.Text = net_amount.ToString();



        //dividend unpaid

        //no_units = 0;
        //net_amount = 0;

        //DataTable dtunPaidWarrantInHand = diviDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'NO') AND (WAR_BK_PAY IS NULL) AND (WAR_BK_PAY_USER IS NULL) AND ( WAR_BK_PAY_ENT_DT IS NOT NULL) ");
        //unpaidLabel1.Text = dtunPaidWarrantInHand.Rows[0]["NO_HOLDER"].ToString();
        //unpaidAmountLabel1.Text = dtunPaidWarrantInHand.Rows[0]["NET_AMOUNT"].ToString();

        //no_units = no_units + Convert.ToInt64(dtunPaidWarrantInHand.Rows[0]["NO_HOLDER"].ToString());
        //net_amount = net_amount + Convert.ToDecimal(dtunPaidWarrantInHand.Rows[0]["NET_AMOUNT"].ToString());

        //DataTable dtunPaidEFTReturn = diviDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'YES') AND (WAR_BK_PAY IS  NULL) AND (WAR_BK_PAY_USER IS NULL) AND (BEFTN_RETURN_DT IS NOT NULL) ");
        //unpaidLabel2.Text = dtunPaidEFTReturn.Rows[0]["NO_HOLDER"].ToString();
        //unpaidAmountLabel2.Text = dtunPaidEFTReturn.Rows[0]["NET_AMOUNT"].ToString();

        //no_units = no_units + Convert.ToInt64(dtunPaidEFTReturn.Rows[0]["NO_HOLDER"].ToString());
        //net_amount = net_amount + Convert.ToDecimal(dtunPaidEFTReturn.Rows[0]["NET_AMOUNT"].ToString());

        //DataTable dtunPaidWarrantPost = diviDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'NO') AND (WAR_BK_PAY IS NULL) AND (WAR_BK_PAY_USER IS NULL) AND ( WAR_BK_PAY_ENT_DT IS NULL)  AND ( POST_DATE IS NOT NULL) ");
        //unpaidLabel3.Text = dtunPaidWarrantPost.Rows[0]["NO_HOLDER"].ToString();
        //unpaidAmountLabel3.Text = dtunPaidWarrantPost.Rows[0]["NET_AMOUNT"].ToString();

        //no_units = no_units + Convert.ToInt64(dtunPaidWarrantPost.Rows[0]["NO_HOLDER"].ToString());
        //net_amount = net_amount + Convert.ToDecimal(dtunPaidWarrantPost.Rows[0]["NET_AMOUNT"].ToString());

        //DataTable dtunPaidWarrantDelivered = diviDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'NO') AND (IS_PAID IS NULL) AND (WAR_DELEVERY_BY IS NOT NULL)  ");
        //unpaidLabel4.Text = dtunPaidWarrantDelivered.Rows[0]["NO_HOLDER"].ToString();
        //unpaidAmountLabel4.Text = dtunPaidWarrantDelivered.Rows[0]["NET_AMOUNT"].ToString();

        //no_units = no_units + Convert.ToInt64(dtunPaidWarrantDelivered.Rows[0]["NO_HOLDER"].ToString());
        //net_amount = net_amount + Convert.ToDecimal(dtunPaidWarrantDelivered.Rows[0]["NET_AMOUNT"].ToString());




        //unpaidLabel6.Text = no_units.ToString();
        //unpaidAmountLabel6.Text = net_amount.ToString();
    }


    protected void unpaidWarINHandPrintButton_Click(object sender, EventArgs e)
    {


        //string status = "Un Paid Warrant In Hand";
        //string fundName = FundNameDropDownList.SelectedItem.Text.ToString();
        //int fundCode = Convert.ToInt32(FundNameDropDownList.SelectedValue.ToString());
        //int divi_no = Convert.ToInt32(fyDropDownList.SelectedValue.ToString());
        //string fy = fyDropDownList.SelectedItem.Text.ToString();
        //DataTable dtDiviPara = diviDAOObj.getDividendPara(fundCode, divi_no, fy, " ");
        //string bankInfo = "AC#:" + dtDiviPara.Rows[0]["BK_AC_NO"].ToString();
        //bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_NAME"].ToString();
        //bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_ADDRS1"].ToString();
        //bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_ADDRS2"].ToString();
        //string recordDate = Convert.ToDateTime(dtDiviPara.Rows[0]["CLOSE_DT"]).ToString("dd-MMM-yyyy");
        //string diviRate = dtDiviPara.Rows[0]["RATE"].ToString();






        //DataTable dtunPaidWarrantInHand = diviDAOObj.getDividendPayDetailsInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'NO') AND (IS_PAID IS  NULL) AND (WAR_DELEVERY_BY IS NULL) AND ( WAR_IN_HAND_ENTRY_DATE IS NOT NULL) ");


        //dtunPaidWarrantInHand.TableName = "dtDividendInfo";
        //// dtunPaidWarrantInHand.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.Dividend\UI\ReportViewer\Report\dtDividendInfo.xsd");

        //ReportDocument rdoc = new ReportDocument();
        //string Path = "";
        //Path = Server.MapPath("ReportViewer/Report/crtReconDividendWarrantDetailsInfo.rpt");
        //rdoc.Load(Path);
        //rdoc.SetDataSource(dtunPaidWarrantInHand);
        //rdoc.SetParameterValue("fundName", fundName);
        //rdoc.SetParameterValue("RecordDate", recordDate);
        //rdoc.SetParameterValue("AccountInfo", bankInfo);
        //rdoc.SetParameterValue("FY", fy);
        //rdoc.SetParameterValue("diviRate", diviRate);
        //rdoc.SetParameterValue("status", status);

        ////For  download
        //rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "UnPaidWarrant" + DateTime.Now + ".pdf");


    }

    protected void unpaidEFTReturnPrintButton_Click(object sender, EventArgs e)
    {
        //string status = "Un Paid BEFTN Return";
        //string fundName = FundNameDropDownList.SelectedItem.Text.ToString();
        //int fundCode = Convert.ToInt32(FundNameDropDownList.SelectedValue.ToString());
        //int divi_no = Convert.ToInt32(fyDropDownList.SelectedValue.ToString());
        //string fy = fyDropDownList.SelectedItem.Text.ToString();
        //DataTable dtDiviPara = dividendDAOObj.getDividendPara(fundCode, divi_no, fy, " ");
        //string bankInfo = "AC#:" + dtDiviPara.Rows[0]["BANK_ACC_NO"].ToString();
        //bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BANK_NAME"].ToString();
        //bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BANK_ADDRS1"].ToString();
        //bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BANK_ADDRS2"].ToString();
        //string recordDate = Convert.ToDateTime(dtDiviPara.Rows[0]["RECORD_DATE"]).ToString("dd-MMM-yyyy");
        //string diviRate = dtDiviPara.Rows[0]["DIVI_RATE"].ToString();






        //DataTable dtunPaidWarrantInHand = dividendDAOObj.getDividendPayDetailsInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'YES') AND (IS_PAID IS NULL) AND (WAR_DELEVERY_BY IS NULL) AND ( BEFTN_RETURN_DATE IS NOT NULL) ");


        //dtunPaidWarrantInHand.TableName = "dtDividendInfo";
        //// dtunPaidWarrantInHand.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.Dividend\UI\ReportViewer\Report\dtDividendInfo.xsd");

        //ReportDocument rdoc = new ReportDocument();
        //string Path = "";
        //Path = Server.MapPath("ReportViewer/Report/crtReconDividendWarrantDetailsInfo.rpt");
        //rdoc.Load(Path);
        //rdoc.SetDataSource(dtunPaidWarrantInHand);
        //rdoc.SetParameterValue("fundName", fundName);
        //rdoc.SetParameterValue("RecordDate", recordDate);
        //rdoc.SetParameterValue("AccountInfo", bankInfo);
        //rdoc.SetParameterValue("FY", fy);
        //rdoc.SetParameterValue("diviRate", diviRate);
        //rdoc.SetParameterValue("status", status);

        ////For  download
        //rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "UnPaidEFFTReturn" + DateTime.Now + ".pdf");
    }

    protected void unpaidPostPrintButton_Click(object sender, EventArgs e)
    {
        //string status = "Un Paid Posted But Not Deposited";
        //string fundName = FundNameDropDownList.SelectedItem.Text.ToString();
        //int fundCode = Convert.ToInt32(FundNameDropDownList.SelectedValue.ToString());
        //int divi_no = Convert.ToInt32(fyDropDownList.SelectedValue.ToString());
        //string fy = fyDropDownList.SelectedItem.Text.ToString();
        //DataTable dtDiviPara = dividendDAOObj.getDividendPara(fundCode, divi_no, fy, " ");
        //string bankInfo = "AC#:" + dtDiviPara.Rows[0]["BANK_ACC_NO"].ToString();
        //bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BANK_NAME"].ToString();
        //bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BANK_ADDRS1"].ToString();
        //bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BANK_ADDRS2"].ToString();
        //string recordDate = Convert.ToDateTime(dtDiviPara.Rows[0]["RECORD_DATE"]).ToString("dd-MMM-yyyy");
        //string diviRate = dtDiviPara.Rows[0]["DIVI_RATE"].ToString();


        //DataTable dtunPaidWarrantInHand = dividendDAOObj.getDividendPayDetailsInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'NO') AND (IS_PAID IS NULL) AND (WAR_DELEVERY_BY IS NULL) AND ( WAR_IN_HAND_ENTRY_DATE IS NULL)  AND ( POST_DATE IS NOT NULL) ");


        //dtunPaidWarrantInHand.TableName = "dtDividendInfo";
        //// dtunPaidWarrantInHand.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.Dividend\UI\ReportViewer\Report\dtDividendInfo.xsd");

        //ReportDocument rdoc = new ReportDocument();
        //string Path = "";
        //Path = Server.MapPath("ReportViewer/Report/crtReconDividendWarrantDetailsInfo.rpt");
        //rdoc.Load(Path);
        //rdoc.SetDataSource(dtunPaidWarrantInHand);
        //rdoc.SetParameterValue("fundName", fundName);
        //rdoc.SetParameterValue("RecordDate", recordDate);
        //rdoc.SetParameterValue("AccountInfo", bankInfo);
        //rdoc.SetParameterValue("FY", fy);
        //rdoc.SetParameterValue("diviRate", diviRate);
        //rdoc.SetParameterValue("status", status);

        ////For  download
        //rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "UnPaidPost" + DateTime.Now + ".pdf");
    }

    protected void unpaidDeliverdPrintButton_Click(object sender, EventArgs e)
    {
        //string status = "Un Paid Delivered But Not Deposited";
        //string fundName = FundNameDropDownList.SelectedItem.Text.ToString();
        //int fundCode = Convert.ToInt32(FundNameDropDownList.SelectedValue.ToString());
        //int divi_no = Convert.ToInt32(fyDropDownList.SelectedValue.ToString());
        //string fy = fyDropDownList.SelectedItem.Text.ToString();
        //DataTable dtDiviPara = dividendDAOObj.getDividendPara(fundCode, divi_no, fy, " ");
        //string bankInfo = "AC#:" + dtDiviPara.Rows[0]["BANK_ACC_NO"].ToString();
        //bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BANK_NAME"].ToString();
        //bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BANK_ADDRS1"].ToString();
        //bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BANK_ADDRS2"].ToString();
        //string recordDate = Convert.ToDateTime(dtDiviPara.Rows[0]["RECORD_DATE"]).ToString("dd-MMM-yyyy");
        //string diviRate = dtDiviPara.Rows[0]["DIVI_RATE"].ToString();


        //DataTable dtunPaidWarrantInHand = dividendDAOObj.getDividendPayDetailsInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'NO') AND (IS_PAID IS NULL) AND (WAR_DELEVERY_BY IS NOT NULL) AND ( WAR_IN_HAND_ENTRY_DATE IS NULL)  AND ( POST_DATE IS NULL) ");


        //dtunPaidWarrantInHand.TableName = "dtDividendInfo";
        //// dtunPaidWarrantInHand.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.Dividend\UI\ReportViewer\Report\dtDividendInfo.xsd");

        //ReportDocument rdoc = new ReportDocument();
        //string Path = "";
        //Path = Server.MapPath("ReportViewer/Report/crtReconDividendWarrantDetailsInfo.rpt");
        //rdoc.Load(Path);
        //rdoc.SetDataSource(dtunPaidWarrantInHand);
        //rdoc.SetParameterValue("fundName", fundName);
        //rdoc.SetParameterValue("RecordDate", recordDate);
        //rdoc.SetParameterValue("AccountInfo", bankInfo);
        //rdoc.SetParameterValue("FY", fy);
        //rdoc.SetParameterValue("diviRate", diviRate);
        //rdoc.SetParameterValue("status", status);

        ////For  download
        //rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "UnPaidDelivered" + DateTime.Now + ".pdf");
    }

    protected void unpaidTotalPrintButton_Click(object sender, EventArgs e)
    {

    }

    protected void paidEFTPrintButton_Click(object sender, EventArgs e)
    {
        string status = "Paid Through BETN/ONLINE ";
        string fundName = FundNameDropDownList.SelectedItem.Text.ToString();
        string fundCode = FundNameDropDownList.SelectedValue.ToString();
        int divi_no = Convert.ToInt32(fyDropDownList.SelectedValue.ToString());
        string fy = fyDropDownList.SelectedItem.Text.ToString();
        DataTable dtDiviPara = DRDAOObj.getDividendPara(fundCode, divi_no, fy, " ");
        string bankInfo = "AC#:" + dtDiviPara.Rows[0]["BK_AC_NO"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_NAME"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_ADDRS1"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_ADDRS2"].ToString();
        string recordDate = Convert.ToDateTime(dtDiviPara.Rows[0]["CLOSE_DT"]).ToString("dd-MMM-yyyy");
        string diviRate = dtDiviPara.Rows[0]["RATE"].ToString();


        DataTable dtunPaidWarrantInHand = DRDAOObj.getDividendPayDetailsInfo(fundCode, divi_no, fy, " AND (DIVIDEND.IS_BEFTN = 'Y') AND (DIVIDEND.BEFTN_RETURN_DT IS  NULL) ");
        //DataTable dtunPaidWarrantInHand2 = diviDAOObj.getDividendPayDetailsInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'Y') AND (BEFTN_RETURN_DT IS NOT NULL) AND (WAR_BK_PAY IS NOT NULL)  ");
        //dtunPaidWarrantInHand.Merge(dtunPaidWarrantInHand);

        dtunPaidWarrantInHand.TableName = "dtUnitDividendReconInfo";
        //dtunPaidWarrantInHand.WriteXmlSchema(@"F:\AMCL_PROJECT\AMCL.OPENMF\AMCL.Web\UI\ReportViewer\Report\dtUnitDividendReconInfo.xsd");

        ReportDocument rdoc = new ReportDocument();
        string Path = "";
        Path = Server.MapPath("ReportViewer/Report/crtReconDividendWarrantDetailsInfo.rpt");
        rdoc.Load(Path);
        rdoc.SetDataSource(dtunPaidWarrantInHand);
        rdoc.SetParameterValue("fundName", fundName);
        rdoc.SetParameterValue("RecordDate", recordDate);
        rdoc.SetParameterValue("AccountInfo", bankInfo);
        rdoc.SetParameterValue("FY", fy);
        rdoc.SetParameterValue("diviRate", diviRate);
        rdoc.SetParameterValue("status", status);

        //For  download
        rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "PaidBEFTN" + DateTime.Now + ".pdf");
    }

    protected void paidWarrantPrintButton_Click(object sender, EventArgs e)
    {
        string status = "Paid Through Warrant ";
        string fundName = FundNameDropDownList.SelectedItem.Text.ToString();
        string fundCode = FundNameDropDownList.SelectedValue.ToString();
        int divi_no = Convert.ToInt32(fyDropDownList.SelectedValue.ToString());
        string fy = fyDropDownList.SelectedItem.Text.ToString();
        DataTable dtDiviPara = DRDAOObj.getDividendPara(fundCode, divi_no, fy, " ");
        string bankInfo = "AC#:" + dtDiviPara.Rows[0]["BK_AC_NO"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_NAME"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_ADDRS1"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_ADDRS2"].ToString();
        string recordDate = Convert.ToDateTime(dtDiviPara.Rows[0]["CLOSE_DT"]).ToString("dd-MMM-yyyy");
        string diviRate = dtDiviPara.Rows[0]["RATE"].ToString();


        DataTable dtunPaidWarrantInHand = DRDAOObj.getDividendPayDetailsInfo(fundCode, divi_no, fy, " AND (DIVIDEND.IS_BEFTN = 'N') AND  (DIVIDEND.WAR_BK_PAY IS NOT NULL) ");


        dtunPaidWarrantInHand.TableName = "dtUnitDividendReconInfo";
        // dtunPaidWarrantInHand.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.Dividend\UI\ReportViewer\Report\dtDividendInfo.xsd");

        ReportDocument rdoc = new ReportDocument();
        string Path = "";
        Path = Server.MapPath("ReportViewer/Report/crtReconDividendWarrantDetailsInfo.rpt");
        rdoc.Load(Path);
        rdoc.SetDataSource(dtunPaidWarrantInHand);
        rdoc.SetParameterValue("fundName", fundName);
        rdoc.SetParameterValue("RecordDate", recordDate);
        rdoc.SetParameterValue("AccountInfo", bankInfo);
        rdoc.SetParameterValue("FY", fy);
        rdoc.SetParameterValue("diviRate", diviRate);
        rdoc.SetParameterValue("status", status);

        //For  download
        rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "PaidBEFTN" + DateTime.Now + ".pdf");
    }
    protected void Page_Unload(object sender, EventArgs e)
    {

        ////rdoc.Close();
        ////rdoc.Dispose();
        ////rdoc = null;
        ////GC.Collect();
    }

    protected void paidAfterReturnButton_Click(object sender, EventArgs e)
    {

        string status = "Paid After BEFT/ONLINE Return";
        string fundName = FundNameDropDownList.SelectedItem.Text.ToString();
        string fundCode = FundNameDropDownList.SelectedValue.ToString();
        int divi_no = Convert.ToInt32(fyDropDownList.SelectedValue.ToString());
        string fy = fyDropDownList.SelectedItem.Text.ToString();
        DataTable dtDiviPara = DRDAOObj.getDividendPara(fundCode, divi_no, fy, " ");
        string bankInfo = "AC#:" + dtDiviPara.Rows[0]["BK_AC_NO"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_NAME"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_ADDRS1"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_ADDRS2"].ToString();
        string recordDate = Convert.ToDateTime(dtDiviPara.Rows[0]["CLOSE_DT"]).ToString("dd-MMM-yyyy");
        string diviRate = dtDiviPara.Rows[0]["RATE"].ToString();






        DataTable dtunPaidWarrantInHand = DRDAOObj.getDividendPayDetailsInfo(fundCode, divi_no, fy, "  AND (DIVIDEND.IS_BEFTN = 'Y') AND (DIVIDEND.BEFTN_RETURN_DT IS NOT NULL) AND (DIVIDEND.WAR_BK_PAY IS NOT NULL) ");


        dtunPaidWarrantInHand.TableName = "dtUnitDividendReconInfo";
       // dtunPaidWarrantInHand.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.Dividend\UI\ReportViewer\Report\dtUnitDividendReconInfo.xsd");

        ReportDocument rdoc = new ReportDocument();
        string Path = "";
        Path = Server.MapPath("ReportViewer/Report/crtReconDividendWarrantDetailsInfo.rpt");
        rdoc.Load(Path);
        rdoc.SetDataSource(dtunPaidWarrantInHand);
        rdoc.SetParameterValue("fundName", fundName);
        rdoc.SetParameterValue("RecordDate", recordDate);
        rdoc.SetParameterValue("AccountInfo", bankInfo);
        rdoc.SetParameterValue("FY", fy);
        rdoc.SetParameterValue("diviRate", diviRate);
        rdoc.SetParameterValue("status", status);

        //For  download
        rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "UnPaidWarrant" + DateTime.Now + ".pdf");
    }

    protected void fyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClosingDateDropDownList.DataSource = diviDAOObj.dtGetFYWiseClosinDate(fyDropDownList.SelectedItem.Text.ToString(), FundNameDropDownList.SelectedValue.ToString().ToUpper());
        ClosingDateDropDownList.DataTextField = "CLOSE_DT";
        ClosingDateDropDownList.DataValueField = "DIVI_NO";
        ClosingDateDropDownList.DataBind();
        Clear();
    }

    private void Clear()
    {
        fundNameLabel.Text = null;
        bankInfoLabel.Text = null;
        holderLabel.Text = null;
        unitsLabel.Text = null;
        rateLabel.Text = null;
        grossLabel.Text = null;
        taxAmountLabel.Text = null;
        netLabel.Text = null;
        paidLabel1.Text = null;
        paidAmountLabel1.Text = null;
        paidRemarksLabel1.Text = null;
        paidAfterReturnLabel.Text = null;
        paidAmountAfterReturnLabel.Text = null;
        paidAfterReturnRemarksLabel.Text = null;
        paidLabel2.Text = null;
        paidAmountLabel2.Text = null;
        paidRemarksLabel2.Text = null;
        paidLabel3.Text = null;
        paidAmountLabel3.Text = null;
        paidRemarksLabel3.Text = null;
        unpaidLabel1.Text = null;
        unpaidAmountLabel1.Text = null;
        unpaidRemarksLabel1.Text = null;
        unpaidLabel2.Text = null;
        unpaidAmountLabel2.Text = null;
        unpaidRemarksLabel2.Text = null;
        unpaidLabel3.Text = null;
        unpaidAmountLabel3.Text = null;
        unpaidRemarksLabel3.Text = null;
        unpaidLabel4.Text = null;
        unpaidAmountLabel4.Text = null;
        unpaidRemarksLabel4.Text = null;
        unpaidLabel5.Text = null;
        unpaidAmountLabel5.Text = null;
        unpaidRemarksLabel5.Text = null;
        unpaidLabel6.Text = null;
        unpaidAmountLabel6.Text = null;
        unpaidRemarksLabel6.Text = null;
    }

    protected void ClosingDateDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string fundCode = FundNameDropDownList.SelectedValue.ToString();
        int divi_no = Convert.ToInt32(ClosingDateDropDownList.SelectedValue.ToString());
        string fy = fyDropDownList.SelectedItem.Text.ToString();

        long no_units = 0;
        decimal net_amount = 0;

        // dividend process
        DataTable dtDiviPara = DRDAOObj.getDividendPara(fundCode, divi_no, fy, " ");
        DataTable dtDividendProceesInfo = DRDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, " ");
        string bankInfo = "AC#:" + dtDiviPara.Rows[0]["BK_AC_NO"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_NAME"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_ADDRS1"].ToString();
        bankInfo = bankInfo + " " + dtDiviPara.Rows[0]["BK_ADDRS2"].ToString();

        fundNameLabel.Text = FundNameDropDownList.SelectedItem.Text.ToString();
        bankInfoLabel.Text = bankInfo.ToString();
        holderLabel.Text = dtDividendProceesInfo.Rows[0]["NO_HOLDER"].ToString();
        unitsLabel.Text = dtDividendProceesInfo.Rows[0]["NO_UNITS"].ToString();
        rateLabel.Text = dtDiviPara.Rows[0]["RATE"].ToString();
        grossLabel.Text = dtDividendProceesInfo.Rows[0]["GROSS"].ToString();
        taxAmountLabel.Text = dtDividendProceesInfo.Rows[0]["TAX_AMOUNT"].ToString();
        netLabel.Text = dtDividendProceesInfo.Rows[0]["NET_AMOUNT"].ToString();

        //dividend Paid
        no_units = 0;
        net_amount = 0;

        DataTable dtPaidEFT = DRDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, "  AND (IS_BEFTN = 'Y') AND (BEFTN_RETURN_DT IS NULL)  ");
        paidLabel1.Text = dtPaidEFT.Rows[0]["NO_HOLDER"].ToString();
        paidAmountLabel1.Text = dtPaidEFT.Rows[0]["NET_AMOUNT"].ToString();

        no_units = no_units + Convert.ToInt64(dtPaidEFT.Rows[0]["NO_HOLDER"].ToString());
        net_amount = net_amount + Convert.ToDecimal(dtPaidEFT.Rows[0]["NET_AMOUNT"].ToString());

        DataTable dtPaidEFTAfterReturn = DRDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, "  AND (IS_BEFTN = 'Y') AND (BEFTN_RETURN_DT IS NOT NULL) ");
        paidAfterReturnLabel.Text = dtPaidEFTAfterReturn.Rows[0]["NO_HOLDER"].ToString();
        paidAmountAfterReturnLabel.Text = dtPaidEFTAfterReturn.Rows[0]["NET_AMOUNT"].ToString();

        no_units = no_units + Convert.ToInt64(dtPaidEFTAfterReturn.Rows[0]["NO_HOLDER"].ToString());
        net_amount = net_amount + Convert.ToDecimal(dtPaidEFTAfterReturn.Rows[0]["NET_AMOUNT"].ToString());

        DataTable dtPaidWarrant = DRDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'N') AND (WAR_BK_PAY IS NOT NULL) ");
        paidLabel2.Text = dtPaidWarrant.Rows[0]["NO_HOLDER"].ToString();
        paidAmountLabel2.Text = dtPaidWarrant.Rows[0]["NET_AMOUNT"].ToString();

        no_units = no_units + Convert.ToInt64(dtPaidWarrant.Rows[0]["NO_HOLDER"].ToString());
        net_amount = net_amount + Convert.ToDecimal(dtPaidWarrant.Rows[0]["NET_AMOUNT"].ToString());

        paidLabel3.Text = no_units.ToString();
        paidAmountLabel3.Text = net_amount.ToString();



        //dividend unpaid

        //no_units = 0;
        //net_amount = 0;

        //DataTable dtunPaidWarrantInHand = diviDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'NO') AND (IS_PAID IS NULL) AND (WAR_DELEVERY_BY IS NULL) AND ( WAR_IN_HAND_ENTRY_DATE IS NOT NULL) ");
        //unpaidLabel1.Text = dtunPaidWarrantInHand.Rows[0]["NO_HOLDER"].ToString();
        //unpaidAmountLabel1.Text = dtunPaidWarrantInHand.Rows[0]["NET_AMOUNT"].ToString();

        //no_units = no_units + Convert.ToInt64(dtunPaidWarrantInHand.Rows[0]["NO_HOLDER"].ToString());
        //net_amount = net_amount + Convert.ToDecimal(dtunPaidWarrantInHand.Rows[0]["NET_AMOUNT"].ToString());

        //DataTable dtunPaidEFTReturn = diviDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'YES') AND (IS_PAID IS  NULL) AND (WAR_DELEVERY_BY IS NULL) AND (BEFTN_RETURN_DATE IS NOT NULL) ");
        //unpaidLabel2.Text = dtunPaidEFTReturn.Rows[0]["NO_HOLDER"].ToString();
        //unpaidAmountLabel2.Text = dtunPaidEFTReturn.Rows[0]["NET_AMOUNT"].ToString();

        //no_units = no_units + Convert.ToInt64(dtunPaidEFTReturn.Rows[0]["NO_HOLDER"].ToString());
        //net_amount = net_amount + Convert.ToDecimal(dtunPaidEFTReturn.Rows[0]["NET_AMOUNT"].ToString());

        //DataTable dtunPaidWarrantPost = diviDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'NO') AND (IS_PAID IS NULL) AND (WAR_DELEVERY_BY IS NULL) AND ( WAR_IN_HAND_ENTRY_DATE IS NULL)  AND ( POST_DATE IS NOT NULL) ");
        //unpaidLabel3.Text = dtunPaidWarrantPost.Rows[0]["NO_HOLDER"].ToString();
        //unpaidAmountLabel3.Text = dtunPaidWarrantPost.Rows[0]["NET_AMOUNT"].ToString();

        //no_units = no_units + Convert.ToInt64(dtunPaidWarrantPost.Rows[0]["NO_HOLDER"].ToString());
        //net_amount = net_amount + Convert.ToDecimal(dtunPaidWarrantPost.Rows[0]["NET_AMOUNT"].ToString());

        //DataTable dtunPaidWarrantDelivered = diviDAOObj.getDividendProcessInfo(fundCode, divi_no, fy, " AND (IS_BEFTN = 'NO') AND (IS_PAID IS NULL) AND (WAR_DELEVERY_BY IS NOT NULL)  ");
        //unpaidLabel4.Text = dtunPaidWarrantDelivered.Rows[0]["NO_HOLDER"].ToString();
        //unpaidAmountLabel4.Text = dtunPaidWarrantDelivered.Rows[0]["NET_AMOUNT"].ToString();

        //no_units = no_units + Convert.ToInt64(dtunPaidWarrantDelivered.Rows[0]["NO_HOLDER"].ToString());
        //net_amount = net_amount + Convert.ToDecimal(dtunPaidWarrantDelivered.Rows[0]["NET_AMOUNT"].ToString());




        //unpaidLabel6.Text = no_units.ToString();
        //unpaidAmountLabel6.Text = net_amount.ToString();
    }


}
