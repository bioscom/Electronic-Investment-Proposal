using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class UserControl_orpCalendar : System.Web.UI.UserControl
{
    public string DateSelected
    {
        get { return sel1.Value; }
        set { sel1.Value = value; }
    }

    //public string ClearDate
    //{
    //    get { return sel1.Value; }
    //    set { sel1.Value = ""; }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}

