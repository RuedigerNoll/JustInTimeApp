using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using JustInTime.Module.BusinessObjects;
using DevExpress.Persistent.Base.General;


namespace JustInTime.Module
{
    public sealed partial class JustInTimeModule : ModuleBase
    {
        public JustInTimeModule()
        {
            InitializeComponent();
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }

        public override void Setup(XafApplication application)
        {
            XafTypesInfo.Instance.RegisterEntity("Employee", typeof(IEmployee));
            XafTypesInfo.Instance.RegisterEntity("Customer", typeof(ICustomer));
            XafTypesInfo.Instance.RegisterEntity("Project", typeof(IProject));
            XafTypesInfo.Instance.RegisterEntity("Address", typeof(JustInTime.Module.BusinessObjects.IAddress));
            XafTypesInfo.Instance.RegisterEntity("Task", typeof(JustInTime.Module.BusinessObjects.ITask));
            XafTypesInfo.Instance.RegisterEntity("Todo", typeof(JustInTime.Module.BusinessObjects.IToDo));
            XafTypesInfo.Instance.RegisterEntity("TodoStatus", typeof(JustInTime.Module.BusinessObjects.IToDoStatus));
            XafTypesInfo.Instance.RegisterEntity("AzureDevOpsWorkItem", typeof(IAzureDevOpsWorkItem));
            XafTypesInfo.Instance.RegisterEntity("Booking", typeof(IBooking));

            base.Setup(application);
        }
    }
}
