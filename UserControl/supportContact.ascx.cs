using System;
using System.Collections.Generic;

public partial class UserControl_supportContact : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Page_Init()
    {
        appUserMgt oAppUsersMgt = new appUserMgt();
       // appUsers oBPO = oAppUsersMgt.objGetUsersByRole((int)appUsersRoles.userRole.Business_Process_Owner);
        List<appUsers> oBPO = oAppUsersMgt.lstGetUsersByRole((int)appUsersRoles.userRole.Business_Process_Owner);
        foreach (appUsers o in oBPO)
        {
            supportBlst.Items.Add("BPO: " + o.m_sFullName );
        }

        //
        //supportBlst.Items.Add(appUsersRoles.userRoleDesc((appUsersRoles.userRole)(int)appUsersRoles.userRole.Business_Process_Owner) + ": " + oBPO.m_sFullName + " (Ext.: " + oBPO.m_sSysAdminExt + ")");
       // supportBlst.Items.Add("BPO: " + oBPO.m_sFullName + " (Ext.: " + oBPO.m_sSysAdminExt + ")");

        //List<appUsers> oAdmins = oAppUsersMgt.lstGetUsersByRole((int)appUsersRoles.userRole.Administrator);
        //foreach (appUsers oAdmin in oAdmins)
        //{
        //    supportBlst.Items.Add(oAdmin.m_sFullName + " (Ext.: " + oAdmin.m_sSysAdminExt + ")");
        //}

        supportBlst.Items.Add("Support Email: uig-it-assist-business@shell.com");
        supportBlst.Items.Add("Support Line: Helpdesk 1010 Option 2");
    }
}
