using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMCL.GATEWAY
{
    public class UnitLien
    {
        private int lienNo;

        public int LienNo
        {
            get { return lienNo; }
            set { lienNo = value; }
        }
        private int lienCancelNo;
        public int LienCancelNo
        {
            get { return lienCancelNo; }
            set { lienCancelNo = value; }
        }
        private int lienInst;

        public int LienInst
        {
            get { return lienInst; }
            set { lienInst = value; }
        }
        private int lienInstBranch;

        public int LienInstBranch
        {
            get { return lienInstBranch; }
            set { lienInstBranch = value; }
        }
        private string lienReqDate;

        public string LienReqDate
        {
            get { return lienReqDate; }
            set { lienReqDate = value; }
        }

        private string lienCancelReqDate;

        public string LienCancelReqDate
        {
            get { return lienCancelReqDate; }
            set { lienCancelReqDate = value; }
        }
        private string lienCancelDate;

        public string LienCancelDate
        {
            get { return lienCancelDate; }
            set { lienCancelDate = value; }
        }
        private string lienRefference;

        public string LienRefference
        {
            get { return lienRefference; }
            set { lienRefference = value; }
        }

    }
}
