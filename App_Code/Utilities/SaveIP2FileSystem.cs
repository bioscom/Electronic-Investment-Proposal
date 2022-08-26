using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

public class fileProperty
{
    public bool controlHasFile;
    public string fileType;
    public string sFileName;
    public bool bRet;

    public fileProperty()
    {
        controlHasFile = false;
        fileType = "";
        sFileName = "";
        bRet = false;
    }
}


/// <summary>
/// Summary description for SaveIP2FileSystem
/// </summary>
public class SaveIP2FileSystem
{
    public SaveIP2FileSystem()
    {
        
    }

    public fileProperty UploadInvestmentProposal(FileUpload ProposalLoader, string sProjectNumber, ref string sMessage)
    {
        TimeDateCulture ourCulture = new TimeDateCulture();

        fileProperty MyFileProperties = getDocType(ProposalLoader);

        if (MyFileProperties.controlHasFile)
        {
            if (MyFileProperties.fileType == "application/pdf")
            {
                //Delete Existing file(s) before saving the newly updated IP
                //ListFiles Myproposals = new ListFiles();
                //Myproposals.DeleteExistingFiles(AppConfiguration.InvestmentProposalsFileLocation, proposal);

                //Save a new IP
                MyFileProperties.sFileName = sProjectNumber + "#" + ourCulture.GetTodaysDateInBritishFormat().Replace("/", "-") + "#" + System.Guid.NewGuid().ToString() + ".pdf";
                //string SaveLocation = AppConfiguration.InvestmentProposalsFileLocation + MyFileProperties.sFileName;
                string SaveLocation2 = HttpContext.Current.Server.MapPath("~/EIPIPDocuments/" + MyFileProperties.sFileName);
                try
                {
                    ProposalLoader.PostedFile.SaveAs(SaveLocation2);
                    sMessage = "The file has been successfully uploaded.";
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
                }
            }
            else
            {
                sMessage = "The IP copy attached is not in PDF format. Please attach a PDF copy of your IP and try again.";
            }
        }
        else
        {
            sMessage = "You did not attach a copy of your IP. Please attach a copy and try again.";
        }

        return MyFileProperties;
    }

    private fileProperty getDocType(FileUpload UploadProposal)
    {
        fileProperty MyFile = new fileProperty();
        if (UploadProposal.HasFile)
        {
            string theProposal = UploadProposal.PostedFile.FileName;
            MyFile.fileType = UploadProposal.PostedFile.ContentType;
            MyFile.controlHasFile = true;

            if (MyFile.fileType == "application/pdf") MyFile.bRet = true;
            else MyFile.bRet = false;
        }
        return MyFile;
    }

    //public string UploadInvestmentProposal(FileUpload ProposalLoader, string sProjectNumber, ref string sMessage)
    //{
    //    TimeDateCulture ourCulture = new TimeDateCulture();
    //    string sFileName = "";
    //    bool fileFound = getDocType(ProposalLoader);

    //    try
    //    {
    //        if (fileFound == true)
    //        {
    //            if (docType == "application/pdf")
    //            {
    //                //Save a new IP
    //                sFileName = sProjectNumber + "#" + ourCulture.GetTodaysDateInBritishFormat().Replace("/", "-") + "#" + System.Guid.NewGuid().ToString() + ".pdf";
    //                //string SaveLocation = AppConfiguration.InvestmentProposalsFileLocation + sFileName;
    //                string SaveLocation2 = HttpContext.Current.Server.MapPath("~/EIPIPDocuments/" + sFileName);

    //                ProposalLoader.PostedFile.SaveAs(SaveLocation2);
    //                sMessage = "The file has been successfully uploaded.";
    //            }
    //            else
    //            {
    //                sMessage = "The IP copy attached is not in PDF format. Please attach a PDF copy of your IP and try again.";
    //            }
    //        }
    //        else
    //        {
    //            sMessage = "You did not attach a copy of your IP. Please attach a copy and try again.";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //    }

    //    return sFileName;
    //}

    //public string UploadInvestmentProposal(FileUpload ProposalLoader, string IPNumber)
    //{
    //    TimeDateCulture ourCulture = new TimeDateCulture();
    //    string sFileName = "";
    //    bool fileFound = getDocType(ProposalLoader);

    //    if (fileFound == true)
    //    {
    //        if (docType == "application/pdf")
    //        {
    //            //Save a new IP
    //            sFileName = IPNumber + "#" + ourCulture.GetTodaysDateInBritishFormat().Replace("/", "-") + "#" + System.Guid.NewGuid().ToString() + ".pdf";
    //            //string SaveLocation = AppConfiguration.InvestmentProposalsFileLocation + sFileName;
    //            string SaveLocation2 = HttpContext.Current.Server.MapPath("~/EIPIPDocuments/" + sFileName);
    //            try
    //            {
    //                ProposalLoader.PostedFile.SaveAs(SaveLocation2);
    //                //MessageBox.Show("The file has been uploaded.");
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //                //Note: Exception.Message returns a detailed message that describes the current exception. 
    //                //For security reasons, we do not recommend that you return Exception.Message to end users in 
    //                //production environments. It would be better to return a generic error message. 
    //            }
    //        }
    //        else
    //        {
    //            MessageBox.Show("The IP copy attached is not in PDF format. Please attach a PDF copy of your IP and try again.");
    //        }
    //    }
    //    else
    //    {
    //        MessageBox.Show("You did not attach a copy of your IP. Please attach a copy and try again.");
    //    }

    //    return sFileName;
    //}

    //public bool getDocType(FileUpload UploadProposal)
    //{
    //    bool fileFound = false;
    //    if (UploadProposal.HasFile)
    //    {
    //        string theProposal = UploadProposal.PostedFile.FileName;
    //        docType = UploadProposal.PostedFile.ContentType;
    //        fileFound = true;
    //    }
    //    else
    //    {
    //        fileFound = false;
    //        //"No file found!!!";
    //    }
    //    return fileFound;
    //}
}