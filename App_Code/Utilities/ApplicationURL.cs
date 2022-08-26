using System.Web;


public class ApplicationURL
{
    private ApplicationURL()
    {

    }

    public static string MyAppURL()
    {
        string ServerURL = "";

        string httpHost = HttpContext.Current.Request.ServerVariables["http_host"].ToString();

        if (httpHost == AppConfiguration.SiteHostName) //Life server
        {
            ServerURL = "http://" + httpHost + "/EIP";
        }
        else if (httpHost == AppConfiguration.SiteDevelopmentEnvironment)         //Test Server
        {
            ServerURL = "http://" + httpHost + "/EIP";
        }
        else
        {
            ServerURL = "http://" + httpHost + "/EIP"; //Development PC
        }

        return ServerURL;
    }

    public static string MyAppURLWithoutEIP()
    {
        string ServerURL = "";
        string httpHost = HttpContext.Current.Request.ServerVariables["http_host"].ToString();

        if (httpHost == "sww.scin.cpdms.shell.com")
        {
            ServerURL = "http://" + httpHost;
        }
        else if (httpHost == "phc-v-01010")
        {
            ServerURL = "http://" + httpHost;
        }
        else
        {
            ServerURL = "http://" + httpHost;
        }

        return ServerURL;
    }

    public static string LiveLinkURLs()
    {
        string LiveLink = "http://sww-knowledge-epg.shell.com/knowtepg1/llisapi.dll?func=ll&objId=12093961&objAction=browse&sort=name";
        return LiveLink;
    }

    public static string eIPBehaviouralGuideLines()
    {
        string behaviouralGuid2 = "http://sww-knowledge-epg.shell.com/knowtepg1/livelink.exe?func=ll&objId=12096514&objAction=Open&viewType=1&nexturl=%2Fknowtepg1%2Flivelink%2Eexe%3Ffunc%3Dll%26objId%3D12093961%26objAction%3Dbrowse%26sort%3Dname";
        //string behaviouralGuide = "http://sww-knowledge-epg.shell.com/knowtepg1/llisapi.dll?func=ll&objId=12102069&objAction=Open&viewType=1&nexturl=%2Fknowtepg1%2Fllisapi%2Edll%3Ffunc%3Dll%26objId%3D12093961%26objAction%3Dbrowse%26sort%3Dname%26viewType%3D1";
        return behaviouralGuid2;
    }
}
