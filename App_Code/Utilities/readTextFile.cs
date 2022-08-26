using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for readTextFile
/// </summary>
public class readTextFile
{
	public readTextFile()
	{
		
	}

    public string readMyTextFile(string sFilePath)
    {
        string sRet = "";
        try
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(sFilePath)) 
            {
                string line;
                while ((line = sr.ReadLine()) != null) 
                {
                    sRet = sRet + line;
                }
            }
        }
        catch(Exception ex)
        {
            //System.Diagnostics.Debug.Fail(ex.TargetSite.ToString() + ex.Message);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return sRet;
    }
}
