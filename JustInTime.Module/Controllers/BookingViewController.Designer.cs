namespace JustInTime.Module.Controllers
{
    partial class BookingViewController
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
            this.SaveBookingAsDraft = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.CreateBooking = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.CopyBooking = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.CloneBooking = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // SaveBookingAsDraft
            // 
            this.SaveBookingAsDraft.Caption = "Save Booking as Draft";
            this.SaveBookingAsDraft.Category = "Menu";
            this.SaveBookingAsDraft.ConfirmationMessage = null;
            this.SaveBookingAsDraft.Id = "SaveBookingAsDraftAction";
            this.SaveBookingAsDraft.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.SaveBookingAsDraft.TargetObjectType = typeof(JustInTime.Module.BusinessObjects.IBooking);
            this.SaveBookingAsDraft.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.SaveBookingAsDraft.ToolTip = null;
            this.SaveBookingAsDraft.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            // 
            // CreateBooking
            // 
            this.CreateBooking.Caption = "Time Booking";
            this.CreateBooking.ConfirmationMessage = null;
            this.CreateBooking.Id = "CreateBookingAction";
            this.CreateBooking.ImageName = "clock";
            this.CreateBooking.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.CreateBooking.TargetObjectType = typeof(JustInTime.Module.BusinessObjects.IBooking);
            this.CreateBooking.TargetViewId = "";
            this.CreateBooking.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.CreateBooking.ToolTip = null;
            this.CreateBooking.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.CreateBooking.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // CopyBooking
            // 
            this.CopyBooking.Caption = "Copy Booking";
            this.CopyBooking.ConfirmationMessage = null;
            this.CopyBooking.Id = "CopyBookingAction";
            this.CopyBooking.ImageName = "Action_Copy";
            this.CopyBooking.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.CopyBooking.TargetObjectType = typeof(JustInTime.Module.BusinessObjects.IBooking);
            this.CopyBooking.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.CopyBooking.ToolTip = "Dupliziert die ausgewählte Buchung mit dem aktuellen Datum";
            this.CopyBooking.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.CopyBooking.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CopyBooking_Execute);
            // 
            // CloneBooking
            // 
            this.CloneBooking.Caption = "Clone Booking";
            this.CloneBooking.ConfirmationMessage = null;
            this.CloneBooking.Id = "CloneBookingAction";
            this.CloneBooking.ImageName = "Action_CloneMerge_Clone_Object";
            this.CloneBooking.TargetObjectType = typeof(JustInTime.Module.BusinessObjects.IBooking);
            this.CloneBooking.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.CloneBooking.ToolTip = null;
            this.CloneBooking.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.CloneBooking.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CloneBooking_Execute);
            // 
            // BookingViewController
            // 
            this.Actions.Add(this.SaveBookingAsDraft);
            this.Actions.Add(this.CreateBooking);
            this.Actions.Add(this.CopyBooking);
            this.Actions.Add(this.CloneBooking);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction CreateBooking;
        private DevExpress.ExpressApp.Actions.SimpleAction CopyBooking;
        private DevExpress.ExpressApp.Actions.SimpleAction SaveBookingAsDraft;
        private DevExpress.ExpressApp.Actions.SimpleAction CloneBooking;
    }
}
