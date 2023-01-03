using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Drawing.Printing;
using System.Net;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using PGMEATS_WEB.Models;

namespace PGMEATS_WEB.Reports
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        ReportDocument rd = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        public void LoadReport()
        {
            PrinterSettings printerSettings = new PrinterSettings();

            var reportParam = (dynamic)HttpContext.Current.Session["ReportParam"];
            if (reportParam != null)
            {
                string directory = reportParam.RptFileName;
                string path = Server.MapPath("~") + "Reports\\Rpt\\" + reportParam.RptFileName;
                directory = Server.MapPath("~") + "Reports\\Rpt\\" + directory.Replace(".rpt", "") + "-" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff") + ".pdf";

                var dataSource = reportParam.DataSource;

                rd.Load(path);
                rd.SetDataSource(dataSource);

                CrystalReportViewer1.ReportSource = rd;
                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, directory);

                WebClient User = new WebClient();
                Byte[] FileBuffer = User.DownloadData(directory);
                if (FileBuffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    Response.BinaryWrite(FileBuffer);
                }

                System.IO.File.Delete(directory);

                if (rd != null)
                {
                    rd.Close();
                    rd.Dispose();
                    rd = null;
                }
                CrystalReportViewer1.Dispose();
                CrystalReportViewer1.ReportSource = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (rd != null)
            {
                rd.Close();
                rd.Dispose();
                rd = null;
            }
            CrystalReportViewer1.Dispose();
            CrystalReportViewer1.ReportSource = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
        {
            if (rd != null)
            {
                rd.Close();
                rd.Dispose();
                rd = null;
            }
            CrystalReportViewer1.Dispose();
            CrystalReportViewer1.ReportSource = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
}