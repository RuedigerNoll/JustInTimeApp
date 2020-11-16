using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp;

namespace JustInTime.Module.BusinessObjects
{
    [DomainComponent]
    [CreatableItemWithOptions(false, CreatableItemOptions.VisibleInEmbeddedListViews)]
    //[NavigationItem("Zeiterfassung")]
    [VisibleInReports(true)]
    public interface IBooking
    {        
        [RuleRequiredField]
        DateTime Date { get; set; }
        
        [RuleRequiredField]
        TimeSpan StartTime { get; set; }
                
        [RuleRequiredField]
        TimeSpan EndTime { get; set; }

        [RuleRequiredField]
        [DevExpress.Xpo.Delayed]
        [DevExpress.Xpo.Custom("AllowEdit", "false")]
        IEmployee Employee { get; set; }

        [RuleRequiredField]
        [DevExpress.Xpo.Delayed]
        [DataSourceProperty("Employee.RelatedCustomers")]
        ICustomer Customer { get; set; }

        [RuleRequiredField]
        [DataSourceProperty("Customer.Projects")]
        [DevExpress.Xpo.Delayed]
        IProject Project { get; set; }

        [RuleRequiredField]
        [DevExpress.Xpo.Delayed]
        [ImmediatePostData]
        ITask Task { get; set; }

        [FieldSize(2048)]
        string TaskDescription { get; set; }

        IAzureDevOpsWorkItem WorkItem { get; set; }

        [NonPersistentDc]
        [VisibleInDetailView(false)]
        string YearMonth { get; }

        [NonPersistentDc]
        [VisibleInListView(false)]
        int Repetition { get; set; }

        [NonPersistentDc]
        [VisibleInDetailView(false)]
        TimeSpan TimeDifference { get; }

        [NonPersistentDc]
        [VisibleInListView(false)]
        bool IgnoreWeekendAndHolidays { get; set; }
    }

    [DomainLogic(typeof(IBooking))]
    public class BookingLogic
    {
        int repetition;
        bool ignoreWeekendAndHolidays;

        public void AfterConstruction(IBooking instance)
        {            
            var objectSpace = XPObjectSpace.FindObjectSpaceByObject(instance);

            if (!objectSpace.IsNewObject(instance)) return;

            var currentUser = objectSpace.GetObject(SecuritySystem.CurrentUser);
            var currentEmployee = objectSpace.FindObject<IEmployee>(
                CriteriaOperator.Parse("User = ?", currentUser));
            

            instance.Date = DateTime.Today;
            instance.Employee = currentEmployee;
            instance.Repetition = 1;
                        
            instance.StartTime = currentEmployee.DefaultStartTime;
            instance.EndTime = currentEmployee.DefaultEndTime;
            instance.Customer = currentEmployee.DefaultCustomer;

            if (instance.Customer != null)
            {
                var projects = from p in instance.Customer.Projects
                               where p.Default
                               select p;
                instance.Project = projects.FirstOrDefault();
            }

            instance.Task = objectSpace.FindObject<ITask>(CriteriaOperator.Parse("Default == true"));
        }

        public void BeforeChange_Task(IBooking instance, ITask value)
        {
            if (value == null)
                return;

            if (string.IsNullOrEmpty(instance.TaskDescription) || (instance.Task != null && instance.TaskDescription.Equals(instance.Task.Name)))
                instance.TaskDescription = value.Name;
        }

        public string Get_YearMonth(IBooking instance)
        {
            return string.Format("{0}-{1:D2}", instance.Date.Year, instance.Date.Month);
        }

        public int Get_Repetition(IBooking instance)
        {
            return repetition;
        }

        public void Set_Repetition(IBooking instance, int value)
        {
            repetition = value;
        }

        public TimeSpan Get_TimeDifference(IBooking instance)
        {
            return instance.EndTime.Subtract(instance.StartTime);
        }

        public bool Get_IgnoreWeekendAndHolidays(IBooking instance)
        {
            return ignoreWeekendAndHolidays;
        }

        public void Set_IgnoreWeekendAndHolidays(IBooking instance, bool value)
        {
            ignoreWeekendAndHolidays = value;
        }
    }
}
