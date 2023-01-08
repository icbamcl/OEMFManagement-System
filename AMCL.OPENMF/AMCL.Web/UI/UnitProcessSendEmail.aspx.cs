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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net.Mail;
using System.Net;

using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitProcessSendEmail : System.Web.UI.Page
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
                         
        }
    
    }
   
    
    protected void CloseButton_Click(object sender, EventArgs e)
    {         
        Response.Redirect("UnitHome.aspx");
           
    }

    protected void PrintButton_Click(object sender, EventArgs e)
    {
      
        
        
        
        //string duplicate = "";
        //string fundCode = fundNameDropDownList.SelectedValue.ToString();
        //StringBuilder sbMaster = new StringBuilder();
        //StringBuilder sbFilter = new StringBuilder();
        //DataTable dtDividend = new DataTable();
        //string FY = DividendFYDropDownList.SelectedValue.ToString();
        //string closingDate=ClosingDateDropDownList.SelectedItem.Text.ToString();
        //if (NormalRadioButton.Checked)
        //{
        //    //ORDER BY DIVIDEND.WAR_NO 

        //    sbMaster.Append("SELECT RECV_BRANCH_CODE, TX_CODE, RECV_AC, DEBIT, CREDIT, RECV_ID, RECV_NAME, PAYMENT_INFO FROM (");
        //    sbMaster.Append("SELECT BANK_BRANCH.ROUTING_NO AS RECV_BRANCH_CODE, 22 AS TX_CODE, DIVIDEND.BK_AC_NO AS RECV_AC, 0 AS DEBIT,");
        //    sbMaster.Append(" DIVIDEND.FI_DIVI_QTY AS CREDIT, LPAD(DIVIDEND.WAR_NO, 7, '0') AS RECV_ID, U_MASTER.HNAME AS RECV_NAME, ");
        //    sbMaster.Append(" U_MASTER.REG_BK || '/' || U_MASTER.REG_BR || '/' || U_MASTER.REG_NO AS PAYMENT_INFO FROM U_MASTER INNER JOIN");
        //    sbMaster.Append(" DIVIDEND ON U_MASTER.REG_BK = DIVIDEND.REG_BK AND U_MASTER.REG_BR = DIVIDEND.REG_BR AND U_MASTER.REG_NO = DIVIDEND.REG_NO INNER JOIN");
        //    sbMaster.Append(" BANK_BRANCH ON DIVIDEND.BK_NM_CD = BANK_BRANCH.BANK_CODE AND DIVIDEND.BK_BR_NM_CD = BANK_BRANCH.BRANCH_CODE WHERE  (DIVIDEND.IS_BEFTN = 'Y') ");

            

        //    if (branchNameDropDownList.SelectedValue.ToString() != "0")
        //    {
        //        sbFilter.Append(" AND DIVIDEND.REG_BR='" + branchNameDropDownList.SelectedValue.ToString() + "'");
        //    }

        //    if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text == "")
        //    {
        //        sbFilter.Append(" AND TO_NUMBER(DIVIDEND.WAR_NO)>=" + Convert.ToUInt32(fromWar_NoTextBox.Text.Trim().ToString()));
        //    }
        //    else if (fromWar_NoTextBox.Text == "" && toWar_NoTextBox.Text != "")
        //    {
        //        sbFilter.Append(" AND TO_NUMBER(DIVIDEND.WAR_NO)<=" + Convert.ToUInt32(toWar_NoTextBox.Text.Trim().ToString()));
        //    }
        //    else if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text != "")
        //    {
        //        sbFilter.Append(" AND TO_NUMBER(DIVIDEND.WAR_NO) BETWEEN " + Convert.ToUInt32(fromWar_NoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToUInt32(toWar_NoTextBox.Text.Trim().ToString()));
        //    }

        //    if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text == "")
        //    {
        //        sbFilter.Append(" AND TO_NUMBER(DIVIDEND.REG_NO)>=" + Convert.ToUInt32(fromRegNoTextBox.Text.Trim().ToString()));
        //    }
        //    else if (fromRegNoTextBox.Text == "" && toRegNoTextBox.Text != "")
        //    {
        //        sbFilter.Append(" AND TO_NUMBER(DIVIDEND.REG_NO)<=" + Convert.ToUInt32(toRegNoTextBox.Text.Trim().ToString()));
        //    }
        //    else if (fromRegNoTextBox.Text != "" && toRegNoTextBox.Text != "")
        //    {
        //        sbFilter.Append(" AND TO_NUMBER(DIVIDEND.REG_NO) BETWEEN " + Convert.ToUInt32(fromRegNoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToUInt32(toRegNoTextBox.Text.Trim().ToString()));
        //    }
        //    sbFilter.Append(") A ");
        //    sbFilter.Append("  ORDER BY TO_NUMBER(RECV_ID)");
        //    sbMaster.Append(sbFilter.ToString());
        //    dtDividend = commonGatewayObj.Select(sbMaster.ToString());
           
        //}
        //else if (IDAccountRadioButton.Checked)
        //{
        //    sbMaster.Append(" SELECT  DIVIDEND_ID.FUND_CD, FUND_INFO.FUND_NM, DIVI_PARA.DIVI_NO, DIVI_PARA.RATE AS DIVI_RATE, DIVI_PARA.F_YEAR, TO_CHAR(DIVI_PARA.CLOSE_DT, ");
        //    sbMaster.Append(" 'DD-MON-YYYY') AS CLOSE_DT, DIVI_PARA.FY_PART, DIVI_PARA.BK_AC_NO, DIVI_PARA.BK_NAME, DIVI_PARA.BK_ADDRS1, DIVI_PARA.BK_ADDRS2, DIVIDEND_ID.BK_AC_NO AS H_BK_AC_NO,DIVIDEND_ID.BK_NM_CD AS H_BK_NM_CD,DIVIDEND_ID.BK_BR_NM_CD AS H_BK_BR_NM_CD,");
        //    sbMaster.Append(" DIVI_PARA.BK_AC_NO_MICR || 'C' AS BK_AC_NO_MICR, DIVI_PARA.BK_TRANSACTION_CODE, DIVI_PARA.BK_ROUTING_NO, DIVIDEND_ID.TOT_DIVI - DIVIDEND_ID.DIDUCT AS NET_DIVI,");
        //    sbMaster.Append(" DIVI_PARA.BK_ROUTING_NO || 'A' AS BK_ROUTING_NO_MICR, TO_CHAR(DIVI_PARA.ISS_DT, 'DD-MON-YYYY') AS ISS_DT, ' ' AS REG_NO, NULL");
        //    sbMaster.Append(" AS JNT_NAME, BANK_NAME.BANK_NAME || ' , ' || BANK_BRANCH.BRANCH_NAME AS HNAME, BANK_BRANCH.BRANCH_ADDRS1 AS ADDRS1,DIVIDEND_ID.REG_BR,");
        //    sbMaster.Append(" BANK_BRANCH.BRANCH_ADDRS2 AS ADDRS2, BANK_BRANCH.BRANCH_DISTRICT AS CITY, LPAD(DIVIDEND_ID.WAR_NO, 7, '0') AS WAR_NO, NULL AS REG_NUM,");
        //    sbMaster.Append(" 'C' || LPAD(DIVIDEND_ID.WAR_NO, 7, '0') || 'C' AS WAR_NO_MICR, DIVIDEND_ID.TOT_BALANCE AS NO_OF_UNITS, DIVIDEND_ID.TOT_DIVI AS TOT_DIVI , NVL(DIVIDEND_ID.DIDUCT,  0) AS TAX_DIDUCT,");
        //    sbMaster.Append(" NVL(DIVIDEND_ID.FI_DIVI_QTY, 0) AS FI_DIVI_QTY, NVL(DIVIDEND_ID.CIP_QTY, 0) AS CIP_QTY, DIVIDEND_ID.CIP,NULL AS REG_TYPE,");
        //    sbMaster.Append(" DIVIDEND_ID.CIP_RATE, DECODE(NVL(DIVIDEND_ID.CIP_QTY, 0), 0, 0, DIVIDEND_ID.FI_DIVI_QTY) AS FRAC_DIVI, TO_CHAR(DIVI_PARA.AGM_DT,  'DD-MON-YYYY') AS AGM_DT , DIVI_PARA.TAX_RT_INDIVIDUAL, DIVI_PARA.TAX_RT_INSTITUTION ");
        //    sbMaster.Append(" FROM   DIVI_PARA INNER JOIN DIVIDEND_ID ON DIVI_PARA.FUND_CD = DIVIDEND_ID.FUND_CD AND DIVI_PARA.F_YEAR = DIVIDEND_ID.FY AND  DIVI_PARA.DIVI_NO = DIVIDEND_ID.DIVI_NO INNER JOIN");
        //    sbMaster.Append(" FUND_INFO ON DIVIDEND_ID.FUND_CD = FUND_INFO.FUND_CD INNER JOIN  BANK_BRANCH ON DIVIDEND_ID.ID_BK_BR_NM_CD = BANK_BRANCH.BRANCH_CODE AND");
        //    sbMaster.Append(" DIVIDEND_ID.ID_BK_NM_CD = BANK_BRANCH.BANK_CODE INNER JOIN   BANK_NAME ON BANK_BRANCH.BANK_CODE = BANK_NAME.BANK_CODE");
        //    sbMaster.Append(" WHERE     (DIVIDEND_ID.FUND_CD = '" + fundNameDropDownList.SelectedValue.ToString().ToUpper() + "')");
        //    sbMaster.Append(" AND (DIVI_PARA.CLOSE_DT='" + closingDate.ToString() + "') AND (DIVI_PARA.F_YEAR = '" + FY + "')");

        //    if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text == "")
        //    {
        //        sbFilter.Append(" AND TO_NUMBER(DIVIDEND_ID.WAR_NO)>=" + Convert.ToUInt32(fromWar_NoTextBox.Text.Trim().ToString()));
        //    }
        //    else if (fromWar_NoTextBox.Text == "" && toWar_NoTextBox.Text != "")
        //    {
        //        sbFilter.Append(" AND TO_NUMBER(DIVIDEND_ID.WAR_NO)<=" + Convert.ToUInt32(toWar_NoTextBox.Text.Trim().ToString()));
        //    }
        //    else if (fromWar_NoTextBox.Text != "" && toWar_NoTextBox.Text != "")
        //    {
        //        sbFilter.Append(" AND TO_NUMBER(DIVIDEND_ID.WAR_NO) BETWEEN " + Convert.ToUInt32(fromWar_NoTextBox.Text.Trim().ToString()) + " AND " + Convert.ToUInt32(toWar_NoTextBox.Text.Trim().ToString()));
        //    }
        //    sbFilter.Append("  ORDER BY TO_NUMBER(WAR_NO)");
        //    sbMaster.Append(sbFilter.ToString());
        //    dtDividend = commonGatewayObj.Select(sbMaster.ToString());

        //}
        //if (dtDividend.Rows.Count > 0)
        //{
        //    //if (DuplicateCheckBox.Checked)
        //    //{
        //    //    duplicate = "DUPLICATE";
        //    //}
        //    //Session["dtDividend"] = dtDividend;
        //    //Session["duplicate"] = duplicate;
        //    //Session["fundCode"] = fundCode;
        //    string separetor = SeparatorTextBox.Text.Trim().ToString();
        //    string fileName = fundNameDropDownList.SelectedValue.ToString() + "_" + fromWar_NoTextBox.Text.Trim().ToString() + " TO " + toWar_NoTextBox.Text.Trim().ToString();

        //    FileStream fileStream = new FileStream(@"D:" + "\\" + fileName + ".txt", FileMode.Create);
        //   // StreamWriter sw = new StreamWriter(fileStream);

        //    StreamWriter swFile = new StreamWriter(fileStream);
        //    StringBuilder sbFileData = new StringBuilder();
        //    for(int loop=0;loop<dtDividend.Rows.Count;loop++)
        //    {
        //        sbFileData.Append(dtDividend.Rows[loop]["RECV_BRANCH_CODE"].ToString());
        //        sbFileData.Append(separetor + dtDividend.Rows[loop]["TX_CODE"].ToString());
        //        sbFileData.Append(separetor + dtDividend.Rows[loop]["RECV_AC"].ToString());
        //        sbFileData.Append(separetor + dtDividend.Rows[loop]["DEBIT"].ToString());
        //        sbFileData.Append(separetor + dtDividend.Rows[loop]["CREDIT"].ToString());
        //        sbFileData.Append(separetor + dtDividend.Rows[loop]["RECV_ID"].ToString());
        //        sbFileData.Append(separetor + dtDividend.Rows[loop]["RECV_NAME"].ToString());
        //        sbFileData.Append(separetor + dtDividend.Rows[loop]["PAYMENT_INFO"].ToString());
                
        //        sbFileData.Append("\r\n");
        //    }
        //    swFile.Write(sbFileData.ToString());
        //    swFile.Close();
        //    fileStream.Close();
        //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Data Exported Succesfully');", true);
        //}
        //else
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data Found');", true);
        //}
        
       
    }
    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DividendFYDropDownList.DataSource = diviDAOObj.dtGetFundWiseFY(fundNameDropDownList.SelectedValue.ToString().ToUpper());
        //DividendFYDropDownList.DataTextField = "F_YEAR";
        //DividendFYDropDownList.DataValueField = "F_YEAR";
        //DividendFYDropDownList.DataBind();

    }
    protected void DividendFYDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //    ClosingDateDropDownList.DataSource = diviDAOObj.dtGetFYWiseClosinDate(DividendFYDropDownList.SelectedItem.Text.ToString(),fundNameDropDownList.SelectedValue.ToString().ToUpper());
        //    ClosingDateDropDownList.DataTextField = "CLOSE_DT";
        //    ClosingDateDropDownList.DataValueField = "DIVI_NO";
        //    ClosingDateDropDownList.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        //  MailMessage mailObj = new MailMessage();
        //  mailObj.IsBodyHtml = true;
        //  mailObj.To.Add(new MailAddress("sakhawat5413@Yahoo.com,sakha5413@yahoo.com,sakha5413@gmail.com,noorzahan.hossain@gmail.com", "test"));
        //  // mailObj.From = (new MailAddress("jikocse@gmail.com","Mahbub"));
        //  // mailObj.From = (new MailAddress("support@icb.gov.bd", "Mahbub"));

        //  mailObj.Subject = "Mail Test from ICB AMCL";
        //  mailObj.Body = "This is a test mail for launching mail service with attachment of Portfolio and other statement";


        //  //SMTP SERVER for Gmail                  -------------------------final
        //  mailObj.From = (new MailAddress("sakhawat5413@gmail.com", "ICB Asset Management Company Ltd."));
        // SmtpClient SMTPServer = new SmtpClient();


        // SMTPServer.Host = "smtp.gmail.com";
        // SMTPServer.Port = 465;  //465, 25, 587
        //// SMTPServer.Timeout = 60000;
        //// SMTPServer.EnableSsl = true;
        // SMTPServer.Credentials = new System.Net.NetworkCredential("sakhawat5413@gmail.com", "02095413");          

        // SMTPServer.Send(mailObj);

        //MailMessage message = new MailMessage();
        //message.To.Add("sakha5413@gmail.com");
        //message.From = new MailAddress("sakhawat5413@gmail.com");
        //message.Subject = "New test mail";
        //message.Body = "Hello test message succeed";
        //message.IsBodyHtml = true;
        //Attachment file = new Attachment(@"E:\result.pdf");
        //file.Name = "test.pdf";
        //message.Attachments.Add(file);
        //message.BodyEncoding = System.Text.Encoding.ASCII;
        ////message.Priority = System.Net.Mail.MailPriority.High;
        //SmtpClient smtp = new SmtpClient();
        //smtp.EnableSsl = true;
        //  smtp.Port = 25;
        ////smtp.Port = 587;
        //smtp.UseDefaultCredentials = false;
        //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //smtp.Host = "smtp.gmail.com";
        //smtp.Credentials = new NetworkCredential("sakhawat5413@gmail.com", "02095413");
        //try
        //{
        //    smtp.Send(message);
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}

        MailMessage message = new MailMessage();
        message.To.Add("sakha5413@gmail.com");
        message.From = new MailAddress("automail@icbamcl.com.bd");
        message.Subject = "New test mail";
        message.Body = "Hello test message succeed";
        message.IsBodyHtml = true;
        Attachment file = new Attachment(@"E:\result.pdf");
        file.Name = "test.pdf";
        message.Attachments.Add(file);
        message.BodyEncoding = System.Text.Encoding.ASCII;
        //message.Priority = System.Net.Mail.MailPriority.High;
        SmtpClient smtp = new SmtpClient();
        smtp.EnableSsl = true;
        smtp.Port = 25;
        //smtp.Port = 587;
        smtp.UseDefaultCredentials = false;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Host = "mail.icbamcl.com.bd";
        smtp.Credentials = new NetworkCredential("automail@icbamcl.com.bd", "Iamcl@765");
        try
        {
            smtp.Send(message);
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
}
