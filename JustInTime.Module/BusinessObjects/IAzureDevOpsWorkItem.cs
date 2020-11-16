using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace JustInTime.Module.BusinessObjects
{

    public enum WorkItemState
    {
        New,
        Approved,
        Active,
        Resolved,
        CodeReview,
        TestQa,
        Closed,
        Removed
    }

    [DomainComponent]
    [CreatableItemWithOptions(false, CreatableItemOptions.VisibleInEmbeddedListViews)]
    [NavigationItem("Konfiguration")]
    [DefaultObject("Name")]
    [ImageName("BO_Task")]
    [VisibleInReports(true)]
    public interface IAzureDevOpsWorkItem
    {
        string Number { get; set; }
        string ShortDescription { get; set; }
        string Description { get; set; }

        WorkItemState State { get; set; }

        [NonPersistentDc]
        string Name { get; set; }
    }

    [DomainLogic(typeof(IAzureDevOpsWorkItem))]
    public class AzureDevOpsWorkItemLogic
    {
        public void AfterConstruction(IAzureDevOpsWorkItem instance)
        {
        }
        
        public string Get_Name(IAzureDevOpsWorkItem instance)
        {
            return $"{instance.Number} - {instance.ShortDescription}";
        }

        public void Set_Name(IAzureDevOpsWorkItem instance, string value)
        {
            instance.Name = value;
        }
    }
}
