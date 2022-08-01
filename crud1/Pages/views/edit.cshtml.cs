using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace crud1.Pages.views
{
    public class editModel : PageModel
    {
       public StudentInfo studentInfo = new StudentInfo();

        public string errorMessage = " ";
        public string successMessage = " ";
        public void OnGet()
        {
            string id = Request.Query["id"];
            

            if (id == null)
            {
                Console.WriteLine("id is null");
            }
            else
            {
                try
                {
                    string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=student;Integrated Security=True";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "SELECT * FROM form WHERE id= " + id;
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    studentInfo.id = "" + reader.GetInt32(0);
                                    studentInfo.name = reader.GetString(1);
                                    studentInfo.dob = reader.GetString(2);
                                    studentInfo.gender = reader.GetString(3);
                                    studentInfo.studentClass = reader.GetString(4);

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

        public void OnPost()
        {
            studentInfo.id = Request.Form["id"];
            studentInfo.name = Request.Form["name"];
            studentInfo.dob = Request.Form["dob"];
            studentInfo.gender = Request.Form["gender"];
            studentInfo.studentClass = Request.Form["studentClass"];

            //if (studentInfo.name.Length == 0 || studentInfo.dob.Length == 0 || studentInfo.gender.Length == 0 ||
            //    studentInfo.studentClass.Length == 0)
            //{
            //    errorMessage = "All fields are Required";
            //    return;
            //}

            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=student;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();                  

                    string sql = "UPDATE form SET name=@name, dob=@dob, gender=@gender, class=@studentClass WHERE id="+ studentInfo.id;

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {                        
                        command.Parameters.AddWithValue("@name", studentInfo.name);
                        command.Parameters.AddWithValue("@dob", studentInfo.dob);
                        command.Parameters.AddWithValue("@gender", studentInfo.gender);
                        command.Parameters.AddWithValue("@studentClass", studentInfo.studentClass);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.ToString());

            }

            Response.Redirect("/views/Index");
        }
    }
}
