using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MFCWebApplication.Pages.ProjectPages
{
    public class WorkerModel : PageModel
    {
        public List<WorkerInfo> listWorker = new List<WorkerInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-CN5RDCJ;Initial Catalog=MFCDB;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Worker";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                WorkerInfo workerInfo = new WorkerInfo();
                                workerInfo.id = "" + reader.GetInt32(0);
                                workerInfo.FIO = reader.GetString(1);
                                workerInfo.DateOfBirth = reader.GetDateTime(2).ToString();
                                workerInfo.PhoneNumber = reader.GetString(3);

                                listWorker.Add(workerInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exeption: " + ex.ToString());
            }
        }
    }

    public class WorkerInfo
    {
        public String id;
        public String FIO;
        public String DateOfBirth;
        public String PhoneNumber;
    }
}
