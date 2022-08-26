using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_userFinder_Search4User : System.Web.UI.UserControl
{
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

    public CompleteStaffDetailsInfo selectedUserDetails
    {
        get
        {
            CompleteStaffDetailsInfo thisUser = Users.objGetStaffFromCompleteStaffDetails(drpUserx.SelectedValue);
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
        List<CompleteStaffDetailsInfo> staffs = Users.lstGetStaffInfoBySearch(txtUserx.Text);

        drpUserx.Visible = true;
        imbEdit.Visible = true;

        txtUserx.Visible = false;
        imbFind.Visible = false;

        drpUserx.Items.Clear();
        drpUserx.Items.Add(new ListItem("Please Select...", "-1"));
        foreach (CompleteStaffDetailsInfo staff in staffs)
        {
            drpUserx.Items.Add(new ListItem(staff.m_sFullName, staff.m_sUserName));
        }

        if (staffs.Count == 0)
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
            oDefItem.Text = "[Select GID User Name]";
        }
        else
        {
            oDefItem.Text = "[" + drpUserx.ToolTip + "]";
        }
        drpUserx.Items.Add(oDefItem);
    }

    public void initUserInfo(string sToolTip, int xWidth)
    {
        txtUserx.Width = (Unit) (xWidth - (imbEdit.Width.Value * 1.6));
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
            txtUserx.Width = (Unit) (value - (imbEdit.Width.Value * 1.6));
            drpUserx.Width = txtUserx.Width;
        }
    }
}