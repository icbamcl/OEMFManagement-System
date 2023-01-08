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

public partial class UI_DividendProcess : System.Web.UI.Page
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
                                                           
        }
    
    }
   
    
    protected void CloseButton_Click(object sender, EventArgs e)
    {         
        Response.Redirect("UnitHome.aspx");
           
    }

   
    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DividendFYDropDownList.DataSource = diviDAOObj.dtGetFundWiseFY(fundNameDropDownList.SelectedValue.ToString().ToUpper());
        DividendFYDropDownList.DataTextField = "F_YEAR";
        DividendFYDropDownList.DataValueField = "F_YEAR";
        DividendFYDropDownList.DataBind();

    }
    protected void DividendFYDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClosingDateDropDownList.DataSource = diviDAOObj.dtGetFYWiseClosinDate(DividendFYDropDownList.SelectedItem.Text.ToString(),fundNameDropDownList.SelectedValue.ToString().ToUpper());
        ClosingDateDropDownList.DataTextField = "CLOSE_DT";
        ClosingDateDropDownList.DataValueField = "DIVI_NO";
        ClosingDateDropDownList.DataBind();

        fyPartDropDownList.DataSource = reportObj.getDtFYPart(fundNameDropDownList.SelectedValue.ToString().ToUpper());
        fyPartDropDownList.DataTextField = "FY_PART";
        fyPartDropDownList.DataValueField = "FY_PART";
        fyPartDropDownList.DataBind();
    }

   


    protected void CIPButton_Click(object sender, EventArgs e)
    {
        string fundCd = fundNameDropDownList.SelectedValue.ToString();
        string fy = DividendFYDropDownList.SelectedValue.ToString();
        string close_date = ClosingDateDropDownList.SelectedItem.Text.ToString();
        string fy_part = fyPartDropDownList.SelectedValue.ToString();

        DataTable dtDividendInfo = commonGatewayObj.Select("SELECT * FROM DIVIDEND WHERE FUND_CD='" + fundCd.ToString() + "' AND FY='" + fy.ToString() + "' AND CLOSE_DT='" + close_date.ToString() + "' AND CIP='Y' AND ID_FLAG='N' AND VALID IS NULL ORDER BY ID ");
        if(dtDividendInfo.Rows.Count>0)
        {
            Hashtable htUpdateDividend = new Hashtable();
            int WAR_NO = 223;
            int CIP_SL_NO = 4888;
            decimal FY_DIVI_QTY = 0;
            decimal NET_DIVI_AFTER_TAX = 0;
            
            int CIP_QTY = 0;
            decimal CIP_RATE = 87;
            for(int loop=0;loop<dtDividendInfo.Rows.Count;loop++)
            {
                
                commonGatewayObj.BeginTransaction();
                

                string REG_BR = dtDividendInfo.Rows[loop]["REG_BR"].ToString();
                string query = "UPDATE DIVIDEND SET WAR_NO=" + WAR_NO + ",";
                NET_DIVI_AFTER_TAX = Convert.ToDecimal(dtDividendInfo.Rows[loop]["NET_DIVI_AFTER_TAX"]);
                FY_DIVI_QTY = NET_DIVI_AFTER_TAX % CIP_RATE;
                CIP_QTY = Convert.ToInt32((NET_DIVI_AFTER_TAX - FY_DIVI_QTY) / CIP_RATE);
                query = query + " FI_DIVI_QTY=" + FY_DIVI_QTY + ",";
                query = query + " CIP_QTY=" + CIP_QTY + ",";
                query = query + " CIP_RATE=" + CIP_RATE + " ";
                if (REG_BR == "AMC/01")
                {
                   
                    query = query + " CIP_SL_NO=" + CIP_SL_NO + " ";
                    CIP_SL_NO++;
                }
                query = query + " WHERE ID =" + dtDividendInfo.Rows[loop]["ID"] + "";
                commonGatewayObj.ExecuteNonQuery(query.ToString());
                commonGatewayObj.CommitTransaction();

                WAR_NO++;


            }

        }
        else
        {

        }
    }

   
}
