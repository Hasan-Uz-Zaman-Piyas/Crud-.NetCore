using CRUD.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CRUD.Services
{
    public  class StudentServices : IStudentServices
    {
        private readonly IConfiguration _configuration;

        public StudentServices(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DBConnsction");
            provideName = "System.Data.SqlClient";
        }

        public string ConnectionString { get; }
        public string provideName { get; }

        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public string DeleteStudent(int StudentId)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var Stdnt = dbConnection.Query<StudentModel>("DeleteStudent",
                        new
                        {
                            StudentId = StudentId
                        },
                        commandType: CommandType.StoredProcedure);
                    if (Stdnt != null && Stdnt.FirstOrDefault().Response == "Delete Sucessfully")
                    {
                        result = "Delete Sucessfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {

                string errorMsg = ex.Message;
                return errorMsg;
            }
        }

        public List<StudentModel> GetStudentList()
        {
            List<StudentModel> students = new List<StudentModel>();
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    students = dbConnection.Query<StudentModel>("GetStudentList", commandType: CommandType.StoredProcedure).ToList();
                    dbConnection.Close();
                    return students;
                }
            }
            catch (Exception ex)
            {

                string errorMsg = ex.Message;
                return students;
            }

        }
        public string InsertStudent(StudentModel student)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var Stdnt = dbConnection.Query<StudentModel>("InsertStudent",
                        new { 
                            FullName = student.FullName,
                            EmailAddress = student.EmailAddress, 
                            City = student.City,
                            CreatedBy = student.CreatedBy 
                        },
                        commandType: CommandType.StoredProcedure);
                    if (Stdnt != null && Stdnt.FirstOrDefault().Response == "Save Sucessfully")
                    {
                        result = "Save Sucessfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {

                string errorMsg = ex.Message;
                return errorMsg;
            }
        }

        public string UpdateStudent(StudentModel student)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var Stdnt = dbConnection.Query<StudentModel>("UpdateStudent",
                        new { FullName = student.FullName, EmailAddress = student.EmailAddress, City = student.City, CreatedBy = student.CreatedBy, StudentId = student.StudentId }, commandType: CommandType.StoredProcedure);
                    if (Stdnt != null && Stdnt.FirstOrDefault().Response == "Save Sucessfully")
                    {
                        result = "Save Sucessfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {

                string errorMsg = ex.Message;
                return errorMsg;
            }

        }
    }
}
