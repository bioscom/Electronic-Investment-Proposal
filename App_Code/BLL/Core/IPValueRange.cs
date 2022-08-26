using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for IPLimit
/// </summary>

public class IPLimit
{
    public int m_iIPLimitId { get; set; }
    public int m_iIPLimit { get; set; }
    public int m_iSeq { get; set; }

    public IPLimit()
	{
		//
		//
	}

    public IPLimit(DataRow dr)
    {
        try
        {
            m_iIPLimitId = int.Parse(dr["IDIPLIMIT"].ToString());
            m_iIPLimit = int.Parse(dr["IPLIMIT"].ToString());
            m_iSeq = int.Parse(dr["SEQ"].ToString());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public DataTable dtGetIPLimits()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getIPLimits();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public IPLimit objGetIPLimits()
    {
        DataTable dt = dtGetIPLimits();
        IPLimit result = new IPLimit();
        foreach (DataRow dr in dt.Rows)
        {
            result = new IPLimit(dr);
        }
        return result;
    }

    public List<IPLimit> lstGetIPLimits()
    {
        List<IPLimit> result = new List<IPLimit>();
        DataTable dt = dtGetIPLimits();
        foreach (DataRow dr in dt.Rows)
        {
            result.Add(new IPLimit(dr));
        }
        return result;
    }

    public bool UpdateIPLimit(int iIPLimitId, int iIPLimit)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.UpdateIPLimit();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDIPLIMIT";
        param.Value = iIPLimitId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IPLIMIT";
        param.Value = iIPLimit;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return (result != -1);
    }

    public bool AddIPLimit(int iIPLimit)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.AddIPLimit();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IPLIMIT";
        param.Value = iIPLimit;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return (result != -1);
    }


    public struct IPLevels
    {
        public int iValue0;
        public int iValue1;
        public int iValue2;
        public int iValue3;
        public int iValue4;
        public int iValue5;

        public int iLimitId0;
        public int iLimitId1;
        public int iLimitId2;
        public int iLimitId3;
        public int iLimitId4;
        public int iLimitId5;
    }

    public IPLevels Limits()
    {
        IPLevels eRet = new IPLevels();
        try
        {
            //TODO: please remove as much as possible, the hard codes 0,1,2,3,4,5 here. Thanks
            List<IPLimit> oIPLimits = lstGetIPLimits();
            foreach (IPLimit oIPLimit in oIPLimits)
            {
                if (oIPLimit.m_iSeq == 0)
                {
                    eRet.iValue0 = oIPLimit.m_iIPLimit;
                    eRet.iLimitId0 = oIPLimit.m_iIPLimitId;
                }
                else if (oIPLimit.m_iSeq == 1) 
                {
                    eRet.iValue1 = oIPLimit.m_iIPLimit;
                    eRet.iLimitId1 = oIPLimit.m_iIPLimitId;
                }
                else if (oIPLimit.m_iSeq == 2) 
                {
                    eRet.iValue2 = oIPLimit.m_iIPLimit;
                    eRet.iLimitId2 = oIPLimit.m_iIPLimitId;
                }
                else if (oIPLimit.m_iSeq == 3) 
                {
                    eRet.iValue3 = oIPLimit.m_iIPLimit;
                    eRet.iLimitId3 = oIPLimit.m_iIPLimitId;
                }
                else if (oIPLimit.m_iSeq == 4) 
                {
                    eRet.iValue4 = oIPLimit.m_iIPLimit;
                    eRet.iLimitId4 = oIPLimit.m_iIPLimitId;
                }
                else if (oIPLimit.m_iSeq == 5) 
                {
                    eRet.iValue5 = oIPLimit.m_iIPLimit;
                    eRet.iLimitId5 = oIPLimit.m_iIPLimitId;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return eRet;
    }
}