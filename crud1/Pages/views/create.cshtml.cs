using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace crud1.Pages.views
{
    public class createModel : PageModel
    {
        StudentInfo studentInfo = new StudentInfo();
        public string errorMessage = " ";
        public string successMessage = " ";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            studentInfo.name = Request.Form["name"];
            studentInfo.dob = Request.Form["dob"];
            studentInfo.gender = Request.Form["gender"];
            studentInfo.studentClass = Request.Form["studentClass"];

            if (studentInfo.name.Length == 0 || studentInfo.dob.Length == 0 || studentInfo.gender.Length == 0 ||
                studentInfo.studentClass.Length == 0)
            {
                errorMessage = "All fields are Required";
                return;
            }

            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=student;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //string sql = "INSERT INTO table_name (column1, column2, column3)
                    //                VALUES(value1, value2, value3, ...) ";

                    //string sql = "INSERT INTO form" +
                    //    "(name, dob, gender, class) VALUES " +
                    //    "(@name, @dob, @gender, @studentClass);";

                    string sql = "INSERT INTO form(name, dob, gender, class) VALUES(@name, @dob, @gender, @studentClass)";

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

            studentInfo.name = ""; studentInfo.dob = ""; studentInfo.gender = ""; studentInfo.studentClass = "";
            successMessage = "student Added Successfully";

            //Response.Write("<script>alert('Inserted..');window.location = 'newpage.aspx';</script>"); //works great

           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

            Response.Redirect("/views/Index");

        }
    }
}
