using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security.Strategy;

namespace JustInTime.Module.BusinessObjects
{
    [DomainComponent]
    [CreatableItemWithOptions(false, CreatableItemOptions.VisibleInEmbeddedListViews)]
    [NavigationItem("Konfiguration")]
    [ImageName("BO_Employee")]
    [VisibleInReports(true)]
    public interface IEmployee
    {
        string Name { get; set; }
        SecuritySystemUser User { get; set; }

        [DataSourceProperty("RelatedCustomers")]
        ICustomer DefaultCustomer { get; set; }

        [BackReferenceProperty("RelatedEmployees")]
        IList<ICustomer> RelatedCustomers { get; }

        TimeSpan DefaultStartTime { get; set; }

        TimeSpan DefaultEndTime { get; set; }


        int Number { get; set; }
    }

    [DomainLogic(typeof(IEmployee))]
    public class EmployeeLogic
    {
        public void AfterConstruction(IEmployee instance)
        {
            instance.DefaultStartTime = new TimeSpan(9, 0, 0);
            instance.DefaultEndTime = new TimeSpan(17, 0, 0);
        }
    }

    // To use a Domain Component in an XAF application, the Component should be registered.
    // Override the ModuleBase.Setup method in the application's module and invoke the ITypesInfo.RegisterEntity method in it:
    //
    // public override void Setup(XafApplication application) {
    //     XafTypesInfo.Instance.RegisterEntity("MyComponent", typeof(IEmployee));
    //     base.Setup(application);
    // }

    //[DomainLogic(typeof(IEmployee))]
    //public class IEmployeeLogic {
    //    public static string Get_CalculatedProperty(IEmployee instance) {
    //        // A "Get_" method is executed when getting a target property value. The target property should be readonly.
    //        // Use this method to implement calculated properties.
    //        return "";
    //    }
    //    public static void AfterChange_PersistentProperty(IEmployee instance) {
    //        // An "AfterChange_" method is executed after a target property is changed. The target property should not be readonly. 
    //        // Use this method to refresh dependant property values.
    //    }
    //    public static void AfterConstruction(IEmployee instance) {
    //        // The "AfterConstruction" method is executed only once, after an object is created. 
    //        // Use this method to initialize new objects with default property values.
    //    }
    //    public static int SumMethod(IEmployee instance, int val1, int val2) {
    //        // You can also define custom methods.
    //        return val1 + val2;
    //    }
    //}
}
