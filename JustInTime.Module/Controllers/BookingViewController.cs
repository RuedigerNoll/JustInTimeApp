using System;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using JustInTime.Module.BusinessObjects;
using JustInTime.Module.HelperClasses;

namespace JustInTime.Module.Controllers
{
    public partial class BookingViewController : ViewController
    {
        IObjectSpace _currentObjectSpace = null;
        public BookingViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetObjectType = typeof(IBooking);
            TargetViewType = ViewType.ListView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var editDate = DateTime.Today;
            if ((View.CurrentObject) != null)
                editDate = (View.CurrentObject as IBooking).Date;
                    
            //_currentObjectSpace = ObjectSpace.CreateNestedObjectSpace();
            _currentObjectSpace = Application.CreateObjectSpace();

            var currentUser = ObjectSpace.GetObject(SecuritySystem.CurrentUser);

            var currentEmployee = ObjectSpace.FindObject<IEmployee>(
                CriteriaOperator.Parse("User = ?", currentUser));

            var bookings = ObjectSpace.GetObjects<IBooking>(
                CriteriaOperator.Parse("Date = ? AND Employee = ?", 
                editDate, 
                currentEmployee));

            var booking = _currentObjectSpace.CreateObject<IBooking>();

            if (bookings != null && bookings.Count() > 0)
            {
                var lastEndTime = bookings.Max(b => b.EndTime);
                booking.StartTime = lastEndTime;
                booking.EndTime = lastEndTime.Add(new TimeSpan(1, 0, 0));
                booking.Date = editDate;
            }

            ShowBookingDialog(_currentObjectSpace, booking);
             
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectSpace"></param>
        /// <param name="booking"></param>
        private void ShowBookingDialog(IObjectSpace objectSpace, IBooking booking)
        {
            var detailViewBooking = Application.CreateDetailView(objectSpace, booking);

            ShowViewParameters parameters = new ShowViewParameters(detailViewBooking)
            {
                TargetWindow = TargetWindow.Default,
                Context = TemplateContext.PopupWindow
            };

            DialogController dialogController = new DialogController();
            dialogController.SaveOnAccept = true;
            
            dialogController.AcceptAction.Caption = CaptionHelper.GetLocalizedText("DialogButtons", "Ok", "Ok");
            dialogController.CancelAction.Caption = CaptionHelper.GetLocalizedText("DialogButtons", "Cancel", "Cancel");

            dialogController.AcceptAction.ActionMeaning = ActionMeaning.Accept;
            dialogController.CancelAction.ActionMeaning = ActionMeaning.Cancel;

            dialogController.CanCloseWindow = true;

            dialogController.Accepting +=new EventHandler<DialogControllerAcceptingEventArgs>(dialogController_Accepting);
            dialogController.Cancelling += new EventHandler(dialogController_Cancelling);    

            parameters.Controllers.Add(dialogController);
            Application.ShowViewStrategy.ShowView(parameters, new ShowViewSource(Frame, null));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dialogController_Cancelling(object sender, EventArgs e)
        {
            return;
        }

        /// <summary>
        /// Speichert die aktuelle Zeitbuchung und generiert mögliche Wiederholungen
        /// unter Berücksichtigung von Wochenenden, festen und beweglichen Feiertagen
        /// wenn die Option "Wochenende und Feiertag ignorieren" gewählt wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dialogController_Accepting(object sender, DialogControllerAcceptingEventArgs e)
        {
            var booking = e.AcceptActionArgs.CurrentObject as IBooking;

            int j = 0;
            DateTime nextDate = booking.Date;
                        
            NextFinancialDate(booking, ref j, ref nextDate);
            booking.Date = nextDate;

            if (booking.Repetition > 1)
            {
                j = 1;
                for (int i = 1; i < booking.Repetition; i++)
                {
                    var nextBooking = _currentObjectSpace.CreateObject<IBooking>();
                    nextBooking.Project = booking.Project;
                    nextBooking.StartTime = booking.StartTime;
                    nextBooking.Task = booking.Task;
                    nextBooking.TaskDescription = booking.TaskDescription;
                    nextBooking.Customer = booking.Customer;
                    nextBooking.Employee = booking.Employee;

                    nextDate = booking.Date.AddDays(j);

                    NextFinancialDate(booking, ref j, ref nextDate);

                    nextBooking.Date = nextDate;
                    nextBooking.EndTime = booking.EndTime;
                    j++;                    
                }

                _currentObjectSpace.CommitChanges();
                //ObjectSpace.CommitChanges();
                ObjectSpace.Refresh();

            }

            return;
        }

        /// <summary>
        /// Sucht den nächsten Werktag/Arbeitstag
        /// </summary>
        /// <param name="booking"></param>
        /// <param name="j"></param>
        /// <param name="nextDate"></param>
        private static void NextFinancialDate(IBooking booking, ref int j, ref DateTime nextDate)
        {
            if (!booking.IgnoreWeekendAndHolidays)
            {
                while (nextDate.IsWeekend() || nextDate.IsHoliday())
                {
                    if (nextDate.DayOfWeek == DayOfWeek.Saturday)
                        j = j + 2;
                    else
                        j = j + 1;

                    nextDate = booking.Date.AddDays(j);
                }
            }
        }

        private void CloneBooking_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var bookingToClone = e.CurrentObject as IBooking;

            if (bookingToClone == null)
                return;

            _currentObjectSpace = Application.CreateObjectSpace();

            var newBooking = _currentObjectSpace.CreateObject<IBooking>();
            bookingToClone = _currentObjectSpace.GetObject(bookingToClone);

            newBooking.Customer = bookingToClone.Customer;
            newBooking.Date = DateTime.Today;
            newBooking.Employee = bookingToClone.Employee;
            newBooking.EndTime = bookingToClone.EndTime.Add(new TimeSpan(1, 0,0));
            newBooking.IgnoreWeekendAndHolidays = bookingToClone.IgnoreWeekendAndHolidays;
            newBooking.Project = bookingToClone.Project;
            newBooking.Repetition = bookingToClone.Repetition;
            newBooking.StartTime = bookingToClone.EndTime;
            newBooking.Task = bookingToClone.Task;
            newBooking.TaskDescription = bookingToClone.TaskDescription;

            _currentObjectSpace.CommitChanges();
            ObjectSpace.Refresh();
        }
    }
}
