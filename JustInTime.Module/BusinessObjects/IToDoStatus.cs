using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public interface IToDoStatus
    {
        string Name { get; set; }
    }
}
