using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

public static class AppConfiguration
{
    // Caches the connection string
    private static string dbConnectionString;
    // Caches the data provider name
    private static string dbProviderName;
    // Store the number of products per page
    private readonly static int productsPerPage;
    // Store the product description length for product lists
    private readonly static int productDescriptionLength;
    // Store the name of your shop
    private readonly static string siteName;
    private static string siteHostName;
    private static string DevelopmentEnvironment;
    private static string EIPInvestmentProposalDocuments;
    private static string informationWareHouse;

    private static string adminName;
    private static string adminEmail;
    private static string adminExt;

    private readonly static string smtpHost;
    private static bool bccAdmin;
    private static string appNameId;

    static AppConfiguration()
    {
        dbConnectionString = ConfigurationManager.ConnectionStrings["EIPConnectionString"].ConnectionString;
        dbProviderName = ConfigurationManager.ConnectionStrings["EIPConnectionString"].ProviderName;
        productsPerPage = System.Int32.Parse(ConfigurationManager.AppSettings["ProductsPerPage"]);
        productDescriptionLength = System.Int32.Parse(ConfigurationManager.AppSettings["ProductDescriptionLength"]);
        siteName = ConfigurationManager.AppSettings["SiteName"];
        siteHostName = ConfigurationManager.AppSettings["SiteHostName"];
        DevelopmentEnvironment = ConfigurationManager.AppSettings["DevelopmentEnvironment"];
        EIPInvestmentProposalDocuments = ConfigurationManager.AppSettings["EIPInvestmentProposalDocuments"];

        informationWareHouse = ConfigurationManager.AppSettings["IWH"];

        //TODO: Complete this
        adminName = ConfigurationManager.AppSettings["cpdms.AdminName"];
        adminEmail = ConfigurationManager.AppSettings["cpdms.AdminMail"];
        adminExt = ConfigurationManager.AppSettings["adminExt"];

        bccAdmin = bool.Parse(ConfigurationManager.AppSettings["cpdms.BccAdmin"]);
        smtpHost = ConfigurationManager.AppSettings["MailServer"];
        appNameId = ConfigurationManager.AppSettings["AppNameId"];
    }
    // Returns the connection string for the BalloonShop database
    public static string DbConnectionString
    {
        get
        {
            return dbConnectionString;
        }
    }
    // Returns the data provider name
    public static string DbProviderName
    {
        get
        {
            return dbProviderName;
        }
    }
    // Returns the address of the mail server
    public static string MailServer
    {
        get
        {
            return ConfigurationManager.AppSettings["MailServer"];
        }
    }
    // Returns the email username
    public static string MailUsername
    {
        get
        {
            return ConfigurationManager.AppSettings["MailUsername"];
        }
    }
    // Returns the email password
    public static string MailPassword
    {
        get
        {
            return ConfigurationManager.AppSettings["MailPassword"];
        }
    }
    // Returns the email password
    public static string MailFrom
    {
        get
        {
            return ConfigurationManager.AppSettings["MailFrom"];
        }
    }
    // Send error log emails?
    public static bool EnableErrorLogEmail
    {
        get
        {
            return bool.Parse(ConfigurationManager.AppSettings["EnableErrorLogEmail"]);
        }
    }
    // Returns the email address where to send error reports
    public static string ErrorLogEmail
    {
        get
        {
            return ConfigurationManager.AppSettings["ErrorLogEmail"];
        }
    }

    // Returns the maximum number of products to be displayed on a page
    public static int ProductsPerPage
    {
        get
        {
            return productsPerPage;
        }
    }
    // Returns the length of product descriptions in products lists
    public static int ProductDescriptionLength
    {
        get
        {
            return productDescriptionLength;
        }
    }
    // Returns the length of product descriptions in products lists
    public static string SiteName
    {
        get
        {
            return siteName;
        }
    }

    // Returns the Site Host Name 
    public static string SiteHostName
    {
        get
        {
            return siteHostName;
        }
    }

    // Returns the Development Environment

    public static string SiteDevelopmentEnvironment
    {
        get
        {
            return DevelopmentEnvironment;
        }
    }

    // Returns EIP Investment Proposal Documents location
    public static string InvestmentProposalsFileLocation
    {
        get
        {
            return EIPInvestmentProposalDocuments;
        }
    }

    public static string InformationWareHouse
    {
        get
        {
            return informationWareHouse;
        }
    }

    public static string AdminName
    {
        get { return adminName; }
    }

    public static string AdminEmail
    {
        get { return adminEmail; }
    }

    public static string AdminExt
    {
        get { return adminExt; }
    }

    public static string SmtpHost
    {
        get { return smtpHost; }
    }

    public static bool BccAdmin
    {
        get { return bccAdmin; }
    }

    public static string AppNameId
    {
        get { return appNameId; }
    }

}  

