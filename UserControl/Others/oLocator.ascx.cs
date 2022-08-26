using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Others_oLocator : System.Web.UI.UserControl
{
    appUserMgt oAppUserMgt = new appUserMgt();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPage()
    {
        if (!IsPostBack)
        {
            drpUserx.Visible = false;
            imbEdit.Visible = false;
        }
    }

    public appUsers selectedUserDetails
    {
        get
        {
            appUsers thisUser = oAppUserMgt.objGetUserByUserId(int.Parse(drpUserx.SelectedValue));
            return thisUser;
        }
    }

    protected void imbEdit_Click(object sender, ImageClickEventArgs e)
    {
        txtUserx.Text = "";
        txtUserx.Visible = true;
        imbFind.Visible = true;
        drpUserx.Visible = false;
        imbEdit.Visible = false;

        if (drpUserx.Items.Count > 1)
        {
            resetUserInfo();
        }
    }
    protected void imbFind_Click(object sender, ImageClickEventArgs e)
    {
        List<appUsers> oAppUsers = oAppUserMgt.lstGetUserInfoBySearch(txtUserx.Text);

        drpUserx.Visible = true;
        imbEdit.Visible = true;

        txtUserx.Visible = false;
        imbFind.Visible = false;

        drpUserx.Items.Clear();
        drpUserx.Items.Add(new ListItem("Please Select...", "-1"));
        foreach (appUsers oAppUser in oAppUsers)
        {
            drpUserx.Items.Add(new ListItem(oAppUser.m_sFullName, oAppUser.m_iUserId.ToString()));
        }

        if (oAppUsers.Count == 0)
        {
            resetUserInfo();
        }
    }

    public void resetUserInfo()
    {
        txtUserx.Visible = true;
        txtUserx.Text = "";
        imbFind.Visible = true;

        drpUserx.Visible = false;
        drpUserx.Items.Clear();
        imbEdit.Visible = false;

        ListItem oDefItem = new ListItem();
        oDefItem.Value = "0";
        if (drpUserx.ToolTip == "")
        {
            oDefItem.Text = "[Select User]";
        }
        else
        {
            oDefItem.Text = "[" + drpUserx.ToolTip + "]";
        }
        drpUserx.Items.Add(oDefItem);
    }

    public void initUserInfo(string sToolTip, int xWidth)
    {
        txtUserx.Width = (Unit)(xWidth - (imbEdit.Width.Value * 1.6));
        drpUserx.Width = txtUserx.Width;
        drpUserx.ToolTip = sToolTip;

        resetUserInfo();
    }

    public bool userIsValid
    {
        get
        {
            if (drpUserx.SelectedValue == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public string _SelectedValue
    {
        get
        {
            return drpUserx.SelectedValue;
        }
    }

    public int setWidth
    {
        set
        {
            txtUserx.Width = (Unit)(value - (imbEdit.Width.Value * 1.6));
            drpUserx.Width = txtUserx.Width;
        }
    }
}