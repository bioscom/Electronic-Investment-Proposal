using System;
using System.Collections.Generic;
using System.Text;

public static class Apps
{
    static Apps()
    {

    }

    public static string LoginIDNoDomain(string loginID)
    {
        if (loginID.IndexOf("\\") >= 0)
        {
            loginID = loginID.Substring(loginID.IndexOf("\\") + 1);
        }
        return loginID;
    }
}