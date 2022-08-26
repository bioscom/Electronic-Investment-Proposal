using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Globalization;

public partial class UserControl_DateSelector : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                drpDay.Attributes.Add("onChange", "validateDate(document.getElementById('" + drpDay.ClientID + "'),document.getElementById('" + drpMonth.ClientID + "'),document.getElementById('" + drpYear.ClientID + "'));");
                drpMonth.Attributes.Add("onChange", "validateDate(document.getElementById('" + drpDay.ClientID + "'),document.getElementById('" + drpMonth.ClientID + "'),document.getElementById('" + drpYear.ClientID + "'));");
                drpYear.Attributes.Add("onChange", "validateDate(document.getElementById('" + drpDay.ClientID + "'),document.getElementById('" + drpMonth.ClientID + "'),document.getElementById('" + drpYear.ClientID + "'));");
                m_LoadYearItems(DateTime.Now.Year + 20, DateTime.Now.Year - 40, DateTime.Now);
            }
        }
        catch (Exception ex)
        {
            Debug.Fail(ex.TargetSite.Name +"\n\n" + ex.StackTrace +"\n\n" + ex.Message);
        }
    }

    private static string m_MonthFullName(byte tMonth)
    {
        string sRet = "Error";
        try
        {
             switch(tMonth)
             {
                case 1 : sRet = "January"; break;
                case 2 : sRet = "February"; break;
                case 3 : sRet = "March"; break;
                case 4 : sRet = "April"; break;
                case 5 : sRet = "May"; break;
                case 7 : sRet = "July"; break;
                case 8 : sRet = "August"; break;
                case 9 : sRet = "September"; break;
                case 10 : sRet = "October"; break;
                case 11 : sRet = "November"; break;
                case 12 : sRet = "December";  break;
             }
        }
        catch(Exception ex)
        {
            Debug.Fail(ex.TargetSite.Name + "\n\n" + ex.StackTrace + "\n\n" + ex.Message);
        }
        return sRet.Substring(0, 3);
    }

    private bool m_LoadYearItems(int dMax, int dMin, DateTime dNow)
    {
        try
        {
            drpDay.Items.Clear();
            for (byte iLoop = 1; iLoop <= 31; iLoop++)
            {
                byte oItemText = iLoop;
                byte oItemValue = iLoop;
                drpDay.Items.Add(new ListItem(oItemText.ToString(), oItemValue.ToString()));
            }
            drpDay.SelectedIndex = dNow.Day - 1;

            drpMonth.Items.Clear();
            for (byte iLoop = 1; iLoop <= 12; iLoop++)
            {
                byte oItemText = iLoop;
                byte oItemValue = iLoop;

                drpMonth.Items.Add(new ListItem(oItemText.ToString(), oItemValue.ToString()));
            }
            drpMonth.SelectedIndex = dNow.Month - 1;

            drpYear.Items.Clear();
            for(int iLoop = dMin; iLoop <=dMax; iLoop++)
            {
                int oItemText = iLoop;
                int oItemValue = iLoop;
                drpYear.Items.Add(new ListItem(oItemText.ToString(), oItemValue.ToString()));
            }
            drpYear.Text = dNow.Year.ToString();
        }
        catch (Exception ex)
        {
            Debug.Fail(ex.TargetSite.Name +"\n\n" + ex.StackTrace +"\n\n" + ex.Message);
        }

        return true;
    }

    private string m_MyDateValue()
    {
        string dDate = "";
        try
        {
            int xDay = Convert.ToInt32(drpDay.SelectedValue);
            int xMonth = Convert.ToInt32(drpMonth.SelectedValue);
            int xYear = Convert.ToInt32(drpYear.SelectedValue);

            switch (xDay)
            {
                case 31:
                    switch (xMonth)
                    {
                        case 2:
                        case 4:
                        case 6:
                        case 9:
                        case 11:
                            xDay = 30; break;
                        case 30:
                            if (xMonth == 2)
                            {
                                xDay = 28;
                            }
                            break;
                        case 29:
                            if (xMonth == 2)
                            {
                                long x;
                                if (Math.DivRem(xYear, 4, out x) == 0)
                                {
                                    if (Math.DivRem(xYear, 100, out x) == 0)
                                    {
                                        if (Math.DivRem(xYear, 400, out x) != 0)
                                        {
                                            xDay = 28;
                                        }
                                    }
                                }
                                xDay = 28;
                            }
                            break;
                    }
                    break;
            }
            string sTemp = "/" + xYear + "#"; //set year value
            sTemp = "#" + xMonth + "/" + xDay + sTemp;

            dDate = xMonth + "/" + xDay + "/" + xYear;
            //If Not Date.TryParse(sTemp, dDate) Then dDate = Now
            //if(!DateTime.TryParse(sTemp, out dDate))
            //{
            //    dDate = DateTime.Now;
            //}
        }
        catch (Exception ex)
        {
            Debug.Fail(ex.TargetSite.Name + "\n\n" + ex.StackTrace + "\n\n" + ex.Message);
        }
        return dDate;
    }


    private DateTime m_MyDateValue2()
    {
        DateTime dDate = DateTime.Now;
        try
        {
            int xDay = Convert.ToInt32(drpDay.SelectedValue);
            int xMonth = Convert.ToInt32(drpMonth.SelectedValue);
            int xYear = Convert.ToInt32(drpYear.SelectedValue);

            switch (xDay)
            {
                case 31:
                    switch (xMonth)
                    {
                        case 2:
                        case 4:
                        case 6:
                        case 9:
                        case 11:
                            xDay = 30; break;
                        case 30:
                            if (xMonth == 2)
                            {
                                xDay = 28;
                            }
                            break;
                        case 29:
                            if (xMonth == 2)
                            {
                                long x;
                                if (Math.DivRem(xYear, 4, out x) == 0)
                                {
                                    if (Math.DivRem(xYear, 100, out x) == 0)
                                    {
                                        if (Math.DivRem(xYear, 400, out x) != 0)
                                        {
                                            xDay = 28;
                                        }
                                    }
                                }
                                xDay = 28;
                            }
                            break;
                    }
                    break;
            }
            string sTemp = "/" + xYear + "#"; //set year value
            sTemp = "#" + xMonth + "/" + xDay + sTemp;

            if (!DateTime.TryParse(sTemp, out dDate))
            {
                dDate = DateTime.Now;
            }
        }
        catch (Exception ex)
        {
            Debug.Fail(ex.TargetSite.Name + "\n\n" + ex.StackTrace + "\n\n" + ex.Message);
        }
        return dDate;
    }


    #region "REGION : Public Property Memebers Exposed to Hosting Page"

    //public string Property {get; set;}

    public int setWidth
    {        
        set
        {
           value = value - 5;
            drpDay.Width = value * (30 / 100);
            drpMonth.Width = value * (32 / 100);
            drpYear.Width = value * (38 / 100);
        }
    }

    public string getDateValue
    {
        get
        {
            return m_MyDateValue();
        }
    }

    public string getDateString
    {
        get
        {
            DateTimeFormatInfo dtFormat = new CultureInfo(CultureInfo.CurrentCulture.ToString(), false).DateTimeFormat;
            return m_MyDateValue2().ToString(dtFormat.LongDatePattern);
        }
    }

    public void resetDateParams(int maximumYear, int minimumYear, DateTime currentDate)
    {
        try
        {
            if(currentDate.Year > maximumYear) maximumYear = currentDate.Year;
            if (minimumYear > currentDate.Year) minimumYear = currentDate.Year;
            if (minimumYear > maximumYear) minimumYear = maximumYear;
            m_LoadYearItems(maximumYear, minimumYear, currentDate);
        }
        catch (Exception ex)
        {
            Debug.Fail(ex.TargetSite.Name + "\n\n" + ex.StackTrace + "\n\n" + ex.Message);
        }
    }

    public void resetDateParams(int maximumYear, int minimumYear)
    {
        DateTime currentDate = DateTime.Now.AddYears(maximumYear - DateTime.Now.Date.Year);
        try
        {
            if (currentDate.Year > maximumYear) maximumYear = currentDate.Year;
            if (minimumYear > currentDate.Year) minimumYear = currentDate.Year;
            if (minimumYear > maximumYear) minimumYear = maximumYear;
            m_LoadYearItems(maximumYear, minimumYear, currentDate);
        }
        catch (Exception ex)
        {
            Debug.Fail(ex.TargetSite.Name + "\n\n" + ex.StackTrace + "\n\n" + ex.Message);
        }
    }

    public void resetDateParams(DateTime currentDate)
    {
        try
        {
            int maximumYear = Convert.ToInt32(drpYear.Items[drpYear.Items.Count - 1].Value);
            int minimumYear = Convert.ToInt32(drpYear.Items[0].Value);
            if (currentDate.Year > maximumYear) maximumYear = currentDate.Year;
            if (minimumYear > currentDate.Year) minimumYear = currentDate.Year;
            if (minimumYear > maximumYear) minimumYear = maximumYear;
            m_LoadYearItems(maximumYear, minimumYear, currentDate);
        }
        catch (Exception ex)
        {
            Debug.Fail(ex.TargetSite.Name + "\n\n" + ex.StackTrace + "\n\n" + ex.Message);
        }
    }

    public bool setEnabled
    {
        get
        {
            return drpDay.Enabled;
        }

        set
        {
            drpDay.Enabled = value;
            drpMonth.Enabled = value;
            drpYear.Enabled = value;
        }
    }
#endregion

}