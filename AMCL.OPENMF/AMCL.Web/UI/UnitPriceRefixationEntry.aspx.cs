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
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitPriceRefixationEntry : System.Web.UI.Page
{
    //System.Web.UI.Page this_page_ref = null;
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();    
    UnitUser userObj = new UnitUser();

    UnitReport unitRepprtObj = new UnitReport();
    UnitRepurchaseBL unitRepBLObj = new UnitRepurchaseBL();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
    BaseClass bcContent = new BaseClass();
  
   
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

           

         
        }
    
    }

    protected void savetButton_Click(object sender, EventArgs e)
    {

        try
        {
            if (unitRepprtObj.CheckDuplicate(refixationDateTextBox.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert('Save Failed: Duplicate Price Refixaton Date');", true);
            }
            else
            {

                Hashtable htInsert = new Hashtable();
                int countCheck = 0;
                commonGatewayObj.BeginTransaction();
                foreach (GridViewRow Drv in SurrenderListGridView.Rows)
                {
                    htInsert.Add("NAV_DATE", NAVDateTextBox.Text);
                    htInsert.Add("REFIX_DT", refixationDateTextBox.Text);
                    htInsert.Add("FUND_CD", Drv.Cells[0].Text.ToString());
                    htInsert.Add("FUND_NM", Drv.Cells[1].Text.ToString());

                    TextBox effectDateTextBox = (TextBox)SurrenderListGridView.Rows[countCheck].FindControl("effectDateTextBox");
                    TextBox salePriceTextBox = (TextBox)SurrenderListGridView.Rows[countCheck].FindControl("salePriceTextBox");
                    TextBox surrenderPriceTextBox = (TextBox)SurrenderListGridView.Rows[countCheck].FindControl("surrenderPriceTextBox");
                    TextBox navMPTextBox = (TextBox)SurrenderListGridView.Rows[countCheck].FindControl("navMPTextBox");
                    TextBox navCPTextBox = (TextBox)SurrenderListGridView.Rows[countCheck].FindControl("navCPTextBox");

                    htInsert.Add("EFFECTIVE_DATE",Convert.ToDateTime( effectDateTextBox.Text).ToString("dd-MMM-yyyy"));
                    htInsert.Add("REFIX_SL_PR", Convert.ToDecimal(salePriceTextBox.Text.ToString()));
                    htInsert.Add("REFIX_REP_PR", Convert.ToDecimal(surrenderPriceTextBox.Text.ToString()));
                    htInsert.Add("NAV_MP", Convert.ToDecimal(navMPTextBox.Text.ToString()));
                    htInsert.Add("NAV_CP", Convert.ToDecimal(navCPTextBox.Text.ToString()));

                    htInsert.Add("USER_NM", userObj.UserID.ToString());
                    htInsert.Add("ENT_DT", DateTime.Today.ToString("dd-MMM-yyyy"));
                    htInsert.Add("ENT_TM", DateTime.Now.ToShortTimeString());

                    commonGatewayObj.Insert(htInsert, "PRICE_REFIX");
                    commonGatewayObj.CommitTransaction();
                    htInsert = new Hashtable();
                    countCheck++;
                  
                }
            }

            refixationDateTextBox.Text = "";
            NAVDateTextBox.Text = "";
            dvGridSurrender.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert('Save Successfully');", true);
        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            throw ex;
        }
    
 }
    

    
    protected void findButton_Click(object sender, EventArgs e)
    {
        string[] refixation = refixationDateTextBox.Text.ToUpper().Split('-');
        string refixationDate = "01-" + refixation[1].ToUpper();


        DataTable dtFundInfoDetails = unitRepprtObj.dtFundInfoDetails(" AND YEAR_START_MONTH <> '"+ refixationDate + "'  ");      
        DataTable dtPriceWithNAV = unitRepprtObj.dtPriceDetails(" ").Clone();
       

        for (int looper=0; looper<dtFundInfoDetails.Rows.Count;looper++)
        {

          DataTable  dtPriceDetails = unitRepprtObj.dtPriceDetails(" AND FUND_CD='" + dtFundInfoDetails.Rows[looper]["FUND_CD"].ToString().ToUpper() + "'  AND REFIX_DT=(SELECT MAX (REFIX_DT) FROM PRICE_REFIX WHERE FUND_CD='" + dtFundInfoDetails.Rows[looper]["FUND_CD"].ToString().ToUpper() + "') ");
          DataTable dtNAVDetails = unitRepprtObj.dtPriceDetailsWithNAV(" AND NAVFUNDID=" + Convert.ToInt16( dtFundInfoDetails.Rows[looper]["FUND_CD_INVEST"].ToString().ToUpper()) + "  AND NAVDATE='" + NAVDateTextBox.Text + "' ");
          
            DataRow drdtPriceWithNAV = dtPriceWithNAV.NewRow();
            if (dtPriceDetails.Rows.Count > 0)
            {
                drdtPriceWithNAV["FUND_CD"] = dtPriceDetails.Rows[0]["FUND_CD"];
                drdtPriceWithNAV["FUND_NM"] = dtPriceDetails.Rows[0]["FUND_NM"];
                drdtPriceWithNAV["EFFECTIVE_DATE"] = dtPriceDetails.Rows[0]["EFFECTIVE_DT"];
                drdtPriceWithNAV["REFIX_SL_PR"] = dtPriceDetails.Rows[0]["REFIX_SL_PR"];
                drdtPriceWithNAV["REFIX_REP_PR"] = dtPriceDetails.Rows[0]["REFIX_REP_PR"];
            }

            if (dtNAVDetails.Rows.Count > 0)
            {
                drdtPriceWithNAV["NAV_MP"] = dtNAVDetails.Rows[0]["NAV_PU_MP"];
                drdtPriceWithNAV["NAV_CP"] = dtNAVDetails.Rows[0]["NAV_PU_CP"];
            }
           
          
            

            dtPriceWithNAV.Rows.Add(drdtPriceWithNAV);
        }


        if (dtPriceWithNAV.Rows.Count > 0)
        {
            dvGridSurrender.Visible = true;
            SurrenderListGridView.DataSource = dtPriceWithNAV;
            SurrenderListGridView.DataBind();
        }
        else
        {
            dvGridSurrender.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Popup", "alert('No Data Found');", true);
        }
    }
}
