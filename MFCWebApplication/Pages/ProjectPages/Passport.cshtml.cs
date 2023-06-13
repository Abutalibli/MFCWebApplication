using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MFCWebApplication.Pages.ProjectPages
{
    public class PassportModel : PageModel
    {
        public List<PassportInfo> listPassport = new List<PassportInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-CN5RDCJ;Initial Catalog=MFCDB;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Passport";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PassportInfo passportInfo = new PassportInfo();
                                passportInfo.id = "" + reader.GetInt32(0);
                                passportInfo.TypeOfPassport = reader.GetString(1);
                                passportInfo.IssuedBy = reader.GetString(2);
                                passportInfo.PassNumber = reader.GetString(3);

                                listPassport.Add(passportInfo);
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
    public class PassportInfo
    {
        public string id;
        public string TypeOfPassport;
        public string IssuedBy;
        public string PassNumber;
    }
}
