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

            /*
            var entry = context.AnimeLists.Where(l => l.Title == "CHENCHEN_v2").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "CHENCHEN_v2", ImageUrl = "http://i.imgur.com/SqlLnfo.jpg", SeriesType = SeriesType.Movie, Episodes = 1, Score = 6.9m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "Kuma_v69").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "Kuma_v69", ImageUrl = "http://i.imgur.com/RHx9BTk.jpg", SeriesType = SeriesType.TV, Episodes = 123, Score = 9.9m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "BEST GIRL").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "BEST GIRL", ImageUrl = "http://i.imgur.com/EOZQHI2.gif", SeriesType = SeriesType.TV, Episodes = 1, Score = 10m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "They see your o-chinchin").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "They see your o-chinchin", ImageUrl = "http://i.imgur.com/2bAuGQE.gif", SeriesType = SeriesType.TV, Episodes = 69, Score = 6.9m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "woof").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "woof", ImageUrl = "http://ih0.redbubble.net/image.14896453.9192/sticker,375x360.u1.png", SeriesType = SeriesType.TV, Episodes = 123, Score = 1m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "...").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "...", ImageUrl = "https://33.media.tumblr.com/b21b982ce3d4cdbd35e4e976031e12aa/tumblr_msiczqHAAD1r0bwkso1_400.gif", SeriesType = SeriesType.TV, Episodes = 12, Score = 8.7m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "tirami").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "tirami", ImageUrl = "http://i.imgur.com/DGXQ7Yy.png", SeriesType = SeriesType.Movie, Episodes = 1, Score = 6.96m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "tirami2").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "tirami2", ImageUrl = "http://i.imgur.com/2X1nRKx.png", SeriesType = SeriesType.TV, Episodes = 12, Score = 3.5m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "tirami3").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "tirami3", ImageUrl = "http://i.imgur.com/RdfD4xt.gif", SeriesType = SeriesType.TV, Episodes = 24, Score = 4.5m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "catcat").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "catcat", ImageUrl = "http://i.imgur.com/SNqajX4.gif", SeriesType = SeriesType.Movie, Episodes = 2, Score = 9.1m });
            }
            */
            var entry = context.AnimeLists.Where(l => l.Title == "Fullmetal Alchemist: Brotherhood").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "Fullmetal Alchemist: Brotherhood", ImageUrl = "http://cdn.myanimelist.net/images/anime/5/47421.jpg", SeriesType = SeriesType.TV, Episodes = 64, Score = 9.25m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "Steins;Gate").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "Steins;Gate", ImageUrl = "http://cdn.myanimelist.net/images/anime/5/73199.jpg", SeriesType = SeriesType.TV, Episodes = 24, Score = 9.18m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "Gintama°").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "Gintama°", ImageUrl = "http://cdn.myanimelist.net/images/anime/3/72078.jpg", SeriesType = SeriesType.TV, Episodes = 24, Score = 9.17m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "Ping Pong The Animation").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "Ping Pong The Animation", ImageUrl = "http://cdn.myanimelist.net/images/anime/10/58041.jpg", SeriesType = SeriesType.TV, Episodes = 11, Score = 8.70m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "Sen to Chihiro no Kamikakushi").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "Sen to Chihiro no Kamikakushi", ImageUrl = "http://cdn.myanimelist.net/images/anime/10/42079.jpg", SeriesType = SeriesType.Movie, Episodes = 1, Score = 8.93m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "Suzumiya Haruhi no Shoushitsu").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "Suzumiya Haruhi no Shoushitsu", ImageUrl = "http://cdn.myanimelist.net/images/anime/9/24646.jpg", SeriesType = SeriesType.Movie, Episodes = 1, Score = 8.88m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "Fate/Zero 2nd Season").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "Fate/Zero 2nd Season", ImageUrl = "http://cdn.myanimelist.net/images/anime/8/41125.jpg", SeriesType = SeriesType.TV, Episodes = 12, Score = 8.79m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "Shingeki no Kyojin").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "Shingeki no Kyojin", ImageUrl = "http://cdn.myanimelist.net/images/anime/10/47347.jpg", SeriesType = SeriesType.TV, Episodes = 25, Score = 8.68m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "Fate/Zero").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "Fate/Zero", ImageUrl = "http://cdn.myanimelist.net/images/anime/2/73249.jpg", SeriesType = SeriesType.TV, Episodes = 13, Score = 8.58m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "Kuroko no Basket").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "Kuroko no Basket", ImageUrl = "http://cdn.myanimelist.net/images/anime/11/50453.jpg", SeriesType = SeriesType.TV, Episodes = 25, Score = 8.57m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "...").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "...", ImageUrl = "https://33.media.tumblr.com/b21b982ce3d4cdbd35e4e976031e12aa/tumblr_msiczqHAAD1r0bwkso1_400.gif", SeriesType = SeriesType.TV, Episodes = 12, Score = 8.7m });
            }
            entry = context.AnimeLists.Where(l => l.Title == "No Game No Life").SingleOrDefault();
            if (entry == null)
            {
                context.AnimeLists.Add(new AnimeList { Title = "No Game No Life", ImageUrl = "http://cdn.myanimelist.net/images/anime/5/65187.jpg", SeriesType = SeriesType.TV, Episodes = 12, Score = 8.57m });
            }

            context.SaveChanges();
        }
    }
}
