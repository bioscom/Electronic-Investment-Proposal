using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Collections;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ListFiles
/// </summary>
public class ListFiles
{
    //private DirectoryInfo m_dir;

	public ListFiles()
	{
		
	}

    //public ListFiles(string sFolder)
    //{
    //    m_dir = new DirectoryInfo(sFolder);
    //}

    public void DeleteExistingFiles(string sFolder, Proposal oProposal)
    {
        DirectoryInfo m_dir = new DirectoryInfo(sFolder);
        foreach (FileInfo fInfo in m_dir.GetFiles(oProposal.m_sProj_Num + "*.*"))
        {
            fInfo.Delete();
        }
    }

    public ArrayList ShowFiles(string sFolder, Proposal oProposal)
    {
        DirectoryInfo m_dir = new DirectoryInfo(sFolder);
        ArrayList alFiles = new ArrayList();
        foreach (FileInfo fInfo in m_dir.GetFiles(oProposal.m_sProj_Num + "*.*"))
        {
            fInfo.Delete();
            string[] FileName = fInfo.Name.Split('#');
            alFiles.Add(fInfo.Name);
            string[] a = new string[] { fInfo.Name, FileName[0].ToString(), FileName[1].ToString(), oProposal.m_sProj_Title, oProposal.m_sDate_Init };
            alFiles.Add(a);
            //alFiles.Add(FileName[0]);
            //alFiles.Add(FileName[1]);
            //alFiles.Add(proposal.PROJ_TITLE);
            //alFiles.Add(proposal.DATE_INIT);
        }
        return alFiles;
    }
}
