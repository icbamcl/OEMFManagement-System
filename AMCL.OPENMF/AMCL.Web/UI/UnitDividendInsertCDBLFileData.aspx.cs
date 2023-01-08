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
using System.Text.RegularExpressions;
using System.IO;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

public partial class UI_UnitDividendInsertCDBLFileData : System.Web.UI.Page
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
    }


    protected void showDataButton_Click(object sender, EventArgs e)
    {
        try
        {

            string FileName = "";
            FileName = @"D:\CDBL_DATA\open end\2022\Shotoborsho\16-JAN-2022\11RT14URT14160122S.TXT";
            if (File.Exists(FileName))
            {
                DataTable dtCDBLdata = diviDAOObj.getdtBOUploadTable();
                int serial = 1;
                DataRow drCDBLdata;
                StreamReader srFileReader;
                string line;

                srFileReader = new StreamReader(FileName);
                string[] lineContent;
                while (srFileReader.Peek() != -1)
                {
                    line = srFileReader.ReadLine();
                    if (Regex.IsMatch(line, "BD"))
                    {
                        lineContent = line.Split('~');
                        drCDBLdata = dtCDBLdata.NewRow();
                        drCDBLdata["SI"] = serial.ToString();
                        drCDBLdata["FUND_NAME"] = lineContent[1].ToString().Trim();
                        drCDBLdata["BO"] = lineContent[2].ToString().Trim();
                        drCDBLdata["NAME1"] = lineContent[3].ToString().Trim().ToUpper();
                        drCDBLdata["BALANCE"] = lineContent[15].ToString().Trim();
                        drCDBLdata["BO_TYPE"] = lineContent[20].ToString().Trim().ToUpper();
                        drCDBLdata["BO_CATAGORY"] = lineContent[21].ToString().Trim().ToUpper();
                        drCDBLdata["ADDRESS1"] = lineContent[24].ToString().Trim().ToUpper();
                        drCDBLdata["ADDRESS2"] = lineContent[25].ToString().Trim().ToUpper();
                        drCDBLdata["ADDRESS3"] = lineContent[26].ToString().Trim().ToUpper();
                        drCDBLdata["CITY"] = lineContent[27].ToString().Trim().ToUpper();
                        drCDBLdata["COUNTRY"] = lineContent[28].Trim().ToString().ToUpper();
                        drCDBLdata["POST_CODE"] = lineContent[29].ToString().Trim();
                        drCDBLdata["PHONE1"] = lineContent[30].ToString().Trim();
                        drCDBLdata["PHONE2"] = lineContent[31].ToString().Trim();
                        drCDBLdata["RESIDENCY"] = lineContent[32].ToString().ToUpper();
                        drCDBLdata["BANK_NAME"] = lineContent[35].ToString().Trim().ToUpper();
                        drCDBLdata["BRANCH_NAME"] = lineContent[36].ToString().Trim().ToUpper();
                        drCDBLdata["BANK_ACC_NO"] = lineContent[37].ToString().Trim().ToUpper();
                        drCDBLdata["ROUTING_NO"] = lineContent[38].ToString().Trim().ToUpper();
                        drCDBLdata["NAME2"] = lineContent[42].ToString().Trim();
                        drCDBLdata["OCCUPATION"] = lineContent[43].ToString().Trim().ToUpper();                      
                        drCDBLdata["GENDER"] = lineContent[45].ToString().Trim().ToUpper();
                        drCDBLdata["BO_NATIONALITY"] = lineContent[46].ToString().Trim();
                        drCDBLdata["ETIN"] = lineContent[47].ToString().Trim();
                        if (checkValidTINNumber(lineContent[47].ToString().Trim()))
                        {
                            drCDBLdata["IS_VALID_ETIN"] = "YES";
                        }

                       DataTable dtRegInfo= diviDAOObj.dtRegInfo(fundNameDropDownList.SelectedValue.ToString(), lineContent[2].ToString().Trim());
                        if(dtRegInfo.Rows.Count>0)
                        {
                            drCDBLdata["REG_BK"] = dtRegInfo.Rows[0]["REG_BK"];
                            drCDBLdata["REG_BR"] = dtRegInfo.Rows[0]["REG_BR"];
                            drCDBLdata["REG_NO"] = dtRegInfo.Rows[0]["REG_NO"];
                            drCDBLdata["NO_OF_REGNO"] = dtRegInfo.Rows.Count;
                        }
                       
                        dtCDBLdata.Rows.Add(drCDBLdata);
                        serial++;
                    }
                }

                if (dtCDBLdata.Rows.Count > 0)
                {
                    grdShowDetails.DataSource = dtCDBLdata;
                    grdShowDetails.DataBind();
                    Session["dtCDBLdata"] = dtCDBLdata;
                    SaveButton.Visible = true;
                }
                else
                {
                    Session["dtCDBLdata"] = null;
                }
            }
            else
            {

                Session["dtCDBLdata"] = null;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No File Saved ');", true);
            }

        }
        catch (Exception Ex)
        {
            string strMessage = string.Format("Data Retrive Failed! {0}", Ex.Message);
            strMessage = strMessage.Replace("\r\n", "");
            Session["dtCDBLdata"] = null;
            ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", string.Format("window.fnAlert(\"{0}\");", strMessage), true);
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        DataTable dtCDBLdata = (DataTable)Session["dtCDBLdata"];
        if (dtCDBLdata.Rows.Count > 0)
        {
            InsertCdblData(dtCDBLdata);
        }
    }
    public void InsertCdblData(DataTable dtCDBLdata)
    {
      
          try
            {
                long SI = commonGatewayObj.GetMaxNo("CDBL_DATA", "ID") + 1;
                commonGatewayObj.BeginTransaction();
                Hashtable htCdblData = new Hashtable();
                for (int loop = 0; loop < dtCDBLdata.Rows.Count; loop++)
                {
                    htCdblData.Add("ID", SI);
                    htCdblData.Add("FUND_CODE", fundNameDropDownList.SelectedValue.ToString());
                    htCdblData.Add("DIVI_NO", ClosingDateDropDownList.SelectedValue.ToString());
                    htCdblData.Add("FY", DividendFYDropDownList.SelectedItem.Text);
                    htCdblData.Add("RECORD_DATE", ClosingDateDropDownList.SelectedItem.Text);
                    htCdblData.Add("BO", dtCDBLdata.Rows[loop]["BO"]);
                    htCdblData.Add("NAME1", dtCDBLdata.Rows[loop]["NAME1"]);
                    htCdblData.Add("BALANCE", dtCDBLdata.Rows[loop]["BALANCE"]);
                    htCdblData.Add("BO_TYPE", dtCDBLdata.Rows[loop]["BO_TYPE"]);
                    htCdblData.Add("BO_CATAGORY", dtCDBLdata.Rows[loop]["BO_CATAGORY"]);
                    htCdblData.Add("ADDRESS1", dtCDBLdata.Rows[loop]["ADDRESS1"]);
                    htCdblData.Add("ADDRESS2", dtCDBLdata.Rows[loop]["ADDRESS2"]);
                    htCdblData.Add("ADDRESS3", dtCDBLdata.Rows[loop]["ADDRESS3"]);
                    htCdblData.Add("CITY", dtCDBLdata.Rows[loop]["CITY"]);
                    htCdblData.Add("COUNTRY", dtCDBLdata.Rows[loop]["COUNTRY"]);
                    htCdblData.Add("POST_CODE", dtCDBLdata.Rows[loop]["POST_CODE"]);
                    htCdblData.Add("PHONE1", dtCDBLdata.Rows[loop]["PHONE1"]);
                    htCdblData.Add("PHONE2", dtCDBLdata.Rows[loop]["PHONE2"]);
                    htCdblData.Add("RESIDENCY", dtCDBLdata.Rows[loop]["RESIDENCY"]);
                    htCdblData.Add("BANK", dtCDBLdata.Rows[loop]["BANK_NAME"]);
                    htCdblData.Add("BRANCH", dtCDBLdata.Rows[loop]["BRANCH_NAME"]);
                    htCdblData.Add("BANK_ACC", dtCDBLdata.Rows[loop]["BANK_ACC_NO"]);
                    htCdblData.Add("ROUTING_NO", dtCDBLdata.Rows[loop]["ROUTING_NO"]);

                    htCdblData.Add("NAME2", dtCDBLdata.Rows[loop]["NAME2"]);
                    htCdblData.Add("GENDER", dtCDBLdata.Rows[loop]["GENDER"]);
                    htCdblData.Add("BO_NATIONALITY", dtCDBLdata.Rows[loop]["BO_NATIONALITY"]);
                    htCdblData.Add("ETIN", dtCDBLdata.Rows[loop]["ETIN"]);
                    htCdblData.Add("IS_VALID_ETIN", dtCDBLdata.Rows[loop]["IS_VALID_ETIN"]);

                htCdblData.Add("REG_BK", dtCDBLdata.Rows[loop]["REG_BK"]);
                htCdblData.Add("REG_BR", dtCDBLdata.Rows[loop]["REG_BR"]);
                htCdblData.Add("REG_NO", dtCDBLdata.Rows[loop]["REG_NO"]);
                htCdblData.Add("NO_OF_REGNO", dtCDBLdata.Rows[loop]["NO_OF_REGNO"]);

                commonGatewayObj.Insert(htCdblData, "CDBL_DATA");
                    htCdblData = new Hashtable();
                    SI++;
                }
                Hashtable htDividendParaUpdate = new Hashtable();
                //htDividendParaUpdate.Add("IS_UPLOAD_CDBL_DATA", "Y");
               // commonGatewayObj.Update(htDividendParaUpdate, "DIVIDEND_PARA", "FUND_CODE=" + Convert.ToInt32(fundNameDropDownList.SelectedValue.ToString()) + " AND FY='" + fyDropDownList.Text.ToString() + "'AND RECORD_DATE='" + recordDateDropDownList.Text.ToString() + "'");
                commonGatewayObj.CommitTransaction();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('CDBL Data Save Successfully');", true);

            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
              //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + ex.ToString() + "');", true);
            }
        }
       
    
    public bool checkValidTINNumber(string TIN)
    {
        bool validTin = true;
        if (TIN.Length == 12)
        {
            if (TIN.Split('-').Length > 1)
            {
                validTin = false;
            }
            else if ((TIN.Split('.').Length > 1))
            {
                validTin = false;
            }
            else
            {
                validTin = true;
            }
        }
        else
        {
            validTin = false;
        }
        return validTin;
    }
}
