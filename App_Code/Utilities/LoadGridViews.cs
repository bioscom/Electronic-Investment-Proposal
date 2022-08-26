using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for LoadGridViews
/// </summary>
public static class LoadGridViews
{
    static LoadGridViews()
    {
        
    }

    public static void LoadMyGridView(GridView grd, string sql, string SortExpression)
    {
        DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        if (dt.Rows.Count > 0)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = SortExpression;
            grd.DataSource = dv;
            grd.DataBind();
        }
        else
        {
            MessageBox.Show("No record found!");
        }
    }

    public static void LoadMyGridView(GridView grd, DataTable dt, string SortExpression)
    {
        if (dt.Rows.Count > 0)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = SortExpression;
            grd.DataSource = dv;
            grd.DataBind();
        }
        else
        {
            MessageBox.Show("No record found!");
        } 
    }

    public static void PageIndexChanging(GridView grdView, GridViewPageEventArgs e, string CurrentSortExpression)
    {
        grdView.PageIndex = e.NewPageIndex;
        DataSorter SortMe = new DataSorter();
    }
}