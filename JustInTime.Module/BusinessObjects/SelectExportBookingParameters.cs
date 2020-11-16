using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.DC;
using JustInTime.Module.HelperClasses;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace JustInTime.Module.BusinessObjects
{
    [DomainComponent]
    public class SelectExportBookingParameters
    {
        IEmployee _employee;
        public IEmployee Employee 
        {
            get 
            {
                return _employee;
            }

            set 
            { 
                _employee = value;
            }
        }

        DateTime _exportStart = DateTime.Today.BeginningOfMonth();
        DateTime _exportEnd = DateTime.Today.EndOfMonth();

        public DateTime ExportStart 
        {
            get { return _exportStart; }

            set { _exportStart = value; } 
        }

        public DateTime ExportEnd
        {
            get { return _exportEnd; }

            set { _exportEnd = value; }
        }
    }
}
