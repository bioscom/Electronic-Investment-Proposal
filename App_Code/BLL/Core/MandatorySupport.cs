using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for MandatorySupport
/// </summary>
public class MandatorySupport
{
    private static string m_sMandatory;
    private static string m_sNotMandatory;

    private static int m_iMandatory;
    private static int m_iNotMandatoryDefault;

	static MandatorySupport()
	{
        m_sMandatory = "Mandatory";
        m_sNotMandatory = "Not Mandatory";

        m_iMandatory = 1;
        m_iNotMandatoryDefault = 0;
	}


    public static string Mandatory
    {
        get
        {
            return m_sMandatory;
        }
    }

    public static string NotMandatory
    {
        get
        {
            return m_sNotMandatory;
        }
    }



    public static int iMandatory
    {
        get
        {
            return m_iMandatory;
        }
    }

    public static int iNotMandatory
    {
        get
        {
            return m_iNotMandatoryDefault;
        }
    }
}
