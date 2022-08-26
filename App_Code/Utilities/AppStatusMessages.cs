using System;
using System.Collections.Generic;
using System.Text;

public class AppStatusMessages
{
    private AppStatusMessages()
    {

    }

    public static void MailServerStatus(Exception ex)
    {
        if (ex.Message.ToString().Length > 0)
        {
            MessageBox.Show(ex.Message.ToString() + " Mail server not available, please try again later.");
        }
        else
        {
            MessageBox.Show("Mail successfully sent.");
        }
    }

    public static void SLANonCompliance(string receiverFullName)
    {
        MessageBox.Show("SLA non-compliance reminder successfully sent to '" + receiverFullName + "'");
    }

    

    public static void InsertUpdate(Exception ex)
    {
        if (ex.Message.ToString().Length > 0)
        {
            MessageBox.Show("Operation was not successful, please try again later.");
        }
        else
        {
            MessageBox.Show("Operation was sucessful.");
        }
    }

    public static void SelectQuery(Exception ex)
    {
        if (ex.Message.ToString().Length > 0)
        {
            MessageBox.Show("Connection to the database failed, please try again later.\n\n" + ex.Message.ToString());
        }
    }

    public static void ConnectionToDataBaseServer(Exception ex)
    {
        if (ex.Message.ToString().Length > 0)
        {
            MessageBox.Show("Connection to database server could not be established, please try again later.\n\n" + ex.Message.ToString());
        }
    }

    public static void UserAlreadyExistsForThisRole(string rolesCurrentUser)
    {
        MessageBox.Show(rolesCurrentUser + " is the current user for this role. \n\n" +
                        "To register another user for this role, the current user must be deactivated.\n\n" +
                        "Delete " + rolesCurrentUser + " from user's list.");
    }

    public static void UserAlreadyExists(string UserName)
    {
        MessageBox.Show("User " + UserName + " already exists!!!");
    }

    public static void UserNotFoundOnStaffDatabase(string UserName)
    {
        MessageBox.Show("Staff User Name: " + UserName + " does not exist on shell network, check the user name and type correctly.");
    }

    public static void BOMNotFound(string UserName)
    {
        MessageBox.Show("BOM User Name: " + UserName + " does not exist on shell network, check the user name and type correctly.");
    }

    public static void UserInfoOnShellStaffDataBaseNotComplete(string UserName)
    {
        MessageBox.Show(UserName + ": Full Name or User email not found from the Complete Staff Details Database, \n\n" +
                        "please notify DBA Services Team and try again later.");
    }

    public static void ConnectionToRemoteDatabaseFailed(Exception ex)
    {
        MessageBox.Show("Connection to the database to verify User Name not available, please try again later. " + ex.Message.ToString());
    }
}

