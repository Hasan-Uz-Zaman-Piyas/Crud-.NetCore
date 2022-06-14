using CRUD.Models;
using System.Collections.Generic;

namespace CRUD.Services
{
    public interface IStudentServices
    {
        public List<StudentModel> GetStudentList();
        public string InsertStudent(StudentModel student);
        public string UpdateStudent(StudentModel student);
        public string DeleteStudent(int StudentId);
    }
}
