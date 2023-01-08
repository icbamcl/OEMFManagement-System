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
using CrystalDecisions.Shared;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitReportPriceRefixation : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();
    dividendDAO diviDAOObj = new dividendDAO();
    Message msgObj = new Message();

    AMCL.REPORT.CR_PriceRefixationAllFundLast CR_PriceRefixation = new AMCL.REPORT.CR_PriceRefixationAllFundLast();

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

            refixationDateFromDropDownList.DataSource = reportObj.dtFillFixationDate(" ");
            refixationDateFromDropDownList.DataTextField = "NAME";            
            refixationDateFromDropDownList.DataValueField = "ID";
            refixationDateFromDropDownList.SelectedValue = "0";
            refixationDateFromDropDownList.DataBind();

            refixationDateToDropDownList.DataSource = reportObj.dtFillFixationDate(" ");
            refixationDateToDropDownList.DataTextField = "NAME";
            refixationDateToDropDownList.DataValueField = "ID";
            refixationDateToDropDownList.SelectedValue = "0";
            refixationDateToDropDownList.DataBind();

            effectiveDateFromDropDownList.DataSource = reportObj.dtFillFixationDate(" ");
            effectiveDateFromDropDownList.DataTextField = "NAME";
            effectiveDateFromDropDownList.DataValueField = "ID";
            effectiveDateFromDropDownList.SelectedValue = "0";
            effectiveDateFromDropDownList.DataBind();

            effectiveDateToDropDownList.DataSource = reportObj.dtFillFixationDate(" ");
            effectiveDateToDropDownList.DataTextField = "NAME";
            effectiveDateToDropDownList.DataValueField = "ID";
            effectiveDateToDropDownList.SelectedValue = "0";
            effectiveDateToDropDownList.DataBind();


            DataTable dtFundInfoDetails = reportObj.dtFundInfoDetails("   ");
            DataTable dtPriceDetails = dtPriceDetails = reportObj.dtPriceDetails(" AND FUND_CD='" + dtFundInfoDetails.Rows[0]["FUND_CD"].ToString().ToUpper() + "'  AND REFIX_DT=(SELECT MAX (REFIX_DT) FROM PRICE_REFIX WHERE FUND_CD='" + dtFundInfoDetails.Rows[0]["FUND_CD"].ToString().ToUpper() + "') ");
            for (int looper = 1; looper < dtFundInfoDetails.Rows.Count; looper++)
            {
                DataTable dtprice = reportObj.dtPriceDetails(" AND FUND_CD='" + dtFundInfoDetails.Rows[looper]["FUND_CD"].ToString().ToUpper() + "'  AND REFIX_DT=(SELECT MAX (REFIX_DT) FROM PRICE_REFIX WHERE FUND_CD='" + dtFundInfoDetails.Rows[looper]["FUND_CD"].ToString().ToUpper() + "') ");
                dtPriceDetails.Merge(dtprice);
            }
            dvGridSurrender.Visible = true;
            SurrenderListGridView.DataSource = dtPriceDetails;
            SurrenderListGridView.DataBind();
        }

    }
   
    
    protected void CloseButton_Click(object sender, EventArgs e)
    {         
        Response.Redirect("UnitHome.aspx");
           
    }



   

    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (fundNameDropDownList.SelectedValue.ToString() != "0")
        {
            refixationDateFromDropDownList.DataSource = reportObj.dtFillFixationDate(" AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString() + "'");
            refixationDateFromDropDownList.DataTextField = "NAME";
            refixationDateFromDropDownList.DataValueField = "ID";
            refixationDateFromDropDownList.DataBind();

            refixationDateToDropDownList.DataSource = reportObj.dtFillFixationDate(" AND FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString() + "'");
            refixationDateToDropDownList.DataTextField = "NAME";
            refixationDateToDropDownList.DataValueField = "ID";
            refixationDateToDropDownList.DataBind();

            effectiveDateFromDropDownList.DataSource = reportObj.dtFillEffectiveDate(" AND FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString() + "'");
            effectiveDateFromDropDownList.DataTextField = "NAME";
            effectiveDateFromDropDownList.DataValueField = "ID";
            effectiveDateFromDropDownList.DataBind();

            effectiveDateToDropDownList.DataSource = reportObj.dtFillEffectiveDate(" AND FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString() + "'");
            effectiveDateToDropDownList.DataTextField = "NAME";
            effectiveDateToDropDownList.DataValueField = "ID";
            effectiveDateToDropDownList.DataBind();
        }
        else
        {
            refixationDateFromDropDownList.DataSource = reportObj.dtFillFixationDate(" ");
            refixationDateFromDropDownList.DataTextField = "NAME";
            refixationDateFromDropDownList.DataValueField = "ID";
            refixationDateFromDropDownList.DataBind();

            refixationDateToDropDownList.DataSource = reportObj.dtFillFixationDate(" ");
            refixationDateToDropDownList.DataTextField = "NAME";
            refixationDateToDropDownList.DataValueField = "ID";
            refixationDateToDropDownList.DataBind();

            effectiveDateFromDropDownList.DataSource = reportObj.dtFillEffectiveDate(" ");
            effectiveDateFromDropDownList.DataTextField = "NAME";
            effectiveDateFromDropDownList.DataValueField = "ID";
            effectiveDateFromDropDownList.DataBind();

            effectiveDateToDropDownList.DataSource = reportObj.dtFillEffectiveDate(" ");
            effectiveDateToDropDownList.DataTextField = "NAME";
            effectiveDateToDropDownList.DataValueField = "ID";
            effectiveDateToDropDownList.DataBind();
        }


    }

    protected void findButton_Click(object sender, EventArgs e)
    {
        StringBuilder sbQuery = new StringBuilder();
        sbQuery.Append("SELECT PRICE_REFIX.*,TO_CHAR(REFIX_DT,'DD-MON-YYYY') AS REFIX_DATE,TO_CHAR(EFFECTIVE_DATE,'DD-MON-YYYY') AS EFFECTIVE_DT  FROM PRICE_REFIX WHERE 1=1 ");
        if (fundNameDropDownList.SelectedValue != "0") 
        {
            sbQuery.Append(" AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString() + "'");
        }

        if (refixationDateFromDropDownList.SelectedValue != "0" && refixationDateToDropDownList.SelectedValue == "0")
        {
            sbQuery.Append("AND REFIX_DT>='" + refixationDateFromDropDownList.SelectedItem.Text.ToString() + "'");
        }
        else if (refixationDateFromDropDownList.SelectedValue == "0" && refixationDateToDropDownList.SelectedValue != "0")
        {
            sbQuery.Append("AND REFIX_DT<='" + refixationDateToDropDownList.SelectedItem.Text.ToString() + "'");
        }
        else if (refixationDateFromDropDownList.SelectedValue != "0" && refixationDateToDropDownList.SelectedValue != "0")
        {
            sbQuery.Append("AND REFIX_DT BETWEEN '" + refixationDateFromDropDownList.SelectedItem.Text.ToString() + "' AND  '" + refixationDateToDropDownList.SelectedItem.Text.ToString() + "'");
        }


        if (effectiveDateFromDropDownList.SelectedValue != "0" && effectiveDateToDropDownList.SelectedValue == "0")
        {
            sbQuery.Append("AND REFIX_DT>='" + effectiveDateFromDropDownList.SelectedItem.Text.ToString() + "'");
        }
        else if (effectiveDateFromDropDownList.SelectedValue == "0" && effectiveDateToDropDownList.SelectedValue != "0")
        {
            sbQuery.Append("AND REFIX_DT<='" + effectiveDateToDropDownList.SelectedItem.Text.ToString() + "'");
        }
        else if (effectiveDateFromDropDownList.SelectedValue != "0" && effectiveDateToDropDownList.SelectedValue != "0")
        {
            sbQuery.Append("AND REFIX_DT BETWEEN '" + effectiveDateFromDropDownList.SelectedItem.Text.ToString() + "' AND  '" + effectiveDateToDropDownList.SelectedItem.Text.ToString() + "'");
        }


        sbQuery.Append(" ORDER BY REFIX_DT DESC, EFFECTIVE_DATE DESC");

        DataTable dtPriceInfo = commonGatewayObj.Select(sbQuery.ToString());
        if (dtPriceInfo.Rows.Count > 0)
        {
            dvGridSurrender.Visible = true;
            SurrenderListGridView.DataSource = dtPriceInfo;
            SurrenderListGridView.DataBind();
        }
        else
        {
            dvGridSurrender.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert('No Data Found');", true);
        }
    }

  

    protected void printButton_Click(object sender, EventArgs e)
    {

        StringBuilder sbQuery = new StringBuilder();
        sbQuery.Append("SELECT * FROM PRICE_REFIX WHERE 1=1 ");
        if (fundNameDropDownList.SelectedValue != "0")
        {
            sbQuery.Append(" AND FUND_CD='" + fundNameDropDownList.SelectedValue.ToString() + "'");
        }

        if (refixationDateFromDropDownList.SelectedValue != "0" && refixationDateToDropDownList.SelectedValue == "0")
        {
            sbQuery.Append("AND REFIX_DT>='" + refixationDateFromDropDownList.SelectedItem.Text.ToString() + "'");
        }
        else if (refixationDateFromDropDownList.SelectedValue == "0" && refixationDateToDropDownList.SelectedValue != "0")
        {
            sbQuery.Append("AND REFIX_DT<='" + refixationDateToDropDownList.SelectedItem.Text.ToString() + "'");
        }
        else if (refixationDateFromDropDownList.SelectedValue != "0" && refixationDateToDropDownList.SelectedValue != "0")
        {
            sbQuery.Append("AND REFIX_DT BETWEEN '" + refixationDateFromDropDownList.SelectedItem.Text.ToString() + "' AND  '" + refixationDateToDropDownList.SelectedItem.Text.ToString() + "'");
        }


        if (effectiveDateFromDropDownList.SelectedValue != "0" && effectiveDateToDropDownList.SelectedValue == "0")
        {
            sbQuery.Append("AND REFIX_DT>='" + effectiveDateFromDropDownList.SelectedItem.Text.ToString() + "'");
        }
        else if (effectiveDateFromDropDownList.SelectedValue == "0" && effectiveDateToDropDownList.SelectedValue != "0")
        {
            sbQuery.Append("AND REFIX_DT<='" + effectiveDateToDropDownList.SelectedItem.Text.ToString() + "'");
        }
        else if (effectiveDateFromDropDownList.SelectedValue != "0" && effectiveDateToDropDownList.SelectedValue != "0")
        {
            sbQuery.Append("AND REFIX_DT BETWEEN '" + effectiveDateFromDropDownList.SelectedItem.Text.ToString() + "' AND  '" + effectiveDateToDropDownList.SelectedItem.Text.ToString() + "'");
        }


        sbQuery.Append(" ORDER BY REFIX_DT DESC, EFFECTIVE_DATE DESC");

        DataTable dtPriceInfo = commonGatewayObj.Select(sbQuery.ToString());
        if (dtPriceInfo.Rows.Count > 0)
        {

            dtPriceInfo.TableName = "dtPriceInfo";
          //  dtPriceInfo.WriteXmlSchema(@"F:\GITHUB_AMCL\DOTNET2015\AMCL.OPENMF\AMCL.REPORT\XMLSCHEMAS\dtPriceRefixation.xsd");
            CR_PriceRefixation.Refresh();
            CR_PriceRefixation.SetDataSource(dtPriceInfo);
            //CR_PriceRefixation.SetParameterValue("user_name", DtUserInfo.Rows[0]["USER_NM"].ToString());
            //CR_PriceRefixation.SetParameterValue("fundName", dtMneyReceiptDetails.Rows[0]["FUND_NM"].ToString());
            //CR_PriceRefixation.SetParameterValue("total_units_word", numberToEnglisObj.changeNumericToWords(Convert.ToDecimal(dtMneyReceiptDetails.Rows[0]["UNIT_QTY"].ToString())).ToString());
            //CR_PriceRefixation.SetParameterValue("totalAmount", totalAmount);
            //CR_PriceRefixation.SetParameterValue("TotalAmountInWord", numberToEnglisObj.changeNumericToWords(totalAmount).ToString());
            //CR_PriceRefixation.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "SaleMoneyReceipt" + DateTime.Now + ".pdf");
        }
        else
        {
            
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert('No Data Found');", true);
        }
    }

    protected void printAllFundButton_Click(object sender, EventArgs e)
    {
        DataTable dtFundInfoDetails = reportObj.dtFundInfoDetails("   ");
        DataTable dtPriceDetails = dtPriceDetails = reportObj.dtPriceDetails(" AND FUND_CD='" + dtFundInfoDetails.Rows[0]["FUND_CD"].ToString().ToUpper() + "'  AND REFIX_DT=(SELECT MAX (REFIX_DT) FROM PRICE_REFIX WHERE FUND_CD='" + dtFundInfoDetails.Rows[0]["FUND_CD"].ToString().ToUpper() + "') ");
        for (int looper = 1; looper < dtFundInfoDetails.Rows.Count; looper++)
        {
            DataTable dtprice = reportObj.dtPriceDetails(" AND FUND_CD='" + dtFundInfoDetails.Rows[looper]["FUND_CD"].ToString().ToUpper() + "'  AND REFIX_DT=(SELECT MAX (REFIX_DT) FROM PRICE_REFIX WHERE FUND_CD='" + dtFundInfoDetails.Rows[looper]["FUND_CD"].ToString().ToUpper() + "') ");
            dtPriceDetails.Merge(dtprice);
        }

        dtPriceDetails.TableName = "dtPriceInfo";      
        CR_PriceRefixation.Refresh();
        CR_PriceRefixation.SetDataSource(dtPriceDetails);
        CR_PriceRefixation.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "AllFundLastPrice" + DateTime.Now + ".pdf");
    }
}
