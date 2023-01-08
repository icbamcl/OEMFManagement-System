using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Summary description for UnitHolderRegistration
/// </summary>
namespace AMCL.GATEWAY
{
public class UnitHolderRegistration
{
    public UnitHolderRegistration()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string fundCode;

    public string FundCode
    {
        get { return fundCode; }
        set { fundCode = value; }
    }
    private string branchCode;

    public string BranchCode
    {
        get { return branchCode; }
        set { branchCode = value; }
    }
    private string regNumber;

    public string RegNumber
    {
        get { return regNumber; }
        set { regNumber = value; }
    }
    private string regType;

    public string RegType
    {
        get { return regType; }
        set { regType = value; }
    }
    private string regIsIDAccount;

    public string IsIDAccount
    {
        get { return regIsIDAccount; }
        set { regIsIDAccount = value; }
    }
    private DateTime regDate;

    public DateTime RegDate
    {
        get { return regDate; }
        set { regDate = value; }
    }
    private string regIsCIP;

    public string RegIsCIP
    {
        get { return regIsCIP; }
        set { regIsCIP = value; }
    }
    private string idAccNo;

    public string IdAccNo
    {
        get { return idAccNo; }
        set { idAccNo = value; }
    }
    private int idBankID;

    public int IdBankID
    {
        get { return idBankID; }
        set { idBankID = value; }
    }
    private int idBankBranchID;

    public int IdBankBranchID
    {
        get { return idBankBranchID; }
        set { idBankBranchID = value; }
    }
    private string bO;

    public string BO
    {
        get { return bO; }
        set { bO = value; }
    }
    private string folio;

    public string Folio
    {
        get { return folio; }
        set { folio = value; }
    }
}
}