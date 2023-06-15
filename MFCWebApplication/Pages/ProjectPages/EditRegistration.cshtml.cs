using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MFCWebApplication.Pages.ProjectPages
{
    public class EditRegistrationModel : PageModel
    {
        public RegistrationInfo registrationInfo = new RegistrationInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=DESKTOP-CN5RDCJ;Initial Catalog=MFCDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Registration WHERE IdRegistration=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                registrationInfo.id = "" + reader.GetInt32(0);
                                registrationInfo.FIO = reader.GetString(1).ToString();
                                registrationInfo.DateOfBirth = reader.GetDateTime(2).ToString();
                                registrationInfo.PlaceOfBirth = reader.GetInt32(3).ToString();
                                registrationInfo.FamilyStatus = reader.GetDouble(4).ToString();
                                registrationInfo.WhereDidComeFrom = reader.GetInt32(5).ToString();
                                registrationInfo.Passport = reader.GetInt32(6).ToString();
                                registrationInfo.PlaceOfDischarge = reader.GetInt32(7).ToString();
                                registrationInfo.RegistrationDate = reader.GetDateTime(8).ToString();
                                registrationInfo.DateOfDischarge = reader.GetDateTime(9).ToString();
                                registrationInfo.idWorker = reader.GetInt32(10).ToString();
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void OnPost()
        {
            registrationInfo.id = Request.Form["id"];
            registrationInfo.FIO = Request.Form["fio"];
            registrationInfo.DateOfBirth = Request.Form["dateofbirth"];
            registrationInfo.PlaceOfBirth = Request.Form["placeofbirth"];
            registrationInfo.FamilyStatus = Request.Form["familystatus"];
            registrationInfo.WhereDidComeFrom = Request.Form["wheredidcomefrom"];
            registrationInfo.Passport = Request.Form["passport"];
            registrationInfo.PlaceOfDischarge = Request.Form["placeofdischarge"];
            registrationInfo.RegistrationDate = Request.Form["registrationdate"];
            registrationInfo.DateOfDischarge = Request.Form["dateofdischarge"];
            registrationInfo.idWorker = Request.Form["idworker"];

            if (registrationInfo.id.Length == 0 || registrationInfo.FIO.Length == 0 || registrationInfo.DateOfBirth.Length == 0 ||
                registrationInfo.PlaceOfBirth.Length == 0 || registrationInfo.FamilyStatus.Length == 0 ||
                registrationInfo.WhereDidComeFrom.Length == 0 || registrationInfo.Passport.Length == 0 || registrationInfo.PlaceOfDischarge.Length == 0 || registrationInfo.RegistrationDate.Length == 0
                || registrationInfo.DateOfDischarge.Length == 0 || registrationInfo.idWorker.Length == 0)
            {
                errorMessage = "Все поля должны быть заполнены";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-CN5RDCJ;Initial Catalog=MFCDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Registration " +
                        "SET FIO=@fio, Date_of_Birth=@dateofbirth, Place_of_Birth=@placeofbirth, Family_status=@familystatus, Where_did_come_from=@wheredidcomefrom,  Passport=@passport,  Place_of_discharge=@placeofdischarge,  Registration_date=@registrationdate, Date_of_discharge=@dateofdischarge, IdWorker=@idworker " +
                        "WHERE IdRegistration=@id ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", registrationInfo.id);
                        command.Parameters.AddWithValue("@fio", registrationInfo.FIO);
                        command.Parameters.AddWithValue("@dateofbirth", registrationInfo.DateOfBirth);
                        command.Parameters.AddWithValue("@placeofbirth", registrationInfo.PlaceOfBirth);
                        command.Parameters.AddWithValue("@familystatus", registrationInfo.FamilyStatus);
                        command.Parameters.AddWithValue("@wheredidcomefrom", registrationInfo.WhereDidComeFrom);
                        command.Parameters.AddWithValue("@passport", registrationInfo.Passport);
                        command.Parameters.AddWithValue("@placeofdischarge", registrationInfo.PlaceOfDischarge);
                        command.Parameters.AddWithValue("@registrationdate", registrationInfo.RegistrationDate);
                        command.Parameters.AddWithValue("@dateofdischarge", registrationInfo.DateOfDischarge);
                        command.Parameters.AddWithValue("@idworker", registrationInfo.idWorker);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


        }
    }
}

