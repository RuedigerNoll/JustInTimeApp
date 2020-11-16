using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Win.Core;
using JustInTime.Win.AppSplashScreen;

namespace JustInTime.Win
{
    public class JustInTimeSplashScreen : ISplash
    {
        static private JustInTimeSplashScreenForm form;
        //static private ApplicationSplashScreenForm form;
        private static bool isStarted = false;

        public void Start()
        {
            isStarted = true;
            form = new JustInTimeSplashScreenForm();
            //form = new ApplicationSplashScreenForm();
            form.Show();
            System.Windows.Forms.Application.DoEvents();
        }
        public void Stop()
        {
            if (form != null)
            {
                form.Hide();
                form.Close();
                form = null;
            }
            isStarted = false;
        }
        public void SetDisplayText(string displayText)
        {
        }
        public bool IsStarted
        {
            get { return isStarted; }
        }
    }
}
