using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            studentInfo.studentClass = Request.Form["class"];

            if (studentInfo.name.Length == 0 || studentInfo.dob.Length == 0 || studentInfo.gender.Length == 0 ||
                studentInfo.studentClass.Length == 0)
            {
                errorMessage = "All fields are Required";
                return;
            }

            studentInfo.name = ""; studentInfo.dob = ""; studentInfo.gender = ""; studentInfo.studentClass = "";
            successMessage = "student Added Successfully";

        }
    }
}
