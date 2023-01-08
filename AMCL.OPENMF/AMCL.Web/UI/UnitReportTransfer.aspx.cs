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


public partial class UI_UnitReportTransfer : System.Web.UI.Page
{
    OMFDAO opendMFDAO = new OMFDAO();
    UnitHolderRegBL unitHolderRegBLObj = new UnitHolderRegBL();
    UnitHolderRegistration regObj = new UnitHolderRegistration();
    Message msgObj = new Message();
    UnitUser userObj = new UnitUser();
    UnitReport reportObj = new UnitReport();
    UnitTransferBL unitTrObj = new UnitTransferBL();
    CommonGateway commonGatewayObj = new CommonGateway();
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
            string tr_Type = "T";
           if(TrDeathRadioButton.Checked)
            {
                tr_Type = "D";
            }
               
         

            sbMaster.Append("SELECT U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO AS RG_NO, U_MASTER.REG_BK || '/' || U_MASTER.REG_BR || '/' || U_MASTER.REG_NO AS REG_NO, U_MASTER.ADDRS1, U_MASTER.ADDRS2, U_MASTER.CITY, ");
            sbMaster.Append(" TRANSFER.TR_NO, TRANSFER.F_CD, TRANSFER.QTY, TRANSFER.OLD_SL_TR_NO,   U_MASTER_1.REG_BK || '/' || U_MASTER_1.REG_BR || '/' || U_MASTER_1.REG_NO AS TFEREE_REG_NO, U_MASTER.HNAME, ");
            sbMaster.Append(" U_MASTER_1.HNAME AS TFEREE_NAME, U_MASTER_1.ADDRS1 AS TFEREE_ADDRS1, U_MASTER_1.ADDRS2 AS TFEREE_ADDRS2,  U_MASTER_1.CITY AS TFEREE_CITY, U_MASTER_1.CIP, TO_CHAR(TRANSFER.TR_DT,'DD-MON-YYYY') AS TR_DT ");
            sbMaster.Append(" FROM    U_MASTER INNER JOIN TRANSFER ON U_MASTER.REG_BK = TRANSFER.REG_BK_O AND U_MASTER.REG_BR = TRANSFER.REG_BR_O AND  U_MASTER.REG_NO = TRANSFER.REG_NO_O INNER JOIN ");
            sbMaster.Append(" U_MASTER U_MASTER_1 ON TRANSFER.REG_BK_I = U_MASTER_1.REG_BK AND TRANSFER.REG_BR_I = U_MASTER_1.REG_BR AND   TRANSFER.REG_NO_I = U_MASTER_1.REG_NO WHERE 1=1 AND TRANSFER.TR_TYPE='"+ tr_Type.ToUpper() + "'");

           
            sbMaster.Append(" AND (TRANSFER.F_CD = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (TRANSFER.BR_CODE ='" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "_" + branchCodeTextBox.Text.Trim().ToString().ToUpper() + "')");

