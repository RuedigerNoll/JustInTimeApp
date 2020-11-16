using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;

namespace JustInTime.Module.BusinessObjects
{
    [DomainComponent]
    [CreatableItemWithOptions(false, CreatableItemOptions.VisibleInEmbeddedListViews)]
    [NavigationItem("Konfiguration")]
    [ImageName("BO_Customer")]
    [VisibleInReports(true)]
    public interface ICustomer
    {
        int Number { get; set; }
        string Name { get; set; }
        string ShortName { get; set; }

        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Always)]
        IAddress Address { get; set; }

        [Aggregated]
        [BackReferenceProperty("CustomerRef")]
        IList<IProject> Projects { get; }

        [BackReferenceProperty("RelatedCustomers")]
        IList<IEmployee> RelatedEmployees { get; }

        [Action(PredefinedCategory.Edit, Caption = "Personalize", TargetObjectsCriteria = "RelatedEmployees.Count = 0 OR RelatedEmployees[User.Oid != @CurrentUserId]")]
        void Personalize();
    }

    [DomainLogic(typeof(ICustomer))]
    public class CustomerLogic
    {
        public void AfterConstruction(ICustomer instance, IObjectSpace objectSpace)
        {
            instance.Address = objectSpace.CreateObject<IAddress>();
        }

        public void Personalize(ICustomer instance, IObjectSpace objectSpace)
        {
            var currentEmployee = objectSpace.FindObject<IEmployee>(
                CriteriaOperator.Parse("User.Oid = ?", SecuritySystem.CurrentUserId));

            instance.RelatedEmployees.Add(currentEmployee);
        }
    }

    // To use a Domain Component in an XAF application, the Component should be registered.
    // Override the ModuleBase.Setup method in the application's module and invoke the ITypesInfo.RegisterEntity method in it:
    //
    // public override void Setup(XafApplication application) {
    //     XafTypesInfo.Instance.RegisterEntity("MyComponent", typeof(ICustomer));
    //     base.Setup(application);
    // }

    //[DomainLogic(typeof(ICustomer))]
    //public class ICustomerLogic {
    //    public static string Get_CalculatedProperty(ICustomer instance) {
    //        // A "Get_" method is executed when getting a target property value. The target property should be readonly.
    //        // Use this method to implement calculated properties.
    //        return "";
    //    }
    //    public static void AfterChange_PersistentProperty(ICustomer instance) {
    //        // An "AfterChange_" method is executed after a target property is changed. The target property should not be readonly. 
    //        // Use this method to refresh dependant property values.
    //    }
    //    public static void AfterConstruction(ICustomer instance) {
    //        // The "AfterConstruction" method is executed only once, after an object is created. 
    //        // Use this method to initialize new objects with default property values.
    //    }
    //    public static int SumMethod(ICustomer instance, int val1, int val2) {
    //        // You can also define custom methods.
    //        return val1 + val2;
    //    }
    //}
}
