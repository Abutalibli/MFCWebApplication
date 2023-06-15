using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MFCWebApplication.Pages.ProjectPages
{
    public class CreateWorkerModel : PageModel
    {
        public WorkerInfo workerlInfo = new WorkerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            workerlInfo.FIO = Request.Form["fio"];
            workerlInfo.DateOfBirth = Request.Form["dateofbirth"];
            workerlInfo.PhoneNumber = Request.Form["phonenumber"];

            if (workerlInfo.FIO.Length == 0 || workerlInfo.DateOfBirth.Length == 0 ||
                workerlInfo.PhoneNumber.Length == 0)
            {
                errorMessage = "Все поля должны быть заполнены";
                return;
            }

            //сохранение новой сделки в базу данных
            try
            {
                String connectionString = "Data Source=DESKTOP-CN5RDCJ;Initial Catalog=MFCDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Worker " +
                        "(FIO, DateOfBirth, PhoneNumber) VALUES" +
                        "(@fio, @dateofbirth, @phonenumber);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@fio", workerlInfo.FIO);
                        command.Parameters.AddWithValue("@dateofbirth", workerlInfo.DateOfBirth);
                        command.Parameters.AddWithValue("@phonenumber", workerlInfo.PhoneNumber);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            workerlInfo.FIO = ""; workerlInfo.DateOfBirth = ""; workerlInfo.PhoneNumber = "";

        }
    }
}
