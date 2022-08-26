using System;

public partial class Others_UserControls_dateControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public DateTime DateSelectedDate
    {
        get
        {
            DateTime dt = new DateTime();
            try
            {
                if (txtDate.Text != "")
                {
                    string[] PF = txtDate.Text.Split('-');
                    dt = new DateTime(int.Parse(PF[2]), int.Parse(PF[1]), int.Parse(PF[0]));
                }
            }
            catch (Exception ex)
            {
                //appMonitor.logAppExceptions(ex);
                System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
            }
            return ((txtDate.Text == "") ? DateTime.Today.Date : dt.Date);
        }
    }

    public void setDate(string sDate)
    {
        txtDate.Text = sDate;
    }
}
