<%@ Application Language="C#" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Data" %>

<script runat="server">
   
    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup 
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        //Code that runs when an unhandled error occurs
        //Server.Transfer("~/AppEvent.aspx");
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        Session["SLA"] = 0;
        Session["SortExpression"] = "";
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Server.Transfer("~/Index.aspx");
    }
       
</script>