            if (fromTrNoTextBox.Text != "" && toTrNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (TRANSFER.TR_NO BETWEEN " + Convert.ToInt32(fromTrNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toTrNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromTrNoTextBox.Text != "" && toTrNoTextBox.Text == "")
            {
                sbFilter.Append(" AND (TRANSFER.TR_NO>=" + Convert.ToInt32(fromTrNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromTrNoTextBox.Text == "" && toTrNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (TRANSFER.TR_NO<=" + Convert.ToInt32(toTrNoTextBox.Text.Trim().ToString()) + ")");
            }

            if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (TRANSFER.REG_NO_I BETWEEN " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
            {
                sbFilter.Append(" AND (TRANSFER.REG_NO_I >=" + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (TRANSFER.REG_NO_I <=" + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ")");
            }


            if (fromTrDateTextBox.Text != "" && toTrDateTextBox.Text != "")
            {
                sbFilter.Append(" AND ( TRANSFER.TR_DT BETWEEN '" + Convert.ToDateTime(fromTrDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(toTrDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            else if (fromTrDateTextBox.Text != "" && toTrDateTextBox.Text == "")
            {
                sbFilter.Append(" AND ( TRANSFER.TR_DT => '" + Convert.ToDateTime(fromTrDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            else if (fromTrDateTextBox.Text == "" && toTrDateTextBox.Text != "")
            {
                sbFilter.Append(" AND (TRANSFER.TR_DT <= '" + Convert.ToDateTime(toTrDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }


            sbFilter.Append(" ORDER BY TRANSFER.TR_NO ");
            sbMaster.Append(sbFilter.ToString());            
            dtReportStatement = commonGatewayObj.Select(sbMaster.ToString());
            if (dtReportStatement.Rows.Count > 0)
            {
                Session["dtReportStatement"] = dtReportStatement;
                Session["fundCode"] = fundCodeTextBox.Text.Trim().ToString();
                Session["branchCode"] = branchCodeTextBox.Text.Trim().ToString();
                Session["TR_TYPE"] = tr_Type.ToString();

                ClientScript.RegisterStartupScript(this.GetType(), "UnitHolderInfo", "window.open('ReportViewer/UnitReportTransferReportViewer.aspx')", true);
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
        string fileName = fundCodeTextBox.Text.Trim().ToString() + "_" + BranchCode[0].ToString() + "_" + BranchCode[1].ToString() + "_TR_";

        if (fromTrNoTextBox.Text != "" && toTrNoTextBox.Text != "")
        {
            fileName = fileName + fromTrNoTextBox.Text.Trim().ToString() + "_" + toTrNoTextBox.Text.Trim().ToString();
        }
        else if (fromTrNoTextBox.Text != "" && toTrNoTextBox.Text == "")
        {
            int MaxTrNo = unitTrObj.getMaxTRNo(regObject);
            MaxTrNo = MaxTrNo - 1;
            fileName = fileName + fromTrNoTextBox.Text.Trim().ToString() + "_" + MaxTrNo.ToString();
        }
        else if (fromTrNoTextBox.Text == "" && toTrNoTextBox.Text != "")
        {
            fileName = fileName + "1_" + toTrNoTextBox.Text.Trim().ToString();
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
            MaxRegNo = MaxRegNo - 1;
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
        StreamWriter swFile = new StreamWriter(fileDirectory + "\\" + fileName + ".txt");
        StringBuilder sbFileHeader = new StringBuilder();
        StringBuilder sbFileData = new StringBuilder();
        sbFileHeader.Append("F_CD~BR_CODE~TR_NO~TR_DT~REG_BK_O~REG_BR_O~REG_NO_O~QTY~TR_USER_NM~TR_ENT_DT~TR_ENT_TM~OLD_SL_TR_NO~");
        sbFileHeader.Append("REG_BK~REG_BR~REG_NO~REG_DT~REG_TYPE~HNAME~FMH_NAME~ADDRS1~ADDRS2~CITY~");
        sbFileHeader.Append("B_DATE~SEX~MAR_STAT~RELIGION~EDU_QUA~TEL_NO~EMAIL~OPN_BAL~BALANCE~CIP~ID_FLAG~ID_AC~BK_FLAG~DIVIDEND~");
        sbFileHeader.Append("CIP_DIV~TOT_DIV~USER_NM~ENT_DT~REMARKS~ENT_TM~NOMI1_REL~NOMI2_REL~NATIONALITY~OCC_CODE~BK_AC_NO~BK_NM_CD~");
        sbFileHeader.Append("BK_BR_NM_CD~ID_BK_NM_CD~ID_BK_BR_NM_CD~MO_NAME~JNT_NO~JNT_NAME~JNT_FMH_NAME~JNT_OCC_CODE~JNT_ADDRS1~");
        sbFileHeader.Append("JNT_ADDRS2~JNT_NATIONALITY~JNT_CITY~JNT_TEL_NO~JNT_FMH_REL~JNT_MO_NAME~");
        //FileHeader.Append("USER_NM~ENT_DT~ENT_TM~MONY_RECT_NO~PAY_TYPE~CHQ_DD_NO~CASH_AMT~BANK_CODE~BRANCH_CODE~MULTI_PAY_REMARKS~CHEQUE_DT~");
        sbFileHeader.Append("NOMI1_NO~NOMI1_NAME~NOMI1_FMH_NAME~NOMI1_OCC_CODE~NOMI1_ADDRS1~NOMI1_REL~NOMI1_PERCENTAGE~NOMI1_ADDRS2~");
        sbFileHeader.Append("NOMI1_REAMARKS~NOMI1_NATIONALITY~NOMI1_CITY~NOMI_CTL_NO~NOMI1_MO_NAME~");
        sbFileHeader.Append("NOMI2_NO~NOMI2_NAME~NOMI2_FMH_NAME~NOMI2_OCC_CODE~NOMI2_ADDRS1~NOMI2_REL~NOMI2_PERCENTAGE~NOMI2_ADDRS2~");
        sbFileHeader.Append("NOMI2_REAMARKS~NOMI2_NATIONALITY~NOMI2_CITY~NOMI2_MO_NAME~TR_CERTIFICATE");
        sbFileHeader.Append("\r\n");

        try
        {
            DataTable dtSaleWithRegInfo = dtGetTransferWithRegInfo();
            if (dtSaleWithRegInfo.Rows.Count > 0)
            {
                for (int loop = 0; loop < dtSaleWithRegInfo.Rows.Count; loop++)
                {
                    sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["F_CD"].ToString() + "~" + dtSaleWithRegInfo.Rows[loop]["BR_CODE"].ToString() + "~" + dtSaleWithRegInfo.Rows[loop]["TR_NO"].ToString() + "~" + dtSaleWithRegInfo.Rows[loop]["TR_DT"].ToString() + "~" + dtSaleWithRegInfo.Rows[loop]["REG_BK_O"].ToString() + "~" + dtSaleWithRegInfo.Rows[loop]["REG_BR_O"].ToString() + "~");
                    sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["REG_NO_O"].ToString() + "~" + dtSaleWithRegInfo.Rows[loop]["QTY"].ToString() + "~");
                    if (dtSaleWithRegInfo.Rows[loop]["TR_USER_NM"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["TR_USER_NM"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["TR_ENT_DT"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["TR_ENT_DT"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["TR_ENT_TM"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["TR_ENT_TM"].ToString() + "~");
                    }
                    if (dtSaleWithRegInfo.Rows[loop]["OLD_SL_TR_NO"].Equals(DBNull.Value))
                    {
                        sbFileData.Append(" ~");
                    }
                    else
                    {
                        sbFileData.Append(dtSaleWithRegInfo.Rows[loop]["OLD_SL_TR_NO"].ToString() + "~");
                    }
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
                    string OLD_SL_TR_NO = dtSaleWithRegInfo.Rows[loop]["OLD_SL_TR_NO"].Equals(DBNull.Value) ? "" : dtSaleWithRegInfo.Rows[loop]["OLD_SL_TR_NO"].ToString();
                    string queryString = "";
                  
                    if (OLD_SL_TR_NO != "")
                    {
                        queryString="SELECT * FROM TRANS_CERT WHERE TR_NO=" + Convert.ToInt32(dtSaleWithRegInfo.Rows[loop]["TR_NO"].ToString()) + " AND BR_CODE='" + dtSaleWithRegInfo.Rows[loop]["BR_CODE"].ToString() + "' AND F_CD='" + dtSaleWithRegInfo.Rows[loop]["F_CD"].ToString() + "' AND OLD_SL_TR_NO='" + OLD_SL_TR_NO.ToString()+"'" ;
                    }
                    else
                    {
                         queryString="SELECT * FROM TRANS_CERT WHERE TR_NO=" + Convert.ToInt32(dtSaleWithRegInfo.Rows[loop]["TR_NO"].ToString()) + " AND BR_CODE='" + dtSaleWithRegInfo.Rows[loop]["BR_CODE"].ToString() + "' AND F_CD='" + dtSaleWithRegInfo.Rows[loop]["F_CD"].ToString() + "'";
                    }
                    string trCertificate = reportObj.getTotalCertNo(queryString.ToString(), dtSaleWithRegInfo.Rows[loop]["F_CD"].ToString());
                    if (trCertificate != "")
                    {
                        sbFileData.Append(trCertificate.ToString());
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
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Export Failed Due to Eror:" + ex.ToString() + " ');", true);
        }
        finally
        {
            swFile.Close();
        }



    }

    public DataTable dtGetTransferWithRegInfo()
    {
        StringBuilder sbMaster = new StringBuilder();
        StringBuilder sbFilter = new StringBuilder();
        DataTable dtReportStatement = new DataTable();
        try
        {
            sbMaster.Append("SELECT  TRANSFER.F_CD, TRANSFER.BR_CODE, TRANSFER.TR_NO, TRANSFER.TR_DT, TRANSFER.REG_BK_O, TRANSFER.REG_BR_O,   TRANSFER.REG_NO_O, TRANSFER.QTY, TRANSFER.USER_NM AS TR_USER_NM, TRANSFER.ENT_DT AS TR_ENT_DT, TRANSFER.ENT_TM AS TR_ENT_TM, TRANSFER.OLD_SL_TR_NO,");
            sbMaster.Append(" U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, TO_CHAR(U_MASTER.REG_DT, 'DD-MON-YYYY') AS REG_DT,  U_MASTER.REG_TYPE, U_MASTER.HNAME, U_MASTER.FMH_NAME, U_MASTER.ADDRS1, U_MASTER.ADDRS2, U_MASTER.CITY,");
            sbMaster.Append(" U_MASTER.B_DATE, U_MASTER.SEX, U_MASTER.MAR_STAT, U_MASTER.RELIGION, U_MASTER.EDU_QUA, U_MASTER.TEL_NO, U_MASTER.EMAIL, U_MASTER.OPN_BAL, U_MASTER.BALANCE, U_MASTER.CIP, U_MASTER.ID_FLAG, U_MASTER.ID_AC, U_MASTER.BK_FLAG, U_MASTER.DIVIDEND,");
            sbMaster.Append(" U_MASTER.CIP_DIV, U_MASTER.TOT_DIV, U_MASTER.ENT_DT, U_MASTER.REMARKS, U_MASTER.ENT_TM, U_MASTER.NOMI1_REL, U_MASTER.NOMI2_REL, U_MASTER.NATIONALITY, U_MASTER.OCC_CODE, U_MASTER.BK_AC_NO, U_MASTER.BK_NM_CD, ");
            sbMaster.Append(" U_MASTER.BK_BR_NM_CD, U_MASTER.ID_BK_NM_CD, U_MASTER.ID_BK_BR_NM_CD, U_MASTER.MO_NAME, U_MASTER.USER_NM,  U_JHOLDER.JNT_NO, U_JHOLDER.JNT_NAME, U_JHOLDER.JNT_FMH_NAME, U_JHOLDER.JNT_OCC_CODE, U_JHOLDER.JNT_ADDRS1, ");
            sbMaster.Append(" U_JHOLDER.JNT_ADDRS2, U_JHOLDER.JNT_NATIONALITY, U_JHOLDER.JNT_CITY, U_JHOLDER.JNT_TEL_NO, U_JHOLDER.JNT_FMH_REL,U_JHOLDER.JNT_MO_NAME FROM  U_MASTER INNER JOIN TRANSFER ON U_MASTER.REG_BK = TRANSFER.REG_BK_I AND U_MASTER.REG_BR = TRANSFER.REG_BR_I AND ");
            sbMaster.Append(" U_MASTER.REG_NO = TRANSFER.REG_NO_I LEFT OUTER JOIN  U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND  U_MASTER.REG_NO = U_JHOLDER.REG_NO ");
            sbMaster.Append(" WHERE 1=1");


            sbMaster.Append(" AND (TRANSFER.REG_BK_O = '" + fundCodeTextBox.Text.Trim().ToString().ToUpper() + "') AND (TRANSFER.REG_BR_O = '" + branchCodeTextBox.Text.Trim().ToString().ToUpper() + "')");

            if (fromTrNoTextBox.Text != "" && toTrNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (TRANSFER.TR_NO BETWEEN " + Convert.ToInt32(fromTrNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toTrNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromTrNoTextBox.Text != "" && toTrNoTextBox.Text == "")
            {
                sbFilter.Append(" AND (TRANSFER.TR_NO>=" + Convert.ToInt32(fromTrNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromTrNoTextBox.Text == "" && toTrNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (TRANSFER.TR_NO<=" + Convert.ToInt32(toTrNoTextBox.Text.Trim().ToString()) + ")");
            }

            if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND (TRANSFER.REG_NO_O BETWEEN " + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
            {
                sbFilter.Append(" AND (TRANSFER.REG_NO_O >=" + Convert.ToInt32(fromRegNoTextBox.Text.Trim().ToString()) + ")");
            }
            else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
            {
                sbFilter.Append(" AND ( TRANSFER.REG_NO_O <=" + Convert.ToInt32(toRegNoTextBox.Text.Trim().ToString()) + ")");
            }

            if (fromTrDateTextBox.Text != "" && toTrDateTextBox.Text != "")
            {
                sbFilter.Append(" AND ( TRANSFER.TR_DT BETWEEN '" + Convert.ToDateTime(fromTrDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(toTrDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            else if (fromTrDateTextBox.Text != "" && toTrDateTextBox.Text == "")
            {
                sbFilter.Append(" AND ( TRANSFER.TR_DT => '" + Convert.ToDateTime(fromTrDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }
            else if (fromTrDateTextBox.Text == "" && toTrDateTextBox.Text != "")
            {
                sbFilter.Append(" AND (TRANSFER.TR_DT <= '" + Convert.ToDateTime(toTrDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy") + "')");
            }

            sbFilter.Append(" ORDER BY TRANSFER.TR_NO ");
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
