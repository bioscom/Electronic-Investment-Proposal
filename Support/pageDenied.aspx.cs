using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Support_pageDenied : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //'Master.pageTasks = "Restricted Resource Access"
                msgTypeResponse(getQueryStringIdx());
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
    }

    private string getQueryStringIdx()
    {
        string sRet = "";
        try
        {
            if (Request.QueryString["Msg"] != null)
            {
                sRet = Request.QueryString["Msg"];
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return sRet;
    }

    private void msgTypeResponse(string sRetCode)
    {
        try
        {
            lblMsg.Text = "User Sign-In was Denied, Contact your Administrator";
            lblHeader.Text = "Requested Access Denied";

            switch (sRetCode)
            {
                //case "eId", "":

                case "":
                    lblMsg.Text = "You do NOT have the Required Access to this Application";
                    lblHeader.Text = "Requested Access Denied";
                    break;

                case "eId":
                    lblMsg.Text = "You do NOT have the Required Access to this Application";
                    lblHeader.Text = "Requested Access Denied";
                    break;

                case "nId":
                    lblMsg.Text = "You do NOT have the Required Access to this Application";
                    //lblMsg.Text = "User Account Has been Removed from this Application. Please, Contact your Administrator";
                    lblHeader.Text = "Invalid Account Status";
                    break;

                case "lId":
                    lblMsg.Text = "User Account is Currently Locked from Accessing this Application. Please, Contact your Administrator";
                    lblHeader.Text = "Locked Account Status";
                    break;

                case "Err":
                    lblMsg.Text = "User Account Sign-In Failed. Please, Try Again Later";
                    lblHeader.Text = "Account Login Error";
                    break;

                case "nLg":
                    lblMsg.Text = "Click the Login Link to Access this Application";
                    lblHeader.Text = "User Login Required";
                    break;
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
    }
}