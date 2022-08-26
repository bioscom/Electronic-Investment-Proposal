using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class taskPage : aspnetPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (oSessnx.getOnlineUser.m_iUserId != 0)
        {
            if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.IP_Initiator)
            {
                Response.Redirect("~/Common/MyProposal.aspx");
            }
            else if ((oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Business_Process_Owner)
                || (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Administrator)
                || (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.CERP)
                || (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Auditor))

                //|| (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.GM_Regional_Planning)
                //|| (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Technical_Planning_Manager)
                //|| (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.MD)
                //|| (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.VP)
                //|| (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.REVP)
                
                //)
            {
                Response.Redirect("~/Common/IPRegister.aspx");
            }
            else
            {
                Response.Redirect("~/ApprovalSupportFunction/PendingProposal.aspx");
            }
        }
        else
        {
            //AccessLabel.Text = "Access denied!!!";
            //regMemLabel.Text = "Access only for registered members.";
            ajaxWebExtension.showJscriptAlert(Page, this, "You were not registered to use this application or your profile has been removed. Contact the System Administrator for further Information. Thank you.");
        }
    }
}