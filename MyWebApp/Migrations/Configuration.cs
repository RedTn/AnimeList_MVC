using System.Web;
namespace MyWebApp.Migrations
{
    using MyWebApp.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Net;

    internal sealed class Configuration : DbMigrationsConfiguration<MyWebApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            //Appharbor, same as Update-Database -Force
            AutomaticMigrationDataLossAllowed = true; 

            ContextKey = "MyWebApp.Models.ApplicationDbContext";
        }

        protected override void Seed(MyWebApp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var entry = context.AnimeLists.Where(l => l.Title == "CHENCHEN_v2").SingleOrDefault();
            if (entry != null)
            {
                entry.ImageUrl = "http://i.imgur.com/SqlLnfo.jpg";
            }
            entry = context.AnimeLists.Where(l => l.Title == "Kuma_v69").SingleOrDefault();
            if (entry != null)
            {
                entry.ImageUrl = "http://i.imgur.com/RHx9BTk.jpg";
            }
            entry = context.AnimeLists.Where(l => l.Title == "BEST GIRL").SingleOrDefault();
            if (entry != null)
            {
                entry.ImageUrl = "http://i.imgur.com/EOZQHI2.gif";
            }
            entry = context.AnimeLists.Where(l => l.Title == "They see your dick").SingleOrDefault();
            if (entry != null)
            {
                entry.ImageUrl = "http://i.imgur.com/2bAuGQE.gif";
            }
            entry = context.AnimeLists.Where(l => l.Title == "woof").SingleOrDefault();
            if (entry != null)
            {
                entry.ImageUrl = "http://ih0.redbubble.net/image.14896453.9192/sticker,375x360.u1.png";
            }
            entry = context.AnimeLists.Where(l => l.Title == "...").SingleOrDefault();
            if (entry != null)
            {
                entry.ImageUrl = "https://33.media.tumblr.com/b21b982ce3d4cdbd35e4e976031e12aa/tumblr_msiczqHAAD1r0bwkso1_400.gif";
            }
        }
    }
}
