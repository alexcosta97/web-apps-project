using src.Models;
using System;
using System.Linq;

namespace src.Data
{
    public class DbInitializer
    {
        public static void Initialize (ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Before populating, make sure there is no data already
            if (context.Address.Any())
            {
                return; // DB has been seeded
            }

            var Address = new Address[]
            {
                /*
                new Address{
                }
                */
            };

            if (context.Favourite.Any())
            {
                return;
            }

            var Favourite = new Favourite[]
            {
            };

            if (context.Line.Any())
            {
                return;
            }

            var Line = new Line[]
            {
            };

            if (context.Route.Any())
            {
                return;
            }

            var Route = new Route[]
            {
            };

            if (context.RouteStop.Any())
            {
                return;
            }

            var RouteStop = new RouteStop[]
            {
            };

            if (context.Staff.Any())
            {
                return;
            }

            var Staff = new Staff[]
            {
            };

            if (context.Stop.Any())
            {
                return;
            }

            var Stop = new Stop[]
            {
            };
        }
    }
}
