using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TravelApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace TravelApi.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class ReviewsController : ControllerBase
  {
    private TravelApiContext _db;

    public ReviewsController(TravelApiContext db)
    {
      _db = db;
    }

    //GET api/reviews -- Get reviews
    [HttpGet]
    public ActionResult<IEnumerable<Review>> Get(string locationCity, string locationCountry, string userName, bool random)
    {
      var query = _db.Reviews.AsQueryable();
      if (locationCity != null)
      {
        query = query.Where(entry => entry.LocationCity == locationCity);
      }
      if (locationCountry != null)
      {
        query = query.Where(entry => entry.LocationCountry == locationCountry);
      }
      if (userName != null)
      {
        query = query.Where(entry => entry.UserName == userName);
      }
      if (random)
      {
        Random rdn = new Random();
        int MaxId = _db.Reviews.Max(entry=>entry.ReviewId);
        query = query.Where(entry=>entry.ReviewId == rdn.Next(MaxId));
      }
      return query.ToList();

    }

    //POST api/reviews  -- Add Reviews
    [HttpPost]
    public void Post([FromBody] Review review)
    {
      _db.Reviews.Add(review);
      _db.SaveChanges();
    }

    //Get api/reviews/5  -- get review by id
    [HttpGet("{id}")]
    public ActionResult<Review> Get(int id)
    {
      return _db.Reviews.FirstOrDefault(entry => entry.ReviewId == id);
    }

    //PUT api/reviews/5 -- update review by id
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Review review, string userName)
    {
      review.ReviewId = id;
      if(review.UserName == userName)
      {
        _db.Entry(review).State = EntityState.Modified;
        _db.SaveChanges();
      }
    }

    //DELETE api/reviews/5 --delete review by id\
    [HttpDelete("{id}")]
    public void Delete(int id, string userName)
    {
      var reviewToDelete = _db.Reviews.FirstOrDefault(entry=>entry.ReviewId == id);

      if(reviewToDelete.UserName == userName)
      {
        _db.Reviews.Remove(reviewToDelete);
        _db.SaveChanges();
      }
    }
  }
}