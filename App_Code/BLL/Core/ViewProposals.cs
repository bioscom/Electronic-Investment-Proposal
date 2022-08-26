using System;
using System.Web;
using System.IO;
using System.Net;


/// <summary>
/// Summary description for ViewProposal
/// </summary>

public class ViewProposals
{
    public ViewProposals()
    {

    }

    public void ViewProposal(string ProposalFileName)
    {
        try
        {
            string SourceFilePath = HttpContext.Current.Server.MapPath("~/EIPIPDocuments/" + ProposalFileName);
            FileInfo SourceFile = new FileInfo(SourceFilePath);

            string DestinationPath = HttpContext.Current.Server.MapPath("~/Proposal.pdf");
            FileInfo destination = new FileInfo(DestinationPath);

            SourceFile.CopyTo(DestinationPath, true);
            destination.Refresh();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
            appMonitor.logAppExceptions(ex);
        }
    }

    public void ViewProposalExt(string ProposalFileName)
    {
        try
        {
            string FilePath = HttpContext.Current.Server.MapPath("~/EIPIPDocuments/" + ProposalFileName);
            WebClient o = new WebClient();

            Byte[] FileBuffer = o.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-length", FileBuffer.Length.ToString());
                HttpContext.Current.Response.BinaryWrite(FileBuffer);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
            appMonitor.logAppExceptions(ex);
        }
    }
}