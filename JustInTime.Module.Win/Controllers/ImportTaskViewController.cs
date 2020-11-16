using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System.IO;
using System.Linq;
using System;
using JustInTime.Module.BusinessObjects;
using System.Windows.Forms;

namespace JustInTime.Module.Win.Controllers
{
    public partial class ImportTaskViewController : ViewController
    {
        public ImportTaskViewController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            OpenFileDialog selectFile = new OpenFileDialog();
            selectFile.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            selectFile.InitialDirectory = @"D:\iCRM-Team-Development\Forschung\JustInTime";

            if (selectFile.ShowDialog() == DialogResult.OK)
            {
                string[] allLines = File.ReadAllLines(selectFile.FileName);

                var query = from line in allLines
                            //let data = line.Split(';')
                            where line != "DESC;NR"
                            select line;
                //select new 
                //{
                //    Desc = data[0],
                //    Nr = data[1]
                //};


                foreach (var item in query)
                {
                    var elements = item.Split(';');

                    if (elements.Count() > 1)
                    {
                        var task = ObjectSpace.CreateObject<ITask>();
                        task.Name = elements[0];
                        task.Number = Convert.ToInt32(elements[1]);
                    }
                }

                ObjectSpace.CommitChanges();
            }
        }
    }
}
