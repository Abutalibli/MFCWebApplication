using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MFCWebApplication.Pages.ProjectPages
{
    public class CreatePassportModel : PageModel
    {
        public PassportInfo passportlInfo = new PassportInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            passportlInfo.TypeOfPassport = Request.Form["typeofpassport"];
            passportlInfo.IssuedBy = Request.Form["issuedby"];
            passportlInfo.PassNumber = Request.Form["passnumber"];

            if (passportlInfo.TypeOfPassport.Length == 0 || passportlInfo.IssuedBy.Length == 0 ||
                passportlInfo.PassNumber.Length == 0)
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
                    String sql = "INSERT INTO Passport " +
                        "(Type_of_passport, Issued_by, PassNumber) VALUES" +
                        "(@typeofpassport, @issuedby, @passnumber);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@typeofpassport", passportlInfo.TypeOfPassport);
                        command.Parameters.AddWithValue("@issuedby", passportlInfo.IssuedBy);
                        command.Parameters.AddWithValue("@passnumber", passportlInfo.PassNumber);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            passportlInfo.TypeOfPassport = ""; passportlInfo.IssuedBy = ""; passportlInfo.PassNumber = "";

        }
    }
}
