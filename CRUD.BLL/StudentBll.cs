using CRUD.Models;
using CRUD.Services;
using System;
using System.Collections.Generic;

namespace CRUD.BLL
{
    public class StudentBll
    {
        private readonly IStudentServices _studentServices;

        public StudentBll(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        public List<StudentModel> GetStudentList()
        {
            return _studentServices.GetStudentList();
        }
        public string InsertStudent(StudentModel student)
        {
            return _studentServices.InsertStudent(student);
        }

        public string UpdateStudent(StudentModel model)
        {
            return _studentServices.UpdateStudent(model);
        }

        public string DeleteStudent(int StudentId)
        {
            return _studentServices.DeleteStudent(StudentId);
        }
       
    }
}
