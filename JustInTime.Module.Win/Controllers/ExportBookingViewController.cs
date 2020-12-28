using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.SystemModule;
using JustInTime.Module.HelperClasses;
using DevExpress.Data.Filtering;
using JustInTime.Module.BusinessObjects;
using DevExpress.ExpressApp.Utils;
using System.Windows.Forms;
using DevExpress.ExpressApp.Xpo;

namespace JustInTime.Module.Win.Controllers
{
    public partial class ExportBookingViewController : ViewController
    {
        private IObjectSpace _objectSpace;

        public ExportBookingViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetObjectType = typeof(IBooking);
        }


        /// <summary>
        /// Start Dialog to select export parameters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportEmployeeBookings_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _objectSpace = ObjectSpace.CreateNestedObjectSpace();

            var currentUser = _objectSpace.GetObject(SecuritySystem.CurrentUser);

            var dialog = new SelectExportBookingParameters();
            dialog.Employee = _objectSpace.FindObject<IEmployee>(CriteriaOperator.Parse("User = ?", currentUser));

            var detailView = Application.CreateDetailView(_objectSpace, dialog);

            Application.ShowViewStrategy.ShowViewInPopupWindow(detailView,
                () => 
                { 
                    Exporting(dialog); 
                    Application.ShowViewStrategy.ShowMessage("Daten wurden exportiert", InformationType.Success, 3000, InformationPosition.Right); 
                },
                () => { DialogCanceled(); },
                "Exportieren",
                "Abbrechen"
             );
        }

        private void Exporting(SelectExportBookingParameters selectedExportParameters)
        {
            if (selectedExportParameters == null)
                return;

            if (selectedExportParameters.Employee == null)
            {
                MessageBox.Show(CaptionHelper.GetLocalizedText("Texts", "NO_EMPLOYEE_SELECTED"),
                                CaptionHelper.GetLocalizedText("Texts", "Error"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var firstDayOfMonth = selectedExportParameters.ExportStart;
            var lastDayOfMonth = selectedExportParameters.ExportEnd;

            var bookingCriteria =
                GroupOperator.And(
                new BinaryOperator("Employee", selectedExportParameters.Employee),
                new BinaryOperator("Date", firstDayOfMonth, BinaryOperatorType.GreaterOrEqual),
            new BinaryOperator("Date", lastDayOfMonth, BinaryOperatorType.LessOrEqual));

            var bookings = _objectSpace.GetObjects<IBooking>(bookingCriteria);

            if (!bookings.Any())
                return;

            var saveFolder = SelectFolderToSaveBookingExport();
            if (saveFolder != null)
                BookingExporter.ExportJdcBooking(_objectSpace, saveFolder, bookings);
        }

        private void DialogCanceled()
        {
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string SelectFolderToSaveBookingExport()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder in which to save the bookung-export-file";
                dialog.ShowNewFolderButton = false;
                dialog.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.SelectedPath;
                }
            }

            return null;
        }
    }
}
