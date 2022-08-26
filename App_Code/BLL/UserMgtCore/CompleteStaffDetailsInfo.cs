using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

public class CompleteStaffDetailsInfo
{
    public string m_sUserName { get; set; }
    public string m_sFullName { get; set; }
    public string m_sUserMail { get; set; }

    public string m_sSurname { get; set; }
    public string m_sFirstName { get; set; }
    public string m_sRefInd { get; set; }
    public string m_sJobPosition { get; set; }
    public string m_sJobTitle { get; set; }

    public CompleteStaffDetailsInfo()
    {

    }

    public CompleteStaffDetailsInfo(DataRow dr)
    {
        m_sUserName = dr["USERNAME"].ToString();
        m_sFullName = dr["FULL_NAME"].ToString();
        m_sUserMail = dr["EMAIL"].ToString();
        m_sSurname = dr["SURNAME"].ToString(); 
        m_sFirstName = dr["FIRST_NAME"].ToString(); 
        m_sRefInd = dr["REF_IND"].ToString(); 
        m_sJobPosition = dr["JOB_POSITION"].ToString(); 
        m_sJobTitle = dr["JOB_TITLE"].ToString(); 
    }
}

public class Users
{
    public Users()
    {

    }

    // For single sign on
    public static CompleteStaffDetailsInfo getUserByDomainLoginID(string activeDirectoryId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUserByDomainLoginID();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":sEmail";
        param.Value = activeDirectoryId;
        param.DbType = DbType.String;
        param.Size = 300;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        CompleteStaffDetailsInfo thisUser = new CompleteStaffDetailsInfo();
        foreach (DataRow dr in dt.Rows)
        {
            thisUser = new CompleteStaffDetailsInfo(dr);
        }
        return thisUser;
    }

    public static CompleteStaffDetailsInfo objGetStaffFromCompleteStaffDetails(string UserName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUserFromCompleteStaffDetailsByUserName();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":USERNAME";
        param.Value = UserName.ToUpper();
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        CompleteStaffDetailsInfo thisUser = new CompleteStaffDetailsInfo();
        foreach (DataRow dr in dt.Rows)
        {
            thisUser = new CompleteStaffDetailsInfo(dr);
        }
        return thisUser;
    }

    public static CompleteStaffDetailsInfo activeDirUser(object oUser)
    {
        CompleteStaffDetailsInfo oUserx = new CompleteStaffDetailsInfo();
        try
        {
            if (oUser != null)
            {
                if (int.Parse(oUserx.m_sUserName.Length.ToString()) > 0)
                {
                    oUserx = (CompleteStaffDetailsInfo)oUser;
                }
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return oUserx;
    }

    public static CompleteStaffDetailsInfo getStaffFromCompleteStaffDetails(string UserName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUserByDomainLoginID();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":sEmail";
        param.Value = UserName.ToUpper();
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        CompleteStaffDetailsInfo thisUser = new CompleteStaffDetailsInfo();
        foreach (DataRow dr in dt.Rows)
        {
            thisUser = new CompleteStaffDetailsInfo(dr);
        }
        return thisUser;
    }

    public static DataTable dtGetStaffInfoByPrefixText(string PrefixText)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.CompleteStafDetailsByPrefix();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":PREFIX";
        param.Value = "%" + PrefixText + "%";
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static List<CompleteStaffDetailsInfo> lstGetStaffInfoByPrefixText(string PrefixText)
    {
        DataTable dt = dtGetStaffInfoByPrefixText(PrefixText);
        List<CompleteStaffDetailsInfo> row = new List<CompleteStaffDetailsInfo>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            row.Add(new CompleteStaffDetailsInfo(dr));
        }

        return row;
    }

    public static List<CompleteStaffDetailsInfo> lstGetStaffInfoBySearch(string UserName)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = StoredProcedure.CompleteStafDetailsByName();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":SURNAME";
        param.Value = UserName.ToUpper();
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        // obtain the results
        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        List<CompleteStaffDetailsInfo> result = new List<CompleteStaffDetailsInfo>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            result.Add(new CompleteStaffDetailsInfo(dr));
        }

        return result;
    }

    public static CompleteStaffDetailsInfo lstGetStaffInfoByStaffNumber(string staffNumber)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = StoredProcedure.CompleteStafDetailsByStaffNumber();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":STAFF_NUMBER";
        param.Value = staffNumber.ToUpper();
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        // obtain the results
        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        CompleteStaffDetailsInfo result = new CompleteStaffDetailsInfo();
        foreach (DataRow dr in dt.Rows)
        {
            result = new CompleteStaffDetailsInfo(dr);
        }

        return result;
    }
}