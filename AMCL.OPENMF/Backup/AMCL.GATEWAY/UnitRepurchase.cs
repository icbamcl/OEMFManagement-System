using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Summary description for UnitTransfer
/// </summary>
namespace AMCL.GATEWAY
{
    public class UnitRepurchase
    {
        public UnitRepurchase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private int repurchaseNo;

        public int RepurchaseNo
        {
            get { return repurchaseNo; }
            set { repurchaseNo = value; }
        }
        private decimal repurchaseRate;

        public decimal RepurchaseRate
        {
            get { return repurchaseRate; }
            set { repurchaseRate = value; }
        }
        private string repurchaseDate;
        public string RepurchaseDate
        {
            get { return repurchaseDate; }
            set { repurchaseDate = value; }
        }
        private string sLTRNo;

        public string SLTRNo
        {
            get { return sLTRNo; }
            set { sLTRNo = value; }
        }

        private string chequeIssueTo;

        public string ChequeIssueTo
        {
            get { return chequeIssueTo; }
            set { chequeIssueTo = value; }
        }

        private int qty;

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        private string payType;

        public string PayType
        {
            get { return payType; }
            set { payType = value; }
        }

    }
}