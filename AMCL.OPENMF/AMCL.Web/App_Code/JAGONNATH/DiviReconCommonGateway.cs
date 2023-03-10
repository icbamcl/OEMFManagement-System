using AMCL.COMMON;
using AMCL.DL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DiviReconCommonGateway
/// </summary>
public class DiviReconCommonGateway
{
    DBConnector dbConectorObj = new DBConnector();
    private OracleTransaction Trans;
    private OracleConnection AppConn = new OracleConnection(ConfigReader.UNIT);
    private OracleCommand Cmnd;
    public DiviReconCommonGateway()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Insert(Hashtable hashTable, string SourceTable)
    {

        OracleConnection AppConn = dbConectorObj.GetConnection;
        OpenAppConnection();
        try
        {
            OracleCommand oracleComd = new OracleCommand("SELECT * FROM " + SourceTable + " WHERE NOT(1=1)", AppConn);
            OracleDataAdapter ODP = new OracleDataAdapter(oracleComd);
            DataSet DS = new DataSet();
            ODP.Fill(DS, SourceTable);
            DataRow drAddrow = DS.Tables[0].NewRow();

            foreach (object OBJ in hashTable.Keys)
            {
                string colName = Convert.ToString(OBJ);
                drAddrow[colName] = hashTable[OBJ];
            }
            DS.Tables[0].Rows.Add(drAddrow);
            OracleCommandBuilder ocmd = new OracleCommandBuilder(ODP);
            ODP.InsertCommand = ocmd.GetInsertCommand();
            return ODP.Update(DS, SourceTable);
        }
        catch (OracleException ex)
        {
            if (Trans != null)
            {

                Trans.Rollback();
                Trans = null;
            }
            throw ex;
        }
        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
            throw Ex;

        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }
        }

    }
    

    public int UpdateDividend(Hashtable htDivi, string SourceTableName, string Filter)
    {
        OpenAppConnection();
        try
        {

            if (Trans == null)
            {
                Cmnd = new OracleCommand("SELECT * FROM " + SourceTableName + " WHERE " + Filter, AppConn);
            }
            else
            {
                Cmnd.CommandText = "SELECT * FROM " + SourceTableName + " WHERE " + Filter;
            }




            OracleDataAdapter ADP = new OracleDataAdapter(Cmnd);
            DataSet DS = new DataSet();
            ADP.Fill(DS, SourceTableName);
            if (DS.Tables[0].Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                int rowNumber = 0;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    DataRow DR_UPDATE = DS.Tables[0].Rows[rowNumber];
                    foreach (object OBJ in htDivi.Keys)
                    {
                        string COLUMN_NAME = Convert.ToString(OBJ);
                        DR_UPDATE[COLUMN_NAME] = htDivi[OBJ];
                    }
                    OracleCommandBuilder BLD = new OracleCommandBuilder(ADP);
                    ADP.UpdateCommand = BLD.GetUpdateCommand();
                    ADP.Update(DS, SourceTableName);
                    rowNumber++;

                }

                return 1;
            }
        }
        catch (OracleException Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;
        }

        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;
        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }

        }
    }

    public short Update(Hashtable HTable, string SourceTableName, string Filter)
    {
        OpenAppConnection();
        try
        {

            if (Trans == null)
            {
                Cmnd = new OracleCommand("SELECT * FROM " + SourceTableName + " WHERE " + Filter, AppConn);
            }
            else
            {
                Cmnd.CommandText = "SELECT * FROM " + SourceTableName + " WHERE " + Filter;
            }


            OracleDataAdapter ADP = new OracleDataAdapter(Cmnd);
            DataSet DS = new DataSet();
            ADP.Fill(DS, SourceTableName);
            int rowNumber = 0;
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                DataRow DR_UPDATE = DS.Tables[0].Rows[rowNumber];
                foreach (object OBJ in HTable.Keys)
                {
                    string COLUMN_NAME = Convert.ToString(OBJ);
                    DR_UPDATE[COLUMN_NAME] = HTable[OBJ];
                }
                OracleCommandBuilder BLD = new OracleCommandBuilder(ADP);
                ADP.UpdateCommand = BLD.GetUpdateCommand();
                ADP.Update(DS, SourceTableName);
                rowNumber++;

            }

            return 1;

        }
        catch (OracleException Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;
        }

        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;
        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }

        }
    }
    public short Update(Hashtable HTable, string SourceTableName, string Filter, OracleConnection Conn)
    {

        try
        {

            Cmnd = new OracleCommand("SELECT * FROM " + SourceTableName + " WHERE " + Filter, Conn);
            OracleDataAdapter ADP = new OracleDataAdapter(Cmnd);
            DataSet DS = new DataSet();
            ADP.Fill(DS, SourceTableName);
            int rowNumber = 0;
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                DataRow DR_UPDATE = DS.Tables[0].Rows[rowNumber];
                foreach (object OBJ in HTable.Keys)
                {
                    string COLUMN_NAME = Convert.ToString(OBJ);
                    DR_UPDATE[COLUMN_NAME] = HTable[OBJ];
                }
                OracleCommandBuilder BLD = new OracleCommandBuilder(ADP);
                ADP.UpdateCommand = BLD.GetUpdateCommand();
                ADP.Update(DS, SourceTableName);
                rowNumber++;

            }

            return 1;

        }
        catch (OracleException Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;
        }

        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;
        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }

        }
    }



    public DataTable Select(string queryString)
    {
        OracleConnection oracleConn = dbConectorObj.GetConnection;
        OracleCommand oraclecmd = new OracleCommand(queryString, oracleConn);
        oraclecmd.CommandType = CommandType.Text;
        DataSet ds = new DataSet();
        OracleDataAdapter adp = new OracleDataAdapter();
        adp.SelectCommand = oraclecmd;
        adp.Fill(ds);
        return ds.Tables[0];
    }
    public DataTable Select(string queryString, OracleConnection Conn)
    {

        OracleCommand oraclecmd = new OracleCommand(queryString, Conn);
        oraclecmd.CommandType = CommandType.Text;
        DataSet ds = new DataSet();
        OracleDataAdapter adp = new OracleDataAdapter();
        adp.SelectCommand = oraclecmd;
        adp.Fill(ds);
        return ds.Tables[0];
    }
    public int DeleteByCommand(string SourceTableName, string Filter)
    {
        OpenAppConnection();
        try
        {

            if (Trans == null)
            {
                Cmnd = new OracleCommand("DELETE FROM " + SourceTableName + " WHERE " + Filter, AppConn);
            }
            else
            {
                Cmnd.CommandText = "DELETE FROM " + SourceTableName + " WHERE " + Filter;
            }

            int affectedRow = Cmnd.ExecuteNonQuery();
            return affectedRow;
        }
        catch (OracleException Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;

        }
        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;

        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }

        }
    }
    public int DeleteByCommand(string SourceTableName, string Filter, OracleConnection Conn)
    {
        try
        {
            Cmnd = new OracleCommand("DELETE FROM " + SourceTableName + " WHERE " + Filter, AppConn);

            int affectedRow = Cmnd.ExecuteNonQuery();
            return affectedRow;
        }
        catch (OracleException Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;

        }
        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;

        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }

        }
    }
    public int ExecuteNonQuery(string strSQL)
    {
        OpenAppConnection();
        try
        {
            if (Trans == null)
            {
                Cmnd = new OracleCommand(strSQL, AppConn);
            }
            else
            {
                Cmnd.CommandText = strSQL;
            }

            int affectedRow = Cmnd.ExecuteNonQuery();

            return affectedRow;
        }

        catch (OracleException Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
            throw Ex;
        }
        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }

            throw Ex;
        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }

        }
    }



    public int ExecuteNonQuery(string strSQL, OracleConnection Conn)
    {
        try
        {

            Cmnd = new OracleCommand(strSQL, Conn);

            int affectedRow = Cmnd.ExecuteNonQuery();

            return affectedRow;
        }
        catch (SqlException Ex)
        {

            throw Ex;
        }
        catch (Exception Ex)
        {

            throw Ex;
        }

    }
    public object ExecuteScalar(string strSQL)
    {
        OpenAppConnection();

        try
        {
            if (Trans == null)
            {
                Cmnd = new OracleCommand(strSQL, AppConn);
            }
            else
            {
                Cmnd.CommandText = string.Format(strSQL);
            }

            Cmnd.CommandType = CommandType.Text;

            object objReturn = Cmnd.ExecuteScalar();

            if (objReturn == null)

                return "";
            else
                return objReturn;
        }
        catch (OracleException Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
            throw Ex;

        }
        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
            throw Ex;

        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }

        }

    }
    public void BeginTransaction()
    {
        OpenAppConnection();
        if (Trans == null)
        {
            Cmnd = AppConn.CreateCommand();
            Trans = AppConn.BeginTransaction();
            Cmnd.Transaction = Trans;
        }
    }
    public void CommitTransaction()
    {
        if (Trans != null)
        {
            Trans.Commit();
            Trans = null;
        }
        CloseAppConnection();
    }
    public void RollbackTransaction()
    {
        if (Trans != null)
        {
            Trans.Rollback();
            Trans = null;
        }
        CloseAppConnection();
    }
    private void OpenAppConnection()
    {
        string ConnectionString = ConfigReader.UNIT;
        if (!ConnectionString.Equals(""))
        {

            if (AppConn.State != ConnectionState.Open)
            {
                AppConn.Open();
            }
        }
    }
    private void CloseAppConnection()
    {
        if (AppConn.State == ConnectionState.Open)
        {
            AppConn.Close();

        }
    }
    public long GetMaxNo(string tableName, string columnName)
    {
        OpenAppConnection();

        try
        {
            if (Trans == null)
            {
                Cmnd = new OracleCommand(string.Format("SELECT NVL(MAX({0}),0) AS ID FROM {1}", columnName, tableName), AppConn);
            }
            else
            {
                Cmnd.CommandText = string.Format("SELECT NVL(MAX({0}),0) AS ID FROM {1}", columnName, tableName);
            }
            long lngMaxno = Convert.ToInt64(Cmnd.ExecuteScalar());

            return lngMaxno;
        }
        catch (OracleException Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
            throw Ex;

        }
        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
            throw Ex;

        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }

        }
    }
    public long GetMaxNo(string tableName, string columnName, string filter)
    {
        OpenAppConnection();

        try
        {
            if (Trans == null)
            {
                Cmnd = new OracleCommand(string.Format("SELECT NVL(MAX({0}),0) AS ID FROM {1} WHERE {2} ", columnName, tableName, filter), AppConn);
            }
            else
            {
                Cmnd.CommandText = string.Format("SELECT NVL(MAX({0}),0) AS ID FROM {1} WHERE {2} ", columnName, tableName, filter);
            }
            long lngMaxno = Convert.ToInt64(Cmnd.ExecuteScalar());

            return lngMaxno;
        }
        catch (OracleException Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
            throw Ex;

        }
        catch (Exception Ex)
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
            throw Ex;

        }
        finally
        {
            if (Trans == null)
            {
                CloseAppConnection();
            }

        }
    }
}