using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace JustInTime.Module.BusinessObjects
{
    [DomainComponent]
    [VisibleInReports(true)]
    public interface IProject
    {
        ICustomer CustomerRef { get; set; }

        bool Default { get; set; }

        int Number { get; set; }
        string Name { get; set; }
    }
}
