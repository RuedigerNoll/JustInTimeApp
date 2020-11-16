using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace JustInTime.Module.BusinessObjects
{
    [DomainComponent]
    [CreatableItemWithOptions(false, CreatableItemOptions.VisibleInEmbeddedListViews)]
    [NavigationItem("Konfiguration")]
    [ImageName("BO_Address")]
    [VisibleInReports(true)]
    public interface IAddress
    {
        string City { get; set; }
        string Zip { get; set; }
        string Street { get; set; }
        string StreetNumber { get; set; }
    }

    // To use a Domain Component in an XAF application, the Component should be registered.
    // Override the ModuleBase.Setup method in the application's module and invoke the ITypesInfo.RegisterEntity method in it:
    //
    // public override void Setup(XafApplication application) {
    //     XafTypesInfo.Instance.RegisterEntity("MyComponent", typeof(IAddress));
    //     base.Setup(application);
    // }

    //[DomainLogic(typeof(IAddress))]
    //public class IAddressLogic {
    //    public static string Get_CalculatedProperty(IAddress instance) {
    //        // A "Get_" method is executed when getting a target property value. The target property should be readonly.
    //        // Use this method to implement calculated properties.
    //        return "";
    //    }
    //    public static void AfterChange_PersistentProperty(IAddress instance) {
    //        // An "AfterChange_" method is executed after a target property is changed. The target property should not be readonly. 
    //        // Use this method to refresh dependant property values.
    //    }
    //    public static void AfterConstruction(IAddress instance) {
    //        // The "AfterConstruction" method is executed only once, after an object is created. 
    //        // Use this method to initialize new objects with default property values.
    //    }
    //    public static int SumMethod(IAddress instance, int val1, int val2) {
    //        // You can also define custom methods.
    //        return val1 + val2;
    //    }
    //}
}
