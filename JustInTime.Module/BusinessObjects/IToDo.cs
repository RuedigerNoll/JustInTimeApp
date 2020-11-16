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
    public interface IToDo
    {
        string Name { get; set; }

        DateTime Created { get; set; }

        IToDoStatus Status { get; set; }

        [FieldSize(1024)]
        string Memo { get; set; }
    }

    [DomainLogic(typeof(IToDo))]
    public class ToDoLogic
    {
        public void AfterConstruction(IToDo instance)
        {
            instance.Created = DateTime.Today;
        }

    }
}
