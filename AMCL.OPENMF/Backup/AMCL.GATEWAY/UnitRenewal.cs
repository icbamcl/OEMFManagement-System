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
    public class UnitRenewal
    {
        public UnitRenewal()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private string renewalNo;

        public string RenewalNo
        {
            get { return renewalNo; }
            set { renewalNo = value; }
        }

        private string renewalDate;

        public string RenewalDate
        {
            get { return renewalDate; }
            set { renewalDate = value; }
        }
        private string renewalType;

        public string RenewalType
        {
            get { return renewalType; }
            set { renewalType = value; }
        }

        private int renewalUnitQty;

        public int RenewalUnitQty
        {
            get { return renewalUnitQty; }
            set { renewalUnitQty = value; }
        }
        private string renewalRemarks;

        public string RenewalRemarks
        {
            get { return renewalRemarks; }
            set { renewalRemarks = value; }
        }



    }
}