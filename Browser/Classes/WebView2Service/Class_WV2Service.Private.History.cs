using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Data.Sqlite;
using System.Reflection;
using System.Data;

namespace WV2Service
{
    public partial class WebViewService
    {
        // Data readers
        private DataTable GetDataTable(SqliteCommand command)
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (SqliteCommand cmd = command)
                {
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
                command.Dispose();
                return dataTable;
            }
            catch (Exception ex)
            {

            }
            command?.Dispose();
            return null;
        }
        private async Task<DataTable> GetHistoryDataTableAsync(SqliteCommand command)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ID", typeof(int));
                dataTable.Columns.Add("URL", typeof(string));
                dataTable.Columns.Add("Title", typeof(string));
                dataTable.Columns.Add("Visit Count", typeof(int));
                dataTable.Columns.Add("Last Visited", typeof(DateTime));
                using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32(0);
                        string url = reader.GetString(1);
                        string title = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        int visitCount = reader.GetInt32(3);
                        long lastVisitTime = reader.GetInt64(4);

                        // Convert Chromium timestamp to DateTime
                        DateTime lastVisitDate = DateTimeOffset.FromUnixTimeMilliseconds(lastVisitTime / 1000).DateTime;

                        // Convert to local time (based on system's time zone)
                        DateTime lastVisitLocal = lastVisitDate.ToLocalTime();

                        dataTable.Rows.Add(id, url, title, visitCount, lastVisitLocal);
                        //dataTable.Rows.Add(id, url, title, visitCount, lastVisitDate);
                    }
                }
                command.Dispose();
                return dataTable;
            }
            catch (SqliteException ex)
            {
                throw new SqliteException(ex.Message, ex.SqliteErrorCode);
            }
        }

        // SQL command builders
        private SqliteCommand SqliteCmd_GetHistory(SqliteConnection conn)
        {
            try
            {
                string query = @"
                    SELECT id, url, title, visit_count, last_visit_time 
                    FROM urls 
                    ORDER BY last_visit_time DESC";

                SqliteCommand command = new(query, conn);
                return command;
            }
            catch (SqliteException ex)
            {
                throw new SqliteException(ex.Message, ex.SqliteErrorCode);
            }
        }

        public async Task<DataTable> ReadHistory(string profileDirectory)
        {
            string historyFilePath = Path.Combine(profileDirectory, "EBWebView", "Default", "History");

            if (!File.Exists(historyFilePath))
            {
                Console.WriteLine("History file does not exist.");
                return null;
            }

            string tempFilePath = Path.Combine(Path.GetTempPath(), "database_temp.db");

            try
            {
                // Move the locked database to a temporary file
                File.Copy(historyFilePath, tempFilePath, overwrite: true);

                SqliteConnection conn;
                DataTable dataTable;
                using (conn = new SqliteConnection($"Data Source={tempFilePath};Mode=ReadOnly;Cache=Shared;"))
                {
                    conn.Open();
                    SqliteCommand command = SqliteCmd_GetHistory(conn);
                    dataTable = await GetHistoryDataTableAsync(command);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return null;
            }

            //SqliteConnection conn;
            //DataTable dataTable;
            //using (conn = new SqliteConnection($"Data Source={historyFilePath};Mode=ReadOnly;Cache=Shared;"))
            //{
            //    conn.Open();
            //    SqliteCommand command = SqliteCmd_GetHistory(conn);
            //    dataTable = await GetHistoryDataTableAsync(command);
            //}
            //conn.Close();
            //return dataTable;
        }
    }
}
