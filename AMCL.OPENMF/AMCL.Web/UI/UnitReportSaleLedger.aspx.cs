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
using System.Data.OracleClient;
using System.IO;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitReportSaleLedger : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    CommonGateway commonGatewayObj = new CommonGateway();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
    BaseClass bcContent = new BaseClass();
    UnitSaleBL unitSaleBLObj = new UnitSaleBL();
    
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
        fundCodeTextBox.Text = fundCode.ToString();
        branchCodeTextBox.Text = branchCode.ToString();
        
   
       // toRegDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
       // fromRegDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy"); ;
        ///holderDateofBirthTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        if (!IsPostBack)
        {
            
        }

    }
   

    

    private void ClearText()
    {

    }
    protected void regCloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitHome.aspx");

    }


    protected void ShowReportButton_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sbMaster = new StringBuilder();
            StringBuilder sbFilter = new StringBuilder();
            DataTable dtReportStatement = new DataTable();

            sbMaster.Append("SELECT NVL(SALE.SL_NO,0) AS SL_NO, TO_CHAR(SALE.SL_DT, 'DD-MON-YYYY') AS SL_DT, U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO AS RG_NO, NVL(SALE.CERT_RCV_BY,'') AS  CERT_RCV_BY, TO_CHAR(NVL(SALE.CERT_DLVRY_DT,''),'DD-MON-YYYY') AS  CERT_DLVRY_DT,");
            sbMaster.Append(" U_MASTER.HNAME,U_MASTER.REG_TYPE, U_JHOLDER.JNT_NAME, U_MASTER.REG_BK || '/' || U_MASTER.REG_BR || '/' || U_MASTER.REG_NO AS REG_NO,");
            sbMaster.Append(" U_MASTER.ADDRS1, U_MASTER.ADDRS2, U_MASTER.CITY, U_MASTER.SPEC_IN1, U_MASTER.SPEC_IN2, U_MASTER.BK_AC_NO,");
            sbMaster.Append(" U_MASTER.BK_NM_CD, U_MASTER.BK_BR_NM_CD,U_MASTER.BK_FLAG, SALE.QTY, SALE.SL_PRICE AS RATE, SALE.QTY * SALE.SL_PRICE AS AMOUNT, SALE.SL_TYPE, ");
            sbMaster.Append(" U_MASTER.CIP, U_MASTER.ID_AC FROM  U_JHOLDER RIGHT OUTER JOIN SALE INNER JOIN U_MASTER ON SALE.REG_BK = U_MASTER.REG_BK AND SALE.REG_BR = U_MASTER.REG_BR AND SALE.REG_NO = U_MASTER.REG_NO ON ");
            sbMaster.Append(" U_JHOLDER.REG_BK = U_MASTER.REG_BK AND U_JHOLDER.REG_BR = U_MASTER.REG_BR AND U_JHOLDER.REG_NO = U_MASTER.REG_NO");
            sbMaster.Append(" WHERE 1=1");


            sbMaster.Append(" AND (U_MASTER.REG_BK = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (U_MASTER.REG_BR = '" + branchCodeTextBox.Text.Trim().ToString().ToUpper() + "')");

            if (fromSaleNoTextBox.Text != "" && toSaleNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (SALE.SL_NO BETWEEN " + Convert.ToInt32(fromSaleNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toSaleNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromSaleNoTextBox.Text != "" && toSaleNoTextBox.Text == "")
            {
                sbFilter.Append(" AND (SALE.SL_NO>=" + Convert.ToInt32(fromSaleNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromSaleNoTextBox.Text == "" && toSaleNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (SALE.SL_NO<=" + Convert.ToInt32(toSaleNoTextBox.Text.Trim().ToString()) + ")");
            }

            if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (U_MASTER.REG_NO BETWEEN " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
            {
                sbFilter.Append(" AND (U_MASTER.REG_NO >=" + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (U_MASTER.REG_NO <=" + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ")");
            }


            if (fromSaleDateTextBox.Text != "" && toSaleDateTextBox.Text != "")
            {
                sbFilter.Append(" AND ( SL_DT BETWEEN '" + Convert.ToDateTime(fromSaleDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(toSaleDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            else if (fromSaleDateTextBox.Text != "" && toSaleDateTextBox.Text == "")
            {
                sbFilter.Append(" AND ( SL_DT => '" + Convert.ToDateTime(fromSaleDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            else if (fromSaleDateTextBox.Text == "" && toSaleDateTextBox.Text != "")
            {
                sbFilter.Append(" AND (SL_DT <= '" + Convert.ToDateTime(toSaleDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            if (saleTypeDropDownList.SelectedValue.ToString() != "0")
            {
                sbFilter.Append(" AND SL_TYPE='" + saleTypeDropDownList.SelectedValue.ToString() + "'");
            }


            sbFilter.Append(" ORDER BY SALE.SL_NO ");
            sbMaster.Append(sbFilter.ToString());
            dtReportStatement = commonGatewayObj.Select(sbMaster.ToString());
         
            if (dtReportStatement.Rows.Count > 0)
            {
                Session["dtReportStatement"] = dtReportStatement;
                Session["fundCode"]=fundCodeTextBox.Text.Trim().ToString();
                Session["branchCode"] = branchCodeTextBox.Text.Trim().ToString();

                ClientScript.RegisterStartupScript(this.GetType(), "UnitHolderSaleInfo", "window.open('ReportViewer/UnitReportSaleLedgerReportViewer.aspx')", true);

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('No data found');", true);
            }
        }
        catch (Exception ex)
        {
         
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + ex.Message.Replace("'", "").ToString() + "');", true);
        }

    }
    protected void ExportReportButton_Click(object sender, EventArgs e)
    {
        UnitHolderRegistration regObject = new UnitHolderRegistration();
        regObject.FundCode = fundCodeTextBox.Text.Trim().ToString();
        regObject.BranchCode = branchCodeTextBox.Text.Trim().ToString();
        string[] BranchCode = branchCodeTextBox.Text.Trim().ToString().Split('/');
        string fileName = fundCodeTextBox.Text.Trim().ToString()+"_"+BranchCode[0].ToString()+"_"+BranchCode[1].ToString()+"_SL_";

        if (fromSaleNoTextBox.Text != "" && toSaleNoTextBox.Text != "")
        {
            fileName = fileName + fromSaleNoTextBox.Text.Trim().ToString() + "_" + toSaleNoTextBox.Text.Trim().ToString();
        }
        else if (fromSaleNoTextBox.Text != "" && toSaleNoTextBox.Text == "")
        {
            int MaxSale = opendMFDAO.GetMaxSaleNo(regObject);
            MaxSale=MaxSale-1;
            fileName = fileName + fromSaleNoTextBox.Text.Trim().ToString() + "_"+MaxSale.ToString();
        }
        else if (fromSaleNoTextBox.Text == "" && toSaleNoTextBox.Text != "")
        {
            fileName = fileName +"1_"+toSaleNoTextBox.Text.Trim().ToString();
        }

        else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
        {
            fileName = fileName + "REG_";
            fileName = fileName + fromRegNoTextBox.Text.Trim().ToString() + "_" + toRegNoTextBox.Text.Trim().ToString();
        }
        else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
        {
            fileName = fileName + "REG_";
            int MaxRegNo = unitHolderRegBLObj.GetMaxRegNo(regObject);
            MaxRegNo=MaxRegNo-1;
            fileName = fileName + fromRegNoTextBox.Text.Trim().ToString() + "_" + MaxRegNo.ToString();
        }
        else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
        {
            fileName = fileName + "REG_";
            fileName = fileName + "1_" + toRegNoTextBox.Text.Trim().ToString();
        }


        //else if (fromSaleDateTextBox.Text != "" && toSaleDateTextBox.Text != "")
        //{
        //    sbFilter.Append(" AND ( SL_DT BETWEEN '" + Convert.ToDateTime(fromSaleDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(toSaleDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
        //}
        //else if (fromSaleDateTextBox.Text != "" && toSaleDateTextBox.Text == "")
        //{
        //    sbFilter.Append(" AND ( SL_DT => '" + Convert.ToDateTime(fromSaleDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
        //}
        //else if (fromSaleDateTextBox.Text == "" && toSaleDateTextBox.Text != "")
        //{
        //    sbFilter.Append(" AND (SL_DT <= '" + Convert.ToDateTime(toSaleDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
        //}
        //else if (saleTypeDropDownList.SelectedValue.ToString() != "0")
        //{
        //    sbFilter.Append(" AND SL_TYPE='" + saleTypeDropDownList.SelectedValue.ToString() + "'");
        //}
        UnitSale saleObj = new UnitSale();
        string fileDirectory = ConfigReader.ExportFileLocation.ToString();
        StreamWriter swFile = new StreamWriter(fileDirectory + "\\" + fileName+".txt");
        StringBuilder sbFileHeader = new StringBuilder();
        StringBuilder sbFileData = new StringBuilder();
        //sbFileHeader.Append("U_MASTER.REG_BK~U_MASTER.REG_BR~U_MASTER.REG_NO~U_MASTER.REG_DT~U_MASTER.REG_TYPE~U_MASTER.HNAME~U_MASTER.FMH_NAME~U_MASTER.ADDRS1~U_MASTER.ADDRS2~U_MASTER.CITY~");
        //sbFileHeader.Append("U_MASTER.B_DATE~U_MASTER.SEX~U_MASTER.MAR_STAT~U_MASTER.RELIGION~U_MASTER.EDU_QUA~U_MASTER.TEL_NO~U_MASTER.EMAIL~U_MASTER.OPN_BAL~U_MASTER.BALANCE~U_MASTER.CIP~U_MASTER.ID_FLAG~U_MASTER.ID_AC~U_MASTER.BK_FLAG~U_MASTER.DIVIDEND~");
        //sbFileHeader.Append("U_MASTER.CIP_DIV~U_MASTER.TOT_DIV~U_MASTER.USER_NM~U_MASTER.ENT_DT~U_MASTER.REMARKS~U_MASTER.ENT_TM~U_MASTER.NOMI1_REL~U_MASTER.NOMI2_REL~U_MASTER.NATIONALITY~U_MASTER.OCC_CODE~U_MASTER.BK_AC_NO~U_MASTER.BK_NM_CD~");
        //sbFileHeader.Append("U_MASTER.BK_BR_NM_CD~U_MASTER.ID_BK_NM_CD~U_MASTER.ID_BK_BR_NM_CD~U_MASTER.MO_NAME~U_JHOLDER.JNT_NO~U_JHOLDER.JNT_NAME~U_JHOLDER.JNT_FMH_NAME~U_JHOLDER.JNT_OCC_CODE~U_JHOLDER.JNT_ADDRS1~");
        //sbFileHeader.Append("U_JHOLDER.JNT_ADDRS2~U_JHOLDER.JNT_NATIONALITY~U_JHOLDER.JNT_CITY~U_JHOLDER.JNT_TEL_NO~U_JHOLDER.JNT_FMH_REL~U_JHOLDER.JNT_MO_NAME~SALE.SL_NO~SALE.SL_DT~SALE.SL_PRICE~SALE.QTY~SALE.SL_TYPE~");
        //sbFileHeader.Append("SALE.USER_NM~SALE.ENT_DT~SALE.ENT_TM~SALE.MONY_RECT_NO~SALE.PAY_TYPE~SALE.CHQ_DD_NO~SALE.CASH_AMT~SALE.BANK_CODE~SALE.BRANCH_CODE~SALE.MULTI_PAY_REMARKS~SALE.CHEQUE_DT~");
        //sbFileHeader.Append("U_NOMINEE.NOMI1_NO~U_NOMINEE.NOMI1_NAME~U_NOMINEE.NOMI1_FMH_NAME~U_NOMINEE.NOMI1_OCC_CODE~U_NOMINEE.NOMI1_ADDRS1~U_NOMINEE.NOMI1_REL~U_NOMINEE.NOMI1_PERCENTAGE~U_NOMINEE.NOMI1_ADDRS2~");
        //sbFileHeader.Append("U_NOMINEE.NOMI1_REAMARKS~U_NOMINEE.NOMI1_NATIONALITY~U_NOMINEE.NOMI1_CITY~U_NOMINEE.NOMI_CTL_NO~U_NOMINEE.NOMI1_MO_NAME");
        //sbFileHeader.Append("U_NOMINEE.NOMI2_NO~U_NOMINEE.NOMI2_NAME~U_NOMINEE.NOMI2_FMH_NAME~U_NOMINEE.NOMI2_OCC_CODE~U_NOMINEE.NOMI2_ADDRS1~U_NOMINEE.NOMI2_REL~U_NOMINEE.NOMI2_PERCENTAGE~U_NOMINEE.NOMI2_ADDRS2~");
        //sbFileHeader.Append("U_NOMINEE.NOMI2_REAMARKS~U_NOMINEE.NOMI2_NATIONALITY~U_NOMINEE.NOMI2_CITY~U_NOMINEE.NOMI2_MO_NAME~SALE_CERTIFICATE");
        sbFileHeader.Append("REG_BK~REG_BR~REG_NO~REG_DT~REG_TYPE~HNAME~FMH_NAME~ADDRS1~ADDRS2~CITY~");
        sbFileHeader.Append("B_DATE~SEX~MAR_STAT~RELIGION~EDU_QUA~TEL_NO~EMAIL~OPN_BAL~BALANCE~CIP~ID_FLAG~ID_AC~BK_FLAG~DIVIDEND~");
        sbFileHeader.Append("CIP_DIV~TOT_DIV~USER_NM~ENT_DT~REMARKS~ENT_TM~NOMI1_REL~NOMI2_REL~NATIONALITY~OCC_CODE~BK_AC_NO~BK_NM_CD~");
        sbFileHeader.Append("BK_BR_NM_CD~ID_BK_NM_CD~ID_BK_BR_NM_CD~MO_NAME~JNT_NO~JNT_NAME~JNT_FMH_NAME~JNT_OCC_CODE~JNT_ADDRS1~");
        sbFileHeader.Append("JNT_ADDRS2~JNT_NATIONALITY~JNT_CITY~JNT_TEL_NO~JNT_FMH_REL~JNT_MO_NAME~SL_NO~SL_DT~SL_PRICE~QTY~SL_TYPE~");
        sbFileHeader.Append("USER_NM~ENT_DT~ENT_TM~MONY_RECT_NO~PAY_TYPE~CHQ_DD_NO~CASH_AMT~BANK_CODE~BRANCH_CODE~MULTI_PAY_REMARKS~CHEQUE_DT~");
        sbFileHeader.Append("NOMI1_NO~NOMI1_NAME~NOMI1_FMH_NAME~NOMI1_OCC_CODE~NOMI1_ADDRS1~NOMI1_REL~NOMI1_PERCENTAGE~NOMI1_ADDRS2~");
        sbFileHeader.Append("NOMI1_REAMARKS~NOMI1_NATIONALITY~NOMI1_CITY~NOMI_CTL_NO~NOMI1_MO_NAME~");
        sbFileHeader.Append("NOMI2_NO~NOMI2_NAME~NOMI2_FMH_NAME~NOMI2_OCC_CODE~NOMI2_ADDRS1~NOMI2_REL~NOMI2_PERCENTAGE~NOMI2_ADDRS2~");
        sbFileHeader.Append("NOMI2_REAMARKS~NOMI2_NATIONALITY~NOMI2_CITY~NOMI2_MO_NAME~SALE_CERTIFICATE");
        sbFileHeader.Append("\r\n");
        
        try
        {
            DataTable dtSaleWithRegInfo = dtGetSaleWithRegInfo();
            if (dtSaleWithRegInfo.Rows.Count > 0)
            {
                for (int loop = 0; loop < dtSaleWithRegInfo.Rows.Count; loop++)
                {
                    sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["REG_BK"].ToString() + "~" + dtSaleWithRegInfo.Rows[loop]["REG_BR"].ToString() + "~" + dtSaleWithRegInfo.Rows[loop]["REG_NO"].ToString() + "~" + dtSaleWithRegInfo.Rows[loop]["REG_DT"].ToString() + "~" + dtSaleWithRegInfo.Rows[loop]["REG_TYPE"].ToString() + "~" + dtSaleWithRegInfo.Rows[loop]["HNAME"].ToString() + "~");
                    if (dtSaleWithRegInfo.Rows[loop]["FMH_NAME"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["FMH_NAME"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["ADDRS1"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["ADDRS1"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["ADDRS2"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["ADDRS2"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["CITY"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["CITY"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["B_DATE"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["B_DATE"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["SEX"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["SEX"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["MAR_STAT"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["MAR_STAT"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["RELIGION"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["RELIGION"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["EDU_QUA"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["EDU_QUA"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["TEL_NO"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["TEL_NO"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["EMAIL"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["EMAIL"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["OPN_BAL"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["OPN_BAL"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["BALANCE"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["BALANCE"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["CIP"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["CIP"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["ID_FLAG"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["ID_FLAG"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["ID_AC"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["ID_AC"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["BK_FLAG"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["BK_FLAG"].ToString() + "~");                        

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["DIVIDEND"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["DIVIDEND"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["CIP_DIV"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["CIP_DIV"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["TOT_DIV"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["TOT_DIV"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["USER_NM"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["USER_NM"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["ENT_DT"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["ENT_DT"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["REMARKS"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["REMARKS"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["ENT_TM"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["ENT_TM"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["NOMI1_REL"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["NOMI1_REL"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["NOMI2_REL"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["NOMI2_REL"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["NATIONALITY"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["NATIONALITY"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["OCC_CODE"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["OCC_CODE"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["BK_AC_NO"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["BK_AC_NO"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["BK_NM_CD"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["BK_NM_CD"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["BK_BR_NM_CD"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["BK_BR_NM_CD"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["ID_BK_NM_CD"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["ID_BK_NM_CD"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["ID_BK_BR_NM_CD"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["ID_BK_BR_NM_CD"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["MO_NAME"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["MO_NAME"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["JNT_NO"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["JNT_NO"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["JNT_NAME"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["JNT_NAME"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["JNT_FMH_NAME"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["JNT_FMH_NAME"].ToString() + "~");

                    }

                    if (dtSaleWithRegInfo.Rows[loop]["JNT_OCC_CODE"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["JNT_OCC_CODE"].ToString() + "~");

                    }

                    if (dtSaleWithRegInfo.Rows[loop]["JNT_ADDRS1"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["JNT_ADDRS1"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["JNT_ADDRS2"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["JNT_ADDRS2"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["JNT_NATIONALITY"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["JNT_NATIONALITY"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["JNT_CITY"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["JNT_CITY"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["JNT_TEL_NO"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["JNT_TEL_NO"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["JNT_FMH_REL"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["JNT_FMH_REL"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["JNT_MO_NAME"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["JNT_MO_NAME"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["SL_NO"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["SL_NO"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["SL_DT"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["SL_DT"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["SL_PRICE"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["SL_PRICE"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["QTY"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["QTY"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["SL_TYPE"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["SL_TYPE"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["SL_USER_NM"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["SL_USER_NM"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["SL_ENT_DT"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["SL_ENT_DT"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["SL_ENT_TM"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["SL_ENT_TM"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["MONY_RECT_NO"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["MONY_RECT_NO"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["PAY_TYPE"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["PAY_TYPE"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["CHQ_DD_NO"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["CHQ_DD_NO"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["CASH_AMT"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["CASH_AMT"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["BANK_CODE"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["BANK_CODE"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["BRANCH_CODE"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["BRANCH_CODE"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["MULTI_PAY_REMARKS"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["MULTI_PAY_REMARKS"].ToString() + "~");

                    }
                    if (dtSaleWithRegInfo.Rows[loop]["CHEQUE_DT"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["CHEQUE_DT"].ToString() + "~");

                    }
                    
                    regObj.FundCode = dtSaleWithRegInfo.Rows[loop]["REG_BK"].ToString();
                    regObj.BranchCode = dtSaleWithRegInfo.Rows[loop]["REG_BR"].ToString();
                    regObj.RegNumber = dtSaleWithRegInfo.Rows[loop]["REG_NO"].ToString();
                    DataTable dtNomineeRegInfo = opendMFDAO.dtNomineeRegInfo(regObj);
                    if (dtNomineeRegInfo.Rows.Count > 0)
                    {
                        sbFileData.Append("1~");
                        if (dtNomineeRegInfo.Rows[0]["NOMI_NAME"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[0]["NOMI_NAME"].ToString() + "~");
                        }
                        if (dtNomineeRegInfo.Rows[0]["NOMI_FMH_NAME"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[0]["NOMI_FMH_NAME"].ToString() + "~");
                        }
                        if (dtNomineeRegInfo.Rows[0]["NOMI_OCC_CODE"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[0]["NOMI_OCC_CODE"].ToString() + "~");
                        }
                        if (dtNomineeRegInfo.Rows[0]["NOMI_ADDRS1"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[0]["NOMI_ADDRS1"].ToString() + "~");
                        }
                        if (dtNomineeRegInfo.Rows[0]["NOMI_REL"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[0]["NOMI_REL"].ToString() + "~");
                        }
                        if (dtNomineeRegInfo.Rows[0]["PERCENTAGE"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[0]["PERCENTAGE"].ToString() + "~");
                        }
                        if (dtNomineeRegInfo.Rows[0]["NOMI_ADDRS2"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[0]["NOMI_ADDRS2"].ToString() + "~");
                        }
                        sbFileData.Append(" ~");
                        if (dtNomineeRegInfo.Rows[0]["NOMI_NATIONALITY"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[0]["NOMI_NATIONALITY"].ToString() + "~");
                        }
                        sbFileData.Append(" ~");
                        if (dtNomineeRegInfo.Rows[0]["NOMI_CTL_NO"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[0]["NOMI_CTL_NO"].ToString() + "~");
                        }
                        if (dtNomineeRegInfo.Rows[0]["NOMI_MO_NAME"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[0]["NOMI_MO_NAME"].ToString() + "~");
                        }
                    }
                    if (dtNomineeRegInfo.Rows.Count > 1)
                    {
                        sbFileData.Append("2~");
                        if (dtNomineeRegInfo.Rows[1]["NOMI_NAME"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[1]["NOMI_NAME"].ToString() + "~");
                        }
                        if (dtNomineeRegInfo.Rows[1]["NOMI_FMH_NAME"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[1]["NOMI_FMH_NAME"].ToString() + "~");
                        }
                        if (dtNomineeRegInfo.Rows[1]["NOMI_OCC_CODE"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[1]["NOMI_OCC_CODE"].ToString() + "~");
                        }
                        if (dtNomineeRegInfo.Rows[1]["NOMI_ADDRS1"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[1]["NOMI_ADDRS1"].ToString() + "~");
                        }
                        if (dtNomineeRegInfo.Rows[1]["NOMI_REL"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[1]["NOMI_REL"].ToString() + "~");
                        }
                        if (dtNomineeRegInfo.Rows[1]["PERCENTAGE"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[1]["PERCENTAGE"].ToString() + "~");
                        }
                        if (dtNomineeRegInfo.Rows[1]["NOMI_ADDRS2"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[1]["NOMI_ADDRS2"].ToString() + "~");
                        }
                        sbFileData.Append(" ~");
                        if (dtNomineeRegInfo.Rows[1]["NOMI_NATIONALITY"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[1]["NOMI_NATIONALITY"].ToString() + "~");
                        }
                        sbFileData.Append(" ~");
                        
                        if (dtNomineeRegInfo.Rows[1]["NOMI_MO_NAME"].Equals(DBNull.Value))
                        {
                            sbFileData.Append(" ~");
                        }
                        else
                        {
                            sbFileData.Append(dtNomineeRegInfo.Rows[1]["NOMI_MO_NAME"].ToString() + "~");
                        }
                    }
                    if (dtNomineeRegInfo.Rows.Count <= 0)
                    {
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                    }
                    if (dtNomineeRegInfo.Rows.Count == 1)
                    {
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                        sbFileData.Append(" ~");
                    }
                    saleObj.SaleNo =Convert.ToInt32( dtSaleWithRegInfo.Rows[loop]["SL_NO"].ToString());
                    DataTable dtDinomination = unitSaleBLObj.dtGetSaleCertBySaleNo(regObj, saleObj);
                    string certificate = "";
                    if (dtDinomination.Rows.Count > 0)
                    {
                        for (int looper = 0; looper < dtDinomination.Rows.Count; looper++)
                        {

                            if (certificate == "")
                            {
                                certificate = unitSaleBLObj.SaleCertNo(Convert.ToInt32(dtDinomination.Rows[looper]["CERT_NO"].ToString()), dtDinomination.Rows[looper]["CERT_TYPE"].ToString().ToUpper());
                            }
                            else
                            {
                                certificate = certificate + "," + unitSaleBLObj.SaleCertNo(Convert.ToInt32(dtDinomination.Rows[looper]["CERT_NO"].ToString()), dtDinomination.Rows[looper]["CERT_TYPE"].ToString().ToUpper());
                            }
                        }
                    }
                    if (certificate != "")
                    {
                        sbFileData.Append(certificate.ToString());
                        
                    }
                    else
                    {
                        sbFileData.Append(" ~");
                    }
                    
                    sbFileData.Append("\r\n");
                }
               sbFileHeader.Append(sbFileData.ToString());
               swFile.Write(sbFileHeader.ToString());
               ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Exported Successfully');", true);
              
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found ');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Export Failed Due to Eror:" + ex.ToString()+ " ');", true);
        }
        finally
        {
            swFile.Close();
        }

       

    }

    public DataTable dtGetSaleWithRegInfo()
    {
        StringBuilder sbMaster = new StringBuilder();
        StringBuilder sbFilter = new StringBuilder();
        DataTable dtReportStatement = new DataTable();
        try
        {           

            sbMaster.Append("SELECT U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, TO_CHAR(U_MASTER.REG_DT, 'DD-MON-YYYY') AS REG_DT,  U_MASTER.REG_TYPE, U_MASTER.HNAME, U_MASTER.FMH_NAME, U_MASTER.ADDRS1, U_MASTER.ADDRS2, U_MASTER.CITY,");
            sbMaster.Append(" U_MASTER.B_DATE, U_MASTER.SEX, U_MASTER.MAR_STAT, U_MASTER.RELIGION, U_MASTER.EDU_QUA, U_MASTER.TEL_NO, U_MASTER.EMAIL, U_MASTER.OPN_BAL, U_MASTER.BALANCE, U_MASTER.CIP, U_MASTER.ID_FLAG, U_MASTER.ID_AC, U_MASTER.BK_FLAG, U_MASTER.DIVIDEND,");
            sbMaster.Append(" U_MASTER.CIP_DIV, U_MASTER.TOT_DIV, U_MASTER.ENT_DT, U_MASTER.REMARKS, U_MASTER.ENT_TM, U_MASTER.NOMI1_REL, U_MASTER.NOMI2_REL, U_MASTER.NATIONALITY, U_MASTER.OCC_CODE, U_MASTER.BK_AC_NO, U_MASTER.BK_NM_CD, ");
            sbMaster.Append(" U_MASTER.BK_BR_NM_CD, U_MASTER.ID_BK_NM_CD, U_MASTER.ID_BK_BR_NM_CD, U_MASTER.MO_NAME, U_MASTER.USER_NM,  U_JHOLDER.JNT_NO, U_JHOLDER.JNT_NAME, U_JHOLDER.JNT_FMH_NAME, U_JHOLDER.JNT_OCC_CODE, U_JHOLDER.JNT_ADDRS1, ");
            sbMaster.Append(" U_JHOLDER.JNT_ADDRS2, U_JHOLDER.JNT_NATIONALITY, U_JHOLDER.JNT_CITY, U_JHOLDER.JNT_TEL_NO, U_JHOLDER.JNT_FMH_REL,U_JHOLDER.JNT_MO_NAME, SALE.SL_NO, TO_CHAR(SALE.SL_DT, 'DD-MON-YYYY') AS SL_DT, SALE.SL_PRICE, SALE.QTY, SALE.SL_TYPE, ");
            sbMaster.Append(" SALE.USER_NM AS SL_USER_NM, TO_CHAR(SALE.ENT_DT, 'DD-MON-YYYY') AS SL_ENT_DT, SALE.ENT_TM AS SL_ENT_TM, SALE.MONY_RECT_NO, SALE.PAY_TYPE, SALE.CHQ_DD_NO, SALE.CASH_AMT, SALE.BANK_CODE, SALE.BRANCH_CODE,");
            sbMaster.Append(" SALE.MULTI_PAY_REMARKS, TO_CHAR(SALE.CHEQUE_DT, 'DD-MON-YYYY') AS CHEQUE_DT FROM    U_MASTER INNER JOIN SALE ON U_MASTER.REG_BK = SALE.REG_BK AND U_MASTER.REG_BR = SALE.REG_BR AND  U_MASTER.REG_NO = SALE.REG_NO LEFT OUTER JOIN");
            sbMaster.Append(" U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND   U_MASTER.REG_NO = U_JHOLDER.REG_NO");
            sbMaster.Append(" WHERE 1=1");


            sbMaster.Append(" AND (U_MASTER.REG_BK = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (U_MASTER.REG_BR = '" + branchCodeTextBox.Text.Trim().ToString().ToUpper() + "')");

            if (fromSaleNoTextBox.Text != "" && toSaleNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (SALE.SL_NO BETWEEN " + Convert.ToInt32(fromSaleNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toSaleNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromSaleNoTextBox.Text != "" && toSaleNoTextBox.Text == "")
            {
                sbFilter.Append(" AND (SALE.SL_NO>=" + Convert.ToInt32(fromSaleNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromSaleNoTextBox.Text == "" && toSaleNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (SALE.SL_NO<=" + Convert.ToInt32(toSaleNoTextBox.Text.Trim().ToString()) + ")");
            }

            if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (U_MASTER.REG_NO BETWEEN " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
            {
                sbFilter.Append(" AND (U_MASTER.REG_NO >=" + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (U_MASTER.REG_NO <=" + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ")");
            }


            if (fromSaleDateTextBox.Text != "" && toSaleDateTextBox.Text != "")
            {
                sbFilter.Append(" AND ( SL_DT BETWEEN '" + Convert.ToDateTime(fromSaleDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(toSaleDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            else if (fromSaleDateTextBox.Text != "" && toSaleDateTextBox.Text == "")
            {
                sbFilter.Append(" AND ( SL_DT => '" + Convert.ToDateTime(fromSaleDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            else if (fromSaleDateTextBox.Text == "" && toSaleDateTextBox.Text != "")
            {
                sbFilter.Append(" AND (SL_DT <= '" + Convert.ToDateTime(toSaleDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            if (saleTypeDropDownList.SelectedValue.ToString() != "0")
            {
                sbFilter.Append(" AND SL_TYPE='" + saleTypeDropDownList.SelectedValue.ToString() + "'");
            }


            sbFilter.Append(" ORDER BY SALE.SL_NO ");
            sbMaster.Append(sbFilter.ToString());
            dtReportStatement = commonGatewayObj.Select(sbMaster.ToString());
         }
        catch (Exception ex)
        {            
            throw ex;            
        }
        return dtReportStatement;

    }
}
