using CRUD.Models;
using CRUD.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace CRUD.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IStudentServices _studentServices;
        public StudentController(IConfiguration configuration, IStudentServices studentServices)
        {
            _configuration = configuration;
            _studentServices = studentServices;
        }

        public IActionResult Index()
        {
            StudentViewModel model = new StudentViewModel();  
            model.StudentsList = _studentServices.GetStudentList().ToList();
            return View(model);
        }
       [HttpPost]
       public IActionResult AddUpdateStudent(StudentModel student)
        {
            StudentViewModel model = new StudentViewModel();
            student.CreatedBy = 1;
            string url = Request.Headers["Referer"].ToString();
            string result = string.Empty;
            if (student.StudentId > 0)
            {
                result = _studentServices.UpdateStudent(student);
            }
            else
            {
                result = _studentServices.InsertStudent(student);
            }
            if(result == "Save Sucessfully")
            {
                TempData["SuccessMsg"] = "Save Sucessfully";
                return Redirect(url);
            }
            else
            {
                TempData["ErrorMsg"] = result;
                return Redirect(url);
            }
        }
        [HttpPost]
        public IActionResult DeleteStudent(int StudentId)
        {
            string url = Request.Headers["Referer"].ToString();
            string result = _studentServices.DeleteStudent(StudentId);

            if (result == "Delete Sucessfully")
            {
                return Json(new { message = "Delete Sucessfully" });
            }
            else
            {
                return Json(new { message = "Error Occured" });
            }
        }

    }
}
