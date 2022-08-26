using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for DiscontinueProposal
/// </summary>
public class DiscontinueProposal
{
    ProposalMgt oProposalMgt = new ProposalMgt();
    appUserMgt oAppUserMgt = new appUserMgt();

	public DiscontinueProposal()
	{
		
	}

    public bool DiscontinueProposals(long lProposalId, appUsers OnlineUser)
    {
        //When a proposal is discontinued, the DISCONTINUE field in the EIP_PROPOSAL table is set to 1, by default it is 0.
        // When this is set to 1, the query that filters pending proposals for all roles should be reset to AND DISCONTINUE <> '1'
        // Once discontinue is set to 1, no users will be able to see to see the proposal until it is UN-DISCONTINUED.
        bool success = false;
        try
        {
            success = oProposalMgt.DiscontinueProposal(lProposalId, IPStatus.iDiscontinued);
            if (success)
            {
                MailUsersProposalDiscontinued(lProposalId, OnlineUser);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return success;
    }

    public bool ReactivateProposal(long lProposalId, appUsers OnlineUser)
    {
        //When a proposal is Undiscontinued, the DISCONTINUE field in the EIP_PROPOSAL table is set back to 0, by default it is 0.
        // When this is set to 0, 
        // Once discontinue is set to 0, users who previously have the IP will be able to see the proposal and should receive an email that the proposal has been restored.
        bool success = false;
        try
        {
            success = oProposalMgt.DiscontinueProposal(lProposalId, IPStatus.iReactivate);
            if (success)
            {
                MailUsersProposalReactivated(lProposalId, OnlineUser);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return success;
    }

    public bool MailUsersProposalDiscontinued(long lProposalId, appUsers OnlineUser)
    {
        bool success = false;
        try
        {
            sendMail oSendMail = new sendMail(OnlineUser.structUserIdx);
            Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);
            appUsers oInitiator = oAppUserMgt.objGetUserByUserId(oProposal.m_iUserId);

            oSendMail.ProposalDiscontinued(oProposalMgt.getEmailAddressForIPLine(oProposal.m_lProposalId), oProposal.m_sProj_Title, oInitiator.m_sFullName, oProposal.m_sProj_Num);

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return success;
    }

    public bool MailUsersProposalReactivated(long lProposalId, appUsers OnlineUser)
    {
        bool success = false;
        try
        {
            sendMail oSendMail = new sendMail(OnlineUser.structUserIdx);
            Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);
            appUsers oInitiator = oAppUserMgt.objGetUserByUserId(oProposal.m_iUserId);

            oSendMail.ProposalReactivated(oProposalMgt.getEmailAddressForIPLine(oProposal.m_lProposalId), oProposal.m_sProj_Title, oInitiator.m_sFullName, oProposal.m_sProj_Num);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return success;
    }
}
