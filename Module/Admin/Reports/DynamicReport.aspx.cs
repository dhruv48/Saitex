using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Module_Admin_Reports_DynamicReport : System.Web.UI.Page
{
    ReportDocument rDoc = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            rDoc.Load(Server.MapPath(@"DynamicReport.rpt"));

            string sql = procesSQL();

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(sql, "", "", "", "", "");

            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
            CrystalReportViewer1.RefreshReport();
        }
    }

    public string procesSQL()
    {
        string sql = null;
        string inSql = null;
        string firstPart = null;
        string lastPart = null;
        int selectStart = 0;
        int fromStart = 0;
        string[] fields = null;
        string[] sep = { "," };
        int i = 0;
        TextObject MyText;

        inSql = TextBox1.Text;
        inSql = inSql.ToUpper();

        selectStart = inSql.IndexOf("SELECT");
        fromStart = inSql.IndexOf("FROM");
        selectStart = selectStart + 6;
        firstPart = inSql.Substring(selectStart, (fromStart - selectStart));
        lastPart = inSql.Substring(fromStart, inSql.Length - fromStart);

        fields = firstPart.Split(',');
        firstPart = "";
        for (i = 0; i < fields.Length - 1; i++)
        {
            if (i > 0)
            {
                firstPart = firstPart + ", " + fields[i].ToString() + " AS COLUMN" + (i + 1);
                firstPart.Trim();

                MyText = (TextObject)rDoc.ReportDefinition.ReportObjects[i + 1];
                MyText.Text = fields[i].ToString();
            }
            else
            {
                firstPart = firstPart + fields[i].ToString() + " AS COLUMN" + (i + 1);
                firstPart.Trim();

                MyText = (TextObject)rDoc.ReportDefinition.ReportObjects[i + 1];
                MyText.Text = fields[i].ToString();
            }
        }
        sql = "SELECT " + firstPart + " " + lastPart;
        return sql;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        rDoc.Load(Server.MapPath(@"DynamicReport.rpt"));

        string sql = procesSQL();

        DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(sql, "", "", "", "", "");

        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
        CrystalReportViewer1.RefreshReport();
    }
}
