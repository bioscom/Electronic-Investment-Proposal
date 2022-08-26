using System;
using System.Web;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Net;

/// <summary>
/// Summary description for WriteFile
/// </summary>
/// 

public class WriteFile
{
    //int ArraySize = 0;

    public WriteFile()
    {
        
    }

    public byte[] DownLoadProposal(string xFileName)
    {
        byte[] buffer = new byte[0];
        try
        {
            string filePath = AppConfiguration.InvestmentProposalsFileLocation + xFileName;
            WebClient client = new WebClient();
            buffer = client.DownloadData(filePath);
            
        }
        catch (Exception ex)
        {
            MessageBox.Show("The proposal does not exist, please contact System Administrator.\n\n" + ex.Message.ToString());
        }
        return buffer;
    }

    //public string WriteData2(string xFileName)
    //{
    //    byte[] buffer = new byte[0];
    //    string SourcePath = AppConfiguration.InvestmentProposalsFileLocation + xFileName;
    //    string DestinationPath = HttpContext.Current.Server.MapPath("~/Proposal.pdf");

    //    FileStream fsRead = new FileStream(SourcePath, FileMode.Open, FileAccess.Read);
    //    buffer = fsRead.

    //    FileStream fsWrite = new FileStream(DestinationPath, FileMode.Create, FileAccess.Write);
    //    //This requires that Write access be granted to this application on the Host server.
    //    fsWrite.Write(buffer, 0, ArraySize);
    //    fsWrite.Close(); //close the stream
    //    fsWrite = null;
    //    //return MyPath;
    //}


    //public string WriteData2(string ProposalID)
    //{
    //   
    //    string sql = db.SelectALLsql(ProposalID);
    //    byte[] MyData = new byte[0];

    //    OracleConnection conn = new OracleConnection(AppConfiguration.DbConnectionString);
    //    OracleDataAdapter da = new OracleDataAdapter(sql, conn);
    //    OracleCommandBuilder MyCB = new OracleCommandBuilder(da);

    //    DataSet ds = new DataSet();
    //    da.Fill(ds, "theProposal");
    //    DataRow myRow = ds.Tables["theProposal"].Rows[0];

    //    if (myRow.IsNull("DIGISIGNEDIP")) { MyData = (byte[])myRow["PROPOSAL"]; }
    //    else if (!myRow.IsNull("DIGISIGNEDIP")) { MyData = (byte[])myRow["DIGISIGNEDIP"]; }

    //    ArraySize = new int();
    //    ArraySize = MyData.GetUpperBound(0);

    //    string MyPath = HttpContext.Current.Server.MapPath("~/DigitalSignedIP.pdf");

    //    //Write the Proposal from the database (BLOB) to the file System
    //    FileStream fsWrite = new FileStream(MyPath, FileMode.Create, FileAccess.Write);
    //    //This requires that Write access be granted to this application on the Host server.
    //    fsWrite.Write(MyData, 0, ArraySize);
    //    fsWrite.Close(); //close the stream
    //    fsWrite = null;

    //    closeClosables(MyCB, ds, da, conn);
    //    return MyPath;
    //}


    //public byte[] WriteData(string ProposalID)
    //{
    //   
    //    string sql = db.SelectALLsql(ProposalID);
    //    byte[] MyData = new byte[0];

    //    OracleConnection conn = new OracleConnection(AppConfiguration.DbConnectionString);
    //    OracleDataAdapter da = new OracleDataAdapter(sql, conn);
    //    OracleCommandBuilder MyCB = new OracleCommandBuilder(da);

    //    DataSet ds = new DataSet();
    //    da.Fill(ds, "theProposal");
    //    DataRow myRow = ds.Tables["theProposal"].Rows[0];

    //    if (myRow.IsNull("DIGISIGNEDIP")) { MyData = (byte[])myRow["PROPOSAL"]; }
    //    else if (!myRow.IsNull("DIGISIGNEDIP")) { MyData = (byte[])myRow["DIGISIGNEDIP"]; }

    //    closeClosables(MyCB, ds, da, conn);
    //    return MyData;
    //}

    //public void closeClosables(OracleCommandBuilder MyCB, DataSet ds, OracleDataAdapter da, OracleConnection conn)
    //{
    //    MyCB = null;
    //    ds = null;
    //    da = null;

    //    conn.Close();
    //    conn = null;
    //}  
}