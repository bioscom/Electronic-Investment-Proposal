using System;
using System.Data;

public class Proposal
{
    public long m_lProposalId { get; set; }
    public int m_iFunctionId { get; set; }
    public int m_iDoc_Stand { get; set; }
    public int m_iBOM { get; set; }
    public int m_iCcompanyId { get; set; }
    public string m_sStatus { get; set; }
    public int m_iUserId { get; set; }
    public int m_iEppriorityId { get; set; }
    public string m_sIPOriginatingUnit { get; set; }
    public int m_iDiscontinue { get; set; }
    public int m_iNSACOUNTER { get; set; }
    public decimal m_lJV { get; set; }
    public decimal m_lSS { get; set; }
    public string m_sProj_Num { get; set; }
    public string m_sProj_Title { get; set; }
    public string m_sPROJ_INIT { get; set; }
    public string m_sDate_Init { get; set; }
    public string m_sDate_Submit { get; set; }
    public string m_sProj_Desc { get; set; }
    public string m_sDateActioned { get; set; }
    public string m_sProposalFileName { get; set; }

    public Proposal()
    {

    }

    public Proposal(DataRow dr)
    {
        try
        {
            m_lProposalId = long.Parse(dr["IDPROPOSAL"].ToString());
            m_sProj_Num = dr["PROJ_NUM"].ToString();
            m_sPROJ_INIT = dr["PROJ_INIT"].ToString();
            m_sIPOriginatingUnit = m_sProj_Num.Substring(0, 4);
            m_sProj_Title = dr["PROJ_TITLE"].ToString();
            m_sDate_Init = dr["DATE_INIT"].ToString();
            m_lJV = decimal.Parse(dr["JV"].ToString());
            m_lSS = decimal.Parse(dr["SS"].ToString());
            m_sDate_Submit = dr["DATE_SUBMIT"].ToString();
            m_iDoc_Stand = int.Parse(dr["DOC_STAND"].ToString());
            m_sStatus = dr["STATUS"].ToString();
            m_iUserId = int.Parse(dr["IDUSERMGT"].ToString());
            m_iNSACOUNTER = int.Parse(dr["NSACOUNTER"].ToString());
            m_sProj_Desc = dr["PROJ_DESC"].ToString();
            m_iEppriorityId = int.Parse(dr["EPPRIORITYID"].ToString());
            m_sDateActioned = dr["DATE_LAST_ACTIONED"].ToString();
            m_sProposalFileName = dr["PROPOSALFILENAME"].ToString();
            m_iDiscontinue = int.Parse(dr["DISCONTINUE"].ToString());
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message.ToString());
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
}