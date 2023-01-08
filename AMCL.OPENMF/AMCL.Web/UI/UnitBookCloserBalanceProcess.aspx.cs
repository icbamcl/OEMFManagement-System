using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Text;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitBookCloserBalanceProcess : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();
    dividendDAO diviDAOObj = new dividendDAO();




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

        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = opendMFDAO.dtFundList();
            fundNameDropDownList.DataTextField = "FUND_NM";
            fundNameDropDownList.DataValueField = "FUND_CD";
            fundNameDropDownList.DataBind();
            branchNameDropDownList.DataSource = opendMFDAO.dtBranchList();
            branchNameDropDownList.DataTextField = "BR_NM";
            branchNameDropDownList.DataValueField = "BR_CD";
            branchNameDropDownList.DataBind();
            asOnDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");

        }

    }



    protected void startProcessButton_Click(object sender, EventArgs e)
    {
        try
        {
            userObj.UserID = bcContent.LoginID.ToString();
            commonGatewayObj.BeginTransaction();
            long balance = 0;
            long saleUnit = 0;
            long repUnit = 0;
            long trINUnit = 0;
            long trOUTUnit = 0;
            long countRow = 0;
            string fundCode = fundNameDropDownList.SelectedValue.ToString();
            string branchCode = branchNameDropDownList.SelectedValue.ToString();
            string asOnDate = asOnDateTextBox.Text.ToString();

            DataTable dtUmaster = new DataTable();
            Hashtable htBookCloser = new Hashtable();
            string querySQL = " SELECT * FROM U_MASTER WHERE 1=1 ";
            if (fundCode.ToString() != "0")
            {
                querySQL = querySQL + " AND REG_BK='" + fundCode.ToString() + "'";
            }
            else if (branchCode.ToString() != "0")
            {
                querySQL = querySQL + " AND REG_BR='" + branchCode.ToString() + "'";
            }

            querySQL = querySQL + " ORDER BY REG_BK, REG_BR, REG_NO ";
            dtUmaster = commonGatewayObj.Select(querySQL.ToString());
            if (dtUmaster.Rows.Count > 0)
            {
                for (int loop = 0; loop < dtUmaster.Rows.Count; loop++)
                {
                    htBookCloser = new Hashtable();

                    htBookCloser.Add("ID", commonGatewayObj.GetMaxNo("U_MASTER_BK_CLOSER", "ID") + 1);
                    htBookCloser.Add("BOOK_CLOSER_DATE", asOnDate);
                    htBookCloser.Add("REG_BK", dtUmaster.Rows[loop]["REG_BK"]);
                    htBookCloser.Add("REG_BR", dtUmaster.Rows[loop]["REG_BR"]);
                    htBookCloser.Add("REG_NO", dtUmaster.Rows[loop]["REG_NO"]);
                    htBookCloser.Add("REG_DT", dtUmaster.Rows[loop]["REG_DT"]);
                    htBookCloser.Add("REG_TYPE", dtUmaster.Rows[loop]["REG_TYPE"]);
                    htBookCloser.Add("HNAME", dtUmaster.Rows[loop]["HNAME"]);

                    htBookCloser.Add("FMH_NAME", dtUmaster.Rows[loop]["FMH_NAME"]);
                    htBookCloser.Add("ADDRS1", dtUmaster.Rows[loop]["ADDRS1"]);
                    htBookCloser.Add("ADDRS2", dtUmaster.Rows[loop]["ADDRS2"]);
                    htBookCloser.Add("CITY", dtUmaster.Rows[loop]["CITY"]);
                    htBookCloser.Add("EMAIL", dtUmaster.Rows[loop]["EMAIL"]);
                    htBookCloser.Add("CIP", dtUmaster.Rows[loop]["CIP"]);
                    htBookCloser.Add("ID_FLAG", dtUmaster.Rows[loop]["ID_FLAG"]);
                    htBookCloser.Add("ID_AC", dtUmaster.Rows[loop]["ID_AC"]);

                    htBookCloser.Add("OCC_CODE", dtUmaster.Rows[loop]["OCC_CODE"]);
                    htBookCloser.Add("BK_AC_NO", dtUmaster.Rows[loop]["BK_AC_NO"]);
                    htBookCloser.Add("BK_NM_CD", dtUmaster.Rows[loop]["BK_NM_CD"]);
                    htBookCloser.Add("BK_BR_NM_CD", dtUmaster.Rows[loop]["BK_BR_NM_CD"]);
                    htBookCloser.Add("ROUTING_NO", dtUmaster.Rows[loop]["ROUTING_NO"]);

                    htBookCloser.Add("ID_BK_NM_CD", dtUmaster.Rows[loop]["ID_BK_NM_CD"]);
                    htBookCloser.Add("ID_BK_BR_NM_CD", dtUmaster.Rows[loop]["ID_BK_BR_NM_CD"]);
                    htBookCloser.Add("MO_NAME", dtUmaster.Rows[loop]["MO_NAME"]);
                    htBookCloser.Add("IS_BEFTN", dtUmaster.Rows[loop]["IS_BEFTN"]);
                    htBookCloser.Add("BO", dtUmaster.Rows[loop]["BO"]);


                    htBookCloser.Add("ALLOT_NO", dtUmaster.Rows[loop]["ALLOT_NO"]);
                    htBookCloser.Add("FOLIO_NO", dtUmaster.Rows[loop]["FOLIO_NO"]);                   
                    htBookCloser.Add("MOBILE1", dtUmaster.Rows[loop]["MOBILE1"]);

                    htBookCloser.Add("OPN_BAL", dtUmaster.Rows[loop]["BALANCE"]);
                    UnitBookCloserBL bookcloserBL = new UnitBookCloserBL();
                    saleUnit = bookcloserBL.saleUnit(dtUmaster.Rows[loop]["REG_BK"].ToString(), dtUmaster.Rows[loop]["REG_BR"].ToString(), Convert.ToInt32(dtUmaster.Rows[loop]["REG_NO"]), asOnDate);
                    repUnit = bookcloserBL.repUnit(dtUmaster.Rows[loop]["REG_BK"].ToString(), dtUmaster.Rows[loop]["REG_BR"].ToString(), Convert.ToInt32(dtUmaster.Rows[loop]["REG_NO"]), asOnDate);
                    trINUnit = bookcloserBL.trINUnit(dtUmaster.Rows[loop]["REG_BK"].ToString(), dtUmaster.Rows[loop]["REG_BR"].ToString(), Convert.ToInt32(dtUmaster.Rows[loop]["REG_NO"]), asOnDate);
                    trOUTUnit = bookcloserBL.trOUTUnit(dtUmaster.Rows[loop]["REG_BK"].ToString(), dtUmaster.Rows[loop]["REG_BR"].ToString(), Convert.ToInt32(dtUmaster.Rows[loop]["REG_NO"]), asOnDate);
                    balance = saleUnit - repUnit + trINUnit - trOUTUnit;

                    htBookCloser.Add("BALANCE", balance);
                    htBookCloser.Add("SL", saleUnit);
                    htBookCloser.Add("REP", repUnit);
                    htBookCloser.Add("TR_IN", trINUnit);
                    htBookCloser.Add("TR_OUT", trOUTUnit);

                    htBookCloser.Add("USER_NM", userObj.UserID);
                   // htInsertBookCertInfo.Add("ENT_TM", DateTime.Now.ToShortTimeString().ToString());
                    htBookCloser.Add("ENT_DT", DateTime.Now.ToShortTimeString().ToString());
                    commonGatewayObj.Insert(htBookCloser, "U_MASTER_BK_CLOSER");
                    commonGatewayObj.CommitTransaction();
                    countRow++;
                }
                ProcssResultLabel.Text = countRow + " Record Proceesed Success!!";
            }

        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            ProcssResultLabel.Text = "Process Failed:" + ex.ToString();

        }
    }
}

