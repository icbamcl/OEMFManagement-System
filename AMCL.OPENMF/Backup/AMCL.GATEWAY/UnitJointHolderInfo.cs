using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Summary description for UnitJholderInfo
/// </summary>
namespace AMCL.GATEWAY
{
    public class UnitJointHolderInfo
    {
        public UnitJointHolderInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private string jHolderName;

        public string JHolderName
        {
            get { return jHolderName; }
            set { jHolderName = value; }
        }
        private string jHolderFMHName;

        public string JHolderFMHName
        {
            get { return jHolderFMHName; }
            set { jHolderFMHName = value; }
        }
        private string jHolderMotherName;

        public string JHolderMotherName
        {
            get { return jHolderMotherName; }
            set { jHolderMotherName = value; }
        }
        private int jHolderOccupation;

        public int JHolderOccupation
        {
            get { return jHolderOccupation; }
            set { jHolderOccupation = value; }
        }
        private string jHolderNationality;

        public string JHolderNationality
        {
            get { return jHolderNationality; }
            set { jHolderNationality = value; }
        }
        private string jHolderAddress1;

        public string JHolderAddress1
        {
            get { return jHolderAddress1; }
            set { jHolderAddress1 = value; }
        }
        private string jHolderAddress2;

        public string JHolderAddress2
        {
            get { return jHolderAddress2; }
            set { jHolderAddress2 = value; }
        }
        private string jHolderCity;

        public string JHolderCity
        {
            get { return jHolderCity; }
            set { jHolderCity = value; }
        }
        private string jHolderTelephone;

        public string JHolderTelephone
        {
            get { return jHolderTelephone; }
            set { jHolderTelephone = value; }
        }
        private string jHolderEmail;

        public string JHolderEmail
        {
            get { return jHolderEmail; }
            set { jHolderEmail = value; }
        }
        private string jHolderSex;

        public string JHolderSex
        {
            get { return jHolderSex; }
            set { jHolderSex = value; }
        }
        private string jHolderMaritialStatus;

        public string JHolderMaritialStatus
        {
            get { return jHolderMaritialStatus; }
            set { jHolderMaritialStatus = value; }
        }
        private string jHolderDateofBirth;

        public string JHolderDateofBirth
        {
            get { return jHolderDateofBirth; }
            set { jHolderDateofBirth = value; }
        }
        private string jHolderReligion;

        public string JHolderReligion
        {
            get { return jHolderReligion; }
            set { jHolderReligion = value; }
        }
        private string jHolderEduQua;

        public string JHolderEduQua
        {
            get { return jHolderEduQua; }
            set { jHolderEduQua = value; }
        }
        private string jHholderRemarks;

        public string JHholderRemarks
        {
            get { return jHholderRemarks; }
            set { jHholderRemarks = value; }
        }
        private string jHolderNPBNo;

        public string JHolderNPBNo
        {
            get { return jHolderNPBNo; }
            set { jHolderNPBNo = value; }
        }
        private string jHolderNPBType;

        public string JHolderNPBType
        {
            get { return jHolderNPBType; }
            set { jHolderNPBType = value; }
        }
        private string jHolderKYC;

        public string JHolderKYC
        {
            get { return jHolderKYC; }
            set { jHolderKYC = value; }
        }
    }
}