using LearnTodayWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LearnTodayWebAPI.Controllers
{
    public class TrainerController : ApiController
    {
        public HttpResponseMessage Put(int id, [FromBody] Trainer trainer)
        {
            try
            {
                using (LearnTodayWebAPIDbEntities dbContext = new LearnTodayWebAPIDbEntities())
                {
                    var entity = dbContext.Trainers.FirstOrDefault(s => s.TrainerId == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Trainer with Id" + id.ToString() + " not found to updaate");
                    }
                    else
                    {
                        entity.Password = trainer.Password;

                        dbContext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Post([FromBody] Trainer trainer)
        {
            try
            {
                using(LearnTodayWebAPIDbEntities dbContext = new LearnTodayWebAPIDbEntities())
                {
                    dbContext.Trainers.Add(trainer);
                    dbContext.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, trainer);
                    return message;
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
