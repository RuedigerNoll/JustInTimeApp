using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Mobile;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Internal;

//using DevExpress.ExpressApp.Security;

namespace JustInTime.Mobile
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/DevExpressExpressAppWinWinApplicationMembersTopicAll
    public partial class JustInTimeMobileApplication : MobileApplication
    {
        public JustInTimeMobileApplication()
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings["ConnectionString"];
            if (connectionStringSettings != null)
            {
                ConnectionString = connectionStringSettings.ConnectionString;
            }
            else if (string.IsNullOrEmpty(ConnectionString) && Connection == null)
            {
                connectionStringSettings = ConfigurationManager.ConnectionStrings["SqlExpressConnectionString"];
                if (connectionStringSettings != null)
                {
                    ConnectionString = DbEngineDetector.PatchConnectionString(connectionStringSettings.ConnectionString);
                }
            }
            InitializeComponent();
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {
            args.ObjectSpaceProvider = new XPObjectSpaceProvider(args.ConnectionString, args.Connection, true);
        }

//protected override void OnSetLogonParametersForUIBuilder() {
//    base.OnSetLogonParametersForUIBuilder();
//    // If your application requires authentication before building a mobile application UI, 
//    // create a test user in DatabaseUpdater and provide these test user credentials through SecuritySystem.LogonParameters here.
//    AuthenticationStandardLogonParameters logonParameters = (AuthenticationStandardLogonParameters)SecuritySystem.LogonParameters;
//    logonParameters.UserName = "Sam";
//    logonParameters.Password = "";
//}
        private void JustInTimeMobileApplication_CustomizeLanguagesList(object sender, CustomizeLanguagesListEventArgs e)
        {
            var userLanguageName = Thread.CurrentThread.CurrentUICulture.Name;
            if (userLanguageName != "en-US" && e.Languages.IndexOf(userLanguageName) == -1)
            {
                e.Languages.Add(userLanguageName);
            }
        }

        private void JustInTimeMobileApplication_DatabaseVersionMismatch(object sender,
            DatabaseVersionMismatchEventArgs e)
        {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if (Debugger.IsAttached)
            {
                e.Updater.Update();
                e.Handled = true;
            }
            else
            {
                throw new InvalidOperationException(
                    "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application.\r\n" +
                    "This error occurred  because the automatic database update was disabled when the application was started without debugging.\r\n" +
                    "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " +
                    "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " +
                    "or manually create a database using the 'DBUpdater' tool.\r\n" +
                    "Anyway, refer to the 'Update Application and Database Versions' help topic at http://help.devexpress.com/#Xaf/CustomDocument2795 " +
                    "for more detailed information. If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/");
            }
#endif
        }
    }
}