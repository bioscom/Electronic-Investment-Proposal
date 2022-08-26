using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Common_MyProposalInformation : aspnetPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IPInitiatorPendingProposals2.LoadMyPendingProposals(oSessnx.getOnlineUser);
        IPInitiatorApprovedProposals2.MyApprovedProposals(oSessnx.getOnlineUser);
    }
}
