using Microsoft.EntityFrameworkCore;

namespace TravelApi.Models
{
  public class TravelApiContext : DbContext
  {
    public TravelApiContext(DbContextOptions <TravelApiContext> options)
    :base(options)
    {

    }
    public DbSet<Review> Reviews {get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Review>()
        .HasData(
          new Review {ReviewId=4, UserName="Test1", LocationCity="Chicago", LocationCountry="USA", ReviewText="asdfsda" } 
        );
    }
  }
}