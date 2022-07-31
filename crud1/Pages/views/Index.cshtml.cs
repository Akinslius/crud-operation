using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace crud1.Pages.views
{
    public class IndexModel : PageModel
    {
        public List<StudentInfo> AllStudent = new List<StudentInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=student;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM form";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentInfo studentInfo = new StudentInfo();
                                studentInfo.id = "" + reader.GetInt32(0);
                                studentInfo.name = reader.GetString(1);
                                studentInfo.dob = reader.GetString(2);
                                studentInfo.gender = reader.GetString(3);
                                studentInfo.studentClass = reader.GetString(4);

                                AllStudent.Add(studentInfo);



                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.ToString());

            }
        }
    }

    public class StudentInfo
    {
        public string id;
        public string name;
        public string dob;
        public string gender;
        public string studentClass;

    }


}
