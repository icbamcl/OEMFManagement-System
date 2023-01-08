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

public partial class UI_UnitCertDeliveryEdit : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO opendMFDAO = new OMFDAO();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    BaseClass bcContent = new BaseClass();
    Message msgObj = new Message();

        string fundCode = "";
        string branchCode = "";
   
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
        fundCode = bcContent.FundCode.ToString();
        branchCode = bcContent.BranchCode.ToString();
       
        
        if (!IsPostBack)
        {

            fundCodeDDL.DataSource = reportObj.dtFundCodeList();
            fundCodeDDL.DataTextField = "NAME";
            fundCodeDDL.DataValueField = "ID";
            fundCodeDDL.SelectedValue = fundCode.ToString();
            fundCodeDDL.DataBind();

            branchCodeDDL.DataSource = reportObj.dtBranchCodeList();
            branchCodeDDL.DataTextField = "NAME";
            branchCodeDDL.DataValueField = "ID";
            branchCodeDDL.SelectedValue = branchCode.ToString();
            branchCodeDDL.DataBind();
            fromSaleNoTextBox.Focus();
            dateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");

        }
    
    }
              

    protected void findButton_Click(object sender, EventArgs e)
    {
        fundCode = fundCodeDDL.SelectedValue.ToString();
        branchCode = branchCodeDDL.SelectedValue.ToString();
        StringBuilder sbQueryString = new StringBuilder();
      
       
        if (SaleNoRadioButton.Checked)
        {
            sbQueryString.Append("SELECT REG_NO,TO_CHAR(SL_NO) AS SL_TR_RN_NO, '' AS OLD_SL_TR_NO,NVL(SL_TYPE,'') AS TRANS_TYPE, NVL2(CERT_DLVRY_DT, TO_CHAR(CERT_DLVRY_DT,'DD-MON-YYYY'),'') AS DELVERY_DT,NVL(CERT_RCV_BY,'') AS RECEIVED_BY, NVL(CERT_DLVRY_BY,'') AS DELVERY_BY,NVL(CERT_CABNET_NO,'') AS CABI_NO,NVL(CERT_LOCKER_NO,'') AS LOCK_NO FROM SALE");
            sbQueryString.Append(" WHERE REG_BK='" + fundCode + "' AND REG_BR='" + branchCode + "' ");
            if (fromSaleNoTextBox.Text != "" && toSaleNoTextBox.Text != "")
            {
                sbQueryString.Append(" AND SL_NO BETWEEN " + Convert.ToInt32(fromSaleNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toSaleNoTextBox.Text.Trim().ToString()));
            }
            else if (fromSaleNoTextBox.Text != "" && toSaleNoTextBox.Text == "")
            {
                sbQueryString.Append(" AND SL_NO>=" + Convert.ToInt32(fromSaleNoTextBox.Text.Trim().ToString()));
            }
            else if (fromSaleNoTextBox.Text == "" && toSaleNoTextBox.Text != "")
            {
                sbQueryString.Append(" AND SL_NO<=" + Convert.ToInt32(toSaleNoTextBox.Text.Trim().ToString()));
            }           
        }
       
        else if (TrNoRadioButton.Checked)
        {
            sbQueryString.Append("SELECT REG_NO_I AS REG_NO,TR_NO AS SL_TR_RN_NO,NVL(OLD_SL_TR_NO,'') AS OLD_SL_TR_NO, 'TRI' AS TRANS_TYPE, NVL2(CERT_DLVRY_DT, TO_CHAR(CERT_DLVRY_DT,'DD-MON-YYYY'),'') AS DELVERY_DT,NVL(CERT_RCV_BY,'') AS RECEIVED_BY, NVL(CERT_DLVRY_BY,'') AS DELVERY_BY,NVL(CERT_CABNET_NO,'') AS CABI_NO,NVL(CERT_LOCKER_NO,'') AS LOCK_NO FROM TRANSFER");
            sbQueryString.Append(" WHERE REG_BK_I='" + fundCode + "' AND REG_BR_I='" + branchCode + "' ");
            if (fromTrNoTextBox.Text != "" && toTrNoTextBox.Text != "")
            {
                sbQueryString.Append(" AND TR_NO BETWEEN " + Convert.ToInt32(fromTrNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toTrNoTextBox.Text.Trim().ToString()));
            }
            else if (fromTrNoTextBox.Text != "" && toTrNoTextBox.Text == "")
            {
                sbQueryString.Append(" AND TR_NO>=" + Convert.ToInt32(fromTrNoTextBox.Text.Trim().ToString()));
            }
            else if (fromTrNoTextBox.Text == "" && toTrNoTextBox.Text != "")
            {
                sbQueryString.Append(" AND TR_NO<=" + Convert.ToInt32(toTrNoTextBox.Text.Trim().ToString()));
            }    
        }
        else if (RegNoRadioButton.Checked)
        {

            sbQueryString.Append("SELECT REG_NO,TO_CHAR(SL_NO) AS SL_TR_RN_NO, '' AS OLD_SL_TR_NO,NVL(SL_TYPE,'') AS TRANS_TYPE, NVL2(CERT_DLVRY_DT, TO_CHAR(CERT_DLVRY_DT,'DD-MON-YYYY'),'') AS DELVERY_DT,NVL(CERT_RCV_BY,'') AS RECEIVED_BY, NVL(CERT_DLVRY_BY,'') AS DELVERY_BY,NVL(CERT_CABNET_NO,'') AS CABI_NO,NVL(CERT_LOCKER_NO,'') AS LOCK_NO FROM SALE");
            sbQueryString.Append(" WHERE REG_BK='" + fundCode + "' AND REG_BR='" + branchCode + "' ");
            if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
            {
                sbQueryString.Append(" AND REG_NO BETWEEN " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()));
            }
            else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
            {
                sbQueryString.Append(" AND REG_NO>=" + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()));
            }
            else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
            {
                sbQueryString.Append(" AND REG_NO<=" + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()));
            }
           
            sbQueryString.Append(" UNION ALL ");

            sbQueryString.Append("SELECT REG_NO_I AS REG_NO,TO_CHAR(TR_NO) AS SL_TR_RN_NO,NVL(OLD_SL_TR_NO,'') AS OLD_SL_TR_NO, 'TRI' AS TRANS_TYPE, NVL2(CERT_DLVRY_DT, TO_CHAR(CERT_DLVRY_DT,'DD-MON-YYYY'),'') AS DELVERY_DT,NVL(CERT_RCV_BY,'') AS RECEIVED_BY, NVL(CERT_DLVRY_BY,'') AS DELVERY_BY,NVL(CERT_CABNET_NO,'') AS CABI_NO,NVL(CERT_LOCKER_NO,'') AS LOCK_NO FROM TRANSFER");
            sbQueryString.Append(" WHERE REG_BK_I='" + fundCode + "' AND REG_BR_I='" + branchCode + "'");
            if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
            {
                sbQueryString.Append("  AND REG_NO_I BETWEEN " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()));
            }
            else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
            {
                sbQueryString.Append("  AND REG_NO_I>=" + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()));
            }
            else if (fromRegNoTextBox.Text == "" && toTrNoTextBox.Text != "")
            {
                sbQueryString.Append("  AND REG_NO_I<=" + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()));
            }
          
            sbQueryString.Append(" UNION ALL ");

            sbQueryString.Append("SELECT REG_NO,TO_CHAR(REN_NO) AS SL_TR_RN_NO,'' AS OLD_SL_TR_NO, 'REN' AS TRANS_TYPE, NVL2(CERT_DLVRY_DT, TO_CHAR(CERT_DLVRY_DT,'DD-MON-YYYY'),'') AS DELVERY_DT,NVL(CERT_RCV_BY,'') AS RECEIVED_BY, NVL(CERT_DLVRY_BY,'') AS DELVERY_BY,NVL(CERT_CABNET_NO,'') AS CABI_NO,NVL(CERT_LOCKER_NO,'') AS LOCK_NO FROM RENEWAL");
            sbQueryString.Append(" WHERE REG_BK='" + fundCode + "' AND REG_BR='" + branchCode + "'");
            if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
            {
                sbQueryString.Append(" AND REG_NO BETWEEN " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()));
            }
            else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
            {
                sbQueryString.Append(" AND REG_NO>=" + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()));
            }
            else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
            {
                sbQueryString.Append(" AND REG_NO<=" + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()));
            }
           
        }
        DataTable dtCertDeliveryInfo = commonGatewayObj.Select(sbQueryString.ToString());
        if (dtCertDeliveryInfo.Rows.Count > 0)
        {
            totalRowCountLabel.Text = dtCertDeliveryInfo.Rows.Count.ToString();
            DataTable dtCertDeliveryInfoforDg = reportObj.getDtCertDeliveryInfo();
            DataRow drCertDeliveryInfoforDg;
            for (int looper = 0; looper < dtCertDeliveryInfo.Rows.Count; looper++)
            {
                drCertDeliveryInfoforDg =dtCertDeliveryInfoforDg.NewRow();
                drCertDeliveryInfoforDg["REG_No"] =Convert.ToInt32( dtCertDeliveryInfo.Rows[looper]["REG_No"].ToString());
                drCertDeliveryInfoforDg["TRANS_TYPE"] = dtCertDeliveryInfo.Rows[looper]["TRANS_TYPE"].Equals(DBNull.Value) ? "" : dtCertDeliveryInfo.Rows[looper]["TRANS_TYPE"].ToString();
                drCertDeliveryInfoforDg["SL_TR_RN_NO"] = dtCertDeliveryInfo.Rows[looper]["SL_TR_RN_NO"].Equals(DBNull.Value) ? "" : dtCertDeliveryInfo.Rows[looper]["SL_TR_RN_NO"].ToString();
                drCertDeliveryInfoforDg["CABI_NO"] = dtCertDeliveryInfo.Rows[looper]["CABI_NO"].Equals(DBNull.Value) ? "" : dtCertDeliveryInfo.Rows[looper]["CABI_NO"].ToString();
                drCertDeliveryInfoforDg["LOCK_NO"] = dtCertDeliveryInfo.Rows[looper]["LOCK_NO"].Equals(DBNull.Value) ? "" : dtCertDeliveryInfo.Rows[looper]["LOCK_NO"].ToString();
                drCertDeliveryInfoforDg["DELVERY_DT"] = dtCertDeliveryInfo.Rows[looper]["DELVERY_DT"].Equals(DBNull.Value) ? "" : dtCertDeliveryInfo.Rows[looper]["DELVERY_DT"].ToString();
                drCertDeliveryInfoforDg["RECEIVED_BY"] = dtCertDeliveryInfo.Rows[looper]["RECEIVED_BY"].Equals(DBNull.Value) ? "" : dtCertDeliveryInfo.Rows[looper]["RECEIVED_BY"].ToString();
                drCertDeliveryInfoforDg["DELVERY_BY"] = dtCertDeliveryInfo.Rows[looper]["DELVERY_BY"].Equals(DBNull.Value) ? "" : dtCertDeliveryInfo.Rows[looper]["DELVERY_BY"].ToString();
                if (dtCertDeliveryInfo.Rows[looper]["TRANS_TYPE"].ToString().ToUpper() == "SL" || dtCertDeliveryInfo.Rows[looper]["TRANS_TYPE"].ToString().ToUpper() == "CIP" ||  dtCertDeliveryInfo.Rows[looper]["TRANS_TYPE"].ToString().ToUpper() == "SIP")
                {
                    drCertDeliveryInfoforDg["CERTIFIACTE"] = reportObj.getTotalCertNo("SELECT * FROM SALE_CERT WHERE SL_NO="+ Convert.ToInt32(dtCertDeliveryInfo.Rows[looper]["SL_TR_RN_NO"].ToString()) +" AND REG_BK='"+ fundCode.ToString()+"' AND REG_BR='"+ branchCode.ToString()+"' AND REG_NO= "+Convert.ToInt32( dtCertDeliveryInfo.Rows[looper]["REG_No"].ToString()), fundCode.ToString());
                }
                else if (dtCertDeliveryInfo.Rows[looper]["TRANS_TYPE"].ToString().ToUpper() == "TRI")
                {
                    drCertDeliveryInfoforDg["CERTIFIACTE"] = reportObj.getTotalCertNo("SELECT * FROM TRANS_CERT WHERE TR_NO=" +Convert.ToInt32( dtCertDeliveryInfo.Rows[looper]["SL_TR_RN_NO"].ToString()) + " AND CURR_REG_BK='" + fundCode.ToString() + "' AND CURR_REG_BR='" + branchCode.ToString() + "' AND OLD_SL_TR_NO='" + dtCertDeliveryInfo.Rows[looper]["OLD_SL_TR_NO"].ToString()+"'", fundCode.ToString());
                }
                else if (dtCertDeliveryInfo.Rows[looper]["TRANS_TYPE"].ToString().ToUpper() == "REN")
                {
                    drCertDeliveryInfoforDg["CERTIFIACTE"] = reportObj.getTotalCertNo("SELECT * FROM RENEWAL_OUT WHERE REN_NO='" + dtCertDeliveryInfo.Rows[looper]["SL_TR_RN_NO"].ToString() + "' AND REG_BK='" + fundCode.ToString() + "' AND REG_BR='" + branchCode.ToString() + "' AND REG_NO= " + Convert.ToInt32(dtCertDeliveryInfo.Rows[looper]["REG_No"].ToString()), fundCode.ToString());
                }
                else
                {
                    drCertDeliveryInfoforDg["CERTIFIACTE"] = "";
                }

                dtCertDeliveryInfoforDg.Rows.Add(drCertDeliveryInfoforDg);
               
            }
            dvCertInfo.Visible = true;
            dgCertInfo.DataSource = dtCertDeliveryInfoforDg;
            dgCertInfo.DataBind();
            Session["dtCertDeliveryInfoforDg"] = dtCertDeliveryInfoforDg;
        }
        else
        {
            ClearText();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
        }
    }
    
    public void ClearText()
    {
        
        selfRadioButton.Checked = true;
        aouthorizeRadioButton.Checked = false;
        authorizeTextBox.Text = "";
        dateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        dvCertInfo.Visible = false;

        totalRowCountLabel.Text = "0";
      
        fundCodeDDL.SelectedValue = bcContent.FundCode.ToString();  
        branchCodeDDL.SelectedValue = bcContent.BranchCode.ToString();
      
        fromSaleNoTextBox.Focus();
        dateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }


    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            fundCode = fundCodeDDL.SelectedValue.ToString();
            branchCode = branchCodeDDL.SelectedValue.ToString();
            commonGatewayObj.BeginTransaction();
            Hashtable htDeliveryInfo = new Hashtable();
            DataTable dtCertDeliveryInfoforDg = (DataTable)Session["dtCertDeliveryInfoforDg"];
            int countRow = 0;
            if (dtCertDeliveryInfoforDg.Rows.Count > 0)
            {
                foreach (DataGridItem gridRow in dgCertInfo.Items)
                {

                    CheckBox leftCheckBox = (CheckBox)gridRow.FindControl("leftCheckBox");
                    if (leftCheckBox.Checked)
                    {
                        TextBox txtCabinet = null;
                        TextBox txtLocker = null;

                        txtCabinet = (TextBox)(gridRow.FindControl("cabinetNoTextBox"));
                        txtLocker = (TextBox)(gridRow.FindControl("lockerNoTextBox"));


                        htDeliveryInfo.Add("CERT_DLVRY_DT", Convert.ToDateTime(dateTextBox.Text.Trim()).ToString("dd-MMM-yyyy"));
                        htDeliveryInfo.Add("CERT_DLVRY_BY", userObj.UserID.ToString());
                        if (selfRadioButton.Checked)
                        {
                            htDeliveryInfo.Add("CERT_RCV_BY", "SELF");
                        }
                        else
                        {
                            htDeliveryInfo.Add("CERT_RCV_BY", authorizeTextBox.Text.Trim().ToString());
                        }
                        htDeliveryInfo.Add("CERT_CABNET_NO", txtCabinet.Text.Trim().ToString());
                        htDeliveryInfo.Add("CERT_LOCKER_NO", txtLocker.Text.Trim().ToString());

                        if (dtCertDeliveryInfoforDg.Rows[countRow]["TRANS_TYPE"].ToString() == "SL" || dtCertDeliveryInfoforDg.Rows[countRow]["TRANS_TYPE"].ToString() == "CIP" || dtCertDeliveryInfoforDg.Rows[countRow]["TRANS_TYPE"].ToString() == "SIP")
                        {
                            commonGatewayObj.Update(htDeliveryInfo, "SALE", "REG_BR='" + branchCode + "' AND REG_BK='" + fundCode + "' AND SL_NO=" + Convert.ToInt32(dtCertDeliveryInfoforDg.Rows[countRow]["SL_TR_RN_NO"].ToString()));
                        }
                        else if (dtCertDeliveryInfoforDg.Rows[countRow]["TRANS_TYPE"].ToString() == "TRI")
                        {
                            commonGatewayObj.Update(htDeliveryInfo, "TRANSFER", "BR_CODE='" + fundCode + "_" + branchCode + "' AND F_CD='" + fundCode + "' AND TR_NO=" + Convert.ToInt32(dtCertDeliveryInfoforDg.Rows[countRow]["SL_TR_RN_NO"].ToString()));
                        }
                        else if (dtCertDeliveryInfoforDg.Rows[countRow]["TRANS_TYPE"].ToString() == "REN")
                        {
                            commonGatewayObj.Update(htDeliveryInfo, "RENEWAL", "REG_BR='" + branchCode + "' AND REG_BK='" + fundCode + "' AND REN_NO='" + dtCertDeliveryInfoforDg.Rows[countRow]["SL_TR_RN_NO"].ToString() + "'");
                        }

                    }
                    htDeliveryInfo = new Hashtable();
                    countRow++;
                }

                commonGatewayObj.CommitTransaction();
                dvCertInfo.Visible = false;
                totalRowCountLabel.Text = "0";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found to Save');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + ex.Message.Replace("'", "").ToString() + "');", true);
        }

    }
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");
    }
    
}
