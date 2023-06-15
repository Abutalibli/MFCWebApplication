using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MFCWebApplication.Pages.ProjectPages
{
    public class CreateAdressModel : PageModel
    {
        public AdressInfo adresslInfo = new AdressInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            adresslInfo.City = Request.Form["namecity"];
            adresslInfo.Street = Request.Form["namestreet"];
            adresslInfo.HomeNumber = Request.Form["homenumber"];
            adresslInfo.Apartment = Request.Form["apartment"];

            if (adresslInfo.City.Length == 0 || adresslInfo.Street.Length == 0 ||
                adresslInfo.HomeNumber.Length == 0 || adresslInfo.Apartment.Length == 0)
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
                    String sql = "INSERT INTO FullAdress " +
                        "(NameCity, NameStreet, HomeNumber, Apartment) VALUES" +
                        "(@namecity, @namestreet, @homenumber, @apartment);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@namecity", adresslInfo.City);
                        command.Parameters.AddWithValue("@namestreet", adresslInfo.Street);
                        command.Parameters.AddWithValue("@homenumber", adresslInfo.HomeNumber);
                        command.Parameters.AddWithValue("@apartment", adresslInfo.Apartment);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            adresslInfo.City = ""; adresslInfo.Street = ""; adresslInfo.HomeNumber = ""; adresslInfo.Apartment = "";

        }
    }
}

