using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MFCWebApplication.Pages.ProjectPages
{
    public class RegistrationModel : PageModel
    {
        public List<RegistrationInfo> listRegistration = new List<RegistrationInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-CN5RDCJ;Initial Catalog=MFCDB;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Registration";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RegistrationInfo registrationInfo = new RegistrationInfo();
                                registrationInfo.id = reader.GetInt32(0).ToString();
                                registrationInfo.FIO = reader.GetString(1);
                                registrationInfo.DateOfBirth = reader.GetDateTime(2).ToString();
                                registrationInfo.PlaceOfBirth = reader.GetInt32(3).ToString();
                                registrationInfo.FamilyStatus = reader.GetString(4);
                                registrationInfo.WhereDidComeFrom = reader.GetInt32(5).ToString();
                                registrationInfo.Passport = reader.GetInt32(6).ToString();
                                registrationInfo.PlaceOfDischarge = reader.GetInt32(7).ToString();
                                registrationInfo.RegistrationDate = reader.GetDateTime(8).ToString();
                                registrationInfo.DateOfDischarge = reader.GetDateTime(9).ToString();
                                registrationInfo.idWorker = reader.GetInt32(10).ToString();

                                listRegistration.Add(registrationInfo);
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
    public class RegistrationInfo
    {
        public String id;
        public String FIO;
        public String DateOfBirth;
        public String PlaceOfBirth;
        public String FamilyStatus;
        public String WhereDidComeFrom;
        public String Passport;
        public String PlaceOfDischarge;
        public String RegistrationDate;
        public String DateOfDischarge;
        public String idWorker;
    }
}
