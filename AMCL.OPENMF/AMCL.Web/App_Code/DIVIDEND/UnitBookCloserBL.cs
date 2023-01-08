using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections;
using System.Data;
using AMCL.DL;
using AMCL.BL;
using AMCL.UTILITY;
using AMCL.GATEWAY;
using AMCL.COMMON;

/// <summary>
/// Summary description for UnitSIPBL
/// </summary>
public class UnitBookCloserBL
{

    CommonGateway commonGatewayObj = new CommonGateway();
    OMFDAO OmfDAOObj = new OMFDAO();
    UnitSIPGetSet unitSIPObj = new UnitSIPGetSet();
    public UnitBookCloserBL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public long saleUnit(string reg_bk, string reg_br,int reg_no, string asOnDate)
    {
        long unit = 0;
        DataTable dtUnit = commonGatewayObj.Select("SELECT NVL(SUM(QTY),0) AS UNIT FROM SALE WHERE REG_BK='"+ reg_bk+"' AND REG_BR='"+ reg_br + "' AND REG_NO="+ reg_no + "  AND SL_DT<='"+ asOnDate + "' ");
        unit = Convert.ToInt64(dtUnit.Rows[0]["UNIT"]);
        return unit;
    }
    public long repUnit(string reg_bk, string reg_br, int reg_no, string asOnDate)
    {
        long unit = 0;
        DataTable dtUnit = commonGatewayObj.Select("SELECT NVL(SUM(QTY),0) AS UNIT FROM REPURCHASE WHERE REG_BK='" + reg_bk + "' AND REG_BR='" + reg_br + "' AND REG_NO=" + reg_no + "  AND REP_DT<='" + asOnDate + "' ");
        unit = Convert.ToInt64(dtUnit.Rows[0]["UNIT"]);
        return unit;
    }
    public long trINUnit(string reg_bk, string reg_br, int reg_no, string asOnDate)
    {
        long unit = 0;
        DataTable dtUnit = commonGatewayObj.Select("SELECT NVL(SUM(QTY),0) AS UNIT FROM TRANSFER WHERE REG_BK_I='" + reg_bk + "' AND REG_BR_I='" + reg_br + "' AND REG_NO_I=" + reg_no + "  AND TR_DT<='" + asOnDate + "' ");
        unit = Convert.ToInt64(dtUnit.Rows[0]["UNIT"]);
        return unit;
    }
    public long trOUTUnit(string reg_bk, string reg_br, int reg_no, string asOnDate)
    {
        long unit = 0;
        DataTable dtUnit = commonGatewayObj.Select("SELECT NVL(SUM(QTY),0) AS UNIT FROM TRANSFER WHERE REG_BK_O='" + reg_bk + "' AND REG_BR_O='" + reg_br + "' AND REG_NO_O=" + reg_no + "  AND TR_DT<='" + asOnDate + "' ");
        unit = Convert.ToInt64(dtUnit.Rows[0]["UNIT"]);
        return unit;
    }

}
    
   