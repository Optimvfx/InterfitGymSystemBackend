using DAL.Entities.Access;
using DAL.Entities.Access.AccessType;
using DAL.Entities.Primary;

namespace DAL.Extensions;

public static class ApplicationDbContextExtensions
{
      public static ApplicationDbContext AddAdminApiKey(this ApplicationDbContext db, string key)
      {
            db.Keys.Add(new ApiKey()
            {
                  Key = key,
                      Description = "This is main api key",
                  CreationDate = DateTime.Now,
                  DurationInDays = int.MaxValue,
                  Access = new Access()
                  {
                        Type = new ApiAdministrator()
                  }
            });

            db.SaveChanges();
            
            return db;
      }
}