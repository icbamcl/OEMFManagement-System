using System;
using System.Data;
using System.Web.UI;
using System.Text;
using System.IO;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitDematePrintFolioTransferForm : System.Web.UI.Page
{
    System.Web.UI.Page this_page_ref = null;
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();    
    UnitUser userObj = new UnitUser();

    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
    BaseClass bcContent = new BaseClass();
    string fundCode = "";
    string branchCode = "";
    string printType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (BaseContent.IsSessionExpired())
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        bcContent = (BaseClass)Session["BCContent"];

        userObj.UserID = bcContent.LoginID.ToString();
        fundCode = bcContent.FundCode.ToString();
        branchCode = bcContent.BranchCode.ToString();
        spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
                                       
        if (!IsPostBack)
        {

            DRRefNoDropDownList.DataSource = unitSaleBLObj.dtDRRefNoListForFolioTransfer("AND SALE.REG_BK='" + fundCode + "'");
            DRRefNoDropDownList.DataTextField = "NAME";
            DRRefNoDropDownList.DataValueField = "ID";
            DRRefNoDropDownList.DataBind();


        }
    
    }

     
    protected void DRRefNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtSaleInfoForDRF = unitSaleBLObj.dtGetSaleInfoForFolioTransferPrint(" AND SALE.REG_BK='" + fundCode + "' AND SALE.REG_BR='" + branchCode + "' AND SALE.DRF_REF_NO=" + DRRefNoDropDownList.SelectedValue.ToString());       

        if (dtSaleInfoForDRF.Rows.Count > 0)
        {
            SaleListGridView.DataSource = dtSaleInfoForDRF;
            SaleListGridView.DataBind();
           
        }
        else
        {
            SaleListGridView.DataSource = dtSaleInfoForDRF;
            SaleListGridView.DataBind();                      
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('No Data Found');", true);

        }
    }

   

    protected void PrintTransferButton_Click(object sender, EventArgs e)
    {
        try
        {
            printType = "SL_TR";
            StringBuilder sbMaster = new StringBuilder();
            DataTable dtReportStatement = new DataTable();

            sbMaster.Append("SELECT A.FUND_CD, C.SL_NO, A.FUND_NM,A.ISIN_NO,UPPER(D.DP_NAME) AS CUST_DP_NAME, D.DP_ID CUST_DP_ID,A.SALE_BO_NAME,A.SALE_OF_UNIT_BO,C.QTY,UPPER(E.DP_NAME) AS HOLDER_DP_NAME,C.HOLDER_DP_ID,B.HNAME,C.HOLDER_BO,'Folio TR Ref No: '||C.DRF_REF_NO AS DRF_REF_NO ");
            sbMaster.Append(" FROM  (SELECT FUND_CD, UPPER(FUND_INFO.FUND_NM) AS FUND_NM ,  ISIN_NO,OMNIBUS_FOLIO_BO_NAME AS  SALE_BO_NAME ,OMNIBUS_FOLIO_BO AS SALE_OF_UNIT_BO, SUBSTR(OMNIBUS_FOLIO_BO, 4, 5) AS CUST_DP_ID  FROM FUND_INFO ) A ");
            sbMaster.Append(" INNER JOIN ( SELECT DP_ID,DP_NAME FROM AMCL_DIVIDEND.CDBL_DP_LIST) D ON A.CUST_DP_ID=D.DP_ID  INNER JOIN ( SELECT REG_BK,REG_BR,REG_NO,HNAME FROM U_MASTER) B ");
            sbMaster.Append(" ON B.REG_BK=A.FUND_CD  INNER JOIN ( SELECT REG_BK,REG_BR,REG_NO,SL_NO,HOLDER_BO,SUBSTR(HOLDER_BO, 4, 5) AS HOLDER_DP_ID,QTY,DRF_REF_NO FROM SALE) C ");
            sbMaster.Append(" ON B.REG_BK=C.REG_BK AND B.REG_BR=C.REG_BR AND B.REG_NO=C.REG_NO INNER JOIN (  SELECT DP_ID,DP_NAME FROM AMCL_DIVIDEND.CDBL_DP_LIST) E  ON C.HOLDER_DP_ID=E.DP_ID ");
            sbMaster.Append(" WHERE A.FUND_CD='" + fundCode + "' AND  C.DRF_REF_NO=" + DRRefNoDropDownList.SelectedValue.ToString() + " ORDER BY C.SL_NO");

            dtReportStatement = commonGatewayObj.Select(sbMaster.ToString());


            if (dtReportStatement.Rows.Count > 0)
            {
                Session["dtReportStatement"] = dtReportStatement;
                Session["printType"] = printType;

                ClientScript.RegisterStartupScript(this.GetType(), "UnitHolderInfo", "window.open('ReportViewer/UnitDematePrintDRFTransfer.aspx')", true);

            }
            else
            {
                Session["dtReportStatement"] = null;
                Session["printType"] = null;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('No data found');", true);
            }
        }
        catch (Exception ex)
        {

            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + ex.Message.Replace("'", "").ToString() + "');", true);
        }
    }
}
