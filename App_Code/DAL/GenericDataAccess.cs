using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;


/// <summary>
/// Class contains generic data access functionality to be accessed from
/// the business tier
/// </summary>

public static class GenericDataAccess
{
    // static constructor
    static GenericDataAccess()
    {
        
    }

    // executes a command and returns the results as a DataTable object
    public static DataTable ExecuteSelectCommand(DbCommand command)
    {
        // The DataTable to be returned
        DataTable table = new DataTable();
        // Execute the command, making sure the connection gets closed in the
        // end
        try
        {
            // Open the data connection
            command.Connection.Open();

            // Execute the command and save the results in a DataTable
            DbDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);

            // Close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        finally
        {
            // Close the connection
            command.Connection.Close();
        }
        return table;
    }

    public static DbCommand CreateCommand()
    {
        // Obtain the database provider name
        string dataProviderName = AppConfiguration.DbProviderName;

        // Obtain the database connection string
        string connectionString = AppConfiguration.DbConnectionString;

        // Create a new data provider factory
        DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderName);

        // Obtain a database-specific connection object
        DbConnection conn = factory.CreateConnection();

        // Set the connection string
        conn.ConnectionString = connectionString;

        // Create a database-specific command object
        DbCommand comm = conn.CreateCommand();

        // Set the command type to stored procedure
        comm.CommandType = CommandType.Text;

        // Return the initialized command object
        return comm;
    }

    // execute an update, delete, or insert command and return the number of affected rows
    public static int ExecuteNonQuery(DbCommand command)
    {
        // The number of affected rows
        int affectedRows = -1;
        // Execute the command making sure the connection gets closed in the end
        try
        {
            // Open the connection of the command
            command.Connection.Open();
            // Execute the command and get the number of affected rows
            affectedRows = command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            // Log eventual errors and rethrow them
            appMonitor.logAppExceptions(ex);
        }
        finally
        {
            // Close the connection
            command.Connection.Close();
        }
        // return the number of affected rows
        return affectedRows;
    }

    // execute a select command and return a single result as a string
    public static string ExecuteScalar(DbCommand command)
    {
        // The value to be returned
        string value = "";
        // Execute the command making sure the connection gets closed in the end
        try
        {
            // Open the connection of the command
            command.Connection.Open();
            // Execute the command and get the number of affected rows
            value = command.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            // Log eventual errors and rethrow them
            appMonitor.logAppExceptions(ex);
            //Utilities.LogError(ex);
            //throw;
        }
        finally
        {
            // Close the connection
            command.Connection.Close();
        }
        // return the result
        return value;
    }

    public static DataSet ExecuteSelectCommand(string sql)
    {
        OracleConnection oCN = new OracleConnection();
        OracleCommand cmd = new OracleCommand();
        DataSet ds = new DataSet();
        //
        try
        {
            oCN.ConnectionString = AppConfiguration.DbConnectionString;
            oCN.Open();

            cmd = new OracleCommand(sql, oCN);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        finally
        {
            if (cmd.Connection.State != ConnectionState.Closed)
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
        }
        return ds;
    }
}

