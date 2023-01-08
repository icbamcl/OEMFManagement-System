using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Collections;
using AMCL.COMMON;
using AMCL.DL;
using AMCL.GATEWAY;

/// <summary>
/// Summary description for UnitUserBL
/// </summary>
namespace AMCL.BL
{
    public class UnitCertBookInfoBL
    {
        public UnitCertBookInfoBL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        CommonGateway commonGatewayObj = new CommonGateway();
        OMFDAO opendMFDAO = new OMFDAO();
        public DataTable dtGetNextBookStatrtNo(string fundCode, string dino)
        {

            DataTable dtGetNextBookStatrtNo = commonGatewayObj.Select(" SELECT MAX(BOOK_NO_END) BOOK_NO_END FROM CERT_BOOK_INFO WHERE FUND_CD='" + fundCode.ToString() + "'AND CERT_TYPE='" + dino.ToString().ToUpper() + "' ");
            return dtGetNextBookStatrtNo;

        }
        public DataTable dtAllBookBalance(string fundCode)
        {
            DataTable dtBookBalance = commonGatewayObj.Select(" SELECT CERT_TYPE, BOOK_AMT_BALANCE FROM CERT_BOOK_STOCK WHERE FUND_CD='" + fundCode.ToString() + "' ORDER BY CERT_TYPE ");
            return dtBookBalance;
        }
    }
}
