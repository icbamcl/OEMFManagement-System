using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;

/// <summary>
/// Summary description for UnitHolderBankInfo
namespace AMCL.GATEWAY
{
    public class UnitHolderBankInfo
    {
        public UnitHolderBankInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private string isBankInfo;

        public string IsBankInfo
        {
            get { return isBankInfo; }
            set { isBankInfo = value; }
        }
        private string bankAccountNo;

        public string BankAccountNo
        {
            get { return bankAccountNo; }
            set { bankAccountNo = value; }
        }
        private string bankName;

        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }
        private string branchName;

        public string BranchName
        {
            get { return branchName; }
            set { branchName = value; }
        }
        private string bankAddress;

        public string BankAddress
        {
            get { return bankAddress; }
            set { bankAddress = value; }
        }
        private string bankCode;

        public string BankCode
        {
            get { return bankCode; }
            set { bankCode = value; }
        }
        private string bankBranchCode;

        public string BankBranchCode
        {
            get { return bankBranchCode; }
            set { bankBranchCode = value;}
        }
        private string isBEFTN;

        public string IsBEFTN
        {
            get { return isBEFTN; }
            set { isBEFTN = value; }
        }

    }


}