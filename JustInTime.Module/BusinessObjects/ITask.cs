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
        string Name { get; set; }

        int Number { get; set; }
    }

    // To use a Domain Component in an XAF application, the Component should be registered.
    // Override the ModuleBase.Setup method in the application's module and invoke the ITypesInfo.RegisterEntity method in it:
    //
    // public override void Setup(XafApplication application) {
    //     XafTypesInfo.Instance.RegisterEntity("MyComponent", typeof(ITask));
    //     base.Setup(application);
    // }

    //[DomainLogic(typeof(ITask))]
    //public class ITaskLogic {
    //    public static string Get_CalculatedProperty(ITask instance) {
    //        // A "Get_" method is executed when getting a target property value. The target property should be readonly.
    //        // Use this method to implement calculated properties.
    //        return "";
    //    }
    //    public static void AfterChange_PersistentProperty(ITask instance) {
    //        // An "AfterChange_" method is executed after a target property is changed. The target property should not be readonly. 
    //        // Use this method to refresh dependant property values.
    //    }
    //    public static void AfterConstruction(ITask instance) {
    //        // The "AfterConstruction" method is executed only once, after an object is created. 
    //        // Use this method to initialize new objects with default property values.
    //    }
    //    public static int SumMethod(ITask instance, int val1, int val2) {
    //        // You can also define custom methods.
    //        return val1 + val2;
    //    }
    //}
}
