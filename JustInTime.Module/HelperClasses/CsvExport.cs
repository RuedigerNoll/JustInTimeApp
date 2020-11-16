using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlTypes;

namespace JustInTime.Module.HelperClasses
{
    /// <summary>
    /// Simple CSV export
    /// Example:
    ///   CsvExport myExport = new CsvExport();
    ///
    ///   myExport.AddRow();
    ///   myExport["Region"] = "New York, USA";
    ///   myExport["Sales"] = 100000;
    ///   myExport["Date Opened"] = new DateTime(2003, 12, 31);
    ///
    ///   myExport.AddRow();
    ///   myExport["Region"] = "Sydney \"in\" Australia";
    ///   myExport["Sales"] = 50000;
    ///   myExport["Date Opened"] = new DateTime(2005, 1, 1, 9, 30, 0);
    ///
    /// Then you can do any of the following three output options:
    ///   string myCsv = myExport.Export();
    ///   myExport.ExportToFile("Somefile.csv");
    ///   byte[] myCsvData = myExport.ExportToBytes();
    /// </summary>

    public class CsvExport
    {

        private string _delimiter;

        private string Delimiter
        {
            get { return _delimiter; }
            set { _delimiter = value; }
        }

        /// <summary>
        /// To keep the ordered list of column names
        /// </summary>
        List<string> fields = new List<string>();

        /// <summary>
        /// The list of rows
        /// </summary>
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        /// <summary>
        /// The current row
        /// </summary>
        Dictionary<string, object> currentRow { get { return rows[rows.Count - 1]; } }

        /// <summary>
        /// Standardkonstruktor, setzt den Delimiter auf Semikolon
        /// </summary>
        public CsvExport()
        {
            Delimiter = ";";
        }

        /// <summary>
        /// Zusätzlicher Konstruktor um den Delimiter zu überschreiben.
        /// </summary>
        /// <param name="delimeter"></param>
        public CsvExport(string delimiter)
        {
            Delimiter = delimiter;
        }

        /// <summary>
        /// Set a value on this column
        /// </summary>
        public object this[string field]
        {
            set
            {
                // Keep track of the field names, because the dictionary loses the ordering
                if (!fields.Contains(field)) fields.Add(field);
                currentRow[field] = value;
            }
        }

        /// <summary>
        /// Call this before setting any fields on a row
        /// </summary>
        public void AddRow()
        {
            rows.Add(new Dictionary<string, object>());
        }

        /// <summary>
        /// Converts a value to how it should output in a csv file
        /// If it has a comma, it needs surrounding with double quotes
        /// Eg Sydney, Australia -> "Sydney, Australia"
        /// Also if it contains any double quotes ("), then they need to be replaced with quad quotes[sic] ("")
        /// Eg "Dangerous Dan" McGrew -> """Dangerous Dan"" McGrew"
        /// </summary>
        string MakeValueCsvFriendly(object value)
        {
            if (value == null) return "";
            if (value is INullable && ((INullable)value).IsNull) return "";
            if (value is DateTime)
            {
                if (((DateTime)value).TimeOfDay.TotalSeconds == 0)
                    return ((DateTime)value).ToString("yyyy-MM-dd");
                return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string output = value.ToString();
            if (output.Contains(",") || output.Contains("\""))
                output = String.Format("\"{0}\"", output.Replace("\"", "\"\""));
            return output;
        }

        /// <summary>
        /// Output all rows as a CSV returning a string
        /// </summary>
        public string Export()
        {
            StringBuilder stringBuilder = new StringBuilder();

            // The header
            int counter = 0;
            foreach (string field in fields)
            {
                if (counter < fields.Count - 1)
                    stringBuilder.Append(field).Append(Delimiter);
                else
                    stringBuilder.Append(field);

                counter++;
            }

            stringBuilder.AppendLine();

            // The rows
            foreach (Dictionary<string, object> row in rows)
            {
                counter = 0;
                foreach (string field in fields)
                {
                    if (counter < fields.Count - 1)
                        stringBuilder.Append(MakeValueCsvFriendly(row[field])).Append(Delimiter);
                    else
                        stringBuilder.Append(MakeValueCsvFriendly(row[field]));

                    counter++;
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Exports to a file
        /// </summary>
        public void ExportToFile(string path)
        {
            File.WriteAllBytes(path, Encoding.UTF8.GetBytes(Export()));
            //File.WriteAllText(path, Export());
        }

        /// <summary>
        /// Exports as raw UTF8 bytes
        /// </summary>
        public byte[] ExportToBytes()
        {
            return Encoding.UTF8.GetBytes(Export());
        }
    }
}
