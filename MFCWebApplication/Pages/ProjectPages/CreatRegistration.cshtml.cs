using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MFCWebApplication.Pages.ProjectPages
{
    public class CreatRegistrationModel : PageModel
    {
        public RegistrationInfo registrationInfo = new RegistrationInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
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

            if (registrationInfo.FIO.Length == 0 || registrationInfo.DateOfBirth.Length == 0 ||
                registrationInfo.PlaceOfBirth.Length == 0 || registrationInfo.FamilyStatus.Length == 0 ||
                registrationInfo.WhereDidComeFrom.Length == 0 || registrationInfo.Passport.Length == 0 ||
                registrationInfo.PlaceOfDischarge.Length == 0 || registrationInfo.RegistrationDate.Length == 0 ||
                registrationInfo.DateOfDischarge.Length == 0 || registrationInfo.idWorker.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-CN5RDCJ;Initial Catalog=MFCDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Registration " +
                        "(FIO, Date_of_Birth, Place_of_Birth, Family_status, Where_did_come_from, Passport,Place_of_discharge, Registration_date, Date_of_discharge, idWorker) VALUES" +
                        "(@fio, @dateofbirth, @placeofbirth, @familystatus, @wheredidcomefrom, @passport, @placeofdischarge, @registrationdate, @dateofdischarge, @idworker);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
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

            //сохранение регистрации в базу
            registrationInfo.FIO = ""; registrationInfo.DateOfBirth = ""; registrationInfo.PlaceOfBirth = ""; registrationInfo.FamilyStatus = ""; registrationInfo.WhereDidComeFrom = ""; registrationInfo.Passport = ""; registrationInfo.PlaceOfDischarge = ""; registrationInfo.RegistrationDate = ""; registrationInfo.DateOfDischarge = ""; registrationInfo.idWorker = "";
            successMessage = "Регистрация добавлена";
        }
    }
}
