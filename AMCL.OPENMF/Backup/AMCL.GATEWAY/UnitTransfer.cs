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
    public class UnitTransfer
    {
        public UnitTransfer()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private int transferNo;
        public int TransferNo
        {
            get { return transferNo; }
            set { transferNo = value; }
        }
        private string transferorRegNo;
        public string TransferorRegNo
        {
            get { return transferorRegNo; }
            set { transferorRegNo = value; }
        }
        private string tferorBranchCode;

        public string TferorBranchCode
        {
            get { return tferorBranchCode; }
            set { tferorBranchCode = value; }
        }
        private string transfereeRegNo;
        public string TransfereeRegNo
        {
            get { return transfereeRegNo; }
            set { transfereeRegNo = value; }
        }

        private string tfereeBranchCode;

        public string TfereeBranchCode
        {
            get { return tfereeBranchCode; }
            set { tfereeBranchCode = value; }
        }
        private string transferDate;
        public string TransferDate
        {
            get { return transferDate; }
            set { transferDate = value; }
        }



    }
}
