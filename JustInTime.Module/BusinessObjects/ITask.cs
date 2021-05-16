using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace JustInTime.Module.BusinessObjects
{
    [DomainComponent]
    [CreatableItemWithOptions(false, CreatableItemOptions.VisibleInEmbeddedListViews)]
    [NavigationItem("Konfiguration")]
    [DefaultObject("Name")]
    [ImageName("BO_Task")]
    [VisibleInReports(true)]
    public interface ITask
    {
        bool Default { get; set; }
        [FieldSize(1024)]
        string DefaultTaskDescription { get; set; }
        string Name { get; set; }

        int Number { get; set; }
    }
}
