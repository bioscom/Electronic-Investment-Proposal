using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for appUserMgt
/// </summary>

public class appUserMgt
{
    public appUserMgt()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable dtGetUsers()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUsers();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetActiveUsers()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getActiveUsers();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetUsersByRole(int iRoleId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUsersByRole();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = iRoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public void SearchUser(GridView grdView, string SortExpression, string SearchCriteria)
    {
        LoadGridViews.LoadMyGridView(grdView, dtGetUserBySearch(SearchCriteria.ToUpper()), SortExpression);
    }

    public DataTable dtGetUserBySearch(string SearchCriteria)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.searchUser();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":SEARCHKEY";
        param.Value = "%" + SearchCriteria + "%";
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)Utilities.status.Active;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetRolesAcceptedOrPendingAcceptance(int iStatus)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getRolesAcceptedOrPendingAcceptance();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = iStatus;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetUsersByRole(int iRoleId, int iDefaultRole)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getDefaultUsersByRole();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = iRoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":FLAG_COLOR";
        param.Value = iDefaultRole;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetUserByUserId(int iUserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUserByUserId();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetUserByUserName(string UserName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUserByUserName();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":USERNAME";
        param.Value = UserName;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetUserByUserDomain(string UserDomainName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUserByDomainLoginId();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":sUserMail";
        param.Value = UserDomainName;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        return dt;
    }

    public appUsers objGetUserByUserId(int iUserId)
    {
        DataTable dt = dtGetUserByUserId(iUserId);

        appUsers xRows = new appUsers();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new appUsers(dr);
        }
        return xRows;
    }

    public appUsers objGetUserByUserName(string UserName)
    {
        DataTable dt = dtGetUserByUserName(UserName);

        appUsers xRows = new appUsers();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new appUsers(dr);
        }
        return xRows;
    }

