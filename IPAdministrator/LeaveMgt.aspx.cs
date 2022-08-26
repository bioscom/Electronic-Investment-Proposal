using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class IPAdministrator_LeaveMgt : aspnetPage
{
    appUserMgt oAppUserMgt = new appUserMgt();

    //TODO: test this modules very well.
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utilities.getDefaultUserRoles(ddlRoles);
        }
    }

    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            if (int.Parse(ddlRoles.SelectedValue) == (int)appUsersRoles.userRole.Administrator)
            {
                dt = oAppUserMgt.dtGetUsersByRole((int)appUsersRoles.userRole.Administrator);
                LoadDelegates(dt);
            }
            else if (int.Parse(ddlRoles.SelectedValue) == (int)appUsersRoles.userRole.Business_Process_Owner)
            {
                dt = oAppUserMgt.dtGetUsersByRole((int)appUsersRoles.userRole.Business_Process_Owner);
                LoadDelegates(dt);
            }
            else if (int.Parse(ddlRoles.SelectedValue) == (int)appUsersRoles.userRole.CERP)
            {
                dt = oAppUserMgt.dtGetUsersByRole((int)appUsersRoles.userRole.CERP);
                LoadDelegates(dt);
            }
            //else if (int.Parse(ddlRoles.SelectedValue) == (int)appUsersRoles.userRole.EPG_IP_Tracker)
            //{
            //    dt = oAppUserMgt.dtGetUsersByRole((int)appUsersRoles.userRole.EPG_IP_Tracker);
            //    LoadDelegates(dt);
            //}
            else if (int.Parse(ddlRoles.SelectedValue) == (int)appUsersRoles.userRole.Finance_Director)
            {
                dt = oAppUserMgt.dtGetUsersByRole((int)appUsersRoles.userRole.Finance_Director); //Note: From a given OU
                LoadDelegates(dt);
            }
            else if (int.Parse(ddlRoles.SelectedValue) == (int)appUsersRoles.userRole.Finance_Signature)
            {
                dt = oAppUserMgt.dtGetUsersByRole((int)appUsersRoles.userRole.Finance_Signature);  //Note: From a given OU
                LoadDelegates(dt);
            }
            else if (int.Parse(ddlRoles.SelectedValue) == (int)appUsersRoles.userRole.GM_Regional_Planning)
            {
                dt = oAppUserMgt.dtGetUsersByRole((int)appUsersRoles.userRole.GM_Regional_Planning);
                LoadDelegates(dt);
            }
            else if (int.Parse(ddlRoles.SelectedValue) == (int)appUsersRoles.userRole.Technical_Planning_Manager)
            {
                dt = oAppUserMgt.dtGetUsersByRole((int)appUsersRoles.userRole.Technical_Planning_Manager);
                LoadDelegates(dt);
            }
            else if (int.Parse(ddlRoles.SelectedValue) == (int)appUsersRoles.userRole.REVP)
            {
                dt = oAppUserMgt.dtGetUsersByRole((int)appUsersRoles.userRole.REVP);
                LoadDelegates(dt);
            }

            lblRole.Text = "Current " + ddlRoles.SelectedItem.Text;
            lblRoleDesc.Text = ddlRoles.SelectedItem.Text;
            lblRoleDesc2.Text = ddlRoles.SelectedItem.Text;
            lblRoleDesc3.Text = ddlRoles.SelectedItem.Text;
            lblRoleDesc4.Text = ddlRoles.SelectedItem.Text;
            lblRoleDesc5.Text = ddlRoles.SelectedItem.Text;
            lblRoleDesc6.Text = ddlRoles.SelectedItem.Text;

            List<appUsers> lstSysAdmin = oAppUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.Administrator);
            foreach(appUsers oAppUser in lstSysAdmin)
            {
                if (oAppUser.m_iDefaultRoleHolder == DefaultRoleHolder.iDefault)
                {
                    SysAdminEmailAddress.Text = oAppUser.m_sEmail;
                    SysAdminExtension.Text = oAppUser.m_sSysAdminExt;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void updateButton_Click(object sender, EventArgs e)
    {
        bool bRet = false;
        try
        {
            if (DefaultDelegateSelected() > 1)
            {
                string oMessage = "You can only select one " + ddlRoles.SelectedItem.Text + " as the Default delegate. Select the Default delegate and unselect the others. Thank you.";
                ajaxWebExtension.showJscriptAlert(Page, this, oMessage);
            }
            else
            {
                foreach (GridViewRow grd in grdView.Rows)
                {
                    var BPOCheckBox = (CheckBox)grdView.Rows[grd.RowIndex].FindControl("BPOCheckBox");
                    int iUserID = int.Parse(BPOCheckBox.Attributes["IDUSERMGT"].ToString());
                    if (BPOCheckBox.Checked)
                    {
                        bRet = oAppUserMgt.bUpdateDefaultRoleAbsenceMgt(DefaultRoleHolder.iDefault, iUserID);
                        bRet = oAppUserMgt.bRouteProposalToNewDefaultRole(SupportState.iApproved, iUserID, int.Parse(ddlRoles.SelectedValue));
                    }
                    else if (!BPOCheckBox.Checked)
                    {
                        bRet = oAppUserMgt.bUpdateDefaultRoleAbsenceMgt(DefaultRoleHolder.iNoneDefault, iUserID);
                    }
                }
            }
            ddlRoles_SelectedIndexChanged(this, e);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private int DefaultDelegateSelected()
    {
        int SelectedBPO = 0;
        foreach (GridViewRow grd in grdView.Rows)
        {
            var BPOCheckBox = (CheckBox)grdView.Rows[grd.RowIndex].FindControl("BPOCheckBox");
            if (BPOCheckBox.Checked == true)
            {
                SelectedBPO += 1;
            }
        }

        return SelectedBPO;
    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Common/IPRegister.aspx");
    }

    private void LoadDelegates(DataTable dt)
    {
        try
        {
            if ((dt.Rows.Count > 0))
            {
                grdView.DataSource = dt;
                grdView.DataBind();

                foreach (GridViewRow grd in grdView.Rows)
                {
                    Label status = (Label)grdView.Rows[grd.RowIndex].FindControl("statusLabel");
                    CheckBox BPOCheckBox = (CheckBox)grdView.Rows[grd.RowIndex].FindControl("BPOCheckBox");
                    int FlagColor = Convert.ToInt32(BPOCheckBox.Attributes["FLAG_COLOR"]);
                    if (FlagColor == DefaultRoleHolder.iDefault)
                    {
                        status.Text = "Default " + ddlRoles.SelectedItem.Text;
                        BPOCheckBox.Checked = true;
                    }
                    else if (FlagColor == 0)
                    {
                        status.Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
}