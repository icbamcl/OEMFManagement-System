using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;

/// <summary>
/// Summary description for UnitHolerInfo
/// </summary>
namespace AMCL.GATEWAY
{
    public class UnitHolderInfo
    {
        public UnitHolderInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private string holderName;

        public string HolderName
        {
            get { return holderName; }
            set { holderName = value; }
        }
        private string holderFMHName;

        public string HolderFMHName
        {
            get { return holderFMHName; }
            set { holderFMHName = value; }
        }
        private string holderMotherName;

        public string HolderMotherName
        {
            get { return holderMotherName; }
            set { holderMotherName = value; }
        }

        private string holderSpouceName;

        public string HolderSpouceName
        {
            get { return holderSpouceName; }
            set { holderSpouceName = value; }
        }
        private int holderOccupation;

        public int HolderOccupation
        {
            get { return holderOccupation; }
            set { holderOccupation = value; }
        }
        private string holderNationality;

        public string HolderNationality
        {
            get { return holderNationality; }
            set { holderNationality = value; }
        }
        private string holderAddress1;

        public string HolderAddress1
        {
            get { return holderAddress1; }
            set { holderAddress1 = value; }
        }
        private string holderAddress2;

        public string HolderAddress2
        {
            get { return holderAddress2; }
            set { holderAddress2 = value; }
        }
        private string holderCity;

        public string HolderCity
        {
            get { return holderCity; }
            set { holderCity = value; }
        }

        private string holderPermanAddress1;

        public string HolderPermanAddress1
        {
            get { return holderPermanAddress1; }
            set { holderPermanAddress1 = value; }
        }
        private string holderPermanAddress2;

        public string HolderPermanAddress2
        {
            get { return holderPermanAddress2; }
            set { holderPermanAddress2 = value; }
        }
        private string holderPermanCity;

        public string HolderPermanCity
        {
            get { return holderPermanCity; }
            set { holderPermanCity = value; }
        }
        private string holderTelephone;

        public string HolderTelephone
        {
            get { return holderTelephone; }
            set { holderTelephone = value; }
        }

        private string holderMobile;
        public string HolderMobile
        {
            get { return holderMobile; }
            set { holderMobile = value; }
        }
        private string holderEmail;

        public string HolderEmail
        {
            get { return holderEmail; }
            set { holderEmail = value; }
        }
        private string holderSex;

        public string HolderSex
        {
            get { return holderSex; }
            set { holderSex = value; }
        }
        private string holderTIN;
        private string holderTINFlag;

        public string HolderTINFlag
        {
            get { return holderTINFlag; }
            set { holderTINFlag = value; }
        }

        public string HolderTIN
        {
            get { return holderTIN; }
            set { holderTIN = value; }
        }
        private string holderNID;

        public string HolderNID
        {
            get { return holderNID; }
            set { holderNID = value; }
        }
        private string holderPassport;

        public string HolderPassport
        {
            get { return holderPassport; }
            set { holderPassport = value; }
        }
        private string holderBirthCertNo;

        public string HolderBirthCertNo
        {
            get { return holderBirthCertNo; }
            set { holderBirthCertNo = value; }
        }

        private string holderMaritialStatus;

        public string HolderMaritialStatus
        {
            get { return holderMaritialStatus; }
            set { holderMaritialStatus = value; }
        }
        private string holderDateofBirth;

        public string HolderDateofBirth
        {
            get { return holderDateofBirth; }
            set { holderDateofBirth = value; }
        }
        private string holderReligion;

        public string HolderReligion
        {
            get { return holderReligion; }
            set { holderReligion = value; }
        }
        private string holderEduQua;

        public string HolderEduQua
        {
            get { return holderEduQua; }
            set { holderEduQua = value; }
        }
        private string holderRemarks;

        public string HolderRemarks
        {
            get { return holderRemarks; }
            set { holderRemarks = value; }
        }

        private string holderSourceFund;

        public string HolderSourceFund
        {
            get { return holderSourceFund; }
            set { holderSourceFund = value; }
        }

        private string holderKYC;

        public string HolderKYC
        {
            get { return holderKYC; }
            set { holderKYC = value; }
        }

        private string holderBONumber;

        public string HolderBONumber
        {
            get { return holderBONumber; }
            set { holderBONumber = value; }
        }
    }
}