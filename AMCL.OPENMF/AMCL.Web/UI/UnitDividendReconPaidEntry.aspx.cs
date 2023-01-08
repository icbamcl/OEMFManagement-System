﻿using System;
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

public partial class UI_UnitDividendReconPaidEntry : System.Web.UI.Page
{
    DiviReconCommonGateway DRcommonGatewayObj = new DiviReconCommonGateway();  
    OMFDAO opendMFDAO = new OMFDAO();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();
    JDiviReconDAO diviDAOObj = new JDiviReconDAO();
    //DiviReconBankStatementBL aDiviReconBankStatementBL = new DiviReconBankStatementBL();
    Message msgObj = new Message();
    NumberToEnglish numberToEnglishObj = new NumberToEnglish();
    private ReportDocument rdoc = new ReportDocument();


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

        Hashtable htWarrantDeliveredInfo = new Hashtable();

        string fundCode = FundNameDropDownList.SelectedValue.ToString();
        string fy = fyDropDownList.SelectedItem.Text.ToString();
        int diviNo = Convert.ToInt16(ClosingDateDropDownList.SelectedValue.ToString());

        try
        {
            DRcommonGatewayObj.BeginTransaction();

            string LoginID = Session["UserID"].ToString();
            htWarrantDeliveredInfo.Add("WAR_BK_PAY_USER", LoginID);
            htWarrantDeliveredInfo.Add("WAR_BK_PAY", "Y");


            int noOfupdatedRows = DRcommonGatewayObj.Update(htWarrantDeliveredInfo, "DIVIDEND", " WAR_BK_PAY IS NULL AND WAR_NO IN (" + warrantNosTextBox.Text.Trim() + ") AND FUND_CD=" + fundCode + " AND FY = '" + fy + "' AND DIVI_NO=" + diviNo);
            DRcommonGatewayObj.CommitTransaction();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + noOfupdatedRows + " Warrant Paid Update Successfully');", true);
            ClearFields();
        }
        catch (Exception ex)
        {
            DRcommonGatewayObj.RollbackTransaction();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert('Process Exucution Failed!!" + ex.Message.Replace("'", "").ToString() + "');", true);
        }
    }

    protected void FundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        fyDropDownList.DataSource = diviDAOObj.dtFY(FundNameDropDownList.SelectedValue.ToString());
        fyDropDownList.DataTextField = "F_YEAR";
        fyDropDownList.DataValueField = "F_YEAR";
        fyDropDownList.DataBind();
    }

    protected void resetButton_Click(object sender, EventArgs e)
    {
        warrantNosTextBox.Text = "";
        fyDropDownList.SelectedValue = "0";
        FundNameDropDownList.SelectedValue = "0";
    }

    protected void fyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClosingDateDropDownList.DataSource = diviDAOObj.dtGetFYWiseClosinDate(fyDropDownList.SelectedItem.Text.ToString(), FundNameDropDownList.SelectedValue.ToString().ToUpper());
        ClosingDateDropDownList.DataTextField = "CLOSE_DT";
        ClosingDateDropDownList.DataValueField = "DIVI_NO";
        ClosingDateDropDownList.DataBind();
    }
}
