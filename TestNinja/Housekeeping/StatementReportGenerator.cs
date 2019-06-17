using System;
using System.IO;

namespace TestNinja.Housekeeping
{
    public interface IStatementReportGenerator
    {
        string SaveStatement(int housekeeperOid, string housekeeperName, DateTime statementDate);
    }

    public class StatementReportGenerator : IStatementReportGenerator
    {
        public string SaveStatement(int housekeeperOid, string housekeeperName, DateTime statementDate)
        {
            var report = new HousekeeperStatementReport(housekeeperOid, statementDate);
            if (!report.HasData)
                return string.Empty;

            report.CreateDocument();

            var filename = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                string.Format("Sandpioper Statment {0:yyyy-MM} {1}.pdf", statementDate, housekeeperOid));

            report.ExportToPdf(filename);

            return filename;
        }
    }

    public class HousekeeperStatementReport
    {
        public bool HasData { get; set; }
        public HousekeeperStatementReport(int Oid, DateTime date) { }
        public void CreateDocument() { }
        public void ExportToPdf(string filename) { }
    }
}
