using System;
using System.Text;
using System.Web;

public partial class ViewProposal : CustomBasePage
{
    ViewProposals ViewProp = new ViewProposals();
    Proposal oProposal = new Proposal();
    ProposalMgt oProposalMgt = new ProposalMgt();

    protected void Page_Init(object sender, EventArgs e)
    {
        //Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        //Response.Expires = -1500;
        //Response.CacheControl = "no-cache";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Page_Init(sender, e);

        //Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        //Response.Expires = -1500;
        //Response.CacheControl = "no-cache";

        //Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Cache.SetNoStore();

        if (Request.QueryString["Proposalid"] != null)
        {
            long lProposalId = long.Parse(Request.QueryString["Proposalid"].ToString());
            oProposal = oProposalMgt.objGetProposalById(lProposalId);
            ViewProp.ViewProposalExt(oProposal.m_sProposalFileName);
	    ajaxWebExtension.showJscriptAlert(Page, this, "User Browser <== Back Button to go back to previous page!!!");
            //IPDetailInfo1.LoadProposalDetails(oProposal);
            //frame1.Controls.Clear();
            //frame1.Attributes["src"] = "Proposal.pdf";
        }
        else 
        {
            frame1.Controls.Clear();
            frame1.Attributes["src"] = null;
        }
        
        if (!IsPostBack)
        {
            string mssg = "Note: Please read. " +
                          "In case the expected Proposal is not displayed in the PDF viewer, " +
                          "Click Refresh button on the browser or press function key F5 on your keyboard.";

            ajaxWebExtension.showJscriptAlert(Page, this, mssg);
        }
        //Response.Redirect(Request.RawUrl);
    }
    //protected void refreshButton_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(Request.RawUrl.ToString());
    //}
}