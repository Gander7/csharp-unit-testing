using System;

namespace TestNinja.Housekeeping
{
    public class HouseKeeperHelper
    {
        private readonly IHouseKeeperRepository _storage;
        private readonly IStatementReportGenerator _generator;
        private readonly IEmailHelper _emailHelper;
        private readonly IXtraMessageBox _messageBox;

        public HouseKeeperHelper(IHouseKeeperRepository storage = null,
                                 IStatementReportGenerator generator = null, 
                                 IEmailHelper emailHelper = null,
                                 IXtraMessageBox messageBox = null)
        {
            _storage = storage ?? new HouseKeeperRepository();
            _generator = generator ?? new StatementReportGenerator();
            _emailHelper = emailHelper ?? new EmailHelper();
            _messageBox = messageBox ?? new XtraMessageBox();
        }

        public bool SendStatementEmails(DateTime statementDate)
        {
            var housekeepers = _storage.GetHousekeepers();

            foreach (var housekeeper in housekeepers)
            {
                if (housekeeper.Email == null)
                    continue;

                var statementFilename = _generator.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate);

                if (string.IsNullOrWhiteSpace(statementFilename))
                    continue;

                var emailAddress = housekeeper.Email;
                var emailBody = housekeeper.StatementEmailBody;

                try
                {
                    _emailHelper.EmailFile(emailAddress, emailBody, statementFilename,
                        string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, housekeeper.Oid));
                }
                catch (Exception e)
                {
                    _messageBox.Show(e.Message, string.Format("Email failure: {0}", emailAddress),
                        MessageBoxButtons.OK);
                }
            }
            return true;
        }
    }

    public enum MessageBoxButtons
    {
        OK
    }

    public interface IXtraMessageBox
    {
        void Show(string s, string housekeeperStatements, MessageBoxButtons ok);
    }

    public class XtraMessageBox : IXtraMessageBox
    {
        public void Show(string s, string housekeeperStatements, MessageBoxButtons ok)
        {
        }
    }

    public class MainForm
    {
        public bool HousekeeperStatementsSending { get; set; }
    }

    public class SystemSettingsHelper
    {
        public static string EmailSmtpHost { get; set; }
        public static int EmailPort { get; set; }
        public static string EmailUsername { get; set; }
        public static string EmailPassword { get; set; }
        public static string EmailFromEmail { get; set; }
        public static string EmailFromName { get; set; }
    }

    public class Housekeeper
    {
        public string Email { get; set; }
        public int Oid { get; set; }
        public string FullName { get; set; }
        public string StatementEmailBody { get; set; }
    }
}
