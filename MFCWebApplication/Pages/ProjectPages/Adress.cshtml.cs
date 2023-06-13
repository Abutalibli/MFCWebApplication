using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MFCWebApplication.Pages.ProjectPages
{
    public class AdressModel : PageModel
    {
        public List<AdressInfo> listAdress = new List<AdressInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-CN5RDCJ;Initial Catalog=MFCDB;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM FullAdress";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AdressInfo adressInfo = new AdressInfo();
                                adressInfo.id = "" + reader.GetInt32(0);
                                adressInfo.City = reader.GetString(1);
                                adressInfo.Street = reader.GetString(2);
                                adressInfo.HomeNumber = reader.GetString(3);
                                adressInfo.Apartment = reader.GetString(4);

                                listAdress.Add(adressInfo);
                            }
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Exeption: " + ex.ToString());
            }
        }
    }

    public class AdressInfo
    {
        public String id;
        public String City;
        public String Street;
        public String HomeNumber;
        public String Apartment;
    }
}
