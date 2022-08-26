using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for appUsers
/// </summary>
public class appUsers
{
    public int m_iUserId { get; set; }
    public string m_sFullName { get; set; }
    public string m_sEmail { get; set; }
    public string m_sUserName { get; set; }
    public int m_iFunction { get; set; }
    public int m_iCompany { get; set; }
    public string m_sSysAdminExt { get; set; }
    public int m_iStatus { get; set; }
    public int m_iDefaultRoleHolder { get; set; }
    public int m_iUserRoleId { get; set; }
    public int m_iOULogic { get; set; }
    public int m_iIPLimitId { get; set; }
    public appUsersRoles.userRole m_eUserRole;

    public IPFunctions eFunction
    {
        get
        {
            IPFunctions oFunction = new IPFunctions();
            return oFunction.objGetIPFunctionByFunctionId(m_iFunction);
        }
    }

    public appUsers()
    {

    }

    public appUsers(DataRow dr)
    {
        try
        {
            m_iUserId = Convert.ToInt32(dr["IDUSERMGT"]);
            m_sFullName = dr["FULLNAME"].ToString();
            m_sEmail = dr["USERMAIL"].ToString();
            m_iUserRoleId = Convert.ToInt32(dr["USERROLESID"]);
            m_sUserName = dr["USERNAME"].ToString();
            m_iFunction = Convert.ToInt32(dr["FUNCTIONID"]);
            m_iCompany = Convert.ToInt32(dr["COMPANYID"]);
            m_sSysAdminExt = dr["SYSADMINEXT"].ToString();
            m_iStatus = Convert.ToInt32(dr["STATUS"].ToString());
            m_iDefaultRoleHolder = Convert.ToInt32(dr["FLAG_COLOR"].ToString()); //Note: this is used to keep the status of the current holder of certain roles that has deligates
            m_iOULogic = Convert.ToInt32(dr["EPNIGERIALOGIC"]);
            m_iIPLimitId = Convert.ToInt32(dr["IDIPLIMIT"]);
            m_eUserRole = (appUsersRoles.userRole)(int.Parse(dr["USERROLESID"].ToString()));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public structUserMailIdx structUserIdx
    {
        get
        {
            return new structUserMailIdx(m_sFullName, m_sEmail, m_sUserName);
        }
    }
}
