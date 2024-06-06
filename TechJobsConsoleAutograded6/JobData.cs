using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TechJobsConsoleAutograded6
{
    public class JobData
    {
        static List<Dictionary<string, string>> AllJobs = new List<Dictionary<string, string>>();
        static bool IsDataLoaded = false;

        public static List<Dictionary<string, string>> FindAll()
        {
            LoadData();
            return AllJobs;
        }

        public static List<string> FindAll(string column)
        {
            LoadData();

            List<string> values = new List<string>();

            foreach (Dictionary<string, string> job in AllJobs)
            {
                string aValue = job[column];

                if (!values.Contains(aValue))
                {
                    values.Add(aValue);
                }
            }

            return values;
        }

        public static List<Dictionary<string, string>> FindByValue(string value)
        {
            LoadData();

            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>();
            HashSet<Dictionary<string, string>> uniqueJobs = new HashSet<Dictionary<string, string>>();

            if (value == null)
            {
                return jobs;
            }

            foreach (Dictionary<string, string> row in AllJobs)
            {
                foreach (KeyValuePair<string, string> field in row)
                {
                    if (field.Value != null && field.Value.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        uniqueJobs.Add(row);
                        break;
                    }
                }
            }

            jobs.AddRange(uniqueJobs);
            return jobs;
        }

        public static List<Dictionary<string, string>> FindByColumnAndValue(string column, string value)
        {
            LoadData();

            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>();

            if (column == null || value == null)
            {
                return jobs;
            }

            foreach (Dictionary<string, string> row in AllJobs)
            {
                if (row.ContainsKey(column))
                {
                    string aValue = row[column];

                    if (aValue != null && aValue.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        jobs.Add(row);
                    }
                }
            }

            return jobs;
        }

        private static void LoadData()
        {
            if (IsDataLoaded)
            {
                return;
            }

            List<string[]> rows = new List<string[]>();

            using (StreamReader reader = File.OpenText("job_data.csv"))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    string[] rowArrray = CSVRowToStringArray(line);
                    if (rowArrray.Length > 0)
                    {
                        rows.Add(rowArrray);
                    }
                }
            }

            string[] headers = rows[0];
            rows.Remove(headers);

            foreach (string[] row in rows)
            {
                Dictionary<string, string> rowDict = new Dictionary<string, string>();

                for (int i = 0; i < headers.Length; i++)
                {
                    rowDict.Add(headers[i], row[i]);
                }
                AllJobs.Add(rowDict);
            }

            IsDataLoaded = true;
        }

        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',', char stringSeparator = '\"')
        {
            bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

            foreach (char c in row.ToCharArray())
            {
                if (c == fieldSeparator && !isBetweenQuotes)
                {
                    rowValues.Add(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                else
                {
                    if (c == stringSeparator)
                    {
                        isBetweenQuotes = !isBetweenQuotes;
                    }
                    else
                    {
                        valueBuilder.Append(c);
                    }
                }
            }

            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();
        }
    }
}
