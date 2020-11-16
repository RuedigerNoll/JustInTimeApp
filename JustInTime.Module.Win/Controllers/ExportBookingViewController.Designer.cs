namespace JustInTime.Module.Win.Controllers
{
    partial class ExportBookingViewController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.exportEmployeeBookings = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // exportEmployeeBookings
            // 
            this.exportEmployeeBookings.Caption = "CSV Export";
            this.exportEmployeeBookings.ConfirmationMessage = null;
            this.exportEmployeeBookings.Id = "7f5a05c8-a6e9-4275-8d80-c9c141ad181e";
            this.exportEmployeeBookings.ImageName = "BO_Security_Permission_Object";
            this.exportEmployeeBookings.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.exportEmployeeBookings.Shortcut = null;
            this.exportEmployeeBookings.Tag = null;
            this.exportEmployeeBookings.TargetObjectsCriteria = null;
            this.exportEmployeeBookings.TargetViewId = null;
            this.exportEmployeeBookings.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.exportEmployeeBookings.ToolTip = null;
            this.exportEmployeeBookings.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.exportEmployeeBookings.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.exportEmployeeBookings_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction exportEmployeeBookings;
    }
}
