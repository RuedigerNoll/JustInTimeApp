namespace JustInTime.Module.Win.Controllers
{
    partial class ImportTaskViewController
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
            this.ImportWinOfficeTasks = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // ImportWinOfficeTasks
            // 
            this.ImportWinOfficeTasks.Caption = "Import WinOffice Tasks";
            this.ImportWinOfficeTasks.Category = "Tools";
            this.ImportWinOfficeTasks.ConfirmationMessage = null;
            this.ImportWinOfficeTasks.Id = "21d95014-0f41-4dba-a7b6-8194430c899a";
            this.ImportWinOfficeTasks.ImageName = "Office-excel-csv-icon";
            this.ImportWinOfficeTasks.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.ImportWinOfficeTasks.Shortcut = null;
            this.ImportWinOfficeTasks.Tag = null;
            this.ImportWinOfficeTasks.TargetObjectsCriteria = null;
            this.ImportWinOfficeTasks.TargetViewId = null;
            this.ImportWinOfficeTasks.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.ImportWinOfficeTasks.ToolTip = null;
            this.ImportWinOfficeTasks.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.ImportWinOfficeTasks.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction ImportWinOfficeTasks;
    }
}
