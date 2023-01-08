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
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitSMSSend : System.Web.UI.Page
{
    //System.Web.UI.Page this_page_ref = null;
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    UnitUser userObj = new UnitUser();

    UnitRepurchaseBL unitRepBLObj = new UnitRepurchaseBL();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
    BaseClass bcContent = new BaseClass();
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
        //spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());

        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = opendMFDAO.dtFundList();
            fundNameDropDownList.DataTextField = "FUND_NM";
            fundNameDropDownList.DataValueField = "FUND_CD";
            fundNameDropDownList.DataBind();

            dvGridSurrender.Visible = true;
            SMSListGridView.DataSource = unitRepBLObj.dtSMSList("0", "0");
            SMSListGridView.DataBind();


        }

    }



    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (fundNameDropDownList.SelectedValue != "0")
        {
            dvGridSurrender.Visible = true;
            SMSListGridView.DataSource = unitRepBLObj.dtSMSList("0", fundNameDropDownList.SelectedValue.ToString());
            SMSListGridView.DataBind();
        }
        else
        {


            dvGridSurrender.Visible = true;
            SMSListGridView.DataSource = unitRepBLObj.dtSMSList("0", "0");
            SMSListGridView.DataBind();
        }
    }



    protected void SMSGenerateButton_Click(object sender, EventArgs e)
    {
        try
        {
            int countCheck = 0;
            int countUpdateRow = 0;
            DataTable dtSMS = new DataTable();
            dtSMS.Columns.Add("MOBILE", typeof(string));
            dtSMS.Columns.Add("SMS_TEXT", typeof(string));
            DataRow drSMS;
            string SMS_Type = "";
            foreach (GridViewRow Drv in SMSListGridView.Rows)
            {
                commonGatewayObj.BeginTransaction();
                Hashtable htUpdate = new Hashtable();

                CheckBox leftCheckBox = (CheckBox)SMSListGridView.Rows[countCheck].FindControl("leftCheckBox");

                if (leftCheckBox.Checked)
                {
                    drSMS = dtSMS.NewRow();
                    drSMS["MOBILE"] = Drv.Cells[6].Text.ToString();
                    drSMS["SMS_TEXT"] = Drv.Cells[7].Text.ToString();
                    dtSMS.Rows.Add(drSMS);
                    SMS_Type = Drv.Cells[9].Text.ToString();
                    htUpdate.Add("SMS_SENT_DATE", DateTime.Now);
                    htUpdate.Add("SMS_SENT_BY", userObj.UserID.ToString());
                    if (SMS_Type == "CHQ")
                    {
                        commonGatewayObj.Update(htUpdate, "REPURCHASE", "REG_BK='" + Drv.Cells[1].Text.ToString() + "' AND REG_BR='" + Drv.Cells[2].Text.ToString() + "' AND REG_NO='" + Drv.Cells[3].Text.ToString() + "' AND REP_NO='" + Drv.Cells[4].Text.ToString() + "'");
                    }

                    countUpdateRow++;
                }

                countCheck++;

            }

            if (dtSMS.Rows.Count > 0)
            {

                dtSMS.TableName = "SMS";
                //  dtSMS.WriteXmlSchema(@"F:\\GITHUB_PROJECT\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtSMS.xsd");
                CR_SMS.Refresh();
                CR_SMS.SetDataSource(dtSMS);
                //CR_MoneyReceiptSale.SetParameterValue("user_name", DtUserInfo.Rows[0]["USER_NM"].ToString());
                //CR_MoneyReceiptSale.SetParameterValue("fundName", dtMneyReceiptDetails.Rows[0]["FUND_NM"].ToString());
                //CR_MoneyReceiptSale.SetParameterValue("total_units_word", numberToEnglisObj.changeNumericToWords(Convert.ToDecimal(dtMneyReceiptDetails.Rows[0]["UNIT_QTY"].ToString())).ToString());
                //CR_MoneyReceiptSale.SetParameterValue("totalAmount", totalAmount);
                //CR_MoneyReceiptSale.SetParameterValue("TotalAmountInWord", numberToEnglisObj.changeNumericToWords(totalAmount).ToString());
                commonGatewayObj.CommitTransaction();


                SMSType.SelectedValue = "0";
                fundNameDropDownList.SelectedValue = "0";
                dvGridSurrender.Visible = false;
                CR_SMS.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, Response, true, "SMS" + DateTime.Now);              

            }
            else
            {
                dvGridSurrender.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
            }
        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('SMS generate failed!!');", true);
        }



    }
}