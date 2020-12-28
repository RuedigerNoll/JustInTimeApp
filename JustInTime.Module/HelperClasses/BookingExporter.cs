using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JustInTime.Module.BusinessObjects;
using DevExpress.ExpressApp;
using Newtonsoft.Json;

namespace JustInTime.Module.HelperClasses
{
    public static class BookingExporter
    {
        private static IObjectSpace _objectSpace;

        /// <summary>
        /// Erzeugt Liste von Buchungen mmit Start  und Endzeit, pro Tag nur eine Buchung
        /// </summary>
        /// <param name="objectSpace"></param>
        /// <param name="folderToSave"></param>
        /// <param name="bookings"></param>
        public static void ExportJdcBooking(IObjectSpace objectSpace, string folderToSave, IList<IBooking> bookings)
        {
            _objectSpace = objectSpace;

            var csvExport = new CsvExport(";");

            var groupedBookingList = from booking in bookings
                                     group booking by booking.Date into newBooking
                                     orderby newBooking.Key ascending
                                     select newBooking;

            var employee = bookings.FirstOrDefault()?.Employee;

            foreach (var item in groupedBookingList)
            {
                AddJdcBookingRow(csvExport,

                    new JdcBooking
                    {
                        Date = item.Key,
                        PersonnelNumber = employee.Number,
                        EmployeeName = $"{employee.Name.Split(' ').Last()}, {employee.Name.Split(' ').First()}",
                        StartTime = item.OrderBy(i => i.StartTime).FirstOrDefault().StartTime,
                        EndTime = item.OrderBy(i => i.StartTime).LastOrDefault().EndTime
                    }
                    );
            }
            csvExport.ExportToFile($@"{folderToSave}\book_csv_{bookings.FirstOrDefault().Date:yyyy_MM}_{Guid.NewGuid()}.csv");
        }

        /// <summary>
        /// Export der aller Buchungen, für die damalige RBA Zeiterfassung
        /// </summary>
        /// <param name="objectSpace"></param>
        /// <param name="folderToSave" />
        /// <param name="bookings"></param>
        public static void Export(IObjectSpace objectSpace, string folderToSave, IList<IBooking> bookings)
        {
            _objectSpace = objectSpace;

            var csvExport = new CsvExport("|");
            var counter = 1;

            var exportBookings = from b in bookings
                                 orderby b.Date ascending
                                 select b;

            foreach (var booking in exportBookings)
            {
                AddRow(csvExport, booking, counter);

                counter++;
            }

            csvExport.ExportToFile($@"{folderToSave}\book_csv_{Guid.NewGuid()}.csv");
        }

        /// <summary>
        /// Export für IOS App der Buchungen, Ausgabe Json Format
        /// </summary>
        /// <param name="objectSpace"></param>
        /// <param name="folderToSave"></param>
        /// <param name="bookings"></param>
        public static void ExportTimeStampClock(IObjectSpace objectSpace, string folderToSave, IList<IBooking> bookings)
        {
            _objectSpace = objectSpace;

            var exportBookings =
                from b in bookings
                orderby b.Date
                select new TimeStampClock
                {
                    StartDate = $"{b.Date.Add(b.StartTime):s}Z",
                    EndDate = $"{b.Date.Add(b.EndTime):s}Z",
                    Location = "Troisdorf",
                    Type = b.Project.Name == "URLAUB" ? "URLAUB" : "timer",
                    IsAutoEnd = false,
                    IsAutoStart = false,
                    Comment = b.TaskDescription
                };

            var json = JsonConvert
                .SerializeObject(exportBookings);

            File.WriteAllText(folderToSave, $"book_json_{bookings.FirstOrDefault()?.Date:yyy_MM}_{Guid.NewGuid()}.json");
        }

        private static void AddJdcBookingRow(CsvExport csvExport,
            JdcBooking booking)
        {
            csvExport.AddRow();
            csvExport["Datum"] = $"{booking.Date.ToShortDateString()}";
            csvExport["Personalnummer"] = $"{booking.PersonnelNumber}";
            csvExport["Name"] = $"{booking.EmployeeName}";
            csvExport["Kommt"] = $"{booking.StartTime}";
            csvExport["Geht"] = $"{booking.EndTime}";

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="csvExport"></param>
        /// <param name="booking"></param>
        /// <param name="counter" ></param>
        private static void AddRow(CsvExport csvExport, IBooking booking, int counter)
        {
            var timeDiff = booking.EndTime.Subtract(booking.StartTime);

            csvExport.AddRow();
            csvExport["UN_ID"] = $"{counter}";
            csvExport["COMP_ID"] = booking.Customer.ShortName;
            csvExport["USER_ID"] = booking.Employee.User.UserName;
            csvExport["DESC"] = booking.Task.Name;
            csvExport["DESC_MEMO"] = booking.TaskDescription;
            csvExport["NDESC"] = booking.Task.Number.ToString();
            csvExport["DAT"] = booking.Date.ToShortDateString();
            csvExport["NR_WEEK"] = string.Format("{0}", booking.Date.CalendarWeek());
            csvExport["T_START"] = booking.StartTime.ToString();
            csvExport["T_END"] = booking.EndTime.ToString();
            csvExport["T_S_FACT"] = "";
            csvExport["T_E_FACT"] = "";
            csvExport["CUSTOMER"] = string.Format("{0} {1}", booking.Customer.Number, booking.Customer.Name);
            csvExport["TIME_DIFF"] = string.Format("{0:D2}:{1:D2}:{2:D2}", timeDiff.Hours, timeDiff.Minutes, timeDiff.Seconds);
            csvExport["TIME_FACT"] = "";
            csvExport["PAYED"] = "FALSCH";
            csvExport["NSEC"] = timeDiff.TotalSeconds.ToString();
            csvExport["NSEC_FACT"] = "0";
            csvExport["KNR"] = booking.Customer.Number.ToString();
            csvExport["NPROJECT"] = booking.Project.Number.ToString();
            csvExport["DESCP"] = booking.Project.Name;
            csvExport["BOOKED"] = "FALSCH";
            csvExport["UNIT"] = "0";
            csvExport["PRICE"] = "{0}";
        }
    }
}