    public appUsers objGetUserByUserDomainName(string UserDomainName)
    {
        DataTable dt = dtGetUserByUserDomain(UserDomainName);

        appUsers xRows = new appUsers();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new appUsers(dr);
        }
        return xRows;
    }

    public DataTable dtGetUserByUserRoleCompany(int iCompanyId, int iRoleId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUserByUserRoleCompany();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":COMPANYID";
        param.Value = iCompanyId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = iRoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":FLAG_COLOR";
        param.Value = DefaultRoleHolder.iDefault;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public appUsers objGetUserByUserRoleCompany(int iCompanyId, int iRoleId)
    {
        DataTable dt = dtGetUserByUserRoleCompany(iCompanyId, iRoleId);

        appUsers xRows = new appUsers();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new appUsers(dr);
        }
        return xRows;
    }

    public appUsers objGetUsersByRole(int iRoleId)
    {
        DataTable dt = dtGetUsersByRole(iRoleId);

        appUsers xRows = new appUsers();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new appUsers(dr);
        }
        return xRows;
    }

    public appUsers objGetDefaultUsersByRole(int iRoleId, int iDefaultRole)
    {
        DataTable dt = dtGetUsersByRole(iRoleId, iDefaultRole);

        appUsers xRows = new appUsers();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new appUsers(dr);
        }
        return xRows;
    }

    public List<appUsers> lstGetUsers()
    {
        DataTable dt = dtGetUsers();

        List<appUsers> xRows = new List<appUsers>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new appUsers(dr));
        }
        return xRows;
    }

    public List<appUsers> lstGetUserInfoBySearch(string sFullName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getAppUsers();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":sFULLNAME";
        param.Value = "%" + sFullName.ToUpper() + "%";
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        List<appUsers> result = new List<appUsers>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            result.Add(new appUsers(dr));
        }

        return result;
    }

    public List<appUsers> lstGetUsersByRole(int iRoleId)
    {
        DataTable dt = dtGetUsersByRole(iRoleId);

        List<appUsers> xRows = new List<appUsers>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new appUsers(dr));
        }
        return xRows;
    }

    public bool CreateUserProfile(string sUserName, string sFullName, string sUSERMAIL, int iFunctionId, int iIPLimitId, int iRoleId, int iOU)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.CreateNewUser();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":USERNAME";
        param.Value = sUserName;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":FULLNAME";
        param.Value = sFullName;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERMAIL";
        param.Value = sUSERMAIL;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":FUNCTIONID";
        param.Value = iFunctionId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDIPLIMIT";
        param.Value = iIPLimitId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = iRoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        //NOTE: when a user is defined by the Administrator or Business Process owner, the profile is locked, 
        //when the user accepts his/her role, he/she unlocks her profile for use.
        param.Value = (int)appUsersRoles.userStatus.lockedUser; 
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":EPNIGERIALOGIC";
        param.Value = iOU;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":COMPANYID";
        param.Value = iOU;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

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

    public bool UpdateUserProfile(int iUserId, string sUserName, int iFunctionId, int iRoleId, int iOU, int iIPLimitId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.UpdateUserProfile();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERNAME";
        param.Value = sUserName;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":FUNCTIONID";
        param.Value = iFunctionId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDIPLIMIT";
        param.Value = iIPLimitId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = iRoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":EPNIGERIALOGIC";
        param.Value = iOU;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":COMPANYID";
        param.Value = iOU;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

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

    public bool deleteUserProfile(int iUserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.DeleteUserProfile();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        //NOTE: when a user is defined by the Administrator or Business Process owner, the profile is locked, 
        //when the user accepts his/her role, he/she unlocks her profile for use.
        param.Value = (int)appUsersRoles.userStatus.lockedUser;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

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

    

    public bool RoleAcceptance(int iUserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.RoleAcceptance();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        //NOTE: when a user is defined by the Administrator or Business Process owner, the profile is locked, 
        //when the user accepts his/her role, he/she unlocks her profile for use.
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

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

    //public List<appUsers> lstGetMyLTLs()
    //{
    //    DataTable dt = dtGetMyLTLs();

    //    List<appUsers> xRows = new List<appUsers>(dt.Rows.Count);
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        xRows.Add(new appUsers(dr));
    //    }
    //    return xRows;
    //}

    //public DataTable dtGetMyLTLs()
    //{
    //    DataTable dt = new DataTable();

    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.GetMyLineTeamLeads();

    //    DbParameter param = comm.CreateParameter();
    //    param.ParameterName = ":USERROLESID";
    //    param.Value = (int)appUsersRoles.userRole.Line_Team_Lead;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":STATUS";
    //    param.Value = (int)appUsersRoles.userStatus.activeUser;
    //    param.DbType = DbType.String;
    //    param.Size = 200;
    //    comm.Parameters.Add(param);

    //    return GenericDataAccess.ExecuteSelectCommand(comm);
    //}

    public bool bUpdateDefaultRoleAbsenceMgt(int iDefaultNonDeault, int iUserId)
    {
        DataTable dt = new DataTable();

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.DefaultRoleAbsenceMgt();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":FLAG_COLOR";
        param.Value = iDefaultNonDeault;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

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

    public bool bRouteProposalToNewDefaultRole(int iStatus, int iUserId, int iRoleId)
    {
        DataTable dt = new DataTable();

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.RouteProposalToNewDefaultRole();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":DOC_STAND";
        param.Value = iStatus;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = iRoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

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
}

//public static CompleteStaffDetailsInfo GetStaffInfo(string UserName)
//{
//    // get a configured DbCommand object
//    DbCommand comm = GenericDataAccess.CreateCommand();
//    // set the stored procedure name
//    string sql = "SELECT USERNAME, FULL_NAME, EMAIL FROM COMPLETE_STAFF_DETAILS@IWH_LINK.WORLD WHERE upper(USERNAME) = :USERNAME";
//    comm.CommandText = sql;

//    DbParameter param = comm.CreateParameter();
//    param.ParameterName = ":USERNAME";
//    param.Value = UserName.ToUpper();
//    param.DbType = DbType.String;
//    param.Size = 500;
//    comm.Parameters.Add(param);

//    // obtain the results
//    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
//    CompleteStaffDetailsInfo result = new CompleteStaffDetailsInfo();
//    foreach (DataRow row in dt.Rows)
//    {
//        result.USERNAME = row["USERNAME"].ToString();
//        result.EMAIL = row["EMAIL"].ToString();
//        result.FULL_NAME = row["FULL_NAME"].ToString();
//    }
//    return result;
//}

//public static string GetFunctionalPlannerEmailByFunctionID(string FunctionID)
//{
//    string FunctionalPlannerEmail = "";

//    try
//    {
//        string sql = "SELECT USERMAIL FROM EIP_USERMGT WHERE FUNCTIONID = :FUNCTIONID AND USERROLESID = :USERROLESID AND STATUS = :STATUS";
//        sql = sql.Replace(":FUNCTIONID", "'" + FunctionID + "'");
//        sql = sql.Replace(":USERROLESID", "'" + (int)appUsersRoles.userRole.Functional_Planner + "'");
//        sql = sql.Replace(":STATUS", "'" + IPStatus.Activated + "'");

//        DataTable dt = DataAccess.ExecuteQueryCommand(sql);
//        if (dt.Rows.Count > 0)
//        {
//            FunctionalPlannerEmail = dt.Rows[0]["USERMAIL"].ToString();
//        }
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show(ex.Message.ToString() + "\n\n" +
//                        "Functional Planner not found for your function. \n\n" +
//                        "Please, if you see this message, report to the Business Process Owner \n" +
//                        "or System Administrator, to register a Functional Planner for your function.\n\n" +
//                        "Thank you.");
//    }
//    return FunctionalPlannerEmail;
//}

//// For single sign on
//public string GetUserByDomainLoginID(string UserName)
//{
//    string sql = "";
//    sql = "SELECT IDUSERMGT, USERNAME, FULLNAME, USERMAIL, USERROLESID, FUNCTIONID, COMPANYID, IDIPLIMIT, FLAG_COLOR, EPNIGERIALOGIC FROM EIP_USERMGT ";
//    sql += "WHERE lower(substr(USERMAIL,1,instr(USERMAIL,'@')-1)) = lower('" + UserName + "') AND STATUS = '" + IPStatus.Activated + "'";

//    return sql;
//}
