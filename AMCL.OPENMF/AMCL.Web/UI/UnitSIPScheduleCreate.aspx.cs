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
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitSIPScheduleCreate : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    CommonGateway commonGatewayObj = new CommonGateway();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    BaseClass bcContent = new BaseClass();
    UnitSIPGetSet unitSIPObj = new UnitSIPGetSet();
    UnitSIPBL unitSIPBLObj = new UnitSIPBL();
    UnitReport reportObj = new UnitReport();
    string CDSStatus = "";
    UnitHolderRegistration regObj = new UnitHolderRegistration();
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
        CDSStatus = bcContent.CDS.ToString().ToUpper();
        regObj.FundCode = fundCode;
        regObj.BranchCode = branchCode;


        if (!IsPostBack)
        {
            sipNumberDropDownList.DataSource = unitSIPBLObj.dtSIPListDDL("AND SIP_MASTER.REG_BK = '" + regObj.FundCode + "' AND SIP_MASTER.REG_BR = '" + regObj.BranchCode + "' AND SIP_MASTER.SCHEDULE_CREATE_DATE IS NULL AND SIP_MASTER.SCHEDULE_CREATE_BY IS NULL ");
            sipNumberDropDownList.DataTextField = "SIP_NO";
            sipNumberDropDownList.DataValueField = "ID";
            sipNumberDropDownList.DataBind();

        }
       
    
    }
    
    
    private void ClearText()
    {                      
        sipInstallmentLabel.Text = "";
        NameLabel.Text = "";
        regNoLabel.Text = "";
        amountLabel.Text = "";
        payFreqLabel.Text = "";
        sipDayLabel.Text = "";
        EFTEndDateLabel.Text = "";
        EFTStartDateLabel.Text = "";
        durationLabel.Text = "";
        accountNoLabel.Text = "";
        bankInfoLabel.Text = "";
    }

    protected void sipNumberDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtSIPListforSchedule = unitSIPBLObj.dtSIPMasterList(" AND SIP_MASTER.ID="+sipNumberDropDownList.SelectedValue.ToString());
        if (dtSIPListforSchedule.Rows.Count > 0)
        {
            NameLabel.Text = dtSIPListforSchedule.Rows[0]["HNAME"].ToString();
            regNoLabel.Text = dtSIPListforSchedule.Rows[0]["REG_BK"].ToString()+"/"+dtSIPListforSchedule.Rows[0]["REG_BR"].ToString() + "/" + dtSIPListforSchedule.Rows[0]["REG_NO"].ToString();
            amountLabel.Text = dtSIPListforSchedule.Rows[0]["AMOUNT"].ToString();
            payFreqLabel.Text = dtSIPListforSchedule.Rows[0]["PAYMENT_FREQUENCY"].ToString().Equals("1") ?  "Monthly": "Qurterly" ;
            sipDayLabel.Text = dtSIPListforSchedule.Rows[0]["MONTH_START_DAY"].ToString().Equals("5") ? "5th" : "10th";          
            EFTStartDateLabel.Text = string.Format("{0:dd-MMM-yyyy}",dtSIPListforSchedule.Rows[0]["SIP_START_DATE"]);
            EFTEndDateLabel.Text = string.Format("{0:dd-MMM-yyyy}", dtSIPListforSchedule.Rows[0]["SIP_MATURE_DATE"]);
            durationLabel.Text = dtSIPListforSchedule.Rows[0]["SIP_DURATION_IN_MONTH"].ToString();
            accountNoLabel.Text = dtSIPListforSchedule.Rows[0]["SIP_ACC_NO"].ToString();
            sipInstallmentLabel.Text = unitSIPBLObj.getTotalInstallment(dtSIPListforSchedule).ToString();

            DataTable dtBankBracnhInfo = unitHolderRegBLObj.dtGetBankBracnhInfo(dtSIPListforSchedule.Rows[0]["SIP_ACC_ROUTING_NO"].Equals(DBNull.Value) ? "0" : dtSIPListforSchedule.Rows[0]["SIP_ACC_ROUTING_NO"].ToString());
            string bankInfo = "";
            if(dtBankBracnhInfo.Rows.Count>0)
            {
                bankInfo = reportObj.getBankNameByBankCode(Convert.ToInt32(dtBankBracnhInfo.Rows[0]["BANK_CODE"].ToString())).ToString();
                bankInfo=bankInfo+","+ reportObj.getBankBranchNameByCode(Convert.ToInt32(dtBankBracnhInfo.Rows[0]["BANK_CODE"].ToString()),Convert.ToInt32(dtBankBracnhInfo.Rows[0]["BRANCH_CODE"].ToString())).ToString()+"["+ dtSIPListforSchedule.Rows[0]["SIP_ACC_ROUTING_NO"] + "]";
                bankInfoLabel.Text = bankInfo;
            }
            
        }
        else
        {
            ClearText();
        }
    }


    protected void sipCreateScheduleButton_Click(object sender, EventArgs e)
    {
        DataTable dtSIPListforSchedule = unitSIPBLObj.dtSIPMasterList(" AND SIP_MASTER.ID=" + sipNumberDropDownList.SelectedValue.ToString());
        if (dtSIPListforSchedule.Rows.Count > 0)
        {
            DataTable dtTotalInslallList = unitSIPBLObj.dtGetTotalInstallmentList(dtSIPListforSchedule, CDSStatus);
            if(dtTotalInslallList.Rows.Count>0)
            {
                SaleListGridView.DataSource = dtTotalInslallList;
                SaleListGridView.DataBind();
            }
            else
            {
                ClearText();
                SaleListGridView.DataSource = dtGetNullTableForGrid();
                SaleListGridView.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);
          
        }
    }

    protected void sipSaveButton_Click(object sender, EventArgs e)
    {
        DataTable dtSIPListforSchedule = unitSIPBLObj.dtSIPMasterList(" AND SIP_MASTER.ID=" + sipNumberDropDownList.SelectedValue.ToString());
        if (dtSIPListforSchedule.Rows.Count > 0)
        {
            DataTable dtTotalInslallList = unitSIPBLObj.dtGetTotalInstallmentList(dtSIPListforSchedule,CDSStatus);
            if (dtTotalInslallList.Rows.Count > 0)
            {
                try
                {                 
                    unitSIPBLObj.SaveSIPSchedule(dtTotalInslallList, userObj);
                    sipNumberDropDownList.DataSource = unitSIPBLObj.dtSIPListDDL("AND SIP_MASTER.REG_BK = '" + regObj.FundCode + "' AND SIP_MASTER.REG_BR = '" + regObj.BranchCode + "' AND SIP_MASTER.SCHEDULE_CREATE_DATE IS NULL AND SIP_MASTER.SCHEDULE_CREATE_BY IS NULL ");
                    sipNumberDropDownList.DataTextField = "SIP_NO";
                    sipNumberDropDownList.DataValueField = "ID";
                    sipNumberDropDownList.DataBind();

                    SaleListGridView.DataSource = dtGetNullTableForGrid();
                    SaleListGridView.DataBind();

                    ClearText();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Saved Successfully');", true);

                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert (' Save Failed:" + msgObj.Error().ToString() + " " + ex.Message.Replace("'", "").ToString() + "');", true);
                }
            }
            else
            {
                SaleListGridView.DataSource = dtGetNullTableForGrid();
                SaleListGridView.DataBind();
                ClearText();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('Save Failed:No Data Found');", true);
            }
        }
        else
        {
            SaleListGridView.DataSource = dtGetNullTableForGrid();
            SaleListGridView.DataBind();            
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert (' Save Failed:No Data Found');", true);
            ClearText();
        }
    }
    private DataTable dtGetNullTableForGrid()
    {
        DataTable dtTotalInstallmentList = new DataTable();
        dtTotalInstallmentList.Columns.Add("REG_BK", typeof(string));
        dtTotalInstallmentList.Columns.Add("REG_BR", typeof(string));
        dtTotalInstallmentList.Columns.Add("REG_NO", typeof(string));

        dtTotalInstallmentList.Columns.Add("HNAME", typeof(string));
        dtTotalInstallmentList.Columns.Add("SIP_NO", typeof(string));
        dtTotalInstallmentList.Columns.Add("SCHEDULE_NO", typeof(string));

        dtTotalInstallmentList.Columns.Add("DEBIT_DATE", typeof(string));
        dtTotalInstallmentList.Columns.Add("AMOUNT", typeof(string));

        return dtTotalInstallmentList;
    }

    protected void sipCreateScheduleButtonNew_Click(object sender, EventArgs e)
    {
        DataTable dtNewSIPList = commonGatewayObj.Select("select * from sip_details where debit_date>='01-Jan-2022' and reg_bk not in ('IAMCL','IAMPH','IUF','CFUF','BDF') and sip_no not in (select sip from sip_master where sip_date>) ORDER BY REG_BK, SIP_NO");
        if (dtNewSIPList.Rows.Count > 0)
        {

            for (int loop = 0; loop < dtNewSIPList.Rows.Count; loop++)
            {
                string debit_date_start = "10-Jan-2023";
               
                int scheduleNo = Convert.ToInt32(dtNewSIPList.Rows[loop]["SCHEDULE_NO"]) ;
                int SIP_NO = Convert.ToInt32(dtNewSIPList.Rows[loop]["SIP_NO"]);
                string reg_bk = dtNewSIPList.Rows[loop]["REG_BK"].ToString();
                string reg_br = dtNewSIPList.Rows[loop]["REG_BR"].ToString();
                int reg_no = Convert.ToInt32(dtNewSIPList.Rows[loop]["REG_NO"]);

                DataTable dtSIPDetails = unitSIPBLObj.dtSIPMasterListRemaining(" AND SIP_MASTER.SIP_NO=" + SIP_NO + " AND SIP_MASTER.REG_NO=" + reg_no + " AND SIP_MASTER.REG_BK='" + reg_bk + "' AND SIP_MASTER.REG_BR='" + reg_br + "'");
                string debit_date_end =Convert.ToDateTime( dtSIPDetails.Rows[loop]["SIP_MATURE_DATE"]).ToString("dd-MMM-yyyy");
                int remainingMonth = Convert.ToInt32(dtSIPDetails.Rows[loop]["SIP_DURATION_IN_MONTH"].ToString()) - scheduleNo;

                DataTable dtTotalInslallList = unitSIPBLObj.dtGetTotalInstallmentListRemaining(dtSIPDetails, debit_date_start, debit_date_end, remainingMonth, scheduleNo+1);
                if (dtTotalInslallList.Rows.Count > 0)
                {
                    try
                    {
                      unitSIPBLObj.SaveSIPScheduleRemaining(dtTotalInslallList, userObj);



                    }
                    catch (Exception ex)
                    {

                    }
                }

            }

        }
    }
}
