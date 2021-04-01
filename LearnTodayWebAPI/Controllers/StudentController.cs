using LearnTodayWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LearnTodayWebAPI.Controllers
{
    public class StudentController : ApiController
    {
        public IEnumerable<Course> GetAllCourses()
        {
            var db = new LearnTodayWebAPIDbEntities();
            return db.Courses.ToList().OrderBy(s => s.Start_Date);
        }
        public HttpResponseMessage Post([FromBody] Student model)
        {
            try
            {
                using (LearnTodayWebAPIDbEntities db = new LearnTodayWebAPIDbEntities())
                {
                    db.Students.Add(model);
                    db.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, model);
                    return message;
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (LearnTodayWebAPIDbEntities db = new LearnTodayWebAPIDbEntities())
                {
                    var entity = db.Students.FirstOrDefault(s => s.EnrollmentId == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No enrollement information found");
                    }
                    else
                    {
                        db.Students.Remove(entity);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
