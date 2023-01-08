using System;
using System.Collections;

using System.Data;
using System.Data.OleDb;

using System.IO;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;
using System.Xml;

public partial class UI_BankUpdateProcess : System.Web.UI.Page
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
        
       // spanFundName.InnerText = opendMFDAO.GetFundName(fundCode.ToString());
//            fundCodeTextBox.Text = fundCode.ToString();
//            branchCodeTextBox.Text = branchCode.ToString();

        
        if (!IsPostBack)
        {
            bankNameDropDownList.DataSource = opendMFDAO.dtFillBankName(" CATE_CODE=1 ");
            bankNameDropDownList.DataTextField = "BANK_NAME";
            bankNameDropDownList.DataValueField = "BANK_CODE";
            bankNameDropDownList.DataBind();
                         
                         
        }
    
    }






    protected void processButton_Click(object sender, EventArgs e)
    {
        DataTable dtBankBranchInfo = commonGatewayObj.Select("SELECT * FROM BANK_BRANCH_BB WHERE BANK_CODE=" + Convert.ToInt16(bankNameDropDownList.SelectedValue.ToString()) + " AND IS_PROCE_COMP IS NULL ORDER BY BRANC_NAME");

       
            if (dtBankBranchInfo.Rows.Count>0)
            {
                 try
                 {
                     Hashtable htInsertBankBranch = new Hashtable();
                     Hashtable htUpdateBankBranch = new Hashtable();
                     Hashtable htUpdateBankBranchBB = new Hashtable();
                     string branchName = "";
                     int Bank_code = 0;
                     
                     for (int loop = 0; loop < dtBankBranchInfo.Rows.Count; loop++)
                     {
                         branchName=dtBankBranchInfo.Rows[loop]["BRANC_NAME"].ToString();
                         Bank_code = Convert.ToInt32(dtBankBranchInfo.Rows[loop]["BANK_CODE"].ToString());

                         if (ISBranchExist(branchName, Bank_code))
                         {
                             htUpdateBankBranch = new Hashtable();
                             htUpdateBankBranch.Add("BANK_CODE_BB", dtBankBranchInfo.Rows[loop]["BANK_CODE_BB"].ToString());
                             htUpdateBankBranch.Add("BRANCH_CODE_BB", dtBankBranchInfo.Rows[loop]["BRANCH_CODE_BB"].ToString());
                             htUpdateBankBranch.Add("DIST_CODE", dtBankBranchInfo.Rows[loop]["DIST_CODE"].ToString());
                             htUpdateBankBranch.Add("ROUTING_NO", dtBankBranchInfo.Rows[loop]["ROUTING_NO"].ToString());
                             htUpdateBankBranch.Add("BRANCH_DISTRICT", dtBankBranchInfo.Rows[loop]["DISTRICT"].ToString());
                             commonGatewayObj.Update(htUpdateBankBranch, "BANK_BRANCH", "BRANCH_NAME='"+branchName.ToString()+"' AND BANK_CODE="+Bank_code);

                             
                         }
                         else
                         {
                             htInsertBankBranch = new Hashtable();
                             htInsertBankBranch.Add("BANK_CODE", Bank_code);
                             htInsertBankBranch.Add("BRANCH_CODE", getMaxBranchCode(Bank_code));
                             htInsertBankBranch.Add("BRANCH_NAME", dtBankBranchInfo.Rows[loop]["BRANC_NAME"].ToString());
                             htInsertBankBranch.Add("BANK_CODE_BB", dtBankBranchInfo.Rows[loop]["BANK_CODE_BB"].ToString());
                             htInsertBankBranch.Add("BRANCH_CODE_BB", dtBankBranchInfo.Rows[loop]["BRANCH_CODE_BB"].ToString());
                             htInsertBankBranch.Add("DIST_CODE", dtBankBranchInfo.Rows[loop]["DIST_CODE"].ToString());
                             htInsertBankBranch.Add("ROUTING_NO", dtBankBranchInfo.Rows[loop]["ROUTING_NO"].ToString());
                             htInsertBankBranch.Add("BRANCH_DISTRICT", dtBankBranchInfo.Rows[loop]["DISTRICT"].ToString());
                             commonGatewayObj.Insert(htInsertBankBranch,"BANK_BRANCH");
                         }

                         htUpdateBankBranchBB = new Hashtable();
                         htUpdateBankBranchBB.Add("IS_PROCE_COMP", "Y");
                         commonGatewayObj.Update(htUpdateBankBranchBB, "BANK_BRANCH_BB", "BRANC_NAME='" + branchName.ToString() + "' AND BANK_CODE=" + Bank_code);
                        
                     }
                     commonGatewayObj.CommitTransaction();
                     ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('Complete Successfylly');", true);
                 }
                 catch (Exception ex)
                 {
                     commonGatewayObj.RollbackTransaction();
                     //errorMassege = msgObj.ExceptionErrorMessageString(ex.Message.ToString());
                     ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('Error');", true);
                 }

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('No Data Found or Process is completed');", true);
            }       
        


    }

    public bool ISBranchExist(string branch_name, int bank_code)
    {
        DataTable dtBranch = commonGatewayObj.Select("SELECT * FROM BANK_BRANCH WHERE BRANCH_NAME='"+branch_name.ToString()+"' AND BANK_CODE=" + bank_code);
        if (dtBranch.Rows.Count>0)
            return true;
        else
            return false;
    }

    public int getMaxBranchCode(int bankCode)
    {
        int maxBranchCode = 0;
        string sqlQueryString = "SELECT max(BRANCH_CODE) as BRANCH_CODE FROM BANK_BRANCH WHERE BANK_CODE=" + Convert.ToInt32(bankCode);
        DataTable dtGetMaxBranchCode = commonGatewayObj.Select(sqlQueryString.ToString());
        if (dtGetMaxBranchCode.Rows.Count > 0)
        {
            maxBranchCode = dtGetMaxBranchCode.Rows[0]["BRANCH_CODE"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtGetMaxBranchCode.Rows[0]["BRANCH_CODE"].ToString());
        }
        return maxBranchCode + 1;


    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        XmlReader xmlFile;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        Hashtable htdt = new Hashtable();

        xmlFile = XmlReader.Create("D:\\Project\\Web\\AMCL.OPENMF\\AMCL.Web\\Sanction List\\1737.xml", new XmlReaderSettings());
        ds.ReadXml(xmlFile);
       dt = ds.Tables[10];
        try
        {
            commonGatewayObj.BeginTransaction();
            for (int loop = 0; loop < dt.Rows.Count; loop++)
            {
                //if (!dt.Rows[loop]["DATAID"].Equals(DBNull.Value))
                //{
                //    htdt.Add("DATAID", dt.Rows[loop]["DATAID"].ToString());
                //}
                //htdt.Add("VERSIONNUM", Convert.ToInt32(dt.Rows[loop]["VERSIONNUM"].Equals(DBNull.Value) ? "0" : dt.Rows[loop]["VERSIONNUM"].ToString()));
                //if (!dt.Rows[loop]["FIRST_NAME"].Equals(DBNull.Value))
                //{
                //    htdt.Add("FIRST_NAME", dt.Rows[loop]["FIRST_NAME"].ToString());
                //}
                //if (!dt.Rows[loop]["SECOND_NAME"].Equals(DBNull.Value))
                //{
                //    htdt.Add("SECOND_NAME", dt.Rows[loop]["SECOND_NAME"].ToString());
                //}
                //if (!dt.Rows[loop]["THIRD_NAME"].Equals(DBNull.Value))
                //{
                //    htdt.Add("THIRD_NAME", dt.Rows[loop]["THIRD_NAME"].ToString());
                //}
                //if (!dt.Rows[loop]["FOURTH_NAME"].Equals(DBNull.Value))
                //{
                //    htdt.Add("FOURTH_NAME", dt.Rows[loop]["FOURTH_NAME"].ToString());
                ////}
                //if (!dt.Rows[loop]["UN_LIST_TYPE"].Equals(DBNull.Value))
                //{
                //    htdt.Add("UN_LIST_TYPE", dt.Rows[loop]["UN_LIST_TYPE"].ToString());
                //}
                //if (!dt.Rows[loop]["REFERENCE_NUMBER"].Equals(DBNull.Value))
                //{
                //    htdt.Add("REFERENCE_NUMBER", dt.Rows[loop]["REFERENCE_NUMBER"].ToString());
                //}
                //if (!dt.Rows[loop]["LISTED_ON"].Equals(DBNull.Value))
                //{
                //    htdt.Add("LISTED_ON", Convert.ToDateTime(Convert.ToDateTime(dt.Rows[loop]["LISTED_ON"]).ToString("dd-MMM-yyyy")));
                //}
                //if (!dt.Rows[loop]["SUBMITTED_BY"].Equals(DBNull.Value))
                //{
                //    htdt.Add("SUBMITTED_BY", dt.Rows[loop]["SUBMITTED_BY"].ToString());
                //}
                //if (!dt.Rows[loop]["GENDER"].Equals(DBNull.Value))
                //{
                //    htdt.Add("GENDER", dt.Rows[loop]["GENDER"].ToString());
                //}
                //if (!dt.Rows[loop]["NAME_ORIGINAL_SCRIPT"].Equals(DBNull.Value))
                //{
                //    htdt.Add("NAME_ORIGINAL_SCRIPT", dt.Rows[loop]["NAME_ORIGINAL_SCRIPT"].ToString());
                //}
                //if (!dt.Rows[loop]["COMMENTS1"].Equals(DBNull.Value))
                //{
                //    htdt.Add("COMMENTS1", dt.Rows[loop]["COMMENTS1"].ToString());
                //}

                //if (!dt.Rows[loop]["SORT_KEY"].Equals(DBNull.Value))
                //{
                //    htdt.Add("SORT_KEY", dt.Rows[loop]["SORT_KEY"].ToString());
                //}
                //if (!dt.Rows[loop]["NATIONALITY2"].Equals(DBNull.Value))
                //{
                //    htdt.Add("NATIONALITY2", dt.Rows[loop]["NATIONALITY2"].ToString());
                //}
                //htdt.Add("ENTITY_ID", commonGatewayObj.GetMaxNo("SLIST_ENTITY", "ENTITY_ID") + 1);
                //if (!dt.Rows[loop]["SORT_KEY_LAST_MOD"].Equals(DBNull.Value))
                //{
                //    htdt.Add("SORT_KEY_LAST_MOD", dt.Rows[loop]["SORT_KEY_LAST_MOD"].ToString());
                //}
                //commonGatewayObj.Insert(htdt, "SLIST_ENTITY");
                //htdt = new Hashtable();


                //if (!dt.Rows[loop]["NOTE"].Equals(DBNull.Value))
                //{
                //    htdt.Add("NOTE", dt.Rows[loop]["NOTE"].ToString());
                //}

                if (!dt.Rows[loop]["STREET"].Equals(DBNull.Value))
                {
                    htdt.Add("STREET", dt.Rows[loop]["STREET"].ToString());
                }
                if (!dt.Rows[loop]["CITY"].Equals(DBNull.Value))
                {
                    htdt.Add("CITY", dt.Rows[loop]["CITY"].ToString());
                }
                //if (!dt.Rows[loop]["STATE_PROVINCE"].Equals(DBNull.Value))
                //{
                //    htdt.Add("STATE_PROVINCE", dt.Rows[loop]["STATE_PROVINCE"].ToString());
                //}
                //if (!dt.Rows[loop]["ZIP_CODE"].Equals(DBNull.Value))
                //{
                //    htdt.Add("ZIP_CODE", dt.Rows[loop]["ZIP_CODE"].ToString());
                //}
                if (!dt.Rows[loop]["COUNTRY"].Equals(DBNull.Value))
                {
                    htdt.Add("COUNTRY", dt.Rows[loop]["COUNTRY"].ToString());
                }

                htdt.Add("ENTITY_ID", 78+Convert.ToInt32(dt.Rows[loop]["ENTITY_ID"].ToString()));

                commonGatewayObj.Insert(htdt, "SLIST_ENTITY_ADDRESS");
                htdt = new Hashtable();



                //if (!dt.Rows[loop]["QUALITY"].Equals(DBNull.Value))
                //{
                //    htdt.Add("QUALITY", dt.Rows[loop]["QUALITY"].ToString());
                //}
                //if (!dt.Rows[loop]["ALIAS_NAME"].Equals(DBNull.Value))
                //{
                //    htdt.Add("ALIAS_NAME", dt.Rows[loop]["ALIAS_NAME"].ToString());
                //}
                //if (!dt.Rows[loop]["ENTITY_ID"].Equals(DBNull.Value))
                //{
                //    htdt.Add("ENTITY_ID", dt.Rows[loop]["ENTITY_ID"].ToString());
                //}

                //commonGatewayObj.Insert(htdt, "SLIST_ENTITY_ALIAS");
                //htdt = new Hashtable();

            }
            commonGatewayObj.CommitTransaction();
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('success');", true);
        }
       catch (Exception ex)
                 {
                     commonGatewayObj.RollbackTransaction();
                     //errorMassege = msgObj.ExceptionErrorMessageString(ex.Message.ToString());
                     ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert ('Error');", true);
                 }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        DirectoryInfo di = new DirectoryInfo(@"D:\ICB Unit Fund Conversion Data\nrb2\signature\BD0166INRB27");
        FileInfo[] finfos = di.GetFiles("*.jpg", SearchOption.TopDirectoryOnly);
        foreach (FileInfo fi in finfos)
        {
            string newFileName = "";
           string fileName = fi.Name.ToString();
           string[] spliteFileName = fileName.Split('.');
            string fileBO = spliteFileName[0].Substring(0,16);
          // string fileBO = spliteFileName[0].ToString();
           string fileNameReg = opendMFDAO.getRegNoByFolio(fileBO.ToString()).ToString();
           if (fileNameReg != "0")
           {
               newFileName = @"\\194.25.1.202\inv_Signature\INRB2\INRB2_AMC_01_" + fileNameReg.ToString() + ".jpg";
           }
           else
           {
               newFileName = @"\\194.25.1.202\inv_Signature\INRB2\INRB2_AMC_01_Not_Found_" + spliteFileName[0].ToString() + ".jpg";
           }
           File.Copy(fi.DirectoryName.ToString() + "\\" + fi.ToString(), newFileName);
          // File.Move(fi.DirectoryName.ToString() + "\\" + fi.ToString(), newFileName);

            //string newFileLocation = System.IO.Path.Combine(@"\\Deco-07\pension_unit_vb\Images\inv_Signature\IAMCL_NEW\", newFileName);
            //.Write(newFileLocation);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        DataTable dtDividend = commonGatewayObj.Select("SELECT * FROM  DIVIDEND ");
        Hashtable htDividend = new Hashtable();
        commonGatewayObj.BeginTransaction();
        for (int loop = 0; loop < dtDividend.Rows.Count; loop++)
        {
            
            htDividend = new Hashtable();
            htDividend.Add("ID", loop+1);
            htDividend.Add("FUND_CD", dtDividend.Rows[loop]["FUND_CD"]);
            htDividend.Add("DIVI_NO", dtDividend.Rows[loop]["DIVI_NO"]);
            htDividend.Add("FY", dtDividend.Rows[loop]["FY"]);
            htDividend.Add("CLOSE_DT", dtDividend.Rows[loop]["CLOSE_DT"]);

            htDividend.Add("WAR_NO", dtDividend.Rows[loop]["WAR_NO"]);
            htDividend.Add("REG_BK", dtDividend.Rows[loop]["REG_BK"]);
            htDividend.Add("REG_BR", dtDividend.Rows[loop]["REG_BR"]);
            htDividend.Add("REG_NO", dtDividend.Rows[loop]["REG_NO"]);

            htDividend.Add("TOT_DIVI", dtDividend.Rows[loop]["TOT_DIVI"]);
            htDividend.Add("DIDUCT", dtDividend.Rows[loop]["DIDUCT"]);
            htDividend.Add("FI_DIVI_QTY", dtDividend.Rows[loop]["FI_DIVI_QTY"]);
            htDividend.Add("CIP_QTY", dtDividend.Rows[loop]["CIP_QTY"]);

            htDividend.Add("CIP_SL_NO", dtDividend.Rows[loop]["CIP_SL_NO"]);
            htDividend.Add("USER_NM", dtDividend.Rows[loop]["USER_NM"]);
            htDividend.Add("ENT_DT", dtDividend.Rows[loop]["ENT_DT"]);
            htDividend.Add("REMARKS", dtDividend.Rows[loop]["REMARKS"]);

            htDividend.Add("VALID", dtDividend.Rows[loop]["VALID"]);
            htDividend.Add("BALANCE", dtDividend.Rows[loop]["BALANCE"]);
            htDividend.Add("ENT_TM", dtDividend.Rows[loop]["ENT_TM"]);
            htDividend.Add("CIP", dtDividend.Rows[loop]["CIP"]);

            htDividend.Add("ID_FLAG", dtDividend.Rows[loop]["ID_FLAG"]);
            htDividend.Add("BK_FLAG", dtDividend.Rows[loop]["BK_FLAG"]);
            htDividend.Add("DIVI_RATE", dtDividend.Rows[loop]["DIVI_RATE"]);
            htDividend.Add("CIP_RATE", dtDividend.Rows[loop]["CIP_RATE"]);

            htDividend.Add("ADDITIONAL_PAY", dtDividend.Rows[loop]["ADDITIONAL_PAY"]);
            htDividend.Add("DIVI_STATUS", dtDividend.Rows[loop]["DIVI_STATUS"]);
            htDividend.Add("WAR_DELEVARY", dtDividend.Rows[loop]["WAR_DELEVARY"]);
            htDividend.Add("WAR_DELEVARY_DT", dtDividend.Rows[loop]["WAR_DELEVARY_DT"]);

            htDividend.Add("WAR_BK_PAY", dtDividend.Rows[loop]["WAR_BK_PAY"]);
            htDividend.Add("WAR_BK_PAY_DT", dtDividend.Rows[loop]["WAR_BK_PAY_DT"]);
            htDividend.Add("WAR_BK_PAY_AC", dtDividend.Rows[loop]["WAR_BK_PAY_AC"]);
            htDividend.Add("WAR_BK_PAY_NM", dtDividend.Rows[loop]["WAR_BK_PAY_NM"]);

            htDividend.Add("WAR_BK_PAY_BR", dtDividend.Rows[loop]["WAR_BK_PAY_BR"]);
            htDividend.Add("WAR_BK_PAY_USER", dtDividend.Rows[loop]["WAR_BK_PAY_USER"]);
            htDividend.Add("WAR_BK_PAY_ENT_DT", dtDividend.Rows[loop]["WAR_BK_PAY_ENT_DT"]);
            htDividend.Add("WAR_BK_PAY_ENT_TM", dtDividend.Rows[loop]["WAR_BK_PAY_ENT_TM"]);

            htDividend.Add("ID_AC", dtDividend.Rows[loop]["ID_AC"]);
            htDividend.Add("ID_CODE", dtDividend.Rows[loop]["ID_CODE"]);
            htDividend.Add("ID_BK_NM_CD", dtDividend.Rows[loop]["ID_BK_NM_CD"]);
            htDividend.Add("ID_BK_BR_NM_CD", dtDividend.Rows[loop]["ID_BK_BR_NM_CD"]);

            htDividend.Add("BK_AC_NO", dtDividend.Rows[loop]["BK_AC_NO"]);
            htDividend.Add("BK_NM_CD", dtDividend.Rows[loop]["BK_NM_CD"]);
            htDividend.Add("BK_BR_NM_CD", dtDividend.Rows[loop]["BK_BR_NM_CD"]);
            htDividend.Add("IS_BEFTN", dtDividend.Rows[loop]["IS_BEFTN"]);

            htDividend.Add("IS_BEFTN_SUCCS", dtDividend.Rows[loop]["IS_BEFTN_SUCCS"]);
            htDividend.Add("BEFTN_CREDIT_DT", dtDividend.Rows[loop]["BEFTN_CREDIT_DT"]);
            htDividend.Add("BEFTN_RETURN_DT", dtDividend.Rows[loop]["BEFTN_RETURN_DT"]);

            htDividend.Add("TIN", dtDividend.Rows[loop]["TIN"]);
            htDividend.Add("TIN_FLAG", dtDividend.Rows[loop]["TIN_FLAG"]);
            htDividend.Add("TAX_REBATE", dtDividend.Rows[loop]["TAX_REBATE"]);
            htDividend.Add("WARR_RECPT_BY", dtDividend.Rows[loop]["WARR_RECPT_BY"]);
           // commonGatewayObj.Insert(htDividend, "DIVIDEND_NEW");
           
            
            
        }
        commonGatewayObj.CommitTransaction();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        string ConStr = "";
        //getting the path of the file  
       string path = Server.MapPath("BEFTN.xlsx");
        //connection string for that file which extantion is .xlsx 
        ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+ path + " ;Extended Properties=\"Excel 12.0;ReadOnly=False;HDR=Yes;\"";
        //making query 
        string query = "INSERT INTO [Sheet1$] ([NAME], [ROLL] VALUES('TEST','1234')";
        //Providing connection 
        OleDbConnection conn = new OleDbConnection(ConStr);
        //checking that connection state is closed or not if closed the  
        //open the connection 
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        //create command object 
        OleDbCommand cmd = new OleDbCommand(query, conn);
        int result = cmd.ExecuteNonQuery();
        if (result > 0)
        {
            Response.Write("<script>alert('Sucessfully Data Inserted Into Excel')</script>");
        }
        else
        {
            Response.Write("<script>alert('Sorry!\n Insertion Failed')</script>");
        }
        conn.Close();

        
    }
}
