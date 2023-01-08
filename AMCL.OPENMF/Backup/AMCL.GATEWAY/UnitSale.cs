using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Summary description for UnitSale
/// </summary>
namespace AMCL.GATEWAY
{
    public class UnitSale
    {
        public UnitSale()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private int saleNo;

        public int SaleNo
        {
            get { return saleNo; }
            set { saleNo = value; }
        }
        private string saleDate;

        public string SaleDate
        {
            get { return saleDate; }
            set { saleDate = value; }
        }
        private string saleType;

        public string SaleType
        {
            get { return saleType; }
            set { saleType = value; }
        }
        private decimal saleRate;

        public decimal SaleRate
        {
            get { return saleRate; }
            set { saleRate = value; }
        }
        private int saleUnitQty;

        public int SaleUnitQty
        {
            get { return saleUnitQty; }
            set { saleUnitQty = value; }
        }
        private string saleRemarks;

        public string SaleRemarks
        {
            get { return saleRemarks; }
            set { saleRemarks = value; }
        }
        private int moneyReceiptNo;

        public int MoneyReceiptNo
        {
            get { return moneyReceiptNo; }
            set { moneyReceiptNo = value; }
        }
        private string paymentType;

        public string PaymentType
        {
            get { return paymentType; }
            set { paymentType = value; }
        }
        private string chequeNo;

        public string ChequeNo
        {
            get { return chequeNo; }
            set { chequeNo = value; }
        }
        private string chequeDate;

        public string ChequeDate
        {
            get { return chequeDate; }
            set { chequeDate = value; }
        }
        private int bankCode;

        public int BankCode
        {
            get { return bankCode; }
            set { bankCode = value; }
        }
        private int branchCode;

        public int BranchCode
        {
            get { return branchCode; }
            set { branchCode = value; }
        }
        private decimal cashAmount;

        public decimal CashAmount
        {
            get { return cashAmount; }
            set { cashAmount = value; }
        }
        private string multiPayType;

        public string MultiPayType
        {
            get { return multiPayType; }
            set { multiPayType = value; }
        }


        
    }
}