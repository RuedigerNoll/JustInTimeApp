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
        /// 
        /// </summary>
        private void ShowSelectEmployeeDialog()
        {
            _objectSpace = ObjectSpace.CreateNestedObjectSpace();

            var detailView = Application.CreateDetailView(_objectSpace, new SelectExportBookingParameters());

            var parameters = new ShowViewParameters(detailView)
            {
                TargetWindow = TargetWindow.NewModalWindow,
                Context = TemplateContext.PopupWindow
            };

            var dialogController = new DialogController {SaveOnAccept = false};

            dialogController.AcceptAction.Caption = CaptionHelper.GetLocalizedText("DialogButtons", "Ok", "Ok");
            dialogController.CancelAction.Caption = CaptionHelper.GetLocalizedText("DialogButtons", "Cancel", "Cancel");

            dialogController.AcceptAction.ActionMeaning = ActionMeaning.Accept;
            dialogController.CancelAction.ActionMeaning = ActionMeaning.Cancel;

            dialogController.CanCloseWindow = true;

            dialogController.Accepting += dialogController_Accepting;
            dialogController.Cancelling += dialogController_Cancelling;

            parameters.Controllers.Add(dialogController);
            Application.ShowViewStrategy.ShowView(parameters, new ShowViewSource(Frame, null));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dialogController_Cancelling(object sender, EventArgs e)
        {
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dialogController_Accepting(object sender, DialogControllerAcceptingEventArgs e)
        {
            var dialogController = sender as DialogController;

            if (dialogController == null)
                return;

            var selectedExportParameters = dialogController.Window.View.CurrentObject as SelectExportBookingParameters;

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
            //if (saveFolder != null)
            //    BookingExporter.Export(_objectSpace, saveFolder, bookings);

            if (saveFolder != null)
                BookingExporter.ExportTimeStampClock(_objectSpace, Path.GetTempPath(), bookings);

        }

        private void exportEmployeeBookings_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ShowSelectEmployeeDialog();
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
