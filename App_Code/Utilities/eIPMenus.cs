using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Collections.Generic;

/// <summary>
/// Summary description for eIPMenus
/// </summary>
/// 

public class eMenus
{
    public int m_iMenu { get; set; }
    public string m_sTitle { get; set; }
    public string m_sDescription { get; set; }
    public string m_NavigateUrl { get; set; }
    public int m_iParentid { get; set; }

    public eMenus()
    {
        
    }

    public eMenus(DataRow dr)
    {
        m_iMenu = int.Parse(dr["MENUID"].ToString());
        m_sTitle = dr["TITLE"].ToString();
        m_sDescription = dr["DESCRIPTION"].ToString();
        m_NavigateUrl = dr["NAVIGATEURL"].ToString();
        m_iParentid = (dr["PARENTID"] == DBNull.Value) ? 0 : int.Parse(dr["PARENTID"].ToString());
    }
}


public class eIPMenus
{
	public eIPMenus()
	{
		
	}

    public DataTable dtGetUserMenu()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getMenu();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetMainMenu()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getMainMenu();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public List<eMenus> lstGetMainMenus()
    {
        DataTable dt = dtGetMainMenu();

        List<eMenus> xRows = new List<eMenus>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new eMenus(dr));
        }
        return xRows;
    }

    public void getUserMenu(XmlDataSource xmlDS, appUsers oAppUser)
    {
        string sql = StoredProcedure.LoadMyMenu().Replace(":USERROLESID", oAppUser.m_iUserRoleId.ToString());

        DataSet ds = GenericDataAccess.ExecuteSelectCommand(sql);                                              
        ds.DataSetName = "Menus";
        ds.Tables[0].TableName = "Menu";

        DataRelation relation = new DataRelation("ParentChild", ds.Tables["Menu"].Columns["MENUID"], ds.Tables["Menu"].Columns["PARENTID"], true);
        relation.Nested = true;
        ds.Relations.Add(relation);

        try
        {
            xmlDS.Data = ds.GetXml(); //Returns the XML representation of the data stored in the System.Data.DataSet

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    
    #region menuBuilder Queries

    public bool AssignMenuToRole(int menuID, int UserRoleID)
    {
        string sql = "INSERT INTO EIP_MENU_USERROLE (MENUID, USERROLESID) VALUES (:MENUID, :USERROLESID)";

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":MENUID";
        param.Value = menuID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = UserRoleID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public bool MyParentFound(int ParentID, int UserRoleID)
    {
        bool ParentFound = false;
        string sql = "SELECT * FROM EIP_MENU_USERROLE WHERE MENUID = :PARENTID AND USERROLESID = :USERROLESID";

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":PARENTID";
        param.Value = ParentID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = UserRoleID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        try
        {
            DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
            if (dt.Rows.Count > 0)
            {
                ParentFound = true;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
            //MessageBox.Show(ex.Message.ToString());
        }

        return ParentFound;
    }

    public bool ConfirmMenuForRole(int menuID, int UserRoleID)
    {
        //Where option has been previously selected for a role, return true
        // if true, then don't recreate the option for the role again

        bool optionExists = false;
        string sql = "SELECT * FROM EIP_MENU_USERROLE WHERE MENUID = :MENUID AND USERROLESID = :USERROLESID";

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":MENUID";
        param.Value = menuID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = UserRoleID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        try
        {
           DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
           if (dt.Rows.Count > 0)
           {
               optionExists = true;
           }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return optionExists;
    }

    //public static bool UpdateMainMenu(string Title, string Desc, string menuID)
    //{
    //    string sql = "UPDATE EIP_MENU_BUILDER SET TITLE = :TITLE, DESCRIPTION = :DESCRIPTION WHERE MENUID = :MENUID";
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = sql;

    //    // create a new parameter
    //    DbParameter param = comm.CreateParameter();
    //    param.ParameterName = ":MENUID";
    //    param.Value = menuID;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":TITLE";
    //    param.Value = Title;
    //    param.DbType = DbType.String;
    //    param.Size = 1000;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":DESCRIPTION";
    //    param.Value = Desc;
    //    param.DbType = DbType.String;
    //    param.Size = 1000;
    //    comm.Parameters.Add(param);

    //    // result will represent the number of changed rows
    //    int result = -1;
    //    try
    //    {
    //        // execute the stored procedure
    //        result = GenericDataAccess.ExecuteNonQuery(comm);
    //    }
    //    catch (Exception ex)
    //    {
    //        //System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //        MessageBox.Show(ex.Message.ToString());
    //    }
    //    // result will be 1 in case of success
    //    return (result != -1);
    //}

    public bool InsertMainMenu(string Title, string Desc)
    {
        string sql = "INSERT INTO EIP_MENU_BUILDER (TITLE, DESCRIPTION) VALUES (:TITLE, :DESCRIPTION)";

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":TITLE";
        param.Value = Title;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DESCRIPTION";
        param.Value = Desc;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public bool InsertSubMenu(string Title, string Desc, string NavigateUrl, int ParentId)
    {
        string sql = "INSERT INTO EIP_MENU_BUILDER (TITLE, DESCRIPTION, NAVIGATEURL, PARENTID) VALUES (:TITLE, :DESCRIPTION, :NAVIGATEURL, :PARENTID)";

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":TITLE";
        param.Value = Title;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DESCRIPTION";
        param.Value = Desc;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":NAVIGATEURL";
        param.Value = NavigateUrl;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PARENTID";
        param.Value = ParentId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public bool UpdateSubMenu(string Title, string Desc, string NavigateUrl, int ParentId, int MenuId)
    {
        string sql = "UPDATE EIP_MENU_BUILDER SET TITLE = :TITLE, DESCRIPTION = :DESCRIPTION, NAVIGATEURL = :NAVIGATEURL, PARENTID = :PARENTID WHERE MENUID = :MENUID";

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":TITLE";
        param.Value = Title;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DESCRIPTION";
        param.Value = Desc;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":NAVIGATEURL";
        param.Value = NavigateUrl;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PARENTID";
        param.Value = ParentId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":MENUID";
        param.Value = MenuId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public bool DeleteMenuForRole(int menuID, int UserRoleID)
    {
        string sql = "DELETE FROM EIP_MENU_USERROLE WHERE MENUID = :MENUID AND USERROLESID = :USERROLESID";

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":MENUID";
        param.Value = menuID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = UserRoleID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public void DeleteMainMenu(string menuID)
    {
        string sql = "DELETE FROM EIP_MENU_BUILDER WHERE MENUID = '" + menuID + "'";
        //db.DeleteQuery(sql);
    }

    public void LoadMainMenu(GridView grd, string SortExpression)
    {
        string sql = "SELECT MENUID, TITLE, DESCRIPTION, NAVIGATEURL, PARENTID FROM EIP_MENU_BUILDER WHERE PARENTID IS NULL";
        DataTable dt = DataAccess.ExecuteQueryCommand(sql);
        LoadGridViews.LoadMyGridView(grd, dt, SortExpression);
    }

    public void LoadSubMenu(GridView grd, string SortExpression, int ParentId)
    {
        string sql = "SELECT MENUID, TITLE, DESCRIPTION, NAVIGATEURL, PARENTID FROM EIP_MENU_BUILDER WHERE PARENTID = :PARENTID ORDER BY MENUID";
        sql.Replace(":PARENTID", "'" + ParentId + "'");
        DataTable dt = DataAccess.ExecuteQueryCommand(sql);
        LoadGridViews.LoadMyGridView(grd, dt, SortExpression);
    }

    public DataTable LoadMyMenu(string RoleId)
    {
        //string sql = "SELECT EIP_MENU_BUILDER.MENUID, EIP_MENU_BUILDER.TITLE, EIP_MENU_BUILDER.DESCRIPTION, EIP_MENU_BUILDER.NAVIGATEURL, ";
        //sql += "EIP_MENU_BUILDER.PARENTID FROM EIP_USERROLES ";
        //sql += "INNER JOIN EIP_MENU_USERROLE ON EIP_USERROLES.USERROLESID = EIP_MENU_USERROLE.USERROLESID ";
        //sql += "INNER JOIN EIP_MENU_BUILDER ON EIP_MENU_USERROLE.MENUID = EIP_MENU_BUILDER.MENUID ";
        //sql += "WHERE EIP_USERROLES.USERROLESID = '" + RoleId + "' ORDER BY MENUID";

        string sql = "SELECT EIP_MENU_BUILDER.MENUID, EIP_MENU_BUILDER.TITLE, EIP_MENU_BUILDER.DESCRIPTION, EIP_MENU_BUILDER.NAVIGATEURL, ";
        sql += "EIP_MENU_BUILDER.PARENTID FROM EIP_MENU_USERROLE ";
        sql += "INNER JOIN EIP_MENU_BUILDER ON EIP_MENU_USERROLE.MENUID = EIP_MENU_BUILDER.MENUID ";
        sql += "WHERE EIP_MENU_USERROLE.USERROLESID = '" + RoleId + "' ORDER BY MENUID";

        return DataAccess.ExecuteQueryCommand(sql);
    }

    public string LoadApplicationMenus()
    {
        string sql = "SELECT MENUID, TITLE, DESCRIPTION, NAVIGATEURL, PARENTID FROM EIP_MENU_BUILDER WHERE NAVIGATEURL IS NOT NULL ORDER BY TITLE";
        return sql;
    }

    #endregion
}