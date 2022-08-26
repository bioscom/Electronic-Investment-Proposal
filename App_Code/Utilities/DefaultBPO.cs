using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DefaultBPO
/// </summary>
public static class DefaultRoleHolder
{
    private static int m_iDefault;
    private static int m_iNoneDefault;

    static DefaultRoleHolder()
    {
        m_iDefault = 1;
        m_iNoneDefault = 0;
    }

    public static int iDefault
    {
        get
        {
            return m_iDefault;
        }
    }

    public static int iNoneDefault
    {
        get
        {
            return m_iNoneDefault;
        }
    }
}