using Microsoft.EntityFrameworkCore;
using TradeMatchLogin.Models;

namespace TradeMatchLogin.DataContext;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new TradeMatchContext(
            serviceProvider.GetRequiredService<DbContextOptions<TradeMatchContext>>());

        // Look for any movies.
        if (context.User.Any())
            return; // DB has been seeded.

        context.User.AddRange(
            new User
            {
                
                FirstName = "David",
                LastName = "Jones",
                Phone = "0422 123 345",
                Email = "david@yahoo.com.au",
                Status = "Registered"
            },
            new User
            {
                FirstName = "Sarah",
                LastName = "Heart",
                Phone = "0424 218 376",
                Email = "sarah@yahoo.com.au",
                ABN = "111-222-333",
                BusinessName = "Clear Screen window cleening",
                Status = "Registered"
            }
        );
        context.SaveChanges();

        context.Login.AddRange(
            new Login
            {
                UserName = "DavidIsTheMan",
                Password = "Dave1234",
                UserID = 1,
            },
            new Login
            {
                UserName = "ClearScreenForever",
                Password = "clear1234",
                UserID = 2,
            }
        );
        context.SaveChanges();

        context.Role.AddRange(
            new Role
            {
                RoleType = "Client",
                UserID = 1,
            },
            new Role
            {
                RoleType = "Service",
                UserID = 2,
            }
        );
        context.SaveChanges();

        context.Address.AddRange(
           new Address
           {
               Number = "23",
               Street = "Miller Street",
               Suburb =  "Geelong",
               PostCode = 3221,
               State = "VIC",
               UserID = 1,
           },
           new Address
           {
               Number = "55",
               Street = "Ryde Road",
               Suburb = "Gosford",
               PostCode = 2250,
               State = "VIC",
               UserID = 2,
           }
       );
        context.SaveChanges();
    }
}
